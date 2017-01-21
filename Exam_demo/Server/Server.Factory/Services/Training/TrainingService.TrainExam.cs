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
        /// 学生端销售机会列表联表获取
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<UnitTrainExamVM> GetUnitTrainExamList(TrainSearch search, int pageIndex, int pageSize, out int totalCount)
        {
            totalCount = 0;
            CustomFilter filter = new CustomFilter
            {
                UserId = search.UserId,
                ClassId = search.ClassId,
                FinancialTypeId = search.FinancialTypeId,
                Status = search.Status,
                ExamTypeId = search.ExamTypeId,
                CheckName = search.CheckName,
                CheckStatus = search.CheckStatus,
                KeyWords = search.KeyWords,//客户姓名，身份证号
                CustomerName = search.CustomerName,
                SortName = search.SortName,
                SortWay = search.SortWay,
                isShow = search.isShow,
                Score = search.Score
            };
            List<UnitTrainExam> list = MyService.GetTrainExamListUnit(filter, pageIndex, pageSize, out totalCount);
            List<UnitTrainExamVM> rtnValue = list.MapList<UnitTrainExamVM, UnitTrainExam>();

            return rtnValue;
        }

        /// <summary>
        /// 教师端销售机会列表联表获取
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<UnitTrainExamVM> GetTecTrainExamListUnit(TrainSearch search, int pageIndex, int pageSize, out int totalCount)
        {
            totalCount = 0;
            CustomFilter filter = new CustomFilter
            {
                UserId = search.UserId,
                Status = search.Status,
                ExamTypeId = search.ExamTypeId,
                SortName = search.SortName,
                FinancialTypeId = search.FinancialTypeId,
                SortWay = search.SortWay,
                TrainExamStatus = search.TrainExamStatus,
                isShow = search.isShow,
                IsLessThanCurrentDate = search.IsLessThanCurrentDate,
                Score = search.Score

            };
            //   if (search.TrainExamStatus == 1) { filter.Score = 1; }
            List<UnitTrainExam> rtnValue = MyService.GetTecTrainExamListUnit(filter, pageIndex, pageSize, out totalCount);
            List<UnitTrainExamVM> result = rtnValue.MapList<UnitTrainExamVM, UnitTrainExam>();
            return result;
        }

        /// <summary>
        /// 新增销售机会
        /// </summary>
        /// <param name="model">销售机会实体</param>
        /// <returns></returns>
        public int AddTrainExam(TrainExamVM model)
        {
            int AddTrainExamResult = MyService.AddTrainExam(model.Map<TrainExam, TrainExamVM>());
            return AddTrainExamResult;
        }

        /// <summary>
        /// 修改销售机会
        /// </summary>
        /// <param name="model">销售机会实体</param>
        /// <returns></returns>
        public bool UpdateTrainExam(TrainExamVM model)
        {
            return MyService.UpdateTrainExam(model.Map<TrainExam, TrainExamVM>());
        }

        /// <summary>
        /// 编辑销售机会/实训考核
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool EditTrainExam(TrainExamVM model)
        {
            return MyService.EditTrainExam(model.Map<TrainExam, TrainExamVM>());
        }


        /// <summary>
        /// 删除实训考核（不删考核点）
        /// </summary>
        /// <param name="Id">销售机会ID</param>
        /// <returns></returns>
        public bool DelTrainExam(int Id)
        {
            return MyService.DeleteTrainExam(Id);
        }

        /// <summary>
        /// 根据ID获取销售机会
        /// </summary>
        /// <param name="id">销售机会ID</param>
        /// <returns></returns>
        public TrainExamVM GetTrainExam(int id)
        {
            return MyService.GetTrainExam(id).Map<TrainExamVM, TrainExam>();
        }

        /// <summary>
        /// 获取未评分考核ID
        /// </summary>
        /// <returns></returns>
        public List<int> GetNoScoreExamId()
        {
            return MyService.GetNoScoreExamId();
        }

        /// <summary>
        /// 获取大赛的案例列表
        /// </summary>
        /// <param name="CompetitionId"></param>
        /// <returns></returns>
        public List<TrainExamVM> GetTrainExamWithDetail(int CompetitionId)
        {
            List<TrainExam> model = MyService.GetTrainExamWithDetail(CompetitionId);
            List<TrainExamVM> rtnValue = model.MapList<TrainExamVM, TrainExam>();
            return rtnValue;
        }

        /// <summary>
        /// 获取大赛的案例列表
        /// </summary>
        /// <param name="CompetitionId"></param>
        /// <returns></returns>
        public List<TrainExamVM> GetTrainExamByMatch(int CompetitionId)
        {
            List<TrainExam> model = MyService.GetTrainExamByMatch(CompetitionId);
            List<TrainExamVM> rtnValue = model.MapList<TrainExamVM, TrainExam>();
            return rtnValue;
        }
    }
}
