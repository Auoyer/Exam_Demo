//************************
//收支储蓄
//************************
var param = "";
var TagNavi = true;
//判断是否非法字符
function CheckNum(num) {
    var pattern6 = /^[-]?\d+(\.\d{1,2})?$/;//只能输入两位小数
    if (!pattern6.test(num)) {
        num = 0;
        return num;
    }
    num = (num * 1).toMyFixed(2);
    return num;
}
//=========================计算
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
//========================3.	工作储蓄=工作收入－生活支出
function clacWolkDeposit01(WorkIncome, LiveExpense) {
    WorkIncome = CheckNum(WorkIncome);
    LiveExpense = CheckNum(LiveExpense);

    var sum = WorkIncome*1 - LiveExpense*1
    return sum;
}
//=======理财储蓄 =理财收入-理财支出
function calcInvestmentDeposit01(InvestmentIncome, InvestmentExpense) {
    InvestmentIncome = CheckNum(InvestmentIncome);
    InvestmentExpense = CheckNum(InvestmentExpense);

    var sum = InvestmentIncome * 1 - InvestmentExpense * 1
    return sum;
}
//======= 储蓄合计=∑（工作储蓄、理财储蓄）
function clacTotalDeposit(WolkDeposit01, InvestmentDeposit01) {
    WolkDeposit01 = CheckNum(WolkDeposit01);
    InvestmentDeposit01 = CheckNum(InvestmentDeposit01);
    var sum = WolkDeposit01*1 + InvestmentDeposit01*1;
    return sum;
}
//==============9.	自由储蓄=储蓄合计－∑（养老保险储蓄、住房公积金储蓄）
function calcFreeTotal(TotalDeposit, EndowmentInsurance, HousingFund) {
    //先判断后赋值
   var TotalDeposit1 = CheckNum(TotalDeposit);
   var EndowmentInsurance1 = CheckNum(EndowmentInsurance)*1;
   var HousingFund1 = CheckNum(HousingFund)*1;
   var sum = 0;
   if (TotalDeposit1 == TotalDeposit && EndowmentInsurance1 == EndowmentInsurance && HousingFund1 == HousingFund) {
       sum = TotalDeposit*1 - (EndowmentInsurance*1 + HousingFund*1);
   } else {
       sum = 0;
   }
    //赋值
   $("#EndowmentInsuranceSub").val(EndowmentInsurance1);
   $("#HousingFundSub").val(HousingFund1);
    //计算
    //var sum = TotalDeposit - (EndowmentInsurance + HousingFund);
    return sum;
}

