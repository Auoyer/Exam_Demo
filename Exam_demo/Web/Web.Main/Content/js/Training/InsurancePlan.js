//***********************************
//生涯规划------------------ 保险规划
//************************************
//切换下拉菜单
var MethodTypeTab = 1;
var param = "";
var TagNavi=true;
//判断是否非法字符
function CheckNum(num) {
    var pattern6 = /^[-]?\d+(\.\d{1,6})?$/;//只能输入两位小数
    if (!pattern6.test(num)) {
        num = 0;
        return num;
    }
    return num;
}
//家庭生活费用实质报酬率1  =1+投资报酬/1+通货膨胀
function calcFamilyExpensesPay(ReturnOnInvestment, InflationRate) {
    var ReturnOnInvestment1 = CheckNum(ReturnOnInvestment)*1;
    var InflationRate1 = CheckNum(InflationRate)*1;
    var sum = 0;
    if (ReturnOnInvestment1 == ReturnOnInvestment && InflationRate1 == InflationRate) {
        sum = (((((ReturnOnInvestment1/100) + 1) / ((InflationRate1/100) + 1)) - 1)*100).toMyFixed(2);

    } else {
        sum = 0;
    }
    return sum;
}
//家庭生活费用实质报酬率1  =1+投资报酬/1+通货膨胀
function calcFamilyExpensesPayOne() {
    var ReturnOnInvestment = $.trim($("#ReturnOnInvestment1").val()) * 1;
    var InflationRate = $.trim($("#InflationRate1").val()) * 1;
    var result = calcFamilyExpensesPay(ReturnOnInvestment, InflationRate)*1;
    $("#FamilyExpensesPay1").val(result.toMyFixed(2));
}
function calcFamilyExpensesPayTwo() {
    var ReturnOnInvestment = $.trim($("#ReturnOnInvestment2").val()) * 1;
    var InflationRate = $.trim($("#InflationRate2").val()) * 1;
    var result = calcFamilyExpensesPay(ReturnOnInvestment, InflationRate) * 1;
    $("#FamilyExpensesPay2").val(result.toMyFixed(2));
}


// 家庭收入实质报酬率1 =1+投资报酬率/1+收入增长率
function calcFamilyIncomePay(ReturnOnInvestment, RevenueGrowth) {
    var ReturnOnInvestment1 = CheckNum(ReturnOnInvestment)*1;
    var RevenueGrowth1 = CheckNum(RevenueGrowth)*1;
    var sum = 0;
    if (ReturnOnInvestment1 == ReturnOnInvestment && RevenueGrowth1 == RevenueGrowth) {
        sum = ((((ReturnOnInvestment/100 + 1) / (RevenueGrowth/100 + 1)) - 1)*100).toMyFixed(2);
    } else {
        sum = 0;
    }
    return sum;
}
function calcFamilyIncomePayOne() {
    var ReturnOnInvestment = $.trim($("#ReturnOnInvestment1").val()) * 1;
    var RevenueGrowth = $.trim($("#RevenueGrowth1").val()) * 1;
    var result = calcFamilyIncomePay(ReturnOnInvestment, RevenueGrowth) * 1;
    $("#FamilyIncomePay1").val(result.toMyFixed(2));
};
function calcFamilyIncomePayTwo() {
    var ReturnOnInvestment = $.trim($("#ReturnOnInvestment2").val()) * 1;
    var RevenueGrowth = $.trim($("#RevenueGrowth2").val()) * 1;
    var result = calcFamilyIncomePay(ReturnOnInvestment, RevenueGrowth) * 1;
    $("#FamilyIncomePay2").val(result.toMyFixed(2));
}
//家庭未来生活费准备年数1
function calcFamilyFutureSaving(RetirementAge2, Age2) {
    var RetirementAge = CheckNum(RetirementAge2)*1;
    var Age = CheckNum(Age2)*1;
    var sum = 0;
    if (Age == Age2 && RetirementAge == RetirementAge2) {
        sum = (RetirementAge2 - Age2);
    } else {
        sum = 0;
    }
    return sum;
};
function calcFamilyFutureSavingOne() {
    var RetirementAge2 = $.trim($("#RetirementAge2").val()) * 1;
    var Age2 = $.trim($("#Age2").val()) * 1;
    var result = calcFamilyFutureSaving(RetirementAge2, Age2) * 1;
    $("#FamilyFutureSaving1").val(result);
    $("#SpouseAge1").val(Age2);
};
function calcFamilyFutureSavingTwo() {
    var RetirementAge2 = $.trim($("#RetirementAge1").val()) * 1;
    var Age2 = $.trim($("#Age1").val()) * 1;
    var result = calcFamilyFutureSaving(RetirementAge2, Age2)*1;
    $("#FamilyFutureSaving2").val(result);
    $("#SpouseAge2").val(Age2);
};
// 调整后家庭生活费用
function calcAdjustMatrimonialFee(MatrimonialFee, AfterAccidentRate) {
    var MatrimonialFee1 = CheckNum(MatrimonialFee)*1;
    var AfterAccidentRate1 = CheckNum(AfterAccidentRate)*1;
    var sum = 0;
    if (MatrimonialFee1 == MatrimonialFee && AfterAccidentRate1 == AfterAccidentRate) {
        sum = ((MatrimonialFee1 * AfterAccidentRate1)/100).toMyFixed(2);
    } else {
        sum = 0;
    }
    return sum;
};

function calcAdjustMatrimonialFeeOne() {
    var MatrimonialFee = $.trim($("#MatrimonialFee1").val()) * 1;
    var AfterAccidentRate = $.trim($("#AfterAccidentRate1").val()) * 1;
    var result = calcAdjustMatrimonialFee(MatrimonialFee, AfterAccidentRate)*1;
    $("#AdjustMatrimonialFee1").val(result.toMyFixed(2));
};
function calcAdjustMatrimonialFeeTwo() {
    var MatrimonialFee = $.trim($("#MatrimonialFee2").val()) * 1;
    var AfterAccidentRate = $.trim($("#AfterAccidentRate2").val()) * 1;
    var result = calcAdjustMatrimonialFee(MatrimonialFee, AfterAccidentRate) * 1;
    $("#AdjustMatrimonialFee2").val(result.toMyFixed(2));
};

// 家庭生活费用现值
function calcMatrimonialFeeNow(FamilyExpensesPay, FamilyFutureSaving, AdjustMatrimonialFee) {
    //这个要用ajax //PV(家庭生活费用实质报酬率,家庭未来生活费准备年数,-调整后家庭生活费用, 0, 1)
    var FamilyExpensesPay1 = CheckNum(FamilyExpensesPay)*1;
    var FamilyFutureSaving1 = CheckNum(FamilyFutureSaving)*1;
    var AdjustMatrimonialFee1 = CheckNum(AdjustMatrimonialFee)*1;
    if (FamilyExpensesPay1 == FamilyExpensesPay && FamilyFutureSaving1 == FamilyFutureSaving && AdjustMatrimonialFee1 == AdjustMatrimonialFee) {
        var sum = 0;
        var rate = FamilyExpensesPay;
        var nper = FamilyFutureSaving;
        var pmt = -AdjustMatrimonialFee;
        var fv = 0;
        var begOfPeriodType = 1;
        sum = CalcPVCommon(rate, nper, pmt, fv, begOfPeriodType);
    } else {
        sum = 0;
    }
    return sum;
};

function calcMatrimonialFeeNowOne() {
    //这个要用ajax //PV(家庭生活费用实质报酬率,家庭未来生活费准备年数,-调整后家庭生活费用, 0, 1)
    var FamilyExpensesPay = $.trim($("#FamilyExpensesPay1").val()) * 1;
    var FamilyFutureSaving = $.trim($("#FamilyFutureSaving1").val()) * 1;
    var AdjustMatrimonialFee = $.trim($("#AdjustMatrimonialFee1").val()) * 1;
    var result = calcMatrimonialFeeNow(FamilyExpensesPay, FamilyFutureSaving, AdjustMatrimonialFee) * 1;
    $("#MatrimonialFeeNow1").val(result.toMyFixed(2));
}
function calcMatrimonialFeeNowTwo() {
    //这个要用ajax //PV(家庭生活费用实质报酬率,家庭未来生活费准备年数,-调整后家庭生活费用, 0, 1)
    var FamilyExpensesPay = $.trim($("#FamilyExpensesPay2").val()) * 1;
    var FamilyFutureSaving = $.trim($("#FamilyFutureSaving2").val()) * 1;
    var AdjustMatrimonialFee = $.trim($("#AdjustMatrimonialFee2").val()) * 1;
    var result = calcMatrimonialFeeNow(FamilyExpensesPay, FamilyFutureSaving, AdjustMatrimonialFee) * 1;
    $("#MatrimonialFeeNow2").val(result.toMyFixed(2));
}

//配偶的个人收入现值/元
function calcSpouseMonthIncome(FamilyIncomePay, FamilyFutureSaving, Income) {
    //配偶的个人收入现值=PV(家庭收入实质报酬率,家庭未来生活费准备年数,配偶的个人年收入)  B16= PV(B8, B10, -B15) 输出；C16= PV(C8, C10, C15) 输出
    var sum = 0;
    var FamilyIncomePay1 = CheckNum(FamilyIncomePay) * 1;
    var FamilyFutureSaving1 = CheckNum(FamilyFutureSaving)*1;
    var Income1 = CheckNum(Income)*1;
    if (FamilyIncomePay1 == FamilyIncomePay && FamilyFutureSaving1 == FamilyFutureSaving && Income1 == Income) {
        var rate = FamilyIncomePay;
        var nper = FamilyFutureSaving;
        var pmt = -Income;
        sum = CalcPVCommon(rate, nper, pmt);
    } else {
        sum = 0;
    }
    return sum;
}
function calcSpouseMonthIncomeOne() {
    //配偶的个人收入现值=PV(家庭收入实质报酬率,家庭未来生活费准备年数,配偶的个人年收入)  B16= PV(B8, B10, -B15) 输出；C16= PV(C8, C10, C15) 输出
    var sum = 0;
    var FamilyExpensesPay = $.trim($("#FamilyIncomePay1").val()) * 1;
    var FamilyFutureSaving = $.trim($("#FamilyFutureSaving1").val()) * 1;
    var Income = $.trim($("#Income1").val()) * 1;
    var result = calcSpouseMonthIncome(FamilyExpensesPay, FamilyFutureSaving, Income)*1;
    $("#SpouseMonthIncome1").val(result.toMyFixed(2))
}
function calcSpouseMonthIncomeTwo() {
    //配偶的个人收入现值=PV(家庭收入实质报酬率,家庭未来生活费准备年数,配偶的个人年收入)  B16= PV(B8, B10, -B15) 输出；C16= PV(C8, C10, C15) 输出
    var sum = 0;
    var FamilyExpensesPay = $.trim($("#FamilyIncomePay2").val()) * 1;
    var FamilyFutureSaving = $.trim($("#FamilyFutureSaving2").val()) * 1;
    var Income = $.trim($("#Income2").val()) * 1;
    var result = calcSpouseMonthIncome(FamilyExpensesPay, FamilyFutureSaving, Income) * 1;
    $("#SpouseMonthIncome2").val(result.toMyFixed(2))
}

