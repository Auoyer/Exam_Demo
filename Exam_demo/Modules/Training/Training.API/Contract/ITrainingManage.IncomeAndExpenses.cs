using System.Collections.Generic;
using System.ServiceModel;

namespace Training.API
{
    public partial interface ITrainingManage
    {
        /// <summary>
        /// 新增收支储蓄页面
        /// </summary>
        /// <param name="model">>财务分析与班级关联表实体类</param>
        /// <returns></returns>
        [OperationContract]
        int AddTrainIncomeAndExpenses(IncomeAndExpenses model);

        /// <summary>
        /// 修改收支储蓄功能
        /// </summary>
        /// <param name="model">财务分析与与班级关联表实体类</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateTrainIncomeAndExpenses(IncomeAndExpenses model);

        /// <summary>
        /// 获取收支储蓄列表全部数据
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<IncomeAndExpenses> GetAllTrainIncomeAndExpenses(CustomFilter filter);

        /// <summary>
        /// 获取收支储蓄
        /// </summary>
        /// <param name="Id">案例Id</param>
        /// <returns></returns>
        [OperationContract]
        IncomeAndExpenses GetIncomeAndExpenses(int Id);

        /// <summary>
        /// 获取收支储蓄--根据建议号ID
        /// </summary>
        /// <param name="proposalId"></param>
        /// <returns></returns>
        [OperationContract]
        IncomeAndExpenses GetIncomeAndExpensesByProposalId(int proposalId);
        
    }
}
