using Match.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Match.Svr
{
    public partial class MatchManage
    {
        ActivityImageDal activityImageDal = new ActivityImageDal();
        /// <summary>
        /// 新增活动图片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddActiveImage(ActivityImage model)
        {
            int result = 0;
            try
            {
                result = activityImageDal.AddActiveImage(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddActiveImage方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 得到活动图片列表
        /// </summary>
        public List<ActivityImage> GetActivityImageList(int collegeId, int homePageId)
        {
            List<ActivityImage> result = new List<ActivityImage>();
            try
            {
                result = activityImageDal.GetActivityImageList(collegeId, homePageId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetActivityImageList方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 更新活动图片
        /// </summary>   
        public bool UpdateActivityImage(ActivityImage model)
        {
            bool result = false;
            try
            {
                result = activityImageDal.UpdateActivityImage(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateActivityImage方法出错", ex);
            }
            return result;
        }

         /// <summary>
        /// 更新活动图片
        /// </summary>   
        public bool BulkUpdateActivityImage(List<ActivityImage> modelList)
        {
            bool result = false;
            try
            {
                result = activityImageDal.BulkUpdateActivityImage(modelList);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("BulkUpdateActivityImage方法出错", ex);
            }
            return result;
        }

          /// <summary>
        /// 新增活动图片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool BulkAddActiveImage(List<ActivityImage> modelList)
        {

            bool result = false;
            try
            {
                result = activityImageDal.BulkAddActiveImage(modelList);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("BulkAddActiveImage方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 删除活动图片
        /// </summary>   
        public bool DeleteActivityImage(int id)
        {
            bool result = false;
            try
            {
                result = activityImageDal.DeleteActivityImage(id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DeleteActivityImage方法出错", ex);
            }
            return result; 
        }
    }
}
