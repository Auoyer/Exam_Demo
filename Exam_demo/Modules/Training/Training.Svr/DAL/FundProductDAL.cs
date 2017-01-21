using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using Training.API;

namespace Training.Svr
{
    /// <summary>
    /// 数据访问类:FundProduct
    /// </summary>
    public partial class FundProductDAL
    {
        public FundProductDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int FundId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from FundProduct where FundId=@FundId ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@FundId", FundId, dbType: DbType.Int32);
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result > 0;
        }

        #endregion

        #region  新增

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(FundProduct model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FundProduct(");
            strSql.Append("FundId,FundCode,FundName,FundType,NewNetValue,TotalNewValue,NavUpdateDate,HostingFees,PurchaseShares,FundCompany,YearlyEarningsRate,UpdateDate)");

            strSql.Append(" values (");
            strSql.Append("@FundId,@FundCode,@FundName,@FundType,@NewNetValue,@TotalNewValue,@NavUpdateDate,@HostingFees,@PurchaseShares,@FundCompany,@YearlyEarningsRate,@UpdateDate)");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@FundId", model.FundId, dbType: DbType.Int32);
                param.Add("@FundCode", model.FundCode, dbType: DbType.String);
                param.Add("@FundName", model.FundName, dbType: DbType.String);
                param.Add("@FundType", model.FundType, dbType: DbType.String);
                param.Add("@NewNetValue", model.NewNetValue, dbType: DbType.Decimal);
                param.Add("@TotalNewValue", model.TotalNewValue, dbType: DbType.Decimal);
                param.Add("@NavUpdateDate", model.NavUpdateDate, dbType: DbType.DateTime);
                param.Add("@HostingFees", model.HostingFees, dbType: DbType.String);
                param.Add("@PurchaseShares", model.PurchaseShares, dbType: DbType.String);
                param.Add("@FundCompany", model.FundCompany, dbType: DbType.String);
                param.Add("@YearlyEarningsRate", model.YearlyEarningsRate, dbType: DbType.Decimal);
                param.Add("@UpdateDate", model.UpdateDate, dbType: DbType.DateTime);
                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(strSql.ToString(), param);
            }
            return result;
        }
        #endregion

        #region  更新
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(FundProduct model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FundProduct set ");
            strSql.Append("FundCode=@FundCode,");
            strSql.Append("FundName=@FundName,");
            strSql.Append("FundType=@FundType,");
            strSql.Append("NewNetValue=@NewNetValue,");
            strSql.Append("TotalNewValue=@TotalNewValue,");
            strSql.Append("NavUpdateDate=@NavUpdateDate,");
            strSql.Append("HostingFees=@HostingFees,");
            strSql.Append("PurchaseShares=@PurchaseShares,");
            strSql.Append("FundCompany=@FundCompany,");
            strSql.Append("YearlyEarningsRate=@YearlyEarningsRate,");
            strSql.Append("UpdateDate=@UpdateDate");
            strSql.Append(" where FundId=@FundId ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@FundId", model.FundId, dbType: DbType.Int32);
                param.Add("@FundCode", model.FundCode, dbType: DbType.String);
                param.Add("@FundName", model.FundName, dbType: DbType.String);
                param.Add("@FundType", model.FundType, dbType: DbType.String);
                param.Add("@NewNetValue", model.NewNetValue, dbType: DbType.Decimal);
                param.Add("@TotalNewValue", model.TotalNewValue, dbType: DbType.Decimal);
                param.Add("@NavUpdateDate", model.NavUpdateDate, dbType: DbType.DateTime);
                param.Add("@HostingFees", model.HostingFees, dbType: DbType.String);
                param.Add("@PurchaseShares", model.PurchaseShares, dbType: DbType.String);
                param.Add("@FundCompany", model.FundCompany, dbType: DbType.String);
                param.Add("@YearlyEarningsRate", model.YearlyEarningsRate, dbType: DbType.Decimal);
                param.Add("@UpdateDate", model.UpdateDate, dbType: DbType.DateTime);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        #endregion

        #region  删除
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int FundId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from FundProduct ");
            strSql.Append(" where FundId=@FundId ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@FundId", FundId, dbType: DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        #endregion

        #region  获取实体
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public FundProduct GetModel(int FundId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from FundProduct where FundId=@FundId; ");
            strSql.Append("select * from FundProductDetail where UpdateDate >= @before and FundId=@FundId; ");

            FundProduct model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@FundId", FundId, dbType: DbType.Int32);
                param.Add("@before", DateTime.Now.Date.AddYears(-1), dbType: DbType.DateTime);
                using (var multi = conn.QueryMultiple(strSql.ToString(), param))
                {
                    model = multi.Read<FundProduct>().FirstOrDefault();
                    if (model != null)
                    {
                        model.FundProductDetail = multi.Read<FundProductDetail>().ToList();
                    }
                }
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<FundProduct> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM FundProduct ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<FundProduct> list = new List<FundProduct>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<FundProduct>(strSql.ToString()).ToList();
            }
            return list;
        }

        /// <summary>
        /// 根据CustomFilter获取where语句
        /// </summary>
        private string GetStrWhere(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (filter.FundType != null && filter.FundType.Count > 0)
            {
                //过滤危险字段，如单引号等
                List<string> result = new List<string>();
                filter.FundType.ForEach(x =>
                {
                    result.Add("'" + x.Replace("'", "''") + "'");
                });

                strSql.AppendFormat(" and FundType in ({0}) ", string.Join(",", result));
            }
            if (!string.IsNullOrEmpty(filter.KeyWords))
            {
                //过滤危险字段，如单引号等
                string key = filter.KeyWords.Replace("'", "''");
                if (filter.FundType != null && filter.FundType.Count > 0)
                {
                    //产品、代码
                    strSql.AppendFormat(" and (FundName like '%{0}%' or FundCode like '%{0}%')", key);
                }
                else
                {
                    //产品、代码、类型
                    strSql.AppendFormat(" and (FundName like '%{0}%' or FundType like '%{0}%' or FundCode like '%{0}%')", key);
                }
            }
            return strSql.ToString();
        }

        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetFundProductPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "FundProduct";
            model.PKey = "FundId";
            model.Sort = "FundId";
            model.Fields = "FundId,FundCode,FundName,FundType,NewNetValue,TotalNewValue,NavUpdateDate,HostingFees,PurchaseShares,FundCompany,YearlyEarningsRate";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion


    }
}

