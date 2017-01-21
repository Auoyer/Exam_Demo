//**************************
//考生端-实训考试-消费需求测算
//**************************

var par = /^[-]*\d+(\.\d+)?$/;
var URL = "";
$(function () {

    //获取URL参数
    //获取URL参数
    var ProposalId = $.getUrlParam("ProposalId");
    var TrainExamId = $.getUrlParam("TrainExamId");
    var StuCustomerId = $.getUrlParam("StuCustomerId");
    URL = "?TrainExamId=" + TrainExamId + "&ProposalId=" + ProposalId + "&StuCustomerId=" + StuCustomerId;
    $("#Add").hide();
    if (ProposalId == null)
    { $("#ShopCarDiv").show(); }
    //加载信息
    GetConsumptionPlan();


    if ($("#ShopCarYear").val() == "" && $("#ShopHouseYear").val() == "") {
        // $("#Add").show();
        $("#ShopCarDiv").show();
    }
    //加载每月可支配资金 可用资产
    EveryMonthMoney("LifeEducationPlan/GetmoneyList", ProposalId, "ConsumptionPlan");

    //把值赋给defaultVal作为原值
    SaveDefaultValueCommon("ConsumptionPlan");



    //保存
    $("#ConsumptionPlan #btnSave").live("click", function () {
        if ($("#HouseArea").val() == 0.01 && $("#HousePrice").val() == 0.01)
        { $("#HouseAllMoney").removeClass("IsMinFloat"); }
        else if ($("#HouseArea").val() == 9999.99 && $("#HousePrice").val() == 9999999.99)
        { $("#HouseAllMoney").removeClass("IsMaxFloat"); }
        //添加数据
        AddConsumptionPlan(0);
    });
    //同时绑定下一页事件
    $("#ConsumptionPlan #btnNext").live("click", function () {
        if ($("#HouseArea").val() == 0.01 && $("#HousePrice").val() == 0.01)
        { $("#HouseAllMoney").removeClass("IsMinFloat"); }
        else if ($("#HouseArea").val() == 9999.99 && $("#HousePrice").val() == 9999999.99)
        { $("#HouseAllMoney").removeClass("IsMaxFloat"); }
        var fag = AddConsumptionPlan(1);

    });
    //同时绑定上一页事件
    $("#ConsumptionPlan #btnPrev").live("click", function () {
        if ($("#HouseArea").val() == 0.01 && $("#HousePrice").val() == 0.01)
        { $("#HouseAllMoney").removeClass("IsMinFloat"); }
        else if ($("#HouseArea").val() == 9999.99 && $("#HousePrice").val() == 9999999.99)
        { $("#HouseAllMoney").removeClass("IsMaxFloat"); }
        var fag = AddConsumptionPlan(2);
    });

    //总金额

    //首付款
    $("#HouseAllMoney,#HouseDownPaymentPercent,#HouseArea,#HousePrice").unbind("blur").blur(function () {
        var HouseArea = $.trim($("#HouseArea").val()) * 1;
        var HousePrice = $.trim($("#HousePrice").val()) * 1;

        var Num2 = HouseArea * HousePrice;

        if (!par.test(Num2)) {
            Num2 = 0;
        }
        if ($("#HouseArea").val() != "" && $("#HousePrice").val() != "") {
            //总金额
            $("#HouseAllMoney").val((Num2).toMyFixed(2));
        }
        //输入了面积和单击，总金额不能编辑
        if (HouseArea > 0 && HousePrice > 0) {
            $("#HouseAllMoney").attr("disabled", true)
        }
        var HouseAllMoney = $.trim($("#HouseAllMoney").val()) * 1;
        var HouseDownPaymentPercent = $.trim($("#HouseDownPaymentPercent").val()) * 1;
        var Num = HouseAllMoney * HouseDownPaymentPercent / 100;

        if (!par.test(Num)) {
            Num = 0;
        }
        //首付款
        $("#HouseDownPayment").val((Num).toMyFixed(2));


        //需准备首付款总金额
        var m1 = $.trim($("#CarDownPayment").val()) * 1;
        var m2 = $.trim($("#HouseDownPayment").val()) * 1;
        var Num3 = m1 + m2;
        if (!par.test(Num3)) {
            Num3 = 0;
        }
        $("#FirstAmount").html((Num3).toMyFixed(2))

        ShopHouse();
        ShopHouseAllMoney();
    });

    //月供×12×贷款年限（输出）
    //购房总花费
    $("#HouseLoanYear,#HouseLoanRate").unbind("blur").blur(function () {
        //月供
        ShopHouse();

        ShopHouseAllMoney();

    });

    //点击隐藏购车
    $("#ShopCar").unbind("click").bind("click", function () {
        if ($("#ShopHouseDiv").is(":hidden")) {
            dialogHelper.Error({ content: "至少需要有一个面板" });
            return;
        }
        // AddTextCar();
        $("#TitleShopCar").hide();
        $("#ShopCarDiv").hide();
        $("#Add").show().attr("disabled", false);
        $("#ShopHouse").hide();

        //需准备首付款总金额
        var m1 = $.trim($("#CarDownPayment").val()) * 1;
        var Num3 = m1;
        if (!par.test(Num3)) {
            Num3 = 0;
        }
        var a = $("#FirstAmount").html()

        $("#FirstAmount").html((a - Num3).toMyFixed(2))

        //清空文本
        ClearText();
    });
    //点击隐藏购房
    $("#ShopHouse").unbind("click").bind("click", function () {
        if ($("#ShopCar").is(":hidden")) {
            dialogHelper.Error({ content: "至少需要有一个面板" });
            return;
        }
        // AddTextHouse();
        $("#TitleShopHouse").hide();
        $("#ShopHouseDiv").hide();
        $("#Add").show().attr("disabled", false);
        $("#ShopCar").hide();


        //需准备首付款总金       
        var m2 = $.trim($("#HouseDownPayment").val()) * 1;
        var Num3 = m2;
        if (!par.test(Num3)) {
            Num3 = 0;
        }
        var a = $("#FirstAmount").html()
        $("#FirstAmount").html((a - Num3).toMyFixed(2))

        //清空文本
        ClearText();
    });
    //点击添加
    $("#Add").unbind("click").bind("click", function () {

        if ($("#ShopHouseDiv").css("display") == "none") {
            $("#ShopHouseDiv").show();
            $("#TitleShopHouse").show();

            //两个删除按钮
            $("#ShopCar").show();
            $("#ShopHouse").show();


            $("#HouseDownPaymentPercent").val(30);
            $("#checked1").attr("checked", true);
            $("#HouseDownPaymentPercent").attr("disabled", true);
        }
        else {
            $("#ShopCarDiv").show();
            $("#ShopCar").show();
            $("#Add").hide();
            $("#ShopHouse").show();

            $("#VehicleAndVesselTax").val(180);
            $("#MotorVehicleCompulsory").val(950);
            $("#Selcts").val(180);
            $("#Selects2").val(950);
            $("#checked5").attr("checked", true);
            //  $("#checked9").attr("checked", true);
            $("#CarDownPaymentPercent").val(30);
            //  $("#CarLoanYear").val(12);
        }
        if (!$("#ShopHouseDiv").is(":hidden") && !$("#ShopCarDiv").is(":hidden"))
        { $("#Add").hide(); }

    });

    //车船使用税改变事件
    $("#Selcts").change(function () {
        $("#VehicleAndVesselTax").val($("#Selcts").val());
        ShopCarAllMoney();
    })

    //交强险改变事件
    $("#MotorVehicleCompulsory").val(950);
    $("#Selects2").change(function () {
        $("#MotorVehicleCompulsory").val($("#Selects2").val());
        ShopCarAllMoney();
    })


    //购车首付款   购置税
    $("#CarPrice,#CarDownPaymentPercent").unbind("blur").blur(function () {
        var CarPrice = $.trim($("#CarPrice").val()) * 1;
        if (CarPrice > 0) {
            var CarDownPaymentPercent = $.trim($("#CarDownPaymentPercent").val()) * 1;

            var Num = CarPrice * CarDownPaymentPercent / 100;

            if (!par.test(Num)) {
                Num = 0;
            }
            //购车首付款
            $("#CarDownPayment").val((Num).toMyFixed(2));

            var Num2 = (CarPrice / 1.17) * 0.1;

            if (!par.test(Num2)) {
                Num2 = 0;
            }
            //购置税
            $("#PurchaseTax").val((Num2).toMyFixed(2));

            //需准备首付款总金额
            var m1 = $.trim($("#CarDownPayment").val()) * 1;
            var m2 = $.trim($("#HouseDownPayment").val()) * 1;
            var Num3 = m1 + m2;

            if (!par.test(Num3)) {
                Num3 = 0;
            }
            $("#FirstAmount").html((Num3).toMyFixed(2))

            //搞车总消费
            ShopCarAllMoney();
            //月供
            ShopCar();
        }
    });

    //上牌费用 
    $("#CarRegFee,#MotorVehicleCommercial,#CarLoanRate").unbind("blur").blur(function () {
        ShopCarAllMoney();
        ShopCar();
    });

    //理财方案
    $("#ReturnOnInvestment,#DisposableInput,#MonthlyInvestment,#RegularYear").unbind("blur").blur(function () {

        var ReturnOnInvestment = $.trim($("#ReturnOnInvestment").val()) * 1 / 100;
        var DisposableInput = $.trim($("#DisposableInput").val()) * 1;
        var MonthlyInvestment = $.trim($("#MonthlyInvestment").val()) * 1;
        var RegularYear = $.trim($("#RegularYear").val()) * 1;

        var Num = ReturnOnInvestment + DisposableInput + MonthlyInvestment + RegularYear;

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
                    rate: ReturnOnInvestment / 12,
                    nper: RegularYear * 12,
                    pmt: -MonthlyInvestment,
                    pv: -DisposableInput,
                    type: 0
                },
                success: function (data) {
                    Num = data;
                }
            });
        }
        $("#TargetAmount").html((Num).toMyFixed(2));
    });

    //绑定一个 车船使用税 事件
    $("#VehicleAndVesselTax").unbind("blur").blur(function () {
        ShopCarAllMoney();
    });



    //客户信息是否保存
    IsProposalSave();
});

