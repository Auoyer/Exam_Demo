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
    public class CompetitionDescDal
    {
        #region 模板生成

       

        #region  新增

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(CompetitionDescription model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"INSERT INTO [dbo].[CompetitionDescription]
                           ([ComDesSettings]
                           ,[EventSchedule]
                           ,[TroubleShooting]                          
                           ,[CollegeId])                         
                            VALUES  (@ComDesSettings,@EventSchedule,@TroubleShooting,@CollegeId)");

            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");
            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ComDesSettings", model.ComDesSettings, dbType: DbType.String);
                param.Add("@EventSchedule", model.EventSchedule, dbType: DbType.String);
                param.Add("@TroubleShooting", model.TroubleShooting, dbType: DbType.String);               
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
        public bool Update(CompetitionDescription model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CompetitionDescription set ");
            strSql.Append(" ComDesSettings = @ComDesSettings , ");
            strSql.Append(" EventSchedule = @EventSchedule , ");
            strSql.Append(" TroubleShooting = @TroubleShooting  ");
            
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ComDesSettings", model.ComDesSettings, dbType: DbType.String);
                param.Add("@EventSchedule", model.EventSchedule, dbType: DbType.String);
                param.Add("@TroubleShooting", model.TroubleShooting, dbType: DbType.String);
               
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        #endregion

       
        #region  获取实体
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public CompetitionDescription GetModel(int collegeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT [Id]
                          ,[ComDesSettings]
                          ,[EventSchedule]
                          ,[TroubleShooting]
                          ,[CollegeId]
                      FROM [GTA_FPBT_Match_V1.5].[dbo].[CompetitionDescription] ");
            strSql.Append(" where CollegeId=@CollegeId");

            CompetitionDescription model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@CollegeId", collegeId, dbType: DbType.Int32);
                model = conn.Query<CompetitionDescription>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

       
        #endregion
    }
}
