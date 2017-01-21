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
    public class NewsDal
    {
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public News GetNewsModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT [Id]
                              ,[Title]
                              ,[Content]
                              ,[UserId]
                              ,[ReleaseTime]
                              ,[CollegeId]
                              ,[UserName]
                              ,[IsHidden]
                              ,[Num],[Image]
                          FROM [dbo].[News]  ");

            strSql.Append(" where Id=@Id");

            News model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", id, dbType: DbType.Int32);
                model = conn.Query<News>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;

        }

        /// <summary>
        /// 根据collegeId获取新闻列表
        /// </summary>
        public List<News> GetNewsList(int collegeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT [Id]
                              ,[Title]
                              ,[Content]
                              ,[UserId]
                              ,[ReleaseTime]
                              ,[CollegeId]
                              ,[UserName]
                              ,[IsHidden]
                              ,[Num],[Image]
                          FROM [dbo].[News]  ");

            strSql.Append(" where CollegeId=@CollegeId");

            List<News> newsList = new List<News>();
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@CollegeId", collegeId, dbType: DbType.Int32);
                newsList = conn.Query<News>(strSql.ToString(), param).ToList();
            }
            return newsList;
        }

        /// <summary>
        /// 更新新闻信息
        /// </summary>
        public bool UpdateNews(News news)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update News set ");
            strSql.Append(" Title = @Title , ");
            strSql.Append(" Content = @Content , ");
            strSql.Append(" UserId = @UserId , ");
            strSql.Append(" ReleaseTime = @ReleaseTime , ");
            strSql.Append(" CollegeId = @CollegeId , ");
            strSql.Append(" UserName = @UserName , ");
            strSql.Append(" IsHidden = @IsHidden , ");
            strSql.Append(" Num = @Num, ");
            strSql.Append(" Image = @Image ");
            strSql.Append(" where Id = @Id");


            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();

                param.Add("@Title", news.Title, dbType: DbType.String);
                param.Add("@Content", news.Content, dbType: DbType.String);
                param.Add("@UserId", news.UserId, dbType: DbType.Int32);
                param.Add("@ReleaseTime", news.ReleaseTime, dbType: DbType.DateTime);
                param.Add("@CollegeId", news.CollegeId, dbType: DbType.Int32);
                param.Add("@UserName", news.UserName, dbType: DbType.String);
                param.Add("@IsHidden", news.IsHidden, dbType: DbType.Boolean);
                param.Add("@Num", news.Num, dbType: DbType.Int32);
                param.Add("@Image", news.Image, dbType: DbType.String);
                param.Add("@Id", news.Id, dbType: DbType.Int32);

                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        /// <summary>
        /// 删除新闻信息
        /// </summary>        
        public bool DeleteNews(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from News  ");
            strSql.Append(" where Id = @Id");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", id, dbType: DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        /// <summary>
        /// 新增新闻信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddNews(News model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into News(");
            strSql.Append("Title,Content,UserId,ReleaseTime,CollegeId,UserName,IsHidden,Num,Image)");

            strSql.Append(" values (");
            strSql.Append("@Title,@Content,@UserId,@ReleaseTime,@CollegeId,@UserName,@IsHidden,@Num,@Image)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Title", model.Title, dbType: DbType.String);
                param.Add("@Content", model.Content, dbType: DbType.String);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@ReleaseTime", model.ReleaseTime, dbType: DbType.DateTime);
                param.Add("@CollegeId", model.CollegeId, dbType: DbType.Int32);
                param.Add("@UserName", model.UserName, dbType: DbType.String);
                param.Add("@IsHidden", model.IsHidden, dbType: DbType.Boolean);
                param.Add("@Num", model.Num, dbType: DbType.Int32);
                param.Add("@Image", model.Image, dbType: DbType.String);
                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(strSql.ToString(), param);
                result = param.Get<int>("@returnid");
            }
            return result;
        }

        /// <summary>
        /// 获取最大的排序号码
        /// </summary>
        /// <param name="collegeId"></param>
        /// <returns></returns>
        public int GetMaxNum(int collegeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select max(Num) from News where CollegeId=@collegeId");
            var param = new DynamicParameters();
            var result = 0;
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@CollegeId", collegeId, dbType: DbType.Int32);
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result;
        }

        /// <summary>
        /// 获取序号最小对应的实体对象
        /// </summary>
        /// <param name="collegeId"></param>
        /// <returns></returns>
        public News GetMaxNumModel(int collegeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from News where CollegeId=@collegeId ");
            strSql.Append(" order by num desc ");
            var param = new DynamicParameters();
            News model = new News();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@CollegeId", collegeId, dbType: DbType.Int32);
                model = conn.Query<News>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        /// <summary>
        /// 更新新闻信息
        /// </summary>
        public bool UpdateNewsNum(News curModel, News minModel)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update News set ");
            strSql.Append(" Num = @Num ");
            strSql.Append(" where Id = @Id");

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("update News set ");
            strSql2.Append(" Num = @Num2 ");
            strSql2.Append(" where Id = @Id2");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", curModel.Id, dbType: DbType.Int32);
                param.Add("@Num", minModel.Num, dbType: DbType.Int32);
                param.Add("@Id2", minModel.Id, dbType: DbType.Int32);
                param.Add("@Num2", curModel.Num, dbType: DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
                result = conn.Execute(strSql2.ToString(), param);
            }
            return result > 0;
        }

        /// <summary>
        /// 更新新闻的发布时间（为置顶方法使用）
        /// </summary>
        public bool UpdateNewsReleaseTime(News model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update News set ");
            strSql.Append(" ReleaseTime = @ReleaseTime ");
            strSql.Append(" where Id = @Id");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ReleaseTime", DateTime.Now, dbType: DbType.DateTime);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        /// <summary>
        /// 隐藏新闻
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool HideNews(int id, bool isHidden)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update News set ");
            strSql.Append(" IsHidden = @IsHidden ");
            strSql.Append(" where Id = @Id");


            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", id, dbType: DbType.Int32);
                param.Add("@IsHidden", isHidden, dbType: DbType.Boolean);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        /// <summary>
        /// 新闻列表分页
        /// </summary>
        /// <param name="collegeId"></param>
        /// <param name="title"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<News> GetNewsPageList(int collegeId, string title, int pageIndex, int pageSize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select top " + pageSize + " * from News  ");
            strSql.Append(" where CollegeId=" + collegeId + " and ishidden=0 and (title like '%" + title + "%' or content like '%" + title + "%') ");
            strSql.Append(" and id not in( ");
            strSql.Append(" select top " + ((pageIndex - 1) * pageSize) + " id from News ");
            strSql.Append(" where CollegeId=" + collegeId + " and ishidden=0 and (title like '%" + title + "%' or content like '%" + title + "%') order by releasetime desc ");
            strSql.Append(" ) order by releasetime desc ");

            List<News> newsList = new List<News>();
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                newsList = conn.Query<News>(strSql.ToString()).ToList();
            }
            return newsList;
        }
    }
}
