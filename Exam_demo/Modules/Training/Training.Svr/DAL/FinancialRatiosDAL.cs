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
    /// 数据访问类:FinancialRatios
    /// </summary>
    public partial class FinancialRatiosDAL
    {
        public FinancialRatiosDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from FinancialRatios where Id=@Id ");

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
        public int Add(FinancialRatios model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FinancialRatios(");
            strSql.Append("ProposalId,LiabilityAnalysis,IncomeAndExpensesAnalysis,Analysis)");

            strSql.Append(" values (");
            strSql.Append("@ProposalId,@LiabilityAnalysis,@IncomeAndExpensesAnalysis,@Analysis)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@LiabilityAnalysis", model.LiabilityAnalysis, dbType: DbType.String);
                param.Add("@IncomeAndExpensesAnalysis", model.IncomeAndExpensesAnalysis, dbType: DbType.String);
                param.Add("@Analysis", model.Analysis, dbType: DbType.String);
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
        public bool Update(FinancialRatios model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FinancialRatios set ");
            strSql.Append("ProposalId=@ProposalId,");
            strSql.Append("LiabilityAnalysis=@LiabilityAnalysis,");
            strSql.Append("IncomeAndExpensesAnalysis=@IncomeAndExpensesAnalysis,");
            strSql.Append("Analysis=@Analysis");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@LiabilityAnalysis", model.LiabilityAnalysis, dbType: DbType.String);
                param.Add("@IncomeAndExpensesAnalysis", model.IncomeAndExpensesAnalysis, dbType: DbType.String);
                param.Add("@Analysis", model.Analysis, dbType: DbType.String);
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
            strSql.Append("delete from FinancialRatios ");
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
        public FinancialRatios GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,LiabilityAnalysis,IncomeAndExpensesAnalysis,Analysis from FinancialRatios ");
            strSql.Append(" where Id=@Id ");

            FinancialRatios model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<FinancialRatios>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public FinancialRatios GetModel2(int ProposalId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,LiabilityAnalysis,IncomeAndExpensesAnalysis,Analysis from FinancialRatios ");
            strSql.Append(" where ProposalId=@ProposalId ");

            FinancialRatios model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", ProposalId, dbType: DbType.Int32);
                model = conn.Query<FinancialRatios>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }
        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<FinancialRatios> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,LiabilityAnalysis,IncomeAndExpensesAnalysis,Analysis ");
            strSql.Append(" FROM FinancialRatios ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<FinancialRatios> list = new List<FinancialRatios>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<FinancialRatios>(strSql.ToString()).ToList();
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
            if (filter.ProposalId.HasValue)
            {
                strSql.Append(" and ProposalId=" + filter.ProposalId);
            }
            return strSql.ToString();
        }

        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetFinancialRatiosPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "FinancialRatios";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,ProposalId,LiabilityAnalysis,IncomeAndExpensesAnalysis,Analysis";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion




    }
}

