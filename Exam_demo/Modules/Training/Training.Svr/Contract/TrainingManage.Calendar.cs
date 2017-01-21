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
        private CalendarDAL calendarDAL = new CalendarDAL();

        /// <summary>
        /// 获取日程安排信息
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<Calendar> GetCalendarList(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount)
        {
            totalCount = 0;
            List<Calendar> result = new List<Calendar>();
            try
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    result = DBHelper.GetPageList<Calendar>(pageIndex.Value, pageSize.Value, calendarDAL.GetCalendarPageParams(filter), out totalCount);
                }
                else
                {
                    result = calendarDAL.GetList(filter);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetCalendarList方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 新增日程安排
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddCalendar(Calendar model)
        {
            int result = 0;
            try
            {
                result = calendarDAL.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddCalendar方法出错", ex);

            }
            return result;
        }

        /// <summary>
        /// 更新日程安排
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateCalendar(Calendar model)
        {
            bool result = false;
            try
            {
                result = calendarDAL.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateCalendar方法出错", ex);

            }
            return result;
        }

        /// <summary>
        /// 获取日程安排实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Calendar GetCalendar(int id)
        {
            Calendar result = new Calendar();
            try
            {
                result = calendarDAL.GetModel(id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetCalendar方法出错", ex);

            }
            return result;
        }

        /// <summary>
        /// 删除日程安排
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelCalendar(int id)
        {
            bool result = false;
            try
            {
                result = calendarDAL.Delete(id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetCalendarList方法出错", ex);

            }
            return result;
        }

        /// <summary>
        /// 删除日程安排
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ForCustomerIdDelCalendar(int StuCustomerId, int UserId)
        {
            bool result = false;
            try
            {
                result = calendarDAL.Delete2(StuCustomerId, UserId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("ForCustomerIdDelCalendar方法出错", ex);

            }
            return result;
        }
    }
}
