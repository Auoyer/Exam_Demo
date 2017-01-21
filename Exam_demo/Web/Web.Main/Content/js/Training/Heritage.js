/**
 * @name 财产传承JS
 */

var URL = "";
$(function () {
    //客户信息是否保存
    IsProposalSave();

    //获取URL参数
    //获取URL参数
    var ProposalId = $.getUrlParam("ProposalId");
    var TrainExamId = $.getUrlParam("TrainExamId");
    var StuCustomerId = $.getUrlParam("StuCustomerId");
    URL = "?TrainExamId=" + TrainExamId + "&ProposalId=" + ProposalId + "&StuCustomerId=" + StuCustomerId;

    //获取财产传承
    GetHeritage(ProposalId);


    $("#Heritage #btnSave").live("click", function () {
        //添加数据
        EditHeritage(0);
    });


    //现金及现金等价物小计
    $("#Cash,#Deposit,#LifeInsurance,#OtherCashAccount").unbind("blur").blur(function () {
        var cash = $.trim($("#Cash").val()) * 1;
        var deposit = $.trim($("#Deposit").val()) * 1;
        var lifeInsurance = $.trim($("#LifeInsurance").val()) * 1;
        var otherCashAccount = $.trim($("#OtherCashAccount").val()) * 1;
        var Num=cash + deposit + lifeInsurance + otherCashAccount;
        var par = /^\d+(\.\d+)?$/;
        if (!par.test(Num)) {
            Num = 0;
        }
        $("#CashSubtotal").val((Num).toMyFixed(2));
        $("#OtherProperty").trigger("blur");
    });
    //投资
    $("#Stock,#Bond,#Fund,#OtherInvestment").unbind("blur").blur(function () {
        var stock = $.trim($("#Stock").val()) * 1;
        var bond = $.trim($("#Bond").val()) * 1;
        var fund = $.trim($("#Fund").val()) * 1;
        var otherInvestment = $.trim($("#OtherInvestment").val()) * 1;

        var Num = stock + bond + fund + otherInvestment;
        var par = /^\d+(\.\d+)?$/;
        if (!par.test(Num)) {
            Num = 0;
        }
        $("#InvestmentSubtotal").val((Num).toMyFixed(2));
        $("#OtherProperty").trigger("blur");
    });
    //退休金
    $("#Pension,#AnnuityRevenue,#OtherPension").unbind("blur").blur(function () {
        var pension = $.trim($("#Pension").val()) * 1;
        var annuityRevenue = $.trim($("#AnnuityRevenue").val()) * 1;
        var otherPension = $.trim($("#OtherPension").val()) * 1;
        var Num = pension + annuityRevenue + otherPension;
        var par = /^\d+(\.\d+)?$/;
        if (!par.test(Num)) {
            Num = 0;
        }
        $("#PensionSubtotal").val((Num).toMyFixed(2));
        $("#OtherProperty").trigger("blur");
    });
    //个人资产
    $("#House,#Car,#Other").unbind("blur").blur(function () {
        var house = $.trim($("#House").val()) * 1;
        var car = $.trim($("#Car").val()) * 1;
        var other = $.trim($("#Other").val()) * 1;
        var Num = house + car + other;
        var par = /^\d+(\.\d+)?$/;
        if (!par.test(Num)) {
            Num = 0;
        }
        $("#PersonSubtotal").val((Num).toMyFixed(2));
        $("#OtherProperty").trigger("blur");
    });
    //资产总计
    $("#OtherProperty").unbind("blur").blur(function () {
        var otherProperty = $.trim($("#OtherProperty").val()) * 1;
        var cashSubtotal = $.trim($("#CashSubtotal").val()) * 1;
        var investmentSubtotal = $.trim($("#InvestmentSubtotal").val()) * 1;
        var pensionSubtotal = $.trim($("#PensionSubtotal").val()) * 1;
        var personSubtotal = $.trim($("#PersonSubtotal").val()) * 1;
        var Num = otherProperty + cashSubtotal + investmentSubtotal + pensionSubtotal + personSubtotal;
        var par = /^\d+(\.\d+)?$/;
        if (!par.test(Num)) {
            Num = 0;
        }
        $("#TotalProperty").val((Num).toMyFixed(2));
        $("#TotalProperty2").val((Num).toMyFixed(2));
        TotalHeritage();
    });
    //贷款
    $("#ShortTermLoan,#MediumTermLoans,#LongTermLoan,#OtherLoan").unbind("blur").blur(function () {
        var shortTermLoan = $.trim($("#ShortTermLoan").val()) * 1;
        var mediumTermLoans = $.trim($("#MediumTermLoans").val()) * 1;
        var longTermLoan = $.trim($("#LongTermLoan").val()) * 1;
        var otherLoan = $.trim($("#OtherLoan").val()) * 1;
        var Num = shortTermLoan + mediumTermLoans + longTermLoan + otherLoan;
        var par = /^\d+(\.\d+)?$/;
        if (!par.test(Num)) {
            Num = 0;
        }
        $("#LoanSubtotal").val((Num).toMyFixed(2));
        $("#OtherLiabilities").trigger("blur");
    });
    //费用
    $("#MedicalCosts,#TaxCosts,#FuneralExpenses,#HeritageCosts,#OtherCosts").unbind("blur").blur(function () {
        var medicalCosts = $.trim($("#MedicalCosts").val()) * 1;
        var taxCosts = $.trim($("#TaxCosts").val()) * 1;
        var funeralExpenses = $.trim($("#FuneralExpenses").val()) * 1;
        var heritageCosts = $.trim($("#HeritageCosts").val()) * 1;
        var otherCosts = $.trim($("#OtherCosts").val()) * 1;
        var Num = medicalCosts + taxCosts + funeralExpenses + heritageCosts + otherCosts;
        var par = /^\d+(\.\d+)?$/;
        if (!par.test(Num)) {
            Num = 0;
        }
        $("#CostSubtotal").val((Num).toMyFixed(2));
        $("#OtherLiabilities").trigger("blur");
    });
    //负债总计
    $("#OtherLiabilities").unbind("blur").blur(function () {
        var otherLiabilities = $.trim($("#OtherLiabilities").val()) * 1;
        var loanSubtotal = $.trim($("#LoanSubtotal").val()) * 1;
        var costSubtotal = $.trim($("#CostSubtotal").val()) * 1;
        var Num = otherLiabilities + loanSubtotal + costSubtotal;
        var par = /^\d+(\.\d+)?$/;
        if (!par.test(Num)) {
            Num = 0;
        }
        $("#TotalLiabilities").val((Num).toMyFixed(2));
        $("#TotalLiabilities2").val((Num).toMyFixed(2));
        TotalHeritage();
    });

    //保存原值
    SaveDefaultValueCommon("Heritage");
})


