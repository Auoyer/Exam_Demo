using System.Collections.Generic;
using System.ServiceModel;

namespace Training.API
{
    public partial interface ITrainingManage
    {

        [OperationContract]
        List<ExamPoint> GetExamPointPage(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount);


    }
}
