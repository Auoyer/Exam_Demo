using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;
using EmitMapper.MappingConfiguration;
using Training.API;
using VM;

namespace Server.Factory
{
    public partial class TrainingService : IService<ITrainingManage>
    {
        /// <summary>
        /// 保存考核结果
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddAssessmentResults(AssessmentResultsVM model)
        {
            AssessmentResults Model = model.Map<AssessmentResults, AssessmentResultsVM>();
            return MyService.AddAssessmentResults(Model);
        }

        /// <summary>
        /// 获取用户id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public AssessmentResultsVM GetAssessmentResultsModel(TrainSearch ts)
        {
            CustomFilter filter = new CustomFilter { Id = ts.Id,TrainExamId=ts.TrainExamId,UserId=ts.UserId };
            AssessmentResults model = MyService.GetAssessmentResultsModel(filter);
            AssessmentResultsVM result = model.Map<AssessmentResultsVM, AssessmentResults>();
            return result;
        }

        public bool UpdateAssessmentResults(AssessmentResultsVM model)
        {
            var Info = model.Map<AssessmentResults, AssessmentResultsVM>();
            return MyService.UpdateAssessmentResults(Info);
        }

        /// <summary>
        /// 根据考核Id获取未评分/已评分数量
        /// list[0]：未评分数量
        /// list[1]：已评分数量
        /// </summary>
        /// <param name="TrainExamId">考核Id</param>
        /// <returns></returns>
        public List<int> CountTrainExamStatus(int TrainExamId)
        {
            return MyService.CountTrainExamStatus(TrainExamId);
        }

        /// <summary>
        /// 根据用户和大赛ID获取成绩
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="competitionId"></param>
        /// <returns></returns>
        public AssessmentResultsVM GetARModel(int userId, int competitionId)
        {
            return MyService.GetARModel(userId, competitionId).Map<AssessmentResultsVM, AssessmentResults>();
        }

    }
}
