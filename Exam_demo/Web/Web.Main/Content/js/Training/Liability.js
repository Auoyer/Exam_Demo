//***************************
//负债规划
//***************************
var TagNavi = false;
var param = "";

//判断是否非法字符
function CheckNum(num) {
    var pattern6 = /^[-]?\d+(\.\d{1,6})?$/;//只能输入两位小数
    if (!pattern6.test(num)) {
        num = 0;
        return num;
    }
    num = (num * 1).toMyFixed(2);
    return num;
}
//=========================计算
//流动资产计算 小计=∑（现金、人民币银行活期存、其他流动资产）
function calcFlowAssets(Cash, RMBDeposit, OtherAsset) {
    var Cash1 = CheckNum(Cash);
    var RMBDeposit1 = CheckNum(RMBDeposit);
    var OtherAsset1 = CheckNum(OtherAsset);
    var sum = 0;
    if (Cash == Cash1 && RMBDeposit == RMBDeposit1 && OtherAsset == OtherAsset1) {
        sum = Cash * 1 + RMBDeposit * 1 + OtherAsset * 1;
    } else {
        sum = 0;
    }
    return sum;
};
//流动资产计算实体 小计=∑（现金、人民币银行活期存、其他流动资产） assetSum01
function calcFlowAssetsVal() {
    var Cash = $.trim($("#Cash").val())*1;
    var RMBDeposit = $.trim($("#RMBDeposit").val())*1;
    var OtherAsset = $.trim($("#OtherAsset").val())*1;
    var result = calcFlowAssets(Cash, RMBDeposit, OtherAsset)*1;
    $("#assetSum01").val(result.toMyFixed(2));
}


//消费资产计算 .小计=∑（信用卡欠款、小额消费信贷、其他消费性负债）
function calcConsumeAssets(CreditCard, Microfinance, OtherLoan) {
   var CreditCard1 = CheckNum(CreditCard);
   var Microfinance1 = CheckNum(Microfinance);
   var OtherLoan1 = CheckNum(OtherLoan);
    var sum = 0;
    if (CreditCard1 == CreditCard && Microfinance1 == Microfinance && OtherLoan1 == OtherLoan) {
        sum = CreditCard * 1 + Microfinance * 1 + OtherLoan * 1
    } else {
        sum = 0;
    }
    return sum;
}
//消费资产计算实体 小计=∑（信用卡欠款、小额消费信贷、其他消费性负债）
function calcConsumeAssetsVal() {
    var CreditCard = $.trim($("#CreditCard").val())*1;
    var Microfinance = $.trim($("#Microfinance").val())*1;
    var OtherLoan = $.trim($("#OtherLoan").val())*1;
    var result = calcConsumeAssets(CreditCard, Microfinance, OtherLoan)*1;
    $("#loanSum01").val(result.toMyFixed(2));
  
}
//投资资产计算 小计=∑（人民币银行定存、外币银行定存、股票投资、债券投资、基金投资、实业投资、投资性房地产、保单现金价值、其他投资性资产）
function calcInvestmentAssets(RMBFixedDeposit, ForeignCurrencyFixedDeposit, StockInvestment, BondInvestment, FundInvestment, IndustryInvestment,
   PolicyInvestment, EstateInvestment, OtherInvestment) {
   var RMBFixedDeposit1 = CheckNum(RMBFixedDeposit);
   var ForeignCurrencyFixedDeposit1 = CheckNum(ForeignCurrencyFixedDeposit);
   var StockInvestment1 = CheckNum(StockInvestment);
   var BondInvestment1 = CheckNum(BondInvestment);
   var FundInvestment1 = CheckNum(FundInvestment);
  var IndustryInvestment1 = CheckNum(IndustryInvestment);
   var PolicyInvestment1 = CheckNum(PolicyInvestment);
   var EstateInvestment1 = CheckNum(EstateInvestment);
   var OtherInvestment1 = CheckNum(OtherInvestment);
   var sum = 0;
   if (RMBFixedDeposit1 == RMBFixedDeposit && ForeignCurrencyFixedDeposit1 == ForeignCurrencyFixedDeposit && StockInvestment1 == StockInvestment && BondInvestment1 == BondInvestment && FundInvestment1 == FundInvestment && IndustryInvestment1 == IndustryInvestment && PolicyInvestment1 == PolicyInvestment && EstateInvestment1 == EstateInvestment && OtherInvestment1 == OtherInvestment) {
      sum = RMBFixedDeposit * 1 + ForeignCurrencyFixedDeposit * 1 + StockInvestment * 1 + BondInvestment * 1 + FundInvestment * 1 + IndustryInvestment * 1 + PolicyInvestment * 1 + EstateInvestment * 1 + OtherInvestment * 1;
   } else {
       sum = 0;
   }
    return sum;
}
//小计=∑（人民币银行定存、外币银行定存、股票投资、债券投资、基金投资、实业投资、投资性房地产、保单现金价值、其他投资性资产）
function calcInvestmentAssetsVal() {
    var RMBFixedDeposit = $.trim($("#RMBFixedDeposit").val())*1;
    var ForeignCurrencyFixedDeposit = $.trim($("#ForeignCurrencyFixedDeposit").val())*1;
    var StockInvestment = $.trim($("#StockInvestment").val())*1;
    var BondInvestment = $.trim($("#BondInvestment").val())*1;
    var FundInvestment = $.trim($("#FundInvestment").val())*1;
    var IndustryInvestment = $.trim($("#IndustryInvestment").val())*1;
    var PolicyInvestment = $.trim($("#PolicyInvestment").val())*1;
    var EstateInvestment = $.trim($("#EstateInvestment").val())*1;
    var OtherInvestment = $.trim($("#OtherInvestment").val())*1;
      var result = calcInvestmentAssets(RMBFixedDeposit, ForeignCurrencyFixedDeposit, StockInvestment, BondInvestment, FundInvestment, IndustryInvestment,PolicyInvestment, EstateInvestment, OtherInvestment)*1;
      $("#assetSum02").val(result.toMyFixed(2));
}



