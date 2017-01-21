using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Training.API;
using Utils;

namespace Training.Svr
{
    public partial class TrainingManage : ITrainingManage
    {
        private CaseDAL caseDAL = new CaseDAL();

        /// <summary>
        /// 获取案例分页列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页(可为空,为空时返回所有数据)</param>
        /// <param name="pageSize">每页页数(可为空,为空时返回所有数据)</param>
        /// <param name="totalCount">总数量</param>
        /// <returns></returns>
        public List<Case> GetCasePage(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount)
        {
            totalCount = 0;
            List<Case> result = new List<Case>();
            try
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    result = DBHelper.GetPageList<Case>(pageIndex.Value, pageSize.Value, caseDAL.GetCasePageParams(filter), out totalCount);
                }
                else
                {
                    result = caseDAL.GetList(filter);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetCasePage方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取案例
        /// </summary>
        /// <param name="Id">案例Id</param>
        /// <returns></returns>
        public Case GetCase(int Id)
        {
            Case result = null;
            try
            {
                result = caseDAL.GetModel(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetCase方法出错", ex);
            }
            return result;
        }


        /// <summary>
        /// 获取案例（根据理财类型）
        /// </summary>
        /// <param name="FinancialTypeId">理财类型Id</param>
        /// <returns></returns>
        public Case GetCase2(int FinancialTypeId)
        {
            Case result = null;
            try
            {
                result = caseDAL.GetModel2(FinancialTypeId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetCase2方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 新增案例
        /// </summary>
        /// <param name="model">案例实体</param>
        /// <returns>Id</returns>
        public int AddCase(Case model)
        {
            int result = 0;
            try
            {
                result = caseDAL.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddCase方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 更新案例
        /// </summary>
        /// <param name="model">案例实体</param>
        /// <returns>是否成功</returns>
        public bool UpdateCase(Case model)
        {
            bool result = false;
            try
            {
                result = caseDAL.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateCase方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 删除案例
        /// </summary>
        /// <param name="Id">案例Id</param>
        /// <returns>是否成功</returns>
        public bool DeleteCase(int Id)
        {
            bool result = false;
            try
            {
                result = caseDAL.Delete(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DeleteCase方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 屏蔽案例
        /// </summary>
        /// <param name="Id">案例Id</param>
        /// <param name="userId">用户Id</param>
        /// <returns>是否成功</returns>
        public bool HiddenCase(int Id, int userId)
        {
            bool result = false;
            try
            {
                result = caseDAL.Hidden(Id, userId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("HiddenCase方法出错", ex);
            }
            return result;
        }
        /// <summary>
        /// 校验是否重复
        /// </summary>
        /// <param name="CaseId">案例主键</param>
        /// <param name="IDNum">身份证号</param>
        /// <returns>返回值为True:不存在重复</returns>
        public bool CheckRepeat(int CaseId, string IDNum)
        {
            bool result = false;
            try
            {
                result = caseDAL.CheckRepeat(CaseId, IDNum);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("HiddenCase方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 检查案列是否被用在未发布的销售机会/实训中，编辑和删除前需要判断
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="status"></param>
        /// <param name="caseId"></param>
        /// <returns></returns>

        public int CheckCaseByUsed(int userId, int status, int caseId)
        {
            int result = 0;
            try
            {
                result = caseDAL.CheckCaseByUsed(userId, status, caseId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("CheckCaseByUsed方法出错", ex);
            }
            return result;
        }

        #region 检查自定义案列是否被用在竞赛中
        /// <summary>
        /// 检查自定义案列是否被用在竞赛中
        /// </summary>
        /// <param name="status"></param>
        /// <param name="caseId"></param>
        /// <returns></returns>

        public int CheckCaseInMatch(int status, int caseId)
        {
            int result = 0;
            try
            {
                result = caseDAL.CheckCaseInMatch(status, caseId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("CheckCaseUsedInMatch方法出错", ex);
            }
            return result;
        } 
        #endregion

        /// <summary>
        /// 修改案例状态（屏蔽，发布，删除）
        /// </summary>
        /// <param name="caseId">案例Id</param>
        /// <param name="type">操作类型</param>
        /// <returns>是否成功</returns>
        public bool ChangeCaseStatus(int caseId, int type)
        {
            try
            {
                return caseDAL.ChangeCaseStatus(caseId, type);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("ChangeCaseStatus方法出错", ex);
            }
            return false;
        }

        /// <summary>
        /// 修改案例查看状态
        /// </summary>
        /// <param name="caseId">案例Id</param>
        /// <returns>是否成功</returns>
        public bool ChangeCaseViewStatus(int caseId)
        {
            try
            {
                return caseDAL.ChangeCaseViewStatus(caseId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("ChangeCaseViewStatus方法出错", ex);
            }
            return false;
        }
    }
}
