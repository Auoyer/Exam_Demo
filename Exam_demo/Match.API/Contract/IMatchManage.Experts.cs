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
        /// 得到专家列表
        /// </summary>
        /// 
        [OperationContract]
        List<Experts> GetExpertsList(int collegeId, int homePageId);


        /// <summary>
        /// 添加专家
        /// </summary>      
        [OperationContract]
        int AddExperts(Experts expertsModel);

        /// <summary>
        /// 更新专家
        /// </summary>      
        [OperationContract]
        bool UpdateExperts(Experts expertsModel);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// 
        [OperationContract]
        Experts GetExpertsModel(int Id);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// 
        [OperationContract]
        bool DeleteExperts(int Id);
    }
}
