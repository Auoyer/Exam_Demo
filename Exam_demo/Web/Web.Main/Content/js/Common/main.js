$(function () {
    //获取URL参数
    $.getUrlParam = function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null)
            return unescape(r[2]);
        return null;
    }
    //为IE8，9的input文本框提供提示语
    $(".DefaultValue").live("blur", function () {
        if ($.trim($(this).val()).length < 1) {
            $(this).val($(this).attr("DefaultValue")).css("color", "#c8c8c8");
        } else {
            if ($.trim($(this).val()) == $(this).attr("DefaultValue")) {
                $(this).val($(this).attr("DefaultValue")).css("color", "#c8c8c8");
            }
        }
    }).live("focus", function () {
        if ($.trim($(this).val()) == $(this).attr("DefaultValue"))
            $(this).val("").css("color", "#000");
    }).css("color", "#c8c8c8");

    //针对IE8下textarea不支持maxlength进行特殊处理 keydown
    $("textarea").live("keyup", function (event) {
        var text = $(this).val();
        var maxlength = $(this).attr("maxlength");
        if (text.length > parseInt(maxlength)) {
            $(this).val(text.substring(0, maxlength));
        }
    });
})



/**
 * html转义
 * @param str 需要转义的字符串
 */
function htmlEncode(str) {
    var div = document.createElement("div");
    div.appendChild(document.createTextNode(str));
    return div.innerHTML;

    //传统写法
    //var s = ""; 
    //if (str.length == 0) return ""; 
    //s = str.replace(/&/g, "&amp;"); 
    //s = s.replace(/</g, "&lt;"); 
    //s = s.replace(/>/g, "&gt;"); 
    //s = s.replace(/ /g, "&nbsp;"); 
    //s = s.replace(/\'/g, "&#39;"); 
    //s = s.replace(/\"/g, "&quot;"); 
    //s = s.replace(/\n/g, "<br/>"); 
    //return s; 
};

/**
 * html反向转义
 * @param str 需要反向转义的字符串
 */