//获取消费规划相关信息
function GetConsumptionPlan() {
    var ProposalId = $.getUrlParam("ProposalId");
    _ajaxhepler({
        url: "/CompetitionUser/ConsumptionPlan/GetConsumptionPlanObj",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: ProposalId,
            rId: Math.random(),
        },
        success: function (datas) {
            //教育规划信息
            var li = datas.list;
            if (li != null) {
                var n = li;
                var number = 0;
                //隐藏域Id
                $("#Id").val(n.Id);
                //购房
                if (n.ShopHouseYear != 0) {
                    number = number + 1;
                    $("#ShopHouseYear").val(n.ShopHouseYear);//年限
                    $("#HouseArea").val(n.HouseArea);//面积
                    $("#HousePrice").val(n.HousePrice);//单价
                    $("#HouseDownPaymentPercent").val(n.HouseDownPaymentPercent);//首付比例
                    $("#HouseLoanYear").val(n.HouseLoanYear);//贷款年限
                    $("#HouseLoanRate").val(n.HouseLoanRate);//贷款利率
                    $("#HouseDownPayment").val(n.HouseDownPayment);//首付款
                    $("#HouseTotalAmount").val(n.HouseTotalAmount);//购房总花费
                    $("#HouseMonthlyAmount").val(n.HouseMonthlyAmount);//购房月供

                    var HouseArea = $.trim($("#HouseArea").val()) * 1;
                    var HousePrice = $.trim($("#HousePrice").val()) * 1;
                    var Num2 = HouseArea * HousePrice;
                    //总金额
                    //   $("#HouseAllMoney").val((Num2).toMyFixed(2));
                    $("#HouseAllMoney").val(n.HouseAllMoney);
                    if (n.HouseDownPaymentPercent == 30) {
                        $("#checked1").attr("checked", true);
                        $("#HouseDownPaymentPercent").attr("disabled", true);
                    } else if (n.HouseDownPaymentPercent == 40) {
                        $("#checked2").attr("checked", true);
                        $("#HouseDownPaymentPercent").attr("disabled");
                    } else if (n.HouseDownPaymentPercent == 50) {
                        $("#checked3").attr("checked", true);
                        $("#HouseDownPaymentPercent").attr("disabled", true);
                    } else {
                        $("#checked4").attr("checked", true);
                        $("#HouseDownPaymentPercent").attr("disabled", false).removeClass("b-gray");
                    }

                    $("#TitleShopHouse").show(0);
                    $("#ShopHouseDiv").show(0);
                    //  $("#ShopHouse").hide(0);
                } else {
                    $("#TitleShopHouse").hide(0);
                    $("#ShopHouseDiv").hide(0);
                    $("#Add").show(0);
                }

                //购车
                if (n.ShopCarYear != 0) {
                    number = number + 1;
                    $("#ShopCarYear").val(n.ShopCarYear);//年限
                    $("#CarType").val(n.CarType);//车款型号
                    $("#CarPrice").val(n.CarPrice);//裸车价格
                    $("#CarDownPaymentPercent").val(n.CarDownPaymentPercent);//首付比例
                    if (n.CarDownPaymentPercent == 30) {
                        $("#checked5").attr("checked", true);
                        $("#CarDownPaymentPercent").attr("disabled", true);
                    } else if (n.CarDownPaymentPercent == 40) {
                        $("#checked6").attr("checked", true);
                        $("#CarDownPaymentPercent").attr("disabled", true);
                    } else if (n.CarDownPaymentPercent == 50) {
                        $("#checked7").attr("checked", true);
                        $("#CarDownPaymentPercent").attr("disabled", true);
                    } else {
                        $("#checked8").attr("checked", true);
                        $("#CarDownPaymentPercent").attr("disabled", false).removeClass("b-gray");
                    }
                    //$("#CarLoanYear").val(n.CarLoanYear);//贷款期限
                    //if (n.CarLoanYear == 12) {
                    //    $("#checked9").attr("checked", true);
                    //    $("#CarLoanYear").attr("disabled", true);
                    //} else if (n.CarLoanYear == 24) {
                    //    $("#checked10").attr("checked", true);
                    //    $("#CarLoanYear").attr("disabled", true);
                    //} else if (n.CarLoanYear == 36) {
                    //    $("#checked11").attr("checked", true);
                    //    $("#CarLoanYear").attr("disabled", true);
                    //} else if (n.CarLoanYear == 48) {
                    //    $("#checked12").attr("checked", true);
                    //    $("#CarLoanYear").attr("disabled", true);
                    //}
                    //else {
                    //    $("#checked13").attr("checked", true);
                    //    $("#CarDownPaymentPercent").attr("disabled", false);
                    //}
                    $("#CarLoanYear").val(n.CarLoanYear);//贷款期限
                    if (n.CarLoanYear == 1) {
                        $("#checked9").attr("checked", true);
                        $("#CarLoanYear").attr("disabled", true);
                    } else if (n.CarLoanYear == 2) {
                        $("#checked10").attr("checked", true);
                        $("#CarLoanYear").attr("disabled", true);
                    } else if (n.CarLoanYear == 3) {
                        $("#checked11").attr("checked", true);
                        $("#CarLoanYear").attr("disabled", true);
                    } else if (n.CarLoanYear == 4) {
                        $("#checked12").attr("checked", true);
                        $("#CarLoanYear").attr("disabled", true);
                    }
                    else {
                        $("#checked13").attr("checked", true);
                        $("#CarDownPaymentPercent").attr("disabled", false);
                    }
                    $("#CarLoanRate").val(n.CarLoanRate);//贷款利率
                    $("#PurchaseTax").val(n.PurchaseTax);//购置税
                    $("#CarRegFee").val(n.CarRegFee);//上牌费用
                    // $("#Displacement").val(n.Displacement);//汽车排量
                    $("#VehicleAndVesselTax").val(n.VehicleAndVesselTax);//车船使用税
                    $("#Selcts").val(n.VehicleAndVesselTax);
                    $("#MotorVehicleCompulsory").val(n.MotorVehicleCompulsory);//交强险
                    $("#Selects2").val(n.MotorVehicleCompulsory);
                    $("#MotorVehicleCommercial").val(n.MotorVehicleCommercial);//商业保险
                    $("#CarDownPayment").val(n.CarDownPayment);//首付款
                    $("#CarTotalAmount").val(n.CarTotalAmount);//购车总花费
                    $("#CarMonthlyAmount").val(n.CarMonthlyAmount);//购车月供

                    $("#TitleShopCar").show();
                    $("#ShopCarDiv").show();
                    //  $("#ShopCar").hide();
                } else {
                    $("#TitleShopCar").hide();
                    $("#ShopCarDiv").hide();
                    $("#Add").show();
                }

                $("#Analysis").val(n.Analysis);//消费规划分析

                $("#FirstAmount").html(n.FirstAmount);//首付款准备的现金总金额
                $("#ReturnOnInvestment").val(n.ReturnOnInvestment);//预计投资收益率
                $("#DisposableInput").val(n.DisposableInput);//一次性投入金额
                $("#MonthlyInvestment").val(n.MonthlyInvestment);//每月定期投资金额
                $("#RegularYear").val(n.RegularYear);//定期定额投资年限
                $("#TargetAmount").html(n.TargetAmount);//此方案能实现的目标金额

                //隐藏域Id
                $("#Id").val(n.Id);

            } else {
                $("#TitleShopCar").hide();
                $("#ShopCarDiv").hide();
            }
            if (number == 2) {
                $("#Add").hide();
            }


            //把值赋给defaultVal作为原值
            SaveDefaultValueCommon("ConsumptionPlan");
        }
    });
}

