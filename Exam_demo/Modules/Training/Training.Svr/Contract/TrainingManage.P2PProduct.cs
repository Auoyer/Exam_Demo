using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.API;
using Utils;

namespace Training.Svr
{
    public partial class TrainingManage : ITrainingManage
    {
        P2PProductDAL p2PProductDAL = new P2PProductDAL();
        /// <summary>
        ///  批量新增。爬起数据入库
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public int AddBlukP2PProduce(List<P2PProduct> models)
        {
            int result = 0;
            try
            {
                result = p2PProductDAL.AddBluk(models);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddBlukP2PProduce", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<P2PProduct> GetP2PProductList(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount)
        {
            totalCount = 0;
            List<P2PProduct> result = new List<P2PProduct>();
            try
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    result = DBHelper.GetPageList<P2PProduct>(pageIndex.Value, pageSize.Value, p2PProductDAL.GetP2PProductPageParams(filter), out totalCount);
                }
                else
                {
                    result = p2PProductDAL.GetList(filter);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddBlukP2PProduce", ex);

            }
            return result;
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public bool UpdateP2PProductBluk(List<P2PProduct> models)
        {
            bool result = false;
            try
            {
                result = p2PProductDAL.UpdateBluk(models);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateP2PProductBluk", ex);
            }
            return result;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sourceIds"></param>
        /// <returns></returns>
        public bool DeleteP2PProduct(List<string> sourceIds)
        {
            bool result = false;
            try
            {
                result = p2PProductDAL.DeleteBluk(sourceIds);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DeleteP2PProduct", ex);
            }
            return result;
        }

        /// <summary>
        /// 删除P2P重复数据
        /// </summary>
        /// <returns></returns>
        public bool DeleteRepeatP2P()
        {
            bool result = false;
            try
            {
                result = p2PProductDAL.DeleteRepeatP2P();
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DeleteRepeatP2P", ex);
            }
            return result;
        }


    }
}
