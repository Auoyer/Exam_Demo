using Server.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utils;
using VM;

namespace Web.Controllers
{
    public class QuestionExamController : Controller
    {
        #region 竞管端-案例资源

        #region 案例资源列表页面 ActionResult CaseList()
        /// <summary>
        /// 案例资源列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult CaseList()
        {
            return View();
        }
        #endregion

        #region 获取案例资源列表 ActionResult GetCaseList(int? FinancialTypeId, string KeyWords, int pageIndex, int pageSize)
        /// <summary>
        /// 资源列表-获取案例资源列表
        /// </summary>
        /// <param name="FinancialTypeId">理财类型</param>
        /// <param name="KeyWords">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetCaseList(int? FinancialTypeId, string KeyWords, int pageIndex, int pageSize)
        {
            TrainSearch search = new TrainSearch
            {
                CaseStatus = 1,
                CollegeId = MvcHelper.User.CollegeId,
                FinancialTypeId = FinancialTypeId,
                KeyWords = KeyWords,
            };
            var page = SvrFactory.Instance.TrainingSvr.GetCasePage(search, pageIndex, pageSize);
            //获取枚举、缓存数据
            page.Data.ForEach(x =>
            {
                x.strFinancialType = EnumHelper.GetAllEnumDesc((FinancialType)x.FinancialTypeId); //理财类型
                if (x.CollegeId == 0)
                {
                    x.strCollege = "系统内置";
                }
                else
                {
                    x.strCollege = "自定义";
                }
            });

            return Json(new JsonModel(true, "", page));
        }
        #endregion

        #region 获取案例资源列表2 ActionResult GetCaseList2(int? FinancialTypeId, string KeyWords, int pageIndex, int pageSize)
        /// <summary>
        /// 添加实训考试-获取案例资源列表
        /// </summary>
        /// <param name="FinancialTypeId">理财类型</param>
        /// <param name="KeyWords">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetCaseList2(int MatchId, int pageIndex, int pageSize)
        {
            int totalCount = 0;
            TrainSearch search = new TrainSearch
            {
                CaseStatus = 1,
                CollegeId = MvcHelper.User.CollegeId,
            };
            List<CaseVM> list = new List<CaseVM>();
            var page = SvrFactory.Instance.TrainingSvr.GetCasePage(search);
            var trainCaseList = SvrFactory.Instance.TrainingSvr.GetTrainExamWithDetail(MatchId).Select(x => x.CaseId).ToList();
            //获取枚举、缓存数据
            page.ForEach(x =>
            {
                if (!trainCaseList.Contains(x.Id))
                {
                    x.strFinancialType = EnumHelper.GetAllEnumDesc((FinancialType)x.FinancialTypeId); //理财类型
                    if (x.CollegeId == 0)
                    {
                        x.strCollege = "系统内置";
                    }
                    else
                    {
                        x.strCollege = "自定义";
                    }
                    list.Add(x);
                }
            });
            list = list.Page(pageIndex, pageSize, out totalCount);

            PagedList<CaseVM> page2 = new PagedList<CaseVM>(list, pageIndex, pageSize, totalCount);
            return Json(new JsonModel(true, "", page2));
        }
        #endregion

        #region 查看案例页面 ActionResult CaseView(int? Id)
        /// <summary>
        /// 查看案例页面
        /// </summary>
        /// <param name="Id">案例Id</param>
        /// <returns></returns>
        public ActionResult CaseView()
        {
            return View();
        }
        #endregion

        #region 获取案例 ActionResult GetCase(int Id)
        /// <summary>
        /// 获取案例
        /// </summary>
        /// <param name="Id">案例Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetCase(int Id)
        {
            var model = SvrFactory.Instance.TrainingSvr.GetCase(Id);
            return Json(new JsonModel(true, "", model));
        }
        #endregion

        #region 屏蔽/发布案例 ActionResult ChangeCaseStatus(int caseId, int statusType)
        /// <summary>
        /// 屏蔽/发布案例
        /// </summary>
        /// <param name="caseId">案例Id</param>
        /// <param name="statusType">操作类型</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangeCaseStatus(int caseId, int statusType)
        {
            var result = SvrFactory.Instance.TrainingSvr.ChangeCaseStatus(caseId, statusType);
            if (result)
            {
                return Json(new JsonModel(true, ""));
            }
            else
            {
                if (statusType == (int)QuestionStauts.Close)
                {
                    // 案例屏蔽失败！
                    return Json(new JsonModel(result, "20056", null));
                }
                else
                {
                    // 案例发布失败！
                    return Json(new JsonModel(result, "20057", null));
                }
            }
        }
        #endregion

        #region 获取理财类型列表 ActionResult GetFinancialTypeList()
        /// <summary>
        /// 获取理财类型列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetFinancialTypeList()
        {
            return Json(new JsonModel(true, "", TrainingCaches.FinancialTypeList.GetList()));
        }
        #endregion

