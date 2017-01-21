
// 加载待报名比赛列表
function GetList(sort) {
    var  userId= $("#hdUserId").val();
    pageHelper.Init({
        url: "/CompetitionUser/MatchList/MatchNotList",
        type: "POST",
        pageDiv: "#pages",
        async: false,
        data: {
            userId:userId,
            sort: sort,
            random: Math.random()
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
                trHtml += "<td>{6} - {7}</td>";
                trHtml += "<td class=\"operate\">";
                trHtml += "<a class=\"d_chakan\" href='javascript:ViewMatch({4});'>查看</a>";
                if (dom.UserOperate == 0) {
                    trHtml += "<a class=\"d_shanchu\" href='javascript:ViewApply({4});'>开始报名</a>";
                }
                if (dom.UserOperate == 1) {
                    trHtml += "<a class=\"d_shanchu\" href='javascript:void(0);'>等待审核</a>";
                }
                if (dom.UserOperate == 2) {
                    trHtml += "<a class=\"d_shanchu\" href='javascript:void(0);'>审核未通过</a>";
                }
                if (dom.UserOperate == 5) {
                    trHtml += "<a class=\"d_shanchu\" href='javascript:ViewApply({4});'>组队中</a>";
                }
                trHtml += "</td>";
                trHtml += "</tr>";
                //拼接tbody
                html += StringHelper.FormatStr(trHtml,
                    ((data.PageIndex - 1) * data.PageSize + index + 1),     //0 序号
                    dom.Name,                                       //1 竞赛名称
                    dom.AddUserName,                                           //2 创建人
                    dom.Type == 1 ? "单项理论赛" : dom.Type == 2 ? "单项实训赛" : "复合赛",      //3 比赛类型
                    dom.Id,                                        //4 Id
                    dom.Type,                        // 5 竞赛类型
                    dom.Type != 32 ? dom._PreliminaryStartTime : dom._RematchStartTime,                        // 6 开始时间
                    dom.Type != 32 ? dom._PreliminaryEndTime : dom._RematchEndTime,                       // 7 结束时间
                    dom.UserOperate                         // 8操作类型 UserOperate （0=开始报名，1=等待审核，2=审核未通过，3=进入考试）
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

// 加载已参加大赛列表
function GetStartList(sort) {
    pageHelper.Init({
        url: "/CompetitionUser/MatchList/MatchJoinList",
        type: "POST",
        pageDiv: "#pages",
        async: false,
        data: {
            sort: sort,
            random: Math.random()
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
                trHtml += "<td>{6} - {7}</td>";
                trHtml += "<td>{8}</td>";
                trHtml += "<td class=\"operate\">";
                if (dom.UserOperate == 3) {
                    trHtml += "<a class=\"d_shanchu\" data-paper=\"{9}\" href='javascript:StartExam({4},{5},{9});'>进入考试</a>";
                }
                if (dom.UserOperate == 4) {
                    trHtml += "<a class=\"d_shanchu\" data-paper=\"{9}\" href='javascript:ViewExamPaper({4},{5},{9});''>查看</a>";
                }
                if (dom.UserOperate == 6) {
                    trHtml += "<a class=\"d_shanchu\" href='javascript:Void(0);''>缺考</a>";
                }
                if (dom.UserOperate == 7) {
                    trHtml += "<a class=\"d_shanchu\" href='javascript:Void(0);''>未开始</a>";
                }
                trHtml += "</td>";
                trHtml += "</tr>";
                //拼接tbody
                html += StringHelper.FormatStr(trHtml,
                    ((data.PageIndex - 1) * data.PageSize + index + 1),     //0 序号
                    dom.Name,                                       //1 竞赛名称
                    dom.AddUserName,                                           //2 创建人
                    dom.Type == 1 ? "单项理论赛" : dom.Type == 2 ? "单项实训赛" : "复合赛",      //3 比赛类型
                    dom.Id,                                        //4 Id
                    dom.Type,                        // 5 竞赛类型
                    dom.Type != 32 ? dom._PreliminaryStartTime : dom._RematchStartTime,                        // 6 开始时间
                    dom.Type != 32 ? dom._PreliminaryEndTime : dom._RematchEndTime,                       // 7 结束时间
                    dom.IsRelease == 1 ? "未结束" : "已结束",                // 8 状态
                    dom.PaperId == null ? 0 : dom.PaperId                               // 9 理论试卷Id
                );
            });
            if (data.Data == "" || data.Data == null) {
                html += "<tr><td  colspan='7'>未找到相关记录！</td>";
                $("#startList").html(html);
            } else {
                $("#startList").html(html);
            }
        }
    });
}

// 加载大赛成绩列表
function GetMatchResult(matchType, keyword) {
    pageHelper.Init({
        url: "/CompetitionUser/MatchList/MatchResultList",
        type: "POST",
        pageDiv: "#pages",
        async: false,
        data: {
            matchType:matchType,
            keyword: keyword,
            random: Math.random()
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
                trHtml += "<td>{6} - {7}</td>";
                trHtml += "<td>{8}</td>";
                trHtml += "<td class=\"operate\">";
                if (dom.UserOperate == 3) {
                    trHtml += "<a class=\"d_shanchu\" href='javascript:Void(0);'>————</a>";
                }
                else {
                    trHtml += "<a class=\"d_shanchu\" href='javascript:PaperView({4},{5},{9});'>查看成绩</a>";
                }
                trHtml += "</td>";
                trHtml += "</tr>";
                //拼接tbody
                html += StringHelper.FormatStr(trHtml,
                    ((data.PageIndex - 1) * data.PageSize + index + 1),     //0 序号
                    dom.Name,                                       //1 竞赛名称
                    dom.AddUserName,                                           //2 创建人
                    dom.Type == 1 ? "单项理论赛" : dom.Type == 2 ? "单项实训赛" : "复合赛",      //3 比赛类型
                    dom.Id,                                        //4 Id
                    dom.Type,                        // 5 竞赛类型
                    dom.Type != 32 ? dom._PreliminaryStartTime : dom._RematchStartTime,                        // 6 开始时间
                    dom.Type != 32 ? dom._PreliminaryEndTime : dom._RematchEndTime,                       // 7 结束时间
                    dom.IsRelease == 1 ? "未结束" : "已结束",                // 8 状态
                    dom.PaperId == null ? 0 : dom.PaperId                                 // 9 理论试卷Id
                );
            });
            if (data.Data == "" || data.Data == null) {
                html += "<tr><td  colspan='7'>未找到相关记录！</td>";
                $("#startList").html(html);
            } else {
                $("#startList").html(html);
            }
        }
    });
}

// 根据大赛ID获取考试内容
function StartExam(matchId, matchType, curPaperId) {
    var userId = $("#hdUserId").val();
    if (matchType == 1 || matchType == 31) {
        //var curPaperId = $(this).attr("data-paper");
        if (curPaperId == null || curPaperId == 0) {
            dialogHelper.Error({
                content: "试卷获取失败！"
            });
        }
        else {
            
            location.href = "/CompetitionUser/EheoryExamine/DoExam?Id=" + curPaperId + "&LibraryId=1&UserId=" + userId;
        }
    }
    else {
        $.ajax({
            url: "/CompetitionUser/MatchList/GetExamPaper",
            async: false,
            type: "POST",
            data: {
                MatchId: matchId,
                MatchType: matchType,
                random: Math.random()
            },
            success: function (data) {
                if (data.IsSuccess == true) {
                    if (matchType == 1 || matchType == 31) {
                        //EditProposal(data.Data.PaperId);
                        location.href = "/CompetitionUser/EheoryExamine/DoExam?Id=" + data.Data.PaperId + "&LibraryId=1&UserId=" + userId;
                    }
                    else {
                        if (data.Data.Type != 2) {
                            EditProposal2(data.Data.TrainExamId, data.Data.ProposalId, matchId);
                        }
                        else {
                            location.href = "/CompetitionUser/ProposalCustomer/CaseView?TrainExamId=" + data.Data.TrainExamId;
                            //dialogHelper.Error({
                            //    content: "该组已经有人在考试了"
                            //});
                        }
                    }
                }
                else {
                    dialogHelper.Error({
                        content: data.ErrorCode
                    });
                }
            }
        });
    }
}

// 已参加-查看试卷
function ViewExamPaper(matchId, matchType, curPaperId) {
    var userId = $("#hdUserId").val();
    if (matchType == 1 || matchType == 31) {
        //var curPaperId = $(this).attr("data-paper");
        if (curPaperId == null || curPaperId == 0) {
            dialogHelper.Error({
                content: "试卷获取失败！"
            });
        }
        else {
            location.href = "/CompetitionUser/EheoryExamine/DoExam?Id=" + curPaperId + "&LibraryId=1&type=1&UserId=" + userId;
        }
    }
    else {
        $.ajax({
            url: "/CompetitionUser/MatchList/ViewExamPaper",
            async: false,
            type: "POST",
            data: {
                MatchId: matchId,
                MatchType: matchType,
                random: Math.random()
            },
            success: function (data) {
                if (matchType == 1 || matchType == 31) {
                    location.href = "/CompetitionUser/EheoryExamine/DoExam?Id=" + data.Data.PaperId + "&LibraryId=1&type=1&UserId=" + userId;
                }
                else {
                    location.href = "/CompetitionUser/ProposalCustomer/PreviewIndex?ProposalId=" + data.Data.ProposalId;
                }
            }
        });
    }
}

//进入考试（理论/初赛）
function EditProposal(Id) {
    var type = $("#hideType").val();
    // 进入考试后添加试卷得分情况
    $.ajax({
        url: "/CompetitionUser/EheoryExamine/AddPaperUserSummary",
        type: "POST",
        async: false,
        dataType: "json",
        data: {
            ExamPaperId: Id,
            LibraryId: 1,
            random: Math.random()
        },
        success: function (data) {
            if (data) {
                //var link = "/CompetitionUser/EheoryExamine/DoExam?Id=" + Id + "&LibraryId=1";
                //ope(link);
                location.href = "/CompetitionUser/EheoryExamine/DoExam?Id=" + Id + "&LibraryId=1";
            }
        }
    });
}

var win;
function ope(link) {
    //打开窗口，'fullscreen'控制新窗口全屏显示
    win = window.open(link, 'gta', 'fullscreen=1,menubar=0,toolbar=0,directories=0,location=0,status=0');
}

//进入考试（实训/复赛）
function EditProposal2(TrainExamId, ProposalId, MatchId) {
    //判断是否已结束考核
    $.ajax({
        url: "/CompetitionUser/ProposalCustomer/CheckExamDate",
        type: "POST",
        async: false,
        dataType: "json",
        data: {
            TrainExamId: TrainExamId,
            random: Math.random()
        },
        success: function (data) {
            location.href = "/CompetitionUser/ProposalCustomer/Index?TrainExamId=" + TrainExamId + "&ProposalId=" + ProposalId + "&MatchId=" + MatchId;
        }
    });
}

//查看试卷
function PaperView(matchId, matchType, curPaperId) {
    if (matchType == 1 || matchType == 31) {
        if (curPaperId == null || curPaperId == 0) {
            dialogHelper.Error({
                content: "试卷获取失败！"
            });
        }
        else {
            location.href = "/CompetitionUser/MatchResult/PaperExamView?Id=" + curPaperId + "&LibraryId=1&type=1";
        }
    }
    else {
        $.ajax({
            url: "/CompetitionUser/MatchResult/GetExamPaper",
            async: false,
            type: "POST",
            data: {
                MatchId: matchId,
                MatchType: matchType,
                random: Math.random()
            },
            success: function (data) {
                if (data != null && data.Data != null) {
                    if (matchType == 1 || matchType == 31) {
                        if (data.Data.PaperId > 0) {
                            location.href = "/CompetitionUser/MatchResult/PaperExamView?Id=" + data.Data.PaperId + "&LibraryId=1&type=1";
                        }
                        else {
                            dialogHelper.Error({
                                content: data.ErrorCode
                            });
                        }
                    }
                    else {
                        if (data.Data.AssessmentResultsId > 0) {
                            location.href = "/CompetitionUser/MatchResult/TrainExamView?AssessmentResultsId=" + data.Data.AssessmentResultsId + "&MatchName=" + escape(data.Data.MatchName);
                        }
                        else {
                            dialogHelper.Error({
                                content: data.ErrorCode
                            });
                        }
                    }
                }
            }
        });
    }
}

// 查看大赛
function ViewMatch(matchId) {
    dialogHelper.Show('popMatchInfo', 500);

    // 加载编辑页面
    $.ajax({
        url: '/CompetitionUser/MatchList/GetModel',
        data: {
            id: matchId,
            random: Math.random()
        },
        success: function (data) {
            if (data.IsSuccess) {
                var isRelease = data.Data.IsRelease;                // 发布类型
                var type = data.Data.Type;                // 竞赛类型
                var state = data.Data.State;              // 比赛进行状态
                var cscjcjfblx = data.Data.PreliminaryResultType;           // 初赛成绩发布类型
                var cscjcjfbsj = data.Data.PreliminaryResultTime;           // 初赛成绩发布时间
                var fscjcjfblx = data.Data.RematchResultType;           // 复赛成绩发布类型
                var fscjcjfbsj = data.Data.RematchResultTime;           // 复赛成绩发布时间

                // 加载竞赛信息
                $('#lblRelease').html(isRelease == 0 ? "未发布" : isRelease == 1 ? "已发布" : "已结束");
                $('#lblMatchTopName').html(data.Data.Name);              // 竞赛名称
                $('#lblMatchName').html(data.Data.Name);
                $('#lblType').html(type == 1 ? "单项理论赛" : type == 2 ? "单项实训赛" : "复合赛");
                $('#hideType').val(type);
                $('#hideRelease').val(isRelease);
                $('#lblBMStartTime').html(data.Data._RegistrationStartTime);             // 报名开始时间
                $('#lblBMEndTime').html(data.Data._RegistrationEndTime);             // 报名结束时间
                $('#lblBMEndTime').html(data.Data._RegistrationEndTime);             // 报名结束时间
                $('#lblBSStartTime').html(data.Data._PreliminaryStartTime);            // 初赛开始时间
                $('#lblBSEndTime').html(data.Data._PreliminaryEndTime);            // 初赛结束时间
                $('#lblFSStartTime').html(data.Data._RematchStartTime);            // 复赛结束时间
                $('#lblFSEndTime').html(data.Data._RematchEndTime)            // 复赛结束时间

                $('#lblPFStartTime').html(data.Data._ScoreStartTime);             // 评分开始时间
                $('#lblPFEndTime').html(data.Data._ScoreEndTime);             // 评分结束时间
                $('#lblJSXZ').html(data.Data.Information);             // 竞赛须知
                $('#lblRWRS').html(data.Data.FinalistNumber);             // 复赛入围人(组)数

                // 未发布的比赛只显示基本信息
                if (isRelease == 0) {
                    $('#divDisplay1').html('');
                    $('#divDisplay2').html('');
                    $('#divDisplay3').html('');
                    $('#divDisplay4').html('');
                } else {
                    $('#divDisplay1').css('display', 'block');
                    $('#trAddUserName').css('display', '');
                    $('#lblAddUserName').html(data.Data.AddUserName);
                }

                // 单项理论赛
                if (type == 1) {
                    // 单项理论赛，只有报名时间、比赛时间、成绩发布
                    // 隐藏单项赛不需要的表单
                    $('#lblBSSJ').html('比赛时间');          // 比赛时间名称更改，可设置为初赛时间
                    $('#trFHS_FS').hide();         // 复赛时间
                    $('#trPFSJ').hide();            // 评分时间
                    $('#trFSCJFB').hide();              // 复赛成绩手动发布
                    $('#trFSCJFB2').hide();             // 复赛成绩定时发布
                    $('#trRWRS').hide();                // 复赛入围
                    $('#divJudge').hide();              // 评委信息
                    $('#lblPFName').html('成绩发布');
                    $('#divTag').hide();                // 信息提示

                    // 未发布状态
                    if (isRelease == 0) {

                    } else {
                        // 隐藏编辑按钮
                        $('.d_weifabu1 a').remove();
                    }
                }
                else
                    if (type == 2) {
                        // 单项实训赛，只有报名时间、比赛时间、评分时间、成绩发布，需要关联评委
                        // 隐藏单项赛不需要的表单
                        $('#lblBSSJ').html('比赛时间');          // 比赛时间名称更改，可设置为初赛时间
                        $('#trFHS_FS').hide();          //  复赛时间
                        $('#trPFSJ').show();            // 评分时间
                        $('#trRWRS').hide();                // 复赛入围
                        $('#lblPFName').html('成绩发布');
                        $('#trFSCJFB').hide();              // 复赛成绩发布
                        $('#trFSCJFB2').hide();              // 复赛成绩发布
                        $('#divTag').hide();                // 信息提示

                        // 未发布状态
                        if (isRelease == 0) {

                        } else {
                            // 隐藏编辑按钮
                            $('.d_weifabu1 a').remove();
                        }
                    }
                    else if (type == 3) {
                        // 复合赛显示全部表单，有报名时间、初赛时间、复赛时间、评分时间、初赛成绩发布、复赛成绩发布
                        $('#lblBSSJ').html('初赛时间');          // 比赛时间名称更改，可设置为初赛时间

                        if (isRelease == 2) {
                            // 竞赛已结束，隐藏编辑按钮
                            $('.d_weifabu1 a').remove();
                        }
                    }
                // 加载评委信息
                var html = '';
                $(data.Data.ListUserInfo).each(function (index, item) {
                    var trhtml = '<tr rel="{6}">';
                    trhtml += '<td>{0}</td>';
                    trhtml += '<td>{1}</td>';
                    trhtml += '<td>{2}</td>';
                    trhtml += '<td>{3}</td>';
                    trhtml += '<td>{4}</td>';
                    trhtml += '<td>{5}</td>';
                    trhtml += '</tr>';
                    //拼接tbody
                    html += StringHelper.FormatStr(trhtml,
                        (index + 1),     //0 序号
                        item.AccountNo,                                       //1 账号
                        item.UserName,                                           //2 姓名
                        item.Email,                                            //3 邮箱
                        item.CollegeName,                                        //4 学院
                        item.Phone,                                          //5 手机
                        item.Id                                                  //6 Id
                    );
                });
                $('#createMatchAddJudge').html(html);

                // 已发布
                if (isRelease == 1) {
                    // 复合赛，初赛结束后
                    if (type == 3 && state > 303) {
                        $('#MatchResult').show();
                    }
                }
                // 已结束
                if (isRelease == 2) {
                    // 复合赛，初赛结束后
                    if (type != 2) {
                        $('#MatchResult').show();
                    }
                }

            }
            else {
                dialogHelper.Error({
                    content: data.ErrorCode,
                });
            }
        }
    })
}

// 大赛报名
function ViewApply(matchId) {

    // 加载编辑页面
    $.ajax({
        url: '/CompetitionUser/MatchList/Apply',
        data: {
            id: matchId,
            random: Math.random()
        },
        success: function (data) {
            if (data.IsSuccess) {
                dialogHelper.Show('popApply', 400);
                $("#hideMatchId").val(matchId);
                if (data.Data.model != null && data.Data.model.length > 0) {
                    var trHtml = "";
                    var trHtml2 = "";
                    var isjujue = false;
                    $(data.Data.model).each(function (index, dom) {
                        //var strName = dom.UserId == data.Data.selfId ? "本人" : "队友";
                        var strStatus = dom.ApplyStatus == 0 ? "等待操作" : (dom.ApplyStatus == 1 ? "已同意" : "已拒绝");
                        trHtml += "<tr>";
                        trHtml += "<td >" + dom.UserName + "</td>";
                        trHtml += "<td style='width:50%;'>" + dom.IDNum + "</td>";
                        trHtml += "<td>" + strStatus + "</td>";
                        trHtml += "</tr>";

                        if (dom.ApplyStatus == 0 && data.Data.selfId == dom.UserId) {
                            trHtml2 += "<input class='btn btn-small btn-blue' type='button' onclick='ApplySelect(1)' value='同意'>";
                            trHtml2 += "<input class='btn btn-small btn-blue btn-close' type='button' onclick='ApplySelect(2)' value='拒绝'>";
                        }
                        if (dom.ApplyStatus == 2 && data.Data.selfId == dom.UserId) {
                            isjujue = true;
                        }
                    });
                    if (isjujue)
                        return;
                    $("#applymember").html("");
                    $("#applymember").html(trHtml);

                    $("#popApplyBtn").html("");
                    if (data.Data.selfId == data.Data.model[0].ApplyUser) {
                        trHtml2 += "<input class='btn btn-small btn-blue' type='button' onclick='Dissolve()' value='解散队伍'>";
                    }
                    $("#popApplyBtn").html(trHtml2);
                }

                $("#duiyou1").blur(function () {
                    var duiyou1 = $("#duiyou1").val();
                    if (duiyou1 != null && duiyou1 != "") {
                        $.ajax({
                            url: '/CompetitionUser/MatchList/GetUserName',
                            data: {
                                IDNum: duiyou1,
                                random: Math.random()
                            },
                            success: function (data) {
                                if (data.IsSuccess) {
                                    $("#duiyou1name").html(data.Data);
                                }
                            }
                        })
                    }
                });

                $("#duiyou2").blur(function () {
                    var duiyou2 = $("#duiyou2").val();
                    if (duiyou2 != null && duiyou2 != "") {
                        $.ajax({
                            url: '/CompetitionUser/MatchList/GetUserName',
                            data: {
                                IDNum: duiyou2,
                                random: Math.random()
                            },
                            success: function (data) {
                                if (data.IsSuccess) {
                                    $("#duiyou2name").html(data.Data);
                                }
                            }
                        })
                    }
                });
            }
            else {
                dialogHelper.Error({
                    content: data.ErrorCode,
                });
            }
        }
    })
}

// 确认报名
function Entry() {
    if (!VerificationHelper.checkFrom("popApply")) {
        return;
    }
    var duiyou1 = $("#duiyou1").val();
    var duiyou2 = $("#duiyou2").val();
    var matchId = $("#hideMatchId").val();
    // 加载编辑页面
    $.ajax({
        url: '/CompetitionUser/MatchList/ApplyEntry',
        data: {
            MatchId: matchId,
            IDNumA: duiyou1,
            IDNumB: duiyou2,
            random: Math.random()
        },
        success: function (data) {
            if (data.IsSuccess) {
                dialogHelper.Success({
                    content: "报名成功！",
                    success: function () {
                        location.href = location.href;
                    }
                });
            }
            else {
                dialogHelper.Error({
                    content: data.ErrorCode,
                });
            }
        }
    })
}

// 1.同意/2.拒绝
function ApplySelect(status) {
    var matchId = $("#hideMatchId").val();
    $.ajax({
        url: '/CompetitionUser/MatchList/ApplySelect',
        data: {
            ApplyStatus: status,
            MatchId: matchId,
            random: Math.random()
        },
        success: function (data) {
            if (data.IsSuccess) {
                dialogHelper.Success({
                    content: "操作成功！",
                    success: function () {
                        location.href = location.href;
                    }
                });
            }
            else {
                dialogHelper.Error({
                    content: data.ErrorCode,
                });
            }
        }
    })
}

// 解散
function Dissolve() {
    dialogHelper.Confirm({
        content: "确认要解散队伍吗？",
        success: function () {
            var matchId = $("#hideMatchId").val();
            $.ajax({
                url: '/CompetitionUser/MatchList/ApplyDissolve',
                data: {
                    MatchId: matchId,
                    random: Math.random()
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        dialogHelper.Success({
                            content: "操作成功！",
                            success: function () {
                                location.href = location.href;
                            }
                        });
                    }
                    else {
                        dialogHelper.Error({
                            content: data.ErrorCode,
                        });
                    }
                }
            });
        }
    });
}