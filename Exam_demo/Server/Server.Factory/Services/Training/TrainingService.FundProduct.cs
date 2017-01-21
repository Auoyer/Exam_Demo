using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;
using EmitMapper.MappingConfiguration;
using Training.API;
using VM;

namespace Server.Factory
{
    public partial class TrainingService : IService<ITrainingManage>
    {
        /// <summary>
        /// 获取基金分页列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页页数</param>
        /// <returns></returns>
        public PagedList<FundProductVM> GetFundProductPage(TrainSearch search, int pageIndex, int pageSize)
        {
            int totalCount = 0;
            CustomFilter filter = new CustomFilter
            {
                FundType = search.FundType,
                KeyWords = search.KeyWords,
            };

            var list = MyService.GetFundProductPage(filter, pageIndex, pageSize, out totalCount);

            List<FundProductVM> rtnValue = list.MapList<FundProductVM, FundProduct>();
            PagedList<FundProductVM> result = new PagedList<FundProductVM>(rtnValue, pageIndex, pageSize, totalCount);

            return result;
        }

        /// <summary>
        /// 获取基金产品详细信息
        /// </summary>
        /// <param name="Id">基金Id</param>
        /// <returns></returns>
        public FundProductVM GetFundProduct(int Id)
        {
            return MyService.GetFundProduct(Id).Map<FundProductVM, FundProduct>();
        }

    }
}