function htmlDecode(str) {
    var s = "";
    if (str.length == 0) return "";
    s = str.replace(/&amp;/g, "&");
    s = s.replace(/&lt;/g, "<");
    s = s.replace(/&gt;/g, ">");
    s = s.replace(/&nbsp;/g, " ");
    s = s.replace(/&#39;/g, "\'");
    s = s.replace(/&quot;/g, "\"");
    s = s.replace(/<br\/>/g, "\n");
    return s;
};

/**
 * html反向转义
 * @param str 需要反向转义的字符串
 */
function htmlDecode2(str) {
    if (str != null && str != "") {
        var s = "";
        if (str.length == 0) return "";
        s = str.replace(/&amp;/g, "&");
        s = s.replace(/&lt;/g, "<");
        s = s.replace(/&gt;/g, ">");
        s = s.replace(/&nbsp;/g, " ");
        s = s.replace(/&#39;/g, "\'");
        s = s.replace(/&quot;/g, "\"");
        s = s.replace(/\n/g, "<br\/>");
        return s;
    }
};

//乘法运算
function Multiplication(num1, num2) {
    var square = 0; //4次方
    var n1 = num1.toString();
    var n2 = num2.toString();
    try {
        square += n1.split(".")[1].length;
    } catch (ex) { }
    try {
        square += n2.split(".")[1].length;
    } catch (ex) { }
    return Number(n1.replace(".", "")) * Number(n2.replace(".", "")) / Math.pow(10, square);
}

Number.prototype.toMyFixed = function (s) {
    return (Math.round(Multiplication(this, Math.pow(10, s))) / Math.pow(10, s)).toFixed(s);
}


//计算天数差的函数，通用  
function DateDiff(sDate1, sDate2) {    //sDate1和sDate2是2006-12-18格式
    if (sDate1.length > 10) {
        sDate1 = sDate1.substring(0, 10);
    }
    if (sDate2.length > 10) {
        sDate2 = sDate2.substring(0, 10);
    }
    var aDate, oDate1, oDate2, iDays;
    aDate = sDate1.split("-");
    oDate1 = new Date(aDate[0], aDate[1] - 1, aDate[2]);     //调用Date的构造函数 
    aDate = sDate2.split("-");
    oDate2 = new Date(aDate[0], aDate[1] - 1, aDate[2]);
    iDays = parseInt(Math.abs(oDate1 - oDate2) / 1000 / 60 / 60 / 24);     //把相差的毫秒数转换为天数
    return iDays;
}

//加载每月可支配资金 可用资产
function EveryMonthMoney(UR, ProposalId, PlanType) {

    $.ajax({
        url: "/CompetitionUser/" + UR,
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: ProposalId
        },
        success: function (datas) {

            //计算公式=表2【自由储蓄】数值÷12－消费规划中【每月定期投资金额】－创业规划中【每月定期投资金额】－退休规划中[每月定期投资金额]
            var freedom = 0;
            var MonthlyInvestment = 0;
            var MonthlyInvestment2 = 0;
            var MonthlyInvestment3 = 0;
            var MonthlyInvestment4 = 0;
            // 收支存蓄表【自由储蓄】数值÷12
            var IAE = datas.IAE;
            if (IAE != null) {
                //理财支出
                var licai = IAE.InterestExpense + IAE.InsuranceExpense + IAE.OtherFinanceExpense;
                //	工作储蓄=工作收入－生活支出
                var workExist = (IAE.JobIncome + IAE.EndowmentInsurance + IAE.MedicalInsurance + IAE.HousingFund + IAE.OtherJobIncome) - (IAE.FamilyExpense + IAE.ChildExpense + IAE.OtherExpense)
                // 理财储蓄=理财收入－理财支出
                var licaiExist = (IAE.Interest + IAE.CapitalGains + IAE.OtherIncome) - (licai)
                //自由储蓄
                freedom = (workExist + licaiExist) - (IAE.EndowmentInsurance + IAE.HousingFund);

                //消费规划中【每月定期投资金额】
                var CP = datas.CP;
                if (CP != null) {
                    MonthlyInvestment4 = CP.MonthlyInvestment;

                }
                //创业规划中【每月定期投资金额】
                var SUP = datas.SUP;
                if (SUP != null) {
                    MonthlyInvestment = SUP.MonthlyInvestment;
                }
                //退休规划中[每月定期投资金额]
                var RP = datas.RP;
                if (RP != null) {
                    MonthlyInvestment2 = RP.MonthlyInvestment;
                }

                //获取教育规划【每月定期投资金额】
                var LEP = datas.LEP;
                if (LEP != null) {
                    MonthlyInvestment3 = LEP.MonthlyInvestment;
                }
                var Money = 0
                //每月可支配资金
                var Money = (freedom / 12) - MonthlyInvestment4 - MonthlyInvestment - MonthlyInvestment2 - MonthlyInvestment3;
                //if (PlanType == "LifeEducationPlan") {
                //    Money = (freedom / 12) - MonthlyInvestment3;
                //} else if (PlanType == "ConsumptionPlan") {
                //    Money = (freedom / 12) - MonthlyInvestment3 - MonthlyInvestment4;
                //} else if (PlanType == "StartAnUndertakingPlan") {
                //    Money = (freedom / 12) - MonthlyInvestment3 - MonthlyInvestment4 - MonthlyInvestment;
                //}
                $("#monthMoney").val(Money.toMyFixed(2));
            }

            //计算公式=表1【净值合计】栏－现金规划中【现金保留规模】栏数值－消费规划中【一次性投入金额】－创业规划中【一次性投入金额】－退休规划中【一次性投入金额】－保险规划中【预算金额】
            var J = 0;
            var RetainCashType = 0;
            var DisposableInput2 = 0;

            var CostInput = 0;
            var ReturnOnInvestmentRate = 0;
            var DisposableInput = 0;
            var BudgetAmount1 = 0;
            //资产负债表【净值合计】栏
            var L = datas.L;
            if (L != null) {
                //消费净值
                var X = (L.Cash + L.RMBDeposit + L.OtherAsset) - (L.CreditCard + L.Microfinance + L.OtherLoan);
                //投资净值
                var a = L.RMBFixedDeposit;
                var b = L.ForeignCurrencyFixedDeposit;
                var c = L.StockInvestment;
                var d = L.BondInvestment;
                var e = L.FundInvestment;
                var f = L.IndustryInvestment;
                var g = L.EstateInvestment;
                var h = L.PolicyInvestment;
                var i = L.OtherInvestment;
                var T = (a + b + c + d + e + f + g + h + i) - (L.FinancialLoan + L.IndustryInvestmentLoan + L.EstateInvestmentLoan + L.OtherInvestmentLoan)
                //自用净值
                var Z = (L.Estate + L.Car + L.Others) - (L.EstateLoan + L.CarLoan + L.OthersLoan);

                //净值合计
                J = X + T + Z;

                //现金规划中【现金保留规模】
                var CP2 = datas.CP2;
                if (CP2 != null) {
                    RetainCashType = CP2.RetainCashMultiple;
                }
                //消费规划中【一次性投入金额】
                var CP = datas.CP;
                if (CP != null) {
                    DisposableInput2 = CP.DisposableInput;
                }
                //创业规划中【一次性投入金额】
                var SUP = datas.SUP;
                if (SUP != null) {
                    CostInput = SUP.DisposableInput;
                }
                //退休规划中【一次性投入金额】
                var RP = datas.RP;
                if (RP != null) {
                    ReturnOnInvestmentRate = RP.DisposableInput;
                }
                //保险规划中【预算金额】
                var IPS = datas.IPS;
                if (IPS != null) {
                    if (IPS.BudgetAmount1 == null || IPS.BudgetAmount1 == "") {
                        BudgetAmount1 = 0;// IPS.BudgetAmount2;
                    } else {
                        BudgetAmount1 = IPS.BudgetAmount;
                    }
                }
                //获取教育规划【一次性投资金额】
                var LEP = datas.LEP;
                if (LEP != null) {
                    DisposableInput = LEP.DisposableInput;
                }
                //可用资产
                var UserableAsset = (J - RetainCashType - DisposableInput2 - CostInput - ReturnOnInvestmentRate - BudgetAmount1 - DisposableInput).toMyFixed(2);
                $("#UserableAsset").val(UserableAsset);
            }
        }
    });
}

//***********************************************************
//预览 的每月算法。~~~~~~~~~~~~~~~~~~~柴志明我真的想杀了你。。
//***********************************************************
function ViewEveryMonthMoney(UR, ProposalId, div) {

    $.ajax({
        url: "/CompetitionUser/" + UR,
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: ProposalId
        },
        success: function (datas) {

            //计算公式=表2【自由储蓄】数值÷12－消费规划中【每月定期投资金额】－创业规划中【每月定期投资金额】－退休规划中[每月定期投资金额]
            var freedom = 0;
            var MonthlyInvestment = 0;
            var MonthlyInvestment2 = 0;
            var MonthlyInvestment3 = 0;
            var MonthlyInvestment4 = 0;
            // 收支存蓄表【自由储蓄】数值÷12
            var IAE = datas.IAE;
            if (IAE != null) {
                //理财支出
                var licai = IAE.InterestExpense + IAE.InsuranceExpense + IAE.OtherFinanceExpense;
                //	工作储蓄=工作收入－生活支出
                var workExist = (IAE.JobIncome + IAE.EndowmentInsurance + IAE.MedicalInsurance + IAE.HousingFund + IAE.Interest) - (IAE.FamilyExpense + IAE.ChildExpense + IAE.OtherExpense)
                // 理财储蓄=理财收入－理财支出
                var licaiExist = (IAE.Interest + IAE.CapitalGains + IAE.OtherIncome) - (licai)
                //自由储蓄
                freedom = (workExist + licaiExist) - (IAE.EndowmentInsurance + IAE.HousingFund);

                //消费规划中【每月定期投资金额】
                var CP = datas.CP;
                if (CP != null) {
                    MonthlyInvestment4 = CP.MonthlyInvestment;

                }
                //创业规划中【每月定期投资金额】
                var SUP = datas.SUP;
                if (SUP != null) {
                    MonthlyInvestment = SUP.MonthlyInvestment;
                }
                //退休规划中[每月定期投资金额]
                var RP = datas.RP;
                if (RP != null) {
                    MonthlyInvestment2 = RP.MonthlyInvestment;
                }

                //获取教育规划【每月定期投资金额】
                var LEP = datas.LEP;
                if (LEP != null) {
                    MonthlyInvestment3 = LEP.MonthlyInvestment;
                }

                //每月可支配资金
                var Money = 0
                //每月可支配资金
                var Money = (freedom / 12) - MonthlyInvestment4 - MonthlyInvestment - MonthlyInvestment2 - MonthlyInvestment3;
                //if (div == "LifeEducationPlan") {
                //    Money = (freedom / 12) - MonthlyInvestment3;
                //} else if (div == "ConsumptionPlan") {
                //    Money = (freedom / 12) - MonthlyInvestment3 - MonthlyInvestment4;
                //} else if (div == "StartAnUndertakingPlan") {
                //    Money = (freedom / 12) - MonthlyInvestment3 - MonthlyInvestment4 - MonthlyInvestment;
                //}
                $("#" + div + " .monthMoney").text(Money.toMyFixed(2));

            }

            //计算公式=表1【净值合计】栏－现金规划中【现金保留规模】栏数值－消费规划中【一次性投入金额】－创业规划中【一次性投入金额】－退休规划中【一次性投入金额】－保险规划中【预算金额】
            var J = 0;
            var RetainCashType = 0;
            var DisposableInput2 = 0;

            var CostInput = 0;
            var ReturnOnInvestmentRate = 0;
            var DisposableInput = 0;
            var BudgetAmount1 = 0;
            //资产负债表【净值合计】栏
            var L = datas.L;
            if (L != null) {
                //消费净值
                var X = (L.Cash + L.RMBDeposit + L.OtherAsset) - (L.CreditCard + L.Microfinance + L.OtherLoan);
                //投资净值
                var a = L.RMBFixedDeposit;
                var b = L.ForeignCurrencyFixedDeposit;
                var c = L.StockInvestment;
                var d = L.BondInvestment;
                var e = L.FundInvestment;
                var f = L.IndustryInvestment;
                var g = L.EstateInvestment;
                var h = L.PolicyInvestment;
                var i = L.OtherInvestment;
                var T = (a + b + c + d + e + f + g + h + i) - (L.FinancialLoan + L.IndustryInvestmentLoan + L.EstateInvestmentLoan + L.OtherInvestmentLoan)
                //自用净值
                var Z = (L.Estate + L.Car + L.Others) - (L.EstateLoan + L.CarLoan + L.OthersLoan);

                //净值合计
                J = X + T + Z;

                //现金规划中【现金保留规模】
                var CP2 = datas.CP2;
                if (CP2 != null) {
                    RetainCashType = CP2.RetainCashMultiple;
                }
                //消费规划中【一次性投入金额】
                var CP = datas.CP;
                if (CP != null) {
                    DisposableInput2 = CP.DisposableInput;
                }
                //创业规划中【一次性投入金额】
                var SUP = datas.SUP;
                if (SUP != null) {
                    CostInput = SUP.DisposableInput;
                }
                //退休规划中【一次性投入金额】
                var RP = datas.RP;
                if (RP != null) {
                    ReturnOnInvestmentRate = RP.DisposableInput;
                }
                //保险规划中【预算金额】
                var IPS = datas.IPS;
                if (IPS != null) {
                    if (IPS.BudgetAmount1 == null || IPS.BudgetAmount1 == "") {
                        BudgetAmount1 = 0;// IPS.BudgetAmount2;
                    } else {
                        BudgetAmount1 = IPS.BudgetAmount;
                    }
                }
                //获取教育规划【一次性投资金额】
                var LEP = datas.LEP;
                if (LEP != null) {
                    DisposableInput = LEP.DisposableInput;
                }
                //可用资产
                var UserableAsset = (J - RetainCashType - DisposableInput2 - CostInput - ReturnOnInvestmentRate - BudgetAmount1 - DisposableInput).toMyFixed(2);
                $("#" + div + " .UserableAsset").text(UserableAsset);

            }
        }
    });
};