//家庭未来生活费用缺口现值/元
function calcFamilyLiveOverdraft(SpouseMonthIncome, MatrimonialFeeNow) {
    var SpouseMonthIncome1 = CheckNum(SpouseMonthIncome)*1;
    var MatrimonialFeeNow1 = CheckNum(MatrimonialFeeNow)*1;
    var sum = 0;
    if (MatrimonialFeeNow1 == MatrimonialFeeNow && SpouseMonthIncome1 == SpouseMonthIncome) {
        //	家庭未来生活费用缺口现值=家庭生活费用现值 -配偶的个人收入现值
        sum = (MatrimonialFeeNow1-SpouseMonthIncome1).toMyFixed(2);
    } else {
        sum = 0;
    }
    return sum;
};
function calcFamilyLiveOverdraftOne() {
    var SpouseMonthIncome = $.trim($("#SpouseMonthIncome1").val()) * 1;
    var MatrimonialFeeNow = $.trim($("#MatrimonialFeeNow1").val()) * 1;
    var result = calcFamilyLiveOverdraft(SpouseMonthIncome, MatrimonialFeeNow.toMyFixed(2)) * 1;
    $("#FamilyLiveOverdraft1").val(result.toMyFixed(2));
}
function calcFamilyLiveOverdraftTwo() {
    var SpouseMonthIncome = $.trim($("#SpouseMonthIncome2").val()) * 1;
    var MatrimonialFeeNow = $.trim($("#MatrimonialFeeNow2").val()) * 1;
    var result = calcFamilyLiveOverdraft(SpouseMonthIncome, MatrimonialFeeNow) * 1;
    $("#FamilyLiveOverdraft2").val(result.toMyFixed(2));
}

//-------------------------------------------------实体方法在下面
// 遗属需求法应有的寿险保额1
function calcRelativeFinancial(FamilyLiveOverdraft, ReserveFund, EduAmount, PensionFunds, DeathExpense, LoanBalance, EarningAssets) {
    //遗属需求法应有的寿险保额=∑(家庭未来生活费用缺口现值,紧急备用金现值，教育金现值，养老基金现值，临终及丧葬支出现值，目前贷款余额)-家庭生息资产
    var FamilyLiveOverdraft1 = CheckNum(FamilyLiveOverdraft)*1;
    var ReserveFund1 = CheckNum(ReserveFund)*1;
    var EduAmount1 = CheckNum(EduAmount)*1;
    var PensionFunds1 = CheckNum(PensionFunds)*1;
    var DeathExpense1 = CheckNum(DeathExpense)*1;
    var LoanBalance1 = CheckNum(LoanBalance)*1;
    var EarningAssets1 = CheckNum(EarningAssets)*1;
    var sum = 0;
    if (FamilyLiveOverdraft1 == FamilyLiveOverdraft && ReserveFund1 == ReserveFund && EduAmount1 == EduAmount && PensionFunds1 == PensionFunds && DeathExpense1 == DeathExpense && LoanBalance1 == LoanBalance && EarningAssets1 == EarningAssets) {
        //	家庭未来生活费用缺口现值=配偶的个人收入现值-家庭生活费用现值 
        sum = ((FamilyLiveOverdraft1 + ReserveFund1 + EduAmount1 + PensionFunds1 + DeathExpense1 + LoanBalance1) - EarningAssets1).toMyFixed(2);
    } else {
        sum = 0;
    }
    return sum;


}

//保险需求额度/元1
function calcInsureNeedCash() {
    // calcRelativeFinancial
}
/// <summary>
/// 缺口额度/元
/// </summary>
function calcGapCash(InsureNeedCash, InsuranceAmount) {
    var InsureNeedCash1 = CheckNum(InsureNeedCash)*1;
    var InsuranceAmount1 = CheckNum(InsuranceAmount)*1;
    var sum = 0;
    if (InsureNeedCash1 == InsureNeedCash && InsuranceAmount1 == InsuranceAmount) {
        //	缺口额度=保险需求额度-已有额度
        sum = (InsureNeedCash1 - InsuranceAmount1).toMyFixed(2);
    } else {
        sum = 0;
    }
    return sum;
}
/// <summary>
/// 欠缺额度/元
/// </summary>
function calcBalanceCash(GapCash, BudgetAmount, SupplementaryQuota) {
    var GapCash1 = CheckNum(GapCash)*1;
    var BudgetAmount1 = CheckNum(BudgetAmount)*1;
    var SupplementaryQuota1 = CheckNum(SupplementaryQuota)*1;
    var sum = 0;
    if (GapCash1 == GapCash && BudgetAmount1 == BudgetAmount && SupplementaryQuota1 == SupplementaryQuota) {
        //	欠缺额度=缺口额度-补充额度 
        sum = (GapCash1 - SupplementaryQuota1).toMyFixed(2);
    } else {
        sum = 0;
    }
    return sum;
}
//***********************************
//生命法则计算方式
//***********************************
//	离退休年数=预计退休年龄-被保险人年龄 
function calcPredictRetirementAgeLIfe(Age1Life, RetirementAge1Life) {
    var Age1Life1 = CheckNum(Age1Life)*1;
    var RetirementAge1Life1 = CheckNum(RetirementAge1Life)*1;
    var sum = 0;
    if (Age1Life1 == Age1Life && RetirementAge1Life1 == RetirementAge1Life) {

        sum = (RetirementAge1Life1 - Age1Life1).toMyFixed(2);
    } else {
        sum = 0;
    }
    return sum;
};

//未来工作期间收入现值=PV( (1+投资报酬率)/(1+收入增长率)－1，离退休年数，- 当前个人年收入) 
function calcFutureIncomeLife(ReturnOnInvestment1Life, RevenueGrowth1Life, PredictRetirementAgeLIfe, Income1Life) {
    var ReturnOnInvestment1Life1 = CheckNum(ReturnOnInvestment1Life)*1;
    var RevenueGrowth1Life1 = CheckNum(RevenueGrowth1Life)*1;
    var PredictRetirementAgeLIfe1 = CheckNum(PredictRetirementAgeLIfe)*1;
    var Income1Life1 = CheckNum(Income1Life)*1;
    var sum = 0;
    if (ReturnOnInvestment1Life1 == ReturnOnInvestment1Life && RevenueGrowth1Life1 == RevenueGrowth1Life && PredictRetirementAgeLIfe1 == PredictRetirementAgeLIfe && Income1Life1 == Income1Life) {
        var rate = (((1 + ReturnOnInvestment1Life1/100) / (1 + RevenueGrowth1Life1/100) - 1)*100).toMyFixed(2);
        var nper = PredictRetirementAgeLIfe1;
        var pmt = -Income1Life1;
        //调用PV的公共方法
        sum = CalcPVCommon(rate, nper, pmt);
    } else {
        sum = 0;
    }
    return sum;
};

//未来工作期间支出现值=PV( ((1+投资报酬率)/(1+年通货膨胀率))－1,离退休年数，- 当前个人年支出)   B10=PV((1+B4)/(1+B9)-1,B3,-B8)
function calcFutureExpend(ReturnOnInvestment1Life, InflationRate1Life, PredictRetirementAgeLIfe, Expenditure) {
    var ReturnOnInvestment1Life1 = CheckNum(ReturnOnInvestment1Life)*1;
    var InflationRate1Life1 = CheckNum(InflationRate1Life)*1;
    var PredictRetirementAgeLIfe1 = CheckNum(PredictRetirementAgeLIfe)*1;
    var Expenditure1 = CheckNum(Expenditure)*1;
    var sum = 0;
    if (ReturnOnInvestment1Life1 == ReturnOnInvestment1Life && InflationRate1Life1 == InflationRate1Life && PredictRetirementAgeLIfe1 == PredictRetirementAgeLIfe && Expenditure1 == Expenditure) {
        var rate = (((1 + ReturnOnInvestment1Life1/100) / (1 + InflationRate1Life1/100) - 1)*100).toMyFixed(2);
        var nper = PredictRetirementAgeLIfe1;
        var pmt = -Expenditure1;
        //调用PV的公共方法
        sum = CalcPVCommon(rate, nper, pmt);
    } else {
        sum = 0;
    }
    return sum;

}
//个人未来净收入的年金现值= 未来工作期间支出现值-未来工作期间收入现值  B11=B10-B7  输出
function calcFutureAnnuityIncome(FutureIncomeLife, FutureExpend) {
    var FutureIncomeLife1 = CheckNum(FutureIncomeLife)*1;
    var FutureExpend1 = CheckNum(FutureExpend)*1;
    var sum = 0;
    if (FutureIncomeLife1 == FutureIncomeLife && FutureExpend1 == FutureExpend) {

        sum = (FutureExpend1-FutureIncomeLife1).toMyFixed(2);
    } else {
        sum = 0;
    }
    return sum;
}



