using System;
namespace VM
{
    /// <summary>
    /// 保存在Session中的简易用户信息
    /// </summary>
    [Serializable]
    public class SessionUser
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 唯一识别码
        /// </summary>
        public string GUID { get; set; }

        /// <summary>
        /// 所属学校ID
        /// </summary>
        public int CollegeId { get; set; }

        /// <summary>
        /// RoleID
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// AccountID
        /// </summary>
        public int AccountID { get; set; }
    }
}