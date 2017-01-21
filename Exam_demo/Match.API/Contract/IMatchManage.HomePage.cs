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
        /// 新增首页信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        int AddHomePage(HomePage model);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        [OperationContract]
        HomePage GetHomePageModel(int collegeId);

        /// <summary>
        /// 更新首页信息
        /// </summary>
        /// <param name="model">大赛说明实体对象</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateHomePage(HomePage model);

        /// <summary>
        /// 更新首页-大赛介绍
        /// </summary>       
        /// 
        [OperationContract]
        bool UpdateCompetitionIntroduction(string competitionIntroduction, int id);

         /// <summary>
        /// 更新首页-图片标题（包括官网欢迎图片的标题文字、活动图片管理、二维码图片）
        /// </summary>
        /// <returns></returns>
        /// 
        [OperationContract]
        bool UpdateImageTitle(HomePage model);

        /// <summary>
        /// 更新首页-联系我们
        /// </summary>      
        /// 
        [OperationContract]
        bool UpdateContactUS(HomePage model);

         /// <summary>
        /// 更新首页-活动日程
        /// </summary>     
        /// 
        [OperationContract]
        bool UpdateActivitySchedule(HomePage model);
    }
}