function JudgesViewEveryMonthMoney(UR, ProposalId, div) {

    $.ajax({
        url: "/CompetitionJudges/" + UR,
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: ProposalId
        },
        success: function (datas) {

            //计算公式=表2【自由储蓄】数值÷12－消费规划中【每月定期投资金额】－创业规划中【每月定期投资金额】－退休规划中[每月定期投资金额]
            var freedom = 0;
            var MonthlyInvestment = 0;
            var MonthlyInvestment2 = 0;
            var MonthlyInvestment3 = 0;
            var MonthlyInvestment4 = 0;
            // 收支存蓄表【自由储蓄】数值÷12
            var IAE = datas.IAE;
            if (IAE != null) {
                //理财支出
                var licai = IAE.InterestExpense + IAE.InsuranceExpense + IAE.OtherFinanceExpense;
                //	工作储蓄=工作收入－生活支出
                var workExist = (IAE.JobIncome + IAE.EndowmentInsurance + IAE.MedicalInsurance + IAE.HousingFund + IAE.Interest) - (IAE.FamilyExpense + IAE.ChildExpense + IAE.OtherExpense)
                // 理财储蓄=理财收入－理财支出
                var licaiExist = (IAE.Interest + IAE.CapitalGains + IAE.OtherIncome) - (licai)
                //自由储蓄
                freedom = (workExist + licaiExist) - (IAE.EndowmentInsurance + IAE.HousingFund);

                //消费规划中【每月定期投资金额】
                var CP = datas.CP;
                if (CP != null) {
                    MonthlyInvestment4 = CP.MonthlyInvestment;

                }
                //创业规划中【每月定期投资金额】
                var SUP = datas.SUP;
                if (SUP != null) {
                    MonthlyInvestment = SUP.MonthlyInvestment;
                }
                //退休规划中[每月定期投资金额]
                var RP = datas.RP;
                if (RP != null) {
                    MonthlyInvestment2 = RP.MonthlyInvestment;
                }

                //获取教育规划【每月定期投资金额】
                var LEP = datas.LEP;
                if (LEP != null) {
                    MonthlyInvestment3 = LEP.MonthlyInvestment;
                }

                //每月可支配资金
                var Money = 0
                //每月可支配资金
                var Money = (freedom / 12) - MonthlyInvestment4 - MonthlyInvestment - MonthlyInvestment2 - MonthlyInvestment3;
                //if (div == "LifeEducationPlan") {
                //    Money = (freedom / 12) - MonthlyInvestment3;
                //} else if (div == "ConsumptionPlan") {
                //    Money = (freedom / 12) - MonthlyInvestment3 - MonthlyInvestment4;
                //} else if (div == "StartAnUndertakingPlan") {
                //    Money = (freedom / 12) - MonthlyInvestment3 - MonthlyInvestment4 - MonthlyInvestment;
                //}
                $("#" + div + " .monthMoney").text(Money.toMyFixed(2));

            }

            //计算公式=表1【净值合计】栏－现金规划中【现金保留规模】栏数值－消费规划中【一次性投入金额】－创业规划中【一次性投入金额】－退休规划中【一次性投入金额】－保险规划中【预算金额】
            var J = 0;
            var RetainCashType = 0;
            var DisposableInput2 = 0;

            var CostInput = 0;
            var ReturnOnInvestmentRate = 0;
            var DisposableInput = 0;
            var BudgetAmount1 = 0;
            //资产负债表【净值合计】栏
            var L = datas.L;
            if (L != null) {
                //消费净值
                var X = (L.Cash + L.RMBDeposit + L.OtherAsset) - (L.CreditCard + L.Microfinance + L.OtherLoan);
                //投资净值
                var a = L.RMBFixedDeposit;
                var b = L.ForeignCurrencyFixedDeposit;
                var c = L.StockInvestment;
                var d = L.BondInvestment;
                var e = L.FundInvestment;
                var f = L.IndustryInvestment;
                var g = L.EstateInvestment;
                var h = L.PolicyInvestment;
                var i = L.OtherInvestment;
                var T = (a + b + c + d + e + f + g + h + i) - (L.FinancialLoan + L.IndustryInvestmentLoan + L.EstateInvestmentLoan + L.OtherInvestmentLoan)
                //自用净值
                var Z = (L.Estate + L.Car + L.Others) - (L.EstateLoan + L.CarLoan + L.OthersLoan);

                //净值合计
                J = X + T + Z;

                //现金规划中【现金保留规模】
                var CP2 = datas.CP2;
                if (CP2 != null) {
                    RetainCashType = CP2.RetainCashMultiple;
                }
                //消费规划中【一次性投入金额】
                var CP = datas.CP;
                if (CP != null) {
                    DisposableInput2 = CP.DisposableInput;
                }
                //创业规划中【一次性投入金额】
                var SUP = datas.SUP;
                if (SUP != null) {
                    CostInput = SUP.DisposableInput;
                }
                //退休规划中【一次性投入金额】
                var RP = datas.RP;
                if (RP != null) {
                    ReturnOnInvestmentRate = RP.DisposableInput;
                }
                //保险规划中【预算金额】
                var IPS = datas.IPS;
                if (IPS != null) {
                    if (IPS.BudgetAmount1 == null || IPS.BudgetAmount1 == "") {
                        BudgetAmount1 = 0;// IPS.BudgetAmount2;
                    } else {
                        BudgetAmount1 = IPS.BudgetAmount;
                    }
                }
                //获取教育规划【一次性投资金额】
                var LEP = datas.LEP;
                if (LEP != null) {
                    DisposableInput = LEP.DisposableInput;
                }
                //可用资产
                var UserableAsset = (J - RetainCashType - DisposableInput2 - CostInput - ReturnOnInvestmentRate - BudgetAmount1 - DisposableInput).toMyFixed(2);
                $("#" + div + " .UserableAsset").text(UserableAsset);

            }
        }
    });
};

