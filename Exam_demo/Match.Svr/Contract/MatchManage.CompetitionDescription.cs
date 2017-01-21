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
        CompetitionDescDal dal = new CompetitionDescDal();
        /// <summary>
        /// 新增大赛说明
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        public  int AddCompetitionDescription(CompetitionDescription model)
        {
            int result = 0;
            try
            {
                result = dal.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddCompetitionDescription方法出错", ex);
            }
            return result;
        }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>

        public CompetitionDescription GetCompetitionDescriptionModel(int collegeId)
        {
            CompetitionDescription result = null;
            try
            {
                result = dal.GetModel(collegeId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetCompetitionDescriptionModel方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 更新大赛说明
        /// </summary>
        /// <param name="model">大赛说明实体对象</param>
        /// <returns></returns>

        public bool UpdateCompetitionDescription(CompetitionDescription model)
        {
            bool result = false;
            try
            {
                result = dal.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateCompetitionDescription方法出错", ex);
            }
            return result;
        }
    }
}