//加载数据
function LoadInsurancePlan(ProposalId) {
    _ajaxhepler({
        url: "/CompetitionUser/InsurancePlan/LoadInsurancePlanByProposalId",
        type: "POST",
        async: false,
        dataType: "json",
        data: {
            ProposalId: ProposalId,
            rId: Math.random
        },
        success: function (data) {
            if (data != null) {
                if (data.Analysis != null) {
                    SetFinanceInsurancePlanDivVal(data);
                } else {
                    setInsurancePlanClearZero(data);
                }
                SaveDefaultValueCommon("FinanceInsurancePlanDiv");//保存原值。和新值要做一个对比的
            }
        }
    });
};
function setInsurancePlanClearZero(data) {
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
    var MonthMoney = data.MonthMoney == 0 ? "" : data.MonthMoney.toMyFixed(2);//每月可支配金额
    var UserableAsset = data.UserableAsset == 0 ? "" : data.UserableAsset.toMyFixed(2); //可用资产
    var Age1 = data.Age1==0?"":data.Age1;
    var Age2 = data.Age2==0?"":data.Age2
    var RetirementAge1 = data.RetirementAge1==0?"":data.RetirementAge1
    var RetirementAge2 = data.RetirementAge2==0?"":data.RetirementAge2
    var ReturnOnInvestment1 = data.ReturnOnInvestment1==0?"":data.ReturnOnInvestment1
    var ReturnOnInvestment2 = data.ReturnOnInvestment2==0?"":data.ReturnOnInvestment2
    var InflationRate1 = data.InflationRate1==0?"":data.InflationRate1
    var InflationRate2 = data.InflationRate2==0?"":data.InflationRate2
    var RevenueGrowth1 = data.RevenueGrowth1==0?"":data.RevenueGrowth1
    var RevenueGrowth2 = data.RevenueGrowth2==0?"":data.RevenueGrowth2
    var FamilyExpensesPay1 = data.FamilyExpensesPay1==0?"":data.FamilyExpensesPay1
    var FamilyExpensesPay2 = data.FamilyExpensesPay2==0?"":data.FamilyExpensesPay2
    var FamilyIncomePay1 = data.FamilyIncomePay1==0?"":data.FamilyIncomePay1
    var FamilyIncomePay2 = data.FamilyIncomePay2==0?"":data.FamilyIncomePay2
    var SpouseAge1 = data.SpouseAge1==0?"":data.SpouseAge1
    var SpouseAge2 = data.SpouseAge2==0?"":data.SpouseAge2
    var FamilyFutureSaving1 = data.FamilyFutureSaving1==0?"":data.FamilyFutureSaving1
    var FamilyFutureSaving2 = data.FamilyFutureSaving2==0?"":data.FamilyFutureSaving2
    var MatrimonialFee1 = data.MatrimonialFee1==0?"":data.MatrimonialFee1
    var MatrimonialFee2 = data.MatrimonialFee2==0?"":data.MatrimonialFee2
    var AfterAccidentRate1 = data.AfterAccidentRate1==0?"":data.AfterAccidentRate1
    var AfterAccidentRate2 = data.AfterAccidentRate2==0?"":data.AfterAccidentRate2
    var AdjustMatrimonialFee1 = data.AdjustMatrimonialFee1 == 0 ? "" : data.AdjustMatrimonialFee1;
    var AdjustMatrimonialFee2 = data.AdjustMatrimonialFee2 == 0 ? "" : data.AdjustMatrimonialFee2;
    var MatrimonialFeeNow1 = data.MatrimonialFeeNow1 == 0 ? "" : data.MatrimonialFeeNow1;
    var MatrimonialFeeNow2 = data.MatrimonialFeeNow2 == 0 ? "" : data.MatrimonialFeeNow2;
    var Income1 = data.Income1 == 0 ? "" : data.Income1;
    var Income2 = data.Income2 == 0 ? "" : data.Income2;
    var SpouseMonthIncome1 = data.SpouseMonthIncome1 == 0 ? "" : data.SpouseMonthIncome1;
    var SpouseMonthIncome2 = data.SpouseMonthIncome2 == 0 ? "" : data.SpouseMonthIncome2;
    var FamilyLiveOverdraft1 = data.FamilyLiveOverdraft1 == 0 ? "" : data.FamilyLiveOverdraft1;
    var FamilyLiveOverdraft2 = data.FamilyLiveOverdraft2 == 0 ? "" : data.FamilyLiveOverdraft2;
    var ReserveFund1 = data.ReserveFund1 == 0 ? "" : data.ReserveFund1.toMyFixed(2); //紧急备用金现值---数据来源现金规划保留规模
    //if (data.ReserveFund1 != 0) {
    //    ReserveFund1 = data.ReserveFund1.toMyFixed(2);
    //    $("#ReserveFund1").attr("readonly", "readonly").css("disabled");
    //}
    var ReserveFund2 = data.ReserveFund2 == 0 ? "" : data.ReserveFund2.toMyFixed(2);
    //if (data.ReserveFund2 != 0) {
    //    ReserveFund2 = data.ReserveFund2.toMyFixed(2);
    //    $("#ReserveFund2").attr("readonly", "readonly").css("disabled");
    //}
    var EduAmount1 = data.EduAmount1 == 0 ? "" : data.EduAmount1.toMyFixed(2); //教育金现值--数据来源教育规划或输
    //if (data.EduAmount1 != 0) {
    //    EduAmount1 = data.EduAmount1.toMyFixed(2);
    //    $("#EduAmount1").attr("readonly", "readonly").css("disabled");
    //}
    var EduAmount2 = data.EduAmount2 == 0 ? "" : data.EduAmount2.toMyFixed(2);
    //if (data.EduAmount2 != 0) {
    //    EduAmount2 = data.EduAmount2.toMyFixed(2);
    //    $("#EduAmount2").attr("readonly", "readonly").css("disabled");
    //}
    var PensionFunds1 = data.PensionFunds1 == 0 ? "" : data.PensionFunds1.toMyFixed(2); //养老基金现值/元--数据来源退休规划或输入
    //if (data.PensionFunds1 != 0) {
    //    PensionFunds1 = data.PensionFunds1.toMyFixed(2);
    //    $("#PensionFunds1").attr("readonly", "readonly").css("disabled");
    //}
    var PensionFunds2 = data.PensionFunds2 == 0 ? "" : data.PensionFunds2.toMyFixed(2);
    //if (data.PensionFunds2 != 0) {
    //    PensionFunds2 = data.PensionFunds2.toMyFixed(2);
    //    $("#PensionFunds2").attr("readonly", "readonly").css("disabled");
    //}
    var DeathExpense1 = data.DeathExpense1 == 0 ? "" : data.DeathExpense1;
    var DeathExpense2 = data.DeathExpense2 == 0 ? "" : data.DeathExpense2;
    var LoanBalance1 = data.LoanBalance1 == 0 ? "" : data.LoanBalance1;
    var LoanBalance2 = data.LoanBalance2 == 0 ? "" : data.LoanBalance2;
    var EarningAssets1 = data.EarningAssets1 == 0 ? "" : data.EarningAssets1;
    var EarningAssets2 = data.EarningAssets2 == 0 ? "" : data.EarningAssets2;
    var RelativeFinancial1 = data.RelativeFinancial1 == 0 ? "" : data.RelativeFinancial1;
    var RelativeFinancial2 = data.RelativeFinancial2 == 0 ? "" : data.RelativeFinancial2;
    var InsureName1 = data.InsureName1 ;
    var InsureName2 = data.InsureName2;
    var InsureNeedCash1 = data.InsureNeedCash1 == 0 ? "" : data.InsureNeedCash1;
    var InsureNeedCash2 = data.InsureNeedCash2 == 0 ? "" : data.InsureNeedCash2;
    var InsuranceAmount1 = data.InsuranceAmount1 == 0 ? "" : data.InsuranceAmount1;
    var InsuranceAmount2 = data.InsuranceAmount2 == 0 ? "" : data.InsuranceAmount2;
    var GapCash1 = data.GapCash1 == 0 ? "" : data.GapCash1;
    var GapCash2 = data.GapCash2 == 0 ? "" : data.GapCash2;
    var BudgetAmount1 = data.BudgetAmount1 == 0 ? "" : data.BudgetAmount1;
    var BudgetAmount2 = data.BudgetAmount2 == 0 ? "" : data.BudgetAmount2;
    var SupplementaryQuota1 = data.SupplementaryQuota1 == 0 ? "" : data.SupplementaryQuota1;
    var SupplementaryQuota2 = data.SupplementaryQuota2 == 0 ? "" : data.SupplementaryQuota2;
    var BalanceCash1 = data.BalanceCash1 == 0 ? "" : data.BalanceCash1;
    var BalanceCash2 = data.BalanceCash2 == 0 ? "" : data.BalanceCash2;
    var Analysis = data.Analysis == 0 ? "" : data.Analysis;
    var Expenditure = data.Expenditure==0?"":data.Expenditure;
    var FutureExpend = data.FutureExpend==0?"":data.FutureExpend;//未来给人支出
    var PredictRetirementAgeLIfe = data.PredictRetirementAgeLIfe==0?"":data.PredictRetirementAgeLIfe;
    var FutureIncomeLife = data.FutureIncomeLife == 0 ? "" : data.FutureIncomeLife;//未来个人收入
    var FutureAnnuityIncome = data.FutureAnnuityIncome == 0 ? "" : data.FutureAnnuityIncome;//个人未来净收入的年金现值/元
    $("#FinanceInsurancePlanDiv #InsurancePlanId").val(Id);
    $("#FinanceInsurancePlanDiv #ProposalId").val(ProposalId);
    $("#FinanceInsurancePlanDiv #MethodTypeId").val(MethodTypeId);
    $("#FinanceInsurancePlanDiv #TabZH").val(MethodTypeId);
    //判断保险规划的需求算法1-遗属需求法 ，2-生命需求法
    $("#FinanceInsurancePlanDiv #monthMoney").val(MonthMoney);//每月可用资金
    $("#FinanceInsurancePlanDiv #UserableAsset").val(UserableAsset);//可用资产
    $("#FinanceInsurancePlanDiv #Analysis").val(Analysis);//--客户财务情况分析
    if (MethodTypeId == 1) {

        $("#FinanceInsurancePlanDiv #InsureName").val(InsureName);
        $("#FinanceInsurancePlanDiv #InsureName1Life").val(InsureName);
        $("#FinanceInsurancePlanDiv #SpouseName").val(SpouseName);
        //这地方特殊两边通用
        $("#FinanceInsurancePlanDiv #Age1").val(Age);
        $("#FinanceInsurancePlanDiv #SpouseAge1").val(Age2);  //---------配偶当前年龄-男的写女的
        $("#FinanceInsurancePlanDiv #SpouseAge2").val(Age);
        $("#FinanceInsurancePlanDiv #Age1Life").val(Age);//被保险人年龄/岁


        $("#FinanceInsurancePlanDiv #Age2").val(Age2);
        $("#FinanceInsurancePlanDiv #RetirementAge1").val(RetirementAge1);
        $("#FinanceInsurancePlanDiv #RetirementAge2").val(RetirementAge2);
        $("#FinanceInsurancePlanDiv #ReturnOnInvestment1").val(ReturnOnInvestment1);
        $("#FinanceInsurancePlanDiv #ReturnOnInvestment2").val(ReturnOnInvestment2);//----人民币固定存款
        $("#FinanceInsurancePlanDiv #InflationRate1").val(InflationRate1);
        $("#FinanceInsurancePlanDiv #InflationRate2").val(InflationRate2);
        $("#FinanceInsurancePlanDiv #RevenueGrowth1").val(RevenueGrowth1);
        $("#FinanceInsurancePlanDiv #RevenueGrowth2").val(RevenueGrowth2);
        $("#FinanceInsurancePlanDiv #FamilyExpensesPay1").val(FamilyExpensesPay1);//家庭生活费用实质报酬率
        $("#FinanceInsurancePlanDiv #FamilyExpensesPay2").val(FamilyExpensesPay2);
        $("#FinanceInsurancePlanDiv #FamilyIncomePay1").val(FamilyIncomePay1);
        $("#FinanceInsurancePlanDiv #FamilyIncomePay2").val(FamilyIncomePay2);

        $("#FinanceInsurancePlanDiv #FamilyFutureSaving1").val(FamilyFutureSaving1);//家庭未来生活费准备年数/年
        $("#FinanceInsurancePlanDiv #FamilyFutureSaving2").val(FamilyFutureSaving2);
        $("#FinanceInsurancePlanDiv #MatrimonialFee1").val(MatrimonialFee1);//-------当前的家庭生活费用/元
        $("#FinanceInsurancePlanDiv #MatrimonialFee2").val(MatrimonialFee2);
        $("#FinanceInsurancePlanDiv #AfterAccidentRate1").val(AfterAccidentRate1);//-----保险事故发生后支出调整率
        $("#FinanceInsurancePlanDiv #AfterAccidentRate2").val(AfterAccidentRate2);
        $("#FinanceInsurancePlanDiv #AdjustMatrimonialFee1").val(AdjustMatrimonialFee1);//调整后家庭年生活费用/元
        $("#FinanceInsurancePlanDiv #AdjustMatrimonialFee2").val(AdjustMatrimonialFee2);
        $("#FinanceInsurancePlanDiv #MatrimonialFeeNow1").val(MatrimonialFeeNow1);//------家庭生活费用现值/元
        $("#FinanceInsurancePlanDiv #MatrimonialFeeNow2").val(MatrimonialFeeNow2);
        $("#FinanceInsurancePlanDiv #Income1").val(Income1);//配偶的个人年收入/元
        $("#FinanceInsurancePlanDiv #Income2").val(Income2);
        $("#FinanceInsurancePlanDiv #SpouseMonthIncome1").val(SpouseMonthIncome1);//配偶的个人收入现值/元
        $("#FinanceInsurancePlanDiv #SpouseMonthIncome2").val(SpouseMonthIncome2);
        $("#FinanceInsurancePlanDiv #FamilyLiveOverdraft1").val(FamilyLiveOverdraft1)//家庭未来生活费用缺口现值/元
        $("#FinanceInsurancePlanDiv #FamilyLiveOverdraft2").val(FamilyLiveOverdraft2);
        $("#FinanceInsurancePlanDiv #ReserveFund1").val(ReserveFund1);//紧急备用金现值/元
        $("#FinanceInsurancePlanDiv #ReserveFund2").val(ReserveFund2);
        $("#FinanceInsurancePlanDiv #EduAmount1").val(EduAmount1);//--教育金现值/元
        $("#FinanceInsurancePlanDiv #EduAmount2").val(EduAmount2);
        $("#FinanceInsurancePlanDiv #PensionFunds1").val(PensionFunds1);//养老基金现值/元
        $("#FinanceInsurancePlanDiv #PensionFunds2").val(PensionFunds2);
        $("#FinanceInsurancePlanDiv #DeathExpense1").val(DeathExpense1);//临终及丧葬支出现值/元
        $("#FinanceInsurancePlanDiv #DeathExpense2").val(DeathExpense2);
        $("#FinanceInsurancePlanDiv #LoanBalance1").val(LoanBalance1);//目前贷款余额/元
        $("#FinanceInsurancePlanDiv #LoanBalance2").val(LoanBalance2);
        $("#FinanceInsurancePlanDiv #EarningAssets1").val(EarningAssets1);//家庭生息资产/元
        $("#FinanceInsurancePlanDiv #EarningAssets2").val(EarningAssets2);
        $("#FinanceInsurancePlanDiv #RelativeFinancial1").val(RelativeFinancial1);//遗属需求法应有的寿险保额/元
        $("#FinanceInsurancePlanDiv #RelativeFinancial2").val(RelativeFinancial2);
        $("#FinanceInsurancePlanDiv #InsureName1").val(InsureName);//-------被保险人
        $("#FinanceInsurancePlanDiv #InsureName2").val(SpouseName);
        $("#FinanceInsurancePlanDiv #InsureNeedCash1").val(InsureNeedCash1);//保险需求额度/元	
        $("#FinanceInsurancePlanDiv #InsureNeedCash2").val(InsureNeedCash2);
        $("#FinanceInsurancePlanDiv #InsuranceAmount1").val(InsuranceAmount1);//----*已有额度/元
        $("#FinanceInsurancePlanDiv #InsuranceAmount2").val(InsuranceAmount2);
        $("#FinanceInsurancePlanDiv #GapCash1").val(GapCash1);//缺口额度/元
        $("#FinanceInsurancePlanDiv #GapCash2").val(GapCash2);
        $("#FinanceInsurancePlanDiv #BudgetAmount1").val(BudgetAmount1);//----*预算金额/元
        $("#FinanceInsurancePlanDiv #BudgetAmount2").val(BudgetAmount2);
        $("#FinanceInsurancePlanDiv #SupplementaryQuota1").val(SupplementaryQuota1);//*补充额度/元
        $("#FinanceInsurancePlanDiv #SupplementaryQuota2").val(SupplementaryQuota2);
        $("#FinanceInsurancePlanDiv #BalanceCash1").val(BalanceCash1);//欠缺额度/元
        $("#FinanceInsurancePlanDiv #BalanceCash2").val(BalanceCash2);

        //   SaveDefaultValueCommon("InsuranceOne");//保存原值。和新值要做一个对比的
    } else {


        //年龄共用
        $("#FinanceInsurancePlanDiv #Age1").val(Age);
        $("#FinanceInsurancePlanDiv #Age1Life").val(Age);//被保险人年龄/岁
        $("#FinanceInsurancePlanDiv #SpouseAge2").val(Age);  //---------配偶当前年龄-男的写女的

        $("#FinanceInsurancePlanDiv #RetirementAge1Life").val(RetirementAge1);//*预计退休年龄/岁
        $("#FinanceInsurancePlanDiv #PredictRetirementAgeLIfe").val(PredictRetirementAgeLIfe);//离退休年数/年
        $("#FinanceInsurancePlanDiv #ReturnOnInvestment1Life").val(ReturnOnInvestment1);//*投资报酬率
        $("#FinanceInsurancePlanDiv #Income1Life").val(Income1);//当前个人年收入/元
        $("#FinanceInsurancePlanDiv #RevenueGrowth1Life").val(RevenueGrowth1);//收入增长率
        $("#FinanceInsurancePlanDiv #FutureIncomeLife").val(FutureIncomeLife);//未来工作期间收入现值/元
        $("#FinanceInsurancePlanDiv #Expenditure").val(Expenditure);//-个人年收入支出
        $("#FinanceInsurancePlanDiv #InflationRate1Life").val(InflationRate1);//年通货膨胀率
        $("#FinanceInsurancePlanDiv #FutureExpend").val(FutureExpend);//未来工作期间支出现值/元
        $("#FinanceInsurancePlanDiv #FutureAnnuityIncome").val(FutureAnnuityIncome);//个人未来净收入的年金现值/元
        $("#FinanceInsurancePlanDiv #FutureAnnuityIncomeSub").val(FutureAnnuityIncome);//弥补收入应有的寿险保额/元
        //---------------------被保险人

        $("#FinanceInsurancePlanDiv #InsureNeedCash1Life").val(FutureAnnuityIncome);//保险需求额度/元
        $("#FinanceInsurancePlanDiv #InsuranceAmount1Life").val(InsuranceAmount1);//已有额度/元
        $("#FinanceInsurancePlanDiv #GapCash1Life").val(GapCash1);//缺口额度/元
        $("#FinanceInsurancePlanDiv #BudgetAmount1Life").val(BudgetAmount1);//预算金额/元

        $("#FinanceInsurancePlanDiv #SupplementaryQuota1Life").val(SupplementaryQuota1);//-补充额度/元
        $("#FinanceInsurancePlanDiv #BalanceCash1Life").val(BalanceCash1);//欠缺额度/元

        $("#FinanceInsurancePlanDiv #InsureName").val(InsureName);//被保险人姓名
        $("#FinanceInsurancePlanDiv #InsureName1Life").val(InsureName);//被保险人姓名
        $("#FinanceInsurancePlanDiv #InsureName1").val(InsureName);//-------被保险人

        //  SaveDefaultValueCommon("InsuranceTwo");//保存原值。和新值要做一个对比的
    }


}



