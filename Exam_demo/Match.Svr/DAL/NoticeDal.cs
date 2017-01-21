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
    public partial class NoticeDal
    {
        #region  新增
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Notice model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Notice(");
            strSql.Append("UserId,NoticeType,[Content],CreateTime,CollegeId)");

            strSql.Append(" values (");
            strSql.Append("@UserId,@NoticeType,@Content,@CreateTime,@CollegeId)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@NoticeType", model.NoticeType, dbType: DbType.Int32);
                param.Add("@Content", model.Content, dbType: DbType.String);
                param.Add("@CreateTime", model.CreateTime, dbType: DbType.DateTime);
                param.Add("@CollegeId", model.CollegeId, dbType: DbType.Int32);
                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(strSql.ToString(), param);
                result = param.Get<int>("@returnid");
            }
            return result;
        }
        #endregion

        #region  删除
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Notice ");
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
        public Notice GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [Id] ,[UserId] ,[NoticeType] ,[Content] ,[CreateTime] ,[ModifyTime],[CollegeId]  FROM [dbo].[Notice] ");
            strSql.Append(" where Id=@Id ");

            Notice model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<Notice>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetNoticePageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "Notice";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "[Id],[UserId],[NoticeType],[Content],[CreateTime],[ModifyTime],[CollegeId]";
            model.Filter = GetStrWhere(filter);
            
            model.Sort = " CreateTime desc";

            return model;
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
                strSql.Append(" and UserId =" + filter.UserId);
            }

            if (filter.CollegeId.HasValue)
            {
                strSql.Append(" and (CollegeId = 0 or CollegeId=" + filter.CollegeId + ")");
            }

            if (filter.CollegeId2.HasValue)
            {
                strSql.AppendFormat(" and CollegeId in (0,{0})", filter.CollegeId2.Value);
            }
            return strSql.ToString();
        }

        #endregion

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Notice> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [Id],[UserId],[NoticeType],[Content],[CreateTime],[ModifyTime],[CollegeId] ");
            strSql.Append(" FROM Notice ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));
            List<Notice> list = new List<Notice>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<Notice>(strSql.ToString()).ToList();
            }
            return list;
        }
        #endregion

        #region  获取大赛说明
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public CompetitionDescription GetDescModel(int collegeId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT *  FROM CompetitionDescription");
            strSql.Append(" where CollegeId=@CollegeId ");

            CompetitionDescription model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@CollegeId", collegeId, dbType: DbType.Int32);
                model = conn.Query<CompetitionDescription>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        /// <summary>
        /// 根据大学的ID获得数据列表
        /// </summary>
        public List<Notice> GetNoticeListByCollegeId(int collegeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 8 [Id],[UserId],[NoticeType],[Content],[CreateTime],[ModifyTime],[CollegeId] ");
            strSql.Append(" FROM Notice ");
            strSql.Append(" where 1=1 ");
            strSql.Append(" and (CollegeId=0 or CollegeId=@CollegeId) order by CreateTime desc");
            List<Notice> list = new List<Notice>();
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();              
                param.Add("@CollegeId", collegeId, dbType: DbType.Int32);
                list = conn.Query<Notice>(strSql.ToString(),param).ToList();
            }
            return list;
        }
    }
}
