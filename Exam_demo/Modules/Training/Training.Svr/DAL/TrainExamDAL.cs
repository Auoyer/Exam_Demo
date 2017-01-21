using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using Utils;
using Training.API;
using System.Data.SqlClient;

namespace Training.Svr
{
    /// <summary>
    /// 数据访问类:TrainExam
    /// </summary>
    public partial class TrainExamDAL
    {
        public TrainExamDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from TrainExam where Id=@Id ");

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
        public int Add(TrainExam model)
        {
            int result = 0;
            var conn = DBHelper.CreateConnection();
            conn.Open();
            var tran = conn.BeginTransaction();

            try
            {
                #region 新增销售机会
                StringBuilder strSql = new StringBuilder();
                var param = new DynamicParameters();

                strSql.Append("insert into TrainExam(");
                strSql.Append("CompetitionId,ExamCaseId,UserId,TrainExamName,Status,CaseId,StartDate,EndDate,AllScore,TrainExamStatus)");
                strSql.Append(" values (");
                strSql.Append("@CompetitionId,@ExamCaseId,@UserId,@TrainExamName,@Status,@CaseId,@StartDate,@EndDate,@AllScore,@TrainExamStatus)");
                strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                param.Add("@CompetitionId", model.CompetitionId, dbType: DbType.Int32);
                param.Add("@ExamCaseId", model.ExamCaseId, dbType: DbType.Int32);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@TrainExamName", model.TrainExamName, dbType: DbType.String);
                param.Add("@Status", model.Status, dbType: DbType.Int32);
                param.Add("@StartDate", model.StartDate, dbType: DbType.DateTime);
                param.Add("@EndDate", model.EndDate, dbType: DbType.DateTime);
                param.Add("@CaseId", model.CaseId, dbType: DbType.Int32);
                param.Add("@AllScore", model.AllScore, dbType: DbType.Decimal);
                param.Add("@TrainExamStatus", model.TrainExamStatus, dbType: DbType.Int32);
                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);

                conn.Execute(strSql.ToString(), param, tran);
                result = param.Get<int>("@returnid");
                #endregion

                #region 考核点和销售机会关联表集合数据新增
                if (model.TrainExamDetail != null && model.TrainExamDetail.Count > 0)
                {
                    foreach (var item in model.TrainExamDetail)
                    {
                        strSql.Clear();
                        param = new DynamicParameters();

                        strSql.Append("insert into TrainExamDetail(");
                        strSql.Append("TrainExamId,ModularId,ExamPointId,Score,ExamPointType)");

                        strSql.Append(" values (");
                        strSql.Append("@TrainExamId,@ModularId,@ExamPointId,@Score,@ExamPointType)");
                        strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                        param.Add("@TrainExamId", result, dbType: DbType.Int32);
                        param.Add("@ModularId", item.ModularId, dbType: DbType.Int32);
                        param.Add("@ExamPointId", item.ExamPointId, dbType: DbType.Int32);

                        param.Add("@Score", item.Score, dbType: DbType.Int32);
                        param.Add("@ExamPointType", item.ExamPointType, dbType: DbType.Int32);
                        param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                        conn.Execute(strSql.ToString(), param, tran);
                    }
                }
                #endregion

                #region 案例
                //if (model.ExamCase != null && model.ExamCase.Count > 0)
                //{
                //    foreach (var item in model.ExamCase)
                //    {
                //        strSql.Clear();
                //        param = new DynamicParameters();

                //        strSql.Append("insert into ExamCase(");
                //        strSql.Append("TrainExamId,CustomerName,IDType,IDNum,FinancialTypeId,CustomerStory,UserId,CreateTime)");

                //        strSql.Append(" values (");
                //        strSql.Append("@TrainExamId,@CustomerName,@IDType,@IDNum,@FinancialTypeId,@CustomerStory,@UserId,@CreateTime)");
                //        strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                //        param.Add("@TrainExamId", result, dbType: DbType.Int32);
                //        param.Add("@CustomerName", item.CustomerName, dbType: DbType.String);
                //        param.Add("@IDType", item.IDType, dbType: DbType.Int32);
                //        param.Add("@IDNum", item.IDNum, dbType: DbType.String);
                //        param.Add("@FinancialTypeId", item.FinancialTypeId, dbType: DbType.Int32);
                //        param.Add("@CustomerStory", item.CustomerStory, dbType: DbType.String);
                //        param.Add("@UserId", item.UserId, dbType: DbType.Int32);
                //        param.Add("@CreateTime", item.CreateTime, dbType: DbType.DateTime);
                //        param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);

                //        conn.Execute(strSql.ToString(), param, tran);
                //    }
                //}
                #endregion

                tran.Commit();
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("TrainExam Add", ex);
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

        #region  更新
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(TrainExam model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TrainExam set ");
            strSql.Append("ExamCaseId=@ExamCaseId,");
            strSql.Append("UserId=@UserId,");
            strSql.Append("TrainExamName=@TrainExamName,");
            strSql.Append("Status=@Status,");
            strSql.Append("ExamTypeId=@ExamTypeId,");
            strSql.Append("StartDate=@StartDate,");
            strSql.Append("EndDate=@EndDate,");
            strSql.Append("CaseId=@CaseId,");
            strSql.Append("AllScore=@AllScore,");
            strSql.Append("TrainExamStatus=@TrainExamStatus");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ExamCaseId", model.ExamCaseId, dbType: DbType.Int32);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@TrainExamName", model.TrainExamName, dbType: DbType.String);
                param.Add("@Status", model.Status, dbType: DbType.Int32);
                param.Add("@ExamTypeId", model.ExamTypeId, dbType: DbType.Int32);
                param.Add("@StartDate", model.StartDate, dbType: DbType.DateTime);
                param.Add("@EndDate", model.EndDate, dbType: DbType.DateTime);
                param.Add("@CaseId", model.CaseId, dbType: DbType.Int32);
                param.Add("@AllScore", model.AllScore, dbType: DbType.Decimal);
                param.Add("@TrainExamStatus", model.TrainExamStatus, dbType: DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }



        /// <summary>
        /// 编辑销售机会/实训考核
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool EditTrainExam(TrainExam model)
        {

            bool result = false;
            var conn = DBHelper.CreateConnection();
            conn.Open();
            var tran = conn.BeginTransaction();

            try
            {

                #region 销售机会修改
                StringBuilder strSql = new StringBuilder();
                var param = new DynamicParameters();

                strSql.Append("update TrainExam set ");
                strSql.Append("ExamCaseId=@ExamCaseId,");
                strSql.Append("UserId=@UserId,");
                //strSql.Append("TrainExamName=@TrainExamName,");
                strSql.Append("Status=@Status,");
                //strSql.Append("ExamTypeId=@ExamTypeId,");
                //strSql.Append("StartDate=@StartDate,");
                //strSql.Append("EndDate=@EndDate,");
                strSql.Append("CaseId=@CaseId,");
                strSql.Append("AllScore=@AllScore,");
                strSql.Append("TrainExamStatus=@TrainExamStatus");
                strSql.Append(" where Id=@Id ");

                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ExamCaseId", model.ExamCaseId, dbType: DbType.Int32);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                //param.Add("@TrainExamName", model.TrainExamName, dbType: DbType.String);
                param.Add("@Status", model.Status, dbType: DbType.Int32);
                //param.Add("@ExamTypeId", model.ExamTypeId, dbType: DbType.Int32);
                //param.Add("@StartDate", model.StartDate, dbType: DbType.DateTime);
                //param.Add("@EndDate", model.EndDate, dbType: DbType.DateTime);
                param.Add("@CaseId", model.CaseId, dbType: DbType.Int32);
                param.Add("@AllScore", model.AllScore, dbType: DbType.Decimal);
                param.Add("@TrainExamStatus", model.TrainExamStatus, dbType: DbType.Int32);
                conn.Execute(strSql.ToString(), param, tran);

                #endregion

                #region 案例删除
                if (model.ExamCase != null && model.ExamCase.Count > 0)
                {

                    strSql.Clear();
                    param = new DynamicParameters();
                    strSql.Append("delete from ExamCase ");
                    strSql.Append(" where TrainExamId=@TrainExamId ");

                    param.Add("@TrainExamId", model.Id, dbType: DbType.Int32);
                    conn.Execute(strSql.ToString(), param, tran);

                }
                #endregion

                #region TrainExamDetail删除
                if (model.TrainExamDetail != null && model.TrainExamDetail.Count > 0)
                {

                    strSql.Clear();
                    param = new DynamicParameters();
                    strSql.Append("delete from TrainExamDetail ");
                    strSql.Append("where TrainExamId=@TrainExamId ");

                    param.Add("@TrainExamId", model.Id, dbType: DbType.Int32);
                    conn.Execute(strSql.ToString(), param, tran);

                }
                #endregion

                #region 新增案例
                if (model.ExamCase != null && model.ExamCase.Count > 0)
                {
                    foreach (var item in model.ExamCase)
                    {
                        strSql.Clear();
                        param = new DynamicParameters();

                        strSql.Append("insert into ExamCase(");
                        strSql.Append("TrainExamId,CustomerName,IDType,IDNum,FinancialTypeId,CustomerStory,UserId,CreateTime)");

                        strSql.Append(" values (");
                        strSql.Append("@TrainExamId,@CustomerName,@IDType,@IDNum,@FinancialTypeId,@CustomerStory,@UserId,@CreateTime)");
                        strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                        param.Add("@TrainExamId", model.Id, dbType: DbType.Int32);
                        param.Add("@CustomerName", item.CustomerName, dbType: DbType.String);
                        param.Add("@IDType", item.IDType, dbType: DbType.Int32);
                        param.Add("@IDNum", item.IDNum, dbType: DbType.String);
                        param.Add("@FinancialTypeId", item.FinancialTypeId, dbType: DbType.Int32);
                        param.Add("@CustomerStory", item.CustomerStory, dbType: DbType.String);
                        param.Add("@UserId", item.UserId, dbType: DbType.Int32);
                        param.Add("@CreateTime", item.CreateTime, dbType: DbType.DateTime);
                        param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                        conn.Execute(strSql.ToString(), param, tran);
                    }
                }
                #endregion

                #region 考核点和销售机会关联表集合新增
                if (model.TrainExamDetail != null && model.TrainExamDetail.Count > 0)
                {
                    foreach (var item in model.TrainExamDetail)
                    {
                        strSql.Clear();
                        param = new DynamicParameters();

                        strSql.Append("insert into TrainExamDetail(");
                        strSql.Append("TrainExamId,ModularId,ExamPointId,Score,ExamPointType)");

                        strSql.Append(" values (");
                        strSql.Append("@TrainExamId,@ModularId,@ExamPointId,@Score,@ExamPointType)");
                        strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                        param.Add("@TrainExamId", model.Id, dbType: DbType.Int32);
                        param.Add("@ModularId", item.ModularId, dbType: DbType.Int32);
                        param.Add("@ExamPointId", item.ExamPointId, dbType: DbType.Int32);
                        param.Add("@Score", item.Score, dbType: DbType.Int32);
                        param.Add("@ExamPointType", item.ExamPointType, dbType: DbType.Int32);
                        param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                        conn.Execute(strSql.ToString(), param, tran);
                    }
                }
                #endregion

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
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TrainExam ");
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
        public TrainExam GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from TrainExam ");
            strSql.Append(" where Id=@Id ");

            //strSql.Append("select Id,TrainExamId,ClassId from TrainExamClass ");
            //strSql.Append(" where TrainExamId=@Id ");

            strSql.Append("select * from TrainExamDetail ");
            strSql.Append(" where TrainExamId=@Id ");

            TrainExam model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                using (var multi = conn.QueryMultiple(strSql.ToString(), param))
                {
                    model = multi.Read<TrainExam>().FirstOrDefault();
                    if (model != null)
                    {
                        //model.TrainExamClass = multi.Read<TrainExamClass>().ToList();
                        model.TrainExamDetail = multi.Read<TrainExamDetail>().ToList();

                        strSql.Clear();
                        strSql.Append("select * from [Case] where Id=" + model.CaseId);
                        var curCase = conn.Query<Case>(strSql.ToString()).FirstOrDefault();
                        model.Case = curCase;
                    }
                }
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<TrainExam> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM TrainExam a ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<TrainExam> list = new List<TrainExam>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<TrainExam>(strSql.ToString()).ToList();
            }
            return list;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<UnitTrainExam> GetList2(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.Id,a.CaseId,a.ExamCaseId,b.CustomerName,b.IDNum,b.FinancialTypeId,a.TrainExamName,a.Status,a.StartDate,a.EndDate,a.UserId,a.AllScore,a.ExamTypeId,a.TrainExamStatus,d.ClassId  ");
            strSql.Append(" FROM ");
            strSql.Append(" dbo.TrainExam a join dbo.[ExamCase] b on a.Id=b.TrainExamId join TrainExamClass d on a.Id=d.TrainExamId");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhereThree(filter));
            strSql.Append(" group by a.Id,a.CaseId,a.ExamCaseId,b.CustomerName,b.IDNum,b.FinancialTypeId,a.TrainExamName,a.Status,a.StartDate,a.EndDate,a.UserId,a.AllScore,a.ExamTypeId,a.TrainExamStatus,d.ClassId");
            List<UnitTrainExam> list = new List<UnitTrainExam>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<UnitTrainExam>(strSql.ToString()).ToList();
            }
            return list;
        }

