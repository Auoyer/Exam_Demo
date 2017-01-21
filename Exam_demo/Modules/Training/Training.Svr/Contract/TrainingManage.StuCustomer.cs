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
        private StuCustomerDAL stuCustomerDAL = new StuCustomerDAL();

        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <returns></returns>
        public List<StuCustomer> GetStuCustomerList(CustomFilter filter)
        {
            List<StuCustomer> list = new List<StuCustomer>();
            try
            {
                list = stuCustomerDAL.GetList(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetStuCustomerList方法出错", ex);
            }
            return list;
        }

        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <returns></returns>
        public List<StuCustomer> GetStuCustomerList2(CustomFilter filter)
        {
            List<StuCustomer> list = new List<StuCustomer>();
            try
            {
                list = stuCustomerDAL.GetList2(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetStuCustomerList2方法出错", ex);
            }
            return list;
        }

        /// <summary>
        /// 获取客户信息及日程表联表分页列表
        /// </summary>
        /// <param name="filter">条件搜索</param>
        /// <returns></returns>
        public List<StuCustomer> GetUnitStuCustomerList(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount)
        {
            totalCount = 0;
            List<StuCustomer> list = new List<StuCustomer>();
            try
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    list = DBHelper.GetPageList<StuCustomer>(pageIndex.Value, pageSize.Value, stuCustomerDAL.GetStuCustomerPageParams(filter), out totalCount);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetUnitStuCustomerList方法出错", ex);
            }
            return list;
        }

        /// <summary>
        /// 新增客户信息
        /// </summary>
        /// <param name="stuCustomer"></param>
        /// <returns></returns>
        public int AddStuCustomer(StuCustomer stuCustomer)
        {
            int addStuCustomer = 0;
            try
            {
                addStuCustomer = stuCustomerDAL.Add(stuCustomer);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddStuCustomer方法出错", ex);
            }
            return addStuCustomer;
        }

        /// <summary>
        /// 修改客户信息
        /// </summary>
        /// <param name="stuCustomer"></param>
        /// <returns></returns>
        public bool UpdateCustomer(StuCustomer stuCustomer)
        {
            bool update = false;
            try
            {
                update = stuCustomerDAL.Update(stuCustomer);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateCustomer方法出错", ex);
            }
            return update;
        }

        /// <summary>
        /// 删除客户信息(真删)
        /// 同步删除以下信息：
        /// 1.建议书(真删)
        /// 2.工作日程(真删)
        /// 3.....后续根据需求增加
        /// </summary>
        /// <param name="Id">客户Id</param>
        /// <returns></returns>
        public bool DelCustomer(int Id)
        {
            bool result = false;
            try
            {
                result = stuCustomerDAL.Delete(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DelCustomer方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 删除客户信息(伪删)
        /// 同步删除以下信息：
        /// 1.建议书(伪删)
        /// 2.工作日程(真删)
        /// 3.....后续根据需求增加
        /// </summary>
        /// <param name="Id">客户Id</param>
        /// <returns></returns>
        public bool RemoveCustomer(int Id)
        {
            bool result = false;
            try
            {
                result = stuCustomerDAL.RemoveCustomer(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("RemoveCustomer方法出错", ex);
            }
            return result;
        }



        /// <summary>
        /// 获取潜在客户/已有客户信息，日程管理要用到
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<StuCustomer> GetStuCustomerListByCalendar(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount)
        {
            totalCount = 0;
            List<StuCustomer> result = new List<StuCustomer>();
            try
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    result = DBHelper.GetPageList<StuCustomer>(pageIndex.Value, pageSize.Value, stuCustomerDAL.GetStuCustomerPageParams(filter), out totalCount);
                }
                else
                {
                    result = stuCustomerDAL.GetList(filter);
                }
            }
            catch (Exception ex)
            { 
                LogHelper.Log.WriteError("GetStuCustomerListByCalendar方法出错", ex); 
            }
            return result;
        }

        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <param name="Id">客户Id</param>
        /// <returns></returns>
        public StuCustomer GetStuCustomer(int Id)
        {
            StuCustomer sCustomer = new StuCustomer();
            try
            {
                sCustomer = stuCustomerDAL.GetModel(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetStuCustomer方法出错", ex);
            }
            return sCustomer;
        }

        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <param name="Id">客户Id</param>
        /// <returns></returns>
        public StuCustomer GetStuCustomerObj(int TrainExamId)
        {
            StuCustomer sCustomer = new StuCustomer();
            try
            {
                sCustomer = stuCustomerDAL.GetModel2(TrainExamId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetStuCustomerObj方法出错", ex);
            }
            return sCustomer;
        }

        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <param name="IDNum">客户证件号</param>
        /// <param name="UserId">学生Id</param>
        /// <returns>客户信息实体</returns>
        public StuCustomer GetStuCustomerByIDNum(string IDNum, int UserId)
        {
            StuCustomer model = new StuCustomer();
            try
            {
                model = stuCustomerDAL.GetModel(IDNum, UserId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetStuCustomerByIDNum方法出错", ex);
            }
            return model;
        }
        

        /// <summary>
        /// 更新潜在客户/已有客户的状态和建议书Id
        /// </summary>
        /// <param name="ProposalId">建议书Id</param>
        /// <param name="Status">状态</param>
        /// <param name="Id">潜在客户Id</param> 
        /// <returns></returns>
        public bool UpdateStuCustomerStatusAndProposalId(int ProposalId, int Status, int Id)
        {
            bool result = false;
            try
            {
                result = stuCustomerDAL.UpdateStuCustomerStatusAndProposalId(ProposalId, Status, Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateStuCustomerStatus方法出错", ex);
            }
            return result; 
        }


        /// <summary>
        /// 获取潜在客户人数/已有客户人数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CustomerCount GetCustomerCountModel(int userId)
        {
            CustomerCount model = new CustomerCount();
            try
            {
                 model= stuCustomerDAL.GetCustomerCountModel(userId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetCustomerCountModel方法出错", ex);
            }

            return model;
        }

        /// <summary>
        /// 根据建议书Id更新潜在客户/已有客户的状态
        /// </summary>
        /// <param name="ProposalId">建议书Id</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public bool UpdateStuCustomerStatus(int ProposalId, int Status, int? IsHightCustomer)
        {
            bool result = false;
            try
            {
                result = stuCustomerDAL.UpdateStuCustomerStatus(ProposalId, Status, IsHightCustomer.Value);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateStuCustomerStatus方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 将潜在客户更新为已有客户
        /// </summary> 
        /// <param name="ProposalId">建议书Id</param>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public bool UpdateCustomerType(int ProposalId, int UserId)
        {
            bool result = false;
            try
            {
                result = stuCustomerDAL.UpdateCustomerType(ProposalId, UserId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateCustomerType方法出错", ex);
            }
            return result;

        }

        /// <summary>
        /// 批量将潜在客户更新为已有客户
        /// </summary>
        /// <param name="TrainExamId">销售机会Id</param>
        /// <returns></returns>
        public bool UpdateCustomerTypeList(int TrainExamId)
        {
            bool result = false;
            try
            {
                result = stuCustomerDAL.UpdateCustomerTypeList(TrainExamId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateCustomerTypeList方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 根据客户Id，用户Id更新该客户拥有建议书数量
        /// </summary>
        /// <param name="stuCustomerId">客户Id</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public bool UpdateCustomerProposalCount(int stuCustomerId, int userId)
        {
            bool result = false;
            try
            {
                result = stuCustomerDAL.UpdateCustomerProposalCount(stuCustomerId, userId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateCustomerProposalCount方法出错", ex);
            }
            return result;
        }

    }
}