//添加消费规划
function AddConsumptionPlan(valu) {
    var bo = false;
    if ($("#ShopHouseDiv").css("display") != "none") {
        //AddTextHouse();
        if (!VerificationHelper.checkFrom("ShopHouseDiv")) {
            bo = true;

        }
    }
    if ($("#ShopCarDiv").css("display") != "none") {
        //AddTextCar();
        if (!VerificationHelper.checkFrom("ShopCarDiv")) {
            bo = true;

        }
    }




    if (!VerificationHelper.checkFrom("licai")) {
        bo = true;

    }
    if (!VerificationHelper.checkFrom("xiaofei")) {
        bo = true;

    }
    var fag = false;
    if (bo == false) {
        var ProposalId = $.getUrlParam("ProposalId");

        if (ProposalId != 0) {

            var obj = new Object();
            obj["Id"] = $("#Id").val();//Id
            obj["ProposalId"] = ProposalId;//建议书Id

            if ($("#ShopHouseDiv").css("display") != "none") {
                obj["ShopHouseYear"] = $("#ShopHouseYear").val();//年限
                obj["HouseArea"] = $("#HouseArea").val();//面积
                obj["HousePrice"] = $("#HousePrice").val();//单价            
                obj["HouseAllMoney"] = $("#HouseAllMoney").val();//总金额
                obj["HouseDownPaymentPercent"] = $("#HouseDownPaymentPercent").val();//首付比例
                obj["HouseLoanYear"] = $("#HouseLoanYear").val();//贷款年限
                obj["HouseLoanRate"] = $("#HouseLoanRate").val();//贷款利率
                obj["HouseDownPayment"] = $("#HouseDownPayment").val();//首付款
                obj["HouseTotalAmount"] = $("#HouseTotalAmount").val();//购房总花费
                obj["HouseMonthlyAmount"] = $("#HouseMonthlyAmount").val();//购房月供
            }

            if ($("#ShopCarDiv").css("display") != "none") {
                obj["ShopCarYear"] = $("#ShopCarYear").val();//车款型号
                obj["CarType"] = $("#CarType").val();//车款型号
                obj["CarPrice"] = $("#CarPrice").val();//裸车价格
                obj["CarDownPaymentPercent"] = $("#CarDownPaymentPercent").val();//首付比例
                obj["CarLoanYear"] = $("#CarLoanYear").val();//贷款期限
                obj["CarLoanRate"] = $("#CarLoanRate").val();//贷款利率
                obj["PurchaseTax"] = $("#PurchaseTax").val();//购置税
                obj["CarRegFee"] = $("#CarRegFee").val();//上牌费用
                obj["VehicleAndVesselTax"] = $("#VehicleAndVesselTax").val();//车船使用税
                obj["MotorVehicleCompulsory"] = $("#MotorVehicleCompulsory").val();//交强险
                obj["MotorVehicleCommercial"] = $("#MotorVehicleCommercial").val();//商业保险
                obj["CarDownPayment"] = $("#CarDownPayment").val();//首付款
                obj["CarTotalAmount"] = $("#CarTotalAmount").val();//购车总花费
                obj["CarMonthlyAmount"] = $("#CarMonthlyAmount").val();//购车月供
            }

            obj["FirstAmount"] = $("#FirstAmount").html();//首付款准备的现金总金额
            obj["ReturnOnInvestment"] = $("#ReturnOnInvestment").val();//预计投资收益率
            obj["DisposableInput"] = $("#DisposableInput").val();//一次性投入金额
            obj["MonthlyInvestment"] = $("#MonthlyInvestment").val();//每月定期投资金额
            obj["RegularYear"] = $("#RegularYear").val();//定期定额投资年限
            obj["TargetAmount"] = $("#TargetAmount").html();//此方案能实现的目标金额


            obj["Analysis"] = $("#Analysis").val();//消费规划分析

            _ajaxhepler({
                url: "/CompetitionUser/ConsumptionPlan/AddConsumptionPlan",
                type: "POST",
                async: false,
                dataType: "json",
                data: JSON.stringify(obj),
                contentType: "application/json",
                success: function (data) {
                    if (valu == 0) {

                        dialogHelper.Success({
                            content: "保存成功！", success: function () {

                                //刷新当前页
                                location.href = location.href;
                                //把值赋给defaultVal作为原值
                                SaveDefaultValueCommon("ConsumptionPlan");
                            }
                        });

                    } else if (valu == 1) {
                        window.location.href = "/CompetitionUser/StartAnUndertakingPlan/Index" + URL;
                    } else if (valu == 2) {
                        window.location.href = "/CompetitionUser/LifeEducationPlan/Index" + URL;
                    }
                }
            });
        } else {
            dialogHelper.Error({ content: "请先添加客户信息！", success: function () { } });
            fag = true;
        }
    }
    return fag;
}

