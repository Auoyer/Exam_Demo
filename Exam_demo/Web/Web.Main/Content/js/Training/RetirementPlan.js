//****************************
//退休规划--------------
//****************************
var param = "";
var TagNavi = true;
//判断是否非法字符
function CheckNum(num) {
    var pattern6 = /^[-]?\d+(\.\d{1,11})?$/;//只能输入两位小数
    if (!pattern6.test(num)) {
        num = 0;
        return num;
    }
    //num = (num.toMyFixed(2)) * 1;
    return num;
}


//满意生活水平=目前生活水平/生活满意度
function claCsatisfactionLive(LivingStandardNow, Satisfaction) {
    var LivingStandardNow1 = CheckNum(LivingStandardNow);
    var Satisfaction1 = CheckNum(Satisfaction);

    var sum = 0;
    if (LivingStandardNow1 == LivingStandardNow && Satisfaction1 == Satisfaction) {
        sum = (LivingStandardNow ) / (Satisfaction/100);
    } else {
        sum = 0;
    }
    return sum;
}
//满意生活水平实体
function claCsatisfactionLive2() {
    var LivingStandardNow =$.trim($("#LivingStandardNow").val())*1;
    var Satisfaction =$.trim($("#Satisfaction").val())*1;
    var result = claCsatisfactionLive(LivingStandardNow, Satisfaction)*1;
    $("#SatisfactionLivingStandard").val(result.toMyFixed(2));
}

//生活满意度下拉选择
var ClacSatisfaction = {
    calcSatisfaction: function (multiple, LivingStandardNow) {
        //var multiple = $("RetainCashMultiple").val();
        var result = (multiple / LivingStandardNow).toMyFixed(2);
        return result;
    }
};
//退休后、退休前生活水平折算比例下拉选择
var clacCashPanVal = {
    calcConvertProportion: function (multiple, LivingStandardNow) {
        //var multiple = $("RetainCashMultiple").val();
        var result = (multiple * LivingStandardNow).toMyFixed(2);
        return result;
    }
}

//退休时生活水平=FV(退休前通货膨胀率,计划退休年龄-当前年龄，0，- 满意生活水平,1)
function calcRetirementLivingStandard(BeforeInflationRate, RetirementAge, Age, SatisfactionLivingStandard) {
    var BeforeInflationRate1 = CheckNum(BeforeInflationRate);
    var RetirementAge1 = CheckNum(RetirementAge);
    var Age1 = CheckNum(Age);
    var SatisfactionLivingStandard1 = CheckNum(SatisfactionLivingStandard);
    if (BeforeInflationRate1 == BeforeInflationRate && RetirementAge1 == RetirementAge && Age1 == Age && SatisfactionLivingStandard1 == SatisfactionLivingStandard) {
        var rate=BeforeInflationRate1;
        var nper=RetirementAge1-Age1;
        var amount=0;
        var pv =-SatisfactionLivingStandard1;
        var begOfPeriodType=1;
        sum = CalcFVCommon(rate, nper, amount, pv, begOfPeriodType)
    } else {
        sum = 0;
    }
    return sum;
};
//退休时生活水平实体
function calcRetirementLivingStandard2() {
    var BeforeInflationRate = $.trim($("#BeforeInflationRate").val())*1;
    var RetirementAge =$.trim(($("#RetirementAge").val()))*1;
    var Age = $.trim($("#Age").val())*1;
    var SatisfactionLivingStandard =$.trim($("#SatisfactionLivingStandard").val())*1;
    var result = calcRetirementLivingStandard(BeforeInflationRate, RetirementAge, Age, SatisfactionLivingStandard) * 1;
    $("#RetirementLivingStandard").val(result.toMyFixed(2));
};

//退休后生活水平=退休时生活水平×退休后、退休前生活水平折算比例
function calcAfterLivingStandard(RetirementLivingStandard, ConvertProportion) {
    var RetirementLivingStandard1 = CheckNum(RetirementLivingStandard);
    var ConvertProportion1 = CheckNum(ConvertProportion);
    var sum = 0;
    if (RetirementLivingStandard1 == RetirementLivingStandard && ConvertProportion1 == ConvertProportion) {
        sum = (RetirementLivingStandard * 1) * (ConvertProportion / 100);
    } else {
        sum = 0;
    }
    return sum;
}
//退休后生活水平实体=退休时生活水平×退休后、退休前生活水平折算比例
function calcAfterLivingStandard2() {
    var RetirementLivingStandard = $.trim($("#RetirementLivingStandard").val())*1;
    var ConvertProportion = $.trim($("#ConvertProportion").val());
    var result =calcAfterLivingStandard(RetirementLivingStandard, ConvertProportion)*1;
    $("#AfterLivingStandard").val(result.toMyFixed(2));
   
}

