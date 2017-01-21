using System.Collections.Generic;
using System.ServiceModel;

namespace Training.API
{
    public partial interface ITrainingManage
    {
        /// <summary>
        /// 获取案例分页列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页(可为空,为空时返回所有数据)</param>
        /// <param name="pageSize">每页页数(可为空,为空时返回所有数据)</param>
        /// <param name="totalCount">总数量</param>
        /// <returns></returns>
        [OperationContract]
        List<Case> GetCasePage(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount);

        /// <summary>
        /// 获取案例
        /// </summary>
        /// <param name="Id">案例Id</param>
        /// <returns></returns>
        [OperationContract]
        Case GetCase(int Id);

        /// <summary>
        /// 获取案例(根据理财类型)
        /// </summary>
        /// <param name="FinancialTypeId">理财类型Id</param>
        /// <returns></returns>
        [OperationContract]
        Case GetCase2(int FinancialTypeId);

        /// <summary>
        /// 新增案例
        /// </summary>
        /// <param name="model">案例实体</param>
        /// <returns>Id</returns>
        [OperationContract]
        int AddCase(Case model);

        /// <summary>
        /// 更新案例
        /// </summary>
        /// <param name="model">案例实体</param>
        /// <returns>是否成功</returns>
        [OperationContract]
        bool UpdateCase(Case model);

        /// <summary>
        /// 删除案例
        /// </summary>
        /// <param name="Id">案例Id</param>
        /// <returns>是否成功</returns>
        [OperationContract]
        bool DeleteCase(int Id);

        /// <summary>
        /// 屏蔽案例
        /// </summary>
        /// <param name="Id">案例Id</param>
        /// <param name="userId">用户Id</param>
        /// <returns>是否成功</returns>
        [OperationContract]
        bool HiddenCase(int Id, int userId);

        /// <summary>
        /// 校验是否重复
        /// </summary>
        /// <param name="CaseId">案例主键</param>
        /// <param name="IDNum">身份证号</param>
        /// <returns>返回值为true:不存在重复</returns>
        [OperationContract]
        bool CheckRepeat(int CaseId, string IDNum);


        /// <summary>
        /// 检查案列是否被用在未发布的销售机会/实训中，编辑和删除前需要判断
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="status"></param>
        /// <param name="caseId"></param>
        /// <returns></returns>
        [OperationContract]
        int CheckCaseByUsed(int userId, int status, int caseId);

        /// <summary>
        /// 检查自定义案列是否被用在竞赛中
        /// </summary>
        /// <param name="status"></param>
        /// <param name="caseId"></param>
        /// <returns></returns>
        [OperationContract]
        int CheckCaseInMatch(int status, int caseId);

        /// <summary>
        /// 修改案例状态（屏蔽，发布，删除）
        /// </summary>
        /// <param name="caseId">案例Id</param>
        /// <param name="type">操作类型</param>
        /// <returns>是否成功</returns>
        [OperationContract]
        bool ChangeCaseStatus(int caseId, int type);

        /// <summary>
        /// 修改案例查看状态
        /// </summary>
        /// <param name="caseId">案例Id</param>
        /// <returns>是否成功</returns>
        [OperationContract]
        bool ChangeCaseViewStatus(int caseId);
    }
}
