using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
    public class CustomerCountVM
    {
        public CustomerCountVM()
        {

        }
        /// <summary>
        /// 潜在客户总数
        /// </summary>
        public int CustomerPotentialSum { get; set; }
        /// <summary>
        /// 已有客户
        /// </summary>
        public int CustomerExistSum { get; set; }

        /// <summary>
        /// 高净资产已有客户数
        /// </summary>
        public decimal CustomerExistHighAssets { get; set; }
        /// <summary>
        ///高净资产潜在客户
        /// </summary>
        public decimal CustomerPotentialHighAssets { get; set; }
        /// <summary>
        /// 获取所有客户条数
        /// </summary>
        public int CustomerSumNum { get; set; }
    }
}
