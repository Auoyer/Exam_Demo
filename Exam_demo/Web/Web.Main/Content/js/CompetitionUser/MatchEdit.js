$(function () {

    // 加载编辑页面
    $.ajax({
        url: '/CompetitionAdmin/Match/GetModel',
        data: {
            id: $('#hideMatchId').val()
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
                $('#hideState').val(data.Data.State);
                $('#txtBMStartTime').val(data.Data._RegistrationStartTime);             // 报名开始时间
                $('#txtBMEndTime').val(data.Data._RegistrationEndTime);             // 报名结束时间
                $('#txtBMEndTime').val(data.Data._RegistrationEndTime);             // 报名结束时间
                $('#txtBSStartTime').val(data.Data._PreliminaryStartTime);            // 初赛开始时间
                $('#txtBSEndTime').val(data.Data._PreliminaryEndTime);            // 初赛结束时间
                $('#txtFSStartTime').val(data.Data._RematchStartTime);            // 复赛结束时间
                $('#txtFSEndTime').val(data.Data._RematchEndTime)            // 复赛结束时间
                $('#txtPFStartTime').val(data.Data._ScoreStartTime);             // 评分开始时间
                $('#txtPFEndTime').val(data.Data._ScoreEndTime);             // 评分结束时间
                $('#txtJSXZ').val(data.Data.Information);             // 竞赛须知
                $('#txtRWRS').val(data.Data.FinalistNumber);             // 复赛入围人(组)数

                // 判断报名开始时间的最小值
                if ($.trim(data.Data._RegistrationStartTime) == '') {
                    //$('#txtBMStartTime').attr('onfocus', 'WdatePicker({ minDate: \'#F{$dp.$D(\\\'txtBSEndTime\\\')}\' })');            // 没有设置报名时间时，报名时间应晚于当前时间
                }
                else {
                    $('#txtBMStartTime').attr('onfocus', 'WdatePicker({ minDate: \'' + data.Data._RegistrationStartTime + '\' , dateFmt: "yyyy-MM-dd HH:mm"})');            // 没有设置报名时间时，报名时间应晚于当前时间
                }


                // 加载成绩发布时间和类型
                if (cscjcjfblx == 1) {
                    $('#rdCSSDFB').attr("checked", "checked");              // 成绩手动发布
                    $('#txtCSCJDSFB').attr('disabled', 'disabled').val('');             // 发布时间
                } else if (cscjcjfblx == 2) {
                    $('#rdCSDSFB').attr("checked", "checked");
                    $('#txtCSCJDSFB').val(data.Data._PreliminaryResultTime);             // 发布时间
                }

                if (fscjcjfblx == 1) {
                    $('#rdFSSDFB').attr("checked", "checked");              // 成绩手动发布
                    $('#txtFSCJDSFB').attr('disabled', 'disabled').val('');             // 发布时间
                } else if (fscjcjfblx == 2) {
                    $('#rdFSDSFB').attr("checked", "checked");
                    $('#txtFSCJDSFB').val(data.Data._RematchResultTime);             // 发布时间
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
                        $('#txtCSCJDSFB').attr('onfocus', 'WdatePicker({ minDate: \'#F{$dp.$D(\\\'txtBSEndTime\\\')}\', dateFmt: "yyyy-MM-dd HH:mm" })');            // 成绩发布时间应晚于比赛结束时间
                    } else {
                        // 已发布的单项理论赛，禁用报名时间、比赛时间、成绩发布，隐藏提交按钮
                        $('#txtBMStartTime').attr('disabled', 'disabled');
                        $('#txtBMEndTime').attr('disabled', 'disabled');

                        $('#txtBSStartTime').attr('disabled', 'disabled');
                        $('#txtBSEndTime').attr('disabled', 'disabled');

                        $('#txtCJFBStartTime').attr('disabled', 'disabled');
                        $('#txtCJFBEndTime').attr('disabled', 'disabled');

                        $('#rdCSSDFB').attr('disabled', 'disabled');
                        $('#rdCSDSFB').attr('disabled', 'disabled');
                        $('#txtCSCJDSFB').attr('disabled', 'disabled');

                        $('#txtJSXZ').attr('disabled', 'disabled');

                        $('.pop-button').html('');
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

                        // 设置时间验证，评分时间大于比赛结束时间，小于成绩发布时间
                        $('#txtPFStartTime').attr('onfocus', 'WdatePicker({ minDate: \'#F{$dp.$D(\\\'txtBSEndTime\\\')}\', dateFmt: "yyyy-MM-dd HH:mm" })');            // 评分时间大于比赛结束时间
                        $('#txtCSCJDSFB').attr('onfocus', 'WdatePicker({ minDate: \'#F{$dp.$D(\\\'txtPFEndTime\\\')}\' , dateFmt: "yyyy-MM-dd HH:mm"})');            // 成绩发布时间应晚于比赛评分时间
                        // 设置为必填
                        $('#txtPFStartTime').addClass('IsRequired');
                        $('#txtPFEndTime').addClass('IsRequired');

                        if (isRelease != 0) {
                            // 单项赛发布后，全部不可编辑
                            $('#txtBMStartTime').attr('disabled', 'disabled');
                            $('#txtBMEndTime').attr('disabled', 'disabled');

                            $('#txtBSStartTime').attr('disabled', 'disabled');
                            $('#txtBSEndTime').attr('disabled', 'disabled');

                            $('#txtCJFBStartTime').attr('disabled', 'disabled');
                            $('#txtCJFBEndTime').attr('disabled', 'disabled');

                            $('#txtPFStartTime').attr('disabled', 'disabled');
                            $('#txtPFEndTime').attr('disabled', 'disabled');

                            $('#rdCSSDFB').attr('disabled', 'disabled');
                            $('#rdCSDSFB').attr('disabled', 'disabled');
                            $('#txtCSCJDSFB').attr('disabled', 'disabled');

                            $('#txtJSXZ').attr('disabled', 'disabled');
                            $('#btnSelectJudge').remove();
                            $('.pop-button').html('');
                        }
                    }
                    else if (type == 3) {
                        // 复合赛显示全部表单，有报名时间、初赛时间、复赛时间、评分时间、初赛成绩发布、复赛成绩发布
                        $('#lblBSSJ').html('初赛时间');          // 比赛时间名称更改，可设置为初赛时间
                        // 设置时间验证，评分时间大于比赛结束时间，小于成绩发布时间
                        $('#txtFSStartTime').attr('onfocus', 'WdatePicker({ minDate: \'#F{$dp.$D(\\\'txtBSEndTime\\\')}\' , dateFmt: "yyyy-MM-dd HH:mm"})');            // 复赛开始时间大于初赛结束时间
                        $('#txtPFStartTime').attr('onfocus', 'WdatePicker({ maxDate: \'#F{$dp.$D(\\\'txtFSEndTime\\\')}\' , dateFmt: "yyyy-MM-dd HH:mm"})');            // 评分开始时间小于复赛结束时间
                        // 初赛成绩发布开始时间大于初赛时间，小于复赛开始时间
                        $('#txtCSCJDSFB').attr('onfocus', 'WdatePicker({ minDate: \'#F{$dp.$D(\\\'txtBSEndTime\\\')}\', dateFmt: "yyyy-MM-dd HH:mm",maxData:  \'#F{$dp.$D(\\\'txtFSStartTime\\\')}\'})');
                        $('#txtFSCJDSFB').attr('onfocus', 'WdatePicker({ maxDate: \'#F{$dp.$D(\\\'txtFSEndTime\\\')}\', dateFmt: "yyyy-MM-dd HH:mm" })');            // 复赛成绩发布小于复赛结束时间
                        // 设置为必填
                        $('#txtFSStartTime').addClass('IsRequired');
                        $('#txtPFStartTime').addClass('IsRequired');
                        //$('#txtCSCJDSFB').addClass('IsRequired');
                        $('#txtRWRS').addClass('IsRequired');
                        // 添加入围人数验证
                        $('#txtRWRS').addClass('IsMinNumber').attr('minnumber', '1');

                        if (isRelease != 0) {
                            $('#txtBMStartTime').attr('disabled', 'disabled');
                            $('#txtBMEndTime').attr('disabled', 'disabled');

                            $('#txtBSStartTime').attr('disabled', 'disabled');
                            $('#txtBSEndTime').attr('disabled', 'disabled');

                            $('#txtCJFBStartTime').attr('disabled', 'disabled');
                            $('#txtCJFBEndTime').attr('disabled', 'disabled');

                            $('#txtPFStartTime').attr('disabled', 'disabled');
                            $('#txtPFEndTime').attr('disabled', 'disabled');

                            $('#txtFSStartTime').attr('disabled', 'disabled');
                            $('#txtFSEndTime').attr('disabled', 'disabled');

                            //$('#rdCSSDFB').attr('disabled', 'disabled');
                            //$('#rdCSDSFB').attr('disabled', 'disabled');
                            //$('#txtCSCJDSFB').attr('disabled', 'disabled');

                            $('#txtJSXZ').attr('disabled', 'disabled');
                            $('#txtRWRS').attr('disabled', 'disabled');

                            // 根据比赛状态，禁用表单
                            var state = $('#hideState').val();
                            if (state > 302) {
                                // 待报名：可编辑初赛成绩发布时间、复赛成绩发布时间、竞赛评委。
                                // 报名中：可编辑初赛成绩发布时间、复赛成绩发布时间、竞赛评委。
                                // 待初赛：可编辑初赛成绩发布时间、复赛成绩发布时间、竞赛评委
                                // 其他状态均不可编辑
                                $('#rdCSSDFB').attr('disabled', 'disabled');
                                $('#rdCSDSFB').attr('disabled', 'disabled');
                                $('#txtCSCJDSFB').attr('disabled', 'disabled');

                                $('#rdFSSDFB').attr('disabled', 'disabled');
                                $('#rdFSDSFB').attr('disabled', 'disabled');
                                $('#txtFSCJDSFB').attr('disabled', 'disabled');

                                $('#btnSelectJudge').remove();
                                $('.pop-button').html('');
                            }
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
                    trhtml += '<td onclick="DeleteSelectJudeg({6})" style="cursor: pointer;">删除</td>';

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
                        GoBack();
                    }
                });
            }
        }
    })

    // 更改初赛成绩发布类型
    $('#rdCSSDFB').click(function () {
        // 手动发布，禁用定时发布时间文本框
        $('#txtCSCJDSFB').attr('disabled', 'disabled');
        $('#txtCSCJDSFB').removeClass('IsRequired').val('');
    })
    $('#rdCSDSFB').click(function () {
        // 自动发布，启用定时发布时间文本框
        $('#txtCSCJDSFB').removeAttr('disabled');
        $('#txtCSCJDSFB').addClass('IsRequired');
    })
    // 更改复赛成绩发布类型
    $('#rdFSSDFB').click(function () {
        // 手动发布，禁用定时发布时间文本框
        $('#txtFSCJDSFB').attr('disabled', 'disabled');
        $('#txtFSCJDSFB').removeClass('IsRequired').val('');
    })
    $('#rdFSDSFB').click(function () {
        // 自动发布，启用定时发布时间文本框
        $('#txtFSCJDSFB').removeAttr('disabled');
        $('#txtFSCJDSFB').addClass('IsRequired');
    })

})


