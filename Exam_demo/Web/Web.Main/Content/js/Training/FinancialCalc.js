/*计算天数差*/
function getDateDiff(date1, date2) {
    var arr1 = date1.split('/');
    var arr2 = date2.split('/');
    var d1 = new Date(arr1[0], arr1[1]-1, arr1[2]);
    var d2 = new Date(arr2[0], arr2[1]-1, arr2[2]);
    return (d2.getTime() - d1.getTime()) / (1000 * 3600 * 24);
}

/*x ^ y*/
function power(x, y) {
    var t = x;
    while (y-- > 1) {
        t *= x;
    }
    return t;
}

//时间格式化
function formatterDate(date) {
    var datetime = date.getFullYear()
            + "/"// "年"
            + ((date.getMonth() + 1) > 10 ? (date.getMonth() + 1) : "0"
                    + (date.getMonth() + 1))
            + "/"// "月"
            + (date.getDate() < 10 ? "0" + date.getDate() : date
                    .getDate());
    return datetime;
}



/*格式化输出,2位小数*/
function formatNum(number) {
    //保留2位小数
    var result = number.toMyFixed(2);
    if (result == "NaN")
    { return 0; }
    else { return result;}
}

function formatNum1(number) {
    //保留4位小数
    var result = number.toMyFixed(4);
    if (result == "NaN")
    { return 0; }
    else { return result; }
}




