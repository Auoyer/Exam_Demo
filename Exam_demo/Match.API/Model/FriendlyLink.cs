using System;
using System.Runtime.Serialization;

namespace Match.API
{
    /// <summary>
    ///友情链接
    /// </summary>
    [DataContract]
    public class FriendlyLink
    {
        public FriendlyLink()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// HomePageId
        /// </summary>		
        [DataMember]
        public int HomePageId { get; set; }

        /// <summary>
        /// CollegeId
        /// </summary>		
        [DataMember]
        public int CollegeId { get; set; }

        /// <summary>
        /// 友情链接图片路径
        /// </summary>		
        [DataMember]
        public string LinkImagePath { get; set; }

        /// <summary>
        /// 链接名称
        /// </summary>		
        [DataMember]
        public string LinkName { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>	
        /// 
        [DataMember]
        public string LinkAddress { get; set; }

    }
}