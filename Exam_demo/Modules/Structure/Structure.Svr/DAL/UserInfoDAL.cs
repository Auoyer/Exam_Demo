using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using Structure.API;
using Utils;

namespace Structure.Svr
{
    /// <summary>
    /// ���ݷ�����:UserInfo
    /// </summary>
    public partial class UserInfoDAL
    {
        #region  �Ƿ����
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UserInfo where Id=@Id ");

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

        #region  ����

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(UserInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UserInfo(");
            strSql.Append("UserName,SchoolNumber,Sex,Phone,Email,Address,Status,RoleId,CreateTime,ModifyTime)");

            strSql.Append(" values (");
            strSql.Append("@UserName,@SchoolNumber,@Sex,@Phone,@Email,@Address,@Status,@RoleId,@CreateTime,@ModifyTime)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;

            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@UserName", model.UserName, dbType: DbType.String);
                //param.Add("@SchoolNumber", model.SchoolNumber, dbType: DbType.String);
                param.Add("@Sex", model.Sex, dbType: DbType.Int32);
                param.Add("@Phone", model.Phone, dbType: DbType.String);
                param.Add("@Email", model.Email, dbType: DbType.String);
                param.Add("@Address", model.Address, dbType: DbType.String);
                param.Add("@Status", model.Status, dbType: DbType.Int32);
                param.Add("@RoleId", model.RoleId, dbType: DbType.Int32);
                param.Add("@CreateTime", model.CreateTime, dbType: DbType.DateTime);
                param.Add("@ModifyTime", model.ModifyTime, dbType: DbType.DateTime);
                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);


                conn.Execute(strSql.ToString(), param);
                result = param.Get<int>("@returnid");
            }
            return result;
        }
        #endregion

        #region  ����
        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(UserInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UserInfo set ");
            strSql.Append("UserName=@UserName,");
            strSql.Append("Email=@Email,");
            strSql.Append("Sex=@Sex,");
            strSql.Append("Phone=@Phone,");
            strSql.Append("DepartmentName=@DepartmentName,");
            strSql.Append("ProvinceCode=@ProvinceCode,");
            strSql.Append("IDCard=@IDCard,");
            strSql.Append("CityCode=@CityCode,");
            strSql.Append("CollegeName=@CollegeName,");
            strSql.Append("IsCreate=@IsCreate,");

            if (model.AvailabilityDate != DateTime.MinValue)
                strSql.Append("AvailabilityDate=@AvailabilityDate,");
            if (model.ExpiryDate != DateTime.MinValue)
                strSql.Append("ExpiryDate=@ExpiryDate,");
            strSql.Append("UserType=@UserType");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@UserName", model.UserName, dbType: DbType.String);
                param.Add("@Sex", model.Sex, dbType: DbType.Int32);
                param.Add("@Phone", model.Phone, dbType: DbType.String);
                param.Add("@Email", model.Email, dbType: DbType.String);
                param.Add("@DepartmentName", model.DepartmentName, dbType: DbType.String);
                param.Add("@ProvinceCode", model.ProvinceCode, dbType: DbType.Int32);
                param.Add("@CityCode", model.CityCode, dbType: DbType.Int32);
                param.Add("@IDCard", model.IDCard, dbType: DbType.String);
                param.Add("@CollegeName", model.CollegeName, dbType: DbType.String);

                if (model.AvailabilityDate != DateTime.MinValue)
                    param.Add("@AvailabilityDate", model.AvailabilityDate, dbType: DbType.DateTime);
                if (model.ExpiryDate != DateTime.MinValue)
                    param.Add("@ExpiryDate", model.ExpiryDate, dbType: DbType.DateTime);

                param.Add("@IsCreate", model.IsCreate, dbType: DbType.Int32);
                param.Add("@UserType", model.UserType, dbType: DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        #endregion

        #region  ɾ��
        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UserInfo ");
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

        #region  ��ȡʵ��
        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public UserInfo GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from UserInfo ");
            strSql.Append(" where Id=@Id ");

            UserInfo model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<UserInfo>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  ���ݲ�ѯ������ȡ�б�
        /// <summary>
        /// ��������б�
        /// </summary>
        public List<UserInfo> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            var param = new DynamicParameters();

            strSql.Append("select Account.AccountNo,UserInfo.* ");
            strSql.Append(" FROM UserInfo, Account");
            strSql.Append(" where UserInfo.id=Account.UserId ");
            strSql.Append(GetStrWhere(filter));
            strSql.Append(" order by " + GetStrSort(filter));

            List<UserInfo> list = new List<UserInfo>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<UserInfo>(strSql.ToString(), param).ToList();
            }
            return list;
        }

        /// <summary>
        /// ����CustomFilter��ȡwhere���
        /// </summary>
        private string GetStrWhere(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (filter.CollegeId != 0)
                strSql.Append(" and UserInfo.collegeId=" + filter.CollegeId);            // ����ѧУId�������ݸ���
            if (filter.Id.HasValue)
            {
                strSql.AppendFormat(" and UserInfo.Id={0}", filter.Id);
            }
            //if (filter.IdList != null && filter.IdList.Count > 0)
            if (filter.IdList != null)
            {
                strSql.AppendFormat(" and UserInfo.Id in('{0}')", string.Join("','", filter.IdList));
            }
            if (filter.StatusId.HasValue)
            {
                strSql.AppendFormat(" and UserInfo.Status={0}", filter.StatusId);
            }
            if (filter.TypeId.HasValue)
            {
                strSql.AppendFormat(" and UserInfo.RoleId={0}", filter.TypeId);
            }
            if (!string.IsNullOrWhiteSpace(filter.KeyWord))
            {
                if (filter.KeyWord1Ex)
                {
                    strSql.AppendFormat(" and ( UserInfo.UserName like '%{0}%' or UserInfo.SchoolNumber like '%{0}%' )", filter.KeyWord.Replace("'", "''"));
                }
                else
                {
                    // ��ѯģ��ƥ���˺ţ�������ѧУ���ֻ�����
                    strSql.AppendFormat(" and (UserInfo.UserName like '%{0}%' or UserInfo.IDCard like '%{0}%' or Account.AccountNo like '%{0}%' or UserInfo.CollegeName like '%{0}%' or UserInfo.Phone like '%{0}%')", filter.KeyWord.Replace("'", "''"));
                }
            }
            if (!string.IsNullOrWhiteSpace(filter.KeyWord2))
            {
                strSql.AppendFormat(" and UserInfo.SchoolNumber like '%{0}%'", filter.KeyWord2.Replace("'", "''"));
            }
            //if (filter.IdList != null && filter.IdList.Count > 0)
            //{
            //    string StrValue = string.Empty;
            //    foreach (var item in filter.IdList)
            //    {
            //        StrValue += item + ",";
            //    }
            //    StrValue = StrValue.TrimEnd(',');
            //    strSql.AppendFormat(" and Id in({0}) ", StrValue);
            //}

            // �ж��û���ע������
            if (filter.RegistrationType.HasValue)
            {
                // �ж��Ƿ��ѯ����Ա��Ӻ����ͨ����
                if (filter.RegistrationType == -1)
                {
                    strSql.Append(" and ( IsPageRegistration=0  or IsAudit=1) ");
                }
                else if (filter.RegistrationType == 0)
                {
                    strSql.Append(" and IsPageRegistration=0 ");
                }
                else if (filter.RegistrationType == 1)
                {
                    strSql.Append(" and IsPageRegistration=1 ");

                    // ע���û������״̬
                    if (filter.IsAudit.HasValue)
                        strSql.AppendFormat(" and IsAudit={0} ", filter.IsAudit);
                }
            }
            //if (filter.RegistrationType.HasValue && filter.RegistrationType != -1)
            //    strSql.AppendFormat(" and IsPageRegistration={0} ", filter.RegistrationType);



            return strSql.ToString();
        }

