$(function () {
    var controller = $("#hdController").val();
    var action = $("#hdAction").val();
    switch (controller) {
        case "proposalcustomer"://1.客户信息
            GetProposalCustomerKnowledge();
            break;
        case "riskevaluation":
            if (action == "index") {
                //2.风险测评-风险指标
                GetRiskIndexKnowledge();
            } else {
                //3.风险测评-风险测评结果
                GetRiskIndexResultKnowledge();
            }
            
            break;
        case "liability"://4.财务分析-资产负债表
            GetLiabilityKnowledge();
            break;
        case "incomeandexpenses"://5.财务分析-收支储蓄表
            GetIncomeAndExpensesKnowledge();
            break;
        case "cashflow"://6.财务分析-现金流量表
            GetCashFlowKnowledge();
            break;
        case "financialratios"://7.财务分析-财务比率分析
            GetFinanciaRationsKnowledge();
            break;
        case "cashplan"://8.现金规划
            GetCashPlanKnowledge();
            break;
        case "lifeeducationplan"://9.生涯规划-教育规划
            GetLifeEducationPlanKnowledge();
            break;
        case "consumptionplan"://10.生涯规划-消费规划
            GetConsumptionPlanKnowledge();
            break;
        case "startanundertakingplan"://11.生涯规划-创业规划
            GetStartAnUndertakingPlanKnowledge();
            break;
        case "retirementplan"://12.生涯规划-退休规划
            GetRetirementPlanKnowledge();
            break;
        case "insuranceplan"://13.生涯规划-保险规划
            GetInsurancePlanKnowledge();
            break;
        case "investmentplan"://14.投资规划
            GetInvestmentPlanKnowledge();
            break;
        case "taxplan"://15.税收筹划
            GetTaxPlanKnowledge();
            break;
        case "distributionofproperty"://16.财产分配
            GetDistributionOfPropertyKnowledge();
            break;
        case "heritage"://17.财产传承
            GetHeritageKnowledge();
            break;
    }
});

/**
 * 获取客户信息知识点
 */
function GetProposalCustomerKnowledge() {
    var html = "";
    html += "<p>" + GetSpace(1) + "一，标准的金融理财包括6个步骤：建立和界定与客户的关系、收集客户信息并帮助客户确定理财目标、分析和评估客户当前财务状况、制定并向客户提交理财规划方案、执行个人理财规划方案、监督个人理财规划方案执行</p>";
    html += "<p>" + GetSpace(1) + "二，理财规划师为客户制订的理财方案是否适合客户的实际情况，关键取决于理财规划师是否对客户的财务信息、与理财相关的非财务信息和客户的期望目标有充分的了解。可见，收集、整理和分析客户的财务信息和与理财相关的非财务信息，是制定理财方案的关键一步</p>";

    $("#knowledge_context").html(html);
}

/**
 * 风险测评-风险指标
 */
function GetRiskIndexKnowledge() {
    var html = "";
    html += "<p>" + GetSpace(1) + "一，理财规划是通过理财方案中的保险规划、教育规划、退休规划、投资规划、税务筹划等实现理财目标的过程</p>";
    html += "<p>" + GetSpace(1) + "二，在理财方案制定过程中，保险规划需要了解客户客观的风险承受能力和主观的风险容忍态度，以确保保险规划中风险自担部分，进而确定保险产品和保额。在投资规划中，需要根据客户的风险承受能力进行投资资产的配置，以使资产组合风险控制在客户承受能力范围内。因此，风险承受能力是个人风险管理和理财规划的重要考量因素</p>";
    html += "<p>" + GetSpace(1) + "三，常见的风险评估方法有：简易量化分析和风险矩阵量化分析。本软件采用风险矩阵量化分析方法，主要考察风险承受能力指标和风险容忍态度指标</p>";

    $("#knowledge_context").html(html);
}

/**
 *风险测评-风险测评结果
 */
function GetRiskIndexResultKnowledge() {
    $(".JS-fixLore").unbind("click").bind("click",function () {
        $(".fix-lore").toggle();
        $(".fix-case").hide();
       // $(".fix-small").css({ "margin-top": "0", "margin-left": "-850px" });
        $("#knowledge").css({ "margin-top": "0", "margin-left": "-365px" });
       //$(" .fix-item-box").css({ "width": "775px" });
        $("#knowledge").css({ "width": "795px" });
    });
    var html = "<p>" + GetSpace(1) + "根据风险指标的结果，查阅风险矩阵，可得出建议资产配置结果";
    html += '<img src="/Content/images/Knowledge01.jpg" />';
    $("#knowledge_context").html(html);
}
/**
 * 财务分析-资产负债表
 */
