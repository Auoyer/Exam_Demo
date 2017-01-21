using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.API;
using VM;

namespace Server.Factory
{
    public partial class TrainingService : IService<ITrainingManage>
    {

        /// <summary>
        /// 新增客户信息
        /// </summary>
        /// <param name="stuCustomer">客户信息</param>
        /// <returns></returns>
        public int AddStuCustomer(StuCustomerVM stuCustomer)
        {
            return MyService.AddStuCustomer(stuCustomer.Map<StuCustomer, StuCustomerVM>());
        }

        /// <summary>
        /// 修改客户信息
        /// </summary>
        /// <param name="stuCustomer">客户信息</param>
        /// <returns></returns>
        public bool UpdateCustomer(StuCustomerVM stuCustomer)
        {
            return MyService.UpdateCustomer(stuCustomer.Map<StuCustomer, StuCustomerVM>());
        }

        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <param name="Id">客户Id</param>
        /// <returns></returns>
        public StuCustomerVM GetStuCustomer(int Id)
        {
            return MyService.GetStuCustomer(Id).Map<StuCustomerVM, StuCustomer>();
        }

        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <param name="IDNum">客户证件号</param>
        /// <param name="UserId">学生Id</param>
        /// <returns>客户信息实体</returns>
        public StuCustomerVM GetStuCustomerByIDNum(string IDNum, int UserId)
        {
            return MyService.GetStuCustomerByIDNum(IDNum, UserId).Map<StuCustomerVM, StuCustomer>();
        }

        /// <summary>
        /// 更新潜在客户/已有客户的状态和建议书Id
        /// </summary>
        /// <param name="ProposalId">建议书Id</param>
        /// <param name="Status">状态</param>
        /// <param name="TrainExamId">销售机会Id</param>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public bool UpdateStuCustomerStatusAndProposalId(int ProposalId, int Status, int Id)
        {
            return MyService.UpdateStuCustomerStatusAndProposalId(ProposalId, Status, Id);
        }

        /// <summary>
        /// 根据建议书Id更新潜在客户/已有客户的状态
        /// </summary>
        /// <param name="ProposalId">建议书Id</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public bool UpdateStuCustomerStatus(int ProposalId, int Status, int? IsHightCustomer)
        {
            return MyService.UpdateStuCustomerStatus(ProposalId, Status, IsHightCustomer.Value);
        }

        /// <summary>
        /// 根据客户Id，用户Id更新该客户拥有建议书数量
        /// </summary>
        /// <param name="stuCustomerId">客户Id</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public bool UpdateCustomerProposalCount(int stuCustomerId, int userId)
        {
            return MyService.UpdateCustomerProposalCount(stuCustomerId, userId);
        }


    }
}
