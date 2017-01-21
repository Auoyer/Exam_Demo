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
        /// 获取成绩列表
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public PagedList<AssessmentResultsDetailVM> GetAssessmentResultsDetailList(int Id, int pageIndex, int pageSize)
        {
            int totalCount = 0;
            CustomFilter filter = new CustomFilter
            {
                Id=Id
            };
            var list = MyService.GetAssessmentResultsDetailList(filter, pageIndex, pageSize, out totalCount);
            List<AssessmentResultsDetailVM> rtnValue = list.MapList<AssessmentResultsDetailVM, AssessmentResultsDetail>();
            return new PagedList<AssessmentResultsDetailVM>(rtnValue, pageIndex, pageSize, totalCount); 
        }
        /// <summary>
        /// 获取考核结果
        /// </summary>
        /// <param name="AssessmentResultsId"></param>
        /// <returns></returns>
        public List<AssessmentResultsDetailVM> GetExamResultScore(TrainSearch ts)
        {
            CustomFilter filter = new CustomFilter
            {
                Id = ts.Id,
                ExamPointType=ts.ExamPointType
            };
             List<AssessmentResultsDetail> Value= MyService.GetExamResultScore(filter);
             return Value.MapList<AssessmentResultsDetailVM, AssessmentResultsDetail>();
        }

        /// <summary>
        /// 添加考核结果
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddAssessmentResultsDetailInfo(AssessmentResultsDetailVM model)
        {
            AssessmentResultsDetail Model = model.Map<AssessmentResultsDetail, AssessmentResultsDetailVM>();
            return MyService.AddAssessmentResultsDetailInfo(Model);
        }

        /// <summary>
        /// 更新考核结果
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateAssessmentResultsDetailInfo(AssessmentResultsDetailVM model)
        {
            AssessmentResultsDetail Model = model.Map<AssessmentResultsDetail, AssessmentResultsDetailVM>();
            return MyService.UpdateAssessmentResultsDetailInfo(Model);
        }
    }
}
