//------------预览JS
//投资规划--序号常量
var IndexFlag = 0;
//财务分配--序号常量
var DistributionOfProperty_index = 0;
//是否显示页面公共字段--------子系统字段
//客户信息
var CustomerInfo1 = false;
var GetRiskEvaluationInfo1 = false;
var LoadLiabilityByProposalId1 = false;
//收支储蓄表
var GetIncomeAndExpenses1 = false;
//现金流量
var GetCashFlowList1 = false;
//财务比率分析
var GetFinancialRatiosList1 = false;
//现金规划
var GetCashPlanByProposalId1 = false;
//教育规划
var GetLifeEducationPlan1 = false;
//消费规划
var GetConsumptionPlan1 = false;
//创业规划
var GetStartAnUndertakingPlanList1 = false;
//退休规划
var LoadRetirementPlan1 = false;
//保险规划
var LoadInsurancePlan1 = false;
//显示计数器
var NumCalc = 0;
$(function () {
    //销售机会Id
    var TrainExamId = $.getUrlParam("TrainExamId");
    //建议书Id
    var ProposalId = $.getUrlParam("ProposalId");
    if (ProposalId != null && ProposalId != "" && typeof ProposalId != "undefined" && ProposalId!=0) {
        LoadPreview(ProposalId);
    }
    //实训考核什么都没有填写，需要查看空白预览
    if (ProposalId == 0) {
        $("#ProposalCustomer").hide();
        $("#RiskIndexDiv").hide();
        $("#RiskIndexDivShow").hide();
        $("#FinanceLiabilityDiv").hide();
        $("#FinanceIncomeAndExpensesDiv").hide();
        $("#FinancialRatios").hide();
        $("#CashFlow").hide();
        $("#FinanceCashPlanDiv").hide();
        $("#LifeEducationPlan").hide();
        $("#StartAnUndertakingPlan").hide();
        $("#ConsumptionPlan").hide();
        $("#LiveRetirementPlanDiv").hide();
        $("#FinanceInsurancePlanDiv").hide();
        $("#InvestmentPlan").hide();
        $("#TaxPlan").hide();
        $("#DistributionOfPropertyDiv").hide();
        $("#Heritage").hide();
    }
});
//加载各个子规划
function LoadPreview(ProposalId) {
    //获取建议书客户信息及家属信息列表
    if (ProposalId != null && ProposalId != "" && ProposalId != undefined) {
        //客户基本信息
        CustomerInfo(ProposalId);
        //风险测评
        GetRiskEvaluationInfo(ProposalId);
        //资产负债表
        LoadLiabilityByProposalId(ProposalId);
        //收支储蓄表
        GetIncomeAndExpenses(ProposalId);
        //现金流量
        GetCashFlowList(ProposalId)
        //财务比率分析
        GetFinancialRatiosList(ProposalId);
        //现金规划
        GetCashPlanByProposalId(ProposalId);
        //教育规划
        //加载每月可支配资金 可用资产
        GetLifeEducationPlan(ProposalId);
        ViewEveryMonthMoney("LifeEducationPlan/GetmoneyList", ProposalId, "LifeEducationPlan");
        //教育配套
        //消费规划
        //加载每月可支配资金 可用资产
        GetConsumptionPlan(ProposalId);
        ViewEveryMonthMoney("LifeEducationPlan/GetmoneyList", ProposalId, "ConsumptionPlan");
        //创业规划
        GetStartAnUndertakingPlanList(ProposalId);
        ViewEveryMonthMoney("LifeEducationPlan/GetmoneyList", ProposalId, "StartAnUndertakingPlan");
        //退休规划
        LoadRetirementPlan(ProposalId);
        //保险规划
        LoadInsurancePlan(ProposalId);
        //投资规划
        LoadInvestmentPlan(ProposalId);
        //税收规划
        GetTaxPlan(ProposalId);
        //财务分配
        LoadDistributionOfPropertyInfo(ProposalId);
        //财产传承
        GetHeritage(ProposalId);
    }
}

//客户信息显示
function CustomerInfo(ProposalId) {
    $.ajax({
        url: "/CompetitionUser/ProposalCustomer/GetProposalCustomer",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: ProposalId,
            rId: Math.random(),
        },
        success: function (data) {
            if (data.ProposalName != "") {
                SetProposalCustomerInfo(data);//编辑建议书客户信息的设置
            }
            else {
                CustomerInfo1 = true;//新增
                $("#ProposalCustomer").hide();
            }
        }
    });
}

//设置建议书客户信息
function SetProposalCustomerInfo(data) {
    //隐藏域
    //  $("#hdProposalCustomerId").text(data.ProposalCustomerVM.Id);
    if (data.ProposalNum != null && data.ProposalNum != "") {
        //建议书
        $("#ProposalNum").text(data.ProposalNum);
    }
    $("#ProposalName").text(data.ProposalName);
    if (data.ProposalCustomerVM != null) {
        NumCalc++;
        var CustomerName = data.ProposalCustomerVM.CustomerName == null ? "" : data.ProposalCustomerVM.CustomerName;
        var PinYin = data.ProposalCustomerVM.PinYin == null ? "" : data.ProposalCustomerVM.PinYin;
        var IDNum = data.ProposalCustomerVM.IDNum == null ? "" : data.ProposalCustomerVM.IDNum;
        var Phone = data.ProposalCustomerVM.Phone == null ? "" : data.ProposalCustomerVM.Phone;
        var Tel = data.ProposalCustomerVM.Tel == null ? "" : data.ProposalCustomerVM.Tel;
        var Email = data.ProposalCustomerVM.Email == null ? "" : data.ProposalCustomerVM.Email;
        var Position = data.ProposalCustomerVM.Position == null ? "" : data.ProposalCustomerVM.Position;
        var Company = data.ProposalCustomerVM.Company == null ? "" : data.ProposalCustomerVM.Company;
        var Address = data.ProposalCustomerVM.Address == null ? "" : data.ProposalCustomerVM.Address;
        //客户信息
        $("#CustomerName").text(CustomerName);
        $("#PinYin").text(PinYin);
        $("#Age").text(data.ProposalCustomerVM.Age);
        $("#IDType").text(data.ProposalCustomerVM.IDType);
        $("#IDNum").text(IDNum);
        $("#Phone").text(Phone);
        $("#Tel").text(Tel);
        $("#Email").text(Email);
        $("#PositionStu").text(Position);
        $("#Company").text(Company);
        $("#AddressStu").text(Address);
        //客户亲属列表
        $("#siblist").html("");
        if (data.ProposalCustomerVM.ProposalCustomerDetailList.length==0) {
            $("#siblist").prev().remove();
        }
        $(data.ProposalCustomerVM.ProposalCustomerDetailList).each(function (index, dom) {
            EditList(dom.DependentName, dom.Age, dom.Relation, dom.InCome);
        });
    }
}
//客户家庭信息
function EditList(DependentName, Age, Relation, InCome) {

    var trHtml = "";
    trHtml += "<div class=\"sib-item\">";
    trHtml += "<div class=\"fif-form\">";
    trHtml += "<div class=\"fif-box\">";
    trHtml += "<label class=\"fif-text\">姓 名</label>";
    trHtml += "<div class=\"input\">{0}</div>";
    trHtml += "</div>";
    trHtml += "<div class=\"fif-box\">";
    trHtml += "<label class=\"fif-text\">年 龄</label>";
    trHtml += "<div class=\"input\">{1}<span class=\"ml10\">岁</span></div>";
    trHtml += "</div>";
    trHtml += "<div class=\"fif-box\">";
    trHtml += "<label class=\"fif-text\">与客户关系</label>";
    trHtml += "<div class=\"input\">{2}</div>";
    trHtml += "</div>";
    trHtml += "<div class=\"fif-box\">";
    trHtml += "<label class=\"fif-text\">年收入</label>";
    trHtml += "<div class=\"input\">{3} 元</div>";
    trHtml += "</div></div></div>";

    var html = StringHelper.FormatStr(trHtml,
        DependentName,      //0 姓名
        Age,                //1 年龄
        Relation,           //2 与客户关系
        InCome            //3 年收入

        );

    $("#siblist").append(html);
}

//风险测评
function GetRiskEvaluationInfo(ProposalId) {

    $.ajax({
        url: "/CompetitionUser/RiskEvaluation/GetRiskEvaluationInfo",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: ProposalId
        },
        success: function (data) {
            if (data != null) {
                NumCalc++;
                $("#RiskIndexId").text(data.Id);
               // $("#ProposalId").text(data.ProposalId);
                $("#AgeScore").text(data.AgeScore);
                $("#JobScore").text(data.JobScore);
                $("#FamilyScore").text(data.FamilyScore);
                $("#HouseScore").text(data.HouseScore);
                $("#EXPScore").text(data.EXPScore);
                $("#KnowledgeScore").text(data.KnowledgeScore);
                $("#RCIScore").text(data.RCIScore);
                $("#TolerateScore").text(data.TolerateScore);
                $("#ConsiderationScore").text(data.ConsiderationScore);
                $("#LossScore").text(data.LossScore);
                $("#MentalityScore").text(data.MentalityScore);
                $("#CharacterScore").text(data.CharacterScore);
                $("#AvoidScore").text(data.AvoidScore);
                $("#UpdateDate").text(data.UpdateDate);
                $("#RAIScore").text(data.RAIScore);
                ShowInfo(data);
            } else {
                $("#RiskIndexDiv").hide();
                $("#RiskIndexDivShow").hide();
            }
        }
    });
}


//显示评测结果
function ShowInfo(data) {
    $("#EvaluationDate").text(data.UpdateDateStr);
    var RCIScore = data.RCIScore;//风险承受能力 
    var RAIScore = data.RAIScore;//风险容忍态度 
    var length = ControlTable.length;
    var AbilityMin = 0, AbilityMax = 0, AttitudeMin = 0;
    for (var i = 0; i < length; i++) {
        AbilityMin = ControlTable[i].AbilityMin;
        AbilityMax = ControlTable[i].AbilityMax;
        AttitudeMin = ControlTable[i].AttitudeMin;
        AttitudeMax = ControlTable[i].AttitudeMax;
        if (AbilityMin <= RCIScore && RCIScore <= AbilityMax && AttitudeMin <= RAIScore && RAIScore <= AttitudeMax) {
            $("#DistributionRatio tr:eq(1) td:eq(0)").text(ControlTable[i].Currency);
            $("#DistributionRatio tr:eq(1) td:eq(1)").text(ControlTable[i].Bond);
            $("#DistributionRatio tr:eq(1) td:eq(2)").text(ControlTable[i].Stock);
            $("#RiskBearingCapacity").text(ControlTable[i].Ability);//风险承受能力
            $("#RiskToleranceAttitude").text(ControlTable[i].Attitude);//风险容忍态度 
            ShowPieInfo(ControlTable[i].Currency, ControlTable[i].Bond, ControlTable[i].Stock);
            return true;
        }
    }
}
//测评饼
function ShowPieInfo(Currency, Bond, Stock) {
    var chart;
    $('.distribute').highcharts({
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false
        },
        title: {
            text: '投资分配比例',
            align: 'left'
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        colors: ['#63b2f4', '#2a91e6', '#086cc1'], //'#46adb7', '#f2a83e', '#e16556'
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: false
                },
                showInLegend: true
            }
        },
        series: [{
            type: 'pie',
            name: '投资分配比例',
            data: [
                ['货币', Currency],
                {
                    name: '债券',
                    y: Bond,
                    sliced: false,
                    selected: false
                },
                ['股票', Stock],
            ]
        }]
    });
}

//资产负债表------------------------预览加载
function LoadLiabilityByProposalId(ProposalId) {
    $.ajax({
        url: "/CompetitionUser/Liability/LoadLiabilityByProposalId",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: ProposalId

        },
        success: function (data) {
            if (data != null) {
                NumCalc++;
                $("#Cash").text(data.Cash);
                $("#RMBDeposit").text(data.RMBDeposit);
                $("#OtherAsset").text(data.OtherAsset);
                $("#RMBFixedDeposit").text(data.RMBFixedDeposit);//----人民币固定存款
                $("#ForeignCurrencyFixedDeposit").text(data.ForeignCurrencyFixedDeposit);
                $("#StockInvestment").text(data.StockInvestment);
                $("#BondInvestment").text(data.BondInvestment);
                $("#FundInvestment").text(data.FundInvestment);
                $("#IndustryInvestment").text(data.IndustryInvestment);
                $("#EstateInvestment").text(data.EstateInvestment);
                $("#PolicyInvestment").text(data.PolicyInvestment);
                $("#OtherInvestment").text(data.OtherInvestment);
                $("#Estate").text(data.Estate);//---------房产
                $("#Car").text(data.Car);
                $("#Others").text(data.Others);
                $("#TotalAssets").text(data.TotalAssets);
                $("#CreditCard").text(data.CreditCard);//-------信用卡借款
                $("#Microfinance").text(data.Microfinance);
                $("#OtherLoan").text(data.OtherLoan);
                $("#FinancialLoan").text(data.FinancialLoan);//-----金融实用借款
                $("#IndustryInvestmentLoan").text(data.IndustryInvestmentLoan);
                $("#EstateInvestmentLoan").text(data.EstateInvestmentLoan);
                $("#OtherInvestmentLoan").text(data.OtherInvestmentLoan);
                $("#EstateLoan").text(data.EstateLoan);//------自用房地产
                $("#CarLoan").text(data.CarLoan);
                $("#OthersLoan").text(data.OthersLoan);
                $("#TotalLoan").text(data.TotalLoan);
                //然后给所有的小计赋值
                var flowAssets = calcFlowAssets(data.Cash, data.RMBDeposit, data.OtherAsset);//流动资产小计
                $("#assetSum01").text(flowAssets.toMyFixed(2));//流动资产小计
                var invesymentAsset = calcInvestmentAssets(data.RMBFixedDeposit, data.ForeignCurrencyFixedDeposit, data.StockInvestment, data.BondInvestment, data.FundInvestment, data.IndustryInvestment, data.EstateInvestment, data.PolicyInvestment, data.OtherInvestment);//投资资产小计
                $("#assetSum02").text(invesymentAsset.toMyFixed(2));//投资资产小计
                var selfAsset = calcSelfAsset(data.Estate, data.Car, data.Others);
                $("#assetSum03").text(selfAsset.toMyFixed(2));//自用资产小计
                var consumeLiability = calcConsumeAssets(data.CreditCard, data.Microfinance, data.OtherLoan);//消费负债
                $("#loanSum01").text(consumeLiability.toMyFixed(2));//消费负债
                var inverstmentLiability = calcInvestmentLiability(data.FinancialLoan, data.IndustryInvestmentLoan, data.EstateInvestmentLoan, data.OtherInvestmentLoan);
                $("#loanSum02").text(inverstmentLiability.toMyFixed(2));//投资负债
                var selfLiability = calcSelfLiability(data.EstateLoan, data.CarLoan, data.OthersLoan);
                $("#loanSum03").text(selfLiability.toMyFixed(2));//自用负债
                //消费净资产
                var consumeVal = flowAssets * 1 - consumeLiability * 1;
                $("#consumeVal").text(consumeVal.toMyFixed(2));
                //投资净自残
                var investmentVal = invesymentAsset * 1 - inverstmentLiability * 1;
                $("#investmentVal").text(investmentVal.toMyFixed(2));
                //自用净值
                var selfVal = selfAsset * 1 - selfLiability * 1;
                $("#selfVal").text(selfVal.toMyFixed(2));
                //净值合计
                var TotalVal = consumeVal * 1 + investmentVal * 1 + selfVal * 1;
                $("#TotalVal").text(TotalVal.toMyFixed(2))
                //资产合计
                SaveDefaultValueCommon("FinanceLiabilityDiv");
            } else {
                $("#FinanceLiabilityDiv").hide();
            }
        }
    });
};

