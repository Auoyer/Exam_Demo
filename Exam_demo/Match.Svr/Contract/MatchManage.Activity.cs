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
        private ActivityDal activityDal = new ActivityDal();
        /// <summary>
        /// 新增活动概况
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        public int AddActivities(Activities model)
        {
            int result = 0;
            try
            {
                result = activityDal.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddActivities方法出错", ex);
            }
            return result;
        }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>

        public Activities GetActivitiesModel(int collegeId)
        {
            Activities result = null;
            try
            {
                result = activityDal.GetModel(collegeId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetModel方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 更新活动概况
        /// </summary>
        /// <param name="model">活动概况实体对象</param>
        /// <returns></returns>

        public bool UpdateActivities(Activities model)
        {
            bool result = false;
            try
            {
                result = activityDal.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateActivities方法出错", ex);
            }
            return result;
        }
    }
}
