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
    /// 数据访问类:BankDeposits
    /// </summary>
    public partial class BankDepositsDAL
    {
        public BankDepositsDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BankDeposits where Id=@Id ");

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
        public int Add(BankDeposits model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BankDeposits(");
            strSql.Append("BankName,DemandDeposit,ThreeMonth,SixMonth,Year,TwoYear,ThreeYear,FiveYear)");

            strSql.Append(" values (");
            strSql.Append("@BankName,@DemandDeposit,@ThreeMonth,@SixMonth,@Year,@TwoYear,@ThreeYear,@FiveYear)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@BankName", model.BankName, dbType: DbType.String);
                param.Add("@DemandDeposit", model.DemandDeposit, dbType: DbType.Decimal);
                param.Add("@ThreeMonth", model.ThreeMonth, dbType: DbType.Decimal);
                param.Add("@SixMonth", model.SixMonth, dbType: DbType.Decimal);
                param.Add("@Year", model.Year, dbType: DbType.Decimal);
                param.Add("@TwoYear", model.TwoYear, dbType: DbType.Decimal);
                param.Add("@ThreeYear", model.ThreeYear, dbType: DbType.Decimal);
                param.Add("@FiveYear", model.FiveYear, dbType: DbType.Decimal);
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
        public bool Update(BankDeposits model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BankDeposits set ");
            strSql.Append("BankName=@BankName,");
            strSql.Append("DemandDeposit=@DemandDeposit,");
            strSql.Append("ThreeMonth=@ThreeMonth,");
            strSql.Append("SixMonth=@SixMonth,");
            strSql.Append("Year=@Year,");
            strSql.Append("TwoYear=@TwoYear,");
            strSql.Append("ThreeYear=@ThreeYear,");
            strSql.Append("FiveYear=@FiveYear");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@BankName", model.BankName, dbType: DbType.String);
                param.Add("@DemandDeposit", model.DemandDeposit, dbType: DbType.Decimal);
                param.Add("@ThreeMonth", model.ThreeMonth, dbType: DbType.Decimal);
                param.Add("@SixMonth", model.SixMonth, dbType: DbType.Decimal);
                param.Add("@Year", model.Year, dbType: DbType.Decimal);
                param.Add("@TwoYear", model.TwoYear, dbType: DbType.Decimal);
                param.Add("@ThreeYear", model.ThreeYear, dbType: DbType.Decimal);
                param.Add("@FiveYear", model.FiveYear, dbType: DbType.Decimal);
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
            strSql.Append("delete from BankDeposits ");
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
        public BankDeposits GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,BankName,DemandDeposit,ThreeMonth,SixMonth,Year,TwoYear,ThreeYear,FiveYear from BankDeposits ");
            strSql.Append(" where Id=@Id ");

            BankDeposits model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<BankDeposits>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<BankDeposits> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,BankName,DemandDeposit,ThreeMonth,SixMonth,Year,TwoYear,ThreeYear,FiveYear ");
            strSql.Append(" FROM BankDeposits ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<BankDeposits> list = new List<BankDeposits>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<BankDeposits>(strSql.ToString()).ToList();
            }
            return list;
        }

        /// <summary>
        /// 根据CustomFilter获取where语句
        /// </summary>
        private string GetStrWhere(CustomFilter filter)
        {
            if (filter == null)
            {
                return "";
            }

            StringBuilder strSql = new StringBuilder();
            if (filter.Id.HasValue)
            {
                strSql.Append(" and Id=" + filter.Id);
            }
            if (!string.IsNullOrEmpty(filter.KeyWords))
            {
                //过滤危险字段，如单引号等
                string key = filter.KeyWords.Replace("'", "''");
                strSql.AppendFormat(" and BankName like '%{0}%' ", key);
            }
            return strSql.ToString();
        }

        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetBankDepositsPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "BankDeposits";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,BankName,DemandDeposit,ThreeMonth,SixMonth,Year,TwoYear,ThreeYear,FiveYear";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion




    }
}

