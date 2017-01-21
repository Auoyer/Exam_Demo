//风险评测
$(function () {
    

    //绑定风险承受能力指标中相关计算字段
    $("#AgeScore,#JobScore,#FamilyScore,#HouseScore,#EXPScore,#KnowledgeScore").unbind("blur").blur(function () {
        var AgeScore = $("#AgeScore").val()*1;
        var JobScore = $("#JobScore").val()*1;
        var FamilyScore = $("#FamilyScore").val()*1;
        var HouseScore = $("#HouseScore").val()*1;
        var EXPScore = $("#EXPScore").val()*1;
        var KnowledgeScore = $("#KnowledgeScore").val() * 1;
        var Total = CheckNum(AgeScore) + CheckNum(JobScore) + CheckNum(FamilyScore) + CheckNum(HouseScore) + CheckNum(EXPScore) + CheckNum(KnowledgeScore);
        $("#RCIScore").val(Total);
    });
    //绑定风险容忍态度指标中相关计算字段
    $("#TolerateScore,#ConsiderationScore,#LossScore,#MentalityScore,#CharacterScore,#AvoidScore").unbind("blur").blur(function () {
        var TolerateScore = $("#TolerateScore").val()*1;
        var ConsiderationScore = $("#ConsiderationScore").val()*1;
        var LossScore = $("#LossScore").val()*1;
        var MentalityScore = $("#MentalityScore").val()*1;
        var CharacterScore = $("#CharacterScore").val()*1;
        var AvoidScore = $("#AvoidScore").val()*1;    
        var Total = CheckNum(TolerateScore) + CheckNum(ConsiderationScore) + CheckNum(LossScore) + CheckNum(MentalityScore) + CheckNum(CharacterScore) + CheckNum(AvoidScore);
        $("#RAIScore").val(Total); 
    });
    //保存添加值
    $("#RiskEvaluationDiv #btnSave").bind("click", function () {
        SaveRiskIndexInfo(false);
    });
    //同时绑定下一页事件
    $("#RiskEvaluationDiv #btnNext").bind("click", function () {
        //获取URL参数
        var ProposalId = $.getUrlParam("ProposalId");
        if (ProposalId == null) {
            ProposalId = 0;
        }
        var TrainExamId = $.getUrlParam("TrainExamId");
        if (TrainExamId == null) {
            TrainExamId = 0;
        }
        //同时还要保存当前数据
        SaveRiskIndexInfo(true);
    });
    
    //1、加载风险评测内容
    GetRiskEvaluationInfo();

    //保存之后必须重新保存一下基础值
    SaveDefaultValueCommon("RiskEvaluationDiv");
    //检测客户信息是否保存
    IsProposalSave();
});


function SaveRiskIndexInfo(flag)
{
    //页面字段检测
    if (!VerificationHelper.checkFrom("RiskEvaluationDiv"))
        return;
    var ProposalId = $("#ProposalId").val();
    var RiskIndexId = $("#RiskIndexId").val();
    var AgeScore = $("#AgeScore").val();
    var JobScore = $("#JobScore").val();
    var FamilyScore = $("#FamilyScore").val();
    var HouseScore = $("#HouseScore").val();
    var EXPScore = $("#EXPScore").val();
    var KnowledgeScore = $("#KnowledgeScore").val();
    var RCIScore = $("#RCIScore").val();
    var TolerateScore = $("#TolerateScore").val();
    var ConsiderationScore = $("#ConsiderationScore").val();
    var LossScore = $("#LossScore").val();
    var MentalityScore = $("#MentalityScore").val();
    var CharacterScore = $("#CharacterScore").val();
    var AvoidScore = $("#AvoidScore").val();
    var RAIScore = $("#RAIScore").val();
    var TrainExamId = $.getUrlParam("TrainExamId");
    var StuCustomerId = $.getUrlParam("StuCustomerId");
    _ajaxhepler({
        url: "/CompetitionUser/RiskEvaluation/SaveRiskIndex",
        type: "POST",
        async: false,
        dataType: "json",
        data: {
            Id: RiskIndexId,
            ProposalId: ProposalId,
            AgeScore: AgeScore,
            JobScore: JobScore,
            FamilyScore: FamilyScore,
            HouseScore: HouseScore,
            EXPScore: EXPScore,
            KnowledgeScore: KnowledgeScore,
            RCIScore:RCIScore,
            TolerateScore: TolerateScore,
            ConsiderationScore: ConsiderationScore,
            LossScore: LossScore,
            MentalityScore: MentalityScore,
            CharacterScore: CharacterScore,
            AvoidScore: AvoidScore,
            RAIScore: RAIScore
        }, 
        success: function (data) {
            if (flag)
            {
                window.location.href = "/CompetitionUser/RiskEvaluation/EvaluationResult?TrainExamId=" + TrainExamId + "&ProposalId=" + ProposalId + "&StuCustomerId=" + StuCustomerId;
            }
            else {
                $("#RiskIndexId").val(data.Id);
                //保存之后必须重新保存一下基础值
                SaveDefaultValueCommon("RiskEvaluationDiv");
                //弹出成功提示 
                dialogHelper.Success({
                    content: "保存成功！",
                    success: function () {
                        location.href = location.href;
                    }
                });
            }
        }
    });
}

function GetRiskEvaluationInfo() {
    //获取URL参数
    var ProposalId = $.getUrlParam("ProposalId");
    if (ProposalId == null) {
        return;
    }
    //隐藏域
    $("#ProposalId").val(ProposalId);

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
                $("#RiskIndexId").val(data.Id);
                $("#ProposalId").val(data.ProposalId);
                $("#AgeScore").val(data.AgeScore);
                $("#JobScore").val(data.JobScore);
                $("#FamilyScore").val(data.FamilyScore);
                $("#HouseScore").val(data.HouseScore);
                $("#EXPScore").val(data.EXPScore);
                $("#KnowledgeScore").val(data.KnowledgeScore);
                $("#RCIScore").val(data.RCIScore);
                $("#TolerateScore").val(data.TolerateScore);
                $("#ConsiderationScore").val(data.ConsiderationScore);
                $("#LossScore").val(data.LossScore);
                $("#MentalityScore").val(data.MentalityScore);
                $("#CharacterScore").val(data.CharacterScore);
                $("#AvoidScore").val(data.AvoidScore);
                $("#UpdateDate").val(data.UpdateDate);
                $("#RAIScore").val(data.RAIScore); 
            }   
        }
    });
}

//判断是否非法字符
function CheckNum(num) {
    var pattern6 = /^\d+$/;
    if (!pattern6.test(num)) {
        return 0;
    }
    else 
        {
        return num;
    } 
}