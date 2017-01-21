using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Training.API
{
    public partial interface ITrainingManage
    {
        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <param name="filter">条件搜索</param>
        /// <returns></returns>
        [OperationContract]
        List<StuCustomer> GetStuCustomerList(CustomFilter filter);


        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <param name="filter">条件搜索</param>
        /// <returns></returns>
        [OperationContract]
        List<StuCustomer> GetStuCustomerList2(CustomFilter filter);

        /// <summary>
        /// 获取客户信息及日程表联表分页列表
        /// </summary>
        /// <param name="filter">条件搜索</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<StuCustomer> GetUnitStuCustomerList(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount);
        
        /// <summary>
        /// 新增客户信息
        /// </summary>
        /// <param name="stuCustomer">客户信息类</param>
        /// <returns></returns>
        [OperationContract]
        int AddStuCustomer(StuCustomer stuCustomer);

        /// <summary>
        /// 修改客户信息
        /// </summary>
        /// <param name="stuCustomer">客户信息类</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateCustomer(StuCustomer stuCustomer);

        /// <summary>
        /// 删除客户信息(真删)
        /// 同步删除以下信息：
        /// 1.建议书(真删)
        /// 2.工作日程(真删)
        /// 3.....后续根据需求增加
        /// </summary>
        /// <param name="Id">客户Id</param>
        /// <returns></returns>
        [OperationContract]
        bool DelCustomer(int Id);

        /// <summary>
        /// 删除客户信息(伪删)
        /// 同步删除以下信息：
        /// 1.建议书(伪删)
        /// 2.工作日程(真删)
        /// 3.....后续根据需求增加
        /// </summary>
        /// <param name="Id">客户Id</param>
        /// <returns></returns>
        [OperationContract]
        bool RemoveCustomer(int Id);


        /// <summary>
        /// 获取潜在客户/已有客户信息，日程管理要用到
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<StuCustomer> GetStuCustomerListByCalendar(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount);


        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <param name="Id">客户Id</param>
        /// <returns></returns>
        [OperationContract]
        StuCustomer GetStuCustomer(int Id);

        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <param name="Id">客户Id</param>
        /// <returns></returns>
        [OperationContract]
        StuCustomer GetStuCustomerObj(int TrainExamId);

        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <param name="IDNum">客户证件号</param>
        /// <param name="UserId">学生Id</param>
        /// <returns>客户信息实体</returns>
        [OperationContract]
        StuCustomer GetStuCustomerByIDNum(string IDNum, int UserId);


        /// <summary>
        /// 更新潜在客户/已有客户的状态和建议书Id
        /// </summary>
        /// <param name="ProposalId">建议书Id</param>
        /// <param name="Status">状态</param>
        /// <param name="TrainExamId">销售机会Id</param>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateStuCustomerStatusAndProposalId(int ProposalId, int Status, int Id);

        /// <summary>
        /// 获取潜在客户数/已有客户数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract]
        CustomerCount GetCustomerCountModel(int userId);

        /// <summary>
        /// 根据建议书Id更新潜在客户/已有客户的状态
        /// </summary>
        /// <param name="ProposalId">建议书Id</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateStuCustomerStatus(int ProposalId, int Status, int? IsHightCustomer);
        /// <summary>
        /// 将潜在客户更新为已有客户
        /// </summary> 
        /// <param name="ProposalId">建议书Id</param>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateCustomerType(int ProposalId, int UserId);

        /// <summary>
        /// 批量将潜在客户更新为已有客户
        /// </summary>
        /// <param name="TrainExamId">销售机会Id</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateCustomerTypeList(int TrainExamId);

        /// <summary>
        /// 根据客户Id，用户Id更新该客户拥有建议书数量
        /// </summary>
        /// <param name="stuCustomerId">客户Id</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateCustomerProposalCount(int stuCustomerId, int userId);

    }
}
