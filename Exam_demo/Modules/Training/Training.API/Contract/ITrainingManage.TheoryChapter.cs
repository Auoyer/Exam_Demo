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
        /// 理论考核章节列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<TheoryChapter> GetTheoryChapterList(CustomFilter filter, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 理论考核章节列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<TheoryChapter> GetSelectQuestionsList(CustomFilter filter);

        /// <summary>
        /// 理论考核章节子类型列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<TheoryQuestionType> GetTheoryQuestionTypeList(CustomFilter filter, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 理论考核章节子类型列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [OperationContract]
        List<TheoryQuestionType> GetTheoryQuestionTypeList1(CustomFilter filter);

        /// <summary>
        /// 理论考核评屏蔽列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<TheoryChapterHidden> GetTheoryChapterHiddenList(CustomFilter filter, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 新增章节
        /// </summary>
        /// <param name="model">章节实体</param>
        /// <returns></returns>
        [OperationContract]
        TheoryChapter AddTheoryChapter(TheoryChapter model);

        /// <summary>
        /// 新增章节子题目类型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        int AddTheoryQuestionType(TheoryQuestionType model);

        /// <summary>
        /// 编辑章节
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateTheoryChapter(TheoryChapter model);

        /// <summary>
        /// 删除章节
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteTheoryChapter(int id);

        /// <summary>
        /// 删除章节子题目类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteTheoryQuestionType(int id);

        /// <summary>
        /// 内置章节逻辑删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract]
        bool TombstoneChapter(int id, int userId);
        /// <summary>
        /// 章节判重
        /// </summary>
        /// <param name="theoryChapterName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        bool IsExistTheoryChapter(string theoryChapterName, int id, int userId);

        /// <summary>
        /// 获得章节信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        TheoryChapter GetTheoryChapter(int id);

        /// <summary>
        /// 获取章节的数量
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int GetTheoryChapterNum(int userId);
    }
}
