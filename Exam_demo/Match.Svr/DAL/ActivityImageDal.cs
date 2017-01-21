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
    public class ActivityImageDal
    {
         /// <summary>
        /// 新增活动图片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddActiveImage(ActivityImage model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"INSERT INTO [dbo].[ActivityImage]
                               ([HomePageId]
                               ,[CollegeId]
                               ,[ImageDescription]
                               ,[ActivityImagePath]
                               ,[CreateTime])
                                VALUES 
            (@HomePageId,@CollegeId,@ImageDescription,@ActivityImagePath,@CreateTime)");

            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");
            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@HomePageId", model.HomePageId, dbType: DbType.Int32);
                param.Add("@CollegeId", model.CollegeId, dbType: DbType.Int32);
                param.Add("@ImageDescription", model.ImageDescription, dbType: DbType.String);
                param.Add("@ActivityImagePath", model.ActivityImagePath, dbType: DbType.String);
                param.Add("@CreateTime", model.CreateTime, dbType: DbType.DateTime);              
                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(strSql.ToString(), param);
                result = param.Get<int>("@returnid");
            }
            return result;
        }

        /// <summary>
        /// 得到活动图片列表
        /// </summary>
        public List<ActivityImage> GetActivityImageList(int collegeId,int homePageId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT [ActivityImageId]
                              ,[HomePageId]
                              ,[CollegeId]
                              ,[ImageDescription]
                              ,[ActivityImagePath]
                              ,[CreateTime]
                          FROM [dbo].[ActivityImage]  ");
            strSql.Append(" where CollegeId=@CollegeId and HomePageId=@HomePageId");
            List<ActivityImage> list = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@CollegeId", collegeId, dbType: DbType.Int32);
                param.Add("@HomePageId", homePageId, dbType: DbType.Int32);
                list = conn.Query<ActivityImage>(strSql.ToString(), param).ToList();
            }
            return list;
        }

         /// <summary>
        /// 更新活动图片
        /// </summary>   
        public bool UpdateActivityImage(ActivityImage model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ActivityImage set ");              
            strSql.Append(" ImageDescription = @ImageDescription , ");
            strSql.Append(" ActivityImagePath = @ActivityImagePath ");
            strSql.Append(" where ActivityImageId=@ActivityImageId ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ActivityImageId", model.ActivityImageId, dbType: DbType.Int32);
                param.Add("@ImageDescription", model.ImageDescription, dbType: DbType.String);
                param.Add("@ActivityImagePath", model.ActivityImagePath, dbType: DbType.String);
               
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        /// <summary>
        /// 更新活动图片
        /// </summary>   
        public bool BulkUpdateActivityImage(List<ActivityImage> modelList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ActivityImage set ");
            strSql.Append(" ImageDescription = @ImageDescription , ");
            strSql.Append(" ActivityImagePath = @ActivityImagePath ");
            strSql.Append(" where ActivityImageId=@ActivityImageId ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                foreach (var model in modelList)
                {
                    param.Add("@ActivityImageId", model.ActivityImageId, dbType: DbType.Int32);
                    param.Add("@ImageDescription", model.ImageDescription, dbType: DbType.String);
                    param.Add("@ActivityImagePath", model.ActivityImagePath, dbType: DbType.String);
                    result = conn.Execute(strSql.ToString(), param);
                }                 
            }
            return result > 0;
        }

        /// <summary>
        /// 新增活动图片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool BulkAddActiveImage(List<ActivityImage> modelList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"INSERT INTO [dbo].[ActivityImage]
                               ([HomePageId]
                               ,[CollegeId]
                               ,[ImageDescription]
                               ,[ActivityImagePath]
                               ,[CreateTime])
                                VALUES 
            (@HomePageId,@CollegeId,@ImageDescription,@ActivityImagePath,@CreateTime)");
            var result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                foreach (var model in modelList)
                {
                    param.Add("@HomePageId", model.HomePageId, dbType: DbType.Int32);
                    param.Add("@CollegeId", model.CollegeId, dbType: DbType.Int32);
                    param.Add("@ImageDescription", model.ImageDescription, dbType: DbType.String);
                    param.Add("@ActivityImagePath", model.ActivityImagePath, dbType: DbType.String);
                    param.Add("@CreateTime", model.CreateTime, dbType: DbType.DateTime);
                    param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    result = conn.Execute(strSql.ToString(), param);
                }              
               
            }
            return result > 0;
        }

        /// <summary>
        /// 删除活动图片
        /// </summary>   
        public bool DeleteActivityImage(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from  ActivityImage ");
            strSql.Append(" where ActivityImageId=@id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@id", id, dbType: DbType.Int32);             
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }
    }
}
