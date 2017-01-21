using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Training.API
{
    public partial interface ITrainingManage
    {
        /// <summary>
        ///  新增教育规划
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [OperationContract]
        int AddEPD(LifeEducationPlanDetail filter);

        /// <summary>
        ///  删除教育规划
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteEPD(int Id);

        /// <summary>
        /// 查询教育规划
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        List<LifeEducationPlanDetail> AccordingIdSelectEPD(CustomFilter filter);

        /// <summary>
        /// 查询教育规划2
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        LifeEducationPlanDetail SelectEPDGetObj(int Id);

        /// <summary>
        /// 修改教育规划
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateEPD(LifeEducationPlanDetail model);
    }
}
