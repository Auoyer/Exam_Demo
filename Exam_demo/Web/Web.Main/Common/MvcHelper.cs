using Server.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utils;
using VM;

namespace Web
{
    public class MvcHelper
    {
        /// <summary>
        /// 登录页常量
        /// </summary>
        public const string LoginUrl = "/SignIn";

        /// <summary>
        /// 系统名称
        /// </summary>
        public const string SiteName = "金融理财规划大赛平台";

        /// <summary>
        /// 获取当前用户(为空时会创建新实体)
        /// </summary>
        public static UserInfoVM User
        {
            get
            {
                return new UserInfoVM();
            }
        }

        public class UserInfoVM 
        {
            public UserInfoVM()
            {
                CreateTime = DateTime.Now;
                ModifyTime = DateTime.Now;
                ExamInfo = new Dictionary<int, PaperVM>();
                TempUserInfo = new Dictionary<int, UserInfoVM>();
            }

            /// <summary>
            /// Id
            /// </summary>		
            public int Id { get; set; }

            /// <summary>
            /// 姓名
            /// </summary>		
            public string UserName { get; set; }

            /// <summary>
            /// 市码
            /// </summary>
            public int CityCode { get; set; }

            /// <summary>
            /// 省码
            /// </summary>
            public int ProvinceCode { get; set; }

            /// <summary>
            /// 身份证号
            /// </summary>
            public string IDCard { get; set; }

            /// <summary>
            /// 大学名称
            /// </summary>
            public string CollegeName { get; set; }

            /// <summary>
            /// 院/部
            /// </summary>
            public string DepartmentName { get; set; }

            /// <summary>
            /// 枚举：1.男，2.女
            /// </summary>		
            public int Sex { get; set; }

            /// <summary>
            /// 电话
            /// </summary>		
            public string Phone { get; set; }

            /// <summary>
            /// 邮箱
            /// </summary>		
            public string Email { get; set; }

            /// <summary>
            /// 地址
            /// </summary>		
            public string Address { get; set; }


            /// <summary>
            /// 枚举：1.超管，2.竞赛管理员，3.竞赛评委，4.竞赛用户，0游客
            /// </summary>		
            public int RoleId { get; set; }

            /// <summary>
            /// 枚举：状态（1失效、2正常、3删除）
            /// </summary>		
            public int Status { get; set; }

            /// <summary>
            /// 状态名称
            /// </summary>
            public string StatusName { get; set; }

            /// <summary>
            /// 1试用帐号、2普通帐号
            /// </summary>
            public int UserType { get; set; }

            /// <summary>
            /// 帐号名称
            /// </summary>
            public string UserTypeName { get; set; }

            /// <summary>
            /// 是否有创建权
            /// </summary>
            public int IsCreate { get; set; }

            /// <summary>
            /// 生效日期
            /// </summary>
            public DateTime? AvailabilityDate { get; set; }

            public string _AvailabilityDate
            {
                get
                {
                    if (AvailabilityDate != null)
                        return this.AvailabilityDate.Value.ToString("yyyy-MM-dd HH:mm");
                    else
                        return "";
                }
            }

            /// <summary>
            /// 失效日期
            /// </summary>
            public DateTime? ExpiryDate { get; set; }

            public string _ExpiryDate
            {
                get
                {
                    if (ExpiryDate != null)
                        return this.ExpiryDate.Value.ToString("yyyy-MM-dd HH:mm");
                    else
                        return "";
                }
            }


            /// <summary>
            /// 创建时间
            /// </summary>		
            public DateTime CreateTime { get; set; }

            /// <summary>
            /// 修改时间
            /// </summary>		
            public DateTime ModifyTime { get; set; }


            /*==============================额外扩展分界线=========================*/
            #region 登录相关

            public string Guid { get; set; }
            public DateTime LastActionTime { get; set; }

            #endregion

            #region 考试相关
            /// <summary>
            /// 理论考核答题相关缓存
            /// </summary>
            public Dictionary<int, PaperVM> ExamInfo { get; set; }
            #endregion

            public AccountVM AccountInfo { get; set; }

            /// <summary>
            /// 账号
            /// </summary>
            public string AccountNo { get; set; }

            /// <summary>
            /// 创建时间
            /// </summary>		
            public string CreateTimeStr
            {
                get
                {
                    return CreateTime.ToString("yyyy/MM/dd HH:mm:ss");
                }
            }

            /// <summary>
            /// 学院Id
            /// </summary>
            public int CollegeId { get; set; }

            /// <summary>
            /// 性别名称
            /// </summary>
            public string SexName { get; set; }

            /// <summary>
            /// 临时存储用户信息
            /// </summary>
            public Dictionary<int, UserInfoVM> TempUserInfo { get; set; }

            /// <summary>
            /// 密码
            /// </summary>
            public string Password { get; set; }

            /// <summary>
            /// 是否官网注册，1=官网注册，0=管理员创建
            /// </summary>
            public int IsPageRegistration { get; set; }

            /// <summary>
            /// 是否审核通过，0=未审核，1=已通过，2=拒绝
            /// </summary>
            public int IsAudit { get; set; }

            /// <summary>
            /// 账号是否存在
            /// </summary>
            public bool IsExist { get; set; }

            /// <summary>
            /// 分组Id
            /// </summary>
            public int GroupId { get; set; }

            /// /// <summary>
            /// 是否查看（1，已查看；0，为查看），针对首页到期帐号数量，点查看以后数量递减。
            /// </summary>       

            public int IsView { get; set; }
        }

        public class AccountVM 
        {
            public AccountVM()
            {
                CreateTime = DateTime.Now;
                ModifyTime = DateTime.Now;
            }

            /// <summary>
            /// Id
            /// </summary>		
            public int Id { get; set; }

            /// <summary>
            /// 账号
            /// </summary>		
            public string AccountNo { get; set; }

            /// <summary>
            /// 密码
            /// </summary>		
            public string password { get; set; }

            /// <summary>
            /// 对应用户Id
            /// </summary>		
            public int UserId { get; set; }

            /// <summary>
            /// 创建时间
            /// </summary>		
            public DateTime CreateTime { get; set; }

            /// <summary>
            /// 修改时间
            /// </summary>		
            public DateTime ModifyTime { get; set; }
        }
    }
}