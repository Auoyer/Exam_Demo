using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
    /// <summary>
    /// 题目状态
    /// </summary>
    public enum TrainExamStauts
    {
        /// <summary>
        /// 未发布
        /// </summary>
        /// 
        [Description("未发布")]
        Unpublish = 0,
        /// <summary>
        /// 已发布
        /// </summary>
        /// 
        [Description("已发布")]
        Publish = 1,

        /// <summary>
        /// 已结束
        /// </summary>
        [Description("已结束")]
        End = 2
    }

    /// <summary>
    /// 题目状态
    /// </summary>
    public enum QuestionStauts
    {
        /// <summary>
        /// 开启
        /// </summary>
        /// 
        [Description("开启")]
        Open = 1,
        /// <summary>
        /// 屏蔽
        /// </summary>
        /// 
        [Description("屏蔽")]
        Close = 2,

        /// <summary>
        /// 逻辑删除
        /// </summary>
        [Description("逻辑删除")]
        Delete = 3
    }

    /// <summary>
    /// 
    ///题型状态
    /// </summary>
    public enum QuestionTypeStatus
    {
        /// <summary>
        /// 开启
        /// </summary>
        /// 
        [Description("开启")]
        Open = 1,       

        /// <summary>
        /// 逻辑删除
        /// </summary>
        [Description("逻辑删除")]
        Delete = 2

    }
    /// <summary>
    /// 考点得分结果状态
    /// </summary>
    public enum IsCorrect
    {
        [Description("正确")]
        Correct = 1,
        [Description("错误")]
        Error = 2
    }

    public enum FormType
    {
        [Description("自动组卷")]
        Automatic = 1,
        [Description("手动组卷")]
        Manual = 2,
    }
    /// <summary>
    /// 题目来源
    /// </summary>
    public enum QuestionSource
    {
        /// <summary>
        /// 系统内置
        /// </summary>
        [Description("系统内置")]
        System = 1,

        /// <summary>
        /// 管理员
        /// </summary>
        [Description("管理员")]
        Admin = 2,
        /// <summary>
        /// 自定义
        /// </summary>
        [Description("自定义")]
        Custom = 3
    }

    /// <summary>
    /// 结构类型
    /// </summary>
    public enum StructType
    {
        /// <summary>
        /// 单选题
        /// </summary>
        [Description("单选题")]
        SelectRadio = 1,

        /// <summary>
        /// 多选题
        /// </summary>
        [Description("多选题")]
        SelectCheckBox = 2,

        /// <summary>
        /// 判断题
        /// </summary>
        [Description("判断题")]
        Determine = 3,

        /// <summary>
        /// 填空题
        /// </summary>
        [Description("填空题")]
        Sketchy = 4,

        /// <summary>
        /// 简答题
        /// </summary>
        [Description("简答题")]
        Synopsis = 5


    }

    /// <summary>
    /// 题目答案
    /// </summary>
    public enum QuestionAnswer
    {
        A = 0,
        B = 1,
        C = 2,
        D = 3,
        E = 4
    }

    /// <summary>
    /// 考试类型
    /// </summary>
    public enum LibraryType
    {
        /// <summary>
        /// 1 理论考试
        /// </summary>
        [Description("理论考试")]
        Theory = 1,

        /// <summary>
        /// 2 认证考试(中国银行业从业人员资格认证)
        /// </summary>
        [Description("中国银行业从业人员资格认证")]
        Certification_Bank = 2,

        /// <summary>
        /// 3 认证考试(助理理财规划师（国家职业资格三级）)
        /// </summary>
        [Description("助理理财规划师（国家职业资格三级）")]
        Certification_Plan = 3

    }

    /// <summary>
    /// 客户类型
    /// </summary>
    public enum CustomerEnum
    {
        [Description("无")]
        None = 0,
        /// <summary>
        /// 潜在客户
        /// </summary>
        [Description("潜在客户")]
        PotentialCustomer = 1,
        /// <summary>
        /// 已有客户
        /// </summary>
        [Description("已有客户")]
        ExistCustomer = 2

    }

    /// <summary>
    /// 章节类型
    /// </summary>
    public enum TheoryChapterType
    {
        /// <summary>
        /// 管理员
        /// </summary>
        [Description("管理员")]
        System = 1,
        /// <summary>
        /// 自定义
        /// </summary>
        [Description("自定义")]
        Custom = 2

    }

    /// <summary>
    /// 理财类型
    /// </summary>
    public enum FinancialType
    {
        /// <summary>
        /// 现金规划
        /// </summary>
        [Description("现金规划")]
        CashPlan = 1,
        /// <summary>
        /// 教育规划
        /// </summary>
        [Description("教育规划")]
        EducationPlan = 2,
        /// <summary>
        /// 消费规划
        /// </summary>
        [Description("消费规划")]
        ConsumptionPlan = 3,
        /// <summary>
        /// 创业规划
        /// </summary>
        [Description("创业规划")]
        StartAnUndertakingPlan = 4,
        /// <summary>
        /// 退休规划
        /// </summary>
        [Description("退休规划")]
        RetirementPlan = 5,
        /// <summary>
        /// 保险规划
        /// </summary>
        [Description("保险规划")]
        InsurancePlan = 6,
        /// <summary>
        /// 投资规划
        /// </summary>
        [Description("投资规划")]
        InvestmentPlan = 7,
        /// <summary>
        /// 税收筹划
        /// </summary>
        [Description("税务筹划")]
        TaxPlan = 8,
        /// <summary>
        /// 财产分配
        /// </summary>
        [Description("财产分配")]
        DistributionOfProperty = 9,
        /// <summary>
        /// 财产传承
        /// </summary>
        [Description("财产传承")]
        Heritage = 10,
        /// <summary>
        /// 综合规划
        /// </summary>
        [Description("综合规划")]
        CompositePlan = 11

    }

    /// <summary>
    /// 考试内容
    /// </summary>
    public enum ExamContent
    {
        /// <summary>
        /// 客户信息
        /// </summary>
        [Description("客户信息")]
        Customerinfo = 1,
        /// <summary>
        /// 风险测评-风险指标
        /// </summary>
        [Description("风险测评-风险指标")]
        RiskIndex = 2,
        /// <summary>
        /// 财务分析-资产负债表
        /// </summary>
        [Description("财务分析-资产负债表")]
        Liability = 3,
        /// <summary>
        /// 财务分析-收支储蓄表
        /// </summary>
        [Description("财务分析-收支储蓄表")]
        IncomeAndExpenses = 4,
        /// <summary>
        /// 财务分析-现金流量表
        /// </summary>
        [Description("财务分析-现金流量表")]
        CashFlow = 5,
        /// <summary>
        /// 财务分析-财务比率分析
        /// </summary>
        [Description("财务分析-财务比率分析")]
        FinancialRatios = 6,
        /// <summary>
        /// 现金规划
        /// </summary>
        [Description("现金规划")]
        CashPlan = 7,
        /// <summary>
        /// 生涯规划-教育规划
        /// </summary>
        [Description("生涯规划-教育规划")]
        LifeEducationPlan = 8,
        /// <summary>
        /// 生涯规划-消费规划
        /// </summary>
        [Description("生涯规划-消费规划")]
        ConsumptionPlan = 9,
        /// <summary>
        /// 生涯规划-创业规划
        /// </summary>
        [Description("生涯规划-创业规划")]
        StartAnUndertakingPlan = 10,
        /// <summary>
        /// 生涯规划-退休规划
        /// </summary>
        [Description("生涯规划-退休规划")]
        RetirementPlan = 11,
        /// <summary>
        /// 生涯规划-保险规划
        /// </summary>
        [Description("生涯规划-保险规划")]
        InsurancePlan = 12,
        /// <summary>
        /// 投资规划
        /// </summary>
        [Description("投资规划")]
        InvestmentPlan = 13,
        /// <summary>
        /// 税收筹划
        /// </summary>
        [Description("税务筹划")]
        TaxPlan = 14,
        /// <summary>
        /// 财产分配
        /// </summary>
        [Description("财产分配")]
        DistributionOfProperty = 15,
        /// <summary>
        /// 财产传承
        /// </summary>
        [Description("财产传承")]
        Heritage = 16

    }

    /// <summary>
    /// 考核点类型
    /// </summary>
    public enum ExamPointType
    {
        /// <summary>
        /// 客观题
        /// </summary>
        [Description("客观题")]
        Objective = 1,
        /// <summary>
        /// 主观题
        /// </summary>
        [Description("主观题")]
        Subjective = 2

    }

    /// <summary>
    /// 案例来源
    /// </summary>
    public enum CaseSource
    {
        /// <summary>
        /// 内置
        /// </summary>
        [Description("内置")]
        System = 1,
        /// <summary>
        /// 自定义
        /// </summary>
        [Description("自定义")]
        Custom = 2
    }

    /// <summary>
    /// 考核类型
    /// </summary>
    public enum ExamineType
    {
        /// <summary>
        /// 销售机会
        /// </summary>
        [Description("销售机会")]
        SalesOpportunities = 1,
        /// <summary>
        /// 实训考核
        /// </summary>
        [Description("实训考核")]
        TrainingEvaluation = 2
    }

    /// <summary>
    /// 文档转换状态
    /// </summary>
    public enum ConvertStatus
    {
        /// <summary>
        /// 正在转换
        /// </summary>
        [Description("正在转换")]
        Converting = 0,


        /// <summary>
        /// 转换成功
        /// </summary>
        [Description("转换成功")]
        Success = 1,

        /// <summary>
        /// 转换失败
        /// </summary>
        [Description("转换失败")]
        Failed = 2
    }

    /// <summary>
    /// 课程资源来源，VM中用到
    /// </summary>
    public enum ResourceSource
    {

        /// <summary>
        /// 内置资源
        /// </summary>
        [Description("内置资源")]
        System = 1,
        /// <summary>
        /// 自定义
        /// </summary>
        [Description("自定义")]
        Custom = 2
    }

    /// <summary>
    /// 学生端销售机会状态
    /// </summary>
    public enum TrainExamState
    {
        /// <summary>
        /// 未开始
        /// </summary>
        [Description("未开始")]
        UnStart = 1,

        /// <summary>
        /// 已开始
        /// </summary>
        [Description("已开始")]
        Starting = 2,

        /// <summary>
        /// 已结束
        /// </summary>
        [Description("已结束")]
        End = 3
    }



    /// <summary>
    /// 选择时间类型
    /// </summary>
    public enum ChoseTimeType
    {
        /// <summary>
        /// 当月
        /// </summary>
        [Description("当月")]
        Month = 1,
        /// <summary>
        /// 当周
        /// </summary>
        [Description("当周")]
        Week = 2,
        /// <summary>
        /// 当天
        /// </summary>
        [Description("当天")]
        Day = 3

    }

    /// <summary>
    /// 教育阶段
    /// </summary>
    public enum EducationStage
    {
        /// <summary>
        /// 幼儿园教育
        /// </summary>
        [Description("幼儿园教育")]
        NurseryschoolEdu = 1,

        /// <summary>
        /// 小学教育
        /// </summary>
        [Description("小学教育")]
        PrimaryschoolEdu = 2,

        /// <summary>
        /// 初中教育
        /// </summary>
        [Description("初中教育")]
        JuniormiddleschoolEdu = 3,

        /// <summary>
        /// 高中教育
        /// </summary>
        [Description("高中教育")]
        SeniormiddleschoolEdu = 4,

        /// <summary>
        /// 大学教育
        /// </summary>
        [Description("大学教育")]
        UniversityEdu = 5,

        /// <summary>
        /// 留学教育
        /// </summary>
        [Description("留学教育")]
        StudyabroadEdu = 6
    }
    /// <summary>
    /// 服务类型
    /// </summary>
    public enum EnServiceType
    {
        /// <summary>
        /// 请选择
        /// </summary>
        [Description("请选择")]
        Chose = 0,

        /// <summary>
        /// 电话拜访
        /// </summary>
        [Description("电话拜访")]
        Phone = 1,

        /// <summary>
        /// 登门拜访
        /// </summary>
        [Description("登门拜访")]
        Home = 2,

        /// <summary>
        /// 接触面谈
        /// </summary>
        [Description("接触面谈")]
        Metting = 3,

        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Other = 4
    }



    /// <summary>
    /// 证件类型
    /// </summary>
    public enum IDType
    {
        /// <summary>
        /// 身份证
        /// </summary>
        [Description("身份证")]
        IdentityCard = 1
    }


    /// <summary>
    /// 编号类型
    /// </summary>
    public enum NumberType
    {
        /// <summary>
        /// 客户编码
        /// </summary>
        [Description("客户编码")]
        Customer = 1,

        /// <summary>
        /// 建议书编号
        /// </summary>
        [Description("建议书编号")]
        Proposal = 2,

    }

    /// <summary>
    /// 建议书客户家属类型
    /// </summary>
    public enum ProposalCustDetailType
    {
        /// <summary>
        /// 客户信息家属
        /// </summary>
        [Description("客户信息家属")]
        CustomerFaimly = 1,
        /// <summary>
        /// 财产分配家属
        /// </summary>
        [Description("财产分配家属")]
        FinancialFaimly = 2
    }


    /// <summary>
    /// 高净值客户 资产负债净值>600万的客户
    /// </summary>
    public enum IsHigStucustomer
    {
        /// <summary>
        /// 高净值潜在客户
        /// </summary>
        [Description("高净值潜在客户")]
        CustomerPotentialHighAssets = 1,

        /// <summary>
        /// 高净值已有客户
        /// </summary>
        [Description("高净值已有客户")]
        CustomerExistHighAssets = 2

    }


    public enum ExaminationStatus
    {
        /// <summary>
        /// 待评分
        /// </summary>
        [Description("待评分")]
        WaitScore = 1,
        /// <summary>
        /// 已评分
        /// </summary>
        [Description("已评分")]
        AlreadyScore = 2,
    }

    /// <summary>
    /// 消息类型：公告、授课进度、课时安排
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// 教师公告
        /// </summary>
        [Description("教师公告")]
        Notice = 1,

        /// <summary>
        /// 授课进度
        /// </summary>
        [Description("授课进度")]
        Schedule = 2,

        /// <summary>
        /// 课时安排
        /// </summary>
        [Description("课时安排")]
        ClassSchedule = 3

    }

    /// <summary>
    /// 客户所处家庭生命周期类型
    /// </summary>
    public enum LifeCycleType
    {
        /// <summary>
        /// 单身期
        /// </summary>
        [Description("单身期")]
        SinglePhase = 1,
        /// <summary>
        /// 家庭形成期（筑巢期）
        /// </summary>
        [Description("家庭形成期（筑巢期）")]
        FormationStage = 2,
        /// <summary>
        /// 家庭成长期（满巢期）
        /// </summary>
        [Description("家庭成长期（满巢期）")]
        GrowthPeriod = 3,
        /// <summary>
        /// 家庭成熟期（离巢期）
        /// </summary>
        [Description("家庭成熟期（离巢期）")]
        MatureStage = 4,
        /// <summary>
        /// 家庭衰老期（空巢期）
        /// </summary>
        [Description("家庭衰老期（空巢期）")]
        AgeingStage = 5
    }
    /// <summary>
    /// 有效时间类型
    /// </summary>
    public enum UserTimeSummaryType
    {
        /// <summary>
        /// 子系统
        /// </summary>
        [Description("子系统")]
        Notice = 1,

        /// <summary>
        /// 资源
        /// </summary>
        [Description("资源")]
        Schedule = 2,

        /// <summary>
        /// 认证考试
        /// </summary>
        [Description("认证考试")]
        ClassSchedule = 3

    }

    /// <summary>
    /// 基金产品类型
    /// </summary>
    public enum FundProductType
    {
        /// <summary>
        /// 1. 货币型基金
        /// </summary>
        [Description("货币型基金")]
        Currency = 1,
        /// <summary>
        /// 2. 股票型基金
        /// </summary>
        [Description("股票型基金")]
        Stock = 2,
        /// <summary>
        /// 3. 债券型基金
        /// </summary>
        [Description("债券型基金")]
        Bond = 3,
        /// <summary>
        /// 4. 混合型基金
        /// </summary>
        [Description("混合型基金")]
        Mixture = 4,
    }

}