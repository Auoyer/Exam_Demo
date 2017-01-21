using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using Utils;
using Training.API;

namespace Training.Svr
{
    /// <summary>
    /// 数据访问类:Common
    /// </summary>
    public partial class AssessmentResultsDetailDAL
    {
        public AssessmentResultsDetailDAL()
        {
        }

        /// <summary>
        /// 获取成绩结果表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<AssessmentResultsDetail> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,AssessmentResultsId,ExamPointType,ModularId,AssessmentPoint,Status,Score ");
            strSql.Append(" FROM [AssessmentResultsDetail] ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<AssessmentResultsDetail> list = new List<AssessmentResultsDetail>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<AssessmentResultsDetail>(strSql.ToString()).ToList();
            }
            return list;
        }


        /// <summary>
        /// 根据CustomFilter获取where语句
        /// </summary>
        private string GetErrorRateWhere(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (filter.TrainExamId.HasValue)
            {
                strSql.Append(" and  AR.TrainExamId=" + filter.TrainExamId.Value);
            }
            if (filter.ClassId.HasValue)
            {
                strSql.Append(" and AR.ClassId="+filter.ClassId.Value);
            } 
            if (filter.ExamPointType.HasValue)
            {
                strSql.Append(" and ARD.ExamPointType="+filter.ExamPointType.Value);
            }
            return strSql.ToString();
        }

        /// <summary>
        /// 根据CustomFilter获取where语句
        /// </summary>
        private string GetStrWhere(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (filter.Id.HasValue)
            {
                strSql.Append(" and AssessmentResultsId=" + filter.Id.Value);
            }
            if (filter.ExamPointType.HasValue)
            {
                strSql.Append(" and ExamPointType= "+filter.ExamPointType.Value);
            }
            return strSql.ToString();
        }

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetScoreResultsPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "[AssessmentResultsDetail]";
            model.PKey = "Id";
            model.Sort = " ModularId,AssessmentPoint";
          //  model.Sort = "Id";
            model.Fields = "Id,AssessmentResultsId,ExamPointType,ModularId,AssessmentPoint,Status,Score";
            model.Filter = GetStrWhere(filter);
            return model;
        }
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetErrorRatePageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "[AssessmentResults] AR Left Join AssessmentResultsDetail ARD  on AR.Id=ARD.AssessmentResultsId ";
            model.PKey = " ARD.AssessmentPoint ";
            model.Fields = " ARD.ModularId,ARD.AssessmentPoint,Convert(decimal(18,2),(Convert(decimal(18,2), sum(Case ARD.Status when 2 then 1 else 0 end))/COUNT(1)))*100 as Rate ";
            model.Filter = GetErrorRateWhere(filter) + " group by ARD.ModularId,ARD.AssessmentPoint ";
            model.Sort = "ARD.ModularId ";
            return model;
        }
        #endregion

        #region  新增

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(AssessmentResultsDetail model)
        {
            int result = 0;
            var conn = DBHelper.CreateConnection();
            conn.Open();
            var tran = conn.BeginTransaction();


            try
            {
                StringBuilder strSql = new StringBuilder();
                var param = new DynamicParameters();

                #region 案例
                strSql.Append("insert into [AssessmentResultsDetail](");
                strSql.Append("AssessmentResultsId,ExamPointType,ModularId,AssessmentPoint,Status,Score)");

                strSql.Append(" values (");
                strSql.Append("@AssessmentResultsId,@ExamPointType,@ModularId,@AssessmentPoint,@Status,@Score)");
                strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                param.Add("@AssessmentResultsId", model.AssessmentResultsId, dbType: DbType.Int32);
                param.Add("@ExamPointType", model.ExamPointType, dbType: DbType.Int32);
                param.Add("@ModularId", model.ModularId, dbType: DbType.Int32);
                param.Add("@AssessmentPoint", model.AssessmentPoint, dbType: DbType.Int32);
                param.Add("@Status", model.Status, dbType: DbType.Int32);
                param.Add("@Score", model.Score, dbType: DbType.Decimal); 
                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(strSql.ToString(), param, tran);
                result = param.Get<int>("@returnid");
                #endregion 

                tran.Commit();
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("[回滚]新增考核得分出错", ex);
                tran.Rollback();
            }
            finally
            {
                if (tran != null)
                    tran.Dispose();
                if (conn != null)
                    conn.Close();
            }
            return result;
        }
        #endregion

        #region  更新
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(AssessmentResultsDetail model)
        {
            int result = 0;
            var conn = DBHelper.CreateConnection();
            conn.Open();
            var tran = conn.BeginTransaction();


            try
            {
                StringBuilder strSql = new StringBuilder();
                var param = new DynamicParameters();

                #region 案例
                strSql.Append("update [AssessmentResultsDetail] set ");
                strSql.Append("AssessmentResultsId=@AssessmentResultsId,");
                strSql.Append("ExamPointType=@ExamPointType,");
                strSql.Append("ModularId=@ModularId,");
                strSql.Append("AssessmentPoint=@AssessmentPoint,");
                strSql.Append("Status=@Status,");
                strSql.Append("Score=@Score"); 
                strSql.Append(" where Id=@Id ");

                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@AssessmentResultsId", model.AssessmentResultsId, dbType: DbType.Int32);
                param.Add("@ExamPointType", model.ExamPointType, dbType: DbType.Int32);
                param.Add("@ModularId", model.ModularId, dbType: DbType.Int32);
                param.Add("@AssessmentPoint", model.AssessmentPoint, dbType: DbType.Int32);
                param.Add("@Status", model.Status, dbType: DbType.Int32);
                param.Add("@Score", model.Score, dbType: DbType.Decimal);
                result = conn.Execute(strSql.ToString(), param, tran);
                #endregion 

                tran.Commit();
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("[回滚]修改考核得分出错", ex);
                tran.Rollback();
            }
            finally
            {
                if (tran != null)
                    tran.Dispose();
                if (conn != null)
                    conn.Close();
            }
            return result > 0;
        }

        #endregion

        #region 获取销售机会/实训考核正确数
        /// <summary>
        /// 获取销售机会/实训考核正确数
        /// </summary>
        /// <param name="pointArray">考核点Id集合</param>
        /// <param name="trainId">考核Id</param>
        /// <param name="classId">班级Id，为空时查全部</param>
        /// <param name="status">结果：正确or错误</param>
        /// <returns></returns>
        public List<AssessmentResultsDetail> CountStatus(List<int> pointArray, int trainId, int? classId, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select AssessmentPoint,sum(case when [Status]={0} then 1 else 0 end) AS [Status]", status);
            strSql.Append(" from AssessmentResultsDetail");
            strSql.Append(" right join AssessmentResults on AssessmentResults.Id = AssessmentResultsDetail.AssessmentResultsId");
            strSql.AppendFormat(" and AssessmentResults.TrainExamId = {0} ", trainId);
            if (classId.HasValue)
            {
                strSql.AppendFormat(" and AssessmentResults.ClassId = {0}", classId.Value);
            }
            strSql.AppendFormat(" where AssessmentResults.TrainExamId = {0}", trainId);
            strSql.AppendFormat(" and AssessmentPoint in ({0})", string.Join(",", pointArray));
            strSql.Append(" group by AssessmentPoint");
            List<AssessmentResultsDetail> list = new List<AssessmentResultsDetail>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<AssessmentResultsDetail>(strSql.ToString()).ToList();
            }
            return list;
        }
        #endregion



    }
}

