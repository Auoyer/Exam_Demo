using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using Utils;
using Training.API;

namespace Training.Svr
{
    /// <summary>
    /// 数据访问类:Proposal
    /// </summary>
    public partial class ProposalDAL
    {
        public ProposalDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Proposal where Id=@Id ");

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
        public int Add(Proposal model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Proposal(");
            strSql.Append("TrainExamId,UserId,ProposalNum,ProposalName,Status,CreateDate,UpdateDate,StuCustomerId)");

            strSql.Append(" values (");
            strSql.Append("@TrainExamId,@UserId,@ProposalNum,@ProposalName,@Status,@CreateDate,@UpdateDate,@StuCustomerId)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@TrainExamId", model.TrainExamId, dbType: DbType.Int32);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@ProposalNum", model.ProposalNum, dbType: DbType.String);
                param.Add("@ProposalName", model.ProposalName, dbType: DbType.String);
                param.Add("@Status", model.Status, dbType: DbType.Int32);
                param.Add("@CreateDate", model.CreateDate, dbType: DbType.DateTime);
                param.Add("@UpdateDate", model.UpdateDate, dbType: DbType.DateTime);
                param.Add("@StuCustomerId", model.StuCustomerId, dbType: DbType.Int32);
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
        public bool Update(Proposal model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Proposal set ");
            strSql.Append("TrainExamId=@TrainExamId,");
            strSql.Append("UserId=@UserId,");
            strSql.Append("ProposalNum=@ProposalNum,");
            strSql.Append("ProposalName=@ProposalName,");
            strSql.Append("Status=@Status,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("UpdateDate=@UpdateDate,");
            strSql.Append("StuCustomerId=@StuCustomerId");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@TrainExamId", model.TrainExamId, dbType: DbType.Int32);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@ProposalNum", model.ProposalNum, dbType: DbType.String);
                param.Add("@ProposalName", model.ProposalName, dbType: DbType.String);
                param.Add("@Status", model.Status, dbType: DbType.Int32);
                param.Add("@CreateDate", model.CreateDate, dbType: DbType.DateTime);
                param.Add("@UpdateDate", model.UpdateDate, dbType: DbType.DateTime);
                param.Add("@StuCustomerId", model.StuCustomerId, dbType: DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        /// <summary>
        /// 修改建议书目前的状态
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public bool UpdateStatus(int Id, int Status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Proposal set ");
            strSql.Append("Status=@Status");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                param.Add("@Status", Status, dbType: DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        /// <summary>
        /// 更新建议书时间
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <param name="UpdateDate">更新时间</param>
        /// <returns></returns>
        public bool UpdateProposalDate(int Id, DateTime UpdateDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Proposal set ");
            strSql.Append("UpdateDate=@UpdateDate");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                param.Add("@UpdateDate", UpdateDate, dbType: DbType.DateTime);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }
        #endregion

        #region  删除
        /// <summary>
        /// 删除建议书(伪删)
        /// 会同步修改以下内容：
        /// 1.客户信息中的建议书状态与建议书数量
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public bool DeleteProposal(int Id)
        {
            List<int> Status = new List<int>();
            Status.Add((int)ProposalStatus.UnCommitted);
            Status.Add((int)ProposalStatus.UnAudited);
            Status.Add((int)ProposalStatus.Audited);

            bool result = true;
            StringBuilder strSql = new StringBuilder();
            var param = new DynamicParameters();

            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    strSql.Clear();
                    param = new DynamicParameters();
                    strSql.Append("select StuCustomerId from Proposal where Id=@Id");
                    param.Add("@Id", Id, dbType: DbType.Int32);
                    int StuCustomerId = conn.Query<int>(strSql.ToString(), param, tran).FirstOrDefault();

                    strSql.Clear();
                    param = new DynamicParameters();
                    strSql.Append("update Proposal set [Status]=@Status where Id=@Id");
                    param.Add("@Status", (int)ProposalStatus.deletes, dbType: DbType.Int32);
                    param.Add("@Id", Id, dbType: DbType.Int32);
                    conn.Execute(strSql.ToString(), param, tran);

                    if (StuCustomerId != 0)
                    {
                        strSql.Clear();
                        param = new DynamicParameters();
                        strSql.Append("select ProposalCount,[Status],ProposalId from StuCustomer where Id = @Id");
                        param.Add("@Id", StuCustomerId, dbType: DbType.Int32);
                        StuCustomer model = conn.Query<StuCustomer>(strSql.ToString(), param, tran).FirstOrDefault();

                        if (model != null)
                        {
                            strSql.Clear();
                            param = new DynamicParameters();

                            if (model.ProposalId == Id)
                            {
                                strSql.Append("update StuCustomer set [Status]=@Status,ProposalId=0");
                                strSql.Append(",ProposalCount=(select count(1) from Proposal where StuCustomerId=@Id and [Status] in @ProposalStatus) ");
                                strSql.Append(" where Id=@Id");

                                var obj = new
                                {
                                    Status = (int)StuCustomerProposalStatus.Add,
                                    Id = StuCustomerId,
                                    ProposalStatus = Status
                                };
                                conn.Execute(strSql.ToString(), obj, tran);
                            }
                            else
                            {
                                strSql.Append("update StuCustomer set ");
                                strSql.Append("ProposalCount=(select count(1) from Proposal where StuCustomerId=@Id and [Status] in @ProposalStatus) ");
                                strSql.Append(" where Id=@Id");

                                var obj = new
                                {
                                    Id = StuCustomerId,
                                    ProposalStatus = Status
                                };
                                conn.Execute(strSql.ToString(), obj, tran);
                            }
                        }
                    }

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("RemoveProposal", ex);
                    tran.Rollback();
                    result = false;
                }
            }
            return result;
        }

        /// <summary>
        /// 删除对应所有的建议书Id数据表
        /// </summary>
        public bool DeleteAllProposalId(int Id)
        {

            bool result = false;
            var conn = DBHelper.CreateConnection();
            conn.Open();
            var tran = conn.BeginTransaction();

            try
            {
                StringBuilder strSql = new StringBuilder();
                var param = new DynamicParameters();

                #region 建议书
                strSql.Clear();
                param = new DynamicParameters();
                strSql.Append("delete from Proposal ");
                strSql.Append(" where Id=@Id ");

                param.Add("@Id", Id, dbType: DbType.Int32);
                conn.Execute(strSql.ToString(), param, tran);
                #endregion

                #region 建议书客户信息              
                strSql.Clear();
                param = new DynamicParameters();
                strSql.Append("delete from ProposalCustomer ");
                strSql.Append(" where ProposalId=@ProposalId ");

                param.Add("@ProposalId", Id, dbType: DbType.Int32);
                conn.Execute(strSql.ToString(), param, tran);
                #endregion

                #region 建议书客户详细信息
                strSql.Clear();
                param = new DynamicParameters();
                strSql.Append("delete from ProposalCustomerDetail ");
                strSql.Append(" where ProposalId=@ProposalId ");

                param.Add("@ProposalId", Id, dbType: DbType.Int32);
                conn.Execute(strSql.ToString(), param, tran);
                #endregion

                #region 风险评测
                strSql.Clear();
                param = new DynamicParameters();
                strSql.Append("delete from RiskIndex ");
                strSql.Append(" where ProposalId=@ProposalId ");

                param.Add("@ProposalId", Id, dbType: DbType.Int32);
                conn.Execute(strSql.ToString(), param, tran);
                #endregion

                #region 资产负债表
                strSql.Clear();
                param = new DynamicParameters();
                strSql.Append("delete from Liability ");
                strSql.Append(" where ProposalId=@ProposalId ");

                param.Add("@ProposalId", Id, dbType: DbType.Int32);
                conn.Execute(strSql.ToString(), param, tran);
                #endregion

                #region 收支储蓄表
                strSql.Clear();
                param = new DynamicParameters();
                strSql.Append("delete from IncomeAndExpenses ");
                strSql.Append(" where ProposalId=@ProposalId ");

                param.Add("@ProposalId", Id, dbType: DbType.Int32);
                conn.Execute(strSql.ToString(), param, tran);
                #endregion

                #region 现金规划
                strSql.Clear();
                param = new DynamicParameters();
                strSql.Append("delete from CashPlan ");
                strSql.Append(" where ProposalId=@ProposalId ");

                param.Add("@ProposalId", Id, dbType: DbType.Int32);
                conn.Execute(strSql.ToString(), param, tran);
                #endregion

                #region 现金流量
                strSql.Clear();
                param = new DynamicParameters();
                strSql.Append("delete from CashFlow ");
                strSql.Append(" where ProposalId=@ProposalId ");

                param.Add("@ProposalId", Id, dbType: DbType.Int32);
                conn.Execute(strSql.ToString(), param, tran);
                #endregion

                #region 财务比例分析
                strSql.Clear();
                param = new DynamicParameters();
                strSql.Append("delete from FinancialRatios ");
                strSql.Append(" where ProposalId=@ProposalId ");

                param.Add("@ProposalId", Id, dbType: DbType.Int32);
                conn.Execute(strSql.ToString(), param, tran);
                #endregion

                #region 教育规划信息
                strSql.Clear();
                param = new DynamicParameters();
                strSql.Append("delete from LifeEducationPlan ");
                strSql.Append(" where ProposalId=@ProposalId ");

                param.Add("@ProposalId", Id, dbType: DbType.Int32);
                conn.Execute(strSql.ToString(), param, tran);
                #endregion

                #region 教育规划详细信息
                strSql.Clear();
                param = new DynamicParameters();
                strSql.Append("delete from LifeEducationPlanDetail ");
                strSql.Append(" where ProposalId=@ProposalId ");

                param.Add("@ProposalId", Id, dbType: DbType.Int32);
                conn.Execute(strSql.ToString(), param, tran);
                #endregion

                #region 消费规划
                strSql.Clear();
                param = new DynamicParameters();
                strSql.Append("delete from ConsumptionPlan ");
                strSql.Append(" where ProposalId=@ProposalId ");

                param.Add("@ProposalId", Id, dbType: DbType.Int32);
                conn.Execute(strSql.ToString(), param, tran);
                #endregion

                #region 创业规划
                strSql.Clear();
                param = new DynamicParameters();
                strSql.Append("delete from StartAnUndertakingPlan ");
                strSql.Append(" where ProposalId=@ProposalId ");

                param.Add("@ProposalId", Id, dbType: DbType.Int32);
                conn.Execute(strSql.ToString(), param, tran);
                #endregion

                #region 退休规划
                strSql.Clear();
                param = new DynamicParameters();
                strSql.Append("delete from RetirementPlan ");
                strSql.Append(" where ProposalId=@ProposalId ");

                param.Add("@ProposalId", Id, dbType: DbType.Int32);
                conn.Execute(strSql.ToString(), param, tran);
                #endregion

                #region 保险规划
                strSql.Clear();
                param = new DynamicParameters();
                strSql.Append("delete from InsurancePlan ");
                strSql.Append(" where ProposalId=@ProposalId ");

                param.Add("@ProposalId", Id, dbType: DbType.Int32);
                conn.Execute(strSql.ToString(), param, tran);
                #endregion

                #region 投资规划
                strSql.Clear();
                param = new DynamicParameters();
                strSql.Append("delete from InvestmentPlan ");
                strSql.Append(" where ProposalId=@ProposalId ");

                param.Add("@ProposalId", Id, dbType: DbType.Int32);
                conn.Execute(strSql.ToString(), param, tran);
                #endregion

                #region 投资规划产品
                strSql.Clear();
                param = new DynamicParameters();
                strSql.Append("delete from InvestmentPlanProduct ");
                strSql.Append(" where ProposalId=@ProposalId ");

                param.Add("@ProposalId", Id, dbType: DbType.Int32);
                conn.Execute(strSql.ToString(), param, tran);
                #endregion

                #region 税收规划
                strSql.Clear();
                param = new DynamicParameters();
                strSql.Append("delete from TaxPlan ");
                strSql.Append(" where ProposalId=@ProposalId ");

                param.Add("@ProposalId", Id, dbType: DbType.Int32);
                conn.Execute(strSql.ToString(), param, tran);
                #endregion

                #region 财产分配
                strSql.Clear();
                param = new DynamicParameters();
                strSql.Append("delete from DistributionOfProperty ");
                strSql.Append(" where ProposalId=@ProposalId ");

                param.Add("@ProposalId", Id, dbType: DbType.Int32);
                conn.Execute(strSql.ToString(), param, tran);
                #endregion

                #region 财产传承
                strSql.Clear();
                param = new DynamicParameters();
                strSql.Append("delete from Heritage ");
                strSql.Append(" where ProposalId=@ProposalId ");

                param.Add("@ProposalId", Id, dbType: DbType.Int32);
                conn.Execute(strSql.ToString(), param, tran);
                #endregion

                result = true;
                tran.Commit();
            }
            catch (Exception ex)
            {
                result = false;
                LogHelper.Log.WriteError("[回滚]删除所以对应的建议书Id表", ex);
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

        /// <summary>
        /// 根据建议书Id集合移除所有建议书
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool RemoveProposal(List<int> ids)
        {
            bool result = true;
            StringBuilder strSql = new StringBuilder();
            var param = new DynamicParameters();

            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
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

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("RemoveProposal", ex);
                    tran.Rollback();
                    result = false;
                }
            }
            return result;
        }
        #endregion

        #region 统计
        /// <summary>
        /// 根据客户Id统计有效（未提交、未审核、已审核）的建议书数量
        /// </summary>
        /// <param name="StuCustomerId">客户Id</param>
        /// <returns></returns>
        public int CountProposal(int StuCustomerId)
        {
            List<int> Status = new List<int>();
            Status.Add((int)ProposalStatus.UnCommitted);
            Status.Add((int)ProposalStatus.UnAudited);
            Status.Add((int)ProposalStatus.Audited);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Proposal where StuCustomerId=@StuCustomerId and [Status] in @Status ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                result = conn.Query<int>(strSql.ToString(), new { StuCustomerId = StuCustomerId, Status = Status }).FirstOrDefault();
            }
            return result;
        }

        #endregion

        #region  获取实体
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Proposal GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,TrainExamId,UserId,ProposalNum,ProposalName,Status,CreateDate,UpdateDate,StuCustomerId from Proposal ");
            strSql.Append(" where Id=@Id ");

            Proposal model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<Proposal>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Proposal GetModel(CustomFilter filter)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from Proposal ");
            strSql.Append(" where 1=1 ");
            if (filter.UserId.HasValue)
            {
                strSql.AppendFormat(" and UserId={0} ",filter.UserId.Value);
            }
            if (filter.TrainExamId.HasValue)
            {
                strSql.AppendFormat(" and TrainExamId={0} ",filter.TrainExamId.Value);
            }
            Proposal model = null; 
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open(); 
                model = conn.Query<Proposal>(strSql.ToString()).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Proposal> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,TrainExamId,UserId,ProposalNum,ProposalName,Status,CreateDate,UpdateDate,StuCustomerId ");
            strSql.Append(" FROM Proposal ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<Proposal> list = new List<Proposal>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<Proposal>(strSql.ToString()).ToList();
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
        public PageModel GetProposalPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "Proposal";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,TrainExamId,UserId,ProposalNum,ProposalName,Status,CreateDate,UpdateDate,StuCustomerId";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion
    }
}

