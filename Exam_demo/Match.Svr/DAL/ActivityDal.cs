using Dapper;
using Match.API;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match.Svr
{
    public class ActivityDal
    {
        #region 模板生成

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Activities where Id=@Id ");

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
        public int Add(Activities model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"INSERT INTO [dbo].[Activities]
                           ([CompetitionPurpose]
                           ,[CompetitionTime]
                           ,[Organization]
                           ,[Content]
                           ,[CollegeId]
                           ,[CreateTime]
                           ,[ModifyTime])
                            VALUES
                           (@CompetitionPurpose,@CompetitionTime,@Organization,@Content,@CollegeId,@CreateTime,@ModifyTime)");
           
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");
            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                //param.Add("@BackImagePath", model.BackImagePath, dbType: DbType.String);
                param.Add("@CompetitionPurpose", model.CompetitionPurpose, dbType: DbType.String);
                param.Add("@CompetitionTime", model.CompetitionTime, dbType: DbType.String);
                param.Add("@Organization", model.Organization, dbType: DbType.String);
                param.Add("@Content", model.Content, dbType: DbType.String);
                param.Add("@CollegeId", model.CollegeId, dbType: DbType.Int32);
                param.Add("@CreateTime", model.CreateTime, dbType: DbType.Date);
                param.Add("@ModifyTime", model.ModifyTime, dbType: DbType.DateTime);
               
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
        public bool Update(Activities model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Activities set ");
            //strSql.Append(" BackImagePath = @BackImagePath , ");
            strSql.Append(" CompetitionPurpose = @CompetitionPurpose , ");
            strSql.Append(" CompetitionTime = @CompetitionTime , ");
            strSql.Append(" Organization = @Organization , ");
            strSql.Append(" Content = @Content , ");
            strSql.Append(" CollegeId = @CollegeId , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" ModifyTime = @ModifyTime");
           

            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                //param.Add("@BackImagePath", model.BackImagePath, dbType: DbType.String);
                param.Add("@CompetitionPurpose", model.CompetitionPurpose, dbType: DbType.String);
                param.Add("@CompetitionTime", model.CompetitionTime, dbType: DbType.String);
                param.Add("@Organization", model.Organization, dbType: DbType.String);
                param.Add("@Content", model.Content, dbType: DbType.String);
                param.Add("@CollegeId", model.CollegeId, dbType: DbType.Int32);
                param.Add("@CreateTime", model.CreateTime, dbType: DbType.DateTime);
                param.Add("@ModifyTime", model.ModifyTime, dbType: DbType.DateTime);
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
            strSql.Append("delete from Activities ");
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
        public Activities GetModel(int collegeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT [Id]                           
                            ,[CompetitionPurpose]
                            ,[CompetitionTime]
                            ,[Organization]
                            ,[Content]
                            ,[CollegeId]
                            ,[CreateTime]
                            ,[ModifyTime]
                        FROM [dbo].[Activities]  ");

            strSql.Append(" where CollegeId=@CollegeId");

            Activities model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@CollegeId", collegeId, dbType: DbType.Int32);
                model = conn.Query<Activities>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Activities> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT [Id]                           
                            ,[CompetitionPurpose]
                            ,[CompetitionTime]
                            ,[Organization]
                            ,[Content]
                            ,[CollegeId]
                            ,[CreateTime]
                            ,[ModifyTime]
                        FROM [dbo].[Activities]  ");
            //strSql.Append(GetStrWhere(filter));

            List<Activities> list = new List<Activities>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<Activities>(strSql.ToString()).ToList();
            }
            return list;
        }

       

        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetActivitiesParams()
        {
            PageModel model = new PageModel();
            model.Tables = "Activities";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "[Id],[CompetitionPurpose] ,[CompetitionTime] ,[Organization] ,[Content] ,[CollegeId] ,[CreateTime]  ,[ModifyTime] ";
            model.Filter = "";
            return model;
        }

        #endregion

        #endregion
    }
}