$(document).ready(function () {
    $(".container").addClass("cont-counter");
    //处理所有的日期控件
    $(".Wdate").focus(function () {
        WdatePicker({ isShowClear: false, readOnly: true, dateFmt: "yyyy/MM/dd" });
    });
    //外汇切换
    $("#ulforex li a").click(function () {
        var aId = $(this).attr("aId");
        $("#ulforex li .active").removeClass("active");
        $(this).addClass("active");

        $("#forex_buy_btnCalcDiv,#forex_sell_btnCalcDiv,#forex_exchange_btnCalcDiv,#forex_ck_btnCalcDiv").hide();

        $("div[id='" + aId + "']").show();

    });
    //存款切换
    $("#uldeposit li a").click(function () {
        var aId = $(this).attr("aId");
        $("#uldeposit li .active").removeClass("active");
        $(this).addClass("active");

        $("#hq_btnCalcDiv,#tz_btnCalcDiv,#zczq_btnCalcDiv,#lczq_btnCalcDiv").hide();

        $("div[id='" + aId + "']").show();
        //初始化页面值
      //  $("#tz_ckType").val(1).trigger('change');

    });
    //股票切换
    $("span[name='choseStock']").unbind("click").bind("click",function () {
        $(".warn-box").removeClass("warn-box");
        var divId = $(this).attr("value1");
        $("span[name='choseStock']").removeClass("active");
        $(this).addClass("active");

        $("#stock_HA_btnCalcDiv,#stock_HB_btnCalcDiv,#stock_SA_btnCalcDiv,#stock_SB_btnCalcDiv").hide();
        $("div[id='" + divId + "']").show();

    });

    //活期存款
    $("#hq_btnCalc").click(function () {
        $("#hq_result").val("");

        if (!VerificationHelper.checkFrom("hq_btnCalcDiv"))
            return;
        //为空检查
        //类型检查
        //范围检查
       


        var days = getDateDiff($("#hq_sDate").val(), $("#hq_eDate").val());

        if ((days-25) / 365 > 100)
        {
            dialogHelper.Error({
                content: "相隔时间不能超过100年"
            });
           
            return;

        }

        if (days < 0)
        {
            dialogHelper.Error({
                content: "提取日期不能小于存入日期"
            });
          //  alert("提取日期不能小于存入日期");
            return;
            //弹窗
        }
        var rate = eval($.trim($("#hq_rate").val())) / 100.00;
        var amount = $.trim($("#hq_initAmount").val());
        $("#hq_result").val(formatNum(amount * 1 + amount * days / 360 * rate));
    });
    $("#hq_btnReset").click(function () {
        $(".warn-box").remove();
        $("#hq_sDate,#hq_initAmount,#hq_rate,#hq_eDate,#hq_result").val("");
    });

    //通知存款
    $("#tz_ckType").change(function () {
        /*当储蓄类型选择一天通知存款时，显示0.8（可编辑），当储蓄类型选择七天通知存款时，显示1.35（可编辑）*/
        $("#tz_rate").val("");
        if ($("#tz_ckType").val() == 1) {
            $("#tz_rate").val("0.8");
        } else if ($("#tz_ckType").val() == 2) {
            $("#tz_rate").val("1.35");
        }
    });
    $("#tz_btnCalc").click(function () {
        $("#tz_result").val("");
        if (!VerificationHelper.checkFrom("tz_btnCalcDiv"))
            return;
        //为空检查
        //类型检查
        //范围检查

        var days = getDateDiff(formatterDate(new Date()), $("#tz_eDate").val());
        if (days < 0) {

            dialogHelper.Error({
                content: "提取日期不能小于存入日期"
            });
            
            return;
            //弹窗
        }
        /*若选择一天通知存款，则提取日期-存入日期需大于等于1，若选择七天通知存款，则提取日期-存入日期需大于等于7*/
        var type = $("#tz_ckType").val();
        if (type == 1) {
            if (days < 1) {
                dialogHelper.Error({
                    content: "提取日期减去存入日期，至少要大于等于1"
                });
               
                return false;
            }
        } else if (type == 2) {
            if (days < 7) {
                dialogHelper.Error({
                    content: "提取日期减去存入日期，至少要大于等于7"
                });
                return false;
            }
        }

        var rate = eval($.trim($("#tz_rate").val())) / 100.00;
        var amount =$.trim($("#tz_initAmount").val());
        $("#tz_result").val(formatNum(amount * 1 + amount * days / 360 * rate));
    });
    $("#tz_btnReset").click(function () {
        $(".warn-box").remove();
        $("#tz_ckType").val(1).trigger('change');
        $("#tz_initAmount,#tz_eDate,#tz_result").val("");
    });

    //整存整取
    $("#zczq_ckType").change(function () {
        /*当储蓄存期选择为三个月，半年，一年，两年，三年，五年时，年利率分别显示2.35,2.55,2.75,3.35,4,4*/
        $("#zczq_rate").val("");
        switch (eval($("#zczq_ckType").val())) {
            case 0.25:
                $("#zczq_rate").val("2.35");
                break;
            case 0.5:
                $("#zczq_rate").val("2.55");
                break;
            case 1:
                $("#zczq_rate").val("2.75");
                break;
            case 2:
                $("#zczq_rate").val("3.35");
                break;
            case 3:
                $("#zczq_rate").val("4");
                break;
            case 5:
                $("#zczq_rate").val("4");
                break;
            default:
        }
    });
    $("#zczq_btnCalc").click(function () {
        if (!VerificationHelper.checkFrom("zczq_btnCalcDiv"))
            return;
        //为空检查
        //类型检查
        //范围检查

        var years = $.trim($("#zczq_ckType").val());
        var rate = eval($.trim($("#zczq_rate").val())) / 100.00;
        var amount = $.trim($("#zczq_initAmount").val());
        $("#zczq_result").val(formatNum(amount * 1 + amount * years * rate));
    });
    $("#zczq_btnReset").click(function () {
        $(".warn-box").remove();
        $("#zczq_ckType").val(0.25).trigger('change');
        $("#zczq_sDate,#zczq_initAmount,#zczq_result").val("");
    });

    //零存整取
    $("#lczq_ckType").change(function () {
        /*当储蓄存期选择为一年，三年，五年时，年利率分别显示2.35,2.55,2.55*/
        $("#lczq_rate").val("");
        switch (eval($("#lczq_ckType").val())) {
            case 1:
                $("#lczq_rate").val("2.35");
                break;
            case 3:
                $("#lczq_rate").val("2.55");
                break;
            case 5:
                $("#lczq_rate").val("2.55");
                break;
            default:
        }
    });
    $("#lczq_btnCalc").click(function () {

        $("#lczq_result").val("");
        $("#lczq_initSum").val("");

        if (!VerificationHelper.checkFrom("lczq_btnCalcDiv"))
            return;
        //为空检查
        //类型检查
        //范围检查
        
        var years = eval($.trim($("#lczq_ckType").val()));
        var rate = eval($.trim($("#lczq_rate").val())) / 100.00;
        var amount = eval($.trim($("#lczq_initAmount").val()));

        
        _ajaxhepler({//要用到FV公式，调用后台
            url: "/CompetitionUser/Calculator/RetailLump",
            type: "POST",
            async: true,
            data: {
                rate: rate,
                nper: years * 12,
                amount: -amount
            },
            success: function (data) {
                var sumSave = amount * years * 12;
                var SumInterest=data
                $("#lczq_result").val(formatNum(SumInterest));
                $("#lczq_initSum").val(formatNum(sumSave));
            }
        });      
    });
    $("#lczq_btnReset").click(function () {
        $(".warn-box").remove();
        $("#lczq_ckType").val(1).trigger('change');
        $("#lczq_sDate,#lczq_initAmount,#lczq_initSum,#lczq_result").val("");
    });

    //贷款
    $("#dk_btnCalc").click(function () {

        $("#dk_list").html("");
        $("#dk_sumInterest,#dk_sumPay").val("");

        if (!VerificationHelper.checkFrom("dk_btnCalcDiv"))
            return;
        //为空检查
        //类型检查
        //范围检查
      //  $("#loadtable1").attr({ "height": "500px", " overflow-y": "scroll", " overflow-x": "hidden" });
        $("#loadtable1").attr("style", "max-height:500px;overflow-y:scroll;");
        var months = eval($.trim($("#dk_monthCount").val()));
        var rate = eval($.trim($("#dk_rate").val())) / 100.00 / 12.00;
        var amount = eval($.trim($("#dk_initAmount").val()));
        var type = $("#dk_ckType").val();


        var list = [];//期次详情列表【利息，本金，剩余本金】
        var SumInterest = 0;//总利息
        var SumPay = 0;//总
        var eachTermSum = 0;//每期的本息和
        var eachTermAmount = 0;//每期本金
        var eachTermInterest = 0;//每期利息
        var lastRemain = amount;//上期剩余本金
        if (type == "2") {
            eachTermSum = amount * rate * power(1 + rate, months) / (power(1 + rate, months) - 1);

            SumPay = eachTermSum * months;
            SumInterest = SumPay - amount;

            for (var i = 0; i < months ; i++) {
                eachTermInterest = lastRemain * rate;
                eachTermAmount = eachTermSum - eachTermInterest;
                lastRemain = lastRemain - eachTermAmount;
                list.push([eachTermInterest, eachTermAmount, lastRemain]);
                //eachTermSum = eachTermSum;
                //eachTermInterest = [amount - e]

                //lastRemain=amount-
            }
        } else if (type == "1") {
            //等额本金
            eachTermAmount = amount / months;

            for (var i = 0; i < months ; i++) {
                eachTermInterest = lastRemain * rate;
                lastRemain = lastRemain - eachTermAmount;
                list.push([eachTermInterest, eachTermAmount, lastRemain]);

                SumPay += eachTermInterest + eachTermAmount;
                SumInterest += eachTermInterest;
            }
        }

        var tbodyStr = "";
        for (var i = 0; i < list.length; i++) {
            tbodyStr += "<tr>";
            tbodyStr += "<td>" + eval(i + 1) + "</td>";
            tbodyStr += "<td>" + formatNum(eval(list[i][0] + list[i][1])) + "</td>";
            tbodyStr += "<td>" + formatNum(eval(list[i][0])) + "</td>";
            tbodyStr += "<td>" + formatNum(eval(list[i][1])) + "</td>";
            tbodyStr += "<td>" + formatNum(eval(list[i][2])) + "</td>";
            tbodyStr += "</tr>";

            $("#dk_list").html(tbodyStr);
        }

        $("#dk_sumInterest").val(formatNum(SumInterest));
        $("#dk_sumPay").val(formatNum(SumPay));
    });
    $("#dk_btnReset").click(function () {
        $(".warn-box").remove();
        $("#loadtable1").removeAttr("style");
        $("#dk_ckType").val(2);
        $("#dk_list").html("");
        $("#dk_initAmount,#dk_monthCount,#dk_rate,#dk_sumInterest,#dk_sumPay").val("");
    });

    //沪市A股
    $("#stock_HA_btnCalc").click(function () {
        $("#stock_HA_exchange,#stock_HA_tax,#stock_HA_commission,#stock_HA_sumFee,#stock_HA_sumProfit,#stock_HA_sumProfitRate").val("");
        if (!VerificationHelper.checkFrom("stock_HA_btnCalcDiv"))
            return;
        //为空检查
        //类型检查
        //范围检查
        var idParam = "#stock_HA";
        CalcStock(idParam, 5, true, false);
    });
    $("#stock_HA_btnReset").click(function () {
        $(".warn-box").remove();
        $("#stock_HA_commissionRate").val(0.3);
        $("#stock_HA_taxRate").val(0.1);
        $("#stock_HA_exchangeRate").val(1);
        $("#stock_HA_buyPrice,#stock_HA_buyQuantity,#stock_HA_sellPrice,#stock_HA_sellQuantity").val("");
        $("#stock_HA_exchange,#stock_HA_tax,#stock_HA_commission,#stock_HA_sumFee,#stock_HA_sumProfit,#stock_HA_sumProfitRate").val("");
    });

    //沪市B股
    $("#stock_HB_btnCalc").click(function () {
        $("#stock_HB_settlement,#stock_HB_tax,#stock_HB_commission,#stock_HB_sumFee,#stock_HB_sumProfit,#stock_HB_sumProfitRate").val("");
        if (!VerificationHelper.checkFrom("stock_HB_btnCalcDiv"))
            return;
        //为空检查
        //类型检查
        //范围检查
        var idParam = "#stock_HB";
        CalcStock(idParam, 1, false, true);
    });
    $("#stock_HB_btnReset").click(function () {
        $(".warn-box").remove();
        $("#stock_HB_commissionRate").val(0.1);
        $("#stock_HB_taxRate").val(0.1);
        $("#stock_HB_settlementRate").val(0.05);
        $("#stock_HB_buyPrice,#stock_HB_buyQuantity,#stock_HB_sellPrice,#stock_HB_sellQuantity").val("");
        $("#stock_HB_settlement,#stock_HB_tax,#stock_HB_commission,#stock_HB_sumFee,#stock_HB_sumProfit,#stock_HB_sumProfitRate").val("");
    });

    //深市A股
    $("#stock_SA_btnCalc").click(function () {
        $("#stock_SA_tax,#stock_SA_commission,#stock_SA_sumFee,#stock_SA_sumProfit,#stock_SA_sumProfitRate").val("");
        if (!VerificationHelper.checkFrom("stock_SA_btnCalcDiv"))
            return;
        //为空检查
        //类型检查
        //范围检查
        var idParam = "#stock_SA";
        CalcStock(idParam, 5, false, false);
    });
    $("#stock_SA_btnReset").click(function () {
        $(".warn-box").remove();
        $("#stock_SA_commissionRate").val(0.3);
        $("#stock_SA_taxRate").val(0.1);
        $("#stock_SA_buyPrice,#stock_SA_buyQuantity,#stock_SA_sellPrice,#stock_SA_sellQuantity").val("");
        $("#stock_SA_tax,#stock_SA_commission,#stock_SA_sumFee,#stock_SA_sumProfit,#stock_SA_sumProfitRate").val("");
    });

    //深市B股
    $("#stock_SB_btnCalc").click(function () {
        $("#stock_SB_settlement,#stock_SB_tax,#stock_SB_commission,#stock_SB_sumFee,#stock_SB_sumProfit,#stock_SB_sumProfitRate").val("");
        //为空检查
        if (!VerificationHelper.checkFrom("stock_SB_btnCalcDiv"))
            return;
        //类型检查
        //范围检查
        var idParam = "#stock_SB";
        CalcStock(idParam, 5, false, true);
    });
    $("#stock_SB_btnReset").click(function () {
        $(".warn-box").remove();
        $("#stock_SB_commissionRate").val(0.1);
        $("#stock_SB_taxRate").val(0.1);
        $("#stock_SB_settlementRate").val(0.05);
        $("#stock_SB_buyPrice,#stock_SB_buyQuantity,#stock_SB_sellPrice,#stock_SB_sellQuantity").val("");
        $("#stock_SB_settlement,#stock_SB_tax,#stock_SB_commission,#stock_SB_sumFee,#stock_SB_sumProfit,#stock_SB_sumProfitRate").val("");
    });

    

    // //开放式基金申赎
    $(".btn-pop").click(function () {
        $(".btn-pop").removeClass("btn-blue");
        $(this).addClass();
        $("#fund_rate").val("btn-blue");
        switch (eval($("#fund_Type").attr("value1"))) {
            case 1:
                $("#fund_rate").val("1.2");
                break;
            case 2:
                $("#fund_rate").val("0.5");
                break;
            case 3:
                $("#fund_rate").val("1.2");
                break;
            default:
        }

    })

    $("#btnfund1").click(function () {
        $(".btn-active").removeClass("btn-active");
        $(this).addClass("btn-active");
        $("#fund_rate").val("1.2");
    });
    $("#btnfund2").click(function () {
        $(".btn-active").removeClass("btn-active");
        $(this).addClass("btn-active");
        $("#fund_rate").val("0.5");
    })
    $("#btnfund3").click(function () {
        $(".btn-active").removeClass("btn-active");
        $(this).addClass("btn-active");
        $("#fund_rate").val("1.2");
    })

    $("#fund_btnCalc").click(function () {
        $("#fund_SumPay,#fund_SumFee").val("");
        if (!VerificationHelper.checkFrom("fund_btnCalcDiv"))
            return;
        //为空检查
        //类型检查
        //范围检查
        var price = $.trim($("#fund_Price").val());
        var quantity = $.trim($("#fund_Quantity").val());
        var rate = eval($.trim($("#fund_rate").val())) / 100.00;

        var sumFee = price * quantity * rate;
        var sumPay = price * quantity;

        $("#fund_SumPay").val(formatNum(sumPay));
        $("#fund_SumFee").val(formatNum(sumFee));
    });
    $("#fund_btnReset").click(function () {
        $(".warn-box").remove();
        $(".btn-active").removeClass("btn-active");
        $("#btnfund1").addClass("btn-active");
        $("#fund_rate").val(1.2);
        $("#fund_Price,#fund_Quantity,#fund_SumPay,#fund_SumFee").val("");
    });

    //购汇
    $("#forex_buy_Type").change(function () {
        /*
        1 美元
        2 英镑
        3 欧元
        4 日元
        5 港币
        */
        //$("#forex_buy_rate").val("");
        $("#forex_buy_Amount").addClass("IsMaxFloat IsFloat2  IsMinFloat");
        $("#forex_buy_Amount").removeClass("IsNumber IsMaxNumber").attr("msgname", "您要买入的外币金额是")
        switch (eval($("#forex_buy_Type").val())) {
            case 1:
                $("#forex_buy_Short").text("USD");
                break;
            case 2:
                $("#forex_buy_Short").text("GBP");
                break;
            case 3:
                $("#forex_buy_Short").text("EUR");
                break;
            case 4:
                $("#forex_buy_Short").text("JPY");
                $("#forex_buy_Amount").removeClass("IsMaxFloat IsFloat2  IsMinFloat");
                $("#forex_buy_Amount").addClass("IsNumber IsMaxNumber").attr("msgname", "日元");
                break;
            case 5:
                $("#forex_buy_Short").text("HKD");
                break;
            default:
        }
    });
    $("#forex_buy_btnCalc").click(function () {
        $("#forex_buy_SumPay").val("");
        if(!VerificationHelper.checkFrom("forex_buy_btnCalcDiv"))
        return;
        //为空检查
        //类型检查
        //范围检查

        var quantity = $.trim($("#forex_buy_Amount").val());
        var rate = $.trim($("#forex_buy_rate").val());

        var sumPay = quantity * rate;
       
            $("#forex_buy_SumPay").val(formatNum(sumPay));
        
    });
    $("#forex_buy_btnReset").click(function () {
        $(".warn-box").remove();
        $("#forex_buy_Type").val(1).trigger('change');
        $("#forex_buy_Amount,#forex_buy_rate,#forex_buy_SumPay").val("");
    });

    //结汇
    $("#forex_sell_Type").change(function () {
        /*
        1 美元
        2 英镑
        3 欧元
        4 日元
        5 港币
        */
        //$("#forex_sell_rate").val("");
        $("#forex_sell_Amount").addClass("IsMaxFloat IsFloat2  IsMinFloat");
        $("#forex_sell_Amount").removeClass("IsNumber IsMaxNumber").attr("msgname", "您要卖出的外币金额是")
        switch (eval($("#forex_sell_Type").val())) {
            case 1:
                $("#forex_sell_Short").text("USD");
                break;
            case 2:
                $("#forex_sell_Short").text("GBP");
                break;
            case 3:
                $("#forex_sell_Short").text("EUR");
                break;
            case 4:
                $("#forex_sell_Amount").removeClass("IsMaxFloat IsFloat2  IsMinFloat");
                $("#forex_sell_Amount").addClass("IsNumber IsMaxNumber").attr("msgname", "日元");
                $("#forex_sell_Short").text("JPY");
                break;
            case 5:
                $("#forex_sell_Short").text("HKD");
                break;
            default:
        }
    });

    $("#btnforex1").click(function () {
        $("#btnforex2").removeClass("btn-active");
        $(this).addClass("btn-active");
    });

    $("#btnforex2").click(function () {
        $("#btnforex1").removeClass("btn-active");
        $(this).addClass("btn-active");
    });

    $("#forex_sell_btnCalc").click(function () {
        //为空检查
        //类型检查
        //范围检查
        $("#forex_sell_SumPay").val("");
        if (!VerificationHelper.checkFrom("forex_sell_btnCalcDiv"))
            return;
        var quantity = $.trim($("#forex_sell_Amount").val());
        var rate = $.trim($("#forex_sell_rate").val());
        var sumPay = quantity * rate;
       
            $("#forex_sell_SumPay").val(formatNum(sumPay));
       
    });
    $("#forex_sell_btnReset").click(function () {
        $(".warn-box").remove();
        $(".btn-active").removeClass("btn-active");
        $("#btnforex1").addClass("btn-active");
        $("#forex_sell_Type").val(1).trigger('change');
        $("#forex_sell_Amount,#forex_sell_rate,#forex_sell_SumPay").val("");
    });

    //外汇间兑换
    $("#forex_exchange_OriginType").change(function () {
        /*
        1 美元
        2 英镑
        3 欧元
        4 日元
        5 港币
        */
        //$("#forex_sell_rate").val("");
        $("#forex_exchange_Amount").addClass("IsMaxFloat IsFloat2  IsMinFloat");
        $("#forex_exchange_Amount").removeClass("IsNumber IsMaxNumber").attr("msgname", "兑换数量")
        switch (eval($("#forex_exchange_OriginType").val())) {
            case 1:
                $("#forex_exchange_OriginShort").text("USD");
                break;
            case 2:
                $("#forex_exchange_OriginShort").text("GBP");
                break;
            case 3:
                $("#forex_exchange_OriginShort").text("EUR");
                break;
            case 4:
                $("#forex_exchange_Amount").removeClass("IsMaxFloat IsFloat2  IsMinFloat");
                $("#forex_exchange_Amount").addClass("IsNumber IsMaxNumber").attr("msgname", "日元兑换数量");
                $("#forex_exchange_OriginShort").text("JPY");
                break;
            case 5:
                $("#forex_exchange_OriginShort").text("HKD");
                break;
            default:
        }
    });
    $("#forex_exchange_TargetType").change(function () {
        /*
        1 美元
        2 英镑
        3 欧元
        4 日元
        5 港币
        */
        //$("#forex_sell_rate").val("");
        switch (eval($("#forex_exchange_TargetType").val())) {
            case 1:
                $("#forex_exchange_TargetShort1").text("USD");
                $("#forex_exchange_TargetShort2").text("USD");
                break;
            case 2:
                $("#forex_exchange_TargetShort1").text("GBP");
                $("#forex_exchange_TargetShort2").text("GBP");
                break;
            case 3:
                $("#forex_exchange_TargetShort1").text("EUR");
                $("#forex_exchange_TargetShort2").text("EUR");
                break;
            case 4:
                $("#forex_exchange_TargetShort1").text("JPY");
                $("#forex_exchange_TargetShort2").text("JPY");
                break;
            case 5:
                $("#forex_exchange_TargetShort1").text("HKD");
                $("#forex_exchange_TargetShort2").text("HKD");
                break;
            default:
        }
    });
    $("#forex_exchange_btnCalc").click(function () {
        //为空检查
        //类型检查
        //范围检查
        $("#forex_exchange_SumPay").val("");
        if (!VerificationHelper.checkFrom("forex_exchange_btnCalcDiv"))
            return;
        if ($("#forex_exchange_OriginType").val() == $("#forex_exchange_TargetType").val())
        {
            dialogHelper.Error({
                content: "请选择不同的外币进行兑换"
            });
            return;
        }



        var quantity = $.trim($("#forex_exchange_Amount").val());
        var rate = $.trim($("#forex_exchange_rate").val());

        var sumPay = quantity * rate;
        if ($("#forex_exchange_TargetType").val() == 4)
        { $("#forex_exchange_SumPay").val(sumPay.toFixed(0)); }
        else {
            $("#forex_exchange_SumPay").val(formatNum(sumPay));
        }
    });
    $("#forex_exchange_btnReset").click(function () {
        $(".warn-box").remove();
        $("#forex_exchange_OriginType").val(1).trigger('change');
        $("#forex_exchange_TargetType").val(1).trigger('change');
        $("#forex_exchange_TargetShort1,#forex_exchange_TargetShort2").val("USD");
        $("#forex_exchange_Amount,#forex_exchange_rate,#forex_exchange_SumPay").val("");
    });

    //外汇储蓄
    $("#forex_ck_CType").change(function () {
        /*
        1 美元
        2 英镑
        3 欧元
        4 日元
        5 港币
        */
        //$("#forex_sell_rate").val("");
        $("#forex_ck_Amount").addClass("IsMaxFloat IsFloat2  IsMinFloat")
        $("#forex_ck_Amount").removeClass("IsNumber IsMaxNumber").attr("msgname", "存款金额")
        switch (eval($("#forex_ck_CType").val())) {
            case 1:
                $("#forex_ck_Short1,#forex_ck_Short2,#forex_ck_Short3,#forex_ck_Short4").text("USD");
                break;
            case 2:
                $("#forex_ck_Short1,#forex_ck_Short2,#forex_ck_Short3,#forex_ck_Short4").text("GBP");
                break;
            case 3:
                $("#forex_ck_Short1,#forex_ck_Short2,#forex_ck_Short3,#forex_ck_Short4").text("EUR");
                break;
            case 4:
                $("#forex_ck_Amount").removeClass("IsMaxFloat IsFloat2  IsMinFloat")
                $("#forex_ck_Amount").addClass("IsNumber IsMaxNumber").attr("msgname", "日元存款金额");
                $("#forex_ck_Short1,#forex_ck_Short2,#forex_ck_Short3,#forex_ck_Short4").text("JPY");
                break;
            case 5:
                $("#forex_ck_Short1,#forex_ck_Short2,#forex_ck_Short3,#forex_ck_Short4").text("HKD");
                break;
            default:
        }
    });

    var dayss = 0;

    $(":button[name='forex_ck_Type']").click(function () {
        /*
        1活期
        2七天通知
        3一个月
        4三个月
        5六个月
        6一年
        7两年
        */
        $(":button[name='forex_ck_Type']").removeClass("btn-active");
        $(this).addClass("btn-active");

        dayss = 0;
        switch (eval($(this).attr("value1"))) {
            case 1:
            case 2:
                dayss = getDateDiff($("#forex_ck_sDate").val(), $("#forex_ck_eDate").val());
                $("#fifhiden1,#fifhiden2").show();
                $("#forex_ck_sDate,#forex_ck_eDate").addClass("IsRequired");
                break;
            case 3: dayss = 30;
                $("#fifhiden1,#fifhiden2").hide();
                $("#forex_ck_sDate,#forex_ck_eDate").removeClass("IsRequired");
                break;
            case 4: dayss = 90;
                $("#fifhiden1,#fifhiden2").hide();
                $("#forex_ck_sDate,#forex_ck_eDate").removeClass("IsRequired");
                break;
            case 5: dayss = 180;
                $("#fifhiden1,#fifhiden2").hide();
                $("#forex_ck_sDate,#forex_ck_eDate").removeClass("IsRequired");
                break;
            case 6: dayss = 360;
                $("#fifhiden1,#fifhiden2").hide();
                $("#forex_ck_sDate,#forex_ck_eDate").removeClass("IsRequired");
                break;
            case 7: dayss = 720;
                $("#fifhiden1,#fifhiden2").hide();
                $("#forex_ck_sDate,#forex_ck_eDate").removeClass("IsRequired");
                break;
            default:
        }
    });
    $("#forex_ck_btnCalc").click(function () {
        $("#forex_ck_SumInterest").val("");
        $("#forex_ck_SumAmount").val("");
        if (!VerificationHelper.checkFrom("forex_ck_btnCalcDiv"))
            return;
        if ($("#btnforex_ck_Type").hasClass("btn-active"))
        {
            dayss = getDateDiff($("#forex_ck_sDate").val(), $("#forex_ck_eDate").val());
            $("#fifhiden1,#fifhiden2").show();
            $("#forex_ck_sDate,#forex_ck_eDate").addClass("IsRequired");
        }
        //有个问题，7天存款是和现在时间比较还是和现在时间比较
        if ($("#btnforex_ck_Type2").hasClass("btn-active"))
        {
            dayss = getDateDiff($("#forex_ck_sDate").val(), $("#forex_ck_eDate").val());
            if (dayss < 7)
            {
                dialogHelper.Error({
                    content: "取款日期减去存款日期，至少要大于等于7"
                });
                return;
            }
           

        }

        if (dayss < 0) {
            dialogHelper.Error({
                content: "取款日期不能小于存款日期"
            });
            return;
        }

        if ((dayss-25) / 365 > 100)
        {
            dialogHelper.Error({
                content: "相隔时间不能超过100年"
            });
            return;

        }

        var amount = $.trim($("#forex_ck_Amount").val()) * 1.00;
        var rate = eval($.trim($("#forex_ck_rate").val())) / 100.00;

        var SumInterest = amount * rate * dayss / 360.00;
        if ($("#forex_ck_CType").val() == 4)
        {
            $("#forex_ck_SumInterest").val(SumInterest.toFixed(0));
            $("#forex_ck_SumAmount").val((SumInterest + amount).toFixed(0));
        }
        else {
            $("#forex_ck_SumInterest").val(formatNum(SumInterest));
            $("#forex_ck_SumAmount").val(formatNum(SumInterest + amount));
        }
    });
    $("#forex_ck_btnReset").click(function () {
        $(".warn-box").remove();
       // $(".btn-active").removeClass("btn-active");
       // $("#btnforex_ck_Type").addClass("btn-active");
       //$("#fifhiden1,#fifhiden2").show();
        $("#forex_ck_Amount,#forex_ck_rate,#forex_ck_sDate,#forex_ck_eDate,#forex_ck_SumInterest,#forex_ck_SumAmount").val("");
    });
});

