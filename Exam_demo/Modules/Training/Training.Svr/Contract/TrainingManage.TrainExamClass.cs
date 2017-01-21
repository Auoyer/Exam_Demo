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
        private TrainExamClassDAL trainExamClass = new TrainExamClassDAL();
        /// <summary>
        /// 新增销售机会与班级关联表功能
        /// </summary>
        /// <param name="model">销售机会与班级关联表实体类</param>
        /// <returns></returns>
        public int AddTrainExamClass(TrainExamClass model)
        {
            int result = 0;
            try
            {
                result = trainExamClass.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddTrainExamClass方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 修改销售机会与班级关联表功能
        /// </summary>
        /// <param name="model">销售机会与班级关联表实体类</param>
        /// <returns></returns>
        public bool UpdateTrainExamClass(TrainExamClass model)
        {
            bool result = false;
            try
            {
                result = trainExamClass.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateTrainExamClass方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 删除销售机会与班级关联表功能
        /// </summary>
        /// <param name="Id">销售机会与班级关联表Id</param>
        /// <returns></returns>
        public bool DelTrainExamClass(int TrainExamId)
        {
            bool result = false;
            try
            {
                result = trainExamClass.Delete(TrainExamId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DelTrainExamClass方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取TrainExamClass列表
        /// </summary>
        /// <param name="filter">查询条件过滤</param>
        /// <returns></returns>
        public List<TrainExamClass> GetTrainExamClass(CustomFilter filter)
        {
            List<TrainExamClass> result = null;
            try
            {
                result = trainExamClass.GetList(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetTrainExamClass方法出错", ex);
            }
            return result;
        }


        /// <summary>
        /// 获取TrainExamClass对象
        /// </summary>
        /// <param name="TrainExamId"></param>
        /// <returns></returns>
        public TrainExamClass GetTrainExamClassObj(int TrainExamId)
        {
            TrainExamClass result = new TrainExamClass();
            try
            {
                result = trainExamClass.GetModel2(TrainExamId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetTrainExamClassObj方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取ClassIds集合
        /// </summary>
        /// <param name="filter">查询条件过滤</param>
        /// <returns></returns>
        public List<int> GetClassIds(CustomFilter filter)
        {
            List<int> result = null;
            try
            {
                result = trainExamClass.GetClassIds(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetClassIds方法出错", ex);
            }
            return result;
        }

    }
}
