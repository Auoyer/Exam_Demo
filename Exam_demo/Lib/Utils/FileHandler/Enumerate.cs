using System.Runtime.Serialization;

namespace Utils
{
    public class Enumerate
    {
        #region 文件转换状态

        /// <summary>
        /// 文件转换状态[正在转换:0,转换成功:1,转换失败:2]
        /// </summary>
        [DataContract]
        public enum ConvertStatus
        {
            /// <summary>
            /// 正在转换:0
            /// </summary>
            [EnumMember]
            Converting = 0,

            /// <summary>
            /// 转换成功:1
            /// </summary>
            [EnumMember]
            Success,

            /// <summary>
            /// 转换失败:2
            /// </summary>
            [EnumMember]
            Failed
        }

        #endregion
    }
}