        /// <summary>
        /// 根据CustomFilter获取where语句
        /// </summary>
        private string GetStrWhereThree(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (filter.Id.HasValue)
            {
                strSql.Append(" and Id=" + filter.Id);
            }
            if (filter.UserId.HasValue)
            {
                strSql.Append(" and a.UserId=" + filter.UserId);
            }
            if (!string.IsNullOrEmpty(filter.IDNum))
            {
                strSql.AppendFormat(" and IDNum='{0}'", filter.IDNum);
            }
            if (filter.ExamTypeId.HasValue)
            {
                strSql.Append(" and ExamTypeId=" + filter.ExamTypeId);
            }
            if (filter.Status.HasValue)
            {
                strSql.Append(" and a.Status=" + filter.Status);
            }
            if (!string.IsNullOrEmpty(filter.CheckName))
            {
                strSql.Append(" and a.TrainExamName='" + filter.CheckName + "'");
            }
            if (filter.SummaryType == 12)
            {
                strSql.Append(" and (DATEDIFF(ss,a.StartDate,getdate()) >=0 or( DATEDIFF(ss,a.EndDate,getdate()) <=0))");
            }
            if (filter.ClassId.HasValue)
            {
                strSql.AppendFormat(" and a.Id in (select TrainExamId from TrainExamClass where ClassId={0})", filter.ClassId);
            }
            return strSql.ToString();
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
            if (filter.CompetitionId.HasValue)
            {
                strSql.Append(" and CompetitionId=" + filter.CompetitionId);
            }
            if (filter.UserId.HasValue)
            {
                strSql.Append(" and UserId=" + filter.UserId);
            }
            if (!string.IsNullOrEmpty(filter.IDNum))
            {
                strSql.Append(" and IDNum=" + filter.IDNum);
            }
            if (filter.ExamTypeId.HasValue)
            {
                strSql.Append(" and ExamTypeId=" + filter.ExamTypeId);
            }
            if (filter.Status.HasValue)
            {
                strSql.Append(" and a.Status=" + filter.Status);
            }
            if (!string.IsNullOrEmpty(filter.CheckName))
            {
                strSql.Append(" and a.TrainExamName='" + filter.CheckName + "'");
            }
            if (filter.SummaryType == 12)
            {
                strSql.Append(" and (DATEDIFF(ss,a.StartDate,getdate()) >=0 or( DATEDIFF(ss,a.EndDate,getdate()) <=0))");
            }
            if (filter.ClassId.HasValue)
            {
                strSql.AppendFormat(" and a.Id in (select TrainExamId from TrainExamClass where ClassId={0})", filter.ClassId);
            }
            return strSql.ToString();
        }