function GetLiabilityKnowledge() {
    var html = "";
    html += "<p>" + GetSpace(1) + "一，资产和负债是存量的概念，显示某个结算时点资产和负债的状况，资产扣除负债后形成净资产</p>";
    html += "<p>" + GetSpace(1) + "二，资产</p>";
    html += "<p>" + GetSpace(2) + "资产分为流动资产（现金、活期存款、货币市场基金等）、投资资产（定期存款、股票、债券、股票型基金等）和自用资产（自用房地产、自用汽车等）</p>";
    html += "<p>" + GetSpace(1) + "三，负债</p>";
    html += "<p>" + GetSpace(2) + "家庭对外形成的、未来需用现金或其他资产偿还的债务。负债一般划分为消费负债（信用卡欠款、小额消费信贷等）、投资负债（金融投资借款、实业投资借款等）和自用负债（自用房地产贷款、自用汽车贷款等）</p>";
    html += "<p>" + GetSpace(1) + "四，净资产</p>";
    html += "<p>" + GetSpace(2) + "净资产=总资产－总负债</p>";
    html += "<p>" + GetSpace(2) + "=（流动资产+投资资产+自用资产）－（消费负债+投资负债+自用负债）</p>";
    html += "<p>" + GetSpace(2) + "=（流动资产－消费负债）+（投资资产－投资负债）+(自用资产－自用负债)</p>";
    html += "<p>" + GetSpace(2) + "=消费净值+投资净值+自用净值</p>";

    $("#knowledge_context").html(html);
}

/**
 * 财务分析-收支储蓄表
 */
function GetIncomeAndExpensesKnowledge() {
    var html = "";
    html += "<p>" + GetSpace(1) + "一，收入和支出是流量的概念。揭示的是某一个时期家庭的收支情况，收入扣除支出形成储蓄</p>";
    html += "<p>" + GetSpace(1) + "二，收入</p>";
    html += "<p>" + GetSpace(2) + "收入是家庭所获得的经济利益的流入。收入获得的判断标准是必须有经济利益的流入，不能以是否收到现金作为判断标准。获得收入表现为一定的资产增加或负债的减少。收入分为工作收入（工资、薪金、佣金等）和理财收入（利息、股息、资本利得等）</p>";
    html += "<p>" + GetSpace(1) + "三，支出</p>";
    html += "<p>" + GetSpace(2) + "支出是家庭经济利益的流出。支出的判断标准是是否有经济利益的流出，而不是是否支出现金。支出表现为资产的减少或负债的增加。支出分为生活支出（家计支出、子女教育支出等）和理财支出（利息支出、保障型保费支出等）</p>";
    html += "<p>" + GetSpace(1) + "四，储蓄</p>";
    html += "<p>" + GetSpace(2) + "储蓄是收入扣除支出后形成的资产的净增加额，资产或者负债的净变化额。</p>";
    html += "<p>" + GetSpace(2) + "总储蓄=总收入－总支出</p>";
    html += "<p>" + GetSpace(2) + "=（工作收入+理财收入）－（生活支出+理财支出）</p>";
    html += "<p>" + GetSpace(2) + "=（工作收入－生活支出）+（理财收入－理财支出）</p>";
    html += "<p>" + GetSpace(2) + "=工作储蓄+理财储蓄</p>";

    $("#knowledge_context").html(html);
}

/**
 * 财务分析-现金流量表
 */
function GetCashFlowKnowledge() {
    var html = "";
    html += "<p>" + GetSpace(1) + "一，现金是家庭的即付资金，是满足家庭日常生活支出和应急准备的必备资产。现金流量是家庭理财规划过程中一个非常重要的考察对象</p>";
    html += "<p>" + GetSpace(1) + "二，现金流量根据其用途可以分为：生活现金流量、投资现金流量、借贷现金流量和保障现金流量</p>";
    html += "<p>" + GetSpace(1) + "三，注意事项：保障现金流中，只包括保障型保费，储蓄型保费由于会形成现金价值，是一种投资行为，因此，储蓄型保费作为投资现金流量；有关资产负债表项目中的调整项目，涉及现金流量的项目填列在该表中，资产重估增加值或未实现资本利得损失不能列入，因为没有实际产生现金流量</p>";
    
    $("#knowledge_context").html(html);
}
/**
 * 财务分析-财务比率分析
 */