//购房单选按钮单击事件
function radioClick(valu) {

    var a = $(valu).val();
    $("#HouseDownPaymentPercent").val(a);

    if (a == 0) {
        $("#HouseDownPaymentPercent").val("").removeClass("b-gray");
        $("#HouseDownPaymentPercent").attr("disabled", false);
    }
    else {
        $("#HouseDownPaymentPercent").attr("disabled", true).val(a).addClass("b-gray");
    }

    var HouseAllMoney = $.trim($("#HouseAllMoney").val()) * 1;
    var HouseDownPaymentPercent = $.trim($("#HouseDownPaymentPercent").val()) * 1;

    var Num = HouseAllMoney * HouseDownPaymentPercent / 100;
    var par = /^\d+(\.\d+)?$/;
    if (!par.test(Num)) {
        Num = 0;
    }
    //首付款
    $("#HouseDownPayment").val((Num).toMyFixed(2));

    //购房月供
    ShopHouse();

    //购房总花费
    ShopHouseAllMoney();

    //需准备首付款总金额
    var m1 = $.trim($("#CarDownPayment").val()) * 1;
    var m2 = $.trim($("#HouseDownPayment").val()) * 1;
    var Num3 = m1 + m2;
    var par = /^\d+(\.\d+)?$/;
    if (!par.test(Num3)) {
        Num3 = 0;
    }

    $("#FirstAmount").html((Num3).toMyFixed(2))


}

