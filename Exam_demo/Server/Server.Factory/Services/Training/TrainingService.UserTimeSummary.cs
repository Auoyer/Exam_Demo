using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.API;
using VM;

namespace Server.Factory
{
    public partial class TrainingService : IService<ITrainingManage>
    {

        /// <summary>
        /// 获取TrainExamDetail列表
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <returns></returns>
        public List<UserTimeSummaryVM> GetUserTimeSummarylist(TrainSearch search)
        {
            CustomFilter filter = new CustomFilter
            {
                TrainExamId = search.TrainExamId,
                ExamPointId = search.ExamPointId,
                UserId=search.UserId
            };
            var list = MyService.GetUserTimeSummarylist(filter);
            return list.MapList<UserTimeSummaryVM, UserTimeSummary>();
        }

        /// <summary>
        /// 新增销售机会与考核点关联表功能
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddUserTimeSummary(UserTimeSummaryVM model)
        {
            return MyService.AddUserTimeSummary(model.Map<UserTimeSummary, UserTimeSummaryVM>());
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public bool UpdateUserTimeSummary(UserTimeSummaryVM AnserList)
        {
            return MyService.UpdateUserTimeSummary(AnserList.Map<UserTimeSummary, UserTimeSummaryVM>());
        }
    }
}