//******************************
//公共上面菜单跳转方法
//******************************

var hdParam = "";
//公共菜单跳转
//定义一个顶部导航跳转菜单第
function navFinancePage(page, LoadPage) {
    //  var ProposalId = $.getUrlParam("ProposalId");
    hdParam = $("#hdParam").val();
    dialogHelper.Confirm({
        content: "当前页面内容未保存，是否继续跳转？",
        success: function () {
            if (typeof LoadPage == "undefined") {
                window.location.href = "/CompetitionUser/" + page + "/Index" + hdParam;
            } else {
                window.location.href = "/CompetitionUser/" + page + "/" + LoadPage + hdParam;
            }
            // eval(func);
        },
        cancle: function () {
            //  eval(func);
        }
    });
};

function navFinancePageTwo(url) {
    dialogHelper.Confirm({
        content: "当前页面内容未保存，是否继续跳转？",
        success: function () {
            window.location.href = url;
        },
        cancle: function () {

        }
    });
}


//保存原值----------------------------------------------这个是加在页面的时调用
function SaveDefaultValueCommon(divElementId) {
    //var compStar = "";
    //var compEnd = "";
    //var compTr = "";
    var compjson = "{";
    var index = 0;

    //*************************************************特殊处理层统一放在这里，不然容易乱。
    if (divElementId == "FinanceInvestmentPlanDiv") {
        $("#" + divElementId + " input[type='hidden']").each(function () {
            $(this).attr("defaultVal", $(this).val());
        });
    };
    //************************************************************************************

    $("#" + divElementId + " input[type='text']").each(function () {

        //compjson += $(this).attr('id') + ":\"" + $(this).val() + "\","; ---别删除了这个留作以后出错对比用
        $(this).attr("defaultVal", $(this).val());
    });

    $("#" + divElementId + " textarea").each(function () {

        $(this).attr("defaultVal", $(this).val());
    });

    $("#" + divElementId + " select").each(function () {
        $(this).attr("defaultVal", $(this).val());
    });
    compjson = compjson.replace(/\,$/, "}");
    //var obj = eval('(' + compjson + ')');
    //return obj;
};

