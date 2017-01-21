using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Utils;
using System.Data;

namespace DataSync
{
    public class FundDataDAL
    {
        public string dataSyncDBConnString = string.Empty;
        public FundDataDAL()
        {
            dataSyncDBConnString = AppSettingsHelper.GetStringByKey("DataSync.SQL", "");
        }

        /// <summary>
        /// 获取基金主体信息表信息
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="selectFiled">查询字段</param>
        /// <param name="fundCodeList">基金代码</param>
        /// <returns></returns>
        public List<FUND_MainInfo> GetFundMainInfoList(string table, string selectFiled, List<string> fundCodeList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ").Append(selectFiled).Append(" from ").Append(table);
            strSql.Append(" where MASTERFUNDCODE in @ids");

            List<FUND_MainInfo> result = new List<FUND_MainInfo>();
            using (var conn = DBHelper.CreateConnection(dataSyncDBConnString))
            {
                conn.Open();
                result = conn.Query<FUND_MainInfo>(strSql.ToString(), new { ids = fundCodeList.ToArray() }).ToList();
            }

            return result;

        }

        /// <summary>
        /// 获取基金主体信息表信息
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="selectFiled">查询字段</param>
        /// <param name="fundIdList">基金代码</param>
        /// <returns></returns>
        public List<FUND_UnitClassInfo> GetFundUnitClassInfoList(string table, string selectFiled, List<int> fundIdList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ").Append(selectFiled).Append(" from ").Append(table);
            strSql.Append(" where FUNDID in @ids");

            List<FUND_UnitClassInfo> result = new List<FUND_UnitClassInfo>();
            using (var conn = DBHelper.CreateConnection(dataSyncDBConnString))
            {
                conn.Open();
                result = conn.Query<FUND_UnitClassInfo>(strSql.ToString(), new { ids = fundIdList.ToArray() }).ToList();
            }

            return result;

        }

        /// <summary>
        /// 获取基金日净值文件
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="selectFiled">查询字段</param>
        /// <param name="fundId">基金Id</param>
        /// <returns></returns>
        public List<FUND_NAV> GetFundNAVList(string table, string selectFiled, int fundId, DateTime? lastUpdate = null)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ").Append(selectFiled).Append(" from ").Append(table);
            strSql.Append(" where FUNDID=@ID");
            strSql.Append(" and TRADINGDATE > @before");
            strSql.Append(" and TRADINGDATE < @after");
            strSql.Append(" order by TRADINGDATE ");

            List<FUND_NAV> result = new List<FUND_NAV>();
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection(dataSyncDBConnString))
            {
                conn.Open();
                param.Add("@ID", fundId, dbType: DbType.Int32);
                if (lastUpdate.HasValue)
                {
                    param.Add("@before", lastUpdate.Value.Date, dbType: DbType.DateTime);
                }
                else
                {
                    param.Add("@before", DateTime.Now.Date.AddYears(-2).AddMonths(-6), dbType: DbType.DateTime);
                }
                param.Add("@after", DateTime.Now.Date, dbType: DbType.DateTime);
                result = conn.Query<FUND_NAV>(strSql.ToString(), param).ToList();
            }

            return result;
        }


        /// <summary>
        /// 获取基金日净值文件
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="selectFiled">查询字段</param>
        /// <param name="fundId">基金Id</param>
        /// <param name="date">净值日期</param>
        /// <returns></returns>
        public FUND_NAV GetFundNAV(string table, string selectFiled, int fundId, DateTime date)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 ").Append(selectFiled).Append(" from ").Append(table);
            strSql.Append(" where FUNDID=@ID");
            strSql.Append(" and TRADINGDATE = @date");
            strSql.Append(" order by TRADINGDATE desc");

            FUND_NAV result = new FUND_NAV();
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection(dataSyncDBConnString))
            {
                conn.Open();
                param.Add("@ID", fundId, dbType: DbType.Int32);
                param.Add("@date", date.Date, dbType: DbType.DateTime);
                result = conn.Query<FUND_NAV>(strSql.ToString(), param).FirstOrDefault();
            }

            return result;
        }


        /// <summary>
        /// 获取托管费率、最低申购份额
        /// </summary>
        /// <param name="FundId">基金Id</param>
        /// <returns></returns>
        public Fund_FeesChange GetFund_FeesChangeModel(int FundId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 PROPORTIONOFFEE from Fund_FeesChange where TYPEOFFEE='B1805' and FUNDID=@FUNDID order by UPDATETIME desc; ");//托管费率
            strSql.Append("select top 1 PROPORTIONOFFEE from Fund_FeesChange where TYPEOFFEE='B1807' and FUNDID=@FUNDID order by UPDATETIME desc; ");//最低申购份额

            Fund_FeesChange model = new Fund_FeesChange();
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection(dataSyncDBConnString))
            {
                conn.Open();
                param.Add("@FUNDID", FundId, dbType: DbType.Int32);
                using (var multi = conn.QueryMultiple(strSql.ToString(), param))
                {
                    model.HostingFees = multi.Read<string>().FirstOrDefault();
                    model.PurchaseShares = multi.Read<string>().FirstOrDefault();
                }
            }
            return model;
        }



    }
}
