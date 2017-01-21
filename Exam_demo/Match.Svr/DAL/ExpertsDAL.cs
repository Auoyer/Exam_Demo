using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using Match.API;

//自己更改命名空间
namespace Training.Svr
{
		/// <summary>
 	///专家风采
	/// </summary>
		public partial class ExpertsDAL
	{
        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Experts where Id=@Id ");

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
        public int Add(Experts model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Experts(");
            strSql.Append("HomePageId,UserId,ExpertsName,ExpertsPicPath,ExpertsIntroduction,CollegeId");
            strSql.Append(") values (");
            strSql.Append("@HomePageId,@UserId,@ExpertsName,@ExpertsPicPath,@ExpertsIntroduction,@CollegeId");
            strSql.Append(") ");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");
            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                                param.Add("@HomePageId", model.HomePageId, dbType: DbType.Int32);
                                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                                param.Add("@ExpertsName", model.ExpertsName, dbType: DbType.String);
                                param.Add("@ExpertsPicPath", model.ExpertsPicPath, dbType: DbType.String);
                                param.Add("@ExpertsIntroduction", model.ExpertsIntroduction, dbType: DbType.String);
                                param.Add("@CollegeId", model.CollegeId, dbType: DbType.Int32);
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
        public bool Update(Experts model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Experts set ");
                                                            strSql.Append(" HomePageId = @HomePageId , ");
                                                strSql.Append(" UserId = @UserId , ");
                                                strSql.Append(" ExpertsName = @ExpertsName , ");
                                                strSql.Append(" ExpertsPicPath = @ExpertsPicPath , ");
                                                strSql.Append(" ExpertsIntroduction = @ExpertsIntroduction , ");
                                                strSql.Append(" CollegeId = @CollegeId  ");
                        	
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                                param.Add("@Id", model.Id, dbType: DbType.Int32);
                                param.Add("@HomePageId", model.HomePageId, dbType: DbType.Int32);
                                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                                param.Add("@ExpertsName", model.ExpertsName, dbType: DbType.String);
                                param.Add("@ExpertsPicPath", model.ExpertsPicPath, dbType: DbType.String);
                                param.Add("@ExpertsIntroduction", model.ExpertsIntroduction, dbType: DbType.String);
                                param.Add("@CollegeId", model.CollegeId, dbType: DbType.Int32);
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
            strSql.Append("delete from Experts ");
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
        public Experts GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, HomePageId, UserId, ExpertsName, ExpertsPicPath, ExpertsIntroduction, CollegeId  ");
            strSql.Append("  from Experts ");
            strSql.Append(" where Id=@Id");

            Experts model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<Experts>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Experts> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, HomePageId, UserId, ExpertsName, ExpertsPicPath, ExpertsIntroduction, CollegeId  ");
            strSql.Append("  from Experts ");
            strSql.Append(GetStrWhere(filter));

            List<Experts> list = new List<Experts>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<Experts>(strSql.ToString()).ToList();
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
            return strSql.ToString();
        }

        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetExpertsPageParams()
        {
            PageModel model = new PageModel();
            model.Tables = "Experts";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id, HomePageId, UserId, ExpertsName, ExpertsPicPath, ExpertsIntroduction, CollegeId ";
            model.Filter = "";
            return model;
        }

        #endregion

        /// <summary>
        /// 得到专家列表
        /// </summary>
        public List<Experts> GetExpertsList(int collegeId, int homePageId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT [Id]
                              ,[HomePageId]
                              ,[UserId]
                              ,[ExpertsName]
                              ,[ExpertsPicPath]
                              ,[ExpertsIntroduction]
                              ,[CollegeId]
                          FROM [dbo].[Experts]  ");
            strSql.Append(" where CollegeId=@CollegeId and HomePageId=@HomePageId");
            List<Experts> list = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@CollegeId", collegeId, dbType: DbType.Int32);
                param.Add("@HomePageId", homePageId, dbType: DbType.Int32);
                list = conn.Query<Experts>(strSql.ToString(), param).ToList();
            }
            return list;
        }


   
	}
}

