using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using Training.API;
using Utils;

namespace Training.Svr
{
    /// <summary>
    /// 数据访问类:TheoryChapter
    /// </summary>
    public partial class TheoryChapterDAL
    {
        public TheoryChapterDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id, string name, int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from TheoryChapter where ChapterName=@ChapterName ");
            strSql.Append("and Id<>@Id ");
            strSql.AppendFormat(" and UserId in (0,{1})", userId);
            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                param.Add("@ChapterName", name, dbType: DbType.String);
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result > 0;
        }

        #endregion

        #region  新增

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TheoryChapter model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TheoryChapter(");
            strSql.Append("ChapterName,ChapterSource,LibraryID,UserId,CreateDate)");

            strSql.Append(" values (");
            strSql.Append("@ChapterName,@ChapterSource,@LibraryID,@UserId,@CreateDate)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ChapterName", model.ChapterName, dbType: DbType.String);
                param.Add("@ChapterSource", model.ChapterSource, dbType: DbType.Int32);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@LibraryID", model.LibraryID, dbType: DbType.Int32);
                param.Add("@CreateDate", model.CreateDate, dbType: DbType.DateTime);
                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(strSql.ToString(), param);
                result = param.Get<int>("@returnid");
                conn.Execute(strSql.ToString(), param);
            }
            return result;
        }
        #endregion

        #region  更新
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(TheoryChapter model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TheoryChapter set ");
            strSql.Append("ChapterName=@ChapterName,");
            strSql.Append("ChapterSource=@ChapterSource,");
            strSql.Append("UserId=@UserId,");
            strSql.Append("LibraryID=@LibraryID,");
            strSql.Append("CreateDate=@CreateDate");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ChapterName", model.ChapterName, dbType: DbType.String);
                param.Add("@ChapterSource", model.ChapterSource, dbType: DbType.Int32);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@LibraryID", model.LibraryID, dbType: DbType.Int32);
                param.Add("@CreateDate", model.CreateDate, dbType: DbType.DateTime);
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
            strSql.Append("delete from TheoryChapter ");
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
        public TheoryChapter GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from TheoryChapter ");
            strSql.Append(" where Id=@Id ");

            TheoryChapter model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<TheoryChapter>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public List<TheoryChapter> GetChapterList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from TheoryChapter ");
            strSql.AppendFormat(" where UserId={0} ",filter.UserId);  
            List<TheoryChapter> model = null; 
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open(); 
                model = conn.Query<TheoryChapter>(strSql.ToString()).ToList();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<TheoryChapter> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ChapterName,ChapterSource,UserId,CreateDate ");
            strSql.Append(" FROM TheoryChapter ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<TheoryChapter> list = new List<TheoryChapter>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<TheoryChapter>(strSql.ToString()).ToList();
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
            if (filter.UserId.HasValue)
            {
                strSql.AppendFormat(" and CollegeId in (0,{0})", filter.CollegeId ?? 0);
            }
            return strSql.ToString();
        }

        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetTheoryChapterPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "TheoryChapter";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "*";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion


        /*=================================自定义分割线=============================*/

        /// <summary>
        /// 更新章节名称
        /// </summary>
        public bool UpdateTheoryChapter(TheoryChapter model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update TheoryChapter set ");
            strSql.Append(" ChapterName=@ChapterName ");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ChapterName", model.ChapterName, dbType: DbType.String);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        /// <summary>
        /// 获取章节的数量
        /// </summary>
        /// <returns></returns>
        public int GetTheoryChapter(int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from TheoryChapter as A join  where TheoryChapterHidden as B on A.Id<>B.TheoryChapterId where");
            strSql.AppendFormat(" UserId in (0,{0})", userId);
            strSql.AppendFormat(" and B.UserId=", userId);
            int result = 0;
            using (var conn = DBHelper.CreateConnection())
            {
                result = conn.Query<int>(strSql.ToString()).FirstOrDefault();
            }
            return result;
        }

        /// <summary>
        /// 新增章节
        /// </summary>
        /// <param name="model"></param>
        /// <param name="subTypes"></param>
        /// <returns></returns>
        public TheoryChapter AddCharpter(TheoryChapter model)
        {
            StringBuilder strSql = new StringBuilder();
            var param = new DynamicParameters();

            #region 题目本体
            strSql.Clear();
            strSql.Append("insert into TheoryChapter(");
            strSql.Append("ChapterName,CollegeId,UserId,CreateDate)");

            strSql.Append(" values (");
            strSql.Append("@ChapterName,@CollegeId,@UserId,@CreateDate)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            param.Add("@ChapterName", model.ChapterName, dbType: DbType.String);
            param.Add("@CollegeId", model.CollegeId, dbType: DbType.Int32);
            param.Add("@UserId", model.UserId, dbType: DbType.Int32);
            //param.Add("@LibraryID", model.LibraryID, dbType: DbType.Int32);
            param.Add("@CreateDate", model.CreateDate, dbType: DbType.DateTime);
            param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);

            #endregion

            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    conn.Execute(strSql.ToString(), param, tran);
                    model.Id = param.Get<int>("@returnid");

                    #region subTypes
                    if (model.SubTypes != null && model.SubTypes.Count > 0)
                    {
                        foreach (var item in model.SubTypes)
                        {
                            strSql.Clear();
                            param = new DynamicParameters();

                            strSql.Append("insert into TheoryQuestionType(");
                            strSql.Append("TheoryChapterId,TypeName,CreateDate)");

                            strSql.Append(" values (");
                            strSql.Append("@TheoryChapterId,@TypeName,@CreateDate)");
                            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                            param.Add("@TheoryChapterId", model.Id, dbType: DbType.Int32);
                            param.Add("@TypeName", item.TypeName, dbType: DbType.String);
                            //param.Add("@TypeSource", item.TypeSource, dbType: DbType.Int32);
                            //param.Add("@UserId", item.UserId, dbType: DbType.Int32);
                            param.Add("@CreateDate", item.CreateDate, dbType: DbType.DateTime);
                            param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);

                            conn.Execute(strSql.ToString(), param, tran);
                            item.Id = param.Get<int>("@returnid");
                            item.TheoryChapterId = model.Id;
                        }
                    }
                    #endregion

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("AddCharpter", ex);
                    tran.Rollback();
                    return null;
                }
            }
            return model;
        }
    }
}

