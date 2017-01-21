using System;
using System.Runtime.Serialization;

namespace Match.API
{
    /// <summary>
    ///专家风采
    /// </summary>
    [DataContract]
    public class Experts
    {
        public Experts()
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
        /// UserId
        /// </summary>		
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 专家名称
        /// </summary>		
        [DataMember]
        public string ExpertsName { get; set; }

        /// <summary>
        /// 专家照片路径
        /// </summary>		
        [DataMember]
        public string ExpertsPicPath { get; set; }

        /// <summary>
        /// 专家介绍
        /// </summary>		
        [DataMember]
        public string ExpertsIntroduction { get; set; }

        /// <summary>
        /// CollegeId
        /// </summary>		
        [DataMember]
        public int CollegeId { get; set; }

    }
}