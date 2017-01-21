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
    /// 数据访问类:TrainExamDetail
    /// </summary>
    public partial class TrainExamDetailDAL
    {
        public TrainExamDetailDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from TrainExamDetail where Id=@Id ");

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
        public int Add(TrainExamDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TrainExamDetail(");
            strSql.Append("TrainExamId,ModularId,ExamPointId,Score,Answer,PointType)");

            strSql.Append(" values (");
            strSql.Append("@TrainExamId,@ModularId,@ExamPointId,@Score,@Answer,@PointType)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@TrainExamId", model.TrainExamId, dbType: DbType.Int32);
                param.Add("@ModularId", model.ModularId, dbType: DbType.Int32);
                param.Add("@ExamPointId", model.ExamPointId, dbType: DbType.Int32);
                param.Add("@Score", model.Score, dbType: DbType.Decimal);
                param.Add("@Answer", model.Answer, dbType: DbType.String);
                param.Add("@PointType", model.ExamPointType, dbType: DbType.Int32);
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
        public bool Update(TrainExamDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TrainExamDetail set ");
            strSql.Append("TrainExamId=@TrainExamId,");
            strSql.Append("ModularId=@ModularId,");
            strSql.Append("ExamPointId=@ExamPointId,");
            strSql.Append("Score=@Score,");
            strSql.Append("Answer=@Answer,");
            strSql.Append("PointType=@PointType");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@TrainExamId", model.TrainExamId, dbType: DbType.Int32);
                param.Add("@ModularId", model.ModularId, dbType: DbType.Int32);
                param.Add("@ExamPointId", model.ExamPointId, dbType: DbType.Int32);
                param.Add("@Score", model.Score, dbType: DbType.Decimal);
                param.Add("@Answer", model.Answer, dbType: DbType.String);
                param.Add("@PointType", model.ExamPointType, dbType: DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        public bool UpdateAnswer(List<ExamPointAnswer> model,int TrainExamId)
        {
            bool result = false;
            var conn = DBHelper.CreateConnection();
            conn.Open();
            var tran = conn.BeginTransaction();

            try
            {
                StringBuilder strSql = new StringBuilder();
                var param = new DynamicParameters();
                foreach (var item in model)
                {
                    strSql.Clear();
                    param = new DynamicParameters();

                    strSql.Append("update TrainExamDetail set ");
                    strSql.Append("Answer=@Answer");
                    strSql.Append(" where ExamPointId=@ExamPointId and TrainExamId=@TrainExamId ");

                    param.Add("@ExamPointId", item.ExamPointId, dbType: DbType.Int32);
                    param.Add("@Answer", item.Answer, dbType: DbType.String);
                    param.Add("@TrainExamId", TrainExamId, dbType: DbType.Int32);
                    conn.Execute(strSql.ToString(), param, tran);
                }
                tran.Commit();
                result = true;
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError(" EditTrainExam", ex);
                tran.Rollback();
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
            return result;
        }

        #endregion

        #region  删除
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int TrainExamId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TrainExamDetail ");
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

        #region  获取实体
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TrainExamDetail GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,TrainExamId,ModularId,ExamPointId,Score,Answer,PointType from TrainExamDetail ");
            strSql.Append(" where Id=@Id ");

            TrainExamDetail model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<TrainExamDetail>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TrainExamDetail GetModel2(int TrainExamId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,TrainExamId,ModularId,ExamPointId,Score,Answer,PointType from TrainExamDetail ");
            strSql.Append(" where TrainExamId=@TrainExamId ");

            TrainExamDetail model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@TrainExamId", TrainExamId, dbType: DbType.Int32);
                model = conn.Query<TrainExamDetail>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }
        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<TrainExamDetail> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,TrainExamId,ModularId,ExamPointId,Score,Answer,PointType ");
            strSql.Append(" FROM TrainExamDetail ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<TrainExamDetail> list = new List<TrainExamDetail>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<TrainExamDetail>(strSql.ToString()).ToList();
            }
            return list;
        }

        /// <summary>
        /// ExamPointIds考核点集合获取
        /// </summary>
        public List<int> GetExamPointIds(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ExamPointId ");
            strSql.Append(" FROM TrainExamDetail ");
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
            if (filter.ExamPointId.HasValue)
            {
                strSql.Append(" and ExamPointId=" + filter.ExamPointId);
            }
            return strSql.ToString();
        }

        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetTrainExamDetailPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "TrainExamDetail";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,TrainExamId,ModularId,ExamPointId,Score,Answer,PointType";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion




    }
}

