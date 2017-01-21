//********************************
//投资规划------
//********************************

var ProposalId = 0; //建议书Id
var DelObjs = new Array();
var IndexFlag = 0;
var ShowPupTagId = "";//记录弹出层标记
var ShowRadioTag = "";//记录选择对象
var param = "";
var TagNavi = false;
var selecVal = "";//记录你选择完的对象 ---为了给搜索用的。
//判断是否非法字符
function CheckNum(num) {
    var pattern6 = /^[-]?\d+(\.\d{1,6})?$/;//只能输入两位小数
    if (!pattern6.test(num)) {
        num = 0;
        return num;
    }
    return num;
}

//计算百分百比。输入值不为百分之百
function CalcPerCent(HoldRate, IncreaseRate, SpeculationRate) {
    var HoldRate1 = CheckNum(HoldRate);
    var IncreaseRate1 = CheckNum(IncreaseRate);
    var SpeculationRate1 = CheckNum(SpeculationRate);
    var sum = 0;
    if (HoldRate1 == HoldRate && IncreaseRate1 == IncreaseRate && SpeculationRate1 == SpeculationRate) {
        sum = HoldRate * 1 + IncreaseRate * 1 + SpeculationRate * 1;
    } else {
        sum = 0;
    }
    return sum;

}

//("意外险", "重疾险", "寿险", "教育年金", "养老年金", "看护险");
var InsureArry = ["意外险", "重疾险", "寿险", "教育年金", "养老年金", "看护险"];
//赋值事件
var SetPercentVal = (function () {
    //百分比事件
    var setPercentValue = function (obj) {
        var selVal = $(obj).val();
        if (selVal == "1") {//单身期
            $("#InsureShow").val(InsureArry[0] + "、" + InsureArry[1]);
            //同时还要改变 保 -增 -投的值
            $("#HoldRate").val(10);
            $("#IncreaseRate").val(10);
            $("#SpeculationRate").val(80);
            ShowPieInfo(10, 10, 80);

        } else if (selVal == "2") {//家庭形成期（筑巢期）
            $("#InsureShow").val(InsureArry[0] + "、" + InsureArry[1] + "、" + InsureArry[2]);
            //同时还要改变 保 -增 -投的值
            $("#HoldRate").val(20);
            $("#IncreaseRate").val(10);
            $("#SpeculationRate").val(70);
            ShowPieInfo(20, 10, 70);

        } else if (selVal == "3") {//家庭成长期（满巢期）
            $("#InsureShow").val(InsureArry[0] + "、" + InsureArry[1] + "、" + InsureArry[2] + "、" + InsureArry[3]);
            //同时还要改变 保 -增 -投的值
            $("#HoldRate").val(10);
            $("#IncreaseRate").val(30);
            $("#SpeculationRate").val(60);
            ShowPieInfo(10, 30, 60);

        } else if (selVal == "4") {//家庭成熟期（离巢期）
            $("#InsureShow").val("意外险" + "、" + "重疾险" + "、" + "养老年金");
            //同时还要改变 保 -增 -投的值
            $("#HoldRate").val(10);
            $("#IncreaseRate").val(40);
            $("#SpeculationRate").val(50);
            ShowPieInfo(10, 40, 50);

        } else if (selVal == "5") {//家庭衰老期（空巢期）
            $("#InsureShow").val(InsureArry[0] + "、" + InsureArry[5]);
            //同时还要改变 保 -增 -投的值
            $("#HoldRate").val(20);
            $("#IncreaseRate").val(60);
            $("#SpeculationRate").val(20);
            ShowPieInfo(20, 60, 20);

        } else if (selVal == "0") {//当为选择的时候全部要清空
            $("#InsureShow").val("");
            $("#HoldRate").val(0);
            $("#IncreaseRate").val(0);
            $("#SpeculationRate").val(0);
            ShowPieInfo(0, 0, 0);
        }
        ////HedgeDiv 保值层 AddStar 增值层 SpeculateDiv 投机层
        if (selVal != "0") {
            AddStarSpan("HedgeDiv", "保值层");
            AddStarSpan("AddStar", "增值层");
            AddStarSpan("SpeculateDiv", "投机层");
        } else {
            ClearStarSpan("HedgeDiv", "保值层");
            ClearStarSpan("AddStar", "增值层");
            ClearStarSpan("SpeculateDiv", "投机层");
        }
    };
    //加载给值方法
    var setPercentValueTwo = function (selVal) {
        if (selVal == "1") {//单身期
            $("#InsureShow").val(InsureArry[0] + "、" + InsureArry[1]);
        } else if (selVal == "2") {//家庭形成期（筑巢期）
            $("#InsureShow").val(InsureArry[0] + "、" + InsureArry[1] + "、" + InsureArry[2]);
        } else if (selVal == "3") {//家庭成长期（满巢期）
            $("#InsureShow").val(InsureArry[0] + "、" + InsureArry[1] + "、" + InsureArry[2] + "、" + InsureArry[3]);
        } else if (selVal == "4") {//家庭成熟期（离巢期）
            $("#InsureShow").val("意外险" + "、" + "重疾险" + "、" + "养老年金");
        } else if (selVal == "5") {//家庭衰老期（空巢期）
            $("#InsureShow").val(InsureArry[0] + "、" + InsureArry[5]);
        }
    }

    //已完成规划事件的方案所需投资收益率
    var setInsetmentVal = function (PlanId, InsetmentType, ProposalId) {
        var ProductSelectDiv = $(PlanId).parent().parent().parent().parent().attr("id");
        _ajaxhepler({
            url: "/CompetitionUser/InvestmentPlan/RequestInsetmentVal",
            type: "POST",
            async: false,
            dataType: "json",
            data: {
                PlanType: InsetmentType,
                ProposalId: ProposalId
            },
            success: function (data) {
                if (data != null) {
                    $("#" + ProductSelectDiv).find("input[eacflag='PlanRate']").val(data.TargetAmount);
                } else {
                    $("#" + ProductSelectDiv).find("input[eacflag='PlanRate']").val("0");
                }
            }
        });
    };
    //各大银行的汇率改变运算--银行变动
    var GetBankDeposits = function (DemandDepositsBank, BankType, Isdeposit, DepositTime) {
        var dtime = 0;
        var DepositsBankRate = "";
        if (typeof DepositTime != "undefined") {
            dtime = DepositTime;
        }
        if (Isdeposit == false) {//表示活期
            DepositsBankRate = $(DemandDepositsBank).next().attr("id");
        } else {//定期
            DepositsBankRate = $(DemandDepositsBank).next().next().attr("id");
        }
        if (BankType == 0) { return false; }//为0的话根本不用查
        _ajaxhepler({
            url: "/CompetitionUser/InvestmentPlan/GetBankDepositsList",
            type: "POST",
            async: false,
            dataType: "json",
            data: {
                BankType: BankType
            },
            success: function (data) {
                //获得整个银行 活期定期的汇率
                if (data != null) {
                    if (Isdeposit == false) {//活期
                        $("#" + DepositsBankRate).val(data.DemandDeposit);
                    } else {
                        //定期。如果没选择月默认为0
                        if (dtime != 0) {
                            var result = checkRateBank(data, dtime);
                            $("#" + DepositsBankRate).val(result);
                        } else {
                            $("#" + DepositsBankRate).val(0);
                        }
                    }

                } else {
                    $("#" + DepositsBankRate).val(0);
                }
            }
        });
    };

    var GetBankDepositsTime = function (TimeDepositBankTime, BankType, DepositTime) {

        var DepositsBankRate = $(TimeDepositBankTime).next().attr("id");
        var result = 0;
        _ajaxhepler({
            url: "/CompetitionUser/InvestmentPlan/GetBankDepositsList",
            type: "POST",
            async: false,
            dataType: "json",
            data: {
                BankType: BankType
            },
            success: function (data) {
                if (data != null) {
                    result = checkRateBank(data, DepositTime);
                    $("#" + DepositsBankRate).val(result);
                }
            }
        });
    }


    var checkRateBank = function (data, deposit) {
        var cashRate = 0;
        if (deposit == 1) {//三月
            cashRate = data.ThreeMonth;
        } else if (deposit == 2) {//六月
            cashRate = data.SixMonth;
        } else if (deposit == 3) {//一年
            cashRate = data.Year;
        } else if (deposit == 4) {//二年
            cashRate = data.TwoYear;
        } else if (deposit == 5) {//三年
            cashRate = data.ThreeYear;
        } else if (deposit == 6) {//五年
            cashRate = data.FiveYear;
        }
        return cashRate;
    }

    return {
        setPercentValue: setPercentValue,
        setPercentValueTwo: setPercentValueTwo,
        setInsetmentVal: setInsetmentVal,
        GetBankDeposits: GetBankDeposits,
        GetBankDepositsTime: GetBankDepositsTime
    }
})();







