using System.ServiceModel;

namespace Training.API
{
    [ServiceContract]
    public partial interface ITrainingManage
    {
        [OperationContract]
        void Test();
    }
}
