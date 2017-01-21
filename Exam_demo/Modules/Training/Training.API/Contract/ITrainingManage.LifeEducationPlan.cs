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
        ///  新增教育规划
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [OperationContract]
        int AddEP(LifeEducationPlan filter);

        /// <summary>
        /// 查询教育规划
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        List<LifeEducationPlan> AccordingIdSelectEP(CustomFilter filter);

        /// <summary>
        /// 查询教育规划2
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        LifeEducationPlan SelectEPGetObj(int Id);

        /// <summary>
        /// 查询教育规划3
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        LifeEducationPlan AdoptProposalIdSelectEPGetObj(int ProposalId);


        /// <summary>
        /// 修改教育规划
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateEP(LifeEducationPlan model);
    }
}
