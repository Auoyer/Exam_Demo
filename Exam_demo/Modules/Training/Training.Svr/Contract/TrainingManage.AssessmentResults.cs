using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Training.API;
using Utils;

namespace Training.Svr
{ 
    public partial class TrainingManage : ITrainingManage
    {
        AssessmentResultsDAL ARDAL = new AssessmentResultsDAL();
        public bool AddAssessmentResults(AssessmentResults model)
        {
            bool Result = false;
            try
            {
                int i = ARDAL.AddAssessmentResults(model);
                if (i > 0)
                    Result = true;
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddAssessmentResults方法出错", ex);
            }
            return Result;
        }

        /// <summary>
        /// 获取成绩列表
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public List<AssessmentResults> GetScoreResultsList(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount)
        {
            totalCount = 0;
            List<AssessmentResults> result = new List<AssessmentResults>();
            try
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    result = DBHelper.GetPageList<AssessmentResults>(pageIndex.Value, pageSize.Value, ARDAL.GetScoreResultsPageParams(filter), out totalCount);
                }
                else
                {
                    result = ARDAL.GetList(filter);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetCasePage方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 查询单个
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public AssessmentResults GetModelByUserId(int userId, int TrainExamId)
        {
            AssessmentResults result = new AssessmentResults();
            try
            {
                result = ARDAL.GetModelByUserId(userId, TrainExamId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetModelByUserId方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取学生所有考核点及分数
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalcount"></param>
        /// <returns></returns>
        public List<AssessmentExamPoint> GetAssessmentExamPointByUserId(CustomFilter filter, int? pageIndex, int? pageSize, out int total)
        {
            total =0;
            List<AssessmentExamPoint> result = new List<AssessmentExamPoint>();
            try
            {
                result = DBHelper.GetPageList<AssessmentExamPoint>(pageIndex.Value, pageSize.Value, ARDAL.GetAssessmentExamPointByUserId(filter), out total);
            }
            catch(Exception ex)
            {
                LogHelper.Log.WriteError("GetAssessmentExamPointByUserId方法出错", ex);
            }

            return result;
        }



        /// <summary>
        /// 获取一个对象
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public AssessmentResults GetAssessmentResultsModel(CustomFilter filter)
        { 
            AssessmentResults result = new AssessmentResults();
            try
            {
                result = ARDAL.GetModel(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetModel方法出错", ex);
            }
            return result;
        }


        /// <summary>
        /// 获取一个对象
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns> 
        public bool UpdateAssessmentResults(AssessmentResults model)
        {
            bool result = false;
            try
            {
                result = ARDAL.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateAssessmentResults方法出错", ex);
            }
            return result;
        }
         
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool IsExistsNoScore(int TrainExamId, int TrainExamStatus)
        {
            bool result = false;
            try
            {
                result = ARDAL.Exists(TrainExamId, TrainExamStatus);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("IsExistsNoScore方法出错", ex);
            }
            return result;
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
            List<int> result = new List<int>();
            try
            {
                result = ARDAL.CountTrainExamStatus(TrainExamId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("CountTrainExamStatus方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 根据用户和大赛ID获取成绩
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public AssessmentResults GetARModel(int userId, int competitionId)
        {
            AssessmentResults result = new AssessmentResults();
            try
            {
                result = ARDAL.GetARModel(userId, competitionId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetARModel方法出错", ex);
            }
            return result;
        }
    }
}
