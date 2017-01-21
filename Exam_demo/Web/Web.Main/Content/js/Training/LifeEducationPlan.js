var LifeEducationPlanDetail = new arrayHelper("EduStage");
var par = /^[-]*\d+(\.\d+)?$/;
var URL = "";
$(function () {

    

    //AchieveCases(1);
    //获取URL参数
    var ProposalId = $.getUrlParam("ProposalId");
    var TrainExamId = $.getUrlParam("TrainExamId");
    var StuCustomerId = $.getUrlParam("StuCustomerId");
    URL = "?TrainExamId=" + TrainExamId + "&ProposalId=" + ProposalId + "&StuCustomerId=" + StuCustomerId;
    //加载信息
    GetLifeEducationPlan(0);
   
    //加载每月可支配资金 可用资产
    EveryMonthMoney("LifeEducationPlan/GetmoneyList", ProposalId,"LifeEducationPlan");

    //把值赋给defaultVal作为原值
    SaveDefaultValueCommon("LifeEducationPlan");

    $("#LifeEducationPlan #btnSave").live("click", function () {
        //添加数据
        AddLifeEducationPlan(0);
    });
    //同时绑定下一页事件
    $("#LifeEducationPlan #btnNext").live("click", function () {
        AddLifeEducationPlan(1);
    });
   
    //已经准备的教育费用
    $("#Insurance,#Deposit,#Other").unbind("blur").blur(function () {
        EduTotalAmount()
       
    });
    //注册一个下拉框事件
    $("#LifeEducationPlan select[field='EduStage']").unbind("change").change(function () {
        var selVal = $(this).val();
        var divID = $(this).parent().parent().attr("id");
        OnChangeEven(divID);
    })


    //理财方案
    $("#ReturnOnInvestment,#DisposableInput,#MonthlyInvestment,#RegularYear").unbind("blur").blur(function () {

        var ReturnOnInvestment = $.trim($("#ReturnOnInvestment").val()) * 1/100;
        var DisposableInput = $.trim($("#DisposableInput").val()) * 1;
        var MonthlyInvestment = $.trim($("#MonthlyInvestment").val()) * 1;
        var RegularYear = $.trim($("#RegularYear").val()) * 1;

        var Num = ReturnOnInvestment + DisposableInput + MonthlyInvestment + RegularYear;
    
        if (!par.test(Num)) {
            Num = 0;
        }
        if(Num!=0){
            //此方案能实现的目标金额 FV（投资收益率÷12，定期定额投资年限×12，- 每月定期定额投资金额，- 一次性投资金额,0）
            _ajaxhepler({
                url: "/CompetitionUser/LifeEducationPlan/RetailLump",
                type: "POST",
                async: false,
                dataType: "json",
                data: {
                    rate: ReturnOnInvestment/12,
                    nper: RegularYear*12,
                    pmt: -MonthlyInvestment,
                    pv: -DisposableInput,
                    type:0
                },
                success: function (data) {
                    Num = data;
                }
            });
        }
        $("#TargetAmount").html((Num).toMyFixed(2));
    });

    //改变增长率时，教育费用计算
    $("#ForeignEduFee,#InlandEduFee").unbind("blur").blur(function () {
      
        for (i = 0; i < 6; i++)
        {
            var everyYearCoat=0;//每年花费
            var edu = $("#EducationList" + i);
            if (edu.length > 0)
            {
                //求学年龄
                var eduAge = $("#EducationList" + i + " input[field='EduAge']").val()*1;
                //求学时间
                var eduTime = $("#EducationList" + i + " input[field='EduTime']").val()*1;
                //目前学费
                var tuition = $("#EducationList" + i + " input[field='Tuition']").val()*1;

                //学费增长率
                var InlandEduFee = 0;
                if ($("#EduStage" + i).val()>0&&$("#EduStage" + i).val() != 6)
                { InlandEduFee = $("#InlandEduFee").val()*1/100; }
                else if ($("#EduStage" + i).val() == 6) { InlandEduFee = $("#ForeignEduFee").val()*1 / 100; }
                else { InlandEduFee = 0 }
                //上学时学费FV(学费增长率,求学年龄-子女年龄,0,-目前学费,1)
                var Num = eduTime + tuition + eduAge + InlandEduFee;
                var par = /^\d+(\.\d+)?$/;
                if (!par.test(Num)) {
                    Num = 0;
                }
                if (Num != 0) {
                    _ajaxhepler({
                        url: "/CompetitionUser/LifeEducationPlan/RetailLump",
                        type: "POST",
                        async: false,
                        dataType: "json",
                        data: {
                            rate: InlandEduFee,
                            nper: eduAge - $.trim($("#ChildAge").val()) * 1,
                            pmt: 0,
                            pv: -tuition,
                            type: 1
                        },
                        success: function (data) {
                            everyYearCoat = data;
                            var a = new String(data);
                            var b = a.indexOf('+');
                            if (b > -1) {
                                Num = Number(a);
                            } else {
                                Num = data.toMyFixed(2);
                            }

                        }
                    });
                    $("#EduTuition" + i).val(Num);
                }

                var TotalTuition= everyYearCoat * eduTime;

                var na = new String(TotalTuition);
                var n = 0;
                var bc = na.indexOf('+');
                if (bc > -1) {
                    n = TotalTuition;
                } else {
                    n = TotalTuition.toMyFixed(2);
                }
                $("#TotalTuition"+i).html((n));

            }
        }
        EduTotalAmount();

    })
    //客户信息是否保存
    IsProposalSave();
});

