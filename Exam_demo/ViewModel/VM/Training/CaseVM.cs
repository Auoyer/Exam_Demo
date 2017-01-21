using System;
using System.Collections.Generic;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///Case
    /// </summary>
    public class CaseVM
    {
        public CaseVM()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 学校Id
        /// </summary>		
        public int CollegeId { get; set; }

        /// <summary>
        /// 学校名称
        /// </summary>		
        public string strCollege { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>		
        public string CustomerName { get; set; }

        /// <summary>
        /// 证件类型：1.身份证
        /// </summary>		
        public int IDType { get; set; }

        /// <summary>
        /// 证件号
        /// </summary>		
        public string IDNum { get; set; }

        /// <summary>
        /// 理财类型：1.现金规划、2.教育规划、3.消费规划、4.创业规划、5.退休规划、
        /// 6.保险规划、7.投资规划、8.税务筹划、9.财产分配、10.财产传承、11.综合规划
        /// </summary>		
        public int FinancialTypeId { get; set; }

        /// <summary>
        /// 理财类型字符串
        /// </summary>
        public string strFinancialType { get; set; }

        /// <summary>
        /// 客户背景
        /// </summary>		
        public string CustomerStory { get; set; }

        /// <summary>
        /// 案例来源：1.内置、2.自定义
        /// </summary>
        public int CaseSource { get; set; }

        /// <summary>
        /// 案例来源字符串
        /// </summary>
        public string strCaseSource { get; set; }

        /// <summary>
        /// 创建者Id
        /// </summary>		
        public int UserId { get; set; }

        /// <summary>
        /// 用户名字符串
        /// </summary>
        public string strUserName { get; set; }

        /// <summary>
        /// 状态：1.正常、2.屏蔽、3.逻辑删除
        /// </summary>		
        public int Status { get; set; }

        /// <summary>
        /// 状态：0.为查看、1.已查看
        /// </summary>		
        public int ViewStatus { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 创建时间字符窜
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
        /// 考核点答案
        /// </summary>
        public List<ExamPointAnswerVM> ExamPointAnswer { get; set; }
    }
}