/*计算股票收益  idParam：类型参数 commissionLimit:佣金下限 hasExchange：是否计算过户费 hasSettlement：是否计算结算费 */
function CalcStock(idParam, commissionLimit, hasExchange, hasSettlement) {

    var buyPrice = $.trim($(idParam + "_buyPrice").val());
    var buyQuantity =$.trim($(idParam + "_buyQuantity").val());

    var sellPrice = $.trim($(idParam + "_sellPrice").val());
    var sellQuantity = $.trim($(idParam + "_sellQuantity").val());

    if (parseInt(sellQuantity) >parseInt(buyQuantity))
    {
        dialogHelper.Error({
            content: "股票卖出数量不能大于股票买入数量。"
        });
        return;
    }

    var commissionRate = eval($.trim($(idParam + "_commissionRate").val())) / 100.00;
    var taxRate = eval($.trim($(idParam + "_taxRate").val())) / 100.00;


    var settlementRate = 0;
    if (hasSettlement) {
        settlementRate = eval($.trim($(idParam + "_settlementRate").val())) / 100.00;
    }

    var exchangeRate = 0;
    if (hasExchange) {
        exchangeRate = eval($.trim($(idParam + "_exchangeRate").val()));
    }

    //过户费（沪市A股 最低收取1元）
    var exchange = 0;
    if (hasExchange) {
        exchange = (formatNum((buyQuantity / 1000.00)) >= 1.00 ? formatNum((buyQuantity / 1000.00)) * exchangeRate : 1.00)
       + (formatNum((sellQuantity / 1000.00)) >= 1.00 ? formatNum((sellQuantity / 1000.00)) * exchangeRate : 1);
    }
 
    //结算费
    var settlement = 0;
    if (hasSettlement) {
        settlement = buyQuantity * buyPrice * settlementRate + sellQuantity * sellPrice * settlementRate;
    }

    var tax = sellPrice * sellQuantity * taxRate;
    var commission = ((buyQuantity * buyPrice * commissionRate) > commissionLimit ? (buyQuantity * buyPrice * commissionRate) : commissionLimit)
        + ((sellQuantity * sellPrice * commissionRate) > commissionLimit ? (sellQuantity * sellPrice * commissionRate) : commissionLimit);

    var sumFee = exchange + tax + commission + settlement;
    var sumProfit = (sellPrice - buyPrice) * buyQuantity - sumFee;
    var sumProfitRate = sumProfit / (buyPrice * buyQuantity) * 100.00;

    //过户费
    if (hasExchange) {
        $(idParam + "_exchange").val(formatNum(exchange));
    }
    ////结算费
    if (hasSettlement) {
        $(idParam + "_settlement").val(formatNum(settlement));
    }
    //印花费
    $(idParam + "_tax").val(formatNum(tax));
   // 券商佣金
    $(idParam + "_commission").val(formatNum(commission));
    //税费合计
    $(idParam + "_sumFee").val(formatNum(sumFee));
    //总体投资损益
    $(idParam + "_sumProfit").val(formatNum(sumProfit));
    //总体盈亏比例
    $(idParam + "_sumProfitRate").val(formatNum(sumProfitRate));

}