//对比JS参数1主层，2跳转页，建议书号（可以不传暂时）---------------------------注意这是主方法放在a标签的href里面滴啊用
function SaveJudgeTag(divElementId, NavPage, LoadPage) {
    //获取URL参数
    //  var ProposalId = $.getUrlParam("ProposalId");
    hdParam = $("#hdParam").val();
    //申请单号
    var oldVal = "";
    var newVal = "";
    var columns = new Array();//记录被修改的控件名
    var flag = false;
    //验证哪些文本框被修改

    $("#" + divElementId + " input[type='text']").not(".disabled").not(".b-gray").each(function () {
        newVal = $.trim($(this).val());
        oldVal = $.trim($(this).attr("defaultVal"));
        if (oldVal == undefined) {
            oldVal = "";
        }
        if (newVal != oldVal) {
            flag = true;
            return false;
        }
    });

    $("#" + divElementId + " select").not(".disabled").not(".b-gray").each(function () {
        newVal = $.trim($(this).val());
        oldVal = $(this).attr("defaultVal");
        if (oldVal == undefined) {
            oldVal = "";
        }
        if (newVal != oldVal) {
            flag = true;
            return false;
        }
    });

    $("#" + divElementId + " textarea").not(".disabled").not(".b-gray").each(function () {
        newVal = $.trim($(this).val());
        oldVal = $.trim($(this).attr("defaultVal"));
        if (oldVal == undefined) {
            oldVal = "";
        }
        if (newVal != oldVal) {
            flag = true;
            return false;
        }
    });

    if (flag) {
        navFinancePage(NavPage, LoadPage);
    } else {
        if (typeof LoadPage == "undefined") {
            window.location.href = "/CompetitionUser/" + NavPage + "/Index" + hdParam;
        } else {
            window.location.href = "/CompetitionUser/" + NavPage + "/" + LoadPage + hdParam;
        }
    }

};

