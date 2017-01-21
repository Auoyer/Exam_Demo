$(function () {
    SelectSchool();

    // 题库列表
    GetQuestionLib(-1);
    // 搜索
    //$("#btnSearch").unbind("click").click(function () {
    //    //获取查询条件
    //    var CollegeId = "";
    //    var college = $("#selectCollegeType").children('option:selected').attr("tag");
    //    CollegeId = college;
    //    GetQuestionLib(CollegeId);
    //});
});

// 下拉框的改变事件
function SelectChange(valu) {
    var CollegeId = "";
    var college = $("#selectCollegeType").children('option:selected').attr("tag");
    CollegeId = college;
    GetQuestionLib(CollegeId);
}

/**
 * @name 获取题库列表
 */
function GetQuestionLib(collegeId) {
    pageHelper.Init({
        url: "/Admin/Resource/GetQuestionLibList",
        type: "POST",
        pageDiv: "#QuestionLibPage",
        data:
        {
            CollegeId: collegeId,
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
                trHtml += "<td class=\"time\"><div class=\"ellipsis\">{4}</div></td>";
                trHtml += "<td class=\"operate\">";
                if (dom.Count == 0) {
                    trHtml += "------";
                }
                else {
                    trHtml += "<a class=\"edit\" title=\"{7}\" href=\"javascript:ViewQuestion({5},{6},{2});\">{7}</a>";
                    if (dom.CollegeId != 0) {
                        trHtml += "<a class=\"edit\" title=\"下载\" href=\"javascript:ExportQuestion({5},{6},{2});\">下载</a>";
                    }
                }

                trHtml += "</td>";
                trHtml += "</tr>";
                //拼接tbody
                html += StringHelper.FormatStr(trHtml,
                    ((data.PageIndex - 1) * data.PageSize + index + 1),   //0 序号
                    dom.StrStructType,                              //1 习题类型
                    dom.Count,                                      //2 习题数量
                    dom.strCollege,                                 //3 习题来源
                    dom.strLastTime,                                //4 创建日期
                    dom.StructType,                                 //5 习题类型
                    dom.CollegeId,                                   //6 学校Id
                    dom.ViewStatus == 1 ? "查看" : "未查看"          //7 查看状态
                    );
            });
            $("#QuestionLibList").html(html);
        }
    });
}

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
 * @name 查看习题
 */
function ViewQuestion(structType, collegeId,count) {
    location.href = "/Admin/Resource/QuestionList?TheoryChapterId=0&StructType=" + structType + "&CollegeId=" + collegeId;
}

/**
 * @name 下载习题
 */
function ExportQuestion(structType, collegeId, count) {
    $.ajax({
        url: '/Admin/Resource/ExportQuestion',
        async: false,
        type: "POST",
        data: {
            StructType: structType,
            CollegeId: collegeId,
            Count: count,
        },
        success: function (data) {
                location.href = data.Data;
        }
    })
}