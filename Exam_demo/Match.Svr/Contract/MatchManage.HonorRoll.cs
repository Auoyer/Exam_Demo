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
        HonorRollDAL honorRollDal = new HonorRollDAL();
        /// <summary>
        /// 获得荣誉榜数据列表
        /// </summary>
        public List<HonorRoll> GetHonorRollList(CustomFilter filter)
        {
            List<HonorRoll> result = new List<HonorRoll>();
            try
            {
                return honorRollDal.GetList(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetHonorRollList方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 新增荣誉
        /// </summary>
        /// <param name="model"></param>

        public int AddHonorRoll(HonorRoll model)
        {
            int res = 0;
            try
            {
                res = honorRollDal.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddHonorRoll方法出错", ex);
            }
            return res;
        }

        /// <summary>
        /// 更新荣誉
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>       
        public bool UpdateHonorRoll(HonorRoll model)
        {
            bool result = false;
            try
            {
                result = honorRollDal.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateHonorRoll方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 删除荣誉
        /// </summary>
        public bool DeleteHonorRoll(int id)
        {
            bool result = false;
            try
            {
                result = honorRollDal.Delete(id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DeleteHonorRoll方法出错", ex);
            }
            return result;
        }
    }
}