//购车单选按钮单击事件
function ShopCarRadioClick(valu) {
    var a = $(valu).val();
    if (a == 0) {
        $("#CarDownPaymentPercent").attr("disabled", false).val("").removeClass("b-gray");
    }
    else { $("#CarDownPaymentPercent").attr("disabled", true).val(a).addClass("b-gray"); }

    //  $("#CarDownPaymentPercent").val(a);

    var HouseAllMoney = $.trim($("#CarPrice").val()) * 1;
    var HouseDownPaymentPercent = $.trim($("#CarDownPaymentPercent").val()) * 1;

    var Num = HouseAllMoney * HouseDownPaymentPercent / 100;
    var par = /^\d+(\.\d+)?$/;
    if (!par.test(Num)) {
        Num = 0;
    }
    //购车首付款
    $("#CarDownPayment").val((Num).toMyFixed(2));

    ShopCarAllMoney();

    //需准备首付款总金额
    var m1 = $.trim($("#CarDownPayment").val()) * 1;
    var m2 = $.trim($("#HouseDownPayment").val()) * 1;
    var Num3 = m1 + m2;
    var par = /^\d+(\.\d+)?$/;
    if (!par.test(Num3)) {
        Num3 = 0;
    }
    $("#FirstAmount").html((Num3).toMyFixed(2))
}

