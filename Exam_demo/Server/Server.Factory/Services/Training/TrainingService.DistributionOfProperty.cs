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
        /// 新增财务分析
        /// </summary>
        /// <param name="model">财务分析实体</param>
        /// <returns>财务分析Id</returns>
        public int AddDistributionOfProperty(DistributionOfPropertyVM model)
        {
            DistributionOfProperty entity = model.Map<DistributionOfProperty, DistributionOfPropertyVM>();
            int Id = MyService.AddDistributionOfProperty(entity);
            return Id;
        }

        /// <summary>
        /// 更新财务分析
        /// </summary>
        /// <param name="model">财务分析实体</param>
        /// <returns>是否成功</returns>
        public bool UpdateDistributionOfProperty(DistributionOfPropertyVM model)
        {
            DistributionOfProperty entity = model.Map<DistributionOfProperty, DistributionOfPropertyVM>();
            bool result = MyService.UpdateDistributionOfProperty(entity);
            return result;
        }

        /// <summary>
        /// 获取财务分析
        /// </summary>
        /// <param name="Id">财务分析Id</param>
        /// <returns></returns>
        public DistributionOfPropertyVM GetDistributionOfPropertyByProposalId(int ProposalId)
        {
            var model = MyService.GetDistributionOfPropertyByProposalId(ProposalId);
            return model.Map<DistributionOfPropertyVM, DistributionOfProperty>();
        }

    }
}