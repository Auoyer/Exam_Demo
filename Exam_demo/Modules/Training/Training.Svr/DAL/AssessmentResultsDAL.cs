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
    public partial class AssessmentResultsDAL
    {
        public AssessmentResultsDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int TrainExamId, int TrainExamStatus)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from AssessmentResults where TrainExamId=@TrainExamId and TrainExamStatus=@TrainExamStatus");
            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@TrainExamId", TrainExamId, dbType: DbType.Int32);
                param.Add("@TrainExamStatus", TrainExamStatus, dbType: DbType.Int32);
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result > 0;
        }

        #endregion

        #region  新增

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddAssessmentResults(AssessmentResults model)
        {
            int result = 0;
            var conn = DBHelper.CreateConnection();
            conn.Open();
            var tran = conn.BeginTransaction();


            try
            {
                StringBuilder strSql = new StringBuilder();
                var param = new DynamicParameters();

                #region 成绩主表
                strSql.Append("insert into [AssessmentResults](");
                strSql.Append("UserID,CompetitionId,TrainExamId,TotalScore,SubjectiveResults,ObjectiveResults,TrainExamStatus,CreateTime)");

                strSql.Append(" values (");
                strSql.Append("@UserID,@CompetitionId,@TrainExamId,@TotalScore,@SubjectiveResults,@ObjectiveResults,@TrainExamStatus,@CreateTime)");
                strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                param.Add("@UserID", model.UserId, dbType: DbType.Int32);
                param.Add("@CompetitionId", model.CompetitionId, dbType: DbType.Int32);
                param.Add("@TrainExamId", model.TrainExamId, dbType: DbType.Int32);
                param.Add("@TotalScore", model.TotalScore, dbType: DbType.Decimal);
                param.Add("@SubjectiveResults", model.SubjectiveResults, dbType: DbType.Decimal);
                param.Add("@ObjectiveResults", model.ObjectiveResults, dbType: DbType.Decimal);
                param.Add("@TrainExamStatus", model.TrainExamStatus, dbType: DbType.Int32);
                param.Add("@CreateTime", model.CreateTime, dbType: DbType.DateTime);
                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(strSql.ToString(), param, tran);
                result = param.Get<int>("@returnid");
                #endregion

                #region 考核成绩明细
                if (model.DetailList != null && model.DetailList.Count > 0)
                {
                    foreach (var item in model.DetailList)
                    {
                        strSql.Clear();
                        param = new DynamicParameters();

                        strSql.Append("insert into AssessmentResultsDetail(");
                        strSql.Append("AssessmentResultsId,ExamPointType,ModularId,AssessmentPoint,Status,Score)");
                        strSql.Append(" values (");
                        strSql.Append("@AssessmentResultsId,@ExamPointType,@ModularId,@AssessmentPoint,@Status,@Score)");
                        param.Add("@AssessmentResultsId", result, dbType: DbType.Int32);
                        param.Add("@ExamPointType", item.ExamPointType, dbType: DbType.Int32);
                        param.Add("@ModularId", item.ModularId, dbType: DbType.Int32);
                        param.Add("@AssessmentPoint", item.AssessmentPoint, dbType: DbType.Int32);
                        param.Add("@Status", item.Status, dbType: DbType.Int32);
                        param.Add("@Score", item.Score, dbType: DbType.Decimal);

                        conn.Execute(strSql.ToString(), param, tran);
                    }
                }

                #endregion

                tran.Commit();
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("[回滚]保存成绩出错", ex);
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

        #region 根据考核Id获取未评分/已评分数量
        /// <summary>
        /// 根据考核Id获取未评分/已评分数量
        /// list[0]：未评分数量
        /// list[1]：已评分数量
        /// </summary>
        /// <param name="TrainExamId">考核Id</param>
        /// <returns></returns>
        public List<int> CountTrainExamStatus(int TrainExamId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from AssessmentResults where TrainExamId=@TrainExamId and TrainExamStatus=@TrainExamStatus1; ");
            strSql.Append("select count(1) from AssessmentResults where TrainExamId=@TrainExamId and TrainExamStatus=@TrainExamStatus2; ");

            List<int> result = new List<int>();
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@TrainExamId", TrainExamId, dbType: DbType.Int32);
                param.Add("@TrainExamStatus1", (int)TrainExamStatu.WaitGrade, dbType: DbType.Int32);
                param.Add("@TrainExamStatus2", (int)TrainExamStatu.AlreadGrade, dbType: DbType.Int32);
                using (var multi = conn.QueryMultiple(strSql.ToString(), param))
                {
                    result.Add(multi.Read<int>().FirstOrDefault());
                    result.Add(multi.Read<int>().FirstOrDefault());
                }
            }
            return result;
        }

        #endregion


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<AssessmentResults> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,UserId,CompetitionId,TrainExamId,TotalScore,SubjectiveResults,ObjectiveResults,TrainExamStatus,CreateTime ");
            strSql.Append(" FROM [AssessmentResults] ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<AssessmentResults> list = new List<AssessmentResults>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<AssessmentResults>(strSql.ToString()).ToList();
            }
            return list;
        }
        /// <summary>
        /// 根据CustomFilter获取where语句
        /// </summary>
        private string GetStrWhere(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (filter.UserId.HasValue)
            {
                strSql.Append(" and UserId=" + filter.UserId.Value);
            }
            if (filter.TrainExamId.HasValue)
            {
                strSql.Append(" and TrainExamId=" + filter.TrainExamId.Value);
            }
            if (filter.CompetitionId.HasValue)
            {
                strSql.AppendFormat(" and CompetitionId={0} ", filter.CompetitionId.Value);
            }
            if (filter.Id.HasValue)
            {
                strSql.AppendFormat(" and Id={0} ", filter.Id.Value);
            }
            //if (filter.KeyWords != null && filter.KeyWords!="")
            //{
            //    strSql.AppendFormat(" and Id={0} ", filter.Id.Value);
            //}
            return strSql.ToString();
        }




        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetScoreResultsPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "[AssessmentResults]";
            model.PKey = "Id";
            if (!filter.SortWay)
            { model.Sort = "TrainExamStatus asc,(SubjectiveResults+ObjectiveResults) desc"; }
            else { model.Sort = "TrainExamStatus asc,(SubjectiveResults+ObjectiveResults) asc"; }
          //  model.Sort = "Id";
            model.Fields = "Id,UserId,CompetitionId,TrainExamId,TotalScore,SubjectiveResults,ObjectiveResults,TrainExamStatus,CreateTime";
            model.Filter = GetStrWhere(filter);
            return model;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public AssessmentResults GetModelByUserId(int UserId, int TrainExamId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from [AssessmentResults] where UserId=@UserId and TrainExamId=@TrainExamId ");

            AssessmentResults model = new AssessmentResults();
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@UserId", UserId, dbType: DbType.Int32);
                param.Add("@TrainExamId", TrainExamId, dbType: DbType.Int32);
                model = conn.Query<AssessmentResults>(strSql.ToString(), param).FirstOrDefault();
                if (model != null)
                {
                    strSql.Clear();
                    strSql.Append("select * from [AssessmentResultsDetail] where AssessmentResultsId=@AssessmentResultsId; ");
                    param.Add("@AssessmentResultsId", model.Id, dbType: DbType.Int32);
                    using (var multiTwo = conn.QueryMultiple(strSql.ToString(), param))
                    {
                        model.DetailList = multiTwo.Read<AssessmentResultsDetail>().ToList();
                    }
                }
            }
            return model;
        }

        private string GetStrWhereTwo(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (filter.UserId.HasValue)
            {
                strSql.Append(" and am.UserId=" + filter.UserId.Value);
            }
            if (filter.TrainExamId.HasValue)
            {
                strSql.Append(" and am.TrainExamId=" + filter.TrainExamId.Value);
            }
            if (filter.CompetitionId.HasValue)//GetScoreResultsPageParams方法使用
            {
                strSql.Append(" and CompetitionId= " + filter.CompetitionId.Value);
            }
            return strSql.ToString();
        }


        public PageModel GetAssessmentExamPointByUserId(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "AssessmentResults as am join AssessmentResultsDetail as a on am.Id =a.AssessmentResultsId";
            model.PKey = "a.Id";
            if (string.IsNullOrEmpty(filter.SortName))
            {
                model.Sort = "a.AssessmentPoint";
            }
            else
            {
                model.Sort = filter.SortName + (filter.SortWay ? "asc" : "desc");
            }
            model.Fields = " a.Id,am.TrainExamId,am.CompetitionId,am.UserId,am.TotalScore,am.SubjectiveResults,am.ObjectiveResults,am.TrainExamStatus, a.ExamPointType,a.ModularId,a.AssessmentPoint,a.[Status],a.Score";
            model.Filter = GetStrWhereTwo(filter);
            return model;

        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public AssessmentResults GetModel(CustomFilter filter)
        {
            AssessmentResults model = new AssessmentResults();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,UserId,CompetitionId,TrainExamId,TotalScore,SubjectiveResults,ObjectiveResults,TrainExamStatus,CreateTime ");
            strSql.Append(" FROM [AssessmentResults] ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            strSql.Append(" select * from AssessmentResultsDetail ");
            strSql.AppendFormat(" where AssessmentResultsId= {0}", filter.Id);
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                using (var multi = conn.QueryMultiple(strSql.ToString()))
                {
                    model = multi.Read<AssessmentResults>().FirstOrDefault();
                    if (model != null)
                    {
                        model.DetailList = multi.Read<AssessmentResultsDetail>().ToList();
                    }
                }
            }
            return model;
        }
        #endregion


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(AssessmentResults model)
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
                strSql.Append("update [AssessmentResults] set ");
                strSql.Append("UserId=@UserId,");
                strSql.Append("CompetitionId=@CompetitionId,");
                strSql.Append("TrainExamId=@TrainExamId,");
                strSql.Append("TotalScore=@TotalScore,");
                strSql.Append("SubjectiveResults=@SubjectiveResults,");
                strSql.Append("ObjectiveResults=@ObjectiveResults,");
                strSql.Append("TrainExamStatus=@TrainExamStatus,");
                strSql.Append("CreateTime=@CreateTime");
                strSql.Append(" where Id=@Id ");

                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@CompetitionId", model.CompetitionId, dbType: DbType.Int32);
                param.Add("@TrainExamId", model.TrainExamId, dbType: DbType.Int32);
                param.Add("@TotalScore", model.TotalScore, dbType: DbType.Decimal);
                param.Add("@SubjectiveResults", model.SubjectiveResults, dbType: DbType.Decimal);
                param.Add("@ObjectiveResults", model.ObjectiveResults, dbType: DbType.Decimal);
                param.Add("@TrainExamStatus", model.TrainExamStatus, dbType: DbType.Int32);
                param.Add("@CreateTime", model.CreateTime, dbType: DbType.DateTime);
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

        /// <summary>
        /// 根据用户和大赛ID获取成绩
        /// </summary>
        public AssessmentResults GetARModel(int userId, int competitionId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from [AssessmentResults] where UserId=@UserId and CompetitionId=@CompetitionId ");

            AssessmentResults model = new AssessmentResults();
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@UserId", userId, dbType: DbType.Int32);
                param.Add("@CompetitionId", competitionId, dbType: DbType.Int32);
                model = conn.Query<AssessmentResults>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }
    }
}

