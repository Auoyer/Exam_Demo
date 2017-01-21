using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Training.API
{

    /// <summary>
    /// 教师理论成绩管理
    /// </summary>
    [DataContract]
   public class ResultListManager
    {
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
        /// 已经评价人数
        /// </summary>
        [DataMember]
        public int AlrEvaluatePerson { get; set; }

        /// <summary>
        /// 未评人数
        /// </summary>
        [DataMember]
        public int NoEvaluatePerson { get; set; }



    }
}
