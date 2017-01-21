var answerHelper = new arrayHelper("ExamPointId");

$(function () {
    //获取URL参数
    var Id = $.getUrlParam("Id");

    // 理财类型下拉菜单
    selectHelper.GetSelect({
        url: "/CompetitionAdmin/Resource/GetFinancialTypeList",
        Id: "#selectFinancialType",
        value: "请选择"
    });
    // 考核内容下拉菜单
    selectHelper.GetSelect({
        url: "/CompetitionAdmin/Resource/GetExamContentList",
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
        $("#btnSave").css("display", "none");  //隐藏保存按钮
        $("#btnCaseCancle").val("返回");
    }

    //获取案例
    if (Id != null && Id != "" && Id != undefined) {
        GetCase(Id);
    }
    //初次加载时，根据考核内容Id获取考核点
    GetExamPointList($("#selectExamContent").val());


    //答案文本框绑定离开事件：离开时，把答案放入答案列表内
    $("input[name='answer']").live("blur", function () {
        var answer = new Object();
        answer["Id"] = 0;
        answer["ExamPointId"] = $(this).attr("tag");
        answer["Answer"] = $.trim($(this).val());
        //新增/修改答案
        answerHelper.Add(answer);
    });

    //新增、修改
    $("#btnSubmit").unbind("click").click(function () {
        EditCase();
    });
});

// 根据考核内容Id，获取考核点
function GetExamPointList(ContentId) {
    $.ajax({
        url: "/CompetitionAdmin/Resource/GetExamPointList",
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
            GenerationHtmlList(data);
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
        $("#ExamContentList").find("input[type='text']").addClass("b-gray").attr("readonly", true);//禁用考点
    }
}

// 获取案例
function GetCase(Id) {
    $.ajax({
        url: "/CompetitionAdmin/Resource/GetCase",
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
            $("#hdCaseId").val(data.Id);
            $("#hdUserId").val(data.UserId);
            $("#CustomerName").val(data.CustomerName);
            $("#IDNum").val(data.IDNum);
            $("#selectFinancialType").val(data.FinancialTypeId);
            $("#CustomerStory").val(data.CustomerStory);
            //答案
            $(data.ExamPointAnswer).each(function (index, dom) {
                answerHelper.Add(dom);
            });
        }
    });
}

//新增/修改案例
function EditCase() {
    // 页面字段检测
    if (!VerificationHelper.checkFrom("ContextDiv")) {
        return;
    }
    var IDNum = $("#IDNum").val();
    var CaseId = $("#hdCaseId").val();
    var Flag = false;
    // 判断身份证号是否重复
    $.ajax({
        url: "/CompetitionAdmin/Resource/CheckRepeat",
        type: "POST",
        async: false,
        dataType: "json",
        data: { CaseId: CaseId, IDNum: IDNum },
        success: function (data) {
            Flag = true;
        }
    });
    if (!Flag) {
        return;
    }
    // 判断是否被待发布的竞赛引用
    if (CaseId > 0) {
        var flag1 = CheckCaseByUsed(CaseId);
        if (flag1) {
            dialogHelper.Error({
                content: "不能修改，该案例已被未结束的竞赛使用"
            });
            return;
        }
    }
    //此处参数必须跟VM一致
    var obj = new Object();
    obj["Id"] = $("#hdCaseId").val();
    obj["CustomerName"] = $("#CustomerName").val();
    obj["IDType"] = 1;//扩展字段，默认固定暂为1身份证
    obj["IDNum"] = $("#IDNum").val();
    obj["FinancialTypeId"] = $("#selectFinancialType").val();
    obj["CustomerStory"] = $("#CustomerStory").val();
    //obj["CaseSource"] = EnumList.CaseSource.Custom;
    //obj["UserId"] = $("#hdUserId").val();
    obj["ExamPointAnswer"] = answerHelper.GetList();

    $.ajax({
        url: "/CompetitionAdmin/Resource/SaveCase",
        type: "POST",
        async: false,
        dataType: "json",
        data: JSON.stringify(obj),
        contentType: "application/json",
        success: function (data) {
            if (data != null) {
                //弹出成功提示 
                dialogHelper.Success({
                    content: "保存成功！",
                    success: function () {
                        btnCancel();
                    }
                });
                $("#hdCaseId").val(data.Id);
            }
        }
    });
}

// 取消返回到列表
function btnCancel() {
    location.href = "/CompetitionAdmin/Resource/CaseList";
}

//检查案列是否被未结束的竞赛引用
function CheckCaseByUsed(Id) {
    var Flag = false;

    $.ajax({
        url: "/CompetitionAdmin/Resource/CheckCaseInUnEndMatch",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            caseId: Id
        },
        success: function (data) {
            //刷新当前页
            Flag = data
        }
    });
    return Flag;
}