function SaveJudgeTagTwo(url) {
    //获取URL参数
    //  var ProposalId = $.getUrlParam("ProposalId");
    //申请单号
    var oldVal = "";
    var newVal = "";
    var columns = new Array();//记录被修改的控件名
    var flag = false;
    var divId = "";

    //********************************特殊处理统一放这里
    divId = $("div .FinancePlan").attr("id");
    if (divId == "FinanceInvestmentPlanDiv") {
        $("#" + divId + " input[type='hidden']").each(function () {
            newVal = $.trim($(this).val());
            oldVal = $.trim($(this).attr("defaultVal"));
            if (oldVal == undefined) {
                oldVal = "";
            }
            if (newVal != oldVal) {
                flag = true;
                return false;
            }
        });
    };

    //*********************************************


    //验证哪些文本框被修改
    $(".FinancePlan input[type='text']").not(".disabled").not(".b-gray").each(function () {
        newVal = $.trim($(this).val());
        oldVal = $.trim($(this).attr("defaultVal"));
        if (oldVal == undefined) {
            oldVal = "";
        }
        if (newVal != oldVal) {
            flag = true;
            return false;
        }
    });

    $(".FinancePlan select").not(".disabled").not(".b-gray").each(function () {
        newVal = $.trim($(this).val());
        oldVal = $(this).attr("defaultVal");
        if (oldVal == undefined) {
            oldVal = "";
        }
        if (newVal != oldVal) {
            flag = true;
            return false;
        }
    });

    $(".FinancePlan textarea").not(".disabled").not(".b-gray").each(function () {
        newVal = $.trim($(this).val());
        oldVal = $.trim($(this).attr("defaultVal"));
        if (oldVal == undefined) {
            oldVal = "";
        }
        if (newVal != oldVal) {
            flag = true;
            return false;
        }
    });

    if (flag) {
        navFinancePageTwo(url);
    } else {
        window.location.href = url;

    }

}



