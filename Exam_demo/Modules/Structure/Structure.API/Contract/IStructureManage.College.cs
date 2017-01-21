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

        /// <summary>
        /// 根据Id获取学院实体
        /// </summary>
        /// <param name="Id">学院Id</param>
        /// <returns></returns>
        [OperationContract]
        College GetCollege(int Id);

        /// <summary>
        /// 新增学院
        /// </summary>
        /// <param name="model">学院实体</param>
        /// <returns></returns>
        [OperationContract]
        int AddCollege(College model);

        /// <summary>
        /// 更新学院
        /// </summary>
        /// <param name="model">学院实体</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateCollege(College model);

        /// <summary>
        /// 删除学院
        /// </summary>
        /// <param name="Id">学院Id</param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteCollege(int Id);

        /// <summary>
        /// 获取学院分页列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页(可为空,为空时返回所有数据)</param>
        /// <param name="pageSize">每页页数(可为空,为空时返回所有数据)</param>
        /// <param name="totalCount">总数量</param>
        /// <returns></returns>
        [OperationContract]
        List<College> GetCollegePage2(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount);

        /// <summary>
        /// 检测学院名称是否重复
        /// </summary>
        /// <param name="Id">学院Id</param>
        /// <param name="collegeName">学院名称</param>
        /// <returns></returns>
        [OperationContract]
        bool GetCollegeExist(int Id, string collegeName);

        /// <summary>
        /// 获取学校名称列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<string> GetCollegeNameList();


        [OperationContract]
        College GetCollegeModel(CustomFilter filter);

        [OperationContract]
        College GetCollegeModel2(CustomFilter filter);

        /// <summary>
        /// 根据domainName获取学校
        /// </summary> 
        /// 
        [OperationContract]
        College getCollegeByDomainName(string domainName);


        /// <summary>
        /// 获取学校模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        College GetCollegeModelById(int id);

    }
}
