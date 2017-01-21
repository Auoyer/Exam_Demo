using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Exam.API
{
    [DataContract]
    public class UnitePaper
    {
        public UnitePaper()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 试卷名称
        /// </summary>		
        [DataMember]
        public string ExamPaperName { get; set; }

        /// <summary>
        /// 枚举：认证、理论
        /// </summary>		
        [DataMember]
        public int LibraryID { get; set; }

        /// <summary>
        /// 枚举：自动组卷、手动组卷
        /// </summary>		
        [DataMember]
        public int FormType { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>		
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 试卷状态
        /// </summary>
        [DataMember]
        public int Status { get; set; }

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
        /// 总分
        /// </summary>		
        [DataMember]
        public decimal TotalScore { get; set; }

        /// <summary>
        /// 得分
        /// </summary>		
        [DataMember]
        public decimal Score { get; set; }


        /*=======================自定义分界线=====================*/
        /// <summary>
        /// 章节信息
        /// </summary>
        [DataMember]
        public List<PaperCharpter> CharpterList { get; set; }
        /// <summary>
        /// 题目信息
        /// </summary>
        [DataMember]
        public List<PaperDetail> Details { get; set; }
        /// <summary>
        /// 分数信息
        /// </summary>
        [DataMember]
        public List<PaperScore> ScoreInfo { get; set; }
        /// <summary>
        /// 班级信息
        /// </summary>
        [DataMember]
        public List<PaperClass> ClassList { get; set; }
        /// <summary>
        /// 用户答题内容
        /// </summary>
        [DataMember]
        public List<PaperUserAnswer> UserAnswer { get; set; }
        /// <summary>
        /// 用户答题得分
        /// </summary>
        [DataMember]
        public List<PaperUserAnswerResult> UserAnswerResult { get; set; }
        /// <summary>
        /// 用户这张卷子的总分
        /// </summary>
        [DataMember]
        public List<PaperUserSummary> UserSummary { get; set; }

        /// <summary>
        /// 单个答题人这张卷子的总分（扩展字段）
        /// </summary>
        [DataMember]
        public decimal UserSumScore { get; set; }
    }
}
