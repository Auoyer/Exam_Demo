// 考生端-实训考试-现金流量

var par = /^[-]*\d+(\.\d+)?$/;
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
    GetCashFlowList();

    //把值赋给defaultVal作为原值
    SaveDefaultValueCommon("CashFlow");  

    //保存
    $("#CashFlow #btnSave").live("click", function () {
        //添加数据
        AddCashFlow(0);
    });
    //同时绑定下一页事件
    $("#CashFlow #btnNext").live("click", function () {

        AddCashFlow(1);
        
    });
    //同时绑定上一页事件
    $("#CashFlow #btnPrev").live("click", function () {

        AddCashFlow(2);
    });

    //生活现金流量失去焦点时计算
    $("#WorkIncome,#LiveExpense").unbind("blur").blur(function () {     
        var WorkIncome = $.trim($("#WorkIncome").val()) * 1;
        var LiveExpense = $.trim($("#LiveExpense").val()) * 1;
        
        var Num = WorkIncome - LiveExpense;
        //var par = /^\d+(\.\d+)?$/;
        if (!par.test(Num)) {
            Num = 0;
        }
        $("#lifeMoney").val((Num).toMyFixed(2));

        loadings();
    });

    //投资现金流量失去焦点时计算
    $("#InvestIncome,#Redemption,#Investment").unbind("blur").blur(function () {
       
        var InvestIncome = $.trim($("#InvestIncome").val()) * 1;
        var Redemption = $.trim($("#Redemption").val()) * 1;
        var Investment = $.trim($("#Investment").val()) * 1;
        var Num = InvestIncome + Redemption - Investment;


        if (!par.test(Num)) {
            Num = 0;
        }
        $("#investMoney").val((Num).toMyFixed(2));
        loadings();
       
    });

    //借贷现金流量失去焦点时计算
    $("#BorrowCapital,#InterestExpense,#RepaymentCapital").unbind("blur").blur(function () {       

        var BorrowCapital = $.trim($("#BorrowCapital").val()) * 1;
        var InterestExpense = $.trim($("#InterestExpense").val()) * 1;
        var RepaymentCapital = $.trim($("#RepaymentCapital").val()) * 1;
        var Num = BorrowCapital - InterestExpense - RepaymentCapital;
        //var par = /^\d+(\.\d+)?$/;
        if (!par.test(Num)) {
            Num = 0;
        }
        $("#borrowMoney").val((Num).toMyFixed(2));

        loadings();
    });

    //保障现金流量失去焦点时计算
    $("#InsuranceExpense").unbind("blur").blur(function () {
        
        var InsuranceExpense = $.trim($("#InsuranceExpense").val()) * 1;      
        
        //var par = /^\d+(\.\d+)?$/;
        if (!par.test(InsuranceExpense)) {
            InsuranceExpense = 0;
        }
        $("#InsuranceExpenses2").val(-InsuranceExpense)

        loadings();
    });

   
});
//获取得到现金流量数据
function GetCashFlowList() {
    var ProposalId = $.getUrlParam("ProposalId");

    _ajaxhepler({
        url: "/CompetitionUser/CashFlow/GetCashFlowList",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: ProposalId
        },
        success: function (datas) {
            var Redemption = 0;
            var Investment = 0;
            var BorrowCapital = 0;
            var RepaymentCapital = 0;
            var li = datas.list; //收支储蓄
            var li2 = datas.list2;//现金流量
            //  if (li.JudgeVal==true){}
            if (li2 != null) {
                var list2 = li2;
                $("#Redemption").val(list2.Redemption.toMyFixed(2));
                $("#Investment").val(list2.Investment.toMyFixed(2));
                $("#BorrowCapital").val(list2.BorrowCapital.toMyFixed(2));
                $("#RepaymentCapital").val(list2.RepaymentCapital.toMyFixed(2));
                if (li2.JudgeVal == false) {
                    $("#WorkIncome").val(li2.WorkIncome.toMyFixed(2));               //工作收入
                    $("#LiveExpense").val(li2.LiveExpense.toMyFixed(2));             //生活支出
                    $("#InvestIncome").val(li2.InvestIncome.toMyFixed(2));           //投资收益
                    $("#InsuranceExpense").val(li2.InsuranceExpense.toMyFixed(2));   //保费支出
                    $("#InterestExpense").val(li2.InterestExpense.toMyFixed(2));     //利息支出

                }

                var InsuranceExpenses = 0;
                var investMoneys = 0;
                if (li != null) {
                    InsuranceExpenses = li.InsuranceExpense;
                    //投资收益（利息收入,资本利得,其他理财收入）
                    var Interest = li.Interest;
                    var CapitalGains = li.CapitalGains;
                    var OtherIncome = li.OtherIncome;

                    investMoneys = Interest + CapitalGains + OtherIncome;
                }
                $("#investMoney").val(investMoneys.toMyFixed(2));
                $("#InsuranceExpenses2").val(InsuranceExpenses.toMyFixed(2));
                //隐藏域
                $("#Id").val(list2.Id);

                //生活现金流量
                var WorkIncome = $.trim($("#WorkIncome").val()) * 1;
                var LiveExpense = $.trim($("#LiveExpense").val()) * 1;
                var Num = WorkIncome - LiveExpense;
                $("#lifeMoney").val((Num).toMyFixed(2));

                //投资现金流量   投资收益+投资赎回-新增投资
                var InvestIncome = $.trim($("#InvestIncome").val()) * 1;
                Redemption = $.trim($("#Redemption").val()) * 1;
                Investment = $.trim($("#Investment").val()) * 1;
                var Num2 = InvestIncome + Redemption - Investment;
                $("#investMoney").val((Num2).toMyFixed(2));

                //借贷现金流量 借入本金－利息支出－还款本金
                BorrowCapital = $.trim($("#BorrowCapital").val()) * 1;
                var InterestExpense = $.trim($("#InterestExpense").val()) * 1;
                RepaymentCapital = $.trim($("#RepaymentCapital").val()) * 1;
                var Num3 = BorrowCapital - InterestExpense - RepaymentCapital;
                $("#borrowMoney").val((Num3).toMyFixed(2));

                //保障现金流量失去焦点时计算               
                var InsuranceExpense = $.trim($("#InsuranceExpense").val()) * 1;
                $("#InsuranceExpenses2").val(-InsuranceExpense.toMyFixed(2))

                //加载本期现金及现金等价物净增加额
                loadings();
            }


            var InsuranceExpense = 0;
            var investMoney = 0;

            if (li != null) {
                data = li;
                //工作收入（薪资收入,养老保险储蓄,医疗保险储蓄,住房公积金储蓄,其他工作收入）
                var JobIncome = data.JobIncome;
                var endowmentInsurance = data.EndowmentInsurance;
                var MedicalInsurance = data.MedicalInsurance;
                var HousingFund = data.HousingFund;
                var OtherJobIncome = data.OtherJobIncome;

                var WorkIncome = JobIncome + endowmentInsurance + MedicalInsurance + HousingFund + OtherJobIncome;
                //生活支出（家计支出,子女教育支出,其他支出）
                var FamilyExpense = data.FamilyExpense;
                var ChildExpense = data.ChildExpense;
                var OtherExpense = data.OtherExpense;

                var LiveExpense = FamilyExpense + ChildExpense + OtherExpense;
                //投资收益（利息收入,资本利得,其他理财收入）
                var Interest = data.Interest;
                var CapitalGains = data.CapitalGains;
                var OtherIncome = data.OtherIncome;

                var InvestIncome = Interest + CapitalGains + OtherIncome;
                //利息支出（利息支出）
                var InterestExpense = data.InterestExpense;
                //保费支出（保障型保费支出）//保障现金流量净额
                InsuranceExpense = data.InsuranceExpense;

                //生活现金流量净额：工作收入-生活支出
                var lifeMoney = WorkIncome - LiveExpense;

                //投资现金流量净额：投资收益+投资赎回-新增投资
                investMoney = InvestIncome + Redemption - Investment;

                //借贷现金流量净额: 借入本金-利息支出-还款本金
                var borrowMoney = BorrowCapital - InterestExpense - RepaymentCapital;

                //本期现金及现金等价物净增加额: ∑（生活现金流量净额，投资现金流量净额，借贷现金流量净额，保障现金流量净额）
                var Money = lifeMoney + investMoney + borrowMoney - InsuranceExpense;
                //if ( li2.JudgeVal == true) {
                $("#WorkIncome").val(WorkIncome.toMyFixed(2));
                $("#WorkIncome").attr("disabled", true);

                $("#LiveExpense").val(LiveExpense.toMyFixed(2));
                $("#LiveExpense").attr("disabled", true);

                $("#InvestIncome").val(InvestIncome.toMyFixed(2));
                $("#InvestIncome").attr("disabled", true);

                $("#InsuranceExpense").val(InsuranceExpense.toMyFixed(2));
                $("#InsuranceExpense").attr("disabled", true);


                $("#InterestExpense").val(InterestExpense.toMyFixed(2));
                $("#InterestExpense").attr("disabled", true);
                //}

                $("#InsuranceExpenses2").val(-Number(InsuranceExpense.toMyFixed(2)));

                $("#lifeMoney").val(lifeMoney.toMyFixed(2));

                $("#borrowMoney").val(borrowMoney.toMyFixed(2));

                $("#Money").val(Money.toMyFixed(2));

                $("#investMoney").val(investMoney.toMyFixed(2));
            }


        }
    });
}


