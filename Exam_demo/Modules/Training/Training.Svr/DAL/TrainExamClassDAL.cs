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
    /// 数据访问类:TrainExamClass
    /// </summary>
    public partial class TrainExamClassDAL
    {
        public TrainExamClassDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from TrainExamClass where Id=@Id ");

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
        public int Add(TrainExamClass model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TrainExamClass(");
            strSql.Append("TrainExamId,ClassId)");

            strSql.Append(" values (");
            strSql.Append("@TrainExamId,@ClassId)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@TrainExamId", model.TrainExamId, dbType: DbType.Int32);
                param.Add("@ClassId", model.ClassId, dbType: DbType.Int32);
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
        public bool Update(TrainExamClass model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TrainExamClass set ");
            strSql.Append("TrainExamId=@TrainExamId,");
            strSql.Append("ClassId=@ClassId");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@TrainExamId", model.TrainExamId, dbType: DbType.Int32);
                param.Add("@ClassId", model.ClassId, dbType: DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        #endregion

        #region  删除
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int TrainExamId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TrainExamClass ");
            strSql.Append(" where TrainExamId=@TrainExamId ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@TrainExamId", TrainExamId, dbType: DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        #endregion
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TrainExamClass GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,TrainExamId,ClassId from TrainExamClass ");
            strSql.Append(" where Id=@Id ");

            TrainExamClass model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<TrainExamClass>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #region  获取实体
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TrainExamClass GetModel2(int TrainExamId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,TrainExamId,ClassId from TrainExamClass ");
            strSql.Append(" where TrainExamId=@TrainExamId ");

            TrainExamClass model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@TrainExamId", TrainExamId, dbType: DbType.Int32);
                model = conn.Query<TrainExamClass>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }
        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<TrainExamClass> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,TrainExamId,ClassId ");
            strSql.Append(" FROM TrainExamClass ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<TrainExamClass> list = new List<TrainExamClass>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<TrainExamClass>(strSql.ToString()).ToList();
            }
            return list;
        }

        /// <summary>
        /// 获取ClassIds集合
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<int> GetClassIds(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ClassId ");
            strSql.Append(" FROM TrainExamClass ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<int> list = new List<int>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<int>(strSql.ToString()).ToList();
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
            if (filter.TrainExamId.HasValue)
            {
                strSql.Append(" and TrainExamId=" + filter.TrainExamId);
            }
            if (filter.ClassId.HasValue)
            {
                strSql.Append(" and ClassId=" + filter.ClassId);
            }
            return strSql.ToString();
        }

        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetTrainExamClassPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "TrainExamClass";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,TrainExamId,ClassId";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion




    }
}