/**
 * @name 净遗产总计
 */
function TotalHeritage() {
    var totalProperty = $.trim($("#TotalProperty").val()) * 1;
    var totalLiabilities = $.trim($("#TotalLiabilities").val()) * 1;
    $("#TotalHeritage").val((totalProperty - totalLiabilities).toFixed(2));
}

/**
 * @name 获取财产传承
 */
function GetHeritage(ProposalId) {
    _ajaxhepler({
        url: "/CompetitionUser/Heritage/GetHeritage",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: ProposalId,
            rId: Math.random()
        },
        success: function (data) {
            if (data != null && data != "") {

                $("#Cash").val(data.Cash);//现金
                $("#Deposit").val(data.Deposit);//银行存款
                $("#LifeInsurance").val(data.LifeInsurance);//人寿保单赔偿金额
                $("#OtherCashAccount").val(data.OtherCashAccount);//其他现金账户
                $("#CashSubtotal").val((data.Cash + data.Deposit + data.LifeInsurance + data.OtherCashAccount).toMyFixed(2));

                $("#Stock").val(data.Stock);//股票
                $("#Bond").val(data.Bond);//债券
                $("#Fund").val(data.Fund);//基金
                $("#OtherInvestment").val(data.OtherInvestment);//其他投资收益
                $("#InvestmentSubtotal").val((data.Stock + data.Bond + data.Fund + data.OtherInvestment).toMyFixed(2));

                $("#Pension").val(data.Pension);//养老金（一次性收入现值）
                $("#AnnuityRevenue").val(data.AnnuityRevenue);//配偶/遗孤年金收益（现值）
                $("#OtherPension").val(data.OtherPension);//其他退休基金
                $("#PensionSubtotal").val((data.Pension + data.AnnuityRevenue + data.OtherPension).toMyFixed(2));

                $("#House").val(data.House);//房产
                $("#Car").val(data.Car);//汽车
                $("#Other").val(data.Other);//其他个人资产
                $("#OtherProperty").val(data.OtherProperty);//其他资产
                $("#PersonSubtotal").val((data.House + data.Car + data.Other).toMyFixed(2));

                $("#TotalProperty").val(data.TotalProperty);//资产总计
                $("#TotalProperty2").val(data.TotalProperty);//资产总计

                $("#ShortTermLoan").val(data.ShortTermLoan);//短期贷款
                $("#MediumTermLoans").val(data.MediumTermLoans);//中期贷款
                $("#LongTermLoan").val(data.LongTermLoan);//长期贷款
                $("#OtherLoan").val(data.OtherLoan);//其他贷款
                $("#LoanSubtotal").val((data.ShortTermLoan + data.MediumTermLoans + data.LongTermLoan + data.OtherLoan).toMyFixed(2));

                $("#MedicalCosts").val(data.MedicalCosts);//临终医疗费用
                $("#TaxCosts").val(data.TaxCosts);//预期收入纳税额支出
                $("#FuneralExpenses").val(data.FuneralExpenses);//丧葬费用
                $("#HeritageCosts").val(data.HeritageCosts);//遗产处置费用
                $("#OtherCosts").val(data.OtherCosts);//其他费用
                $("#CostSubtotal").val((data.MedicalCosts + data.TaxCosts + data.FuneralExpenses + data.HeritageCosts + data.OtherCosts).toMyFixed(2));

                $("#OtherLiabilities").val(data.OtherLiabilities);//其他负债
                $("#TotalLiabilities").val(data.TotalLiabilities);//负债总计
                $("#TotalLiabilities2").val(data.TotalLiabilities);

                $("#FinanceAnalysis").val(data.FinanceAnalysis);//财务分析
                $("#PlanTool").val(data.PlanTool);//财产传承规划工具
                $("#PlanAnalysis").val(data.PlanAnalysis);//财产传承规划分析
             
                $("#TotalHeritage").val((data.TotalProperty - data.TotalLiabilities).toFixed(2));//净遗产总计
                //隐藏域
                $("#Id").val(data.Id);
            }
        }
    });
}

