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
        ///  新增创业规划
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [OperationContract]
        int AddSUP(StartAnUndertakingPlan filter);

        /// <summary>
        ///  删除创业规划
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteSUP(int Id);

        /// <summary>
        /// 查询创业规划
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        List<StartAnUndertakingPlan> AccordingIdSelectSUP(CustomFilter filter);

        /// <summary>
        /// 查询创业规划2
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        StartAnUndertakingPlan SelectSUPGetObj(int Id);

        /// <summary>
        /// 查询创业规划3
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        StartAnUndertakingPlan SelectSUPProposalId(int ProposalId);

        /// <summary>
        /// 修改创业规划
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateSUP(StartAnUndertakingPlan model);
    }
}
