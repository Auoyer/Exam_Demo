using Dapper;
using Structure.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structure.Svr
{
    public partial class UserInfoDAL
    {
        #region 获取总人数（为现有账号数量（不包括失效帐号)） int GetTotalUserNum()
        /// <summary>
        /// 获取总人数（为现有账号数量（不包括失效帐号)）
        /// </summary>
        /// <returns></returns>
        public int GetTotalUserNum()
        {
            int result = 0;
            StringBuilder strSql = new StringBuilder();
            var param = new DynamicParameters();
            strSql.Append("select COUNT(*) from Account a inner join UserInfo b on a.UserId = b.Id and b.Status=2");

            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result;
        }
        #endregion

        #region 获取到期帐号数量 int GetExpireAccountNum()
        /// <summary>
        /// 获取到期帐号数量
        /// </summary>
        /// <returns></returns>
        public int GetExpireAccountNum()
        {
            int result = 0;
            StringBuilder strSql = new StringBuilder();
            var param = new DynamicParameters();
            strSql.Append("select COUNT(1) from Account a inner join UserInfo b on a.UserId = b.Id and b.RoleId=2 and Status<>3 and (IsView!=1 or IsView is null) and (b.ExpiryDate<=GETDATE() or DATEADD(day,-7,b.ExpiryDate)<=GETDATE())");

            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result;
        }
        #endregion

       

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数（祖恒）
        /// </summary>
        public PageModel GetPageParams(CustomFilter filter = null)
        {
            PageModel model = new PageModel();
            model.Tables = "UserInfo join Account on UserInfo.Id = Account.UserId";
            model.PKey = "UserInfo.Id";
            model.Sort = GetStrSort(filter);
            model.Fields = @"UserInfo.[Id],[UserName],[Sex],[Email]
                              ,[DepartmentName]
                              ,[ProvinceCode]
                              ,[CityCode]
                              ,[IDCard]
                              ,[Phone]
                              ,[RoleId]
                              ,[Address]
                              ,UserInfo.[CreateTime]
                              ,UserInfo.[ModifyTime]
                              ,[UserType]
                              ,[Status]
                              ,[IsCreate]
                              ,[AvailabilityDate]
                              ,[ExpiryDate]
                              ,[CollegeId]
                              ,[CollegeName]
                              ,[IsPageRegistration]
                              ,[IsAudit],[IsView]";
            model.Filter = GetStrWhere2(filter);
            return model;
        }

        #endregion

        /// <summary>
        /// 根据CustomFilter获取where语句(祖恒)
        /// </summary>
        private string GetStrWhere2(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();


            if (filter.UserType != 0)
            {
                strSql.AppendFormat(" and UserInfo.UserType={0}", filter.UserType);
            }
            if (filter.StatusId.HasValue)
            {
                strSql.AppendFormat(" and UserInfo.Status<>{0}", filter.StatusId);
            }
            if (filter.TypeId.HasValue)
            {
                strSql.AppendFormat(" and UserInfo.RoleId={0}", filter.TypeId);
            }
            if (!string.IsNullOrWhiteSpace(filter.KeyWord))
            {
                // 查询模糊匹配账号，姓名，学校，手机号码
                strSql.AppendFormat(" and (UserInfo.UserName like '%{0}%' or Account.AccountNo like '%{0}%' or UserInfo.CollegeName like '%{0}%' or UserInfo.Phone like '%{0}%')", filter.KeyWord.Replace("'", "''"));

            }

            /// <summary>
            /// 是否查看（1，已查看；0，为查看），针对首页到期帐号数量，点查看以后数量递减。
            /// </summary>
            //if (!string.IsNullOrWhiteSpace(filter.IsView))
            //{
            //    strSql.AppendFormat(" and {0}", filter.IsView);
            //}

            if (!string.IsNullOrEmpty(filter.DateKey))
            {
                strSql.AppendFormat("  and (UserInfo.ExpiryDate<=GETDATE() or DATEADD(day,-7,UserInfo.ExpiryDate)<=GETDATE())");
            }
            return strSql.ToString();
        }

        #region 获取总人数（为现有账号数量（不包括失效帐号)） int GetTotalUserNum()
        /// <summary>
        /// 获取总人数（为现有账号数量（不包括失效帐号)）(非超管)
        /// </summary>
        /// <returns></returns>
        public int GetTotalUserNum2(int collegeId)
        {
            int result = 0;
            StringBuilder strSql = new StringBuilder();
            var param = new DynamicParameters();
            strSql.Append("select COUNT(*) from Account a inner join UserInfo b on a.UserId = b.Id and b.Status=2 and b.CollegeId=" + collegeId);

            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result;
        }
        #endregion
    }
}
