var customer_index = 0;

$(function () {
    //新增时默认显示一条
    EditList();

    //增加客户家属信息
    $("#Add").unbind("click").bind("click", function () {
        var num = $("#siblist .sib-item").size();
        if (num < 5) {
            EditList();
        }
    });

    //保存信息
    $("#Save").bind("click", function () {
        SaveInfo(false);
    });


    //销售机会Id
    var TrainExamId = $.getUrlParam("TrainExamId");
    //建议书Id
    var ProposalId = $.getUrlParam("ProposalId");
    var StuCustomerId = $.getUrlParam("StuCustomerId");
    //获取建议书客户信息及家属信息列表
    if (ProposalId != null && ProposalId != "" && ProposalId != undefined) {
        CustomerInfo(ProposalId);
    } 

    //下一步
    $("#btNextStep").bind("click", function () {
        SaveInfo(true);
    });

    //保存之后必须重新保存一下基础值
    SaveDefaultValueCommon("EditProposalCustomerDiv");

})



//客户信息显示
function CustomerInfo(ProposalId) {
    _ajaxhepler({
        url: "/CompetitionUser/ProposalCustomer/GetProposalCustomer",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: ProposalId,
            rId: Math.random(),
        },
        success: function (data) {
            if (data != null) {
                SetProposalCustomerInfo(data);//编辑建议书客户信息的设置
            }
            else {
                ProposalId = 0;//新增
            }
        }
    });
}


//设置建议书客户信息
function SetProposalCustomerInfo(data) {
    //隐藏域
    $("#hdProposalCustomerId").val(data.ProposalCustomerVM.Id);
    if ((data.ProposalNum == null && data.ProposalNum == "")) {
    } else { 
        $("#ProposalNum").html(data.ProposalNum);
    }

    $("#ProposalName").val(data.ProposalName);
    //客户信息
    $("#CustomerName").val(data.ProposalCustomerVM.CustomerName);
    $("#PinYin").val(data.ProposalCustomerVM.PinYin);
    $("#Age").val(data.ProposalCustomerVM.Age);
    $("#IDType").val(data.ProposalCustomerVM.IDType);
    $("#IDNum").val(data.ProposalCustomerVM.IDNum);
    $("#Phone").val(data.ProposalCustomerVM.Phone);
    $("#Tel").val(data.ProposalCustomerVM.Tel);
    $("#Email").val(data.ProposalCustomerVM.Email);
    $("#Position").val(data.ProposalCustomerVM.Position);
    $("#Company").val(data.ProposalCustomerVM.Company);
    $("#Address").val(data.ProposalCustomerVM.Address);
    //客户亲属列表
    $("#siblist").html("");
    $(data.ProposalCustomerVM.ProposalCustomerDetailList).each(function (index, dom) {
        EditList(dom.DependentName, dom.Age, dom.Relation, dom.InCome);
    });
}


//增加建议书客户家属列表
function EditList(DependentName, Age, Relation, InCome) {
    customer_index += 1;
    var trHtml = "";
    trHtml += "<div class=\"sib-item\">";
    trHtml += "<span class=\"close\" id=\"closeId\" onclick=\"Close(this)\"></span>";
    trHtml += "<div class=\"fif-form\">";
    trHtml += "<div class=\"fif-box\">";
    trHtml += "<label class=\"fif-text\">姓 名</label>";
    trHtml += "<div class=\"input\"><input id=\"customer_name_{4}\" name=\"customer_detail_name\" class=\"ipt-text IsRequired IsMinLength IsMaxLength IsReg\" reg=\"[A-Za-z \u4e00-\u9fa5 \s]+\" msgreg=\"只能输入汉字、字母和空格\" type=\"text\" value='{0}' MinLength=\"2\" MaxLength=\"15\" MsgName=\"姓名\"></div>";
    trHtml += "</div>";
    trHtml += "<div class=\"fif-box\">";
    trHtml += "<label class=\"fif-text\">年 龄</label>";
    trHtml += "<div class=\"input\"><input id=\"customer_age_{4}\" name=\"customer_detail_age\" class=\"ipt-text IsNumber IsMinNumber IsMaxNumber \" type=\"text\" value='{1}' MinNumber=\"1\" MaxNumber=\"150\" MsgName=\"年龄\"><span class=\"ml10\">岁</span></div>";
    trHtml += "</div>";
    trHtml += "<div class=\"fif-box\">";
    trHtml += "<label class=\"fif-text\">与客户关系</label>";
    trHtml += "<div class=\"input\"><input id=\"customer_relation_{4}\" name=\"customer_detail_relation\" class=\"ipt-text  IsMinLength IsMaxLength IsReg\" reg=\"[A-Za-z \u4e00-\u9fa5 \s]+\" msgreg=\"只能输入汉字、字母和空格\" type=\"text\" value='{2}' MinLength=\"2\" MaxLength=\"20\" MsgName=\"与客户关系\"></div>";
    trHtml += "</div>";
    trHtml += "<div class=\"fif-box\">";
    trHtml += "<label class=\"fif-text\">年收入</label>";
    trHtml += "<div class=\"input\"><input id=\"customer_income_{4}\" name=\"customer_detail_income\" class=\"ipt-text IsMaxFloat IsMinFloat IsReg\" msgreg=\"请输入0.00到999999999.99的数字\" reg=\"\\d{0,9}\\.*\\d{0,2}\" type=\"text\" value='{3}' MinFloat=\"0.00\" MaxFloat=\"999999999.99\" MsgName=\"年收入\"><span class=\"ml10\">元</span></div>";
    trHtml += "</div></div></div>";

    var html = StringHelper.FormatStr(trHtml,
        DependentName,      //0 姓名
        Age,                //1 年龄
        Relation,           //2 与客户关系
        InCome,             //3 年收入
        customer_index      //4 随机Id
        );

    $("#siblist").append(html);
}