/**
 * 客户信息是否保存
 */
function IsProposalSave() {
    var flag = $(".ims-left ul li:eq(0) a").hasClass("finish");
    if (!flag) {
        //未保存客户信息
        //1.禁用所有输入框、下拉框
        $(".con-right input[type='text']").attr("disabled", true).addClass("disabled");
        $(".con-right input[type='radio']").attr("disabled", true).addClass("disabled");
        $(".con-right textarea").attr("disabled", true).addClass("disabled");
        $(".con-right select").attr("disabled", true).addClass("disabled");
        //2.禁用按钮
        $(".con-right input[type='button']").unbind("click").removeAttr("onclick").attr("disabled", true).addClass("btn-disabled");
        $(".con-right .spr-del").unbind("click").removeAttr("onclick").attr("href", "javascript:void(0);");
        $(".con-right .add-sib").unbind("click").removeAttr("onclick");
        //3.弹出错误提示
        dialogHelper.Error({ content: msgList["20011"] });
    }
}

/**
 * 获取空格
 * @param num 多少个中文字符空格(1中文=2空格)
 */
function GetSpace(num) {
    var space = "";
    for (var i = 0; i < num; i++) {
        space += "&nbsp;&nbsp;&nbsp;&nbsp;";
    }
    return space;
}

/**
 * 登出系统
 */
function SignOut() {
    dialogHelper.Confirm({
        content: msgList["20002"],
        success: function () {
            location.href = "/SignIn/SignOut";
        }
    });
}

/**
 * 登出系统2 -非超管
 */
function SignOut2() {
    dialogHelper.Confirm({
        //content: msgList["20002"],
        content: "是否确认退出？",
        success: function () {
            location.href = "/SignIn/SignOut2";
        }
    });
}

/**
 * 修改密码
 */
function ChangePwd() {
    $("#btnChangePwd").unbind("click").click(function () {
        var area = $("#hdArea").val();
        var oldPwd = $.trim($("#txtOldPwd").val());
        var newPwd = $.trim($("#txtNewPwd").val());
        var confirmPwd = $.trim($("#txtConfirmPwd").val());

        //输入验证
        var checkData = function () {
            if (newPwd != confirmPwd) {
                showValidateMsg("txtConfirmPwd", "确认密码与新密码不一致!");
            }
        };
        if (!VerificationHelper.checkFrom("popChangePassword", checkData)) {
            return;
        }

        _ajax_backup({
            url: "/" + area + "/Common/ChangePass",
            type: "POST",
            dataType: "json",
            data:
            {
                oldPwd: oldPwd,
                newPwd: newPwd,
                rId: Math.random()
            },
            //成功，绑定数据
            success: function (data) {
                if (data.IsSuccess) {
                    dialogHelper.Success({
                        content: "密码修改成功，请重新登录！",
                        success: function () {
                            if (area == "admin")
                                location.href = "/SignIn/Index";
                            else {
                                // 用户端密码修改成功后，跳转到官网首页
                                location.href = "/";
                                //location.href = "/" + area + "/Home/Index";
                            }
                            //location.href = location.href;
                        }
                    });
                } else {
                    dialogHelper.Error({
                        content: msgList[data.ErrorCode]
                    });
                }
            }
        });
    });
    $("#popChangePassword .btn-close").unbind("click").click(function () {
        dialogHelper.Close("popChangePassword");
    });

    //清空数据
    $("#popChangePassword .warn-box").remove()
    $("#txtOldPwd").val("");
    $("#txtNewPwd").val("");
    $("#txtConfirmPwd").val("");
    dialogHelper.Show("popChangePassword");

}




