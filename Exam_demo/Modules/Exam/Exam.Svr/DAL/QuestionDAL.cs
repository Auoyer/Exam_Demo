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
    /// 数据访问类:Question
    /// </summary>
    public partial class QuestionDAL
    {

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Question where Id=@Id ");

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
        public int Add(Question model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Question(");
            strSql.Append("Context,StructType,LibraryID,CharpterID,Analysis,Status,Source,UserId,CreatedTime)");

            strSql.Append(" values (");
            strSql.Append("@Context,@StructType,@LibraryID,@CharpterID,@Analysis,@Status,@Source,@UserId,@CreatedTime)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Context", model.Context, dbType: DbType.String);
                param.Add("@StructType", model.StructType, dbType: DbType.Int32);
                param.Add("@LibraryID", model.LibraryID, dbType: DbType.Int32);
                param.Add("@CharpterID", model.CharpterID, dbType: DbType.Int32);
                param.Add("@Analysis", model.Analysis, dbType: DbType.String);
                param.Add("@Status", model.Status, dbType: DbType.Int32);
                param.Add("@Source", model.Source, dbType: DbType.Int32);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@CreatedTime", model.CreatedTime, dbType: DbType.DateTime);
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
        public bool Update(Question model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Question set ");
            strSql.Append("Context=@Context,");
            strSql.Append("StructType=@StructType,");
            strSql.Append("LibraryID=@LibraryID,");
            strSql.Append("CharpterID=@CharpterID,");
            strSql.Append("Analysis=@Analysis,");
            strSql.Append("Status=@Status,");
            strSql.Append("Source=@Source,");
            strSql.Append("UserId=@UserId,");
            strSql.Append("CreatedTime=@CreatedTime");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@Context", model.Context, dbType: DbType.String);
                param.Add("@StructType", model.StructType, dbType: DbType.Int32);
                param.Add("@LibraryID", model.LibraryID, dbType: DbType.Int32);
                param.Add("@CharpterID", model.CharpterID, dbType: DbType.Int32);
                param.Add("@Analysis", model.Analysis, dbType: DbType.String);
                param.Add("@Status", model.Status, dbType: DbType.Int32);
                param.Add("@Source", model.Source, dbType: DbType.Int32);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@CreatedTime", model.CreatedTime, dbType: DbType.DateTime);
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

            bool result = false;
            var conn = DBHelper.CreateConnection();
            conn.Open();
            var tran = conn.BeginTransaction();
            try
            {
                StringBuilder strSql = new StringBuilder();
                var param = new DynamicParameters();

                #region 题干
                strSql.Clear();
                param = new DynamicParameters();
                strSql.Append("delete from Question ");
                strSql.Append(" where Id=@Id ");

                param.Add("@Id", Id, dbType: DbType.Int32);
                conn.Execute(strSql.ToString(), param, tran);
                #endregion

                #region 选项

                strSql.Clear();
                param = new DynamicParameters();
                strSql.Append("delete from QuestionOption ");
                strSql.Append(" where QuestionId=@QuestionId ");

                param.Add("@QuestionId", Id, dbType: DbType.Int32);
                conn.Execute(strSql.ToString(), param, tran);

                #endregion


                #region 答案

                strSql.Clear();
                param = new DynamicParameters();

                strSql.Append("delete from QuestionAnswer ");
                strSql.Append(" where QuestionId=@QuestionId ");

                param.Add("@QuestionId", Id, dbType: DbType.Int32);
                conn.Execute(strSql.ToString(), param, tran);
                #endregion

                #region 附件

                strSql.Clear();
                param = new DynamicParameters();

                strSql.Append("delete from QuestionAttachments ");
                strSql.Append(" where QuestionId=@QuestionId ");

                param.Add("@QuestionId", Id, dbType: DbType.Int32);
                conn.Execute(strSql.ToString(), param, tran);

                #endregion
                result = true;
                tran.Commit();
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("[回滚]删除题目出错", ex);
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

        #region  获取实体
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Question GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,Context,StructType,LibraryID,CharpterID,Analysis,Status,Source,UserId,CreatedTime from Question ");
            strSql.Append(" where Id=@Id ");

            Question model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<Question>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Question> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,Context,StructType,LibraryID,CharpterID,Analysis,Status,Source,UserId,CreatedTime ");
            strSql.Append(" FROM Question ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<Question> list = new List<Question>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<Question>(strSql.ToString()).ToList();
            }
            return list;
        }

        /// <summary>
        /// 根据CustomFilter获取where语句
        /// </summary>
        private string GetStrWhere(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (filter.Id.HasValue && filter.isBool == false)
            {
                strSql.Append(" and Id=" + filter.Id);
            }
            if (filter.isBool==true)
            {
                strSql.Append(" and Id !=" + filter.Id);
            }
            if (filter.CharpterID.HasValue)
            {
                if (filter.CharpterID != 0)
                {
                    strSql.Append(" and CharpterID=" + filter.CharpterID);
                }
                else {strSql.AppendFormat(" and CharpterID in ({0}) ",filter.Listtypeid); }
            }
            if (filter.StructType.HasValue && filter.StructType!=0)
            {
                strSql.Append(" and StructType=" + filter.StructType);
            }
            if (filter.Status.HasValue && filter.Status != -1)
            {
                strSql.Append(" and Status=" + filter.Status);
            }
            if (filter.Status.HasValue && filter.Status==-1)
            {
              
                strSql.Append(" and Status in (1,2)");
            }
            if (filter.LiburaryId.HasValue)
            {
                //if (filter.LiburaryId==1)
                //{
                //    strSql.Append(" and LibraryID=" + filter.LiburaryId );
                //}
                //else
                //{
                    strSql.Append(" and LibraryID=" + filter.LiburaryId + " and Id not in (select QuestionId from QuestionHidden where UserId=" + filter.UserId + " and IsDelete=1)");
               // }
                
            }
            
            if (filter.UserId.HasValue)
            {
                strSql.Append(" and (UserId=" + filter.UserId+" or UserId=0)");
            }
            if (filter.CollegeId.HasValue)
            {
                if (filter.BySchool)
                {
                    strSql.Append(" and CollegeId=" + filter.CollegeId);
                }
                else
                {
                    strSql.Append(" and (CollegeId=" + filter.CollegeId + " or CollegeId=0)");
                }
            }
            if (!string.IsNullOrEmpty(filter.Context))
            {
                strSql.AppendFormat(" and Context='" + filter.Context + "'");
            }
            if (!string.IsNullOrEmpty(filter.KeyWords))
            {
                strSql.AppendFormat(" and Context like '%{0}%'", filter.KeyWords);
            }
            return strSql.ToString();
        }

        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetQuestionPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "Question";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "*";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion


        /*=======================自定义分界线=====================*/

        /// <summary>
        /// 添加问题
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        internal int AddQuestion(Question model)
        {
            int result = 0;
            StringBuilder strSql = new StringBuilder();
            var param = new DynamicParameters();

            #region 题目本体
            strSql.Clear();
            strSql.Append("insert into Question(");
            strSql.Append("CollegeId,Context,StructType,CharpterID,Analysis,Status,UserId,CreatedTime,ViewStatus)");

            strSql.Append(" values (");
            strSql.Append("@CollegeId,@Context,@StructType,@CharpterID,@Analysis,@Status,@UserId,@CreatedTime,0)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            param.Add("@CollegeId", model.CollegeId, dbType: DbType.Int32);
            param.Add("@Context", model.Context, dbType: DbType.String);
            param.Add("@StructType", model.StructType, dbType: DbType.Int32);
            //param.Add("@LibraryID", model.LibraryID, dbType: DbType.Int32);
            param.Add("@CharpterID", model.CharpterID, dbType: DbType.Int32);
            param.Add("@Analysis", model.Analysis, dbType: DbType.String);
            param.Add("@Status", model.Status, dbType: DbType.Int32);
            //param.Add("@Source", model.Source, dbType: DbType.Int32);
            param.Add("@UserId", model.UserId, dbType: DbType.Int32);
            param.Add("@CreatedTime", model.CreatedTime, dbType: DbType.DateTime);
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

                    #region OptionList
                    if (model.OptionList != null && model.OptionList.Count > 0)
                    {
                        foreach (var item in model.OptionList)
                        {
                            strSql.Clear();
                            param = new DynamicParameters();

                            strSql.Append("insert into QuestionOption(");
                            strSql.Append("QuestionId,OptionName,Sort)");

                            strSql.Append(" values (");
                            strSql.Append("@QuestionId,@OptionName,@Sort)");

                            param.Add("@QuestionId", result, dbType: DbType.Int32);
                            param.Add("@OptionName", item.OptionName, dbType: DbType.String);
                            param.Add("@Sort", item.Sort, dbType: DbType.Int32);

                            conn.Execute(strSql.ToString(), param, tran);
                        }
                    }
                    #endregion

                    #region AnswerList
                    if (model.AnswerList != null && model.AnswerList.Count > 0)
                    {
                        foreach (var item in model.AnswerList)
                        {
                            strSql.Clear();
                            param = new DynamicParameters();

                            strSql.Append("insert into QuestionAnswer(");
                            strSql.Append("QuestionId,Answer,Sort)");

                            strSql.Append(" values (");
                            strSql.Append("@QuestionId,@Answer,@Sort)");

                            param.Add("@QuestionId", result, dbType: DbType.Int32);
                            param.Add("@Answer", item.Answer, dbType: DbType.Int32);
                            param.Add("@Sort", item.Sort, dbType: DbType.Int32);

                            conn.Execute(strSql.ToString(), param, tran);
                        }
                    }
                    #endregion

                    #region AttachmentList
                    if (model.AttachmentList != null && model.AttachmentList.Count > 0)
                    {
                        foreach (var item in model.AttachmentList)
                        {
                            strSql.Clear();
                            param = new DynamicParameters();

                            strSql.Append("insert into QuestionAttachments(");
                            strSql.Append("QuestionId,FileUrl,Name)");

                            strSql.Append(" values (");
                            strSql.Append("@QuestionId,@FileUrl,@Name)");

                            param.Add("@QuestionId", result, dbType: DbType.Int32);
                            param.Add("@FileUrl", item.FileUrl, dbType: DbType.String);
                            param.Add("@Name", item.Name, dbType: DbType.String);

                            conn.Execute(strSql.ToString(), param, tran);
                        }
                    }
                    #endregion

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("AddQuestion", ex);
                    tran.Rollback();
                    result = 0;
                }
            }
            return result;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Question GetQuestion(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from Question ");
            strSql.Append(" where Id=@Id ");

            strSql.Append(" select * from QuestionOption ");
            strSql.Append(" where QuestionId=@Id ");

            strSql.Append(" select * from QuestionAnswer ");
            strSql.Append(" where QuestionId=@Id ");

            strSql.Append(" select * from QuestionAttachments ");
            strSql.Append(" where QuestionId=@Id ");

            Question model = null;
            var param = new DynamicParameters();
            param.Add("Id", Id, dbType: DbType.Int32);
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                using (var multi = conn.QueryMultiple(strSql.ToString(), param))
                {
                    model = multi.Read<Question>().FirstOrDefault();
                    model.OptionList = multi.Read<QuestionOption>().ToList();
                    model.AnswerList = multi.Read<QuestionAnswer>().ToList();
                    model.AttachmentList = multi.Read<QuestionAttachments>().ToList();
                }
            }
            return model;
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="questions"></param>
        /// <returns></returns>
        internal List<Question> BatchAddQuestion(List<Question> questions)
        {
            StringBuilder strSql = new StringBuilder();
            var param = new DynamicParameters();

            using (var conn = DBHelper.CreateConnection())
             {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    foreach (var model in questions)
                    {
                        #region 题目本体
                        strSql.Clear();
                        strSql.Append("insert into Question(");
                        strSql.Append("CollegeId,Context,StructType,CharpterID,Analysis,Status,UserId,CreatedTime,ViewStatus)");

                        strSql.Append(" values (");
                        strSql.Append("@CollegeId,@Context,@StructType,@CharpterID,@Analysis,@Status,@UserId,@CreatedTime,0)");
                        strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                        param.Add("@CollegeId", model.CollegeId, dbType: DbType.Int32);
                        param.Add("@Context", model.Context, dbType: DbType.String);
                        param.Add("@StructType", model.StructType, dbType: DbType.Int32);
                        //param.Add("@LibraryID", model.LibraryID, dbType: DbType.Int32);
                        param.Add("@CharpterID", model.CharpterID, dbType: DbType.Int32);
                        param.Add("@Analysis", model.Analysis, dbType: DbType.String);
                        param.Add("@Status", model.Status, dbType: DbType.Int32);
                        //param.Add("@Source", model.Source, dbType: DbType.Int32);
                        param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                        param.Add("@CreatedTime", model.CreatedTime, dbType: DbType.DateTime);
                        param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);

                        #endregion

                        conn.Execute(strSql.ToString(), param, tran);
                        model.Id = param.Get<int>("@returnid");

                        #region OptionList
                        if (model.OptionList != null && model.OptionList.Count > 0)
                        {
                            foreach (var item in model.OptionList)
                            {
                                strSql.Clear();
                                param = new DynamicParameters();

                                strSql.Append("insert into QuestionOption(");
                                strSql.Append("QuestionId,OptionName,Sort)");

                                strSql.Append(" values (");
                                strSql.Append("@QuestionId,@OptionName,@Sort)");
                                strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                                param.Add("@QuestionId", model.Id, dbType: DbType.Int32);
                                param.Add("@OptionName", item.OptionName, dbType: DbType.String);
                                param.Add("@Sort", item.Sort, dbType: DbType.Int32);
                                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);

                                conn.Execute(strSql.ToString(), param, tran);
                                item.Id = param.Get<int>("@returnid");
                            }
                        }
                        #endregion

                        #region AnswerList
                        if (model.AnswerList != null && model.AnswerList.Count > 0)
                        {
                            foreach (var item in model.AnswerList)
                            {
                                strSql.Clear();
                                param = new DynamicParameters();

                                strSql.Append("insert into QuestionAnswer(");
                                strSql.Append("QuestionId,Answer,Sort)");

                                strSql.Append(" values (");
                                strSql.Append("@QuestionId,@Answer,@Sort)");
                                strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                                param.Add("@QuestionId", model.Id, dbType: DbType.Int32);
                                param.Add("@Answer", item.Answer, dbType: DbType.Int32);
                                param.Add("@Sort", item.Sort, dbType: DbType.Int32);
                                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);

                                conn.Execute(strSql.ToString(), param, tran);
                                item.Id = param.Get<int>("@returnid");
                            }
                        }
                        #endregion

                        #region AttachmentList
                        if (model.AttachmentList != null && model.AttachmentList.Count > 0)
                        {
                            foreach (var item in model.AttachmentList)
                            {
                                strSql.Clear();
                                param = new DynamicParameters();

                                strSql.Append("insert into QuestionAttachments(");
                                strSql.Append("QuestionId,FileUrl,Name)");

                                strSql.Append(" values (");
                                strSql.Append("@QuestionId,@FileUrl,@Name)");
                                strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                                param.Add("@QuestionId", model.Id, dbType: DbType.Int32);
                                param.Add("@FileUrl", item.FileUrl, dbType: DbType.String);
                                param.Add("@Name", item.Name, dbType: DbType.String);
                                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);

                                conn.Execute(strSql.ToString(), param, tran);
                                item.Id = param.Get<int>("@returnid");
                            }
                        }
                        #endregion
                    }

                    tran.Commit();
                    return questions;
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("AddQuestion", ex);
                    tran.Rollback();
                    return null;
                }
            }
        }

        /// <summary>
        /// 批量删除题目
        /// 定时器180分钟清除假删习题
        /// </summary>
        /// <param name="ids">伪删除试题集合</param>
        /// <returns>未组卷的试题集合</returns>
        internal List<int> RemoveQuestion(List<int> ids)
        {
            int result = 0;
            StringBuilder strSql = new StringBuilder();
            List<int> delete_question = new List<int>();

            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    //获取已组卷试题
                    strSql.Clear();
                    strSql.Append("select DISTINCT QuesionId from PaperDetail ");
                    List<int> no_delete_question = conn.Query<int>(strSql.ToString(), null, tran).ToList();

                    //筛选出未组卷题目
                    delete_question = ids.Except(no_delete_question).ToList();

                    if (delete_question != null && delete_question.Count > 0)
                    {
                        strSql.Clear();
                        strSql.Append("delete from Question where Id in @Ids ");
                        result = conn.Execute(strSql.ToString(), new { ids = delete_question.ToArray() }, tran);

                        if (result > 0)
                        {
                            strSql.Clear();
                            strSql.Append("delete from QuestionOption where QuestionId in @Ids ");
                            conn.Execute(strSql.ToString(), new { ids = delete_question.ToArray() }, tran);

                            strSql.Clear();
                            strSql.Append("delete from QuestionAnswer where QuestionId in @ids ");
                            conn.Execute(strSql.ToString(), new { ids = delete_question.ToArray() }, tran);

                            strSql.Clear();
                            strSql.Append("delete from QuestionAttachments where QuestionId in @ids ");
                            conn.Execute(strSql.ToString(), new { ids = delete_question.ToArray() }, tran);
                        }
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("RemoveQuestion", ex);
                    tran.Rollback();
                    result = 0;
                    delete_question = new List<int>();
                }
            }
            return delete_question;
        }

        /// <summary>
        /// 更新问题状态
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        internal bool EditQuestionStatus(int questionId, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Question set ");
            strSql.Append("Status=@Status");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", questionId, dbType: DbType.Int32);
                param.Add("@Status", status, dbType: DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        /// <summary>
        /// 批量更新问题状态
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        internal bool EditQuestionStatus2(List<int> ids)
        {
            int result = 0;
            StringBuilder strSql = new StringBuilder();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    strSql.Append("update Question set ");
                    strSql.Append("Status=3");
                    strSql.Append(" where Id in @Id ");

                    result = conn.Execute(strSql.ToString(), new { id = ids.ToArray() }, tran);                  
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("RemoveQuestion", ex);
                    tran.Rollback();
                    result = 0;
                }
            }
            return result > 0;
        }

        /// <summary>
        /// 批量获取问题的扩展信息
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="OptionList"></param>
        /// <param name="AnswerList"></param>
        /// <param name="AttachmentList"></param>
        internal void GetQuestionExInfo(List<int> questionId, Question model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,QuestionId,OptionName,Sort from QuestionOption ");
            strSql.Append(" where QuestionId in @Id ");

            strSql.Append("select Id,QuestionId,Answer,Sort from QuestionAnswer ");
            strSql.Append(" where QuestionId in @Id ");

            strSql.Append("select Id,QuestionId,FileUrl,Name from QuestionAttachments ");
            strSql.Append(" where QuestionId in @Id ");

            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                using (var multi = conn.QueryMultiple(strSql.ToString(), new { Id = questionId.ToArray() }))
                {
                    model.OptionList = multi.Read<QuestionOption>().ToList();
                    model.AnswerList = multi.Read<QuestionAnswer>().ToList();
                    model.AttachmentList = multi.Read<QuestionAttachments>().ToList();
                }
            }
        }

        /// <summary>
        /// 修改习题查看状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal bool ChangeQuestionViewStatus(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Question set ");
            strSql.Append("ViewStatus=1 ");
            strSql.Append("where Id=@Id ");

            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                try
                {
                    conn.Open();
                    param.Add("@Id", id, dbType: DbType.Int32);
                    return conn.Execute(strSql.ToString(), param) > 0;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}