        /// <summary>
        /// (学生端/XXX/YYY)销售机会列表拼接语句
        /// </summary>
        private string GetStrWhereCopy(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (filter.FinancialTypeId.HasValue && filter.FinancialTypeId != 0)
            {
                strSql.Append(" and b.FinancialTypeId=" + filter.FinancialTypeId);
            }
            if (filter.Status.HasValue)
            {
                if (filter.Score.HasValue && filter.Score == 1)
                {
                    //2015年9月21日17:54:21，将=改为int ，由于教师端和学生端取列表都用到这个，学生端传人Score判断下显示假删的销售机会
                    strSql.AppendFormat(" and a.Status in({0},2) ", filter.Status);
                }
                else { strSql.Append(" and a.Status = " + filter.Status); }
             
            }
            if (filter.ExamTypeId.HasValue)
            {
                strSql.Append(" and a.ExamTypeId=" + filter.ExamTypeId);
            }
            if (filter.UserId.HasValue)
            {
                strSql.Append(" and a.UserId=" + filter.UserId);
            }
            if (filter.TrainExamStatus.HasValue && filter.TrainExamStatus != 0)
            {
                strSql.Append(" and a.TrainExamStatus=" + filter.TrainExamStatus);
            }
            if (!string.IsNullOrEmpty(filter.IDNum))
            {
                strSql.Append(" and IDNum=" + filter.IDNum);
            }
            if (filter.ClassId.HasValue)
            {
                strSql.AppendFormat(" and a.Id in (select TrainExamId from TrainExamClass where ClassId={0})", filter.ClassId);
            }
            if (!string.IsNullOrEmpty(filter.KeyWords))
            {
                //过滤危险字段，如单引号等
                string key = filter.KeyWords.Replace("'", "''");
                //客户姓名，身份证号
                strSql.AppendFormat(" and (b.CustomerName like '%{0}%' or b.IDNum like '%{0}%')", key);
            }
            if (filter.isShow)
            {
                strSql.AppendFormat(" and (DATEDIFF(ss,a.StartDate,getdate()) >=0 and DATEDIFF(ss,a.EndDate,getdate()) <=0 and c.UserId is null)");
            }
            if (filter.IsLessThanCurrentDate)
            {
                strSql.Append(" and EndDate<=SYSDATETIME()");
            }
            return strSql.ToString();
        }

