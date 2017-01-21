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
        private ResourceChapterDAL resourceChapterDAL = new ResourceChapterDAL();

        /// <summary>
        /// 资源章节列表
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<ResourceChapter> GetResourceChapterList(int userId)
        {
            
            List<ResourceChapter> list = new List<ResourceChapter>();
            try
            {
                list = resourceChapterDAL.GetResourceChapterList(userId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetResourceChapterList章节列表方法出错", ex);
            }
            return list;
        }

        /// <summary>
        /// 新增资源章节
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddResourceChapter(ResourceChapter model)
        {
            int result = 0;
            try
            {
                result = resourceChapterDAL.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddResourceChapter章节列表方法出错", ex);
            }

            return result;
        }

        /// <summary>
        ///  更新资源章节
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateResourceChapter(ResourceChapter model)
        {
            bool result = false;
            try
            {
                result = resourceChapterDAL.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateResourceChapter章节列表方法出错", ex);

            }
            return result;
        }

        /// <summary>
        /// 删除资源章节
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool DeleteResourceChapter(int Id)
        {
            bool result = false;
            try
            {
                result = resourceChapterDAL.Delete(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DeleteResourceChapter章节列表方法出错", ex);

            }
            return result;
        }

        /// <summary>
        /// 内置章节逻辑删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool TombstoneResourceChapter(int id, int userId)
        {
            bool result = false;
            try
            {
                result = resourceChapterDAL.TombstoneResourceChapter(id, userId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("TombstoneResourceChapter章节列表方法出错", ex);

            }
            return result;
        }

        /// <summary>
        /// 获取资源章节
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResourceChapter GetResourceChapter(int Id)
        {
            ResourceChapter result = new ResourceChapter();
            try
            {
                result = resourceChapterDAL.GetModel(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetResourceChapter章节列表方法出错", ex);

            }
            return result;
        }

        /// <summary>
        /// 获取资源章节数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetResourceChapterNum(int userId)
        {
            int result = 0;
            try
            {
                result = resourceChapterDAL.GetResourceChapterNum(userId);

            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateResourceChapter章节列表方法出错", ex);

            }
            return result;
        }

        /// <summary>
        /// 判重
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExistResourceChapter(int id, int userId, string name)
        {
            bool result = false;
            try
            {
                result = resourceChapterDAL.Exists(id,name,userId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateResourceChapter章节列表方法出错", ex);
            }
            return result;
        }
    }
}
