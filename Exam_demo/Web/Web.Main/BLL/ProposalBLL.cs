using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.Factory;
using VM;

namespace Web
{
    public class ProposalBLL
    {
        /// <summary>
        /// 获取建议书
        /// </summary>
        /// <param name="ProposalId">建议书Id</param>
        /// <returns></returns>
        public static ProposalVM GetProposal(int ProposalId)
        {
            return TrainingCaches.GetProposalCache(ProposalId);
        }

        /// <summary>
        /// 获取建议书
        /// </summary>
        /// <param name="trainId">销售机会/实训考核Id</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public static ProposalVM GetProposal(int trainId, int userId)
        {
            ProposalVM model = new ProposalVM();
            var entity = TrainingCaches.CurProposalCache().FirstOrDefault(x => x.TrainExamId == trainId && x.UserId == userId);
            if (entity == null)
            {
                //建议书
                entity = SvrFactory.Instance.TrainingSvr.GetProposal(trainId, userId);
            }

            return TrainingCaches.GetProposalCache(entity.Id);
        }

    }
}