//贷款期限单击事件
function CarLoanYearRadioClick(valu) {
    $("#HouseDownPaymentPercent").attr("disabled", true);


    var a = $(valu).val();
    $("#CarLoanYear").val(a);
    // $("#CarLoanYear").val(a*12);

    ShopCar();//购车月供

    ShopCarAllMoney();
}

var allmoney = 0;
//购房月供
function ShopHouse() {
    var HouseLoanYear = $.trim($("#HouseLoanYear").val()) * 12;//贷款年限    
    var HouseLoanRate = $.trim($("#HouseLoanRate").val()) * 1;//贷款利率       
    var HouseAllMoney = $.trim($("#HouseAllMoney").val()) * 1; //总金额
    var HouseDownPayment = $.trim($("#HouseDownPayment").val()) * 1;//首付款

    var Num = HouseLoanYear + HouseLoanRate / 100 + HouseAllMoney + HouseDownPayment;
    var par = /^\d+(\.\d+)?$/;
    if (!par.test(Num)) {
        Num = 0;
    }

    if (Num != 0 && HouseLoanYear != 0) {
        //PMT(贷款利率/12，贷款年限×12，总金额-首付款,0,0)  （输出）
        _ajaxhepler({
            url: "/CompetitionUser/ConsumptionPlan/RetailLump",
            type: "POST",
            async: false,
            dataType: "json",
            data: {
                rate: HouseLoanRate / 100 / 12,
                nper: HouseLoanYear,
                pmt: HouseAllMoney - HouseDownPayment,
                pv: 0,
                type: 0
            },
            success: function (data) {
                Num = data;
                allmoney = data;
            }
        });
    } else {
        Num = 0;
    }
    //月供
    $("#HouseMonthlyAmount").val((-Num).toMyFixed(2));
}