//收支储蓄表------------------------预览加载
function GetIncomeAndExpenses(ProposalId) {
    $.ajax({
        url: "/CompetitionUser/IncomeAndExpenses/LoadIncomeAndExpensesByProposalId",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: ProposalId
        },
        success: function (data) {
            if (data != null) {

                NumCalc++;
                var EndowmentInsurance = data.EndowmentInsurance;
                var HousingFund = data.HousingFund;
                $("#FinanceIncomeAndExpensesDiv #IncomeAndExpensesId").text(data.Id);;//---工作收入小计;
                $("#FinanceIncomeAndExpensesDiv #ProposalId").text(data.ProposalId);;//---工作收入小计;
                $("#FinanceIncomeAndExpensesDiv #JobIncome").text(data.JobIncome);
                $("#FinanceIncomeAndExpensesDiv #EndowmentInsurance").text(EndowmentInsurance);//养老
                $("#FinanceIncomeAndExpensesDiv #MedicalInsurance").text(data.MedicalInsurance);
                $("#FinanceIncomeAndExpensesDiv #HousingFund").text(HousingFund);//住房
                $("#FinanceIncomeAndExpensesDiv #OtherJobIncome").text(data.OtherJobIncome);
                $("#FinanceIncomeAndExpensesDiv #FamilyExpense").text(data.FamilyExpense);;//---2.	生活支出
                $("#FinanceIncomeAndExpensesDiv #ChildExpense").text(data.ChildExpense);
                $("#FinanceIncomeAndExpensesDiv #OtherExpense").text(data.OtherExpense);
                $("#FinanceIncomeAndExpensesDiv #Interest").text(data.Interest);//--3理财收入
                $("#FinanceIncomeAndExpensesDiv #CapitalGains").text(data.CapitalGains);
                $("#FinanceIncomeAndExpensesDiv #OtherIncome").text(data.OtherIncome);
                $("#FinanceIncomeAndExpensesDiv #InterestExpense").text(data.InterestExpense);//理财支出
                $("#FinanceIncomeAndExpensesDiv #InsuranceExpense").text(data.InsuranceExpense);
                $("#FinanceIncomeAndExpensesDiv #OtherFinanceExpense").text(data.OtherFinanceExpense);
                //减：养老保险储蓄 住房公积金储蓄
                $("#FinanceIncomeAndExpensesDiv #EndowmentInsuranceSub").text(data.EndowmentInsurance.toMyFixed(2));//养老
                $("#FinanceIncomeAndExpensesDiv #HousingFundSub").text(data.HousingFund.toMyFixed(2));//住房
                //然后给所有的小计赋值
                var WorkIncome = calcWorkIncome(data.JobIncome, data.EndowmentInsurance, data.MedicalInsurance, data.HousingFund, data.OtherJobIncome);//---工作收入小计;
                $("#FinanceIncomeAndExpensesDiv #workIncome01").text(WorkIncome.toMyFixed(2));//工作收入小计;
                var LiveExpense = calcLiveExpense(data.FamilyExpense, data.ChildExpense, data.OtherExpense);//-2.	生活支出
                $("#FinanceIncomeAndExpensesDiv #liveExpense01").text(LiveExpense.toMyFixed(2));//	生活支出
                var InvestmentIncome = calcInvestmentIncome(data.Interest, data.CapitalGains, data.OtherIncome);//--3理财收入
                $("#FinanceIncomeAndExpensesDiv #investmentIncome01").text(InvestmentIncome.toMyFixed(2));///--3理财收入
                var InvestmentExpense = calcInvestmentExpense(data.InterestExpense, data.InsuranceExpense, data.OtherFinanceExpense);//理财支出
                $("#FinanceIncomeAndExpensesDiv #investmentExpense01").text(InvestmentExpense.toMyFixed(2));//理财支出

                //3.	工作储蓄
                var WolkDeposit01 = WorkIncome * 1 - LiveExpense * 1;
                $("#FinanceIncomeAndExpensesDiv #wolkDeposit01").text(WolkDeposit01.toMyFixed(2));
                //6.	理财储蓄
                var InvestmentDeposit01 = InvestmentIncome * 1 - InvestmentExpense * 1;
                $("#FinanceIncomeAndExpensesDiv #InvestmentDeposit01").text(InvestmentDeposit01.toMyFixed(2));
                //7.	储蓄合计=工作储蓄+理财储蓄
                var TotalDeposit = WolkDeposit01 * 1 + InvestmentDeposit01 * 1;
                $("#FinanceIncomeAndExpensesDiv #TotalDeposit").text(TotalDeposit.toMyFixed(2));
                //9.	自由储蓄=储蓄合计－∑（养老保险储蓄、住房公积金储蓄）
                var FreeMoney = TotalDeposit * 1 - (EndowmentInsurance * 1 + HousingFund * 1);
                $("#FinanceIncomeAndExpensesDiv #FreeMoney").text(FreeMoney.toMyFixed(2))
                //获取原值
                SaveDefaultValueCommon("FinanceIncomeAndExpensesDiv");
            } else {
                $("#FinanceIncomeAndExpensesDiv").hide();
            }
        }
    });
}

//获取得到现金流量数据--------------预览加载
function GetCashFlowList() {

    var ProposalId = $.getUrlParam("ProposalId");
    $.ajax({
        url: "/CompetitionUser/CashFlow/GetCashFlowList",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: ProposalId
        },
        success: function (datas) {
            var Redemption = 0;
            var Investment = 0;
            var BorrowCapital = 0;
            var RepaymentCapital = 0;
            var li = datas.list; //收支储蓄
            var li2 = datas.list2;//现金流量
            //  if (li.JudgeVal==true){}
            if (li2 == null) {
                $("#CashFlow").hide();
            }

                if (li2 != null) {
                    var list2 = li2;
                    $("#Redemption").text(list2.Redemption.toMyFixed(2));
                    $("#Investment").text(list2.Investment.toMyFixed(2));
                    $("#BorrowCapital").text(list2.BorrowCapital.toMyFixed(2));
                    $("#RepaymentCapital").text(list2.RepaymentCapital.toMyFixed(2));
                    if (li2.JudgeVal == false) {
                        $("#WorkIncome").text(li2.WorkIncome.toMyFixed(2));                        //工作收入
                        $("#LiveExpense").text(li2.LiveExpense.toMyFixed(2));                      //生活支出
                        $("#InvestIncome").text(li2.InvestIncome.toMyFixed(2));                    //投资收益
                        $("#InsuranceExpenseFlow").text(li2.InsuranceExpense.toMyFixed(2));        //保费支出
                        $("#InterestExpenseFlow").text(li2.InterestExpense.toMyFixed(2));          //利息支出
                    }

                    var InsuranceExpenses = 0;
                    var investMoneys = 0;
                    if (li != null) {
                        InsuranceExpenses = li.InsuranceExpense;
                        //投资收益（利息收入,资本利得,其他理财收入）
                        var Interest = li.Interest;
                        var CapitalGains = li.CapitalGains;
                        var OtherIncome = li.OtherIncome;

                        investMoneys = Interest + CapitalGains + OtherIncome;
                    }
                    $("#investMoney").text(investMoneys.toMyFixed(2));
                    $("#InsuranceExpenses2").text(InsuranceExpenses.toMyFixed(2));
                    //生活现金流量
                    var WorkIncome = $.trim($("#WorkIncome").text()) * 1;
                    var LiveExpense = $.trim($("#LiveExpense").text()) * 1;
                    var Num = WorkIncome - LiveExpense;
                    $("#lifeMoney").text((Num).toMyFixed(2));

                    //投资现金流量
                    var InvestIncome = $.trim($("#InvestIncome").text()) * 1;
                    Redemption = $.trim($("#Redemption").text()) * 1;
                    Investment = $.trim($("#Investment").text()) * 1;
                    var Num2 = InvestIncome + Redemption - Investment;
                    $("#investMoney").text((Num2).toMyFixed(2));

                    //借贷现金流量
                    BorrowCapital = $.trim($("#BorrowCapital").text()) * 1;
                    var InterestExpense = $.trim($("#InterestExpenseFlow").text()) * 1;
                    RepaymentCapital = $.trim($("#RepaymentCapital").text()) * 1;
                    var Num3 = BorrowCapital - InterestExpense - RepaymentCapital;
                    $("#borrowMoney").text((Num3).toMyFixed(2));

                    //保障现金流量失去焦点时计算               
                    var InsuranceExpenseFlow = li2.InsuranceExpense * 1;
                    $("#InsuranceExpense2").text(-InsuranceExpenseFlow.toMyFixed(2))

                    //加载本期现金及现金等价物净增加额
                    loadings();
                }


                var InsuranceExpenseFlow = 0;
                var investMoney = 0;

                if (li != null) {
                    data = li;
                    //工作收入（薪资收入,养老保险储蓄,医疗保险储蓄,住房公积金储蓄,其他工作收入）
                    var JobIncome = data.JobIncome;
                    var endowmentInsurance = data.EndowmentInsurance;
                    var MedicalInsurance = data.MedicalInsurance;
                    var HousingFund = data.HousingFund;
                    var OtherJobIncome = data.OtherJobIncome;

                    var WorkIncome = JobIncome + endowmentInsurance + MedicalInsurance + HousingFund + OtherJobIncome;
                    //生活支出（家计支出,子女教育支出,其他支出）
                    var FamilyExpense = data.FamilyExpense;
                    var ChildExpense = data.ChildExpense;
                    var OtherExpense = data.OtherExpense;

                    var LiveExpense = FamilyExpense + ChildExpense + OtherExpense;
                    //投资收益（利息收入,资本利得,其他理财收入）
                    var Interest = data.Interest;
                    var CapitalGains = data.CapitalGains;
                    var OtherIncome = data.OtherIncome;

                    var InvestIncome = Interest + CapitalGains + OtherIncome;
                    //利息支出（利息支出）
                    var InterestExpense = data.InterestExpense;
                    //保费支出（保障型保费支出）//保障现金流量净额
                    InsuranceExpenseFlow = data.InsuranceExpense;

                    //生活现金流量净额：工作收入-生活支出
                    var lifeMoney = WorkIncome - LiveExpense;

                    //投资现金流量净额：投资收益+投资赎回-新增投资
                    investMoney = InvestIncome + Redemption - Investment;

                    //借贷现金流量净额: 借入本金-利息支出-还款本金
                    var borrowMoney = BorrowCapital - InterestExpense - RepaymentCapital;

                    //本期现金及现金等价物净增加额: ∑（生活现金流量净额，投资现金流量净额，借贷现金流量净额，保障现金流量净额）
                    var Money = lifeMoney + investMoney + borrowMoney - InsuranceExpenseFlow;
                    //if ( li2.JudgeVal == true) {
                    $("#WorkIncome").text(WorkIncome.toMyFixed(2));
                    $("#LiveExpense").text(LiveExpense.toMyFixed(2));
                    $("#InvestIncome").text(InvestIncome.toMyFixed(2));
                    $("#InsuranceExpenseFlow").text((InsuranceExpenseFlow.toMyFixed(2)));
                    // $("#InterestExpense").text(InterestExpense.toMyFixed(2));
                    $("#InterestExpenseFlow").text(InterestExpense.toMyFixed(2));
                    $("#InsuranceExpenses2").text((InsuranceExpenseFlow.toMyFixed(2)));
                    $("#lifeMoney").text(lifeMoney.toMyFixed(2));
                    $("#borrowMoney").text(borrowMoney.toMyFixed(2));
                    $("#Money").text(Money.toMyFixed(2));

                    $("#investMoney").text(investMoney.toMyFixed(2));
                }
           


        }
    });
}