$(function () {
    IsProposalSave()//客户验证

    ////获取URL参数
    //var ProposalId = $.getUrlParam("ProposalId");

    ////获取财产传承
    //if (ProposalId != null && ProposalId != "" && ProposalId != undefined) {
    //}
    //工作收入计算
    $("#JobIncome,#EndowmentInsurance,#MedicalInsurance,#HousingFund,#OtherJobIncome").unbind("blur").blur(function () {
        var JobIncome = $.trim($("#JobIncome").val()) * 1;
        var EndowmentInsurance = $.trim($("#EndowmentInsurance").val()) * 1;
        var MedicalInsurance = $.trim($("#MedicalInsurance").val()) * 1;
        var HousingFund = $.trim($("#HousingFund").val()) * 1;
        var OtherJobIncome = $.trim($("#OtherJobIncome").val()) * 1;

        $("#FinanceIncomeAndExpensesDiv #workIncome01").val(calcWorkIncome(JobIncome, EndowmentInsurance, MedicalInsurance, HousingFund, OtherJobIncome).toMyFixed(2));
        //
        //3.	工作储蓄=工作收入－生活支出
        var workIncome01 = $.trim($("#workIncome01").val()) * 1;
        var liveExpense01 = $.trim($("#liveExpense01").val()) * 1;
        $("#FinanceIncomeAndExpensesDiv #wolkDeposit01").val(clacWolkDeposit01(workIncome01, liveExpense01).toMyFixed(2));
        ////储蓄合计
        var wolkDeposit01 = $.trim($("#wolkDeposit01").val()) * 1;
        var InvestmentDeposit01 = $.trim($("#InvestmentDeposit01").val()) * 1;
        $("#FinanceIncomeAndExpensesDiv #TotalDeposit").val(clacTotalDeposit(wolkDeposit01, InvestmentDeposit01).toMyFixed(2));
        //自由储蓄合计
        var TotalDeposit = $.trim($("#TotalDeposit").val()) * 1;
        var EndowmentInsurance = $.trim($("#EndowmentInsurance").val()) * 1;
        var HousingFund = $.trim($("#HousingFund").val()) * 1;
        $("#FinanceIncomeAndExpensesDiv #FreeMoney").val(calcFreeTotal(TotalDeposit, EndowmentInsurance, HousingFund).toMyFixed(2));

    });

    //减：生活支出
    $("#FamilyExpense,#ChildExpense,#OtherExpense").unbind("blur").blur(function () {
        var FamilyExpense = $.trim($("#FamilyExpense").val()) * 1;
        var ChildExpense = $.trim($("#ChildExpense").val()) * 1;
        var OtherExpense = $.trim($("#OtherExpense").val()) * 1;
   

        $("#FinanceIncomeAndExpensesDiv #liveExpense01").val(calcLiveExpense(FamilyExpense, ChildExpense, OtherExpense).toMyFixed(2));
        //  消费净值净值计算
        var workIncome01 = $.trim($("#workIncome01").val()) * 1;
        var liveExpense01 = $.trim($("#liveExpense01").val()) * 1;
        $("#FinanceIncomeAndExpensesDiv #wolkDeposit01").val(clacWolkDeposit01(workIncome01, liveExpense01).toMyFixed(2));
        ////储蓄合计
        var wolkDeposit01 = $.trim($("#wolkDeposit01").val()) * 1;
        var InvestmentDeposit01 = $.trim($("#InvestmentDeposit01").val()) * 1;
        $("#FinanceIncomeAndExpensesDiv #TotalDeposit").val(clacTotalDeposit(wolkDeposit01, InvestmentDeposit01).toMyFixed(2));
        //自由储蓄合计
        var TotalDeposit = $.trim($("#TotalDeposit").val()) * 1;
        var EndowmentInsurance = $.trim($("#EndowmentInsurance").val()) * 1;
        var HousingFund = $.trim($("#HousingFund").val()) * 1;
        $("#FinanceIncomeAndExpensesDiv #FreeMoney").val(calcFreeTotal(TotalDeposit, EndowmentInsurance, HousingFund).toMyFixed(2));

    });

    //理财收入计算
    $("#Interest,#CapitalGains,#OtherIncome").unbind("blur").blur(function () {
        var Interest = $.trim($("#Interest").val()) * 1;
        var CapitalGains = $.trim($("#CapitalGains").val()) * 1;
        var OtherIncome = $.trim($("#OtherIncome").val()) * 1;
    
        $("#FinanceIncomeAndExpensesDiv #investmentIncome01").val(calcInvestmentIncome(Interest, CapitalGains, OtherIncome).toMyFixed(2));
        //理财储蓄计算
        var investmentIncome01 = $.trim($("#investmentIncome01").val()) * 1;
        var investmentExpense01 = $.trim($("#investmentExpense01").val()) * 1;
        $("#FinanceIncomeAndExpensesDiv #InvestmentDeposit01").val(calcInvestmentDeposit01(investmentIncome01, investmentExpense01).toMyFixed(2));
        ////储蓄合计
        var wolkDeposit01 = $.trim($("#wolkDeposit01").val()) * 1;
        var InvestmentDeposit01 = $.trim($("#InvestmentDeposit01").val()) * 1;
        $("#FinanceIncomeAndExpensesDiv #TotalDeposit").val(clacTotalDeposit(wolkDeposit01, InvestmentDeposit01).toMyFixed(2));
        //自由储蓄合计
        var TotalDeposit = $.trim($("#TotalDeposit").val()) * 1;
        var EndowmentInsurance = $.trim($("#EndowmentInsurance").val()) * 1;
        var HousingFund = $.trim($("#HousingFund").val()) * 1;
        $("#FinanceIncomeAndExpensesDiv #FreeMoney").val(calcFreeTotal(TotalDeposit, EndowmentInsurance, HousingFund).toMyFixed(2));

    });

    //理财支出计算
    $("#InterestExpense,#InsuranceExpense,#OtherFinanceExpense").unbind("blur").blur(function () {
        var InterestExpense = $.trim($("#InterestExpense").val()) * 1;
        var InsuranceExpense = $.trim($("#InsuranceExpense").val()) * 1;
        var OtherFinanceExpense = $.trim($("#OtherFinanceExpense").val()) * 1;
    

        $("#FinanceIncomeAndExpensesDiv #investmentExpense01").val(calcInvestmentExpense(InterestExpense, InsuranceExpense, OtherFinanceExpense).toMyFixed(2));
        //理财储蓄计算
        var investmentIncome01 = $.trim($("#investmentIncome01").val()) * 1;
        var investmentExpense01 = $.trim($("#investmentExpense01").val()) * 1;
        $("#FinanceIncomeAndExpensesDiv #InvestmentDeposit01").val(calcInvestmentDeposit01(investmentIncome01, investmentExpense01).toMyFixed(2));
        ////储蓄合计
        var wolkDeposit01 = $.trim($("#wolkDeposit01").val()) * 1;
        var InvestmentDeposit01 = $.trim($("#InvestmentDeposit01").val()) * 1;
        $("#FinanceIncomeAndExpensesDiv #TotalDeposit").val(clacTotalDeposit(wolkDeposit01, InvestmentDeposit01).toMyFixed(2));
        //自由储蓄合计
        var TotalDeposit = $.trim($("#TotalDeposit").val()) * 1;
        var EndowmentInsurance = $.trim($("#EndowmentInsurance").val()) * 1;
        var HousingFund = $.trim($("#HousingFund").val()) * 1;
        $("#FinanceIncomeAndExpensesDiv #FreeMoney").val(calcFreeTotal(TotalDeposit, EndowmentInsurance, HousingFund).toMyFixed(2));

    });

})

