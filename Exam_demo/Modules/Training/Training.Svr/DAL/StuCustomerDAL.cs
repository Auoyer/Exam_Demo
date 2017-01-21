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
    /// 数据访问类:StuCustomer
    /// </summary>
    public partial class StuCustomerDAL
    {
        public StuCustomerDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="IDNum">客户证件号</param>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public bool Exists(string IDNum, int UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from StuCustomer where UserId=@UserId and IDNum=@IDNum ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@UserId", UserId, dbType: DbType.Int32);
                param.Add("@IDNum", IDNum, dbType: DbType.String);
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result > 0;
        }

        #endregion

        #region  新增

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(StuCustomer model)
        {
            int result = 0;
            var conn = DBHelper.CreateConnection();
            conn.Open();
            var tran = conn.BeginTransaction();

            try
            {
                #region 新增客户信息
                StringBuilder strSql = new StringBuilder();
                var param = new DynamicParameters();

                strSql.Append("insert into StuCustomer(");
                strSql.Append("UserId,Source,CustomerNo,CustomerName,CustomerType,IDType,IDNum,CustomerStory,UpdateDate,PinYin,Age,InCome,Tel,Phone,Email,Position,Company,Address,ProposalCount,Status,ProposalId,TrainExamId,CustomerHighAssets)");

                strSql.Append(" values (");
                strSql.Append("@UserId,@Source,@CustomerNo,@CustomerName,@CustomerType,@IDType,@IDNum,@CustomerStory,@UpdateDate,@PinYin,@Age,@InCome,@Tel,@Phone,@Email,@Position,@Company,@Address,@ProposalCount,@Status,@ProposalId,@TrainExamId,@CustomerHighAssets)");
                strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");


                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@Source", model.Source, dbType: DbType.Int32);
                param.Add("@CustomerNo", model.CustomerNo, dbType: DbType.String);
                param.Add("@CustomerName", model.CustomerName, dbType: DbType.String);
                param.Add("@CustomerType", model.CustomerType, dbType: DbType.Int32);
                param.Add("@IDType", model.IDType, dbType: DbType.Int32);
                param.Add("@IDNum", model.IDNum, dbType: DbType.String);
                param.Add("@CustomerStory", model.CustomerStory, dbType: DbType.String);
                param.Add("@UpdateDate", model.UpdateDate, dbType: DbType.DateTime);
                param.Add("@PinYin", model.PinYin, dbType: DbType.String);
                param.Add("@Age", model.Age, dbType: DbType.Int32);
                param.Add("@InCome", model.InCome, dbType: DbType.Decimal);
                param.Add("@Tel", model.Tel, dbType: DbType.String);
                param.Add("@Phone", model.Phone, dbType: DbType.String);
                param.Add("@Email", model.Email, dbType: DbType.String);
                param.Add("@Position", model.Position, dbType: DbType.String);
                param.Add("@Company", model.Company, dbType: DbType.String);
                param.Add("@Address", model.Address, dbType: DbType.String);
                param.Add("@ProposalCount", model.ProposalCount, dbType: DbType.Int32);
                param.Add("@Status", model.Status, dbType: DbType.Int32);
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@TrainExamId", model.TrainExamId, dbType: DbType.Int32);
                param.Add("@CustomerHighAssets", model.CustomerHighAssets, dbType: DbType.Boolean);
                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);

                conn.Execute(strSql.ToString(), param, tran);
                result = param.Get<int>("@returnid");
                #endregion

                #region 现在家属信息
                if (model.StuCustomerDetail != null && model.StuCustomerDetail.Count > 0)
                {
                    foreach (var item in model.StuCustomerDetail)
                    {
                        strSql.Clear();
                        param = new DynamicParameters();

                        strSql.Append("insert into StuCustomerDetail(");
                        strSql.Append("CustomerId,DependentName,Age,Relation,InCome)");

                        strSql.Append(" values (");
                        strSql.Append("@CustomerId,@DependentName,@Age,@Relation,@InCome)");
                        strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                        param.Add("@CustomerId", result, dbType: DbType.Int32);
                        param.Add("@DependentName", item.DependentName, dbType: DbType.String);
                        param.Add("@Age", item.Age, dbType: DbType.Int32);
                        param.Add("@Relation", item.Relation, dbType: DbType.String);
                        param.Add("@InCome", item.InCome, dbType: DbType.Decimal);
                        param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                        conn.Execute(strSql.ToString(), param, tran);
                    }
                }
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
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("insert into StuCustomer(");
            //strSql.Append("UserId,Source,CustomerNo,CustomerName,CustomerType,IDType,IDNum,CustomerStory,UpdateDate,PinYin,Age,InCome,Tel,Phone,Email,Position,Company,Address,ProposalCount,Status,ProposalId,TrainExamId)");

            //strSql.Append(" values (");
            //strSql.Append("@UserId,@Source,@CustomerNo,@CustomerName,@CustomerType,@IDType,@IDNum,@CustomerStory,@UpdateDate,@PinYin,@Age,@InCome,@Tel,@Phone,@Email,@Position,@Company,@Address,@ProposalCount,@Status,@ProposalId,@TrainExamId)");
            //strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            //int result = 0;
            //var param = new DynamicParameters();
            //using (var conn = DBHelper.CreateConnection())
            //{
            //    conn.Open();
            //    param.Add("@UserId", model.UserId, dbType: DbType.Int32);
            //    param.Add("@Source", model.Source, dbType: DbType.Int32);
            //    param.Add("@CustomerNo", model.CustomerNo, dbType: DbType.String);
            //    param.Add("@CustomerName", model.CustomerName, dbType: DbType.String);
            //    param.Add("@CustomerType", model.CustomerType, dbType: DbType.Int32);
            //    param.Add("@IDType", model.IDType, dbType: DbType.Int32);
            //    param.Add("@IDNum", model.IDNum, dbType: DbType.String);
            //    param.Add("@CustomerStory", model.CustomerStory, dbType: DbType.String);
            //    param.Add("@UpdateDate", model.UpdateDate, dbType: DbType.DateTime);
            //    param.Add("@PinYin", model.PinYin, dbType: DbType.String);
            //    param.Add("@Age", model.Age, dbType: DbType.Int32);
            //    param.Add("@InCome", model.InCome, dbType: DbType.Decimal);
            //    param.Add("@Tel", model.Tel, dbType: DbType.String);
            //    param.Add("@Phone", model.Phone, dbType: DbType.String);
            //    param.Add("@Email", model.Email, dbType: DbType.String);
            //    param.Add("@Position", model.Position, dbType: DbType.String);
            //    param.Add("@Company", model.Company, dbType: DbType.String);
            //    param.Add("@Address", model.Address, dbType: DbType.String);
            //    param.Add("@ProposalCount", model.ProposalCount, dbType: DbType.Int32);
            //    param.Add("@Status", model.Status, dbType: DbType.Int32);
            //    param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
            //    param.Add("@TrainExamId", model.TrainExamId, dbType: DbType.Int32);
            //    param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
            //    conn.Execute(strSql.ToString(), param);
            //    result = param.Get<int>("@returnid");
            //}
            //return result;
        }
        #endregion

        #region  更新
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(StuCustomer model)
        {
            bool addResult = false;
            var conn = DBHelper.CreateConnection();
            conn.Open();
            var tran = conn.BeginTransaction();

            try
            {
                #region 修改客户信息

                StringBuilder strSql = new StringBuilder();
                var param = new DynamicParameters();

                strSql.Append("update StuCustomer set ");
                strSql.Append("UserId=@UserId,");
                strSql.Append("Source=@Source,");
                strSql.Append("CustomerNo=@CustomerNo,");
                strSql.Append("CustomerName=@CustomerName,");
                strSql.Append("CustomerType=@CustomerType,");
                strSql.Append("IDType=@IDType,");
                strSql.Append("IDNum=@IDNum,");
                strSql.Append("CustomerStory=@CustomerStory,");
                strSql.Append("UpdateDate=@UpdateDate,");
                strSql.Append("PinYin=@PinYin,");
                strSql.Append("Age=@Age,");
                strSql.Append("InCome=@InCome,");
                strSql.Append("Tel=@Tel,");
                strSql.Append("Phone=@Phone,");
                strSql.Append("Email=@Email,");
                strSql.Append("Position=@Position,");
                strSql.Append("Company=@Company,");
                strSql.Append("Address=@Address,");

                strSql.Append("ProposalCount=@ProposalCount,");
                strSql.Append("Status=@Status,");
                strSql.Append("ProposalId=@ProposalId,");
                strSql.Append("TrainExamId=@TrainExamId,");
                strSql.Append("CustomerHighAssets=@CustomerHighAssets");
                strSql.Append(" where Id=@Id ");

                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@Source", model.Source, dbType: DbType.Int32);
                param.Add("@CustomerNo", model.CustomerNo, dbType: DbType.String);
                param.Add("@CustomerName", model.CustomerName, dbType: DbType.String);
                param.Add("@CustomerType", model.CustomerType, dbType: DbType.Int32);
                param.Add("@IDType", model.IDType, dbType: DbType.Int32);
                param.Add("@IDNum", model.IDNum, dbType: DbType.String);
                param.Add("@CustomerStory", model.CustomerStory, dbType: DbType.String);
                param.Add("@UpdateDate", model.UpdateDate, dbType: DbType.DateTime);
                param.Add("@PinYin", model.PinYin, dbType: DbType.String);
                param.Add("@Age", model.Age, dbType: DbType.Int32);
                param.Add("@InCome", model.InCome, dbType: DbType.Decimal);
                param.Add("@Tel", model.Tel, dbType: DbType.String);
                param.Add("@Phone", model.Phone, dbType: DbType.String);
                param.Add("@Email", model.Email, dbType: DbType.String);
                param.Add("@Position", model.Position, dbType: DbType.String);
                param.Add("@Company", model.Company, dbType: DbType.String);
                param.Add("@Address", model.Address, dbType: DbType.String);

                param.Add("@ProposalCount", model.ProposalCount, dbType: DbType.Int32);
                param.Add("@Status", model.Status, dbType: DbType.Int32);
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@TrainExamId", model.TrainExamId, dbType: DbType.Int32);
                param.Add("@CustomerHighAssets", model.CustomerHighAssets, dbType: DbType.Boolean);

                conn.Execute(strSql.ToString(), param, tran);

                #endregion

                #region 删除客户家属信息
                strSql.Clear();
                param = new DynamicParameters();
                strSql.Append(" delete from StuCustomerDetail ");
                strSql.Append(" where CustomerId=@CustomerId");
                param.Add("@CustomerId", model.Id, dbType: DbType.Int32);
                conn.Execute(strSql.ToString(), param, tran);

                #endregion


                #region 新增建议书客户家属信息

                if (model.StuCustomerDetail != null && model.StuCustomerDetail.Count > 0)
                {


                    model.StuCustomerDetail.ForEach(r =>
                    {
                        strSql.Clear();
                        param = new DynamicParameters();

                        strSql.Append("insert into StuCustomerDetail(");
                        strSql.Append("CustomerId,DependentName,Age,Relation,InCome)");

                        strSql.Append(" values (");
                        strSql.Append("@CustomerId,@DependentName,@Age,@Relation,@InCome)");
                        strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                        param.Add("@CustomerId", model.Id, dbType: DbType.Int32);
                        param.Add("@DependentName", r.DependentName, dbType: DbType.String);
                        param.Add("@Age", r.Age, dbType: DbType.Int32);
                        param.Add("@Relation", r.Relation, dbType: DbType.String);
                        param.Add("@InCome", r.InCome, dbType: DbType.Decimal);
                        param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                        conn.Execute(strSql.ToString(), param, tran);
                    });
                }

                #endregion

                tran.Commit();
                addResult = true;
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("Proposal Add", ex);
                tran.Rollback();
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
            return addResult;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update2(int Id, int Status)
        {
            bool addResult = false;
            var conn = DBHelper.CreateConnection();
            conn.Open();
            var tran = conn.BeginTransaction();

            try
            {
                #region 修改客户信息

                StringBuilder strSql = new StringBuilder();
                var param = new DynamicParameters();

                strSql.Append("update StuCustomer set ");
                strSql.Append("Status=@Status,");
                strSql.Append(" where Id=@Id ");
                param.Add("@Status", Status, dbType: DbType.Int32);
                param.Add("@Id", Id, dbType: DbType.Int32);

                conn.Execute(strSql.ToString(), param, tran);

                #endregion

                tran.Commit();
                addResult = true;
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("Proposal Add", ex);
                tran.Rollback();
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
            return addResult;
        }

        #endregion

        #region  删除
        /// <summary>
        /// 删除客户信息(真删)
        /// 同步删除以下信息：
        /// 1.建议书(真删)
        /// 2.工作日程(真删)
        /// 3.....后续根据需求增加
        /// </summary>
        /// <param name="Id">客户Id</param>
        /// <returns></returns>
        public bool Delete(int Id)
        {
            bool result = false;
            StringBuilder strSql = new StringBuilder();
            var param = new DynamicParameters();

            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();

                try
                {

                    #region 客户信息

                    strSql.Clear();
                    param = new DynamicParameters();
                    strSql.Append("delete from StuCustomer ");
                    strSql.Append(" where Id=@Id ");

                    param.Add("@Id", Id, dbType: DbType.Int32);
                    conn.Execute(strSql.ToString(), param, tran);
                    #endregion

                    #region 客户详细信息

                    strSql.Clear();
                    param = new DynamicParameters();
                    strSql.Append("delete from StuCustomerDetail ");
                    strSql.Append(" where CustomerId=@CustomerId ");

                    param.Add("@CustomerId", Id, dbType: DbType.Int32);
                    conn.Execute(strSql.ToString(), param, tran);
                    #endregion

                    #region 建议书(真删)
                    List<int> ids = new List<int>();

                    strSql.Clear();
                    param = new DynamicParameters();
                    strSql.Append("select Id from Proposal where StuCustomerId = @StuCustomerId");
                    param.Add("@StuCustomerId", Id, dbType: DbType.Int32);
                    ids = conn.Query<int>(strSql.ToString(), param, tran).ToList();

                    #region 建议书 Proposal
                    if (ids != null && ids.Count > 0)
                    {
                        strSql.Clear();
                        strSql.Append("delete from Proposal where Id in @Ids ");
                        conn.Execute(strSql.ToString(), new { ids = ids.ToArray() }, tran);
                    }
                    #endregion

                    #region 建议书客户信息 ProposalCustomer
                    if (ids != null && ids.Count > 0)
                    {
                        strSql.Clear();
                        strSql.Append("delete from ProposalCustomer where ProposalId in @Ids ");
                        conn.Execute(strSql.ToString(), new { ids = ids.ToArray() }, tran);
                    }
                    #endregion

                    #region 建议书客户详细信息 ProposalCustomerDetail
                    if (ids != null && ids.Count > 0)
                    {
                        strSql.Clear();
                        strSql.Append("delete from ProposalCustomerDetail where ProposalId in @Ids ");
                        conn.Execute(strSql.ToString(), new { ids = ids.ToArray() }, tran);
                    }
                    #endregion

                    #region 风险评测 RiskIndex
                    if (ids != null && ids.Count > 0)
                    {
                        strSql.Clear();
                        strSql.Append("delete from RiskIndex where ProposalId in @Ids ");
                        conn.Execute(strSql.ToString(), new { ids = ids.ToArray() }, tran);
                    }
                    #endregion

                    #region 资产负债表 Liability
                    if (ids != null && ids.Count > 0)
                    {
                        strSql.Clear();
                        strSql.Append("delete from Liability where ProposalId in @Ids ");
                        conn.Execute(strSql.ToString(), new { ids = ids.ToArray() }, tran);
                    }
                    #endregion

                    #region 收支储蓄表 IncomeAndExpenses
                    if (ids != null && ids.Count > 0)
                    {
                        strSql.Clear();
                        strSql.Append("delete from IncomeAndExpenses where ProposalId in @Ids ");
                        conn.Execute(strSql.ToString(), new { ids = ids.ToArray() }, tran);
                    }
                    #endregion

                    #region 现金规划 CashPlan
                    if (ids != null && ids.Count > 0)
                    {
                        strSql.Clear();
                        strSql.Append("delete from CashPlan where ProposalId in @Ids ");
                        conn.Execute(strSql.ToString(), new { ids = ids.ToArray() }, tran);
                    }
                    #endregion

                    #region 现金流量 CashFlow
                    if (ids != null && ids.Count > 0)
                    {
                        strSql.Clear();
                        strSql.Append("delete from CashFlow where ProposalId in @Ids ");
                        conn.Execute(strSql.ToString(), new { ids = ids.ToArray() }, tran);
                    }
                    #endregion

                    #region 财务比例分析 FinancialRatios
                    if (ids != null && ids.Count > 0)
                    {
                        strSql.Clear();
                        strSql.Append("delete from FinancialRatios where ProposalId in @Ids ");
                        conn.Execute(strSql.ToString(), new { ids = ids.ToArray() }, tran);
                    }
                    #endregion

                    #region 教育规划信息 LifeEducationPlan
                    if (ids != null && ids.Count > 0)
                    {
                        strSql.Clear();
                        strSql.Append("delete from LifeEducationPlan where ProposalId in @Ids ");
                        conn.Execute(strSql.ToString(), new { ids = ids.ToArray() }, tran);
                    }
                    #endregion

                    #region 教育规划详细信息 LifeEducationPlanDetail
                    if (ids != null && ids.Count > 0)
                    {
                        strSql.Clear();
                        strSql.Append("delete from LifeEducationPlanDetail where ProposalId in @Ids ");
                        conn.Execute(strSql.ToString(), new { ids = ids.ToArray() }, tran);
                    }
                    #endregion

                    #region 消费规划 ConsumptionPlan
                    if (ids != null && ids.Count > 0)
                    {
                        strSql.Clear();
                        strSql.Append("delete from ConsumptionPlan where ProposalId in @Ids ");
                        conn.Execute(strSql.ToString(), new { ids = ids.ToArray() }, tran);
                    }
                    #endregion

                    #region 创业规划 StartAnUndertakingPlan
                    if (ids != null && ids.Count > 0)
                    {
                        strSql.Clear();
                        strSql.Append("delete from StartAnUndertakingPlan where ProposalId in @Ids ");
                        conn.Execute(strSql.ToString(), new { ids = ids.ToArray() }, tran);
                    }
                    #endregion

                    #region 退休规划 RetirementPlan
                    if (ids != null && ids.Count > 0)
                    {
                        strSql.Clear();
                        strSql.Append("delete from RetirementPlan where ProposalId in @Ids ");
                        conn.Execute(strSql.ToString(), new { ids = ids.ToArray() }, tran);
                    }
                    #endregion

                    #region 保险规划 InsurancePlan
                    if (ids != null && ids.Count > 0)
                    {
                        strSql.Clear();
                        strSql.Append("delete from InsurancePlan where ProposalId in @Ids ");
                        conn.Execute(strSql.ToString(), new { ids = ids.ToArray() }, tran);
                    }
                    #endregion

                    #region 投资规划 InvestmentPlan
                    if (ids != null && ids.Count > 0)
                    {
                        strSql.Clear();
                        strSql.Append("delete from InvestmentPlan where ProposalId in @Ids ");
                        conn.Execute(strSql.ToString(), new { ids = ids.ToArray() }, tran);
                    }
                    #endregion

                    #region 投资规划产品 InvestmentPlanProduct
                    if (ids != null && ids.Count > 0)
                    {
                        strSql.Clear();
                        strSql.Append("delete from InvestmentPlanProduct where ProposalId in @Ids ");
                        conn.Execute(strSql.ToString(), new { ids = ids.ToArray() }, tran);
                    }
                    #endregion

                    #region 税收规划 TaxPlan
                    if (ids != null && ids.Count > 0)
                    {
                        strSql.Clear();
                        strSql.Append("delete from TaxPlan where ProposalId in @Ids ");
                        conn.Execute(strSql.ToString(), new { ids = ids.ToArray() }, tran);
                    }
                    #endregion

                    #region 财产分配 DistributionOfProperty
                    if (ids != null && ids.Count > 0)
                    {
                        strSql.Clear();
                        strSql.Append("delete from DistributionOfProperty where ProposalId in @Ids ");
                        conn.Execute(strSql.ToString(), new { ids = ids.ToArray() }, tran);
                    }
                    #endregion

                    #region 财产传承 Heritage
                    if (ids != null && ids.Count > 0)
                    {
                        strSql.Clear();
                        strSql.Append("delete from Heritage where ProposalId in @Ids ");
                        conn.Execute(strSql.ToString(), new { ids = ids.ToArray() }, tran);
                    }
                    #endregion

                    #endregion

                    #region 工作日程(真删)
                    strSql.Clear();
                    param = new DynamicParameters();
                    strSql.Append("delete from Calendar where StuCustomerId=@StuCustomerId ");

                    param.Add("@StuCustomerId", Id, dbType: DbType.Int32);
                    conn.Execute(strSql.ToString(), param, tran);
                    #endregion

                    result = true;
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    result = false;
                    LogHelper.Log.WriteError("[回滚]删除潜在客户，已有客户信息", ex);
                    tran.Rollback();
                }
            }
            return result;
        }

        /// <summary>
        /// 删除客户信息(伪删)
        /// 同步删除以下信息：
        /// 1.建议书(伪删)
        /// 2.工作日程(真删)
        /// 3.....后续根据需求增加
        /// </summary>
        /// <param name="Id">客户Id</param>
        /// <returns></returns>
        public bool RemoveCustomer(int Id)
        {
            bool result = false;
            StringBuilder strSql = new StringBuilder();
            var param = new DynamicParameters();

            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();

                try
                {
                    #region 客户信息(伪删)

                    strSql.Clear();
                    param = new DynamicParameters();
                    strSql.Append("update StuCustomer set [Status]=@Status where Id=@Id ");

                    param.Add("@Status", (int)StuCustomerProposalStatus.Delete, dbType: DbType.Int32);
                    param.Add("@Id", Id, dbType: DbType.Int32);
                    conn.Execute(strSql.ToString(), param, tran);
                    #endregion

                    #region 建议书(伪删)
                    strSql.Clear();
                    param = new DynamicParameters();
                    strSql.Append("update Proposal set [Status]=@Status where StuCustomerId=@StuCustomerId ");

                    param.Add("@Status", (int)StuCustomerProposalStatus.Delete, dbType: DbType.Int32);
                    param.Add("@StuCustomerId", Id, dbType: DbType.Int32);
                    conn.Execute(strSql.ToString(), param, tran);
                    #endregion

                    #region 工作日程(真删)
                    strSql.Clear();
                    param = new DynamicParameters();
                    strSql.Append("delete from Calendar where StuCustomerId=@StuCustomerId ");

                    param.Add("@StuCustomerId", Id, dbType: DbType.Int32);
                    conn.Execute(strSql.ToString(), param, tran);
                    #endregion

                    result = true;
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    result = false;
                    LogHelper.Log.WriteError("[回滚]删除潜在客户，已有客户信息", ex);
                    tran.Rollback();
                }
            }
            return result;
        }

        #endregion

        #region  获取实体
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public StuCustomer GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,UserId,Source,CustomerName,CustomerNo,CustomerType,IDType,IDNum,CustomerStory,UpdateDate,PinYin,Age,InCome,Tel,Phone,Email,Position,Company,Address,ProposalCount,Status,ProposalId,TrainExamId,CustomerHighAssets from StuCustomer ");
            strSql.Append(" where Id=@Id ");

            StuCustomer model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<StuCustomer>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <param name="IDNum">客户证件号</param>
        /// <param name="UserId">学生Id</param>
        /// <returns>客户信息实体</returns>
        public StuCustomer GetModel(string IDNum, int UserId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,UserId,Source,CustomerName,CustomerNo,CustomerType,IDType,IDNum,CustomerStory,UpdateDate,PinYin,Age,InCome,Tel,Phone,Email,Position,Company,Address,ProposalCount,Status,ProposalId,TrainExamId,CustomerHighAssets from StuCustomer ");
            strSql.Append(" where IDNum=@IDNum and  UserId=@UserId and Source<>2");

            StuCustomer model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@IDNum", IDNum, dbType: DbType.String);
                param.Add("@UserId", UserId, dbType: DbType.Int32);
                model = conn.Query<StuCustomer>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <param name="IDNum">客户证件号</param>
        /// <param name="UserId">学生Id</param>
        /// <returns>客户信息实体</returns>
        public StuCustomer GetModel2(int TrainExamId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,UserId,Source,CustomerName,CustomerNo,CustomerType,IDType,IDNum,CustomerStory,UpdateDate,PinYin,Age,InCome,Tel,Phone,Email,Position,Company,Address,ProposalCount,Status,ProposalId,TrainExamId,CustomerHighAssets from StuCustomer ");
            strSql.Append(" where TrainExamId=@TrainExamId");

            StuCustomer model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@TrainExamId", TrainExamId, dbType: DbType.String);
                model = conn.Query<StuCustomer>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region 获取最高净值用户
        public CustomerCount GetCustomerCountModel(int UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append("CustomerPotentialSum=COUNT(case when customertype=1 and Status!=4 then 0 end),");
            strSql.Append("CustomerExistSum=COUNT(case when customertype=2 and Status!=4 then 0 end ),");
            strSql.Append("CustomerPotentialHighAssets=COUNT(case when customertype=1 and CustomerHighAssets=1 and Status!=4 then 0 end),");
            strSql.Append("CustomerExistHighAssets=COUNT(case when customertype=2 and CustomerHighAssets=1 and Status!=4 then 0 end),");
            strSql.Append("CustomerSumNum=COUNT(case when UserId =@UserId and Status!=4 then 0 end)");
            strSql.Append(" from StuCustomer");
            strSql.Append(" where UserId=@UserId");

            CustomerCount model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@UserId", UserId, dbType: DbType.Int32);
                model = conn.Query<CustomerCount>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }


        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<StuCustomer> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,UserId,Source,CustomerName,CustomerNo,CustomerType,IDType,IDNum,UpdateDate ");
            strSql.Append(" FROM StuCustomer ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<StuCustomer> list = new List<StuCustomer>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<StuCustomer>(strSql.ToString()).ToList();
            }
            return list;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<StuCustomer> GetList2(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,UserId,Source,CustomerName,CustomerNo,CustomerType,IDType,IDNum,UpdateDate ");
            strSql.Append(" FROM StuCustomer ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere2(filter));

            List<StuCustomer> list = new List<StuCustomer>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<StuCustomer>(strSql.ToString()).ToList();
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
                strSql.Append(" and UserId=" + filter.UserId);
            }
            if (filter.CustomerSourceType.HasValue && filter.CustomerSourceType != 0)
            {
                strSql.Append(" and Source=" + filter.CustomerSourceType);
            }
            if (filter.CustomerType.HasValue)
            {
                strSql.Append(" and CustomerType=" + filter.CustomerType);
            }
            if (filter.Status.HasValue && filter.Status != 0 && filter.Status != 4)
            {
                strSql.Append(" and Status in (1,2,3)");
            }
            if (filter.Status == 4)
            {
                strSql.Append(" and Status not in (4)");
            }
            if (!string.IsNullOrEmpty(filter.KeyWords))
            {
                //过滤危险字段，如单引号等
                string key = filter.KeyWords.Replace("'", "''");
                //客户姓名，身份证号,客户编号
                strSql.AppendFormat(" and (CustomerName like '%{0}%' or IDNum like '%{0}%' or CustomerNo like '%{0}%')", key);

            }
            return strSql.ToString();
        }

        /// <summary>
        /// 根据CustomFilter获取where语句
        /// </summary>
        private string GetStrWhere22(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();

            if (filter.UserId.HasValue)
            {
                strSql.Append(" and UserId=" + filter.UserId);
            }
            if (filter.CustomerSourceType.HasValue && filter.CustomerSourceType != 0)
            {
                strSql.Append(" and Source=" + filter.CustomerSourceType);
            }
            if (filter.CustomerType.HasValue)
            {
                strSql.Append(" and CustomerType=" + filter.CustomerType);
            }
            if (filter.Status.HasValue)
            {
                strSql.Append(" and Status in (1)");
            }

            if (!string.IsNullOrEmpty(filter.KeyWords))
            {
                //过滤危险字段，如单引号等
                string key = filter.KeyWords.Replace("'", "''");
                //客户姓名，身份证号,客户编号
                strSql.AppendFormat(" and (CustomerName like '%{0}%' or IDNum like '%{0}%' or CustomerNo like '%{0}%')", key);

            }
            return strSql.ToString();
        }

        /// <summary>
        /// 根据CustomFilter获取where语句
        /// </summary>
        private string GetStrWhereCopy(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (filter.Id.HasValue)
            {
                strSql.Append(" and Id=" + filter.Id);
            }
            if (filter.UserId.HasValue)
            {
                strSql.Append(" and UserId=" + filter.UserId);
            }
            if (filter.CustomerSourceType.HasValue && filter.CustomerSourceType != 0)
            {
                strSql.Append(" and Source=" + filter.CustomerSourceType);
            }
            if (filter.CustomerType.HasValue)
            {
                strSql.Append(" and CustomerType=" + filter.CustomerType);
            }
            if (!string.IsNullOrEmpty(filter.KeyWords))
            {
                //过滤危险字段，如单引号等
                string key = filter.KeyWords.Replace("'", "''");
                //客户姓名，身份证号
                strSql.AppendFormat(" and (CustomerName like '%{0}%' or IDNum like '%{0}%' or CustomerNo like '%{0}%')", key);
            }
            return strSql.ToString();
        }


        /// <summary>
        /// 根据CustomFilter获取where语句
        /// </summary>
        private string GetStrWhere2(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (filter.UserId.HasValue)
            {
                strSql.Append(" and UserId=" + filter.UserId);
            }
            if (filter.CustomerSourceType.HasValue && filter.CustomerSourceType != 0)
            {
                strSql.Append(" and Source=" + filter.CustomerSourceType);
            }
            if (filter.CustomerType.HasValue)
            {
                strSql.Append(" and CustomerType=" + filter.CustomerType);
            }
            if (filter.Status.HasValue && filter.Status != 0)
            {
                strSql.Append(" and Status=" + filter.Status);
            }

            return strSql.ToString();
        }
        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetStuCustomerPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "StuCustomer";
            model.PKey = "Id";
            model.Sort = "UpdateDate DESC";
            model.Fields = "Id,UserId,Source,CustomerName,CustomerNo,CustomerType,IDType,IDNum,UpdateDate,ProposalCount,Status,ProposalId,TrainExamId";
            model.Filter = GetStrWhere(filter);
            return model;
        }




        #endregion

        /// <summary>
        /// 更新潜在客户/已有客户的状态和建议书Id
        /// </summary>     strSql.Append("ProposalCount=ProposalCount-1");
        /// <param name="ProposalId">建议书Id</param>
        /// <param name="Status">状态</param>
        /// <param name="TrainExamId">销售机会Id</param>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public bool UpdateStuCustomerStatusAndProposalId(int ProposalId, int Status, int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update StuCustomer set ");
            strSql.Append("ProposalId=@ProposalId,");
            strSql.Append("Status=@Status");
            strSql.Append(" where Id=@Id");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", ProposalId, dbType: DbType.Int32);
                param.Add("@Status", Status, dbType: DbType.Int32);
                param.Add("@Id", Id, dbType: DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        /// <summary>
        /// 根据建议书Id更新潜在客户/已有客户的状态
        /// </summary>
        /// <param name="ProposalId">建议书Id</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public bool UpdateStuCustomerStatus(int ProposalId, int Status, int? IsHightCustomer)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update StuCustomer set ");
            strSql.Append("Status=@Status");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                if (IsHightCustomer.HasValue)
                {
                    if (IsHightCustomer.Value > 0)
                    {
                        strSql.Append(", CustomerHighAssets=@CustomerHighAssets");
                        param.Add("CustomerHighAssets", true, dbType: DbType.Boolean);
                    }
                }

                strSql.Append(" where ProposalId=@ProposalId");
                param.Add("@ProposalId", ProposalId, dbType: DbType.Int32);
                param.Add("@Status", Status, dbType: DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        /// <summary>
        /// 根据建议书Id更新潜在客户/已有客户的状态
        /// </summary>
        /// <param name="ProposalId">建议书Id</param>
        /// <param name="UserId">用户ID</param>
        /// <returns></returns>
        public bool UpdateCustomerType(int ProposalId, int UserId)
        {
            bool result = false;
            StringBuilder strSql = new StringBuilder();
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                  conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    strSql.Append("SELECT StuCustomerId FROM Proposal WHERE Id=@ProposalId");
                    param.Add("@ProposalId", ProposalId, dbType: DbType.Int32);
                    List<int> stuIds = conn.Query<int>(strSql.ToString(), param, tran).ToList();
                    if (stuIds.Count > 0)
                    {

                        strSql.Clear();
                        strSql.Append("update StuCustomer set ");
                        strSql.Append("CustomerType=@CustomerType");
                        strSql.Append(" where Id=@stuIds");
                      //  param.Add("@CustomerType", (int)CustomerType.ExistCustomer, dbType: DbType.Int32);
                        conn.Execute(strSql.ToString(), new {CustomerType=(int)CustomerType.ExistCustomer,stuIds=stuIds},tran);

                    }
                    tran.Commit();
                }
                catch(Exception ex)
                {
                    LogHelper.Log.WriteError("UpdateCustomerTypeList", ex);
                    tran.Rollback();
                    result = false;
                }

            }
            return result ;
        }

        #region 批量将潜在客户更新为已有客户
        /// <summary>
        /// 批量将潜在客户更新为已有客户
        /// </summary>
        /// <param name="TrainExamId">销售机会Id</param>
        /// <returns></returns>
        public bool UpdateCustomerTypeList(int TrainExamId)
        {
            bool result = true;
            StringBuilder strSql = new StringBuilder();
            var param = new DynamicParameters();

            List<int> Status = new List<int>();
            Status.Add((int)ProposalStatus.UnCommitted);        //未提交
            Status.Add((int)ProposalStatus.UnAudited);          //未审核
            Status.Add((int)ProposalStatus.Audited);            //已审核

            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    //获取分数大于合格分的用户
                    strSql.Clear();
                    param = new DynamicParameters();
                    strSql.Append("SELECT UserId FROM AssessmentResults WHERE TrainExamId = @TrainExamId AND SubjectiveResults + ObjectiveResults > TotalScore * 0.6");
                    param.Add("@TrainExamId", TrainExamId, dbType: DbType.Int32);
                    List<int> userIds = conn.Query<int>(strSql.ToString(), param, tran).ToList();

                    //根据销售机会Id和用户Id，获取要更新的潜在客户
                    if (userIds != null && userIds.Count > 0)
                    {
                        //foreach (int userId in userIds)
                        //{
                        //    strSql.Clear();
                        //    param = new DynamicParameters();
                        //    strSql.Append("SELECT StuCustomerId FROM Proposal WHERE TrainExamId = @TrainExamId and UserId = @UserId");
                        //    param.Add("@TrainExamId", TrainExamId, dbType: DbType.Int32);
                        //    param.Add("@UserId", userId, dbType: DbType.Int32);
                        //    int StuCustomerId = conn.Query<int>(strSql.ToString(), param, tran).FirstOrDefault();

                        //    //统计此用户的有效建议书
                        //    strSql.Clear();
                        //    param = new DynamicParameters();
                        //    strSql.Append("SELECT COUNT(1) FROM Proposal WHERE UserId = @UserId AND StuCustomerId = @StuCustomerId AND [Status] in @Status");
                        //    int count = conn.Query<int>(strSql.ToString(), new { UserId = userId, StuCustomerId = StuCustomerId, Status = Status }, tran).FirstOrDefault();


                        //    if (StuCustomerId != 0)
                        //    {
                        //        strSql.Clear();
                        //        param = new DynamicParameters();
                        //        strSql.Append("UPDATE StuCustomer SET CustomerType = @CustomerType, ProposalCount = @ProposalCount WHERE Id = @Id");
                        //        param.Add("@CustomerType", (int)CustomerType.ExistCustomer, dbType: DbType.Int32);
                        //        param.Add("@ProposalCount", count, dbType: DbType.Int32);
                        //        param.Add("@Id", StuCustomerId, dbType: DbType.Int32);
                        //        conn.Execute(strSql.ToString(), param, tran);
                        //    }
                        //}

                        strSql.Clear();
                        strSql.Append("SELECT StuCustomerId FROM Proposal WHERE TrainExamId = @TrainExamId and UserId in @UserId");
                        List<int> stuIds = conn.Query<int>(strSql.ToString(), new { TrainExamId = TrainExamId, UserId = userIds }, tran).ToList();

                        if (stuIds != null && stuIds.Count > 0)
                        {
                            strSql.Clear();
                            strSql.Append("UPDATE StuCustomer SET CustomerType = @CustomerType WHERE Id in @Id");
                            conn.Execute(strSql.ToString(), new { CustomerType = (int)CustomerType.ExistCustomer, Id = stuIds }, tran);
                        }

                    }

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("UpdateCustomerTypeList", ex);
                    tran.Rollback();
                    result = false;
                }
            }
            return result;
        }
        #endregion

        #region 根据客户Id，用户Id更新该客户拥有建议书数量
        /// <summary>
        /// 根据客户Id，用户Id更新该客户拥有建议书数量
        /// </summary>
        /// <param name="stuCustomerId">客户Id</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public bool UpdateCustomerProposalCount(int stuCustomerId, int userId)
        {
            List<int> Status = new List<int>();
            Status.Add((int)ProposalStatus.UnCommitted);        //未提交
            Status.Add((int)ProposalStatus.UnAudited);          //未审核
            Status.Add((int)ProposalStatus.Audited);            //已审核

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update StuCustomer set ProposalCount=");
            strSql.Append("(SELECT COUNT(1) FROM Proposal WHERE UserId = @UserId AND StuCustomerId = @StuCustomerId AND [Status] in @Status)");
            strSql.Append(" where Id = @StuCustomerId");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                result = conn.Execute(strSql.ToString(), new { UserId = userId, StuCustomerId = stuCustomerId, Status = Status });
            }
            return result > 0;
        }
        #endregion

    }
}

