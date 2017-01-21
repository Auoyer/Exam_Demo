using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Resource.API
{
    public partial interface IResourceManage
    {
        /// <summary>
        /// 新增操作指南
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        int AddResource(Resources model);

        /// <summary>
        /// 更新操作指南
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdResource(Resources model);

        /// <summary>
        /// 删除单个课程资源
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteResource(int Id);

        /// <summary>
        /// 得到课程资源列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [OperationContract]
        List<Resources> GetResourceList(CustomFilter filter);

        /// <summary>
        /// 删除章节时，删除教师自己上传的所有课程资源
        /// </summary>
        /// <param name="chapterId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteResourceByChapter(int chapterId, int userId);

        /// <summary>
        /// 获取章节下的资源数目
        /// </summary>
        /// <param name="chapterId"></param>
        /// <returns></returns>
        [OperationContract]
        int GetResourceNum(int chapterId);

        /// <summary>
        /// 更新转换状态
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateConvertStatus(string fileName, int status);

        /// <summary>
        /// 获取资源实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        Resources GetResourceModel(int id);

        /// <summary>
        /// 判断上传的资源名称是否存在
        /// </summary>
        /// <param name="chapterId"></param>
        /// <param name="userId"></param>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        [OperationContract]
        bool IsExists(int chapterId, int userId, string resourceName);

    }
}