//获取得到现金比率分析---------------预览加载
function GetFinancialRatiosList(ProposalId) {

    $.ajax({
        url: "/CompetitionUser/FinancialRatios/GetFRList",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: ProposalId
        },
        success: function (data) {
            if (data.list3 != null) {
                var li = data.list;
                if (li != null) {
                    NumCalc++;
                    var list = li;
                    //负债比率（负债合计/资产合计）           
                    var bearScale = list.TotalLoan / list.TotalAssets;
                    //融资比率（投资负债小计/投资资产小计）
                    var a = list.RMBFixedDeposit;
                    var b = list.ForeignCurrencyFixedDeposit;
                    var c = list.StockInvestment;
                    var d = list.BondInvestment;
                    var e = list.FundInvestment;
                    var f = list.IndustryInvestment;
                    var g = list.EstateInvestment;
                    var h = list.PolicyInvestment;
                    var i = list.OtherInvestment;
                    var jinancingScale = (list.FinancialLoan + list.IndustryInvestmentLoan + list.EstateInvestmentLoan + list.OtherInvestmentLoan) / (a + b + c + d + e + f + g + h + i)
                    //投资性资产权数（投资资产小计/资产合计）
                    var invest = (a + b + c + d + e + f + g + h + i) / list.TotalAssets;
                    //流动性资产权数（流动资产小计/资产合计）
                    //流动性资产权数（流动资产小计/资产合计）
                    var flowMoney = (list.Cash + list.RMBDeposit + list.OtherAsset) / list.TotalAssets;

                    if ((a + b + c + d + e + f + g + h + i) == 0) {
                        $("#jinancingScale").text("无法统计该指标");
                    } else {
                        $("#jinancingScale").text(jinancingScale.toMyFixed(2) * 1000 / 10);
                    }

                    if (list.TotalAssets == 0) {
                        $("#invest").text("无法统计该指标");
                        $("#flowMoney").text("无法统计该指标");
                        $("#bearScale").text("无法统计该指标");
                    } else {
                        $("#invest").text(invest.toMyFixed(2) * 1000 / 10);
                        $("#flowMoney").text(flowMoney.toMyFixed(2) * 1000 / 10);
                        $("#bearScale").text(bearScale.toMyFixed(2) * 1000 / 10);
                    }
                }
                var li2 = data.list2;
                if (li2 != null) {
                    var list2 = li2;

                    //支出比率（生活支出小计+理财支出小计）/（工作收入小计+理财收入小计）
                    var licai = list2.InterestExpense + list2.InsuranceExpense + list2.OtherFinanceExpense;
                    var liftpay = (list2.FamilyExpense + list2.ChildExpense + list2.OtherExpense + licai);
                    var work = (list2.JobIncome + list2.EndowmentInsurance + list2.MedicalInsurance + list2.HousingFund + list2.Interest + list2.CapitalGains + list2.OtherIncome);
                    // var payScale = liftpay / work;
                    var payScale = ((list2.LiveExpense01 + list2.InvestmentExpense01) / (list2.WorkIncome01 + list2.InvestmentIncome01)).toMyFixed(2);
                    //财务负担率：理财支出小计/（工作收入小计+理财收入小计）       
                    //  var finance = licai / work;
                    var finance = (list2.InvestmentExpense01 / (list2.WorkIncome01 + list2.InvestmentIncome01)).toMyFixed(2);
                    //自由储蓄率：自由储蓄/（工作收入小计+理财收入小计）
                    //	工作储蓄=工作收入－生活支出
                    var workExist = (list2.JobIncome + list2.EndowmentInsurance + list2.MedicalInsurance + list2.HousingFund + list2.Interest) - (list2.FamilyExpense + list2.ChildExpense + list2.OtherExpense)
                    //// 理财储蓄=理财收入－理财支出
                    var licaiExist = (list2.Interest + list2.CapitalGains + list2.OtherIncome) - (licai)
                    ////自由储蓄
                    var freedom = (workExist + licaiExist) - (list2.EndowmentInsurance + list2.HousingFund);
                    //自由储蓄率 ：自由储蓄/（工作收入小计+理财收入小计）
                    // var FreedomScale = freedom / (work);
                    var FreedomScale = (list2.FreeMoney / (list2.WorkIncome01 + list2.InvestmentIncome01)).toMyFixed(2);
                    // var FreedomScale = Division(list2.FreeMoney/)

                    if (work == 0) {
                        $("#payScale").text("无法统计该指标");
                        $("#finance").text("无法统计该指标");
                        $("#FreedomScale").text("无法统计该指标");
                    } else {
                        $("#payScale").text(payScale * 1000 / 10);

                        $("#finance").text((finance) * 1000 / 10);
                        $("#FreedomScale").text((FreedomScale) * 1000 / 10);
                    }
                }

                if (li != null && li2 != null) {
                    //净资产增长率（致富公式）:(工作储蓄+理财储蓄)/(资产合计-负债合计)
                    var a = (workExist + licaiExist);
                    var b = (list.TotalAssets - list.TotalLoan);
                    if (b == 0) {
                        $("#addScale").text(0);
                    } else {
                        var addScale = a / b;
                        $("#addScale").text(addScale.toMyFixed(2) * 1000 / 10);
                    }

                }

                var li3 = data.list3;
                if (li3 != null) {
                    var n = li3;
                    var LiabilityAnalysis = n.LiabilityAnalysis == null ? "" : n.LiabilityAnalysis
                    var IncomeAndExpensesAnalysis = n.IncomeAndExpensesAnalysis == null ? "" : n.IncomeAndExpensesAnalysis
                    //资产负债结构分析
                    $("#LiabilityAnalysis").text(LiabilityAnalysis);
                    //收支储蓄结构分析
                    $("#IncomeAndExpensesAnalysis").text(IncomeAndExpensesAnalysis);
                    //客户财务情况分析
                    $("#AnalysisRate").text(n.Analysis);
                }
            } else {
                $("#FinancialRatios").hide();
            }
        }
    });

}


//获取现金规划页面------------------预览加载
function GetCashPlanByProposalId(proposalId) {
    $.ajax({
        url: "/CompetitionUser/CashPlan/GetCashPlanByProposalId",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            proposalId: proposalId
        },
        success: function (data) {
            if (data.Analysis != null) {
                NumCalc++;
                var Id = data.Id;
                var FamilyMonthExpense = data.FamilyMonthExpense;
                var RetainCashType = data.RetainCashType;
                var Deposit = data.Deposit;
                var Fund = data.Fund;
                var CreditCard = data.CreditCard;
                var Analysis = data.Analysis;
                var ProposalId = data.ProposalId;
           //     $("#ProposalId").text(ProposalId);//赋值建议书ID
                $("#CashPlanId").text(Id);
                $("#FamilyMonthExpense").attr("Family", FamilyMonthExpense).text(FamilyMonthExpense);
                //$("#RetainCashType").find("option[value='" + RetainCashType + "']").attr("selected", true);
                var CashRate = EnumConvert.CashConvert(RetainCashType);
                $("#RetainCashType").text(CashRate);
                var retainCashVal = clacCashPanVal.calcRetainCashType(RetainCashType);
                $("#RetainCashMultiple").text(retainCashVal);
                $("#Deposit").text(Deposit);
                $("#Fund").text(Fund);
                $("#CreditCardPlan").text(CreditCard);
                $("#Analysis").text(Analysis);
            } else {
                $("#FinanceCashPlanDiv").hide();
            }
        }
    });
}
//获取教育规划-----------------------预览加载
function GetLifeEducationPlan(proposalId) {
    $.ajax({
        url: "/CompetitionUser/LifeEducationPlan/GetLifeEducationPlanList",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: proposalId
        },
        success: function (datas) {
            if (datas != null) {
                //教育规划信息
                //var data =new Object(datas)
                var li = datas.list;
                var LifeEducationPlanDetailList = null;
                if (li != null) {
                    NumCalc++;
                    var LifeEducationPlanDetailList = li.LifeEducationPlanDetailList;
                    var n = li;
                    $("#ChildAge").text(n.ChildAge);//子女年龄
                    $("#InlandEduFee").text(n.InlandEduFee);//国内学费增长率
                    $("#ForeignEduFee").text(n.ForeignEduFee);//国外学费增长率
                    $("#Insurance").text(n.Insurance.toMyFixed(2));//商业保险
                    $("#DepositEducation").text(n.Deposit.toMyFixed(2));//储蓄计划
                    $("#Other").text(n.Other.toMyFixed(2));//其他安排
                    $("#EduTotalAmount").text(n.EduTotalAmount.toMyFixed(2));//上学前需准备的现金总金额
                    $("#ReturnOnInvestment").text(n.ReturnOnInvestment.toMyFixed(2));//预计投资收益率
                    $("#DisposableInput").text(n.DisposableInput.toMyFixed(2));//一次性投入金额
                    $("#MonthlyInvestment").text(n.MonthlyInvestment.toMyFixed(2));//每月定期投资金额
                    $("#RegularYear").text(n.RegularYear);//定期定额投资年限
                    $("#TargetAmount").text(n.TargetAmount.toMyFixed(2));//此方案能实现的目标金额
                    $("#AnalysisLife").text(n.Analysis);//教育规划分析
                    //小计
                    var xiao = n.Insurance + n.Deposit + n.Other;
                    $("#xiaoji").text(xiao.toMyFixed(2));
                } else {
                    $("#LifeEducationPlan").hide();
                }

                //教育规划详细信息

                if (LifeEducationPlanDetailList != null) {
                    var num = 0;
                    var obj = new Object(LifeEducationPlanDetailList);
                    $.each(obj, function (i, n) {
                        //先生成后赋值
                        num = i + 1;
                        $("#EducationList").remove();
                        AddHTML(num);
                        var educationLife = EnumConvert.LifeConvet(n.EduStage);
                        $("#EducationList" + num).find("span[field='EduStage']").text(educationLife);//教育阶段
                        $("#EducationList" + num).find("span[field='EduAge']").text(n.EduAge);//求学年龄
                        $("#EducationList" + num).find("span[field='EduTime']").text(n.EduTime);//求学时间
                        $("#EducationList" + num).find("span[field='Tuition']").text(n.Tuition.toMyFixed(2));//目前学费
                        $("#EducationList" + num).find("span[field='EduTuition']").text(n.EduTuition.toMyFixed(2));//上学时学费
                        $("#EducationList" + num).find("span[field='TotalTuition']").text(n.TotalTuition.toMyFixed(2));//上学前需准备的总学费  
                        $("#EducationList" + num).find("input[field='ID']").text(n.Id);
                    });

                    //var lifthtml ="";
                    //if (obj.length > 4) {
                    // lifthtml  = "<div id=\"LifeEducationPlanPark1\" style=\"background-color:white\">"
                    // $("#LifeEducationPlanDW").after(lifthtml);
                    // lifthtml = "</div>";
                    // $("#EducationList4").prev().after(lifthtml);
                    //}


                }
                //每月可支配资金
                //可用资产
            } else {
                $("#LifeEducationPlan").hide();
            }
        }
    });
}

//获取消费规划相关信息--------------预览加载
function GetConsumptionPlan(proposalId) {

    $.ajax({
        url: "/CompetitionUser/ConsumptionPlan/GetConsumptionPlanObj",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: proposalId
        },
        success: function (datas) {
            if (datas != null) {
                //教育规划信息
                var li = datas.list;
                if (li != null) {
                    var n = li;
                    //购房
                    if (n.ShopHouseYear != 0) {
                        NumCalc++;
                        $("#ShopHouseYear").text(n.ShopHouseYear);//年限
                        $("#HouseArea").text(n.HouseArea==null?"":n.HouseArea);//面积
                        $("#HousePrice").text(n.HousePrice==null?"":n.HousePrice);//单价
                        $("#HouseDownPaymentPercent").text(n.HouseDownPaymentPercent);//首付比例
                        $("#HouseLoanYear").text(n.HouseLoanYear);//贷款年限
                        $("#HouseLoanRate").text(n.HouseLoanRate);//贷款利率
                        $("#HouseDownPayment").text(n.HouseDownPayment);//首付款
                        $("#HouseTotalAmount").text(n.HouseTotalAmount);//购房总花费
                        $("#HouseMonthlyAmount").text(n.HouseMonthlyAmount);//购房月供

                        var HouseArea = $.trim($("#HouseArea").html()) * 1;
                        var HousePrice = $.trim($("#HousePrice").html()) * 1;
                        var Num2 =(HouseArea * HousePrice).toMyFixed(2);
                        //总金额
                        $("#HouseAllMoney").text(n.HouseAllMoney==null?(Num2):n.HouseAllMoney);
                    } else {
                        $("#TitleShopHouse").hide(0);
                        $("#ShopHouseDiv").hide(0);
                        $("#Add").show(0);
                    }

                    //购车
                    if (n.ShopCarYear != 0) {
                        $("#ShopCarYear").text(n.ShopCarYear);//年限
                       // $("#CarType").text(n.CarType);//车款型号

                        if (n.CarType != null) {
                            $("#CarType").text(n.CarType);//车款型号
                        } else {
                            $("#CarType").text("");//车款型号
                        }

                        $("#CarPrice").text(n.CarPrice);//裸车价格
                        $("#CarDownPaymentPercent").text(n.CarDownPaymentPercent);//首付比例

                        $("#CarLoanYear").text(n.CarLoanYear);//贷款期限

                        $("#CarDownPaymentPercent").attr("disabled", false);

                        $("#CarLoanRate").text(n.CarLoanRate);//贷款利率
                        $("#PurchaseTax").text(n.PurchaseTax);//购置税
                        $("#CarRegFee").text(n.CarRegFee);//上牌费用
                        // $("#Displacement").text(n.Displacement);//汽车排量
                        $("#VehicleAndVesselTax").text(n.VehicleAndVesselTax);//车船使用税
                        $("#Selcts").text(n.VehicleAndVesselTax);
                        $("#MotorVehicleCompulsory").text(n.MotorVehicleCompulsory);//交强险
                        $("#Selects2").text(n.MotorVehicleCompulsory);
                        $("#MotorVehicleCommercial").text(n.MotorVehicleCommercial);//商业保险
                        $("#CarDownPayment").text(n.CarDownPayment);//首付款
                        $("#CarTotalAmount").text(n.CarTotalAmount);//购车总花费
                        $("#CarMonthlyAmount").text(n.CarMonthlyAmount);//购车月供
                    } else {
                        $("#TitleShopCar").hide();
                        $("#ShopCarDiv").hide();
                        $("#Add").show();
                    }

                    $("#AnalysisConsumption").text(n.Analysis);//消费规划分析

                    $("#FirstAmount").text(n.FirstAmount);//首付款准备的现金总金额
                    $("#ReturnOnInvestmentConsumption").text(n.ReturnOnInvestment);//预计投资收益率
                    $("#DisposableInputConsumption").text(n.DisposableInput);//一次性投入金额
                    $("#MonthlyInvestmentConsumption").text(n.MonthlyInvestment);//每月定期投资金额
                    $("#RegularYearConsumption").text(n.RegularYear);//定期定额投资年限
                    $("#TargetAmountConsumption").text(n.TargetAmount);//此方案能实现的目标金额

                    //隐藏域Id
                    $("#Id").text(n.Id);

                } else {
                    $("#ConsumptionPlan").hide();
                }
            }
        }
    });
}

//获取创业规划相关信息---------------预览加载
function GetStartAnUndertakingPlanList(proposalId) {
    $.ajax({
        url: "/CompetitionUser/StartAnUndertakingPlan/GetSUPList",
        type: "POST",
        async: false,
        dataType: "json",
        data: {
            ProposalId: proposalId
        },
        success: function (data) {
            if (data != null) {
                //创业规划信息
                var li = data.list;
                if (li != null) {
                    NumCalc++;
                    $("#AgeUndertaking").text(li.Age);//当前年龄

                    $("#StartPlanAge").text(li.StartPlanAge);//计划创业年龄
                    $("#CostInput").text(li.CostInput.toMyFixed(2));//创业时一次性投入
                    $("#DistanceYear").text(li.DistanceYear);//离创业年限
                    $("#ReturnOnInvestmentRateUndertaking").text(li.ReturnOnInvestmentRate);//预计投资收益率
                    $("#DisposableInputUndertaking").text(li.DisposableInput.toMyFixed(2));//一次性投入金额
                    $("#MonthlyInvestmentUndertaking").text(li.MonthlyInvestment.toMyFixed(2));//每月定期投资金额
                    $("#RegularYearUndertaking").text(li.RegularYear);//定期定额投资年限
                    $("#TargetAmountUndertaking").text(li.TargetAmount.toMyFixed(2));//此方案能实现的目标金额
                    $("#AnalysisUndertaking").text(li.Analysis);//创业规划分析
                    $("#Id").text(li.Id)
                } else {
                    $("#StartAnUndertakingPlan").hide();
                }
                //客户信息
                var li2 = data.list2;
                if (li2 != null) {
                    var n = li2;
                    $("#Age").text(n.Age);//当前年龄                
                }
            }
        }
    });
}

