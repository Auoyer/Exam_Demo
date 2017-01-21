using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

using Server.Factory;
using Utils;
using VM;

namespace Web.Controllers
{
    public class TrainExamController : Controller
    {

        /// <summary>
        /// 销售机会界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取销售机会列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="FinancialTypeId">理财类型</param>
        /// <param name="keyword">关键字</param>
        /// <param name="sortName">排序字段名称</param>
        /// <param name="sortWay">排序方向</param>
        /// <param name="isShow">是否只显示未领取</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TrainExamList(int pageIndex, int pageSize, int FinancialTypeId, string keyword, string sortName, bool sortWay,
            bool isShow)
        {
            int total = 0;
            TrainSearch ts = new TrainSearch()
            {
                UserId = MvcHelper.User.Id,
                //ClassId = MvcHelper.User.UserClassInfo[0].ClassId,
                Status = (int)TrainExamPublishState.Published,
                FinancialTypeId = FinancialTypeId,
                ExamTypeId = (int)ExamineType.SalesOpportunities,
                KeyWords = keyword,
                SortName = sortName,
                SortWay = sortWay,
                isShow = isShow,
                Score = 1
            };
            List<UnitTrainExamVM> TrainExamList = SvrFactory.Instance.TrainingSvr.GetUnitTrainExamList(ts, pageIndex, pageSize, out total);

            //从缓存获取字段
            TrainExamList.ForEach(r =>
            {
                r.FinancialTypeName = EnumHelper.GetAllEnumDesc((FinancialType)r.FinancialTypeId);          //理财类型名称
            });
            PagedList<UnitTrainExamVM> result = new PagedList<UnitTrainExamVM>(TrainExamList, pageIndex, pageSize, total);
            return Json(new JsonModel(true, "", result));
        }

