//*******************************
//考生端-实训考试-财务分析
//*******************************

var URL = "";
$(function () {
    //客户信息是否保存
    IsProposalSave();

    //获取URL参数
    //获取URL参数
    var ProposalId = $.getUrlParam("ProposalId");
    var TrainExamId = $.getUrlParam("TrainExamId");
    var StuCustomerId = $.getUrlParam("StuCustomerId");
    URL = "?TrainExamId=" + TrainExamId + "&ProposalId=" + ProposalId + "&StuCustomerId=" + StuCustomerId;

    //加载
    GetFinancialRatiosList()

    //把值赋给defaultVal作为原值
    SaveDefaultValueCommon("FinancialRatios");

    $("#FinancialRatios #btnSave").live("click", function () {
        //添加数据
        AddFinancialRatios(0);
    });
    //同时绑定下一页事件
    $("#FinancialRatios #btnNext").live("click", function () {
        var fag = AddFinancialRatios(1);
       
        
    });
    //同时绑定上一页事件
    $("#FinancialRatios #btnPrev").live("click", function () {
        AddFinancialRatios(2);
     
    });
});

//获取得到现金流量数据
function GetFinancialRatiosList() {

    //获取URL参数
    var ProposalId = $.getUrlParam("ProposalId");
    if(ProposalId==null){
        ProposalId = 0;
    }
    //隐藏域
    $("#ProposalId").val(ProposalId);


    _ajaxhepler({
            url: "/CompetitionUser/FinancialRatios/GetFRList",
            type: "POST",
            async: false,
            dataType: "json",
            data:
            {
                ProposalId: ProposalId
            },
            success: function (data) {
                var li = data.list;
                if (li != null) {
                    var list = li;
                    //负债比率（负债合计/资产合计）           
                    var bearScale = list.TotalLoan / list.TotalAssets;
                    //融资比率（投资负债小计/投资资产小计）
                    var a = list.RMBFixedDeposit;
                    var b = list.ForeignCurrencyFixedDeposit;
                    var c = list.StockInvestment;
                    var d = list.BondInvestment;
                    var e = list.FundInvestment;
                    var f = list.IndustryInvestment;
                    var g = list.EstateInvestment;
                    var h = list.PolicyInvestment;
                    var i = list.OtherInvestment;
                    var jinancingScale = (list.FinancialLoan + list.IndustryInvestmentLoan + list.EstateInvestmentLoan + list.OtherInvestmentLoan) / (a + b + c + d + e + f + g + h + i)
                    //投资性资产权数（投资资产小计/资产合计）
                    var invest = (a + b + c + d + e + f + g + h + i) / list.TotalAssets;
                    //流动性资产权数（流动资产小计/资产合计）
                    var flowMoney = (list.Cash + list.RMBDeposit + list.OtherAsset) / list.TotalAssets;

                    if ((a + b + c + d + e + f + g + h + i)==0) {
                        $("#jinancingScale").val("无法统计该指标");
                    } else {
                        $("#jinancingScale").val(jinancingScale.toMyFixed(2)*1000/10);
                    }

                    if (list.TotalAssets==0) {
                        $("#invest").val("无法统计该指标");
                        $("#flowMoney").val("无法统计该指标");
                        $("#bearScale").val("无法统计该指标");
                    } else {
                        $("#invest").val(invest.toMyFixed(2)*1000/10);
                        $("#flowMoney").val(flowMoney.toMyFixed(2)*1000/10);
                        $("#bearScale").val(bearScale.toMyFixed(2)*1000/10);
                    }                                                          
                }


                var li2 = data.list2;
                if (li2 != null) {
                    var list2 = li2;

                    //支出比率（生活支出小计+理财支出小计）/（工作收入小计+理财收入小计）
                    var licai = list2.InterestExpense + list2.InsuranceExpense + list2.OtherFinanceExpense;
                    var liftpay = (list2.FamilyExpense + list2.ChildExpense + list2.OtherExpense + licai);
                    var work = (list2.JobIncome + list2.EndowmentInsurance + list2.MedicalInsurance + list2.HousingFund + list2.Interest + list2.CapitalGains + list2.OtherIncome);
                   // var payScale = liftpay / work;
                    var payScale = ((list2.LiveExpense01 + list2.InvestmentExpense01) / (list2.WorkIncome01 + list2.InvestmentIncome01)).toMyFixed(2);
                    //财务负担率：理财支出小计/（工作收入小计+理财收入小计）       
                  //  var finance = licai / work;
                    var finance =(list2.InvestmentExpense01 / (list2.WorkIncome01 + list2.InvestmentIncome01)).toMyFixed(2);
                    //自由储蓄率：自由储蓄/（工作收入小计+理财收入小计）
                    //	工作储蓄=工作收入－生活支出
                    var workExist = (list2.JobIncome + list2.EndowmentInsurance + list2.MedicalInsurance + list2.HousingFund + list2.Interest) - (list2.FamilyExpense + list2.ChildExpense + list2.OtherExpense)
                    //// 理财储蓄=理财收入－理财支出
                   var licaiExist = (list2.Interest + list2.CapitalGains + list2.OtherIncome) - (licai)
                    ////自由储蓄
                   var freedom = (workExist + licaiExist) - (list2.EndowmentInsurance + list2.HousingFund);
                    //自由储蓄率 ：自由储蓄/（工作收入小计+理财收入小计）
                   // var FreedomScale = freedom / (work);
                   var FreedomScale = (list2.FreeMoney / (list2.WorkIncome01 + list2.InvestmentIncome01)).toMyFixed(2);
                  // var FreedomScale = Division(list2.FreeMoney/)

                    if (work == 0) {
                        $("#payScale").val("无法统计该指标");
                        $("#finance").val("无法统计该指标");
                        $("#FreedomScale").val("无法统计该指标");
                    } else {
                        $("#payScale").val(payScale*1000/10);
            
                        $("#finance").val((finance)*1000/10);
                        $("#FreedomScale").val((FreedomScale)*1000/10);
                    }                    
                }

                if (li != null && li2 != null) {
                    //净资产增长率（致富公式）:(工作储蓄+理财储蓄)/(资产合计-负债合计)
                    var a = (workExist + licaiExist);
                    var b = (list.TotalAssets - list.TotalLoan);
                    if (b == 0) {
                        $("#addScale").val(0);
                    } else {
                        var addScale = a / b;
                        $("#addScale").val(addScale.toMyFixed(2)*1000/10);
                    }

                }
               
                var li3 = data.list3;
                if (li3 != null) {
                    var n = li3;
                        //资产负债结构分析
                        $("#LiabilityAnalysis").val(n.LiabilityAnalysis);
                        //收支储蓄结构分析
                        $("#IncomeAndExpensesAnalysis").val(n.IncomeAndExpensesAnalysis);
                        //客户财务情况分析
                        $("#Analysis").val(n.Analysis);
                        //隐藏域
                        $("#Id").val(n.Id);
                   
                   
                }
            }
        });
    
}