var TotalTuition = 0;

//获取教育规划
function GetLifeEducationPlan(valu) {
    var ProposalId = $.getUrlParam("ProposalId");

    _ajaxhepler({
        url: "/CompetitionUser/LifeEducationPlan/GetLifeEducationPlanList",
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
                var n=li;
                    $("#ChildAge").val(n.ChildAge);//子女年龄
                    $("#InlandEduFee").val(n.InlandEduFee);//国内学费增长率
                    $("#ForeignEduFee").val(n.ForeignEduFee);//国外学费增长率
                    $("#Insurance").val(n.Insurance.toMyFixed(2));//商业保险
                    $("#Deposit").val(n.Deposit.toMyFixed(2));//储蓄计划
                    $("#Other").val(n.Other.toMyFixed(2));//其他安排

                    var bb = Number(n.EduTotalAmount);
                    var a = new String(bb);                    
                    var b = a.indexOf("+");
                    var c = 0;
                    if (b > -1) {
                        c = n.EduTotalAmount
                    } else {
                        c=  n.EduTotalAmount.toMyFixed(2);
                    }
                    $("#EduTotalAmount").html(c);//上学前需准备的现金总金额
                    $("#ReturnOnInvestment").val(n.ReturnOnInvestment.toMyFixed(2));//预计投资收益率
                    $("#DisposableInput").val(n.DisposableInput.toMyFixed(2));//一次性投入金额
                    $("#MonthlyInvestment").val(n.MonthlyInvestment.toMyFixed(2));//每月定期投资金额
                    $("#RegularYear").val(n.RegularYear);//定期定额投资年限
                    $("#TargetAmount").html(n.TargetAmount.toMyFixed(2));//此方案能实现的目标金额
                    $("#Analysis").val(n.Analysis);//教育规划分析
                    //小计
                    var xiao = n.Insurance + n.Deposit + n.Other;
                    $("#xiaoji").val(xiao.toMyFixed(2));
                    $("#Id").val(n.Id);
            }
            //教育规划详细信息
            var li2 = datas.list;
            if (li2 != null) {                
                $.each(li2.LifeEducationPlanDetailList, function (i, n) {
                    
                    $("#EduStage" + i).val(n.EduStage);//教育阶段
                    $("#EduAge" + i).val(n.EduAge);//求学年龄
                    $("#EduTime" + i).val(n.EduTime);//求学时间
                    $("#Tuition" + i).val(n.Tuition.toMyFixed(2));//目前学费

                    var nu = Number(n.EduTuition);
                    var nn = new String(nu);
                    var m = nn.indexOf("+");
                    var mm = 0;
                    if (m > -1) {
                        mm = n.EduTuition
                    } else {
                        mm = n.EduTuition.toMyFixed(2);
                    }
                    $("#EduTuition" + i).val(mm);//上学时学费

                    var bb = Number(n.TotalTuition);
                    var a = new String(bb);                   
                    var b = a.indexOf("+");
                    var c = 0;
                    if(b>-1){
                        c = n.TotalTuition
                    } else {
                       c= n.TotalTuition.toMyFixed(2);
                    }

                    $("#TotalTuition" + i).html(c);//上学前需准备的总学费  
                    $("#ID" + i).val(n.Id);                  

                    TotalTuition = TotalTuition + n.TotalTuition;
                    N = li2.LifeEducationPlanDetailList.length;
                    if (N != i + 1) {
                        AddHTML(i + 1);
                    }
                    //把值赋给defaultVal作为原值
                    SaveDefaultValueCommon("LifeEducationPlan");
                });
            }
            //每月可支配资金
            //可用资产
        }
    });
}

