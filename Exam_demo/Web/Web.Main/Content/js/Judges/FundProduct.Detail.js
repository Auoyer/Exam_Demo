$(function () {
    //获取URL参数
    var Id = $.getUrlParam("Id");
    //获取详情
    if (Id != null && Id != "" && Id != undefined) {
        GetFundProduct(Id);
    }

    var keyword = $.getUrlParam("keyword");

    $("#btnback").click(function () {
        location.href = "/CompetitionJudges/FundProduct/Index?keyword=" + escape(keyword);
    });

});

function ComeBack() {
    var keyword = $.getUrlParam("keyword");
    location.href = "/CompetitionJudges/FundProduct/Index?keyword=" + escape(keyword);
}

/**
 * 获取基金详细数据
 */
function GetFundProduct(id) {
    $.ajax({
        url: "/CompetitionJudges/FundProduct/GetFundDetail",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            Id: id,
            rId: Math.random()
        },
        success: function (data) {
            if (data != null && data != "") {
                $("#txtFundName").html(data.FundName);
                $("#txtFundCode").html(data.FundCode);
                $("#txtFundType").html(data.FundType);
                $("#txtHostingFees").html(data.HostingFees + "%");
                $("#txtPurchaseShares").html(data.PurchaseShares);
                $("#txtFundCompany").html(data.FundCompany);
                $("#hdstrNavUpdateDate").val(data.strNavUpdateDate);

                if (data.FundType == "货币型基金") {
                    $("#divNewNetValue").hide();
                    $("#txtYRName").html("7日年化收益率：");
                    $("#txtYearlyEarningsRate").html(data.YearlyEarningsRate.toMyFixed(2) + "%");
                } else {
                    $("#divNewNetValue").show();
                    $("#txtYRName").html("近一年收益率：");
                    $("#txtYearlyEarningsRate").html((data.YearlyEarningsRate * 100).toMyFixed(2) + "%");
                    $("#txtNewNetValue").html(data.NewNetValue + "（净值日期：" + data.strNavUpdateDate + "）");
                }

                if (data.FundProductDetail != null && data.FundProductDetail.length > 0) {
                    var date = $("#hdstrNavUpdateDate").val().split("/");
                    var name = "";

                    var list = new Array();
                    $(data.FundProductDetail).each(function (index, dom) {
                        var temp = dom.strUpdateDate.split("/");
                        var obj = new Array();
                        obj.push(Date.UTC(temp[0], temp[1] - 1, temp[2]));
                        obj.push(dom.YearlyEarningsRate);
                        list.push(obj);
                    });
                    if (data.FundType == "货币型基金") {
                        name = "7日年化收益率";
                    } else {
                        name = "近一年收益率";
                        $(list).each(function (index, dom) {
                            dom[1] = calcHelper.Multiplication(dom[1], 100);
                            list[index] = dom;
                        });
                    }
                    Draw(name,date, list);
                }
            }

        }
    });
}

/**
 * 绘制图表
 * @param date 数组[年，月，日]
 * @param list 图表Y轴数据
 */
function Draw(name,date, list) {
    $('#container').highcharts({
        chart: {
            zoomType: 'x',
            spacingRight: 20
        },
        title: {
            text: ''
        },
        xAxis: {
            type: 'datetime',
            labels: {
                formatter: function () {
                    var now = new Date();
                    now.setTime(this.value);
                    return now.getFullYear() + "/" + (now.getMonth() + 1) + "";
                }
            },
            title: {
                text: null
            }
        },
        yAxis: {
            title: {
                text: null
            }
        },
        tooltip: {
            dateTimeLabelFormats: {
                day: "%Y/%m/%d",
            },
            shared: true,
            valueSuffix:"%",
        },
        legend: {
            enabled: false
        },
        plotOptions: {
            line: {
                lineWidth: 1,
                marker: {
                    enabled: false
                },
                shadow: false,
                states: {
                    hover: {
                        lineWidth: 1
                    }
                },
                threshold: null
            }
        },

        series: [{
            type: 'line',
            name: name,
            data: list
        }]
    });
}


