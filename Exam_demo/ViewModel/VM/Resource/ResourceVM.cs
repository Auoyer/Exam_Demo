using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///Resource
    /// </summary>
    public class ResourceVM
    {
        public ResourceVM()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 资源名称
        /// </summary>		
        public string ResourceName { get; set; }

        /// <summary>
        /// 上传后文件名
        /// </summary>		
        public string FileName { get; set; }

        /// <summary>
        /// 文件地址
        /// </summary>		
        public string FilePath { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>		
        public string ImagePath { get; set; }

        /// <summary>
        /// 章节Id
        /// </summary>		
        public int ChapterId { get; set; }

        /// <summary>
        /// 转换状态
        /// </summary>		
        public int ConvertStatus { get; set; }

        /// <summary>
        /// 上传用户Id
        /// </summary>		
        public int UserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 枚举：1.超管，2.竞赛管理员，3.竞赛评委，4.竞赛用户
        /// </summary>		
        public int GuideRole { get; set; }
    }
}