//新增/修改 教育规划
function AddLifeEducationPlan(valu) {

    var ProposalId = $.getUrlParam("ProposalId");

    if (!VerificationHelper.checkFrom("LifeEducationPlan")) {
         return false;
     } else {
        var number = 0;
        var ChildAge =Number( $("#ChildAge").val());
        var trList = $("#LifeEducationPlan #div2").find("[field=EducationList]"); //全部数据
        $.each(trList, function (i, n) {
            var EduAge =Number($("#EduAge" + i).val());
            if(EduAge<ChildAge){
                
                number = number + 1;
            }
        });
        if (number > 0) {
            dialogHelper.Error({ content: "求学年龄必须大于等于子女年龄", success: function () { } });
        } else {
            var obj = new Object();
            obj["Id"] = $("#Id").val();
            obj["ProposalId"] = ProposalId;
            obj["ChildAge"] = $("#ChildAge").val();
            obj["InlandEduFee"] = $("#InlandEduFee").val();
            obj["ForeignEduFee"] = $("#ForeignEduFee").val();
            obj["Insurance"] = $("#Insurance").val();
            obj["Deposit"] = $("#Deposit").val();
            obj["Other"] = $("#Other").val();
            obj["EduTotalAmount"] = $("#EduTotalAmount").html();
            obj["ReturnOnInvestment"] = $("#ReturnOnInvestment").val();
            obj["DisposableInput"] = $("#DisposableInput").val();
            obj["MonthlyInvestment"] = $("#MonthlyInvestment").val();
            obj["RegularYear"] = $("#RegularYear").val();
            obj["TargetAmount"] = $("#TargetAmount").html();
            obj["Analysis"] = $("#Analysis").val();

            LifeEducationPlanDetail.RemoveAll();
            LifeEducationPlanDetail.GetList();
            var trList = $("#LifeEducationPlan #div2").find("[field=EducationList]"); //全部数据
            var number = 0;
            $.each(trList, function (i, n) {
                //var Id = $("#ID" + i).val();
                var obj = new Object();               
                var Id = $(n).find("IDs").val();
                obj["Id"] = Id;
                obj["ProposalId"] = ProposalId;
                obj["EduStage"] = $(n).find("select").val();
                obj["EduAge"] = $(n).find("[field=EduAge]").val();
                obj["EduTime"] = $(n).find("[field=EduTime]").val();
                obj["Tuition"] = $(n).find("[field=Tuition]").val();
                obj["EduTuition"] = $(n).find("[field=EduTuition]").val();
                obj["TotalTuition"] = $(n).find("[field=TotalTuition]").html();
               var index= LifeEducationPlanDetail.Add(obj);
               if(index>-1){
                   dialogHelper.Success({
                       content: "不能选择相同的教育阶段。", success: function () {

                       }
                   });
                   number = number + 1;
               }
            });
            if (number > 0) {
              
                LifeEducationPlanDetail.RemoveAll();
                return false;
            }

            var list = LifeEducationPlanDetail.GetList();
            obj["LifeEducationPlanDetailList"] = list;

            _ajaxhepler({
                url: "/CompetitionUser/LifeEducationPlan/AddLifeEducationPlans",
                type: "POST",
                async: false,
                dataType: "json",
                data: JSON.stringify(obj),
                contentType: "application/json",
                success: function (data) {
                    Nums = 0;
                    if (valu == 0) {
                       
                       
                        dialogHelper.Success({
                            content: "保存成功！", success: function () {
                                LifeEducationPlanDetail.RemoveAll();                                
                               
                                AddLifeEducationPlanDetail();

                                //GetLifeEducationPlan(0);
                                //刷新当前页
                                location.reload();
                            }

                        });
                      
                      //  location.href = location.href;
                    } else {
                        window.location.href = "/CompetitionUser/ConsumptionPlan/Index" + URL;
                    }

                    TotalTuition = 0;
                }
            });
        }
    }
}
//新增教育规划详细信息
function AddLifeEducationPlanDetail() {

    if (!VerificationHelper.checkFrom("LifeEducationPlan")) {
        return;
    } else {

        var trList = $("#LifeEducationPlan #div2").find("[field=EducationList]"); //全部数据
        $.each(trList, function (i, n) {
            if (i != 0) {
                $("#EducationList" + i).remove();
            }
        });
        GetLifeEducationPlan(0);
    }
}