//小计=∑（社会保险，商业保险，租金收入，其他收入） 
function calcSum(SocialInsurance, CommercialInsurance, RentIncome, OtherIncome) {
    var SocialInsurance1 = CheckNum(SocialInsurance);
    var CommercialInsurance1 = CheckNum(CommercialInsurance);
    var RentIncome1 = CheckNum(RentIncome);
    var OtherIncome1 = CheckNum(OtherIncome);

    var sum = 0;
    if (SocialInsurance1 == SocialInsurance && CommercialInsurance1 == CommercialInsurance && RentIncome1 == RentIncome && OtherIncome1 == OtherIncome) {
        sum = (SocialInsurance * 1) + (CommercialInsurance * 1) + (RentIncome * 1) + (OtherIncome * 1);
    } else {
        sum = 0;
    }
    return sum;
}
//小计实体=∑（社会保险，商业保险，租金收入，其他收入） 
function calcSum2(){
    var SocialInsurance =$.trim($("#SocialInsurance").val())*1;
    var CommercialInsurance=$.trim($("#CommercialInsurance").val())*1;
    var RentIncome =$.trim($("#RentIncome").val())*1;
    var OtherIncome=$.trim($("#OtherIncome").val())*1;
    var result = calcSum(SocialInsurance, CommercialInsurance, RentIncome, OtherIncome)*1;
    $("#TotalIncome").val(result.toMyFixed(2));
}

//退休时需准备的现金总金额 TotalAmount PV((退休后投资收益率-退休后通货膨胀率)/(1+退休后通货膨胀率)/12,希望享有退休生活年限×12,小计-退休后生活水平,- 子女传承费用,1)
function calcTotalAmount(RetirementRate, AfterInflationRate, RetirementYears, TotalIncome, AfterLivingStandard, lineageFee) {
    var RetirementRate1 = CheckNum(RetirementRate);
    var AfterInflationRate1 = CheckNum(AfterInflationRate);
    var RetirementYears1 = CheckNum(RetirementYears);
    var TotalIncome1 = CheckNum(TotalIncome);
    var AfterLivingStandard1 = CheckNum(AfterLivingStandard);
    var lineageFee1 = CheckNum(lineageFee);
    var sum = 0;
    if (RetirementRate1 == RetirementRate && AfterInflationRate1 == AfterInflationRate && RetirementYears1 == RetirementYears && TotalIncome1 == TotalIncome && AfterLivingStandard1 == AfterLivingStandard && lineageFee1 == lineageFee) {
        var rate = ((RetirementRate1/100 - AfterInflationRate1/100) / (1 + AfterInflationRate1/100))/12;
        var nper = RetirementYears * 12;
        var pmt = TotalIncome - AfterLivingStandard;
        var fv = -lineageFee;
        var begOfPeriodType = 1;
        sum = CalcPVCommon(rate*100, nper, pmt, fv, begOfPeriodType);
    } else {
        sum = 0;
    }
    return sum;
}
//退休时需准备的现金总金额
function calcTotalAmount2() {
    var RetirementRate =$.trim($("#RetirementRate").val())*1;
    var AfterInflationRate =$.trim($("#AfterInflationRate").val())*1;
    var RetirementYears = $.trim($("#RetirementYears").val())*1;
    var TotalIncome = $.trim($("#TotalIncome").val())*1;
    var AfterLivingStandard = $.trim($("#AfterLivingStandard").val())*1;
    var lineageFee = $.trim($("#lineageFee").val())*1;
    var result = calcTotalAmount(RetirementRate, AfterInflationRate, RetirementYears, TotalIncome, AfterLivingStandard, lineageFee)*1;
    $("#TotalAmount").html(result.toMyFixed(2));
}
//定期定额投资年限：学生输入  （小于等于（计划退休年龄-当前年龄））RegularYear
//	此方案能实现的目标金额 TargetAmount=FV（投资收益率÷12，定期定额投资年限×12，- 每月定期定额投资金额，- 一次性投资金额,0） 
function calcTargetAmount(ReturnOnInvestmentRate, RegularYear, MonthlyInvestment, DisposableInput) {
    var ReturnOnInvestmentRate1 = CheckNum(ReturnOnInvestmentRate);
    var RegularYear1 = CheckNum(RegularYear);
    var MonthlyInvestment1 = CheckNum(MonthlyInvestment);
    var DisposableInput1 = CheckNum(DisposableInput);
    var sum = 0;
    if (ReturnOnInvestmentRate1 == ReturnOnInvestmentRate && RegularYear1 == RegularYear && MonthlyInvestment1 == MonthlyInvestment && DisposableInput1 == DisposableInput) {
        var rate = ReturnOnInvestmentRate1/12;
        var nper = RegularYear * 12;
        var pmt = -MonthlyInvestment;
        var fv = -DisposableInput;
        var begOfPeriodType = 0;
        sum = CalcFVCommon(rate, nper, pmt, fv, begOfPeriodType);
    } else {
        sum = 0;
    }
    return sum;
};
//此方案能实现的目标金额 TargetAmount2
function calcTargetAmount2() {
    var ReturnOnInvestmentRate = $.trim($("#ReturnOnInvestmentRate").val())*1;
    var RegularYear = $.trim($("#RegularYear").val())*1;
    var MonthlyInvestment =$.trim($("#MonthlyInvestment").val())*1;
    var DisposableInput = $.trim($("#DisposableInput").val())*1;
    var result = calcTargetAmount(ReturnOnInvestmentRate, RegularYear, MonthlyInvestment, DisposableInput)*1;
    $("#TargetAmount").html(result.toMyFixed(2));
};

