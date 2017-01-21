using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.API;
using VM;

namespace Server.Factory
{
    public partial class TrainingService : IService<ITrainingManage>
    {

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="modelVMs"></param>
        /// <returns></returns>
        public int AddBlukP2PProduce(List<P2PProductVM> modelVMs)
        {
            int result = 0;
            List<P2PProduct> models = modelVMs.MapList<P2PProduct, P2PProductVM>();
            result = MyService.AddBlukP2PProduce(models);
            return result;

        }

        /// <summary>
        /// 存入缓存
        /// </summary>
        /// <returns></returns>
        public List<P2PProductVM> GetP2PProductbyCache()
        {
            int totalCount = 0;
            CustomFilter filter = new Training.API.CustomFilter();
            var list = MyService.GetP2PProductList(filter,null,null,out totalCount);
            List<P2PProductVM> result = list.MapList<P2PProductVM,P2PProduct>();
            return result;
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public PagedList<P2PProductVM> GetP2PProductList(string keywords, int pageIndex, int pageSize, out int totalCount)
        {
            totalCount = 0;
            CustomFilter filter = new CustomFilter() { KeyWords=keywords };
            List<P2PProduct> list = MyService.GetP2PProductList(filter,pageIndex,pageSize,out totalCount);
            List<P2PProductVM> rtnValue = list.MapList<P2PProductVM, P2PProduct>();
            PagedList<P2PProductVM> result = new PagedList<P2PProductVM>(rtnValue,pageIndex,pageSize, totalCount);
            return result;

 
        }

        /// <summary>
        /// 获取数据库原有列表，
        /// </summary>
        /// <returns></returns>
        public List<P2PProductVM> GetOldP2PProductList()
        {
            int totalCount=0;
            CustomFilter filter = new CustomFilter();
            List<P2PProduct> list = MyService.GetP2PProductList(filter, null, null, out totalCount);
            List<P2PProductVM> rtnValue = list.MapList<P2PProductVM, P2PProduct>();
            return rtnValue;
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="modeVMS"></param>
        /// <returns></returns>
        public bool UpdateP2PProduct(List<P2PProductVM> modeVMS)
        {
            bool result = false;
            List<P2PProduct> models = modeVMS.MapList<P2PProduct,P2PProductVM>();
            result = MyService.UpdateP2PProductBluk(models);
            return result;
 
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sourceIds"></param>
        /// <returns></returns>
        public bool DeleteP2PProductBluk(List<string> sourceIds)
        {
            bool result = false;
            result = MyService.DeleteP2PProduct(sourceIds);
            return result;
        }

        /// <summary>
        /// 删除P2P重复数据
        /// </summary>
        /// <returns></returns>
        public bool DeleteRepeatP2P()
        {
            return MyService.DeleteRepeatP2P();
        }

     }
}
