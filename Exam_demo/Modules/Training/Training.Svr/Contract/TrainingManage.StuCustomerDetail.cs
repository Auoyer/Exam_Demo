using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.API;
using Utils;

namespace Training.Svr
{
    public partial class TrainingManage : ITrainingManage
    {
        private StuCustomerDetailDAL StuCustomerDetailDAL = new StuCustomerDetailDAL();
        /// <summary>
        /// 获取潜在客户/已有客户信息
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<StuCustomerDetail> GetStuCustomerDetailList(CustomFilter filter)
        {

            List<StuCustomerDetail> result = new List<StuCustomerDetail>();
            try
            {
                    result = StuCustomerDetailDAL.GetList(filter);

            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetStuCustomerDetailList方法出错", ex);
            }
            return result;
        }
    }
}
