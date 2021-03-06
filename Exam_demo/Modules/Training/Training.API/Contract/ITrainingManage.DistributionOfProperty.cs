﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;


namespace Training.API
{
    public partial interface ITrainingManage
    {
        /// <summary>
        ///    新增现金规划分析页面
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        int AddDistributionOfProperty(DistributionOfProperty model);
        /// <summary>
        /// 修改现金规划页面
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateDistributionOfProperty(DistributionOfProperty model);
        /// <summary>
        /// 获取现金规划
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        DistributionOfProperty GetDistributionOfProperty(int id);

        /// <summary>
        /// 获取现金规划---根据建议书Id
        /// </summary>
        /// <param name="proposalId">建议书id</param>
        /// <returns></returns>
        [OperationContract]
        DistributionOfProperty GetDistributionOfPropertyByProposalId(int proposalId);

        /// <summary>
        /// 获取现金规划列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
       [OperationContract]
        List<DistributionOfProperty> GetDistributionOfPropertyList(CustomFilter filter);
    }
}



