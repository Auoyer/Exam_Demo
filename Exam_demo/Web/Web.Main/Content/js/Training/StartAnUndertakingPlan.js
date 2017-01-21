var URL = "";
$(function () {
    IsProposalSave()//客户验证
    //获取URL参数
    //获取URL参数
    var ProposalId = $.getUrlParam("ProposalId");
    var TrainExamId = $.getUrlParam("TrainExamId");
    var StuCustomerId = $.getUrlParam("StuCustomerId");
    URL = "?TrainExamId=" + TrainExamId + "&ProposalId=" + ProposalId + "&StuCustomerId=" + StuCustomerId;


    //加载创业规划相关信息
    GetStartAnUndertakingPlanList()

    //加载每月可支配资金 可用资产
    EveryMonthMoney("StartAnUndertakingPlan/GetmoneyList2", ProposalId, "StartAnUndertakingPlan");

    //把值赋给defaultVal作为原值
    SaveDefaultValueCommon("StartAnUndertakingPlan");

    //保存
    $("#StartAnUndertakingPlan #btnSave").live("click", function () {
        //添加数据
        AddStartAnUndertakingPlan(0);
    });
    //同时绑定下一页事件
    $("#StartAnUndertakingPlan #btnNext").live("click", function () {
        var fag = AddStartAnUndertakingPlan(1);
             
    });
    //同时绑定上一页事件
    $("#StartAnUndertakingPlan #btnPrev").live("click", function () {
        var fag = AddStartAnUndertakingPlan(2);
    });

    //离创业年限
    $("#StartPlanAge").unbind("blur").blur(function () {

        var Age = $.trim($("#Age").val()) * 1;
        var StartPlanAge = $.trim($("#StartPlanAge").val()) * 1;
        if (Age > 0 && StartPlanAge > 0) {
            var Num = StartPlanAge - Age;
            var par = /^\d+(\.\d+)?$/;
            if (!par.test(Num)) {
                Num = 0;
            }
            //离创业年限
            $("#DistanceYear").val((Num));
        }
    });

    //此方案能实现的目标金额
    $("#ReturnOnInvestmentRate,#DisposableInput,#MonthlyInvestment,#RegularYear").unbind("blur").blur(function () {

        var ReturnOnInvestmentRate = $.trim($("#ReturnOnInvestmentRate").val()) * 1;
        var DisposableInput = $.trim($("#DisposableInput").val()) * 1;
        var MonthlyInvestment = $.trim($("#MonthlyInvestment").val()) * 1;
        var RegularYear = $.trim($("#RegularYear").val()) * 1;

        //FV（投资收益率÷12，定期定额投资年限×12，- 每月定期定额投资金额，- 一次性投资金额,0）
        if (ReturnOnInvestmentRate > 0 && DisposableInput > 0 && MonthlyInvestment > 0 && RegularYear > 0)
        { 
            var Num = ReturnOnInvestmentRate + DisposableInput + MonthlyInvestment + RegularYear;
            var par = /^\d+(\.\d+)?$/;
            if (!par.test(Num)) {
                Num = 0;
            }
            if (Num != 0) {
                //此方案能实现的目标金额 FV（投资收益率÷12，定期定额投资年限×12，- 每月定期定额投资金额，- 一次性投资金额,0）
                _ajaxhepler({
                    url: "/CompetitionUser/LifeEducationPlan/RetailLump",
                    type: "POST",
                    async: false,
                    dataType: "json",
                    data: {
                        rate: ReturnOnInvestmentRate/100/12,
                        nper: RegularYear*12,
                        pmt:- MonthlyInvestment,
                        pv: -DisposableInput,
                        type: 0
                    },
                    success: function (data) {
                        Num = data;
                    }
                });
            }
            //此方案能实现的目标金额
            $("#TargetAmount").html((Num.toMyFixed(2)));
            }
        });
    
        //客户信息是否保存
        IsProposalSave();
    
});

