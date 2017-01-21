using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Structure.API
{
    public partial interface IStructureManage
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName">账号</param>
        /// <param name="password">密码</param>
        /// <param name="user">用户信息</param>
        /// <param name="account">账号信息</param>
        /// <returns></returns>
        [OperationContract]
        bool Login(string userName, string password, int collegeId, out UserInfo user, out Account account);
        //bool Login(string userName, string password, out UserInfo user, out Account account, out List<UserClass> classInfo);


        #region 用户信息管理

        ///// <summary>
        ///// 更新用户信息
        ///// </summary>
        ///// <param name="user">用户对象</param>
        [OperationContract]
        bool UpdateUser(UserInfo user, out string errCode);

        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="model">账号对象</param>
        [OperationContract]
        bool UpdatePassword(Account model, out string errCode);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="userList">用户信息集合</param>
        [OperationContract]
        bool AddUserInfoBulk(List<UserInfo> userList, int classId, out string errCode);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">将id批量传人，用逗号分开</param>
        /// <param name="delType">1为更新状态删除，2为物理删除</param>
        [OperationContract]
        bool DeleteUserInfoBulk(List<int> ids);

        /// <summary>
        /// 根据用户ID获取单个用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract]
        UserInfo GetUserInfo(int userId);

        /// <summary>
        /// 获取用户分页列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页(可为空,为空时返回所有数据)</param>
        /// <param name="pageSize">每页页数(可为空,为空时返回所有数据)</param>
        /// <param name="totalCount">总数量</param>
        /// <returns></returns>
        [OperationContract]
        List<UserInfo> GetUserInfoPage(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount);

        #region 黄忠情（测试用）
        /// <summary>
        /// 获取学校分页列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页(可为空,为空时返回所有数据)</param>
        /// <param name="pageSize">每页页数(可为空,为空时返回所有数据)</param>
        /// <param name="totalCount">总数量</param>
        /// <returns></returns>
        [OperationContract]
        List<College> GetCollegePage(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount);
        #endregion

        /// <summary>
        ///用户的状态更新（批量） 启用用户，未启用，删除用户
        /// </summary>
        /// <param name="ids">用户ID集合</param>
        /// <param name="status">用户操作状态</param>
        [OperationContract]
        bool UpdateUserStatus(List<int> ids, int status);


        /// <summary>
        /// 统计人数
        /// </summary>
        /// <param name="classId">班级ID</param>
        /// <param name="roleID">角色ID</param>
        /// <returns></returns>
        [OperationContract]
        int GetUserCount(int? classId, int? roleID);

        /// <summary>
        /// 获取重复的学号列表
        /// </summary>
        /// <param name="schoolNum"></param>
        /// <returns></returns>
        [OperationContract]
        List<string> GetExistSchoolNumber(List<string> schoolNum);
        #endregion


        #region cww 新增扩展方法

        /// <summary>
        /// 新增用户，添加用户信息和账号信息，事务提交
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="account">账号信息</param>
        /// <returns></returns>
        [OperationContract]
        bool AddUserInfoAndAccount(UserInfo userInfo, Account account);

        /// <summary>
        /// 判断账号是否存在
        /// </summary>
        /// <param name="accountNo">账号</param>
        /// <param name="userType">用户类型，3=评委，4=用户</param>
        /// <returns></returns>
        [OperationContract]
        bool ExistsAccountNo(string accountNo, int userType, int collegeId, int userId);

        // <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateUserInfo(UserInfo userInfo);

        /// <summary>
        /// 判断账号是否存在
        /// </summary>
        /// <param name="accountNoList">账号列表</param>
        /// <param name="userType">用户类型，3=评委，4=用户</param>
        [OperationContract]
        List<string> ExistsAccountNoList(List<string> accountNoList, int collegeId, int userType);

        /// <summary>
        /// 批量添加用户
        /// </summary>
        /// <param name="listUser">要添加的用户集合</param>
        /// <returns>返回操作结果，true=事务提交成功</returns>
        [OperationContract]
        bool AddImportUser(List<UserInfo> listUser);

        /// <summary>
        /// 批量更新用户状态
        /// </summary>
        /// <param name="ids">用户ID集合</param>
        /// <param name="status">用户操作状态，1失效、2正常、3删除</param>
        /// <param name="userType">用户类型，1=评委，2=参赛用户</param>
        [OperationContract]
        bool BatchUpdateUserStatus(List<int> ids, int status, int userType);

        /// <summary>
        /// 判断身份证号码是否存在
        /// </summary>
        /// <param name="idCard">身份证号码</param>
        /// <param name="userType">用户类型，3=评委，4=用户</param>
        /// <returns></returns>
        [OperationContract]
        bool ExistsIdCard(string idCard, int userType, int collegeId, int userId);

        /// <summary>
        /// 判断账号是否存在
        /// </summary>
        /// <param name="idCardNoList">身份证号码列表</param>
        /// <param name="userType">用户类型，3=评委，4=用户</param>
        [OperationContract]
        List<string> ExistsIDCardList(List<string> idCardNoList, int userType, int collegeId);

        /// <summary>
        /// 批量修改用户审核状态
        /// </summary>
        /// <param name="ids">用户ID集合</param>
        /// <param name="audit">审核状态，1通过、2拒绝</param>
        [OperationContract]
        bool UpdateAudit(List<int> ids, int audit);


        /// <summary>
        /// 返回未分组用户信息，但包括手动分组-右侧列表的用户信息
        /// </summary>
        /// <param name="collegeId">学校ID</param>
        /// <param name="competitionId">比赛Id</param>
        /// <param name="groupIds">忽略的用户分组</param>
        [OperationContract]
        List<UserInfo> NotGroupUser(int collegeId, int competitionId, int groupId, string query, int? pageIndex, int? pageSize, out int totalCount);

        /// <summary>
        /// 根据账号，查询用户信息
        /// </summary>
        /// <param name="accountNo"></param>
        /// <param name="collegeId"></param>
        /// <returns></returns>
        [OperationContract]
        UserInfo GetAccountNoToInfo(string accountNo, int collegeId);

        /// <summary>
        /// 竞赛用户分组-批量导入
        /// </summary>
        /// <param name="listUser"></param>
        /// <param name="competitionId"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddImportGroupUser(List<UserInfo> listUser, int competitionId);

        /// <summary>
        /// 获取注册待审核用户数量
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int GetRegisterNotAduitNum(int collegeId);


        /// <summary>
        /// 更新竞赛管理员
        /// </summary>
        [OperationContract]
        bool UpdateComAdmin(UserInfo model);
        #endregion

        /// <summary>
        /// 根据身份证ID获取单个用户信息
        /// </summary>
        /// <param name="IDNum"></param>
        /// <param name="CollegeId"></param>
        /// <returns></returns>
        [OperationContract]
        UserInfo GetUserInfoByID(string IDNum, int CollegeId);
    }
}
