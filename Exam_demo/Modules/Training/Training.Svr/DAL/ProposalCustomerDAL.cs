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
    /// 数据访问类:ProposalCustomer
    /// </summary>
    public partial class ProposalCustomerDAL
    {
        public ProposalCustomerDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ProposalCustomer where Id=@Id ");

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
        public int Add(ProposalCustomer model)
        {
            int result = 0;
            var conn = DBHelper.CreateConnection();
            conn.Open();
            var tran = conn.BeginTransaction();

            try
            {
                #region 新增建议书客户信息

                StringBuilder strSql = new StringBuilder();
                var param = new DynamicParameters();

                strSql.Append("insert into ProposalCustomer(");
                strSql.Append("ProposalId,CustomerName,PinYin,Age,IDType,IDNum,Tel,Phone,Email,Position,Company,Address)");
                strSql.Append(" values (");
                strSql.Append("@ProposalId,@CustomerName,@PinYin,@Age,@IDType,@IDNum,@Tel,@Phone,@Email,@Position,@Company,@Address)");
                strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@CustomerName", model.CustomerName, dbType: DbType.String);
                param.Add("@PinYin", model.PinYin, dbType: DbType.String);
                param.Add("@Age", model.Age, dbType: DbType.Int32);
                param.Add("@IDType", model.IDType, dbType: DbType.Int32);
                param.Add("@IDNum", model.IDNum, dbType: DbType.String);
                param.Add("@Tel", model.Tel, dbType: DbType.String);
                param.Add("@Phone", model.Phone, dbType: DbType.String);
                param.Add("@Email", model.Email, dbType: DbType.String);
                param.Add("@Position", model.Position, dbType: DbType.String);
                param.Add("@Company", model.Company, dbType: DbType.String);
                param.Add("@Address", model.Address, dbType: DbType.String);
                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(strSql.ToString(), param, tran);
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
                        param.Add("@Type", item.Type, dbType: DbType.Int32);
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
        public bool Update(ProposalCustomer model)
        {
            bool addResult = false;
            var conn = DBHelper.CreateConnection();
            conn.Open();
            var tran = conn.BeginTransaction();

            try
            {
                #region 修改建议书客户信息

                StringBuilder strSql = new StringBuilder();
                var param = new DynamicParameters();

                strSql.Append("update ProposalCustomer set ");
                strSql.Append("ProposalId=@ProposalId,");
                strSql.Append("CustomerName=@CustomerName,");
                strSql.Append("PinYin=@PinYin,");
                strSql.Append("Age=@Age,");
                strSql.Append("IDType=@IDType,");
                strSql.Append("IDNum=@IDNum,");
                strSql.Append("Tel=@Tel,");
                strSql.Append("Phone=@Phone,");
                strSql.Append("Email=@Email,");
                strSql.Append("Position=@Position,");
                strSql.Append("Company=@Company,");
                strSql.Append("Address=@Address");
                strSql.Append(" where Id=@Id ");

                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@CustomerName", model.CustomerName, dbType: DbType.String);
                param.Add("@PinYin", model.PinYin, dbType: DbType.String);
                param.Add("@Age", model.Age, dbType: DbType.Int32);
                param.Add("@IDType", model.IDType, dbType: DbType.Int32);
                param.Add("@IDNum", model.IDNum, dbType: DbType.String);
                param.Add("@Tel", model.Tel, dbType: DbType.String);
                param.Add("@Phone", model.Phone, dbType: DbType.String);
                param.Add("@Email", model.Email, dbType: DbType.String);
                param.Add("@Position", model.Position, dbType: DbType.String);
                param.Add("@Company", model.Company, dbType: DbType.String);
                param.Add("@Address", model.Address, dbType: DbType.String);
                conn.Execute(strSql.ToString(), param, tran);

                #endregion

                #region 删除建议书客户家属信息
                strSql.Clear();
                param = new DynamicParameters();
                strSql.Append(" delete ProposalCustomerDetail ");
                strSql.Append(" where  ProposalId=@ProposalId and Type=@Type");
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@Type", (int)ProposalCustDetailType.CustomerFaimly, dbType: DbType.Int32);
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
                        param.Add("@Type", r.Type, dbType: DbType.Int32);
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
            strSql.Append("delete from ProposalCustomer ");
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
        public ProposalCustomer GetModel(int ProposalId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from ProposalCustomer where ProposalId=@ProposalId; ");
            strSql.Append("select * from ProposalCustomerDetail where ProposalId=@ProposalId and [Type]=@Type; ");

            ProposalCustomer model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", ProposalId, dbType: DbType.Int32);
                param.Add("@Type", (int)ProposalCustDetailType.CustomerFaimly, dbType: DbType.Int32);
                using (var multi = conn.QueryMultiple(strSql.ToString(), param))
                {
                    model = multi.Read<ProposalCustomer>().FirstOrDefault();
                    model.ProposalCustomerDetailList = multi.Read<ProposalCustomerDetail>().ToList();
                }
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ProposalCustomer> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,CustomerName,PinYin,Age,IDType,IDNum,Tel,Phone,Email,Position,Company,Address ");
            strSql.Append(" FROM ProposalCustomer ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<ProposalCustomer> list = new List<ProposalCustomer>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<ProposalCustomer>(strSql.ToString()).ToList();
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
            if (filter.ProposalId.HasValue)
            {
                strSql.Append(" and ProposalId=" + filter.ProposalId);
            }
            if (!string.IsNullOrEmpty(filter.ProposalName))
            {
                strSql.Append(" and ProposalId=" + filter.ProposalId);
            }
            return strSql.ToString();
        }

        /// <summary>
        /// 根据CustomFilter获取GetStrWhereCopy语句
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private string GetStrWhereCopy(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(filter.ProposalName))
            {
                strSql.AppendFormat(" and p.ProposalName='{0}'", filter.ProposalName);
            }
            if (!string.IsNullOrEmpty(filter.CustomerName))
            {
                strSql.AppendFormat(" and pc.CustomerName='{0}'", filter.CustomerName);
            }
            if (filter.Status.HasValue && filter.Status != 0)
            {
                strSql.AppendFormat(" and p.Status ={0}",filter.Status);
            }
            else
            {
                strSql.Append(" and p.Status between 1 and 3");
            }
            if (filter.UserId!=null&&filter.UserId!=0)
            {
                strSql.AppendFormat(" and sc.UserId={0}", filter.UserId);
              //  strSql.AppendFormat(" and p.UserId={0}", filter.UserId);
            }
            //FinancialTypeId=30为选中--，表示搜索自主新增的建议书
            if (filter.FinancialTypeId.HasValue && filter.FinancialTypeId != 0&&filter.FinancialTypeId!=-1)
            {
                strSql.Append(" and e.FinancialTypeId="+filter.FinancialTypeId);
            }
            if (filter.FinancialTypeId == -1)
            {
                strSql.Append(" and sc.Source=2");
 
            }
            if (!string.IsNullOrEmpty(filter.KeyWords))
            {
                //过滤危险字段，如单引号等
                string key = filter.KeyWords.Replace("'", "''");
                //建议书名称/客户姓名/建议书编号/身份证号
                strSql.AppendFormat(" and (p.ProposalName like '%{0}%' or pc.CustomerName like '%{0}%' or p.ProposalNum like '%{0}%' or pc.IDNum like '%{0}%')", key);
            }
            strSql.AppendFormat(" and p.ProposalNum is not null and sc.Status <> 4");
            return strSql.ToString();
        }
        #endregion

        /// <summary>
        /// 自主实训过滤
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private string GetStrSelfTrainWhere(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (filter.UserId!=0&&filter.UserId!=null)
            {
                strSql.Append(" and p.UserId=" + filter.UserId);
            }
            strSql.AppendFormat(" and p.TrainExamId={0}", 0);

            return strSql.ToString();
        }


        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetProposalCustomerPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "ProposalCustomer";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,ProposalId,CustomerName,PinYin,Age,IDType,IDNum,Tel,Phone,Email,Position,Company,Address";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        /// <summary>
        /// 规划理财分页参数
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public PageModel GetFinancialPlanningPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "Proposal as p left join StuCustomer as sc on sc.Id =p.StuCustomerId join ProposalCustomer as pc on p.Id=pc.ProposalId left join TrainExam as t on p.TrainExamId =t.id left join ExamCase e on t.Id=e.TrainExamId";
            model.PKey = "sc.Id";
            if (string.IsNullOrEmpty(filter.SortName))
            {
                model.Sort = "p.UpdateDate desc";
            }
            else
            {
                model.Sort = filter.SortName + (filter.SortWay ? " desc" : " asc");
            }
            model.Fields = "p.Id,p.ProposalNum,p.ProposalName,p.UserId,e.TrainExamId,pc.CustomerName,pc.IDNum,e.FinancialTypeId,p.CreateDate,p.UpdateDate,p.Status,p.StuCustomerId,sc.CustomerType,t.EndDate";
            model.Filter = GetStrWhereCopy(filter);
            return model;
        }
        
        #endregion

       /// <summary>
       /// 自主练习查询
       /// </summary>
       /// <param name="filter"></param>
       /// <returns></returns>
        public PageModel GetFinancialPlanningPageSelfTrain(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "Proposal as p left join StuCustomer as sc on p.Id=sc.ProposalId";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "p.Id,sc.CustomerName,sc.IDNum,p.UpdateDate";
            model.Filter = GetStrSelfTrainWhere(filter);
            return model;
        }


    }
}

