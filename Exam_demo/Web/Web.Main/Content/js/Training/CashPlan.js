//**************************
//考生端-实训考试-现金规划
//**************************
var param = "";
var TagNavi = true;

//判断是否非法字符
function CheckNum(num) {
    var pattern6 = /^[-]?\d+(\.\d{1,6})?$/;//只能输入两位小数
    if (!pattern6.test(num)) {
        num = 0;
        return num;
    }
    num = (num * 1).toMyFixed(2);
    return num;
};

//家月支出乘以倍数
function calcCashPanValOne(FamilyMonthExpense, RetainCashType) {
    //大数字计算，采用decimal.js
    var sum = new Decimal(CheckNum(FamilyMonthExpense));
    sum = sum.times(RetainCashType).toFixed(2);
    return sum
}

function calcCashPanValTwo() {
    var FamilyMonthExpense = $.trim($("#FamilyMonthExpense").val());
    var RetainCashType = $("#RetainCashType").val();
    var result = calcCashPanValOne(FamilyMonthExpense, RetainCashType);
    $("#RetainCashMultiple").val(result);
}


//计算
var clacCashPanVal = {
    calcRetainCashType: function (multiple) {
        //var multiple = $("RetainCashMultiple").val();
        if (multiple == "0") {
            $("#RetainCashMultiple").val(0);
        } else {
            var familyMonthExpense = new Decimal(CheckNum($.trim($("#FamilyMonthExpense").val())));
            var result = familyMonthExpense.times(multiple).toFixed(2);
            return result;
        }
    }
}


//加载  //这里后期可以改成GetCashPlanByProposalId这个方法。根据建议书去获取  （?t=" + Math.random()）
var CashPlanJsObj = {
    GetCashPlanByProposalId: function (proposalId) {
        _ajaxhepler({
            url: "/CompetitionUser/CashPlan/GetCashPlanByProposalId",
            type: "POST",
            async: false,
            dataType: "json",
            data:
            {
                proposalId: proposalId
            },
            success: function (data) {
                if (data != null) {
                    if (data.Analysis != null) {
                        SetCashFlow(data);
                    } else {
                        SetCashFlowTwo(data);
                    }
                }
            }
        });
    }
};
//保存后加载加在现金规划
function SetCashFlow(data) {
    var Id = data.Id;
    var FamilyMonthExpense = data.FamilyMonthExpense;
    if (FamilyMonthExpense != 0) {
        //家月支出
        // $("#FamilyMonthExpense").addClass("disabled").attr("readonly", "readonly");
    }
    var RetainCashType = data.RetainCashType;
    var Deposit = data.Deposit;
    var Fund = data.Fund;
    var CreditCard = data.CreditCard;
    var Analysis = data.Analysis;
    var ProposalId = data.ProposalId;
    $("#ProposalId").val(ProposalId);//赋值建议书ID
    $("#CashPlanId").val(Id);
    $("#FamilyMonthExpense").val(FamilyMonthExpense);
    //$("#RetainCashType").find("option[value='" + RetainCashType + "']").attr("selected", true);
    $("#RetainCashType").val(RetainCashType);
    var retainCashVal = clacCashPanVal.calcRetainCashType(RetainCashType);
    $("#RetainCashMultiple").val(retainCashVal);
    $("#Deposit").val(Deposit);
    $("#Fund").val(Fund);
    $("#CreditCard").val(CreditCard);
    $("#Analysis").val(Analysis);
};
//初次加载
function SetCashFlowTwo(data) {
    var Id = data.Id;
    var FamilyMonthExpense = data.FamilyMonthExpense;
    if (FamilyMonthExpense != 0) {
        //家月支出
        //  $("#FamilyMonthExpense").addClass("disabled").attr("readonly", "readonly");
    } else {
        FamilyMonthExpense = "";

    }
    var RetainCashType = data.RetainCashType;
    var Deposit = data.Deposit == 0 ? "" : data.Deposit;
    var Fund = data.Fund == 0 ? "" : data.Fund;;
    var CreditCard = data.CreditCard == 0 ? "" : data.CreditCard;
    var Analysis = data.Analysis;
    var ProposalId = data.ProposalId;
    $("#ProposalId").val(ProposalId);//赋值建议书ID
    $("#CashPlanId").val(Id);
    $("#FamilyMonthExpense").val(FamilyMonthExpense);
    //$("#RetainCashType").find("option[value='" + RetainCashType + "']").attr("selected", true);
    $("#RetainCashType").val(RetainCashType);
    //   var retainCashVal = clacCashPanVal.calcRetainCashType(RetainCashType);
    $("#RetainCashMultiple").val("");
    $("#Deposit").val(Deposit);
    $("#Fund").val(Fund);
    $("#CreditCard").val(CreditCard);
    $("#Analysis").val(Analysis);
}


