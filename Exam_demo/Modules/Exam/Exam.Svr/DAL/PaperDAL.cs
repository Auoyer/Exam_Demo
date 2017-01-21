using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using Exam.API;
using Utils;

namespace Exam.Svr
{
    /// <summary>
    /// 数据访问类:Paper
    /// </summary>
    public partial class PaperDAL
    {
        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Paper where Id=@Id ");

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

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists2(int QuestionId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from PaperDetail where QuesionId=@QuestionId ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@QuestionId", QuestionId, dbType: DbType.Int32);
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result > 0;
        }
        #endregion

        #region  新增

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Paper model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Paper(");
            strSql.Append("ExamPaperName,CompetitionId,UserId,Status,StartDate,EndDate,TotalScore)");

            strSql.Append(" values (");
            strSql.Append("@ExamPaperName,@CompetitionId,@UserId,@Status,@StartDate,@EndDate,@TotalScore)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ExamPaperName", model.ExamPaperName, dbType: DbType.String);
                param.Add("@CompetitionId", model.CompetitionId, dbType: DbType.Int32);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@Status", model.Status, dbType: DbType.Int32);
                param.Add("@StartDate", model.StartDate, dbType: DbType.DateTime);
                param.Add("@EndDate", model.EndDate, dbType: DbType.DateTime);
                param.Add("@TotalScore", model.TotalScore, dbType: DbType.Decimal);
                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(strSql.ToString(), param);
                result = param.Get<int>("@returnid");
            }
            return result;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add2(PaperDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PaperDetail(");
            strSql.Append("ExamPaperId,QuesionId)");

            strSql.Append(" values (");
            strSql.Append("@ExamPaperId,@QuesionId)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ExamPaperId", model.ExamPaperId, dbType: DbType.String);
                param.Add("@QuesionId", model.QuesionId, dbType: DbType.Int32);

                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(strSql.ToString(), param);
                // result = param.Get<int>("@returnid");
            }
            return result;
        }

        #endregion

        #region  更新
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Paper model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Paper set ");
            strSql.Append("ExamPaperName=@ExamPaperName,");
            strSql.Append("CompetitionId=@CompetitionId,");
            strSql.Append("UserId=@UserId,");
            strSql.Append("Status=@Status,");
            //strSql.Append("StartDate=@StartDate,");
            //strSql.Append("EndDate=@EndDate,");
            strSql.Append("TotalScore=@TotalScore");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ExamPaperName", model.ExamPaperName, dbType: DbType.String);
                param.Add("@CompetitionId", model.CompetitionId, dbType: DbType.Int32);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@Status", model.Status, dbType: DbType.Int32);
                //param.Add("@StartDate", model.StartDate, dbType: DbType.DateTime);
                //param.Add("@EndDate", model.EndDate, dbType: DbType.DateTime);
                param.Add("@TotalScore", model.TotalScore, dbType: DbType.Decimal);
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
            strSql.Append("delete from Paper ");
            strSql.Append(" where Id=@Id ");

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
        public Paper GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Paper ");
            strSql.Append(" where Id=@Id ");

            Paper model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<Paper>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表

        /// <summary>
        /// 根据CustomFilter获取where语句
        /// </summary>
        private string GetStrWhere(CustomFilter filter, PaperFilter pfilter)
        {
            StringBuilder strSql = new StringBuilder();
            if (filter.Id.HasValue)
            {
                strSql.Append(" and Paper.Id=" + filter.Id);
            }
            if (pfilter.AuthorID.HasValue)
            {
                strSql.Append(" and Paper.UserId=" + pfilter.AuthorID);
            }
            if (pfilter.AnswererID.HasValue)
            {
                strSql.Append(" and PaperUserSummary.UserId=" + pfilter.AnswererID);
            }
            if (filter.Status.HasValue)
            {
                strSql.Append(" and Paper.Status!=" + (int)ExamPaperStatus.Del);
            }
            //已评分、未评分列表状态使用
            if (filter.TheoryExamStatus.HasValue)
            {
                strSql.Append(" and Paper.Status=" + filter.TheoryExamStatus.Value);
            }
            if (filter.IsOverEndTime)
            {
                strSql.Append(" and Paper.EndDate <= getdate()");
            }
            if (!string.IsNullOrEmpty(filter.KeyWords))
            {
                strSql.AppendFormat(" and Paper.ExamPaperName like ('%{0}%') ", filter.KeyWords);
            }
            if (filter.UserId.HasValue)
            {
                strSql.Append(" and Paper.UserId=" + filter.UserId.Value);
            }
            return strSql.ToString();
        }


        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetPaperPageParams(CustomFilter filter, PaperFilter pfilter)
        {
            PageModel model = new PageModel();
            model.Tables = pfilter.AnswererID.HasValue ? "Paper,left join PaperUserSummary on Paper.Id = PaperUserSummary.ExamPaperId " : "Paper";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = pfilter.AnswererID.HasValue ? "Paper.Id,ExamPaperName,CompetitionId,Paper.UserId,Status,StartDate,EndDate,Paper.TotalScore,PaperUserSummary.Score as UserSumScore"
            : "Paper.Id,ExamPaperName,CompetitionId,Paper.UserId,Status,StartDate,EndDate,Paper.TotalScore";
            model.Filter = GetStrWhere(filter, pfilter);
            return model;
        }




        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetList(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "Paper";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "*";
            model.Filter = GetStrWhereCopy(filter);
            return model;
        }

