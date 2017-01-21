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
        /// 新增活动图片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        int AddActiveImage(ActivityImage model);

        /// <summary>
        /// 得到活动图片列表
        /// </summary>
        [OperationContract]
        List<ActivityImage> GetActivityImageList(int collegeId, int homePageId);       

        /// <summary>
        /// 更新活动图片
        /// </summary>     
      
        [OperationContract]
        bool UpdateActivityImage(ActivityImage model);

          /// <summary>
        /// 新增活动图片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// 
        [OperationContract]
         bool BulkAddActiveImage(List<ActivityImage> modelList);

          /// <summary>
        /// 更新活动图片
        /// </summary>   
        /// 
        [OperationContract]
         bool BulkUpdateActivityImage(List<ActivityImage> modelList);

        /// <summary>
        /// 删除活动图片
        /// </summary>   
        /// 
        [OperationContract]
         bool DeleteActivityImage(int id);
    }
}