//获取退休规划---------------------预览加载
function LoadRetirementPlan(proposalId) {
    $.ajax({
        url: "/CompetitionUser/RetirementPlan/GetRetirementPlanByProposalId",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: proposalId
        },
        success: function (data) {
            if (data != null) {
                if (data.Analysis != null) {
                    NumCalc++;
                    var Id = data.Id;
                    var Age = data.Age;
                    var BeforeInflationRate = data.BeforeInflationRate;
                    var AfterInflationRate = data.AfterInflationRate;
                    var RetirementRate = data.RetirementRate;
                    var SociaSecurityRate = data.SociaSecurityRate;
                    var RentRate = data.RentRate;
                    var OtherRate = data.OtherRate;
                    var SocialInsurance = data.SocialInsurance;
                    var RetirementAge = data.RetirementAge;
                    var RetirementYears = data.RetirementYears;
                    var LivingStandardNow = data.LivingStandardNow;
                    var Satisfaction = data.Satisfaction;
                    var SatisfactionLivingStandard = data.SatisfactionLivingStandard;
                    var ConvertProportion = data.ConvertProportion;
                    var lineageFee = data.lineageFee;
                    var CommercialInsurance = data.CommercialInsurance;
                    var RentIncome = data.RentIncome;
                    var RetirementLivingStandard = data.RetirementLivingStandard;
                    var AfterLivingStandard = data.AfterLivingStandard;
                    var OtherIncome = data.OtherIncome;
                    var TotalIncome = data.TotalIncome;
                    var TotalAmount = data.TotalAmount;
                    var ReturnOnInvestmentRate = data.ReturnOnInvestmentRate;
                    var MonthlyInvestment = data.MonthlyInvestment;
                    var DisposableInput = data.DisposableInput;
                    var RegularYear = data.RegularYear;
                    var TargetAmount = data.TargetAmount;
                    var Analysis = data.Analysis;
                    //每月可支配资金
                    var MonthMoney = data.MonthMoney;
                    //可用资产
                    var UserableAsset = data.UserableAsset;
                    $("#LiveRetirementPlanDiv #AgeRetirement").text(Age);
                    $("#LiveRetirementPlanDiv #BeforeInflationRate").text(BeforeInflationRate);
                    $("#LiveRetirementPlanDiv #AfterInflationRate").text(AfterInflationRate);

                    $("#LiveRetirementPlanDiv #RetirementRate").text(RetirementRate);

                    if (SociaSecurityRate == null) {
                        $("#LiveRetirementPlanDiv #SociaSecurityRate").text("");
                    } else {
                        $("#LiveRetirementPlanDiv #SociaSecurityRate").text(SociaSecurityRate);
                    }
                    if (RentRate == null) {
                        $("#LiveRetirementPlanDiv #RentRate").text("");
                    } else {
                        $("#LiveRetirementPlanDiv #RentRate").text(RentRate);
                    }
                    if (OtherRate == null) {
                        $("#LiveRetirementPlanDiv #OtherRate").text("");
                    } else {
                        $("#LiveRetirementPlanDiv #OtherRate").text(OtherRate);
                    }


                    $("#LiveRetirementPlanDiv #RetirementAge").text(RetirementAge);
                    $("#LiveRetirementPlanDiv #RetirementYears").text(RetirementYears);
                    $("#LiveRetirementPlanDiv #LivingStandardNow").text(LivingStandardNow);
                    $("#LiveRetirementPlanDiv #Satisfaction").text(Satisfaction);
                    $("#LiveRetirementPlanDiv #SatisfactionLivingStandard").text(SatisfactionLivingStandard);
                    $("#LiveRetirementPlanDiv #ConvertProportion").text(ConvertProportion);
                    $("#LiveRetirementPlanDiv #lineageFee").text(lineageFee);
                    $("#LiveRetirementPlanDiv #SocialInsurance").text(SocialInsurance);
                    $("#LiveRetirementPlanDiv #CommercialInsurance").text(CommercialInsurance);
                    $("#LiveRetirementPlanDiv #RentIncome").text(RentIncome);
                    $("#LiveRetirementPlanDiv #RetirementLivingStandard").text(RetirementLivingStandard);
                    $("#LiveRetirementPlanDiv #AfterLivingStandard").text(AfterLivingStandard);
                    $("#LiveRetirementPlanDiv #OtherIncome").text(OtherIncome);
                    $("#LiveRetirementPlanDiv #TotalIncome").text(TotalIncome);
                    $("#LiveRetirementPlanDiv #TotalAmountRetirement").text(TotalAmount);
                    $("#LiveRetirementPlanDiv #ReturnOnInvestmentRateRetirement").text(ReturnOnInvestmentRate);
                    $("#LiveRetirementPlanDiv #DisposableInputRetirement").text(DisposableInput);
                    $("#LiveRetirementPlanDiv #MonthlyInvestmentRetirement").text(MonthlyInvestment);
                    $("#LiveRetirementPlanDiv #RegularYearRetirement").text(RegularYear);
                    $("#LiveRetirementPlanDiv #TargetAmountRetirement").text(TargetAmount);
                    $("#LiveRetirementPlanDiv #AnalysisRetirement").text(Analysis);
                    $("#LiveRetirementPlanDiv #monthMoneyRetirement").text(MonthMoney.toMyFixed(2));
                    $("#LiveRetirementPlanDiv #UserableAssetRetirement").text(UserableAsset.toMyFixed(2))
                } else {
                    $("#LiveRetirementPlanDiv").hide();
                };
            }
        }
    });
};

//获取保险规划--------------------预览加载
function LoadInsurancePlan(proposalId) {
    $.ajax({
        url: "/CompetitionUser/InsurancePlan/LoadInsurancePlanByProposalId",
        type: "POST",
        async: false,
        dataType: "json",
        data: {
            ProposalId: proposalId,
            rId: Math.random
        },
        success: function (data) {
            if (data != null) {
                if (data.Analysis != null) {
                    NumCalc++;
                    var Id = data.Id;
                    var ProposalId = data.ProposalId
                    var MethodTypeId = data.MethodTypeId;
                    if (MethodTypeId == 1) {
                        $("#FinanceInsurancePlanDiv #InsuranceOne").css("display", "block");
                        $("#FinanceInsurancePlanDiv #InsuranceTwo").css("display", "none");
                    } else {
                        $("#FinanceInsurancePlanDiv #InsuranceOne").hide()//.css("display","none");
                        $("#FinanceInsurancePlanDiv #InsuranceTwo").show()//.css("display","block");
                    }
                    var InsureName = data.InsureName;//被保险人名
                    var SpouseName = data.SpouseName;//客户名
                    var Age = data.Age;
                    var MonthMoney = data.MonthMoney;//每月可支配金额
                    var UserableAsset = data.UserableAsset//可用资产
                    var Age1 = data.Age1;
                    var Age2 = data.Age2
                    var RetirementAge1 = data.RetirementAge1
                    var RetirementAge2 = data.RetirementAge2
                    var ReturnOnInvestment1 = data.ReturnOnInvestment1
                    var ReturnOnInvestment2 = data.ReturnOnInvestment2
                    var InflationRate1 = data.InflationRate1
                    var InflationRate2 = data.InflationRate2
                    var RevenueGrowth1 = data.RevenueGrowth1
                    var RevenueGrowth2 = data.RevenueGrowth2
                    var FamilyExpensesPay1 = data.FamilyExpensesPay1
                    var FamilyExpensesPay2 = data.FamilyExpensesPay2
                    var FamilyIncomePay1 = data.FamilyIncomePay1
                    var FamilyIncomePay2 = data.FamilyIncomePay2
                    var SpouseAge1 = data.SpouseAge1
                    var SpouseAge2 = data.SpouseAge2
                    var FamilyFutureSaving1 = data.FamilyFutureSaving1
                    var FamilyFutureSaving2 = data.FamilyFutureSaving2
                    var MatrimonialFee1 = data.MatrimonialFee1
                    var MatrimonialFee2 = data.MatrimonialFee2
                    var AfterAccidentRate1 = data.AfterAccidentRate1
                    var AfterAccidentRate2 = data.AfterAccidentRate2
                    var AdjustMatrimonialFee1 = data.AdjustMatrimonialFee1
                    var AdjustMatrimonialFee2 = data.AdjustMatrimonialFee2
                    var MatrimonialFeeNow1 = data.MatrimonialFeeNow1
                    var MatrimonialFeeNow2 = data.MatrimonialFeeNow2
                    var Income1 = data.Income1
                    var Income2 = data.Income2
                    var SpouseMonthIncome1 = data.SpouseMonthIncome1
                    var SpouseMonthIncome2 = data.SpouseMonthIncome2
                    var FamilyLiveOverdraft1 = data.FamilyLiveOverdraft1
                    var FamilyLiveOverdraft2 = data.FamilyLiveOverdraft2
                    var ReserveFund1 = data.ReserveFund1;
                    var ReserveFund2 = data.ReserveFund2;
                    var EduAmount1 = data.EduAmount1;
                    var EduAmount2 = data.EduAmount2;
                    var PensionFunds1 = data.PensionFunds1;
                    var PensionFunds2 = data.PensionFunds2;
                    var DeathExpense1 = data.DeathExpense1
                    var DeathExpense2 = data.DeathExpense2
                    var LoanBalance1 = data.LoanBalance1
                    var LoanBalance2 = data.LoanBalance2
                    var EarningAssets1 = data.EarningAssets1
                    var EarningAssets2 = data.EarningAssets2
                    var RelativeFinancial1 = data.RelativeFinancial1
                    var RelativeFinancial2 = data.RelativeFinancial2
                    var InsureName1 = data.InsureName1
                    var InsureName2 = data.InsureName2
                    var InsureNeedCash1 = data.InsureNeedCash1
                    var InsureNeedCash2 = data.InsureNeedCash2
                    var InsuranceAmount1 = data.InsuranceAmount1
                    var InsuranceAmount2 = data.InsuranceAmount2
                    var GapCash1 = data.GapCash1
                    var GapCash2 = data.GapCash2
                    var BudgetAmount1 = data.BudgetAmount1
                    var BudgetAmount2 = data.BudgetAmount2
                    var SupplementaryQuota1 = data.SupplementaryQuota1
                    var SupplementaryQuota2 = data.SupplementaryQuota2
                    var BalanceCash1 = data.BalanceCash1
                    var BalanceCash2 = data.BalanceCash2
                    var Analysis = data.Analysis;
                    var Expenditure = data.Expenditure;
                    var FutureExpend = data.FutureExpend;//未来给人支出
                    var PredictRetirementAgeLIfe = data.PredictRetirementAgeLIfe;
                    var FutureIncomeLife = data.FutureIncomeLife;//未来个人收入
                    var FutureAnnuityIncome = data.FutureAnnuityIncome;//个人未来净收入的年金现值/元
                    //加入枚举
                    var methodVal = EnumConvert.InsuraneConvet(MethodTypeId)
                    $("#FinanceInsurancePlanDiv #MethodTypeId").text(methodVal);
                    $("#FinanceInsurancePlanDiv #TabZH").text(MethodTypeId)
                    //判断保险规划的需求算法1-遗属需求法 ，2-生命需求法
                    $("#FinanceInsurancePlanDiv #monthMoneyInsurance").text(MonthMoney.toMyFixed(2));//每月可用资金
                    $("#FinanceInsurancePlanDiv #UserableAssetInsurance").text(UserableAsset.toMyFixed(2));//可用资产
                    $("#FinanceInsurancePlanDiv #AnalysisInsuranceTwo").text(Analysis);//--客户财务情况分析
                    if (MethodTypeId == 1) {

                        $("#FinanceInsurancePlanDiv #InsureName").text(InsureName);
                        $("#FinanceInsurancePlanDiv #InsureName1Life").text(InsureName);
                        $("#FinanceInsurancePlanDiv #SpouseName").text(SpouseName);
                        //这地方特殊两边通用
                        $("#FinanceInsurancePlanDiv #Age1").text(Age);
                        $("#FinanceInsurancePlanDiv #SpouseAge1").text(Age2); //女的写男的
                        $("#FinanceInsurancePlanDiv #SpouseAge2").text(Age);  //---------配偶当前年龄-男的写女的
                        $("#FinanceInsurancePlanDiv #Age1Life").text(Age);//被保险人年龄/岁


                        $("#FinanceInsurancePlanDiv #Age2").text(Age2)
                        $("#FinanceInsurancePlanDiv #RetirementAge1").text(RetirementAge1);
                        $("#FinanceInsurancePlanDiv #RetirementAge2").text(RetirementAge2);
                        $("#FinanceInsurancePlanDiv #ReturnOnInvestment1").text(ReturnOnInvestment1);
                        $("#FinanceInsurancePlanDiv #ReturnOnInvestment2").text(ReturnOnInvestment2);//----人民币固定存款
                        $("#FinanceInsurancePlanDiv #InflationRate1").text(InflationRate1);
                        $("#FinanceInsurancePlanDiv #InflationRate2").text(InflationRate2);
                        $("#FinanceInsurancePlanDiv #RevenueGrowth1").text(RevenueGrowth1);
                        $("#FinanceInsurancePlanDiv #RevenueGrowth2").text(RevenueGrowth2);
                        $("#FinanceInsurancePlanDiv #FamilyExpensesPay1").text(FamilyExpensesPay1.toMyFixed(2));//家庭生活费用实质报酬率
                        $("#FinanceInsurancePlanDiv #FamilyExpensesPay2").text(FamilyExpensesPay2.toMyFixed(2));
                        $("#FinanceInsurancePlanDiv #FamilyIncomePay1").text(FamilyIncomePay1.toMyFixed(2));
                        $("#FinanceInsurancePlanDiv #FamilyIncomePay2").text(FamilyIncomePay2.toMyFixed(2));

                     // 
                        $("#FinanceInsurancePlanDiv #FamilyFutureSaving1").text(FamilyFutureSaving1);//家庭未来生活费准备年数/年
                        $("#FinanceInsurancePlanDiv #FamilyFutureSaving2").text(FamilyFutureSaving2);
                        $("#FinanceInsurancePlanDiv #MatrimonialFee1").text(MatrimonialFee1.toMyFixed(2));//-------当前的家庭生活费用/元
                        $("#FinanceInsurancePlanDiv #MatrimonialFee2").text(MatrimonialFee2.toMyFixed(2));
                        $("#FinanceInsurancePlanDiv #AfterAccidentRate1").text(AfterAccidentRate1);//-----保险事故发生后支出调整率
                        $("#FinanceInsurancePlanDiv #AfterAccidentRate2").text(AfterAccidentRate2);
                        $("#FinanceInsurancePlanDiv #AdjustMatrimonialFee1").text(AdjustMatrimonialFee1.toMyFixed(2));//调整后家庭年生活费用/元
                        $("#FinanceInsurancePlanDiv #AdjustMatrimonialFee2").text(AdjustMatrimonialFee2.toMyFixed(2));
                        $("#FinanceInsurancePlanDiv #MatrimonialFeeNow1").text(MatrimonialFeeNow1.toMyFixed(2));//------家庭生活费用现值/元
                        $("#FinanceInsurancePlanDiv #MatrimonialFeeNow2").text(MatrimonialFeeNow2.toMyFixed(2));
                        $("#FinanceInsurancePlanDiv #Income1").text(Income1.toMyFixed(2));//配偶的个人年收入/元
                        $("#FinanceInsurancePlanDiv #Income2").text(Income2.toMyFixed(2));
                        $("#FinanceInsurancePlanDiv #SpouseMonthIncome1").text(SpouseMonthIncome1.toMyFixed(2));//配偶的个人收入现值/元
                        $("#FinanceInsurancePlanDiv #SpouseMonthIncome2").text(SpouseMonthIncome2.toMyFixed(2));
                        $("#FinanceInsurancePlanDiv #FamilyLiveOverdraft1").text(FamilyLiveOverdraft1.toMyFixed(2))//家庭未来生活费用缺口现值/元
                        $("#FinanceInsurancePlanDiv #FamilyLiveOverdraft2").text(FamilyLiveOverdraft2.toMyFixed(2));
                        $("#FinanceInsurancePlanDiv #ReserveFund1").text(ReserveFund1.toMyFixed(2));//紧急备用金现值/元
                        $("#FinanceInsurancePlanDiv #ReserveFund2").text(ReserveFund2.toMyFixed(2));
                        $("#FinanceInsurancePlanDiv #EduAmount1").text(EduAmount1.toMyFixed(2));//--教育金现值/元
                        $("#FinanceInsurancePlanDiv #EduAmount2").text(EduAmount2.toMyFixed(2));
                        $("#FinanceInsurancePlanDiv #PensionFunds1").text(PensionFunds1.toMyFixed(2));//养老基金现值/元
                        $("#FinanceInsurancePlanDiv #PensionFunds2").text(PensionFunds2.toMyFixed(2));
                        $("#FinanceInsurancePlanDiv #DeathExpense1").text(DeathExpense1.toMyFixed(2));//临终及丧葬支出现值/元
                        $("#FinanceInsurancePlanDiv #DeathExpense2").text(DeathExpense2.toMyFixed(2));
                        $("#FinanceInsurancePlanDiv #LoanBalance1").text(LoanBalance1.toMyFixed(2));//目前贷款余额/元
                        $("#FinanceInsurancePlanDiv #LoanBalance2").text(LoanBalance2.toMyFixed(2));
                        $("#FinanceInsurancePlanDiv #EarningAssets1").text(EarningAssets1.toMyFixed(2));//家庭生息资产/元
                        $("#FinanceInsurancePlanDiv #EarningAssets2").text(EarningAssets2.toMyFixed(2));
                        $("#FinanceInsurancePlanDiv #RelativeFinancial1").text(RelativeFinancial1.toMyFixed(2));//遗属需求法应有的寿险保额/元
                        $("#FinanceInsurancePlanDiv #RelativeFinancial2").text(RelativeFinancial2.toMyFixed(2));
                        $("#FinanceInsurancePlanDiv #InsureName1").text(InsureName);//-------被保险人
                        $("#FinanceInsurancePlanDiv #InsureName2").text(SpouseName);
                        $("#FinanceInsurancePlanDiv #InsureNeedCash1").text(InsureNeedCash1.toMyFixed(2));//保险需求额度/元	
                        $("#FinanceInsurancePlanDiv #InsureNeedCash2").text(InsureNeedCash2.toMyFixed(2));
                        $("#FinanceInsurancePlanDiv #InsuranceAmount1").text(InsuranceAmount1.toMyFixed(2));//----*已有额度/元
                        $("#FinanceInsurancePlanDiv #InsuranceAmount2").text(InsuranceAmount2.toMyFixed(2));
                        $("#FinanceInsurancePlanDiv #GapCash1").text(GapCash1.toMyFixed(2));//缺口额度/元
                        $("#FinanceInsurancePlanDiv #GapCash2").text(GapCash2.toMyFixed(2));
                        $("#FinanceInsurancePlanDiv #BudgetAmount1").text(BudgetAmount1);//----*预算金额/元
                        $("#FinanceInsurancePlanDiv #BudgetAmount2").text(BudgetAmount2);
                        $("#FinanceInsurancePlanDiv #SupplementaryQuota1").text(SupplementaryQuota1);//*补充额度/元
                        $("#FinanceInsurancePlanDiv #SupplementaryQuota2").text(SupplementaryQuota2);
                        $("#FinanceInsurancePlanDiv #BalanceCash1").text(BalanceCash1.toMyFixed(2));//欠缺额度/元
                        $("#FinanceInsurancePlanDiv #BalanceCash2").text(BalanceCash2.toMyFixed(2));



                        SaveDefaultValueCommon("InsuranceOne");//保存原值。和新值要做一个对比的
                    } else {


                        //年龄共用
                        $("#FinanceInsurancePlanDiv #Age1").text(Age);
                        $("#FinanceInsurancePlanDiv #Age1Life").text(Age);//被保险人年龄/岁
                        $("#FinanceInsurancePlanDiv #SpouseAge2").text(Age);  //---------配偶当前年龄-男的写女的

                        $("#FinanceInsurancePlanDiv #RetirementAge1Life").text(RetirementAge1);//*预计退休年龄/岁
                        $("#FinanceInsurancePlanDiv #PredictRetirementAgeLIfe").text(PredictRetirementAgeLIfe);//离退休年数/年
                        $("#FinanceInsurancePlanDiv #ReturnOnInvestment1Life").text(ReturnOnInvestment1);//*投资报酬率
                        $("#FinanceInsurancePlanDiv #Income1Life").text(Income1);//当前个人年收入/元
                        $("#FinanceInsurancePlanDiv #RevenueGrowth1Life").text(RevenueGrowth1);//收入增长率
                        $("#FinanceInsurancePlanDiv #FutureIncomeLife").text(FutureIncomeLife.toMyFixed(2));//未来工作期间收入现值/元
                        $("#FinanceInsurancePlanDiv #Expenditure").text(Expenditure);//-个人年收入支出
                        $("#FinanceInsurancePlanDiv #InflationRate1Life").text(InflationRate1);//年通货膨胀率
                        $("#FinanceInsurancePlanDiv #FutureExpend").text(FutureExpend.toMyFixed(2));//未来工作期间支出现值/元
                        $("#FinanceInsurancePlanDiv #FutureAnnuityIncome").text(FutureAnnuityIncome.toMyFixed(2));//个人未来净收入的年金现值/元
                        $("#FinanceInsurancePlanDiv #FutureAnnuityIncomeSub").text(FutureAnnuityIncome.toMyFixed(2));//弥补收入应有的寿险保额/元
                        //---------------------被保险人

                        $("#FinanceInsurancePlanDiv #InsureNeedCash1Life").text(FutureAnnuityIncome.toMyFixed(2));//保险需求额度/元
                        $("#FinanceInsurancePlanDiv #InsuranceAmount1Life").text(InsuranceAmount1);//已有额度/元
                        $("#FinanceInsurancePlanDiv #GapCash1Life").text(GapCash1.toMyFixed(2));//缺口额度/元
                        $("#FinanceInsurancePlanDiv #BudgetAmount1Life").text(BudgetAmount1);//预算金额/元

                        $("#FinanceInsurancePlanDiv #SupplementaryQuota1Life").text(SupplementaryQuota1);//-补充额度/元
                        $("#FinanceInsurancePlanDiv #BalanceCash1Life").text(BalanceCash1.toMyFixed(2));//欠缺额度/元

                        $("#FinanceInsurancePlanDiv #InsureName").text(InsureName);//被保险人姓名
                        $("#FinanceInsurancePlanDiv #InsureName1Life").text(InsureName);//被保险人姓名
                        $("#FinanceInsurancePlanDiv #InsureName1").text(InsureName);//-------被保险人

                        SaveDefaultValueCommon("InsuranceTwo");//保存原值。和新值要做一个对比的
                    }
                } else {
                    $("#FinanceInsurancePlanDiv").hide();
                }
            }
        }
    });

}