//正常赋值
function SetFinanceInsurancePlanDivVal(data) {
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
    var ReserveFund1 = 0; //紧急备用金现值---数据来源现金规划保留规模
    //if (data.ReserveFund1 != 0) {
        ReserveFund1 = data.ReserveFund1.toMyFixed(2);
       // $("#ReserveFund1").attr("readonly", "readonly").addClass("disabled");
    //}
    var ReserveFund2 = 0;
    //if (data.ReserveFund2 != 0) {
        ReserveFund2 = data.ReserveFund2.toMyFixed(2);
       // $("#ReserveFund2").attr("readonly", "readonly").addClass("disabled");
    //}
    var EduAmount1 = 0; //教育金现值--数据来源教育规划或输
    //if (data.EduAmount1 != 0) {
        EduAmount1 = data.EduAmount1.toMyFixed(2);
     //   $("#EduAmount1").attr("readonly", "readonly").addClass("disabled");
    //}
    var EduAmount2 =0;
    //if (data.EduAmount2 != 0) {
        EduAmount2 = data.EduAmount2.toMyFixed(2);
       // $("#EduAmount2").attr("readonly", "readonly").addClass("disabled");
    //}
    var PensionFunds1 = 0; //养老基金现值/元--数据来源退休规划或输入
    //if (data.PensionFunds1 != 0) {
        PensionFunds1 = data.PensionFunds1.toMyFixed(2);
      //  $("#PensionFunds1").attr("readonly", "readonly").addClass("disabled");
    //}
    var PensionFunds2 =0;
    //if (data.PensionFunds2 != 0) {
        PensionFunds2 = data.PensionFunds2.toMyFixed(2);
      //  $("#PensionFunds2").attr("readonly", "readonly").addClass("disabled");
    //}
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
    $("#FinanceInsurancePlanDiv #InsurancePlanId").val(Id);
    $("#FinanceInsurancePlanDiv #ProposalId").val(ProposalId);
    $("#FinanceInsurancePlanDiv #MethodTypeId").val(MethodTypeId);
    $("#FinanceInsurancePlanDiv #TabZH").val(MethodTypeId)
    //判断保险规划的需求算法1-遗属需求法 ，2-生命需求法
    $("#FinanceInsurancePlanDiv #monthMoney").val(MonthMoney.toMyFixed(2));//每月可用资金
    $("#FinanceInsurancePlanDiv #UserableAsset").val(UserableAsset.toMyFixed(2));//可用资产
    $("#FinanceInsurancePlanDiv #Analysis").val(Analysis);//--客户财务情况分析
    if (MethodTypeId == 1) {

        $("#FinanceInsurancePlanDiv #InsureName").val(InsureName);
        $("#FinanceInsurancePlanDiv #InsureName1Life").val(InsureName);
        $("#FinanceInsurancePlanDiv #SpouseName").val(SpouseName);
        //这地方特殊两边通用
        $("#FinanceInsurancePlanDiv #Age1").val(Age);
        $("#FinanceInsurancePlanDiv #SpouseAge1").val(Age2);  //---------配偶当前年龄-男的写女的
        $("#FinanceInsurancePlanDiv #SpouseAge2").val(Age);
        $("#FinanceInsurancePlanDiv #Age1Life").val(Age);//被保险人年龄/岁


        $("#FinanceInsurancePlanDiv #Age2").val(Age2)
        $("#FinanceInsurancePlanDiv #RetirementAge1").val(RetirementAge1);
        $("#FinanceInsurancePlanDiv #RetirementAge2").val(RetirementAge2);
        $("#FinanceInsurancePlanDiv #ReturnOnInvestment1").val(ReturnOnInvestment1);
        $("#FinanceInsurancePlanDiv #ReturnOnInvestment2").val(ReturnOnInvestment2);//----人民币固定存款
        $("#FinanceInsurancePlanDiv #InflationRate1").val(InflationRate1);
        $("#FinanceInsurancePlanDiv #InflationRate2").val(InflationRate2);
        $("#FinanceInsurancePlanDiv #RevenueGrowth1").val(RevenueGrowth1);
        $("#FinanceInsurancePlanDiv #RevenueGrowth2").val(RevenueGrowth2);
        $("#FinanceInsurancePlanDiv #FamilyExpensesPay1").val(FamilyExpensesPay1.toMyFixed(2));//家庭生活费用实质报酬率
        $("#FinanceInsurancePlanDiv #FamilyExpensesPay2").val(FamilyExpensesPay2.toMyFixed(2));
        $("#FinanceInsurancePlanDiv #FamilyIncomePay1").val(FamilyIncomePay1.toMyFixed(2));
        $("#FinanceInsurancePlanDiv #FamilyIncomePay2").val(FamilyIncomePay2.toMyFixed(2));

        $("#FinanceInsurancePlanDiv #FamilyFutureSaving1").val(FamilyFutureSaving1);//家庭未来生活费准备年数/年
        $("#FinanceInsurancePlanDiv #FamilyFutureSaving2").val(FamilyFutureSaving2);
        $("#FinanceInsurancePlanDiv #MatrimonialFee1").val(MatrimonialFee1.toMyFixed(2));//-------当前的家庭生活费用/元
        $("#FinanceInsurancePlanDiv #MatrimonialFee2").val(MatrimonialFee2.toMyFixed(2));
        $("#FinanceInsurancePlanDiv #AfterAccidentRate1").val(AfterAccidentRate1);//-----保险事故发生后支出调整率
        $("#FinanceInsurancePlanDiv #AfterAccidentRate2").val(AfterAccidentRate2);
        $("#FinanceInsurancePlanDiv #AdjustMatrimonialFee1").val(AdjustMatrimonialFee1.toMyFixed(2));//调整后家庭年生活费用/元
        $("#FinanceInsurancePlanDiv #AdjustMatrimonialFee2").val(AdjustMatrimonialFee2.toMyFixed(2));
        $("#FinanceInsurancePlanDiv #MatrimonialFeeNow1").val(MatrimonialFeeNow1.toMyFixed(2));//------家庭生活费用现值/元
        $("#FinanceInsurancePlanDiv #MatrimonialFeeNow2").val(MatrimonialFeeNow2.toMyFixed(2));
        $("#FinanceInsurancePlanDiv #Income1").val(Income1.toMyFixed(2));//配偶的个人年收入/元
        $("#FinanceInsurancePlanDiv #Income2").val(Income2.toMyFixed(2));
        $("#FinanceInsurancePlanDiv #SpouseMonthIncome1").val(SpouseMonthIncome1.toMyFixed(2));//配偶的个人收入现值/元
        $("#FinanceInsurancePlanDiv #SpouseMonthIncome2").val(SpouseMonthIncome2.toMyFixed(2));
        $("#FinanceInsurancePlanDiv #FamilyLiveOverdraft1").val(FamilyLiveOverdraft1.toMyFixed(2))//家庭未来生活费用缺口现值/元
        $("#FinanceInsurancePlanDiv #FamilyLiveOverdraft2").val(FamilyLiveOverdraft2.toMyFixed(2));
        $("#FinanceInsurancePlanDiv #ReserveFund1").val(ReserveFund1);//紧急备用金现值/元
        $("#FinanceInsurancePlanDiv #ReserveFund2").val(ReserveFund2);
        $("#FinanceInsurancePlanDiv #EduAmount1").val(EduAmount1);//--教育金现值/元
        $("#FinanceInsurancePlanDiv #EduAmount2").val(EduAmount2);
        $("#FinanceInsurancePlanDiv #PensionFunds1").val(PensionFunds1);//养老基金现值/元
        $("#FinanceInsurancePlanDiv #PensionFunds2").val(PensionFunds2);
        $("#FinanceInsurancePlanDiv #DeathExpense1").val(DeathExpense1.toMyFixed(2));//临终及丧葬支出现值/元
        $("#FinanceInsurancePlanDiv #DeathExpense2").val(DeathExpense2.toMyFixed(2));
        $("#FinanceInsurancePlanDiv #LoanBalance1").val(LoanBalance1.toMyFixed(2));//目前贷款余额/元
        $("#FinanceInsurancePlanDiv #LoanBalance2").val(LoanBalance2.toMyFixed(2));
        $("#FinanceInsurancePlanDiv #EarningAssets1").val(EarningAssets1.toMyFixed(2));//家庭生息资产/元
        $("#FinanceInsurancePlanDiv #EarningAssets2").val(EarningAssets2.toMyFixed(2));
        $("#FinanceInsurancePlanDiv #RelativeFinancial1").val(RelativeFinancial1.toMyFixed(2));//遗属需求法应有的寿险保额/元
        $("#FinanceInsurancePlanDiv #RelativeFinancial2").val(RelativeFinancial2.toMyFixed(2));
        $("#FinanceInsurancePlanDiv #InsureName1").val(InsureName);//-------被保险人
        $("#FinanceInsurancePlanDiv #InsureName2").val(SpouseName);
        $("#FinanceInsurancePlanDiv #InsureNeedCash1").val(InsureNeedCash1.toMyFixed(2));//保险需求额度/元	
        $("#FinanceInsurancePlanDiv #InsureNeedCash2").val(InsureNeedCash2.toMyFixed(2));
        $("#FinanceInsurancePlanDiv #InsuranceAmount1").val(InsuranceAmount1.toMyFixed(2));//----*已有额度/元
        $("#FinanceInsurancePlanDiv #InsuranceAmount2").val(InsuranceAmount2.toMyFixed(2));
        $("#FinanceInsurancePlanDiv #GapCash1").val(GapCash1.toMyFixed(2));//缺口额度/元
        $("#FinanceInsurancePlanDiv #GapCash2").val(GapCash2.toMyFixed(2));
        $("#FinanceInsurancePlanDiv #BudgetAmount1").val(BudgetAmount1);//----*预算金额/元
        $("#FinanceInsurancePlanDiv #BudgetAmount2").val(BudgetAmount2);
        $("#FinanceInsurancePlanDiv #SupplementaryQuota1").val(SupplementaryQuota1);//*补充额度/元
        $("#FinanceInsurancePlanDiv #SupplementaryQuota2").val(SupplementaryQuota2);
        $("#FinanceInsurancePlanDiv #BalanceCash1").val(BalanceCash1.toMyFixed(2));//欠缺额度/元
        $("#FinanceInsurancePlanDiv #BalanceCash2").val(BalanceCash2.toMyFixed(2));

     //   SaveDefaultValueCommon("InsuranceOne");//保存原值。和新值要做一个对比的
    } else {


        //年龄共用
        $("#FinanceInsurancePlanDiv #Age1").val(Age);
        $("#FinanceInsurancePlanDiv #Age1Life").val(Age);//被保险人年龄/岁
        $("#FinanceInsurancePlanDiv #SpouseAge2").val(Age);  //---------配偶当前年龄-男的写女的

        $("#FinanceInsurancePlanDiv #RetirementAge1Life").val(RetirementAge1);//*预计退休年龄/岁
        $("#FinanceInsurancePlanDiv #PredictRetirementAgeLIfe").val(PredictRetirementAgeLIfe);//离退休年数/年
        $("#FinanceInsurancePlanDiv #ReturnOnInvestment1Life").val(ReturnOnInvestment1);//*投资报酬率
        $("#FinanceInsurancePlanDiv #Income1Life").val(Income1);//当前个人年收入/元
        $("#FinanceInsurancePlanDiv #RevenueGrowth1Life").val(RevenueGrowth1);//收入增长率
        $("#FinanceInsurancePlanDiv #FutureIncomeLife").val(FutureIncomeLife.toMyFixed(2));//未来工作期间收入现值/元
        $("#FinanceInsurancePlanDiv #Expenditure").val(Expenditure);//-个人年收入支出
        $("#FinanceInsurancePlanDiv #InflationRate1Life").val(InflationRate1);//年通货膨胀率
        $("#FinanceInsurancePlanDiv #FutureExpend").val(FutureExpend.toMyFixed(2));//未来工作期间支出现值/元
        $("#FinanceInsurancePlanDiv #FutureAnnuityIncome").val(FutureAnnuityIncome.toMyFixed(2));//个人未来净收入的年金现值/元
        $("#FinanceInsurancePlanDiv #FutureAnnuityIncomeSub").val(FutureAnnuityIncome.toMyFixed(2));//弥补收入应有的寿险保额/元
        //---------------------被保险人

        $("#FinanceInsurancePlanDiv #InsureNeedCash1Life").val(FutureAnnuityIncome.toMyFixed(2));//保险需求额度/元
        $("#FinanceInsurancePlanDiv #InsuranceAmount1Life").val(InsuranceAmount1);//已有额度/元
        $("#FinanceInsurancePlanDiv #GapCash1Life").val(GapCash1.toMyFixed(2));//缺口额度/元
        $("#FinanceInsurancePlanDiv #BudgetAmount1Life").val(BudgetAmount1);//预算金额/元

        $("#FinanceInsurancePlanDiv #SupplementaryQuota1Life").val(SupplementaryQuota1);//-补充额度/元
        $("#FinanceInsurancePlanDiv #BalanceCash1Life").val(BalanceCash1.toMyFixed(2));//欠缺额度/元

        $("#FinanceInsurancePlanDiv #InsureName").val(InsureName);//被保险人姓名
        $("#FinanceInsurancePlanDiv #InsureName1Life").val(InsureName);//被保险人姓名
        $("#FinanceInsurancePlanDiv #InsureName1").val(InsureName);//-------被保险人

      //  SaveDefaultValueCommon("InsuranceTwo");//保存原值。和新值要做一个对比的
    }
}

