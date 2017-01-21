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
    public class AwardDal
    {
        /// <summary>
        /// 得到一个对象实体
        /// </summary>

        public Award GetAwardModel(int collegeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT [Id]
                              ,[CollegeId]
                              ,[UserId]
                              ,[AwardType]
                              ,[AwardTypeComment]
                              ,[AwardDescription]
                              ,[IsVisible]
                          FROM [dbo].[Award]  ");

            strSql.Append(" where CollegeId=@CollegeId");

            Award model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@CollegeId", collegeId, dbType: DbType.Int32);
                model = conn.Query<Award>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        
        }

        /// <summary>
        /// 根据collegeId获取奖项设置列表
        /// </summary>
        /// <param name="collegeId"></param>
        /// <returns></returns>
        public List<Award> GetAwardList(int collegeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT [Id]
                              ,[CollegeId]
                              ,[UserId]
                              ,[AwardType]
                              ,[AwardTypeComment]
                              ,[AwardDescription]
                              ,[IsVisible]
                          FROM [dbo].[Award]  ");

            strSql.Append(" where CollegeId=@CollegeId");

            List<Award> awardList = new List<Award>();
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@CollegeId", collegeId, dbType: DbType.Int32);
                awardList = conn.Query<Award>(strSql.ToString(), param).ToList();
            }
            return awardList;
        }

       /// <summary>
        /// 更新奖项设置
       /// </summary>
       /// <param name="awardList">奖项设置对象列表</param>
       /// <returns></returns>

        public bool UpdateAward(List<Award > awardList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Award set ");
            strSql.Append(" AwardTypeComment = @AwardTypeComment , ");
            strSql.Append(" AwardDescription = @AwardDescription , ");
            strSql.Append(" IsVisible = @IsVisible , ");
            strSql.Append(" where Id = @Id");

            
            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                if (awardList != null && awardList.Count > 0)
                {
                    foreach (Award a in awardList)
                    {
                        param.Add("@AwardTypeComment", a.AwardTypeComment, dbType: DbType.String);
                        param.Add("@AwardDescription", a.AwardDescription, dbType: DbType.String);
                        param.Add("@IsVisible", a.IsVisible, dbType: DbType.Boolean);
                        result = conn.Execute(strSql.ToString(), param);
                    }
                }
                
            }
            return result > 0;
        }

        /// <summary>
        /// 更新奖项设置
        /// </summary>
        /// <param name="awardList">奖项设置对象</param>
        /// <returns></returns>

        public bool UpdateAward(Award award)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Award set ");
            strSql.Append(" AwardTypeComment = @AwardTypeComment , ");
            strSql.Append(" AwardDescription = @AwardDescription , ");
            strSql.Append(" IsVisible = @IsVisible , ");
            strSql.Append(" where Id = @Id");


            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();

                param.Add("@AwardTypeComment", award.AwardTypeComment, dbType: DbType.String);
                param.Add("@AwardDescription", award.AwardDescription, dbType: DbType.String);
                param.Add("@IsVisible", award.IsVisible, dbType: DbType.Boolean);
                result = conn.Execute(strSql.ToString(), param);                
            }
            return result > 0;
        }

        /// <summary>
        /// 删除奖项设置
        /// </summary>
        /// <param name="awardList">奖项设置对象</param>
        /// <returns></returns>

        public bool DeleteAward(int collegeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Award  ");
            strSql.Append(" where CollegeId = @collegeId");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@CollegeId", collegeId, dbType: DbType.Int32);               
                result = conn.Execute(strSql.ToString(), param);
            }
            return result >= 0;
        }

        /// <summary>
        /// 新增奖项设置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddAward(Award model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Award(");
            strSql.Append("CollegeId,UserId,AwardType,AwardTypeComment,AwardDescription,IsVisible)");

            strSql.Append(" values (");
            strSql.Append("@CollegeId,@UserId,@AwardType,@AwardTypeComment,@AwardDescription,@IsVisible)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@CollegeId", model.CollegeId, dbType: DbType.Int32);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@AwardType", model.AwardType, dbType: DbType.Int32);
                param.Add("@AwardTypeComment", model.AwardTypeComment, dbType: DbType.String);
                param.Add("@AwardDescription", model.AwardDescription, dbType: DbType.String);
                param.Add("@IsVisible", model.IsVisible, dbType: DbType.Boolean);
                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(strSql.ToString(), param);
                result = param.Get<int>("@returnid");
            }
            return result;
        }
    }
}
