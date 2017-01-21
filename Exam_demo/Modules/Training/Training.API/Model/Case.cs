using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    /// 案例
    /// </summary>
    [DataContract]
    public class Case
    {
        public Case()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 学校Id
        /// </summary>		
        [DataMember]
        public int CollegeId { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>		
        [DataMember]
        public string CustomerName { get; set; }

        /// <summary>
        /// 证件类型：1.身份证
        /// </summary>		
        [DataMember]
        public int IDType { get; set; }

        /// <summary>
        /// 证件号
        /// </summary>		
        [DataMember]
        public string IDNum { get; set; }

        /// <summary>
        /// 理财类型：1.现金规划、2.教育规划、3.消费规划、4.创业规划、5.退休规划、
        /// 6.保险规划、7.投资规划、8.税务筹划、9.财产分配、10.财产传承、11.综合规划
        /// </summary>		
        [DataMember]
        public int FinancialTypeId { get; set; }

        /// <summary>
        /// 客户背景
        /// </summary>		
        [DataMember]
        public string CustomerStory { get; set; }

        /// <summary>
        /// 案例来源(启用)：1.内置、2.所在学校ID
        /// </summary>
        [DataMember]
        public int CaseSource { get; set; }

        /// <summary>
        /// 创建者Id
        /// </summary>		
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        [DataMember]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 状态：1.正常、2.屏蔽、3.逻辑删除
        /// </summary>		
        [DataMember]
        public int Status { get; set; }

        /// <summary>
        /// 状态：0.为查看、1.已查看
        /// </summary>
        [DataMember]
        public int ViewStatus { get; set; }

        /// <summary>
        /// 案例对应答案
        /// </summary>
        [DataMember]
        public List<ExamPointAnswer> ExamPointAnswer { get; set; }

    }
}