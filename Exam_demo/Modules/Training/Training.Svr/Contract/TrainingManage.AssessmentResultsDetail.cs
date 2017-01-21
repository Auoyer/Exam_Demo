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
        AssessmentResultsDetailDAL ARDal = new AssessmentResultsDetailDAL();

        public List<AssessmentResultsDetail> GetAssessmentResultsDetailList(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount)
        {
            totalCount = 0;
            List<AssessmentResultsDetail> result = new List<AssessmentResultsDetail>();
            try
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    result = DBHelper.GetPageList<AssessmentResultsDetail>(pageIndex.Value, pageSize.Value, ARDal.GetScoreResultsPageParams(filter), out totalCount);
                }
                else
                {
                    result = ARDal.GetList(filter);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetAssessmentResultsDetailList方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取销售机会/实训考核正确数
        /// </summary>
        /// <param name="pointArray">考核点Id集合</param>
        /// <param name="trainId">考核Id</param>
        /// <param name="classId">班级Id，为空时查全部</param>
        /// <param name="status">结果：正确or错误</param>
        /// <returns></returns>
        public Dictionary<int, int> CountRight(List<int> pointArray, int trainId, int? classId, int status)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();
            try
            {
                result = ARDal.CountStatus(pointArray, trainId, classId, status).GroupBy(l => l.AssessmentPoint).ToDictionary(m => m.Key, n => n.FirstOrDefault().Status);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("CountRight方法出错", ex);
            }
            return result;
        }


        /// <summary>
        /// 获取考核结果 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<AssessmentResultsDetail> GetExamResultScore(CustomFilter filter)
        { 
            List<AssessmentResultsDetail> Result = new List<AssessmentResultsDetail>();
            try
            {
                Result = ARDal.GetList(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetExamResultScore方法出错", ex); 
            }
            return Result;
        }

        /// <summary>
        /// 添加考核结果
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddAssessmentResultsDetailInfo(AssessmentResultsDetail model)
        {
            int result = 0;
            try
            {
               result= ARDal.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddAssessmentResultsDetailInfo方法出错", ex);  
            }
            return result;
        }
        /// <summary>
        /// 添加考核结果
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateAssessmentResultsDetailInfo(AssessmentResultsDetail model)
        {
            bool result = false;
            try
            {
                result= ARDal.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateAssessmentResultsDetailInfo方法出错", ex);
            }
            return result;
        }
         
    }
}
