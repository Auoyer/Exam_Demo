using Match.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Svr;
using Utils;

namespace Match.Svr
{
    public partial class MatchManage
    {
        FriendlyLinkDAL friendlyLinkDAL = new FriendlyLinkDAL();

        /// <summary>
        /// 得到友情链接列表
        /// </summary>
        public List<FriendlyLink> GetFriendlyLinkList(int collegeId, int homePageId)
        {
            List<FriendlyLink> result = new List<FriendlyLink>();
            try
            {
                result = friendlyLinkDAL.GetFriendlyLinkList(collegeId, homePageId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetFriendlyLinkList方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 添加友情链接
        /// </summary>     
        public int AddFriendLink(FriendlyLink linkModel)
        {
            int res = 0;
            try
            {
                res = friendlyLinkDAL.Add(linkModel);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddFriendLink方法出错", ex);
            }
            return res;
        }

        /// <summary>
        /// 更新友情链接
        /// </summary> 
        public bool UpdateFriendLink(FriendlyLink linkModel)
        {
            bool result = false;
            try
            {
                result = friendlyLinkDAL.Update(linkModel);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateFriendLink方法出错", ex);
            }
            return result;
        }
       
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public FriendlyLink GetFriendLinkModel(int Id)
        {
            FriendlyLink linkModel = new FriendlyLink();
            try
            {
                linkModel = friendlyLinkDAL.GetModel(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetFriendLinkModel方法出错", ex);
            }
            return linkModel;
        }

         /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteFriendlyLink(int Id)
        {
            bool result = false;
            try
            {
                result = friendlyLinkDAL.Delete(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DeleteFriendlyLink方法出错", ex);
            }
            return result;
        }
    }
}
