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
    /// 数据访问类:LifeEducationPlan
    /// </summary>
    public partial class LifeEducationPlanDAL
    {
        public LifeEducationPlanDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from LifeEducationPlan where Id=@Id ");

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
        public int Add(LifeEducationPlan model)
        {
            int result = 0;
            var conn = DBHelper.CreateConnection();
            conn.Open();
            var tran = conn.BeginTransaction();

            try
            {
                StringBuilder strSql = new StringBuilder();
                var param = new DynamicParameters();
                #region 教育规划信息
                strSql.Append("insert into LifeEducationPlan(");
                strSql.Append("ProposalId,ChildAge,InlandEduFee,ForeignEduFee,Insurance,Deposit,Other,EduTotalAmount,ReturnOnInvestment,DisposableInput,MonthlyInvestment,RegularYear,TargetAmount,Analysis)");

                strSql.Append(" values (");
                strSql.Append("@ProposalId,@ChildAge,@InlandEduFee,@ForeignEduFee,@Insurance,@Deposit,@Other,@EduTotalAmount,@ReturnOnInvestment,@DisposableInput,@MonthlyInvestment,@RegularYear,@TargetAmount,@Analysis)");
                strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@ChildAge", model.ChildAge, dbType: DbType.Int32);
                param.Add("@InlandEduFee", model.InlandEduFee, dbType: DbType.Decimal);
                param.Add("@ForeignEduFee", model.ForeignEduFee, dbType: DbType.Decimal);
                param.Add("@Insurance", model.Insurance, dbType: DbType.Decimal);
                param.Add("@Deposit", model.Deposit, dbType: DbType.Decimal);
                param.Add("@Other", model.Other, dbType: DbType.Decimal);
                param.Add("@EduTotalAmount", model.EduTotalAmount, dbType: DbType.Decimal);
                param.Add("@ReturnOnInvestment", model.ReturnOnInvestment, dbType: DbType.Decimal);
                param.Add("@DisposableInput", model.DisposableInput, dbType: DbType.Decimal);
                param.Add("@MonthlyInvestment", model.MonthlyInvestment, dbType: DbType.Decimal);
                param.Add("@RegularYear", model.RegularYear, dbType: DbType.Int32);
                param.Add("@TargetAmount", model.TargetAmount, dbType: DbType.Decimal);
                param.Add("@Analysis", model.Analysis, dbType: DbType.String);
                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(strSql.ToString(), param, tran);
                result = param.Get<int>("@returnid");
                #endregion               

                #region 教育规划详细信息
                if (model.LifeEducationPlanDetailList != null && model.LifeEducationPlanDetailList.Count > 0)
                {
                    foreach (var item in model.LifeEducationPlanDetailList)
                    {
                        strSql.Clear();
                        param = new DynamicParameters();

                        strSql.Append("insert into LifeEducationPlanDetail(");
                        strSql.Append("ProposalId,EduStage,EduAge,EduTime,Tuition,EduTuition,TotalTuition)");

                        strSql.Append(" values (");
                        strSql.Append("@ProposalId,@EduStage,@EduAge,@EduTime,@Tuition,@EduTuition,@TotalTuition)");
                        strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                        param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                        param.Add("@EduStage", item.EduStage, dbType: DbType.Int32);
                        param.Add("@EduAge", item.EduAge, dbType: DbType.Int32);
                        param.Add("@EduTime", item.EduTime, dbType: DbType.Int32);
                        param.Add("@Tuition", item.Tuition, dbType: DbType.Decimal);
                        param.Add("@EduTuition", item.EduTuition, dbType: DbType.Decimal);
                        param.Add("@TotalTuition", item.TotalTuition, dbType: DbType.Decimal);
                        param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                        conn.Execute(strSql.ToString(), param, tran);
                    }
                }
                #endregion
                tran.Commit();
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("[回滚]新增案例出错", ex);
                tran.Rollback();
            }
            finally
            {
                if (tran != null)
                    tran.Dispose();
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
        public bool Update(LifeEducationPlan model)
        {
            int result = 0;
            var conn = DBHelper.CreateConnection();
            conn.Open();
            var tran = conn.BeginTransaction();


            try
            {
                StringBuilder strSql = new StringBuilder();
                var param = new DynamicParameters();

                #region 教育规划信息
                strSql.Append("update LifeEducationPlan set ");
                strSql.Append("ProposalId=@ProposalId,");
                strSql.Append("ChildAge=@ChildAge,");
                strSql.Append("InlandEduFee=@InlandEduFee,");
                strSql.Append("ForeignEduFee=@ForeignEduFee,");
                strSql.Append("Insurance=@Insurance,");
                strSql.Append("Deposit=@Deposit,");
                strSql.Append("Other=@Other,");
                strSql.Append("EduTotalAmount=@EduTotalAmount,");
                strSql.Append("ReturnOnInvestment=@ReturnOnInvestment,");
                strSql.Append("DisposableInput=@DisposableInput,");
                strSql.Append("MonthlyInvestment=@MonthlyInvestment,");
                strSql.Append("RegularYear=@RegularYear,");
                strSql.Append("TargetAmount=@TargetAmount,");
                strSql.Append("Analysis=@Analysis");
                strSql.Append(" where Id=@Id ");

                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@ChildAge", model.ChildAge, dbType: DbType.Int32);
                param.Add("@InlandEduFee", model.InlandEduFee, dbType: DbType.Decimal);
                param.Add("@ForeignEduFee", model.ForeignEduFee, dbType: DbType.Decimal);
                param.Add("@Insurance", model.Insurance, dbType: DbType.Decimal);
                param.Add("@Deposit", model.Deposit, dbType: DbType.Decimal);
                param.Add("@Other", model.Other, dbType: DbType.Decimal);
                param.Add("@EduTotalAmount", model.EduTotalAmount, dbType: DbType.Decimal);
                param.Add("@ReturnOnInvestment", model.ReturnOnInvestment, dbType: DbType.Decimal);
                param.Add("@DisposableInput", model.DisposableInput, dbType: DbType.Decimal);
                param.Add("@MonthlyInvestment", model.MonthlyInvestment, dbType: DbType.Decimal);
                param.Add("@RegularYear", model.RegularYear, dbType: DbType.Int32);
                param.Add("@TargetAmount", model.TargetAmount, dbType: DbType.Decimal);
                param.Add("@Analysis", model.Analysis, dbType: DbType.String);
                result = conn.Execute(strSql.ToString(), param, tran);
                #endregion

                #region 删除教育规划详细信息
                if (model.LifeEducationPlanDetailList != null && model.LifeEducationPlanDetailList.Count > 0)
                {
                    strSql.Clear();
                    param = new DynamicParameters();
                    strSql.Append("delete from LifeEducationPlanDetail ");
                    strSql.Append(" where ProposalId=@ProposalId ");

                    param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                    result = conn.Execute(strSql.ToString(), param, tran);
                }
                #endregion


                #region 教育规划详细信息
                if (model.LifeEducationPlanDetailList != null && model.LifeEducationPlanDetailList.Count > 0)
                {
                    foreach (var item in model.LifeEducationPlanDetailList)
                    {
                        strSql.Clear();
                        param = new DynamicParameters();

                        strSql.Append("insert into LifeEducationPlanDetail(");
                        strSql.Append("ProposalId,EduStage,EduAge,EduTime,Tuition,EduTuition,TotalTuition)");

                        strSql.Append(" values (");
                        strSql.Append("@ProposalId,@EduStage,@EduAge,@EduTime,@Tuition,@EduTuition,@TotalTuition)");
                        strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                        param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                        param.Add("@EduStage", item.EduStage, dbType: DbType.Int32);
                        param.Add("@EduAge", item.EduAge, dbType: DbType.Int32);
                        param.Add("@EduTime", item.EduTime, dbType: DbType.Int32);
                        param.Add("@Tuition", item.Tuition, dbType: DbType.Decimal);
                        param.Add("@EduTuition", item.EduTuition, dbType: DbType.Decimal);
                        param.Add("@TotalTuition", item.TotalTuition, dbType: DbType.Decimal);
                        param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                        conn.Execute(strSql.ToString(), param, tran);
                    }
                }
                #endregion


                tran.Commit();
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("[回滚]修改案例出错", ex);
                tran.Rollback();
            }
            finally
            {
                if (tran != null)
                    tran.Dispose();
                if (conn != null)
                    conn.Close();
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
            strSql.Append("delete from LifeEducationPlan ");
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
        public LifeEducationPlan GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,ChildAge,InlandEduFee,ForeignEduFee,Insurance,Deposit,Other,EduTotalAmount,ReturnOnInvestment,DisposableInput,MonthlyInvestment,RegularYear,TargetAmount,Analysis from LifeEducationPlan ");
            strSql.Append(" where Id=@Id ");

            LifeEducationPlan model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<LifeEducationPlan>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LifeEducationPlan GetModel2(int ProposalId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from [LifeEducationPlan] where ProposalId=@ProposalId; ");
            strSql.Append("select * from [LifeEducationPlanDetail] where ProposalId=@ProposalId; ");

            LifeEducationPlan model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", ProposalId, dbType: DbType.Int32);
                using (var multi = conn.QueryMultiple(strSql.ToString(), param))
                {
                    model = multi.Read<LifeEducationPlan>().FirstOrDefault();
                    if (model != null)
                    {
                        model.LifeEducationPlanDetailList = multi.Read<LifeEducationPlanDetail>().ToList();
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
        public List<LifeEducationPlan> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,ChildAge,InlandEduFee,ForeignEduFee,Insurance,Deposit,Other,EduTotalAmount,ReturnOnInvestment,DisposableInput,MonthlyInvestment,RegularYear,TargetAmount,Analysis ");
            strSql.Append(" FROM LifeEducationPlan ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<LifeEducationPlan> list = new List<LifeEducationPlan>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<LifeEducationPlan>(strSql.ToString()).ToList();
            }
            return list;
        }

        /// <summary>
        /// 根据CustomFilter获取where语句
        /// </summary>
        private string GetStrWhere(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (filter.ProposalId.HasValue)
            {
                strSql.Append(" and ProposalId=" + filter.ProposalId);
            }
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
        public PageModel GetLifeEducationPlanPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "LifeEducationPlan";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,ProposalId,ChildAge,InlandEduFee,ForeignEduFee,Insurance,Deposit,Other,EduTotalAmount,ReturnOnInvestment,DisposableInput,MonthlyInvestment,RegularYear,TargetAmount,Analysis";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion




    }
}

