using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Structure.API
{
    /// <summary>
    ///UserInfo
    /// </summary>
    [DataContract]
    public class UserInfo
    {
        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>		
        [DataMember]
        public string UserName { get; set; }

        ///// <summary>
        ///// 学号/工号
        ///// </summary>		
        //[DataMember]
        //public string SchoolNumber { get; set; }

        /// <summary>
        /// 市码
        /// </summary>
        [DataMember]
        public int CityCode { get; set; }

        /// <summary>
        /// 省码
        /// </summary>
        [DataMember]
        public int ProvinceCode { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [DataMember]
        public string IDCard { get; set; }


        /// <summary>
        /// 学校ID
        /// </summary>
        [DataMember]
        public string CollegeId { get; set; }


        /// <summary>
        /// 大学名称
        /// </summary>
        /// 
        [DataMember]
        public string CollegeName { get; set; }

        /// <summary>
        /// 院/部
        /// </summary>
        /// 
        [DataMember]
        public string DepartmentName { get; set; }

        /// <summary>
        /// 枚举：1.男，2.女
        /// </summary>		
        [DataMember]
        public int Sex { get; set; }

        /// <summary>
        /// 电话
        /// </summary>		
        [DataMember]
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>		
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// 地址
        /// </summary>		
        [DataMember]
        public string Address { get; set; }

        /// <summary>
        /// 枚举：1.超管，2.竞赛管理员，3.竞赛评委，4.竞赛用户，0游客
        /// </summary>		
        /// 
        [DataMember]
        public int RoleId { get; set; }

        /// <summary>
        /// 枚举：状态（1失效、2正常、3删除）
        /// </summary>		
        /// 
        [DataMember]
        public int Status { get; set; }

        /// <summary>
        /// 1试用帐号、2普通帐号
        /// </summary>
        /// 
        [DataMember]
        public int UserType { get; set; }

        /// <summary>
        /// 是否有创建权
        /// </summary>
        /// 
        [DataMember]
        public int IsCreate { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        [DataMember]
        public DateTime? AvailabilityDate
        {
            get
            {
                if (!_AvailabilityDate.HasValue)
                {
                    return DateTime.Now;
                }
                return _AvailabilityDate;
            }
            set
            {
                _AvailabilityDate = value;
            }
        }

        [DataMember]
        private DateTime? _AvailabilityDate;       
        

        /// <summary>
        /// 失效日期
        /// </summary>
        /// 
        [DataMember]
        public DateTime? ExpiryDate
        {
            get
            {
                if (!_ExpiryDate.HasValue)
                {
                    return DateTime.MaxValue;
                }
                return _ExpiryDate;
            }
            set
            {
                _ExpiryDate = value;
            }
        }

        [DataMember]
        private DateTime? _ExpiryDate; 

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

        /*===============================扩展字段===========================*/

        /// <summary>
        /// 账号
        /// </summary>
        [DataMember]
        public Account AccountInfo { get; set; }

        /// <summary>
        /// 用户登录账号
        /// </summary>
        [DataMember]
        public string AccountNo { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DataMember]
        public string Password { get; set; }


        /// <summary>
        /// 是否官网注册，1=官网注册，0=管理员创建
        /// </summary>
        [DataMember]
        public int IsPageRegistration { get; set; }

        /// <summary>
        /// 是否审核通过，0=未审核，1=已通过，2=拒绝
        /// </summary>
        [DataMember]
        public int IsAudit { get; set; }

        /// <summary>
        /// 账号是否存在
        /// </summary>
        [DataMember]
        public bool IsExist { get; set; }

        /// <summary>
        /// 分组Id
        /// </summary>
        [DataMember]
        public int GroupId { get; set; }
        
        /// /// <summary>
        /// 是否查看（1，已查看；0，为查看），针对首页到期帐号数量，点查看以后数量递减。
        /// </summary>       
        [DataMember]
        public int IsView { get; set; }

        ///// <summary>
        ///// 关联的班级信息
        ///// </summary>
        //[DataMember]
        //public List<UserClass> UserClassInfo { get; set; }
    }
}