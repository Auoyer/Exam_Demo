using System;
using System.Globalization;

namespace VM
{
    public class FinancialPlanningVM
    {
        public FinancialPlanningVM()
        {

        }

        /// <summary>
        /// 建议书id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 建议书编号
        /// </summary>		
        public string ProposalNum { get; set; }

        /// <summary>
        /// 建议书名称
        /// </summary>		
        public string ProposalName { get; set; }

        /// <summary>
        /// 建议书客户名称
        /// </summary>		
        public string CustomerName { get; set; }

        /// <summary>
        /// 建议书客户身份证号
        /// </summary>		
        public string IDNum { get; set; }

        /// <summary>
        /// 理财类型Id
        /// </summary>		
        public int? FinancialTypeId { get; set; }

        /// <summary>
        /// 理财类型名称
        /// </summary>		
        public string FinancialTypeName { get; set; }

        /// <summary>
        /// 建议书创建日期
        /// </summary>		
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 销售机会结束时间
        /// </summary>		
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 建议书更新日期
        /// </summary>		
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// 建议书目前的状态
        /// </summary>		
        public int Status { get; set; }

        /// <summary>
        /// 建议书目前的状态名称
        /// </summary>	
        public string StatusName { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>		
        public int UserId { get; set; }

        /// <summary>
        /// 销售机会Id
        /// </summary>		
        public int TrainExamId { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public int StuCustomerId { get; set; }
        /// <summary>
        /// 获取客户类型1：潜在2：已有
        /// </summary>
        public int CustomerType { get; set; }
        //===========================================================



        /// <summary>
        /// 建议书创建日期字符串
        /// </summary>
        public string strCreateDate
        {
            get
            {
                return CreateDate.ToString("yyyy/MM/dd", DateTimeFormatInfo.InvariantInfo);
            }
            private set { }
        }

        /// <summary>
        /// 建议书更新日期字符串
        /// </summary>
        public string strUpdateDate
        {
            get
            {
                return UpdateDate.ToString("yyyy/MM/dd", DateTimeFormatInfo.InvariantInfo);
            }
            private set { }
        }

        /// <summary>
        /// 销售机会结束时间
        /// </summary>
        public string strEndTime
        {
            get
            {
                if (EndDate.HasValue)
                {
                    return EndDate.Value.ToString("yyyy/MM/dd", DateTimeFormatInfo.InvariantInfo);
                }
                return "";
            }
            private set { }
        }

        /// <summary>
        /// 销售机会结束时间
        /// </summary>
        public string EndTimeType { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public int Serial { get; set; }

    }
}
