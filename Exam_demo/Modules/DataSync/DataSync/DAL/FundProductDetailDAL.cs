using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using Utils;

namespace DataSync
{
    /// <summary>
    /// 数据访问类:FundProductDetail
    /// </summary>
    public partial class FundProductDetailDAL
    {
        public string trainDBConnString = string.Empty;
        public FundProductDetailDAL()
        {
            trainDBConnString = AppSettingsHelper.GetStringByKey("Training.Svr.SQL", "");
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from FundProductDetail where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection(trainDBConnString))
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result > 0;
        }

        #endregion

        #region  新增

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(FundProductDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FundProductDetail(");
            strSql.Append("FundId,NewNetValue,YearlyEarningsRate,UpdateDate)");

            strSql.Append(" values (");
            strSql.Append("@FundId,@NewNetValue,@YearlyEarningsRate,@UpdateDate)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection(trainDBConnString))
            {
                conn.Open();
                param.Add("@FundId", model.FundId, dbType: DbType.Int32);
                param.Add("@NewNetValue", model.NewNetValue, dbType: DbType.Decimal);
                param.Add("@YearlyEarningsRate", model.YearlyEarningsRate, dbType: DbType.Decimal);
                param.Add("@UpdateDate", model.UpdateDate, dbType: DbType.DateTime);
                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(strSql.ToString(), param);
                result = param.Get<int>("@returnid");
            }
            return result;
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="list"></param>
        public void AddList(List<FundProductDetail> list)
        {
            StringBuilder strSql = new StringBuilder();
            var param = new DynamicParameters();

            using (var conn = DBHelper.CreateConnection(trainDBConnString))
            {
                conn.Open();
                var tran = conn.BeginTransaction();

                try
                {
                    if (list != null && list.Count > 0)
                    {
                        foreach (var item in list)
                        {
                            strSql.Clear();
                            param = new DynamicParameters();

                            strSql.Append("insert into FundProductDetail(");
                            strSql.Append("FundId,NewNetValue,TotalNewValue,AnnualizedYield,YearlyEarningsRate,UpdateDate)");
                            strSql.Append(" values (");
                            strSql.Append("@FundId,@NewNetValue,@TotalNewValue,@AnnualizedYield,@YearlyEarningsRate,@UpdateDate)");

                            param.Add("@FundId", item.FundId, dbType: DbType.Int32);
                            param.Add("@NewNetValue", item.NewNetValue, dbType: DbType.Decimal);
                            param.Add("@TotalNewValue", item.TotalNewValue, dbType: DbType.Decimal);
                            param.Add("@AnnualizedYield", item.AnnualizedYield, dbType: DbType.Decimal);
                            param.Add("@YearlyEarningsRate", item.YearlyEarningsRate, dbType: DbType.Decimal);
                            param.Add("@UpdateDate", item.UpdateDate, dbType: DbType.DateTime);

                            conn.Execute(strSql.ToString(), param, tran);
                        }
                    }

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("[回滚]新增基金详细信息出错", ex);
                    tran.Rollback();
                }
            }
        }
        #endregion

        #region  更新
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(FundProductDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FundProductDetail set ");
            strSql.Append("FundId=@FundId,");
            strSql.Append("NewNetValue=@NewNetValue,");
            strSql.Append("YearlyEarningsRate=@YearlyEarningsRate,");
            strSql.Append("UpdateDate=@UpdateDate");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection(trainDBConnString))
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@FundId", model.FundId, dbType: DbType.Int32);
                param.Add("@NewNetValue", model.NewNetValue, dbType: DbType.Decimal);
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
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from FundProductDetail ");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection(trainDBConnString))
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        #endregion

        #region  获取实体
        /// <summary>
        /// 获取最新详细记录
        /// </summary>
        /// <param name="fundId">基金Id</param>
        /// <returns></returns>
        public FundProductDetail GetLastDetail(int fundId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from FundProductDetail where FundId=@FundId order by UpdateDate desc");

            FundProductDetail model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection(trainDBConnString))
            {
                conn.Open();
                param.Add("@FundId", fundId, dbType: DbType.Int32);
                model = conn.Query<FundProductDetail>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<FundProductDetail> GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,FundId,NewNetValue,YearlyEarningsRate,UpdateDate ");
            strSql.Append(" FROM FundProductDetail ");;

            List<FundProductDetail> list = new List<FundProductDetail>();
            using (var conn = DBHelper.CreateConnection(trainDBConnString))
            {
                conn.Open();
                list = conn.Query<FundProductDetail>(strSql.ToString()).ToList();
            }
            return list;
        }



        #endregion






    }
}

