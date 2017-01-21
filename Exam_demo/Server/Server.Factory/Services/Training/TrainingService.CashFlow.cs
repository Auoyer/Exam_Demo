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
        /// 新增现金流量
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public int AddCashFlow(CashFlowVM model)
        {
            CashFlow entity = model.Map<CashFlow, CashFlowVM>();
            int Id = MyService.AddCashFlow(entity);
            return Id;
        }

        /// <summary>
        /// 查询现金流量2
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public CashFlowVM SelectCashFlowGetObj(int ProposalId)
        {          
            var model = MyService.SelectCashFlowGetObj(ProposalId);
            return model.Map<CashFlowVM, CashFlow>();
        }

        /// <summary>
        /// 修改现金流量
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public bool UpdateCashFolw(CashFlowVM model)
        {
            bool bo = false;
            CashFlow entity = model.Map<CashFlow, CashFlowVM>();
            bo = MyService.UpdateCashFolw(entity);
            return bo;
        }
    }
}