//新增/修改 财务分析现金流量
function AddCashFlow(valu) {

    if ($("#WorkIncome").attr("disabled") == "disabled") {
        $("#WorkIncome").attr("maxfloat", "9999999999");
        $("#WorkIncome").removeClass("IsReg");

        
        $("#LiveExpense").attr("maxfloat", "9999999999");
        $("#LiveExpense").removeClass("IsReg");

        
        $("#InvestIncome").attr("maxfloat", "9999999999");
        $("#InvestIncome").removeClass("IsReg");
    }

    var ProposalId = $.getUrlParam("ProposalId");
    if (!VerificationHelper.checkFrom("CashFlow")) {
        return;
    } else {
        var Id = $("#Id").val();
        var obj = new Object();
        obj["Id"] = Id;
        obj["ProposalId"] = ProposalId;
        obj["Redemption"] = $.trim($("#Redemption").val());
        obj["Investment"] = $.trim($("#Investment").val());
        obj["BorrowCapital"] = $.trim($("#BorrowCapital").val());
        obj["RepaymentCapital"] = $.trim($("#RepaymentCapital").val());
        obj["WorkIncome"] = $.trim($("#WorkIncome").val());
        obj["LiveExpense"] = $.trim($("#LiveExpense").val())
        obj["InvestIncome"] = $.trim($("#InvestIncome").val());
        obj["InterestExpense"] = $.trim($("#InterestExpense").val());
        obj["InsuranceExpense"] = $.trim($("#InsuranceExpense").val());
        
        _ajaxhepler({
            url: "/CompetitionUser/CashFlow/CashFlows",
            type: "POST",
            async: false,
            dataType: "json",
            data: JSON.stringify(obj),
            contentType: "application/json",
            success: function (data, txtStatus) {
                GetCashFlowList();
                if(valu==0){
                    
                    dialogHelper.Success({
                        content: "保存成功！", success: function () {
                            //刷新当前页
                            window.location.reload();
                            //把值赋给defaultVal作为原值
                            SaveDefaultValueCommon("CashFlow");

                        }
                    });

                } else if (valu==1) {
                    window.location.href = "/CompetitionUser/FinancialRatios/Index" + URL;
                } else if (valu == 2) {
                    window.location.href = "/CompetitionUser/IncomeandExpenses/Index" + URL;
                }
            }
        });
    }    
}

//加载本期现金及现金等价物净增加额
function loadings() {
    var Money1 = $.trim($("#lifeMoney").val()) * 1;
    var Money2 = $.trim($("#investMoney").val()) * 1;
    var Money3 = $.trim($("#borrowMoney").val()) * 1;
    var Money4 = $.trim($("#InsuranceExpenses2").val()) * 1;
    var All = Money1 + Money2 + Money3 + Money4;
    $("#Money").val(All.toMyFixed(2));
}