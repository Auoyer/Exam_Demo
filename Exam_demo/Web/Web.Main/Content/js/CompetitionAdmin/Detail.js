$(function () {

    loadInfo();

    // 更改初赛成绩发布类型
    $('#rdCSSDFB').click(function () {
        // 手动发布，禁用定时发布时间文本框
        $('#lblCSCJDSFB').attr('disabled', 'disabled');
        $('#lblCSCJDSFB').removeClass('IsRequired');
    })
    $('#rdCSDSFB').click(function () {
        // 自动发布，启用定时发布时间文本框
        $('#lblCSCJDSFB').removeAttr('disabled');
        $('#lblCSCJDSFB').addClass('IsRequired');
    })
    // 更改复赛成绩发布类型
    $('#rdFSSDFB').click(function () {
        // 手动发布，禁用定时发布时间文本框
        $('#lblFSCJDSFB').attr('disabled', 'disabled');
        $('#lblFSCJDSFB').removeClass('IsRequired');
    })
    $('#rdFSDSFB').click(function () {
        // 自动发布，启用定时发布时间文本框
        $('#lblFSCJDSFB').removeAttr('disabled');
        $('#lblFSCJDSFB').addClass('IsRequired');
    })

})


// 加载竞赛页面
function loadInfo() {
    $.ajax({
        url: '/CompetitionAdmin/Match/GetModel',
        data: {
            id: $('#hideMatchId').val(),
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

                if (data.Data.PreliminaryResultType == 1) {
                    // 初赛成绩发布类型为手动发布
                    $('#lblCSCJDSFB').html('手动发布');
                }
                else if (data.Data.PreliminaryResultType == 2) {
                    // 初赛成绩发布类型为定时发布
                    $('#lblCSCJDSFB').html('定时发布：' + data.Data._PreliminaryResultTime);
                }

                if (data.Data.RematchResultType == 1) {
                    // 复赛成绩发布类型为手动发布
                    $('#lblFSCJDSFB').html('手动发布');
                }
                else if (data.Data.RematchResultType == 2) {
                    // 复赛成绩发布类型为定时发布
                    $('#lblFSCJDSFB').html('定时发布：' + data.Data._RematchResultTime);
                }

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
                    trhtml += '<td>{7}</td>';
                    trhtml += '</tr>';
                    //拼接tbody
                    html += StringHelper.FormatStr(trhtml,
                        (index + 1),     //0 序号
                        item.AccountNo,                                       //1 账号
                        item.UserName,                                           //2 姓名
                        item.Email,                                            //3 邮箱
                        item.CollegeName,                                        //4 学院
                        item.Phone,                                          //5 手机
                        item.Id,                                                  //6 Id
                        item.Status == 1 ? "失效" : item.Status == 2 ? "正常" : "删除"    //7 状态
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
                    success: function () {
                        history.go(-1);
                    }
                });
            }
        }
    })
}

// 加载比赛用户
function loadUser() {
    var matchId = $('#hideMatchId').val();             // 竞赛ID
    var release = $('#hideRelease').val();

    pageHelper.Init({
        url: "/CompetitionAdmin/Match/UserSetList",
        type: "POST",
        pageDiv: "#pages_UserList",
        data: {
            id: matchId,
            name: $("#searchName").val(),
            souce: $('#selGroupSouce').val(),                // 分组来源
            isAudit: 1,               // 审核状态
            isDetail: 0                 // 不验证竞赛状态
            //pageIndex: 1,
            //pageSize: 10
        },
        bind: function (data) {
            var html = "";
            $(data.Data).each(function (index, dom) {
                //每行html
                var trHtml = "";
                trHtml += "<tr>";
                trHtml += "<td>{0}</td>";
                trHtml += "<td><div class=\"ellipsis\">{1}</div></td>";
                trHtml += "<td><div class=\"ellipsis userNames\" userIds=\"{7}\">{2}</div></td>";
                trHtml += "<td title='" + dom.CollegeName + "'>{6}</td>";
                trHtml += "<td class='csqk_'>{8}</td>";
                trHtml += "<td>{3}</td>";
                trHtml += "</tr>";
                //拼接tbody
                html += StringHelper.FormatStr(trHtml,
                    ((data.PageIndex - 1) * data.PageSize + index + 1),     //0 序号
                    dom.TeamNumber,                                       //1 组队人数
                    dom.UserNames,                                           //2 姓名
                    dom.GroupSouce == 1 ? "批量导入" : dom.GroupSouce == 2 ? "手动导入" : "报名审核",      //3 赛组来源
                    dom.GroupId,                                       //4 分组Id
                    matchId,                        // 5
                    dom.CollegeName.toString().ToLeft(16),             // 6 学校名称
                    dom.UserIds,             // 7 用户Ids
                    dom.IsFinal == 0 ? "初赛" : "复赛"               // 8 是否进入复赛
                );
            });
            $("#userList").html(html);

            $("#userList .userNames").each(function (index, item) {
                var arrName = $(this).html().split(' | ');
                var arrIds = $(this).attr('userIds').split(',');
                var str = '';
                for (var i = 0; i < arrName.length; i++) {
                    str += '<a onclick="OpenInfo(' + arrIds[i] + ')" title="' + arrName[i] + '">' + arrName[i].toString().ToLeft(6) + '</a>';
                }
                $(this).html(str);
            })

            if (release == 2) {
                $("#divDisplay2  .csqk_").show();
            }
            else
                $("#divDisplay2 .csqk_").hide();
        }
    });
}

