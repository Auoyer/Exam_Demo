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
        /// 新增活动概况
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        int AddActivities(Activities model);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        [OperationContract]
        Activities GetActivitiesModel(int collegeId);

        /// <summary>
        /// 更新活动概况
        /// </summary>
        /// <param name="model">活动概况实体对象</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateActivities(Activities model);
    }
}
