using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Training.API
{
    public partial interface ITrainingManage
    {
        /// <summary>
        ///  新增税收筹划
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [OperationContract]
        int AddTaxPlan(TaxPlan filter);

        /// <summary>
        ///  删除税收筹划
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteTaxPlan(int Id);

        /// <summary>
        /// 查询税收筹划
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        List<TaxPlan> AccordingIdSelectTaxPlan(CustomFilter filter);

        /// <summary>
        /// 查询税收筹划2
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        TaxPlan SelectTaxPlanGetObj(int Id);

        /// <summary>
        /// 查询税收筹划3
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        TaxPlan AdoptProposalIdSelectTaxPlanGetObj(int ProposalId);

        /// <summary>
        /// 修改税收筹划
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateTaxPlan(TaxPlan model);
    }
}