// 查看用户信息
function OpenInfo(userId) {
    $.ajax({
        url: '/CompetitionAdmin/User/Detail',
        type: "GET",
        //async: false,
        data: { uid: userId },
        success: function (data) {
            $('#popInfo').html(data);
            dialogHelper.Show("popInfo");
        }
    });
}


// 返回，根据竞赛发布状态不同，跳转不同页面
function GoBack() {
    var release = $('#hideRelease').val();

    if (release == 0)
        window.location = '/CompetitionAdmin/Match/NotStart';
    else if (release == 1)
        window.location = '/CompetitionAdmin/Match/Start';
    else if (release == 2)
        window.location = '/CompetitionAdmin/Match/End';
}

// 加载比赛内容
function loadContent() {
    var MatchId = $("#hideMatchId").val();
    var MatchType = $("#hideType").val();
    $.ajax({
        url: '/CompetitionAdmin/Paper/MatchContent/',
        type: "GET",
        success: function (data) {
            $('#divDisplay3').show();
            $('#divDisplay3').html($(data).find('.d_yonghu2_2').eq(0));
            $('.textr a').remove();
            $('.MatchContentA .textr').prepend("<a class='d_lanbaia ml10' onclick='EditQuestionsLoading()'>查看详情</a>");
            if (MatchType == 1 || MatchType == 3) {
                $(".MatchContentA").show();
                if (MatchType != 3) {
                    $("h4").html("竞赛内容");
                }
                // 根据竞赛Id获取理论试卷Id
                var curPaperId = GetPaperId(MatchId);
                $("#hidePaperId").val(curPaperId);
                // 根据试卷ID获取分数信息
                LodingTestPapers(curPaperId);
            }
            if (MatchType == 2 || MatchType == 3) {
                $(".MatchContentB").show();
                if (MatchType != 3) {
                    $("h4").html("竞赛内容");
                }
                // 获取大赛考核案例列表
                LodingTrainCase(MatchId);
            }

            //$("#KeyWord").unbind("focus").focus(function () {
            //    $("#KeyWord").val("").css("color", "black");
            //});
        }
    })
}