        /// <summary>
        /// 拼接语句
        /// </summary>
        private string GetStrWhereCopy2(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (filter.FinancialTypeId.HasValue && filter.FinancialTypeId != 0)
            {
                strSql.Append(" and b.FinancialTypeId=" + filter.FinancialTypeId);
            }
            if (filter.Status.HasValue)
            {
                //特殊处理，学生端实训考核列表需要将删除的也显示出来
                if (filter.Status == 5)
                { strSql.Append(" and a.Status in(1,2)"); }
                else
                {
                    strSql.Append(" and a.Status=" + filter.Status);
                }
            }

            if (filter.UserId.HasValue)
            {
                strSql.Append(" and c.UserId=" + filter.UserId);
            }
            if (filter.ExamTypeId.HasValue)
            {
                strSql.Append(" and a.ExamTypeId=" + filter.ExamTypeId);
            }
            if (!string.IsNullOrEmpty(filter.CheckName))
            {
                strSql.AppendFormat(" and a.TrainExamName like '%{0}%' ", filter.CheckName);
            }
            if (!string.IsNullOrEmpty(filter.CustomerName))
            {
                strSql.AppendFormat(" and b.CustomerName like '%{0}%' ", filter.CustomerName);
            }

            if (filter.CheckStatus.HasValue && filter.CheckStatus != 0)
            {
                if (filter.CheckStatus == 1)//未开始
                {
                    strSql.Append(" and a.StartDate>GETDATE()");
                }
                else if (filter.CheckStatus == 2)//已开始
                {
                    strSql.Append(" and a.StartDate<GETDATE() and a.EndDate>GETDATE()");
                }
                else//已结束
                {
                    strSql.Append(" and a.EndDate<GETDATE()");
                }

            }
            if (filter.ClassId.HasValue)
            {
                strSql.AppendFormat(" and a.Id in (select TrainExamId from TrainExamClass where ClassId={0})", filter.ClassId);
            }
            if (!string.IsNullOrEmpty(filter.KeyWords))
            {
                //过滤危险字段，如单引号等
                string key = filter.KeyWords.Replace("'", "''");
                //客户姓名，身份证号,考核名称
                strSql.AppendFormat(" and (b.CustomerName like '%{0}%' or b.IDNum like '%{0}%' or a.TrainExamName like '%{0}%')", key);
            }
            return strSql.ToString();
        }
        private string GetStrWhereCopy4(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (filter.ScoreStatus.HasValue)
            {
                if (filter.ScoreStatus == 0)
                {
                    strSql.Append(" and c.Status in (2,3) and d.UserId=" + filter.UserId);
                }
                else
                {
                    strSql.Append(" and c.Status=" + filter.ScoreStatus + " and d.UserId=" + filter.UserId);
                }

            }
            return strSql.ToString();
        }
        private string GetStrWhereCopy5(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (filter.ScoreStatus.HasValue)
            {
                strSql.Append(" and ( c.Status is null or c.Status=1 )");
            }
            if (filter.isShow)
            {
                strSql.AppendFormat(" and (DATEDIFF(ss,a.StartDate,getdate()) >=0 and DATEDIFF(ss,a.EndDate,getdate()) <=0 and c.Id is null)");
            }
            return strSql.ToString();
        }
        //专用
        private string GetStrWhereCopy6(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (filter.Id.HasValue)
            {
                strSql.Append(" and ( c.Id is null)");
            }
            if (filter.ExamTypeId.HasValue)
            {
                strSql.Append(" and a.ExamTypeId=" + filter.ExamTypeId);
            }
            if (filter.UserId.HasValue)
            {
                strSql.Append(" and a.UserId=" + filter.UserId);
            }
            if (filter.UserId2.HasValue)
            {
                strSql.Append(" and (c.UserId is null or c.UserId=" + filter.UserId2 + ")");
            }
            if (filter.Status.HasValue && filter.Status != 3 && filter.Status != 23)
            {
                strSql.Append(" and a.Status=" + filter.Status + " and EndDate>GETDATE() and a.StartDate<GETDATE()");
            }
            if (filter.Status.HasValue && filter.Status == 3)
            {
                strSql.Append(" and c.Status=" + filter.Status);
            }
            if (filter.Status.HasValue && filter.Status == 23)
            {
                strSql.Append(" and c.Status in (2,3) and  a.Status!=2");
            }
            if (filter.ClassId.HasValue)
            {
                strSql.AppendFormat(" and a.Id in (select TrainExamId from TrainExamClass where ClassId={0})", filter.ClassId);
            }

            return strSql.ToString();
        }

