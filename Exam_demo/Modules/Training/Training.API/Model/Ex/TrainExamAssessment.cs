﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Training.API
{
    [DataContract]
    public class TrainExamAssessment
    {
        public TrainExamAssessment()
        {

        }

        /// <summary>
        //实训考核ID
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [DataMember]
        public int UserId { get; set; }
        /// <summary>
        /// 班级ID
        /// </summary>
        [DataMember]
        public int CompetitionId { get; set; }
        /// <summary>
        /// 实训考核ID
        /// </summary>
        [DataMember]
        public int TrainExamId { get; set; }

        /// <summary>
        /// 总分
        /// </summary>
        [DataMember]
        public decimal TotalScore { get; set; }

        /// <summary>
        /// 主观分
        /// </summary>
        [DataMember]
        public decimal SubjectiveResults { get; set; }
        /// <summary>
        /// 客观分
        /// </summary>
        [DataMember]
        public decimal ObjectiveResults { get; set; }
   
        /// <summary>
        /// 考核名称TrainExamName
        /// </summary>
        [DataMember]
        public string TrainExamName { get; set; }


        /// <summary>
        /// 开始时间
        /// </summary>		
        [DataMember]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>		
        [DataMember]
        public DateTime EndDate { get; set; }


        /// <summary>
        /// 客户姓名
        /// </summary>		
        [DataMember]
        public string CustomerName { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>		
        [DataMember]
        public int IDType { get; set; }

        /// <summary>
        /// 证件号
        /// </summary>		
        [DataMember]
        public string IDNum { get; set; }

        /// <summary>
        /// 理财类型Id
        /// </summary>		
        [DataMember]
        public int FinancialTypeId { get; set; }

        /// <summary>
        /// 创建时间---实训的创建时间
        /// </summary>		
        [DataMember]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改时期--完成日期
        /// </summary>
           [DataMember]
        public DateTime UpdateDate { get; set; }
    }
}