$(function () {
    IsProposalSave()//客户验证

    param = $("#hdParam").val();
    var sum = 0;

    //获取URL参数
    ProposalId = $.getUrlParam("ProposalId");

    //获取财产传承
    if (ProposalId != null && ProposalId != "" && ProposalId != undefined) {
        //加载
        LoadInvestmentPlan(ProposalId);
    } else {
        return false
    }



    //点击添加
    $("#Add").unbind("click").bind("click", function () {
        var num = $("div[eacflag='ProductSelect']").size();
        if (num < 8) {
            AddList();
        }
    });
    //保存
    $("#btnSave").bind("click", function () {
        SaveProductPlan();
    });

    //绑定下拉选择事件
    $("#LifeCycleId").unbind("change").change(function () {
        var obj = new Object(this);
        SetPercentVal.setPercentValue(obj);
        //同时要联动有边动态图
        //var HoldRate = $.trim($("#HoldRate").val());
        //var IncreaseRate = $.trim($("#IncreaseRate").val());
        //var SpeculationRate = $.trim($("#SpeculationRate").val());
        //ShowPieInfo(HoldRate * 1, IncreaseRate * 1, SpeculationRate * 1);
        productIncomeRateVal();

    });

    //绑定下拉规划收益值
    $("select[eacflag='PlanId']").unbind("change").change(function () {
        var InsetmentType = $(this).val();//获得值
        var obj = new Object(this);
        SetPercentVal.setInsetmentVal(obj, InsetmentType, ProposalId);
    });



    //银行储蓄选择-------活期
    $("select[eacflag='DemandDepositsBank']").unbind("change").change(function () {
        var DemandDepositsBankType = $(this).val();//获得值
        var obj = new Object(this);
        if (DemandDepositsBankType != 0) {
            SetPercentVal.GetBankDeposits(obj, DemandDepositsBankType, false);
        } else {
            $(this).next().val(0);
        }
        //选择了就要去计算
        productIncomeRateVal();
    });
    //银行储蓄选择-------定期
    $("select[eacflag='TimeDepositBank']").unbind("change").change(function () {
        //只要上级改变下面的就清0
        $(this).next().val(0);
        $(this).next().next().val(0);
        var DemandDepositsBankType = $(this).val();//获得值
        var timeType = $(this).next().val();
        var obj = new Object(this);
        if (DemandDepositsBankType != 0) {
            if (timeType == 0) {
                SetPercentVal.GetBankDeposits(obj, DemandDepositsBankType, true);
            } else {
                SetPercentVal.GetBankDeposits(obj, DemandDepositsBankType, true, timeType);
            }
        } else {
            $(this).next().next().val(0);
        };
      

        //选择了就要去计算
        productIncomeRateVal();
    });
    //银行储蓄选择-------时间
    $("select[eacflag='TimeDepositBankTime']").unbind("change").change(function () {
        var DepositTime = $(this).val();//获得值
        var BankType = $(this).prev().val();
        var obj = new Object(this);
        if (BankType != 0) {//如果银行都没选直接不用查
            if (DepositTime != 0) {
                SetPercentVal.GetBankDepositsTime(obj, BankType, DepositTime);
            } else {
                $(this).next().val("0");//直接赋值
            };
        };
        //选择了就要去计算
        productIncomeRateVal();
    });

    var checkRadio = false
    //------绑定弹出层OK事件
    $("#popFundType .btn-ok").unbind("click").bind("click", function () {
        $("#popFundType input[type='radio']").each(function () {
            var ck = $(this).attr("checked");
            if (ck == "checked") {
                checkRadio = true;
            }
        });
        if (checkRadio) {
            var type = $("#hidType").val();
            if (type == "CashFund") {
                OkClick("CashFund");
            } else if (type == "BondFund") {
                OkClick("BondFund");
            } else if (type == "StockFund") {
                OkClick("StockFund");
            }
        } else {
            dialogHelper.Error({ content: "至少选择一样产品！" })
        }
    });
    var ptpRadio = false;
    //绑定P2p弹出层的OK事件
    $("#popPTPType .btn-ok").unbind("click").bind("click", function () {
        $("#popPTPType input[type='radio']").each(function () {
            var ck = $(this).attr("checked");
            if (ck == "checked") {
                ptpRadio = true;
            }
        });
        if (ptpRadio) {
            PTPOnclick()
        } else {
            dialogHelper.Error({ content: "至少选择一样产品！" })
        }
    })

    //-------绑定弹出层取消事件。
    $("#popFundType .btn-cancel").unbind("click").bind("click", function () {
        $("#popFundType input[type='radio']").each(function () {
            $(this).attr("checked", false);
        });
        //先清空收索框
      //  $("#popFundType #FundSearch").val("");
        dialogHelper.Close("popFundType");
    });

    //-------绑定P2P弹出层取消事件。
    $("#popPTPType .btn-cancel").unbind("click").bind("click", function () {
        $("#popPTPType input[type='radio']").each(function () {
            $(this).attr("checked", false);
        });
        //先清空
      //  $("#popPTPType #PTPSearch").val("");
        dialogHelper.Close("popPTPType");
    });
    //基金的搜索按钮
    $("#popFundType .btnSearch").bind("click", function () {
        var keywords = $.trim($("#FundSearch").val());
        if (keywords == "代码/产品") { keywords = ""; }
        var type = $.trim($("#hidType").val());
        var typeId = 1;
        if (type == "CashFund") {
            typeId = EnumList.FundProductType.Currency;
        } else if (type == "BondFund") {
            typeId = EnumList.FundProductType.Bond;
        } else if (type == "StockFund") {
            typeId = EnumList.FundProductType.Stock;
        }
        GetList(typeId, "", keywords, selecVal);

    });
    //p2p的搜索按钮
    $("#popPTPType .btnSearch").bind("click", function () {
        var keywords = $.trim($("#PTPSearch").val());
        if (keywords == "产品名称/投资领域") { keywords = ""; }
        pTwoProduct("", keywords, selecVal);

    });

    //移出事件
    $("#HoldRate,#IncreaseRate,#SpeculationRate").unbind("blur").blur(function () {
        var HoldRate = $.trim($("#HoldRate").val());
        var IncreaseRate = $.trim($("#IncreaseRate").val());
        var SpeculationRate = $.trim($("#SpeculationRate").val());
        var ckinputId = $(this).attr("id");
        if (HoldRate != "" && IncreaseRate != "" && SpeculationRate != "") {
            //页面字段检测
            if (!VerificationHelper.checkFrom("headInvestment",
                function () {
                sum = CalcPerCent(HoldRate, IncreaseRate, SpeculationRate);
                 if (sum * 1 != 100) {
                 showValidateMsg(ckinputId, "保值层,增值层,投机层之和必须为百分之百");
            };
            })) {
                return;
            }
            //同时还要加个*号  ////HedgeDiv 保值层 AddStar 增值层 SpeculateDiv 投机层
            if (HoldRate > 0) {
                AddStarSpan("HedgeDiv", "保值层");
            } else {
                //去除星号
                ClearStarSpan("HedgeDiv", "保值层");
            }
            if (IncreaseRate > 0) {
                AddStarSpan("AddStar", "增值层");
            } else {
                //去除星号
                ClearStarSpan("AddStar", "增值层");
            }
            if (SpeculationRate > 0) {
                AddStarSpan("SpeculateDiv", "投机层");
            } else {
                //去除星号
                ClearStarSpan("SpeculateDiv", "投机层");
            }



            //运算结果正常的话就执行
            ShowPieInfo(HoldRate * 1, IncreaseRate * 1, SpeculationRate * 1);
            //然后开始计算
            productIncomeRateVal();
        }
    });

    //绑定下一页事件
    $("#btnNext").unbind("click").bind("click", function () {
        SaveProductPlan(0);
        if (TagNavi) {
            window.location.href = "/CompetitionUser/TaxPlan/Index" + param;
        }
    });

    //$("#FundSearch,#PTPSearch").unbind("focus").focus(function () {
    //    $(this).val("").css("color", "black");
    //});

    //进行计算
    productIncomeRateVal();

    //保存原值
    SaveDefaultValueCommon("FinanceInvestmentPlanDiv");

});

//弹出货币基金列表
function PupCashFund(obj) {
    $("#FundSearch").val("代码/产品").css("color", "#aea8a8");
    //先可以获得对对象的值
    selecVal = $(obj).next().next().val();
    GetList(EnumList.FundProductType.Currency, "", "", selecVal);
    dialogHelper.Show("popFundType", 800);

    $("#popFundType #hidType").val("CashFund");
    var id = $(obj).parent().parent().parent().parent().attr("id");
    ShowPupTagId = id;
}

//弹出股票型基金列表
function PupBondFund(obj) {
    $("#FundSearch").val("代码/产品").css("color", "#aea8a8");
    //先可以获得对对象的值
    selecVal = $(obj).next().next().val();
    GetList(EnumList.FundProductType.Bond, "", "", selecVal);
    dialogHelper.Show("popFundType", 800);

    $("#popFundType #hidType").val("BondFund");
    var id = $(obj).parent().parent().parent().parent().attr("id");
    ShowPupTagId = id;

}

//弹出债券型基金列表
function PupStockFund(obj) {
    $("#FundSearch").val("代码/产品").css("color", "#aea8a8");
    //先可以获得对对象的值
    selecVal = $(obj).next().next().val();
    GetList(EnumList.FundProductType.Stock, "", "", selecVal);
    dialogHelper.Show("popFundType", 800);

    $("#popFundType #hidType").val("StockFund");
    var id = $(obj).parent().parent().parent().parent().attr("id");
    ShowPupTagId = id;
}


//p2p产品弹出层
function PupPTwoProduct(obj) {
    $("#PTPSearch").val("产品名称/投资领域").css("color", "#aea8a8");
    selecVal = $(obj).next().next().val();
    pTwoProduct("", "", selecVal);
    dialogHelper.Show("popPTPType", 800);
    var id = $(obj).parent().parent().parent().parent().attr("id");

    ShowPupTagId = id;

}

//删除对应选择的基金
function delSelectedFund(obj) {
    //先要清空标记   //同时要把原值清空
    $(obj).parent().parent().next().val(0);

    $(obj).parent().parent().next().next().val("");
    $(obj).parent().children().remove();
    //同时要计算
    productIncomeRateVal();
}

//p2p产品点击OK时
function PTPOnclick() {
    var trId = $("#popPTPType :radio:checked").parent().parent().attr("value");
    var P2PName = $("#popPTPType tr" + "[value='" + trId + "']").find("td:eq(1)").text();        //产品名称
    var InvestmentField = $("#popPTPType tr" + "[value='" + trId + "']").find("td:eq(2)").text();//投资领域
    var InvestmentCycle = $("#popPTPType tr" + "[value='" + trId + "']").find("td:eq(3)").text();//投资周期
    var StartAmount = $("#popPTPType tr" + "[value='" + trId + "']").find("td:eq(4)").text();    //起投金额
    var EarningsRate = $("#popPTPType tr" + "[value='" + trId + "']").find("td:eq(5)").text();   //预期收益率
    $("#" + ShowPupTagId).find("input[eacflag='P2PProduct']").val(trId); //p2p产品赋值
    var P2PProductRate = EarningsRate.replace(/\%/g, "")
    $("#" + ShowPupTagId).find("input[eacflag='P2PProductRate']").val(P2PProductRate * 1); //p2p产品赋值
    var trHtml = "";
    trHtml += "<span class=\"close\" onclick=\"delSelectedFund(this)\" title=\"" + trId + "\"></span> <div class=\"fif-form b-grayish\"> "
    trHtml += "<span class=\"\" eacflag=\"P2PName\" title=\"" + P2PName + "\">" + P2PName + "</span> "
    trHtml += "<span class=\"grid-2\" eacflag=\"InvestmentField\" title=\"" + InvestmentField + "\">" + InvestmentField + "</span> "
    trHtml += "<span class=\"grid-2\" eacflag=\"InvestmentCycle\" title=\"" + InvestmentCycle + "\">" + InvestmentCycle + "</span> "
    trHtml += "<span class=\"grid-2\" eacflag=\"StartAmount\"  title=\"" + StartAmount + "\">" + StartAmount + "</span> "
    trHtml += " <span class=\"grid-2\"  eacflag=\"EarningsRate\"  title=\"" + EarningsRate + "\">" + EarningsRate + "</span>  "
    trHtml += "</div> "

    $("#" + ShowPupTagId).find("tbody tr td div[field='P2PProduct']").html("");
    $("#" + ShowPupTagId).find("tbody tr td div[field='P2PProduct']").append(trHtml);
    //先清空收索框
    $("#popPTPType #PTPSearch").val("");
    //后关闭
    //关闭层还要计算
    dialogHelper.Close("popPTPType");

    //然后开始计算
    productIncomeRateVal();
}

