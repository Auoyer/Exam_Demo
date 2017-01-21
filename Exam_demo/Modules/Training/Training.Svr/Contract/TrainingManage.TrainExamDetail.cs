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

        private TrainExamDetailDAL trainExamDetail = new TrainExamDetailDAL();
        /// <summary>
        /// 新增销售机会与考核点关联表功能
        /// </summary>
        /// <param name="model">销售机会与考核点关联表实体类</param>
        /// <returns></returns>
        public int AddTrainExamDetail(TrainExamDetail model)
        {
            int result = 0;
            try
            {
                result = trainExamDetail.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddTrainExamDetail方法出错", ex);
            }
            return result;
        }


        /// <summary>
        /// 修改销售机会与考核点关联表功能
        /// </summary>
        /// <param name="model">销售机会与考核点关联表实体类</param>
        /// <returns></returns>
        public bool UpdateTrainExamDetail(TrainExamDetail model)
        {
            bool result = false;
            try
            {
                result = trainExamDetail.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateTrainExamDetail方法出错", ex);
            }
            return result;
        
        }

        /// <summary>
        /// 修改销售机会答案
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateTrainExamDetail2(List<ExamPointAnswer> AnserList, int TrainExamId)
        {
            bool result = false;
            try
            {
                result = trainExamDetail.UpdateAnswer(AnserList, TrainExamId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("TrainExam方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 删除销售机会与考核点关联表功能
        /// </summary>
        /// <param name="Id">销售机会与考核点关联表ID</param>
        /// <returns></returns>
        public bool DelTrainExamDetail(int TrainExamId)
        {
            bool result = false;
            try
            {
                result = trainExamDetail.Delete(TrainExamId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DelTrainExamDetail方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取GetTrainExamDetail列表
        /// </summary>
        /// <param name="filter">查询条件过滤</param>
        /// <returns></returns>
        public List<TrainExamDetail> GetTrainExamDetail(CustomFilter filter)
        {
            List<TrainExamDetail> result = null;
            try
            {
                result = trainExamDetail.GetList(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetTrainExamDetail方法出错", ex);
            }
            return result;
        }
        /// <summary>
        /// 获取GetTrainExamDetail对象
        /// </summary>
        /// <param name="TrainExamId"></param>
        /// <returns></returns>
        public TrainExamDetail GetTrainExamDetailObj(int TrainExamId)
        {
            TrainExamDetail result = null;
            try
            {
                result = trainExamDetail.GetModel2(TrainExamId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetTrainExamDetailObj方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// ExamPointIds考核点集合获取
        /// </summary>
        /// <param name="filter">查询条件过滤</param>
        /// <returns></returns>
        public List<int> GetExamPointIds(CustomFilter filter)
        {
            List<int> result = null;
            try
            {
                result = trainExamDetail.GetExamPointIds(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetExamPointIds方法出错", ex);
            }
            return result;
        }
    }
}
