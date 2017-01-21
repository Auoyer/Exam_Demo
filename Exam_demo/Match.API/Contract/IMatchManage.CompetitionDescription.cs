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
        /// 新增大赛说明
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        int AddCompetitionDescription(CompetitionDescription model);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        [OperationContract]
        CompetitionDescription GetCompetitionDescriptionModel(int collegeId);

        /// <summary>
        /// 更新大赛说明
        /// </summary>
        /// <param name="model">大赛说明实体对象</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateCompetitionDescription(CompetitionDescription model);
    }
}