//新增财务分析现金流量
function AddFinancialRatios(valu) {
    var fag = false;
    if (!VerificationHelper.checkFrom("FinancialRatios")) {
        return false;
    } else {
        var ProposalId = $("#ProposalId").val()
        if (ProposalId != 0) {

            var obj = new Object();
            obj["Id"] = $("#Id").val();
            obj["ProposalId"] = ProposalId;
            obj["LiabilityAnalysis"] = $("#LiabilityAnalysis").val();
            obj["IncomeAndExpensesAnalysis"] = $("#IncomeAndExpensesAnalysis").val();
            obj["Analysis"] = $("#Analysis").val();

            _ajaxhepler({
                url: "/CompetitionUser/FinancialRatios/AddFR",
                type: "POST",
                async: false,
                dataType: "json",
                data: JSON.stringify(obj),
                contentType: "application/json",
                success: function (data) {
                    GetFinancialRatiosList();
                    if (valu == 0) {
                        
                        dialogHelper.Success({
                            content: "保存成功！", success: function () {
                                //刷新当前页
                                window.location.reload();
                                //把值赋给defaultVal作为原值
                                SaveDefaultValueCommon("FinancialRatios");
                            }
                        });

                    } else if(valu==1){
                        window.location.href = "/CompetitionUser/CashPlan/Index" + URL;
                    } else if (valu == 2) {
                        window.location.href = "/CompetitionUser/CashFlow/Index" + URL;
                    }
                }
            });
        } else {
            dialogHelper.Error({ content: "请先添加客户信息！", success: function () { } });
            fag = true;
        }
    }
    return fag;
}