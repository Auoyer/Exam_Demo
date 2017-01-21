using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Server.Factory;
using System.Threading;
using System.Threading.Tasks;
using Utils;
using VM;
using System.IO;
using System.Text;
using System.Configuration;
using System.Web.SessionState;
using System.Reflection;

namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        bool isCalcScore = false;
        AssessmentCalculationBLL assessmentCalculationBLL = new AssessmentCalculationBLL();


        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //GlobalFilters.Filters.Add(new ExceptionAttribute());

            Initialize();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = this.Context.Server.GetLastError();
            //LogHelper.Log.WriteError("Application_Error", ex);
            LogHelper.Log.WriteError("Application_Error:" + Server.GetLastError().Message, ex);

        }

        #region 定时器事件

        /// <summary>
        /// Time1定时器方法(默认3分钟执行)
        /// 实训考核自动提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!isCalcScore)
            {
                AutoCommitTrainExam();
            }
        }

        /// <summary>
        /// Time3定时器方法(默认180分钟执行)
        /// 定时清理假删习题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer3_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            #region 定时清理Question假删
            //获取假删数据
            List<int> delete_question_list = ExamCaches.CurQuestionCache().Where(x => x.Status == (int)QuestionStauts.Delete).Select(x => x.Id).ToList();
            if (delete_question_list != null && delete_question_list.Count > 0)
            {
                //在数据库内进行查找，删除未组卷的，并把对应Id集合返回
                List<int> remove_question_list = SvrFactory.Instance.ExamSvr.RemoveQuestion(delete_question_list);
                //清除未组卷的缓存
                if (remove_question_list != null && remove_question_list.Count > 0)
                {
                    foreach (var item in remove_question_list)
                    {
                        ExamCaches.RemoveQuestionCache(item);
                    }
                }
            }
            #endregion
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 实训考核自动提交
        /// </summary>
        private void AutoCommitTrainExam()
        {
            //var examIds = TrainingCaches.CurExamIDCache();
            var examIds = SvrFactory.Instance.TrainingSvr.GetNoScoreExamId();
            if (examIds != null && examIds.Count > 0)
            {
                //建立副本
                List<int> tempList = new List<int>();
                //tempList.AddRange(examIds);
                foreach (int item in tempList)
                {
                    var train = TrainingBLL.GetTrainExam(item);
                    //若实训已结束
                    if (train != null && train.EndDate.AddMinutes(1) <= DateTime.Now)
                    {
                        isCalcScore = true;

                        if (true)
                        {
                            #region 实训考核
                            //参与考试，但未提交试卷的
                            var accessList = TrainingCaches.CurEntryAssessmentCache().Where(x => x.TrainExamId == train.Id).ToList();
                            if (accessList != null && accessList.Count > 0)
                            {
                                #region 算分并提交试卷
                                //考核模块列表
                                List<ExamModuleVM> ExamModuleList = TrainingCaches.ExamModuleList.GetList();
                                //考核点列表
                                List<ExamPointVM> ExamPointList = TrainingCaches.ExamPointList.GetList();
                                //循环每个未手动交卷学生
                                foreach (var access in accessList)
                                {
                                    //建议书
                                    var proposal = ProposalBLL.GetProposal(train.Id, access.UserId);
                                    if (proposal == null)
                                    {
                                        #region 只进入考试，然后什么都没干
                                        proposal = new ProposalVM();
                                        proposal.TrainExamId = train.Id;
                                        proposal.UserId = access.UserId;
                                        proposal.ProposalName = "";
                                        proposal.ProposalNum = NumHelper.Instance.GetNum(NumberType.Proposal);
                                        proposal.CreateDate = DateTime.Now;
                                        //保存建议书
                                        proposal.Id = SvrFactory.Instance.TrainingSvr.AddProposal(proposal);
                                        //添加成功
                                        if (proposal.Id > 0)
                                        {
                                            TrainingCaches.SetProposalCache(proposal.Id, proposal);
                                        }
                                        #endregion
                                    }

                                    #region 算分
                                    //循环考核点算分
                                    AssessmentResultsVM model = new AssessmentResultsVM();
                                    foreach (var train_item in train.TrainExamDetail)
                                    {
                                        var detailInfo = assessmentCalculationBLL.AssessmentCalculation(train_item, proposal, ExamModuleList, ExamPointList);
                                        if (detailInfo != null)
                                        {
                                            AssessmentResultsDetailVM assessmentResultsDetail = new AssessmentResultsDetailVM();
                                            assessmentResultsDetail.AssessmentResultsId = detailInfo.AssessmentResultsId;
                                            assessmentResultsDetail.ExamPointType = detailInfo.ExamPointType;
                                            assessmentResultsDetail.ModularId = detailInfo.ModularId;
                                            assessmentResultsDetail.AssessmentPoint = detailInfo.AssessmentPoint;
                                            assessmentResultsDetail.Status = detailInfo.Status;
                                            assessmentResultsDetail.Score = detailInfo.Score;
                                            model.DetailList.Add(assessmentResultsDetail);
                                        }
                                    }
                                    model.UserId = access.UserId;
                                    model.CompetitionId = access.CompetitionId;
                                    model.TrainExamId = train.Id;
                                    model.TotalScore = train.TrainExamDetail.Sum(x => x.Score);//考核总分 
                                    model.SubjectiveResults = 0;
                                    model.CreateTime = DateTime.Now;
                                    if (model.DetailList != null && model.DetailList.Count > 0)
                                    {
                                        //客观成绩  
                                        model.ObjectiveResults = model.DetailList.Where(x => x.Status == (int)IsCorrect.Correct).Sum(x => x.Score);
                                    }
                                    var train_exampoint_list = train.TrainExamDetail.Select(x => x.ExamPointId).ToList();
                                    int SubjectiveNum = ExamPointList.Count(x => train_exampoint_list.Contains(x.Id) && x.ExamPointType == (int)ExamPointType.Subjective);
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
                                    #endregion

                                    #region 保存
                                    //保存成绩
                                    if (SvrFactory.Instance.TrainingSvr.AddAssessmentResults(model))
                                    {
                                        //更新建议书
                                        SvrFactory.Instance.TrainingSvr.UpdateProposal(proposal);
                                        //更新考核结束时间
                                        access.EndTime = train.EndDate;
                                        if (SvrFactory.Instance.TrainingSvr.UpdateEntryAssessment(access))
                                        {
                                            //成功则移除缓存
                                            TrainingCaches.RemoveEntryAssessmentCache(access.Id);
                                        }
                                    }
                                    #endregion

                                }
                                #endregion
                            }
                            #endregion
                        }

                        #region 校验是否存在未评分列表,不存在则可以更新考核状态
                        List<int> count = SvrFactory.Instance.TrainingSvr.CountTrainExamStatus(item);
                        //重现获取最新的销售机会/实训考核
                        train = SvrFactory.Instance.TrainingSvr.GetTrainExam(item);
                        bool isChange = false;
                        if (count[0] <= 0 && count[1] > 0)
                        {
                            //未评数量小于等于0，已评数量大于0
                            train.TrainExamStatus = (int)TrainExamStatu.AlreadGrade;
                            isChange = true;
                        }
                        else if (count[0] <= 0 && count[1] <= 0)
                        {
                            //未评数量小于等于0，已评数量小于等于0
                            //没有人提交销售机会/实训考核，不显示
                            train.TrainExamStatus = (int)TrainExamStatu.NoShow;
                            isChange = true;
                        }
                        else
                        {
                            //未评数量大于0
                            //不用处理
                        }

                        if (isChange)
                        {
                            if (SvrFactory.Instance.TrainingSvr.UpdateTrainExam(train))
                            {
                                TrainingCaches.RemoveExamIDCache(train.Id);
                            }
                        }
                        #endregion


                    }
                }

                isCalcScore = false;
            }

        }

        /// <summary>
        /// 实训考核自动评分
        /// </summary>
        private void AutoMarkTrainExam(int id)
        {
            try
            {
                var perposalList = new List<ProposalVM>();
                //var listResult = SvrFactory.Instance.MatchSvr.GetMatchResultList(-1, null).FindAll(x => x.CompetitionId == id);
                foreach (ProposalVM prop in perposalList)
                {
                    bool result = false;
                    //1、获取建议书根据ID取得缓存
                    ProposalVM perposal = SvrFactory.Instance.TrainingSvr.GetProposal(prop.TrainExamId, prop.UserId);
                    perposal = ProposalBLL.GetProposal(perposal.Id);
                    //2、更新建议书状态为已审核
                    if (perposal.Status != (int)ProposalStatus.deletes)
                    {
                        perposal.Status = (int)ProposalStatus.Audited;
                    }
                    TrainingCaches.SetProposalCache(perposal.Id, perposal);
                    result = SvrFactory.Instance.TrainingSvr.UpdateProposal(perposal);
                    //3、更新考核结果表状态为已评分
                    TrainSearch ts = new TrainSearch { TrainExamId = prop.TrainExamId, UserId = prop.UserId };
                    var ExamResultInfo = SvrFactory.Instance.TrainingSvr.GetAssessmentResultsModel(ts);
                    if (ExamResultInfo != null)
                    {
                        ExamResultInfo.TrainExamStatus = (int)ExaminationStatus.AlreadyScore;
                    }
                    result = SvrFactory.Instance.TrainingSvr.UpdateAssessmentResults(ExamResultInfo);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("定时修改评分状态出错", ex);
            }
        }

        #endregion

        private void Initialize()
        {
            //启动WCF服务
            while (!SvrFactory.Instance.Initialize())
            {
                Thread.Sleep(50);//这里必须等WCF连接上
            }

            //初始化缓存
            ExamCaches.Init();
            TrainingCaches.Init();
            //AutoCommit.Instance.Init();
        }

        public override void Init()
        {
            base.Init();
            foreach (string moduleName in this.Modules)
            {
                string appName = "APPNAME";
                IHttpModule module = this.Modules[moduleName];
                SessionStateModule ssm = module as SessionStateModule;
                if (ssm != null)
                {
                    FieldInfo storeInfo = typeof(SessionStateModule).GetField("_store", BindingFlags.Instance | BindingFlags.NonPublic);
                    SessionStateStoreProviderBase store = (SessionStateStoreProviderBase)storeInfo.GetValue(ssm);
                    if (store == null)//In IIS7 Integrated mode, module.Init() is called later
                    {
                        FieldInfo runtimeInfo = typeof(HttpRuntime).GetField("_theRuntime", BindingFlags.Static | BindingFlags.NonPublic);
                        HttpRuntime theRuntime = (HttpRuntime)runtimeInfo.GetValue(null);
                        FieldInfo appNameInfo = typeof(HttpRuntime).GetField("_appDomainAppId", BindingFlags.Instance | BindingFlags.NonPublic);
                        appNameInfo.SetValue(theRuntime, appName);
                    }
                    else
                    {
                        Type storeType = store.GetType();
                        if (storeType.Name.Equals("OutOfProcSessionStateStore"))
                        {
                            FieldInfo uribaseInfo = storeType.GetField("s_uribase", BindingFlags.Static | BindingFlags.NonPublic);
                            uribaseInfo.SetValue(storeType, appName);
                        }
                    }
                }
            }
        }

    }

}