//*********************************************************
//获取投资规划--------------------预览加载
function LoadInvestmentPlan(proposalId) {

    $.ajax({
        url: "/CompetitionUser/InvestmentPlan/LoadInvestmentPlan",
        type: "POST",
        async: false,
        dataType: "json",
        data: {
            ProposalId: proposalId
        },
        success: function (data) {
            if (data != null) {

                analysisData(data);
            } else {
                $("#InvestmentPlan").hide();
            }
        }

    });
};
//分解data数据
function analysisData(data) {
    NumCalc++;
    var InvestmentPlanList = data.InvestmentPlanProductList;
    var LifeCycleId = data.LifeCycleId;
    var HoldRate = data.HoldRate * 1;
    var IncreaseRate = data.IncreaseRate * 1;
    var SpeculationRate = data.SpeculationRate * 1;
    var Analysis = data.Analysis;
    var Id = data.Id;
  //  $("#InvestmentPlanId").text(Id);
    var LifeCycleId1 = EnumConvert.InsertmentConvert(LifeCycleId);
    $("#LifeCycleId").text(LifeCycleId1);
    $("#HoldRate").text(HoldRate);
    $("#IncreaseRate").text(IncreaseRate);
    $("#SpeculationRate").text(SpeculationRate);
    $("#AnalysisInsertment").text(Analysis);
    var InsureShow = EnumConvert.InsertmentBaseConvet(LifeCycleId);
    $("#InsureShow").text(InsureShow);

    //赋值饼状图
    ShowInsertmentInfo(HoldRate, IncreaseRate, SpeculationRate)

    if (InvestmentPlanList != null) {
        // $("#Add").prev().remove();
        $("#ProductSelect").remove();// 这样也可以
        $(InvestmentPlanList).each(function (index, dom) {
            var fundobj = new Object(dom);
            index = index + 1;
            //应该先删除第一行然后添加
            //添加
            if (index != InvestmentPlanList.length) {
                AddList(index, fundobj);
            } else {
                AddListLast(index, fundobj);
            }
            //f赋值
            eacTransVal(index, fundobj);
            //给常量赋值
            IndexFlag = index;
        });
    }
}

//添加产品选择
function AddList(index, dom) {
    var trHtml = "";
    if (typeof index == "undefined") {
        IndexFlag = IndexFlag + 1;
        index = IndexFlag;
    }

    trHtml += "<div class=\"item b-grayish Tageach\"  id=\"ProductSelect" + index + "\" eacflag=\"ProductSelect\"><div class=\"fif-con  mb10\">";
    trHtml += "<div class=\"fif-box grid-4\"><label class=\"fif-text\">已完成规划：</label> <div class=\"input\">";
    trHtml += "<span id=\"PlanId" + index + " \" class=\"\" eacflag=\"PlanId\"></span> </div></div>";
    trHtml += "<div class=\"fif-box grid-6\"><label class=\"fif-text\"><i class=\"c-red\">*</i>方案所需投资收益率：</label>";
    trHtml += " <div class=\"input\"> <span id=\"PlanRate" + index + "\" class=\"grid-4\" eacflag=\"PlanRate\"></span><span class=\"ml10\"></span></div> </div>";
    trHtml += "<a ></a></div>";
    trHtml += "<div class=\"table\"> <table class=\"mb10\" id=\"ProductSelect" + index + "TabCashFund\" eacflag=\"ProductSelectTabCashFund\" width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
    trHtml += " <tbody> <tr> <td width=\"83\" rowspan=\"2\" align=\"center\"  class=\"c-white\" style=\"background-color: #63b2f4\">保值层</td>";
    trHtml += "<td><span class=\"fl\">银行储蓄选择</span><div class=\"fif-con fr grid-10\"> <div class=\"fif-box grid-12\" style=\"margin-bottom:10px;\">";
    trHtml += " <label class=\"fif-text\">活期：</label>   <div class=\"input grid-11\">";

    //银行选择
    trHtml += "<span id=\"DemandDepositsBank" + index + "\" class=\"\" eacflag=\"DemandDepositsBank\"></span>"

    trHtml += " <span id=\"DemandDepositsBankRate" + index + "\" class=\"\" eacflag=\"DemandDepositsBankRate\"></span>  </div></div>";
    trHtml += "<div class=\"fif-box grid-12\"> <label class=\"fif-text\">定期：</label><div class=\"input grid-11\">";
    //银行选择定期
    trHtml += "<span id=\"TimeDepositBank" + index + "\" class=\"\" eacflag=\"TimeDepositBank\"></span>";
    //银行期限
    trHtml += "<span id=\"TimeDepositBankTime" + index + "\" class=\"\" eacflag=\"TimeDepositBankTime\"></span>";

    trHtml += "<span id=\"TimeDepositBankRate" + index + "\" class=\"\" eacflag=\"TimeDepositBankRate\"></span> </div> </div></div></td> </tr>";
    trHtml += "<tr> <td> <span>基金产品选择</span>  <span class=\"grid-4\">货币市场基金</span> ";
    //货币基金
    if (dom.Fund1 != 0) {
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund1\"> </span> <div class=\"fif-form b-grayish\">  <span class=\"grid-3\" eacflag=\"CashCode\">小龙</span>     <span class=\"grid-4\" eacflag=\"CashFund\">汇添富基金-汇添富社会责任股票</span>   <span class=\"grid-2\" eacflag=\"CashMarket\">货币市场</span>   <span class=\"grid-3\"  eacflag=\"YearlyEarningsRate1\">151.15%</span>  </div>  </div>    </div>";
    } else {
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund1\"></div></div>"
    }
    trHtml += "<input type=\"hidden\" id=\"Fund1" + index + "\" name=\"Fund1" + index + "\" value=\"\" eacflag=\"Fund1\" />  <input type=\"hidden\" eacflag=\"CashCode\" value=\" \" /><input type=\"hidden\" eacflag=\"CashFund\" value=\" \" /> <input type=\"hidden\" eacflag=\"CashMarket\" value=\" \" /><input type=\"hidden\" eacflag=\"YearlyEarningsRate1\" value=\" \" /> </td> </tr> <tr class=\"end\"></tr> </tbody> </table>";
    trHtml += "  <table class=\"mb10\" id=\"ProductSelect" + index + "TabBondFund\" eacflag=\"ProductSelectTabBondFund\" width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">  <tbody> <tr> <td width=\"83\" align=\"center\" class=\"c-white\"  style=\"background-color: #2a91e6\">增值层</td>";
    trHtml += "<td>  <span><i class=\"c-red\">*</i>基金产品选择</span> <span class=\"grid-4\">债券型基金、混合型基金</span> ";
    //债券基金
    if (dom.Fund2 != 0) {
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund2\"><div class=\"fif-form b-grayish\">  <span class=\"grid-3\" eacflag=\"BondCode\">小龙</span>     <span class=\"grid-4\" eacflag=\"BondFund\">汇添富基金-汇添富社会责任股票</span>   <span class=\"grid-2\" eacflag=\"BondMarket\">货币市场</span>   <span class=\"grid-3\"  eacflag=\"YearlyEarningsRate2\">151.15%</span>  </div>  </div>    </div>";
    } else {
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund2\"></div></div>"
    }
    trHtml += "  <input type=\"hidden\" name=\"Fund2\" id=\"Fund2" + index + "\" value=\"\" eacflag=\"Fund2\" class=\"eac\" />  <input type=\"hidden\" eacflag=\"BondCode\" value=\" \" /><input type=\"hidden\" eacflag=\"BondFund\" value=\" \" /> <input type=\"hidden\" eacflag=\"BondMarket\" value=\" \" /><input type=\"hidden\" eacflag=\"YearlyEarningsRate2\" value=\" \" /> </td> </tr> <tr class=\"end\"></tr>  </tbody>  </table>";
    trHtml += "<table class=\"mb10\" id=\"ProductSelect" + index + "TabStockFund\" eacflag=\"ProductSelectTabStockFund\" width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">  <tbody> <tr> <td width=\"83\" rowspan=\"2\" align=\"center\" class=\"c-white\"  style=\"background-color: #086cc1\">投机层</td>";
    //p2p产品
    trHtml += "   <td> <span>P2P产品选择</span> <span class=\"grid-4\">P2P产品</span>  ";
    if (dom.P2PProduct != 0) {
        trHtml += "<div class=\"selected-list sib-list mt10\"><div class=\"sib-item\" field=\"P2PProduct\"><span title=\"\"></span> <div class=\"fif-form b-grayish\"> <span class=\"\" eacflag=\"P2PName\" title=></span> <span class=\"grid-2\" eacflag=\"InvestmentField\" title=></span> <span class=\"grid-2\" eacflag=\"InvestmentCycle\" title=\"\"></span> <span class=\"grid-2\" eacflag=\"StartAmount\" title=></span>  <span class=\"grid-2\" eacflag=\"EarningsRate\" title=\"\"></span>  </div> </div></div>"
    } else {
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\"  field=\"P2PProduct\"></div></div>"
    }
    trHtml += "<input type=\"hidden\" class=\"eac\" id=\"P2PProduct" + index + "\" value=\"0\" eacflag=\"P2PProduct\"><input type=\"hidden\" class=\"eac\" id=\"P2PProductRate" + index + "\" value=\"0\" eacflag=\"P2PProductRate\"></td></tr>";


    trHtml += "<tr> <td>  <span>基金产品选择</span>  <span class=\"grid-4\">股票型基金</span> "
    //股票基金
    if (dom.Fund3 != 0) {
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund3\"> <div class=\"fif-form b-grayish\">  <span class=\"grid-3\" eacflag=\"StockCode\">小龙</span>     <span class=\"grid-4\" eacflag=\"StockFund\">汇添富基金-汇添富社会责任股票</span>   <span class=\"grid-2\" eacflag=\"StockMarket\">货币市场</span>   <span class=\"grid-3\"  eacflag=\"YearlyEarningsRate3\">151.15%</span>  </div>  </div>    </div>";
    } else {
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund3\"></div></div>"
    }
    trHtml += " <input type=\"hidden\" id=\"Fund3" + index + "\" name=\"Fund3'\" value=\"\" eacflag=\"Fund3\" />  <input type=\"hidden\" eacflag=\"StockCode\" value=\" \" /><input type=\"hidden\" eacflag=\"StockFund\" value=\" \" /> <input type=\"hidden\" eacflag=\"StockMarket\" value=\" \" /><input type=\"hidden\" eacflag=\"YearlyEarningsRate3\" value=\" \" /> </td> </tr>    <tr class=\"end\"></tr> </tbody>  </table>   </div>";
    trHtml += "  <div class=\"fif-con \"> <div class=\"fif-box grid-5\"> <label class=\"fif-text\">产品组合预期收益率：</label>";
    trHtml += " <div class=\"input\"><span id=\"TotalRate" + index + "\" class=\"grid-4\" eacflag=\"TotalRate\"></span> <span class=\"ml10\">%</span> </div></div></div></div>"

    if (index == 1) {
        $("#InvestmentPlanParkchild101").append(trHtml);
    }
    if (index == 2) {
        $("#InvestmentPlanParkchild102").append(trHtml);
    }
    if (index == 3) {
        $("#InvestmentPlanParkchild201").append(trHtml);
    }
    if (index == 4) {
        $("#InvestmentPlanParkchild202").append(trHtml);
    }
    if (index == 5) {
        $("#InvestmentPlanParkchild301").append(trHtml);
    }
    if (index == 6) {
        $("#InvestmentPlanParkchild302").append(trHtml);
    }
    if (index == 7) {
        $("#InvestmentPlanParkchild401").append(trHtml);
    }
    if (index == 8) {
        $("#InvestmentPlanParkchild402").append(trHtml);
    }

};