        //专用
        private string GetStrWhereCopy7(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (filter.Id.HasValue)
            {
                strSql.Append(" and ( c.Id is null)");
            }
            if (filter.ExamTypeId.HasValue)
            {
                strSql.Append(" and a.ExamTypeId=" + filter.ExamTypeId);
            }
            if (filter.UserId.HasValue)
            {
                strSql.Append(" and a.UserId=" + filter.UserId);
            }

            if (filter.Status.HasValue && filter.Status == 3)
            {
                strSql.Append(" and c.Status=" + filter.Status);
            }
            if (filter.UserId2.HasValue)
            {
                strSql.Append(" and (c.UserId is null or c.UserId=" + filter.UserId2 + ")");
            }
            if (filter.Status.HasValue && filter.Status != 3 && filter.Status != 23 && filter.Status != 0)
            {
                strSql.Append(" and a.Status=" + filter.Status + " and EndDate>GETDATE() and a.StartDate<GETDATE() and (a.Id!=c.TrainExamId or c.TrainExamId is null)");
            }
            if (filter.ClassId.HasValue)
            {
                strSql.AppendFormat(" and a.Id in (select TrainExamId from TrainExamClass where ClassId={0})", filter.ClassId);
            }
            if (!string.IsNullOrEmpty(filter.IDNum))
            {
                strSql.Append(" and b.IDNum=" + filter.IDNum);
            }
            return strSql.ToString();
        }
        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数 
        public PageModel GetTrainExamPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "TrainExam";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,ExamCaseId,UserId,TrainExamName,Status,ExamTypeId,StartDate,EndDate,AllScore,TrainExamStatus";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion

        #region 统计数量
        /// <summary>
        /// 根据条件统计销售机会/实训考核
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <returns></returns>
        public int Count(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) ");
            strSql.Append(" from TrainExam");
            //strSql.Append(" join ExamCase on TrainExam.Id=ExamCase.TrainExamId");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrCountWhere(filter));

