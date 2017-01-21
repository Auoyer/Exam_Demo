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
        private TrainExamDAL trainExamDAL = new TrainExamDAL();
        /// <summary>
        /// 销售机会列表
        /// </summary>
        /// <returns></returns>
        public List<TrainExam> GetTrainExamList(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount)
        {
            totalCount = 0;
            List<TrainExam> list = new List<TrainExam>();
            try
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    list = DBHelper.GetPageList<TrainExam>(pageIndex.Value, pageSize.Value, trainExamDAL.GetTrainExamPageParams(filter), out totalCount);
                }
                else { list = trainExamDAL.GetList(filter); }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetTrainExamList方法出错", ex);
            }
            return list;
        }

        /// <summary>
        /// 获取销售机会列表全部数据
        /// </summary>
        /// <returns></returns>
        public List<TrainExam> GetAllTrainExamList(CustomFilter model)
        {
            List<TrainExam> result=null;
            try
            {
                result = trainExamDAL.GetList(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetAllTrainExamList方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取销售机会列表全部数据
        /// </summary>
        /// <returns></returns>
        public List<UnitTrainExam> GetList2(CustomFilter model)
        {
            List<UnitTrainExam> result = null;
            try
            {
                result = trainExamDAL.GetList3(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetList2方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取销售机会列表全部数据
        /// </summary>
        /// <returns></returns>
        public List<UnitTrainExam> GetList3(CustomFilter model)
        {
            List<UnitTrainExam> result = null;
            try
            {
                result = trainExamDAL.GetList4(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetList3方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取销售机会列表全部数据
        /// </summary>
        /// <returns></returns>
        public List<UnitTrainExam> GetList4(CustomFilter model)
        {
            List<UnitTrainExam> result = null;
            try
            {
                result = trainExamDAL.GetList5(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetList4方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取销售机会列表全部数据
        /// </summary>
        /// <returns></returns>
        public List<UnitTrainExam> GetList5(CustomFilter model)
        {
            List<UnitTrainExam> result = null;
            try
            {
                result = trainExamDAL.GetList6(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetList5方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取销售机会列表全部数据
        /// </summary>
        /// <returns></returns>
        public List<UnitTrainExam> GetExamAndClass(CustomFilter model)
        {
            List<UnitTrainExam> result = null;
            try
            {
                result = trainExamDAL.GetList2(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetExamAndClass方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 学生端销售机会列表联表获取
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="totalCount">总条数</param>
        /// <returns></returns>
        public List<UnitTrainExam> GetTrainExamListUnit(CustomFilter filter,int? pageIndex, int? pageSize, out int totalCount)
        {
            totalCount = 0;
            List<UnitTrainExam> list = new List<UnitTrainExam>();
            try
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    list = DBHelper.GetPageList<UnitTrainExam>(pageIndex.Value, pageSize.Value, trainExamDAL.GetDetailTrainExamPageParams(filter), out totalCount);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetTrainExamListUnit方法出错", ex);
            }
            return list;
        }

        /// <summary>
        /// 学生端销售机会列表联表获取
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<UnitTrainExam> GetTrainExamAndProposal(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount)
        {
            totalCount = 0;
            List<UnitTrainExam> list = new List<UnitTrainExam>();
            try
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    list = DBHelper.GetPageList<UnitTrainExam>(pageIndex.Value, pageSize.Value, trainExamDAL.GetTrainExamAndProposal(filter), out totalCount);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetTrainExamAndProposal方法出错", ex);
            }
            return list;
        }

        /// <summary>
        /// 学生端销售机会列表联表获取
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<UnitTrainExam> GetTrainExamAndAssessmentResults(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount)
        {
            totalCount = 0;
            List<UnitTrainExam> list = new List<UnitTrainExam>();
            try
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    list = DBHelper.GetPageList<UnitTrainExam>(pageIndex.Value, pageSize.Value, trainExamDAL.GetTrainExamAndAssessmentResults(filter), out totalCount);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetTrainExamAndAssessmentResults方法出错", ex);
            }
            return list;
        }

        /// <summary>
        /// 教师端实训考核列表联表获取
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<UnitTrainExam> GetTecTrainExamListUnit(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount)
        {
            totalCount = 0;
            List<UnitTrainExam> list = new List<UnitTrainExam>();
            try
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    list = DBHelper.GetPageList<UnitTrainExam>(pageIndex.Value, pageSize.Value, trainExamDAL.GetTecDetailTrainExamPageParams(filter), out totalCount);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetTecTrainExamListUnit方法出错", ex);
            }
            return list;
        }

        /// <summary>
        /// 新增销售机会
        /// </summary>
        /// <param name="trainExam"></param>
        /// <returns></returns>
        public int AddTrainExam(TrainExam model)
        {
            int result = 0;
            try
            {
                result = trainExamDAL.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddTrainExam方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 修改销售机会
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateTrainExam(TrainExam model)
        {
            bool result = false;
            try
            {
                result = trainExamDAL.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("TrainExam方法出错", ex);
            }
            return result;
        }

       

        /// <summary>
        /// 删除销售机会
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteTrainExam(int id)
        {
            bool result = false;
            try
            {
                result = trainExamDAL.Delete(id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DeleteTrainExam方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 是否存在销售机会
        /// </summary>
        /// <param name="id">销售机会ID</param>
        /// <returns></returns>
        public bool Exists(int id)
        {
            bool result = false;
            try
            {
                result = trainExamDAL.Exists(id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("Exists方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取销售机会实体
        /// </summary>
        /// <param name="id">销售机会ID</param>
        /// <returns></returns>
        public TrainExam GetTrainExam(int id)
        {
            TrainExam result=new TrainExam();
            try
            {
                result = trainExamDAL.GetModel(id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetTrainExam方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 编辑销售机会/实训考核
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool EditTrainExam(TrainExam model)
        {
            bool result = false;
            try
            {
                result = trainExamDAL.EditTrainExam(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("EditTrainExam方法出错", ex);
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TrainExamId"></param>
        /// <returns></returns>
        public TrainExam GetTrainExamInfo(int TrainExamId)
        { 
            TrainExam result = null;
            try
            {
                result = trainExamDAL.GetTrainExam(TrainExamId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetTrainExamInfo方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取对应的考核分数
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<TrainExamAssessment> GetTranExamAssessmentByUserId(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount)
        {
            totalCount = 0;
            List<TrainExamAssessment> list = new List<TrainExamAssessment>();
            try
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {

                    list = DBHelper.GetPageList<TrainExamAssessment>(pageIndex.Value, pageSize.Value, trainExamDAL.GetTranExamAssessmentByUserId(filter), out totalCount);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetTranExamAssessmentByUserId方法出错", ex);
            }
            return list;
        }

        /// <summary>
        /// 获取自主实训
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<TrainExamAssessment> GettTranExamAssessmentPageSelfTrain(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount)
        {
            totalCount = 0;
            List<TrainExamAssessment> list = new List<TrainExamAssessment>();
            try
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                     
                    list = DBHelper.GetPageList<TrainExamAssessment>(pageIndex.Value, pageSize.Value, trainExamDAL.GettTranExamAssessmentPageSelfTrain(filter), out totalCount);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetTranExamAssessmentByUserId方法出错", ex);
            }
            return list;
        }

        /// <summary>
        /// 获取未评分考核ID
        /// </summary>
        /// <returns></returns>
        public List<int> GetNoScoreExamId()
        {
            List<int> list = new List<int>();
            try
            {
                list = trainExamDAL.GetNoScoreExamId();
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetNoScoreExamId方法出错", ex);
            }
            return list;
        }

        /// <summary>
        /// 班级集合中，是否有已发布的销售机会/实训考核
        /// </summary>
        /// <param name="classId">班级集合</param>
        /// <returns></returns>
        public bool IsPublish(List<int> classId)
        {
            bool result = false;
            try
            {
                result = trainExamDAL.IsPublish(classId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("IsPublish方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 根据条件统计销售机会/实训考核
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <returns></returns>
        public int CountTrainExam(CustomFilter filter)
        {
            int result = 0;
            try
            {
                result = trainExamDAL.Count(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("CountTrainExam方法出错", ex);
            }
            return result;
        }


        /// <summary>
        /// 获取大赛的案例列表
        /// </summary>
        /// <param name="CompetitionId"></param>
        /// <returns></returns>
        public List<TrainExam> GetTrainExamWithDetail(int CompetitionId)
        {
            List<TrainExam> result = null;
            try
            {
                result = trainExamDAL.GetTrainExamWithDetail(CompetitionId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetTrainExamWithDetail方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取大赛的案例列表
        /// </summary>
        /// <param name="CompetitionId"></param>
        /// <returns></returns>
        public List<TrainExam> GetTrainExamByMatch(int CompetitionId)
        {
            List<TrainExam> result = null;
            try
            {
                result = trainExamDAL.GetTrainExamByMatch(CompetitionId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetTrainExamByMatch方法出错", ex);
            }
            return result;
        }
    }
}
