//*******************************
//考生端-实训考试-财产分配
//*******************************

var DistributionOfProperty_index = 0;
var TagNaviDistri = false;//跳转标记
var paramDistribution = "";//str标记
$(function () {
   

  paramDistribution = $("#hdParam").val();
    //新增时默认显示一条
    SetCustomerDetailList();

    //增加客户家属信息
    $("#DistributionOfPropertyDiv #Add").unbind("click").bind("click", function () {
        var num = $("#siblist .sib-item").size();
        if (num < 5) {
            SetCustomerDetailList();
        }
    });

    //保存信息
    $("#btnSave").unbind("click").bind("click", function () {
        SaveInfo();
    });
    $("#btnNext").unbind("click").bind("click", function () {
        SaveInfo(0)
    })

    //建议书Id
    var ProposalId = $.getUrlParam("ProposalId");

    //获取建议书客户信息及家属信息列表
    if (ProposalId != null && ProposalId != "" && ProposalId != undefined) {
        LoadDistributionOfPropertyInfo(ProposalId);
    }

    //保存原值
    SaveDefaultValueCommon("DistributionOfPropertyDiv");
    IsProposalSave();//客户验证
});



//财产分配表显示
function LoadDistributionOfPropertyInfo(ProposalId) {
    _ajaxhepler({
        url: "/CompetitionUser/DistributionOfProperty/GetDistributionOfPropertyByProposalId",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: ProposalId
        },
        success: function (data) {
            if (data != null) {
                SetDistributionOfPropertyInfo(data);//编辑建议书财产分配表设置
            }
        }
    });
}

//设置财产分配表显示
function SetDistributionOfPropertyInfo(data) {
    //隐藏域
    $("#hdDistributionOfPropertyId").val(data.Id);
    if (data.ProposalId != "") {
        //建议书
        $("#ProposalId").val(data.ProposalId);
    }
    //客户性别
    $("#CustomerName").val(data.CustomerName);
    $("#CustomerAge").val(data.CustomerAge);
    $("#CustomerSex").val(data.CustomerSex);
  
    $("#Address").val(data.Address);
    $("#Position").val(data.Position);
    $("#FamilyNum").val(data.FamilyNum);
    $("#SituationAnalysis").val(data.SituationAnalysis);
    $("#PlanTool").val(data.PlanTool);
    $("#PlanAnalysis").val(data.PlanAnalysis);
    //先清空
    $("#siblist").html("");
    if (data.ProposalCustomerDetailList != null && data.ProposalCustomerDetailList.length > 0) {
        //客户亲属列表
 
        $(data.ProposalCustomerDetailList).each(function (index, dom) {
            SetCustomerDetailList(dom.DependentName, dom.Age, dom.Relation, dom.InCome);
        });
    }
}


//增加建议书客户家属列表
function SetCustomerDetailList(DependentName, Age, Relation, InCome) {
    DistributionOfProperty_index += 1;
    var trHtml = "";
    trHtml += "<div class=\"sib-item\">";
    trHtml += "<span class=\"close\" id=\"closeId\" onclick=\"Close(this)\"></span>";
    trHtml += "<div class=\"fif-form\">";
    trHtml += "<div class=\"fif-box\">";
    trHtml += "<label class=\"fif-text\">姓 名</label>";
    trHtml += "<div class=\"input\"><input id=\"customer_name_{4}\" name=\"customer_detail_name\" class=\"ipt-text IsRequired IsMinLength IsMaxLength\" type=\"text\" value='{0}' MinLength=\"2\" MaxLength=\"20\" MsgName=\"姓名\"></div>";
    trHtml += "</div>";
    trHtml += "<div class=\"fif-box\">";
    trHtml += "<label class=\"fif-text\">年 龄</label>";
    trHtml += "<div class=\"input\"><input id=\"customer_age_{4}\" name=\"customer_detail_age\" class=\"ipt-text IsRequired IsNumber IsMinNumber IsMaxNumber\" type=\"text\" value='{1}' MinNumber=\"1\" MaxNumber=\"150\" MsgName=\"年龄\"><span class=\"ml10\">岁</span></div>";
    trHtml += "</div>";
    trHtml += "<div class=\"fif-box\">";
    trHtml += "<label class=\"fif-text\">与客户关系</label>";
    trHtml += "<div class=\"input\"><input id=\"customer_relation_{4}\" name=\"customer_detail_relation\" class=\"ipt-text IsRequired  IsMinLength IsMaxLength\" type=\"text\" value='{2}' MinLength=\"2\" MaxLength=\"20\" MsgName=\"与客户关系\"></div>";
    trHtml += "</div>";
    trHtml += "<div class=\"fif-box\">";
    trHtml += "<label class=\"fif-text\">年收入</label>";
    trHtml += "<div class=\"input\"><input id=\"customer_income_{4}\" name=\"customer_detail_income\" class=\"ipt-text IsFloat2 IsRequired IsMaxFloat IsMinFloat IsReg\" type=\"text\" value='{3}' msgreg=\"请输入0.00到999999999.99的数字\" Reg=\"\\d{0,9}\\.*\\d{0,2}\" MinFloat=\"0\" MaxFloat=\"999999999.99\" MsgName=\"年收入\"><span class=\"ml10\">元</span></div>";
    trHtml += "</div></div></div>";

    var html = StringHelper.FormatStr(trHtml,
        DependentName,      //0 姓名
        Age,                //1 年龄
        Relation,           //2 与客户关系
        InCome,             //3 年收入
        DistributionOfProperty_index      //4 随机Id
        );

    $("#siblist").append(html);
}