//定义及时方法blur
$(function () {
    IsProposalSave()//客户验证

    //目前生活水平 
    $("#LivingStandardNow").unbind("blur").blur(function () {
        //满意生活水平
        claCsatisfactionLive2();
       //退休时生活水平
        calcRetirementLivingStandard2();
        //退休后生活水平
        calcAfterLivingStandard2();
        //退休时准备的现金总额
        calcTotalAmount2();
    });
    //退休前通货膨胀率*计划退休年龄
    $("#BeforeInflationRate,#RetirementAge").unbind("blur").blur(function () {
        //退休时生活水平
        calcRetirementLivingStandard2();
        //退休后生活水平
        calcAfterLivingStandard2();
        //退休时准备的现金总额
        calcTotalAmount2();
    });

    //*社会保险*商业保险*租金收入*其他收入 =小计
    $("#SocialInsurance,#CommercialInsurance,#RentIncome,#OtherIncome").unbind("blur").blur(function () {
        //小计
        calcSum2();
        //退休时准备的现金总额
        calcTotalAmount2();
    });

    //退休后投资收益率：*退休后通货膨胀率
    $("#RetirementRate,#AfterInflationRate,#RetirementYears,#lineageFee").unbind("blur").blur(function () {
        //退休时准备的现金总额
        calcTotalAmount2();
    })

    $("#ReturnOnInvestmentRate,#RegularYear,#MonthlyInvestment,#DisposableInput").unbind("blur").blur(function () {
        calcTargetAmount2()
    })

    //生活满意度
    $("#LiveRetirementPlanDiv #Satisfaction").unbind("change").change(function () {
        var multiple = $(this).val();
        //满意生活水平
        var LivingStandardNow = $("#LivingStandardNow").val() * 1;
        var result = claCsatisfactionLive(LivingStandardNow, multiple)*1;
        $("#LiveRetirementPlanDiv #SatisfactionLivingStandard").val(result.toMyFixed(2));
        //退休时生活水平
        calcRetirementLivingStandard2();
        //退休后生活水平
        calcAfterLivingStandard2();
        //退休时准备的现金总额
        calcTotalAmount2();


    });
    // 退休后、退休前生活水平折算比例
    $("#LiveRetirementPlanDiv #ConvertProportion").unbind("change").change(function () {
        var multiple = $(this).val();
        //退休后生活水平
        var SatisfactionLivingStandard = $("#RetirementLivingStandard").val() * 1;
        var result = calcAfterLivingStandard(SatisfactionLivingStandard,multiple)*1;
        $("#LiveRetirementPlanDiv #AfterLivingStandard").val(result.toMyFixed(2));
        //退休时准备的现金总额
        calcTotalAmount2();

    });

});



