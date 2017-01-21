using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;
using EmitMapper.MappingConfiguration;
using Training.API;
using VM;

namespace Server.Factory
{
    public partial class TrainingService : IService<ITrainingManage>
    {
        /// <summary>
        /// 新增税收筹划
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public int AddTaxPlan(TaxPlanVM model)
        {
            TaxPlan entity = model.Map<TaxPlan, TaxPlanVM>();
            int Id = MyService.AddTaxPlan(entity);
            return Id;
        }

        /// <summary>
        /// 查询税收筹划3
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public TaxPlanVM AdoptProposalIdSelectTaxPlanGetObj(int ProposalId)
        {
            var model = MyService.AdoptProposalIdSelectTaxPlanGetObj(ProposalId);
            return model.Map<TaxPlanVM, TaxPlan>();
        }

        /// <summary>
        /// 修改税收筹划
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public bool UpdateTaxPlan(TaxPlanVM model)
        {

            bool bo = false;
            TaxPlan entity = model.Map<TaxPlan, TaxPlanVM>();
            bo = MyService.UpdateTaxPlan(entity);
            return bo;
        }
    }
}
