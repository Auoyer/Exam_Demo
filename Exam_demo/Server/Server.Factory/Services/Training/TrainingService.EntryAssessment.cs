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
        public int AddEntryAssessment(EntryAssessmentVM model)
        {
            EntryAssessment Model = model.Map<EntryAssessment, EntryAssessmentVM>();
            return MyService.AddEntryAssessment(Model);
        }

        /// <summary>
        /// 获取成绩列表
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public List<EntryAssessmentVM> GetEntryAssessmentList(TrainSearch search)
        {
            CustomFilter filter = new CustomFilter
            {
                UserId = search.UserId,
                TrainExamId = search.TrainExamId
            };
            return MyService.GetEntryAssessmentList(filter).MapList<EntryAssessmentVM, EntryAssessment>();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateEntryAssessment(EntryAssessmentVM model)
        {
            var Info = model.Map<EntryAssessment, EntryAssessmentVM>();
            return MyService.UpdateEntryAssessment(Info);
        }
    }
}
