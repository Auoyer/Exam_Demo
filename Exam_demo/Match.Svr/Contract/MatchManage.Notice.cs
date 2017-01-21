using Match.API;
using Match.Svr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Match.Svr
{
    public partial class MatchManage
    {
        NoticeDal noticeDal = new NoticeDal();

        /// <summary>
        /// 获取公告列表
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<Notice> GetNoticeList(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount)
        {
            totalCount = 0;
            List<Notice> result = new List<Notice>();
            try
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    result = DBHelper.GetPageList<Notice>(pageIndex.Value, pageSize.Value, noticeDal.GetNoticePageParams(filter), out totalCount);
                }
                else
                {
                    result = noticeDal.GetList(filter);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetNoticeList方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 新增公告
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>       
        public int AddNotice(Notice model)
        {

            int result = 0;
            try
            {
                result = noticeDal.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddNotice方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>       
        public bool DeleteNotice(int id)
        {
            bool result = false;
            try
            {
                result = noticeDal.Delete(id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DeleteNotice方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取公告实体对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Notice GetNoticeModel(int id)
        {
            Notice model = null;
            try
            {
                model = noticeDal.GetModel(id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetNoticeModel方法出错", ex);
            }

            return model;
        }

        /// <summary>
        /// 获取大赛说明
        /// </summary>
        /// <param name="collegeId"></param>
        /// <returns></returns>
        public CompetitionDescription GetDescModel(int collegeId)
        {
            CompetitionDescription model = null;
            try
            {
                model = noticeDal.GetDescModel(collegeId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetDescModel方法出错", ex);
            }

            return model;
        }

         /// <summary>
        /// 根据大学的ID获得数据列表
        /// </summary>
        public List<Notice> GetNoticeListByCollegeId(int collegeId)
        {
          
            List<Notice> result = new List<Notice>();
            try
            {
                result = noticeDal.GetNoticeListByCollegeId(collegeId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetCollegeList方法出错", ex);
            }
            return result;
        }
    }
}
