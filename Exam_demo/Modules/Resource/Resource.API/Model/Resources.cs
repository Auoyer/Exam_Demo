using System;
using System.Runtime.Serialization;

namespace Resource.API
{
    /// <summary>
    ///Resource
    /// </summary>
    [DataContract]
    public class Resources
    {
        public Resources()
        {
            CreateDate = DateTime.Now;
        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 资源名称
        /// </summary>		
        [DataMember]
        public string ResourceName { get; set; }

        /// <summary>
        /// 上传后文件名
        /// </summary>		
        [DataMember]
        public string FileName { get; set; }

        /// <summary>
        /// 文件地址
        /// </summary>		
        [DataMember]
        public string FilePath { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>		
        [DataMember]
        public string ImagePath { get; set; }

        /// <summary>
        /// 章节Id
        /// </summary>		
        [DataMember]
        public int ChapterId { get; set; }

        /// <summary>
        /// 转换状态
        /// </summary>		
        [DataMember]
        public int ConvertStatus { get; set; }

        /// <summary>
        /// 上传用户Id
        /// </summary>		
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        [DataMember]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 枚举：1.超管，2.竞赛管理员，3.竞赛评委，4.竞赛用户
        /// </summary>		
        [DataMember]
        public int GuideRole { get; set; }
    }
}