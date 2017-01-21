using Match.API;
using System.ServiceModel;
using Utils;

namespace Match.Svr
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class MatchManage : IMatchManage
    {
        /// <summary>
        /// 测试连接
        /// </summary>
        public void Test()
        {
#if DEBUG
            //Test Action,Do nothing
            LogHelper.Log.WriteInfo("[IMatchManage]Someone Testing Connection!");
#endif
        }
    }
}