function GetFinanciaRationsKnowledge() {
    var html = "";
    html += "<p>" + GetSpace(1) + "一，资产负债结构：</p>";
    html += "<p>" + GetSpace(2) + "1，负债比率=总负债÷总资产，负债比率越高，财务负担越重</p>";
    html += "<p>" + GetSpace(2) + "2，融资比率=投资负债÷投资资产市值（投资资产小计）（投资负债是运用财务杠杆，期望投资报酬率高于贷款利率的情况下，加速资产成长的负债。）</p>";
    html += "<p>" + GetSpace(2) + "3，投资性资产权数=投资性资产（投资资产小计）÷总资产（投资性资产是资产中最具生产力的部分，投资性资产占总资产的比重越大，表示资产中可累积生息滚利，或赚取资本利得的部分越多，成长的机会越大）</p>";
    html += "<p>" + GetSpace(2) + "4，流动性资产权数=流动性资产÷总资产（流动性资产因为收益较低，只要能够应付交易性需求和预防性需求即可）</p>";
    html += "<p>" + GetSpace(1) + "二，收支储蓄结构</p>";
    html += "<p>" + GetSpace(2) + "1，支出比率=总支出÷总收入</p>";
    html += "<p>" + GetSpace(2) + "2，财务负担率=理财支出÷总收入</p>";
    html += "<p>" + GetSpace(2) + "3，自由储蓄率=自由储蓄额÷总收入</p>";
    html += "<p>" + GetSpace(1) + "三，情境分析</p>";
    html += "<p>" + GetSpace(2) + "1，净资产增长率（致富公式）=净资产增加额÷期初净值=（工作储蓄+理财储蓄）÷（资产－负债）</p>";
    html += "<p>" + GetSpace(1) + "四，依据以上公式，可以依据客户不同家庭生命周期阶段，给出提高净资产增长率的建议：</p>";
    html += "<p>" + GetSpace(2) + "1，家庭形成期，净资产起始点低，工资薪金所得远大于理财收入，此阶段提升工资薪金储蓄率为快速致富的主要因素。</p>";
    html += "<p>" + GetSpace(2) + "2，家庭成长期和成熟期，已累积了不少净资产，理财收入的比重逐步提高，此阶段投资报酬率的提升是能否快速致富的决定性因素</p>";
    html += "<p>" + GetSpace(2) + "3，尽量提高生息资产占总资产的比重。自用资产不能带来理财收入。在各项资产比重中，增加生息资产的比重能够快速提高净资产增值率，在家庭形成期，可以考虑延缓购房或购车的时间，降低自用资产比重，提高生息资产比重。</p>";
    html += "<p>" + GetSpace(2) + "4，在家庭衰退期，必须使净值增长到能够使财务自由度(（目前的净产值*投资报酬率）/目前的年支出 )高于1的水平，因此在整个家庭生命内，应逐年降低工资薪金所得的比重，降低方式不是通过降低工资薪金水平实现，而是通过提高理财收入占整个收入的比重。</p>";
    html += "<p>" + GetSpace(2) + "5，提高负债比重。运用财务杠杆原理扩充信用来投资，当投资报酬率高于负债利率时，净资产报酬率就会高于总投资报酬率，通过杠杆效应加速净资产的增长。</p>";

    $("#knowledge_context").html(html);
}

/**
 * 现金规划
 */

function GetCashPlanKnowledge() {
    var html = "";
    html += "<p>" + GetSpace(1) + "一，一般家庭发生紧急备用金需求主要有以下两个方面的原因：失业或者失能导致的工作收入中断；紧急医疗或意外灾变所导致的超额费用</p>";
    html += "<p>" + GetSpace(1) + "二，紧急备用金的储备形式：</p>";
    html += "<p>" + GetSpace(1) + "三，从资产配置角度，紧急备用金储备主要考虑存款保障，存款保障可以用两种方式储备：</p>";
    html += "<p>" + GetSpace(2) + "1，为安全性且流动性高的活期存款或短期的定期存款</p>";
    html += "<p>" + GetSpace(2) + "2，为备用的贷款额度（信用卡、保单贷款等）</p>";
    
    $("#knowledge_context").html(html);
}
/**
 * 生涯规划-教育规划
 */

