$(function () {

    // 加载编辑页面
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


// 加载比赛用户
function loadUser() {
    var id = $('#hideMatchId').val();
    $.ajax({
        url: '/CompetitionAdmin/Match/UserSet/' + id,
        type: "GET",
        success: function (data) {
            GetList(1);          // 加载用户列表

            $('#divDisplay2').html($(data).find('.d_yonghu3').eq(0));
            $('#divDisplay2 .d_yonghu3').find('.d_yonghu2_1').eq(1).remove();             // 隐藏操作按钮
            // 去除最后一列
            $('#divDisplay2 .d_yonghu3').find('.temp').remove();
        }
    })
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