//购车月供
function ShopCar() {
    //月供=PMT(贷款利率/12，贷款期限，裸车价格-首付款,0,0)

    var CarLoanRate = $.trim($("#CarLoanRate").val()) * 1;//贷款利率    
    //  var CarLoanYear = $.trim($("#CarLoanYear").val()) * 1;//贷款期限
    var CarLoanYear = $.trim($("#CarLoanYear").val()) * 12;//贷款期限  
    var CarPrice = $.trim($("#CarPrice").val()) * 1; //裸车价格
    var CarDownPayment = $.trim($("#CarDownPayment").val()) * 1;//首付款

    var Num = CarLoanRate + CarLoanYear + CarPrice + CarDownPayment;
    var par = /^\d+(\.\d+)?$/;
    if (!par.test(Num)) {
        Num = 0;
    }

    if (Num != 0 && CarLoanYear != 0) {
        //PMT(贷款利率/12，贷款年限，裸车价格-首付款,0,0)  （输出）
        _ajaxhepler({
            url: "/CompetitionUser/ConsumptionPlan/RetailLump",
            type: "POST",
            async: false,
            dataType: "json",
            data: {
                rate: CarLoanRate / 12 / 100,
                nper: CarLoanYear,
                pmt: CarPrice - CarDownPayment,
                pv: 0,
                type: 0
            },
            success: function (data) {
                Num = data;
            }
        });
    } else {
        Num = 0;
    }
    //月供
    $("#CarMonthlyAmount").val((-Num).toMyFixed(2));
}

