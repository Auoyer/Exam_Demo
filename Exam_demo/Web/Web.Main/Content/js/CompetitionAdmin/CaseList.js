$(function () {
    // 理财类型下拉菜单
    selectHelper.GetSelect({
        url: "/CompetitionAdmin/Resource/GetFinancialTypeList",
        Id: "#selectFinancialType",
        value: "全部"
    });
    // 案例列表
    GetList("", "");
    // 搜索
    $("#btnSearch").unbind("click").click(function () {
        //获取查询条件
        var TypeId = "";
        var KeyWord = "";
        var type = $("#selectFinancialType").val();
        if (parseInt(type) != 0) {
            TypeId = type;
        }
        var key = $.trim($("#txtKeyWord").val()).replace(/[<>&"]/g, function (c) { return { '<': '&lt;', '>': '&gt;', '&': '&amp;', '"': '&quot;', '"': '&quot;', "'": '&prime;', "'": '&prime;' }[c]; });
        //if (key == "客户姓名/身份证号")
        //{ key = ""; }
        if (key != null && key != "" && key.length > 0) {
            KeyWord = key;
        }
        GetList(TypeId, KeyWord);
    });

    //$("#txtKeyWord").unbind("focus").focus(function () {

    //    $("#txtKeyWord").val("").css("color", "black");
    //});
});

/**
 * @name 获取案例列表
 */
function GetList(TypeId, KeyWord) {
    pageHelper.Init({
        url: "/CompetitionAdmin/Resource/GetCaseList",
        type: "POST",
        pageDiv: "#CasePage",
        data:
        {
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
                trHtml += "<a class=\"edit\" title=\"查看\" href=\"javascript:ViewCase({6});\">查看</a>";
                if (dom.CollegeId != 0) {
                    trHtml += "<a class=\"edit\" title=\"编辑\" href=\"javascript:EditCase({6});\">编辑</a>";
                    trHtml += "<a class=\"edit\" title=\"删除\" href=\"javascript:DelCase({6});\">删除</a>";
                }
                trHtml += "</td></tr>";
                //拼接tbody
                html += StringHelper.FormatStr(trHtml,
                    ((data.PageIndex - 1) * data.PageSize + index + 1),   //0 序号
                    dom.CustomerName,                               //1 客户姓名
                    dom.IDNum,                                      //2 身份证号
                    dom.strFinancialType,                           //3 理财类型
                    dom.strCollege,                                 //4 来源
                    dom.strCreateDate,                              //5 创建日期
                    dom.Id                                          //6 Id
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
 * @name 修改案例
 */
function EditCase(id) {
    var flg = CheckCaseByUsed(id);
    if (flg) {
        dialogHelper.Error({
            content: "不能编辑，该案例已被待发布的竞赛引用"
        });
        return;
    }
    location.href = "/CompetitionAdmin/Resource/CaseView?Id=" + id + "&Status=Edit";
}

/**
 * @name 查看案例
 */
function ViewCase(id) {
    location.href = "/CompetitionAdmin/Resource/CaseView?Id=" + id + "&Status=View";
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
                url: "/CompetitionAdmin/Resource/ChangeCaseStatus",
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
 * @name 删除案例
 */
function DelCase(id) {
    //弹出确认框
    dialogHelper.Confirm({
        content: "确定删除该案例？",
        success: function () {
            //var flg = CheckCaseByUsed(id);
            //if (flg) {
            //    dialogHelper.Error({
            //        content: "不能删除，该案例已被待发布的竞赛引用"
            //    });
            //    return;
            //}

            $.ajax({
                url: "/CompetitionAdmin/Resource/DelCase",
                type: "POST",
                async: true,
                dataType: "json",
                data:
                {
                    Id: id,
                    rId: Math.random()
                },
                success: function (data) {
                    dialogHelper.Success({
                        content: "删除成功！",
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

//检查案列是否被待发布的竞赛引用
function CheckCaseByUsed(Id) {
    var Flag = false;

    $.ajax({
        url: "/CompetitionAdmin/Resource/CheckCaseInUnpublishMatch",
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