//删除教育规划详细信息
function DeleteLifeEducationPlanDetail(valu) {  
    var Id = 0;   
    var trList = $("#LifeEducationPlan #div2").find("[field=EducationList]"); //全部数据
    $.each(trList, function (i,n) {
        Id = Id + 1;
    });
    if (Id != 1) {
        $("#EducationList" + valu).remove();
    }
    else {
        dialogHelper.Error({ content: "至少需要有一个面板" });
        return;
    }
    //计算总金额
    EduTotalAmount();
}

var Nums = 0;
var N = 0;
//添加HTML
function AddHTML(valu) {
        if (valu > 0) {            
            Nums = valu;
        } else {
            Nums = Nums + 1;
        }      
        
        if ($(".js_itemboxs").size() > 5)
        {
            return;
        }

        var html = '';


        html += '<div class="itemBox js_itemboxs" id="EducationList' + Nums + '"  field="EducationList">';
        html += '<div class="item-title b-gray">';
        html += '<a class="spr spr-del js_remove fr" href="#" onclick="DeleteLifeEducationPlanDetail(' + Nums + ')"></a>';
        html += '<i class="c-red">*</i>教育阶段选择&nbsp;';
        html += '<select field="EduStage" id="EduStage' + Nums + '"  class="IsRequired" selecttag="" msgname="教育阶段选择">';
        html += '<option value="0">请选择</option>';
        html += '<option value="1">幼儿园教育</option>';
        html += '<option value="2">小学教育</option>';
        html += '<option value="3">初中教育</option>';
        html += '<option value="4">高中教育</option>';
        html += '<option value="5">大学教育</option>';
        html += '<option value="6">留学教育</option>';
        html += '</select>';
        html += '</div>';
        html += ' <div class="item">';
        html += ' <div class="fif-form fif-form3  grid-7 ">';

        html += '<div class="fif-box w100">';
        html += '<label class="fif-text"><i class="c-red">*</i>求学年龄：</label>';
        html += '<div class="input"><input class="ipt-text IsRequired IsMaxNumber IsMinNumber IsNumber" onblur="Onblu1(this)" type="text" field="EduAge" msgname="求学年龄" maxnumber="50" minnumber="1" id="EduAge' + Nums + '"><span class="ml10">岁</span></div>';
        html += '</div>';

        html += '<div class="fif-box w100">';
        html += '<label class="fif-text"><i class="c-red">*</i>求学时间：</label>';
        html += '<div class="input"><input field="EduTime" class="ipt-text IsRequired IsMaxNumber IsMinNumber" onblur="Onblu1(this)"  type="text" msgname="求学时间" maxnumber="10" minnumber="1" id="EduTime' + Nums + '"><span class="ml10">年</span></div>';
        html += '</div>';

        html += '<div class="fif-box w100">';
        html += '<label class="fif-text"><i class="c-red">*</i>目前学费：</label>';
        html += '<div class="input"><input field="Tuition" class="ipt-text IsRequired IsMinFloat IsMinFloat IsReg" onblur="Onblu1(this)"  type="text" msgname="目前学费" minfloat="0" msgreg="请输入0.00到99999999999.99的数字" reg="\\d{0,9}\\.*\\d{0,2}" maxfloat="99999999999.99" id="Tuition' + Nums + '"><span class="ml10">元/年</span></div>';
        html += '</div>';

        html += '<div class="fif-box w100">';
        html += '<label class="fif-text">上学时学费：</label>';
        html += '<div class="input"><input  field="EduTuition" type="text" disabled class="ipt-text disabled IsRequired" msgname="上学时学费" id="EduTuition' + Nums + '"><span class="ml10">元/年</span></div>';
        html += '</div>';

        html += '<div class="fif-box w100">';
        html += '<div class="amount" style="font-size:16px;font-weight:600;height:50px;padding-top:20px;">上学前需准备的总学费&nbsp;&nbsp;';
        html += '<strong style="font-size:20px;font-weight:600;height:50px;padding-top:20px;color:#f87608;" field="TotalTuition" id="TotalTuition' + Nums + '"></strong>元';
        html += '</div>';
        html += '</div>';

        html += '<input type="hidden" value="' + Nums + '" id="NUM" />';
        html += '<input type="hidden" field="IDs" value="0" id="ID' + Nums + '" />';
        html += '</div>';
        html += '</div>';
        html += '</div>';
        $("#span").prev(".js_itemboxs").after(html);

        
}