//新增以及修改数据
function AddIncomeAndExpenses(saveTag) {
    param = $("#hdParam").val();
    TagNavi = true;
    //页面字段检测
    if (!VerificationHelper.checkFrom("FinanceIncomeAndExpensesDiv")) {
        TagNavi = false;
        return;
    }
    //此处参数必须跟VM一致
    var obj = new Object();

    //获取URL参数
    var ProposalId = $.getUrlParam("ProposalId");

    //获取财产传承
    if (ProposalId != null && ProposalId != "" && ProposalId != undefined) {
        obj["ProposalId"] = ProposalId;
    }

    obj["Id"] = $("#FinanceIncomeAndExpensesDiv #IncomeAndExpensesId").val();
    //obj["ProposalId"] = $("#FinanceIncomeAndExpensesDiv #ProposalId").val();//---工作收入小计;
    //obj["ProposalId"] = 1;//---暂时先用这货
    obj["JobIncome"] = $("#FinanceIncomeAndExpensesDiv #JobIncome").val();
    obj["EndowmentInsurance"] = $("#FinanceIncomeAndExpensesDiv #EndowmentInsurance").val();
    obj["MedicalInsurance"] = $("#FinanceIncomeAndExpensesDiv #MedicalInsurance").val();
    obj["HousingFund"] = $("#FinanceIncomeAndExpensesDiv #HousingFund").val();
    obj["OtherJobIncome"] = $("#FinanceIncomeAndExpensesDiv #OtherJobIncome").val();
    obj["FamilyExpense"] = $("#FinanceIncomeAndExpensesDiv #FamilyExpense").val();//---2.	生活支出
    obj["ChildExpense"] = $("#FinanceIncomeAndExpensesDiv #ChildExpense").val();
    obj["OtherExpense"] = $("#FinanceIncomeAndExpensesDiv #OtherExpense").val();
    obj["Interest"] = $("#FinanceIncomeAndExpensesDiv #Interest").val();//--3理财收入
    obj["CapitalGains"] = $("#FinanceIncomeAndExpensesDiv #CapitalGains").val();
    obj["OtherIncome"] = $("#FinanceIncomeAndExpensesDiv #OtherIncome").val();
    obj["InterestExpense"] = $("#FinanceIncomeAndExpensesDiv #InterestExpense").val();//理财支出
    obj["InsuranceExpense"] = $("#FinanceIncomeAndExpensesDiv #InsuranceExpense").val();
    obj["OtherFinanceExpense"] = $("#FinanceIncomeAndExpensesDiv #OtherFinanceExpense").val();

   

    _ajaxhepler({
        url: "/CompetitionUser/IncomeAndExpenses/SaveIncomeAndExpenses",
        type: "POST",
        async: false,
        dataType: "json",
        data: JSON.stringify(obj),
        contentType: "application/json",
        success: function (data) {
            if (data != null) {
                //刷新当前页
                // location.href = location.href;
                $("#FinanceIncomeAndExpensesDiv #IncomeAndExpensesId").val(data.Id);
                //获取原值
                SaveDefaultValueCommon("FinanceIncomeAndExpensesDiv");
                //弹出成功提示
                if (typeof saveTag == "undefined") {
                    dialogHelper.Success({
                        content: "保存成功！", success: function () {
                            window.location.reload();
                        }
                    });
            
                }
            };
        }
    });
}


