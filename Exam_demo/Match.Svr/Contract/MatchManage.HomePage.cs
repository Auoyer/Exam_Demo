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
        HomePageDal homeDal=new HomePageDal();
        /// <summary>
        /// 新增首页信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        
        public int AddHomePage(HomePage model)
        {
            int result = 0;
            try
            {
                result = homeDal.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddHomePage方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>

        public HomePage GetHomePageModel(int collegeId)
        {
            HomePage result = null;
            try
            {
                result = homeDal.GetModel(collegeId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetHomePageModel方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 更新首页信息
        /// </summary>
        /// <param name="model">大赛说明实体对象</param>
        /// <returns></returns>

        public bool UpdateHomePage(HomePage model)
        {
            bool result = false;
            try
            {
                result = homeDal.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateHomePage方法出错", ex);
            }
            return result;
        }

         /// <summary>
        /// 更新首页-大赛介绍
        /// </summary>       
        public bool UpdateCompetitionIntroduction(string competitionIntroduction, int id)
        {
            bool result = false;
            try
            {
                result = homeDal.UpdateCompetitionIntroduction(competitionIntroduction,id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateCompetitionIntroduction方法出错", ex);
            }
            return result;
        }

         /// <summary>
        /// 更新首页-图片标题（包括官网欢迎图片的标题文字、活动图片管理、二维码图片）
        /// </summary>
        /// <returns></returns>
        public bool UpdateImageTitle(HomePage model)
        {
            bool result = false;
            try
            {
                result = homeDal.UpdateImageTitle(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateImageTitle方法出错", ex);
            }
            return result;
        }

         /// <summary>
        /// 更新首页-联系我们
        /// </summary>      
        public bool UpdateContactUS(HomePage model)
        {
            bool result = false;
            try
            {
                result = homeDal.UpdateContactUS(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateContactUS方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 更新首页-活动日程
        /// </summary>      
        public bool UpdateActivitySchedule(HomePage model)
        {
            bool result = false;
            try
            {
                result = homeDal.UpdateActivitySchedule(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateActivitySchedule方法出错", ex);
            }
            return result;
        }
    }
}
