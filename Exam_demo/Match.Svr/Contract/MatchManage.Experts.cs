using Match.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Svr;
using Utils;

namespace Match.Svr
{
    public partial class MatchManage
    {
        ExpertsDAL expertsDal = new ExpertsDAL();
        /// <summary>
        /// 得到专家列表
        /// </summary>        
        public List<Experts> GetExpertsList(int collegeId, int homePageId)
        {
            List<Experts> result = new List<Experts>();
            try
            {
                result = expertsDal.GetExpertsList(collegeId, homePageId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetExpertsList方法出错", ex);
            }
            return result;
        }


        /// <summary>
        /// 添加专家
        /// </summary>    
        public int AddExperts(Experts expertsModel)
        {
            int res = 0;
            try
            {
                res = expertsDal.Add(expertsModel);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddExperts方法出错", ex);
            }
            return res;
        }

        /// <summary>
        /// 更新专家
        /// </summary>      
        public bool UpdateExperts(Experts expertsModel)
        {
            bool result = false;
            try
            {
                result = expertsDal.Update(expertsModel);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateExperts方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Experts GetExpertsModel(int Id)
        {
            Experts expertsModel = new Experts();
            try
            {
                expertsModel = expertsDal.GetModel(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetExpertsModel方法出错", ex);
            }
            return expertsModel;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteExperts(int Id)
        {
            bool result = false;
            try
            {
                result = expertsDal.Delete(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DeleteExperts方法出错", ex);
            }
            return result;
        }
    }
}