//获取创业规划相关信息
function GetStartAnUndertakingPlanList() {

    //获取URL参数
    var ProposalId = $.getUrlParam("ProposalId");

    _ajaxhepler({
        url: "/CompetitionUser/StartAnUndertakingPlan/GetSUPList",
        type: "POST",
        async: false,
        dataType: "json",
        data: {
            ProposalId: ProposalId
        },
        success: function (data) {
            
            //创业规划信息
            var li = data.list;
            if (li != null)
            {
                $("#Age").val(li.Age);//当前年龄

                $("#StartPlanAge").val(li.StartPlanAge);//计划创业年龄
                $("#CostInput").val(li.CostInput.toMyFixed(2));//创业时一次性投入
                $("#DistanceYear").val(li.DistanceYear);//离创业年限
                $("#ReturnOnInvestmentRate").val(li.ReturnOnInvestmentRate);//预计投资收益率
                $("#DisposableInput").val(li.DisposableInput.toMyFixed(2));//一次性投入金额
                $("#MonthlyInvestment").val(li.MonthlyInvestment.toMyFixed(2));//每月定期投资金额
                $("#RegularYear").val(li.RegularYear);//定期定额投资年限
                $("#TargetAmount").html(li.TargetAmount.toMyFixed(2));//此方案能实现的目标金额
                $("#Analysis").val(li.Analysis);//创业规划分析
                $("#Id").val(li.Id)
            }
            //客户信息
            var li2 = data.list2;
            if (li2 != null) {
                var n = li2;
                $("#Age").val(n.Age);//当前年龄                
            }
        }
    });
}
//添加/修改 创业规划
function AddStartAnUndertakingPlan(valu) {
    var fag = false;
    if (!VerificationHelper.checkFrom("StartAnUndertakingPlan")) {
        return;
    } else {
        //获取URL参数
        var ProposalId = $.getUrlParam("ProposalId");
        if (ProposalId != 0) {

            var age = $("#Age").val();
            var StartPlanAge =Number( $("#StartPlanAge").val());
            var RegularYear = Number($("#RegularYear").val());
            var DistanceYear = Number($("#DistanceYear").val());
            if (StartPlanAge >= age) {
                

                    var obj = new Object();
                    obj["Id"] = $("#Id").val();
                    obj["ProposalId"] = ProposalId;
                    obj["Age"] = $("#Age").val();//当前年龄
                    obj["StartPlanAge"] = $("#StartPlanAge").val();//计划创业年龄
                    obj["DistanceYear"] = $("#DistanceYear").val();
                    obj["CostInput"] = $("#CostInput").val();//创业时一次性投入
                    obj["ReturnOnInvestmentRate"] = $("#ReturnOnInvestmentRate").val();//预计投资收益率
                    obj["DisposableInput"] = $("#DisposableInput").val();//一次性投入金额
                    obj["MonthlyInvestment"] = $("#MonthlyInvestment").val();//每月定期投资金额
                    obj["RegularYear"] = $("#RegularYear").val();//定期定额投资年限
                    obj["TargetAmount"] = $("#TargetAmount").html();//此方案能实现的目标金额
                    obj["Analysis"] = $("#Analysis").val();//创业规划分析

                    _ajaxhepler({
                        url: "/CompetitionUser/StartAnUndertakingPlan/AddSUP",
                        type: "POST",
                        async: false,
                        dataType: "json",
                        data: JSON.stringify(obj),
                        contentType: "application/json",
                        success: function (data, txtStatus) {
                            GetStartAnUndertakingPlanList();
                            if (valu == 0) {

                                dialogHelper.Success({
                                    content: "保存成功！", success: function () {

                                        //刷新当前页
                                        location.href = location.href;

                                        //把值赋给defaultVal作为原值
                                        SaveDefaultValueCommon("StartAnUndertakingPlan");
                                    }
                                });

                            } else if (valu==1) {
                                window.location.href = "/CompetitionUser/RetirementPlan/Index" + URL;
                            } else if (valu == 2) {
                                window.location.href = "/CompetitionUser/ConsumptionPlan/Index" + URL;
                            }
                            
                        }
                    });
                
            } else {
                dialogHelper.Error({ content: "计划创业年龄要大于等于当前年龄！", success: function () { } });
                fag = true;
            }
        } else {

            dialogHelper.Error({ content: "请先添加客户信息！", success: function () { } });
            fag = true;
        }
    }
    return fag;
}