//保存数据
function SaveRetirementPlan(saveFalg) {
    //跳转标记
    TagNavi = true;

    var Id = $.trim($("#RetirementPlanId").val());
    var Age =$.trim($("#Age").val());
    var BeforeInflationRate =$.trim( $("#BeforeInflationRate").val());
    var AfterInflationRate =$.trim( $("#AfterInflationRate").val());
    var RetirementRate = $.trim($("#RetirementRate").val());
    var SociaSecurityRate =$.trim( $("#SociaSecurityRate").val());
    var RentRate = $.trim($("#RentRate").val());
    var OtherRate =$.trim( $("#OtherRate").val());
    var RetirementAge =$.trim( $("#RetirementAge").val());
    var RetirementYears =$.trim( $("#RetirementYears").val());
    var LivingStandardNow =$.trim( $("#LivingStandardNow").val());
    var SocialInsurance =$.trim( $("#SocialInsurance").val());
    var Satisfaction = $.trim($("#Satisfaction").val());
    var SatisfactionLivingStandard =$.trim( $("#SatisfactionLivingStandard").val());
    var ConvertProportion = $.trim($("#ConvertProportion").val());
    var lineageFee =$.trim( $("#lineageFee").val());
    var CommercialInsurance = $.trim($("#CommercialInsurance").val());
    var RentIncome =$.trim( $("#RentIncome").val());
    var RetirementLivingStandard = $.trim($("#RetirementLivingStandard").val());
    var AfterLivingStandard =$.trim( $("#AfterLivingStandard").val());
    var OtherIncome =$.trim( $("#OtherIncome").val());
    var TotalIncome = $.trim($("#TotalIncome").val());
    var TotalAmount =$.trim( $("#TotalAmount").html());
    var ReturnOnInvestmentRate = $.trim($("#ReturnOnInvestmentRate").val());
    var MonthlyInvestment =$.trim( $("#MonthlyInvestment").val());
    var DisposableInput =$.trim( $("#DisposableInput").val());
    var RegularYear = $.trim($("#RegularYear").val());
    var TargetAmount =$.trim($("#TargetAmount").html());
    var Analysis = $.trim($("#Analysis").val());

    //页面字段检测
    if (!VerificationHelper.checkFrom("LiveRetirementPlanDiv",
        function () {
        //计划退休年龄 验证
        if (RetirementAge*1 < Age*1) {
          showValidateMsg("RetirementAge", "计划退休年龄必须大于等于当前年龄");
    }
    //    //定期定额投资年限 验证
    //    if (RegularYear * 1 > (RetirementAge * 1 - Age * 1)) {
    //        showValidateMsg("RegularYear", "小于等于（计划退休年龄-当前年龄）");
    //}
    })) {
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
    } else {
        return false;
    }

    obj["Id"] = $("#LiveRetirementPlanDiv #RetirementPlanId").val();
  
    obj["Age"] = $("#LiveRetirementPlanDiv #Age").val();
    obj["BeforeInflationRate"] = $("#LiveRetirementPlanDiv #BeforeInflationRate").val();
    obj["AfterInflationRate"] = $("#LiveRetirementPlanDiv #AfterInflationRate").val();
    obj["RetirementRate"] = $("#LiveRetirementPlanDiv #RetirementRate").val();
    obj["SociaSecurityRate"] = $("#LiveRetirementPlanDiv #SociaSecurityRate").val();
    obj["RentRate"] = $("#LiveRetirementPlanDiv #RentRate").val();
    obj["OtherRate"] = $("#LiveRetirementPlanDiv #OtherRate").val();
    obj["RetirementAge"] = $("#LiveRetirementPlanDiv #RetirementAge").val();
    obj["RetirementYears"] = $("#LiveRetirementPlanDiv #RetirementYears").val();
    obj["SocialInsurance"] = $("#LiveRetirementPlanDiv #SocialInsurance").val();
    obj["LivingStandardNow"] = $("#LiveRetirementPlanDiv #LivingStandardNow").val();
    obj["Satisfaction"] = $("#LiveRetirementPlanDiv #Satisfaction").val();
    obj["SatisfactionLivingStandard"] = $("#LiveRetirementPlanDiv #SatisfactionLivingStandard").val();
    obj["ConvertProportion"] = $("#LiveRetirementPlanDiv #ConvertProportion").val();
    obj["lineageFee"] = $("#LiveRetirementPlanDiv #lineageFee").val();
    obj["CommercialInsurance"] = $("#LiveRetirementPlanDiv #CommercialInsurance").val();
    obj["RentIncome"] = $("#LiveRetirementPlanDiv #RentIncome").val();
    obj["RetirementLivingStandard"] = $("#LiveRetirementPlanDiv #RetirementLivingStandard").val();
    obj["AfterLivingStandard"] = $("#LiveRetirementPlanDiv #AfterLivingStandard").val();
    obj["OtherIncome"] = $("#LiveRetirementPlanDiv #OtherIncome").val();
    obj["TotalIncome"] = $("#LiveRetirementPlanDiv #TotalIncome").val();
    obj["TotalAmount"] = $("#LiveRetirementPlanDiv #TotalAmount").html();
    obj["ReturnOnInvestmentRate"] = $("#LiveRetirementPlanDiv #ReturnOnInvestmentRate").val();
    obj["MonthlyInvestment"] = $("#LiveRetirementPlanDiv #MonthlyInvestment").val();
    obj["DisposableInput"] = $("#LiveRetirementPlanDiv #DisposableInput").val();
    obj["RegularYear"] = $("#LiveRetirementPlanDiv #RegularYear").val();
    obj["TargetAmount"] = $("#LiveRetirementPlanDiv #TargetAmount").html();
    obj["Analysis"] = $("#LiveRetirementPlanDiv #Analysis").val();
    //SocialInsurance

    _ajaxhepler({
        url: "/CompetitionUser/RetirementPlan/SaveRetirementPlan",
        type: "POST",
        async: false,
        dataType: "json",
        data: JSON.stringify(obj),
        contentType: "application/json",
        success: function (data) {
            if (data != null) {
                $("#RetirementPlanId").val(data.Id);
                SaveDefaultValueCommon("LiveRetirementPlanDiv");//保存原值。和新值要做一个对比的
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
    });
}

//退休规划加载
function LoadRetirementPlan(ProposalId) {
    _ajaxhepler({
        url: "/CompetitionUser/RetirementPlan/GetRetirementPlanByProposalId",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: ProposalId
        },
        success: function (data) {
            if (data != null) {
                if (data.Analysis != null && data.Analysis != "") {
                    SetLiveRetirementPlanDivVal(data);
                }else{
                    //表示第一次加载
                    SetLiveRetirementPlanDivValStart(data);
                }
                SaveDefaultValueCommon("LiveRetirementPlanDiv");
            }
        }
    });
};
//给退休规划赋值
function SetLiveRetirementPlanDivVal(data) {
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
    //获取URL参数
    var ProposalId = $.getUrlParam("ProposalId");
    $("#LiveRetirementPlanDiv #RetirementPlanId").val(Id);
    $("#LiveRetirementPlanDiv #ProposalId").val(ProposalId);
    $("#LiveRetirementPlanDiv #Age").val(Age);
    $("#LiveRetirementPlanDiv #BeforeInflationRate").val(BeforeInflationRate);
    $("#LiveRetirementPlanDiv #AfterInflationRate").val(AfterInflationRate);
    $("#LiveRetirementPlanDiv #RetirementRate").val(RetirementRate);
    $("#LiveRetirementPlanDiv #SociaSecurityRate").val(SociaSecurityRate);
    $("#LiveRetirementPlanDiv #RentRate").val(RentRate);
    $("#LiveRetirementPlanDiv #OtherRate").val(OtherRate);
    $("#LiveRetirementPlanDiv #RetirementAge").val(RetirementAge);
    $("#LiveRetirementPlanDiv #RetirementYears").val(RetirementYears);
    $("#LiveRetirementPlanDiv #LivingStandardNow").val(LivingStandardNow);
    $("#LiveRetirementPlanDiv #Satisfaction").val(Satisfaction);
    $("#LiveRetirementPlanDiv #SatisfactionLivingStandard").val(SatisfactionLivingStandard);
    $("#LiveRetirementPlanDiv #ConvertProportion").val(ConvertProportion);
    $("#LiveRetirementPlanDiv #lineageFee").val(lineageFee);
    $("#LiveRetirementPlanDiv #SocialInsurance").val(SocialInsurance);
    $("#LiveRetirementPlanDiv #CommercialInsurance").val(CommercialInsurance);
    $("#LiveRetirementPlanDiv #RentIncome").val(RentIncome);
    $("#LiveRetirementPlanDiv #RetirementLivingStandard").val(RetirementLivingStandard);
    $("#LiveRetirementPlanDiv #AfterLivingStandard").val(AfterLivingStandard);
    $("#LiveRetirementPlanDiv #OtherIncome").val(OtherIncome);
    $("#LiveRetirementPlanDiv #TotalIncome").val(TotalIncome);
    $("#LiveRetirementPlanDiv #TotalAmount").html(TotalAmount);
    $("#LiveRetirementPlanDiv #ReturnOnInvestmentRate").val(ReturnOnInvestmentRate);
    $("#LiveRetirementPlanDiv #DisposableInput").val(DisposableInput);
    $("#LiveRetirementPlanDiv #MonthlyInvestment").val(MonthlyInvestment);
    $("#LiveRetirementPlanDiv #RegularYear").val(RegularYear);
    $("#LiveRetirementPlanDiv #TargetAmount").html(TargetAmount);
    $("#LiveRetirementPlanDiv #Analysis").val(Analysis);
    $("#LiveRetirementPlanDiv #monthMoney").val(MonthMoney.toMyFixed(2));
    $("#LiveRetirementPlanDiv #UserableAsset").val(UserableAsset.toMyFixed(2));
}

//初次加载退休规划的时候
//给退休规划赋值
function SetLiveRetirementPlanDivValStart(data) {
    var Id = data.Id;
    var Age = data.Age == 0 ? "" : data.Age;
    var BeforeInflationRate = data.BeforeInflationRate == 0 ? "" : data.BeforeInflationRate;
    var AfterInflationRate = data.AfterInflationRate == 0 ? "" : data.AfterInflationRate;
    var RetirementRate = data.RetirementRate == 0 ? "" : data.RetirementRate;
    var SociaSecurityRate = data.SociaSecurityRate == 0 ? "" : data.SociaSecurityRate;
    var RentRate = data.RentRate == 0 ? "" : data.RentRate;
    var OtherRate = data.OtherRate == 0 ? "" : data.OtherRate;
    var SocialInsurance = data.SocialInsurance == 0 ? "" : data.SocialInsurance;
    var RetirementAge = data.RetirementAge == 0 ? "" : data.RetirementAge;
    var RetirementYears = data.RetirementYears == 0 ? "" : data.RetirementYears;
    var LivingStandardNow = data.LivingStandardNow == 0 ? "" : data.LivingStandardNow;
    var Satisfaction = data.Satisfaction;  //*生活满意度
    var SatisfactionLivingStandard = data.SatisfactionLivingStandard == 0 ? "" : data.AgeSatisfactionLivingStandard
    var ConvertProportion = data.ConvertProportion ;//退休后、退休前生活水平折算比例
    var lineageFee = data.lineageFee == 0 ? "" : data.lineageFee;
    var CommercialInsurance = data.CommercialInsurance == 0 ? "" : data.CommercialInsurance;
    var RentIncome = data.RentIncome == 0 ? "" : data.RentIncome;
    var RetirementLivingStandard = data.RetirementLivingStandard == 0 ? "" : data.RetirementLivingStandard;
    var AfterLivingStandard = data.AfterLivingStandard == 0 ? "" : data.AfterLivingStandard;
    var OtherIncome = data.OtherIncome == 0 ? "" : data.OtherIncome;
    var TotalIncome = data.TotalIncome == 0 ? "" : data.TotalIncome;
    var TotalAmount = data.TotalAmount == 0 ? "" : data.AgeTotalAmount
    var ReturnOnInvestmentRate = data.ReturnOnInvestmentRate == 0 ? "" : data.ReturnOnInvestmentRate;
    var MonthlyInvestment = data.MonthlyInvestment == 0 ? "" : data.MonthlyInvestment;
    var DisposableInput = data.DisposableInput == 0 ? "" : data.DisposableInput;
    var RegularYear = data.RegularYear == 0 ? "" : data.RegularYear;
    var TargetAmount = data.TargetAmount == 0 ? "" : data.TargetAmount;
    var Analysis = data.Analysis;
    //每月可支配资金
    var MonthMoney = data.MonthMoney == 0 ? "" : data.MonthMoney.toMyFixed(2);
    //可用资产
    var UserableAsset = data.UserableAsset == 0 ? "" : data.UserableAsset.toMyFixed(2);
    //获取URL参数
    var ProposalId = $.getUrlParam("ProposalId");
    $("#LiveRetirementPlanDiv #RetirementPlanId").val(Id);
    $("#LiveRetirementPlanDiv #ProposalId").val(ProposalId);
    $("#LiveRetirementPlanDiv #Age").val(Age);
    $("#LiveRetirementPlanDiv #BeforeInflationRate").val(BeforeInflationRate);
    $("#LiveRetirementPlanDiv #AfterInflationRate").val(AfterInflationRate);
    $("#LiveRetirementPlanDiv #RetirementRate").val(RetirementRate);
    $("#LiveRetirementPlanDiv #SociaSecurityRate").val(SociaSecurityRate);
    $("#LiveRetirementPlanDiv #RentRate").val(RentRate);
    $("#LiveRetirementPlanDiv #OtherRate").val(OtherRate);
    $("#LiveRetirementPlanDiv #RetirementAge").val(RetirementAge);
    $("#LiveRetirementPlanDiv #RetirementYears").val(RetirementYears);
    $("#LiveRetirementPlanDiv #LivingStandardNow").val(LivingStandardNow);
    $("#LiveRetirementPlanDiv #Satisfaction").val(Satisfaction);
    $("#LiveRetirementPlanDiv #SatisfactionLivingStandard").val(SatisfactionLivingStandard);
    $("#LiveRetirementPlanDiv #ConvertProportion").val(ConvertProportion);
    $("#LiveRetirementPlanDiv #lineageFee").val(lineageFee);
    $("#LiveRetirementPlanDiv #SocialInsurance").val(SocialInsurance);
    $("#LiveRetirementPlanDiv #CommercialInsurance").val(CommercialInsurance);
    $("#LiveRetirementPlanDiv #RentIncome").val(RentIncome);
    $("#LiveRetirementPlanDiv #RetirementLivingStandard").val(RetirementLivingStandard);
    $("#LiveRetirementPlanDiv #AfterLivingStandard").val(AfterLivingStandard);
    $("#LiveRetirementPlanDiv #OtherIncome").val(OtherIncome);
    $("#LiveRetirementPlanDiv #TotalIncome").val(TotalIncome);
    $("#LiveRetirementPlanDiv #TotalAmount").html(TotalAmount);
    $("#LiveRetirementPlanDiv #ReturnOnInvestmentRate").val(ReturnOnInvestmentRate);
    $("#LiveRetirementPlanDiv #DisposableInput").val(DisposableInput);
    $("#LiveRetirementPlanDiv #MonthlyInvestment").val(MonthlyInvestment);
    $("#LiveRetirementPlanDiv #RegularYear").val(RegularYear);
    $("#LiveRetirementPlanDiv #TargetAmount").html(TargetAmount);
    $("#LiveRetirementPlanDiv #Analysis").val(Analysis);
    $("#LiveRetirementPlanDiv #monthMoney").val(MonthMoney);
    $("#LiveRetirementPlanDiv #UserableAsset").val(UserableAsset);
}

$(function () {
    //先要加载数据
    param = $("#hdParam").val();
    //获取URL参数
    var ProposalId = $.getUrlParam("ProposalId");

    //获取财产传承
    if (ProposalId != null && ProposalId != "" && ProposalId != undefined) {
        LoadRetirementPlan(ProposalId);
    } 
    //绑定保存事件
    $("#LiveRetirementPlanDiv #btnSave").live("click", function () {
        SaveRetirementPlan();
    });

    //同时绑定下一页事件
    $("#LiveRetirementPlanDiv #btnNext").live("click", function () {
        //同时还要保存当前数据
        SaveRetirementPlan(0);
        if (TagNavi) {
            window.location.href = "/CompetitionUser/InsurancePlan/Index" + param;
        }
    });
    //同时绑定上一页事件
    $("#LiveRetirementPlanDiv #btnPrev").live("click", function () {
        //同时还要保存当前数据
        SaveRetirementPlan(0);
        if (TagNavi) {
            window.location.href = "/CompetitionUser/StartAnUndertakingPlan/Index" + param;
        }
    });

});
