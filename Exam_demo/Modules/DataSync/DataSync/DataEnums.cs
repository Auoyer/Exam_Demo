using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSync
{
    public class DataEnums
    {
        /// <summary>
        /// 基金主体信息表对应字段映射
        /// </summary>
        public enum MainInfo
        {
            FUNDID,
            FundType,
            FundCompany,
        }

        /// <summary>
        /// 基金份额类别信息表对应字段映射
        /// </summary>
        public enum UnitClassInfo
        {
            FUNDID,
            FundName,
            FundCode,
        }

        /// <summary>
        /// 基金日净值文件对应字段映射
        /// </summary>
        public enum NAV
        {
            NewNetValue,
            TotalNewValue,
            AnnualizedYield,
            UpdateDate
        }


    }
}
