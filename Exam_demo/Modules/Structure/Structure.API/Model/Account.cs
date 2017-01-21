using System;
using System.Runtime.Serialization;

namespace Structure.API
{
    /// <summary>
    ///Account
    /// </summary>
    [DataContract]
    public class Account
    {
        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>		
        [DataMember]
        public string AccountNo { get; set; }

        /// <summary>
        /// 密码
        /// </summary>		
        [DataMember]
        public string password { get; set; }

        /// <summary>
        /// 对应用户Id
        /// </summary>		
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        [DataMember]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>		
        [DataMember]
        public DateTime ModifyTime { get; set; }

    }
}