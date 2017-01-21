using System;
using System.Runtime.Serialization;

namespace Structure.API
{
    /// <summary>
    ///UserClass
    /// </summary>
    [DataContract]
    public class UserClass
    {
        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>		
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 班级Id
        /// </summary>		
        [DataMember]
        public int ClassId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>		
        [DataMember]
        public int RoleId { get; set; }

    }
}