function GetLifeEducationPlanKnowledge() {
    var html = "";
   
    html += "<p>" + GetSpace(1) + "一，子女教育金是没有时间弹性和费用弹性的理财目标，因此要预先进行规划</p>";
    html += "<p>" + GetSpace(1) + "二，工作程序：</p>";
    html += "<p>" + GetSpace(2) + "第一步：了解客户家庭成员结构及财务状况</p>";
    html += "<p>" + GetSpace(2) + "第二步：确定客户对子女的教育目标</p>";
    html += "<p>" + GetSpace(2) + "第三步：估算教育费用</p>";
    html += "<p>" + GetSpace(2) + "第四步：选择适当的规划工具</p>";
    html += "<p>" + GetSpace(1) + "三，计算公式：</p>";
    html += "<p>" + GetSpace(2) + "上学时学费= FV(学费增长率,求学年龄-子女年龄,0,-目前学费,1)</p>";
    html += "<p>" + GetSpace(2) + "上学前需准备的总学费=上学时学费×求学时间</p>";
    html += "<p>" + GetSpace(2) + "上学前需准备的现金总金额=上学前需准备的总学费－已经准备的教育费用小计</p>";
    html += "<p>" + GetSpace(2) + "此方案能实现的目标金额= FV（投资收益率÷12，定期定额投资年限×12，- 每月定期</p>";
    html += "<p>" + GetSpace(2) + "定额投资金额，- 一次性投资金额,0）</p>";

    $("#knowledge_context").html(html);
}
/**
 * 生涯规划-消费规划
 */
function GetConsumptionPlanKnowledge() {
    var html = "";

    html += "<p>" + GetSpace(1) + "一，家庭消费支出包括：住房支出、汽车消费支出、信用卡消费支出等，家庭消费支出规划的目的是要合理安排消费资金、树立正确的消费观念，节约成本，保持稳健的财务状况，本软件主要关注购房规划和购车规划</p>";
    html += "<p>" + GetSpace(1) + "二，购房规划：根据目的不同，住房支出分住房消费和住房投资两类</p>";
    html += "<p>" + GetSpace(2) + "1，住房消费：是指居民为取得住房提供的庇护、休息、娱乐和生活空间的服务而进行的消费，这种消费的实现形式可以是租房也可以是买房。</p>";
    html += "<p>" + GetSpace(2) + "2，住房投资：指是指将住房看成投资工具，通过住房价格上涨来应对通货膨胀、获得投资收益以希望资产保值或增值。</p>";
    html += "<p>" + GetSpace(1) + "三，购房目标：必须是量化的，包括计划购房的时间、面积、房价 </p>";
    html += "<p>" + GetSpace(1) + "四，规划原则：不必盲目求大、无需一次到位、量力而行  </p>";
    html += "<p>" + GetSpace(1) + "五，购房财务规划的基本方法：</p>";
    html += "<p>" + GetSpace(2) + "1，以储蓄及还贷能力估算负担得起的房屋总价可负担首付款=目前净资产在未来购房时的终值+以目前到未来购房这段时间内可用于购房的结余终值 </p>";
    html += "<p>" + GetSpace(2) + "2，可负担房贷=未来购房时可用于支付房贷的月结余</p>";
    html += "<p>" + GetSpace(2) + "3，按照想购买的房屋价格来计算每月需负担的费用：首付=房屋总价×首付比例</p>";
    html += "<p>" + GetSpace(2) + "4，月供=PMT(贷款利率，贷款期限，贷款金额，0，0) </p>";
    html += "<p>" + GetSpace(2) + "5，比较：需求金额－可负担金额，以结果判定目标可否实现，是否合理，做出相应规划</p>";
    html += "<p>" + GetSpace(1) + "六，贷款方式选择： </p>";
    html += "<p>" + GetSpace(2) + "1，公积金贷款（建议客户尽量选择这种方式，额度不够再由商业贷款补充，减轻还款压力） </p>";
    html += "<p>" + GetSpace(2) + "2，商业贷款 </p>";
    html += "<p>" + GetSpace(2) + "3，组合贷款 </p>";
    html += "<p>" + GetSpace(1) + "七，计算公式：</p>";
    html += "<p>" + GetSpace(2) + "1，总金额=面积×单价</p>";
    html += "<p>" + GetSpace(2) + "2，首付款=总金额×首付比例 </p>";
    html += "<p>" + GetSpace(2) + "3，月供=PMT（贷款年利率÷12，贷款年限×12，贷款金额， 0,0）</p>";
    html += "<p>" + GetSpace(2) + "4，购房总花费=月供×贷款年限×12+首付 </p>";
    html += "<p>" + GetSpace(1) + "八，购车规划</p>";
    html += "<p>" + GetSpace(2) + "虽然相对于房屋，汽车较为便宜，但是对于一般家庭而言，购买汽车仍然是一笔较大的开支，需要合理筹划，据统计显示，各种税负占了购车总费用的40%，而购车后每年缴纳的费用（如过路费、过桥费、强制保险费、年检费、高额停车费、拥堵费、车辆养护费）几乎占到购车款的15%-20%，而且是按年缴纳，这意味着购车后每年都将有一笔不小的现金流量。所以购车规划不仅要考虑购车时经费支出及还贷压力还要考虑到以后每年的花费对家庭整体财务规划的影响，这些都是在规划师在购车规划中需要考虑到的。 </p>";
    html += "<p>" + GetSpace(1) + "九，计算公式：</p>";
    html += "<p>" + GetSpace(2) + "1，首付款=裸车价格×首付比例 </p>";
    html += "<p>" + GetSpace(2) + "2，购车总花费=∑（月供*贷款年限，首付，购置税，上牌费用，车船使用税，交强险，商业保险）</p>";
    html += "<p>" + GetSpace(2) + "3，月供=PMT(贷款利率÷12，贷款年限×12，裸车价格-首付款,0,0 ) </p>";

    $("#knowledge_context").html(html);
}

