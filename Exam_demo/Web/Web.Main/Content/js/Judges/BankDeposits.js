$(function () {
    GetBankDepositslist("");
    $("#btnSearch").click(function () {
        var keywords = $.trim($("#txtkeyword").val()).replace(/[<>&"]/g, function (c) { return { '<': '&lt;', '>': '&gt;', '&': '&amp;', '"': '&quot;', '"': '&quot;', "'": '&prime;', "'": '&prime;' }[c]; });
        if (keywords == "银行名称") { keywords = ""; }
        GetBankDepositslist(keywords);

    });

    //$("#txtkeyword").unbind("focus").focus(function () {

    //    $("#txtkeyword").val("").css("color","black");
    //});

});

function GetBankDepositslist(keywords)
{
    pageHelper.Init({
        url: "/CompetitionJudges/BankDeposits/GetBankDepositslist",
        type: "POST",
        pageDiv: "#pages",
        data:
        {
            keywords: keywords,
            rId: Math.random(),
        },
        bind: function (data) {
            var html = "";
            $(data.Data).each(function (index, dom) {
                //每行html
                var trHtml = "";
                trHtml += "<tr>";
                trHtml += "<td title=\"{0}\" style=\"padding:0 5px;\" class=\"ellipsis\" >{0}</td>";
                trHtml += "<td title=\"{1}\">{1}</td>";
                trHtml += "<td title=\"{2}\">{2}</td>";
                trHtml += "<td title=\"{3}\">{3}</td>";
                trHtml += "<td title=\"{4}\">{4}</td>";
                trHtml += "<td title=\"{5}\">{5}</td>";
                trHtml += "<td title=\"{6}\">{6}</td>";
                trHtml += "<td title=\"{7}\">{7}</td>";
                trHtml += "</tr>";

                //拼接tbody
                html += StringHelper.FormatStr(trHtml,
                    dom.BankName,
                    dom.DemandDeposit.toMyFixed(3),
                    dom.ThreeMonth.toMyFixed(3),
                    dom.SixMonth.toMyFixed(3),
                    dom.Year.toMyFixed(3),
                    dom.TwoYear.toMyFixed(3),
                    dom.ThreeYear.toMyFixed(3),
                    dom.FiveYear.toMyFixed(3)
                    );
            });
            $("#bankProducelist").html(html);
        }
    });
}