//基金产品选择点击OK时
function OkClick(type) {
    var trId = $("#popFundType :radio:checked").parent().parent().attr("value");
    var tdCode = $("#popFundType tr" + "[value='" + trId + "']").find("td:eq(1)").text();
    var tdFund = $("#popFundType tr" + "[value='" + trId + "']").find("td:eq(2)").text();
    var tdType = $("#popFundType tr" + "[value='" + trId + "']").find("td:eq(3)").text();
    var tdIncome = $("#popFundType tr" + "[value='" + trId + "']").find("td:eq(4)").text();
    //然后找到对应table下面的input
    if (type == "CashFund") {
        $("#" + ShowPupTagId).find("input[eacflag='Fund1']").val(trId);
        $("#" + ShowPupTagId).find("input[eacflag='CashCode']").val(tdCode);
        //$("#" + ShowPupTagId).find("input[eacflag='CashFund']").val(tdFund);
        //$("#" + ShowPupTagId).find("input[eacflag='CashMarket']").val(tdType);
        //$("#" + ShowPupTagId).find("input[eacflag='YearlyEarningsRate1']").val(tdIncome);
    } else if (type == "BondFund") {
        $("#" + ShowPupTagId).find("input[eacflag='Fund2']").val(trId);
        $("#" + ShowPupTagId).find("input[eacflag='BondCode']").val(tdCode);
        //$("#" + ShowPupTagId).find("input[eacflag='BondFund']").val(tdFund);
        //$("#" + ShowPupTagId).find("input[eacflag='BondMarket']").val(tdType);
        //$("#" + ShowPupTagId).find("input[eacflag='YearlyEarningsRate2']").val(tdIncome);
    } else if (type == "StockFund") {
        $("#" + ShowPupTagId).find("input[eacflag='Fund3']").val(trId);
        $("#" + ShowPupTagId).find("input[eacflag='StockCode']").val(tdCode);
        //$("#" + ShowPupTagId).find("input[eacflag='StockFund']").val(tdFund);
        //$("#" + ShowPupTagId).find("input[eacflag='StockMarket']").val(tdType);
        //$("#" + ShowPupTagId).find("input[eacflag='YearlyEarningsRate3']").val(tdIncome);
    }

    //    var trHtml = $(":radio:checked").parent().parent().html();
    //var tabHtml = "<table id=\"" + ShowPupTagId + "\"> <tbody><tr>" + trHtml + "<td><a class=\"spr spr-del js_remove fr\" onclick=\"javascript: DelProduct(this)\" href=\"\"></a></td></tr></tbody></table>";
    //然后找到对应table下面的input
    var trHtml = "";
    if (type == "CashFund") {
        trHtml += "<span class=\"close\" onclick=\"delSelectedFund(this)\" title=\"" + trId + "\"></span> <div class=\"fif-form b-grayish\"> "
        trHtml += "<span class=\"grid-3\" eacflag=\"CashCode\"  title=\"" + tdCode + "\">" + tdCode + "</span> "
        trHtml += "<span class=\"grid-4\" eacflag=\"CashFund\"  title=\"" + tdFund + "\">" + tdFund + "</span> "
        trHtml += "<span class=\"grid-2\" eacflag=\"CashMarket\"  title=\"" + tdType + "\">" + tdType + "</span> "
        trHtml += " <span class=\"grid-3\"  eacflag=\"YearlyEarningsRate1\"  title=\"" + tdIncome + "\">" + tdIncome + "</span>  "
        trHtml += "</div> "
        //先清空后添加
        $("#" + ShowPupTagId).find("tbody tr td div[field='Fund1']").html("");
        $("#" + ShowPupTagId).find("tbody tr td div[field='Fund1']").append(trHtml);

    } else if (type == "BondFund") {
        trHtml += "<span class=\"close\" onclick=\"delSelectedFund(this)\" title=\"" + trId + "\"></span> <div class=\"fif-form b-grayish\"> "
        trHtml += "<span class=\"grid-3\" eacflag=\"BondCode\"  title=\"" + tdCode + "\">" + tdCode + "</span> "
        trHtml += "<span class=\"grid-4\" eacflag=\"BondFund\"  title=\"" + tdFund + "\">" + tdFund + "</span> "
        trHtml += "<span class=\"grid-2\" eacflag=\"BondMarket\"  title=\"" + tdType + "\">" + tdType + "</span> "
        trHtml += " <span class=\"grid-3\"  eacflag=\"YearlyEarningsRate2\"  title=\"" + tdIncome + "\">" + tdIncome + "</span>  "
        trHtml += "</div> "
        //先清空后添加
        $("#" + ShowPupTagId).find("tbody tr td div[field='Fund2']").html("");
        $("#" + ShowPupTagId).find("tbody tr td div[field='Fund2']").append(trHtml);

    } else if (type == "StockFund") {
        trHtml += "<span class=\"close\" onclick=\"delSelectedFund(this)\" title=\"" + trId + "\"></span> <div class=\"fif-form b-grayish\"> "
        trHtml += "<span class=\"grid-3\" eacflag=\"StockCode\" title=\"" + tdCode + "\">" + tdCode + "</span> "
        trHtml += "<span class=\"grid-4\" eacflag=\"StockFund\" title=\"" + tdFund + "\">" + tdFund + "</span> "
        trHtml += "<span class=\"grid-2\" eacflag=\"StockMarket\"  title=\"" + tdType + "\">" + tdType + "</span> "
        trHtml += " <span class=\"grid-3\"  eacflag=\"YearlyEarningsRate3\"  title=\"" + tdIncome + "\">" + tdIncome + "</span>  "
        trHtml += "</div> "
        //先清空后添加
        $("#" + ShowPupTagId).find("tbody tr td div[field='Fund3']").html("");
        $("#" + ShowPupTagId).find("tbody tr td div[field='Fund3']").append(trHtml);
    }
    //先清空后添加
    //$("#" + ShowPupTagId).find("tbody tr td div[field='Fund1']").html("");
    //$("#" + ShowPupTagId).find("tbody tr td div[field='Fund1']").append(trHtml);
    // ShowRadioTag = trHtml;
    //先清空收索框
    $("#popFundType #FundSearch").val("");
    //后关闭
    dialogHelper.Close("popFundType");
    //然后开始计算
    productIncomeRateVal();
}


function LoadInvestmentPlan(ProposalId) {

    _ajaxhepler({
        url: "/CompetitionUser/InvestmentPlan/LoadInvestmentPlan",
        type: "POST",
        async: false,
        dataType: "json",
        data: {
            ProposalId: ProposalId
        },
        success: function (data) {
            if (data != null) {
                var obj = new Object(data);
                analysisData(obj);
            }
        }

    });
};
//分解data数据
function analysisData(data) {
    var InvestmentPlanList = data.InvestmentPlanProductList;
    var LifeCycleId = data.LifeCycleId;
    var HoldRate = data.HoldRate * 1;
    var IncreaseRate = data.IncreaseRate * 1;
    var SpeculationRate = data.SpeculationRate * 1;
    var Analysis = data.Analysis;
    var Id = data.Id;
    $("#InvestmentPlanId").val(Id);
    $("#LifeCycleId").val(LifeCycleId);
    $("#HoldRate").val(HoldRate);
    $("#IncreaseRate").val(IncreaseRate);
    $("#SpeculationRate").val(SpeculationRate);
    $("#Analysis").val(Analysis);
    //同时还要给隔壁的对象赋值
    SetPercentVal.setPercentValueTwo(LifeCycleId);
    //赋值饼状图
    ShowPieInfo(HoldRate, IncreaseRate, SpeculationRate)

    $("#ProductSelect").remove();// 这样也可以
    // $("#Add").prev().remove();
    if (InvestmentPlanList != null && InvestmentPlanList.length > 0) {

        $(InvestmentPlanList).each(function (index, dom) {
            var fundobj = new Object(dom);
            index = index + 1;
            //应该先删除第一行然后添加
            //添加
            AddList(index, fundobj);
            //f赋值
            eacTransVal(index, fundobj);
            //给常量赋值
            IndexFlag = index;
        });

        //还要给增值层加个星号  //HedgeDiv 保值层 AddStar 增值层 SpeculateDiv 投机层
        if (HoldRate > 0) {
            AddStarSpan("HedgeDiv", "保值层");
        }
        if (IncreaseRate > 0) {
            AddStarSpan("AddStar", "增值层");
        }
        if (SpeculationRate > 0) {
            AddStarSpan("SpeculateDiv", "投机层");
        }
        // else {
        //    ClearStarSpan();
        //}
    }
}