//教育规划详细信息失去焦点事件
function Onblu1(valu) { 
    var EduTime = $.trim($(valu).parent().parent().parent().find("[field=EduTime]").val()) * 1;
    var Tuition = $.trim($(valu).parent().parent().parent().find("[field=Tuition]").val()) * 1;
    var EduAge = $.trim($(valu).parent().parent().parent().find("[field=EduAge]").val()) * 1;
    var ChildAge = $.trim($("#ChildAge").val()) * 1;

    //学费增长率
    var InlandEduFee = 0;
    var edu = $(valu).parent().parent().parent().parent().parent().find("select").val();
    if (edu != "6") {
        InlandEduFee = $.trim($("#InlandEduFee").val()) * 1 / 100;
    } else {
        InlandEduFee = $.trim($("#ForeignEduFee").val()) * 1 / 100;
    }
   
    //上学时学费FV(学费增长率,求学年龄-子女年龄,0,-目前学费,1)
    var Num = EduTime + Tuition + EduAge + InlandEduFee;
    var par = /^\d+(\.\d+)?$/;
    if (!par.test(Num)) {
        Num = 0;
    }
    if (Num != 0) {
        _ajaxhepler({
            url: "/CompetitionUser/LifeEducationPlan/RetailLump",
            type: "POST",
            async: false,
            dataType: "json",
            data: {
                rate: InlandEduFee,
                nper: EduAge - ChildAge,
                pmt: 0,
                pv: -Tuition,
                type: 1
            },
            success: function (data) {
                var a = new String(data);
                var b = a.indexOf('+');
                if(b>-1){
                    Num =Number(a);
                } else {
                    Num = data.toMyFixed(2);
                }
                
            }
        });
    }
    $(valu).parent().parent().parent().find("[field=EduTuition]").val((Num));
    //上学前需准备的总学费
    var nu=Num * EduTime
    var na=new String(nu);
    var n = 0;
    var bc = na.indexOf('+');
    if (bc > -1) {
        n = nu;
    }else{
        n = nu.toMyFixed(2);
    }
    $(valu).parent().parent().parent().find("[field=TotalTuition]").html((n));
  
    EduTotalAmount();
}