            int result = 0;
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                result = conn.Query<int>(strSql.ToString()).FirstOrDefault();
            }
            return result;
        }
        /// <summary>
        /// 获取统计查询条件语句
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private string GetStrCountWhere(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (filter.ExamTypeId.HasValue)
            {
                strSql.Append(" and TrainExam.ExamTypeId=" + filter.ExamTypeId.Value);
            }
            if (filter.UserId.HasValue)
            {
                strSql.Append(" and TrainExam.UserId=" + filter.UserId.Value);
            }
            if (!string.IsNullOrEmpty(filter.CheckName))
            {
                //过滤危险字段，如单引号等
                string checkName = filter.CheckName.Replace("'", "''");
                //考核名称
                strSql.Append(" and TrainExam.TrainExamName='" + filter.CheckName + "' ");
            }
            //同一个Id不算在内
            if (filter.Id.HasValue)
            {
                strSql.Append(" and TrainExam.Id <>" + filter.Id.Value);
            }
            //已删除的不查
            strSql.Append(" and TrainExam.Status <> " + (int)TrainExamPublishState.Deleted);

            return strSql.ToString();
        }
        #endregion

        /// <summary>
        /// (学生端)销售机会列表联表获取
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></return>
        public PageModel GetDetailTrainExamPageParams(CustomFilter filter)
        {
            //特殊处理
            int userId = 0;
            if (filter.UserId.HasValue)
            {
                userId = filter.UserId.Value;
                filter.UserId = null;
            }
            StringBuilder strTable = new StringBuilder();
            strTable.Append(" TrainExam a JOIN ExamCase b on a.Id = b.TrainExamId LEFT JOIN TrainExamUser c ");
            strTable.AppendFormat(" on b.TrainExamId = c.TrainExamId and c.UserId = {0} ", userId);

            PageModel model = new PageModel();
            model.Tables = strTable.ToString();
            model.PKey = "a.Id";
            if (string.IsNullOrEmpty(filter.SortName))
            {
                model.Sort = "a.Id";
            }
            else
            {
                model.Sort = filter.SortName + (filter.SortWay ? " asc" : " desc");
            }
            model.Fields = " a.Id,b.CustomerName,b.IDNum,b.FinancialTypeId,a.TrainExamName,a.StartDate,a.EndDate,a.UserId,c.UserId as StuCustomerId";
            model.Filter = GetStrWhereCopy(filter);
            return model;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<UnitTrainExam> GetList3(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.Id,b.CustomerName,b.IDNum,b.FinancialTypeId,a.TrainExamName,a.StartDate,a.EndDate,a.UserId,c.UserId as StuCustomerId,a.ExamTypeId  ");
            strSql.Append(" FROM ");
            strSql.Append(" TrainExam a join [ExamCase] b on a.Id=b.TrainExamId LEFT JOIN TrainExamUser c on a.Id=c.TrainExamId  and c.UserId=" + filter.UserId2);
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhereCopy7(filter));
            List<UnitTrainExam> list = new List<UnitTrainExam>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<UnitTrainExam>(strSql.ToString()).ToList();
            }
            return list;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<UnitTrainExam> GetList5(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.Id,b.CustomerName,b.IDNum,b.FinancialTypeId,a.TrainExamName,a.StartDate,a.EndDate,a.UserId,c.Id as StuCustomerId,c.ProposalId,a.ExamTypeId,a.Status  ");
            strSql.Append(" FROM ");
            strSql.Append(" dbo.TrainExam a join dbo.[ExamCase] b on a.Id=b.TrainExamId LEFT JOIN StuCustomer c on a.Id=c.TrainExamId ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhereCopy7(filter));
            List<UnitTrainExam> list = new List<UnitTrainExam>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<UnitTrainExam>(strSql.ToString()).ToList();
            }
            return list;
        }

        /// <summary>
        /// 学生端销售机会列表联表获取
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></return>
        public PageModel GetTrainExamAndProposal(CustomFilter filter)
        {
            //特殊处理
            int userId = 0;
            if (filter.UserId.HasValue)
            {
                userId = filter.UserId.Value;
                filter.UserId = null;//(其他地方应该不要把)
            }
            StringBuilder strTable = new StringBuilder();
            strTable.Append(" TrainExam a JOIN ExamCase b on a.Id = b.TrainExamId LEFT JOIN Proposal c ");
            strTable.AppendFormat(" on a.Id = c.TrainExamId  and c.UserId = {0}  and c.UpdateDate >= a.StartDate", userId);

            PageModel model = new PageModel();
            model.Tables = strTable.ToString();
            model.PKey = "a.Id";
            if (string.IsNullOrEmpty(filter.SortName))
            {
                model.Sort = " a.StartDate desc";
            }
            else
            {
                model.Sort = filter.SortName + (filter.SortWay ? " desc" : " asc");
            }
            model.Fields = " a.Id,b.CustomerName,b.IDNum,b.FinancialTypeId,a.TrainExamName,a.StartDate,a.EndDate,a.UserId,c.Id as ProposalId,c.Status";
            model.Filter = GetStrWhereCopy2(filter) + GetStrWhereCopy5(filter);
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<UnitTrainExam> GetList4(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.Id,b.CustomerName,b.IDNum,b.FinancialTypeId,a.TrainExamName,a.StartDate,a.EndDate,a.UserId,c.Id as ProposalId ");
            strSql.Append(" FROM ");
            strSql.Append(" dbo.TrainExam a join dbo.[ExamCase] b on a.Id=b.TrainExamId inner join TrainExamClass d on d.TrainExamId=a.Id and d.ClassId=" + filter.ClassId + " LEFT JOIN EntryAssessment c on a.Id=c.TrainExamId and c.UserId=" + filter.UserId2);
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhereCopy6(filter));
            List<UnitTrainExam> list = new List<UnitTrainExam>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<UnitTrainExam>(strSql.ToString()).ToList();
            }
            return list;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<UnitTrainExam> GetList6(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.Id,b.CustomerName,b.IDNum,b.FinancialTypeId,a.TrainExamName,a.StartDate,a.EndDate,a.UserId,c.Id as ProposalId ");
            strSql.Append(" FROM ");
            strSql.Append(" dbo.TrainExam a join dbo.[ExamCase] b on a.Id=b.TrainExamId LEFT JOIN Proposal c on a.Id=c.TrainExamId ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhereCopy6(filter));
            List<UnitTrainExam> list = new List<UnitTrainExam>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<UnitTrainExam>(strSql.ToString()).ToList();
            }
            return list;
        }

        /// <summary>
        /// 学生端销售机会列表联表获取
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></return>
        public PageModel GetTrainExamAndAssessmentResults(CustomFilter filter)
        {
            //特殊处理
            int userId = 0;
            if (filter.UserId.HasValue)
            {
                userId = filter.UserId.Value;

            }
            StringBuilder strTable = new StringBuilder();
            strTable.Append(" TrainExam a JOIN ExamCase b on a.Id = b.TrainExamId inner join AssessmentResults d on a.Id=d.TrainExamId LEFT JOIN Proposal c ");
            strTable.AppendFormat(" on a.Id = c.TrainExamId  and c.UserId = {0}  and c.UpdateDate >= a.StartDate", userId);

            PageModel model = new PageModel();
            model.Tables = strTable.ToString();
            model.PKey = "a.Id";
            if (string.IsNullOrEmpty(filter.SortName))
            {
                model.Sort = "c.UpdateDate desc";
            }
            else
            {
                model.Sort = filter.SortName + (filter.SortWay ? " desc" : " asc");
            }
            model.Fields = " a.Id,b.CustomerName,b.IDNum,b.FinancialTypeId,a.TrainExamName,a.TrainExamStatus as TrainExamStatus,a.StartDate,a.EndDate,a.UserId,c.Id as ProposalId,d.TotalScore,d.SubjectiveResults,d.ObjectiveResults,d.UserId as UID,d.CreateTime,c.Status as ScoreStatus,a.AllScore,c.UpdateDate";

            model.Filter = GetStrWhereCopy2(filter) + GetStrWhereCopy4(filter);
            return model;
        }


        /// <summary>
        /// 教师端销售机会实训考核列表联表获取
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public PageModel GetTecDetailTrainExamPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = " dbo.[TrainExam] a join dbo.[Case] b on a.CaseId=b.Id";
            model.PKey = "Id";

            if (string.IsNullOrEmpty(filter.SortName))
            {
                model.Sort = " a.StartDate desc";
            }
            else
            {
                model.Sort = filter.SortName + (filter.SortWay ? " desc" : " asc");
            }
            model.Fields = " a.Id,a.CaseId,a.ExamCaseId,b.CustomerName,b.IDNum,b.FinancialTypeId,a.TrainExamName,a.Status,a.StartDate,a.EndDate,a.UserId,a.AllScore,a.ExamTypeId ";
            model.Filter = GetStrWhereCopy(filter);

            return model;
        }


        public TrainExam GetTrainExam(int TrainExamId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from [TrainExam] where Id=@TrainExamId; ");
            strSql.Append("select * from [TrainExamClass] where TrainExamId=@TrainExamId; ");
            strSql.Append("select * from [TrainExamDetail] where TrainExamId=@TrainExamId; ");

            TrainExam model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@TrainExamId", TrainExamId, dbType: DbType.Int32);
                using (var multi = conn.QueryMultiple(strSql.ToString(), param))
                {
                    model = multi.Read<TrainExam>().FirstOrDefault();
                    model.TrainExamClass = multi.Read<TrainExamClass>().ToList();
                    model.TrainExamDetail = multi.Read<TrainExamDetail>().ToList();
                }
            }
            return model;
        }



        /// <summary>
        /// 根据获取实训考核发布的数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private string GetStrWhereTwo(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (filter.UserId.HasValue)
            {
                strSql.Append(" and a.UserId =" + filter.UserId.Value);
            }
            if (filter.TrainExamStatus.HasValue)
            {
                strSql.Append(" and t.TrainExamStatus=" + filter.TrainExamStatus.Value);
            }
            if (filter.ExamTypeId.HasValue)
            {
                strSql.Append(" and t.ExamTypeId=" + filter.ExamTypeId.Value);
            }
            if (filter.Status.HasValue)
            {
                strSql.Append(" and t.[Status]=" + filter.Status.Value);
            }
            return strSql.ToString();

        }

        /// <summary>
        /// 获取考核实训
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public PageModel GetTranExamAssessmentByUserId(CustomFilter filter)
        {

            PageModel model = new PageModel();

            StringBuilder strTable = new StringBuilder();
            strTable.Append(" [AssessmentResults] as a join [TrainExam] as t on a.TrainExamId =t.Id join [ExamCase] on t.Id =ExamCase.TrainExamId");//left join [Proposal] on t.Id= Proposal.TrainExamId
            model.Tables = strTable.ToString();
            model.PKey = "a.Id";
            if (string.IsNullOrEmpty(filter.SortName))
            {
                model.Sort = "a.Id";
            }
            else
            {
                model.Sort = filter.SortName + (filter.SortWay ? " asc" : " desc");
            }
            model.Fields = " a.Id,a.UserId,a.TotalScore,a.SubjectiveResults,a.ObjectiveResults,a.CreateTime,t.TrainExamName,t.StartDate,t.EndDate,ExamCase.CustomerName,ExamCase.IDType,ExamCase.IDNum,ExamCase.FinancialTypeId";
            model.Filter = GetStrWhereTwo(filter);

            return model;

        }

        public List<int> GetNoScoreExamId()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id ");
            strSql.Append(" FROM ");
            strSql.Append(" TrainExam ");
            strSql.Append(" where 1=1 and TrainExamStatus=1 ");
            List<int> list = new List<int>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<int>(strSql.ToString()).ToList();
            }
            return list;
        }

        /// <summary>
        /// 自主实训过滤
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private string GetStrSelfTrainWhere(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (filter.UserId != 0 && filter.UserId != null)
            {
                strSql.Append(" and p.UserId=" + filter.UserId);
            }
            if (filter.Status.HasValue)
            {
                strSql.Append(" and p.Status=" + filter.Status.Value);
            }
            strSql.AppendFormat(" and p.TrainExamId={0}", 0);

            return strSql.ToString();
        }



        /// <summary>
        /// 自主练习查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public PageModel GettTranExamAssessmentPageSelfTrain(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "Proposal as p left join StuCustomer as sc on p.Id=sc.ProposalId";
            model.PKey = "p.Id";
            model.Sort = "p.Id desc";
            model.Fields = " p.Id,sc.CustomerName,sc.IDNum,p.UpdateDate";
            model.Filter = GetStrSelfTrainWhere(filter);
            return model;
        }


        /// <summary>
        /// 班级集合中，是否有已发布的销售机会/实训考核
        /// </summary>
        /// <param name="classId">班级集合</param>
        /// <returns></returns>
        public bool IsPublish(List<int> classId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from TrainExam where Status=@Status and GETDATE()<=EndDate ");
            strSql.Append(" and Id in (select TrainExamId from TrainExamClass where ClassId in @classIds)");

            int result = 0;
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                result = conn.Query<int>(strSql.ToString(), new { Status = (int)TrainExamPublishState.Published, classIds = classId.ToArray() }).FirstOrDefault();
            }
            return result > 0;
        }

        /// <summary>
        /// 获取大赛的案例列表
        /// </summary>
        /// <param name="TrainExamId"></param>
        /// <returns></returns>
        public List<TrainExam> GetTrainExamWithDetail(int MatchId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from [TrainExam] where CompetitionId=@CompetitionId; ");

            var param = new DynamicParameters();
            List<TrainExam> list = new List<TrainExam>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                //param.Add("@CompetitionId", CompetitionId, dbType: DbType.Int32);
                list = conn.Query<TrainExam>(strSql.ToString(), new { CompetitionId = MatchId }).ToList();

                foreach (TrainExam te in list)
                {
                    strSql.Clear();
                    strSql.Append("select * from [TrainExamDetail] where TrainExamId=@Id; ");
                    strSql.Append("select * from [Case] where Id=@CaseId; ");

                    var param2 = new DynamicParameters();
                    param2.Add("@Id", te.Id, dbType: DbType.Int32);
                    param2.Add("@CaseId", te.CaseId, dbType: DbType.Int32);
                    using (var multi = conn.QueryMultiple(strSql.ToString(), param2))
                    {
                        te.TrainExamDetail = multi.Read<TrainExamDetail>().ToList();
                        te.Case = multi.Read<Case>().FirstOrDefault();
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 获取大赛的案例列表
        /// </summary>
        /// <param name="TrainExamId"></param>
        /// <returns></returns>
        public List<TrainExam> GetTrainExamByMatch(int MatchId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from [TrainExam] where CompetitionId=@CompetitionId; ");

            var param = new DynamicParameters();
            List<TrainExam> list = new List<TrainExam>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<TrainExam>(strSql.ToString(), new { CompetitionId = MatchId }).ToList();
            }

            return list;
        }
    }
}