//购车总消费
function ShopCarAllMoney() {
    //	购车总花费=∑（首付款，月供*贷款期限，购置税，上牌费用，车船使用税，交强险，商业保险）
    var CarDownPayment = $.trim($("#CarDownPayment").val()) * 1;//购车首付款

    var CarMonthlyAmount = $.trim($("#CarMonthlyAmount").val());
    //  var CarLoanYear = $.trim($("#CarLoanYear").val()) * 1;
    var CarLoanYear = $.trim($("#CarLoanYear").val()) * 12;
    var PurchaseTax = $.trim($("#PurchaseTax").val()) * 1;
    var CarRegFee = $.trim($("#CarRegFee").val()) * 1;
    var VehicleAndVesselTax = $.trim($("#VehicleAndVesselTax").val()) * 1;
    var MotorVehicleCompulsory = $.trim($("#MotorVehicleCompulsory").val()) * 1;
    var MotorVehicleCommercial = $.trim($("#MotorVehicleCommercial").val()) * 1;

    var a = -CarMonthlyAmount;

    var Num = CarDownPayment + CarMonthlyAmount * CarLoanYear + PurchaseTax + CarRegFee + VehicleAndVesselTax + MotorVehicleCompulsory + MotorVehicleCommercial;

    if (!par.test(Num)) {
        Num = 0;
    }

    $("#CarTotalAmount").val((Num).toMyFixed(2));
}

//清空文本
function ClearText() {
    if ($("#ShopCarDiv").css("display") == "none") {

        $("#TitleShopCar").show();
        $("#ShopCarYear").val(null);//车款型号
        $("#CarType").val(null);//车款型号
        $("#CarPrice").val(null);//裸车价格
        //$("#CarDownPaymentPercent").val(null);//首付比例
        //$("#CarLoanYear").val(null);//贷款期限
        $("#CarLoanRate").val(null);//贷款利率
        $("#PurchaseTax").val(null);//购置税
        $("#CarRegFee").val(null);//上牌费用
        //$("#VehicleAndVesselTax").val(null);//车船使用税
        //$("#MotorVehicleCompulsory").val(null);//交强险
        $("#MotorVehicleCommercial").val(null);//商业保险
        $("#CarDownPayment").val(null);//首付款
        $("#CarTotalAmount").val(null);//购车总花费
        $("#CarMonthlyAmount").val(null);//购车月供
    }
    if ($("#ShopHouseDiv").css("display") == "none") {
        $("#TitleShopHouse").show();
        $("#ShopHouseYear").val(null);//年限
        $("#HouseArea").val(null);//面积
        $("#HousePrice").val(null);//单价            
        $("#HouseAllMoney").val(null);//总金额
        //$("#HouseDownPaymentPercent").val(null);//首付比例
        $("#HouseLoanYear").val(null);//贷款年限
        $("#HouseLoanRate").val(null);//贷款利率
        $("#HouseDownPayment").val(null);//首付款
        $("#HouseTotalAmount").val(null);//购房总花费
        $("#HouseMonthlyAmount").val(null);//购房月供
    }
}

//购房总花费
function ShopHouseAllMoney() {
    var HouseLoanYear = $.trim($("#HouseLoanYear").val()) * 1;
    var HouseMonthlyAmount = $.trim($("#HouseMonthlyAmount").val()) * 1;
    var HouseMonthlyAmount1 = -allmoney;
    var Num = HouseLoanYear * (-HouseMonthlyAmount1) * 12 - HouseMonthlyAmount;
    if (!par.test(Num)) {
        Num = 0;
    }
    //购房总花费
    $("#HouseTotalAmount").val((-Num).toMyFixed(2));
}