// 加载比赛成绩
function loadResult() {
    var release = $('#hideRelease').val();
    var MatchId = $("#hideMatchId").val();
    var MatchType = $("#hideType").val();
    if (release == 1 || MatchType != 3) {
        // 已发布未结束 || 理论赛
        $('#score3').hide();
    }
    loadResultList("");

    // 搜索
    $("#btnSearch").unbind("click").click(function () {
        var KeyWord = "";
        var key = $.trim($("#txtKeyWord").val()).replace(/[<>&"]/g, function (c) { return { '<': '&lt;', '>': '&gt;', '&': '&amp;', '"': '&quot;', '"': '&quot;', "'": '&prime;', "'": '&prime;' }[c]; });
        //if (key == "姓名/学校")
        //{ key = ""; }
        if (key != null && key != "" && key.length > 0) {
            KeyWord = key;
        }
        loadResultList(KeyWord);
    });

    //$("#txtKeyWord").unbind("focus").focus(function () {

    //    $("#txtKeyWord").val("").css("color", "black");
    //});

    $(".thSort").click(function () {
        var sortscore = $(this).attr("data-sort");
        $("#hideSortId").val(sortscore);
        $(".sort-tip").html("");
        $(this).find('.sort-tip').eq(0).html("↓");
        $('#btnSearch').trigger("click");
    });
}

function loadResultList(keyWord) {
    var matchId = $('#hideMatchId').val();
    var type = $('#hideType').val();
    var release = $('#hideRelease').val();
    var sortScore = $('#hideSortId').val();

    pageHelper.Init({
        url: "/CompetitionAdmin/Paper/GetResultList",
        type: "POST",
        pageDiv: "#ResultPage",
        data:
        {
            MatchId: matchId,
            KeyWords: keyWord,
            Release: release,
            SortScore: sortScore,
            rId: Math.random()
        },
        bind: function (data) {
            // 已发布未结束
            if (release == 1 || type != 3) {
                var html = "";
                $(data.Data).each(function (index, dom) {
                    //每行html
                    var trHtml = "";
                    trHtml += "<tr>";
                    trHtml += "<td name=\"dataNo\">{0}</td>";
                    trHtml += "<td><div class=\"ellipsis\" title=\"{1}\">{1}</div></td>";
                    //trHtml += "<td><div class=\"ellipsis\" title=\"{2}\">{2}</div></td>";
                    trHtml += "<td><div class=\"ellipsis\" title=\"{3}\">{3}</div></td>";
                    trHtml += "<td><div class=\"ellipsis\" title=\"{4}\">{4}</div></td>";
                    trHtml += "<td><div class=\"ellipsis\">{9}</div></td>";
                    trHtml += "<td><div class=\"ellipsis\">{5}</div></td>";
                    trHtml += "<td class=\"operate\">";
                    if (dom.ExamPaperId != 0) {
                        trHtml += "<a class=\"edit\" title=\"查看\" href=\"javascript:ViewPaper({7},{8});\">查看</a>";
                    }
                    else {
                        trHtml += "<a class=\"edit\" title=\"缺考\" href=\"javascript:void(0);\">缺考</a>";
                    }
                    trHtml += "</td></tr>";
                    //拼接tbody
                    html += StringHelper.FormatStr(trHtml,
                        ((data.PageIndex - 1) * data.PageSize + index + 1),   //0 序号
                        dom.AccountNo,                                  //1 客户账号
                        dom.Phone,                                      //2 联系方式
                        dom.UserName,                                   //3 姓名
                        dom.CollegeName,                                //4 学校
                        dom.EvaScore.toFixed(2),                        //5 小组成绩
                        dom.Id,                                         //6 Id
                        dom.UserId,                                     //7 UserId
                        dom.ExamPaperId,                                //8 试卷Id 
                        dom.Score                                      //9 个人成绩                               
                        );
                });
                if (data.Data == "" || data.Data == null) {
                    html += "<tr><td  colspan='7'>未找到相关记录！</td>";
                    $("#ResultList").html(html);
                } else {
                    $("#ResultList").html(html);
                }
            }
            else {
                var html = "";
                $(data.Data).each(function (index, dom) {
                    //每行html
                    var trHtml = "";
                    trHtml += "<tr>";
                    trHtml += "<td name=\"dataNo\">{0}</td>";
                    trHtml += "<td><div class=\"ellipsis\" title=\"{1}\">{1}</div></td>";
                    //trHtml += "<td><div class=\"ellipsis\" title=\"{2}\">{2}</div></td>";
                    trHtml += "<td><div class=\"ellipsis\" title=\"{3}\">{3}</div></td>";
                    trHtml += "<td><div class=\"ellipsis\" title=\"{4}\">{4}</div></td>";
                    trHtml += "<td><div class=\"ellipsis\" title=\"{10}\">{10}</div></td>";
                    trHtml += "<td><div class=\"ellipsis\" title=\"{5}\">{5}</div></td>";
                    trHtml += "<td><div class=\"ellipsis\">{6}</div></td>";
                    trHtml += "<td class=\"operate\">";
                    if (dom.ExamPaperId != 0) {
                        trHtml += "<a class=\"edit\" title=\"查看\" href=\"javascript:ViewPaper({8},{9});\">查看</a>";
                    }
                    else {
                        trHtml += "<a class=\"edit\" title=\"缺考\" href=\"javascript:void(0);\">缺考</a>";
                    }
                    trHtml += "</td></tr>";
                    //拼接tbody
                    html += StringHelper.FormatStr(trHtml,
                        ((data.PageIndex - 1) * data.PageSize + index + 1),   //0 序号
                        dom.AccountNo,                                  //1 客户账号
                        dom.Phone,                                      //2 联系方式
                        dom.UserName,                                   //3 姓名
                        dom.CollegeName,                                //4 学校
                        dom.EvaScore.toFixed(2),                        //5 理论小组成绩
                        dom.SubjectiveResults + dom.ObjectiveResults,   //6 实训成绩
                        dom.Id,                                         //7 Id
                        dom.UserId,                                     //8 UserId
                        dom.ExamPaperId,                                //9 试卷Id 
                        dom.Score                                       //10 理论个人成绩
                        );
                });
                if (data.Data == "" || data.Data == null) {
                    html += "<tr><td  colspan='8'>未找到相关记录！</td>";
                    $("#ResultList").html(html);
                } else {
                    $("#ResultList").html(html);
                }
            }

        }
    });


}

function ViewPaper(UserId, PaperId) {
    location.href = "/CompetitionAdmin/Paper/DoExam?Id=" + PaperId + "&UserId=" + UserId + "&LibraryId=1&type=1";

}