//投资负债计算小计=∑（金融投资借款、实业投资借款、投资性房地产按揭贷款、其他投资性负债）

function calcInvestmentLiability(FinancialLoan, IndustryInvestmentLoan, EstateInvestmentLoan, OtherInvestmentLoan) {
   var FinancialLoan1 = CheckNum(FinancialLoan);
   var IndustryInvestmentLoan1 = CheckNum(IndustryInvestmentLoan);
   var EstateInvestmentLoan1 = CheckNum(EstateInvestmentLoan);
   var OtherInvestmentLoan1 = CheckNum(OtherInvestmentLoan);
   var sum = 0;
   if (FinancialLoan1 == FinancialLoan && IndustryInvestmentLoan1 == IndustryInvestmentLoan && EstateInvestmentLoan1 == EstateInvestmentLoan && OtherInvestmentLoan1 == OtherInvestmentLoan) {
       sum = FinancialLoan * 1 + IndustryInvestmentLoan * 1 + EstateInvestmentLoan * 1 + OtherInvestmentLoan * 1;
   } else {
       sum = 0;
   }
    return sum;
}
//小计=∑（金融投资借款、实业投资借款、投资性房地产按揭贷款、其他投资性负债）
function calcInvestmentLiabilityVal() {
    var FinancialLoan = $.trim($("#FinancialLoan").val())*1;
    var IndustryInvestmentLoan = $.trim($("#IndustryInvestmentLoan").val())*1;
    var EstateInvestmentLoan = $.trim($("#EstateInvestmentLoan").val())*1;
    var OtherInvestmentLoan = $.trim($("#OtherInvestmentLoan").val())*1;
    var result = calcInvestmentLiability(FinancialLoan, IndustryInvestmentLoan, EstateInvestmentLoan, OtherInvestmentLoan)*1;
    $("#loanSum02").val(result.toMyFixed(2));

};

