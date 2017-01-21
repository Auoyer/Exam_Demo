using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Match.API
{
    public partial interface IMatchManage
    {
         /// <summary>
        /// 得到友情链接列表
        /// </summary>
        /// 
        [OperationContract]
        List<FriendlyLink> GetFriendlyLinkList(int collegeId, int homePageId);


        /// <summary>
        /// 添加友情链接
        /// </summary>      
        [OperationContract]
        int AddFriendLink(FriendlyLink linkModel);

        /// <summary>
        /// 更新友情链接
        /// </summary>      
        [OperationContract]
        bool UpdateFriendLink(FriendlyLink linkModel);

         /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// 
        [OperationContract]
        FriendlyLink GetFriendLinkModel(int Id);

         /// <summary>
        /// 删除一条数据
        /// </summary>
        /// 
        [OperationContract]
        bool DeleteFriendlyLink(int Id);
    }
}
