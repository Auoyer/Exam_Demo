using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.API;
using VM;

namespace Server.Factory
{
    public partial class TrainingService : IService<ITrainingManage>
    {
        /// <summary>
        /// 获取银行储蓄
        /// </summary>
        /// <returns></returns>
        public List<BankDepositsVM> GetBankDepositsList()
        {
            int totalCount = 0;

            var list = MyService.GetBankDepositsList(null, null, null, out totalCount);
            List<BankDepositsVM> rtnValue = list.MapList<BankDepositsVM, BankDeposits>();

            return rtnValue;
        }
    }
}
