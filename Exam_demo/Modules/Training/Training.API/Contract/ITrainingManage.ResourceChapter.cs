using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Training.API
{
    public partial interface ITrainingManage
    {
        /// <summary>
        /// 资源章节列表
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<ResourceChapter> GetResourceChapterList(int userId);

        /// <summary>
        /// 新增资源章节
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        int AddResourceChapter(ResourceChapter model);

        /// <summary>
        /// 更新资源章节
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateResourceChapter(ResourceChapter model);

        /// <summary>
        /// 删除资源章节
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteResourceChapter(int Id);

        /// <summary>
        /// 内置章节逻辑删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract]
        bool TombstoneResourceChapter(int id, int userId);

        /// <summary>
        /// 获取资源章节
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [OperationContract]
        ResourceChapter GetResourceChapter(int Id);

        /// <summary>
        /// 获取资源章节数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract]
        int GetResourceChapterNum(int userId);

        /// <summary>
        /// 判断新增的名称是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [OperationContract]
        bool IsExistResourceChapter(int id, int userId, string name);
    }
}
