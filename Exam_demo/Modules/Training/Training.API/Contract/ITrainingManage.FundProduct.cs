using System.Collections.Generic;
using System.ServiceModel;

namespace Training.API
{
    public partial interface ITrainingManage
    {
        /// <summary>
        /// 获取基金分页列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页(可为空,为空时返回所有数据)</param>
        /// <param name="pageSize">每页页数(可为空,为空时返回所有数据)</param>
        /// <param name="totalCount">总数量</param>
        /// <returns></returns>
        [OperationContract]
        List<FundProduct> GetFundProductPage(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount);

        /// <summary>
        /// 获取基金产品详细信息
        /// </summary>
        /// <param name="Id">基金Id</param>
        /// <returns></returns>
        [OperationContract]
        FundProduct GetFundProduct(int Id);

    }
}
