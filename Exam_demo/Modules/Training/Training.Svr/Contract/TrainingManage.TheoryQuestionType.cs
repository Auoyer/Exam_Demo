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

        private TheoryQuestionTypeDAL TheoryQuestionType = new TheoryQuestionTypeDAL();

        /// <summary>
        /// 获取列表全部数据
        /// </summary>
        /// <returns></returns>
        public List<TheoryQuestionType> GetTheoryQuestionTypelist(CustomFilter model)
        {
            List<TheoryQuestionType> result = null;
            try
            {
                result = TheoryQuestionType.GetList(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetTheoryQuestionTypeList方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public TheoryQuestionType GetTheoryQuestionTypes(int Id)
        {
            TheoryQuestionType result = null;
            try
            {
                result = TheoryQuestionType.GetModel(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetTheoryQuestionTypes方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 删除章节子题目类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteTheoryQuestionTypes(int TheoryChapterId)
        {
            bool result = false;
            try
            {

                result = TheoryQuestionType.Delete2(TheoryChapterId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DeleteTheoryQuestionTypes 出错", ex);
            }
            return result;

        }

        /// <summary>
        /// 修改题目类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateTheoryQuestionTypes(TheoryQuestionType model)
        {
            bool result = false;
            try
            {

                result = TheoryQuestionType.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateTheoryQuestionTypes 出错", ex);
            }
            return result;

        }
    }
}
