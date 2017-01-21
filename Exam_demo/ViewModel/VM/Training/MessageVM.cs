using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///Message
    /// </summary>
    public class MessageVM
    {
        public MessageVM()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 信息类型：授课进度、课时安排、公告
        /// </summary>		
        public int MessageTypeId { get; set; }

        /// <summary>
        /// 授课进度/章节名称/公告内容
        /// </summary>		
        public string Context { get; set; }

        /// <summary>
        /// 课时
        /// </summary>		
        public decimal Period { get; set; }

        /// <summary>
        /// 班级Id
        /// </summary>		
        public int ClassId { get; set; }

        /// <summary>
        /// 创建用户Id
        /// </summary>		
        public int UserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassName { get; set; }

    }
}