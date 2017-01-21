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
        /// 得到一个对象实体
        /// </summary>
        [OperationContract]
        Award GetAwardModel(int collegeId);

        /// <summary>
        /// 更新奖项设置
       /// </summary>
       /// <param name="awardList">奖项设置对象列表</param>
       /// <returns></returns>
        [OperationContract]
        bool UpdateAward(List<Award > awardList);

        /// <summary>
        /// 更新奖项设置
        /// </summary>
        /// <param name="awardList">奖项设置对象</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateAwardModel(Award award);

        /// <summary>
        /// 根据collegeId获取奖项设置列表
        /// </summary>
        /// <param name="collegeId"></param>
        /// <returns></returns>
        [OperationContract]
        List<Award> GetAwardList(int collegeId);

        /// <summary>
        /// 新增奖项设置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        int AddAward(Award model);

         /// <summary>
        /// 删除奖项设置
        /// </summary>
       
        /// <returns></returns>
        [OperationContract]
        bool DeleteAward(int collegeId);
        
    }
}
