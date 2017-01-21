using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Match.API
{
    public partial interface IMatchManage
    {
        /// <summary>
        /// 获取公告列表
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<Notice> GetNoticeList(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount);

        /// <summary>
        /// 新增公告
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        int AddNotice(Notice model);

        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteNotice(int id);

        [OperationContract]
        Notice GetNoticeModel(int id);

        /// <summary>
        /// 获取大赛说明
        /// </summary>
        /// <param name="collegeId"></param>
        /// <returns></returns>
        [OperationContract]
        CompetitionDescription GetDescModel(int collegeId);

         /// <summary>
        /// 根据大学的ID获得数据列表
        /// </summary>
        /// 
        [OperationContract]
        List<Notice> GetNoticeListByCollegeId(int collegeId);
    }
}
