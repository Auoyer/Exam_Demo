$(function () {
    SelectSchool();
    // 理财类型下拉菜单
    selectHelper.GetSelect({
        url: "/Admin/Resource/GetFinancialTypeList",
        Id: "#selectFinancialType",
        value: "全部"
    });
    // 案例列表
    GetList("", "", "");
    // 搜索
    $("#btnSearch").unbind("click").click(function () {
        //获取查询条件
        var CollegeId = "";
        var TypeId = "";
        var KeyWord = "";
        var type = $("#selectFinancialType").val();
        if (parseInt(type) != 0) {
            TypeId = type;
        }
        var college = $("#selectCollegeType").children('option:selected').attr("tag");
        if (parseInt(college) != -1) {
            CollegeId = college;
        }

        var key = $.trim($("#txtKeyWord").val()).replace(/[<>&"]/g, function (c) { return { '<': '&lt;', '>': '&gt;', '&': '&amp;', '"': '&quot;', '"': '&quot;', "'": '&prime;', "'": '&prime;' }[c]; });
        //if (key == "客户姓名/身份证号")
        //{ key = ""; }
        if (key != null && key != "" && key.length > 0) {
            KeyWord = key;
        }
        GetList(CollegeId, TypeId, KeyWord);
    });

    //$("#txtKeyWord").unbind("focus").focus(function () {

    //    $("#txtKeyWord").val("").css("color", "black");
    //});
});

// 学校下拉框
function SelectSchool() {
    $.ajax({
        url: "/Admin/Resource/GetCollegeList",
        async: false,
        type: "POST",
        data: {
        },
        success: function (data) {
            var html = "";
            $.each(data.Data, function (e, f) {
                html += "<option tag='" + f.Id + "' text='" + f.CollegeName + "'>" + f.CollegeName + "</option>";
            });
            $("#selectCollegeType").html(html);
        }
    });
}

/**
 * @name 获取案例列表
 */
function GetList(CollegeId, TypeId, KeyWord) {
    pageHelper.Init({
        url: "/Admin/Resource/GetCaseList",
        type: "POST",
        pageDiv: "#CasePage",
        data:
        {
            CollegeId:CollegeId,
            FinancialTypeId: TypeId,
            KeyWords: KeyWord,
            rId: Math.random()
        },
        bind: function (data) {
            var html = "";
            $(data.Data).each(function (index, dom) {
                //每行html
                var trHtml = "";
                trHtml += "<tr>";
                trHtml += "<td name=\"dataNo\">{0}</td>";
                trHtml += "<td><div class=\"ellipsis\" title=\"{1}\">{1}</div></td>";
                trHtml += "<td><div class=\"ellipsis\" title=\"{2}\">{2}</div></td>";
                trHtml += "<td><div class=\"ellipsis\" title=\"{3}\">{3}</div></td>";
                trHtml += "<td><div class=\"ellipsis\" title=\"{4}\">{4}</div></td>";
                trHtml += "<td class=\"time\"><div class=\"ellipsis\">{5}</div></td>";
                trHtml += "<td class=\"operate\">";
                trHtml += "<a class=\"edit\" title=\"查看\" href=\"javascript:ViewCase({6});\">{7}</a>";
                if (dom.CollegeId != 0) {
                    trHtml += "<a class=\"edit\" title=\"下载\" href=\"javascript:ExportCase({6});\">下载</a>";
                }
                else {
                    if (dom.Status == 1) {
                        trHtml += "<a class=\"del\" title=\"屏蔽\" href=\"javascript:ChangeCaseStatus({6},2);\">屏蔽</a>";
                    }
                    else {
                        trHtml += "<a class=\"del\" title=\"发布\" href=\"javascript:ChangeCaseStatus({6},1);\">发布</a>";
                    }
                }
                trHtml += "</td>";
                trHtml += "</tr>";
                //拼接tbody
                html += StringHelper.FormatStr(trHtml,
                    ((data.PageIndex - 1) * data.PageSize + index + 1),   //0 序号
                    dom.CustomerName,                               //1 客户姓名
                    dom.IDNum,                                      //2 身份证号
                    dom.strFinancialType,                           //3 理财类型
                    dom.strCollege,                                 //4 来源
                    dom.strCreateDate,                              //5 创建日期
                    dom.Id,                                          //6 案例Id
                    dom.ViewStatus == 1 ? "查看" : "未查看"       //7 查看状态
                    );
            });
            if (data.Data == "" || data.Data == null) {
                html += "<tr><td  colspan='7'>未找到相关记录！</td>";
                $("#caseList").html(html);
            } else {
                $("#caseList").html(html);
            }
        }
    });
}

/**
 * @name 查看案例
 */
function ViewCase(id) {
    location.href = "/Admin/Resource/CaseView?Id=" + id + "&Status=View";
}

/**
 * @name 修改案例状态（屏蔽/发布）
 */
function ChangeCaseStatus(caseId, statusType) {
    var title = "确定屏蔽该案例？";
    if (statusType == 1)
    {
        title = "确定发布该案例？";
    }
    // 弹出确认框
    dialogHelper.Confirm({
        content: title,
        success: function () {
            // 修改案例状态
            $.ajax({
                url: "/Admin/Resource/ChangeCaseStatus",
                type: "POST",
                async: true,
                dataType: "json",
                data:
                {
                    caseId: caseId,
                    statusType: statusType
                },
                success: function (data) {
                    dialogHelper.Success({
                        content: "操作成功！",
                        success: function () {
                            //刷新当前页
                            location.href = location.href;
                        }
                    });
                }
            });
        }
    })

}

/**
 * @name 下载习题
 */
function ExportCase(caseId) {
    location.href = "/Admin/Resource/ExportCase?CaseId=" + caseId + "&randomId=" + Math.random();

    //$.ajax({
    //    url: '/Admin/Resource/ExportCase',
    //    async: false,
    //    type: "POST",
    //    data: {
    //        CaseId: caseId,
    //    },
    //    success: function (data) {
    //        //location.href = data.Data;
    //    }
    //})
}