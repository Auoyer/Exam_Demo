using System.Collections.Generic;
using System.ServiceModel;

namespace Structure.API
{
    public partial interface IStructureManage
    {
        /// <summary>
        /// 获取总人数（为现有账号数量（不包括失效帐号））
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int GetTotalUserNum();

        /// <summary>
        /// 获取到期帐号数量
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int GetExpireAccountNum();

        /// <summary>
        /// 获取用户分页列表(祖恒)
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页(可为空,为空时返回所有数据)</param>
        /// <param name="pageSize">每页页数(可为空,为空时返回所有数据)</param>
        /// <param name="totalCount">总数量</param>
        /// <returns></returns>
        [OperationContract]
        List<UserInfo> GetUserInfoList(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount);

        /// <summary>
        /// 获取总人数（为现有账号数量（不包括失效帐号））(非超管)
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int GetTotalUserNum2(int collegeId);

        [OperationContract]
        bool UpdateUserView(int IsView, int id);
        
    }
}