//添加产品选择
function AddList(index, dom) {
    var trHtml = "";
    if (typeof index == "undefined") {
        IndexFlag = IndexFlag + 1;
        index = IndexFlag;
    }
    if (typeof dom == "undefined") {
        trHtml += "<div class=\"item b-grayish Tageach\"  id=\"ProductSelect" + index + "\"  eacflag=\"ProductSelect\"><div class=\"fif-con  mb10\">";
        trHtml += "<div class=\"fif-box grid-4\"><label class=\"fif-text\">已完成规划：</label> <div class=\"input\">";
        trHtml += "<select id=\"PlanId" + index + "\" name=\"PlanId\" class=\"eac IsRequired\" msgname=\"已完成规划\" eacflag=\"PlanId\"> <option value=\"0\">请选择</option> <option  value=\"1\">教育规划</option>  <option  value=\"2\">消费规划</option>  <option value=\"3\">创业规划</option> <option value=\"4\">退休规划</option></select> </div></div>";
        trHtml += "<div class=\"fif-box grid-6\"><label class=\"fif-text\"><i class=\"c-red\">*</i>方案所需投资收益率：</label>";
        trHtml += " <div class=\"input\"><input type=\"text\" class=\"ipt-text grid-12 disabled eac\" value=\"\" id=\"PlanRate" + index + "\" eacflag=\"PlanRate\" readonly=\"readonly\" value='0'><span class=\"ml10\">%</span></div> </div>";
        trHtml += "<a class=\"spr spr-del js_remove fr\" onclick=\"javascript: DelcloseProduct(this)\" href=\"javascript:void(0)\"></a></div>";
        trHtml += "<div class=\"table\"> <table class=\"mb10\" id=\"ProductSelect" + index + "TabCashFund\"  eacflag=\"ProductSelectTabCashFund\" width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
        trHtml += " <tbody> <tr> <td width=\"83\" rowspan=\"2\" align=\"center\" class=\"c-white\"  eacflag=\"HedgeDiv\" style=\"background-color: #63b2f4\">保值层</td>";
        trHtml += "<td><span class=\"fl\">银行储蓄选择</span><div class=\"fif-con fr grid-10\"> <div class=\"fif-box grid-12\" style=\"margin-bottom:10px;\">";
        trHtml += " <label class=\"fif-text\">活期：</label>   <div class=\"input grid-11\">";
        //银行选择活期
        trHtml += "<select style=\"width:35%;\" id=\"DemandDepositsBank" + index + "\" eacflag=\"DemandDepositsBank\" class=\"eac\"><option  value=\"0\">请选择</option>  <option  value=\"1\">工商银行</option> <option  value=\"2\">农业银行</option> <option  value=\"3\">中国银行</option> <option  value=\"4\">建设银行</option> <option  value=\"5\">交通银行</option> <option  value=\"6\">招商银行</option> <option  value=\"7\">浦发银行</option> <option  value=\"8\">上海银行</option> <option  value=\"9\">上海农商银行</option> <option  value=\"10\">邮政银行</option> <option  value=\"11\">兴业银行</option> <option  value=\"12\">中信银行</option> <option  value=\"13\">平安银行</option> <option  value=\"14\">广发银行</option> <option  value=\"15\">民生银行</option> <option  value=\"16\">光大银行</option> <option  value=\"17\">华夏银行</option> <option  value=\"18\">渤海银行</option> <option  value=\"19\">南京银行</option></select>"

        trHtml += "<input style=\"width:25%;\" type=\"text\" class=\"ipt-text disabled ml10\" value=\"0\" id=\"DemandDepositsBankRate" + index + "\" eacflag=\"DemandDepositsBankRate\" readonly=\"readonly\"> %  </div></div>";
        trHtml += "<div class=\"fif-box grid-12\"> <label class=\"fif-text\">定期：</label><div class=\"input grid-11\">";
        //银行选择定期
        trHtml += "<select style=\"width:35%;\" id=\"TimeDepositBank" + index + "\" eacflag=\"TimeDepositBank\" class=\"eac\"> <option  value=\"\">请选择</option> <option  value=\"1\">工商银行</option> <option  value=\"2\">农业银行</option> <option  value=\"3\">中国银行</option> <option  value=\"4\">建设银行</option> <option  value=\"5\">交通银行</option> <option  value=\"6\">招商银行</option> <option  value=\"7\">浦发银行</option> <option  value=\"8\">上海银行</option> <option  value=\"9\">上海农商银行</option> <option  value=\"10\">邮政银行</option> <option  value=\"11\">兴业银行</option> <option  value=\"12\">中信银行</option> <option  value=\"13\">平安银行</option> <option  value=\"14\">广发银行</option> <option  value=\"15\">民生银行</option> <option  value=\"16\">光大银行</option> <option  value=\"17\">华夏银行</option> <option  value=\"18\">渤海银行</option> <option  value=\"19\">南京银行</option> </select>";
        //银行期限
        trHtml += "<select class=\"ml10 eac\" style=\"width:35%;\" id=\"TimeDepositBankTime" + index + "\" eacflag=\"TimeDepositBankTime\"> <option value=\"0\">请选择期限</option> <option value=\"1\">三个月</option><option value=\"2\">半年</option><option value=\"3\">一年</option><option value=\"4\">二年</option><option value=\"5\">三年</option><option value=\"6\">五年</option> </select>";


        trHtml += "<input style=\"width:20%;\" type=\"text\"  class=\"ipt-text disabled ml10 eac\" value=\"0\" id=\"TimeDepositBankRate" + index + "\" eacflag=\"TimeDepositBankRate\"  readonly=\"readonly\"> % </div> </div></div></td> </tr>";
        trHtml += "<tr> <td> <span>基金产品选择</span>   <input class=\"btn btn-blue btn-big ml10 CashFund\" type=\"button\" value=\"货币市场基金\" onclick=\"javascript:PupCashFund(this)\">";
        //货币基金
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund1\"></div></div>"

        trHtml += "<input type=\"hidden\" id=\"Fund1" + index + "\" name=\"Fund1" + index + "\" value=\"\" eacflag=\"Fund1\" />  <input type=\"hidden\" eacflag=\"CashCode\" value=\"\" /><input type=\"hidden\" eacflag=\"CashFund\" value=\"\" /> <input type=\"hidden\" eacflag=\"CashMarket\" value=\"\" /><input type=\"hidden\" eacflag=\"YearlyEarningsRate1\" value=\"\" /> </td> </tr> <tr class=\"end\"></tr> </tbody> </table>";
        trHtml += "  <table class=\"mb10\" id=\"ProductSelect" + index + "TabBondFund\" width=\"100%\" border=\"0\" eacflag=\"ProductSelectTabBondFund\" cellspacing=\"0\" cellpadding=\"0\">  <tbody> <tr> <td width=\"83\" align=\"center\" class=\"c-white\" eacflag=\"AddStar\"  style=\"background-color: #2a91e6\">增值层</td>";
        trHtml += "<td>  <span>基金产品选择</span>  <input class=\"btn btn-blue btn-big ml10 BondFund\"  type=\"button\" value=\"债券型基金、混合型基金\" onclick=\"javascript:PupBondFund(this)\">";
        //债券基金
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund2\"></div></div>"

        trHtml += "  <input type=\"hidden\" name=\"Fund2\" id=\"Fund2" + index + "\" value=\"\" eacflag=\"Fund2\" class=\"eac\" />  <input type=\"hidden\" eacflag=\"BondCode\" value=\"\" /><input type=\"hidden\" eacflag=\"BondFund\" value=\"\" /> <input type=\"hidden\" eacflag=\"BondMarket\" value=\"\" /><input type=\"hidden\" eacflag=\"YearlyEarningsRate2\" value=\"\" /> </td> </tr> <tr class=\"end\"></tr>  </tbody>  </table>";
        trHtml += "<table class=\"mb10\" id=\"ProductSelect" + index + "TabStockFund\" width=\"100%\" border=\"0\" eacflag=\"ProductSelectTabStockFund\" cellspacing=\"0\" cellpadding=\"0\">  <tbody> <tr> <td width=\"83\" rowspan=\"2\" align=\"center\" class=\"c-white\" eacflag=\"SpeculateDiv\"  style=\"background-color: #086cc1\">投机层</td>";
        //p2p新加
        trHtml += " <td> <span>P2P产品选择</span> <input class=\"btn btn-blue btn-big ml10 StockFund\" type=\"button\" value=\"P2P产品\" onclick=\"javascript:PupPTwoProduct(this)\"><div class=\"selected-list sib-list mt10\"><div class=\"sib-item\" field=\"P2PProduct\"> </div></div>";
        trHtml += "<input type=\"hidden\" class=\"eac\" id=\"P2PProduct" + index + "\" value=\"0\" eacflag=\"P2PProduct\"><input type=\"hidden\" class=\"eac\" id=\"P2PProductRate" + index + "\" value=\"0\" eacflag=\"P2PProductRate\"></td></tr>"


        //trHtml += "<td><div class=\"fif-con \"> <div class=\"fif-box grid-4\"><label class=\"fif-text\">P2P产品选择：</label>";
        //trHtml += "<div class=\"input \"> <select class=\"eac\" id=\"P2PProduct" + index + "\" eacflag=\"P2PProduct\"> <option  value=\"0\">请选择</option><option value=\"1\">有利网</option></select> </div> </div>"
        //trHtml += "<div class=\"fif-box grid-4\"><label class=\"fif-text\">P2P产品选择：</label>  <div class=\"input\">  <input type=\"text\" class=\"ipt-text grid-12 disabled eac\" id=\"P2PProductRate" + index + "\" value=\"0\" eacflag=\"P2PProductRate\" readonly=\"readonly\"> </div> </div> </div></td> ";
        trHtml += "<tr> <td>  <span>基金产品选择</span> <input class=\"btn btn-blue btn-big ml10 StockFund\"  type=\"button\" value=\"股票型基金\" onclick=\"javascript:PupStockFund(this)\">"
        //股票基金
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund3\"></div></div>"

        trHtml += " <input type=\"hidden\" id=\"Fund3" + index + "\" name=\"Fund3'\" value=\"\" eacflag=\"Fund3\" />  <input type=\"hidden\" eacflag=\"StockCode\" value=\"\" /><input type=\"hidden\" eacflag=\"StockFund\" value=\"\" /> <input type=\"hidden\" eacflag=\"StockMarket\" value=\"\" /><input type=\"hidden\" eacflag=\"YearlyEarningsRate3\" value=\"\" /> </td> </tr>    <tr class=\"end\"></tr> </tbody>  </table>   </div>";
        trHtml += "  <div class=\"fif-con \"> <div class=\"fif-box grid-5\"> <label class=\"fif-text\">产品组合预期收益率：</label>";
        trHtml += " <div class=\"input\"> <input type=\"text\" class=\"ipt-text grid-12 disabled eac\" value=\"\" id=\"TotalRate" + index + "\" eacflag=\"TotalRate\" readonly=\"readonly\"><span class=\"ml10\">%</span></div></div></div></div>"
        $("#Add").prev().after(trHtml);

        //绑定下拉选择事件
        $("#LifeCycleId").unbind("change").change(function () {
            var obj = new Object(this);
            SetPercentVal.setPercentValue(obj);
        });

        //绑定下拉规划收益值
        $("select[eacflag='PlanId']").unbind("change").change(function () {
            var InsetmentType = $(this).val();//获得值
            var obj = new Object(this);
            SetPercentVal.setInsetmentVal(obj, InsetmentType, ProposalId);
        });
        //银行储蓄选择-------活期
        $("select[eacflag='DemandDepositsBank']").unbind("change").change(function () {
            var DemandDepositsBankType = $(this).val();//获得值
            var obj = new Object(this);
            if (DemandDepositsBankType != 0) {
                SetPercentVal.GetBankDeposits(obj, DemandDepositsBankType, false);
            } else {
                $(this).next().val(0);
            }
            //选择了就要去计算
            productIncomeRateVal();
        });
        //银行储蓄选择-------定期
        $("select[eacflag='TimeDepositBank']").unbind("change").change(function () {
            $(this).next().val(0);
            $(this).next().next().val(0);
            var DemandDepositsBankType = $(this).val();//获得值
            var timeType = $(this).next().val();
            var obj = new Object(this);
            if (DemandDepositsBankType != 0) {
                if (timeType == 0) {
                    SetPercentVal.GetBankDeposits(obj, DemandDepositsBankType, true);
                } else {
                    SetPercentVal.GetBankDeposits(obj, DemandDepositsBankType, true, timeType);
                }
            } else {
                $(this).next().next().val(0);
            };
            //选择了就要去计算
            productIncomeRateVal();
        });
        //银行储蓄选择-------时间
        $("select[eacflag='TimeDepositBankTime']").unbind("change").change(function () {
            var DepositTime = $(this).val();//获得值
            var BankType = $(this).prev().val();
            var obj = new Object(this);
            if (BankType != 0) {//如果银行都没选直接不用查
                if (DepositTime != 0) {
                    SetPercentVal.GetBankDepositsTime(obj, BankType, DepositTime);
                } else {
                    $(this).next().val("0");//直接赋值
                };
            };
            //选择了就要去计算
            productIncomeRateVal();
        });



        //添加的时候判断一下上面的坑
        var HoldRate = $("#HoldRate").val() * 1;
        var IncreaseRate = $("#IncreaseRate").val() * 1;
        var SpeculationRate = $("#SpeculationRate").val() * 1;

        if (HoldRate > 0) {
            AddStarSpan("HedgeDiv", "保值层");
        }
        if (IncreaseRate > 0) {
            AddStarSpan("AddStar", "增值层");
        }
        if (SpeculationRate > 0) {
            AddStarSpan("SpeculateDiv", "投机层");
        }


    } else {

        trHtml += "<div class=\"item b-grayish Tageach\"  id=\"ProductSelect" + index + "\" eacflag=\"ProductSelect\"><div class=\"fif-con  mb10\">";
        trHtml += "<div class=\"fif-box grid-4\"><label class=\"fif-text\">已完成规划：</label> <div class=\"input\">";
        trHtml += "<select id=\"PlanId" + index + "\" name=\"PlanId\" class=\"eac IsRequired\" msgname=\"已完成规划\" eacflag=\"PlanId\"> <option value=\"0\">请选择</option> <option  value=\"1\">教育规划</option>  <option  value=\"2\">消费规划</option>  <option value=\"3\">创业规划</option> <option value=\"4\">退休规划</option></select> </div></div>";
        trHtml += "<div class=\"fif-box grid-6\"><label class=\"fif-text\"><i class=\"c-red\">*</i>方案所需投资收益率：</label>";
        trHtml += " <div class=\"input\"><input type=\"text\" class=\"ipt-text grid-12 disabled eac\" value=\"\" id=\"PlanRate" + index + "\" eacflag=\"PlanRate\" readonly=\"readonly\"><span class=\"ml10\">%</span></div> </div>";
        trHtml += "<a class=\"spr spr-del js_remove fr\" onclick=\"javascript: DelcloseProduct(this)\" href=\"javascript:void(0)\"></a></div>";
        trHtml += "<div class=\"table\"> <table class=\"mb10\" id=\"ProductSelect" + index + "TabCashFund\" eacflag=\"ProductSelectTabCashFund\" width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
        trHtml += " <tbody> <tr> <td width=\"83\" rowspan=\"2\" align=\"center\" class=\"c-white\" eacflag=\"HedgeDiv\" style=\"background-color: #63b2f4\">保值层</td>";
        trHtml += "<td><span class=\"fl\">银行储蓄选择</span><div class=\"fif-con  fr grid-10\"> <div class=\"fif-box grid-12\" style=\"margin-bottom:10px;\">";
        trHtml += " <label class=\"fif-text\">活期：</label>   <div class=\"input grid-11\">";

        //银行选择
        trHtml += "<select style=\"width:35%;\" id=\"DemandDepositsBank" + index + "\" eacflag=\"DemandDepositsBank\" class=\"eac\"><option  value=\"0\">请选择</option>  <option  value=\"1\">工商银行</option> <option  value=\"2\">农业银行</option> <option  value=\"3\">中国银行</option> <option  value=\"4\">建设银行</option> <option  value=\"5\">交通银行</option> <option  value=\"6\">招商银行</option> <option  value=\"7\">浦发银行</option> <option  value=\"8\">上海银行</option> <option  value=\"9\">上海农商银行</option> <option  value=\"10\">邮政银行</option> <option  value=\"11\">兴业银行</option> <option  value=\"12\">中信银行</option> <option  value=\"13\">平安银行</option> <option  value=\"14\">广发银行</option> <option  value=\"15\">民生银行</option> <option  value=\"16\">光大银行</option> <option  value=\"17\">华夏银行</option> <option  value=\"18\">渤海银行</option> <option  value=\"19\">南京银行</option></select>"

        trHtml += "<input style=\"width:25%;\" type=\"text\" class=\"ipt-text disabled ml10\" value=\"0\" id=\"DemandDepositsBankRate" + index + "\" eacflag=\"DemandDepositsBankRate\" readonly=\"readonly\"> %  </div></div>";
        trHtml += "<div class=\"fif-box grid-12\"> <label class=\"fif-text\">定期：</label><div class=\"input grid-11\">";
        //银行选择定期
        trHtml += "<select style=\"width:35%;\" id=\"TimeDepositBank" + index + "\" eacflag=\"TimeDepositBank\" class=\"eac\"> <option  value=\"\">请选择</option> <option  value=\"1\">工商银行</option> <option  value=\"2\">农业银行</option> <option  value=\"3\">中国银行</option> <option  value=\"4\">建设银行</option> <option  value=\"5\">交通银行</option> <option  value=\"6\">招商银行</option> <option  value=\"7\">浦发银行</option> <option  value=\"8\">上海银行</option> <option  value=\"9\">上海农商银行</option> <option  value=\"10\">邮政银行</option> <option  value=\"11\">兴业银行</option> <option  value=\"12\">中信银行</option> <option  value=\"13\">平安银行</option> <option  value=\"14\">广发银行</option> <option  value=\"15\">民生银行</option> <option  value=\"16\">光大银行</option> <option  value=\"17\">华夏银行</option> <option  value=\"18\">渤海银行</option> <option  value=\"19\">南京银行</option> </select>";
        //银行期限
        trHtml += "<select class=\"ml10 eac\" style=\"width:35%;\" id=\"TimeDepositBankTime" + index + "\" eacflag=\"TimeDepositBankTime\"> <option value=\"0\">请选择期限</option> <option value=\"1\">三个月</option><option value=\"2\">半年</option><option value=\"3\">一年</option><option value=\"4\">二年</option><option value=\"5\">三年</option><option value=\"6\">五年</option> </select>";

        trHtml += "<input style=\"width:20%;\" type=\"text\"  class=\"ipt-text disabled ml10 eac\" value=\"0\" id=\"TimeDepositBankRate" + index + "\" eacflag=\"TimeDepositBankRate\" readonly=\"readonly\"> % </div> </div></div></td> </tr>";
        trHtml += "<tr> <td> <span>基金产品选择</span>   <input class=\"btn btn-blue btn-big ml10 CashFund\" type=\"button\" value=\"货币市场基金\" onclick=\"javascript:PupCashFund(this)\">";
        //货币基金
        if (dom.Fund1 != 0) {
            trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund1\"> <span class=\"close\" onclick=\"delSelectedFund(this)\"></span> <div class=\"fif-form b-grayish\">  <span class=\"grid-3\" eacflag=\"CashCode\">小龙</span>     <span class=\"grid-4\" eacflag=\"CashFund\">汇添富基金-汇添富社会责任股票</span>   <span class=\"grid-2\" eacflag=\"CashMarket\">货币市场</span>   <span class=\"grid-3\"  eacflag=\"YearlyEarningsRate1\">151.15%</span>  </div>  </div>    </div>";
        } else {
            trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund1\"></div></div>"
        }
        trHtml += "<input type=\"hidden\" id=\"Fund1" + index + "\" name=\"Fund1" + index + "\" value=\"\" eacflag=\"Fund1\" />  <input type=\"hidden\" eacflag=\"CashCode\" value=\"\" /><input type=\"hidden\" eacflag=\"CashFund\" value=\"\" /> <input type=\"hidden\" eacflag=\"CashMarket\" value=\"\" /><input type=\"hidden\" eacflag=\"YearlyEarningsRate1\" value=\"\" /> </td> </tr> <tr class=\"end\"></tr> </tbody> </table>";
        trHtml += "  <table class=\"mb10\" id=\"ProductSelect" + index + "TabBondFund\" eacflag=\"ProductSelectTabBondFund\" width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">  <tbody> <tr> <td width=\"83\" align=\"center\" class=\"c-white\" eacflag=\"AddStar\" style=\"background-color: #2a91e6 \">增值层</td>";
        trHtml += "<td>  <span>基金产品选择</span>  <input class=\"btn btn-blue btn-big ml10 BondFund\"  type=\"button\" value=\"债券型基金、混合型基金\" onclick=\"javascript:PupBondFund(this)\">";
        //债券基金
        if (dom.Fund2 != 0) {
            trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund2\"> <span class=\"close\" onclick=\"delSelectedFund(this)\"></span> <div class=\"fif-form b-grayish\">  <span class=\"grid-3\" eacflag=\"BondCode\">小龙</span>     <span class=\"grid-4\" eacflag=\"BondFund\">汇添富基金-汇添富社会责任股票</span>   <span class=\"grid-2\" eacflag=\"BondMarket\">货币市场</span>   <span class=\"grid-3\"  eacflag=\"YearlyEarningsRate2\">151.15%</span>  </div>  </div>    </div>";
        } else {
            trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\"  field=\"Fund2\"></div></div>"
        }
        trHtml += "  <input type=\"hidden\" name=\"Fund2\" id=\"Fund2" + index + "\" value=\"\" eacflag=\"Fund2\" class=\"eac\" />  <input type=\"hidden\" eacflag=\"BondCode\" value=\"\" /><input type=\"hidden\" eacflag=\"BondFund\" value=\"\" /> <input type=\"hidden\" eacflag=\"BondMarket\" value=\"\" /><input type=\"hidden\" eacflag=\"YearlyEarningsRate2\" value=\"\" /> </td> </tr> <tr class=\"end\"></tr>  </tbody>  </table>";
        trHtml += "<table class=\"mb10\" id=\"ProductSelect" + index + "TabStockFund\" eacflag=\"ProductSelectTabStockFund\" width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">  <tbody> <tr> <td width=\"83\" rowspan=\"2\" align=\"center\" class=\"c-white\" eacflag=\"SpeculateDiv\" style=\"background-color: #086cc1\">投机层</td>";

        //p2p新加
        trHtml += "   <td> <span>P2P产品选择</span> <input class=\"btn btn-blue btn-big ml10 StockFund\" type=\"button\" value=\"P2P产品\" onclick=\"javascript:PupPTwoProduct(this)\"> ";
        if (dom.P2PProduct != 0) {
            trHtml += "<div class=\"selected-list sib-list mt10\"><div class=\"sib-item\" field=\"P2PProduct\"><span class=\"close\" onclick=\"delSelectedFund(this)\" title=\"\"></span> <div class=\"fif-form b-grayish\"> <span class=\"\" eacflag=\"P2PName\" title=></span> <span class=\"grid-2\" eacflag=\"InvestmentField\" title=></span> <span class=\"grid-2\" eacflag=\"InvestmentCycle\" title=\"\"></span> <span class=\"grid-2\" eacflag=\"StartAmount\" title=></span>  <span class=\"grid-2\" eacflag=\"EarningsRate\" title=\"\"></span>  </div> </div></div>"
        } else {
            trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\"  field=\"P2PProduct\"></div></div>"
        }
        trHtml += "<input type=\"hidden\" class=\"eac\" id=\"P2PProduct" + index + "\" value=\"0\" eacflag=\"P2PProduct\"><input type=\"hidden\" class=\"eac\" id=\"P2PProductRate" + index + "\" value=\"0\" eacflag=\"P2PProductRate\"></td></tr>";

        trHtml += "<tr> <td>  <span>基金产品选择</span> <input class=\"btn btn-blue btn-big ml10 StockFund\"  type=\"button\" value=\"股票型基金\" onclick=\"javascript:PupStockFund(this)\">"
        //股票基金
        if (dom.Fund3 != 0) {
            trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund3\"> <span class=\"close\" onclick=\"delSelectedFund(this)\"></span> <div class=\"fif-form b-grayish\">  <span class=\"grid-3\" eacflag=\"StockCode\">小龙</span>     <span class=\"grid-4\" eacflag=\"StockFund\">汇添富基金-汇添富社会责任股票</span>   <span class=\"grid-2\" eacflag=\"StockMarket\">货币市场</span>   <span class=\"grid-3\"  eacflag=\"YearlyEarningsRate3\">151.15%</span>  </div>  </div>    </div>";
        } else {
            trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund3\"></div></div>"
        }
        trHtml += " <input type=\"hidden\" id=\"Fund3" + index + "\" name=\"Fund3'\" value=\"\" eacflag=\"Fund3\" />  <input type=\"hidden\" eacflag=\"StockCode\" value=\"\" /><input type=\"hidden\" eacflag=\"StockFund\" value=\"\" /> <input type=\"hidden\" eacflag=\"StockMarket\" value=\"\" /><input type=\"hidden\" eacflag=\"YearlyEarningsRate3\" value=\"\" /> </td> </tr>    <tr class=\"end\"></tr> </tbody>  </table>   </div>";
        trHtml += "  <div class=\"fif-con \"> <div class=\"fif-box grid-5\"> <label class=\"fif-text\">产品组合预期收益率：</label>";
        trHtml += " <div class=\"input\"> <input type=\"text\" class=\"ipt-text grid-12 disabled eac\" value=\"\" id=\"TotalRate" + index + "\" eacflag=\"TotalRate\" readonly=\"readonly\"><span class=\"ml10\">%</span></div></div></div></div>"
        $("#Add").prev().after(trHtml);
    }
}

