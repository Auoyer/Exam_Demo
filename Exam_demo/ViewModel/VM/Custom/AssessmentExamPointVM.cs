using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
    public class AssessmentExamPointVM
    {
        public AssessmentExamPointVM()
        {

        }
        /// <summary>
        /// 考核点的AssessmentResultsDetail的ID
        /// </summary>	
        public int Id { get; set; }
        /// <summary>
        /// 实训考核ID
        /// </summary>
       
        public int TrainExamId { get; set; }
        /// <summary>
        /// 班级ID
        /// </summary>
       
        public int ClassId { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
       
        public int UserId { get; set; }

        /// <summary>
        /// 总分
        /// </summary>

        public decimal TotalScore { get; set; }

        /// <summary>
        /// 主观分
        /// </summary>
       
        public decimal SubjectiveResults { get; set; }
        /// <summary>
        /// 客观分
        /// </summary>
       
        public decimal ObjectiveResults { get; set; }
        /// <summary>
        /// 考核状态
        /// </summary>
       
        public int TrainExamStatus { get; set; }

        /// <summary>
        /// 考核点类型
        /// </summary>
       
        public int ExamPointType { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>	
       
        public int ModularId { get; set; }

        /// <summary>
        /// 考核点
        /// </summary>
       
        public int AssessmentPoint { get; set; }

        /// <summary>
        /// 状态
        /// </summary>	
       
        public int Status { get; set; }

        /// <summary>
        /// 得分
        /// </summary>	
       
        public decimal Score { get; set; }

        #region 扩展字段
        /// <summary>
        /// 考核名
        /// </summary>
        public string TrainExamName { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 学号
        /// </summary>
        public string SchoolNum { get; set; }
        /// <summary>
        /// 学校名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 模块名
        /// </summary>
        public string ModularName { get; set; }
        /// <summary>
        /// 正确/错误
        /// </summary>
        public string StatusName { get; set; }
        /// <summary>
        /// 考核点名的名称
        /// </summary>
        public string AssessmentPointName { get; set; }

        /// <summary>
        /// 考生实际成绩
        /// </summary>
        public decimal UserScore
        {
            get
            {
                return SubjectiveResults + ObjectiveResults;
            }
           
        }
        /// <summary>
        /// 序列id
        /// </summary>
        public int Serial { get; set; }
        #endregion

    }
}