/**
 * 生涯规划-创业规划
 */
function GetStartAnUndertakingPlanKnowledge() {
    var html = "";

    html += "<p>" + GetSpace(1) + "一，计算公式：</p>";
    html += "<p>" + GetSpace(2) + "此方案能实现的目标金额=FV（投资收益率÷12，定期定额投资年限×12，- 每月定期定额投资金额，- 一次性投资金额,0） </p>";
   
    $("#knowledge_context").html(html);
}

/**
 * 生涯规划-退休规划
 */

function GetRetirementPlanKnowledge() {
    var html = "";

    html += "<p>" + GetSpace(1) + "一，退休养老规划的必要性：</p>";
    html += "<p>" + GetSpace(2) + "1，养老问题对于每个人来说都是不可回避的问题 </p>";
    html += "<p>" + GetSpace(2) + "2，预期寿命的延长</p>";
    html += "<p>" + GetSpace(2) + "3，提前退休的风险</p>";
    html += "<p>" + GetSpace(2) + "4，社保基金（国际基本养老保险及企业年金）不能足够维持退休时的基本生活所需</p>";
    html += "<p>" + GetSpace(2) + "5，传统养老方式弊端显现，“养儿防老”不堪重负</p>";
    html += "<p>" + GetSpace(2) + "6，其他不确定性因素：通货膨胀、市场利率、家庭成员健康状况、医疗保险制度变化</p>";
    html += "<p>" + GetSpace(1) + "二，退休规划的工具：</p>";
    html += "<p>" + GetSpace(2) + "1，社保基金</p>";
    html += "<p>" + GetSpace(2) + "2，企业年金</p>";
    html += "<p>" + GetSpace(2) + "3，商业保险</p>";
    html += "<p>" + GetSpace(2) + "4，其他工具：如以房养老</p>";
    html += "<p>" + GetSpace(1) + "三，退休规划流程</p>";
    html += "<p>" + GetSpace(2) + "1，确定退休目标：退休年龄与退休后生活质量要求</p>";
    html += "<p>" + GetSpace(2) + "2，估算退休后的支出：认为退休后年支出为目前年支出的一定百分比（通常为80%），再计入通货膨胀率得出退休第一年的生活费用</p>";
    html += "<p>" + GetSpace(2) + "3，估算退休后的收入：一般来说，需将退休后各个时点的收入折现至退休时点</p>";
    html += "<p>" + GetSpace(2) + "4，估算退休金缺口=资金需求－退休收入－现有储备（均以折现到退休时点计）</p>";
    html += "<p>" + GetSpace(2) + "5，制定退休规划：以现有财务资源做出安排，弥补缺口，一般选择定期定投，尽量避免选择风险较高的投资产品。不能突破现有财务资源限制，也以不降低现有生活质量和现有理财目标为宜</p>";
    html += "<p>" + GetSpace(2) + "6，选择退休规划工具</p>";
    html += "<p>" + GetSpace(2) + "7，执行计划</p>";
    html += "<p>" + GetSpace(2) + "8，退休规划反馈与调整</p>";
    html += "<p>" + GetSpace(1) + "四，计算公式：</p>";
    html += "<p>" + GetSpace(2) + "1，满意生活水平= 目前生活水平/生活满意度</p>";
    html += "<p>" + GetSpace(2) + "2，退休时生活水平=FV(退休前通货膨胀率,计划退休年龄-当前年龄，0，- 满意生活水平,1)</p>";
    html += "<p>" + GetSpace(2) + "3，退休后生活水平=退休时生活水平×退休后、退休前生活水平折算比例</p>";
    html += "<p>" + GetSpace(2) + "4，退休时需准备的现金总金额=PV( （退休后投资收益-退休后通货膨胀）/(1+退休后通货膨胀)÷12,希望享有退休生活年限×12,已准备的养老金项目小计－退休后生活水平,- 子女传承费用,1)  </p>";
    html += "<p>" + GetSpace(2) + "5，此方案能实现的目标金额=FV（投资收益率÷12，定期定额投资年限×12，- 每月定期定额投资金额，- 一次性投资金额,0）</p>";

    $("#knowledge_context").html(html);
}
/**
 * 生涯规划-保险规划
 */
