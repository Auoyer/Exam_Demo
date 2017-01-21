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
    /// 数据访问类:Case
    /// </summary>
    public partial class CaseDAL
    {
        public CaseDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [Case] where Id=@Id ");

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
        /// <summary>
        /// 校验是否重复
        /// </summary>
        /// <param name="CaseId">案例主键</param>
        /// <param name="IDNum">身份证号</param>
        /// <returns>返回值为true:不存在重复</returns>
        public bool CheckRepeat(int CaseId, string IDNum)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [Case]");
            strSql.AppendFormat(" where IDNum='{0}' and Id!={1} and Status!=3 ", IDNum, CaseId);
            bool result = true;
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                int i = conn.Query<int>(strSql.ToString()).FirstOrDefault();
                if (i > 0)
                {
                    return false;
                }
            }
            return result;
        }

        #endregion

        #region  新增

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Case model)
        {
            int result = 0;
            var conn = DBHelper.CreateConnection();
            conn.Open();
            var tran = conn.BeginTransaction();


            try
            {
                StringBuilder strSql = new StringBuilder();
                var param = new DynamicParameters();

                #region 案例
                strSql.Append("insert into [Case](");
                strSql.Append("CollegeId,CustomerName,IDType,IDNum,FinancialTypeId,CustomerStory,UserId,CreateDate,Status,ViewStatus)");

                strSql.Append(" values (");
                strSql.Append("@CollegeId,@CustomerName,@IDType,@IDNum,@FinancialTypeId,@CustomerStory,@UserId,@CreateDate,@Status,0)");
                strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                param.Add("@CollegeId", model.CollegeId, dbType: DbType.Int32);
                param.Add("@CustomerName", model.CustomerName, dbType: DbType.String);
                param.Add("@IDType", model.IDType, dbType: DbType.Int32);
                param.Add("@IDNum", model.IDNum, dbType: DbType.String);
                param.Add("@FinancialTypeId", model.FinancialTypeId, dbType: DbType.Int32);
                param.Add("@CustomerStory", model.CustomerStory, dbType: DbType.String);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@CreateDate", model.CreateDate, dbType: DbType.DateTime);
                param.Add("@Status", model.Status, dbType: DbType.Int32);
                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(strSql.ToString(), param, tran);
                result = param.Get<int>("@returnid");
                #endregion

                #region 案例答案
                if (model.ExamPointAnswer != null && model.ExamPointAnswer.Count > 0)
                {
                    foreach (var item in model.ExamPointAnswer)
                    {
                        strSql.Clear();
                        param = new DynamicParameters();

                        strSql.Append("insert into ExamPointAnswer(");
                        strSql.Append("CaseId,ExamPointId,Answer)");
                        strSql.Append(" values (");
                        strSql.Append("@CaseId,@ExamPointId,@Answer)");

                        param.Add("@CaseId", result, dbType: DbType.Int32);
                        param.Add("@ExamPointId", item.ExamPointId, dbType: DbType.Int32);
                        param.Add("@Answer", item.Answer, dbType: DbType.String);

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
        public bool Update(Case model)
        {
            int result = 0;
            var conn = DBHelper.CreateConnection();
            conn.Open();
            var tran = conn.BeginTransaction();

            try
            {
                StringBuilder strSql = new StringBuilder();
                var param = new DynamicParameters();

                #region 案例
                strSql.Append("update [Case] set ");
                strSql.Append("CustomerName=@CustomerName, ");
                strSql.Append("IDNum=@IDNum, ");
                strSql.Append("FinancialTypeId=@FinancialTypeId, ");
                strSql.Append("CustomerStory=@CustomerStory ");
                strSql.Append("where Id=@Id ");

                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@CustomerName", model.CustomerName, dbType: DbType.String);
                param.Add("@IDNum", model.IDNum, dbType: DbType.String);
                param.Add("@FinancialTypeId", model.FinancialTypeId, dbType: DbType.Int32);
                param.Add("@CustomerStory", model.CustomerStory, dbType: DbType.String);
                result = conn.Execute(strSql.ToString(), param, tran);
                #endregion

                #region 案例答案
                if (model.ExamPointAnswer != null && model.ExamPointAnswer.Count > 0)
                {
                    foreach (var item in model.ExamPointAnswer)
                    {
                        strSql.Clear();
                        param = new DynamicParameters();

                        strSql.Append("update ExamPointAnswer set Answer=@Answer where CaseId=@CaseId and ExamPointId=@ExamPointId;");

                        param.Add("@CaseId", model.Id, dbType: DbType.Int32);
                        param.Add("@ExamPointId", item.ExamPointId, dbType: DbType.Int32);
                        param.Add("@Answer", item.Answer, dbType: DbType.String);

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
            int result = 0;
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    StringBuilder strSql = new StringBuilder();
                    var param = new DynamicParameters();
                    strSql.Append("delete from [ExamPointAnswer] ");
                    strSql.Append(" where CaseId=@Id ");
                    param.Add("@Id", Id, dbType: DbType.Int32);//答案
                    conn.Execute(strSql.ToString(), param, tran);

                    strSql.Clear();

                    strSql.Append("delete from [Case] ");
                    strSql.Append(" where Id=@Id ");
                    param.Add("@Id", Id, dbType: DbType.Int32);//删除案例
                    result = conn.Execute(strSql.ToString(), param, tran);

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
            }
            return result > 0;
        }

        #endregion

        #region  获取实体
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Case GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from [Case] where Id=@Id; ");
            strSql.Append("select * from [ExamPointAnswer] where CaseId=@Id; ");

            Case model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                using (var multi = conn.QueryMultiple(strSql.ToString(), param))
                {
                    model = multi.Read<Case>().FirstOrDefault();
                    model.ExamPointAnswer = multi.Read<ExamPointAnswer>().ToList();
                }
            }
            return model;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Case GetModel2(int FinancialTypeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from [Case] where Id=@FinancialTypeId; ");

            Case model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@FinancialTypeId", FinancialTypeId, dbType: DbType.Int32);
                using (var multi = conn.QueryMultiple(strSql.ToString(), param))
                {
                    model = multi.Read<Case>().FirstOrDefault();

                }
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Case> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [Case] ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<Case> list = new List<Case>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<Case>(strSql.ToString()).ToList();
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
                strSql.Append(" and Id=" + filter.Id.Value);
            }
            if (filter.UserId.HasValue)
            {
                strSql.Append(" and UserId=" + filter.UserId.Value);
            }
            if (filter.CollegeId.HasValue)
            {
                if (filter.BySchool)
                {
                    strSql.Append(" and CollegeId=" + filter.CollegeId.Value);
                }
                else
                {
                    // 超管显示所有,竞赛管理员按学校查看
                    if (filter.CollegeId != 0)
                    {
                        //查询内置案例+教师本身案例
                        strSql.AppendFormat(" and CollegeId in (0,{0})", filter.CollegeId.Value);
                    }
                }
            }
            if (filter.CaseStatus.HasValue)
            {
                // 超管显示所有,竞赛管理员按学校查看
                if (filter.CaseStatus == -1)
                {
                    // 启用和屏蔽的案例
                    strSql.AppendFormat(" and Status in (1,2)");
                }
                else
                {
                    strSql.AppendFormat(" and Status=" + filter.CaseStatus.Value);
                }
            }
            if (filter.FinancialTypeId.HasValue)
            {
                strSql.Append(" and FinancialTypeId=" + filter.FinancialTypeId.Value);
            }
            if (!string.IsNullOrEmpty(filter.KeyWords))
            {
                //过滤危险字段，如单引号等
                string key = filter.KeyWords.Replace("'", "''");
                //客户姓名，身份证号
                strSql.AppendFormat(" and (CustomerName like '%{0}%' or IDNum like '%{0}%')", key);
            }

            return strSql.ToString();
        }

        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetCasePageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "[Case]";
            model.PKey = "Id";
            model.Sort = "CreateDate DESC";
            model.Fields = "*";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion

        /// <summary>
        /// 屏蔽案例
        /// </summary>
        /// <param name="Id">案例Id</param>
        /// <param name="userId">用户Id</param>
        /// <returns>是否成功</returns>
        public bool Hidden(int Id, int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CaseHidden(");
            strSql.Append("CaseId,UserId)");
            strSql.Append(" values (");
            strSql.Append("@CaseId,@UserId)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@CaseId", Id, dbType: DbType.Int32);
                param.Add("@UserId", userId, dbType: DbType.String);
                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(strSql.ToString(), param);
                result = param.Get<int>("@returnid");
            }
            return result > 0;
        }

        /// <summary>
        /// 检查案列是否被用在未发布的销售机会/实训中，编辑和删除前需要判断
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public int CheckCaseByUsed(int userId, int status, int caseId)
        {
            int result = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select COUNT(*) from TrainExam where ");
            strSql.Append(" CaseId=@CaseId ");
            strSql.Append(" and UserId=@UserId ");
            strSql.Append(" and [Status]=@Status ");
            var param = new DynamicParameters();
            param.Add("@CaseId", caseId, dbType: DbType.Int32);
            param.Add("@UserId", userId, dbType: DbType.Int32);
            param.Add("@Status", status, dbType: DbType.Int32);
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();

            }
            return result;
        }

        #region 检查自定义案列是否被用在竞赛中
        /// <summary>
        /// 检查自定义案列是否被用在竞赛中
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public int CheckCaseInMatch(int status, int caseId)
        {
            int result = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select COUNT(*) from TrainExam where ");
            strSql.Append("CaseId=@CaseId ");
            if (status == -2)
            {
                strSql.Append("and [Status]<@Status ");
            }
            else
            {
                strSql.Append("and [Status]=@Status ");
            }
            var param = new DynamicParameters();
            param.Add("@CaseId", caseId, dbType: DbType.Int32);
            param.Add("@Status", -status, dbType: DbType.Int32);
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 修改案例状态（屏蔽，发布，删除）
        /// </summary>
        /// <param name="caseId">案例Id</param>
        /// <param name="type">操作类型</param>
        /// <returns>是否成功</returns>
        public bool ChangeCaseStatus(int caseId, int type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [Case] set ");
            strSql.Append("Status=@Status ");
            strSql.Append("where Id=@Id ");

            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                try
                {
                    conn.Open();
                    param.Add("@Id", caseId, dbType: DbType.Int32);
                    param.Add("@Status", type, dbType: DbType.Int32);

                    return conn.Execute(strSql.ToString(), param) > 0;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 修改案例查看状态
        /// </summary>
        /// <param name="caseId">案例Id</param>
        /// <returns>是否成功</returns>
        public bool ChangeCaseViewStatus(int caseId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [Case] set ");
            strSql.Append("ViewStatus=1 ");
            strSql.Append("where Id=@Id ");

            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                try
                {
                    conn.Open();
                    param.Add("@Id", caseId, dbType: DbType.Int32);
                    return conn.Execute(strSql.ToString(), param) > 0;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}

