using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Structure.API
{
    public partial interface IStructureManage
    {
        /*
         * 1.班级的增删改查（单个）
         * 2.班级的删除（批量）
         * 3.班级的分页查询
         * 4.班级的状态更新（批量）
         * 5.班级和用户关系新增/删除
         */
        #region 获取或新增班级信息

        /// <summary>
        /// 依据班级Id，获取班级信息
        /// </summary>
        /// <param name="id">班级id</param>
        /// <returns></returns>
        [OperationContract]
        Class GetClass(int id);


        /// <summary>
        /// 班级的分页查询
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页(可为空,为空时返回所有数据)</param>
        /// <param name="pageSize">每页页数(可为空,为空时返回所有数据)</param>
        /// <param name="totalCount">总数量</param>
        /// <returns></returns>
        [OperationContract]
        List<Class> GetClassPage(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount);

        /// <summary>
        /// 添加班级
        /// </summary>
        /// <param name="model">班级实体</param>
        /// <returns></returns>
        [OperationContract]
        int AddClass(Class model);
        #endregion

        #region 班级用户关系
        /// <summary>
        /// 添加班级及用户的关系
        /// </summary>
        /// <param name="model">班级用户关系视图</param>
        /// <returns></returns>
        [OperationContract]
        int AddUserClass(UserClass model);

        /// <summary>
        /// 添加班级及用户的关系列表
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        [OperationContract]
        List<UserClass> GetUserClassPage(CustomFilter filter);

        /// <summary>
        /// 删除班级用户关系
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteUserClass(UserClass model);
        #endregion

        #region 删除班级(单个/批量）
        /// <summary>
        /// 依据班级Id，删除班级
        /// </summary>
        /// <param name="id">班级Id</param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteClass(int id);

        /// <summary>
        /// 批量删除班级
        /// </summary>
        /// <param name="ids">将班级id批量传入，用于删除选定的班级信息</param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteClassBulk(List<int> ids);
        #endregion

        #region 更新班级

        /// <summary>
        /// 更新班级信息
        /// </summary>
        /// <param name="models">班级models</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateClass(Class model);

        
        /// <summary>
        /// 批量更新班级状态
        /// </summary>
        /// <param name="classId">班级id列表</param>
        /// <param name="statusId">状态ID</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateClassStatus(List<int> classId, int statusId);

        #endregion


    }
}