        #endregion

        /*=======================自定义分界线=====================*/

        /// <summary>
        /// 新增一张试卷
        /// </summary>
        /// <param name="model"></param>
        /// <param name="charpterList"></param>
        /// <param name="details"></param>
        /// <param name="scoreInfo"></param>
        /// <returns></returns>
        public int AddPaper(Paper model)
        {
            int result = 0;
            StringBuilder strSql = new StringBuilder();
            var param = new DynamicParameters();

            #region 试卷本体
            strSql.Clear();
            strSql.Append("insert into Paper(");
            strSql.Append("ExamPaperName,CompetitionId,FormType,UserId,Status,TotalScore)");
            strSql.Append(" values (");
            strSql.Append("@ExamPaperName,@CompetitionId,@FormType,@UserId,@Status,@TotalScore)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            param.Add("@ExamPaperName", model.ExamPaperName, dbType: DbType.String);
            param.Add("@CompetitionId", model.CompetitionId, dbType: DbType.Int32);
            param.Add("@FormType", model.FormType, dbType: DbType.Int32);
            param.Add("@UserId", model.UserId, dbType: DbType.Int32);
            param.Add("@Status", model.Status, dbType: DbType.Int32);
            //param.Add("@StartDate", model.StartDate, dbType: DbType.DateTime);
            //param.Add("@EndDate", model.EndDate, dbType: DbType.DateTime);
            param.Add("@TotalScore", model.TotalScore, dbType: DbType.Decimal);
            param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);

            #endregion

            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    conn.Execute(strSql.ToString(), param, tran);
                    result = param.Get<int>("@returnid");

                    #region charpterList
                    if (model.CharpterList != null && model.CharpterList.Count > 0)
                    {

                        foreach (var item in model.CharpterList)
                        {
                            strSql.Clear();
                            param = new DynamicParameters();

                            strSql.Append("insert into PaperCharpter(");
                            strSql.Append("PaperID,CharpterID)");

                            strSql.Append(" values (");
                            strSql.Append("@PaperID,@CharpterID)");

                            param.Add("@PaperID", result, dbType: DbType.Int32);
                            param.Add("@CharpterID", item.CharpterID, dbType: DbType.Int32);
                            conn.Execute(strSql.ToString(), param, tran);
                        }
                    }
                    #endregion

                    #region details
                    if (model.Details != null && model.Details.Count > 0)
                    {
                        foreach (var item in model.Details)
                        {
                            strSql.Clear();
                            param = new DynamicParameters();
                            strSql.Append("insert into PaperDetail(");
                            strSql.Append("ExamPaperId,QuesionId)");

                            strSql.Append(" values (");
                            strSql.Append("@ExamPaperId,@QuesionId)");

                            param.Add("@ExamPaperId", result, dbType: DbType.Int32);
                            param.Add("@QuesionId", item.QuesionId, dbType: DbType.Int32);
                            conn.Execute(strSql.ToString(), param, tran);
                        }
                    }
                    #endregion

                    #region scoreInfo
                    if (model.ScoreInfo != null && model.ScoreInfo.Count > 0)
                    {
                        foreach (var item in model.ScoreInfo)
                        {
                            strSql.Clear();
                            param = new DynamicParameters();
                            strSql.Append("insert into PaperScore(");
                            strSql.Append("PaperID,CharpterID,Count,Score)");

                            strSql.Append(" values (");
                            strSql.Append("@PaperID,@CharpterID,@Count,@Score)");

                            param.Add("@PaperID", result, dbType: DbType.Int32);
                            param.Add("@CharpterID", item.CharpterID, dbType: DbType.String);
                            param.Add("@Count", item.Count, dbType: DbType.Int32);
                            param.Add("@Score", item.Score, dbType: DbType.Decimal);
                            conn.Execute(strSql.ToString(), param, tran);
                        }
                    }
                    #endregion

                    #region CompetitionInfo
                    if (model.CompetitionList != null && model.CompetitionList.Count > 0)
                    {
                        foreach (var item in model.CompetitionList)
                        {
                            strSql.Clear();
                            param = new DynamicParameters();
                            strSql.Append("insert into PaperCompetition(");
                            strSql.Append("ExamPaperId,CompetitionId)");

                            strSql.Append(" values (");
                            strSql.Append("@ExamPaperId,@CompetitionId)");

                            param.Add("@ExamPaperId", result, dbType: DbType.Int32);
                            param.Add("@CompetitionId", item.CompetitionId, dbType: DbType.Int32);
                            conn.Execute(strSql.ToString(), param, tran);
                        }
                    }
                    #endregion

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("AddPaper", ex);
                    tran.Rollback();
                    result = 0;
                }
            }
            return result;

        }

        /// <summary>
        /// 删除一张试卷
        /// </summary>
        /// <param name="paperId"></param>
        /// <returns></returns>
        public bool DeletePaper(int paperId)
        {
            int result = 0;
            StringBuilder strSql = new StringBuilder();
            var param = new DynamicParameters();

            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    strSql.Append("delete from Paper ");
                    strSql.Append(" where Id=@Id ");
                    param.Add("@Id", paperId, dbType: DbType.Int32);
                    result = conn.Execute(strSql.ToString(), param, tran);

                    if (result > 0)
                    {
                        strSql.Clear();
                        strSql.Append("delete from PaperCharpter where PaperID=@Id ");
                        conn.Execute(strSql.ToString(), param, tran);

                        strSql.Clear();
                        strSql.Append("delete from PaperDetail where ExamPaperId=@Id ");
                        conn.Execute(strSql.ToString(), param, tran);

                        strSql.Clear();
                        strSql.Append("delete from PaperScore where PaperID=@Id ");
                        conn.Execute(strSql.ToString(), param, tran);
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("DeletePaper", ex);
                    tran.Rollback();

                    result = 0;
                }
            }
            return result > 0;

        }

        /// <summary>
        /// 更新试卷试题
        /// </summary>
        public bool UpdatePaper(Paper model)
        {
            int result = 0;
            StringBuilder strSql = new StringBuilder();
            var param = new DynamicParameters();

            strSql.Append("update Paper set ");
            strSql.Append("ExamPaperName=@ExamPaperName,");
            strSql.Append("FormType=@FormType,");
            strSql.Append("StartDate=@StartDate,");
            strSql.Append("EndDate=@EndDate,");
            strSql.Append("TotalScore=@TotalScore");
            strSql.Append(" where Id=@Id ");

            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    param.Add("@Id", model.Id, dbType: DbType.Int32);
                    param.Add("@ExamPaperName", model.ExamPaperName, dbType: DbType.String);
                    param.Add("@FormType", model.FormType, dbType: DbType.Int32);
                    param.Add("@StartDate", model.StartDate, dbType: DbType.DateTime);
                    param.Add("@EndDate", model.EndDate, dbType: DbType.DateTime);
                    param.Add("@TotalScore", model.TotalScore, dbType: DbType.Decimal);
                    result = conn.Execute(strSql.ToString(), param, tran);

                    #region charpterList
                    //1.清空之前的
                    strSql.Clear();
                    param = new DynamicParameters();
                    strSql.Append("delete from PaperCharpter where ");
                    strSql.Append("PaperID=@PaperID ");
                    param.Add("@PaperID", model.Id, dbType: DbType.Int32);
                    conn.Execute(strSql.ToString(), param, tran);
                    //2.添加这次的
                    if (model.CharpterList != null && model.CharpterList.Count > 0)
                    {
                        foreach (var item in model.CharpterList)
                        {
                            strSql.Clear();
                            param = new DynamicParameters();
                            strSql.Append("insert into PaperCharpter(");
                            strSql.Append("PaperID,CharpterID)");
                            strSql.Append(" values (");
                            strSql.Append("@PaperID,@CharpterID)");
                            param.Add("@PaperID", model.Id, dbType: DbType.Int32);
                            param.Add("@CharpterID", item.CharpterID, dbType: DbType.Int32);
                            conn.Execute(strSql.ToString(), param, tran);
                        }
                    }
                    param.Add("@Id", model.Id, dbType: DbType.Int32);
                    param.Add("@ExamPaperName", model.ExamPaperName, dbType: DbType.String);
                    param.Add("@FormType", model.FormType, dbType: DbType.Int32);
                    param.Add("@StartDate", model.StartDate, dbType: DbType.DateTime);
                    param.Add("@EndDate", model.EndDate, dbType: DbType.DateTime);
                    result = conn.Execute(strSql.ToString(), param);
                    #endregion

                    #region details
                    //1.清空之前的
                    strSql.Clear();
                    param = new DynamicParameters();
                    strSql.Append("delete from PaperDetail where ");
                    strSql.Append("ExamPaperId=@PaperID ");
                    param.Add("@PaperID", model.Id, dbType: DbType.Int32);
                    conn.Execute(strSql.ToString(), param, tran);
                    //2.添加这次的
                    if (model.Details != null && model.Details.Count > 0)
                    {
                        foreach (var item in model.Details)
                        {
                            strSql.Clear();
                            param = new DynamicParameters();
                            strSql.Append("insert into PaperDetail(");
                            strSql.Append("ExamPaperId,QuesionId)");

                            strSql.Append(" values (");
                            strSql.Append("@ExamPaperId,@QuesionId)");

                            param.Add("@ExamPaperId", model.Id, dbType: DbType.Int32);
                            param.Add("@QuesionId", item.QuesionId, dbType: DbType.Int32);
                            conn.Execute(strSql.ToString(), param, tran);
                        }
                    }
                    #endregion

                    #region scoreInfo
                    //1.清空之前的
                    strSql.Clear();
                    param = new DynamicParameters();
                    strSql.Append("delete from PaperScore where ");
                    strSql.Append("PaperID=@PaperID ");
                    param.Add("@PaperID", model.Id, dbType: DbType.Int32);
                    conn.Execute(strSql.ToString(), param, tran);
                    //2.添加这次的
                    if (model.ScoreInfo != null && model.ScoreInfo.Count > 0)
                    {
                        foreach (var item in model.ScoreInfo)
                        {
                            strSql.Clear();
                            param = new DynamicParameters();
                            strSql.Append("insert into PaperScore(");
                            strSql.Append("PaperID,CharpterID,Count,Score)");

                            strSql.Append(" values (");
                            strSql.Append("@PaperID,@CharpterID,@Count,@Score)");

                            param.Add("@PaperID", model.Id, dbType: DbType.Int32);
                            param.Add("@CharpterID", item.CharpterID, dbType: DbType.String);
                            param.Add("@Count", item.Count, dbType: DbType.Int32);
                            param.Add("@Score", item.Score, dbType: DbType.Decimal);
                            conn.Execute(strSql.ToString(), param, tran);
                        }
                    }
                    #endregion

                    //#region ClassInfo
                    ////1.清空之前的
                    //strSql.Clear();
                    //param = new DynamicParameters();
                    //strSql.Append("delete from PaperClass where ");
                    //strSql.Append("ExamPaperId=@PaperID ");
                    //param.Add("@PaperID", model.Id, dbType: DbType.Int32);
                    //conn.Execute(strSql.ToString(), param, tran);
                    ////2.添加这次的
                    //if (model.ClassList != null && model.ClassList.Count > 0)
                    //{
                    //    foreach (var item in model.ClassList)
                    //    {
                    //        strSql.Clear();
                    //        param = new DynamicParameters();
                    //        strSql.Append("insert into PaperClass(");
                    //        strSql.Append("ExamPaperId,ClassId)");

                    //        strSql.Append(" values (");
                    //        strSql.Append("@ExamPaperId,@ClassId)");

                    //        param.Add("@ExamPaperId", model.Id, dbType: DbType.Int32);
                    //        param.Add("@ClassId", item.ClassId, dbType: DbType.Int32);
                    //        conn.Execute(strSql.ToString(), param, tran);
                    //    }
                    //}
                    //#endregion

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("UpdatePaper", ex);
                    tran.Rollback();
                    result = 0;
                }
            }
            return result > 0;
        }

        /// <summary>
        /// 更新试卷试题
        /// </summary>
        public bool UpdatePaperStatus(int papaerId, int statusId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Paper set ");
            strSql.Append(" Status=@Status ");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", papaerId, dbType: DbType.Int32);
                param.Add("@Status", statusId, dbType: DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        /// <summary>
        /// 获取大赛的理论试卷Id
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public int GetPaperIdByMatch(int matchId)
        {
            int result = 0;
            var param = new DynamicParameters();
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select max(Id) from Paper where CompetitionId={0}", matchId);
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Paper GetPaper(int Id, PaperFilter pFilter)
        {
            StringBuilder strSql = new StringBuilder();

            if (pFilter.AnswererID.HasValue)
            {
                strSql.Append(" select Paper.Id,ExamPaperName,Paper.CompetitionId,FormType,Paper.UserId,Paper.Status,StartDate,EndDate,Paper.TotalScore,PaperUserSummary.Score as UserSumScore");
                strSql.Append(" from  Paper  left join PaperUserSummary on Paper.Id = PaperUserSummary.ExamPaperId");
                strSql.Append(" and PaperUserSummary.UserId=@AnswererID");
                strSql.Append(" where Paper.Id =@Id");
            }
            else
            {
                strSql.Append(" select * from Paper ");
                strSql.Append(" where Id=@Id ");
            }
            if (pFilter.AuthorID.HasValue)
            {
                strSql.Append(" and Paper.UserId =" + pFilter.AuthorID.HasValue);
            }
            if (pFilter.CharpterList)
            {
                strSql.Append(" select * from PaperCharpter ");
                strSql.Append(" where PaperID=@Id ");
            }
            if (pFilter.Details)
            {
                strSql.Append(" select * from PaperDetail ");
                strSql.Append(" where ExamPaperId=@Id ");
            }
            if (pFilter.ScoreInfo)
            {
                strSql.Append(" select * from PaperScore ");
                strSql.Append(" where PaperID=@Id ");
            }
            //if (pFilter.ClassList)
            //{
            //    strSql.Append(" select ExamPaperId,ClassId from PaperClass ");
            //    strSql.Append(" where ExamPaperId=@Id ");
            //}
            if (pFilter.CompetitionList)
            {
                strSql.Append(" select * from PaperCompetition ");
                strSql.Append(" where ExamPaperId=@Id ");
            }
            if (pFilter.UserAnswer)
            {
                strSql.Append(" select * from PaperUserAnswer ");
                strSql.Append(" where ExamPaperId=@Id ");
                if (pFilter.AnswererID.HasValue)
                {
                    strSql.Append(" and UserId=@AnswererID");
                }
            }
            if (pFilter.UserAnswerResult)
            {
                strSql.Append(" select * from PaperUserAnswerResult ");
                strSql.Append(" where ExamPaperId=@Id ");
                if (pFilter.AnswererID.HasValue)
                {
                    strSql.Append(" and UserId=@AnswererID");
                }
            }
            if (pFilter.UserSummary)
            {
                strSql.Append(" select * from PaperUserSummary ");
                strSql.Append(" where ExamPaperId=@Id ");
                if (pFilter.AnswererID.HasValue)
                {
                    strSql.Append(" and UserId=@AnswererID");
                }
            }
            Paper model = new Paper();
            var param = new DynamicParameters();
            param.Add("@Id", Id, dbType: DbType.Int32);
            if (pFilter.AnswererID.HasValue)
            {
                param.Add("@AnswererID", pFilter.AnswererID.Value, dbType: DbType.Int32);
            }
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                using (var multi = conn.QueryMultiple(strSql.ToString(), param))
                {
                    model = multi.Read<Paper>().FirstOrDefault();
                    if (model != null)
                    {
                        if (pFilter.CharpterList)
                        {
                            model.CharpterList = multi.Read<PaperCharpter>().ToList();
                        }
                        if (pFilter.Details)
                        {
                            model.Details = multi.Read<PaperDetail>().ToList();
                        }
                        if (pFilter.ScoreInfo)
                        {
                            model.ScoreInfo = multi.Read<PaperScore>().ToList();
                        }
                        //if (pFilter.ClassList)
                        //{
                        //    model.ClassList = multi.Read<PaperClass>().ToList();
                        //}
                        if (pFilter.CompetitionList)
                        {
                            model.CompetitionList = multi.Read<PaperCompetition>().ToList();
                        }
                        if (pFilter.UserAnswer)
                        {
                            model.UserAnswer = multi.Read<PaperUserAnswer>().ToList();
                        }
                        if (pFilter.UserAnswerResult)
                        {
                            model.UserAnswerResult = multi.Read<PaperUserAnswerResult>().ToList();
                        }
                        if (pFilter.UserSummary)
                        {
                            model.UserSummary = multi.Read<PaperUserSummary>().ToList();
                        }
                    }
                }
            }
            return model;
        }

        public void GetPaperExInfo(List<int> paperIdList, PaperFilter pFilter, Paper model)
        {
            StringBuilder strSql = new StringBuilder();

            if (pFilter.CharpterList)
            {
                strSql.Append(" select * from PaperCharpter ");
                strSql.Append(" where PaperID in @Id ");
            }
            if (pFilter.Details)
            {
                strSql.Append(" select * from PaperDetail ");
                strSql.Append(" where ExamPaperId in @Id ");
            }
            if (pFilter.ScoreInfo)
            {
                strSql.Append(" select * from PaperScore ");
                strSql.Append(" where PaperID in @Id ");
            }
            if (pFilter.ClassList)
            {
                strSql.Append(" select * from PaperClass ");
                strSql.Append(" where ExamPaperId in @Id ");
            }
            if (pFilter.UserAnswer)
            {
                strSql.Append(" select * from PaperUserAnswer ");
                strSql.Append(" where ExamPaperId in @Id ");
                if (pFilter.AnswererID.HasValue)
                {
                    strSql.Append(" and UserId=@AnswererID");
                }
            }
            if (pFilter.UserAnswerResult)
            {
                strSql.Append(" select * from PaperUserAnswerResult ");
                strSql.Append(" where ExamPaperId in @Id ");
                if (pFilter.AnswererID.HasValue)
                {
                    strSql.Append(" and UserId=@AnswererID");
                }
            }
            if (pFilter.UserSummary)
            {
                strSql.Append(" select * from PaperUserSummary ");
                strSql.Append(" where ExamPaperId in @Id ");
                if (pFilter.AnswererID.HasValue)
                {
                    strSql.Append(" and UserId=@AnswererID");
                }
            }
            if (strSql.Length <= 0)
            {
                return;
            }
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                var param1 = new
                {
                    Id = paperIdList.ToArray()
                };
                object param2 = null;
                if (pFilter.AnswererID.HasValue)
                {
                    param2 = new
                    {
                        Id = paperIdList.ToArray(),
                        AnswererID = pFilter.AnswererID.Value
                    };
                }
                using (var multi = conn.QueryMultiple(strSql.ToString(),
                    pFilter.AnswererID.HasValue ? param2 : param1))
                {
                    if (pFilter.CharpterList)
                    {
                        model.CharpterList = multi.Read<PaperCharpter>().ToList();
                    }
                    if (pFilter.Details)
                    {
                        model.Details = multi.Read<PaperDetail>().ToList();
                    }
                    if (pFilter.ScoreInfo)
                    {
                        model.ScoreInfo = multi.Read<PaperScore>().ToList();
                    }
                    if (pFilter.ClassList)
                    {
                        model.ClassList = multi.Read<PaperClass>().ToList();
                    }
                    if (pFilter.UserAnswer)
                    {
                        model.UserAnswer = multi.Read<PaperUserAnswer>().ToList();
                    }
                    if (pFilter.UserAnswerResult)
                    {
                        model.UserAnswerResult = multi.Read<PaperUserAnswerResult>().ToList();
                    }
                    if (pFilter.UserSummary)
                    {
                        model.UserSummary = multi.Read<PaperUserSummary>().ToList();
                    }
                }
            }
        }

        #region 班级集合中，是否有正在进行的卷子
        /// <summary>
        /// 班级集合中，是否有正在进行的卷子
        /// </summary>
        /// <param name="classId">班级集合</param>
        /// <returns></returns>
        public bool IsPaperPublish(List<int> classId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM Paper WHERE Status=@Status AND GETDATE()<=EndDate ");
            strSql.Append(" AND Id IN (SELECT ExamPaperId FROM PaperClass WHERE ClassId IN @classIds)");

            int result = 0;
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                result = conn.Query<int>(strSql.ToString(), new { Status = (int)ExamPaperStatus.Publish, classIds = classId.ToArray() }).FirstOrDefault();
            }
            return result > 0;
        }
        #endregion

        public bool AddPaperDetail(List<PaperDetail> List)
        {
            int result = 0;
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    foreach (var item in List)
                    {
                        var param = new DynamicParameters();
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("insert into PaperDetail(");
                        strSql.Append("ExamPaperId,QuesionId)");
                        strSql.Append(" values (");
                        strSql.Append("@ExamPaperId,@QuesionId)");
                        strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");
                        param.Add("@ExamPaperId", item.ExamPaperId, dbType: DbType.Int16);
                        param.Add("@QuesionId", item.QuesionId, dbType: DbType.Int16);
                        param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                        result = conn.Execute(strSql.ToString(), param, tran);


                    }
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
            return result > 0;
        }
        public bool UpdatePaperDetail(PaperDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PaperDetail set ");
            strSql.Append("ExamPaperId=@ExamPaperId,");
            strSql.Append("QuesionId=@QuesionId");
            strSql.Append(" where ExamPaperId=@ExamPaperId and QuesionId=@QuesionId ");
            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ExamPaperId", model.ExamPaperId, dbType: DbType.Int16);
                param.Add("@QuesionId", model.QuesionId, dbType: DbType.Int16);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        public bool DeletePaperDetail(PaperDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Delete PaperDetail ");
            strSql.Append(" where ExamPaperId=@ExamPaperId ");
            strSql.Append(" and QuesionId=@QuesionId ");
            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ExamPaperId", model.ExamPaperId, dbType: DbType.Int16);
                param.Add("@QuesionId", model.QuesionId, dbType: DbType.Int16);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        public bool UpdatePaperScore(PaperScore model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PaperScore set ");
            strSql.Append("PaperID=@PaperID,");
            strSql.Append("CharpterID=@CharpterID,");
            strSql.Append("Count=@Count,");
            strSql.Append("Score=@Score");
            strSql.Append(" where PaperID=@PaperID");
            strSql.AppendFormat(" and CharpterID like ('%{0}%') ", model.CharpterID);
            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@PaperID", model.PaperID, dbType: DbType.Int32);
                param.Add("@CharpterID", model.CharpterID, dbType: DbType.String);
                param.Add("@Count", model.Count, dbType: DbType.Int32);
                param.Add("@Score", model.Score, dbType: DbType.Decimal);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }


        public bool DeletePaperScore(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete PaperScore ");
            strSql.Append(" where id=@Id ");
            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", id, dbType: DbType.Int16);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        public bool SavePaperScore(List<PaperScore> list)
        {
            using (var conn = DBHelper.CreateConnection())
            {
                int result = 0;
                StringBuilder strSql = new StringBuilder();
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    strSql.Append("Delete PaperScore ");
                    strSql.Append(" where PaperID=@PaperID");
                    var param = new DynamicParameters();
                    param.Add("@PaperID", list[0].PaperID, dbType: DbType.Int16);

                    result = conn.Execute(strSql.ToString(), param, tran);
                    foreach (var item in list)
                    {
                        strSql.Clear();
                        strSql.Append("insert into PaperScore(");
                        strSql.Append("PaperID,CharpterID,Count,Score)");
                        strSql.Append(" values (");
                        strSql.Append("@PaperID,@CharpterID,@Count,@Score)");
                        strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");
                        param.Add("@PaperID", item.PaperID, dbType: DbType.Int16);
                        param.Add("@CharpterID", item.CharpterID, dbType: DbType.String);
                        param.Add("@Count", item.Count, dbType: DbType.Int16);
                        param.Add("@Score", item.Score, dbType: DbType.Decimal);
                        param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                        result = conn.Execute(strSql.ToString(), param, tran);
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("UpdatePaperScoreList", ex);
                    tran.Rollback();
                    result = 0;
                }
                return result > 0;
            }
        }

        public bool UpdatePaperScoreList(List<PaperScore> list)
        {
            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    foreach (var item in list)
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update PaperScore set ");
                        strSql.Append("PaperID=@PaperID,");
                        strSql.Append("CharpterID=@CharpterID,");
                        strSql.Append("Count=@Count,");
                        strSql.Append("Score=@Score");
                        strSql.Append(" where Id=@Id ");
                        param.Add("@PaperID", item.PaperID, dbType: DbType.Int16);
                        param.Add("@CharpterID", item.CharpterID, dbType: DbType.String);
                        param.Add("@Count", item.Count, dbType: DbType.Int16);
                        param.Add("@Score", item.Score, dbType: DbType.Int16);
                        param.Add("@Id", item.Id, dbType: DbType.Int16);
                        result = conn.Execute(strSql.ToString(), param, tran);
                    }

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("UpdatePaperScoreList", ex);
                    tran.Rollback();
                    result = 0;
                }
            }
            return result > 0;
        }
        /// <summary>
        /// 根据CustomFilter获取where语句
        /// </summary>
        private string GetStrWhereCopy(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (filter.Id.HasValue)
            {
                strSql.Append(" and a.Id=" + filter.Id);
            }
            if (filter.Status.HasValue)
            {
                strSql.Append(" and a.Status=" + filter.Status);
            }
            if (filter.UserId.HasValue)
            {
                strSql.Append(" and a.UserId=" + filter.UserId);
            }

            return strSql.ToString();
        }

        /// <summary>
        /// 根据CustomFilter获取where语句（专用）
        /// </summary>
        private string GetStrWhereCopy2(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            //用于待考核列表
            if (filter.Id.HasValue)
            {
                strSql.Append(" and Id not in (select ExamPaperId from PaperUserSummary where PaperUserSummary.Status in (2,3) and PaperUserSummary.UserId=" + filter.UserId2 + ")");
            }
            //用于获取首页待考核数量
            if (filter.UserId2.HasValue && filter.isShow == true)
            {
                strSql.Append(" and Id not in (select ExamPaperId from PaperUserSummary where Status!=1 and UserId=" + filter.UserId2 + ")");

            }


            if (filter.UserId.HasValue)
            {
                strSql.Append(" and UserId=" + filter.UserId);
            }

            if (filter.Status.HasValue && filter.Status != 3 && filter.Status != 23 && filter.Status != -1)
            {
                strSql.Append(" and Status=" + filter.Status);
            }
            if (filter.Status == -1)
            {
                strSql.Append(" and Status in (2,3)");
            }
            if (filter.Status == 23)
            {
                strSql.Append(" and Status in (2,3,4)");
            }
            if (filter.isShow == true)
            {
                strSql.Append(" and StartDate<GETDATE() and EndDate>GETDATE()");
            }
            if (filter.IsLessThanCurrentDate == true)
            {
                strSql.Append(" and StartDate<GETDATE() ");
            }
            if (filter.LiburaryId.HasValue)
            {
                strSql.Append(" and CompetitionId= " + filter.CompetitionId);
            }
            return strSql.ToString();
        }

        /// <summary>
        /// 学生端理论考核（待考核）列表联表获取
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></return>
        public PageModel GetPaperList(CustomFilter filter)
        {
            //特殊处理

            StringBuilder strTable = new StringBuilder();
            strTable.Append(" Paper ");
            strTable.Append(" inner join PaperClass  on  PaperClass.ExamPaperId=Paper.Id ");
            if (filter.ClassId != 0 && filter.ClassId != null)
            {
                strTable.Append(" and PaperClass.ClassId=" + filter.ClassId + " ");
            }
            PageModel model = new PageModel();
            model.Tables = strTable.ToString();
            model.PKey = "Paper.Id";
            if (string.IsNullOrEmpty(filter.SortName))
            {
                model.Sort = " StartDate desc";
            }
            else
            {
                model.Sort = filter.SortName + (filter.SortWay ? " desc" : " asc");
            }
            model.Fields = " Paper.Id, Paper.ExamPaperName, Paper.CompetitionId, Paper.FormType, Paper.UserId, Paper.Status, Paper.StartDate, Paper.EndDate, Paper.TotalScore";
            model.Filter = GetStrWhereCopy2(filter);
            return model;
        }

        /// <summary>
        /// 获取班级理论考试月平局分
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<TheoryClassScore> GetTheoryClassScoreList(int userId)
        {
            List<TheoryClassScore> result = new List<TheoryClassScore>();
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@UserId", userId, dbType: DbType.Int32);

                result = conn.Query<TheoryClassScore>("Proc_ClassTheorySorce", param, commandType: CommandType.StoredProcedure).ToList();
            }
            return result;

        }

        /// <summary>
        /// 统计教师下未评分的理论考核/认证考试试卷
        /// </summary>
        /// <param name="userId">教师Id</param>
        /// <param name="CompetitionId">理论考核/认证考试</param>
        /// <returns></returns>
        public int CountSubmittedPaper(int userId, int CompetitionId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT COUNT(1) FROM Paper ");
            strSql.Append(" INNER JOIN PaperUserSummary ON Paper.Id = PaperUserSummary.ExamPaperId ");
            strSql.Append(" WHERE Paper.EndDate <= GETDATE() AND Paper.Status = @Status1 AND Paper.UserId = @UserId  AND Paper.CompetitionId = @CompetitionId  ");
            strSql.Append(" AND PaperUserSummary.Status = @Status2 ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@UserId", userId, dbType: DbType.Int32);
                param.Add("@Status1", (int)ExamPaperStatus.Publish, dbType: DbType.Int32);
                param.Add("@Status2", (int)PaperUserSummaryStatus.Submitted, dbType: DbType.Int32);
                param.Add("@CompetitionId", CompetitionId, dbType: DbType.Int32);
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result;
        }

        /// <summary>
        /// 获取用户考试试卷（存储过程）
        /// </summary>
        /// <param name="paperId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Paper GetUserPaperProc(int paperId, int userId)
        {
            Paper result = new Paper();
            var param = new DynamicParameters();

            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@paperId", paperId, dbType: DbType.Int32);
                param.Add("@userId", userId, dbType: DbType.Int32);

                using (var multi = conn.QueryMultiple("Proc_GetCurUserPaper", param, commandType: CommandType.StoredProcedure))
                {
                    result = multi.Read<Paper>().FirstOrDefault();
                    if (result != null)
                    {
                        result.CharpterList = multi.Read<PaperCharpter>().ToList();
                        result.Details = multi.Read<PaperDetail>().ToList();
                        result.ScoreInfo = multi.Read<PaperScore>().ToList();
                        result.UserAnswer = multi.Read<PaperUserAnswer>().ToList();
                        result.UserAnswerResult = multi.Read<PaperUserAnswerResult>().ToList();
                        result.UserSummary = multi.Read<PaperUserSummary>().ToList();
                    }
                }

                return result;
            }
        }
    }
}