/**
 * @name 获取实例
 */
function GetIncomeAndExpenses(ProposalId) {
    _ajaxhepler({
        url: "/CompetitionUser/IncomeAndExpenses/LoadIncomeAndExpensesByProposalId",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: ProposalId,
            rId: Math.random()
        },
        success: function (data) {

            if (data != null) {
                setIncomeAndExpenses(data);
                //获取原值
                SaveDefaultValueCommon("FinanceIncomeAndExpensesDiv");
            } else {

            }
        }
    });
}

function setIncomeAndExpenses(data) {
    var EndowmentInsurance = data.EndowmentInsurance;
    var HousingFund = data.HousingFund;
    $("#FinanceIncomeAndExpensesDiv #IncomeAndExpensesId").val(data.Id);//---工作收入小计;
    $("#FinanceIncomeAndExpensesDiv #ProposalId").val(data.ProposalId);//---工作收入小计;
    $("#FinanceIncomeAndExpensesDiv #JobIncome").val(data.JobIncome);
    $("#FinanceIncomeAndExpensesDiv #EndowmentInsurance").val(EndowmentInsurance);//养老
    $("#FinanceIncomeAndExpensesDiv #MedicalInsurance").val(data.MedicalInsurance);
    $("#FinanceIncomeAndExpensesDiv #HousingFund").val(HousingFund);//住房
    $("#FinanceIncomeAndExpensesDiv #OtherJobIncome").val(data.OtherJobIncome);
    $("#FinanceIncomeAndExpensesDiv #FamilyExpense").val(data.FamilyExpense);;//---2.	生活支出
    $("#FinanceIncomeAndExpensesDiv #ChildExpense").val(data.ChildExpense);
    $("#FinanceIncomeAndExpensesDiv #OtherExpense").val(data.OtherExpense);
    $("#FinanceIncomeAndExpensesDiv #Interest").val(data.Interest);//--3理财收入
    $("#FinanceIncomeAndExpensesDiv #CapitalGains").val(data.CapitalGains);
    $("#FinanceIncomeAndExpensesDiv #OtherIncome").val(data.OtherIncome);
    $("#FinanceIncomeAndExpensesDiv #InterestExpense").val(data.InterestExpense);//理财支出
    $("#FinanceIncomeAndExpensesDiv #InsuranceExpense").val(data.InsuranceExpense);
    $("#FinanceIncomeAndExpensesDiv #OtherFinanceExpense").val(data.OtherFinanceExpense);
    //减：养老保险储蓄 住房公积金储蓄
    $("#FinanceIncomeAndExpensesDiv #EndowmentInsuranceSub").val(data.EndowmentInsurance.toMyFixed(2));//养老
    $("#FinanceIncomeAndExpensesDiv #HousingFundSub").val(data.HousingFund.toMyFixed(2));//住房
    //然后给所有的小计赋值
    var WorkIncome = calcWorkIncome(data.JobIncome, data.EndowmentInsurance, data.MedicalInsurance, data.HousingFund, data.OtherJobIncome);//---工作收入小计;
    $("#FinanceIncomeAndExpensesDiv #workIncome01").val(WorkIncome.toMyFixed(2));//工作收入小计;
    var LiveExpense = calcLiveExpense(data.FamilyExpense, data.ChildExpense, data.OtherExpense);//-2.	生活支出
    $("#FinanceIncomeAndExpensesDiv #liveExpense01").val(LiveExpense.toMyFixed(2));//	生活支出
    var InvestmentIncome = calcInvestmentIncome(data.Interest, data.CapitalGains, data.OtherIncome);//--3理财收入
    $("#FinanceIncomeAndExpensesDiv #investmentIncome01").val(InvestmentIncome.toMyFixed(2));///--3理财收入
    var InvestmentExpense = calcInvestmentExpense(data.InterestExpense, data.InsuranceExpense, data.OtherFinanceExpense);//理财支出
    $("#FinanceIncomeAndExpensesDiv #investmentExpense01").val(InvestmentExpense.toMyFixed(2));//理财支出

    //3.	工作储蓄
    var WolkDeposit01 = WorkIncome * 1 - LiveExpense * 1;
    $("#FinanceIncomeAndExpensesDiv #wolkDeposit01").val(WolkDeposit01.toMyFixed(2));
    //6.	理财储蓄
    var InvestmentDeposit01 = InvestmentIncome * 1 - InvestmentExpense * 1;
    $("#FinanceIncomeAndExpensesDiv #InvestmentDeposit01").val(InvestmentDeposit01.toMyFixed(2));
    //7.	储蓄合计=工作储蓄+理财储蓄
    var TotalDeposit = WolkDeposit01 * 1 + InvestmentDeposit01 * 1;
    $("#FinanceIncomeAndExpensesDiv #TotalDeposit").val(TotalDeposit.toMyFixed(2));
    //9.	自由储蓄=储蓄合计－∑（养老保险储蓄、住房公积金储蓄）
    var FreeMoney = TotalDeposit * 1 - (EndowmentInsurance * 1 + HousingFund * 1);
    $("#FinanceIncomeAndExpensesDiv #FreeMoney").val(FreeMoney.toMyFixed(2))
}

$(function () {
    //先加载数据

    //获取URL参数
    var ProposalId = $.getUrlParam("ProposalId");

    //获取财产传承
    if (ProposalId != null && ProposalId != "" && ProposalId != undefined) {
        GetIncomeAndExpenses(ProposalId);
    }

    $("#FinanceIncomeAndExpensesDiv #btnSave").live("click", function () {
        //添加数据
        AddIncomeAndExpenses();
    });
    //同时绑定下一页事件
    $("#FinanceIncomeAndExpensesDiv #btnNext").live("click", function () {
        //跳转之前先要保存
        AddIncomeAndExpenses(0);
        if (TagNavi) {
            window.location.href = "/CompetitionUser/CashFlow/Index" + param;
        }
    });
    //同时绑定上一页事件
    $("#FinanceIncomeAndExpensesDiv #btnPrev").live("click", function () {
        //跳转之前保存
        AddIncomeAndExpenses(0);
        if (TagNavi) {
            window.location.href = "/CompetitionUser/Liability/Index" + param;
        }
    });
    var data = ["Liability", "IncomeandExpenses", "CashFlow", "FinancialRatios"];
    //navMenuTopReg.Regiclick(data);
});

