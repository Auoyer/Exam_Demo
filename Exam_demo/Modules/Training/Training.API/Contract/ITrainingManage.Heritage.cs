using System.Collections.Generic;
using System.ServiceModel;

namespace Training.API
{
    public partial interface ITrainingManage
    {
        /// <summary>
        /// 获取财产传承实体
        /// </summary>
        /// <param name="ProposalId">计划书Id</param>
        /// <returns></returns>
        [OperationContract]
        Heritage GetHeritage(int ProposalId);

        /// <summary>
        ///  新增财产传承
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [OperationContract]
        int AddHeritage(Heritage filter);

        /// <summary>
        /// 查询财产传承
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        List<Heritage> AccordingIdSelectHeritage(CustomFilter filter);

        /// <summary>
        /// 查询财产传承2
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        Heritage SelectHeritageGetObj(int Id);

       
        /// <summary>
        /// 修改财产传承
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateHeritage(Heritage model);
    }
}
