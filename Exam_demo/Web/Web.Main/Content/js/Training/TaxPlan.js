var par = /^[-]*\d+(\.\d+)?$/;
var URL = "";
$(function () {
    //客户信息是否保存
    IsProposalSave();

    //加载信息
    GetTaxPlan();
    //获取URL参数
    //获取URL参数
    var ProposalId = $.getUrlParam("ProposalId");
    var TrainExamId = $.getUrlParam("TrainExamId");
    var StuCustomerId = $.getUrlParam("StuCustomerId");
    URL = "?TrainExamId=" + TrainExamId + "&ProposalId=" + ProposalId + "&StuCustomerId=" + StuCustomerId;
    //保存
    $("#TaxPlan #btnSave").live("click", function () {
        //添加数据
        AddTaxPlan(0);
    });
    //同时绑定下一页事件
    $("#TaxPlan #btnNext").live("click", function () {
        var fag = AddTaxPlan(1);
       
    });
   
    //工资、薪金所得 / 元
    $("#Salary").unbind("blur").blur(function () {
        var Salary = $.trim($("#Salary").val()) * 1;
       
        var Num = Salary;
        var par = /^\d+(\.\d+)?$/;
        if (!par.test(Num)) {
            Num = 0;
        }
        var revenue = 0;
        //计算公式：所得税金额=（薪资所得-3500）×税率-速算扣除数
        var Money = Num - 3500;
        if(Money<=0){
            revenue = 0;
        } else if (Money<=1500) {
            revenue = Money * 0.03;
        } else if (Money>1500 && Money <= 4500) {
            revenue = Money * 0.1 - 105;
        } else if (Money > 4500 && Money <= 9000) {
            revenue = Money * 0.2 - 555;
        } else if (Money > 9000 && Money <= 35000) {
            revenue = Money * 0.25 - 1005;
        } else if (Money > 35000 && Money <= 55000) {
            revenue = Money * 0.3 - 2755;
        } else if (Money > 55000 && Money <= 80000) {
            revenue = Money * 0.35 - 5505;
        } else if (Money > 80000) {
            revenue = Money * 0.45 - 13505;
        }

        $("#SalaryTax").val((revenue).toMyFixed(2));
        summation();
    });
    //个体工商户的生产、经营所得/元
    $("#OperatingRevenue").unbind("blur").blur(function () {
        var OperatingRevenue = $.trim($("#OperatingRevenue").val()) * 1;

        var Num = OperatingRevenue;

        if (!par.test(Num)) {
            Num = 0;
        }
        var revenue = 0;
        //所得税金额=应纳税所得额×适用税率-速算扣除数=[全年收入总额-成本、费用及损失-42000元/n年（3500元/月×12）]×适用税率-速算扣除数
        var Money = Num - 42000;
        if (Money>0 && Money <= 15000) {
            revenue = Money * 0.05;
        } else if (Money > 15000 && Money <= 30000) {
            revenue = Money * 0.1 - 750;
        } else if (Money > 30000 && Money <= 60000) {
            revenue = Money * 0.2 - 3750;
        } else if (Money > 60000 && Money <= 100000) {
            revenue = Money * 0.3 - 9750;
        } else if (Money > 100000) {
            revenue = Money * 0.35 - 14750;
        }
        $("#OperatingRevenueTax").val((revenue).toMyFixed(2));
        summation();
    });
    //对企业事业单位承包、承租经营所得/元
    $("#EnterprisesRevenue").unbind("blur").blur(function () {
        var EnterprisesRevenue = $.trim($("#EnterprisesRevenue").val()) * 1;

        var Num = EnterprisesRevenue;

        if (!par.test(Num)) {
            Num = 0;
        }
        var revenue = 0;
        //所得税金额=应纳税所得额×适用税率-速算扣除数=[全年收入总额-成本、费用及损失-42000元/n年（3500元/月×12）]×适用税率-速算扣除数
        var Money = Num - 42000;
        if (Money > 0 && Money <= 15000) {
            revenue = Money * 0.05;
        } else if (Money > 15000 && Money <= 30000) {
            revenue = Money * 0.1 - 750;
        } else if (Money > 30000 && Money <= 60000) {
            revenue = Money * 0.2 - 3750;
        } else if (Money > 60000 && Money <= 100000) {
            revenue = Money * 0.3 - 9750;
        } else if (Money > 100000) {
            revenue = Money * 0.35 - 14750;
        }
        $("#EnterprisesRevenueTax").val((revenue).toMyFixed(2));
        summation();
    });
    //劳务报酬所得/元
    $("#ServiceIncome").unbind("blur").blur(function () {
        var ServiceIncome = $.trim($("#ServiceIncome").val()) * 1;

        var Num = ServiceIncome;

        if (!par.test(Num)) {
            Num = 0;
        }
        var revenue = 0;     

        if (Num < 4000) {
            var Money = Num - 800;
            //（劳务报酬所得－800 ）×税率－速算扣除数
            if(Money>0 && Money<=20000){
                revenue = Money * 0.2;
            } else if(Money>20000 && Money<=50000) {
                revenue = Money * 0.3 - 2000;
            } else if (Money > 20000 && Money <= 50000) {
                revenue = Money * 0.4 - 7000;
            }
        } else {
            var Money = Num * 0.8;
            //[劳务报酬所得×（1-20%）]×所得税率－速算扣除数
            if (Money > 0 && Money <= 20000) {
                revenue = Money * 0.2;
            } else if (Money > 20000 && Money <= 50000) {
                revenue = Money * 0.3 - 2000;
            } else if (Money > 50000 ) {
                revenue = Money * 0.4 - 7000;
            }
        }
        
        $("#ServiceIncomeTax").val((revenue).toMyFixed(2));
        summation();
    });

    //稿酬所得/元
    $("#Remuneration").unbind("blur").blur(function () {
        var Remuneration = $.trim($("#Remuneration").val()) * 1;

        var Num = Remuneration;

        if (!par.test(Num)) {
            Num = 0;
        }
        var revenue = 0;
        
        if (Num<=800) {
            revenue = 0;
        }
        else if (Num <= 4000) {
            //[（稿酬所得－800 ）×20%]（1-30%）
            revenue = [(Num-800) * 0.2] * 0.7;
        } else {
            //｛[劳务报酬所得×（1-20%）]×20%｝×（1-30%）
            revenue = [(Num * 0.8) * 0.2] * 0.7;
        } 

        $("#RemunerationTax").val((revenue).toMyFixed(2));
        summation();
    });


    //特许权使用费所得/元
    $("#Loyalities").unbind("blur").blur(function () {
        var Loyalities = $.trim($("#Loyalities").val()) * 1;

        var Num = Loyalities;

        if (!par.test(Num)) {
            Num = 0;
        }
        var revenue = 0;
       
        if(Num<=800){
            revenue = 0;
        }
        else if (Num < 4000) {
            revenue = (Num - 800) * 0.2;
        } else {
            revenue = [Num * 0.8] * 0.2;
        }     

        $("#LoyalitiesTax").val((revenue).toMyFixed(2));
        summation();
    });



    //	财产转让所得
    $("#Demise").unbind("blur").blur(function () {
        var Demise = $.trim($("#Demise").val()) * 1;

        var Num = Demise;

        if (!par.test(Num)) {
            Num = 0;
        }
        var revenue = Num * 0.2;       
        $("#DemiseTax").val((revenue).toMyFixed(2));
        summation();
    });

    //偶然所得/元
    $("#IncidentalIncome").unbind("blur").blur(function () {
        var IncidentalIncome = $.trim($("#IncidentalIncome").val()) * 1;

        var Num = IncidentalIncome;

        if (!par.test(Num)) {
            Num = 0;
        }
        var revenue = Num * 0.2;
        $("#IncidentalIncomeTax").val((revenue).toMyFixed(2));
        summation();
    });

    //利息、红利、股利所得/元
    $("#Interest").unbind("blur").blur(function () {
        var Interest = $.trim($("#Interest").val()) * 1;

        var Num = Interest;

        if (!par.test(Num)) {
            Num = 0;
        }
        var revenue = Num * 0.2;
        $("#InterestTax").val((revenue).toMyFixed(2));
        summation();
    });

    //保存原值
    SaveDefaultValueCommon("TaxPlan");
});