function GetInsurancePlanKnowledge() {
    var html = "";

    html += "<p>" + GetSpace(1) + "一，保险在家庭理财规划中的功能</p>";
    html += "<p>" + GetSpace(2) + "1，风险保障</p>";
    html += "<p>" + GetSpace(2) + "2，储蓄功能</p>";
    html += "<p>" + GetSpace(2) + "3，资产保护功能</p>";
    html += "<p>" + GetSpace(2) + "4，融通资金功能：保单质押贷款</p>";
    html += "<p>" + GetSpace(2) + "5，避税功能</p>";
    html += "<p>" + GetSpace(2) + "6，规避通货膨胀利率风险功能：投资连结险，万能寿险</p>";
    html += "<p>" + GetSpace(1) + "二，人身险险种分类</p>";
    html += "<p>" + GetSpace(2) + "1，人寿保险：以人的寿命为保险标的，以人的生存或死亡为保险事件；当发生保险事件是，保险人履行给付保险金责任的一种人身保险</p>";
    html += "<p>" + GetSpace(2) + "2，年金保险：是指在被保险人生存期间，保险人按照合同约定的金额、方式，在约定的时间内开始有规则的、定期地向被保险人给付保险金的保险</p>";
    html += "<p>" + GetSpace(2) + "3，健康保险：是以被保险人的身体为保险标的，使被保险人在疾病或意外事故所致伤害时发生的费用或损失获得补偿的一种保险</p>";
    html += "<p>" + GetSpace(2) + "4，意外伤害保险</p>";
    html += "<p>" + GetSpace(1) + "三，寿险需求方法的选择</p>";
    html += "<p>" + GetSpace(1) + "四，在客户未婚、没有依赖人口时，可以根据生命价值法计算所需保额。</p>";
    html += "<p>" + GetSpace(2) + "1，遗属需求法：适合已婚家庭</p>";
    html += "<p>" + GetSpace(3) + "① 家庭生活费用实质报酬率=(1+投资报酬率)/(1+通货膨胀率)-1=(投资报酬率-通货膨胀率)/(1+通货膨胀率)</p>";
    html += "<p>" + GetSpace(3) + "② 家庭收入实质报酬率=(1+投资报酬率)/(1+收入增长率)-1=(投资报酬率-收入增长率)/(1+收入增长率)</p>";
    html += "<p>" + GetSpace(3) + "③ 调整后家庭生活费用=当前的家庭生活费用×保险事故发生后支出调整率</p>";
    html += "<p>" + GetSpace(3) + "④ 家庭生活费用现值=PV(家庭生活费用实质报酬率,家庭未来生活费准备年数,-调整后家庭生活费用, 0, 1)</p>";
    html += "<p>" + GetSpace(3) + "⑤ 配偶的个人收入现值=PV（家庭收入实际报酬率，家庭未来生活费准备年数，配偶的个人年收入，0,0）</p>";
    html += "<p>" + GetSpace(3) + "⑥ 家庭未来生活费用缺口现值=家庭生活费用现值＋家庭个人收入现值</p>";
    html += "<p>" + GetSpace(3) + "⑦ 遗属需求法应有的寿险保额=∑(家庭未来生活费用缺口现值,紧急备用金现值，教育金现值，养老基金现值，临终及丧葬支出现值，目前贷款余额，家庭生息资产)</p>";
    html += "<p>" + GetSpace(3) + "⑧ 缺口额度=保险需求额度-已有额度</p>";
    html += "<p>" + GetSpace(3) + "⑨ 欠缺额度=缺口额度-预算额度-补充额度</p>";
    html += "<p>" + GetSpace(1) + "五，生命价值法：适合未婚、没有依赖人口时</p>";
    html += "<p>" + GetSpace(1) + "六，应有保额等于未来收入折现值减去未来支出的折现值</p>";
    html += "<p>" + GetSpace(3) + "① 未来工作期间收入现值=PV[（1+投资报酬率）/（1+收入增长率）－1，离退休年数，-当前个人年收入，0,0]</p>";
    html += "<p>" + GetSpace(3) + "② 未来工作期间支出现值=PV[（1+投资报酬率）/（1+通货膨胀率）－1，离退休年数，-当前个人年收入，0,0]</p>";
    html += "<p>" + GetSpace(3) + "③ 个人未来净收入的年金现值=未来工作期间收入现值－未来工作期间支出现值</p>";
    html += "<p>" + GetSpace(3) + "④ 弥补收入应有的寿险保额=个人未来净收入的年金现值</p>";

    $("#knowledge_context").html(html);
}

