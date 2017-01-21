using Structure.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Structure.Svr
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class StructureManage : IStructureManage
    {
        public void Test()
        {
#if DEBUG
            //Test Action,Do nothing
            LogHelper.Log.WriteInfo("[IStructureManage]Someone Testing Connection!");
#endif
        }
    }
}