//获取税收筹划相关信息
function GetTaxPlan() {
    var ProposalId = $.getUrlParam("ProposalId");

    _ajaxhepler({
        url: "/CompetitionUser/TaxPlan/GetTaxPlanObj",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: ProposalId
        },
        success: function (datas) {
            //教育规划信息
            var li = datas.list;
            if (li != null) {
                var n = li;
                    $("#Salary").val(n.Salary.toMyFixed(2));//工资、薪金所得
                    $("#SalaryTax").val(n.SalaryTax.toMyFixed(2));//工资、薪金所得税
                    $("#OperatingRevenue").val(n.OperatingRevenue.toMyFixed(2));//个体工商户的生产、经营所得
                    $("#OperatingRevenueTax").val(n.OperatingRevenueTax.toMyFixed(2));//个体工商户的生产、经营所得税
                    $("#EnterprisesRevenue").val(n.EnterprisesRevenue.toMyFixed(2));// 对企事业单位承包、承租经营所得
                    $("#EnterprisesRevenueTax").val(n.EnterprisesRevenueTax.toMyFixed(2));//对企事业单位承包、承租经营所得税
                    $("#ServiceIncome").val(n.ServiceIncome.toMyFixed(2));//劳务报酬所得
                    $("#ServiceIncomeTax").val(n.ServiceIncomeTax.toMyFixed(2));//劳务报酬所得税
                    $("#Remuneration").val(n.Remuneration.toMyFixed(2));//稿酬所得
                    $("#RemunerationTax").val(n.RemunerationTax.toMyFixed(2));//稿酬所得税
                    $("#Loyalities").val(n.Loyalities.toMyFixed(2));//特许权使用费所得
                    $("#LoyalitiesTax").val(n.LoyalitiesTax.toMyFixed(2));//特许权使用费所得税
                    $("#Demise").val(n.Demise.toMyFixed(2));// 财产转让所得
                    $("#DemiseTax").val(n.DemiseTax.toMyFixed(2));//财产转让所得税
                    $("#IncidentalIncome").val(n.IncidentalIncome.toMyFixed(2));//偶然所得
                    $("#IncidentalIncomeTax").val(n.IncidentalIncomeTax.toMyFixed(2));//偶然所得税
                    $("#Interest").val(n.Interest.toMyFixed(2));//利息、红利、股利所得
                    $("#InterestTax").val(n.InterestTax.toMyFixed(2));//利息、红利、股利所得税
                    $("#TotalAmount").val(n.TotalAmount.toMyFixed(2));//合计
                    $("#TotalTax").val(n.TotalTax.toMyFixed(2));//合计税
                    $("#Analysis").val(n.Analysis);//税收筹划分析
                    //隐藏域
                    $("#Id").val(n.Id);

            }          
        }
    });
}