//关闭建议书客户家属列表
function Close(obj) {
    $(obj).parent().remove();
}


//保存建议书客户信息(新增和修改)
function SaveInfo(flag) {
    //页面字段检测
    if (!VerificationHelper.checkFrom("EditProposalCustomerDiv",
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
        return;
    }

    var proposalObj = new Object();
    var proposalCustomerObj = new Object();
    var proposalCustomerDetailObj;

    var StuCustomerId = $.getUrlParam("StuCustomerId");
    //建议书信息
    var ProposalId = $.getUrlParam("ProposalId");
    proposalObj["Id"] = ProposalId;
    proposalObj["TrainExamId"] = $.getUrlParam("TrainExamId");
    proposalObj["ProposalNum"] = $("#ProposalNum").text();
    proposalObj["ProposalName"] = $("#ProposalName").val();
    proposalObj["StuCustomerId"] = StuCustomerId;

    //建议书客户信息
    proposalCustomerObj["Id"] = $("#hdProposalCustomerId").val();
    proposalCustomerObj["ProposalId"] = ProposalId;
    proposalCustomerObj["CustomerName"] = $.trim($("#CustomerName").val());
    proposalCustomerObj["PinYin"] = $.trim($("#PinYin").val());
    proposalCustomerObj["Age"] = $.trim($("#Age").val());
    proposalCustomerObj["IDType"] = $("#IDType").val();
    proposalCustomerObj["IDNum"] = $.trim($("#IDNum").val());
    proposalCustomerObj["Phone"] = $.trim($("#Phone").val());
    proposalCustomerObj["Tel"] = $.trim($("#Tel").val());
    proposalCustomerObj["Email"] = $.trim($("#Email").val());
    proposalCustomerObj["Position"] = $.trim($("#Position").val());
    proposalCustomerObj["Company"] = $.trim($("#Company").val());
    proposalCustomerObj["Address"] = $.trim($("#Address").val());

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
    proposalObj["ProposalCustomerVM"] = proposalCustomerObj; 
    var TrainExamId = $.getUrlParam("TrainExamId");
    var StuCustomerId = $.getUrlParam("StuCustomerId"); 
    _ajaxhepler({
        url: "/CompetitionUser/ProposalCustomer/AddUpdateProposalCustomer",
        type: "POST",
        async: false,
        dataType: "json",
        data: JSON.stringify(proposalObj),
        contentType: "application/json",
        success: function (data, txtStatus) {

            //保存之后必须重新保存一下基础值
            SaveDefaultValueCommon("EditProposalCustomerDiv");
            if (flag){
                location.href = "/CompetitionUser/RiskEvaluation/Index?TrainExamId=" + TrainExamId + "&ProposalId=" + data + "&StuCustomerId=" + StuCustomerId;
            }
            else{
                dialogHelper.Success({
                    content: msgList["20010"],
                    success: function () {
                        location.href = "/CompetitionUser/ProposalCustomer/Index?TrainExamId=" + TrainExamId + "&ProposalId=" + data + "&StuCustomerId=" + StuCustomerId;
                    }
                });
            }
        }
    });
}

