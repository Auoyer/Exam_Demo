using Match.API;
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
        NewsDal newsDal = new NewsDal();

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ///        
        public News GetNewsModel(int id)
        {
            News model = null;
            try
            {
                model = newsDal.GetNewsModel(id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetNewsModel方法出错", ex);
            }

            return model;
        }

        /// <summary>
        /// 根据collegeId获取新闻列表
        /// </summary>
        ///         
        public List<News> GetNewsList(int collegeId)
        {
            List<News> result = new List<News>();
            try
            {
                return newsDal.GetNewsList(collegeId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetNewsList方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 更新新闻信息
        /// </summary>
        ///        
        public bool UpdateNews(News news)
        {
            bool result = false;
            try
            {
                result = newsDal.UpdateNews(news);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateNews方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 更新新闻的发布时间（为置顶方法使用）
        /// </summary>
        public bool UpdateNewsReleaseTime(News model)
        {
            bool result = false;
            try
            {
                result = newsDal.UpdateNewsReleaseTime(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateNewsReleaseTime方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 删除新闻信息
        /// </summary>     
        ///        
        public bool DeleteNews(int id)
        {
            bool res = false;
            try
            {
                res = newsDal.DeleteNews(id);

            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DeleteNews方法出错", ex);
            }
            return res;
        }

        /// <summary>
        /// 新增新闻信息
        /// </summary>
        ///         
        public int AddNews(News model)
        {
            int res = 0;
            try
            {
                res = newsDal.AddNews(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddNews方法出错", ex);
            }
            return res;
        }

        /// <summary>
        /// 获取最大的排序号码
        /// </summary>
        /// <param name="collegeId"></param>
        /// <returns></returns>
        public int GetMaxNum(int collegeId)
        {
            int res = 0;
            try
            {
                res = newsDal.GetMaxNum(collegeId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetMaxNum方法出错", ex);
            }
            return res;
        }

        /// <summary>
        /// 获取序号最小对应的实体对象
        /// </summary>
        /// <param name="collegeId"></param>
        /// <returns></returns>
        public News GetMaxNumModel(int collegeId)
        {
            News model = null;
            try
            {
                model = newsDal.GetMaxNumModel(collegeId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetMaxNumModel方法出错", ex);
            }

            return model;
        }

        /// <summary>
        /// 更新新闻信息
        /// </summary>
        public bool UpdateNewsNum(News curModel, News minModel)
        {
            bool res = false;
            try
            {
                res = newsDal.UpdateNewsNum(curModel, minModel);

            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateNewsNum方法出错", ex);
            }
            return res;
        }

        /// <summary>
        /// 隐藏新闻
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool HideNews(int id, bool isHidden)
        {
            bool res = false;
            try
            {
                res = newsDal.HideNews(id, isHidden);

            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("HideNews方法出错", ex);
            }
            return res;

        }

        /// <summary>
        /// 新闻列表分页
        /// </summary>
        /// <param name="collegeId"></param>
        /// <param name="title"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<News> GetNewsPageList(int collegeId, string title, int pageIndex, int pageSize)
        {
            List<News> result = new List<News>();
            try
            {
                return newsDal.GetNewsPageList(collegeId, title, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetNewsPageList方法出错", ex);
            }
            return result;
        }
    }
}
