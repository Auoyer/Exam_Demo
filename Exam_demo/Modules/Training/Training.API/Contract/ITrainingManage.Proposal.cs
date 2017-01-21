using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Training.API
{
    public partial interface ITrainingManage
    {
        /// <summary>
        /// 新增建议书
        /// </summary>
        /// <param name="proposal">建议书实体</param>
        /// <returns></returns>
        [OperationContract]
        int AddProposal(Proposal proposal);

        /// <summary>
        /// 更新建议书
        /// </summary>
        /// <param name="proposal">建议书实体</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateProposal(Proposal proposal);

        /// <summary>
        /// 删除建议书(伪删)
        /// 会同步修改以下内容：
        /// 1.客户信息中的建议书状态与建议书数量
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteProposal(int Id);

        /// <summary>
        /// 删除对应所有的建议书Id数据表
        /// </summary>
        /// <param name="proposal">建议书实体</param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteAllProposalIds(int ProposalId);

        /// <summary>
        /// 更新建议书状态
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <param name="Status">建议书状态</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateProposalStatus(int Id, int Status);

        /// <summary>
        /// 更新建议书时间
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <param name="UpdateDate">更新时间</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateProposalDate(int Id, DateTime UpdateDate);

        /// <summary>
        /// 获取建议书列表
        /// </summary>
        /// <param name="model">通用查询条件</param>
        /// <returns></returns>
        [OperationContract]
        List<Proposal> proposalCustomerList(CustomFilter model);

        /// <summary>
        /// 获取建议书
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        Proposal GetProposal(int Id);
        /// <summary>
        /// 获取建议书
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        Proposal GetProposalInfo(CustomFilter filter);

        /// <summary>
        /// 根据客户Id统计有效（未提交、未审核、已审核）的建议书数量
        /// </summary>
        /// <param name="StuCustomerId">客户Id</param>
        /// <returns></returns>
        [OperationContract]
        int CountProposal(int StuCustomerId);

    }
}
