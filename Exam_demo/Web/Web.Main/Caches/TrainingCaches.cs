using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.Factory;
using Utils;
using VM;
using ServiceStack.Redis;
using System.Configuration;
using ServiceStack.Redis.Generic;

namespace Web
{
    /// <summary>
    /// 实训缓存管理类
    /// </summary>
    public static class TrainingCaches
    {
        #region Redis缓存关键字
        /// <summary>
        /// 建议书信息缓存key
        /// </summary>
        static string redisProposalKey = "RedisProposalInfo";

        /// <summary>
        /// 有效时间信息缓存key
        /// </summary>
        static string redisUserTimeKey = "RedisUserTimeInfo";

        /// <summary>
        /// 考核案例信息缓存key
        /// </summary>
        static string redisExamCaseKey = "RedisExamCaseInfo";

        /// <summary>
        /// 真实案例信息缓存key
        /// </summary>
        static string redisCaseKey = "RedisCaseInfo";

        /// <summary>
        /// 实训考核信息缓存key
        /// </summary>
        static string redisTrainExamKey = "RedisTrainExamInfo";

        /// <summary>
        /// 考试章节信息缓存key
        /// </summary>
        static string redisChaptertKey = "RedisChapterInfo";

        /// <summary>
        /// 章节子类型信息缓存key
        /// </summary>
        static string redisQueTypeKey = "RedisQueTypeInfo";

        /// <summary>
        /// 考核实训人员信息缓存key
        /// </summary>
        static string redisEntryAssessmentKey = "RedisEntryAssessmentInfo";

        /// <summary>
        /// P2P产品信息缓存key
        /// </summary>
        static string redisP2PKey = "RedisP2PInfo";

        /// <summary>
        /// 公告信息缓存key
        /// </summary>
        static string redisNoticeKey = "RedisNoticeInfo";

        /// <summary>
        /// 未评分考核ID缓存key
        /// </summary>
        static string redisExamIDKey = "RedisExamIDInfo";


        //static RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口

        //static IRedisTypedClient<ProposalVM> redisProposalVM;
        //static IRedisSet<ProposalVM> ListProposal;

        //static IRedisTypedClient<UserTimeSummaryVM> redisUserTimeVM;
        //static IRedisSet<UserTimeSummaryVM> ListUserTime;

        //static IRedisTypedClient<ExamCaseVM> redisExamCaseVM;
        //static IRedisSet<ExamCaseVM> ListExamCase;

        //static IRedisTypedClient<TrainExamVM> redisCaseVM;
        //static IRedisSet<TrainExamVM> ListCase;

        //static IRedisTypedClient<TrainExamVM> redisTrainExamVM;
        //static IRedisSet<TrainExamVM> ListTrainExam;

        //static IRedisTypedClient<TheoryChapterVM> redisTheoryChapterVM;
        //static IRedisSet<TheoryChapterVM> ListTheoryChapter;

        //static IRedisTypedClient<TheoryQuestionTypeVM> redisQueTypeVM;
        //static IRedisSet<TheoryQuestionTypeVM> ListQueType;

        //static IRedisTypedClient<EntryAssessmentVM> redisEntryAssessmentVM;
        //static IRedisSet<EntryAssessmentVM> ListEntryAssessment;

        //static IRedisTypedClient<P2PProductVM> redisP2PVM;
        //static IRedisSet<P2PProductVM> ListP2P;

        //static IRedisTypedClient<NoticeVM> redisNoticeVM;
        //static IRedisSet<NoticeVM> ListNotice;

        //static IRedisTypedClient<int> redisExamIDVM;
        //static IRedisSet<int> ListExamID;

        #endregion

        #region 缓存容器
        /// <summary>
        /// 建议书
        /// </summary>
        public static SynchronisedDictionary<int, ProposalVM> ProposalCache { get; set; }

        /// <summary>
        /// 有效时间
        /// </summary>
        public static SynchronisedDictionary<int, UserTimeSummaryVM> UserTimeSummary { get; set; }

        /// <summary>
        /// 考核案例
        /// </summary>
        public static SynchronisedDictionary<int, ExamCaseVM> ExamCaseCache { get; set; }

        /// <summary>
        /// 案例(考核用)
        /// </summary>
        public static SynchronisedDictionary<int, TrainExamVM> CaseCache { get; set; }

