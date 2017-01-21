using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using Exam.API;

namespace Exam.Svr
{
    /// <summary>
    /// 数据访问类:PaperUserSummary
    /// </summary>
    public partial class PaperUserSummaryDAL
    {

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from PaperUserSummary where Id=@Id ");

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
        public int Add(PaperUserSummary model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PaperUserSummary(");
            strSql.Append("ExamPaperId,UserId,CompetitionId,TotalScore,Score,Status,FinishDate)");

            strSql.Append(" values (");
            strSql.Append("@ExamPaperId,@UserId,@CompetitionId,@TotalScore,@Score,@Status,@FinishDate)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ExamPaperId", model.ExamPaperId, dbType: DbType.Int32);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@CompetitionId", model.CompetitionId, dbType: DbType.Int32);
                //param.Add("@UnScoredCount", model.UnScoredCount, dbType: DbType.Int32);
                param.Add("@TotalScore", model.TotalScore, dbType: DbType.Decimal);
                param.Add("@Score", model.Score, dbType: DbType.Decimal);
                param.Add("@Status", model.Status, dbType: DbType.Int32);
                param.Add("@FinishDate", model.FinishDate, dbType: DbType.DateTime);
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
        public bool Update(PaperUserSummary model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PaperUserSummary set ");
            strSql.Append("ExamPaperId=@ExamPaperId,");
            strSql.Append("UserId=@UserId,");
            strSql.Append("CompetitionId=@CompetitionId,");
            //strSql.Append("UnScoredCount=@UnScoredCount,");
            strSql.Append("TotalScore=@TotalScore,");
            strSql.Append("Score=@Score,");
            strSql.Append("Status=@Status,");
            strSql.Append("FinishDate=@FinishDate");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ExamPaperId", model.ExamPaperId, dbType: DbType.Int32);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@CompetitionId", model.CompetitionId, dbType: DbType.Int32);
                //param.Add("@UnScoredCount", model.UnScoredCount, dbType: DbType.Int32);
                param.Add("@TotalScore", model.TotalScore, dbType: DbType.Decimal);
                param.Add("@Score", model.Score, dbType: DbType.Decimal);
                param.Add("@Status", model.Status, dbType: DbType.Int32);
                param.Add("@FinishDate", model.FinishDate, dbType: DbType.DateTime);

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
            strSql.Append("delete from PaperUserSummary ");
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
        public PaperUserSummary GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from PaperUserSummary ");
            strSql.Append(" where Id=@Id ");

            PaperUserSummary model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<PaperUserSummary>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<PaperUserSummary> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM PaperUserSummary ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<PaperUserSummary> list = new List<PaperUserSummary>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<PaperUserSummary>(strSql.ToString()).ToList();
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
                strSql.Append(" and PaperUserSummary.Id=" + filter.Id);
            }
            if (filter.Id2.HasValue)
            {
                strSql.Append(" and PaperUserSummary.ExamPaperId=" + filter.Id2);
            }
            if (filter.UserId.HasValue && filter.isShow==true)
            {
                strSql.Append(" and Paper.UserId=" + filter.UserId);
            }

            if (filter.CompetitionId.HasValue)
            {
                strSql.Append(" and PaperUserSummary.CompetitionId=" + filter.CompetitionId);
            }
            if (filter.UserId2.HasValue )
            {
                strSql.Append(" and PaperUserSummary.UserId=" + filter.UserId2);
            }

            if (filter.Status.HasValue && filter.Status==23)
            {
                strSql.Append(" and PaperUserSummary.Status in (2,3)");
            }
            if (filter.Status.HasValue && filter.Status == 234)
            {
                strSql.Append(" and Paper.Id in (select PaperUserSummary.ExamPaperId from PaperUserSummary where  PaperUserSummary.Status in (2,3)) and  Paper.Status in (2,3,4)");
            }
            if (filter.LiburaryId.HasValue)
            {
                strSql.Append(" and Paper.LibraryID=" + filter.LiburaryId);
            }
            if (filter.Status.HasValue && filter.Status != 23 && filter.Status != 234)
            {
                strSql.Append(" and Paper.Status=" + filter.Status);
            }
            return strSql.ToString();
        }

        /// <summary>
        /// 根据CustomFilter获取where语句
        /// </summary>
        private string GetStrWhere2(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
           
            if (filter.CharpterID.HasValue)
            {
                strSql.Append(" and CharpterID=" + filter.CharpterID);
            }
            if (filter.UserId.HasValue)
            {
                strSql.Append(" and Paper.UserId=" + filter.UserId);
            }
            if (filter.IsLessThanCurrentDate==true)
            {
                strSql.Append(" and Paper.Status=3");
            }
            else
            {
                strSql.Append(" and Paper.EndDate>GETDATE() and Paper.Status=2");
            }
            return strSql.ToString();
        }
        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetPaperUserSummaryPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = filter.isShow ? "PaperUserSummary inner join Paper on PaperUserSummary.ExamPaperId=Paper.Id" : "PaperUserSummary";
            model.PKey = "PaperUserSummary.Id";
            if (string.IsNullOrEmpty(filter.SortName))
            {
                model.Sort = " PaperUserSummary.FinishDate desc";
            }
            else
            {
                model.Sort = filter.SortName + (filter.SortWay ? " desc" : " asc");
            }
            model.Fields = "PaperUserSummary.Id,PaperUserSummary.ExamPaperId,PaperUserSummary.UserId,PaperUserSummary.CompetitionId,PaperUserSummary.TotalScore,PaperUserSummary.Score,PaperUserSummary.Status,PaperUserSummary.FinishDate";
            model.Fields += filter.isShow ? ",Paper.UserId as UId,Paper.ExamPaperName as ExamPaperName,Paper.EndDate as EndDate,Paper.Status as Status2" : "";
            model.Filter = GetStrWhere(filter);

            return model;
        }


        /// <summary>
        /// 获取分页参数
        /// </summary>
        public List<PaperCharpter> GetPaperCharpterList(CustomFilter filter)
        {
            
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PaperID,CharpterID,Paper.EndDate ");
            strSql.Append(" FROM PaperCharpter ");
            strSql.Append(" inner join Paper on Paper.Id= PaperCharpter.PaperID ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere2(filter));

            List<PaperCharpter> list = new List<PaperCharpter>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<PaperCharpter>(strSql.ToString()).ToList();
            }
            return list;
        }

        #endregion

        /// <summary>
        /// 获取用户试卷概况
        /// </summary>
        public PaperUserSummary GetModel(int userId, int competitionId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from PaperUserSummary ");
            strSql.Append(" where UserId=@UserId and CompetitionId=@CompetitionId ");

            PaperUserSummary model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@UserId", userId, dbType: DbType.Int32);
                param.Add("@CompetitionId", competitionId, dbType: DbType.Int32);
                model = conn.Query<PaperUserSummary>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }
       
    }
}

