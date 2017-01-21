using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Utils;

namespace DataSync
{
    public class DataSyncService
    {
        private static object objLock = new object();
        private static DataSyncService _intance;


        private DataSyncService() { }

        /// <summary>
        /// 单例访问入口
        /// </summary>
        public static DataSyncService Instance
        {
            get
            {
                lock (objLock)
                {
                    if (_intance == null)
                    {
                        _intance = new DataSyncService();
                    }
                }
                return _intance;
            }
        }


        private List<string> FundCodeList = null;
        private List<int> FundIdList = null;
        private Dictionary<int,FundProduct> FundProductDic = null;
        private List<FUND_MainInfo> FUND_MainInfo_List = null;
        private List<FUND_UnitClassInfo> FUND_UnitClassInfo_List = null;

        private FundCodeDAL fundCodeDAL = new FundCodeDAL();
        private FundProductDAL fundProductDAL = new FundProductDAL();
        private FundProductDetailDAL fundProductDetailDAL = new FundProductDetailDAL();
        private FundDataDAL fundDataDAL = new FundDataDAL();

        /// <summary>
        /// 初始化服务
        /// </summary>
        /// <returns>是否初始化成功</returns>
        public bool Initialize()
        {
            bool result = true;
            try
            {
                //初始化XML文件读取
                XMLHandler.Instance.Init();
                // 获取需要同步的基金代码
                FundCodeList = fundCodeDAL.GetFundCodeList();
                //FundCodeList = new List<string>();
                //FundCodeList.Add("000061");//混合
                //FundCodeList.Add("090005");//货币
                //获取Train库的所有数据
                FundProductDic = fundProductDAL.GetList().ToDictionary(x => x.FundId);

                StartSync();
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("数据同步服务初始化异常", ex);
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 开始数据同步
        /// </summary>
        public void StartSync()
        {
            try
            {
                //基金主体信息表对应字段
                string MainInfo_TableName = string.Empty;
                Dictionary<DataEnums.MainInfo, string> dtMainInfo = XMLHandler.Instance.GetMainInfoNames(out MainInfo_TableName);
                string selectMainInfoFiled = string.Join(",", dtMainInfo.Values);
                FUND_MainInfo_List = fundDataDAL.GetFundMainInfoList(MainInfo_TableName, selectMainInfoFiled, FundCodeList);
                FundIdList = FUND_MainInfo_List.Select(x => x.FUNDID).ToList();

                //基金份额类别信息表
                string UnitClassInfo_TableName = string.Empty;
                Dictionary<DataEnums.UnitClassInfo, string> dtUnitClassInfo = XMLHandler.Instance.GetUnitClassInfoNames(out UnitClassInfo_TableName);
                string selectUnitClassInfoFiled = string.Join(",", dtUnitClassInfo.Values);
                FUND_UnitClassInfo_List = fundDataDAL.GetFundUnitClassInfoList(UnitClassInfo_TableName, selectUnitClassInfoFiled, FundIdList);

                //更新数据
                foreach (var main in FUND_MainInfo_List)
                {
                    LogHelper.Log.WriteInfo("开始更新基金,ID:" + main.FUNDID);
                    if (FundProductDic.ContainsKey(main.FUNDID))
                    {
                        #region 更新
                        Fund_FeesChange fee = fundDataDAL.GetFund_FeesChangeModel(main.FUNDID);

                        FundProduct model = FundProductDic[main.FUNDID];
                        model.HostingFees = fee.HostingFees;
                        model.PurchaseShares = fee.PurchaseShares;

                        #region 详细信息
                        List<FundProductDetail> list = new List<FundProductDetail>();
                        List<FUND_NAV> navList = null;

                        string NAV_TableName = string.Empty;
                        Dictionary<DataEnums.NAV, string> dtNAV = XMLHandler.Instance.GetNAVNames(out NAV_TableName);
                        string selectNAVFiled = string.Join(",", dtNAV.Values);

                        var lastDetail = fundProductDetailDAL.GetLastDetail(main.FUNDID);
                        if (lastDetail != null)
                        {
                            navList = fundDataDAL.GetFundNAVList(NAV_TableName, selectNAVFiled, main.FUNDID, lastDetail.UpdateDate);
                        }
                        else
                        {
                            navList = fundDataDAL.GetFundNAVList(NAV_TableName, selectNAVFiled, main.FUNDID);
                        }

                        if (navList != null && navList.Count > 0)
                        {
                            var nav = navList.OrderByDescending(x => x.TRADINGDATE).FirstOrDefault();
                            model.NewNetValue = nav.NAV;
                            model.TotalNewValue = nav.AccumulativeNAV;
                            model.NavUpdateDate = nav.TRADINGDATE;

                            foreach (var item in navList)
                            {
                                FundProductDetail entity = new FundProductDetail();
                                entity.FundId = main.FUNDID;
                                entity.NewNetValue = item.NAV;
                                entity.TotalNewValue = item.AccumulativeNAV;
                                if (model.FundType.Equals("货币型基金"))
                                {
                                    //7日年化收益率
                                    entity.AnnualizedYield = item.ANNUALIZEDYIELD;
                                    entity.YearlyEarningsRate = item.ANNUALIZEDYIELD;
                                }
                                else
                                {
                                    //计算1年收益率
                                    var y_nav = fundDataDAL.GetFundNAV(NAV_TableName, selectNAVFiled, main.FUNDID, item.TRADINGDATE.Date.AddYears(-1));
                                    if (y_nav != null && y_nav.NAV != null && y_nav.NAV.Value > 0)
                                    {
                                        entity.YearlyEarningsRate = Math.Round((item.AccumulativeNAV.Value - y_nav.AccumulativeNAV.Value) / y_nav.NAV.Value, 4);
                                    }
                                    else
                                    {
                                        if (list.Count > 0)
                                        {
                                            entity.YearlyEarningsRate = list.Last().YearlyEarningsRate;
                                        }
                                        else
                                        {
                                            var temp = fundProductDetailDAL.GetLastDetail(main.FUNDID);
                                            if (temp != null)
                                            {
                                                entity.YearlyEarningsRate = temp.YearlyEarningsRate;
                                            }
                                        }
                                    }
                                }
                                entity.UpdateDate = item.TRADINGDATE;
                                list.Add(entity);
                            }

                            model.YearlyEarningsRate = list.OrderByDescending(x => x.UpdateDate).FirstOrDefault().YearlyEarningsRate;
                        }

                        #endregion

                        //保存
                        fundProductDAL.Update(model);
                        fundProductDetailDAL.AddList(list);


                        #endregion
                    }
                    else
                    {
                        #region 新增
                        FUND_UnitClassInfo unit = FUND_UnitClassInfo_List.FirstOrDefault(x => x.FUNDID == main.FUNDID);
                        Fund_FeesChange fee = fundDataDAL.GetFund_FeesChangeModel(main.FUNDID);

                        FundProduct model = new FundProduct();
                        model.FundId = main.FUNDID;
                        model.FundType = main.CATEGORY;
                        model.FundCompany = main.FUNDCOMPANYNAME;

                        model.HostingFees = fee.HostingFees;
                        model.PurchaseShares = fee.PurchaseShares;

                        if (unit != null)
                        {
                            model.FundName = unit.SHORTNAME;
                            model.FundCode = unit.SYMBOL;
                        }


                        #region 详细信息
                        List<FundProductDetail> list = new List<FundProductDetail>();

                        string NAV_TableName = string.Empty;
                        Dictionary<DataEnums.NAV, string> dtNAV = XMLHandler.Instance.GetNAVNames(out NAV_TableName);
                        string selectNAVFiled = string.Join(",", dtNAV.Values);
                        List<FUND_NAV> navList = fundDataDAL.GetFundNAVList(NAV_TableName, selectNAVFiled, main.FUNDID);

                        if (navList != null && navList.Count > 0)
                        {
                            var nav = navList.OrderByDescending(x => x.TRADINGDATE).FirstOrDefault();
                            model.NewNetValue = nav.NAV;
                            model.TotalNewValue = nav.AccumulativeNAV;
                            model.NavUpdateDate = nav.TRADINGDATE;

                            foreach (var item in navList)
                            {
                                FundProductDetail entity = new FundProductDetail();
                                entity.FundId = main.FUNDID;
                                entity.NewNetValue = item.NAV;
                                entity.TotalNewValue = item.AccumulativeNAV;
                                if (model.FundType.Equals("货币型基金"))
                                {
                                    //7日年化收益率
                                    entity.AnnualizedYield = item.ANNUALIZEDYIELD;
                                    entity.YearlyEarningsRate = item.ANNUALIZEDYIELD;
                                }
                                else
                                {
                                    //计算1年收益率
                                    var y_nav = navList.FirstOrDefault(x => x.TRADINGDATE.Date == item.TRADINGDATE.Date.AddYears(-1));
                                    if (y_nav != null && y_nav.NAV != null && y_nav.NAV.Value > 0)
                                    {
                                        entity.YearlyEarningsRate = Math.Round((item.AccumulativeNAV.Value - y_nav.AccumulativeNAV.Value) / y_nav.NAV.Value, 4);
                                    }
                                    else
                                    {
                                        if (list.Count > 0)
                                        {
                                            entity.YearlyEarningsRate = list.Last().YearlyEarningsRate;
                                        }
                                    }
                                }
                                entity.UpdateDate = item.TRADINGDATE;
                                list.Add(entity);
                            }

                            model.YearlyEarningsRate = list.OrderByDescending(x => x.UpdateDate).FirstOrDefault().YearlyEarningsRate;
                        }

                        #endregion

                        //保存
                        fundProductDAL.Add(model);
                        fundProductDetailDAL.AddList(list);

                        //缓存
                        FundProductDic.Add(model.FundId, model);

                        #endregion
                    }
                }
                LogHelper.Log.WriteInfo("基金更新完毕");
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("数据同步服务操作异常", ex);
            }
        }

    }
}