        private string GetStrSort(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(filter.SortName))
            {
                strSql.Append(filter.SortName);
            }
            else
            {
                strSql.Append("UserInfo.Id desc");              // ����ӵ��û���ʾ��ǰ
            }
            if (filter.SortWay.HasValue)
            {
                strSql.Append(filter.SortWay.Value ? " " : " desc");
            }
            return strSql.ToString();
        }

        #endregion

        #region  ��ȡ��ҳ����
        /// <summary>
        /// ��ȡ��ҳ����
        /// </summary>
        public PageModel GetUserInfoPageParams(CustomFilter filter = null)
        {
            bool crossTable = filter != null && filter.Id2List != null && filter.Id2List.Count > 0;

            PageModel model = new PageModel();
            model.Tables = "UserInfo" + (crossTable ? " left join UserClass on UserInfo.Id = UserClass.UserId" : "");
            model.PKey = "UserInfo.Id";
            model.Sort = GetStrSort(filter);
            model.Fields = "UserInfo.Id,UserName,Sex,Phone,Email,Status,UserInfo.RoleId,CreateTime,ModifyTime";
            model.Filter = GetStrWhere(filter) + (crossTable ? string.Format(" and UserClass.ClassId in({0})", string.Join(",", filter.Id2List)) : "");
            return model;
        }

        #endregion

        /*===============================�Զ���ֽ���============================*/

