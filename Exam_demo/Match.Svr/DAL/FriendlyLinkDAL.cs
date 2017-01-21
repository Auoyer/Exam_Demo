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
 	///友情链接
	/// </summary>
		public partial class FriendlyLinkDAL
	{
        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from FriendlyLink where Id=@Id ");

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
        public int Add(FriendlyLink model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FriendlyLink(");
            strSql.Append("HomePageId,CollegeId,LinkImagePath,LinkAddress,LinkName");
            strSql.Append(") values (");
            strSql.Append("@HomePageId,@CollegeId,@LinkImagePath,@LinkAddress,@LinkName");
            strSql.Append(") ");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");
            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                                param.Add("@HomePageId", model.HomePageId, dbType: DbType.Int32);
                                param.Add("@CollegeId", model.CollegeId, dbType: DbType.Int32);
                                param.Add("@LinkImagePath", model.LinkImagePath, dbType: DbType.String);
                                param.Add("@LinkAddress", model.LinkAddress, dbType: DbType.String);
                                param.Add("@LinkName", model.LinkName, dbType: DbType.String);
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
        public bool Update(FriendlyLink model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FriendlyLink set ");
                                                            strSql.Append(" HomePageId = @HomePageId , ");
                                                strSql.Append(" CollegeId = @CollegeId , ");
                                                strSql.Append(" LinkImagePath = @LinkImagePath , ");
                                                strSql.Append(" LinkName = @LinkName,  ");
                                                strSql.Append(" LinkAddress = @LinkAddress  ");
                        	
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                                param.Add("@Id", model.Id, dbType: DbType.Int32);
                                param.Add("@HomePageId", model.HomePageId, dbType: DbType.Int32);
                                param.Add("@CollegeId", model.CollegeId, dbType: DbType.Int32);
                                param.Add("@LinkImagePath", model.LinkImagePath, dbType: DbType.String);
                                param.Add("@LinkName", model.LinkName, dbType: DbType.String);
                                param.Add("@LinkAddress", model.LinkAddress, dbType: DbType.String);
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
            strSql.Append("delete from FriendlyLink ");
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
        public FriendlyLink GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, HomePageId, CollegeId, LinkImagePath, LinkAddress ,LinkName ");
            strSql.Append("  from FriendlyLink ");
            strSql.Append(" where Id=@Id");

            FriendlyLink model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<FriendlyLink>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<FriendlyLink> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, HomePageId, CollegeId, LinkImagePath, LinkAddress ,LinkName ");
            strSql.Append("  from FriendlyLink ");
            strSql.Append(GetStrWhere(filter));

            List<FriendlyLink> list = new List<FriendlyLink>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<FriendlyLink>(strSql.ToString()).ToList();
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
        public PageModel GetFriendlyLinkPageParams()
        {
            PageModel model = new PageModel();
            model.Tables = "FriendlyLink";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id, HomePageId, CollegeId, LinkImagePath, LinkAddress,LinkName ";
            model.Filter = "";
            return model;
        }

        #endregion

        /// <summary>
        /// 得到友情链接列表
        /// </summary>
        public List<FriendlyLink> GetFriendlyLinkList(int collegeId, int homePageId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  [Id]
                            ,[HomePageId]
                            ,[CollegeId]
                            ,[LinkImagePath]
                            ,[LinkName]
                            ,[LinkAddress]
                            FROM [GTA_FPBT_Match_V1.5].[dbo].[FriendlyLink]  ");
            strSql.Append(" where CollegeId=@CollegeId and HomePageId=@HomePageId");
            List<FriendlyLink> list = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@CollegeId", collegeId, dbType: DbType.Int32);
                param.Add("@HomePageId", homePageId, dbType: DbType.Int32);
                list = conn.Query<FriendlyLink>(strSql.ToString(), param).ToList();
            }
            return list;
        }
   
	}
}

