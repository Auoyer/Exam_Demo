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
        /// 得到一个对象实体
        /// </summary>
        /// 
        [OperationContract]
        News GetNewsModel(int id);

        /// <summary>
        /// 根据collegeId获取新闻列表
        /// </summary>
        /// 
        [OperationContract]
        List<News> GetNewsList(int collegeId);

        /// <summary>
        /// 更新新闻信息
        /// </summary>
        /// 
        [OperationContract]
        bool UpdateNews(News news);

        
        /// <summary>
        /// 更新新闻的发布时间（为置顶方法使用）
        /// </summary>
        /// 
        [OperationContract]
        bool UpdateNewsReleaseTime(News model);

        /// <summary>
        /// 删除新闻信息
        /// </summary>     
        /// 
        [OperationContract]
        bool DeleteNews(int id);

        /// <summary>
        /// 新增新闻信息
        /// </summary>
        /// 
        [OperationContract]
        int AddNews(News model);

        /// <summary>
        /// 获取最大的排序号码
        /// </summary>
        /// <param name="collegeId"></param>
        /// <returns></returns>
        /// 
        [OperationContract]
        int GetMaxNum(int collegeId);

        /// <summary>
        /// 获取序号最小对应的实体对象
        /// </summary>
        /// <param name="collegeId"></param>
        /// <returns></returns>
        /// 
        [OperationContract]
        News GetMaxNumModel(int collegeId);

        /// <summary>
        /// 更新新闻信息
        /// </summary>
        /// 
        [OperationContract]
        bool UpdateNewsNum(News curModel, News minModel);

        /// <summary>
        /// 隐藏新闻
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [OperationContract]
        bool HideNews(int id, bool isHidden);

        /// <summary>
        /// 新闻列表分页
        /// </summary>
        /// <param name="collegeId"></param>
        /// <param name="title"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [OperationContract]
        List<News> GetNewsPageList(int collegeId, string title, int pageIndex, int pageSize);
    }
}