        /// <summary>
        /// �ж�ѧ��/�����Ƿ����
        /// </summary>
        /// <param name="SchoolNumber"></param>
        public bool Exists(int userID, string SchoolNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UserInfo where SchoolNumber=@SchoolNumber");
            if (userID > 0)
            {
                strSql.Append(" and Id<>@Id");
            }

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@SchoolNumber", SchoolNumber, dbType: DbType.String);
                if (userID > 0)
                {
                    param.Add("@Id", userID, dbType: DbType.Int32);
                }
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result > 0;
        }
        /// <summary>
        /// �ж�ѧ��/�����Ƿ����
        /// </summary>
        /// <param name="SchoolNumber"></param>
        public List<string> Exists(IEnumerable<string> schoolNumberList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select SchoolNumber from UserInfo where SchoolNumber in ('{0}')", string.Join("','", schoolNumberList));
            List<string> result = new List<string>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                result = conn.Query<string>(strSql.ToString()).ToList();
            }
            return result;
        }

        /// <summary>
        /// ����һ������
        /// �û���Ϣ
        /// </summary>
        public int AddUserInfo(UserInfo model, List<UserClass> classInfo)
        {
            int result = 0;
            StringBuilder strSql = new StringBuilder();
            var param = new DynamicParameters();

            #region �û�
            strSql.Clear();
            strSql.Append("insert into UserInfo(");
            strSql.Append("UserName,SchoolNumber,Sex,Phone,Email,Address,Status,RoleId,CreateTime,ModifyTime)");

            strSql.Append(" values (");
            strSql.Append("@UserName,@SchoolNumber,@Sex,@Phone,@Email,@Address,@Status,@RoleId,@CreateTime,@ModifyTime)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            param.Add("@UserName", model.UserName, dbType: DbType.String);
            //param.Add("@SchoolNumber", model.SchoolNumber, dbType: DbType.String);
            param.Add("@Sex", model.Sex, dbType: DbType.Int32);
            param.Add("@Phone", model.Phone, dbType: DbType.String);
            param.Add("@Email", model.Email, dbType: DbType.String);
            param.Add("@Address", model.Address, dbType: DbType.String);
            param.Add("@Status", model.Status, dbType: DbType.Int32);
            param.Add("@RoleId", model.RoleId, dbType: DbType.Int32);
            param.Add("@CreateTime", model.CreateTime, dbType: DbType.DateTime);
            param.Add("@ModifyTime", model.ModifyTime, dbType: DbType.DateTime);
            param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
            #endregion
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    conn.Execute(strSql.ToString(), param, tran);
                    result = param.Get<int>("@returnid");

                    #region �˺�

                    strSql.Clear();
                    param = new DynamicParameters();

                    strSql.Append("insert into Account(");
                    strSql.Append("AccountNo,password,UserId,CreateTime,ModifyTime)");

                    strSql.Append(" values (");
                    strSql.Append("@AccountNo,@password,@UserId,@CreateTime,@ModifyTime)");

                    param.Add("@AccountNo", model.AccountInfo.AccountNo, dbType: DbType.String);
                    param.Add("@password", model.AccountInfo.password, dbType: DbType.String);
                    param.Add("@UserId", result, dbType: DbType.Int32);
                    param.Add("@CreateTime", model.AccountInfo.CreateTime, dbType: DbType.DateTime);
                    param.Add("@ModifyTime", model.AccountInfo.ModifyTime, dbType: DbType.DateTime);
                    param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    conn.Execute(strSql.ToString(), param, tran);

                    #endregion

                    #region �û��༶
                    if (classInfo != null && classInfo.Count > 0)
                    {
                        foreach (var item in classInfo)
                        {
                            strSql.Clear();
                            param = new DynamicParameters();
                            strSql.Append("insert into UserClass(");
                            strSql.Append("UserId,ClassId,RoleId)");

                            strSql.Append(" values (");
                            strSql.Append("@UserId,@ClassId,@RoleId)");

                            param.Add("@UserId", result, dbType: DbType.Int32);
                            param.Add("@ClassId", item.ClassId, dbType: DbType.Int32);
                            param.Add("@RoleId", item.RoleId, dbType: DbType.Int32);
                            conn.Execute(strSql.ToString(), param, tran);
                        }
                    }
                    #endregion

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("UserInfoDAL Add", ex);
                    tran.Rollback();
                    result = 0;
                }
            }
            return result;
        }

        /// <summary>
        /// ���������û�
        /// </summary>
        /// <param name="userList">�û��б�</param>
        /// <returns></returns>
        public bool AddBatchUserInfo(List<UserInfo> userList, int classId)
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
                    int tmpRes = 0;
                    foreach (var model in userList)
                    {

                        strSql.Clear();
                        strSql.Append("insert into UserInfo(");
                        strSql.Append("UserName,SchoolNumber,Sex,Phone,Email,Address,Status,RoleId,CreateTime,ModifyTime)");

                        strSql.Append(" values (");
                        strSql.Append("@UserName,@SchoolNumber,@Sex,@Phone,@Email,@Address,@Status,@RoleId,@CreateTime,@ModifyTime)");
                        strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                        param.Add("@UserName", model.UserName, dbType: DbType.String);
                        //param.Add("@SchoolNumber", model.SchoolNumber, dbType: DbType.String);
                        param.Add("@Sex", model.Sex, dbType: DbType.Int32);
                        param.Add("@Phone", model.Phone, dbType: DbType.String);
                        param.Add("@Email", model.Email, dbType: DbType.String);
                        param.Add("@Address", model.Address, dbType: DbType.String);
                        param.Add("@Status", model.Status, dbType: DbType.Int32);
                        param.Add("@RoleId", model.RoleId, dbType: DbType.Int32);
                        param.Add("@CreateTime", DateTime.Now, dbType: DbType.DateTime);
                        param.Add("@ModifyTime", DateTime.Now, dbType: DbType.DateTime);
                        param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);

                        conn.Execute(strSql.ToString(), param, tran);
                        model.Id = param.Get<int>("@returnid");

                        if (model.Id > 0)
                        {
                            #region �˺�
                            if (model.AccountInfo != null)
                            {
                                strSql.Clear();
                                param = new DynamicParameters();

                                strSql.Append("insert into Account(");
                                strSql.Append("AccountNo,password,UserId,CreateTime,ModifyTime)");

                                strSql.Append(" values (");
                                strSql.Append("@AccountNo,@password,@UserId,@CreateTime,@ModifyTime)");

                                param.Add("@AccountNo", model.AccountInfo.AccountNo, dbType: DbType.String);
                                param.Add("@password", model.AccountInfo.password, dbType: DbType.String);
                                param.Add("@UserId", model.Id, dbType: DbType.Int32);
                                param.Add("@CreateTime", DateTime.Now, dbType: DbType.DateTime);
                                param.Add("@ModifyTime", DateTime.Now, dbType: DbType.DateTime);
                                tmpRes = conn.Execute(strSql.ToString(), param, tran);
                                if (tmpRes <= 0)
                                {
                                    tran.Rollback();
                                    return false;
                                }
                            }
                            #endregion

                            #region �û��༶

                            strSql.Clear();
                            param = new DynamicParameters();
                            strSql.Append("insert into UserClass(");
                            strSql.Append("UserId,ClassId,RoleId)");
                            strSql.Append(" values (");
                            strSql.Append("@UserId,@ClassId,@RoleId)");
                            param.Add("@UserId", model.Id, dbType: DbType.Int32);
                            param.Add("@ClassId", classId, dbType: DbType.Int32);
                            param.Add("@RoleId", model.RoleId, dbType: DbType.Int32);
                            tmpRes = conn.Execute(strSql.ToString(), param, tran);
                            if (tmpRes <= 0)
                            {
                                tran.Rollback();
                                return false;
                            }
                            #endregion
                        }
                        else
                        {
                            tran.Rollback();
                            return false;
                        }
                    }
                    tran.Commit();
                    result = true;
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("UserInfoDAL AddBluk", ex);
                    tran.Rollback();
                }
            }
            return result;
        }

        /// <summary>
        /// �����û�
        /// </summary>
        /// <param name="model"></param>
        /// <param name="author">zh 160427�޸�</param>
        /// <returns></returns>
        public bool UpdateUserInfo(UserInfo model)
        {

            int result = 0;
            StringBuilder strSql = new StringBuilder();
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                //var tran = conn.BeginTransaction();
                try
                {
                    #region �����û�

                    strSql.Append("update UserInfo set ");
                    strSql.Append("UserName=@UserName,");
                    strSql.Append("Sex=@Sex,");
                    strSql.Append("ProvinceCode=@ProvinceCode,");
                    strSql.Append("CityCode=@CityCode,");
                    strSql.Append("CollegeName=@CollegeName,");
                    strSql.Append("DepartmentName=@DepartmentName,");
                    strSql.Append("Phone=@Phone,");
                    strSql.Append("Email=@Email,");
                    strSql.Append("ModifyTime=@ModifyTime");
                    strSql.Append(" where Id=@Id ");

                    param.Add("@Id", model.Id, dbType: DbType.Int32);
                    param.Add("@UserName", model.UserName, dbType: DbType.String);
                    param.Add("@Sex", model.Sex, dbType: DbType.Int32);
                    param.Add("@ProvinceCode", model.ProvinceCode, dbType: DbType.Int32);
                    param.Add("@CityCode", model.CityCode, dbType: DbType.Int32);
                    param.Add("@CollegeName", model.CollegeName, dbType: DbType.String);
                    param.Add("@DepartmentName", model.DepartmentName, dbType: DbType.String);
                    param.Add("@Phone", model.Phone, dbType: DbType.String);
                    param.Add("@Email", model.Email, dbType: DbType.String);
                    param.Add("@ModifyTime", DateTime.Now, dbType: DbType.DateTime);
                    result = conn.Execute(strSql.ToString(), param);

                    #endregion
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("UpdateUserInfo", ex);
                    //tran.Rollback();
                }
            }
            return result > 0;
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="ids">���û�id�������ˣ��ö��ŷֿ�</param>
        public bool DeleteUserInfoBulk(List<int> ids)
        {
            int result = 0;
            StringBuilder strSql = new StringBuilder();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    strSql.Append("delete from UserInfo where Id in @Ids ");
                    result = conn.Execute(strSql.ToString(), new { ids = ids.ToArray() }, tran);

                    if (result > 0)
                    {
                        strSql.Clear();
                        strSql.Append("delete from Account where UserId in @Ids ");
                        conn.Execute(strSql.ToString(), new { ids = ids.ToArray() }, tran);

                        strSql.Clear();
                        strSql.Append("delete from UserClass where UserId in @ids ");
                        conn.Execute(strSql.ToString(), new { ids = ids.ToArray() }, tran);
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("UserInfoDAL DeleteBulk", ex);
                    tran.Rollback();
                    result = 0;
                }
                finally
                {
                    if (conn != null)
                        conn.Close();
                }
            }
            return result > 0;
        }

        /// <summary>
        /// �����û�״̬
        /// </summary>
        /// <param name="ids">�û�ID����</param>
        /// <param name="status">�û�����״̬</param>
        public bool UpdateUserStatus(List<int> ids, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UserInfo set Status={0} where Id in @ids");
            int result = 0;
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                result = conn.Execute(string.Format(strSql.ToString(), status), new { ids = ids.ToArray() });
            }
            return result > 0;
        }

        public bool UpdateUserView(int IsView, int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UserInfo set IsView={0} where Id ={1}");
            int result = 0;
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                result = conn.Execute(string.Format(strSql.ToString(), IsView, id));
            }
            return result > 0;
        }


        #region cww ����


        /// <summary>
        /// cww-�û���ҳ�������˺ű���û���Ϣ��
        /// </summary>
        public PageModel GetUserInfoPage(CustomFilter filter = null)
        {
            PageModel model = new PageModel();
            model.Tables = "UserInfo join Account on UserInfo.Id = Account.UserId";
            model.PKey = "UserInfo.Id";
            model.Sort = GetStrSort(filter);
            model.Fields = "UserInfo.*,Account.AccountNo";
            model.Filter = GetStrWhere(filter);
            return model;
        }


        /// <summary>
        /// �����û��������ύ�������˺���Ϣ���û���Ϣ
        /// </summary>
        /// <param name="userInfo">�û���Ϣ</param>
        /// <param name="account">�˺���Ϣ</param>
        /// <returns></returns>
        public bool AddUserInfoAndAccount(UserInfo userInfo, Account account)
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
                    int tmpRes = 0;

                    strSql.Clear();
                    strSql.Append("insert into UserInfo(");
                    strSql.Append("UserName,Sex,Email,CollegeId,CollegeName,DepartmentName,ProvinceCode,CityCode,IDCard,Phone,RoleId,UserType,Status,IsCreate,CreateTime,IsPageRegistration,IsAudit,AvailabilityDate,ExpiryDate)");

                    strSql.Append(" values (");
                    strSql.Append("@UserName,@Sex,@Email,@CollegeId,@CollegeName,@DepartmentName,@ProvinceCode,@CityCode,@IDCard,@Phone,@RoleId,@UserType,@Status,@IsCreate,@CreateTime,@IsPageRegistration,@IsAudit,@AvailabilityDate,@ExpiryDate)");
                    strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                    param.Add("@UserName", userInfo.UserName, dbType: DbType.String);
                    param.Add("@Sex", userInfo.Sex, dbType: DbType.Int32);
                    param.Add("@Phone", userInfo.Phone, dbType: DbType.String);
                    param.Add("@Email", userInfo.Email, dbType: DbType.String);
                    param.Add("@Address", userInfo.Address, dbType: DbType.String);
                    param.Add("@Status", userInfo.Status, dbType: DbType.Int32);
                    param.Add("@RoleId", userInfo.RoleId, dbType: DbType.Int32);
                    param.Add("@CollegeId", userInfo.CollegeId, dbType: DbType.Int32);
                    param.Add("@CollegeName", userInfo.CollegeName, dbType: DbType.String);
                    param.Add("@DepartmentName", userInfo.DepartmentName, dbType: DbType.String);
                    param.Add("@ProvinceCode", userInfo.ProvinceCode, dbType: DbType.Int32);
                    param.Add("@CityCode", userInfo.CityCode, dbType: DbType.Int32);
                    param.Add("@IDCard", userInfo.IDCard, dbType: DbType.String);
                    param.Add("@IsCreate", userInfo.IsCreate, dbType: DbType.Int32);
                    param.Add("@UserType", userInfo.UserType, dbType: DbType.Int32);
                    param.Add("@CreateTime", DateTime.Now, dbType: DbType.DateTime);
                    param.Add("@IsPageRegistration", userInfo.IsPageRegistration, dbType: DbType.Int32);
                    param.Add("@IsAudit", userInfo.IsAudit, dbType: DbType.Int32);
                    param.Add("@AvailabilityDate", userInfo.AvailabilityDate, dbType: DbType.DateTime);
                    param.Add("@ExpiryDate", userInfo.ExpiryDate, dbType: DbType.DateTime);
                    param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    conn.Execute(strSql.ToString(), param, tran);
                    int userId = param.Get<int>("@returnid");               // ���������û���Ϣ��ID

                    if (userId > 0)
                    {
                        #region �˺�
                        if (account != null)
                        {
                            strSql.Clear();
                            param = new DynamicParameters();

                            strSql.Append("insert into Account(");
                            strSql.Append("AccountNo,password,UserId,CreateTime,ModifyTime)");

                            strSql.Append(" values (");
                            strSql.Append("@AccountNo,@password,@UserId,@CreateTime,@ModifyTime)");

                            param.Add("@AccountNo", account.AccountNo, dbType: DbType.String);
                            param.Add("@password", account.password, dbType: DbType.String);
                            param.Add("@UserId", userId, dbType: DbType.Int32);
                            param.Add("@CreateTime", DateTime.Now, dbType: DbType.DateTime);
                            param.Add("@ModifyTime", DateTime.Now, dbType: DbType.DateTime);
                            tmpRes = conn.Execute(strSql.ToString(), param, tran);
                            if (tmpRes <= 0)
                            {
                                tran.Rollback();
                                return false;
                            }
                        }
                        #endregion

                    }
                    else
                    {
                        tran.Rollback();
                        return false;
                    }
                    tran.Commit();
                    result = true;
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("UserInfoDAL AddUserInfoAndAccount", ex);
                    tran.Rollback();
                }
            }
            return result;
        }


        /// <summary>
        /// �˺��Ƿ���ڣ�����true=�˺Ŵ���
        /// </summary>
        /// <param name="accountNo">�˺�</param>
        /// <param name="userType">�û����ͣ�3=��ί��4=�û�</param>
        /// <returns>true=�˺Ŵ���</returns>
        public bool ExistsAccountNo(string accountNo, int userType, int collegeId, int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Account a,UserInfo b where a.UserId=b.id and accountNo=@accountNo and Status<>3 and collegeId=@collegeId and b.id<>@userId");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@accountNo", accountNo, dbType: DbType.String);
                param.Add("@roleId", userType, dbType: DbType.Int32);
                param.Add("@collegeId", collegeId, dbType: DbType.Int32);
                param.Add("@userId", userId, dbType: DbType.Int32);
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result > 0;
        }


        /// <summary>
        /// �ж��˺��Ƿ����
        /// </summary>
        /// <param name="accountNoList">�˺��б�</param>
        /// <param name="userType">�û����ͣ�3=��ί��4=�û�</param>
        public List<string> ExistsAccountNo(IEnumerable<string> accountNoList, int userType, int collegeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select AccountNo from Account a, UserInfo b where a.userid=b.id and collegeId=" + collegeId + " and AccountNo in ('{0}') and Status<>3", string.Join("','", accountNoList));
            List<string> result = new List<string>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                result = conn.Query<string>(strSql.ToString()).ToList();
            }
            return result;
        }

        /// <summary>
        /// ���������û�
        /// </summary>
        /// <param name="listUser">Ҫ��ӵ��û�����</param>
        /// <returns>���ز��������true=�����ύ�ɹ�</returns>
        public bool AddImportUser(List<UserInfo> listUser)
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
                    int tmpRes = 0;

                    foreach (var userInfo in listUser)
                    {
                        strSql.Clear();
                        strSql.Append("insert into UserInfo(");
                        strSql.Append("UserName,Sex,Email,CollegeId,CollegeName,DepartmentName,ProvinceCode,CityCode,IDCard,Phone,RoleId,UserType,Status,IsCreate,CreateTime,AvailabilityDate,IsAudit)");

                        strSql.Append(" values (");
                        strSql.Append("@UserName,@Sex,@Email,@CollegeId,@CollegeName,@DepartmentName,@ProvinceCode,@CityCode,@IDCard,@Phone,@RoleId,@UserType,@Status,@IsCreate,@CreateTime,@AvailabilityDate,1)");
                        strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                        param.Add("@UserName", userInfo.UserName, dbType: DbType.String);
                        param.Add("@Sex", userInfo.Sex, dbType: DbType.Int32);
                        param.Add("@Phone", userInfo.Phone, dbType: DbType.String);
                        param.Add("@Email", userInfo.Email, dbType: DbType.String);
                        param.Add("@Address", userInfo.Address, dbType: DbType.String);
                        param.Add("@Status", userInfo.Status, dbType: DbType.Int32);
                        param.Add("@RoleId", userInfo.RoleId, dbType: DbType.Int32);
                        param.Add("@CollegeId", userInfo.CollegeId, dbType: DbType.Int32);
                        param.Add("@CollegeName", userInfo.CollegeName, dbType: DbType.String);
                        param.Add("@DepartmentName", userInfo.DepartmentName, dbType: DbType.String);
                        param.Add("@ProvinceCode", userInfo.ProvinceCode, dbType: DbType.Int32);
                        param.Add("@CityCode", userInfo.CityCode, dbType: DbType.Int32);
                        param.Add("@IDCard", userInfo.IDCard, dbType: DbType.String);
                        param.Add("@IsCreate", userInfo.IsCreate, dbType: DbType.Int32);
                        param.Add("@UserType", userInfo.UserType, dbType: DbType.Int32);
                        param.Add("@CreateTime", DateTime.Now, dbType: DbType.DateTime);
                        param.Add("@AvailabilityDate", DateTime.Now, dbType: DbType.DateTime);
                        param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);

                        conn.Execute(strSql.ToString(), param, tran);
                        int userId = param.Get<int>("@returnid");               // ���������û���Ϣ��ID

                        if (userId > 0)
                        {
                            #region �˺�

                            strSql.Clear();
                            param = new DynamicParameters();

                            strSql.Append("insert into Account(");
                            strSql.Append("AccountNo,password,UserId,CreateTime,ModifyTime)");

                            strSql.Append(" values (");
                            strSql.Append("@AccountNo,@password,@UserId,@CreateTime,@ModifyTime)");

                            param.Add("@AccountNo", userInfo.AccountNo, dbType: DbType.String);
                            param.Add("@password", userInfo.Password, dbType: DbType.String);
                            param.Add("@UserId", userId, dbType: DbType.Int32);
                            param.Add("@CreateTime", DateTime.Now, dbType: DbType.DateTime);
                            param.Add("@ModifyTime", DateTime.Now, dbType: DbType.DateTime);
                            tmpRes = conn.Execute(strSql.ToString(), param, tran);
                            if (tmpRes <= 0)
                            {
                                tran.Rollback();
                                return false;
                            }

                            #endregion
                        }
                        else
                        {
                            tran.Rollback();
                            return false;
                        }
                    }
                    tran.Commit();
                    result = true;
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("UserInfoDAL AddImportUser", ex);
                    tran.Rollback();
                }
            }
            return result;
        }

        /// <summary>
        /// ���������û�״̬
        /// </summary>
        /// <param name="ids">�û�ID����</param>
        /// <param name="status">�û�����״̬��1ʧЧ��2������3ɾ��</param>
        /// <param name="userType">�û����ͣ�1=��ί��2=�����û�</param>
        public bool BatchUpdateUserStatus(List<int> ids, int status, int userType)
        {
            List<string[]> sqls = new List<string[]>();
            StringBuilder strSql = new StringBuilder();

            foreach (var item in ids)
            {
                string sql = "";

                // �޸��û�״̬Ϊ��ɾ��
                sql = "update UserInfo set Status=" + status + " where Id =" + item;
                sqls.Add(new string[] { EnumDataBaseName.GetDataBaseConfig(EnumDataBase.User), sql });

                // ɾ��������Ϣ
                //if (userType == 1)
                //    sql = "delete CompetitionJudges where UserId=" + item;
                //else
                //    sql = "delete CompetitionUser where UserId=" + item;

                //sqls.Add(new string[] { EnumDataBaseName.GetDataBaseConfig(EnumDataBase.Match), sql });
            }

            // ��������ύ
            return DBHelper.ExecuteMultiTran(sqls);
        }

        /// <summary>
        /// ���֤�����Ƿ���ڣ�����true=�˺Ŵ���
        /// </summary>
        /// <param name="idCard">���֤����</param>
        /// <param name="userType">�û����ͣ�3=��ί��4=�û�</param>
        /// <returns>true=idCode</returns>
        public bool ExistsIdCard(string idCard, int userType, int collegeId, int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UserInfo where IDCard=@idCard and Status<>3 and CollegeId=@collegeId and id<>@userId");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@idCard", idCard, dbType: DbType.String);
                param.Add("@roldId", userType, dbType: DbType.Int32);
                param.Add("@collegeId", collegeId, dbType: DbType.Int32);
                param.Add("@userId", userId, dbType: DbType.Int32);
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result > 0;
        }

        /// <summary>
        /// ���֤�����Ƿ���ڣ�����true=�˺Ŵ���
        /// </summary>
        /// <param name="idCardList">���֤�����б�</param>
        /// <param name="userType">�û����ͣ�3=��ί��4=�û�</param>
        public List<string> ExistsIDCardNoList(IEnumerable<string> idCardList, int userType, int collegeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select IDCard from UserInfo where IDCard in ('{0}') and Status<>3 and collegeId=" + collegeId + " and roleid=" + userType, string.Join("','", idCardList));
            List<string> result = new List<string>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                result = conn.Query<string>(strSql.ToString()).ToList();
            }
            return result;
        }


        /// <summary>
        /// �����޸��û����״̬
        /// </summary>
        /// <param name="ids">�û�ID����</param>
        /// <param name="audit">���״̬��1ͨ����2�ܾ�</param>
        public bool UpdateAudit(List<int> ids, int audit)
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
                    foreach (var item in ids)
                    {
                        strSql.Clear();
                        strSql.Append("update UserInfo set isaudit=@isAudit where Id =@userId");

                        param.Add("@isAudit", audit, dbType: DbType.Int32);
                        param.Add("@userId", item, dbType: DbType.Int32);

                        conn.Execute(strSql.ToString(), param, tran);
                    }
                    tran.Commit();
                    result = true;
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("UserInfoDAL UpdateAudit", ex);
                    tran.Rollback();
                }
            }
            return result;
        }


        /// <summary>
        /// ����δ�����û���Ϣ���������ֶ�����-�Ҳ��б���û���Ϣ
        /// </summary>
        /// <param name="collegeId">ѧУID</param>
        /// <param name="competitionId">����Id</param>
        /// <param name="groupIds">���Ե��û�����</param>
        public List<UserInfo> NotGroupUser(int collegeId, int competitionId, int groupId, string query, int? pageIndex, int? pageSize, out int totalCount)
        {
            List<UserInfo> result = new List<UserInfo>();
            StringBuilder strSql = new StringBuilder();

            totalCount = 0;

            // ��ѯ������
            strSql.Append("select count(1) from [GTA_FPBT_Structure_V1.5].dbo.UserInfo a, [GTA_FPBT_Structure_V1.5].dbo.Account b ");
            strSql.Append(" where RoleId=4 and a.IsAudit=1 and a.Id=b.UserId and a.Status=2 and a.CollegeId=" + collegeId);
            strSql.Append(" and ( a.id not in ( ");
            strSql.Append(" select userid ");
            strSql.Append(" from [GTA_FPBT_Match_V1.5].dbo.CompetitionUser where CompetitionId=" + competitionId);
            strSql.Append(" )or a.id in(select userid from [GTA_FPBT_Match_V1.5].dbo.CompetitionUser where CompetitionId=" + competitionId + "  ");

            if (groupId != 0)
                strSql.Append(" and GroupId=" + groupId + "))");
            else
                strSql.Append(" and GroupSouce=2 ))");

            if (!string.IsNullOrEmpty(query))
                strSql.Append(" and (a.UserName like '%" + query + "%' or b.AccountNo like '%" + query + "%')");

            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                totalCount = conn.Query<int>(strSql.ToString()).FirstOrDefault();
            }

            // ��ѯ��ҳ����
            strSql.Clear();
            strSql.Append("select * from (");
            strSql.Append("select row_number() over (order by a.id) as no, a.*,b.AccountNo from [GTA_FPBT_Structure_V1.5].dbo.UserInfo a, [GTA_FPBT_Structure_V1.5].dbo.Account b ");
            strSql.Append(" where RoleId=4 and a.IsAudit=1 and a.Id=b.UserId and a.Status=2 and a.CollegeId=" + collegeId);
            strSql.Append(" and ( a.id not in ( ");
            strSql.Append(" select userid ");
            strSql.Append(" from [GTA_FPBT_Match_V1.5].dbo.CompetitionUser where CompetitionId=" + competitionId);
            strSql.Append(" )or a.id in(select userid from [GTA_FPBT_Match_V1.5].dbo.CompetitionUser where CompetitionId=" + competitionId + "  ");

            if (groupId != 0)
                strSql.Append(" and GroupId=" + groupId + "))");
            else
                strSql.Append(" and GroupSouce=2 ))");

            if (!string.IsNullOrEmpty(query))
                strSql.Append(" and (a.UserName like '%" + query + "%' or b.AccountNo like '%" + query + "%')");

            strSql.Append(" ) x  where no BETWEEN " + ((pageIndex - 1) * pageSize + 1) + " and " + (pageIndex * pageSize) + "  order by x.id");

            //strSql.Append("select top " + pageSize + " a.*,b.AccountNo from [GTA_FPBT_Structure_V1.5].dbo.UserInfo a, [GTA_FPBT_Structure_V1.5].dbo.Account b ");
            //strSql.Append(" where RoleId=4 and a.Id=b.UserId and a.Status=2 and a.CollegeId=" + collegeId);
            //strSql.Append(" and ( a.id not in ( ");
            //strSql.Append(" select top " + (pageIndex - 1) * pageSize + " a.id from [GTA_FPBT_Structure_V1.5].dbo.UserInfo a, [GTA_FPBT_Structure_V1.5].dbo.Account b");
            //strSql.Append(" where RoleId=4 and a.Id=b.UserId and a.Status=2 and a.CollegeId=" + collegeId + " and(  a.id not in (select userid ");
            //strSql.Append(" from [GTA_FPBT_Match_V1.5].dbo.CompetitionUser where CompetitionId=" + competitionId);
            //strSql.Append(" )or a.id in(select userid from [GTA_FPBT_Match_V1.5].dbo.CompetitionUser where CompetitionId=" + competitionId + "  ");

            //if (groupId != 0)
            //    strSql.Append(" and GroupId=" + groupId + ")))");
            //else
            //    strSql.Append(" and GroupSouce=2 )))");

            //strSql.Append(" and	a.id not in (select userid from [GTA_FPBT_Match_V1.5].dbo.CompetitionUser where CompetitionId=" + competitionId);
            //strSql.Append(" )or a.id in(select userid from [GTA_FPBT_Match_V1.5].dbo.CompetitionUser where CompetitionId=6 ");
            //if (groupId != 0)
            //    strSql.Append(" and GroupId=" + groupId + "))");
            //else
            //    strSql.Append(" and GroupSouce=2 ))");

            //if (!string.IsNullOrEmpty(query))
            //    strSql.Append(" and (a.UserName like '%" + query + "%' or b.AccountNo like '%" + query + "%')");

            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                result = conn.Query<UserInfo>(strSql.ToString()).ToList();
            }
            return result;
        }


        /// <summary>
        /// �����˺ţ���ѯ�û���Ϣ
        /// </summary>
        /// <param name="accountNo"></param>
        /// <param name="collegeId"></param>
        /// <returns></returns>
        public UserInfo GetAccountNoToInfo(string accountNo, int collegeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.*,b.accountNo from UserInfo a,Account b where a.id=b.userid and AccountNo=@AccountNo and Status<>3 and CollegeId=@collegeId");

            UserInfo result = new UserInfo();
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@AccountNo", accountNo, dbType: DbType.String);
                param.Add("@collegeId", collegeId, dbType: DbType.Int32);
                result = conn.Query<UserInfo>(strSql.ToString(), param).FirstOrDefault();
            }
            return result;
        }

        /// <summary>
        /// �����û�����-��������
        /// </summary>
        /// <param name="listUser"></param>
        /// <param name="competitionId"></param>
        /// <returns></returns>
        public bool AddImportGroupUser(List<UserInfo> listUser, int competitionId)
        {
            var param = new DynamicParameters();
            StringBuilder strSql = new StringBuilder();

            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();

                try
                {
                    // ����
                    int maxGroupId = 0;                 // �˴��ڸ߲���ʱ��һ���ķ���=====================
                    List<IGrouping<int, UserInfo>> listGroup = listUser.GroupBy(m => m.GroupId).ToList<IGrouping<int, UserInfo>>();
                    foreach (var item in listGroup)
                    {
                        // ��ѯ���ķ�����
                        strSql.Clear();
                        using (var conn1 = DBHelper.CreateConnection(EnumDataBaseName.GetDataBaseConfig(EnumDataBase.Match)))
                        {
                            conn1.Open();
                            // �ж��Ƿ�������
                            if (maxGroupId == 0)
                            {
                                strSql.Append("select count(GroupId) from [GTA_FPBT_Match_V1.5].dbo.CompetitionUser where CompetitionId=@CompetitionId");
                                param.Add("@CompetitionId", competitionId, dbType: DbType.Int32);
                                int temp = conn1.Query<int>(strSql.ToString(), param).FirstOrDefault();
                                if (temp == 0)
                                    maxGroupId = 1;
                                else
                                {
                                    // �����ݣ���ѯ�������
                                    strSql.Clear();
                                    strSql.Append("select max(GroupId) from [GTA_FPBT_Match_V1.5].dbo.CompetitionUser where CompetitionId=@CompetitionId");
                                    param.Add("@CompetitionId", competitionId, dbType: DbType.Int32);
                                    temp = conn1.Query<int>(strSql.ToString(), param).FirstOrDefault();
                                    maxGroupId = temp + 1;
                                }
                            }
                            else
                                maxGroupId++;
                        }

                        // ѭ������û�
                        foreach (var userInfo in listUser.FindAll(m => m.GroupId == item.Key))
                        {
                            int userId;
                            // �ж��û��Ƿ����
                            if (userInfo.IsExist)
                            {
                                userId = userInfo.Id;
                            }
                            else
                            {
                                // �½��û�
                                #region �����û���Ϣ
                                strSql.Clear();
                                strSql.Append("insert into UserInfo(");
                                strSql.Append("UserName,Sex,Email,CollegeId,CollegeName,DepartmentName,ProvinceCode,CityCode,IDCard,Phone,RoleId,UserType,Status,IsCreate,CreateTime,IsAudit)");

                                strSql.Append(" values (");
                                strSql.Append("@UserName,@Sex,@Email,@CollegeId,@CollegeName,@DepartmentName,@ProvinceCode,@CityCode,@IDCard,@Phone,@RoleId,@UserType,@Status,@IsCreate,@CreateTime,1)");
                                strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                                param.Add("@UserName", userInfo.UserName, dbType: DbType.String);
                                param.Add("@Sex", userInfo.Sex, dbType: DbType.Int32);
                                param.Add("@Phone", userInfo.Phone, dbType: DbType.String);
                                param.Add("@Email", userInfo.Email, dbType: DbType.String);
                                param.Add("@Address", userInfo.Address, dbType: DbType.String);
                                param.Add("@Status", userInfo.Status, dbType: DbType.Int32);
                                param.Add("@RoleId", userInfo.RoleId, dbType: DbType.Int32);
                                param.Add("@CollegeId", userInfo.CollegeId, dbType: DbType.Int32);
                                param.Add("@CollegeName", userInfo.CollegeName, dbType: DbType.String);
                                param.Add("@DepartmentName", userInfo.DepartmentName, dbType: DbType.String);
                                param.Add("@ProvinceCode", userInfo.ProvinceCode, dbType: DbType.Int32);
                                param.Add("@CityCode", userInfo.CityCode, dbType: DbType.Int32);
                                param.Add("@IDCard", userInfo.IDCard, dbType: DbType.String);
                                param.Add("@IsCreate", userInfo.IsCreate, dbType: DbType.Int32);
                                param.Add("@UserType", userInfo.UserType, dbType: DbType.Int32);
                                param.Add("@CreateTime", DateTime.Now, dbType: DbType.DateTime);
                                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);

                                conn.Execute(strSql.ToString(), param, tran);
                                #endregion
                                userId = param.Get<int>("@returnid");               // ���������û���Ϣ��ID

                                if (userId > 0)
                                {
                                    #region ����˺���Ϣ

                                    strSql.Clear();
                                    param = new DynamicParameters();

                                    strSql.Append("insert into Account(");
                                    strSql.Append("AccountNo,password,UserId,CreateTime,ModifyTime)");

                                    strSql.Append(" values (");
                                    strSql.Append("@AccountNo,@password,@UserId,@CreateTime,@ModifyTime)");

                                    param.Add("@AccountNo", userInfo.AccountNo, dbType: DbType.String);
                                    param.Add("@password", userInfo.Password, dbType: DbType.String);
                                    param.Add("@UserId", userId, dbType: DbType.Int32);
                                    param.Add("@CreateTime", DateTime.Now, dbType: DbType.DateTime);
                                    param.Add("@ModifyTime", DateTime.Now, dbType: DbType.DateTime);
                                    int temp = conn.Execute(strSql.ToString(), param, tran);
                                    if (temp <= 0)
                                    {
                                        tran.Rollback();
                                        return false;
                                    }

                                    #endregion
                                }
                                else
                                {
                                    tran.Rollback();
                                    return false;
                                }
                            }

                            #region ��ӷ�����Ϣ

                            strSql.Clear();
                            param = new DynamicParameters();

                            strSql.Append("insert into [GTA_FPBT_Match_V1.5].dbo.CompetitionUser(");
                            strSql.Append("CompetitionId,UserId,GroupId,GroupSouce,IsAudit)");

                            strSql.Append(" values (");
                            strSql.Append("@CompetitionId,@UserId,@GroupId,1,1)");

                            param.Add("@CompetitionId", competitionId, dbType: DbType.Int32);
                            param.Add("@UserId", userId, dbType: DbType.Int32);
                            param.Add("@GroupId", maxGroupId, dbType: DbType.Int32);
                            int temp2 = conn.Execute(strSql.ToString(), param, tran);
                            if (temp2 <= 0)
                            {
                                tran.Rollback();
                                return false;
                            }
                            #endregion
                        }
                    }

                    tran.Commit();
                    return true;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    return false;
                }
            }



        }


        /// <summary>
        /// ��ȡע�������û�����
        /// </summary>
        /// <returns></returns>
        public int GetRegisterNotAduitNum(int collegeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UserInfo  where  Status=2 and CollegeId=@collegeId and IsPageRegistration=1 and IsAudit=0 ");

            int result;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@collegeId", collegeId, dbType: DbType.Int32);
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result;
        }

        /// <summary>
        /// ���¾�������Ա
        /// </summary>
        public bool UpdateComAdmin(UserInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UserInfo set ");
            strSql.Append("UserName=@UserName,");
            strSql.Append("Email=@Email,");
            strSql.Append("Sex=@Sex,");
            strSql.Append("Phone=@Phone,");
            strSql.Append("DepartmentName=@DepartmentName,");
            strSql.Append("ProvinceCode=@ProvinceCode,");
            strSql.Append("IDCard=@IDCard,");
            strSql.Append("CityCode=@CityCode,");
            strSql.Append("CollegeName=@CollegeName,");
            strSql.Append("CollegeId=@CollegeId,");
            strSql.Append("IsCreate=@IsCreate,");

            if (model.AvailabilityDate != DateTime.MinValue)
                strSql.Append("AvailabilityDate=@AvailabilityDate,");
            if (model.ExpiryDate != DateTime.MinValue)
                strSql.Append("ExpiryDate=@ExpiryDate,");
            strSql.Append("UserType=@UserType");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@UserName", model.UserName, dbType: DbType.String);
                param.Add("@Sex", model.Sex, dbType: DbType.Int32);
                param.Add("@Phone", model.Phone, dbType: DbType.String);
                param.Add("@Email", model.Email, dbType: DbType.String);
                param.Add("@DepartmentName", model.DepartmentName, dbType: DbType.String);
                param.Add("@ProvinceCode", model.ProvinceCode, dbType: DbType.Int32);
                param.Add("@CityCode", model.CityCode, dbType: DbType.Int32);
                param.Add("@IDCard", model.IDCard, dbType: DbType.String);
                param.Add("@CollegeName", model.CollegeName, dbType: DbType.String);
                param.Add("@CollegeId", model.CollegeId, dbType: DbType.Int32);

                if (model.AvailabilityDate != DateTime.MinValue)
                    param.Add("@AvailabilityDate", model.AvailabilityDate, dbType: DbType.DateTime);
                if (model.ExpiryDate != DateTime.MinValue)
                    param.Add("@ExpiryDate", model.ExpiryDate, dbType: DbType.DateTime);

                param.Add("@IsCreate", model.IsCreate, dbType: DbType.Int32);
                param.Add("@UserType", model.UserType, dbType: DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        #endregion

        /// <summary>
        /// �������֤ID��ȡ�����û���Ϣ
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CollegeId"></param>
        /// <returns></returns>
        public UserInfo GetUserInfoByID(string ID, int CollegeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from UserInfo where IDCard=@IDCard and RoleId=4 and Status=2 and IsAudit=1 and CollegeId=@CollegeId");

            UserInfo result = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@IDCard", ID, dbType: DbType.String);
                param.Add("@CollegeId", CollegeId, dbType: DbType.Int32);
                result = conn.Query<UserInfo>(strSql.ToString(), param).FirstOrDefault();
            }
            return result;
        }
    }
}

