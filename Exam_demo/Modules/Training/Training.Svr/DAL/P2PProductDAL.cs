using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using Utils;
using Training.API;

namespace Training.Svr
{
    /// <summary>
    /// 数据访问类:P2PProduct
    /// </summary>
    public partial class P2PProductDAL
    {
        public P2PProductDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from P2PProduct where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
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
        public int Add(P2PProduct model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into P2PProduct(");
            strSql.Append("P2PName,InvestmentField,InvestmentCycle,StartAmount,EarningsRate,SourceId)");

            strSql.Append(" values (");
            strSql.Append("@P2PName,@InvestmentField,@InvestmentCycle,@StartAmount,@EarningsRate,@SourceId)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@P2PName", model.P2PName, dbType: DbType.String);
                param.Add("@InvestmentField", model.InvestmentField, dbType: DbType.String);
                param.Add("@InvestmentCycle", model.InvestmentCycle, dbType: DbType.String);
                param.Add("@StartAmount", model.StartAmount, dbType: DbType.String);
                param.Add("@EarningsRate", model.EarningsRate, dbType: DbType.String);
                param.Add("@SourceId",model.SourceId,dbType:DbType.String);
                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                
                conn.Execute(strSql.ToString(), param);
                result = param.Get<int>("@returnid");
            }
            return result;
        }
        #endregion

        #region  更新
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(P2PProduct model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update P2PProduct set ");
            strSql.Append("P2PName=@P2PName,");
            strSql.Append("InvestmentField=@InvestmentField,");
            strSql.Append("InvestmentCycle=@InvestmentCycle,");
            strSql.Append("StartAmount=@StartAmount,");
            strSql.Append("EarningsRate=@EarningsRate,");
            strSql.Append("SourceId=@SourceId");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@P2PName", model.P2PName, dbType: DbType.String);
                param.Add("@InvestmentField", model.InvestmentField, dbType: DbType.String);
                param.Add("@InvestmentCycle", model.InvestmentCycle, dbType: DbType.String);
                param.Add("@StartAmount", model.StartAmount, dbType: DbType.String);
                param.Add("@EarningsRate", model.EarningsRate, dbType: DbType.String);
                param.Add("@SourceId",model.SourceId,dbType:DbType.String);
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
            strSql.Append("delete from P2PProduct ");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
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
        /// 得到一个对象实体
        /// </summary>
        public P2PProduct GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,P2PName,InvestmentField,InvestmentCycle,StartAmount,EarningsRate,SourceId from P2PProduct ");
            strSql.Append(" where Id=@Id ");

            P2PProduct model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<P2PProduct>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<P2PProduct> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,P2PName,InvestmentField,InvestmentCycle,StartAmount,EarningsRate,SourceId ");
            strSql.Append(" FROM P2PProduct ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<P2PProduct> list = new List<P2PProduct>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<P2PProduct>(strSql.ToString()).ToList();
            }
            return list;
        }

        /// <summary>
        /// 根据CustomFilter获取where语句
        /// </summary>
        private string GetStrWhere(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (filter.Id.HasValue)
            {
                strSql.Append(" and Id=" + filter.Id);
            }
            if (!string.IsNullOrEmpty(filter.KeyWords))
            {
                //过滤危险字段，如单引号等
                string key = filter.KeyWords.Replace("'", "''");
                strSql.AppendFormat(" and (P2PName like '%{0}%' or InvestmentField like '%{0}%' )", key);
            }
            return strSql.ToString();
        }

        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetP2PProductPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "P2PProduct";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,P2PName,InvestmentField,InvestmentCycle,StartAmount,EarningsRate,SourceId";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion

        /// <summary>
        /// 爬来的数据批量新增
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public int AddBluk(List<P2PProduct> models)
        {
            int result = 0;
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    foreach (var model in models)
                    {
                        StringBuilder strSql = new StringBuilder();
                        var param = new DynamicParameters();
                        strSql.Append("insert into P2PProduct(");
                        strSql.Append("P2PName,InvestmentField,InvestmentCycle,StartAmount,EarningsRate,SourceId)");

                        strSql.Append(" values (");
                        strSql.Append("@P2PName,@InvestmentField,@InvestmentCycle,@StartAmount,@EarningsRate,@SourceId)");
                        strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");
                        param.Add("@P2PName", model.P2PName, dbType: DbType.String);
                        param.Add("@InvestmentField", model.InvestmentField, dbType: DbType.String);
                        param.Add("@InvestmentCycle", model.InvestmentCycle, dbType: DbType.String);
                        param.Add("@StartAmount", model.StartAmount, dbType: DbType.String);
                        param.Add("@EarningsRate", model.EarningsRate, dbType: DbType.String);
                        param.Add("@SourceId",model.SourceId,dbType:DbType.String);
                        param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                        conn.Execute(strSql.ToString(), param, tran);
                        result = param.Get<int>("@returnid");
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("P2PProductDAL AddBluk", ex);
                    tran.Rollback();
                }
            }
            return result;

        }


        public bool UpdateBluk(List<P2PProduct> models)
        {
            int result = 0;

            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    foreach (var model in models)
                    {
                        StringBuilder strSql = new StringBuilder();
                        var param = new DynamicParameters();
                        strSql.Append("update P2PProduct set ");
                        strSql.Append("P2PName=@P2PName,");
                        strSql.Append("InvestmentField=@InvestmentField,");
                        strSql.Append("InvestmentCycle=@InvestmentCycle,");
                        strSql.Append("StartAmount=@StartAmount,");
                        strSql.Append("EarningsRate=@EarningsRate");
                        strSql.Append(" where SourceId=@SourceId");
                        param.Add("@P2PName", model.P2PName, dbType: DbType.String);
                        param.Add("@InvestmentField", model.InvestmentField, dbType: DbType.String);
                        param.Add("@InvestmentCycle", model.InvestmentCycle, dbType: DbType.String);
                        param.Add("@StartAmount", model.StartAmount, dbType: DbType.String);
                        param.Add("@EarningsRate", model.EarningsRate, dbType: DbType.String);
                        param.Add("@SourceId", model.SourceId, dbType: DbType.String);

                       result= conn.Execute(strSql.ToString(), param, tran);
                       
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("P2PProductDAL AddBluk", ex);
                    tran.Rollback();
                }
            }

            return result > 0;
        }


        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sourceIds"></param>
        /// <returns></returns>
        public bool DeleteBluk(List<string> sourceIds)
        {
            int result = 0;
            StringBuilder strSql = new StringBuilder();
            
            strSql.Append("delete from P2PProduct where SourceId in @SourceId");

            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                result = conn.Execute(strSql.ToString(), new { SourceId=sourceIds });
            }
            return result > 0;
        }

        /// <summary>
        /// 删除P2P重复数据
        /// </summary>
        /// <returns></returns>
        public bool DeleteRepeatP2P()
        {
            int result = 0;
            StringBuilder strSql = new StringBuilder();

            strSql.Append("delete from P2PProduct");
            strSql.Append(" where SourceId in (select SourceId from P2PProduct group by SourceId having count(SourceId) > 1) ");
            strSql.Append(" and Id not in (select min(Id) from P2PProduct group by SourceId having count(SourceId)>1) ");

            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                result = conn.Execute(strSql.ToString());
            }
            return result > 0;
        }

    }
}

