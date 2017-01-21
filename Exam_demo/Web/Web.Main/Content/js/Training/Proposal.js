/**
 * @name 生成理财建议书
 */
function CreateProposal() {
    var ProposalNum = $("#hdProposalNum").val();
    if (ProposalNum != null && ProposalNum != "" && ProposalNum != undefined) {
        dialogHelper.Success({
            content: "已成功生成理财建议书!",
            success: function () {
                var parms = $("#hdParam").val();
                location.href = "/CompetitionUser/ProposalCustomer/Index" + parms;
            },
        });
        return;
    }
    var TrainExamId = $.getUrlParam("TrainExamId");           //
    var StuCustomerId = $.getUrlParam("StuCustomerId");           //
    var ProposalId = $.getUrlParam("ProposalId");           //建议书Id

    if (ProposalId == null || ProposalId == 0)
    {
        dialogHelper.Error({
            content: "您未做任何理财规划，无法生成理财建议书"
        });
        return;

    }

    //if (ProposalId == 0) {
    //    dialogHelper.Error({
    //        content: msgList["20011"],//20011 请先保存客户信息!
    //    });
    //    return;
    //}

    $.ajax({
        url: "/CompetitionUser/Proposal/Create",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: ProposalId,
            StuCustomerId: StuCustomerId,
            rId: Math.random(),
        },
        success: function (data) {
            dialogHelper.Success({
                content: "已成功生成理财建议书!",
                success: function () {
                    var parms = $("#hdParam").val();
                    location.href = "/CompetitionUser/ProposalCustomer/Index" + parms;
                },
            });
        }
    });
}

/**
 * @name 预览理财建议书
 */
function PreviewProposal() {
    var ProposalNum = $.getUrlParam("ProposalId");

    if (ProposalNum != null && ProposalNum != "" && ProposalNum != undefined) {
        var parms = $("#hdParam").val();
        location.href = "/CompetitionUser/ProposalCustomer/PreviewIndex" + parms + "&ProposalId=" + ProposalNum;
    } else {
        dialogHelper.Error({
            content: "请先生成理财建议书!",
        });
    }
}






