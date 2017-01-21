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
        /// 获取题型列表
        /// </summary>
        /// <returns></returns>
        public List<TheoryQuestionTypeVM> GetTheoryQuestionTypelist(TrainSearch search)
        {
            CustomFilter filter = new CustomFilter
            {
                   ChapterId = search.ChapterId
            };
            var list = MyService.GetTheoryQuestionTypelist(filter);
            List<TheoryQuestionTypeVM> rtnValue = list.MapList<TheoryQuestionTypeVM, TheoryQuestionType>();
            return rtnValue;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public TheoryQuestionTypeVM GetTheoryQuestionTypes(int Id)
        {
            var model = MyService.GetTheoryQuestionTypes(Id);
            return model.Map<TheoryQuestionTypeVM, TheoryQuestionType>();           
        }
    }
}
