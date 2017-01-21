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
    /// 数据访问类:ResourceChapter
    /// </summary>
    public partial class ResourceChapterDAL
    {
        public ResourceChapterDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id,string name,int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ResourceChapter where ChapterName=@ChapterName ");
            strSql.Append("and Id<>@Id ");
            strSql.AppendFormat(" and UserId in (0,{0})", userId);
            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                param.Add("@ChapterName", name, dbType: DbType.String);
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result == 0;
        }

        #endregion

        #region  新增

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ResourceChapter model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ResourceChapter(");
            strSql.Append("ChapterName,ChapterSource,UserId,CreateDate)");

            strSql.Append(" values (");
            strSql.Append("@ChapterName,@ChapterSource,@UserId,@CreateDate)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ChapterName", model.ChapterName, dbType: DbType.String);
                param.Add("@ChapterSource", model.ChapterSource, dbType: DbType.Int32);
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
        public bool Update(ResourceChapter model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ResourceChapter set ");
            strSql.Append("ChapterName=@ChapterName,");
            strSql.Append("ChapterSource=@ChapterSource,");
            strSql.Append("UserId=@UserId,");
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
            strSql.Append("delete from ResourceChapter ");
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
        public ResourceChapter GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ChapterName,ChapterSource,UserId,CreateDate from ResourceChapter ");
            strSql.Append(" where Id=@Id ");

            ResourceChapter model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<ResourceChapter>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ResourceChapter> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ChapterName,ChapterSource,UserId,CreateDate ");
            strSql.Append(" FROM ResourceChapter ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<ResourceChapter> list = new List<ResourceChapter>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<ResourceChapter>(strSql.ToString()).ToList();
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
                strSql.AppendFormat(" and UserId in (0,{0})", filter.UserId);
            }
            return strSql.ToString();
        }

        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetResourceChapterPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "ResourceChapter";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,ChapterName,ChapterSource,UserId,CreateDate";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion


        /// <summary>
        /// 内置资源章节逻辑删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool TombstoneResourceChapter(int id,int userId)
        {
            int result = 0;
            StringBuilder strSql = new StringBuilder();
            var param = new DynamicParameters();
            strSql.Append("insert into ResourceChapterHidden(");
            strSql.Append("ResourceChapterId,UserId)");

            strSql.Append(" values (");
            strSql.Append("@ResourceChapterId,@UserId)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ResourceChapterId", id, dbType: DbType.Int32);
                param.Add("@UserId", userId, dbType: DbType.Int32);
                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(strSql.ToString(), param);
                result = param.Get<int>("@returnid");
            }
            return result > 0;
 
        }

        /// <summary>
        /// 获取资源章节数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetResourceChapterNum(int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ResourceChapter where ");
            strSql.AppendFormat(" UserId in (0,{0})", userId);
            strSql.Append(" and Id not in (select ResourceChapterId from ResourceChapterHidden ");
            strSql.AppendFormat(" where UserId={0})",userId);
            int result = 0;
            using (var conn = DBHelper.CreateConnection())
            {
                result = conn.Query<int>(strSql.ToString()).FirstOrDefault();
            }
            return result;
        }

        /// <summary>
        /// 获取资源章节列表，用这个方法
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<ResourceChapter> GetResourceChapterList(int userId)
        {
            List<ResourceChapter> result = new List<ResourceChapter>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from ResourceChapter ");
            strSql.AppendFormat(" where UserId in (0,{0})", userId);
            strSql.AppendFormat(" and Id not in(select ResourceChapterId from ResourceChapterHidden where UserId={0})",userId);
            using (var conn = DBHelper.CreateConnection())
            {
                result = conn.Query<ResourceChapter>(strSql.ToString()).ToList();
            }

            return result;
        }

    }
}

