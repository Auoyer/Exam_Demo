using Server.Core;
using System.ServiceProcess;
using Utils;

namespace Server.WinService
{
    public partial class MainService : ServiceBase
    {
        public MainService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            int errCode = 0;
            if (SvrManager.StartAllService(out errCode))
            {
                LogHelper.Log.WriteInfo("服务启动成功！");
            }
            else
            {
                LogHelper.Log.WriteInfo(errCode.ToString());
            }
        }


    }
}
