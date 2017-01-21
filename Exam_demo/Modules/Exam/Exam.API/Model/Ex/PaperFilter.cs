using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Exam.API
{
    /// <summary>
    /// 试卷查询过滤器（针对扩展信息）
    /// </summary>
    [DataContract]
    public class PaperFilter
    {
        /// <summary>
        /// 试卷创建者
        /// </summary>
        [DataMember]
        public Nullable<int> AuthorID { get; set; }
        /// <summary>
        /// 答题人
        /// </summary>
        [DataMember]
        public Nullable<int> AnswererID { get; set; }

        /// <summary>
        /// 是否包含班级信息
        /// </summary>
        [DataMember]
        public bool ClassList { get; set; }
        /// <summary>
        /// 是否包含大赛信息
        /// </summary>
        [DataMember]
        public bool CompetitionList { get; set; }
        /// <summary>
        /// 是否包含分数信息
        /// </summary>
        [DataMember]
        public bool ScoreInfo { get; set; }
        /// <summary>
        /// 是否包含题目信息
        /// </summary>
        [DataMember]
        public bool Details { get; set; }
        /// <summary>
        /// 是否包含章节信息
        /// </summary>
        [DataMember]
        public bool CharpterList { get; set; }
        /// <summary>
        /// 是否包含用户答案
        /// </summary>
        [DataMember]
        public bool UserAnswer { get; set; }
        /// <summary>
        /// 是否包含用户答案得分
        /// </summary>
        [DataMember]
        public bool UserAnswerResult { get; set; }
        /// <summary>
        /// 是否包含用户试卷总结
        /// </summary>
        [DataMember]
        public bool UserSummary { get; set; }
    }
}
