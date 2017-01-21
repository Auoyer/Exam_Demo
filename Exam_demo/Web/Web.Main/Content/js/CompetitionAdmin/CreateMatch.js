$(function () {

    // 创建大赛保存，选择竞赛类型
    $('#btnSave').click(function () {

        // 表单输入验证
        if (!VerificationHelper.checkFrom("popAddMatch")) {
            return;
        }


        var type = $('#selType').val();

        if (type == 0) {
            // 选择竞赛类型
            dialogHelper.Error({
                content: '请先选择竞赛类型！'
            });
            return;
        }


        // 判断竞赛名称是否重复
        var flag = false;
        $.ajax({
            url: '/CompetitionAdmin/Match/IsMatchNameRepeat',
            data: { name: $('#uName').val(), rad: Math.random() },
            async: false,
            type: "POST",
            success: function (data) {
                if (!data.IsSuccess) {
                    flag = true;
                    return;
                }
            }
        });

        if (flag) {
            showValidateMsg("uName", "竞赛名称已存在");
            return;
        }

        // 竞赛类型=1，直接创建大赛，其他需添加先评委
        if (type == 1) {
            dialogHelper.Confirm({
                content: "确定创建大赛吗？",
                success: function () {
                    $.ajax({
                        url: "/CompetitionAdmin/Match/Create",
                        type: "POST",
                        async: false,
                        dataType: "json",
                        data: {
                            Id: 0,
                            Name: $.trim($("#uName").val()),
                            Type: $("#selType").val()
                        },
                        success: function (data) {
                            if (data.IsSuccess) {
                                dialogHelper.Success({
                                    content: '大赛创建成功',
                                    success: function () {
                                        // 跳转到未发布大赛页面
                                        location.href = '/CompetitionAdmin/Match/NotStart';
                                    }
                                });
                            }
                            else {
                                dialogHelper.Error({
                                    content: data.ErrorCode
                                });
                            }
                        }
                    });
                }
            });
        }
        else {
            // 显示评委列表
            $('#popAddJudge').show();
            $('#popAddJudge2').show();
        }

    });


    // 非单项理论赛，提交创建大赛，添加大赛和评委
    $('#btnMatchSave').click(function () {
        // 判断是否选择了评委
        var length = $('#createMatchAddJudge tr').length;
        if (length > 0) {
            var judgeIdArr = new Array();
            $('#createMatchAddJudge tr').each(function (index) {
                judgeIdArr[index] = $(this).attr('rel');
            });

            // 提交保存
            dialogHelper.Confirm({
                content: "确定创建大赛吗？",
                success: function () {
                    $.ajax({
                        url: "/CompetitionAdmin/Match/Create",
                        type: "POST",
                        async: false,
                        dataType: "json",
                        data: {
                            Id: 0,
                            Name: $.trim($("#uName").val()),
                            Type: $("#selType").val(),
                            ListJudgeId: judgeIdArr
                        },
                        success: function (data) {
                            if (data.IsSuccess) {
                                dialogHelper.Success({
                                    content: '大赛创建成功',
                                    success: function () {
                                        // 跳转到未发布大赛页面
                                        location.href = '/CompetitionAdmin/Match/NotStart';
                                    }
                                });
                            }
                            else {
                                dialogHelper.Error({
                                    content: data.ErrorCode
                                });
                            }
                        }
                    });
                }
            });
        }
        else {
            dialogHelper.Error({
                content: '请添加竞赛评委！'
            });
        }
    });


    $('#btnCancel').click(function () {
        dialogHelper.Confirm({
            content: '确认取消创建大赛吗？',
            success: function () {
                window.location = window.location;
            }
        })
    })
});


// 判断账号是否存在，账号不重复返回true
