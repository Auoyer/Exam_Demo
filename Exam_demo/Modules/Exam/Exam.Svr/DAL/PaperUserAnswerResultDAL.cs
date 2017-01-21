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
    /// 数据访问类:PaperUserAnswerResult
    /// </summary>
    public partial class PaperUserAnswerResultDAL
    {

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from PaperUserAnswerResult where Id=@Id ");

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
        public int Add(PaperUserAnswerResult model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PaperUserAnswerResult(");
            strSql.Append("ExamPaperId,UserId,QuesionId,QuestionTypeId,StructType,QuestionScore,Result,UserScore)");

            strSql.Append(" values (");
            strSql.Append("@ExamPaperId,@UserId,@QuesionId,@QuestionTypeId,@StructType,@QuestionScore,@Result,@UserScore)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ExamPaperId", model.ExamPaperId, dbType: DbType.Int32);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@QuesionId", model.QuesionId, dbType: DbType.Int32);
                param.Add("@QuestionTypeId", model.QuestionTypeId, dbType: DbType.Int32);
                param.Add("@StructType", model.StructType, dbType: DbType.Int32);
                param.Add("@QuestionScore", model.QuestionScore, dbType: DbType.Decimal);
                param.Add("@Result", model.Result, dbType: DbType.Int32);
                param.Add("@UserScore", model.UserScore, dbType: DbType.Decimal);
                //param.Add("@Analyse", model.Analyse, dbType: DbType.String);
                //param.Add("@IsMark", model.IsMark, dbType: DbType.Boolean);
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
        public bool Update(PaperUserAnswerResult model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PaperUserAnswerResult set ");
            strSql.Append("ExamPaperId=@ExamPaperId,");
            strSql.Append("UserId=@UserId,");
            strSql.Append("QuesionId=@QuesionId,");
            strSql.Append("QuestionTypeId=@QuestionTypeId,");
            strSql.Append("StructType=@StructType,");
            strSql.Append("QuestionScore=@QuestionScore,");
            strSql.Append("Result=@Result,");
            strSql.Append("UserScore=@UserScore");
            //strSql.Append("Analyse=@Analyse,");
            //strSql.Append("IsMark=@IsMark");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ExamPaperId", model.ExamPaperId, dbType: DbType.Int32);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@QuesionId", model.QuesionId, dbType: DbType.Int32);
                param.Add("@QuestionTypeId", model.QuestionTypeId, dbType: DbType.Int32);
                param.Add("@StructType", model.StructType, dbType: DbType.Int32);
                param.Add("@QuestionScore", model.QuestionScore, dbType: DbType.Decimal);
                param.Add("@Result", model.Result, dbType: DbType.Int32);
                param.Add("@UserScore", model.UserScore, dbType: DbType.Decimal);
                //param.Add("@Analyse", model.Analyse, dbType: DbType.String);
                //param.Add("@IsMark", model.IsMark, dbType: DbType.Boolean);
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
            strSql.Append("delete from PaperUserAnswerResult ");
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
        public PaperUserAnswerResult GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from PaperUserAnswerResult ");
            strSql.Append(" where Id=@Id ");

            PaperUserAnswerResult model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<PaperUserAnswerResult>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<PaperUserAnswerResult> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM PaperUserAnswerResult ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<PaperUserAnswerResult> list = new List<PaperUserAnswerResult>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<PaperUserAnswerResult>(strSql.ToString()).ToList();
            }
            return list;
        }

        /// <summary>
        /// 多表联查获得数据列表
        /// </summary>
        public List<PaperUserAnswerResult> GetList1(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PD.QuesionId,ISNULL(RUAR.Result,1) Result,RUAR.UserScore ");
            strSql.AppendFormat(" FROM PaperDetail PD left join PaperUserAnswerResult RUAR on RUAR.QuesionId=PD.QuesionId and RUAR.UserId={0} and RUAR.ExamPaperId={1} ",filter.Id2,filter.UserId);
            strSql.AppendFormat(" where  PD.ExamPaperId={0} ", filter.Id2); 

            List<PaperUserAnswerResult> list = new List<PaperUserAnswerResult>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<PaperUserAnswerResult>(strSql.ToString()).ToList();
            }
            return list;
        } 


        /// <summary>
        /// 根据CustomFilter获取where语句
        /// </summary>
        private string GetStrWhere(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (filter.Id.HasValue)
            {
                strSql.Append(" and Id=" + filter.Id);
            }
            if (filter.Id2.HasValue)
            {
                strSql.Append(" and ExamPaperId=" + filter.Id2);
            }
            if (filter.UserId.HasValue)
            {
                strSql.Append(" and UserId=" + filter.UserId);
            }
            return strSql.ToString();
        }

        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetPaperUserAnswerResultPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "PaperUserAnswerResult";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "*";
            model.Filter = GetStrWhere(filter);
            return model;
        }


        /// <summary>
        /// 多表联查获取分页参数
        /// </summary>
        public PageModel GetPaperUserAnswerResultPageParams1(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables =string.Format( "PaperDetail PD left join PaperUserAnswerResult RUAR on RUAR.QuesionId=PD.QuesionId and RUAR.UserId={0} and RUAR.ExamPaperId={1} ",filter.UserId,filter.Id2);
            model.PKey = "PD.QuesionId";
            model.Sort = "RUAR.StructType";
            model.Fields = "PD.QuesionId,ISNULL(RUAR.Result,1) Result,RUAR.UserScore,RUAR.StructType";
            model.Filter = string.Format("and PD.ExamPaperId={0} ",filter.Id2);
            return model;
        }

        #endregion


        /*=========================自定义部分=========================*/

        public int AnswerQuetion(List<PaperUserAnswer> answers, PaperUserAnswerResult anResult)
        {
            int excuteRes = 0;
            StringBuilder strSql = new StringBuilder();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    if (anResult.Id > 0)
                    {
                        #region 更新
                        strSql.Append("update PaperUserAnswerResult set ");
                        strSql.Append("Result=@Result,");
                        strSql.Append("UserScore=@UserScore");
                        strSql.Append(" where Id=@Id ");

                        var param = new DynamicParameters();
                        param.Add("@Id", anResult.Id, dbType: DbType.Int32);
                        param.Add("@Result", anResult.Result, dbType: DbType.Int32);
                        param.Add("@UserScore", anResult.UserScore, dbType: DbType.Int32);
                        excuteRes = conn.Execute(strSql.ToString(), param, tran);
                        if (excuteRes <= 0)
                        {
                            tran.Rollback();
                            return 0;
                        }

                        strSql.Clear();
                        strSql.Append("delete from PaperUserAnswer where  ");
                        strSql.Append("ExamPaperId=@ExamPaperId and UserId=@UserId and QuesionId=@QuesionId  ");
                        param = new DynamicParameters();
                        param.Add("@ExamPaperId", anResult.ExamPaperId, dbType: DbType.Int32);
                        param.Add("@UserId", anResult.UserId, dbType: DbType.Int32);
                        param.Add("@QuesionId", anResult.QuesionId, dbType: DbType.Int32);
                        excuteRes = conn.Execute(strSql.ToString(), param, tran);


                        if (answers.Count > 0 && anResult.Result != (int)PaperUserAnswerStatus.Init)
                        {
                            foreach (var model in answers)
                            {
                                strSql.Clear();
                                strSql.Append("insert into PaperUserAnswer(");
                                strSql.Append("ExamPaperId,UserId,QuesionId,QuesionTypeId,Answer)");
                                strSql.Append(" values (");
                                strSql.Append("@ExamPaperId,@UserId,@QuesionId,@QuesionTypeId,@Answer)");

                                param = new DynamicParameters();
                                param.Add("@ExamPaperId", model.ExamPaperId, dbType: DbType.Int32);
                                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                                param.Add("@QuesionId", model.QuesionId, dbType: DbType.Int32);
                                param.Add("@QuesionTypeId", model.QuesionTypeId, dbType: DbType.Int32);
                                param.Add("@Answer", model.Answer, dbType: DbType.Int32);
                                excuteRes = conn.Execute(strSql.ToString(), param, tran);
                                if (excuteRes <= 0)
                                {
                                    tran.Rollback();
                                    return 0;
                                }
                            }
                        }

                        #endregion
                    }
                    else
                    {
                        #region 新增

                        strSql.Append("insert into PaperUserAnswerResult(");
                        strSql.Append("ExamPaperId,UserId,QuesionId,QuestionTypeId,StructType,QuestionScore,Result,UserScore)");
                        strSql.Append(" values (");
                        strSql.Append("@ExamPaperId,@UserId,@QuesionId,@QuestionTypeId,@StructType,@QuestionScore,@Result,@UserScore)");
                        strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                        var param = new DynamicParameters();
                        param.Add("@ExamPaperId", anResult.ExamPaperId, dbType: DbType.Int32);
                        param.Add("@UserId", anResult.UserId, dbType: DbType.Int32);
                        param.Add("@QuesionId", anResult.QuesionId, dbType: DbType.Int32);
                        param.Add("@QuestionTypeId", anResult.QuestionTypeId, dbType: DbType.Int32);
                        param.Add("@StructType", anResult.StructType, dbType: DbType.Int32);
                        param.Add("@QuestionScore", anResult.QuestionScore, dbType: DbType.Decimal);
                        param.Add("@Result", anResult.Result, dbType: DbType.Int32);
                        param.Add("@UserScore", anResult.UserScore, dbType: DbType.Decimal);
                        //param.Add("@Analyse", anResult.Analyse, dbType: DbType.String);
                        //param.Add("@IsMark", anResult.IsMark, dbType: DbType.Boolean);
                        param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                        excuteRes = conn.Execute(strSql.ToString(), param, tran);
                        if (excuteRes <= 0)
                        {
                            tran.Rollback();
                            return 0;
                        }
                        anResult.Id = param.Get<int>("@returnid");

                        if (answers.Count > 0)
                        {
                            foreach (var model in answers)
                            {
                                strSql.Clear();
                                strSql.Append("insert into PaperUserAnswer(");
                                strSql.Append("ExamPaperId,UserId,QuesionId,QuesionTypeId,Answer)");
                                strSql.Append(" values (");
                                strSql.Append("@ExamPaperId,@UserId,@QuesionId,@QuesionTypeId,@Answer)");

                                param = new DynamicParameters();
                                param.Add("@ExamPaperId", model.ExamPaperId, dbType: DbType.Int32);
                                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                                param.Add("@QuesionId", model.QuesionId, dbType: DbType.Int32);
                                param.Add("@QuesionTypeId", model.QuesionTypeId, dbType: DbType.Int32);
                                param.Add("@Answer", model.Answer, dbType: DbType.Int32);
                                excuteRes = conn.Execute(strSql.ToString(), param, tran);
                                if (excuteRes <= 0)
                                {
                                    tran.Rollback();
                                    return 0;
                                }
                            }
                        }

                        #endregion
                    }
                    tran.Commit();
                    return anResult.Id;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }


        internal List<PaperUserAnswerResult> Count(List<int> questionArray, int paperId, int? competitionId, int status)
        {
            StringBuilder strSql = new StringBuilder(); 
            strSql.AppendFormat("select QuesionId,sum(case when Result={0} then 1 else 0 end) AS [Result]", status);
            strSql.Append(" FROM PaperUserAnswerResult");
            strSql.Append(" right join paperUserSummary on paperUserSummary.userid = [PaperUserAnswerResult].userid");
            strSql.AppendFormat(" and PaperUserSummary.ExamPaperId = {0} ", paperId);
            if (competitionId.HasValue)
            {
                strSql.AppendFormat(" and CompetitionId = {0}", competitionId.Value);
            }
            strSql.AppendFormat(" where PaperUserAnswerResult.ExamPaperId = {0}", paperId);
            strSql.AppendFormat(" and QuesionId in ({0})", string.Join(",", questionArray));
            strSql.Append(" group by QuesionId");
            List<PaperUserAnswerResult> list = new List<PaperUserAnswerResult>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<PaperUserAnswerResult>(strSql.ToString()).ToList();
            }
            return list;
        }


        public int AnswerQuetion2(List<PaperUserAnswer> answers, PaperUserAnswerResult anResult)
        {
            int excuteRes = 0;
            StringBuilder strSql = new StringBuilder();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    var param = new DynamicParameters();
                    if (anResult.Id > 0)
                    {
                        #region 更新
                        strSql.Append("update PaperUserAnswerResult set ");
                        strSql.Append("Result=@Result,");
                        strSql.Append("UserScore=@UserScore");
                        strSql.Append(" where Id=@Id ");

                        
                        param.Add("@Id", anResult.Id, dbType: DbType.Int32);
                        param.Add("@Result", anResult.Result, dbType: DbType.Int32);
                        param.Add("@UserScore", anResult.UserScore, dbType: DbType.Int32);
                        excuteRes = conn.Execute(strSql.ToString(), param, tran);
                        if (excuteRes <= 0)
                        {
                            tran.Rollback();
                            return 0;
                        }

                        strSql.Clear();
                        strSql.Append("delete from PaperUserAnswer where  ");
                        strSql.Append("ExamPaperId=@ExamPaperId and UserId=@UserId and QuesionId=@QuesionId  ");
                        param = new DynamicParameters();
                        param.Add("@ExamPaperId", anResult.ExamPaperId, dbType: DbType.Int32);
                        param.Add("@UserId", anResult.UserId, dbType: DbType.Int32);
                        param.Add("@QuesionId", anResult.QuesionId, dbType: DbType.Int32);
                        excuteRes = conn.Execute(strSql.ToString(), param, tran);


                        if (answers.Count > 0 && anResult.Result != (int)PaperUserAnswerStatus.Init)
                        {
                            foreach (var model in answers)
                            {
                                strSql.Clear();
                                strSql.Append("insert into PaperUserAnswer(");
                                strSql.Append("ExamPaperId,UserId,QuesionId,QuesionTypeId,Answer)");
                                strSql.Append(" values (");
                                strSql.Append("@ExamPaperId,@UserId,@QuesionId,@QuesionTypeId,@Answer)");

                                param = new DynamicParameters();
                                param.Add("@ExamPaperId", model.ExamPaperId, dbType: DbType.Int32);
                                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                                param.Add("@QuesionId", model.QuesionId, dbType: DbType.Int32);
                                param.Add("@QuesionTypeId", model.QuesionTypeId, dbType: DbType.Int32);
                                param.Add("@Answer", model.Answer, dbType: DbType.Int32);
                                excuteRes = conn.Execute(strSql.ToString(), param, tran);
                                if (excuteRes <= 0)
                                {
                                    tran.Rollback();
                                    return 0;
                                }
                            }
                        }

                        #endregion
                    }
                    else
                    {
                        #region 新增

                        strSql.Append("insert into PaperUserAnswerResult(");
                        strSql.Append("ExamPaperId,UserId,QuesionId,QuestionTypeId,StructType,QuestionScore,Result,UserScore)");
                        strSql.Append(" values (");
                        strSql.Append("@ExamPaperId,@UserId,@QuesionId,@QuestionTypeId,@StructType,@QuestionScore,@Result,@UserScore)");
                        strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                        param.Add("@ExamPaperId", anResult.ExamPaperId, dbType: DbType.Int32);
                        param.Add("@UserId", anResult.UserId, dbType: DbType.Int32);
                        param.Add("@QuesionId", anResult.QuesionId, dbType: DbType.Int32);
                        param.Add("@QuestionTypeId", anResult.QuestionTypeId, dbType: DbType.Int32);
                        param.Add("@StructType", anResult.StructType, dbType: DbType.Int32);
                        param.Add("@QuestionScore", anResult.QuestionScore, dbType: DbType.Decimal);
                        param.Add("@Result", anResult.Result, dbType: DbType.Int32);
                        param.Add("@UserScore", anResult.UserScore, dbType: DbType.Decimal);
                        param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                        excuteRes = conn.Execute(strSql.ToString(), param, tran);
                        if (excuteRes <= 0)
                        {
                            tran.Rollback();
                            return 0;
                        }
                        anResult.Id = param.Get<int>("@returnid");

                        if (answers.Count > 0)
                        {
                            foreach (var model in answers)
                            {
                                strSql.Clear();
                                strSql.Append("insert into PaperUserAnswer(");
                                strSql.Append("ExamPaperId,UserId,QuesionId,QuesionTypeId,Answer)");
                                strSql.Append(" values (");
                                strSql.Append("@ExamPaperId,@UserId,@QuesionId,@QuesionTypeId,@Answer)");

                                param = new DynamicParameters();
                                param.Add("@ExamPaperId", model.ExamPaperId, dbType: DbType.Int32);
                                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                                param.Add("@QuesionId", model.QuesionId, dbType: DbType.Int32);
                                param.Add("@QuesionTypeId", model.QuesionTypeId, dbType: DbType.Int32);
                                param.Add("@Answer", model.Answer, dbType: DbType.Int32);
                                excuteRes = conn.Execute(strSql.ToString(), param, tran);
                                if (excuteRes <= 0)
                                {
                                    tran.Rollback();
                                    return 0;
                                }
                            }
                        }

                        #endregion
                    }

                    // 更新总成绩
                    strSql.Clear();
                    strSql.Append("update PaperUserSummary set Score=( select sum(UserScore) from PaperUserAnswerResult where ExamPaperId=@ExamPaperId and UserId=@UserId ) where ExamPaperId=@ExamPaperId and UserId=@UserId ");
                    param = new DynamicParameters();
                    param.Add("@ExamPaperId", anResult.ExamPaperId, dbType: DbType.Int32);
                    param.Add("@UserId", anResult.UserId, dbType: DbType.Int32);
                    excuteRes = conn.Execute(strSql.ToString(), param, tran);
                    if (excuteRes <= 0)
                    {
                        tran.Rollback();
                        return 0;
                    }

                    tran.Commit();
                    return anResult.Id;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
    }
}

