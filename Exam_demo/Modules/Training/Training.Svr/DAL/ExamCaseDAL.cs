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
    /// 数据访问类:ExamCase
    /// </summary>
    public partial class ExamCaseDAL
    {
        public ExamCaseDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ExamCase where Id=@Id ");

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
        public int Add(ExamCase model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ExamCase(");
            strSql.Append("TrainExamId,CustomerName,IDType,IDNum,FinancialTypeId,CustomerStory,UserId,CreateTime)");

            strSql.Append(" values (");
            strSql.Append("@TrainExamId,@CustomerName,@IDType,@IDNum,@FinancialTypeId,@CustomerStory,@UserId,@CreateTime)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@TrainExamId", model.TrainExamId, dbType: DbType.Int32);
                param.Add("@CustomerName", model.CustomerName, dbType: DbType.String);
                param.Add("@IDType", model.IDType, dbType: DbType.Int32);
                param.Add("@IDNum", model.IDNum, dbType: DbType.String);
                param.Add("@FinancialTypeId", model.FinancialTypeId, dbType: DbType.Int32);
                param.Add("@CustomerStory", model.CustomerStory, dbType: DbType.String);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@CreateTime", model.CreateTime, dbType: DbType.DateTime);
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
        public bool Update(ExamCase model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ExamCase set ");
            strSql.Append("TrainExamId=@TrainExamId,");
            strSql.Append("CustomerName=@CustomerName,");
            strSql.Append("IDType=@IDType,");
            strSql.Append("IDNum=@IDNum,");
            strSql.Append("FinancialTypeId=@FinancialTypeId,");
            strSql.Append("CustomerStory=@CustomerStory,");
            strSql.Append("UserId=@UserId,");
            strSql.Append("CreateTime=@CreateTime");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@TrainExamId", model.TrainExamId, dbType: DbType.Int32);
                param.Add("@CustomerName", model.CustomerName, dbType: DbType.String);
                param.Add("@IDType", model.IDType, dbType: DbType.Int32);
                param.Add("@IDNum", model.IDNum, dbType: DbType.String);
                param.Add("@FinancialTypeId", model.FinancialTypeId, dbType: DbType.Int32);
                param.Add("@CustomerStory", model.CustomerStory, dbType: DbType.String);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@CreateTime", model.CreateTime, dbType: DbType.DateTime);
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
            strSql.Append("delete from ExamCase ");
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
        public bool Delete2(int TrainExamId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ExamCase ");
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
        public ExamCase GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,TrainExamId,CustomerName,IDType,IDNum,FinancialTypeId,CustomerStory,UserId,CreateTime from ExamCase ");
            strSql.Append(" where Id=@Id ");

            ExamCase model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<ExamCase>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        /// <summary>
        /// 获取考核案例实体
        /// </summary>
        /// <param name="TrainExamId">销售机会/实训考核Id</param>
        /// <returns></returns>
        public ExamCase GetModelByTrainExamId(int TrainExamId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,TrainExamId,CustomerName,IDType,IDNum,FinancialTypeId,CustomerStory,UserId,CreateTime from ExamCase ");
            strSql.Append(" where TrainExamId=@TrainExamId ");

            ExamCase model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@TrainExamId", TrainExamId, dbType: DbType.Int32);
                model = conn.Query<ExamCase>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ExamCase> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,TrainExamId,CustomerName,IDType,IDNum,FinancialTypeId,CustomerStory,UserId,CreateTime ");
            strSql.Append(" FROM ExamCase ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<ExamCase> list = new List<ExamCase>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<ExamCase>(strSql.ToString()).ToList();
            }
            return list;
        }

        public List<ExamCaseTrainExam> GetSalesJudgeList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select TE.Id,CustomerName,IDNum,EC.FinancialTypeId,TE.AllScore ");
            strSql.Append(" From  TrainExam TE ");
            strSql.Append(" inner join ExamCase EC on  EC.TrainExamId=TE.Id ");
            strSql.Append(" where 1=1 and EndDate <=SYSDATETIME() ");
            strSql.Append(GetStrWhere(filter));
            strSql.Append(" Order By TE.Id Desc ");
            List<ExamCaseTrainExam> list = new List<ExamCaseTrainExam>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<ExamCaseTrainExam>(strSql.ToString()).ToList();
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
            if (filter.TrainExamStatus.HasValue)
            {
                strSql.Append(" and  TrainExamStatus=" + filter.TrainExamStatus);
            }
            if (filter.ExamTypeId.HasValue)
            {
                strSql.Append(" and ExamTypeId= " + filter.ExamTypeId);
            }
            if (filter.Status.HasValue)
            { strSql.Append(" and TE.Status=" + filter.Status); }
            if (filter.UserId.HasValue)
            {
                strSql.Append(" and TE.UserId= " + filter.UserId);
            }

            return strSql.ToString();
        }

        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetExamCasePageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "ExamCase";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,TrainExamId,CustomerName,IDType,IDNum,FinancialTypeId,CustomerStory,UserId,CreateTime";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        /// <summary>
        /// 获取分页参数--多表联查
        /// </summary>
        public PageModel GetSalesJudgePageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = " TrainExam TE inner join  ExamCase EC on  EC.TrainExamId=TE.Id ";
            model.PKey = " TE.Id ";
            //测试提出一开始时间倒序
            model.Sort = "TE.StartDate Desc";
          //  model.Sort = " TE.Id Desc";
            model.Fields = " TE.Id,CustomerName,IDNum,EC.FinancialTypeId,TE.AllScore,TE.StartDate ";
            model.Filter = " and EndDate<=SYSDATETIME()  and TE.Status in (1,2)" + GetStrWhere(filter);
            return model;
        }

        #endregion


    }
}

