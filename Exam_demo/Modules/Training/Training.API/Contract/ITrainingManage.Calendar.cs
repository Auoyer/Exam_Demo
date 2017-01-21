using System.Collections.Generic;
using System.ServiceModel;


namespace Training.API
{
    public partial interface ITrainingManage
    {
      /// <summary>
      /// 获取日程安排信息
      /// </summary>
      /// <param name="filter"></param>
      /// <param name="pageIndex"></param>
      /// <param name="pageSize"></param>
      /// <param name="totalCount"></param>
      /// <returns></returns>
      [OperationContract]
      List<Calendar> GetCalendarList(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount);

      /// <summary>
      /// 新增日程安排
      /// </summary>
      /// <param name="model"></param>
      /// <returns></returns>
      [OperationContract]
      int AddCalendar(Calendar model);

      /// <summary>
      /// 更新日程安排
      /// </summary>
      /// <param name="model"></param>
      /// <returns></returns>
      [OperationContract]
      bool UpdateCalendar(Calendar model);

      /// <summary>
      /// 获取日程安排实体
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      [OperationContract]
      Calendar GetCalendar(int id);

      /// <summary>
      /// 删除日程安排
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      [OperationContract]
      bool DelCalendar(int id);

      /// <summary>
      /// 删除日程安排
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      [OperationContract]
      bool ForCustomerIdDelCalendar(int StuCustomerId, int UserId);
    }
}
