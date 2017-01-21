using System.Collections.Generic;
using System.ServiceModel;

namespace Training.API
{
    public partial interface ITrainingManage
    {
        /// <summary>
        /// 新增销财务分析页面
        /// </summary>
        /// <param name="model">>财务分析与班级关联表实体类</param>
        /// <returns></returns>
        [OperationContract]
        int AddTrainLiability(Liability model);

        /// <summary>
        /// 修改财务分析
        /// </summary>
        /// <param name="model">财务分析关联表实体类</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateTrainLiability(Liability model);

        /// <summary>
        /// 获取财务分析列表全部数据
        /// </summary>
        /// <param name="filter">查询条件</param>
        [OperationContract]
        List<Liability> GetLiabilityList(CustomFilter filter);

        /// <summary>
        /// 获取财务分析列表
        /// </summary>
        /// <param name="Id">案例Id</param>
        /// <returns></returns>
        [OperationContract]
        Liability GetLiability(int Id);
        /// <summary>
        /// 获取财务分析列表--根据建议号
        /// </summary>
        /// <param name="proposalId"></param>
        /// <returns></returns>
        [OperationContract]
        Liability GetLiabilityByProposalId(int proposalId);
    }

}
