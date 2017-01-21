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
    /// 数据访问类:DistributionOfProperty
    /// </summary>
    public partial class DistributionOfPropertyDAL
    {
        public DistributionOfPropertyDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from DistributionOfProperty where Id=@Id ");

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
        public int Add(DistributionOfProperty model)
        {

            int result = 0;
            var conn = DBHelper.CreateConnection();
            conn.Open();
            var tran = conn.BeginTransaction();
            //新增财产分配表
            try
            {
                #region 新增新增财产分配表
                StringBuilder strSql = new StringBuilder();
                var param = new DynamicParameters();

                strSql.Append("insert into DistributionOfProperty(");
                strSql.Append("ProposalId,CustomerSex,Address,Position,FamilyNum,SituationAnalysis,PlanTool,PlanAnalysis)");

                strSql.Append(" values (");
                strSql.Append("@ProposalId,@CustomerSex,@Address,@Position,@FamilyNum,@SituationAnalysis,@PlanTool,@PlanAnalysis)");
                strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

 
                    param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                    param.Add("@CustomerSex", model.CustomerSex, dbType: DbType.Int32);
                    param.Add("@Address", model.Address, dbType: DbType.String);
                    param.Add("@Position", model.Position, dbType: DbType.String);
                    param.Add("@FamilyNum", model.FamilyNum, dbType: DbType.Int32);
                    param.Add("@SituationAnalysis", model.SituationAnalysis, dbType: DbType.String);
                    param.Add("@PlanTool", model.PlanTool, dbType: DbType.Int32);
                    param.Add("@PlanAnalysis", model.PlanAnalysis, dbType: DbType.String);
                    param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    conn.Execute(strSql.ToString(), param,tran);
                    result = param.Get<int>("@returnid");
                #endregion

                    #region 新增建议书客户家属信息
                    if (model.ProposalCustomerDetailList != null && model.ProposalCustomerDetailList.Count > 0)
                    {
                        foreach (var item in model.ProposalCustomerDetailList)
                        {
                            strSql.Clear();
                            param = new DynamicParameters();

                            strSql.Append("insert into ProposalCustomerDetail(");
                            strSql.Append("ProposalId,Type,DependentName,Age,Relation,InCome)");
                            strSql.Append(" values (");
                            strSql.Append("@ProposalId,@Type,@DependentName,@Age,@Relation,@InCome)");
                            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                            param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                            param.Add("@Type", (int)ProposalCustDetailType.FinancialFaimly, dbType: DbType.Int32);
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
                LogHelper.Log.WriteError("Proposal Add", ex);
                tran.Rollback();
            }
            finally
            {
                if (tran != null)
                    tran.Dispose();
                if (conn != null)
                    conn.Dispose();
            };
            return result;
        }
        #endregion

        #region  更新
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DistributionOfProperty model)
        {

            bool addResult = false;
            var conn = DBHelper.CreateConnection();
            conn.Open();
            var tran = conn.BeginTransaction();
            try
            {

                #region 新增新增财产分配表
                StringBuilder strSql = new StringBuilder();
                var param = new DynamicParameters();
                strSql.Append("update DistributionOfProperty set ");
                strSql.Append("ProposalId=@ProposalId,");
                strSql.Append("CustomerSex=@CustomerSex,");
                strSql.Append("Address=@Address,");
                strSql.Append("Position=@Position,");
                strSql.Append("FamilyNum=@FamilyNum,");
                strSql.Append("SituationAnalysis=@SituationAnalysis,");
                strSql.Append("PlanTool=@PlanTool,");
                strSql.Append("PlanAnalysis=@PlanAnalysis");
                strSql.Append(" where Id=@Id ");



                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@CustomerSex", model.CustomerSex, dbType: DbType.Int32);
                param.Add("@Address", model.Address, dbType: DbType.String);
                param.Add("@Position", model.Position, dbType: DbType.String);
                param.Add("@FamilyNum", model.FamilyNum, dbType: DbType.Int32);
                param.Add("@SituationAnalysis", model.SituationAnalysis, dbType: DbType.String);
                param.Add("@PlanTool", model.PlanTool, dbType: DbType.Int32);
                param.Add("@PlanAnalysis", model.PlanAnalysis, dbType: DbType.String);
                conn.Execute(strSql.ToString(), param, tran);
                #endregion

                #region 删除建议书客户家属信息
                strSql.Clear();
                param = new DynamicParameters();
                strSql.Append(" delete ProposalCustomerDetail ");
                strSql.Append(" where  ProposalId=@ProposalId and Type=@Type");
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@Type", (int)ProposalCustDetailType.FinancialFaimly, dbType: DbType.Int32);
                conn.Execute(strSql.ToString(), param, tran);
                #endregion


                #region 新增建议书客户家属信息

                if (model.ProposalCustomerDetailList != null && model.ProposalCustomerDetailList.Count > 0)
                {
                    model.ProposalCustomerDetailList.ForEach(r =>
                    {
                        strSql.Clear();
                        param = new DynamicParameters();

                        strSql.Append("insert into ProposalCustomerDetail(");
                        strSql.Append("ProposalId,Type,DependentName,Age,Relation,InCome)");
                        strSql.Append(" values (");
                        strSql.Append("@ProposalId,@Type,@DependentName,@Age,@Relation,@InCome)");
                        strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                        param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                        param.Add("@Type", (int)ProposalCustDetailType.FinancialFaimly, dbType: DbType.Int32);
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

        #endregion

        #region  删除
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from DistributionOfProperty ");
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
        public DistributionOfProperty GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,CustomerSex,Address,Position,FamilyNum,SituationAnalysis,PlanTool,PlanAnalysis from DistributionOfProperty ");
            strSql.Append(" where Id=@Id ");

            DistributionOfProperty model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<DistributionOfProperty>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }


        /// <summary>
        /// 得到一个对象实体--根建议书ID
        /// </summary>
        public DistributionOfProperty GetModelByProposalId(int ProposalId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from DistributionOfProperty where ProposalId=@ProposalId; ");
            strSql.Append("select * from ProposalCustomerDetail where ProposalId=@ProposalId and [Type]=@Type; ");
            DistributionOfProperty model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", ProposalId, dbType: DbType.Int32);
                param.Add("@Type", (int)ProposalCustDetailType.FinancialFaimly, dbType: DbType.Int32);
                using (var multi = conn.QueryMultiple(strSql.ToString(), param))
                {
                    model = multi.Read<DistributionOfProperty>().FirstOrDefault();
                    if (model != null)
                    {
                        model.ProposalCustomerDetailList = multi.Read<ProposalCustomerDetail>().ToList();
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
        public List<DistributionOfProperty> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,CustomerSex,Address,Position,FamilyNum,SituationAnalysis,PlanTool,PlanAnalysis ");
            strSql.Append(" FROM DistributionOfProperty ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<DistributionOfProperty> list = new List<DistributionOfProperty>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<DistributionOfProperty>(strSql.ToString()).ToList();
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
        public PageModel GetDistributionOfPropertyPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "DistributionOfProperty";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,ProposalId,CustomerSex,Address,Position,FamilyNum,SituationAnalysis,PlanTool,PlanAnalysis";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion




    }
}