//自用负载实体 小计=∑（自用房地产贷款、自用汽车贷款、其他自用贷款）
function calcSelfLiability(EstateLoan, CarLoan, OthersLoan) {
    var EstateLoan1 = CheckNum(EstateLoan);
    var CarLoan1 = CheckNum(CarLoan);
    var OthersLoan1 = CheckNum(OthersLoan);
    var sum = 0;
    if (EstateLoan1 == EstateLoan && CarLoan1 == CarLoan && OthersLoan1 == OthersLoan) {
        sum = EstateLoan * 1 + CarLoan * 1 + OthersLoan * 1;
    } else {
        sum = 0;
    }
    return sum;
}
//自用负债 小计=∑（自用房地产贷款、自用汽车贷款、其他自用贷款）
function calcSelfLiabilityVal(EstateLoan, CarLoan, OthersLoan) {
    var EstateLoan = $.trim($("#EstateLoan").val())*1;
    var CarLoan = $.trim($("#CarLoan").val())*1;
    var OthersLoan = $.trim($("#OthersLoan").val())*1;
    var result = calcSelfLiability(EstateLoan, CarLoan, OthersLoan)*1;
    $("#loanSum03").val(result.toMyFixed(2));
};

//自用资产计算 小计=∑（自用房地产、自用汽车、自用其他资产）
function calcSelfAsset(Estate, Car, Others) {
    var Others1 = CheckNum(Others);
    var Car1 = CheckNum(Car);
    var Estate1 = CheckNum(Estate);
    //TotalAssets
    var sum = 0;
    if (Others1 == Others && Car1 == Car && Estate1 == Estate) {
        sum = Estate * 1 + Car * 1 + Others * 1;
    } else {
        sum = 0;
    }
    return sum;
};
//自用资产计算 小计=∑（自用房地产、自用汽车、自用其他资产）
function calcSelfAssetVal() {
    var Others = $.trim($("#Others").val())*1;
    var Car = $.trim($("#Car").val())*1;
    var Estate = $.trim($("#Estate").val())*1;
    var result = calcSelfAsset(Estate, Car, Others)*1;
    $("#assetSum03").val(result.toMyFixed(2));
    //TotalAssets
 
};

//资产合计 ∑（流动资产小计、投资资产小计、自用资产小计）
function calcTotalAssets(FlowAssets, InvestmentAssets, SelfAsset) {
    FlowAssets = CheckNum(FlowAssets);
    InvestmentAssets = CheckNum(InvestmentAssets);
    SelfAsset = CheckNum(SelfAsset);
    var sum = FlowAssets * 1 + InvestmentAssets * 1 + SelfAsset * 1;
    return sum;
};
//资产合计 ∑（流动资产小计、投资资产小计、自用资产小计）
function calcTotalAssetsVal() {
    var FlowAssets = $.trim($("#assetSum01").val()) * 1;
    var InvestmentAssets = $.trim($("#assetSum02").val()) * 1;
    var SelfAsset = $.trim($("#assetSum03").val()) * 1;
    var result = calcTotalAssets(FlowAssets, InvestmentAssets, SelfAsset)*1;
    $("#TotalAssets").val(result.toMyFixed(2))
};

//负债合计 负债合计=∑（消费负债小计、投资负债小计、自用负债小计）
function calcTotalLoan(ConsumeAssets, InvestmentLiability, SelfLiability) {
    ConsumeAssets = CheckNum(ConsumeAssets);
    InvestmentLiability = CheckNum(InvestmentLiability);
    SelfLiability = CheckNum(SelfLiability);
    var sum = ConsumeAssets * 1 + InvestmentLiability * 1 + SelfLiability * 1;
    return sum;
};
//负债合计 负债合计=∑（消费负债小计、投资负债小计、自用负债小计）
function calcTotalLoanVal() {
  var ConsumeAssets = $.trim($("#loanSum01").val())*1;
  var InvestmentLiability = $.trim($("#loanSum02").val())*1;
   var SelfLiability = $.trim($("#loanSum03").val())*1;
    var result = calcTotalLoan(ConsumeAssets, InvestmentLiability, SelfLiability)*1
    $("#TotalLoan").val(result.toMyFixed(2));
}