//**********************************
//公共PV方法
//**********************************
//公共PV方法 rate-每一期的利率,nper-所有的期数，一共存多少次,pmt-各期所应支付的金额,fv-未来值,begOfPeriodType-输入1是指期初付款-指月初付款
function CalcPVCommon(rate, nper, pmt, fv, begOfPeriodType) {
    var result = 0;
    if (typeof fv == "undefined") {
        fv = 0;
    };
    if (typeof begOfPeriodType == "undefined") {
        begOfPeriodType = 0;
    }
    $.ajax({
        url: "/CompetitionUser/Calculator/PVCommon",
        type: "POST",
        async: false,
        dataType: "json",
        data: {
            rate: rate / 100,
            nper: nper * 1,
            pmt: pmt * 1,
            fv: fv * 1,
            begOfPeriodType: begOfPeriodType * 1
        },
        success: function (date) {
            if (date != null && date != "") {
                var num = date;
                result = num.toMyFixed(2);
            } else {
                result = 0;
            }
        }
    });
    return result;
}


/**
 * 记录进入子系统的入口
 */
function RemarkSystemLogin() {
    var href = encodeURI(location.href);
    $.cookie('RemarkSystemLogin', href, { expires: 1, path: '/' });
}

/**
 * 子系统返回进入入口
 */
function SystemReturn() {
    var href = $.cookie('RemarkSystemLogin');
    if (href == null || href == "" || href == undefined) {
        location.href = "/CompetitionUser/StuCustomer/Index";
        return;
    }
    // location.href = href;
    SaveJudgeTagTwo(href);
}


//公共Fv方法"rate">每一期的利率"nper">所有的期数，一共存多少次 name="amount">每期存款的数字name="pv">返回投资的现值name="begOfPeriodType">输入1是指期初付款-指月初付款
function CalcFVCommon(rate, nper, amount, pv, begOfPeriodType) {
    var result = 0;
    if (typeof pv == "undefined") {
        pv = 0;
    };
    if (typeof begOfPeriodType == "undefined") {
        begOfPeriodType = 0;
    }
    $.ajax({
        url: "/CompetitionUser/Calculator/FVCommon",
        type: "POST",
        async: false,
        dataType: "json",
        data: {
            rate: rate / 100,
            nper: nper * 1,
            amount: amount * 1,
            pv: pv * 1,
            begOfPeriodType: begOfPeriodType * 1
        },
        success: function (date) {
            if (date != null && date != "") {
                var num = date;
                result = num.toMyFixed(2);
            } else {
                result = 0;
            }
        }
    });
    return result;
}

/**
 * 暂停执行（毫秒）
 */
function sleep(d) {
    for (var t = Date.now() ; Date.now() - t <= d;);
}

//*************************
//公共截取方法
//*************************
function LineBreak(txtAreaSub) {
    var txtArea = txtAreaSub;

    if (txtArea.length > 10) {
        txtArea = txtArea.substr(0, 9) + "...";
    }
    return txtArea;
}

//左右点击按钮滚动
function adactive() {
    $(".caption-list-title").each(function () {
        var Wid = 51,  //单个的宽度
			n = 1,     //一次翻动的个数 
			$Ul = $(this).children(".caption-switch").children(".caption-switch-con"),
			$Pre = $(this).find(".prev"),
			$Nex = $(this).find(".next"),
			Len = $Ul.children("span").length,
			Left = parseInt($Ul.css("left"));
        $Pre.click(function () {
            Left = parseInt(Left) + n * Wid;
            if (Left > 0) {
                Left = 0;
            }
            $Ul.stop().animate({ left: Left });
        });
        $Nex.click(function () {
            Left -= n * Wid;//n为一次滚动的个数
            if (Left < -Wid * (Len - 4)) {//4为显示的个数
                Left = -Wid * (Len - 4);
            };
            $Ul.stop().animate({ left: Left });
        });
        $(".caption-switch-con span").click(function () {
            $(this).addClass("on").siblings().removeClass("on");
        });
    });
}