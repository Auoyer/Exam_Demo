var answerHelper = new arrayHelper("ExamPointId");

$(function () {
    //获取URL参数
    var Id = $.getUrlParam("Id");

    // 理财类型下拉菜单
    selectHelper.GetSelect({
        url: "/Admin/Resource/GetFinancialTypeList",
        Id: "#selectFinancialType",
        value: "请选择"
    });
    // 考核内容下拉菜单
    selectHelper.GetSelect({
        url: "/Admin/Resource/GetExamContentList",
        Id: "#selectExamContent",
        flag: false,
        changeFun: function (value) { 
            GetExamPointList(value);
        }
    });

    var Status = $.getUrlParam("Status");//进入状态
    //当查看进入时，不可输入
    if (Status == "View") {
        $("#CustomerName,#IDNum,#selectFinancialType").addClass("b-gray").attr("readonly", true);//禁用案例
        //textarea不能禁用
        $("#CustomerStory").addClass("b-gray").attr("readonly", true);
        $("#ExamContentList").find("input[type='text']").addClass("b-gray").attr("readonly", true);//禁用考点
    }

    //获取案例
    if (Id != null && Id != "" && Id != undefined) {
        GetCase(Id);
    }
    //初次加载时，根据考核内容Id获取考核点
    GetExamPointList($("#selectExamContent").val());

});

// 根据考核内容Id，获取考核点
function GetExamPointList(ContentId) {
    $.ajax({
        url: "/Admin/Resource/GetExamPointList",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ContentId: ContentId,
            rId: Math.random()
        },
        success: function (data) {
            // 生成考核点列表
            GenerationHtmlList(data.Data);
        }
    });
}

// 生成考核点列表
function GenerationHtmlList(data) {
    var StrHtml = "";
    // 先绑模块及考核点
    var ModuleLength = data.ExamModuleList.length;
    var PointLength = data.ExamPointList.length;
    for (var i = 0; i < ModuleLength; i++) {
        var ExamModuleName = data.ExamModuleList[i].ExamModuleName;
        var Id = data.ExamModuleList[i].Id;
        if (ExamModuleName != "" && ExamModuleName != null) {
            StrHtml += " <tr> <td colspan=\"3\" align=\"left\"> " + ExamModuleName + " </td> </tr>";
        }      
        if (ExamModuleName == "") {
            StrHtml += " <tr> <td colspan=\"3\" align=\"left\" style='height:20px'></td> </tr>";
        }

        for (var j = 0; j < PointLength; j++) {
            var ExamModuleId = data.ExamPointList[j].ExamModuleId;
            var ExamPointId = data.ExamPointList[j].Id
            if (ExamModuleId == Id) {
                var ExamPointType = data.ExamPointList[j].ExamPointType;
                var TypeName = "客观题";
                var MaxLength = 50;
                if (ExamPointType == 2) {
                    TypeName = "主观题";
                    MaxLength = 500;
                }
                //查找当前考核点答案
                var Record = answerHelper.FindRecord(ExamPointId);
                var Answer = "";
                if (Record != undefined && Record.Answer != null) {
                    Answer = Record.Answer;
                }
                StrHtml += " <tr> <td>" + TypeName + "</td> <td> " + data.ExamPointList[j].ExamPointName + " </td> ";
                StrHtml += " <td> <input style=\"width:130px;\" class=\"ipt-text IsMaxLength IsMinLength\" msgreg=\"请输入0到" + MaxLength + "的字符\" minlength=\"0\" maxlength=\"" + MaxLength + "\" msgname=\"data.ExamPointList[j].ExamPointName\" tag=\"" + ExamPointId + "\" value=\"" + Answer + "\" name=\"answer\" type=\"text\"> </td> </tr> ";

            }
        }
    }
    $("#ExamContentList").empty().append(StrHtml);
    var Status = $.getUrlParam("Status");//进入状态
    //当查看进入时，不可输入
    if (Status == "View") {
        $("#ExamContentList").find("input[type='text']").addClass("b-gray").attr("readonly", "true");//禁用考点
    }
}

// 获取案例
function GetCase(Id) {
    $.ajax({
        url: "/Admin/Resource/GetCase",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            Id: Id,
            rId: Math.random()
        },
        success: function (data) {
            //给界面上控件赋值
            $("#hdCaseId").val(data.Data.Id);
            $("#hdUserId").val(data.Data.UserId);
            $("#CustomerName").val(data.Data.CustomerName);
            $("#IDNum").val(data.Data.IDNum);
            $("#selectFinancialType").val(data.Data.FinancialTypeId);
            $("#CustomerStory").val(data.Data.CustomerStory);
            //答案
            $(data.Data.ExamPointAnswer).each(function (index, dom) {
                answerHelper.Add(dom);
            });
        }
    });
}

// 取消返回到列表
function btnCancel() {
    location.href = "/Admin/Resource/CaseList";
}