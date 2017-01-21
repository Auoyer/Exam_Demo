/**
 * @name 枚举数据类
 * @remark 需跟后台枚举数据保持一致
 */
; var EnumList = {
	/**
	 * 登录角色
	 */
	Role: {
	    SA: 1,
	    Admin: 2,
	    Teacher: 3,
	    Student: 4,
	    Guest: 100,
	},
    /**
	 * 案例来源
	 */
	CaseSource: {
	    System: 1,
	    Custom: 2,
	},

    /**
    *保存状态：0-新增
    */
	SaveState: {
	    Add: 0, //新增
        Update:1 //编辑
	},

    /**
    *客户类型
    */
	CustomerTyle: {
	    PotentialCustomer: 1,//潜在客户
	    ExistCustomer: 2     //已有客户
	},

    /**
    *客户来源
    */
	CustomerSourceType: {
	    SalesOpportunities : 1,//销售机会
	    MyselfAdd : 2          //自主增加
	},

    /**
    *建议书客户家属类型
    */
    ProposalCustDetailType:{
        CustomerFaimly: 1,    //客户信息家属
        FinancialFaimly: 2    //财产分配家属
    },

    /**
    *建议书目前的状态
    */
    ProposalStatus: {
        None:0,
        UnCommitted: 1,
        UnAudited: 2,
        Audited: 3
    },

    /**
    *潜在客户/已有客户对应的建议书状态
    */
    StuCustomerProposalStatus: {
        Add: 1,
        Edit: 2,
        Submit: 3,
    },

    //消息类型：公告、授课进度、课时安排
    MessageType: {
        Notice: 1,
        Schedule: 2,
        ClassSchedule:3
    },
    //考核点得分状态
    IsCorrect: {
        Correct: 1,//正确
        Error: 2//错误
    },
    //考核内容
    ExamContent:{
        Customerinfo: 1,//客户信息
        RiskIndex: 2,//风险测评-风险指标
        Liability: 3,//财务分析-资产负债表
        IncomeAndExpenses: 4,//财务分析-收支储蓄表
        CashFlow: 5,//财务分析-现金流量表
        FinancialRatios: 6,//财务分析-财务比率分析
        CashPlan: 7,//现金规划
        LifeEducationPlan: 8,//生涯规划-教育规划
        ConsumptionPlan: 9,//生涯规划-消费规划
        StartAnUndertakingPlan: 10,//生涯规划-创业规划
        RetirementPlan: 11,//生涯规划-退休规划
        InsurancePlan: 12,//生涯规划-保险规划
        InvestmentPlan: 13,//投资规划
        TaxPlan: 14,//税收筹划
        DistributionOfProperty: 15,//财产分配
        Heritage: 16//财产传承
    },
    ExamPointType: {
        Objective: 1,//客观题
        Subjective: 2//主观题
    },
    //提交内容的评分状态
    ExaminationStatus: {
        WaitScore: 1,//待评分
        AlreadyScore: 2 //已评分
    },
    //考核类型
    ExamineType: {
        SalesOpportunities: 1,//销售机会
        TrainingEvaluation: 2 //实训考核
    },
    //试卷状态
    ExamPaperStatus: {
        UnPublish: 0,
        Publish: 1,
        End: 2,
        Del: 3
    },
    /**
    *  基金产品类型
    */
    FundProductType: {
        Currency: 1,
        Stock: 2,
        Bond: 3,
        Mixture: 4
    },

};