function AddListLast(index, dom) {
    var trHtml = "";
    if (typeof index == "undefined") {
        IndexFlag = IndexFlag + 1;
        index = IndexFlag;
    }

    trHtml += "<div class=\"item b-grayish Tageach\"  id=\"ProductSelect" + index + "\" eacflag=\"ProductSelect\"><div class=\"fif-con  mb10\">";
    trHtml += "<div class=\"fif-box grid-4\"><label class=\"fif-text\">已完成规划：</label> <div class=\"input\">";
    trHtml += "<span id=\"PlanId" + index + " \" class=\"\" eacflag=\"PlanId\"></span> </div></div>";
    trHtml += "<div class=\"fif-box grid-6\"><label class=\"fif-text\"><i class=\"c-red\">*</i>方案所需投资收益率：</label>";
    trHtml += " <div class=\"input\"> <span id=\"PlanRate" + index + "\" class=\"grid-4\" eacflag=\"PlanRate\"></span><span class=\"ml10\">%</span></div> </div>";
    trHtml += "<a ></a></div>";
    trHtml += "<div class=\"table\"> <table class=\"mb10\" id=\"ProductSelect" + index + "TabCashFund\" eacflag=\"ProductSelectTabCashFund\" width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
    trHtml += " <tbody> <tr> <td width=\"83\" rowspan=\"2\" align=\"center\"  class=\"c-white\" style=\"background-color: #63b2f4\">保值层</td>";
    trHtml += "<td><span class=\"fl\">银行储蓄选择</span><div class=\"fif-con  fr grid-10\"> <div class=\"fif-box grid-12\" style=\"margin-bottom:10px;\">";
    trHtml += " <label class=\"fif-text\">活期：</label>   <div class=\"input grid-11\">";

    //银行选择
    trHtml += "<span id=\"DemandDepositsBank" + index + "\" class=\"\" eacflag=\"DemandDepositsBank\"></span>"

    trHtml += " <span id=\"DemandDepositsBankRate" + index + "\" class=\"\" eacflag=\"DemandDepositsBankRate\"></span>  </div></div>";
    trHtml += "<div class=\"fif-box grid-12\"> <label class=\"fif-text\">定期：</label><div class=\"input grid-11\">";
    //银行选择定期
    trHtml += "<span id=\"TimeDepositBank" + index + "\" class=\"\" eacflag=\"TimeDepositBank\"></span>";
    //银行期限
    trHtml += "<span id=\"TimeDepositBankTime" + index + "\" class=\"\" eacflag=\"TimeDepositBankTime\"></span>";

    trHtml += "<span id=\"TimeDepositBankRate" + index + "\" class=\"\" eacflag=\"TimeDepositBankRate\"></span> </div> </div></div></td> </tr>";
    trHtml += "<tr> <td> <span>基金产品选择</span>  <span class=\"grid-4\">货币市场基金</span> ";
    //货币基金
    if (dom.Fund1 != 0) {
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund1\"> </span> <div class=\"fif-form b-grayish\">  <span class=\"grid-3\" eacflag=\"CashCode\">小龙</span>     <span class=\"grid-4\" eacflag=\"CashFund\">汇添富基金-汇添富社会责任股票</span>   <span class=\"grid-2\" eacflag=\"CashMarket\">货币市场</span>   <span class=\"grid-3\"  eacflag=\"YearlyEarningsRate1\">151.15%</span>  </div>  </div>    </div>";
    } else {
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund1\"></div></div>"
    }
    trHtml += "<input type=\"hidden\" id=\"Fund1" + index + "\" name=\"Fund1" + index + "\" value=\"\" eacflag=\"Fund1\" />  <input type=\"hidden\" eacflag=\"CashCode\" value=\" \" /><input type=\"hidden\" eacflag=\"CashFund\" value=\" \" /> <input type=\"hidden\" eacflag=\"CashMarket\" value=\" \" /><input type=\"hidden\" eacflag=\"YearlyEarningsRate1\" value=\" \" /> </td> </tr> <tr class=\"end\"></tr> </tbody> </table>";
    trHtml += "  <table class=\"mb10\" id=\"ProductSelect" + index + "TabBondFund\" eacflag=\"ProductSelectTabBondFund\" width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">  <tbody> <tr> <td width=\"83\" align=\"center\" class=\"c-white\"  style=\"background-color: #2a91e6\">增值层</td>";
    trHtml += "<td>  <span>基金产品选择</span> <span class=\"grid-4\">债券型基金、混合型基金</span> ";
    //债券基金
    if (dom.Fund2 != 0) {
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund2\"><div class=\"fif-form b-grayish\">  <span class=\"grid-3\" eacflag=\"BondCode\">小龙</span>     <span class=\"grid-4\" eacflag=\"BondFund\">汇添富基金-汇添富社会责任股票</span>   <span class=\"grid-2\" eacflag=\"BondMarket\">货币市场</span>   <span class=\"grid-3\"  eacflag=\"YearlyEarningsRate2\">151.15%</span>  </div>  </div>    </div>";
    } else {
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund2\"></div></div>"
    }
    trHtml += "  <input type=\"hidden\" name=\"Fund2\" id=\"Fund2" + index + "\" value=\"\" eacflag=\"Fund2\" class=\"eac\" />  <input type=\"hidden\" eacflag=\"BondCode\" value=\" \" /><input type=\"hidden\" eacflag=\"BondFund\" value=\" \" /> <input type=\"hidden\" eacflag=\"BondMarket\" value=\" \" /><input type=\"hidden\" eacflag=\"YearlyEarningsRate2\" value=\" \" /> </td> </tr> <tr class=\"end\"></tr>  </tbody>  </table>";
    trHtml += "<table class=\"mb10\" id=\"ProductSelect" + index + "TabStockFund\" eacflag=\"ProductSelectTabStockFund\" width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">  <tbody> <tr> <td width=\"83\" rowspan=\"2\" align=\"center\" class=\"c-white\"  style=\"background-color: #086cc1\">投机层</td>";
    //p2p产品
    trHtml += "   <td> <span>P2P产品选择</span> <span class=\"grid-4\">P2P产品</span>  ";
    if (dom.P2PProduct != 0) {
        trHtml += "<div class=\"selected-list sib-list mt10\"><div class=\"sib-item\" field=\"P2PProduct\"><span title=\"\"></span> <div class=\"fif-form b-grayish\"> <span class=\"\" eacflag=\"P2PName\" title=></span> <span class=\"grid-2\" eacflag=\"InvestmentField\" title=></span> <span class=\"grid-2\" eacflag=\"InvestmentCycle\" title=\"\"></span> <span class=\"grid-2\" eacflag=\"StartAmount\" title=></span>  <span class=\"grid-2\" eacflag=\"EarningsRate\" title=\"\"></span>  </div> </div></div>"
    } else {
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\"  field=\"P2PProduct\"></div></div>"
    }
    trHtml += "<input type=\"hidden\" class=\"eac\" id=\"P2PProduct" + index + "\" value=\"0\" eacflag=\"P2PProduct\"><input type=\"hidden\" class=\"eac\" id=\"P2PProductRate" + index + "\" value=\"0\" eacflag=\"P2PProductRate\"></td></tr>";


    trHtml += "<tr> <td>  <span>基金产品选择</span>  <span class=\"grid-4\">股票型基金</span> "
    //股票基金
    if (dom.Fund3 != 0) {
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund3\"> <div class=\"fif-form b-grayish\">  <span class=\"grid-3\" eacflag=\"StockCode\">小龙</span>     <span class=\"grid-4\" eacflag=\"StockFund\">汇添富基金-汇添富社会责任股票</span>   <span class=\"grid-2\" eacflag=\"StockMarket\">货币市场</span>   <span class=\"grid-3\"  eacflag=\"YearlyEarningsRate3\">151.15%</span>  </div>  </div>    </div>";
    } else {
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund3\"></div></div>"
    }
    trHtml += " <input type=\"hidden\" id=\"Fund3" + index + "\" name=\"Fund3'\" value=\"\" eacflag=\"Fund3\" />  <input type=\"hidden\" eacflag=\"StockCode\" value=\" \" /><input type=\"hidden\" eacflag=\"StockFund\" value=\" \" /> <input type=\"hidden\" eacflag=\"StockMarket\" value=\" \" /><input type=\"hidden\" eacflag=\"YearlyEarningsRate3\" value=\" \" /> </td> </tr>    <tr class=\"end\"></tr> </tbody>  </table>   </div>";
    trHtml += "  <div class=\"fif-con \"> <div class=\"fif-box grid-5\"> <label class=\"fif-text\">产品组合预期收益率：</label>";
    trHtml += " <div class=\"input\"><span id=\"TotalRate" + index + "\" class=\"grid-4\" eacflag=\"TotalRate\"></span> </div></div></div></div>"


    $("#InvestmentPlanParkchild081").append(trHtml);

}