//保存数据
function SaveInsurancePlan(saveFalg) {
    TagNavi = true;
    MethodTypeTab = $("#FinanceInsurancePlanDiv #MethodTypeId").val() * 1;


    //获取URL参数
    var ProposalId = $.getUrlParam("ProposalId");

    //此处参数必须跟VM一致
    var obj = new Object();

    //获取财产传承
    if (ProposalId != null && ProposalId != "" && ProposalId != undefined) {
        obj["ProposalId"] = ProposalId;
    } else {
        alert("清先保存基本信息")
        return false;
    }
    var Analysis= $("#FinanceInsurancePlanDiv #Analysis").val();
    obj["Id"] = $("#FinanceInsurancePlanDiv #InsurancePlanId").val();
    obj["MethodTypeId"] = MethodTypeTab;

    if (MethodTypeTab == 1) {
       
        if (!VerificationHelper.checkFrom("InsuranceOne") || !VerificationHelper.checkFrom("AreRquest")) {
            TagNavi = false;
            return;
        }
        obj["SpouseName"] = $("#FinanceInsurancePlanDiv #SpouseName").val();
        obj["Age1"] = $("#FinanceInsurancePlanDiv #Age1").val();
        obj["Age2"] = $("#FinanceInsurancePlanDiv #Age2").val();
        obj["RetirementAge1"] = $("#FinanceInsurancePlanDiv #RetirementAge1").val();
        obj["RetirementAge2"] = $("#FinanceInsurancePlanDiv #RetirementAge2").val();
        obj["ReturnOnInvestment1"] = $("#FinanceInsurancePlanDiv #ReturnOnInvestment1").val();
        obj["ReturnOnInvestment2"] = $("#FinanceInsurancePlanDiv #ReturnOnInvestment2").val();//----人民币
        obj["InflationRate1"] = $("#FinanceInsurancePlanDiv #InflationRate1").val();
        obj["InflationRate2"] = $("#FinanceInsurancePlanDiv #InflationRate2").val();
        obj["RevenueGrowth1"] = $("#FinanceInsurancePlanDiv #RevenueGrowth1").val();
        obj["RevenueGrowth2"] = $("#FinanceInsurancePlanDiv #RevenueGrowth2").val();
        obj["FamilyExpensesPay1"] = $("#FinanceInsurancePlanDiv #FamilyExpensesPay1").val();
        obj["FamilyExpensesPay2"] = $("#FinanceInsurancePlanDiv #FamilyExpensesPay2").val();
        obj["FamilyIncomePay1"] = $("#FinanceInsurancePlanDiv #FamilyIncomePay1").val();
        obj["FamilyIncomePay2"] = $("#FinanceInsurancePlanDiv #FamilyIncomePay2").val();
        obj["SpouseAge1"] = $("#FinanceInsurancePlanDiv #SpouseAge1").val();//---------房产
        obj["SpouseAge2"] = $("#FinanceInsurancePlanDiv #SpouseAge2").val();
        obj["FamilyFutureSaving1"] = $("#FinanceInsurancePlanDiv #FamilyFutureSaving1").val();
        obj["FamilyFutureSaving2"] = $("#FinanceInsurancePlanDiv #FamilyFutureSaving2").val();
        obj["MatrimonialFee1"] = $("#FinanceInsurancePlanDiv #MatrimonialFee1").val();//-------信用卡借款
        obj["MatrimonialFee2"] = $("#FinanceInsurancePlanDiv #MatrimonialFee2").val();
        obj["AfterAccidentRate1"] = $("#FinanceInsurancePlanDiv #AfterAccidentRate1").val();
        obj["AfterAccidentRate2"] = $("#FinanceInsurancePlanDiv #AfterAccidentRate2").val();//-----金融实用
        obj["AdjustMatrimonialFee1"] = $("#FinanceInsurancePlanDiv #AdjustMatrimonialFee1").val();
        obj["AdjustMatrimonialFee2"] = $("#FinanceInsurancePlanDiv #AdjustMatrimonialFee2").val();
        obj["MatrimonialFeeNow1"] = $("#FinanceInsurancePlanDiv #MatrimonialFeeNow1").val();
        obj["MatrimonialFeeNow2"] = $("#FinanceInsurancePlanDiv #MatrimonialFeeNow2").val();//------自用房
        obj["Income1"] = $("#FinanceInsurancePlanDiv #Income1").val();
        obj["Income2"] = $("#FinanceInsurancePlanDiv #Income2").val();
        obj["SpouseMonthIncome1"] = $("#FinanceInsurancePlanDiv #SpouseMonthIncome1").val();
        obj["SpouseMonthIncome2"] = $("#FinanceInsurancePlanDiv #SpouseMonthIncome2").val();
        obj["FamilyLiveOverdraft1"] = $("#FinanceInsurancePlanDiv #FamilyLiveOverdraft1").val()
        obj["FamilyLiveOverdraft2"] = $("#FinanceInsurancePlanDiv #FamilyLiveOverdraft2").val();
        obj["ReserveFund1"] = $("#FinanceInsurancePlanDiv #ReserveFund1").val();//紧急备用金现值/元
        obj["ReserveFund2"] = $("#FinanceInsurancePlanDiv #ReserveFund2").val();
        obj["EduAmount1"] = $("#FinanceInsurancePlanDiv #EduAmount1").val();//--教育金现值/元
        obj["EduAmount2"] = $("#FinanceInsurancePlanDiv #EduAmount2").val();
        obj["PensionFunds1"] = $("#FinanceInsurancePlanDiv #PensionFunds1").val();//养老基金现值/元
        obj["PensionFunds2"] = $("#FinanceInsurancePlanDiv #PensionFunds2").val();
        obj["DeathExpense1"] = $("#FinanceInsurancePlanDiv #DeathExpense1").val();
        obj["DeathExpense2"] = $("#FinanceInsurancePlanDiv #DeathExpense2").val();
        obj["LoanBalance1"] = $("#FinanceInsurancePlanDiv #LoanBalance1").val();
        obj["LoanBalance2"] = $("#FinanceInsurancePlanDiv #LoanBalance2").val();
        obj["EarningAssets1"] = $("#FinanceInsurancePlanDiv #EarningAssets1").val();
        obj["EarningAssets2"] = $("#FinanceInsurancePlanDiv #EarningAssets2").val();//---------房产
        obj["RelativeFinancial1"] = $("#FinanceInsurancePlanDiv #RelativeFinancial1").val();
        obj["RelativeFinancial2"] = $("#FinanceInsurancePlanDiv #RelativeFinancial2").val();
        obj["InsureName1"] = $("#FinanceInsurancePlanDiv #InsureName1").val();
        obj["InsureName2"] = $("#FinanceInsurancePlanDiv #InsureName2").val();//-------信用
        obj["InsureNeedCash1"] = $("#FinanceInsurancePlanDiv #InsureNeedCash1").val();
        obj["InsureNeedCash2"] = $("#FinanceInsurancePlanDiv #InsureNeedCash2").val();
        obj["InsuranceAmount1"] = $("#FinanceInsurancePlanDiv #InsuranceAmount1").val();//----
        obj["InsuranceAmount2"] = $("#FinanceInsurancePlanDiv #InsuranceAmount2").val();
        obj["GapCash1"] = $("#FinanceInsurancePlanDiv #GapCash1").val();
        obj["GapCash2"] = $("#FinanceInsurancePlanDiv #GapCash2").val();
        obj["BudgetAmount1"] = $("#FinanceInsurancePlanDiv #BudgetAmount1").val();//----
        obj["BudgetAmount2"] = $("#FinanceInsurancePlanDiv #BudgetAmount2").val();
        obj["SupplementaryQuota1"] = $("#FinanceInsurancePlanDiv #SupplementaryQuota1").val();
        obj["SupplementaryQuota2"] = $("#FinanceInsurancePlanDiv #SupplementaryQuota2").val();
        obj["BalanceCash1"] = $("#FinanceInsurancePlanDiv #BalanceCash1").val();
        obj["BalanceCash2"] = $("#FinanceInsurancePlanDiv #BalanceCash2").val();
    } else {
       
        if (!VerificationHelper.checkFrom("InsuranceTwo")||!VerificationHelper.checkFrom("AreRquest")) {
            TagNavi = false;
            return;
        }

        obj["RetirementAge1"] = $("#FinanceInsurancePlanDiv #RetirementAge1Life").val();
        obj["ReturnOnInvestment1"] = $("#FinanceInsurancePlanDiv #ReturnOnInvestment1Life").val();
        obj["Income1"] = $("#FinanceInsurancePlanDiv #Income1Life").val();
        obj["RevenueGrowth1"] = $("#FinanceInsurancePlanDiv #RevenueGrowth1Life").val();//收入增长率
        obj["InflationRate1"] = $("#FinanceInsurancePlanDiv #InflationRate1Life").val();//年通货膨胀率
        obj["Expenditure"] = $("#FinanceInsurancePlanDiv #Expenditure").val();
        obj["#InsureName1"] = $("#FinanceInsurancePlanDiv #InsureName1Life").val();
        obj["InsureNeedCash1"] = $("#FinanceInsurancePlanDiv #InsureNeedCash1Life").val();
        obj["InsuranceAmount1"] = $("#FinanceInsurancePlanDiv #InsuranceAmount1Life").val();
        obj["GapCash1"] = $("#FinanceInsurancePlanDiv #GapCash1Life").val();
        obj["BudgetAmount1"] = $("#FinanceInsurancePlanDiv #BudgetAmount1Life").val();
        obj["SupplementaryQuota1"] = $("#FinanceInsurancePlanDiv #SupplementaryQuota1Life").val();
        obj["BalanceCash1"] = $("#FinanceInsurancePlanDiv #BalanceCash1Life").val();
    }
  

    obj["Analysis"] = Analysis;//----

    _ajaxhepler({
        url: "/CompetitionUser/InsurancePlan/SaveInsurancePlan",
        type: "POST",
        async: false,
        dataType: "json",
        data: JSON.stringify(obj),
        contentType: "application/json",
        success: function (data) {
            if (data != null) {
                $("#FinanceInsurancePlanDiv #InsurancePlanId").val(data.Id);
                SaveDefaultValueCommon("FinanceInsurancePlanDiv");//保存原值。和新值要做一个对比的

                if (typeof saveFalg == "undefined") {
                    dialogHelper.Success({
                        content: "保存成功！", success: function () {
                            //同时刷新页面
                            window.location.reload();
                        }
                    });
                }
            }

        }

    })
}



