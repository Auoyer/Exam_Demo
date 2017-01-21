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
        private UserTimeSummaryDAL UserTimeSummary = new UserTimeSummaryDAL();

        /// <summary>
        /// 获取GetTrainExamDetail对象
        /// </summary>
        /// <param name="TrainExamId"></param>
        /// <returns></returns>
        public UserTimeSummary GetUserTimeSummaryObj(int UserId)
        {
            UserTimeSummary result = null;
            try
            {
                result = UserTimeSummary.GetModel(UserId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetUserTimeSummaryObj方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取GetTrainExamDetail对象
        /// </summary>
        /// <param name="TrainExamId"></param>
        /// <returns></returns>      
        public List<UserTimeSummary> GetUserTimeSummarylist(CustomFilter filter)
        {
            List<UserTimeSummary> result = null;
            try
            {
                result = UserTimeSummary.GetList(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetUserTimeSummarylist方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddUserTimeSummary(UserTimeSummary model)
        {
            int result = 0;
            try
            {
                result = UserTimeSummary.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddUserTimeSummary方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public bool UpdateUserTimeSummary(UserTimeSummary model)
        {
            bool result = false;
            try
            {
                result = UserTimeSummary.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateUserTimeSummary方法出错", ex);
            }
            return result;

        }
    }
}
