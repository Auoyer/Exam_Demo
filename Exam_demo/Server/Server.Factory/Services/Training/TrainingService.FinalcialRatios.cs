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
        /// 新增财务比例分析
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddFinalcialRatios(FinancialRatiosVM model)
        {
            int result = 0;
            FinancialRatios entity = model.Map<FinancialRatios, FinancialRatiosVM>();
            result = MyService.AddFinalcialRatios(entity);
           
            return result;

        }

        /// <summary>
        /// 修改财务比例分析
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateFinalcialRatios(FinancialRatiosVM model)
        {
            bool bo = false;
            FinancialRatios entity = model.Map<FinancialRatios, FinancialRatiosVM>();
            bo = MyService.UpdateFinalcialRatios(entity);

            return bo;
        }

        /// <summary>
        /// 查询财务比例分析
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public FinancialRatiosVM SelectFinalcialRatiosGetObj(int ProposalId)
        {
            var model = MyService.SelectFinalcialRatiosGetObj(ProposalId);
            return model.Map<FinancialRatiosVM, FinancialRatios>();

        }
    }
}