$(function () {
    IsProposalSave()//客户验证
    param = $("#hdParam").val();

    //获取URL参数
    var ProposalId = $.getUrlParam("ProposalId");

    //获取财产传承
    if (ProposalId != null && ProposalId != "" && ProposalId != undefined) {
        LoadInsurancePlan(ProposalId);
    } else {

    }

    $("#FinanceInsurancePlanDiv #btnSave").live("click", function () {
        //添加数据
        SaveInsurancePlan();
    });



    //同时绑定下一页事件
    $("#FinanceInsurancePlanDiv #btnNext").live("click", function () {
        //同时还要保存当前数据
        SaveInsurancePlan(0);
        if (TagNavi) {
            window.location.href = "/CompetitionUser/InvestmentPlan/Index" + param;
        }
    });
    //同时绑定上一页事件
    $("#FinanceInsurancePlanDiv #btnPrev").live("click", function () {
        //先验证后保存，然后跳转

        //同时还要保存当前数据
        SaveInsurancePlan(0);
        if (TagNavi) {
            window.location.href = "/CompetitionUser/RetirementPlan/Index" + param;
        }
    });

    //绑定切换需求算法切换事件
    $("#FinanceInsurancePlanDiv #MethodTypeId").bind("change", function () {

        $(".warn-box").remove();

        MethodTypeTab = $(this).val();
        var nameAndAge = "";
        if (MethodTypeTab == "1") {
            $("#FinanceInsurancePlanDiv #InsuranceOne").show();
            $("#FinanceInsurancePlanDiv #InsuranceTwo").hide();
        } else if (MethodTypeTab == "2") {
            $("#FinanceInsurancePlanDiv #InsuranceOne").hide();
            $("#FinanceInsurancePlanDiv #InsuranceTwo").show();
        }

        $("#FinanceInsurancePlanDiv #Analysis").val("");
        $("#FinanceInsurancePlanDiv").find("input[type='text']").each(function () {
            nameAndAge = $(this).attr("id");
            if (nameAndAge == "InsureName") { //客户名
                return true;
            } else if (nameAndAge == "Age1") { //客户年龄
                return true;
            } else if (nameAndAge == "InsureName1") {
                return true;
            } else if (nameAndAge == "Age1Life") {
                return true;
            } else if (nameAndAge == "InsureName1Life") {
                return true;
            } else if (nameAndAge == "monthMoney") {
                return true;
            } else if (nameAndAge == "UserableAsset") {
                return true;
            }

            else {
                $(this).val("");
            }
        });
    });

});

//遗属需求法应有的寿险保额=∑(家庭未来生活费用缺口现值,紧急备用金现值，教育金现值，养老基金现值，临终及丧葬支出现值，目前贷款余额)-家庭生息资产
//***************
//直接调用方法 ----需求价值法
//***************
function CalcInsreVal2() {
    var FamilyLiveOverdraft2 = $.trim($("#FamilyLiveOverdraft2").val()) * 1;
    var ReserveFund2 = $.trim($("#ReserveFund2").val()) * 1;
    var EduAmount2 = $.trim($("#EduAmount2").val()) * 1;
    var PensionFunds2 = $.trim($("#PensionFunds2").val()) * 1;
    var DeathExpense2 = $.trim($("#DeathExpense2").val()) * 1;
    var LoanBalance2 = $.trim($("#LoanBalance2").val()) * 1;
    var EarningAssets2 = $.trim($("#EarningAssets2").val()) * 1;
    var RelativeFinancial2 = calcRelativeFinancial(FamilyLiveOverdraft2, ReserveFund2, EduAmount2, PensionFunds2, DeathExpense2, LoanBalance2, EarningAssets2)*1;
    $("#FinanceInsurancePlanDiv #RelativeFinancial2").val(RelativeFinancial2.toMyFixed(2));
    $("#FinanceInsurancePlanDiv #InsureNeedCash2").val(RelativeFinancial2.toMyFixed(2));
}

function CalcInsreVal1() {
    var FamilyLiveOverdraft1 = $.trim($("#FamilyLiveOverdraft1").val()) * 1;
    var ReserveFund1 = $.trim($("#ReserveFund1").val()) * 1;
    var EduAmount1 = $.trim($("#EduAmount1").val()) * 1;
    var PensionFunds1 = $.trim($("#PensionFunds1").val()) * 1;
    var DeathExpense1 = $.trim($("#DeathExpense1").val()) * 1;
    var LoanBalance1 = $.trim($("#LoanBalance1").val()) * 1;
    var EarningAssets1 = $.trim($("#EarningAssets1").val()) * 1;
    var RelativeFinancial1 = calcRelativeFinancial(FamilyLiveOverdraft1, ReserveFund1, EduAmount1, PensionFunds1, DeathExpense1, LoanBalance1, EarningAssets1)*1;
    $("#FinanceInsurancePlanDiv #RelativeFinancial1").val(RelativeFinancial1.toMyFixed(2));
    $("#FinanceInsurancePlanDiv #InsureNeedCash1").val(RelativeFinancial1.toMyFixed(2));
}