//保存
var CashPlanJsSave = {
    GetCashPlanBySave: function (saveTag) {
        TagNavi = true;
        //字段验证
        if (!VerificationHelper.checkFrom("FinanceCashPlanDiv")) {
            TagNavi = false;
            return;
        }
        //验证下面3货是否相同
        var FamilyMonthExpense = $.trim($("#FinanceCashPlanDiv #FamilyMonthExpense").val());
        var RetainCashType = $.trim($("#FinanceCashPlanDiv #RetainCashType").val());
        var Deposit = $.trim($("#FinanceCashPlanDiv #Deposit").val());
        var Fund = $.trim($("#FinanceCashPlanDiv #Fund").val());
        var CreditCard = $.trim($("#FinanceCashPlanDiv #CreditCard").val());
        var Analysis = $.trim($("#FinanceCashPlanDiv #Analysis").val());
        if (!CheckValIsEqual()) {
            dialogHelper.Error({ content: "建议方案总额必须与现金保留规模保持一致" })
            TagNavi = false;
            return;
        }

        //加载现金规划
        //获取URL参数
        var ProposalId = $.getUrlParam("ProposalId");

        //获取财产传承
        if (ProposalId != null && ProposalId != "" && ProposalId != undefined) {

            var obj = new Object();
            obj["Id"] = $("#FinanceCashPlanDiv #CashPlanId").val();
            obj["ProposalId"] = ProposalId;
            obj["FamilyMonthExpense"] = FamilyMonthExpense;
            obj["RetainCashType"] = RetainCashType;
            obj["Deposit"] = Deposit;
            obj["Fund"] = Fund;
            obj["CreditCard"] = CreditCard;
            obj["Analysis"] = Analysis;//取隐藏用户Id

            _ajaxhepler({
                url: "/CompetitionUser/CashPlan/SaveCashPlanBy",
                type: "POST",
                async: false,
                dataType: "json",
                data: JSON.stringify(obj),
                contentType: "application/json",
                success: function (data) {
                    if (data != null) {
                        $("#FinanceCashPlanDiv #CashPlanId").val(data.Id);
                        //保存之后必须重新保存一下基础值
                        SaveDefaultValueCommon("FinanceCashPlanDiv");
                        if (typeof saveTag == "undefined") {
                            //弹出提示
                            dialogHelper.Success({
                                content: "保存成功！", success: function () {
                                    window.location.reload();
                                }
                            });
                        }
                    }

                }
            });
        } else {
            return false;
        }
        //没有proposalId+
    }
}
//检验总额与现金保留规模保持一致
function CheckValIsEqual() {
    var result = false;
    var Deposit = $.trim($("#FinanceCashPlanDiv #Deposit").val());
    var Fund = $.trim($("#FinanceCashPlanDiv #Fund").val());
    var CreditCard = $.trim($("#FinanceCashPlanDiv #CreditCard").val());
    var RetainCashMultiple = new Decimal($.trim($("#RetainCashMultiple").val()));
    //大数字计算，采用decimal.js
    //var sum = Deposit + Fund + CreditCard;
    var sum = new Decimal(Deposit);
    sum = sum.plus(Fund).plus(CreditCard);
    if (sum.comparedTo(RetainCashMultiple) == 0) {
        result = true;
        return result;
    }
    return result;
}


$(function () {
    IsProposalSave()//客户验证

    param = $("#hdParam").val();
    $("#FinanceCashPlanDiv #RetainCashType").unbind("change").change(function () {
        var multiple = $(this).val();
        var result = clacCashPanVal.calcRetainCashType(multiple);
        $("#RetainCashMultiple").val(result);
    });
    //加载现金规划
    //获取URL参数
    var ProposalId = $.getUrlParam("ProposalId");

    //获取财产传承
    if (ProposalId != null && ProposalId != "" && ProposalId != undefined) {

        CashPlanJsObj.GetCashPlanByProposalId(ProposalId);
    };
    //绑定已开鼠标事件
    $("#FamilyMonthExpense").unbind("blur").blur(function () {
        calcCashPanValTwo();
    });

    //给保存注册click事件
    $("#FinanceCashPlanDiv #btnSave").live("click", function () {
        CashPlanJsSave.GetCashPlanBySave();
    });

    //给下一页注册click事件
    $("#FinanceCashPlanDiv #btnNext").live("click", function () {
        CashPlanJsSave.GetCashPlanBySave(0);
        //同时跳转
        if (TagNavi) {
            window.location.href = "/CompetitionUser/LifeEducationPlan/Index" + param;
        }
    });


    //保存之后必须重新保存一下基础值
    SaveDefaultValueCommon("FinanceCashPlanDiv");

});