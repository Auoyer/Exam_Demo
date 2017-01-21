using Structure.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Structure.Svr
{
    public partial class StructureManage
    {
        private CollegeDAL collegeDAL = new CollegeDAL();

        /// <summary>
        /// 获取学院分页列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页(可为空,为空时返回所有数据)</param>
        /// <param name="pageSize">每页页数(可为空,为空时返回所有数据)</param>
        /// <param name="totalCount">总数量</param>
        /// <returns></returns>
        public List<College> GetCollegePage2(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount)
        {
            totalCount = 0;
            List<College> result = new List<College>();
            try
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    result = DBHelper.GetPageList<College>(pageIndex.Value, pageSize.Value, collegeDAL.GetCollegePageParams(filter), out totalCount);
                }
                else
                {
                    result = collegeDAL.GetList(filter);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetCollegePage方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 根据Id获取学院实体
        /// </summary>
        /// <param name="Id">学院Id</param>
        /// <returns></returns>
        public College GetCollege(int Id)
        {
            College result = null;
            try
            {
                result = collegeDAL.GetModel(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetCollege方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 检测学院名称是否重复
        /// </summary>
        /// <param name="Id">学院Id</param>
        /// <param name="collegeName">学院名称</param>
        /// <returns></returns>
        public bool GetCollegeExist(int Id, string collegeName)
        {
            bool result = false;
            try
            {
                result = collegeDAL.Exists(Id, collegeName);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetCollegeExist方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 新增学院
        /// </summary>
        /// <param name="model">学院实体</param>
        /// <returns></returns>
        public int AddCollege(College model)
        {
            int result = 0;
            try
            {
                result = collegeDAL.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddCollege方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 更新学院
        /// </summary>
        /// <param name="model">学院实体</param>
        /// <returns></returns>
        public bool UpdateCollege(College model)
        {
            bool result = false;
            try
            {
                result = collegeDAL.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddCollege方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 删除学院
        /// </summary>
        /// <param name="Id">学院Id</param>
        /// <returns></returns>
        public bool DeleteCollege(int Id)
        {
            bool result = false;
            try
            {
                result = collegeDAL.Delete(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DeleteCollege方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取学校名称列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetCollegeNameList()
        {

            List<string> result = null;
            try
            {
                result = collegeDAL.GetCollegeNameList();
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetCollegeNameList方法出错", ex);
            }
            return result;
        }

        public College GetCollegeModel(CustomFilter filter)
        {
            return collegeDAL.GetList(filter).FirstOrDefault();
        }

        public College GetCollegeModel2(CustomFilter filter)
        {
            return collegeDAL.GetList2(filter).FirstOrDefault();
        }

        /// <summary>
        /// 根据domainName获取学校
        /// </summary>     
        public College getCollegeByDomainName(string domainName)
        {
            College result = null;
            try
            {
                result = collegeDAL.getCollegeByDomainName(domainName);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("getCollegeByDomainName方法出错", ex);
            }
            return result;
        }


        /// <summary>
        /// 获取学校模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public College GetCollegeModelById(int id)
        {
            College result = new College();
            try
            {
                result = collegeDAL.GetModel(id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetCollegeModel方法出错", ex);
            }
            return result;
        }
    }
}
