using Structure.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Structure.Svr
{
    public partial class StructureManage
    {

        private ClassDAL classDAL = new ClassDAL();
        private UserClassDAL userclassDAL = new UserClassDAL();

        #region 获取或新增班级信息

        /// <summary>
        /// 依据班级Id，获取班级信息
        /// </summary>
        /// <param name="id">班级id</param>
        /// <returns></returns>
        public Class GetClass(int id)
        {
            Class result = null;
            try
            {
                result = classDAL.GetModel(id);
            }
            catch (Exception ex)
            {
                //TODO:修改错误返回信息
                LogHelper.Log.WriteError("GetClass()执行出错,错误信息{0}", ex);
            }

            return result;
        }


        /// <summary>
        /// 班级的分页查询
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页(可为空,为空时返回所有数据)</param>
        /// <param name="pageSize">每页页数(可为空,为空时返回所有数据)</param>
        /// <param name="totalCount">总数量</param>
        /// <returns></returns>
        public List<Class> GetClassPage(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount)
        {
            totalCount = 0;
            List<Class> result = new List<Class>();
            try
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    result = DBHelper.GetPageList<Class>(pageIndex.Value, pageSize.Value, classDAL.GetClassPageParams(filter), out totalCount);
                }
                else
                {
                    result = classDAL.GetList(filter);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetClassPage方法出错", ex);
            }
            return result;
        }



        /// <summary>
        /// 添加班级
        /// </summary>
        /// <param name="model">班级实体</param>
        /// <returns></returns>
        public int AddClass(Class model)
        {
            int result = 0;
            try
            {
                result = classDAL.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddClass", ex);
            }
            return result;
        }

        #endregion

        #region 班级用户关系
        /// <summary>
        /// 添加班级及用户的关系
        /// </summary>
        /// <param name="model">班级用户关系视图</param>
        /// <returns></returns>
        public int AddUserClass(UserClass model)
        {
            int result = 0;
            try
            {
                result = classDAL.AddUserClass(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddUserClass执行失败", ex);
            }
            return result;
        }


        public List<UserClass> GetUserClassPage(CustomFilter filter)
        {
            List<UserClass> result = new List<UserClass>();
            try
            {

                result = userclassDAL.GetList(filter);
                
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetUserClassPage方法出错", ex);
            }
            return result;
        }


        /// <summary>
        /// 删除班级用户关系
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public bool DeleteUserClass(UserClass model)
        {
            bool result = false;
            try
            {
                result = classDAL.DeleteUserClass(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DeleteUserClass执行失败!", ex);
            }
            return result;
        }
        #endregion

        #region 删除班级(单个/批量）
        /// <summary>
        /// 依据班级Id，删除班级
        /// </summary>
        /// <param name="id">班级Id</param>
        /// <returns></returns>
        public bool DeleteClass(int id)
        {
            bool result = false;
            try
            {
                result = classDAL.Delete(id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DeleteClass方法执行失败!", ex);
            }
            return result;
        }

        /// <summary>
        /// 批量删除班级
        /// </summary>
        /// <param name="ids">将班级id批量传入，用于删除选定的班级信息</param>
        /// <returns></returns>
        public bool DeleteClassBulk(List<int> ids)
        {
            bool result = false;
            try
            {
                result = classDAL.DeleteBulk(ids);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DeleteClassBulk执行失败", ex);
            }
            return result;
        }
        #endregion

        #region 更新班级

        /// <summary>
        /// 依据班级models更新班级状态
        /// </summary>
        /// <param name="models">班级models</param>
        /// <returns></returns>
        public bool UpdateClass(Class model)
        {
            bool result = false;
            try
            {
                result = classDAL.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateClass方法执行失败", ex);
            }
            return result;
        }


        /// <summary>
        /// 批量更新班级状态
        /// </summary>
        /// <param name="classId">班级id列表</param>
        /// <param name="statusId">状态ID</param>
        /// <returns></returns>
        public bool UpdateClassStatus(List<int> classId, int statusId)
        {
            bool result = false;
            try
            {
                result = classDAL.UpdateBulk(classId, statusId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateClassBulk方法执行失败", ex);
            }
            return result;
        }
        #endregion
    }
}
