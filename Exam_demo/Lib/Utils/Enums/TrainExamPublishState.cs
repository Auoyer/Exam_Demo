using System.ComponentModel;

namespace Utils
{
    /// <summary>
    /// 销售机会发布状态
    /// </summary>
    public enum TrainExamPublishState
    {
        /// <summary>
        /// 未发布
        /// </summary>
        [Description("未发布")]
        UnPublished = 0,
        /// <summary>
        /// 已发布
        /// </summary>
        [Description("已发布")]
        Published = 1,
        /// <summary>
        /// 已删除
        /// </summary>
        [Description("已删除")]
        Deleted = 2,
    }
}