/**
 * 投资规划
 */
function GetInvestmentPlanKnowledge() {

    $(".JS-fixLore").unbind("click").bind("click", function () {
        $(".fix-lore").toggle();
        $(".fix-case").hide();
       // $(".fix-small").css({ "margin-top": "0", "margin-left": "-575px" });
        // $(" .fix-item-box").css({ "width": "500px" });

        $("#knowledge").css({ "margin-top": "0", "margin-left": "-245px" });
        $("#knowledge").css({ "width": "675px" });
    });


    var html = '';
    html += "<p>" + GetSpace(1) + "一，生命周期理论</p>";
    html += "<p>" + GetSpace(2) + "1，单身期：指从参加工作至结婚的这段时间</p>";
    html += "<p>" + GetSpace(2) + "2，家庭形成期：指从结婚到新生儿诞生的这段时期</p>";
    html += "<p>" + GetSpace(2) + "3，家庭成长期：指子女出生到子女完成大学教育的这段时期</p>";
    html += "<p>" + GetSpace(2) + "4，家庭成熟期：指从子女参加工作到个人退休之前</p>";
    html += "<p>" + GetSpace(2) + "5，家庭衰落期：指退休后的这段时间</p>";
    html += '<img src="/Content/images/Knowledge02.jpg" />';
                
                            
    html += "<p>" + GetSpace(2) + "产品组合预期收益率=保值层比例×保值层产品综合收益率+增值层比例×增值层综合收益率+投机层比例×投机层综合收益率</p>";
    $("#knowledge_context").html(html);
}

/**
 * 税收筹划
 */
function GetTaxPlanKnowledge() {
    $(".JS-fixLore").unbind("click").bind("click", function () {
        $(".fix-lore").toggle();
        $(".fix-case").hide();
       // $(".fix-small").css({ "margin-top": "0", "margin-left": "-880px" });
       // $(" .fix-item-box").css({ "width": "800px" });

      
        $("#knowledge").css({ "margin-top": "0", "margin-left": "-345px" });
        $("#knowledge").css({ "width": "775px" });
    });


    var html = "";
    html += "<p>" + GetSpace(1) + "一，概念：税务是国家借助政治权力，按照法律规定取得财政收入的一种形式</p>";
    html += "<p>" + GetSpace(1) + "二，税务特征：强制性、无偿性、固定性</p>";
    html += "<p>" + GetSpace(1) + "三，个人所得税的征税范围：</p>";
    html += "<p>" + GetSpace(2) + "1，工资、薪金所得</p>";
    html += "<p>" + GetSpace(2) + "2，个体工商户的生产、经营所</p>";
    html += "<p>" + GetSpace(2) + "3，企事业单位的承包经营、承租经营所得</p>";
    html += "<p>" + GetSpace(2) + "4，劳务报酬所得</p>";
    html += "<p>" + GetSpace(2) + "5，稿酬所得</p>";
    html += "<p>" + GetSpace(2) + "6，特许权使用费所得</p>";
    html += "<p>" + GetSpace(2) + "7，利息、股息、红利所得</p>";
    html += "<p>" + GetSpace(2) + "8，财产租赁所得</p>";
    html += "<p>" + GetSpace(2) + "9，财产转让所得</p>";
    html += "<p>" + GetSpace(2) + "10，偶然所得</p>";
    html += "<p>" + GetSpace(2) + "11，经国务院财经部门确定征税的其他所得</p>";
    html += "<p>" + GetSpace(1) + "计算步骤</p>";
    html += '<img src="/Content/images/Knowledge03.jpg" />';
    html += "<p>" + GetSpace(1) + "表-1：仅适用于工资、薪金</p>";
    html += '<img src="/Content/images/Knowledge04.jpg" />';
    html += "<p>" + GetSpace(1) + "表-2：个体工商户的生产经营所得和对企业、事业单位承包经营、租赁经营所得适用</p>";
    html += '<img src="/Content/images/Knowledge05.jpg" />';
    html += "<p>" + GetSpace(1) + "表-3适用于劳务报酬所得</p>";
    html += '<img src="/Content/images/Knowledge06.jpg" />';
    html += "<p>" + GetSpace(1) + "企业所得税</p>";
    html += "<p>" + GetSpace(1) + "流转税类：是以商品交换和提供劳务为前提，以商品流转额和非商品流转额为课税对象的税类。主要分类：营业税、增值税、消费税</p>";
    html += "<p>" + GetSpace(1) + "资源、财产税类</p>";
    html += "<p>" + GetSpace(1) + "目的、行为税类：印花税、城市维护建设税及教育费附加、契税</p>";
    html += "<p>" + GetSpace(1) + "车船税</p>";
    html += "<p>" + GetSpace(1) + "关税</p>";
    $("#knowledge_context").html(html);
}


