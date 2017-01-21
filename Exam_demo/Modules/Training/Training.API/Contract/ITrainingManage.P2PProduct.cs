using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Training.API
{
    /// <summary>
    /// p2p产品
    /// </summary>
    public partial interface ITrainingManage
    {
        /// <summary>
        /// 批量新增。爬起数据入库
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        [OperationContract]
        int AddBlukP2PProduce(List<P2PProduct> models);


        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [OperationContract]
        List<P2PProduct> GetP2PProductList(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount);

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateP2PProductBluk(List<P2PProduct> models);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sourceIds"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteP2PProduct(List<string> sourceIds);

        /// <summary>
        /// 删除P2P重复数据
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool DeleteRepeatP2P();

    }
}
