//*******************************
//考生端-基金产品
//*******************************

$(function () {
    $("#btnSearch").unbind("click").click(function () {
        //若为提示语则不进行查询
        $("#txtKeyword").val($.trim($("#txtKeyword").val()));
        if ($("#txtKeyword").val() == $("#txtKeyword").attr("DefaultValue")) {
            $("#txtKeyword").val($("#txtKeyword").attr("DefaultValue")).css("color", "#c8c8c8");
            GetList("");
            return;
        }

        var key = $("#txtKeyword").val().replace(/[<>&"]/g, function (c) { return { '<': '&lt;', '>': '&gt;', '&': '&amp;', '"': '&quot;', '"': '&quot;', "'": '&prime;', "'": '&prime;' }[c]; });
        GetList(key);
    });
    //$("#txtKeyword").unbind("focus").focus(function () {

    //    $("#txtKeyword").val("").css("color", "black");
    //});
    var keyword = $.getUrlParam("keyword");
    if (keyword == "" || keyword == null || keyword == undefined || keyword == "产品/代码/类型")
    { keyword = "" } else { $("#txtKeyword").val(keyword).css("color", "black"); }
    GetList(keyword);
});

/**
 * @name 获取列表
 */
function GetList(KeyWord) {
    pageHelper.Init({
        url: "/CompetitionUser/FundProduct/GetFundProductList",
        type: "POST",
        pageDiv: "#pages",
        data:
        {
            KeyWords: KeyWord,
            rId: Math.random()
        },
        bind: function (data) {
            var html = "";
            $(data.Data).each(function (index, dom) {
                //每行html
                var trHtml = "";
                trHtml += "<tr>";
                trHtml += "<td><div class=\"ellipsis\" title=\"{0}\" style=\"padding:0 5px;\">{0}</div></td>";
                trHtml += "<td><div class=\"ellipsis\" title=\"{1}\">{1}</div></td>";
                trHtml += "<td><div class=\"ellipsis\" title=\"{2}\">{2}</div></td>";
                trHtml += "<td>{3}</td>";
                trHtml += "<td>{4}</td>";
                trHtml += "<td>{5}</td>";
                trHtml += "<td class=\"operate\">";
                trHtml += "<a class=\"edit\" title=\"详情\" href=\"javascript:FundDetail({6});\">详情</a>";
                trHtml += "</td>";
                trHtml += "</tr>";
                //拼接tbody
                html += StringHelper.FormatStr(trHtml,
                    dom.FundName,                                                           //0 产品
                    dom.FundCode,                                                           //1 代码
                    dom.FundType,                                                           //2 类型
                    //3 近一年收益率
                    dom.FundType.indexOf("货币") > -1 ? dom.YearlyEarningsRate.toMyFixed(2) + "%": (dom.YearlyEarningsRate * 100).toMyFixed(2) + "%",
                    dom.NewNetValue != null ? dom.NewNetValue.toMyFixed(2) : "——",        //4 最新净值
                    dom.TotalNewValue != null ? dom.TotalNewValue.toMyFixed(2) : "——",    //5 累计净值
                    dom.FundId                                                              //6 Id
                    );
            });
            $("#FundProductList").html(html);
        }
    });
}

/**
 * @name 跳转到详细页
 */
function FundDetail(id) {
    location.href = "/CompetitionUser/FundProduct/Detail?Id=" + id + "&keyword=" + escape($.trim($("#txtKeyword").val()));
}


