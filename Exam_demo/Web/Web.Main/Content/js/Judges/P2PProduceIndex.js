
$(function (){
    GetP2PProduct("");
    $("#btnSearch").bind("click", function () {
        var keywords = $.trim($("#txtKeyword").val()).replace(/[<>&"]/g, function (c) { return { '<': '&lt;', '>': '&gt;', '&': '&amp;', '"': '&quot;', '"': '&quot;', "'": '&prime;', "'": '&prime;' }[c]; });
        if (keywords == "产品名称/投资领域") { keywords = "";}
        GetP2PProduct(keywords)

    });
    //$("#txtKeyword").unbind("focus").focus(function () {

    //    $("#txtKeyword").val("").css("color","black");
    //});
});

function GetP2PProduct(keywords)
{

    pageHelper.Init({
        url: "/CompetitionJudges/P2PProducet/GetP2PProduceList",
        type: "POST",
        pageDiv: "#pages",
        data:
            {
                rId: Math.random(),
               
                keywords: keywords
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
                trHtml += "</tr>";

                //拼接tbody
                html += StringHelper.FormatStr(trHtml,
                    dom.P2PName,
                    dom.InvestmentField,
                    dom.InvestmentCycle,
                    dom.StartAmount,
                    dom.EarningsRate
                    );
            });
            $("#P2PProductList").html(html);
        }
    });
}