        /// <summary>
        /// 银行储蓄
        /// </summary>
        public static ListCache<BankDepositsVM> BankDepositsList { get; set; }

        /// <summary>
        /// 考核模块
        /// </summary>
        public static ListCache<ExamModuleVM> ExamModuleList { get; set; }

        /// <summary>
        /// 考核点
        /// </summary>
        public static ListCache<ExamPointVM> ExamPointList { get; set; }

        /// <summary>
        /// 实训考核
        /// </summary>
        public static SynchronisedDictionary<int, TrainExamVM> TrainExamCache { get; set; }

        /// <summary>
        /// 理论考试章节信息
        /// </summary>
        public static SynchronisedDictionary<int, TheoryChapterVM> TheoryChapterCache { get; set; }

        /// <summary>
        /// 理论考试章节子类型缓存
        /// </summary>
        public static SynchronisedDictionary<int, TheoryQuestionTypeVM> TheoryQuestionTypeCache { get; set; }

        /// <summary>
        /// 考核实训人员列表
        /// </summary>
        public static ListCache<EntryAssessmentVM> EntryAssessmentList { get; set; }

        /// <summary>
        /// p2P产品
        /// </summary>
        public static SynchronisedDictionary<string, P2PProductVM> P2PProductList1 { get; set; }

        #region 枚举
        /// <summary>
        /// 理财类型
        /// </summary>
        public static ListCache<EnumKeyValue> FinancialTypeList { get; set; }

        /// <summary>
        /// 考试内容
        /// </summary>
        public static ListCache<EnumKeyValue> ExamContentList { get; set; }

        /// <summary>
        /// 未评分考核ID
        /// </summary>
        public static List<int> ExamIDList { get; set; }

        #endregion

        #endregion

        public static void Init()
        {
            //实训缓存
            ExamModuleList = new ListCache<ExamModuleVM>(SvrFactory.Instance.TrainingSvr.GetExamModuleList());
            ExamPointList = new ListCache<ExamPointVM>(SvrFactory.Instance.TrainingSvr.GetExamPointList());
            ProposalCache = new SynchronisedDictionary<int, ProposalVM>(x => x.Id, LoadProposalCache());
            ExamCaseCache = new SynchronisedDictionary<int, ExamCaseVM>(x => x.TrainExamId, LoadExamCaseCache());
            CaseCache = new SynchronisedDictionary<int, TrainExamVM>(x => x.Id, LoadCaseCache());

            BankDepositsList = new ListCache<BankDepositsVM>(SvrFactory.Instance.TrainingSvr.GetBankDepositsList());
            TrainExamCache = new SynchronisedDictionary<int, TrainExamVM>(x => x.Id, LoadTrainExamCache());

            P2PProductList1 = new SynchronisedDictionary<string, P2PProductVM>(x => x.SourceId, LoadP2PCache());

            //理论考核章节
            TheoryChapterCache = new SynchronisedDictionary<int, TheoryChapterVM>(x => x.Id, LoadChapterCache());
            TheoryQuestionTypeCache = new SynchronisedDictionary<int, TheoryQuestionTypeVM>(x => x.Id, LoadQueTypeCache());

            //枚举缓存
            FinancialTypeList = new ListCache<EnumKeyValue>(EnumHelper.GetEnumList<FinancialType>());
            ExamContentList = new ListCache<EnumKeyValue>(EnumHelper.GetEnumList<ExamContent>());

            //有效时间缓存
            UserTimeSummary = new SynchronisedDictionary<int, UserTimeSummaryVM>(x => x.UserId, LoadUserTimeCache());
            //获取进入实训时间
            EntryAssessmentList = new ListCache<EntryAssessmentVM>(LoadEntryAssessmentCache());

            //获取未评分考核ID
            ExamIDList = LoadExamIDCache();
        }

        #region 建议书缓存操作
        static List<ProposalVM> LoadProposalCache()
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<ProposalVM> redisProposalVM = redisClient.As<ProposalVM>();
            IRedisSet<ProposalVM> ListProposal = redisProposalVM.Sets[redisProposalKey];

            ListProposal.Clear();
            redisProposalVM.DeleteAll();
            List<ProposalVM> list = new List<ProposalVM>();
            if (list != null && list.Count != 0)
                redisProposalVM.StoreAll(list);
            return list;
        }

        /// <summary>
        /// 加载缓存信息
        /// </summary>
        /// <returns></returns>
        public static List<ProposalVM> CurProposalCache()
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<ProposalVM> redisProposalVM = redisClient.As<ProposalVM>();

