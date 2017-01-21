using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///Calendar
    /// </summary>
    [DataContract]
    public class Calendar
    {
        public Calendar()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 学生客户Id
        /// </summary>		
        [DataMember]
        public int StuCustomerId { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>		
        [DataMember]
        public string CustomerName { get; set; }

        /// <summary>
        /// 服务类型(枚举)
        /// </summary>		
        [DataMember]
        public int ServiceType { get; set; }

        /// <summary>
        /// 计划内容
        /// </summary>		
        [DataMember]
        public string Context { get; set; }

        /// <summary>
        /// 预约时间
        /// </summary>		
        [DataMember]
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [DataMember]
        public int UserId { get; set; }

    }
}