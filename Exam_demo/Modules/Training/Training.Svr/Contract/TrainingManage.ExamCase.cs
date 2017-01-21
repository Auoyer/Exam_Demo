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
        private ExamCaseDAL examCaseDAL = new ExamCaseDAL();


        /// <summary>
        /// 获取案例
        /// </summary>
        /// <param name="Id">案例Id</param>
        /// <returns>Case实体</returns>
        public ExamCase GetExamCase(int Id)
        {
            ExamCase result = null;
            try
            {
                result = examCaseDAL.GetModel(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetExamCase方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取案例
        /// </summary>
        /// <param name="TrainExamId">销售机会/实训考核Id</param>
        /// <returns></returns>
        public ExamCase GetExamCaseByTrainExamId(int TrainExamId)
        {
            ExamCase result = null;
            try
            {
                result = examCaseDAL.GetModelByTrainExamId(TrainExamId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetExamCase方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 新增案例
        /// </summary>
        /// <param name="model">案例实体</param>
        /// <returns>Id</returns>
        public int AddExamCase(ExamCase model)
        {
            int result = 0;
            try
            {
                result = examCaseDAL.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddExamCase方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 更新案例
        /// </summary>
        /// <param name="model">案例实体</param>
        /// <returns>是否成功</returns>
        public bool UpdateExamCase(ExamCase model)
        {

            bool result = false;
            try
            {
                result = examCaseDAL.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateExamCase方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 删除案例
        /// </summary>
        /// <param name="Id">案例Id</param>
        /// <returns>是否成功</returns>
        public bool DeleteExamCase(int Id)
        {
            bool result = false;
            try
            {
                result = examCaseDAL.Delete(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DeleteExamCase方法出错", ex);
            }
            return result;
        }
        /// <summary>
        /// 删除案例
        /// </summary>
        /// <param name="Id">案例Id</param>
        /// <returns>是否成功</returns>
        public bool ForTrainExamIdDeleteExamCase(int TrainExamId)
        {
            bool result = false;
            try
            {
                result = examCaseDAL.Delete2(TrainExamId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("ForTrainExamIdDeleteExamCase方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取案例全部数据
        /// </summary>
        /// <param name="model">案例实体</param>
        /// <returns>案例实体列表</returns>
        public List<ExamCase> GetAllExamCaseList(CustomFilter model)
        {
            List<ExamCase> result = null;
            try
            {
                result = examCaseDAL.GetList(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetAllExamCaseList方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取案例分页列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页(可为空,为空时返回所有数据)</param>
        /// <param name="pageSize">每页页数(可为空,为空时返回所有数据)</param>
        /// <param name="totalCount">总数量</param>
        /// <returns></returns>
        public List<ExamCase> GetExamCasePage(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount)
        {
            totalCount = 0;
            List<ExamCase> result = new List<ExamCase>();
            try
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    result = DBHelper.GetPageList<ExamCase>(pageIndex.Value, pageSize.Value, examCaseDAL.GetExamCasePageParams(filter), out totalCount);
                }
                else
                {
                    result = examCaseDAL.GetList(filter);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetCasePage方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取案例分页列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页(可为空,为空时返回所有数据)</param>
        /// <param name="pageSize">每页页数(可为空,为空时返回所有数据)</param>
        /// <param name="totalCount">总数量</param>
        /// <returns></returns>
        public List<ExamCaseTrainExam> GetSalesJudgeList(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount)
        {
            totalCount = 0;
            List<ExamCaseTrainExam> result = new List<ExamCaseTrainExam>();
            try
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    result = DBHelper.GetPageList<ExamCaseTrainExam>(pageIndex.Value, pageSize.Value, examCaseDAL.GetSalesJudgePageParams(filter), out totalCount);
                }
                else
                {
                    result = examCaseDAL.GetSalesJudgeList(filter);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetCasePage方法出错", ex);
            }
            return result;
        }
    }
}
