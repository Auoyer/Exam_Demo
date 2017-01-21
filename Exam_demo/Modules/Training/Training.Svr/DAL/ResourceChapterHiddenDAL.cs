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
    /// 数据访问类:ResourceChapterHidden
    /// </summary>
    public partial class ResourceChapterHiddenDAL
    {
        public ResourceChapterHiddenDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ResourceChapterHidden where Id=@Id ");

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
        public int Add(ResourceChapterHidden model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ResourceChapterHidden(");
            strSql.Append("ResourceChapterId,UserId)");

            strSql.Append(" values (");
            strSql.Append("@ResourceChapterId,@UserId)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ResourceChapterId", model.ResourceChapterId, dbType: DbType.Int32);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
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
        public bool Update(ResourceChapterHidden model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ResourceChapterHidden set ");
            strSql.Append("ResourceChapterId=@ResourceChapterId,");
            strSql.Append("UserId=@UserId");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ResourceChapterId", model.ResourceChapterId, dbType: DbType.Int32);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
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
            strSql.Append("delete from ResourceChapterHidden ");
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
        public ResourceChapterHidden GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ResourceChapterId,UserId from ResourceChapterHidden ");
            strSql.Append(" where Id=@Id ");

            ResourceChapterHidden model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<ResourceChapterHidden>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ResourceChapterHidden> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ResourceChapterId,UserId ");
            strSql.Append(" FROM ResourceChapterHidden ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<ResourceChapterHidden> list = new List<ResourceChapterHidden>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<ResourceChapterHidden>(strSql.ToString()).ToList();
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
            return strSql.ToString();
        }

        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetResourceChapterHiddenPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "ResourceChapterHidden";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,ResourceChapterId,UserId";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion




    }
}

