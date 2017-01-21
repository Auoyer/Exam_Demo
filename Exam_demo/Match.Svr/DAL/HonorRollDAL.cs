using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using Match.API;

//自己更改命名空间
namespace Match.Svr
{
		/// <summary>
 	///荣誉榜
	/// </summary>
		public partial class HonorRollDAL
	{
        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from HonorRoll where Id=@Id ");

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
        public int Add(HonorRoll model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into HonorRoll(");
            strSql.Append("HomePageId,CollegeId,CompetitionName,CompetitionId");
            strSql.Append(") values (");
            strSql.Append("@HomePageId,@CollegeId,@CompetitionName,@CompetitionId");
            strSql.Append(") ");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");
            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                                param.Add("@HomePageId", model.HomePageId, dbType: DbType.Int32);
                                param.Add("@CollegeId", model.CollegeId, dbType: DbType.Int32);
                                param.Add("@CompetitionName", model.CompetitionName, dbType: DbType.String);
                                param.Add("@CompetitionId", model.CompetitionId, dbType: DbType.Int32);
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
        public bool Update(HonorRoll model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update HonorRoll set ");
                                                            strSql.Append(" HomePageId = @HomePageId , ");
                                                strSql.Append(" CollegeId = @CollegeId , ");
                                                strSql.Append(" CompetitionName = @CompetitionName , ");
                                                strSql.Append(" CompetitionId = @CompetitionId  ");
                        	
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                                param.Add("@Id", model.Id, dbType: DbType.Int32);
                                param.Add("@HomePageId", model.HomePageId, dbType: DbType.Int32);
                                param.Add("@CollegeId", model.CollegeId, dbType: DbType.Int32);
                                param.Add("@CompetitionName", model.CompetitionName, dbType: DbType.String);
                                param.Add("@CompetitionId", model.CompetitionId, dbType: DbType.Int32);
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
            strSql.Append("delete from HonorRoll ");
            strSql.Append(" where Id=@Id");

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
        public HonorRoll GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, HomePageId, CollegeId, CompetitionName, CompetitionId  ");
            strSql.Append("  from HonorRoll ");
            strSql.Append(" where Id=@Id");

            HonorRoll model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<HonorRoll>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<HonorRoll> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, HomePageId, CollegeId, CompetitionName, CompetitionId  ");
            strSql.Append("  from HonorRoll ");
            strSql.Append(GetStrWhere(filter));

            List<HonorRoll> list = new List<HonorRoll>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<HonorRoll>(strSql.ToString()).ToList();
            }
            return list;
        }

        /// <summary>
        /// 根据CustomFilter获取where语句
        /// </summary>
        private string GetStrWhere(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" where 1=1 ");
            if (filter.Id.HasValue)
            {
                strSql.Append(" and Id=" + filter.Id);
            }
            if (filter.CollegeId.HasValue)
            {
                strSql.Append(" and CollegeId=" + filter.CollegeId);
            }
            return strSql.ToString();
        }

        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetHonorRollPageParams()
        {
            PageModel model = new PageModel();
            model.Tables = "HonorRoll";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id, HomePageId, CollegeId, CompetitionName, CompetitionId ";
            model.Filter = "";
            return model;
        }

        #endregion




   
	}
}

