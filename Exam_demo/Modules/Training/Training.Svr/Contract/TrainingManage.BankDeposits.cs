using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Training.API;
using Utils;

namespace Training.Svr
{
    public partial class TrainingManage : ITrainingManage
    {
        private BankDepositsDAL bankDepositsDAL = new BankDepositsDAL();

        /// <summary>
        /// 获取银行储蓄分页列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页(可为空,为空时返回所有数据)</param>
        /// <param name="pageSize">每页页数(可为空,为空时返回所有数据)</param>
        /// <param name="totalCount">总数量</param>
        /// <returns></returns>
        public List<BankDeposits> GetBankDepositsList(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount)
        {
            totalCount = 0;
            List<BankDeposits> result = new List<BankDeposits>();
            try
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    result = DBHelper.GetPageList<BankDeposits>(pageIndex.Value, pageSize.Value, bankDepositsDAL.GetBankDepositsPageParams(filter), out totalCount);
                }
                else
                {
                    result = bankDepositsDAL.GetList(filter);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetBankDepositsList方法出错", ex);
            }
            return result;
        }
    }
}