// 提交编辑保存
function btnSaveClick() {
    // 输入验证
    if (!VerificationHelper.checkFrom("popMatchEdit")) {
        return;
    }

    // 判断是否输入了竞赛须知
    var jsxz = $('#txtJSXZ').val();
    if ($.trim(jsxz) == '') {
        dialogHelper.Error({
            content: '请输入竞赛须知！'
        });
        return;
    }

    // 
    var type = $('#hideType').val();                // 竞赛类型
    var isRelease = $('#hideRelease').val();      // 发布状态
    //if ((type == 1 && isRelease == 1) || (type == 2 && isRelease == 1))

    // 提交保存
    dialogHelper.Confirm({
        content: "确认要修改竞赛信息吗？",
        success: function () {
            // 实训或者复合赛需要添加评委
            if ($('#hideType').val() != 1) {
                // 判断是否选择了评委
                var length = $('#createMatchAddJudge tr').length;
                if (length > 0) {
                    var judgeIdArr = new Array();
                    $('#createMatchAddJudge tr').each(function (index) {
                        judgeIdArr[index] = $(this).attr('rel');
                    });
                } else {
                    dialogHelper.Error({
                        content: '请添加竞赛评委！'
                    });
                    return;
                }
            }

            // 提交保存
            $.ajax({
                url: '/CompetitionAdmin/Match/Update',
                data: {
                    Id: $('#hideMatchId').val(),                // 竞赛ID
                    RegistrationStartTime: $('#txtBMStartTime').val(),             // 报名开始时间
                    RegistrationEndTime: $('#txtBMEndTime').val(),             // 报名结束时间
                    RegistrationEndTime: $('#txtBMEndTime').val(),             // 报名结束时间
                    PreliminaryStartTime: $('#txtBSStartTime').val(),            // 初赛开始时间
                    PreliminaryEndTime: $('#txtBSEndTime').val(),            // 初赛结束时间
                    RematchStartTime: $('#txtFSStartTime').val(),            // 复赛结束时间
                    RematchEndTime: $('#txtFSEndTime').val(),            // 复赛结束时间
                    PreliminaryResultType: $('.d_bianji_tab input:radio[name="RadioGroup1"]:checked').val(),            // 初赛成绩发布类型
                    PreliminaryResultTime: $('#txtCSCJDSFB').val(),            // 初赛成绩发布时间
                    RematchResultType: $('.d_bianji_tab input:radio[name="RadioGroup2"]:checked').val(),            //复赛成绩发布类型
                    RematchResultTime: $('#txtFSCJDSFB').val(),            // 复赛成绩发布时间
                    ScoreStartTime: $('#txtPFStartTime').val(),             // 评分开始时间
                    ScoreEndTime: $('#txtPFEndTime').val(),             // 评分结束时间
                    Information: $('#txtJSXZ').val(),             // 竞赛须知
                    FinalistNumber: $('#txtRWRS').val(),             // 复赛入围人(组)数
                    Type: $('#hideType').val(),              // 竞赛类型
                    ListJudgeId: judgeIdArr              // 评委IDs
                },
                type: "POST",
                //async: false,
                dataType: "json",
                success: function (data) {
                    if (data.IsSuccess) {
                        dialogHelper.Success({
                            content: data.ErrorCode,
                            success: function () {
                                GoBack();             // 跳转到列表页面
                            }
                        });
                    }
                    else {
                        dialogHelper.Error({
                            content: data.ErrorCode
                        });
                    }
                }
            })
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