        /// <summary>
        /// 领取销售机会
        /// </summary>
        /// <param name="CustomerName">客户名称</param>
        /// <param name="IDNum">客户证件号</param>
        /// <returns></returns>
        public ActionResult GetTrainExamSales(string IDNum, int TrainExamId)
        {
            int userId = MvcHelper.User.Id;
            bool isSuccess = false;
            string errCode = "";
            //根据销售机会/实训考核Id获取考核案例实体
            ExamCaseVM examCase = CaseBLL.GetExamCase(TrainExamId);
            //插入销售机会-用户关系
            SvrFactory.Instance.TrainingSvr.AddTrainExamUser(TrainExamId, userId);
            //根据身份证号和用户Id检查以前是否有领取
            var model = SvrFactory.Instance.TrainingSvr.GetStuCustomerByIDNum(IDNum, userId);
            if (model != null)
            {
                #region 若存在，则更新信息
                if (model.Status == (int)StuCustomerProposalStatus.Delete)
                {
                    model.CustomerType = (int)CustomerEnum.PotentialCustomer;
                }

                model.CustomerName = examCase.CustomerName;
                model.IDNum = examCase.IDNum;
                model.CustomerStory = examCase.CustomerStory;
                model.UpdateDate = DateTime.Now;
                model.Status = (int)StuCustomerProposalStatus.Add;
                model.ProposalId = 0;
                model.TrainExamId = TrainExamId;


                bool result = SvrFactory.Instance.TrainingSvr.UpdateCustomer(model);
                if (result)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                    errCode = "20007";
                }
                return Json(new JsonModel(isSuccess, errCode, model.CustomerType));//20007 修改失败!请联系管理员!
                #endregion
            }
            else
            {
                #region 新增
                model = new StuCustomerVM
                {
                    UserId = userId,
                    CustomerName = examCase.CustomerName,
                    CustomerNo = NumHelper.Instance.GetNum(NumberType.Customer),
                    IDType = (int)IDType.IdentityCard,
                    IDNum = IDNum,
                    CustomerType = (int)CustomerEnum.PotentialCustomer,
                    CustomerStory = examCase.CustomerStory,
                    Source = (int)CustomerSourceType.SalesOpportunities,//客户来源
                    UpdateDate = DateTime.Now,
                    Status = (int)StuCustomerProposalStatus.Add,
                    TrainExamId = TrainExamId
                };

                model.Id = SvrFactory.Instance.TrainingSvr.AddStuCustomer(model);

                if (model.Id > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                    errCode = "20006";
                }
                return Json(new JsonModel(isSuccess, errCode, null));//20006 新增失败!请联系管理员!
                #endregion
            }

        }

        /// <summary>
        /// 获取案例
        /// </summary>
        /// <param name="TrainExamId">TrainExamId</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetCase2(int TrainExamId)
        {
            ExamCaseVM model = CaseBLL.GetExamCase(TrainExamId);
            if (model != null)
            {
                //从缓存获取字段                                     
                model.FinancialTypeName = EnumHelper.GetAllEnumDesc((FinancialType)model.FinancialTypeId);          //理财类型名称  
            }

            return Json(new JsonModel(true, "", model));
        }

        /// <summary>
        /// 获取案例(考核用)
        /// </summary>
        /// <param name="TrainExamId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetCase(int TrainExamId)
        {
            CaseVM model = CaseBLL.GetCase(TrainExamId);
            if (model != null)
            {
                //从缓存获取字段                                     
                model.strFinancialType = EnumHelper.GetAllEnumDesc((FinancialType)model.FinancialTypeId);          //理财类型名称  
            }

            return Json(new JsonModel(true, "", model));
        }

        /// <summary>
        /// 计算得分
        /// </summary>
        /// <param name="TrainExamId">实训ID</param>
        /// <param name="ProposalId">建议书ID</param>
        /// <param name="Status">状态（自主新增时用到）</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CalculateScore(int TrainExamId, int? ProposalId, int? Status)
        {
            int IsHightCustomer = 0;
            int userId = MvcHelper.User.Id;
            ProposalVM proposal = null;


            if (TrainExamId != 0)
            {
                #region 销售机会/实训考核
                if (ProposalId.HasValue && ProposalId.Value > 0)
                {
                    //1.建议书缓存
                    proposal = ProposalBLL.GetProposal(ProposalId.Value);
                }
                else
                {
                    #region 交白卷情况
                    //写入建议书
                    proposal = new ProposalVM();
                    proposal.TrainExamId = TrainExamId;
                    proposal.UserId = userId;
                    proposal.ProposalName = "";
                    proposal.CreateDate = DateTime.Now;
                    proposal.ProposalNum = NumHelper.Instance.GetNum(NumberType.Proposal);
                    //保存建议书
                    int Id = SvrFactory.Instance.TrainingSvr.AddProposal(proposal);
                    proposal.Id = Id;
                    //当添加成功后同步缓存
                    if (Id > 0)
                    {
                        TrainingCaches.SetProposalCache(Id, proposal);
                    }
                    #endregion
                }
                //已评分这里要做一个判断  财务分析中【资产负债表】填写数据，使净值>6000000
                //这里做一个计算
                LiabilityVM LiabilityModel = proposal.LiabilityVM;
                if (LiabilityModel != null)
                {
                    if (LiabilityModel.TotalVal >= 6000000)
                    {
                        IsHightCustomer = (int)IsHigStucustomer.CustomerPotentialHighAssets;
                    }
                }
                //判断是否生成计划书编号 
                if (string.IsNullOrEmpty(proposal.ProposalNum))
                {
                    proposal.ProposalNum = NumHelper.Instance.GetNum(NumberType.Proposal);
                }
                AssessmentResultsVM model = new AssessmentResultsVM();
                //2.考核缓存   TranExam
                TrainExamVM TrainExam = TrainingBLL.GetTrainExam(TrainExamId);
                List<ExamModuleVM> EMList = TrainingCaches.ExamModuleList.GetList(); //SvrFactory.Instance.TrainingSvr.GetExamModuleList(); 
                List<ExamPointVM> EPList = TrainingCaches.ExamPointList.GetList();//SvrFactory.Instance.TrainingSvr.GetExamPointList();
                //3.循环考核点
                AssessmentCalculationBLL ACBLL = new AssessmentCalculationBLL();
                foreach (var item in TrainExam.TrainExamDetail)
                {
                    var DetailInfo = ACBLL.AssessmentCalculation(item, proposal, EMList, EPList);
                    if (DetailInfo != null)
                    {
                        AssessmentResultsDetailVM ModelVM = new AssessmentResultsDetailVM();
                        ModelVM.AssessmentResultsId = DetailInfo.AssessmentResultsId;
                        ModelVM.ExamPointType = DetailInfo.ExamPointType;
                        ModelVM.ModularId = DetailInfo.ModularId;
                        ModelVM.AssessmentPoint = DetailInfo.AssessmentPoint;
                        ModelVM.Status = DetailInfo.Status;
                        ModelVM.Score = DetailInfo.Score;
                        model.DetailList.Add(ModelVM);
                    }
                }
                model.UserId = userId;
                model.CompetitionId = TrainExam.CompetitionId;
                model.TrainExamId = TrainExamId;
                model.TotalScore = TrainExam.TrainExamDetail.Sum(x => x.Score);//考核总分 
                model.SubjectiveResults = 0;
                model.CreateTime = DateTime.Now;
                if (model.DetailList != null && model.DetailList.Count > 0)
                {
                    //客观成绩  
                    model.ObjectiveResults = model.DetailList.Where(x => x.Status == (int)IsCorrect.Correct).Sum(x => x.Score);
                }
                var ExamPointList = TrainExam.TrainExamDetail.Select(x => x.ExamPointId).ToList();
                int SubjectiveNum = EPList.Count(x => ExamPointList.Contains(x.Id) && x.ExamPointType == (int)ExamPointType.Subjective);
                //当存在主观题时，将考核状态更新为待评分，否则为已评分
                proposal.UpdateDate = DateTime.Now;
                if (SubjectiveNum > 0)
                {
                    model.TrainExamStatus = (int)ExaminationStatus.WaitScore;
                    proposal.Status = (int)ProposalStatus.UnAudited;
                }
                else
                {
                    model.TrainExamStatus = (int)ExaminationStatus.AlreadyScore;
                    proposal.Status = (int)ProposalStatus.Audited;
                }
                //保存成绩
                bool Result = SvrFactory.Instance.TrainingSvr.AddAssessmentResults(model);
                //更新建议书
                SvrFactory.Instance.TrainingSvr.UpdateProposal(proposal);
                //更新潜在/已有客户状态
                SvrFactory.Instance.TrainingSvr.UpdateStuCustomerStatus(proposal.Id, (int)StuCustomerProposalStatus.Submit, IsHightCustomer);

                #region 异步操作：1.更新考核结束时间
                Task.Run(() =>
                {
                    //更新考核结束时间
                    var assess = TrainingCaches.CurEntryAssessmentCache().FirstOrDefault(x => x.TrainExamId == TrainExamId && x.UserId == userId);
                    if (assess != null)
                    {
                        assess.EndTime = DateTime.Now;
                        if (SvrFactory.Instance.TrainingSvr.UpdateEntryAssessment(assess))
                        {
                            //成功则移除缓存
                            TrainingCaches.RemoveEntryAssessmentCache(assess.Id);
                        }
                    }
                });
                #endregion


                //将状态更新到库中
                //Result = SvrFactory.Instance.TrainingSvr.UpdateTrainExam(TrainExam);
                if (Result)
                    return Json(new JsonModel(Result, "", Result));
                else
                    return Json(new JsonModel(Result, "20014", null));
                #endregion
            }
            else
            {
                #region 自主新增潜在客户
                string errCode = "";
                if (ProposalId.HasValue && ProposalId.Value > 0)
                {
                    //1.建议书缓存
                    proposal = ProposalBLL.GetProposal(ProposalId.Value);
                }
                //这里做一个计算
                LiabilityVM LiabilityModel = proposal.LiabilityVM;
                if (LiabilityModel != null)
                {
                    if (LiabilityModel.TotalVal >= 6000000)
                    {
                        IsHightCustomer = (int)IsHigStucustomer.CustomerPotentialHighAssets;
                    }
                }

                bool isSuccess = SvrFactory.Instance.TrainingSvr.UpdateProposalStatus(ProposalId.Value, Status.Value);
                //更新潜在/已有客户状态
                SvrFactory.Instance.TrainingSvr.UpdateStuCustomerStatus(ProposalId.Value, (int)StuCustomerProposalStatus.Submit, IsHightCustomer);
                if (!isSuccess)
                {
                    errCode = "20007";  //"20007": "修改失败!请联系管理员!",
                }

                return Json(new JsonModel(isSuccess, errCode, null));
                #endregion
            }
        }

        /// <summary>
        /// 获取剩余时间
        /// </summary>
        /// <param name="TrainExamId">实训考核ID</param>
        /// <returns></returns>
        public ActionResult GetRemainingTime(int TrainExamId)
        {
            var TrainExam = TrainingBLL.GetTrainExam(TrainExamId);
            DateTime dt = DateTime.Now;
            if (TrainExam != null)
            {
                if (TrainExam.StartDate <= dt && dt <= TrainExam.EndDate)
                {
                    int Surplus = 0;//剩余时间（秒）
                    TimeSpan ts = TrainExam.EndDate - DateTime.Now;
                    Surplus = (int)ts.TotalSeconds;
                    return Json(new JsonModel(true, "", Surplus));
                }
                else
                {
                    return Json(new JsonModel(false, "20027", null));
                }
            }
            return Json(new JsonModel(false, "20028", null));
        }

    }
}