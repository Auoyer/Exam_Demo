using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Resource.API;

namespace Resource.Svr
{
    public partial class ResourceManage : IResourceManage
    {
        private ResourceDAL resourceDAL = new ResourceDAL();
        /// <summary>
        /// 新增操作指南
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddResource(Resources model)
        {
            int result = 0;
            try { result = resourceDAL.Add(model); }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddResource方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 更新操作指南
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdResource(Resources model)
        {
            bool result = false;
            try { result = resourceDAL.Update(model); }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddResource方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 删除课程资源
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool DeleteResource(int Id)
        {
            bool result = false;
            try
            {
                result = resourceDAL.Delete(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DeleteResource方法出错", ex);
            }
            return result;
        }
        /// <summary>
        /// 获取题目列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<Resources> GetResourceList(CustomFilter filter)
        {
            List<Resources> result = new List<Resources>();
            try
            {
                result = resourceDAL.GetList(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetResourceList方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 根据章节和用户删除资源
        /// </summary>
        /// <param name="chapterId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteResourceByChapter(int chapterId, int userId)
        {
            bool result = false;
            try
            { result = resourceDAL.DeleteResourceByChapter(chapterId, userId); }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DeleteResourceByChapter方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取章节下课程资源数量
        /// </summary>
        /// <param name="chapterId"></param>
        /// <returns></returns>
        public int GetResourceNum(int chapterId)
        {
            int result = 0;
            try
            {
                result = resourceDAL.GetResourceNum(chapterId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetResourceNum方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 更新转换状态
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateConvertStatus(string fileName, int status)
        {
            bool result = false;
            try
            {
                result = resourceDAL.UpdateConvertStatus(fileName, status);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateConvertStatus方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取资源实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public Resources GetResourceModel(int id)
        {
            Resources model = new Resources();
            try
            {
                model = resourceDAL.GetModelByRole(id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetResourceModel方法出错", ex);
            }
            return model;
        }

        /// <summary>
        /// 判断资源是否存在
        /// </summary>
        /// <param name="chapterId"></param>
        /// <param name="userId"></param>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public bool IsExists(int chapterId, int userId, string resourceName)
        {
            bool result = false;
            try
            {
                result = resourceDAL.Exists(chapterId,userId,resourceName);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("IsExists方法出错", ex);
            }
            return result;

        }
    }
}