//==========================净值计算==================
//消费净值计算 1）	消费净值=流动资产小计-流动负债小计
function calcConsumeVal(flowAssets, ConsumeAssets) {
    flowAssets = CheckNum(flowAssets);
    ConsumeAssets = CheckNum(ConsumeAssets);

    var sum = flowAssets * 1 - ConsumeAssets * 1;
    return sum;
};
function calcConsumeValTwo() {
  var  flowAssets = $.trim($("#assetSum01").val())*1;
  var  ConsumeAssets = $.trim($("#loanSum01").val())*1;
    var result = calcConsumeVal(flowAssets, ConsumeAssets)*1;
    $("#consumeVal").val(result.toMyFixed(2));
   
};
//2）	投资净值=投资资产小计-投资负债小计
function calcInvestmentVal(InvestmentAssets, InvestmentLiability) {
   var InvestmentAssets1 = CheckNum(InvestmentAssets);
   var InvestmentLiability1 = CheckNum(InvestmentLiability);
    var sum = InvestmentAssets * 1 - InvestmentLiability * 1;
    return sum;
};
function calcInvestmentValTwo() {
   var InvestmentAssets = $.trim($("#assetSum02").val())*1;
  var InvestmentLiability = $.trim($("#loanSum02").val())*1;
    var result = calcInvestmentVal(InvestmentAssets, InvestmentLiability)*1;
    $("#investmentVal").val(result.toMyFixed(2));
    
}
//3）	自用净值=自用资产-自用负债
function clacSelfVal(SelfAsset, SelfLiability) {
    SelfAsset = CheckNum(SelfAsset);
    SelfLiability = CheckNum(SelfLiability);

    var sum = SelfAsset * 1 - SelfLiability * 1;
    return sum;
}
//3）	自用净值=自用资产-自用负债
function clacSelfValTow() {
   var SelfAsset = $.trim($("#assetSum03").val())*1;
   var SelfLiability = $.trim($("#loanSum03").val()) * 1;
    var result = clacSelfVal(SelfAsset, SelfLiability)*1;
    $("#selfVal").val(result.toMyFixed(2));
   
}
//4）	净值合计=∑（消费净值、投资净值、自用净值）
function calcSumVal(ConsumeVal, InvestmentVal, SelfVal) {
    ConsumeVal = CheckNum(ConsumeVal);
    InvestmentVal = CheckNum(InvestmentVal);
    SelfVal = CheckNum(SelfVal);
    var sum = ConsumeVal * 1 + InvestmentVal * 1 + SelfVal * 1;
    return sum;
};
function calcSumValTwo() {
   var ConsumeVal = $.trim($("#consumeVal").val())*1;
  var InvestmentVal = $.trim($("#investmentVal").val())*1;
  var SelfVal = $.trim($("#selfVal").val())*1;
    var result = calcSumVal(ConsumeVal, InvestmentVal, SelfVal)*1;
    $("#TotalVal").val(result.toMyFixed(2));
}

