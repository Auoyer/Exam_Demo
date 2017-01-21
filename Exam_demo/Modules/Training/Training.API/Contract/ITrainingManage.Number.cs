using System.Collections.Generic;
using System.ServiceModel;

namespace Training.API
{
    public partial interface ITrainingManage
    {
        /// <summary>
        /// 获取最新编号
        /// </summary>
        /// <param name="numtype">编号类型</param>
        /// <returns>编号实体</returns>
        [OperationContract]
        Number GetNumber(int numtype);

        /// <summary>
        /// 新增编号
        /// </summary>
        /// <param name="model">编号实体</param>
        /// <returns>新增编号Id</returns>
        [OperationContract]
        int AddNumber(Number model);

        /// <summary>
        /// 更新编号
        /// </summary>
        /// <param name="model">编号实体</param>
        /// <returns>更新是否成功</returns>
        [OperationContract]
        bool UpdateNumber(Number model);
    }
}