//缺口额度 =保险需求额度-已有额度
function CalcGapCash1Val1() {
    var InsureNeedCash1 = $.trim($("#InsureNeedCash1").val())*1;
    var InsuranceAmount1 = $.trim($("#InsuranceAmount1").val())*1;
    var GapCash1 = calcGapCash(InsureNeedCash1, InsuranceAmount1)*1;
    $("#GapCash1").val(GapCash1.toMyFixed(2));
}
//缺口额度
function CalcGapCash1Val2() {
    var InsureNeedCash1 = $.trim($("#InsureNeedCash2").val())*1;
    var InsuranceAmount1 = $.trim($("#InsuranceAmount2").val())*1;
    var GapCash1 = calcGapCash(InsureNeedCash1, InsuranceAmount1)*1;
    $("#GapCash2").val(GapCash1.toMyFixed(2));
}

//欠缺额度=缺口额度-预算额度-补充额度
function CalcBalanceCashVal1() {
    var GapCash1 = $.trim($("#GapCash1").val())*1;
    var BudgetAmount1 = $.trim($("#BudgetAmount1").val())*1;
    var SupplementaryQuota1 = $.trim($("#SupplementaryQuota1").val())*1;
    var BalanceCash1 = calcBalanceCash(GapCash1, BudgetAmount1, SupplementaryQuota1)*1;
    $("#BalanceCash1").val(BalanceCash1.toMyFixed(2));
}
function CalcBalanceCashVal2() {
    var GapCash2 = $.trim($("#GapCash2").val())*1;
    var BudgetAmount2 = $.trim($("#BudgetAmount2").val())*1;
    var SupplementaryQuota2 = $.trim($("#SupplementaryQuota2").val())*1;
    var BalanceCash2 = calcBalanceCash(GapCash2, BudgetAmount2, SupplementaryQuota2)*1;
    $("#BalanceCash2").val(BalanceCash2.toMyFixed(2));
}
//******************
//直接调用方法------生命价值法
//******************
//	离退休年数=预计退休年龄-被保险人年龄
function calcPredictRetirementAgeLIfeVal() {
    //离退休年数=预计退休年龄-被保险人年龄
    var Age1Life = $.trim($("#Age1Life").val()) * 1;
    var RetirementAge1Life = $.trim($("#RetirementAge1Life").val()) * 1;

    var PredictRetirementAgeLIfe = calcPredictRetirementAgeLIfe(Age1Life, RetirementAge1Life)*1;
    $("#FinanceInsurancePlanDiv #PredictRetirementAgeLIfe").val(PredictRetirementAgeLIfe);
}
//	未来工作期间收入现值
function calcFutureIncomeLifeVal() {
    var ReturnOnInvestment1Life = $.trim($("#ReturnOnInvestment1Life").val()) * 1;//投资报酬
    var RevenueGrowth1Life = $.trim($("#RevenueGrowth1Life").val()) * 1;//收入增长
    var PredictRetirementAgeLIfe = $.trim($("#PredictRetirementAgeLIfe").val()) * 1;//离退休年数
    var Income1Life = $.trim($("#Income1Life").val()) * 1; //当前个人年收入
    var FutureIncomeLife = calcFutureIncomeLife(ReturnOnInvestment1Life, RevenueGrowth1Life, PredictRetirementAgeLIfe, Income1Life)*1;
    $("#FinanceInsurancePlanDiv #FutureIncomeLife").val(FutureIncomeLife.toMyFixed(2));
}
//	未来工作期间支出现值
function calcFutureExpendVal() {
    var ReturnOnInvestment1Life = $.trim($("#ReturnOnInvestment1Life").val()) * 1;//投资报酬
    var InflationRate1Life = $.trim($("#InflationRate1Life").val()) * 1;//年通货膨胀
    var PredictRetirementAgeLIfe = $.trim($("#PredictRetirementAgeLIfe").val()) * 1;//离退休年数
    var Expenditure = $.trim($("#Expenditure").val()) * 1;//当前个人年收入
    var FutureExpend = calcFutureExpend(ReturnOnInvestment1Life, InflationRate1Life, PredictRetirementAgeLIfe, Expenditure)*1;
    $("#FinanceInsurancePlanDiv #FutureExpend").val(FutureExpend.toMyFixed(2));
}
//	个人未来净收入的年金现值
function calcFutureAnnuityIncomeVal() {
    var FutureIncomeLife = $.trim($("#FutureIncomeLife").val())*1;
    var FutureExpend = $.trim($("#FutureExpend").val())*1;
    var FutureAnnuityIncome = calcFutureAnnuityIncome(FutureIncomeLife, FutureExpend)*1;
    $("#FutureAnnuityIncome").val(FutureAnnuityIncome.toMyFixed(2));
    //*弥补收入应有的寿险保额
    $("#FutureAnnuityIncomeSub").val(FutureAnnuityIncome.toMyFixed(2));
    //保险需求额度赋值
    $("#InsureNeedCash1Life").val(FutureAnnuityIncome.toMyFixed(2));
}
//缺口额度=保险需求额度-已有额度 
function calcGapCashLifeVal() {
    var InsureNeedCash1Life = $.trim($("#InsureNeedCash1Life").val())*1;
    var InsuranceAmount1Life = $.trim($("#InsuranceAmount1Life").val())*1;
    var GapCash = calcGapCash(InsureNeedCash1Life, InsuranceAmount1Life)*1;
    $("#GapCash1Life").val(GapCash.toMyFixed(2));
}
//欠缺额度 = 缺口额度 - 预算额度 - 补充额度
function calcBalanceCashSubVal() {
    var GapCash1Life = $.trim($("#GapCash1Life").val()) * 1;
    var BudgetAmount1Life = $.trim($("#BudgetAmount1Life").val()) * 1;
    var SupplementaryQuota1Life = $.trim($("#SupplementaryQuota1Life").val()) * 1;
    var BalanceCash1Life = calcBalanceCash(GapCash1Life, BudgetAmount1Life, SupplementaryQuota1Life)*1;
    $("#BalanceCash1Life").val(BalanceCash1Life.toMyFixed(2));
}
//***********************************
//直接调用方法结尾
//***********************************

