using Match.API;
using Match.Svr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Match.Svr
{
    public partial class MatchManage
    {
        AwardDal awardDal = new AwardDal();
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
       
        public Award GetAwardModel(int collegeId)
        {
            Award model = null;
            try
            {
                model = awardDal.GetAwardModel(collegeId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetAwardModel方法出错", ex);
            }

            return model;
        }


        /// <summary>
        /// 更新奖项设置
        /// </summary>
        /// <param name="awardList">奖项设置对象列表</param>
        /// <returns></returns>
        
        public bool UpdateAward(List<Award> awardList)
        {
            bool result = false;
            try
            {
                result = awardDal.UpdateAward(awardList);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateAward方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 更新奖项设置
        /// </summary>
        /// <param name="awardList">奖项设置对象</param>
        /// <returns></returns>

        public bool UpdateAwardModel(Award award)
        {
            bool result = false;
            try
            {
                result = awardDal.UpdateAward(award);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateAward方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 新增奖项设置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddAward(Award model)
        {
            int res = 0;
            try
            {
                res = awardDal.AddAward(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddAward方法出错", ex);
            }
            return res;
        }

        /// <summary>
        /// 删除奖项设置
        /// </summary>
        /// <param name="awardList">奖项设置对象</param>
        /// <returns></returns>

        public bool DeleteAward(int collegeId)
        {
            bool res = false;
            try
            {
                res = awardDal.DeleteAward(collegeId);
                
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DeleteAward方法出错", ex);            
            }
            return res;
        }

        /// <summary>
        /// 根据collegeId获取奖项设置列表
        /// </summary>
        /// <param name="collegeId"></param>
        /// <returns></returns>
        public List<Award> GetAwardList(int collegeId)
        {

            List<Award> result = new List<Award>();
            try
            {
                return awardDal.GetAwardList(collegeId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetAwardList方法出错", ex);
            }
            return result;
        }
    }
}