//添加税收筹划
function AddTaxPlan(valu) {
    if (!VerificationHelper.checkFrom("TaxPlan")) {
        return;
    } else
    {
        var ProposalId = $.getUrlParam("ProposalId");

        if (ProposalId != 0) {

            var obj = new Object();
            obj["Id"] = $("#Id").val();//Id
            obj["ProposalId"] = ProposalId;//建议书Id
            obj["Salary"] = $("#Salary").val();//工资、薪金所得
            obj["SalaryTax"] = $("#SalaryTax").val();//工资、薪金所得税
            obj["OperatingRevenue"] = $("#OperatingRevenue").val();//个体工商户的生产、经营所得
            obj["OperatingRevenueTax"] = $("#OperatingRevenueTax").val();//个体工商户的生产、经营所得税
            obj["EnterprisesRevenue"] = $("#EnterprisesRevenue").val();// 对企事业单位承包、承租经营所得
            obj["EnterprisesRevenueTax"] = $("#EnterprisesRevenueTax").val();//对企事业单位承包、承租经营所得税
            obj["ServiceIncome"] = $("#ServiceIncome").val();//劳务报酬所得
            obj["ServiceIncomeTax"] = $("#ServiceIncomeTax").val();//劳务报酬所得税
            obj["Remuneration"] = $("#Remuneration").val();//稿酬所得
            obj["RemunerationTax"] = $("#RemunerationTax").val();//稿酬所得税
            obj["Loyalities"] = $("#Loyalities").val();//特许权使用费所得
            obj["LoyalitiesTax"] = $("#LoyalitiesTax").val();//特许权使用费所得税
            obj["Demise"] = $("#Demise").val();// 财产转让所得
            obj["DemiseTax"] = $("#DemiseTax").val();//财产转让所得税
            obj["IncidentalIncome"] = $("#IncidentalIncome").val();//偶然所得
            obj["IncidentalIncomeTax"] = $("#IncidentalIncomeTax").val();//偶然所得税
            obj["Interest"] = $("#Interest").val();//利息、红利、股利所得
            obj["InterestTax"] = $("#InterestTax").val();//利息、红利、股利所得税
            obj["TotalAmount"] = $("#TotalAmount").val();//合计
            obj["TotalTax"] = $("#TotalTax").val();//合计税
            obj["Analysis"] = $("#Analysis").val();//税收筹划分析

            _ajaxhepler({
                url: "/CompetitionUser/TaxPlan/AddTaxPlan",
                type: "POST",
                async: false,
                dataType: "json",
                data: JSON.stringify(obj),
                contentType: "application/json",
                success: function (data, txtStatus) {
                    //保存原值
                    SaveDefaultValueCommon("TaxPlan");
                    if (valu == 0) {
                        dialogHelper.Success({
                            content: "保存成功！", success: function () {
                                //刷新当前页
                                location.href = location.href;
                            }
                        });
                    } else {
                        window.location.href = "/CompetitionUser/DistributionOfProperty/Index" + URL;
                    }
                }
            });
        } else {
            dialogHelper.Error({ content: "请先添加客户信息！", success: function () { } });
            fag = true;
        }
    }
}

