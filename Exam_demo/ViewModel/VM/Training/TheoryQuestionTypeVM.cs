using System;
using System.Collections.Generic;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///TheoryQuestionType
    /// </summary>
    [Serializable]
    public class TheoryQuestionTypeVM
    {
        public TheoryQuestionTypeVM()
        {
            IdList = new List<int>();
        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 章节ID
        /// </summary>
        public int TheoryChapterId { get; set; }

        /// <summary>
        /// 题型名称
        /// </summary>		
        public string TypeName { get; set; }

        /// <summary>
        /// 来源：内置，自定义（弃用）
        /// </summary>		
        public int TypeSource { get; set; }

        /// <summary>
        /// 学校Id
        /// </summary>		
        public int CollegeId { get; set; }   

        /// <summary>
        /// 创建用户Id
        /// </summary>		
        public int UserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime CreateDate { get; set; }

        /*=====================================================================*/

        /// <summary>
        /// 题目数量
        /// </summary>
        public int CurQuestionCount { get; set; }

        /// <summary>
        /// 总数量 
        /// </summary>
        public int TotalNum { get; set; }
        /// <summary>
        /// 分值 
        /// </summary>
        public int Score { get; set; } 
        /// <summary>
        /// 相同题型名称的ID组合
        /// </summary>
        public List<int> IdList { get;set;}

        /// <summary>
        /// 相同题型名称的ID组合
        /// </summary>
        public string strIdList { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; } 
    }
}