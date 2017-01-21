
namespace VM
{
    public class ExamPointAnswerVM
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 案例Id
        /// </summary>
        public int CaseId { get; set; }

        /// <summary>
        /// 考核点Id
        /// </summary>
        public int ExamPointId { get; set; }

        /// <summary>
        /// 标准答案
        /// </summary>
        public string Answer {get;set; }

        /// <summary>
        /// 考核点
        /// </summary>
        public string strExamPoint { get; set; }

        /// <summary>
        /// 考核类型
        /// </summary>
        public string strExamType { get; set; }

    }
}
