using Structure.API;
using System;
using System.Collections.Generic;
using Utils;
using System.Linq;
using System.Threading.Tasks;

namespace Structure.Svr
{
    public partial class StructureManage
    {
        #region 获取总人数（为现有账号数量（不包括失效帐号）） int GetTotalUserNum()

        /// <summary>
        /// 获取总人数（为现有账号数量（不包括失效帐号））
        /// </summary>
        /// <returns></returns>
        public int GetTotalUserNum()
        {
            int result = 0;
            try
            {
                result = userDal.GetTotalUserNum();
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetTotalUserNum", ex);
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
            int res = 0;
            try
            {
                res = userDal.GetExpireAccountNum();
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetExpireAccountNum", ex);
            }
            return res;
        }

        #endregion

        #region 获取用户分页列表
        /// <summary>
        /// 获取用户分页列表（祖恒）
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页(可为空,为空时返回所有数据)</param>
        /// <param name="pageSize">每页页数(可为空,为空时返回所有数据)</param>
        /// <param name="totalCount">总数量</param>
        /// <returns></returns>       
        public List<UserInfo> GetUserInfoList(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount)
        {
            totalCount = 0;
            List<UserInfo> result = new List<UserInfo>();
            try
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    result = DBHelper.GetPageList<UserInfo>(pageIndex.Value, pageSize.Value, userDal.GetPageParams(filter), out totalCount);
                }
                else
                {
                    result = userDal.GetList(filter);
                }
                if (result.Count > 0)
                {
                    filter = new CustomFilter { IdList = result.Select(l => l.Id).ToList() };
                    var accountInfo = accountDal.GetList(filter);

                    Parallel.ForEach(result, l =>
                    {
                        l.AccountInfo = accountInfo.FirstOrDefault(m => m.UserId == l.Id);
                    });
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetUserInfoList方法出错", ex);
            }
            return result;
        }
        #endregion

        #region 获取总人数（为现有账号数量（不包括失效帐号）） int GetTotalUserNum()

        /// <summary>
        /// 获取总人数（为现有账号数量（不包括失效帐号））(非超管)
        /// </summary>
        /// <returns></returns>
        public int GetTotalUserNum2(int collegeId)
        {
            int result = 0;
            try
            {
                result = userDal.GetTotalUserNum2(collegeId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetTotalUserNum2", ex);
            }
            return result;
        }

        #endregion
    }
}