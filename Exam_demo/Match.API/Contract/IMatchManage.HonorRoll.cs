using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Match.API
{
    public partial interface IMatchManage
    {
         /// <summary>
        /// 获得荣誉榜数据列表
        /// </summary>
        /// 
        [OperationContract]
        List<HonorRoll> GetHonorRollList(CustomFilter filter);

        /// <summary>
        /// 新增荣誉
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        int AddHonorRoll(HonorRoll model);

        /// <summary>
        /// 更新荣誉
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateHonorRoll(HonorRoll model);

        /// <summary>
        /// 删除荣誉
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteHonorRoll(int id);
    }
}
