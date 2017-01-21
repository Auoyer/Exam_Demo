using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///Message
    /// </summary>
    [DataContract]
    public class Message
    {
        public Message()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 信息类型：授课进度、课时安排、公告
        /// </summary>		
        [DataMember]
        public int MessageTypeId { get; set; }

        /// <summary>
        /// 授课进度/章节名称/公告内容
        /// </summary>		
        [DataMember]
        public string Context { get; set; }

        /// <summary>
        /// 课时
        /// </summary>		
        [DataMember]
        public decimal Period { get; set; }

        /// <summary>
        /// 班级Id
        /// </summary>		
        [DataMember]
        public int ClassId { get; set; }

        /// <summary>
        /// 创建用户Id
        /// </summary>		
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        [DataMember]
        public DateTime CreateDate { get; set; }

    }
}