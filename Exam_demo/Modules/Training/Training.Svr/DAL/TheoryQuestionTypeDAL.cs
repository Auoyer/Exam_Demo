using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using Training.API;

namespace Training.Svr
{
    /// <summary>
    /// 数据访问类:TheoryQuestionType
    /// </summary>
    public partial class TheoryQuestionTypeDAL
    {
        public TheoryQuestionTypeDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int theoryChapterId, string name, int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from TheoryQuestionType where TheoryChapterId=@TheoryChapterId and TypeName=@TypeName ");
            strSql.Append("and Id<>@Id");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                param.Add("@TypeName", name, dbType: DbType.String);
                param.Add("@TheoryChapterId", theoryChapterId, dbType: DbType.Int32);
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result > 0;
        }

        #endregion

        #region  新增

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TheoryQuestionType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TheoryQuestionType(");
            strSql.Append("TheoryChapterId,TypeName,TypeSource,UserId,CreateDate)");
            strSql.Append(" values (");
            strSql.Append("@TheoryChapterId,@TypeName,@TypeSource,@UserId,@CreateDate)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@TheoryChapterId", model.TheoryChapterId, dbType: DbType.Int32);
                param.Add("@TypeName", model.TypeName, dbType: DbType.String);
                param.Add("@TypeSource", model.TypeSource, dbType: DbType.Int32);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@CreateDate", model.CreateDate, dbType: DbType.DateTime);
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
        public bool Update(TheoryQuestionType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TheoryQuestionType set ");
            strSql.Append("TheoryChapterId=@TheoryChapterId,");
            strSql.Append("TypeName=@TypeName,");
            strSql.Append("TypeSource=@TypeSource,");
            strSql.Append("UserId=@UserId,");
            strSql.Append("CreateDate=@CreateDate");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@TheoryChapterId", model.TheoryChapterId, dbType: DbType.Int32);
                param.Add("@TypeName", model.TypeName, dbType: DbType.String);
                param.Add("@TypeSource", model.TypeSource, dbType: DbType.Int32);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
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
            strSql.Append("delete from TheoryQuestionType ");
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

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete2(int TheoryChapterId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TheoryQuestionType ");
            strSql.Append(" where TheoryChapterId=@TheoryChapterId  and TypeSource=2");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@TheoryChapterId", TheoryChapterId, dbType: DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        #endregion

        #region  获取实体
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TheoryQuestionType GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from TheoryQuestionType ");
            strSql.Append(" where Id=@Id ");

            TheoryQuestionType model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<TheoryQuestionType>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<TheoryQuestionType> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM TheoryQuestionType ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<TheoryQuestionType> list = new List<TheoryQuestionType>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<TheoryQuestionType>(strSql.ToString()).ToList();
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
            if (filter.ChapterId.HasValue)
            {
                strSql.Append(" and TheoryChapterId=" + filter.ChapterId);
            }
            if (filter.Score.HasValue)
            {
                strSql.Append(" and TypeSource=" + filter.Score);
            }
            if (filter.CollegeId.HasValue)
            {
                strSql.Append(" and (CollegeId=" + filter.CollegeId + " or CollegeId=0)");
            }
            if (filter.UserId2.HasValue)
            {
                strSql.Append(" and (UserId=" + filter.UserId2 + " or UserId=0)");
            }
            if (filter.KeyField != null)
            {
                string TheoryChapterId = "";
                foreach (var item in filter.KeyField)
                {
                    TheoryChapterId += item + ",";
                }
                TheoryChapterId = TheoryChapterId.TrimEnd(',');
                if (!string.IsNullOrEmpty(TheoryChapterId))
                    strSql.AppendFormat(" and TheoryChapterId in ({0}) ", TheoryChapterId);
            }
            if (filter.UserId.HasValue)
            {
                strSql.AppendFormat(" and UserId={0} ", filter.UserId);
            }
            if (filter.ChapterId.HasValue)
            {
                strSql.Append(" and TheoryChapterId=" + filter.ChapterId);
            }
            return strSql.ToString();
        }

        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetTheoryQuestionTypePageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "TheoryQuestionType";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "*";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion

        /// <summary>
        /// 获取章节下题型的数量
        /// </summary>
        /// <param name="theoryChapterId"></param>
        /// <returns></returns>
        public int GettheoryQuestionTypeNum(int theoryChapterId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from TheoryQuestionType where TheoryChapterId=@TheoryChapterId ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@TheoryChapterId", theoryChapterId, dbType: DbType.Int32);
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result;

        }

        /*=======================自定义分界线=====================*/
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<TheoryQuestionType> GetList(List<int> TheoryChapterIdList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM TheoryQuestionType ");
            strSql.Append(" where TheoryChapterId in @TheoryChapterId ");

            List<TheoryQuestionType> list = new List<TheoryQuestionType>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<TheoryQuestionType>(strSql.ToString(), new { TheoryChapterId = TheoryChapterIdList.ToArray() }).ToList();
            }
            return list;
        }
    }
}

