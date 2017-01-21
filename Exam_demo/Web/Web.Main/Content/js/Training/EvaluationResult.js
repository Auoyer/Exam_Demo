//*******************************
//考生端-实训考试-风险评测结果
//*******************************

$(function () {
    //获取URL参数
    var ProposalId = $.getUrlParam("ProposalId");
    if (ProposalId == null) {
        ProposalId = 0;
    }
    var TrainExamId = $.getUrlParam("TrainExamId");
    if (TrainExamId == null) {
        TrainExamId = 0;
    }
    var StuCustomerId = $.getUrlParam("StuCustomerId");
    if (StuCustomerId == null)
    { StuCustomerId = 0;}
    //获取评测结果
    GetRiskEvaluationInfo(ProposalId);

    //绑定下一步按钮
    $("#EvaluationResultDev #btnNextStep").unbind("click").bind("click", function () {
        window.location.href = "/CompetitionUser/Liability/Index?TrainExamId=" + TrainExamId + "&ProposalId=" + ProposalId + "&StuCustomerId=" + StuCustomerId;
    });
    //绑定上一步按钮
    $("#EvaluationResultDev #btnLastStep").unbind("click").bind("click", function () {
        window.location.href = "/CompetitionUser/RiskEvaluation/Index?TrainExamId=" + TrainExamId + "&ProposalId=" + ProposalId + "&StuCustomerId=" + StuCustomerId;
    }); 
});
function GetRiskEvaluationInfo(ProposalId) {  
    _ajaxhepler({
        url: "/CompetitionUser/RiskEvaluation/GetRiskEvaluationInfo",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: ProposalId
        },
        success: function (data) {
            if (data != null) {
                ShowInfo(data);
            }
        }
    });
}
//显示评测结果
function ShowInfo(data) { 
    $("#EvaluationDate").text(data.UpdateDateStr);
    var RCIScore = data.RCIScore;//风险承受能力 
    var RAIScore = data.RAIScore;//风险容忍态度 
    var length = ControlTable.length;
    var AbilityMin = 0, AbilityMax = 0, AttitudeMin = 0;
    for (var i = 0; i < length; i++) {
        AbilityMin = ControlTable[i].AbilityMin;
        AbilityMax = ControlTable[i].AbilityMax;
        AttitudeMin = ControlTable[i].AttitudeMin;
        AttitudeMax = ControlTable[i].AttitudeMax;
        if (AbilityMin <= RCIScore && RCIScore <= AbilityMax && AttitudeMin <= RAIScore && RAIScore <= AttitudeMax)
        { 
            $("#DistributionRatio tr:eq(1) td:eq(0)").text(ControlTable[i].Currency);
            $("#DistributionRatio tr:eq(1) td:eq(1)").text(ControlTable[i].Bond);
            $("#DistributionRatio tr:eq(1) td:eq(2)").text(ControlTable[i].Stock);
            $("#RiskBearingCapacity").text(ControlTable[i].Ability);//风险承受能力
            $("#RiskToleranceAttitude").text(ControlTable[i].Attitude);//风险容忍态度 
            ShowPieInfo(ControlTable[i].Currency, ControlTable[i].Bond, ControlTable[i].Stock);
            return true;
        }
    }
}

function ShowPieInfo(Currency, Bond, Stock) {
    var chart;
    $('.distribute').highcharts({
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false
        },
        title: {
            text: '投资分配比例',
            align: 'left',
            style: {
                fontSize: '14px'
            }
            
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        colors: ['#63b2f4', '#2a91e6', '#086cc1'],
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: false
                },
                showInLegend: true
            }
        },
        series: [{
            type: 'pie',
            name: '投资分配比例',
            data: [
                ['货币', Currency],
                {
                    name: '债券',
                    y: Bond,
                    sliced: true,
                    selected: true
                },
                ['股票', Stock],
            ]
        }]
    });
} 