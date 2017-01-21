using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
    /// <summary>
    /// 题目查询通用类
    /// </summary>
    public class QuestionSearch
    {
        /// <summary>
        /// 章节Id，认证类别Id
        /// </summary>
       
        public Nullable<int> CertificationId { get; set; }

        /// <summary>
        /// 题型
        /// </summary>
      
        public Nullable<int> QuestionTypeId { get; set; }

        /// <summary>
        /// 科目Id
        /// </summary>
      
        public Nullable<int> SubjectId { get; set; }

        /// <summary>
        /// 用户Id
      
        public Nullable<int> UserId { get; set; }

        /// <summary>
        /// 学校Id

        public Nullable<int> CollegeId { get; set; }

        /// <summary>
        /// 通过学校查询
        /// </summary>
        public bool BySchool { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
      
        public string KeyWords { get; set; }

        /// <summary>
        /// 状态(-1时，搜索启用和屏蔽的题目)
        /// </summary>
        public Nullable<int> Status { get; set; }      

        /// <summary>
        /// 章节
        /// </summary>
        public Nullable<int> CharpterID { get; set; }

        /// <summary>
        /// 题型名称
        /// </summary>
        public string StructTypeName { get; set; }

        /// <summary>
        /// 题型：1单项，2多选，3判断
        /// </summary>
        public int StructType { get; set; }

        /// <summary>
        /// 题干
        /// </summary>
        public string Context { get; set; }

        /// <summary>
        /// 章节下题型的集合
        /// </summary>
        public List<int> Listtypeid { get; set; }

        /// <summary>
        /// 题目Id
        /// </summary>
        public Nullable<int> Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool isBool { get; set; }
    }
}
