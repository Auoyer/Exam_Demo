using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Training.API
{
    public partial interface ITrainingManage
    {
        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="UserId">查询条件过滤</param>
        /// <returns></returns>
        [OperationContract]
        List<TheoryQuestionType> GetTheoryQuestionTypelist(CustomFilter filter);


        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool DeleteTheoryQuestionTypes(int TheoryChapterId);

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        TheoryQuestionType GetTheoryQuestionTypes(int Id);

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool UpdateTheoryQuestionTypes(TheoryQuestionType model);
    }
}