$(function () {
    IsProposalSave()//客户验证
    //获取URL参数
    var ProposalId = $.getUrlParam("ProposalId");

    //获取财产传承
    if (ProposalId != null && ProposalId != "" && ProposalId != undefined) {
       
    }
    //流动资产计算
    $("#Cash,#RMBDeposit,#OtherAsset").unbind("blur").blur(function () {
        //流动资产小计
        calcFlowAssetsVal();
        //资产合计
        calcTotalAssetsVal();
        //消费净值
        calcConsumeValTwo();
        //净值合计
        calcSumValTwo();
       
    });
    //消费资产计算
    $("#CreditCard,#Microfinance,#OtherLoan").unbind("blur").blur(function () {
        //消费资产小计
        calcConsumeAssetsVal();
        //负债合计
        calcTotalLoanVal();
        //消费净值
        calcConsumeValTwo();
        //净值合计
        calcSumValTwo();
     
    });
    //RMBFixedDeposit, ForeignCurrencyFixedDeposit, StockInvestment, BondInvestment, FundInvestment, IndustryInvestment,
   // PolicyInvestment, EstateInvestment, OtherInvestment
    //投资资产
    $("#RMBFixedDeposit,#ForeignCurrencyFixedDeposit,#StockInvestment,#BondInvestment,#FundInvestment,#IndustryInvestment,#PolicyInvestment,#EstateInvestment,#OtherInvestment").unbind("blur").blur(function () {
        //投资资产小计
        calcInvestmentAssetsVal();
        //资产合计
        calcTotalAssetsVal();
        //投资净值
        calcInvestmentValTwo();
        //净值合计
        calcSumValTwo();
    });

    //投资负债
    $("#FinancialLoan,#IndustryInvestmentLoan,#EstateInvestmentLoan,#OtherInvestmentLoan").unbind("blur").blur(function () {
        //投资负债
        calcInvestmentLiabilityVal();
        //负债合计
        calcTotalLoanVal();
        //投资净值
        calcInvestmentValTwo();
        //净值合计
        calcSumValTwo();
    });

    //自用资产
    $("#Estate,#Car,#Others").unbind("blur").blur(function () {
        //自用资产
        calcSelfAssetVal();
        //资产合计
        calcTotalAssetsVal();
        //自用净值计算
        clacSelfValTow()
        //净值合计
        calcSumValTwo();
    });

    //自用负债
    $("#EstateLoan,#CarLoan,#OthersLoan").unbind("blur").blur(function () {
        //自用负债
        calcSelfLiabilityVal();
        //负债合计
        calcTotalLoanVal();
        //自用净值计算
        clacSelfValTow()
        //净值合计
        calcSumValTwo();
    });

})


//新增以及修改数据
function SaveLiability(saveFalg) {
    TagNavi = true;
    //页面字段检测
    if (!VerificationHelper.checkFrom("FinanceLiabilityDiv")) {
        TagNavi = false;
        return;
    }
    //获取URL参数
    var ProposalId = $.getUrlParam("ProposalId");

    //此处参数必须跟VM一致
    var obj = new Object();

    //获取财产传承
    if (ProposalId != null && ProposalId != "" && ProposalId != undefined) {
        obj["ProposalId"] = ProposalId;
    } else {
        return false;
    }

    obj["Id"] = $("#FinanceLiabilityDiv #LiabilityId").val();
   
    obj["Cash"] = $("#FinanceLiabilityDiv #Cash").val();
    obj["RMBDeposit"] = $("#FinanceLiabilityDiv #RMBDeposit").val();
    obj["OtherAsset"] = $("#FinanceLiabilityDiv #OtherAsset").val();
    obj["RMBFixedDeposit"] = $("#FinanceLiabilityDiv #RMBFixedDeposit").val();
    obj["ForeignCurrencyFixedDeposit"] = $("#FinanceLiabilityDiv #ForeignCurrencyFixedDeposit").val();
    obj["StockInvestment"] = $("#FinanceLiabilityDiv #StockInvestment").val();//取隐藏用户Id
    obj["BondInvestment"] = $("#FinanceLiabilityDiv #BondInvestment").val();
    obj["FundInvestment"] = $("#FinanceLiabilityDiv #FundInvestment").val();
    obj["IndustryInvestment"] = $("#FinanceLiabilityDiv #IndustryInvestment").val();
    obj["EstateInvestment"] = $("#FinanceLiabilityDiv #EstateInvestment").val();
    obj["PolicyInvestment"] = $("#FinanceLiabilityDiv #PolicyInvestment").val();
    obj["OtherInvestment"] = $("#FinanceLiabilityDiv #OtherInvestment").val();
    obj["Estate"] = $("#FinanceLiabilityDiv #Estate").val();
    obj["Car"] = $("#FinanceLiabilityDiv #Car").val();
    obj["Others"] = $("#FinanceLiabilityDiv #Others").val();
    obj["TotalAssets"] = $("#FinanceLiabilityDiv #TotalAssets").val();
    obj["CreditCard"] = $("#FinanceLiabilityDiv #CreditCard").val();
    obj["Microfinance"] = $("#FinanceLiabilityDiv #Microfinance").val();
    obj["OtherLoan"] = $("#FinanceLiabilityDiv #OtherLoan").val();
    obj["FinancialLoan"] = $("#FinanceLiabilityDiv #FinancialLoan").val();
    obj["IndustryInvestmentLoan"] = $("#FinanceLiabilityDiv #IndustryInvestmentLoan").val();
    obj["EstateInvestmentLoan"] = $("#FinanceLiabilityDiv #EstateInvestmentLoan").val();
    obj["OtherInvestmentLoan"] = $("#FinanceLiabilityDiv #OtherInvestmentLoan").val();
    obj["EstateLoan"] = $("#FinanceLiabilityDiv #EstateLoan").val();
    obj["CarLoan"] = $("#FinanceLiabilityDiv #CarLoan").val();
    obj["OthersLoan"] = $("#FinanceLiabilityDiv #OthersLoan").val();
    obj["TotalLoan"] = $("#FinanceLiabilityDiv #TotalLoan").val();
    _ajaxhepler({
        url: "/CompetitionUser/Liability/SaveLiability",
        type: "POST",
        async: false,
        dataType: "json",
        data: JSON.stringify(obj),
        contentType: "application/json",
        success: function (data) {
            if (data != null) {
                //刷新当前页
                $("#LiabilityId").val(data.Id);
                //保存之后必须重新保存一下基础值
                SaveDefaultValueCommon("FinanceLiabilityDiv");
                //弹出成功提示
                if (typeof saveFalg == "undefined") {
                    dialogHelper.Success({
                        content: "保存成功！", success: function () {
                            window.location.reload();
                        }
                    });
                }
            }
        }
    });
}



