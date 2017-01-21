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
        private FundProductDAL fundProductDAL = new FundProductDAL();

        /// <summary>
        /// 获取基金分页列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页(可为空,为空时返回所有数据)</param>
        /// <param name="pageSize">每页页数(可为空,为空时返回所有数据)</param>
        /// <param name="totalCount">总数量</param>
        /// <returns></returns>
        public List<FundProduct> GetFundProductPage(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount)
        {
            totalCount = 0;
            List<FundProduct> result = new List<FundProduct>();
            try
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    result = DBHelper.GetPageList<FundProduct>(pageIndex.Value, pageSize.Value, fundProductDAL.GetFundProductPageParams(filter), out totalCount);
                }
                else
                {
                    result = fundProductDAL.GetList(filter);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetFundProductPage方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取基金产品详细信息
        /// </summary>
        /// <param name="Id">基金Id</param>
        /// <returns></returns>
        public FundProduct GetFundProduct(int Id)
        {
            FundProduct result = null;
            try
            {
                result = fundProductDAL.GetModel(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetFundProduct方法出错", ex);
            }
            return result;
        }

    }
}
