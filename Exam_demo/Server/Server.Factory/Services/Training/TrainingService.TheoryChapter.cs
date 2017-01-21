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
        #region Get

        /// <summary>
        /// 获取所有章节信息
        /// </summary>
        /// <returns></returns>
        public List<TheoryChapterVM> GetAllCharpters()
        {
            List<TheoryChapterVM> rtnValue = new List<TheoryChapterVM>();

            int pageLength = 500;
            int totalCount = 0;
            var curPage = MyService.GetTheoryChapterList(null, 1, pageLength, out totalCount);
            if (curPage != null && curPage.Count > 0)
            {
                rtnValue.AddRange(curPage.MapList<TheoryChapterVM, TheoryChapter>());

                int remainder = totalCount % 500;
                int pageCount = remainder == 0 ? totalCount / 500 : (totalCount - remainder) / 500 + 1;
                for (int i = 2; i <= remainder; i++)
                {
                    curPage = MyService.GetTheoryChapterList(null, i, pageLength, out totalCount);
                    if (curPage != null && curPage.Count > 0)
                    {
                        rtnValue.AddRange(curPage.MapList<TheoryChapterVM, TheoryChapter>());
                    }
                }
            }
            return rtnValue;
        }

        /// <summary>
        /// 获取所有章节子类型信息
        /// </summary>
        /// <returns></returns>
        public List<TheoryQuestionTypeVM> GetAllQuestionType()
        {
            List<TheoryQuestionTypeVM> rtnValue = new List<TheoryQuestionTypeVM>();

            int pageLength = 500;
            int totalCount = 0;
            var curPage = MyService.GetTheoryQuestionTypeList(null, 1, pageLength, out totalCount);
            if (curPage != null && curPage.Count > 0)
            {
                rtnValue.AddRange(curPage.MapList<TheoryQuestionTypeVM, TheoryQuestionType>());

                int remainder = totalCount % 500;
                int pageCount = remainder == 0 ? totalCount / 500 : (totalCount - remainder) / 500 + 1;
                for (int i = 2; i <= remainder; i++)
                {
                    curPage = MyService.GetTheoryQuestionTypeList(null, i, pageLength, out totalCount);
                    if (curPage != null && curPage.Count > 0)
                    {
                        rtnValue.AddRange(curPage.MapList<TheoryQuestionTypeVM, TheoryQuestionType>());
                    }
                }
            }
            return rtnValue;
        }

        /// <summary>
        /// 获取章节
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<TheoryQuestionTypeVM> GetTheoryQuestionTypeList1(TrainSearch search)
        {
            CustomFilter filter=new CustomFilter()
            {
                KeyField=search.KeyField,
                ChapterId=search.CharpterID,
                Score=search.Score,
                UserId2=search.UserId2,
                CollegeId = search.CollegeId
            };
            var list=  MyService.GetTheoryQuestionTypeList1(filter);

            return list.MapList<TheoryQuestionTypeVM, TheoryQuestionType>();
        }
        #endregion

        #region Edit

        /// <summary>
        /// 获取章节信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TheoryChapterVM GetTheoryChapter(int id)
        {
            var model = MyService.GetTheoryChapter(id);
            return model.Map<TheoryChapterVM, TheoryChapter>();

        }

        /// <summary> 
        /// 新增章节
        /// </summary>
        /// <param name="modelVM"></param>
        /// <returns></returns>
        public TheoryChapterVM AddTheoryChapter(TheoryChapterVM modelVM)
        {
            TheoryChapterVM result = null;
            var model = modelVM.Map<TheoryChapter, TheoryChapterVM>();
            result = MyService.AddTheoryChapter(model).Map<TheoryChapterVM, TheoryChapter>();
            return result;
        }

        /// <summary>
        /// 删除章节(物理删除)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collegeId"></param>
        /// <returns></returns>
        public bool DeleteTheoryChapter(TheoryChapterVM model)
        {
            bool result = false;
            result = MyService.DeleteTheoryChapter(model.Id);
            return result;
        }

        /// <summary>
        /// 更新章节名称
        /// </summary>
        /// <param name="modelVM"></param>
        /// <returns></returns>
        public bool UpdateTheoryChapter(TheoryChapterVM modelVM)
        {
            bool result = false;
            var model = modelVM.Map<TheoryChapter, TheoryChapterVM>();
            result = MyService.UpdateTheoryChapter(model);
            return result;
        }

        #endregion
    }
}