//獲取消費規劃
function GetLiability(ProposalId) {
    _ajaxhepler({
        url: "/CompetitionUser/Liability/LoadLiabilityByProposalId",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: ProposalId,
            rId: Math.random()
        },
        success: function (data) {

            if (data != null) {
                SetProposalLibily(data);
                //保存元数据
                SaveDefaultValueCommon("FinanceLiabilityDiv");
            } else {
                ProposalId = 0;
            }
        }
    });
};
//加载设置参数
function SetProposalLibily(data) {
    $("#FinanceLiabilityDiv #LiabilityId").val(data.Id);
    $("#FinanceLiabilityDiv #ProposalId").val(data.ProposalId)
    $("#FinanceLiabilityDiv #Cash").val(data.Cash);
    $("#FinanceLiabilityDiv #RMBDeposit").val(data.RMBDeposit);
    $("#FinanceLiabilityDiv #OtherAsset").val(data.OtherAsset);
    $("#FinanceLiabilityDiv #RMBFixedDeposit").val(data.RMBFixedDeposit);//----人民币固定存款
    $("#FinanceLiabilityDiv #ForeignCurrencyFixedDeposit").val(data.ForeignCurrencyFixedDeposit);
    $("#FinanceLiabilityDiv #StockInvestment").val(data.StockInvestment);
    $("#FinanceLiabilityDiv #BondInvestment").val(data.BondInvestment);
    $("#FinanceLiabilityDiv #FundInvestment").val(data.FundInvestment);
    $("#FinanceLiabilityDiv #IndustryInvestment").val(data.IndustryInvestment);
    $("#FinanceLiabilityDiv #EstateInvestment").val(data.EstateInvestment);
    $("#FinanceLiabilityDiv #PolicyInvestment").val(data.PolicyInvestment);
    $("#FinanceLiabilityDiv #OtherInvestment").val(data.OtherInvestment);
    $("#FinanceLiabilityDiv #Estate").val(data.Estate);//---------房产
    $("#FinanceLiabilityDiv #Car").val(data.Car);
    $("#FinanceLiabilityDiv #Others").val(data.Others);
    $("#FinanceLiabilityDiv #TotalAssets").val(data.TotalAssets);
    $("#FinanceLiabilityDiv #CreditCard").val(data.CreditCard);//-------信用卡借款
    $("#FinanceLiabilityDiv #Microfinance").val(data.Microfinance);
    $("#FinanceLiabilityDiv #OtherLoan").val(data.OtherLoan);
    $("#FinanceLiabilityDiv #FinancialLoan").val(data.FinancialLoan);//-----金融实用借款
    $("#FinanceLiabilityDiv #IndustryInvestmentLoan").val(data.IndustryInvestmentLoan);
    $("#FinanceLiabilityDiv #EstateInvestmentLoan").val(data.EstateInvestmentLoan);
    $("#FinanceLiabilityDiv #OtherInvestmentLoan").val(data.OtherInvestmentLoan);
    $("#FinanceLiabilityDiv #EstateLoan").val(data.EstateLoan);//------自用房地产
    $("#FinanceLiabilityDiv #CarLoan").val(data.CarLoan);
    $("#FinanceLiabilityDiv #OthersLoan").val(data.OthersLoan);
    $("#FinanceLiabilityDiv #TotalLoan").val(data.TotalLoan);
    //然后给所有的小计赋值
    var flowAssets = calcFlowAssets(data.Cash, data.RMBDeposit, data.OtherAsset);//流动资产小计
    $("#FinanceLiabilityDiv #assetSum01").val(flowAssets.toMyFixed(2));//流动资产小计
    var invesymentAsset = calcInvestmentAssets(data.RMBFixedDeposit, data.ForeignCurrencyFixedDeposit, data.StockInvestment, data.BondInvestment, data.FundInvestment, data.IndustryInvestment, data.EstateInvestment, data.PolicyInvestment, data.OtherInvestment);//投资资产小计
    $("#FinanceLiabilityDiv #assetSum02").val(invesymentAsset.toMyFixed(2));//投资资产小计
    var selfAsset = calcSelfAsset(data.Estate, data.Car, data.Others);
    $("#FinanceLiabilityDiv #assetSum03").val(selfAsset.toMyFixed(2));//自用资产小计
    var consumeLiability = calcConsumeAssets(data.CreditCard, data.Microfinance, data.OtherLoan);//消费负债
    $("#FinanceLiabilityDiv #loanSum01").val(consumeLiability.toMyFixed(2));//消费负债
    var inverstmentLiability = calcInvestmentLiability(data.FinancialLoan, data.IndustryInvestmentLoan, data.EstateInvestmentLoan, data.OtherInvestmentLoan);
    $("#FinanceLiabilityDiv #loanSum02").val(inverstmentLiability.toMyFixed(2));//投资负债
    var selfLiability = calcSelfLiability(data.EstateLoan, data.CarLoan, data.OthersLoan);
    $("#FinanceLiabilityDiv #loanSum03").val(selfLiability.toMyFixed(2));//自用负债
    //消费净资产
    var consumeVal = flowAssets * 1 - consumeLiability * 1;
    $("#FinanceLiabilityDiv #consumeVal").val(consumeVal.toMyFixed(2));
    //投资净自残
    var investmentVal = invesymentAsset * 1 - inverstmentLiability * 1;
    $("#FinanceLiabilityDiv #investmentVal").val(investmentVal.toMyFixed(2));
    //自用净值
    var selfVal = selfAsset * 1 - selfLiability * 1;
    $("#FinanceLiabilityDiv #selfVal").val(selfVal.toMyFixed(2));
    //净值合计
    var TotalVal = consumeVal * 1 + investmentVal * 1 + selfVal * 1;
    $("#FinanceLiabilityDiv #TotalVal").val(TotalVal.toMyFixed(2))
    //资产合计
}