//循环赋值
function eacTransVal(index, dom) {
    var panid = EnumConvert.CompleteplanConvet(dom.PlanId)
    $("#ProductSelect" + index).find("span[eacflag='PlanId']").text(panid);
    $("#ProductSelect" + index).find("span[eacflag='PlanRate']").text(dom.PlanRate);
    $("#ProductSelect" + index).find("span[eacflag='DemandDepositsBank']").text(dom.BankView);
    $("#ProductSelect" + index).find("span[eacflag='DemandDepositsBankRate']").text(dom.DemandDepositsBankRate + " %");

    if (dom.BankTimeView==null) {
        $("#ProductSelect" + index).find("span[eacflag='TimeDepositBank']").text("");
    } else {
        $("#ProductSelect" + index).find("span[eacflag='TimeDepositBank']").text(dom.BankTimeView);
    }
    

    var TimeDepositBankTime = EnumConvert.YearSelectConvet(dom.TimeDepositBankTime);
    if (TimeDepositBankTime == "" || TimeDepositBankTime==null) {
        $("#ProductSelect" + index).find("span[eacflag='TimeDepositBankTime']").text("");
    } else {
        $("#ProductSelect" + index).find("span[eacflag='TimeDepositBankTime']").text(TimeDepositBankTime);
    }
    
    if (dom.TimeDepositBankRate==null) {
        $("#ProductSelect" + index).find("span[eacflag='TimeDepositBankRate']").text( " %");
    } else {
        $("#ProductSelect" + index).find("span[eacflag='TimeDepositBankRate']").text(dom.TimeDepositBankRate + " %");
    }

   


    $("#ProductSelect" + index).find("span[eacflag='Fund1']").text(dom.Fund1);
    $("#ProductSelect" + index).find("span[eacflag='Fund2']").text(dom.Fund2);
    $("#ProductSelect" + index).find("span[eacflag='P2PProduct']").text(dom.P2PProduct);
    $("#ProductSelect" + index).find("span[eacflag='P2PProductRate']").text(dom.P2PProductRate);
    $("#ProductSelect" + index).find("span[eacflag='Fund3']").text(dom.Fund3);
    $("#ProductSelect" + index).find("span[eacflag='TotalRate']").text(dom.TotalRate+" %");
    //将其赋值到隐藏域里面
    ////货币基金
    //$("#ProductSelect" + index).find("input[eacflag='CashCode']").val(dom.CashCode);
    //$("#ProductSelect" + index).find("input[eacflag='CashFund']").val(dom.CashFund);
    //$("#ProductSelect" + index).find("input[eacflag='CashMarket']").val(dom.CashMarket);
    //$("#ProductSelect" + index).find("input[eacflag='YearlyEarningsRate1']").val(dom.YearlyEarningsRate1);
    //// 债券型基金
    //$("#ProductSelect" + index).find("input[eacflag='BondCode']").val(dom.BondCode);
    //$("#ProductSelect" + index).find("input[eacflag='BondFund']").val(dom.BondFund);
    //$("#ProductSelect" + index).find("input[eacflag='BondMarket']").val(dom.BondMarket);
    //$("#ProductSelect" + index).find("input[eacflag='YearlyEarningsRate2']").val(dom.YearlyEarningsRate2);
    ////股票型基金
    //$("#ProductSelect" + index).find("input[eacflag='StockCode']").val(dom.StockCode);
    //$("#ProductSelect" + index).find("input[eacflag='StockFund']").val(dom.StockFund);
    //$("#ProductSelect" + index).find("input[eacflag='StockMarket']").val(dom.StockMarket);
    //$("#ProductSelect" + index).find("input[eacflag='YearlyEarningsRate3']").val(dom.YearlyEarningsRate3);

    //货币基金
    $("#ProductSelect" + index).find("span[eacflag='CashCode']").attr("title", dom.CashCode).text(dom.CashCode);
    $("#ProductSelect" + index).find("span[eacflag='CashFund']").attr("title", dom.CashFund).text(dom.CashFund);
    $("#ProductSelect" + index).find("span[eacflag='CashMarket']").attr("title", dom.CashMarket).text(dom.CashMarket);
    $("#ProductSelect" + index).find("span[eacflag='YearlyEarningsRate1']").attr("title", dom.YearlyEarningsRate1).text(dom.YearlyEarningsRate1+"%");
    // 债券型基金
    $("#ProductSelect" + index).find("span[eacflag='BondCode']").attr("title", dom.BondCode).text(dom.BondCode);
    $("#ProductSelect" + index).find("span[eacflag='BondFund']").attr("title", dom.BondFund).text(dom.BondFund);
    $("#ProductSelect" + index).find("span[eacflag='BondMarket']").attr("title", dom.BondMarket).text(dom.BondMarket);
    $("#ProductSelect" + index).find("span[eacflag='YearlyEarningsRate2']").attr("title", dom.YearlyEarningsRate2).text(dom.YearlyEarningsRate2+"%");
    //股票型基金
    $("#ProductSelect" + index).find("span[eacflag='StockCode']").attr("title", dom.StockCode).text(dom.StockCode);
    $("#ProductSelect" + index).find("span[eacflag='StockFund']").attr("title", dom.StockFund).text(dom.StockFund);
    $("#ProductSelect" + index).find("span[eacflag='StockMarket']").attr("title", dom.StockMarket).text(dom.StockMarket);
    $("#ProductSelect" + index).find("span[eacflag='YearlyEarningsRate3']").attr("title", dom.YearlyEarningsRate3).text(dom.YearlyEarningsRate3+"%");
    //p2p产品赋值
    $("#ProductSelect" + index).find("span[eacflag='P2PName']").attr("title", dom.P2PName).text(dom.P2PName);
    $("#ProductSelect" + index).find("span[eacflag='InvestmentField']").attr("title", dom.InvestmentField).text(dom.InvestmentField);
    $("#ProductSelect" + index).find("span[eacflag='InvestmentCycle']").attr("title", dom.InvestmentCycle).text(dom.InvestmentCycle);
    $("#ProductSelect" + index).find("span[eacflag='StartAmount']").attr("title", dom.StartAmount).text(dom.StartAmount);
    $("#ProductSelect" + index).find("span[eacflag='EarningsRate']").attr("title", dom.EarningsRate).text(dom.EarningsRate);
}

//投资规划饼
function ShowInsertmentInfo(Currency, Bond, Stock) {
    var chart;
    $('.showPie').highcharts({
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false
        },
        title: {
            text: '投资分配比例',
            align: 'left'
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        colors: ['#63b2f4', '#2a91e6', '#086cc1'], //'#46adb7', '#f2a83e', '#e16556'
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: false
                },
                showInLegend: true
            }
        },
        series: [{
            type: 'pie',
            name: '投资分配比例',
            data: [
                ['货币', Currency],
                {
                    name: '证券',
                    y: Bond,
                    sliced: false,
                    selected: false
                },
                ['股票', Stock],
            ]
        }]
    });
}

//投资规划结束--------------------预览加载投资规划结束
//************************************************************

//获取税收筹划相关信息--------------预览加载
function GetTaxPlan(proposalId) {
    $.ajax({
        url: "/CompetitionUser/TaxPlan/GetTaxPlanObj",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: proposalId
        },
        success: function (datas) {
            if (datas.list != null) {

                //教育规划信息
                var li = datas.list;
                if (li != null) {
                    NumCalc++;
                    var n = li;
                    $("#Salary").text(n.Salary.toMyFixed(2));//工资、薪金所得
                    $("#SalaryTax").text(n.SalaryTax.toMyFixed(2));//工资、薪金所得税
                    $("#OperatingRevenue").text(n.OperatingRevenue.toMyFixed(2));//个体工商户的生产、经营所得
                    $("#OperatingRevenueTax").text(n.OperatingRevenueTax.toMyFixed(2));//个体工商户的生产、经营所得税
                    $("#EnterprisesRevenue").text(n.EnterprisesRevenue.toMyFixed(2));// 对企事业单位承包、承租经营所得
                    $("#EnterprisesRevenueTax").text(n.EnterprisesRevenueTax.toMyFixed(2));//对企事业单位承包、承租经营所得税
                    $("#ServiceIncome").text(n.ServiceIncome.toMyFixed(2));//劳务报酬所得
                    $("#ServiceIncomeTax").text(n.ServiceIncomeTax.toMyFixed(2));//劳务报酬所得税
                    $("#Remuneration").text(n.Remuneration.toMyFixed(2));//稿酬所得
                    $("#RemunerationTax").text(n.RemunerationTax.toMyFixed(2));//稿酬所得税
                    $("#Loyalities").text(n.Loyalities.toMyFixed(2));//特许权使用费所得
                    $("#LoyalitiesTax").text(n.LoyalitiesTax.toMyFixed(2));//特许权使用费所得税
                    $("#Demise").text(n.Demise.toMyFixed(2));// 财产转让所得
                    $("#DemiseTax").text(n.DemiseTax.toMyFixed(2));//财产转让所得税
                    $("#IncidentalIncome").text(n.IncidentalIncome.toMyFixed(2));//偶然所得
                    $("#IncidentalIncomeTax").text(n.IncidentalIncomeTax.toMyFixed(2));//偶然所得税
                    $("#InterestTaxMain").text(n.Interest.toMyFixed(2));//利息、红利、股利所得
                    $("#InterestTax").text(n.InterestTax.toMyFixed(2));//利息、红利、股利所得税
                    $("#TotalAmount").text(n.TotalAmount.toMyFixed(2));//合计
                    $("#TotalTax").text(n.TotalTax.toMyFixed(2));//合计税
                    $("#AnalysisTax").text(n.Analysis);//税收筹划分析
                    //隐藏域
                    $("#Id").text(n.Id);
                }
            } else {
                $("#TaxPlan").hide();
            }
        }
    });
}

//***********************************************************
//获取财产分配表显示-----------------预览加载
function LoadDistributionOfPropertyInfo(proposalId) {
    $.ajax({
        url: "/CompetitionUser/DistributionOfProperty/GetDistributionOfPropertyByProposalId",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: proposalId,
            rId: Math.random(),
        },
        success: function (data) {
            if (data.SituationAnalysis != null) {
                SetDistributionOfPropertyInfo(data);//编辑建议书财产分配表设置
            }
            else {
                $("#DistributionOfPropertyDiv").hide();
            }
        }
    });
}

//设置财产分配表显示
function SetDistributionOfPropertyInfo(data) {
    NumCalc++;
    //客户性别
    var CustomerName = data.CustomerName == null ? "" : data.CustomerName;
    $("#CustomerNameDistribution").text(CustomerName);
    var CustomerAge = data.CustomerAge == 0 ? "" : data.CustomerAge;
    $("#CustomerAge").text(CustomerAge);
    //$("#CustomerNameDistribution").text(data.CustomerName);
    //$("#CustomerAge").text(data.CustomerAge);
    var CustomerSex = EnumConvert.SexConvert(data.CustomerSex);
    $("#CustomerSex").text(CustomerSex);

   // $("#AddressDistribution").text(data.Address);
   // $("#PositionDistribution").text(data.Position);

    if (data.Address == null) {
        $("#AddressDistribution").text("");
    } else {
        $("#AddressDistribution").text(data.Address);
    }

    if (data.Address == null) {
        $("#PositionDistribution").text("");
    } else {
        $("#PositionDistribution").text(data.Position);
    }

    $("#FamilyNum").text(data.FamilyNum);
    $("#SituationAnalysis").text(data.SituationAnalysis);
    var PlanTool = EnumConvert.DistributionConvert(data.PlanTool)
    $("#PlanTool").text(PlanTool);
    $("#PlanAnalysis").text(data.PlanAnalysis);

    //客户亲属列表
    $("#siblistDistribution").html("");
    $(data.ProposalCustomerDetailList).each(function (index, dom) {
        SetDistributionList(dom.DependentName, dom.Age, dom.Relation, dom.InCome);
    });
}
//class=\"ipt-text IsRequired IsMinLength IsMaxLength\"
//增加建议书客户家属列表  
function SetDistributionList(DependentName, Age, Relation, InCome) {
    DistributionOfProperty_index += 1;
    var trHtml = "";
    trHtml += "<div class=\"sib-item\">";
    trHtml += "<span id=\"closeId\"></span>";
    trHtml += "<div class=\"fif-form\">";
    trHtml += "<div class=\"fif-box\">";
    trHtml += "<label class=\"fif-text\">姓 名</label>";
    trHtml += "<div class=\"input\"><span id=\"Distribution_name_{4}\" name=\"Distribution_detail_name\" class=\"\" type=\"text\" value='{0}' >{0}</span></div>";
    trHtml += "</div>";
    trHtml += "<div class=\"fif-box\">";
    trHtml += "<label class=\"fif-text\">年 龄</label>";
    trHtml += "<div class=\"input\"><span id=\"Distribution_age_{4}\" name=\"Distribution_detail_age\" class=\"\" type=\"text\" value='{1}' >{1}</span><span class=\"ml10\">岁</span></div>";
    trHtml += "</div>";
    trHtml += "<div class=\"fif-box\">";
    trHtml += "<label class=\"fif-text\">与客户关系</label>";
    trHtml += "<div class=\"input\"><span id=\"Distribution_relation_{4}\" name=\"Distribution_detail_relation\" class=\"\" type=\"text\" value='{2}'>{2}</span></div>";
    trHtml += "</div>";
    trHtml += "<div class=\"fif-box\">";
    trHtml += "<label class=\"fif-text\">年收入</label>";
    trHtml += "<div class=\"input\"><span id=\"Distribution_income_{4}\" name=\"Distribution_detail_income\" class=\"\" type=\"text\" value='{3}'>{3}</span><span class=\"ml10\">元</span></div>";
    trHtml += "</div></div></div>";

    var html = StringHelper.FormatStr(trHtml,
        DependentName,      //0 姓名
        Age,                //1 年龄
        Relation,           //2 与客户关系
        InCome,             //3 年收入
        DistributionOfProperty_index      //4 随机Id
        );

    $("#siblistDistribution").append(html);
}

//*********************************************************
/**
 * @name 获取财产传承--------------预览加载
 */
function GetHeritage(proposalId) {
    $.ajax({
        url: "/CompetitionUser/Heritage/GetHeritage",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: proposalId,
            rId: Math.random()
        },
        success: function (data) {
            if (data != null && data != "") {
                NumCalc++;
                $("#CashHeritage").text(data.Cash);//现金
                $("#DepositHeritage").text(data.Deposit);//银行存款
                $("#LifeInsurance").text(data.LifeInsurance);//人寿保单赔偿金额
                $("#OtherCashAccount").text(data.OtherCashAccount);//其他现金账户
                $("#CashSubtotal").text((data.Cash + data.Deposit + data.LifeInsurance + data.OtherCashAccount).toMyFixed(2));

                $("#StockHeritage").text(data.Stock);//股票
                $("#BondHeritage").text(data.Bond);//债券
                $("#FundHeritage").text(data.Fund);//基金
                $("#OtherInvestmentHeritage").text(data.OtherInvestment);//其他投资收益
                $("#InvestmentSubtotal").text((data.Stock + data.Bond + data.Fund + data.OtherInvestment).toMyFixed(2));

                $("#Pension").text(data.Pension);//养老金（一次性收入现值）
                $("#AnnuityRevenue").text(data.AnnuityRevenue);//配偶/遗孤年金收益（现值）
                $("#OtherPension").text(data.OtherPension);//其他退休基金
                var subtotal1 = (data.Pension + data.AnnuityRevenue + data.OtherPension).toMyFixed(2);
                $("#PensionSubtotal").text(subtotal1);

                $("#House").text(data.House);//房产
                $("#CarHeritage").text(data.Car);//汽车
                $("#OtherHeritage").text(data.Other);//其他个人资产
                $("#OtherProperty").text(data.OtherProperty);//其他资产
                var subtotal2 = (data.House + data.Car + data.Other).toMyFixed(2)
                $("#PersonSubtotal").text(subtotal2);

                $("#TotalProperty").text(data.TotalProperty);//资产总计
                $("#TotalProperty2").text(data.TotalProperty);//资产总计

                $("#ShortTermLoan").text(data.ShortTermLoan);//短期贷款
                $("#MediumTermLoans").text(data.MediumTermLoans);//中期贷款
                $("#LongTermLoan").text(data.LongTermLoan);//长期贷款
                $("#OtherLoanHeritage").text(data.OtherLoan);//其他贷款
                $("#LoanSubtotalHeritage").text((data.ShortTermLoan + data.MediumTermLoans + data.LongTermLoan + data.OtherLoan).toMyFixed(2));

                $("#MedicalCosts").text(data.MedicalCosts);//临终医疗费用
                $("#TaxCosts").text(data.TaxCosts);//预期收入纳税额支出
                $("#FuneralExpenses").text(data.FuneralExpenses);//丧葬费用
                $("#HeritageCosts").text(data.HeritageCosts);//遗产处置费用
                $("#OtherCosts").text(data.OtherCosts);//其他费用
                $("#CostSubtotal").text((data.MedicalCosts + data.TaxCosts + data.FuneralExpenses + data.HeritageCosts + data.OtherCosts).toMyFixed(2));

                $("#OtherLiabilities").text(data.OtherLiabilities);//其他负债
                $("#TotalLiabilities").text(data.TotalLiabilities);//负债总计
                $("#TotalLiabilities2").text(data.TotalLiabilities);

                $("#FinanceAnalysis").text(data.FinanceAnalysis);//财务分析
                var plantool = EnumConvert.HeritageConvert(data.PlanTool)
                $("#PlanToolHeritage").text(plantool);//财产传承规划工具
                $("#PlanAnalysisHeritage").text(data.PlanAnalysis);//财产传承规划分析

                $("#TotalHeritage").text((data.TotalProperty - data.TotalLiabilities).toFixed(2));//净遗产总计
                //隐藏域
                $("#Id").text(data.Id);
            } else {
                $("#Heritage").hide();
            }
        }
    });
}