//教育规划详细信息选择事件
function OnChangeEven(valu) {
    valu = "#" + valu;
    var EduTime = $.trim($(valu).find("[field=EduTime]").val()) * 1;
    var Tuition = $.trim($(valu).find("[field=Tuition]").val()) * 1;
    var EduAge = $.trim($(valu).find("[field=EduAge]").val()) * 1;
    var ChildAge = $.trim($("#ChildAge").val()) * 1;

    //学费增长率
    var InlandEduFee = 0;
    var edu = $(valu).find("select").val();
    if (edu != "6") {
        InlandEduFee = $.trim($("#InlandEduFee").val()) * 1 / 100;
    } else {
        InlandEduFee = $.trim($("#ForeignEduFee").val()) * 1 / 100;
    }

    //上学时学费FV(学费增长率,求学年龄-子女年龄,0,-目前学费,1)
    var Num = EduTime + Tuition + EduAge + InlandEduFee;
    var par = /^\d+(\.\d+)?$/;
    if (!par.test(Num)) {
        Num = 0;
    }
    if (Num != 0) {
        _ajaxhepler({
            url: "/CompetitionUser/LifeEducationPlan/RetailLump",
            type: "POST",
            async: false,
            dataType: "json",
            data: {
                rate: InlandEduFee,
                nper: EduAge - ChildAge,
                pmt: 0,
                pv: -Tuition,
                type: 1
            },
            success: function (data) {
                var a = new String(data);
                var b = a.indexOf('+');
                if (b > -1) {
                    Num = Number(a);
                } else {
                    Num = data.toMyFixed(2);
                }

            }
        });
    }
    $(valu).find("[field=EduTuition]").val((Num));
    //上学前需准备的总学费
    var nu = Num * EduTime
    var na = new String(nu);
    var n = 0;
    var bc = na.indexOf('+');
    if (bc > -1) {
        n = nu;
    } else {
        n = nu.toMyFixed(2);
    }
    $(valu).find("[field=TotalTuition]").html((n));

    EduTotalAmount();
}

//上学前需准备的现金总金额
function EduTotalAmount() {

    //上学前需准备的总学费
    var AllTotal = 0;
    var trList = $("#LifeEducationPlan #div2").find("[field=EducationList]"); //全部数据
    $.each(trList, function (i, n) {
       // AllTotal = AllTotal + Number($("#TotalTuition" + i).html());
        AllTotal = AllTotal + Number($(n).find("[field=TotalTuition]").html());
    });

    var Insurance = $.trim($("#Insurance").val()) * 1;
    var Deposit = $.trim($("#Deposit").val()) * 1;
    var Other = $.trim($("#Other").val()) * 1;

    var Num = Insurance + Deposit + Other;
    var par = /^\d+(\.\d+)?$/;
    if (!par.test(Num)) {
        Num = 0;
    }
    //小计
    $("#xiaoji").val((Num).toMyFixed(2));
    //上学前需准备的现金总金额
    var a = new String(AllTotal);
    var b = a.indexOf('+');
    var c = 0;
    if(b>-1){
        c = AllTotal - Num;
    } else {
        c = (AllTotal - Num).toMyFixed(2)
    }
    $("#EduTotalAmount").html(c);
}
