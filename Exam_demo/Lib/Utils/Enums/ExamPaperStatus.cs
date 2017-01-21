using System.ComponentModel;

namespace Utils
{
    /// <summary>
    /// 试卷状态
    /// </summary>
    public enum ExamPaperStatus
    {
        /// <summary>
        /// 0 未发布
        /// </summary>
        [Description("未发布")]
        UnPublish = 0,

        /// <summary>
        /// 1 已发布
        /// </summary>
        [Description("已发布")]
        Publish = 1,

        /// <summary>
        /// 2 已结束
        /// </summary>
        [Description("已结束")]
        End = 2,

        /// <summary>
        /// 3 已删除
        /// </summary>
        [Description("已删除")]
        Del = 3
    }

    /// <summary>
    /// 用户试卷总结状态枚举
    /// </summary>
    public enum PaperUserSummaryStatus
    {
        /// <summary>
        /// 1 刚领到试卷
        /// </summary>
        [Description("刚领到试卷")]
        Init = 1,

        /// <summary>
        /// 2 已交卷 /未评分
        /// </summary>
        [Description("未评分")]
        Submitted = 2,

        /// <summary>
        /// 3 已评分
        /// </summary>
        [Description("已评分")]
        Marked = 3,
    }

    /// <summary>
    /// 用户答题状态枚举
    /// </summary>
    public enum PaperUserAnswerStatus
    {
        /// <summary>
        /// 1 未答
        /// </summary>
        Init = 1,

        /// <summary>
        /// 2 正确
        /// </summary>
        Right = 2,

        /// <summary>
        /// 3 错误
        /// </summary>
        Wrong = 3,
    }

    /// <summary>
    /// 评分状态
    /// </summary>
    public enum GradeStatus
    {
        /// <summary>
        /// 已发布
        /// </summary>
        [Description("未审核")]
        Auditing = 2,

        /// <summary>
        /// 未发布
        /// </summary>
        [Description("已审核")]
        UnAuditing = 3
    }
}