//判断是否非法字符
function CheckNum(num) {
    var pattern6 = /^[-]?\d+(\.\d{1,6})?$/;//只能输入两位小数
    if (!pattern6.test(num)) {
        num = 0;
        return num;
    }
    return num;
}

//=========================计算
//流动资产计算
function calcFlowAssets(Cash, RMBDeposit, OtherAsset) {
    var Cash1 = CheckNum(Cash);
    var RMBDeposit1 = CheckNum(RMBDeposit);
    var OtherAsset1 = CheckNum(OtherAsset);
    var sum = 0;
    if (Cash == Cash1 && RMBDeposit == RMBDeposit1 && OtherAsset == OtherAsset1) {
        sum = Cash * 1 + RMBDeposit * 1 + OtherAsset * 1;
    } else {
        sum = 0;
    }
    return sum;
}
//消费资产计算
function calcConsumeAssets(CreditCard, Microfinance, OtherLoan) {
    var CreditCard1 = CheckNum(CreditCard);
    var Microfinance1 = CheckNum(Microfinance);
    var OtherLoan1 = CheckNum(OtherLoan);
    var sum = 0;
    if (CreditCard1 == CreditCard && Microfinance1 == Microfinance && OtherLoan1 == OtherLoan) {
        sum = CreditCard * 1 + Microfinance * 1 + OtherLoan * 1
    } else {
        sum = 0;
    }
    return sum;
}
//投资资产计算
function calcInvestmentAssets(RMBFixedDeposit, ForeignCurrencyFixedDeposit, StockInvestment, BondInvestment, FundInvestment, IndustryInvestment,
   PolicyInvestment, EstateInvestment, OtherInvestment) {
    var RMBFixedDeposit1 = CheckNum(RMBFixedDeposit);
    var ForeignCurrencyFixedDeposit1 = CheckNum(ForeignCurrencyFixedDeposit);
    var StockInvestment1 = CheckNum(StockInvestment);
    var BondInvestment1 = CheckNum(BondInvestment);
    var FundInvestment1 = CheckNum(FundInvestment);
    var IndustryInvestment1 = CheckNum(IndustryInvestment);
    var PolicyInvestment1 = CheckNum(PolicyInvestment);
    var EstateInvestment1 = CheckNum(EstateInvestment);
    var OtherInvestment1 = CheckNum(OtherInvestment);
    var sum = 0;
    if (RMBFixedDeposit1 == RMBFixedDeposit && ForeignCurrencyFixedDeposit1 == ForeignCurrencyFixedDeposit && StockInvestment1 == StockInvestment && BondInvestment1 == BondInvestment && FundInvestment1 == FundInvestment && IndustryInvestment1 == IndustryInvestment && PolicyInvestment1 == PolicyInvestment && EstateInvestment1 == EstateInvestment && OtherInvestment1 == OtherInvestment) {
        sum = RMBFixedDeposit * 1 + ForeignCurrencyFixedDeposit * 1 + StockInvestment * 1 + BondInvestment * 1 + FundInvestment * 1 + IndustryInvestment * 1 + PolicyInvestment * 1 + EstateInvestment * 1 + OtherInvestment * 1;
    } else {
        sum = 0;
    }
    return sum;
}
//投资负债计算

function calcInvestmentLiability(FinancialLoan, IndustryInvestmentLoan, EstateInvestmentLoan, OtherInvestmentLoan) {
    var FinancialLoan1 = CheckNum(FinancialLoan);
    var IndustryInvestmentLoan1 = CheckNum(IndustryInvestmentLoan);
    var EstateInvestmentLoan1 = CheckNum(EstateInvestmentLoan);
    var OtherInvestmentLoan1 = CheckNum(OtherInvestmentLoan);
    var sum = 0;
    if (FinancialLoan1 == FinancialLoan && IndustryInvestmentLoan1 == IndustryInvestmentLoan && EstateInvestmentLoan1 == EstateInvestmentLoan && OtherInvestmentLoan1 == OtherInvestmentLoan) {
        sum = FinancialLoan * 1 + IndustryInvestmentLoan * 1 + EstateInvestmentLoan * 1 + OtherInvestmentLoan * 1;
    } else {
        sum = 0;
    }
    return sum;
}
//自用负债
function calcSelfLiability(EstateLoan, CarLoan, OthersLoan) {
    var EstateLoan1 = CheckNum(EstateLoan);
    var CarLoan1 = CheckNum(CarLoan);
    var OthersLoan1 = CheckNum(OthersLoan);
    var sum = 0;
    if (EstateLoan1 == EstateLoan && CarLoan1 == CarLoan && OthersLoan1 == OthersLoan) {
        sum = EstateLoan * 1 + CarLoan * 1 + OthersLoan * 1;
    } else {
        sum = 0;
    }
    return sum;
}
//自用资产计算
function calcSelfAsset(Estate, Car, Others) {
    var Others1 = CheckNum(Others);
    var Car1 = CheckNum(Car);
    var Estate1 = CheckNum(Estate);
    //TotalAssets
    var sum = 0;
    if (Others1 == Others && Car1 == Car && Estate1 == Estate) {
        sum = Estate * 1 + Car * 1 + Others * 1;
    } else {
        sum = 0;
    }
    return sum;
}
//资产合计
function calcTotalAssets(FlowAssets, InvestmentAssets, SelfAsset) {
    FlowAssets = CheckNum(FlowAssets);
    InvestmentAssets = CheckNum(InvestmentAssets);
    SelfAsset = CheckNum(SelfAsset);
    var sum = FlowAssets * 1 + InvestmentAssets * 1 + SelfAsset * 1;
    return sum;
}
//负债合计
function calcTotalLoan(ConsumeAssets, InvestmentLiability, SelfLiability) {
    ConsumeAssets = CheckNum(ConsumeAssets);
    InvestmentLiability = CheckNum(InvestmentLiability);
    SelfLiability = CheckNum(SelfLiability);
    var sum = ConsumeAssets * 1 + InvestmentLiability * 1 + SelfLiability * 1;
    return sum;
}

//==========================净值计算==================
//消费净值计算 1）	消费净值=流动资产小计-流动负债小计
function calcConsumeVal(flowAssets, ConsumeAssets) {
    flowAssets = CheckNum(flowAssets);
    ConsumeAssets = CheckNum(ConsumeAssets);

    var sum = flowAssets * 1 - ConsumeAssets * 1;
    return sum;
}
//2）	投资净值=投资资产小计-投资负债小计
function calcInvestmentVal(InvestmentAssets, InvestmentLiability) {
    InvestmentAssets = CheckNum(InvestmentAssets);
    InvestmentLiability = CheckNum(InvestmentLiability);

    var sum = InvestmentAssets * 1 - InvestmentLiability * 1;
    return sum;
}
//3）	自用净值=自用资产-自用负债
function clacSelfVal(SelfAsset, SelfLiability) {
    SelfAsset = CheckNum(SelfAsset);
    SelfLiability = CheckNum(SelfLiability);

    var sum = SelfAsset * 1 - SelfLiability * 1;
    return sum;
}
//4）	净值合计=∑（消费净值、投资净值、自用净值）
function calcSumVal(ConsumeVal, InvestmentVal, SelfVal) {
    ConsumeVal = CheckNum(ConsumeVal);
    InvestmentVal = CheckNum(InvestmentVal);
    SelfVal = CheckNum(SelfVal);

    var sum = ConsumeVal * 1 + InvestmentVal * 1 + SelfVal * 1;
    return sum;
}

//=========================计算---------------------------收支储蓄中的计算
//1.	工作收入计算
function calcWorkIncome(JobIncome, EndowmentInsurance, MedicalInsurance, HousingFund, OtherJobIncome) {
    var JobIncome1 = CheckNum(JobIncome);
    var EndowmentInsurance1 = CheckNum(EndowmentInsurance);
    var MedicalInsurance1 = CheckNum(MedicalInsurance);
    var HousingFund1 = CheckNum(HousingFund);
    var OtherJobIncome1 = CheckNum(OtherJobIncome);
    var sum = 0;
    if (JobIncome1 == JobIncome && EndowmentInsurance1 == EndowmentInsurance && MedicalInsurance1 == MedicalInsurance && HousingFund1 == HousingFund && OtherJobIncome1 == OtherJobIncome) {
        sum = JobIncome * 1 + EndowmentInsurance * 1 + MedicalInsurance * 1 + HousingFund * 1 + OtherJobIncome * 1
    } else {
        sum = 0;
    }
    return sum;
}

//1.	生活支出
function calcLiveExpense(FamilyExpense, ChildExpense, OtherExpense) {
    var FamilyExpense1 = CheckNum(FamilyExpense);
    var ChildExpense1 = CheckNum(ChildExpense);
    var OtherExpense1 = CheckNum(OtherExpense);
    var sum = 0;
    if (FamilyExpense1 == FamilyExpense && ChildExpense1 == ChildExpense && OtherExpense1 == OtherExpense) {
        sum = FamilyExpense * 1 + ChildExpense * 1 + OtherExpense * 1;
    } else {
        sum = 0;
    }
    return sum;
}
//1.	理财收入
function calcInvestmentIncome(Interest, CapitalGains, OtherIncome) {
    var Interest1 = CheckNum(Interest);
    var CapitalGains1 = CheckNum(CapitalGains);
    var OtherIncome1 = CheckNum(OtherIncome);
    var sum = 0;
    if (Interest1 == Interest && CapitalGains1 == CapitalGains && OtherIncome1 == OtherIncome) {
        sum = Interest * 1 + CapitalGains * 1 + OtherIncome * 1;
    } else {
        sum = 0;
    }
    return sum;
}
//1.	理财支出
function calcInvestmentExpense(InterestExpense, InsuranceExpense, OtherFinanceExpense) {
    var InterestExpense1 = CheckNum(InterestExpense);
    var InsuranceExpense1 = CheckNum(InsuranceExpense);
    var OtherFinanceExpense1 = CheckNum(OtherFinanceExpense);
    var sum = 0;
    if (InterestExpense1 == InterestExpense && InsuranceExpense1 == InsuranceExpense && OtherFinanceExpense1 == OtherFinanceExpense) {
        sum = InterestExpense * 1 + InsuranceExpense * 1 + OtherFinanceExpense * 1;
    } else {
        sum = 0;
    }
    return sum;
}

//--------------------------------------现金流量的方法
//加载本期现金及现金等价物净增加额
function loadings() {
    var Money1 = $.trim($("#lifeMoney").text()) * 1;
    var Money2 = $.trim($("#investMoney").text()) * 1;
    var Money3 = $.trim($("#borrowMoney").text()) * 1;
    var Money4 = $.trim($("#InsuranceExpense2").text()) * 1;
    var All = Money1 + Money2 + Money3 + Money4;
    $("#Money").text(All.toMyFixed(2));
}

//计算现金规划计算页面
var clacCashPanVal = {
    calcRetainCashType: function (multiple) {
        //var multiple = $("RetainCashMultiple").text();
        if (multiple == "0") {
          //  $("#RetainCashMultiple").text(0);
        } else {
            var familyMonthExpense = $("#FamilyMonthExpense").attr("Family") * 1;
            var result = (multiple * familyMonthExpense).toMyFixed(2);
            return result;
        }
    }
}
//-----------------------------------教育规划添加HTML方法


function AddHTML(valu) {

    var Nums = valu;

    var html = '';
    html += '<div id="EducationList' + Nums + '" field="EducationList" class="itemBox js_itemboxs" style="border-bottom:1px solid #d7d7d7;">';
    html += '<div class="item-title b-gray">';
    html += '<strong><i class="c-red">*</i>教育阶段选择&nbsp;&nbsp;</strong>';
    html += '<span id="EduStage' + Nums + '" class="" style="width:110px;" field="EduStage"></span>'
    html += '</div>';
    html += ' <div class="fif-form fif-form3 grid-7">';
    html += '<div class="fif-box w100">';
    html += '<label class="fif-text"><i class="c-red">*</i>求学年龄：</label>';
    html += '<div class="input"><span id="EduAge' + Nums + '" field="EduAge" style="" class=""></span><span class="ml10">岁</span></div>';
    html += '</div>';
    html += '<div class="fif-box w100">';
    html += '<label class="fif-text"><i class="c-red">*</i>求学时间：</label>';
    html += '<div class="input"><span id="EduTime' + Nums + '" class="" style="" field="EduTime"></span><span class="ml10">年</span></div>';
    html += '</div>';
    html += '<div class="fif-box w100">';
    html += '<label class="fif-text"><i class="c-red">*</i>目前学费：</label>';
    html += '<div class="input"><span id="Tuition' + Nums + '" class="" style="" field="Tuition"></span><span class="ml10">元/年</span></div>';
    html += '</div>';
    html += '<div class="fif-box w100">';
    html += '<label class="fif-text">上学时学费：</label>';
    html += '<div class="input"><span id="EduTuition' + Nums + '" class=""  field="EduTuition"></span><span class="ml10">元/年</span></div></div>';

    html += '<div class="fif-box w100">';
    html += '<label class="fif-text"  style="font-size:16px;font-weight:600;height:50px;padding-top:20px;margin-left:30px;">上学前需准备的总学费：</label>';
    html += '<div class="input"><span style="font-size:20px;font-weight:600;height:50px;padding-top:20px;color:#f87608;" id="TotalTuition' + Nums + '" class="" field="TotalTuition"></span><span class="ml10">元</span></div></div>';


    html += '</div>';
    html += '<p class="clear"></p>';
    html += '<input type="hidden" value="' + Nums + '" id="NUM' + Nums + '"/>';
    html += '<input type="hidden" value="0" id="IDN' + Nums + '" field="IDN" /></div>';

    if (Nums < 4) {
        $("#LifeEducationPlanPart1").append(html);
    } else {
        $("#LifeEducationPlanPartSub01").append(html);
    }
    //$("#TagSpan").prev().after(html);
}

//打印当前页面
function PrintPage() {
    $("#printCover").show();
    $(".btn").hide();
    $(".main-hd").before("<div name=\"newLineDiv\" style=\"page-break-after: always\"></div>");
    $(".main-bd").after("<div name=\"newLineDiv\" style=\"page-break-after: always\"></div>");
    var options = {
        callBackFunc: function () {
            $("#printCover").hide();
            $(".btn").show();
            $("div[name='newLineDiv']").remove();
        }
    };
    $("#PreviewIndexDiv").printArea(options);
    return;
};