            return redisProposalVM.GetAll().ToList();
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public static ProposalVM GetProposalCache(int id)
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<ProposalVM> redisProposalVM = redisClient.As<ProposalVM>();

            ProposalVM vm = redisProposalVM.GetById(id);

            if (vm != null)
            {
                return vm;
            }
            else
            {
                vm = SvrFactory.Instance.TrainingSvr.GetProposal(id);
                if (vm != null)
                {
                    //2.客户信息
                    vm.ProposalCustomerVM = SvrFactory.Instance.TrainingSvr.GetProposalCustomer(id);
                    //3.风险评测
                    vm.RiskIndexVM = SvrFactory.Instance.TrainingSvr.GetRiskEvaluationInfo(id);
                    //5.财务分析
                    vm.LiabilityVM = SvrFactory.Instance.TrainingSvr.GetLiabilityByProposalId(id);
                    //6.收支储蓄表
                    vm.IncomeAndExpensesVM = SvrFactory.Instance.TrainingSvr.GetIncomeAndExpensesByProposalId(id);
                    //7.现金规划
                    vm.CashPlanVM = SvrFactory.Instance.TrainingSvr.GetCashPlanByProposalId(id);
                    //现金流量
                    vm.CashFlowVM = SvrFactory.Instance.TrainingSvr.SelectCashFlowGetObj(id);
                    //财务比例分析
                    vm.FinancialRatiosVM = SvrFactory.Instance.TrainingSvr.SelectFinalcialRatiosGetObj(id);
                    //教育规划
                    vm.LifeEducationPlanVM = SvrFactory.Instance.TrainingSvr.AdoptProposalIdSelectEPGetObj(id);
                    //消费规划
                    vm.ConsumptionPlanVM = SvrFactory.Instance.TrainingSvr.AdoptProposalIdSelectCPlanGetObj(id);
                    //创业规划
                    vm.StartAnUndertakingPlanVM = SvrFactory.Instance.TrainingSvr.GetModelProposalId(id);
                    //退休规划
                    vm.RetirementPlanVM = SvrFactory.Instance.TrainingSvr.GetRetirementPlanByProposalId(id);
                    //保险规划-----变态规划
                    vm.InsurancePlanVM = SvrFactory.Instance.TrainingSvr.GetInsurancePlanByProposalId(id);
                    //投资规划-----逆天规划
                    vm.InvestmentPlanVM = SvrFactory.Instance.TrainingSvr.GetInvestmentPlanByProposalId(id);
                    //税收规划
                    vm.TaxPlanVM = SvrFactory.Instance.TrainingSvr.AdoptProposalIdSelectTaxPlanGetObj(id);
                    //财产分配
                    vm.DistributionOfPropertyVM = SvrFactory.Instance.TrainingSvr.GetDistributionOfPropertyByProposalId(id);
                    //财产传承
                    vm.HeritageVM = SvrFactory.Instance.TrainingSvr.SelectHeritageGetObj(id);

                    redisProposalVM.Store(vm);
                }
                return vm;
            }
        }

        /// <summary>
        /// 更新或新建缓存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public static void SetProposalCache(int id, ProposalVM value)
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<ProposalVM> redisProposalVM = redisClient.As<ProposalVM>();

            redisProposalVM.DeleteById(id);
            redisProposalVM.Store(value);
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="id"></param>
        public static void RemoveProposalCache(int id)
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<ProposalVM> redisProposalVM = redisClient.As<ProposalVM>();

            redisProposalVM.DeleteById(id);
        }
        #endregion

        #region 有效时间缓存操作
        static List<UserTimeSummaryVM> LoadUserTimeCache()
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<UserTimeSummaryVM> redisUserTimeVM = redisClient.As<UserTimeSummaryVM>();
            IRedisSet<UserTimeSummaryVM> ListUserTime = redisUserTimeVM.Sets[redisUserTimeKey];

            ListUserTime.Clear();
            redisUserTimeVM.DeleteAll();
            List<UserTimeSummaryVM> list = new List<UserTimeSummaryVM>();
            if (list != null && list.Count != 0)
                redisUserTimeVM.StoreAll(list);
            return list;
        }


        /// <summary>
        /// 加载缓存信息
        /// </summary>
        /// <returns></returns>
        public static List<UserTimeSummaryVM> CurUserTimeCache()
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<UserTimeSummaryVM> redisUserTimeVM = redisClient.As<UserTimeSummaryVM>();

            return redisUserTimeVM.GetAll().ToList();
        }

        /// <summary>
        /// 更新或新建缓存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public static void SetUserTimeCache(int id, UserTimeSummaryVM value)
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<UserTimeSummaryVM> redisUserTimeVM = redisClient.As<UserTimeSummaryVM>();

            redisUserTimeVM.DeleteById(id);
            redisUserTimeVM.Store(value);
        }
        #endregion

        #region 考核案例缓存操作
        static List<ExamCaseVM> LoadExamCaseCache()
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<ExamCaseVM> redisExamCaseVM = redisClient.As<ExamCaseVM>();
            IRedisSet<ExamCaseVM> ListExamCase = redisExamCaseVM.Sets[redisExamCaseKey];

            ListExamCase.Clear();
            redisExamCaseVM.DeleteAll();
            List<ExamCaseVM> list = new List<ExamCaseVM>();
            if (list != null && list.Count != 0)
                redisExamCaseVM.StoreAll(list);
            return list;
        }


        /// <summary>
        /// 加载缓存信息
        /// </summary>
        /// <returns></returns>
        public static List<ExamCaseVM> CurExamCaseCache()
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<ExamCaseVM> redisExamCaseVM = redisClient.As<ExamCaseVM>();

            return redisExamCaseVM.GetAll().ToList();
        }

        /// <summary>
        /// 更新或新建缓存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public static void SetExamCaseCache(int id, ExamCaseVM value)
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<ExamCaseVM> redisExamCaseVM = redisClient.As<ExamCaseVM>();

            redisExamCaseVM.DeleteById(id);
            redisExamCaseVM.Store(value);
        }
        #endregion

        #region 真实案例缓存操作
        static List<TrainExamVM> LoadCaseCache()
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<TrainExamVM> redisCaseVM = redisClient.As<TrainExamVM>();
            IRedisSet<TrainExamVM> ListCase = redisCaseVM.Sets[redisCaseKey];

            ListCase.Clear();
            redisCaseVM.DeleteAll();
            List<TrainExamVM> list = new List<TrainExamVM>();
            if (list != null && list.Count != 0)
                redisCaseVM.StoreAll(list);
            return list;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public static TrainExamVM GetCaseCache(int id)
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<TrainExamVM> redisCaseVM = redisClient.As<TrainExamVM>();
            IRedisSet<TrainExamVM> ListCase = redisCaseVM.Sets[redisCaseKey];

            TrainExamVM vm = redisCaseVM.GetById(id);

            if (vm != null)
            {
                return vm;
            }
            else
            {
                vm = SvrFactory.Instance.TrainingSvr.GetTrainExam(id);
                if (vm != null && vm.Case != null)
                {
                    redisCaseVM.Store(vm);
                }
                return vm;
            }
        }
        #endregion

        #region 实训考核缓存操作
        static List<TrainExamVM> LoadTrainExamCache()
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<TrainExamVM> redisTrainExamVM = redisClient.As<TrainExamVM>();
            IRedisSet<TrainExamVM> ListTrainExam = redisTrainExamVM.Sets[redisTrainExamKey];

            ListTrainExam.Clear();
            redisTrainExamVM.DeleteAll();

            List<TrainExamVM> list = new List<TrainExamVM>();
            if (list != null && list.Count != 0)
                redisTrainExamVM.StoreAll(list);
            return list;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public static TrainExamVM GetTrainExamCache(int id)
        {
            TrainExamVM vm = SvrFactory.Instance.TrainingSvr.GetTrainExam(id);
            return vm;
            //RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            //IRedisTypedClient<TrainExamVM> redisTrainExamVM = redisClient.As<TrainExamVM>();
            //IRedisSet<TrainExamVM> ListTrainExam = redisTrainExamVM.Sets[redisTrainExamKey];

            //TrainExamVM vm = null;
            //if (redisTrainExamVM != null)
            //{
            //    vm = redisTrainExamVM.GetById(id);
            //}
            //if (vm != null)
            //{
            //    return vm;
            //}
            //else
            //{
            //    vm = SvrFactory.Instance.TrainingSvr.GetTrainExam(id);
            //    if (vm != null)
            //    {
            //        redisTrainExamVM.Store(vm);
            //    }
            //    return vm;
            //}
        }
        #endregion

        #region 考试章节缓存操作
        // 加载缓存信息
        static List<TheoryChapterVM> LoadChapterCache()
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<TheoryChapterVM> redisTheoryChapterVM = redisClient.As<TheoryChapterVM>();
            IRedisSet<TheoryChapterVM> ListTheoryChapter = redisTheoryChapterVM.Sets[redisChaptertKey];

            ListTheoryChapter.Clear();
            redisTheoryChapterVM.DeleteAll();
            List<TheoryChapterVM> list = SvrFactory.Instance.TrainingSvr.GetAllCharpters();
            if (list != null && list.Count != 0)
                redisTheoryChapterVM.StoreAll(list);
            return list;
        }

        /// <summary>
        /// 加载缓存信息
        /// </summary>
        /// <returns></returns>
        public static List<TheoryChapterVM> CurChapterCache()
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<TheoryChapterVM> redisTheoryChapterVM = redisClient.As<TheoryChapterVM>();

            var result = redisTheoryChapterVM.GetAll().ToList();
            if (result == null || result.Count == 0)
            {
                return LoadChapterCache();
            }
            return result;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public static TheoryChapterVM GetChapterCache(int id)
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<TheoryChapterVM> redisTheoryChapterVM = redisClient.As<TheoryChapterVM>();

            TheoryChapterVM vm = redisTheoryChapterVM.GetById(id);
            if (vm != null)
            {
                return vm;
            }
            else
            {
                vm = SvrFactory.Instance.TrainingSvr.GetTheoryChapter(id);
                if (vm != null)
                {
                    redisTheoryChapterVM.Store(vm);
                }
                return vm;
            }
        }

        /// <summary>
        /// 更新或新建缓存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public static void SetChapterCache(int id, TheoryChapterVM value)
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<TheoryChapterVM> redisTheoryChapterVM = redisClient.As<TheoryChapterVM>();

            redisTheoryChapterVM.DeleteById(id);
            redisTheoryChapterVM.Store(value);
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="id"></param>
        public static void RemoveChapterCache(int id)
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<TheoryChapterVM> redisTheoryChapterVM = redisClient.As<TheoryChapterVM>();

            redisTheoryChapterVM.DeleteById(id);
        }
        #endregion

        #region 章节子类型缓存操作
        // 加载缓存信息
        static List<TheoryQuestionTypeVM> LoadQueTypeCache()
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<TheoryQuestionTypeVM> redisQueTypeVM = redisClient.As<TheoryQuestionTypeVM>();
            IRedisSet<TheoryQuestionTypeVM> ListQueType = redisQueTypeVM.Sets[redisQueTypeKey];

            ListQueType.Clear();
            redisQueTypeVM.DeleteAll();
            List<TheoryQuestionTypeVM> list = SvrFactory.Instance.TrainingSvr.GetAllQuestionType();
            if (list != null && list.Count != 0)
                redisQueTypeVM.StoreAll(list);
            return list;
        }

        /// <summary>
        /// 加载缓存信息
        /// </summary>
        /// <returns></returns>
        public static List<TheoryQuestionTypeVM> CurQueTypeCache()
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<TheoryQuestionTypeVM> redisQueTypeVM = redisClient.As<TheoryQuestionTypeVM>();

            var result = redisQueTypeVM.GetAll().ToList();
            if (result == null || result.Count == 0)
            {
                return LoadQueTypeCache();
            }
            return result;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public static TheoryQuestionTypeVM GetQueTypeCache(int id)
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<TheoryQuestionTypeVM> redisQueTypeVM = redisClient.As<TheoryQuestionTypeVM>();

            TheoryQuestionTypeVM vm = redisQueTypeVM.GetById(id);
            if (vm != null)
            {
                return vm;
            }
            else
            {
                vm = SvrFactory.Instance.TrainingSvr.GetTheoryQuestionTypes(id);
                if (vm != null)
                {
                    redisQueTypeVM.Store(vm);
                }
                return vm;
            }
        }

        /// <summary>
        /// 更新或新建缓存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public static void SetQueTypeCache(int id, TheoryQuestionTypeVM value)
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<TheoryQuestionTypeVM> redisQueTypeVM = redisClient.As<TheoryQuestionTypeVM>();

            redisQueTypeVM.DeleteById(id);
            redisQueTypeVM.Store(value);
        }
        #endregion

        #region 考核实训人员缓存操作
        // 加载缓存信息
        static List<EntryAssessmentVM> LoadEntryAssessmentCache()
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<EntryAssessmentVM> redisEntryAssessmentVM = redisClient.As<EntryAssessmentVM>();
            IRedisSet<EntryAssessmentVM> ListEntryAssessment = redisEntryAssessmentVM.Sets[redisEntryAssessmentKey];

            ListEntryAssessment.Clear();
            redisEntryAssessmentVM.DeleteAll();
            List<EntryAssessmentVM> list = SvrFactory.Instance.TrainingSvr.GetEntryAssessmentList(new TrainSearch());
            if (list != null && list.Count != 0)
                redisEntryAssessmentVM.StoreAll(list);
            return list;
        }

        /// <summary>
        /// 加载缓存信息
        /// </summary>
        /// <returns></returns>
        public static List<EntryAssessmentVM> CurEntryAssessmentCache()
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<EntryAssessmentVM> redisEntryAssessmentVM = redisClient.As<EntryAssessmentVM>();

            var result = redisEntryAssessmentVM.GetAll().ToList();
            if (result == null || result.Count == 0)
            {
                result = LoadEntryAssessmentCache();
            }
            return result;
        }

        /// <summary>
        /// 更新或新建缓存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public static void SetEntryAssessmentCache(int id, EntryAssessmentVM value)
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<EntryAssessmentVM> redisEntryAssessmentVM = redisClient.As<EntryAssessmentVM>();

            redisEntryAssessmentVM.DeleteById(id);
            redisEntryAssessmentVM.Store(value);
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="id"></param>
        public static void RemoveEntryAssessmentCache(int id)
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<EntryAssessmentVM> redisEntryAssessmentVM = redisClient.As<EntryAssessmentVM>();

            redisEntryAssessmentVM.DeleteById(id);
        }
        #endregion

        #region P2P缓存操作
        // 加载缓存信息
        public static List<P2PProductVM> LoadP2PCache()
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<P2PProductVM> redisP2PVM = redisClient.As<P2PProductVM>();
            IRedisSet<P2PProductVM> ListP2P = redisP2PVM.Sets[redisP2PKey];

            ListP2P.Clear();
            redisP2PVM.DeleteAll();
            List<P2PProductVM> list = SvrFactory.Instance.TrainingSvr.GetP2PProductbyCache();
            if (list != null && list.Count != 0)
                redisP2PVM.StoreAll(list);
            return list;
        }

        /// <summary>
        /// 加载缓存信息
        /// </summary>
        /// <returns></returns>
        public static List<P2PProductVM> CurP2PCache()
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<P2PProductVM> redisP2PVM = redisClient.As<P2PProductVM>();

            var result = redisP2PVM.GetAll().ToList();
            if (result == null || result.Count == 0)
            {
                result = LoadP2PCache();
            }
            return result;
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="id"></param>
        public static void ClearP2PCache()
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<P2PProductVM> redisP2PVM = redisClient.As<P2PProductVM>();

            redisP2PVM.DeleteAll();
        }
        #endregion

        #region 未评分考核ID缓存操作
        // 加载缓存信息
        static List<int> LoadExamIDCache()
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<int> redisExamIDVM = redisClient.As<int>();
            IRedisSet<int> ListExamID = redisExamIDVM.Sets[redisExamIDKey];

            ListExamID.Clear();
            redisExamIDVM.DeleteAll();
            List<int> list = SvrFactory.Instance.TrainingSvr.GetNoScoreExamId();
            if (list != null && list.Count != 0)
                redisExamIDVM.StoreAll(list);
            return list;
        }

        /// <summary>
        /// 加载缓存信息
        /// </summary>
        /// <returns></returns>
        public static List<int> CurExamIDCache()
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<int> redisExamIDVM = redisClient.As<int>();

            var result = redisExamIDVM.GetAll().ToList();
            if (result == null || result.Count == 0)
            {
                result = LoadExamIDCache();
            }
            return result;
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="id"></param>
        public static void RemoveExamIDCache(int id)
        {
            RedisClient redisClient = new RedisClient(ConfigurationManager.AppSettings["CacheIpAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["CachePortAddress"]));//redis服务IP和端口 6379默认端口
            IRedisTypedClient<int> redisExamIDVM = redisClient.As<int>();

            redisExamIDVM.DeleteById(id);
        }
        #endregion
    }
}