/**
 * @name 新增修改财产传承
 */
function EditHeritage(valu) {
    var ProposalId = $.getUrlParam("ProposalId");
    //页面字段检测
    if (!VerificationHelper.checkFrom("Heritage")) {
        return;
    } else {
        //此处参数必须跟VM一致
        var obj = new Object();
        obj["Id"] = $("#Id").val();
        obj["ProposalId"] = ProposalId;
        obj["Cash"] = $("#Cash").val();//现金
        obj["Deposit"] = $("#Deposit").val();//银行存款
        obj["LifeInsurance"] = $("#LifeInsurance").val();//人寿保单赔偿金额
        obj["OtherCashAccount"] = $("#OtherCashAccount").val();//其他现金账户
        obj["Stock"] = $("#Stock").val();//股票
        obj["Bond"] = $("#Bond").val();//债券
        obj["Fund"] = $("#Fund").val();//基金
        obj["OtherInvestment"] = $("#OtherInvestment").val();//其他投资收益
        obj["Pension"] = $("#Pension").val();//养老金（一次性收入现值）
        obj["AnnuityRevenue"] = $("#AnnuityRevenue").val();//配偶/遗孤年金收益（现值）
        obj["OtherPension"] = $("#OtherPension").val();//其他退休基金
        obj["House"] = $("#House").val();//房产
        obj["Car"] = $("#Car").val();//汽车
        obj["Other"] = $("#Other").val();//其他个人资产
        obj["OtherProperty"] = $("#OtherProperty").val();//其他资产
        obj["TotalProperty"] = $("#TotalProperty").val();//资产总计
        obj["ShortTermLoan"] = $("#ShortTermLoan").val();//短期贷款
        obj["MediumTermLoans"] = $("#MediumTermLoans").val();//中期贷款
        obj["LongTermLoan"] = $("#LongTermLoan").val();//长期贷款
        obj["OtherLoan"] = $("#OtherLoan").val();//其他贷款
        obj["MedicalCosts"] = $("#MedicalCosts").val();//临终医疗费用
        obj["TaxCosts"] = $("#TaxCosts").val();//预期收入纳税额支出
        obj["FuneralExpenses"] = $("#FuneralExpenses").val();//丧葬费用
        obj["HeritageCosts"] = $("#HeritageCosts").val();//遗产处置费用
        obj["OtherCosts"] = $("#OtherCosts").val();//其他费用
        obj["OtherLiabilities"] = $("#OtherLiabilities").val();//其他负债
        obj["TotalLiabilities"] = $("#TotalLiabilities").val();//负债总计
        obj["FinanceAnalysis"] = $("#FinanceAnalysis").val();//财务分析
        obj["PlanTool"] = $("#PlanTool").val();//财产传承规划工具
        obj["PlanAnalysis"] = $("#PlanAnalysis").val();//财产传承规划分析


        _ajaxhepler({
            url: "/CompetitionUser/Heritage/AddHeritage",
            type: "POST",
            async: false,
            dataType: "json",
            data: obj,
            success: function (data) {
                //保存原值
                SaveDefaultValueCommon("Heritage");
                if (valu == 0) {
                    //弹出成功提示  
                    dialogHelper.Success({
                        content: "保存成功！", success: function () {
                            //刷新当前页
                            location.href = location.href;
                        }
                    });
                } else {
                    window.location.href = "/CompetitionUser/ConsumptionPlan/Index" + URL;
                }
               
            }
        });
    }
}






