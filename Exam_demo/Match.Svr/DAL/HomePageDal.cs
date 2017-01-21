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
    public class HomePageDal
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(HomePage model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"INSERT INTO [dbo].[HomePage]
                               ([CompetitionIntroduction]
                               ,[Title1]
                               ,[Title2]
                               ,[Title3]
                               ,[QRCodeImgPath]
                               ,[QRCodeIntroduction]
                               ,[ComTelConsultation]
                               ,[ComPhone]
                               ,[ComQQ]
                               ,[Step1Description]
                               ,[Step2Description]
                               ,[Step3Description]
                               ,[Step4Description]
                               ,[CollegeId])
                         VALUES
                               (@CompetitionIntroduction
                               ,@Title1
                               ,@Title2
                               ,@Title3
                               ,@QRCodeImgPath
                               ,@QRCodeIntroduction
                               ,@ComTelConsultation
                               ,@ComPhone
                               ,@ComQQ
                               ,@Step1Description
                               ,@Step2Description
                               ,@Step3Description
                               ,@Step4Description
                               ,@CollegeId)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");
            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@CompetitionIntroduction", model.CompetitionIntroduction, dbType: DbType.String);
                param.Add("@Title1", model.Title1, dbType: DbType.String);
                param.Add("@Title2", model.Title2, dbType: DbType.String);
                param.Add("@Title3", model.CollegeId, dbType: DbType.String);
                param.Add("@QRCodeImgPath", model.QRCodeImgPath, dbType: DbType.String);
                param.Add("@QRCodeIntroduction", model.QRCodeIntroduction, dbType: DbType.String);
                param.Add("@ComTelConsultation", model.ComTelConsultation, dbType: DbType.String);
                param.Add("@ComPhone", model.ComPhone, dbType: DbType.String);
                param.Add("@ComQQ", model.ComQQ, dbType: DbType.String);
                param.Add("@Step1Description", model.Step1Description, dbType: DbType.String);
                param.Add("@Step2Description", model.Step2Description, dbType: DbType.String);
                param.Add("@Step3Description", model.Step3Description, dbType: DbType.String);
                param.Add("@Step4Description", model.Step4Description, dbType: DbType.String);
                param.Add("@CollegeId", model.CollegeId, dbType: DbType.Int32);
                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(strSql.ToString(), param);
                result = param.Get<int>("@returnid");
            }
            return result;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(HomePage model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update HomePage set ");
            strSql.Append(" CompetitionIntroduction = @CompetitionIntroduction , ");
            strSql.Append(" Title1 = @Title1 , ");
            strSql.Append(" Title2 = @Title2  ");
            strSql.Append(" Title3 = @Title3  ");
            strSql.Append(" QRCodeImgPath = @QRCodeImgPath  ");
            strSql.Append(" QRCodeIntroduction = @QRCodeIntroduction  ");
            strSql.Append(" ComTelConsultation = @ComTelConsultation  ");
            strSql.Append(" ComPhone = @ComPhone  ");
            strSql.Append(" ComQQ = @ComQQ  ");
            strSql.Append(" Step1Description = @Step1Description  ");
            strSql.Append(" Step2Description = @Step2Description  ");
            strSql.Append(" Step3Description = @Step3Description  ");
            strSql.Append(" Step4Description = @Step4Description  ");
            strSql.Append(" CollegeId = @CollegeId  ");         

            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@CompetitionIntroduction", model.CompetitionIntroduction, dbType: DbType.String);
                param.Add("@Title1", model.Title1, dbType: DbType.String);
                param.Add("@Title2", model.Title2, dbType: DbType.String);
                param.Add("@Title3", model.CollegeId, dbType: DbType.String);
                param.Add("@QRCodeImgPath", model.QRCodeImgPath, dbType: DbType.String);
                param.Add("@QRCodeIntroduction", model.QRCodeIntroduction, dbType: DbType.String);
                param.Add("@ComTelConsultation", model.ComTelConsultation, dbType: DbType.String);
                param.Add("@ComPhone", model.ComPhone, dbType: DbType.String);
                param.Add("@ComQQ", model.ComQQ, dbType: DbType.String);
                param.Add("@Step1Description", model.Step1Description, dbType: DbType.String);
                param.Add("@Step2Description", model.Step2Description, dbType: DbType.String);
                param.Add("@Step3Description", model.Step3Description, dbType: DbType.String);
                param.Add("@Step4Description", model.Step4Description, dbType: DbType.String);
                param.Add("@CollegeId", model.CollegeId, dbType: DbType.Int32);

                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        /// <summary>
        /// 更新首页-大赛介绍
        /// </summary>       
        public bool UpdateCompetitionIntroduction(string competitionIntroduction, int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update HomePage set ");
            strSql.Append(" CompetitionIntroduction = @CompetitionIntroduction ");
            strSql.Append(" where Id=@Id ");
            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", id, dbType: DbType.Int32);
                param.Add("@CompetitionIntroduction", competitionIntroduction, dbType: DbType.String);              
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        /// <summary>
        /// 更新首页-联系我们
        /// </summary>      
        public bool UpdateContactUS(HomePage model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update HomePage set ");
            strSql.Append(" ComTelConsultation = @ComTelConsultation, ");
            strSql.Append(" ComPhone = @ComPhone, ");
            strSql.Append(" ComQQ = @ComQQ ");
            strSql.Append(" where Id=@Id ");
            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ComTelConsultation", model.ComTelConsultation, dbType: DbType.String);
                param.Add("@ComPhone", model.ComPhone, dbType: DbType.String);
                param.Add("@ComQQ", model.ComQQ, dbType: DbType.String);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        /// <summary>
        /// 更新首页-活动日程
        /// </summary>      
        public bool UpdateActivitySchedule(HomePage model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update HomePage set ");
            strSql.Append(" Step1Description = @Step1Description, ");
            strSql.Append(" Step2Description = @Step2Description, ");
            strSql.Append(" Step3Description = @Step3Description ,");
            strSql.Append(" Step4Description = @Step4Description ");
            strSql.Append(" where Id=@Id ");
            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@Step1Description", model.Step1Description, dbType: DbType.String);
                param.Add("@Step2Description", model.Step2Description, dbType: DbType.String);
                param.Add("@Step3Description", model.Step3Description, dbType: DbType.String);
                param.Add("@Step4Description", model.Step4Description, dbType: DbType.String);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }


        /// <summary>
        /// 更新首页-图片标题（包括官网欢迎图片的标题文字、活动图片管理、二维码图片）
        /// </summary>
        /// <returns></returns>
        public bool UpdateImageTitle(HomePage model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update HomePage set ");
            strSql.Append(" Title1 = @Title1 ,");
            strSql.Append(" Title2 = @Title2, ");
            strSql.Append(" Title3 = @Title3 ,");
            strSql.Append(" QRCodeIntroduction = @QRCodeIntroduction, ");
            strSql.Append(" QRCodeImgPath = @QRCodeImgPath ");
            strSql.Append(" where Id=@Id ");
            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@Title1", model.Title1, dbType: DbType.String);
                param.Add("@Title2", model.Title2, dbType: DbType.String);
                param.Add("@Title3", model.Title3, dbType: DbType.String);
                param.Add("@QRCodeIntroduction", model.QRCodeIntroduction, dbType: DbType.String);
                param.Add("@QRCodeImgPath", model.QRCodeImgPath, dbType: DbType.String);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        #region  获取实体
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HomePage GetModel(int collegeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT [Id]
                                  ,[CompetitionIntroduction]
                                  ,[Title1]
                                  ,[Title2]
                                  ,[Title3]
                                  ,[QRCodeImgPath]
                                  ,[QRCodeIntroduction]
                                  ,[ComTelConsultation]
                                  ,[ComPhone]
                                  ,[ComQQ]
                                  ,[Step1Description]
                                  ,[Step2Description]
                                  ,[Step3Description]
                                  ,[Step4Description]
                                  ,[CollegeId]
                              FROM [dbo].[HomePage] ");
            strSql.Append(" where CollegeId=@CollegeId");

            HomePage model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@CollegeId", collegeId, dbType: DbType.Int32);
                model = conn.Query<HomePage>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion
    }
}
