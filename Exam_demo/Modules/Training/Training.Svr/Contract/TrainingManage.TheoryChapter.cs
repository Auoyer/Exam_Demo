using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.API;
using Utils;

namespace Training.Svr
{
    public partial class TrainingManage : ITrainingManage
    {
        private TheoryChapterDAL theoryChapterDAL = new TheoryChapterDAL();

        /// <summary>
        /// 章节列表
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<TheoryChapter> GetTheoryChapterList(CustomFilter filter, int pageIndex, int pageSize, out int totalCount)
        {
            totalCount = 0;
            List<TheoryChapter> list = new List<TheoryChapter>();
            try
            {
                if (pageIndex < 1 || pageSize < 1)
                {
                    return list;
                }
                if (filter == null)
                {
                    filter = new CustomFilter();
                }
                list = DBHelper.GetPageList<TheoryChapter>(pageIndex, pageSize, theoryChapterDAL.GetTheoryChapterPageParams(filter), out totalCount);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetTheoryChapterList章节列表方法出错", ex);

            }
            return list;
        }


        /// <summary>
        /// 章节列表
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<TheoryChapter> GetSelectQuestionsList(CustomFilter filter)
        { 
            List<TheoryChapter> list = new List<TheoryChapter>();
            try
            {
                list = theoryChapterDAL.GetChapterList(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetTheoryChapterList章节列表方法出错", ex);

            }
            return list;
        }

        /// <summary>
        /// 理论考核章节子类型列表
        /// </summary>
        /// <returns></returns>
        public List<TheoryQuestionType> GetTheoryQuestionTypeList(CustomFilter filter, int pageIndex, int pageSize, out int totalCount)
        {
            totalCount = 0;
            List<TheoryQuestionType> list = new List<TheoryQuestionType>();
            try
            {
                if (pageIndex < 1 || pageSize < 1)
                {
                    return list;
                }
                if (filter == null)
                {
                    filter = new CustomFilter();
                }
                TheoryQuestionTypeDAL dal = new TheoryQuestionTypeDAL();
                list = DBHelper.GetPageList<TheoryQuestionType>(pageIndex, pageSize, dal.GetTheoryQuestionTypePageParams(filter), out totalCount);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetTheoryQuestionTypeList章节列表方法出错", ex);

            }
            return list;
        }

        /// <summary>
        /// 理论考核章节子类型列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<TheoryQuestionType> GetTheoryQuestionTypeList1(CustomFilter filter)
        {
            List<TheoryQuestionType> list = new List<TheoryQuestionType>();
            try
            { 
                TheoryQuestionTypeDAL dal = new TheoryQuestionTypeDAL();
                list= dal.GetList(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetTheoryQuestionTypeList章节列表方法出错", ex);
                throw;
            }
            return list;
        }

        /// <summary>
        /// 理论考核评屏蔽列表
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<TheoryChapterHidden> GetTheoryChapterHiddenList(CustomFilter filter, int pageIndex, int pageSize, out int totalCount)
        {
            totalCount = 0;
            List<TheoryChapterHidden> list = new List<TheoryChapterHidden>();
            try
            {
                if (pageIndex < 1 || pageSize < 1)
                {
                    return list;
                }
                if (filter == null)
                {
                    filter = new CustomFilter();
                }
                TheoryChapterHiddenDAL dal = new TheoryChapterHiddenDAL();
                list = DBHelper.GetPageList<TheoryChapterHidden>(pageIndex, pageSize, dal.GetTheoryChapterHiddenPageParams(filter), out totalCount);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetTheoryChapterHiddenList 方法出错", ex);
            }
            return list;
        }

        /// <summary>
        /// 新增章节
        /// </summary>
        /// <param name="model">章节实体</param>
        /// <returns></returns>
        public TheoryChapter AddTheoryChapter(TheoryChapter model)
        {
            TheoryChapter result = null;
            try
            {
                result = theoryChapterDAL.AddCharpter(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddTheoryChapter新增章节方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 新增章节子题目类型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddTheoryQuestionType(TheoryQuestionType model)
        {
            int result = 0;
            try
            {
                TheoryQuestionTypeDAL dal = new TheoryQuestionTypeDAL();
                result = dal.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddTheoryQuestionType新增章节子题目类型方法出错", ex);
            }
            return result;
        }


        /// <summary>
        /// 编辑章节
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateTheoryChapter(TheoryChapter model)
        {
            bool result = false;
            try
            {
                result = theoryChapterDAL.UpdateTheoryChapter(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateTheoryChapter 编辑章节方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 删除章节
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteTheoryChapter(int id)
        {
            bool result = false;
            try
            {
                result = theoryChapterDAL.Delete(id);

            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DeleteTheoryChapter 删除章节方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 删除章节子题目类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteTheoryQuestionType(int id)
        {
            bool result = false;
            try
            {
                //教师自定义直接删除，系统内置存入TheoryChapterHidden表，与题目屏蔽一样,
                TheoryQuestionTypeDAL dal = new TheoryQuestionTypeDAL();
                result = dal.Delete(id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DeleteTheoryQuestionType 出错", ex);
            }
            return result;

        }

        /// <summary>
        /// 内置章节逻辑删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool TombstoneChapter(int id, int userId)
        {
            bool result = false;
            try
            {
                TheoryChapterHiddenDAL dal = new TheoryChapterHiddenDAL();
                result = dal.Add(new TheoryChapterHidden { TheoryChapterId = id, UserId = userId }) > 0;
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("TombstoneChapter 内置章节逻辑删除方法出错", ex);
            }
            return result;

        }


        /// <summary>
        /// 章节判重
        /// </summary>
        /// <param name="theoryChapterName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsExistTheoryChapter(string theoryChapterName, int id, int userId)
        {
            bool result = false;
            try
            {
                result = theoryChapterDAL.Exists(id, theoryChapterName, userId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("IsExistTheoryChapter 章节判重方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取章节实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public TheoryChapter GetTheoryChapter(int id)
        {
            TheoryChapter model = null;
            try
            {
                model = theoryChapterDAL.GetModel(id);
            }
            catch (Exception ex) { LogHelper.Log.WriteError("GetTheoryChapter获取章节实体方法出错", ex); }
            return model;
        }

        /// <summary>
        /// 获取章节的数量
        /// </summary>
        /// <returns></returns>
        public int GetTheoryChapterNum(int userId)
        {
            int result = 0;
            try
            {
                result = theoryChapterDAL.GetTheoryChapter(userId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetTheoryChapterNum获取章节的数量方法出错", ex);

            }
            return result;
        }
    }
}
