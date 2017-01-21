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
        EntryAssessmentDAL EADAL = new EntryAssessmentDAL();
        public int AddEntryAssessment(EntryAssessment model)
        {
            int Result = 0;
            try
            {
                Result = EADAL.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddEntryAssessment方法出错", ex);
            }
            return Result;
        }

        public List<EntryAssessment> GetEntryAssessmentList(CustomFilter filter)
        {
            List<EntryAssessment> result = new List<EntryAssessment>();
            try
            {
                result = EADAL.GetList(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetEntryAssessmentList方法出错", ex);
            }
            return result;
        }


        /// <summary>
        /// 更新一个对象
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns> 
        public bool UpdateEntryAssessment(EntryAssessment model)
        {
            bool result = false;
            try
            {
                result = EADAL.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateEntryAssessment方法出错", ex);
            }
            return result;
        }
    }
}
