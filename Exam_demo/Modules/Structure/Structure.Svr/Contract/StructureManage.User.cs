using Structure.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Structure.Svr
{
    public partial class StructureManage
    {
        UserInfoDAL userDal = new UserInfoDAL();
        AccountDAL accountDal = new AccountDAL();
        //UserClassDAL userClassDal = new UserClassDAL();

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName">账号</param>
        /// <param name="password">密码</param>
        /// <param name="user">用户信息</param>
        /// <param name="account">账号信息</param>
        /// <returns></returns>
        //public bool Login(string userName, string password, out UserInfo user, out Account account, out List<UserClass> classInfo)
        public bool Login(string userName, string password, int collegeId, out UserInfo user, out Account account)
        {
            bool rtnValue = true;
            user = null;
            account = null;
            //classInfo = new List<UserClass>();
            try
            {
                account = accountDal.GetModel(userName, password, collegeId);
                if (account != null)
                {
                    user = userDal.GetModel(account.UserId);
                    if (user == null)
                    {
                        account = null;
                    }
                    else
                    {
                        if (user.Status != 2)
                            rtnValue = false;
                    }
                }
                else
                {
                    rtnValue = false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("登录验证异常", ex);
            }
            return rtnValue;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user">用户对象</param>
        public int AddUserInfo(UserInfo user, List<UserClass> classInfo, out string errCode)
        {
            errCode = string.Empty;
            int rtnValue = 0;
            return rtnValue;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user">用户对象</param>
        public bool UpdateUser(UserInfo user, out string errCode)
        {
            errCode = string.Empty;
            bool rtnValue = false;
            try
            {
                rtnValue = userDal.UpdateUserInfo(user);
            }
            catch (Exception ex)
            {
                errCode = "20007";//修改失败!请联系管理员!
                LogHelper.Log.WriteError(errCode, ex);
            }
            return rtnValue;
        }

        public bool UpdateUserView(int IsView, int id)
        {
            string errCode = string.Empty;
            bool rtnValue = false;
            try
            {
                rtnValue = userDal.UpdateUserView(IsView, id);
            }
            catch (Exception ex)
            {
                errCode = "20007";//修改失败!请联系管理员!
                LogHelper.Log.WriteError(errCode, ex);
            }
            return rtnValue;
        }

        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="model">账号对象</param>
        public bool UpdatePassword(Account model, out string errCode)
        {
            errCode = string.Empty;
            bool rtnValue = false;
            try
            {
                rtnValue = accountDal.UpdateAccountPwdById(model);
            }
            catch (Exception ex)
            {
                errCode = "20007";//修改失败!请联系管理员!
                LogHelper.Log.WriteError(errCode, ex);
            }
            return rtnValue;
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="userList">用户信息集合</param>
        public bool AddUserInfoBulk(List<UserInfo> userList, int classId, out string errCode)
        {
            errCode = string.Empty;
            bool rtnValue = false;
            try
            {
                rtnValue = userDal.AddBatchUserInfo(userList, classId);
            }
            catch (Exception ex)
            {
                errCode = "20007";//修改失败!请联系管理员!
                LogHelper.Log.WriteError(errCode, ex);
            }
            return rtnValue;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">用户ID列表</param>
        public bool DeleteUserInfoBulk(List<int> ids)
        {
            bool rtnValue = false;
            try
            {
                rtnValue = userDal.DeleteUserInfoBulk(ids);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DeleteUserInfoBulk", ex);
            }
            return rtnValue;
        }

        /// <summary>
        /// 根据用户ID获取单个用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserInfo GetUserInfo(int userId)
        {
            UserInfo result = new UserInfo();
            try
            {
                result = userDal.GetModel(userId);
                result.AccountInfo = accountDal.GetModelByUserId(userId);
                result.AccountNo = result.AccountInfo.AccountNo;
                //result.UserClassInfo = userClassDal.GetList(new CustomFilter { Id2 = userId }); ;
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetUserInfo方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取用户分页列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页(可为空,为空时返回所有数据)</param>
        /// <param name="pageSize">每页页数(可为空,为空时返回所有数据)</param>
        /// <param name="totalCount">总数量</param>
        /// <returns></returns>
        public List<UserInfo> GetUserInfoPage(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount)
        {
            totalCount = 0;
            List<UserInfo> result = new List<UserInfo>();
            try
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    result = DBHelper.GetPageList<UserInfo>(pageIndex.Value, pageSize.Value, userDal.GetUserInfoPage(filter), out totalCount);
                }
                else
                {
                    result = userDal.GetList(filter);
                }
                if (result.Count > 0)
                {
                    /*
                    filter = new CustomFilter { IdList = result.Select(l => l.Id).ToList() };
                    var accountInfo = accountDal.GetList(filter);
                    //var userClassInfo = userClassDal.GetList(filter);
                    Parallel.ForEach(result, l =>
                    {
                        l.AccountInfo = accountInfo.FirstOrDefault(m => m.UserId == l.Id);
                        //l.UserClassInfo = userClassInfo.Where(m => m.UserId == l.Id).ToList();
                    });
                     */
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetUserInfoPage方法出错", ex);
            }
            return result;

        }

        /// <summary>
        /// 获取学校分页列表（hzq测试）
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页(可为空,为空时返回所有数据)</param>
        /// <param name="pageSize">每页页数(可为空,为空时返回所有数据)</param>
        /// <param name="totalCount">总数量</param>
        /// <returns></returns>
        public List<College> GetCollegePage(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount)
        {
            totalCount = 0;
            List<College> result = new List<College>();
            result.Add(new College() { Id = 1, CollegeName = "国泰安大学" });
            result.Add(new College() { Id = 2, CollegeName = "北京大学" });
            result.Add(new College() { Id = 3, CollegeName = "清华大学" });
            result.Add(new College() { Id = 6, CollegeName = "安徽科技大学" });
            return result;

        }

        /// <summary>
        ///用户的状态更新（批量） 启用用户，未启用
        /// </summary>
        /// <param name="ids">用户ID集合</param>
        /// <param name="status">用户操作状态</param>
        public bool UpdateUserStatus(List<int> ids, int status)
        {
            bool rtnValue = false;
            try
            {
                rtnValue = userDal.UpdateUserStatus(ids, status);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateUserStatus", ex);
            }
            return rtnValue;
        }

        /// <summary>
        /// 统计人数
        /// </summary>
        /// <param name="classId">班级ID</param>
        /// <param name="roleID">角色ID</param>
        /// <returns></returns>
        public int GetUserCount(int? classId, int? roleID)
        {
            int rtnValue = 0;
            //try
            //{
            //    rtnValue = userClassDal.Count(classId, roleID);
            //}
            //catch (Exception ex)
            //{
            //    LogHelper.Log.WriteError("GetUserCount", ex);
            //}
            return rtnValue;
        }


        /// <summary>
        /// 获取重复的学号列表
        /// </summary>
        /// <param name="schoolNum"></param>
        /// <returns></returns>
        public List<string> GetExistSchoolNumber(List<string> schoolNum)
        {
            List<string> rtnValue = new List<string>();
            try
            {
                rtnValue = userDal.Exists(schoolNum);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetExistSchoolNumber", ex);
            }
            return rtnValue;
        }

        #region cww 新增扩展方法

        // <summary>
        /// 新增用户，添加用户信息和账号信息，事务提交
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="account">账号信息</param>
        /// <returns></returns>
        public bool AddUserInfoAndAccount(UserInfo userInfo, Account account)
        {
            // 验证账号是否重复
            bool flag = false;

            try
            {
                flag = userDal.AddUserInfoAndAccount(userInfo, account);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddUserInfoAndAccount", ex);
            }
            return flag;
        }


        /// <summary>
        /// 账号是否存在，返回true=账号存在
        /// </summary>
        /// <param name="accountNo"></param>
        /// <param name="userType">用户类型，3=评委，4=用户</param>
        /// <returns></returns>
        public bool ExistsAccountNo(string accountNo, int userType, int collegeId, int userId)
        {
            // 验证账号是否重复
            bool flag = userDal.ExistsAccountNo(accountNo, userType, collegeId, userId);

            return flag;
        }


        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        public bool UpdateUserInfo(UserInfo userInfo)
        {
            // 验证账号是否重复
            bool flag = userDal.Update(userInfo);

            return flag;
        }


        /// <summary>
        /// 判断账号是否存在
        /// </summary>
        /// <param name="accountNoList">账号列表</param>
        /// <param name="userType">用户类型，3=评委，4=用户</param>
        public List<string> ExistsAccountNoList(List<string> accountNoList, int userType, int collegeId)
        {
            List<string> rtnValue = new List<string>();
            try
            {
                rtnValue = userDal.ExistsAccountNo(accountNoList, userType, collegeId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("ExistsAccountNo", ex);
            }
            return rtnValue;

        }

        /// <summary>
        /// 判断账号是否存在
        /// </summary>
        /// <param name="listUser">要添加的用户集合</param>
        /// <returns>返回操作结果，true=事务提交成功</returns>
        public bool AddImportUser(List<UserInfo> listUser)
        {
            return userDal.AddImportUser(listUser);
        }

        /// <summary>
        /// 批量更新用户状态
        /// </summary>
        /// <param name="ids">用户ID集合</param>
        /// <param name="status">用户操作状态，1失效、2正常、3删除</param>
        /// <param name="userType">用户类型，1=评委，2=参赛用户</param>
        public bool BatchUpdateUserStatus(List<int> ids, int status, int userType)
        {
            return userDal.BatchUpdateUserStatus(ids, status, userType);
        }

        /// <summary>
        /// 判断身份证号码是否存在
        /// </summary>
        /// <param name="idCode">身份证号码</param>
        /// <param name="userType">用户类型，3=评委，4=用户</param>
        /// <returns></returns>
        public bool ExistsIdCard(string idCode, int userType, int collegeId, int userId)
        {
            // 验证身份证号码是否重复
            bool flag = userDal.ExistsIdCard(idCode, userType, collegeId, userId);
            return flag;
        }

        /// <summary>
        /// 判断账号是否存在
        /// </summary>
        /// <param name="idCardNoList">身份证号码列表</param>
        /// <param name="userType">用户类型，3=评委，4=用户</param>
        public List<string> ExistsIDCardList(List<string> idCardNoList, int userType, int collegeId)
        {
            // 验证身份证号码是否重复
            List<string> flag = userDal.ExistsIDCardNoList(idCardNoList, userType, collegeId);
            return flag;
        }

        /// <summary>
        /// 批量修改用户审核状态
        /// </summary>
        /// <param name="ids">用户ID集合</param>
        /// <param name="audit">审核状态，1通过、2拒绝</param>
        public bool UpdateAudit(List<int> ids, int audit)
        {
            bool flag = userDal.UpdateAudit(ids, audit);
            return flag;
        }

        /// <summary>
        /// 返回未分组用户信息，但包括手动分组-右侧列表的用户信息
        /// </summary>
        /// <param name="collegeId">学校ID</param>
        /// <param name="competitionId">比赛Id</param>
        /// <param name="groupIds">忽略的用户分组</param>
        public List<UserInfo> NotGroupUser(int collegeId, int competitionId, int groupId, string quertFile, int? pageIndex, int? pageSize, out int totalCount)
        {
            return userDal.NotGroupUser(collegeId, competitionId, groupId, quertFile, pageIndex, pageSize, out totalCount);
        }

        /// <summary>
        /// 根据账号，查询用户信息
        /// </summary>
        /// <param name="accountNo"></param>
        /// <param name="collegeId"></param>
        /// <returns></returns>
        public UserInfo GetAccountNoToInfo(string accountNo, int collegeId)
        {
            return userDal.GetAccountNoToInfo(accountNo, collegeId);
        }

        /// <summary>
        /// 竞赛用户分组-批量导入
        /// </summary>
        /// <param name="listUser"></param>
        /// <param name="competitionId"></param>
        /// <returns></returns>
        public bool AddImportGroupUser(List<UserInfo> listUser, int competitionId)
        {
            return userDal.AddImportGroupUser(listUser, competitionId);
        }

        /// <summary>
        /// 获取注册待审核用户数量
        /// </summary>
        /// <returns></returns>
        public int GetRegisterNotAduitNum(int collegeId)
        {
            return userDal.GetRegisterNotAduitNum(collegeId);
        }


        /// <summary>
        /// 更新竞赛管理员
        /// </summary>
        public bool UpdateComAdmin(UserInfo model)
        {
            return userDal.UpdateComAdmin(model);
        }
        #endregion

        /// <summary>
        /// 根据身份证ID获取单个用户信息
        /// </summary>
        /// <param name="IDNum"></param>
        /// <param name="CollegeId"></param>
        /// <returns></returns>
        public UserInfo GetUserInfoByID(string IDNum, int CollegeId)
        {
            UserInfo result = new UserInfo();
            try
            {
                result = userDal.GetUserInfoByID(IDNum, CollegeId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetModelByID方法出错", ex);
            }
            return result;
        }
    }
}
