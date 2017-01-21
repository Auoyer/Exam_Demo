

// 加载未评分实训列表
function GetList(keyword) {
    pageHelper.Init({
        url: "/CompetitionJudges/MatchList/MatchList",
        type: "POST",
        pageDiv: "#pages",
        data: {
            keyword: keyword,
            status: 1,
        },
        bind: function (data) {
            var html = "";
            $(data.Data).each(function (index, dom) {
                //每行html
                var trHtml = "";
                trHtml += "<tr>";
                trHtml += "<td>{0}</td>";
                trHtml += "<td><div title=\"{1}\" class=\"ellipsis\">{1}</div></td>";
                trHtml += "<td><div title=\"{2}\" class=\"ellipsis\">{2}</div></td>";
                trHtml += "<td>{3}</td>";
                trHtml += "<td>{4} - {5}</td>";
                trHtml += "<td class=\"operate\">";
                trHtml += "<a class=\"d_chakan\" href='javascript:MarkPaper({6},{7},{8},{9});'>评分</a>";
                trHtml += "</td>";
                trHtml += "</tr>";
                //拼接tbody
                html += StringHelper.FormatStr(trHtml,
                    ((data.PageIndex - 1) * data.PageSize + index + 1),     //0 序号
                    dom.GroupName,                                               //1 组别
                    dom.Match.Name,                                           //2 竞赛名称
                    dom.TrainExam.Case.strFinancialType,        //3 理财类型
                    dom.Match._ScoreStartTime,                                        //4 开始时间
                    dom.Match._ScoreEndTime,                        // 5 结束时间
                    dom.Match.Id,                           // 6 竞赛Id
                    dom.TrainExamId,                        // 7 实训Id
                    dom.AssessmentId,                        // 8 实训结果ID
                    dom.UserId                              // 9 考生Id
                );
            });
            if (data.Data == "" || data.Data == null) {
                html += "<tr><td  colspan='6'>未找到相关记录！</td>";
                $("#notStartList").html(html);
            } else {
                $("#notStartList").html(html);
            }
        }
    });
}

// 加载已评分实训列表
function GetStartList(keyword) {
    pageHelper.Init({
        url: "/CompetitionJudges/MatchList/MatchList",
        type: "POST",
        pageDiv: "#pages",
        data: {
            keyword: keyword,
            status: 2,
        },
        bind: function (data) {
            var html = "";
            $(data.Data).each(function (index, dom) {
                //每行html
                var trHtml = "";
                trHtml += "<tr>";
                trHtml += "<td>{0}</td>";
                trHtml += "<td><div title=\"{1}\" class=\"ellipsis\">{1}</div></td>";
                trHtml += "<td><div title=\"{2}\" class=\"ellipsis\">{2}</div></td>";
                trHtml += "<td>{3}</td>";
                trHtml += "<td>{4} - {5}</td>";
                trHtml += "<td class=\"operate\">";
                trHtml += "<a class=\"d_chakan\" href=\"javascript:ViewReport({8},'{2}');\">查看成绩</a>";
                trHtml += "<a class=\"d_chakan\" href='javascript:ViewProposal({6},{7},{9});'>查看建议书</a>";
                trHtml += "</td>";
                trHtml += "</tr>";
                //拼接tbody
                html += StringHelper.FormatStr(trHtml,
                    ((data.PageIndex - 1) * data.PageSize + index + 1),     //0 序号
                    dom.GroupName,                                               //1 组别
                    dom.Match.Name,                                           //2 竞赛名称
                    dom.TrainExam.Case.strFinancialType,        //3 理财类型
                    dom.Match._ScoreStartTime,                                        //4 开始时间
                    dom.Match._ScoreEndTime,                        // 5 结束时间
                    dom.Match.Id,                           // 6 竞赛Id
                    dom.TrainExamId,                        // 7 实训Id
                    dom.AssessmentId,                        // 8 实训结果ID
                    dom.UserId                              // 9 考生Id
                );
            });
            if (data.Data == "" || data.Data == null) {
                html += "<tr><td  colspan='6'>未找到相关记录！</td>";
                $("#startList").html(html);
            } else {
                $("#startList").html(html);
            }
        }
    });
}

//试卷评分
function MarkPaper(MatchId, TrainExamId, AssessmentId, UserId) {
    $.ajax({
        url: "/CompetitionJudges/MatchList/GetExamPaper",
        async: false,
        type: "POST",
        data: {
            MatchId: MatchId,
            TrainExamId: TrainExamId,
            UserId: UserId,
        },
        success: function (data) {
            if (data != null && data.IsSuccess == true) {
                location.href = "/CompetitionJudges/Assessment/Assessment?AssessmentResultsId=" + AssessmentId + "&ProposalId=" + data.Data.ProposalId + "&TrainExamId=" + TrainExamId + "&MatchId=" + MatchId;
            }
            else {
                dialogHelper.Error({
                    content: data.ErrorCode
                });
            }
        }
    });
}

//查看成绩报告
function ViewReport(AssessmentId, MatchName) {
    location.href = "/CompetitionJudges/Assessment/ScoreReport?AssessmentResultsId=" + AssessmentId + "&MatchName=" + escape(MatchName);
}

//查看建议书
function ViewProposal(MatchId, TrainExamId, UserId) {
    $.ajax({
        url: "/CompetitionJudges/MatchList/GetExamPaper",
        async: false,
        type: "POST",
        data: {
            MatchId: MatchId,
            TrainExamId: TrainExamId,
            UserId: UserId,
        },
        success: function (data) {
            if (data != null && data.Data.ProposalId > 0) {
                location.href = "/CompetitionJudges/Assessment/TrainExamView?ProposalId=" + data.Data.ProposalId;
            }
        }
    });
}