        #region 获取考核内容列表 ActionResult GetExamContentList()
        /// <summary>
        /// 获取考核内容列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetExamContentList()
        {
            return Json(new JsonModel(true, "", TrainingCaches.ExamContentList.GetList()));
        }
        #endregion

        #region 获取考核模块、考核点列表 ActionResult GetExamPointList(int ContentId)
        /// <summary>
        /// 获取考核模块、考核点列表
        /// </summary>
        /// <param name="ContentId">考核内容Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetExamPointList(int ContentId)
        {
            var moduleList = TrainingCaches.ExamModuleList.Where(x => x.ExamContentId == ContentId).Select(x => x.Id).ToList();
            object obj = new
            {
                ExamModuleList = TrainingCaches.ExamModuleList.Where(x => x.ExamContentId == ContentId).ToList(),
                ExamPointList = TrainingCaches.ExamPointList.Where(x => moduleList.Contains(x.ExamModuleId)).ToList(),
            };
            return Json(new JsonModel(true, "", obj));
        }
        #endregion

        #region 新增/修改案例 ActionResult SaveCase(CaseVM model)
        /// <summary>
        /// 新增/修改案例
        /// </summary>
        /// <param name="model">案例实体</param>
        /// <returns></returns> 
        public ActionResult SaveCase(CaseVM model)
        {
            if (model.Id > 0)
            {
                #region 更新
                bool result = SvrFactory.Instance.TrainingSvr.UpdateCase(model);
                if (result)
                {
                    return Json(new JsonModel(true, "", model));
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
                model.CollegeId = MvcHelper.User.CollegeId;
                model.UserId = MvcHelper.User.Id;
                model.CreateDate = DateTime.Now;
                model.Status = (int)QuestionStauts.Open;
                //备份用户填写数据
                List<ExamPointAnswerVM> answers = new List<ExamPointAnswerVM>();
                if (model.ExamPointAnswer != null && model.ExamPointAnswer.Count > 0)
                {
                    answers.AddRange(model.ExamPointAnswer);
                }
                //插入全考点
                model.ExamPointAnswer = new List<ExamPointAnswerVM>();
                List<ExamPointVM> list = TrainingCaches.ExamPointList.GetList();
                if (list != null && list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        model.ExamPointAnswer.Add(new ExamPointAnswerVM
                        {
                            Id = 0,
                            CaseId = 0,
                            ExamPointId = item.Id,
                            Answer = ""
                        });
                    }
                }
                //用户填写数据更新
                if (answers != null && answers.Count > 0)
                {
                    foreach (var item in answers)
                    {
                        model.ExamPointAnswer.ForEach(x =>
                        {
                            if (x.ExamPointId == item.ExamPointId)
                            {
                                x.Answer = item.Answer;
                            }
                        });
                    }
                }

                int id = SvrFactory.Instance.TrainingSvr.AddCase(model);
                if (id != 0)
                {
                    model.Id = id;
                    return Json(new JsonModel(true, "", model));
                }
                else
                {
                    return Json(new JsonModel(false, "20006", null));//20006 新增失败!请联系管理员!
                }
                #endregion
            }
        }
        #endregion

        #region 删除案例 ActionResult DelCase(int Id)
        /// <summary>
        /// 删除案例
        /// </summary>
        /// <param name="Id">案例Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DelCase(int Id)
        {
            //// 物理删除
            //var result = SvrFactory.Instance.TrainingSvr.DeleteCase(Id);
            // 逻辑删除
            var result = SvrFactory.Instance.TrainingSvr.ChangeCaseStatus(Id, (int)QuestionStauts.Delete);
            if (result)
            {
                return Json(new JsonModel(true, ""));
            }
            else
            {
                return Json(new JsonModel(result, "20005", null));//20005 删除失败!请联系管理员!
            }
        }
        #endregion

        #region 校验身份证号是否重复 ActionResult CheckRepeat(int CaseId, string IDNum)
        /// <summary>
        /// 校验身份证号是否重复
        /// </summary>
        /// <param name="CaseId"></param>
        /// <param name="IDNum"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CheckRepeat(int CaseId, string IDNum)
        {
            var result = SvrFactory.Instance.TrainingSvr.CheckRepeat(CaseId, IDNum);
            if (result)
                return Json(new JsonModel(true, ""));
            else
                return Json(new JsonModel(false, "20015", null));
        }
        #endregion

        #region 检查自定义案列是否被用在未发布的竞赛中 ActionResult CheckCaseInUnEndMatch(int caseId)
        /// <summary>
        /// 检查自定义案列是否被用在未结束的竞赛中
        /// </summary>
        /// <param name="caseId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CheckCaseInUnEndMatch(int caseId)
        {
            bool result = false;
            result = SvrFactory.Instance.TrainingSvr.CheckCaseInMatch(-2, caseId);
            return Json(new JsonModel(true, "", result));
        }
        #endregion

        #endregion
	}
}