//合计
function summation() {
    //金额 合计
    var Salary = $.trim($("#Salary").val()) * 1;
    var OperatingRevenue = $.trim($("#OperatingRevenue").val()) * 1;
    var EnterprisesRevenue = $.trim($("#EnterprisesRevenue").val()) * 1;
    var ServiceIncome = $.trim($("#ServiceIncome").val()) * 1;
    var Remuneration = $.trim($("#Remuneration").val()) * 1;
    var Loyalities = $.trim($("#Loyalities").val()) * 1;
    var Demise = $.trim($("#Demise").val()) * 1;
    var IncidentalIncome = $.trim($("#IncidentalIncome").val()) * 1;
    var Interest = $.trim($("#Interest").val()) * 1;

    var Num = Salary + OperatingRevenue + EnterprisesRevenue + ServiceIncome + Remuneration + Loyalities + Demise + IncidentalIncome + Interest;

    if (!par.test(Num)) {
        Num = 0;
    }
    $("#TotalAmount").val((Num).toMyFixed(2));

    //所得税金额 合计
    var SalaryTax = $.trim($("#SalaryTax").val()) * 1;
    var OperatingRevenueTax = $.trim($("#OperatingRevenueTax").val()) * 1;
    var EnterprisesRevenueTax = $.trim($("#EnterprisesRevenueTax").val()) * 1;
    var ServiceIncomeTax = $.trim($("#ServiceIncomeTax").val()) * 1;
    var RemunerationTax = $.trim($("#RemunerationTax").val()) * 1;
    var LoyalitiesTax = $.trim($("#LoyalitiesTax").val()) * 1;
    var DemiseTax = $.trim($("#DemiseTax").val()) * 1;
    var IncidentalIncomeTax = $.trim($("#IncidentalIncomeTax").val()) * 1;
    var InterestTax = $.trim($("#InterestTax").val()) * 1;

    var Num2 = SalaryTax + OperatingRevenueTax + EnterprisesRevenueTax + ServiceIncomeTax + RemunerationTax + LoyalitiesTax + DemiseTax + IncidentalIncomeTax + InterestTax;

    if (!par.test(Num2)) {
        Num2 = 0;
    }
    $("#TotalTax").val((Num2).toMyFixed(2));
}