$(function () {
    //先加载数据
    param = $("#hdParam").val();
    //获取URL参数
    var ProposalId = $.getUrlParam("ProposalId");

    //获取财产传承
    if (ProposalId != null && ProposalId != "" && ProposalId != undefined) {
        GetLiability(ProposalId);
    }

    $("#FinanceLiabilityDiv #btnSave").live("click", function () {
        //添加数据
        SaveLiability();
    });
    //同时绑定下一页事件
    $("#FinanceLiabilityDiv #btnNext").live("click", function () {
        //同时还要保存当前数据
        SaveLiability(0);
        if (TagNavi) {
            window.location.href = "/CompetitionUser/IncomeAndExpenses/Index" + param;
        }
    });
   
    //navMenuTopReg.Regiclick(data);
})


function checkValidateInfo(tr) {
    if (!VerificationHelper.checkFrom("FinanceLiabilityDiv")) {
        return;
    } else {
        //此处参数必须跟VM一致
        var obj = new Object();
        //  var Id=   $("#FinanceLiabilityDiv #Id") 
        obj["ProposalId"] = $("#FinanceLiabilityDiv #ProposalId").val();
        obj["Cash"] = $("#FinanceLiabilityDiv #Cash").val();//流动资产--现金
        obj["RMBDeposit"] = $("#FinanceLiabilityDiv #RMBDeposit").val();
        obj["OtherAsset"] = $("#FinanceLiabilityDiv #OtherAsset").val();//其他流动资产
        obj["RMBFixedDeposit"] = $("#FinanceLiabilityDiv #RMBFixedDeposit").val();//投资资产--人民币银行定存
        obj["ForeignCurrencyFixedDeposit"] = $("#FinanceLiabilityDiv #ForeignCurrencyFixedDeposit").val();
        obj["StockInvestment"] = $("#FinanceLiabilityDiv #StockInvestment").val();//取隐藏用户Id
        obj["BondInvestment"] = $("#FinanceLiabilityDiv #BondInvestment").val();
        obj["FundInvestment"] = $("#FinanceLiabilityDiv #FundInvestment").val();
        obj["IndustryInvestment"] = $("#FinanceLiabilityDiv #IndustryInvestment").val();
        obj["EstateInvestment"] = $("#FinanceLiabilityDiv #EstateInvestment").val();
        obj["PolicyInvestment"] = $("#FinanceLiabilityDiv #PolicyInvestment").val();
        obj["OtherInvestment"] = $("#FinanceLiabilityDiv #OtherInvestment").val();
        obj["Estate"] = $("#FinanceLiabilityDiv #Estate").val();//自用资产----自用房地产
        obj["Car"] = $("#FinanceLiabilityDiv #Car").val();
        obj["Others"] = $("#FinanceLiabilityDiv #Others").val();
        obj["TotalAssets"] = $("#FinanceLiabilityDiv #TotalAssets").val();
        obj["CreditCard"] = $("#FinanceLiabilityDiv #CreditCard").val(); //消费负载--信用卡欠款
        obj["Microfinance"] = $("#FinanceLiabilityDiv #Microfinance").val();
        obj["OtherLoan"] = $("#FinanceLiabilityDiv #OtherLoan").val();
        obj["FinancialLoan"] = $("#FinanceLiabilityDiv #FinancialLoan").val();
        obj["IndustryInvestmentLoan"] = $("#FinanceLiabilityDiv #IndustryInvestmentLoan").val();//投资负债--金融投资负载
        obj["EstateInvestmentLoan"] = $("#FinanceLiabilityDiv #EstateInvestmentLoan").val();
        obj["OtherInvestmentLoan"] = $("#FinanceLiabilityDiv #OtherInvestmentLoan").val();
        obj["EstateLoan"] = $("#FinanceLiabilityDiv #EstateLoan").val();//自用负债---自用房地产负债
        obj["CarLoan"] = $("#FinanceLiabilityDiv #CarLoan").val();
        obj["OthersLoan"] = $("#FinanceLiabilityDiv #OthersLoan").val();
        obj["TotalLoan"] = $("#FinanceLiabilityDiv #TotalLoan").val();

    }
}
   