//计算加载啊~~~~~~~~~~~~~~~~~~~~~~~~~~~ReturnOnInvestment1  
$(function () {
    //家庭生活费用实质报酬率1&2
    $("#InflationRate1").unbind("blur").blur(function () {
        //家庭生活费用实质报酬率1
        calcFamilyExpensesPayOne();
        //家庭生活费用现值=  /PV(家庭生活费用实质报酬率,家庭未来生活费准备年数,-调整后家庭生活费用, 0, 1)
        calcMatrimonialFeeNowOne();
        //家庭未来生活费用缺口现值=配偶的个人收入现值-家庭生活费用现值 
        calcFamilyLiveOverdraftOne();
        //	遗属需求法应有的寿险保额=∑(家庭未来生活费用缺口现值,紧急备用金现值，教育金现值，养老基金现值，临终及丧葬支出现值，目前贷款余额)-家庭生息资产
        CalcInsreVal1()
        //缺口额度方法直接调用
        CalcGapCash1Val1()
        //欠缺额度方法直接调用
        CalcBalanceCashVal1();



    });
    $("#InflationRate2").unbind("blur").blur(function () {
        //家庭生活费用实质报酬率1
        calcFamilyExpensesPayTwo();
        //家庭生活费用现值=  /PV(家庭生活费用实质报酬率,家庭未来生活费准备年数,-调整后家庭生活费用, 0, 1)
        calcMatrimonialFeeNowTwo();;
        //家庭未来生活费用缺口现值=配偶的个人收入现值-家庭生活费用现值 
        calcFamilyLiveOverdraftTwo();
        //	遗属需求法应有的寿险保额=∑(家庭未来生活费用缺口现值,紧急备用金现值，教育金现值，养老基金现值，临终及丧葬支出现值，目前贷款余额)-家庭生息资产
        CalcInsreVal2();
        //缺口额度方法直接调用
        CalcGapCash1Val2()
        //欠缺额度方法直接调用
        CalcBalanceCashVal2();
    });
    //家庭生活费用实质报酬率1&2   ------------end


  
    $("#RetirementAge1,#Age1").unbind("blur").blur(function () {
        //家庭未来生活费准备年数
        calcFamilyFutureSavingTwo();
        //家庭生活费用现值=  /PV(家庭生活费用实质报酬率,家庭未来生活费准备年数,-调整后家庭生活费用, 0, 1)
        calcMatrimonialFeeNowTwo();

        //这货还关联上了第二个PV计算
        //	配偶的个人收入现值=PV(家庭收入实质报酬率,家庭未来生活费准备年数,配偶的个人年收入) 
        calcSpouseMonthIncomeTwo();

        //家庭未来生活费用缺口现值=配偶的个人收入现值-家庭生活费用现值 
        calcFamilyLiveOverdraftTwo();

        //	遗属需求法应有的寿险保额=∑(家庭未来生活费用缺口现值,紧急备用金现值，教育金现值，养老基金现值，临终及丧葬支出现值，目前贷款余额)-家庭生息资产
        CalcInsreVal2();
        //缺口额度方法直接调用
        CalcGapCash1Val2()
        //欠缺额度方法直接调用
        CalcBalanceCashVal2();
        //同时还要赋值给--配偶当前年龄/岁
    //    $("#FinanceInsurancePlanDiv #SpouseAge2").val(Age2);

    });

    $("#RetirementAge2,#Age2").unbind("blur").blur(function () {
        //家庭未来生活费准备年数
        calcFamilyFutureSavingOne();

        //家庭生活费用现值=  /PV(家庭生活费用实质报酬率,家庭未来生活费准备年数,-调整后家庭生活费用, 0, 1)

        calcMatrimonialFeeNowOne();
        //	配偶的个人收入现值=PV(家庭收入实质报酬率,家庭未来生活费准备年数,配偶的个人年收入) 
        calcSpouseMonthIncomeOne();

        //家庭未来生活费用缺口现值=配偶的个人收入现值-家庭生活费用现值 
        calcFamilyLiveOverdraftOne();
        //	遗属需求法应有的寿险保额=∑(家庭未来生活费用缺口现值,紧急备用金现值，教育金现值，养老基金现值，临终及丧葬支出现值，目前贷款余额)-家庭生息资产
        CalcInsreVal1();
        //缺口额度方法直接调用
        CalcGapCash1Val1()
        //欠缺额度方法直接调用
        CalcBalanceCashVal1();
        //同时还要赋值给--配偶当前年龄/岁
       // $("#FinanceInsurancePlanDiv #SpouseAge1").val(Age2);

    });
    //家庭未来生活费准备年数1&2 ---------------end



    //调整后家庭生活费用
    $("#MatrimonialFee1,#AfterAccidentRate1").unbind("blur").blur(function () {
        //调整后家庭生活费用
        calcAdjustMatrimonialFeeOne();

        //家庭生活费用现值=  /PV(家庭生活费用实质报酬率,家庭未来生活费准备年数,-调整后家庭生活费用, 0, 1)

        calcMatrimonialFeeNowOne();

        //家庭未来生活费用缺口现值=配偶的个人收入现值-家庭生活费用现值 
        calcFamilyLiveOverdraftOne();

        //	遗属需求法应有的寿险保额=∑(家庭未来生活费用缺口现值,紧急备用金现值，教育金现值，养老基金现值，临终及丧葬支出现值，目前贷款余额)-家庭生息资产
        CalcInsreVal1();

        //缺口额度方法直接调用
        CalcGapCash1Val1()
        //欠缺额度方法直接调用
        CalcBalanceCashVal1();

    });

    $("#MatrimonialFee2,#AfterAccidentRate2").unbind("blur").blur(function () {

        calcAdjustMatrimonialFeeTwo();
        //家庭生活费用现值=  /PV(家庭生活费用实质报酬率,家庭未来生活费准备年数,-调整后家庭生活费用, 0, 1)

        calcMatrimonialFeeNowTwo();

        //家庭未来生活费用缺口现值=配偶的个人收入现值-家庭生活费用现值 
        calcFamilyLiveOverdraftTwo();


        //	遗属需求法应有的寿险保额=∑(家庭未来生活费用缺口现值,紧急备用金现值，教育金现值，养老基金现值，临终及丧葬支出现值，目前贷款余额)-家庭生息资产
        CalcInsreVal2();
        //缺口额度方法直接调用
        CalcGapCash1Val2()
        //欠缺额度方法直接调用
        CalcBalanceCashVal2();

    });

    //调整后家庭生活费用-------------------end



    //------------------------------------------第二个PV计算


   
    $("#RevenueGrowth1").unbind("blur").blur(function () {
        //家庭收入实质报酬率
        calcFamilyIncomePayOne()
        //	配偶的个人收入现值=PV(家庭收入实质报酬率,家庭未来生活费准备年数,配偶的个人年收入) 
        calcSpouseMonthIncomeOne();

        //家庭未来生活费用缺口现值=配偶的个人收入现值-家庭生活费用现值 
        calcFamilyLiveOverdraftOne();

        //	遗属需求法应有的寿险保额=∑(家庭未来生活费用缺口现值,紧急备用金现值，教育金现值，养老基金现值，临终及丧葬支出现值，目前贷款余额)-家庭生息资产
        CalcInsreVal1();
        //缺口额度方法直接调用
        CalcGapCash1Val1()
        //欠缺额度方法直接调用
        CalcBalanceCashVal1();

    });

    $("#RevenueGrowth2").unbind("blur").blur(function () {
        calcFamilyIncomePayTwo();
        //	配偶的个人收入现值=PV(家庭收入实质报酬率,家庭未来生活费准备年数,配偶的个人年收入) 
        calcSpouseMonthIncomeTwo();

        //家庭未来生活费用缺口现值=配偶的个人收入现值-家庭生活费用现值 
        calcFamilyLiveOverdraftTwo();

        //	遗属需求法应有的寿险保额=∑(家庭未来生活费用缺口现值,紧急备用金现值，教育金现值，养老基金现值，临终及丧葬支出现值，目前贷款余额)-家庭生息资产
        CalcInsreVal2();

        //缺口额度方法直接调用
        CalcGapCash1Val2()
        //欠缺额度方法直接调用
        CalcBalanceCashVal2();


    });
    //家庭收入实质报酬率1&2   ------------end



    //	配偶的个人收入现值=PV(家庭收入实质报酬率,家庭未来生活费准备年数,配偶的个人年收入)  B16= PV(B8, B10, -B15) 输出；C16= PV(C8, C10, C15) 输出
    $("#Income1").unbind("blur").blur(function () {
        calcSpouseMonthIncomeOne();

        //家庭未来生活费用缺口现值=配偶的个人收入现值-家庭生活费用现值 
        calcFamilyLiveOverdraftOne();
        //	遗属需求法应有的寿险保额=∑(家庭未来生活费用缺口现值,紧急备用金现值，教育金现值，养老基金现值，临终及丧葬支出现值，目前贷款余额)-家庭生息资产
        CalcInsreVal1();
        //缺口额度方法直接调用
        CalcGapCash1Val1()
        //欠缺额度方法直接调用
        CalcBalanceCashVal1();
    });
    $("#Income2").unbind("blur").blur(function () {
        calcSpouseMonthIncomeTwo();

        //家庭未来生活费用缺口现值=配偶的个人收入现值-家庭生活费用现值 
        calcFamilyLiveOverdraftTwo();

        //	遗属需求法应有的寿险保额=∑(家庭未来生活费用缺口现值,紧急备用金现值，教育金现值，养老基金现值，临终及丧葬支出现值，目前贷款余额)-家庭生息
        CalcInsreVal2();
        //缺口额度方法直接调用
        CalcGapCash1Val2()
        //欠缺额度方法直接调用
        CalcBalanceCashVal2();
    });


    $("#ReturnOnInvestment1").unbind("blur").blur(function () {

        //	家庭生活费用实质报酬率
        calcFamilyExpensesPayOne();


        //	家庭收入实质报酬率
        calcFamilyIncomePayOne();


        //家庭生活费用现值=  /PV(家庭生活费用实质报酬率,家庭未来生活费准备年数,-调整后家庭生活费用, 0, 1)
        calcMatrimonialFeeNowOne();


        //家庭未来生活费用缺口现值=配偶的个人收入现值-家庭生活费用现值 
        calcFamilyLiveOverdraftOne()

        //	遗属需求法应有的寿险保额=∑(家庭未来生活费用缺口现值,紧急备用金现值，教育金现值，养老基金现值，临终及丧葬支出现值，目前贷款余额)-家庭生息资产
        CalcInsreVal1();
        //缺口额度方法直接调用
        CalcGapCash1Val1()
        //欠缺额度方法直接调用
        CalcBalanceCashVal1();
        //	配偶的个人收入现值=PV(家庭收入实质报酬率,家庭未来生活费准备年数,配偶的个人年收入) 
        calcSpouseMonthIncomeOne()

    });

    $("#ReturnOnInvestment2").unbind("blur").blur(function () {
        calcFamilyExpensesPayTwo();

   
        //家庭生活费用现值=  /PV(家庭生活费用实质报酬率,家庭未来生活费准备年数,-调整后家庭生活费用, 0, 1)
        calcFamilyIncomePayTwo();


        //家庭未来生活费用缺口现值=配偶的个人收入现值-家庭生活费用现值 
        calcFamilyLiveOverdraftTwo()

        //	遗属需求法应有的寿险保额=∑(家庭未来生活费用缺口现值,紧急备用金现值，教育金现值，养老基金现值，临终及丧葬支出现值，目前贷款余额)-家庭生息资产
        CalcInsreVal2();
    
        //缺口额度方法直接调用
        CalcGapCash1Val2()
        //欠缺额度方法直接调用
        CalcBalanceCashVal2();
        //	家庭收入实质报酬率
        calcFamilyIncomePayTwo();
        //	配偶的个人收入现值=PV(家庭收入实质报酬率,家庭未来生活费准备年数,配偶的个人年收入) 
        calcSpouseMonthIncomeTwo()

    });


    //	遗属需求法应有的寿险保额1
    $("#ReserveFund1,#EduAmount1,#PensionFunds1,#DeathExpense1,#LoanBalance1,#EarningAssets1").unbind("blur").blur(function () {
        //遗属需求法应有的寿险保额
        CalcInsreVal1();
        //缺口额度方法直接调用
        CalcGapCash1Val1()
        //欠缺额度方法直接调用
        CalcBalanceCashVal1();
    

    });

    //	遗属需求法应有的寿险保额2
    $("#ReserveFund2,#EduAmount2,#PensionFunds2,#DeathExpense2,#LoanBalance2,#EarningAssets2").unbind("blur").blur(function () {
        //遗属需求法应有的寿险保额
        CalcInsreVal2();
        //缺口额度方法直接调用
        CalcGapCash1Val2()
        //欠缺额度方法直接调用
        CalcBalanceCashVal2();
    });

    //配偶的名字
    $("#SpouseName").unbind("blur").blur(function () {
        $("#InsureName2").val($.trim($(this).val()));
    })

    //欠缺额度方法直接调用
    $("#BudgetAmount1,#SupplementaryQuota1").unbind("blur").blur(function () {
        //欠缺额度方法直接调用
        CalcBalanceCashVal1();
    });
    //欠缺额度方法直接调用
    $("#BudgetAmount2,#SupplementaryQuota2").unbind("blur").blur(function () {
        //欠缺额度方法直接调用
        CalcBalanceCashVal2();
    });
    //保险需求额度/元
    $("#InsuranceAmount1").unbind("blur").blur(function () {
        //缺口额度方法直接调用
        CalcGapCash1Val1()
        //欠缺额度方法直接调用
        CalcBalanceCashVal1();
    });
    //保险需求额度/元
    $("#InsuranceAmount2").unbind("blur").blur(function () {
        //缺口额度方法直接调用
        CalcGapCash1Val2()
        //欠缺额度方法直接调用
        CalcBalanceCashVal2();
    });
    //***********************************
    //      生命价值法计算
    //***********************************
    //投资报酬  预计退休年龄/岁 影响两个PV
    $("#RetirementAge1Life,#ReturnOnInvestment1Life").unbind("blur").blur(function () {
        //离退休年数=预计退休年龄-被保险人年龄
        calcPredictRetirementAgeLIfeVal();
        //	未来工作期间收入现值
        calcFutureIncomeLifeVal();
        //	未来工作期间支出现值
        calcFutureExpendVal();
        //	个人未来净收入的年金现值=未来工作期间收入现值-未来工作期间支出现
        calcFutureAnnuityIncomeVal();
        //缺口额度=保险需求额度-已有额度 
        calcGapCashLifeVal();
        //欠缺额度 = 缺口额度 - 预算额度 - 补充额度
        calcBalanceCashSubVal();
    });

    // *投资报酬率-年收入增长率   当前个人年收入/元  RevenueGrowth1Life
    $("#RevenueGrowth1Life,#Income1Life").unbind("blur").blur(function () {
        //	未来工作期间收入现值
        calcFutureIncomeLifeVal();
        //	个人未来净收入的年金现值=未来工作期间收入现值-未来工作期间支出现
        calcFutureAnnuityIncomeVal();
        //缺口额度=保险需求额度-已有额度 
        calcGapCashLifeVal()
        //欠缺额度 = 缺口额度 - 预算额度 - 补充额度
        calcBalanceCashSubVal();
    });

    //	年通货膨胀  当前个人年支出)
    $("#InflationRate1Life,#Expenditure").unbind("blur").blur(function () {
        //	未来工作期间支出现值
        calcFutureExpendVal();
        //	个人未来净收入的年金现值=未来工作期间收入现值-未来工作期间支出现
        calcFutureAnnuityIncomeVal();
        //缺口额度=保险需求额度-已有额度 
        calcGapCashLifeVal();
        //欠缺额度 = 缺口额度 - 预算额度 - 补充额度
        calcBalanceCashSubVal();
    });
    //已有额度
    $("#InsuranceAmount1Life").unbind("blur").blur(function () {
        //缺口额度=保险需求额度-已有额度 
        calcGapCashLifeVal();
        //欠缺额度 = 缺口额度 - 预算额度 - 补充额度
        calcBalanceCashSubVal();
    });
    //欠缺额度
    $("#BudgetAmount1Life,#SupplementaryQuota1Life").unbind("blur").blur(function () {
        //欠缺额度 = 缺口额度 - 预算额度 - 补充额度
        calcBalanceCashSubVal();
    })



});