//关闭建议书客户家属列表
function Close(obj) {
    $(obj).parent().remove();
}




//保存建议书客户信息(新增和修改)
function SaveInfo(saveFlag) {
    TagNaviDistri = true;
    //页面字段检测
    if (!VerificationHelper.checkFrom("DistributionOfPropertyDiv",
        function () {
            var IDType = parseInt($("#IDType").val());
            var IDNum = $.trim($("#IDNum").val());
            $("#IDNum").val(IDNum);

            if (IDNum.length > 0) {
                if (IDType == 1) {
        //身份证
                    if (IDNum.length != 18) {
                        showValidateMsg("IDNum", "请输入正确的证件号码");
    }
    }
    } else {
                showValidateMsg("IDNum", "请输入证件号码");
    };
    })) {
        TagNaviDistri = false;
        return;
    };
    var FamilyNum = $("#FamilyNum").val() * 1;//家庭人数
    var num = $("#DistributionOfPropertyDiv .sib-item").size()+1;
    if (FamilyNum != num) {
        dialogHelper.Error({ content: "家庭成员的记录条数与家庭成员数要一致" });
        return;
    }



    var proposalObj = new Object();
    var proposalCustomerObj = new Object();
    var proposalCustomerDetailObj;


    var ProposalId = $.getUrlParam("ProposalId");
 
    //proposalObj["TrainExamId"] = $.getUrlParam("TrainExamId");
    //proposalObj["ProposalId"] = ProposalId
 

    //建议书客户信息
    proposalCustomerObj["Id"] = $("#hdDistributionOfPropertyId").val();
    proposalCustomerObj["ProposalId"] = ProposalId;
    proposalCustomerObj["CustomerSex"] = $("#CustomerSex").val();
    proposalCustomerObj["Address"] = $.trim($("#Address").val());
    proposalCustomerObj["Position"] = $.trim($("#Position").val());
    proposalCustomerObj["FamilyNum"] = FamilyNum;
    proposalCustomerObj["SituationAnalysis"] = $.trim($("#SituationAnalysis").val());
    proposalCustomerObj["PlanTool"] = $("#PlanTool").val();
    proposalCustomerObj["PlanAnalysis"] = $.trim($("#PlanAnalysis").val());


    //建议书客户家属信息
    var objList = new Array();
    //遍历建议书客户家属信息
    $("#siblist .sib-item").each(function (index, dom) {
        proposalCustomerDetailObj = new Object();
        proposalCustomerDetailObj["Type"] = EnumList.ProposalCustDetailType.CustomerFaimly;
        proposalCustomerDetailObj["DependentName"] = $(dom).find("input[name='customer_detail_name']").val();
        proposalCustomerDetailObj["Age"] = $(dom).find("input[name='customer_detail_age']").val();
        proposalCustomerDetailObj["Relation"] = $(dom).find("input[name='customer_detail_relation']").val();
        proposalCustomerDetailObj["InCome"] = $(dom).find("input[name='customer_detail_income']").val();
        objList.push(proposalCustomerDetailObj);
    });
    proposalCustomerObj["ProposalCustomerDetailList"] = objList;


    _ajaxhepler({
        url: "/CompetitionUser/DistributionOfProperty/SaveDistributionOfProperty",
        type: "POST",
        async: false,
        dataType: "json",
        data: JSON.stringify(proposalCustomerObj),
        contentType: "application/json",
        success: function (data, txtStatus) {
            if (data != null) {
                //保存原值
                SaveDefaultValueCommon("DistributionOfPropertyDiv");

                $("#hdDistributionOfPropertyId").val(data.Id);
            }
            if (typeof saveFlag == "undefined") {
                dialogHelper.Success({
                    content: msgList["20010"],
                    success: function () {
                        window.location.reload();
                    },
                    cancle: function () {
                        window.location.reload();
                    }
                });
            } else {
                window.location.href = "/CompetitionUser/Heritage/Index" + paramDistribution;
            }
        }
    });
}

