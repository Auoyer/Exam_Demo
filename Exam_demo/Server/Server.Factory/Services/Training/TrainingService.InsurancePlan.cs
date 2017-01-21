using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using Training.API;
using EmitMapper;
using EmitMapper.MappingConfiguration;
using VM;
using Utils;

namespace Server.Factory
{
    public partial class TrainingService : IService<ITrainingManage>
    {
       

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns>Id</returns>
        public int AddInsurancePlan(InsurancePlanVM model)
        {
            InsurancePlan entity = model.Map<InsurancePlan, InsurancePlanVM>();
            int Id = MyService.AddInsurancePlan(entity);
            return Id;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model">财务分析实体</param>
        /// <returns>是否成功</returns>
        public bool UpdateInsurancePlan(InsurancePlanVM model)
        {
            InsurancePlan entity = model.Map<InsurancePlan, InsurancePlanVM>();
            bool result = MyService.UpdateInsurancePlan(entity);
            return result;
            
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="Id">案例Id</param>
        /// <returns>Liability实体</returns>
        public InsurancePlanVM GetInsurancePlanById(int Id)
        {
            InsurancePlan result = MyService.GetInsurancePlanById(Id);
           return result.Map<InsurancePlanVM, InsurancePlan>();
           
        }

        /// <summary>
        /// 获取---根据建议书ID
        /// </summary>
        /// <param name="ProposalId"></param>
        /// <returns></returns>
        public InsurancePlanVM GetInsurancePlanByProposalId(int ProposalId)
        {
            InsurancePlan result = MyService.GetInsurancePlanByProposalId(ProposalId);
            return result.Map<InsurancePlanVM, InsurancePlan>();

        }


    }
}
