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
        /// 新增建议书
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddProposal(ProposalVM model)
        {
            Proposal proposal = model.Map<Proposal, ProposalVM>();
            return MyService.AddProposal(proposal);
        }

        /// <summary>
        /// 修改建议书
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateProposal(ProposalVM model)
        {
            Proposal proposal = model.Map<Proposal, ProposalVM>();
            return MyService.UpdateProposal(proposal);
        }

        /// <summary>
        /// 修改建议书目前的状态
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public bool UpdateProposalStatus(int Id, int Status)
        {
            return MyService.UpdateProposalStatus(Id, Status);
        }

        /// <summary>
        /// 获取建议书
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public ProposalVM GetProposal(int Id)
        {
            Proposal model = MyService.GetProposal(Id);
            return model.Map<ProposalVM, Proposal>();
        }

        /// <summary>
        /// 获取建议书
        /// </summary>
        /// <param name="TrainExamId">销售机会/实训考核Id</param>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public ProposalVM GetProposal(int TrainExamId, int UserId)
        {
            CustomFilter filter = new CustomFilter { TrainExamId = TrainExamId, UserId = UserId };
            return MyService.GetProposalInfo(filter).Map<ProposalVM, Proposal>();
        }
    }
}
