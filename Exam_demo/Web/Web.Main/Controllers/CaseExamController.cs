using Server.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VM;
using Utils;

namespace Web.Controllers
{
    public class CaseExamController : Controller
    {
        /// <summary>
        /// 实训考核列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult CaseExamList()
        {
            return View();
        }

        /// <summary>
        /// 获取实训考核列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCaseExamList(int pageIndex, int pageSize, int FinancialTypeId, string KeyWords)
        {
            int total = 0;
            TrainSearch ts = new TrainSearch();
            List<UnitTrainExamVM> TrainExamList = SvrFactory.Instance.TrainingSvr.GetTecTrainExamListUnit(ts, pageIndex, pageSize, out total);
            //从缓存获取字段
            TrainExamList.ForEach(r =>
            {
                r.FinancialTypeName = EnumHelper.GetAllEnumDesc((FinancialType)r.FinancialTypeId);          //理财类型名称
            });
            PagedList<UnitTrainExamVM> result = new PagedList<UnitTrainExamVM>(TrainExamList, pageIndex, pageSize, total);
            return Json(new JsonModel(true, "", result));


        }

        /// <summary>
        /// 实训考核页面（新增/修改）
        /// </summary>
        /// <returns></returns>
        public ActionResult AddTrainExam()
        {
            return View();
        }

        /// <summary>
        /// 实训考核内容（新增/修改）
        /// </summary>
        /// <returns></returns>
        public ActionResult AddTrainExam2(TrainExamVM model)
        {
            if (model.StartDate <= DateTime.Now)
            {
                return Json(new JsonModel(false, "20025", null));//开始时间不能小于当前时间
            }

            if (model.EndDate <= model.StartDate)
            {
                return Json(new JsonModel(false, "20026", null));//结束时间不能小于开始时间
            }
            //实训考核验证是否在同一个自然日
            if (model.ExamTypeId == (int)ExamineType.TrainingEvaluation)
            {
                if (model.StartDate.Date != model.EndDate.Date)
                {
                    return Json(new JsonModel(false, "20030", null));//开始时间和结束时间必须在同一个自然日！
                }
            }

            TrainSearch ts = new TrainSearch()
            {
                IDNum = model.ExamCase[0].IDNum,
                Status = (int)TrainExamPublishState.Published

            };

            #region 实训考核，实训名称判重
            TrainSearch ts2 = new TrainSearch()
            {
                Id = model.Id,
                CheckName = model.TrainExamName,
            };
            //判重
            //int count = SvrFactory.Instance.TrainingSvr.CountTrainExam(ts2);
            //if (count > 0)
            //{
            //    return Json(new JsonModel(false, "21023", model));//保存失败！该教师已存在相同名称的实训考核。
            //}
            #endregion

            if (model.Id > 0)
            {
                #region 更新

                var detail = model.TrainExamDetail.Sum(x => x.Score);//客观成绩  ;

                model.AllScore = detail;
                model.TrainExamStatus = 1;
                bool result = SvrFactory.Instance.TrainingSvr.EditTrainExam(model);
                if (result)
                {
                    return Json(new JsonModel(true, "21016", model.Id));//21016 修改成功!!
                }
                else
                {
                    return Json(new JsonModel(false, "20007", null));//20007 修改失败!请联系管理员!
                }
                #endregion
            }
            else
            {
                #region 新增

                var detail = model.TrainExamDetail.Sum(x => x.Score);//客观成绩  ;

                model.AllScore = detail;
                int result = SvrFactory.Instance.TrainingSvr.AddTrainExam(model);

                if (result > 0)
                {


                    return Json(new JsonModel(true, "20010", result));//20010 保存成功!
                }
                else
                {
                    return Json(new JsonModel(false, "21015", null));//21015 保存失败!请联系管理员!!
                }
                #endregion
            }
        }

        /// <summary>
        /// 实训考核删除
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteTrainExam(int Id)
        {
            var TrainExamObg = SvrFactory.Instance.TrainingSvr.GetTrainExam(Id);
            // 判断核销售机会是否已开始               

            string msg = "";
            if (TrainExamObg != null
                && TrainExamObg.Status == (int)TrainExamPublishState.Published
                && DateTime.Compare(TrainExamObg.StartDate, DateTime.Now) <= 0
                && DateTime.Compare(TrainExamObg.EndDate, DateTime.Now) > 0)
            {

                return Json(new JsonModel(true, "21020", null));//21020 考核已开始，无法删除！
            }

            //删除销售机会
            if (SvrFactory.Instance.TrainingSvr.DelTrainExam(Id))
            {
                ////删除班级关系
                //var bo1 = SvrFactory.Instance.TrainingSvr.DelTrainExamClass(Id);
                ////删除详细信息
                //var bo2 = SvrFactory.Instance.TrainingSvr.DelTrainExamDetail(Id);
                ////删除考核案例表
                //var bo3 = SvrFactory.Instance.TrainingSvr.ForTrainExamIdDeleteExamCase(Id);
            }
            return Json(new JsonModel(true, "21019", null));//21019 删除成功
        }


        /// <summary>
        /// 销售机会（发布/回收）
        /// </summary>
        /// <returns></returns>
        public ActionResult publishTrainExam(int Id, int Status, string IDNum, string strStartDate)
        {
            TrainExamVM te = SvrFactory.Instance.TrainingSvr.GetTrainExam(Id);


            int S = 0;
            if (Status == 0)
            {
                //发布时判断开始时间不能小于当前时间
                var starTime = Convert.ToDateTime(strStartDate);
                if (starTime < DateTime.Now)
                {
                    return Json(new JsonModel(false, "20025", null));//开始时间不能小于当前时间
                }

                //发布时填充答案
                var model = SvrFactory.Instance.TrainingSvr.GetCase(te.CaseId);
                List<ExamPointAnswerVM> detail = model.ExamPointAnswer;
                bool AnswerResult = SvrFactory.Instance.TrainingSvr.UpdateTrainExamDetail2(detail, Id);

                //添加缓存
                TrainingCaches.ExamIDList.Add(Id);

                S = (int)TrainExamPublishState.Published;
            }
            else
            {
                //取消缓存
                TrainingCaches.ExamIDList.Remove(Id);

                //取消发布
                S = (int)TrainExamPublishState.UnPublished;
            }

            te.Status = S;
            bool Result = SvrFactory.Instance.TrainingSvr.UpdateTrainExam(te);

            return Json(new JsonModel(true, "21018", null));//21018 发布成功
        }
    }
}