/**
 * 财产分配
 */

function GetDistributionOfPropertyKnowledge() {
    var html = "";

    html += "<p>" + GetSpace(1) + "一，财产分配与传承从特定的角度为个人和家庭提供了一种规避风险的保障机制，当个人及家庭在遭遇到现实中存在的风险时，这种规划能够帮助客户隔离风险或降低遭遇风险所带来的损失</p>";
    html += "<p>" + GetSpace(1) + "二，个人和家庭可能遭遇的风险主要包括：</p>";
    html += "<p>" + GetSpace(2) + "1，经营所涉及的风险</p>";
    html += "<p>" + GetSpace(2) + "2，夫妻中一方或者双方丧失劳动能力或经济能力的风险</p>";
    html += "<p>" + GetSpace(2) + "3，离婚或者再婚风险</p>";
    html += "<p>" + GetSpace(2) + "4，家庭成员去世的风险</p>";
    html += "<p>" + GetSpace(1) + "三，确定财产分配目标的原则</p>";
    html += "<p>" + GetSpace(2) + "1，风险隔离的原则</p>";
    html += "<p>" + GetSpace(2) + "2，合情合法原则</p>";
    html += "<p>" + GetSpace(2) + "3，照顾妇女儿童原则</p>";
    html += "<p>" + GetSpace(2) + "4，有利方便的原则</p>";
    html += "<p>" + GetSpace(2) + "5，不得损害国家、集体和他人利益的原则</p>";
    html += "<p>" + GetSpace(1) + "四，财产分配常用工具</p>";
    html += "<p>" + GetSpace(2) + "1，公证</p>";
    html += "<p>" + GetSpace(2) + "2，信托</p>";
    $("#knowledge_context").html(html);
}
/**
 * 财产传承
 */
function GetHeritageKnowledge() {
    var html = "";

    html += "<p>" + GetSpace(1) + "一，遗产的范围</p>";
    html += "<p>" + GetSpace(1) + "1，遗产包括：公民的收入；公民的房屋、储蓄和生活用品；公民的林木、牲畜和家禽；公民的文物、图书资料；法律允许公民所有的生产资料；公民的著作权、专利权中的财产权利；公民的其他合法财产。</p>";
    html += "<p>" + GetSpace(1) + "二，继承顺序</p>";
    html += "<p>" + GetSpace(2) + "1，第一顺序的法定继承人：</p>";
    html += "<p>" + GetSpace(3) + "① 配偶</p>";
    html += "<p>" + GetSpace(3) + "② 子女（包括婚生子女、非婚生子女、养子女和有抚养关系的继子女）</p>";
    html += "<p>" + GetSpace(3) + "③ 父母（生父母、养父母和有抚养关系的继父母）</p>";
    html += "<p>" + GetSpace(3) + "④ 对公婆尽了主要赡养义务的丧偶儿媳、对岳父母尽了主要赡养义务的丧偶女婿</p>";
    html += "<p>" + GetSpace(2) + "2，第二顺序法定继承人：</p>";
    html += "<p>" + GetSpace(3) + "① 兄弟姐妹</p>";
    html += "<p>" + GetSpace(3) + "② 祖父们</p>";
    html += "<p>" + GetSpace(3) + "③ 外祖父母</p>";
    html += "<p>" + GetSpace(1) + "三，财产传承规划工具</p>";
    html += "<p>" + GetSpace(2) + "1，遗嘱</p>";
    html += "<p>" + GetSpace(2) + "2，遗嘱信托</p>";
    html += "<p>" + GetSpace(2) + "3，人寿保险信托</p>";
    $("#knowledge_context").html(html);
}