// var trHtml = "";
//trHtml += "<span class=\"close\" onclick=\"delSelectedFund(this)\"></span> <div class=\"fif-form b-grayish\"> "
//trHtml += "<span class=\"grid-3\" eacflag=\"CashCode\">{0}</span> "
//trHtml += "<span class=\"grid-4\" eacflag=\"CashFund\">{1}</span> "
//trHtml += "<span class=\"grid-2\" eacflag=\"CashMarket\">{2}</span> "
//trHtml += " <span class=\"grid-3\"  eacflag=\"YearlyEarningsRate1\">{3}</span>  "
//trHtml += "</div> "
// $("#popFundType #mianBody tbody tr td .sib-item").html(html);

//添加产品 -无法复用因为返回值不同
function GetList(TypeId, url, keyword, selVal) {
    var code = "";
    var pageIndex = 0;
    var pageSize = 10;

    if (typeof keyword == "undefined") {
        keyword = "";
    }
    if (typeof url == "undefined") {
        url = "/CompetitionUser/FundProduct/GetFundProductList";
    }
    url = "/CompetitionUser/FundProduct/GetFundProductList";
    keyword = keyword.replace(/[<>&"]/g, function (c) { return { '<': '&lt;', '>': '&gt;', '&': '&amp;', '"': '&quot;', '"': '&quot;', "'": '&prime;', "'": '&prime;' }[c]; }),//搜索关键字
       pageHelper.Init({
           url: url,
           type: "POST",
           pageDiv: "#pageDiv",
           data:
           {
               FundSearchType: TypeId,
               keywords: keyword,
               pageIndex: 1,
               pageSize: 10
           },
           bind: function (data) {
               var html = "";
               $(data.Data).each(function (index, dom) {
                   //每行html
                   var trHtml = "";
                   trHtml += "<tr value=\"{0}\">";
                   trHtml += "<td name=\"dataNo\"><input name=\"cz\" type=\"radio\" value=\"{0}\"></td>";
                   trHtml += "<td><span title=\"{1}\">{1}</span></td>";
                   trHtml += "<td><span title=\"{2}\">{2}</span></td>";
                   trHtml += "<td><span title=\"{3}\">{3}</span></td>";
                   trHtml += "<td><span title=\"{4}\">{4}</span></td>";
                   trHtml += "</tr>";

                   //拼接tbody
                   html += StringHelper.FormatStr(trHtml,
                       dom.FundId,                                              //0 基金id
                       dom.FundCode,                                            //1 代码编号
                       dom.FundName,                                            //2 基金名称
                       dom.FundType,                                            //3 基金类型
                       //4 年收益率   
                       dom.FundType.indexOf("货币") > -1 ? dom.YearlyEarningsRate.toMyFixed(2) + "%": (dom.YearlyEarningsRate * 100).toMyFixed(2) + "%"           
                       );
               });
               $("#popFundType #mianBody tbody").html(html);
               if (TypeId == EnumList.FundProductType.Currency) {
                   $("#FundTitle").text("货币市场基金");
               } else if (TypeId == EnumList.FundProductType.Bond) {
                   $("#FundTitle").text("债券、混合型基金");
               } else if (TypeId == EnumList.FundProductType.Stock) {
                   $("#FundTitle").text("股票型基金");
               }
               //让其被选中
               $("#popFundType #mianBody tbody tr").each(function () {
                   var val = $(this).attr("value");
                   if (val == selVal) {
                       $(this).find("td:eq(0) input[type='radio']").attr("checked", true);
                   }
               });

               dialogHelper.Reset("popFundType");
           }
       });
};

//获得P2P产品
function pTwoProduct(TypeId, keyword, selVal) {
    var code = "";
    var pageIndex = 0;
    var pageSize = 10;

    if (typeof keyword == "undefined") {
        keyword = "";
    }

    pageHelper.Init({
        url: "/CompetitionUser/P2PProducet/GetP2PProduceList",
        type: "POST",
        pageDiv: "#pageDivPTP",
        data:
        {
            FundType: TypeId,
            keywords: keyword,
            pageIndex: 1,
            pageSize: 10
        },
        bind: function (data) {
            var html = "";
            $(data.Data).each(function (index, dom) {
                //每行html
                var trHtml = "";
                trHtml += "<tr value=\"{0}\">";
                trHtml += "<td name=\"dataNo\"><input name=\"cz\" type=\"radio\" value=\"{0}\"></td>";
                trHtml += "<td><div class=\"ellipsis\" title=\"{1}\">{1}</div></td>";
                trHtml += "<td><div class=\"ellipsis\" title=\"{2}\">{2}</div></td>";
                trHtml += "<td><div class=\"ellipsis\" title=\"{3}\">{3}</div></td>";
                trHtml += "<td><div class=\"ellipsis\" title=\"{4}\">{4}</div></td>";
                trHtml += "<td><div class=\"ellipsis\" title=\"{5}\">{5}</div></td>";
                trHtml += "</tr>";

                //拼接tbody
                html += StringHelper.FormatStr(trHtml,
                   // ((data.PageIndex - 1) * data.PageSize + index + 1),   //0 序号
                    dom.Id,
                    dom.P2PName,                                    //产品名称
                    dom.InvestmentField,                           //投资领域
                    dom.InvestmentCycle,                           //投资周期
                    dom.StartAmount,                                //起投金额
                    dom.EarningsRate                                //预期收益率    
                    );
            });
            $("#popPTPType #mianBodyPTP tbody").html(html);
            //让其被选中
            $("#popPTPType #mianBodyPTP tbody tr").each(function () {
                var val = $(this).attr("value");
                if (val == selVal) {
                    $(this).find("td:eq(0) input[type='radio']").attr("checked", true);
                }
            });

            dialogHelper.Reset("popPTPType");
        }
    });
}

// "<tr><td><input name=\"cz\" type=\"radio\" value=\"" + item.Id + "\"></td><td>" + item.FundCode + "</td><td><div class=\"ellipsis\">" + item.FundName + "</div></td><td>" + item.FundType + "</td><td>" + item.YearlyEarningsRate + "</td></tr>";


//绑定页码
function RebindInfoPages() {
    $("#popFundType #pageDiv a").unbind("click").bind("click", function () {
        ProductList($(this).attr("tag"), $(this).parent().attr("tag"));
    });
}
//删除选择产品

function DelcloseProduct(obj) {
    // var num = $("div[eacflag='ProductSelect']").size();
    //if(num>1){
    $(obj).parent().parent().remove();
    //}

}
//删除所选产品
function DelProduct(obj) {
    $(obj).parent().parent().parent().remove();
};


//循环赋值
function eacTransVal(index, dom) {
    $("#ProductSelect" + index).find("select[eacflag='PlanId']").val(dom.PlanId);
    $("#ProductSelect" + index).find("input[eacflag='PlanRate']").val(dom.PlanRate);
    $("#ProductSelect" + index).find("select[eacflag='DemandDepositsBank']").val(dom.DemandDepositsBank);
    $("#ProductSelect" + index).find("input[eacflag='DemandDepositsBankRate']").val(dom.DemandDepositsBankRate);
    $("#ProductSelect" + index).find("select[eacflag='TimeDepositBank']").val(dom.TimeDepositBank);
    $("#ProductSelect" + index).find("select[eacflag='TimeDepositBankTime']").val(dom.TimeDepositBankTime);
    $("#ProductSelect" + index).find("input[eacflag='TimeDepositBankRate']").val(dom.TimeDepositBankRate);
    $("#ProductSelect" + index).find("input[eacflag='Fund1']").val(dom.Fund1);
    $("#ProductSelect" + index).find("input[eacflag='Fund2']").val(dom.Fund2);
    $("#ProductSelect" + index).find("input[eacflag='P2PProduct']").val(dom.P2PProduct);
    $("#ProductSelect" + index).find("input[eacflag='P2PProductRate']").val(dom.P2PProductRate);
    $("#ProductSelect" + index).find("input[eacflag='Fund3']").val(dom.Fund3);
    $("#ProductSelect" + index).find("input[eacflag='TotalRate']").val(dom.TotalRate);
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
    $("#ProductSelect" + index).find("span[eacflag='YearlyEarningsRate1']").attr("title", dom.YearlyEarningsRate1).text(dom.YearlyEarningsRate1 + "%");
    // 债券型基金
    $("#ProductSelect" + index).find("span[eacflag='BondCode']").attr("title", dom.BondCode).text(dom.BondCode);
    $("#ProductSelect" + index).find("span[eacflag='BondFund']").attr("title", dom.BondFund).text(dom.BondFund);
    $("#ProductSelect" + index).find("span[eacflag='BondMarket']").attr("title", dom.BondMarket).text(dom.BondMarket);
    $("#ProductSelect" + index).find("span[eacflag='YearlyEarningsRate2']").attr("title", dom.YearlyEarningsRate2).text(dom.YearlyEarningsRate2 + "%");
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


//新增和修改----------------------------------保存
function SaveProductPlan(saveFalg) {
    TagNavi = true;
    //页面字段检测
    if (!VerificationHelper.checkFrom("FinanceInvestmentPlanDiv")) {
        TagNavi = false;
        return;
    }


    //同时还要检验三个数加起来是否为百分之百
    var HoldRate = $.trim($("#FinanceInvestmentPlanDiv #HoldRate").val()) * 1;
    var IncreaseRate = $.trim($("#FinanceInvestmentPlanDiv #IncreaseRate").val()) * 1;
    var SpeculationRate = $.trim($("#FinanceInvestmentPlanDiv #SpeculationRate").val()) * 1;
    var RateSum = HoldRate + IncreaseRate + SpeculationRate;
    if (RateSum != 100) {
        dialogHelper.Error({
            content: "保值层、增值层、投机层的比率加起来不为100"
        });
        TagNavi = false;
        return false;
    }


    //if (divflag.length > 0) {
    //同时还要根据上面的数字验证 ---用这三货做一个验证。当他们谁为0的时候，下面可以不选
    //前提是必须选择一个
    var divflag = $("#FinanceInvestmentPlanDiv").find("div[eacflag='ProductSelect']");
    var result = CheckSelectVal();
    if (result != "true") {
        dialogHelper.Confirm({
            content: result,
            success: function () {
            }
        });
        TagNavi = false;
        return false;
    }
    //}


    //获取URL参数
    //此处参数必须跟VM一致
    var ProductPlanObj = new Object();

    //获取财产传承
    if (ProposalId != null && ProposalId != "" && ProposalId != undefined) {
        ProductPlanObj["ProposalId"] = ProposalId;
    } else {
        ProposalId = $("#FinanceInvestmentPlanDiv #ProposalId").val();
        ProductPlanObj["ProposalId"] = ProposalId
    }

    var InvestmentPlanId = $("#FinanceInvestmentPlanDiv #InvestmentPlanId").val();
    var LifeCycleId = $("#FinanceInvestmentPlanDiv #LifeCycleId").val()



    ProductPlanObj["Id"] = InvestmentPlanId;
    ProductPlanObj["LifeCycleId"] = LifeCycleId;
    ProductPlanObj["HoldRate"] = HoldRate;
    ProductPlanObj["IncreaseRate"] = IncreaseRate;
    ProductPlanObj["SpeculationRate"] = SpeculationRate;

    //获得所有的产品列表
    var objList = new Array();
    $.each($("#FinanceInvestmentPlanDiv .Tageach"), function (i, ob) {
        var InvestmentPlanProductObj = new Object();
        InvestmentPlanProductObj["ProposalId"] = ProposalId;
        InvestmentPlanProductObj["PlanId"] = $(ob).find("select[eacflag='PlanId']").val();
        InvestmentPlanProductObj["PlanRate"] = $(ob).find("input[eacflag='PlanRate']").val();
        InvestmentPlanProductObj["DemandDepositsBank"] = $(ob).find("select[eacflag='DemandDepositsBank']").val();
        InvestmentPlanProductObj["DemandDepositsBankRate"] = $(ob).find("input[eacflag='DemandDepositsBankRate']").val();
        InvestmentPlanProductObj["TimeDepositBank"] = $(ob).find("select[eacflag='TimeDepositBank']").val();
        InvestmentPlanProductObj["TimeDepositBankTime"] = $(ob).find("select[eacflag='TimeDepositBankTime']").val();
        InvestmentPlanProductObj["TimeDepositBankRate"] = $(ob).find("input[eacflag='TimeDepositBankRate']").val();
        InvestmentPlanProductObj["Fund1"] = $(ob).find("input[eacflag='Fund1']").val();
        InvestmentPlanProductObj["Fund2"] = $(ob).find("input[eacflag='Fund2']").val();
        InvestmentPlanProductObj["P2PProduct"] = $(ob).find("input[eacflag='P2PProduct']").val();
        InvestmentPlanProductObj["P2PProductRate"] = $(ob).find("input[eacflag='P2PProductRate']").val();
        InvestmentPlanProductObj["Fund3"] = $(ob).find("input[eacflag='Fund3']").val();
        InvestmentPlanProductObj["TotalRate"] = $(ob).find("input[eacflag='TotalRate']").val();
        ////货币基金
        //InvestmentPlanProductObj["CashCode"] = $(ob).find("input[eacflag='CashCode']").val();
        //InvestmentPlanProductObj["CashFund"] = $(ob).find("input[eacflag='CashFund']").val();
        //InvestmentPlanProductObj["CashMarket"] = $(ob).find("input[eacflag='CashMarket']").val();
        //InvestmentPlanProductObj["YearlyEarningsRate1"] = $(ob).find("input[eacflag='YearlyEarningsRate1']").val();
        ////债劵基金
        //InvestmentPlanProductObj["BondCode"] = $(ob).find("input[eacflag='BondCode']").val();
        //InvestmentPlanProductObj["BondFund"] = $(ob).find("input[eacflag='BondFund']").val();
        //InvestmentPlanProductObj["BondMarket"] = $(ob).find("input[eacflag='BondMarket']").val();
        //InvestmentPlanProductObj["YearlyEarningsRate2"] = $(ob).find("input[eacflag='YearlyEarningsRate2']").val();
        ////股票基金
        //InvestmentPlanProductObj["StockCode"] = $(ob).find("input[eacflag='StockCode']").val();
        //InvestmentPlanProductObj["StockFund"] = $(ob).find("input[eacflag='StockFund']").val();
        //InvestmentPlanProductObj["StockMarket"] = $(ob).find("input[eacflag='StockMarket']").val();
        //InvestmentPlanProductObj["YearlyEarningsRate3"] = $(ob).find("input[eacflag='YearlyEarningsRate3']").val();
        var YearlyEarningsRate1, YearlyEarningsRate2, YearlyEarningsRate3 = 0;
        //货币基金
        InvestmentPlanProductObj["CashCode"] = $(ob).find("span[eacflag='CashCode']").attr("title");
        InvestmentPlanProductObj["CashFund"] = $(ob).find("span[eacflag='CashFund']").attr("title");
        InvestmentPlanProductObj["CashMarket"] = $(ob).find("span[eacflag='CashMarket']").attr("title");

         YearlyEarningsRate1 = $(ob).find("span[eacflag='YearlyEarningsRate1']").attr("title");
        if (YearlyEarningsRate1 != undefined && YearlyEarningsRate1.indexOf("%") > -1) {
            YearlyEarningsRate1 = (YearlyEarningsRate1.replace(/\%$/g, "")) * 1;
        };
        InvestmentPlanProductObj["YearlyEarningsRate1"] = YearlyEarningsRate1
      
        //债劵基金
        InvestmentPlanProductObj["BondCode"] = $(ob).find("span[eacflag='BondCode']").attr("title");
        InvestmentPlanProductObj["BondFund"] = $(ob).find("span[eacflag='BondFund']").attr("title");
        InvestmentPlanProductObj["BondMarket"] = $(ob).find("span[eacflag='BondMarket']").attr("title");

         YearlyEarningsRate2 = $(ob).find("span[eacflag='YearlyEarningsRate2']").attr("title");
        if (YearlyEarningsRate2 != undefined && YearlyEarningsRate2.indexOf("%") > -1) {
            YearlyEarningsRate2 = (YearlyEarningsRate2.replace(/\%$/g, "")) * 1;
        };

        InvestmentPlanProductObj["YearlyEarningsRate2"] = YearlyEarningsRate2;
     
        //股票基金
        InvestmentPlanProductObj["StockCode"] = $(ob).find("span[eacflag='StockCode']").attr("title");
        InvestmentPlanProductObj["StockFund"] = $(ob).find("span[eacflag='StockFund']").attr("title");
        InvestmentPlanProductObj["StockMarket"] = $(ob).find("span[eacflag='StockMarket']").attr("title");

        YearlyEarningsRate3 = $(ob).find("span[eacflag='YearlyEarningsRate3']").attr("title");
        if (YearlyEarningsRate3 != undefined && YearlyEarningsRate3.indexOf("%") > -1) {
            YearlyEarningsRate3 = (YearlyEarningsRate3.replace(/\%$/g, "")) * 1;
        };
        InvestmentPlanProductObj["YearlyEarningsRate3"] = YearlyEarningsRate3;
  
        //获取p2p产品
        InvestmentPlanProductObj["P2PName"] = $(ob).find("span[eacflag='P2PName']").attr("title");
        InvestmentPlanProductObj["InvestmentField"] = $(ob).find("span[eacflag='InvestmentField']").attr("title");
        InvestmentPlanProductObj["InvestmentCycle"] = $(ob).find("span[eacflag='InvestmentCycle']").attr("title");
        InvestmentPlanProductObj["StartAmount"] = $(ob).find("span[eacflag='StartAmount']").attr("title");
        InvestmentPlanProductObj["EarningsRate"] = $(ob).find("span[eacflag='EarningsRate']").attr("title");

        objList.push(InvestmentPlanProductObj);
    });
    //产品选择列表添加
    ProductPlanObj["InvestmentPlanProductList"] = objList;

    //获得投资规划分析
    var Analysis = $("#Analysis").val();
    ProductPlanObj["Analysis"] = Analysis;

    _ajaxhepler({
        url: "/CompetitionUser/InvestmentPlan/SaveInvestmentPlan",
        type: "POST",
        async: false,
        data: JSON.stringify(ProductPlanObj),
        contentType: "application/json",
        success: function (data) {
            if (data != null) {
                //保存原值
                SaveDefaultValueCommon("FinanceInvestmentPlanDiv");
                //先要赋值
                $("#FinanceInvestmentPlanDiv #InvestmentPlanId").val(data.Id);
                if (typeof saveFalg == "undefined") {
                    dialogHelper.Success({
                        content: "保存成功！",
                        success: function () {
                            window.location.reload();
                        }
                    });
                    //刷新页面


                }

            }
        }

    });
};

//判断三个必须选中一个
function CheckSelectVal() {
    var HoldRate = $.trim($("#FinanceInvestmentPlanDiv #HoldRate").val()); //保值层
    var IncreaseRate = $.trim($("#FinanceInvestmentPlanDiv #IncreaseRate").val());//增值层
    var SpeculationRate = $.trim($("#FinanceInvestmentPlanDiv #SpeculationRate").val());//投机层
    var DemandDepositsBankRate = "";//银行储蓄活期
    var TimeDepositBankRate = "";//银行储蓄定期
    var P2PProductRate = "";//P2P产品选择

    var CashCode = "";
    var BondCode = "";
    var StockCode = "";
    var result = "true";
    if (HoldRate * 1 != 0) {
        $("table[eacflag='ProductSelectTabCashFund']").each(function () {
            DemandDepositsBankRate = $(this).find("input[eacflag='DemandDepositsBankRate']").val();
            TimeDepositBankRate = $(this).find("input[eacflag='TimeDepositBankRate']").val();
            CashCode = $(this).find("span[eacflag='CashCode']").attr("title");
            if ((DemandDepositsBankRate * 1) == 0 && (TimeDepositBankRate * 1) == 0 && CashCode == undefined) {
                result = "保值层银行活期、定期、基金产品至少选择一个"; //表示你必须最少选中一项
                return false;
            };
        });
    }
    if (IncreaseRate * 1 != 0) {
        $("table[eacflag='ProductSelectTabBondFund']").each(function () {
            BondCode = $(this).find("span[eacflag='BondCode']").attr("title");
            if (BondCode == undefined) {
                result = "增值层里面基金产品为必填项";
                return false;
            }
        });
    }
    if (SpeculationRate * 1 != 0) {
        $("table[eacflag='ProductSelectTabStockFund']").each(function () {
            P2PProductRate = $(this).find("input[eacflag='P2PProduct']").val();
            StockCode = $(this).find("span[eacflag='StockCode']").attr("title");
            if ((P2PProductRate * 1) == 0 && StockCode == undefined) {
                result = "投机层里面P2P产品和基金产品至少选一个";
                return false;
            }
        });
    }
    return result;

};


//画饼
function ShowPieInfo(Currency, Bond, Stock) {
    var chart;
    $('.showPie').highcharts({
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false
        },
        title: {
            text: '投资分配比例',
            //align: 'left'
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
            name: '投资比例分配',
            data: [
                ['保值层', Currency],
                {
                    name: '增值层',
                    y: Bond,
                    sliced: false,
                    selected: false
                },
                ['投机层', Stock],
            ]
        }]
    });
}


function CheckNumTwo(num) {
    if (num == undefined || num == "") {
        num = 0;
    }
    num = num * 1
    return num;
}
//产品组合预期收益率：保值层比例× (E产品收益/n)（n，选择的总类数）+增值层比例×增值层产品收益率+投机层比例× (E各产品收益/n)
function productIncomeRate(HoldRate, IncreaseRate, SpeculationRate, DemandDepositsBankRate, TimeDepositBankRate, YearlyEarningsRate1, YearlyEarningsRate2, P2PProductRate, YearlyEarningsRate3) {
    var HoldRate1 = CheckNum(HoldRate);
    var IncreaseRate1 = CheckNum(IncreaseRate);
    var SpeculationRate1 = CheckNum(SpeculationRate);
    var CashNum = 0;//保值层计数
    var BondNum = 0;//增值层计数
    var FoundNum = 0//投机层计数
    var sum = 0;
    if (HoldRate1 == HoldRate && IncreaseRate1 == IncreaseRate && SpeculationRate1 == SpeculationRate) {
        HoldRate1 = HoldRate1 / 100;
        IncreaseRate1 = IncreaseRate1 / 100;
        SpeculationRate1 = SpeculationRate1 / 100;

        //保值层计数
        var calcList = new Array();
        calcList.push(DemandDepositsBankRate);
        calcList.push(TimeDepositBankRate);
        calcList.push(YearlyEarningsRate1);

        $(calcList).each(function (index, dom) {
            if (dom != 0) {
                CashNum++;
            }
        });
        //增值层计数
        if (YearlyEarningsRate2 != 0) {
            BondNum = 1;
        };
        //投机层计数
        var calcP2p = new Array();
        calcP2p.push(P2PProductRate);
        calcP2p.push(YearlyEarningsRate3);
        $(calcP2p).each(function (index, dom) {
            if (dom != 0) {
                FoundNum++;
            }

        });
        if (CashNum == 0) {
            CashNum = 1;
        }
        if (FoundNum == 0) {
            FoundNum = 1;
        }
        var CashRate = (HoldRate1 * (DemandDepositsBankRate + TimeDepositBankRate + YearlyEarningsRate1)) / CashNum;
        var BondRate = IncreaseRate1 * YearlyEarningsRate2;
        var FoundRate = SpeculationRate1 * (P2PProductRate + YearlyEarningsRate3) / FoundNum;
        sum = CashRate + BondRate + FoundRate;

    } else {
        sum = 0;
    }
    return sum;
}

/**
 * @name 计算每层产品收益
 */
function productIncomeRateVal() {
    var HoldRate = $("#HoldRate").val();
    var IncreaseRate = $("#IncreaseRate").val();
    var SpeculationRate = $("#SpeculationRate").val();
    var percent = /\%/g;
    if ($("div[eacflag='ProductSelect']").length > 0) {
        $("div[eacflag='ProductSelect']").each(function (index, dom) {
            var DivId = $(dom).attr("id");

            //保值层
            var DemandDepositsBankRate = $("#" + DivId).find("input[eacflag='DemandDepositsBankRate']").val();
            var TimeDepositBankRate = $("#" + DivId).find("input[eacflag='TimeDepositBankRate']").val();
            var YearlyEarningsRate1 = $("#" + DivId).find("span[eacflag='YearlyEarningsRate1']").attr("title");
            if (YearlyEarningsRate1 != undefined && YearlyEarningsRate1.indexOf("%") > -1) {
                YearlyEarningsRate1 = (YearlyEarningsRate1.replace(/\%$/g, "")) * 1;
            };
            //增值层
            var YearlyEarningsRate2 = $("#" + DivId).find("span[eacflag='YearlyEarningsRate2']").attr("title");
            if (YearlyEarningsRate2 != undefined && YearlyEarningsRate2.indexOf("%") > -1) {
                YearlyEarningsRate2 = (YearlyEarningsRate2.replace(/\%$/g, "")) * 1;
            };
            //投机层
            var EarningsRate = $("#" + DivId).find("span[eacflag='EarningsRate']").attr("title");
            if (EarningsRate != undefined && EarningsRate.indexOf("%") > -1) {
                EarningsRate = (EarningsRate.replace(/\%$/g, "")) * 1;
            };
            var YearlyEarningsRate3 = $("#" + DivId).find("span[eacflag='YearlyEarningsRate3']").attr("title");
            if (YearlyEarningsRate3 != undefined && YearlyEarningsRate3.indexOf("%") > -1) {
                YearlyEarningsRate3 = (YearlyEarningsRate3.replace(/\%$/g, "")) * 1;
            };

            HoldRate = CheckNumTwo(HoldRate);
            IncreaseRate = CheckNumTwo(IncreaseRate);
            SpeculationRate = CheckNumTwo(SpeculationRate);

            DemandDepositsBankRate = CheckNumTwo(DemandDepositsBankRate);
            TimeDepositBankRate = CheckNumTwo(TimeDepositBankRate);
            YearlyEarningsRate1 = CheckNumTwo(YearlyEarningsRate1);

            YearlyEarningsRate2 = CheckNumTwo(YearlyEarningsRate2);

            EarningsRate = CheckNumTwo(EarningsRate);
            YearlyEarningsRate3 = CheckNumTwo(YearlyEarningsRate3);

            var result = productIncomeRate(HoldRate, IncreaseRate, SpeculationRate, DemandDepositsBankRate, TimeDepositBankRate, YearlyEarningsRate1, YearlyEarningsRate2, EarningsRate, YearlyEarningsRate3);

            $("#" + DivId).find("input[eacflag='TotalRate']").val(result.toMyFixed(2));

        })
    }
};



//给增值曾添加星号
function AddStarSpan(flagDiv, strDiv) {
    //HedgeDiv 保值层 AddStar 增值层 SpeculateDiv 投机层
    $("#FinanceInvestmentPlanDiv tbody tr td[eacflag='" + flagDiv + "']").each(function () {
        $(this).html("<span class=\"c-red\" style=\"padding:5px 2px 0px 0px\">*</span><span>" + strDiv + "</span>");
    });
};
//去除增值层中的星号
function ClearStarSpan(flagDiv, strDiv) {
    $("#FinanceInvestmentPlanDiv tbody tr td[eacflag='" + flagDiv + "']").each(function () {
        $(this).html("<span>" + strDiv + "</span>");
    });
}


