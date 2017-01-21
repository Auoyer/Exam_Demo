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
    public class ProposalCustomerController : Controller
    {
        /// <summary>
        /// 子系统-客户信息界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int? TrainExamId, int? ProposalId)
        {
            int UserId = MvcHelper.User.Id;
            //int classId = MvcHelper.User.UserClassInfo[0].ClassId;

            if (TrainExamId.HasValue && TrainExamId.Value > 0)
            {
                #region 实训考核时，插入进入考试信息
                var train = TrainingBLL.GetTrainExam(TrainExamId.Value);
                if (train != null)
                {
                    //当为实训考核时，检测是否第一次进入考核
                    TrainSearch search = new TrainSearch
                    {
                        UserId = UserId,
                        TrainExamId = TrainExamId,
                    };
                    var assess = SvrFactory.Instance.TrainingSvr.GetEntryAssessmentList(search).FirstOrDefault();
                    if (assess == null)
                    {
                        //插入进入数据
                        EntryAssessmentVM model = new EntryAssessmentVM();
                        model.TrainExamId = TrainExamId.Value;
                        model.UserId = UserId;
                        model.EntryTime = DateTime.Now;
                        model.Id = SvrFactory.Instance.TrainingSvr.AddEntryAssessment(model);
                        model.CompetitionId = train.CompetitionId;
                        //当添加成功后同步缓存
                        if (model.Id > 0)
                        {
                            TrainingCaches.SetEntryAssessmentCache(model.Id,model);
                        }
                    }
                }
                #endregion
            }

            if (ProposalId.HasValue && ProposalId.Value > 0)
            {
                //读取缓存
                ProposalBLL.GetProposal(ProposalId.Value);
            }
            return View();
        }

        /// <summary>
        /// 获取建议书客户信息
        /// </summary>
        /// <param name="ProposalId">建议书Id</param>
        /// <returns></returns>
        public ActionResult GetProposalCustomer(int ProposalId)
        {
            if (ProposalId != 0)
            {
                ProposalCustomerVM pCustomer = null;
                ProposalVM proposal = ProposalBLL.GetProposal(ProposalId);
                if (proposal != null)
                {
                    pCustomer = proposal.ProposalCustomerVM;
                }

                var returnObj = new
                {
                    ProposalNum = proposal.ProposalNum,
                    ProposalName = proposal.ProposalName,
                    ProposalCustomerVM = pCustomer
                };

                return Json(new JsonModel(true, "", returnObj));
            }
            else
            {
                return Json(new JsonModel(true, "", null));
            }
        }

        /// <summary>
        /// 新增/修改建议书客户信息
        /// </summary>
        /// <param name="model">建议书客户信息类</param>
        /// <returns></returns>
        public ActionResult AddUpdateProposalCustomer(ProposalVM model)
        {
            bool Success = false;
            if (model.Id > 0)
            {
                #region 编辑
                //1.更新建议书时间
                ProposalVM proposal = ProposalBLL.GetProposal(model.Id);//获取不到证明程序有问题，正常情况更新时必然有缓存
                DateTime now = DateTime.Now;
                model.UpdateDate = now;
                model.UserId = MvcHelper.User.Id;//加入用户ID
                model.Status = proposal.Status;//状态
                Success = SvrFactory.Instance.TrainingSvr.UpdateProposal(model);
                if (!Success)
                {
                    return Json(new JsonModel(false, "20007", null));//"20007": "修改失败!请联系管理员!"
                }
                //2.更新客户信息
                Success = SvrFactory.Instance.TrainingSvr.UpdateProposalCustomer(model.ProposalCustomerVM);
                if (!Success)
                {
                    return Json(new JsonModel(false, "20007", null));//"20007": "修改失败!请联系管理员!"
                }
                ////3.更新缓存

                ////获取不到证明程序有问题，正常情况更新时必然有缓存
                proposal.UpdateDate = now;
                proposal.ProposalName = model.ProposalName;
                proposal.ProposalCustomerVM = model.ProposalCustomerVM;
                TrainingCaches.SetProposalCache(proposal.Id, proposal);
                return Json(new JsonModel(true, "", model.Id));
                #endregion
            }
            else
            {
                #region 新增
                //1.新增建议书
                model.Status = (int)ProposalStatus.UnCommitted;
                model.UserId = MvcHelper.User.Id;
                model.ProposalNum = NumHelper.Instance.GetNum(NumberType.Proposal);
                model.Id = SvrFactory.Instance.TrainingSvr.AddProposal(model);
                if (model.Id == 0)
                {
                    return Json(new JsonModel(false, "20006", null));//"20006": "新增失败!请联系管理员!"
                }

                //2.新增客户信息
                model.ProposalCustomerVM.ProposalId = model.Id;
                model.ProposalCustomerVM.Id = SvrFactory.Instance.TrainingSvr.AddProposalCustomer(model.ProposalCustomerVM);
                if (model.ProposalCustomerVM.Id == 0)
                {
                    return Json(new JsonModel(false, "20006", null));//"20006": "新增失败!请联系管理员!"
                }

                if (model.StuCustomerId != 0)
                {
                    //3.更新潜在客户/已有客户的状态
                    bool flag = SvrFactory.Instance.TrainingSvr.UpdateStuCustomerStatusAndProposalId(model.Id, (int)StuCustomerProposalStatus.Edit, model.StuCustomerId);
                    if (!flag)
                    {
                        return Json(new JsonModel(false, "20006", null));//"20006": "新增失败!请联系管理员!"
                    }
                }


                //4.新增缓存
                TrainingCaches.SetProposalCache(model.Id, model);

                return Json(new JsonModel(true, "", model.Id));
                #endregion
            }
        }


        /// <summary>
        /// 建议书预览
        /// </summary>
        /// <param name="TrainExamId"></param>
        /// <param name="ProposalId"></param>
        /// <returns></returns>
        public ActionResult PreviewIndex(int? TrainExamId, int? ProposalId)
        {
            if (ProposalId.HasValue)
            {
                ProposalBLL.GetProposal(ProposalId.Value);
            }
            return View();

        }

        /// <summary>
        /// 校验当前实训是否已结束
        /// </summary>
        /// <param name="TrainExamId">考核ID</param>
        /// <returns></returns>
        public ActionResult CheckExamDate(int TrainExamId)
        {
            var model = TrainingBLL.GetTrainExam(TrainExamId);
            DateTime dt = DateTime.Now;
            if (model != null && model.StartDate <= dt && dt <= model.EndDate)
            {
                var pp = SvrFactory.Instance.TrainingSvr.GetProposal(TrainExamId,0);
                return Json(new JsonModel(true, "", pp==null?0:pp.Id));
            }
            return Json(new JsonModel(false, "20027", null, "/CaseExam/CaseExamList"));
        }

        public ActionResult CaseView(int? TrainExamId)
        {
            var result = new CaseVM();
            if (TrainExamId.HasValue && TrainExamId.Value != 0)
            {
               result = CaseBLL.GetCase(TrainExamId.Value);
            }
            return View(result);
        }
    }
}