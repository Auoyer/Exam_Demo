// 判断是否从首页待审核用户点击进入
function LoadUrl() {
    var name, value;
    var str = location.href; //取得整个地址栏
    var num = str.indexOf("#")
    if (num != -1) {
        $('.d_yonghu1 a').eq(1).click();
    }
    else {
        //GetList();
        $('.d_yonghu1 a').eq(0).click();
    }
}

// 加载竞赛信息
function LoadInfo() {
    var matchId = $('#hideMatchId').val();             // 竞赛ID

    // 加载竞赛信息
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
                $('#lblRelease').html(isRelease == 0 ? "未发布" : isRelease == 1 ? "已发布" : "已结束");
                $('#lblMatchTopName').html(data.Data.Name);              // 竞赛名称
                $('#lblMatchName').html(data.Data.Name);
                $('#lblType').html(type == 1 ? "单项理论赛" : type == 2 ? "单项实训赛" : "复合赛");
                $('#hideType').val(type);
                $('#hideRelease').val(isRelease);

                // 未发布的大赛，隐藏选择卡，只显示参赛用户列表
                if (isRelease == 0) {
                    $('#divDisplay1').html('');
                    $('#divDisplay2').html('');
                }
                else {
                    //LoadUrl();
                }


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
    });
}

// 人员设置页面-加载分组用户列表
function GetList(isDel) {
    var matchId = $('#hideMatchId').val();             // 竞赛ID

    pageHelper.Init({
        url: "/CompetitionAdmin/Match/UserSetList",
        type: "POST",
        pageDiv: "#pages_UserList",
        data: {
            id: matchId,
            name: $("#searchName").val(),
            souce: $('#selGroupSouce').val(),                // 分组来源
            isAudit: 1,               // 审核状态
            random: Math.random()
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
                trHtml += "<td>{3}</td>";
                if (isDel != 1) {           // 判断是否显示操作按钮
                    trHtml += "<td class='temp'><a href='/CompetitionAdmin/Match/UserGroup?id={5}&groupId={4}'>成员管理</a>";
                    trHtml += "<a onclick='DeleteGroupUser({4})'>删除</a></td>";
                }
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
                    dom.UserIds             // 7 用户Ids
                );
            });
            $("#userList").html(html);

            $("#userList .userNames").each(function (index, item) {
                var arrName = $(this).html().split(' | ');
                var arrIds = $(this).attr('userIds').split(',');
                var str = '';
                for (var i = 0; i < arrName.length; i++) {
                    str += '<a onclick="OpenInfo(' + arrIds[i] + ')" title="' + arrName[i] + '">' + arrName[i].toString().ToLeft(8) + '</a>';
                }
                $(this).html(str);
            })
        }
    });
}


// 手动分组页面-加载用户列表
function GetUserList() {
    var matchId = $('#hideMatchId').val();             // 竞赛ID
    var groupId = $('#hideGroupId').val();             // 分组ID

    pageHelper.Init({
        url: "/CompetitionAdmin/Match/NotGroupUser",
        type: "POST",
        pageDiv: "#pages",
        async: false,
        data: {
            queryFile: $("#searchName").val(),
            id: matchId,
            groupId: groupId,
            isPage: 0
        },
        bind: function (data) {
            var html = "";
            $(data.Data).each(function (index, dom) {
                //每行html
                var trHtml = "";
                trHtml += "<tr rel='" + dom.Id + "' ondblclick='AddUser(" + dom.Id + ",this)'>";
                trHtml += "<td>{1}</td>";
                trHtml += "<td><div title=\"{2}\" class=\"ellipsis userName\">{2}</div></td>";
                trHtml += "<td>{3}</td>";
                trHtml += "<td><div title=\"{4}\" class=\"ellipsis\">{4}</div></td>";
                trHtml += "<td class=\"operate\">";
                trHtml += "<a class=\"d_chakan\" href=\"javascript:OpenInfo({5});\">查看</a>";
                trHtml += "</td>";
                trHtml += "</tr>";
                //拼接tbody
                html += StringHelper.FormatStr(trHtml,
                    ((data.PageIndex - 1) * data.PageSize + index + 1),     //0 序号
                    dom.AccountNo,                                       //1 账号
                    dom.UserName,                                           //2 姓名
                    dom.IDCard,                                          //3 身份证号码
                    dom.CollegeName,                                        //4 学院
                    dom.Id                                                  //5 Id
                );
            });
            $("#userGroupList").html(html);
        },
        completed: function () {
            // 分页后重新加载背景色
            IsSelectedLoadBackColor();
        }
    });
}


// 加载已分组信息
function LoadAlreadyGroup() {
    var matchId = $('#hideMatchId').val();             // 竞赛ID
    var groupId = $('#hideGroupId').val();             // 分组ID

    $.ajax({
        url: "/CompetitionAdmin/Match/UserGroupList",
        type: "POST",
        data: {
            id: matchId,
            groupId: groupId
        },
        success: function (data) {
            var html = '';
            $(data.Data).each(function (index, dom) {
                var groupId = dom.GroupId;

                //每行html
                html += "<dl groupId='" + groupId + "'>";
                html += "<dt onclick='ChangeGroup(this)'><span>第 " + (index + 1) + " 组</span><a onclick='DeleteGroup(this)'></a></dt>";
                $(dom.List).each(function (index1, dom1) {
                    html += "<dd rel='" + dom1.UserId + "' title='" + dom1.UserNames + "'><span>" + dom1.UserNames.ToLeft(6) + "</span><a onclick='DeleteSelectUser(this)'></a></dd>";
                })
                html += "</dl>";
            });

            $('#alreadyGroup').html(html);

            // 控制组的显示和隐藏
            //$(".d_ren3_1 dl dt").click(function () {

            //    $(".d_ren3_1 dl dt").removeClass('select');
            //    $(".d_ren3_1 dl dd").hide();
            //    $(this).parent("dl").children("dd").show();
            //    $(this).addClass('select');
            //});
            $(".d_ren3_1 dl:first dd").show();
            $(".d_ren3_1 dl:first dt").addClass('select');

            // 加载已添加项
            IsSelectedLoadBackColor();

            // 指定分组的时候，右侧不显示新建按钮
            if (groupId != 0) {
                $('#btnCreateGroup').hide();
                $('#alreadyGroup dt:first a').hide();
            }
        }
    });
}


// 双击选择用户，添加到右侧用户分组列表
function AddUser(userId, obj) {
    // 判断是否已选择了分组
    if ($('#alreadyGroup dl').length == 0) {
        dialogHelper.Error({
            content: '请先建小组！'
        });
        return;
    }

    // 判断是否已选择了分组
    if ($('#alreadyGroup dt.select').length == 0) {
        dialogHelper.Error({
            content: '请先选择小组！'
        });
        return;
    }

    // 判断该组用户是否已有3个
    if ($('#alreadyGroup dt.select').parent().find('dd').length == 3) {
        dialogHelper.Error({
            content: '该组人员已满！'
        });
        return;
    }

    // 判断该用户是否已添加到分组
    var flag = false;
    $('#alreadyGroup dd').each(function (index, dom) {
        // 右边用户Id
        var rightUserId = $(this).attr('rel');
        if (userId == rightUserId) {
            dialogHelper.Error({
                content: '该用户已添加到分组，无法操作！'
            });
            flag = true;
            return;
        }
    });

    if (flag)
        return;

    // 验证成功，添加到分组
    var userName = $(obj).find('td .userName').html();
    $('#alreadyGroup dt.select').parent().append('<dd rel="' + userId + '" title="' + userName + '"><span>' + userName.ToLeft(6) + '</span><a onclick="DeleteSelectUser(this)"></a></dd>');
    $('#alreadyGroup dt.select').parent().find('dd').show();
    // 添加背景颜色
    $(obj).find('td').css('background-color', '#f93');
}


// 删除已选择的用户
function DeleteSelectUser(obj) {
    $(obj).parent().remove();
    IsSelectedLoadBackColor();
}


// 删除分组
function DeleteGroup(obj) {
    $(obj).parent().parent().remove();
    IsSelectedLoadBackColor();
}


// 添加一个新组
function AddGroup() {
    // 取最后一个组号
    var lastGroup = 0;
    if ($('#alreadyGroup dl').length == 0)
        lastGroup = 0;
    else
        lastGroup = $('#alreadyGroup dl:last').attr('groupId');

    // 移除其他的选中样式，自己添加选中样式
    //$('#alreadyGroup dt').removeClass('select')
    $('#alreadyGroup').append('<dl groupId="' + (parseInt(lastGroup) + 1) + '"><dt onclick="ChangeGroup(this)"><span>第 ' + (parseInt(lastGroup) + 1) + ' 组</span><a onclick="DeleteGroup(this)"></a></dt></dl>');
    // 点击自己
    $('#alreadyGroup dt:last').click();
}


// 更改选中组样式
function ChangeGroup(obj) {
    $(".d_ren3_1 dl dt").removeClass('select');
    $(".d_ren3_1 dl dd").hide();
    $(obj).parent("dl").children("dd").show();
    $(obj).addClass('select');
}


// 判断左边用户列表是否有右边已添加分组的用户，如果有，添加背景颜色
function IsSelectedLoadBackColor() {

    // 先清空全部背景色
    $('#userGroupList tr td').css('background-color', '#fefefe');

    // 遍历比较
    $('#alreadyGroup dd').each(function (index, dom) {
        // 右边用户Id
        var rightUserId = $(this).attr('rel');
        // 循环左侧
        $('#userGroupList tr').each(function (index1, dom1) {
            var leftUserId = $(this).attr('rel');
            if (leftUserId == rightUserId) {
                $(this).find('td').css('background-color', '#f93');
            }
        })
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


// 查看用户信息
function OpenInfo(userId) {
    $.ajax({
        url: '/CompetitionAdmin/User/Detail',
        type: "GET",
        //async: false,
        data: { uid: userId },
        success: function (data) {
            $('#popInfo').html(data);
            dialogHelper.Show("popInfo", 650);
        }
    });
}


// 提交分组保存
function btnSumbitGroup() {
    var matchId = $('#hideMatchId').val();             // 竞赛ID
    var groupId = $('#hideGroupId').val();             // 分组ID

    var list = new Array();

    // 判断是否有分组
    if ($('#alreadyGroup dl').length == 0) {
        dialogHelper.Error({
            content: '请先建小组！'
        });
        return;
    }

    var flag = false;
    var modelList = new Array();

    var index = 0;
    $('#alreadyGroup dl').each(function () {
        var groupId = $(this).attr('groupid');          // 分组ID
        // 判断组员是否都是3人
        if ($(this).find('dd').length != 3) {
            dialogHelper.Error({
                content: '小组成员不足，请继续添加！'
            });
            flag = true;
            return;
        }

        // 查询分组用户Id 

        $(this).find('dd').each(function () {
            var model = new Object();
            model["GroupId"] = groupId;
            model["UserId"] = $(this).attr('rel');
            modelList[index] = model;
            index++;
        });
    })

    if (flag)
        return;

    dialogHelper.Confirm({
        content: "确认要提交分组吗？",
        success: function () {
            $.ajax({
                url: "/CompetitionAdmin/Match/SubmitGroup",
                type: "POST",
                dataType: "json",
                data: {
                    List: modelList,
                    groupId: groupId,
                    matchId: matchId
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        dialogHelper.Success({
                            content: '用户分组提交成功！',
                            success: function () {
                                window.location = '/CompetitionAdmin/Match/UserSet/' + matchId;
                            }
                        });
                    }
                    else {
                        dialogHelper.Error({
                            content: msgList[data.ErrorCode],
                        });
                    }
                }
            });
        }
    })
}

// 点击取消，跳转到人员设置页面
function btnGoToList() {
    dialogHelper.Confirm({
        content: '信息未保存，确认取消吗？',
        success: function () {
            var matchId = $('#hideMatchId').val();             // 竞赛ID
            window.location = '/CompetitionAdmin/Match/UserSet/' + matchId;
        }
    })

}

// 人员设置页面-列表点击“删除”按钮，提交删除分组
function DeleteGroupUser(groupId) {
    var matchId = $('#hideMatchId').val();             // 竞赛ID

    dialogHelper.Confirm({
        content: "确认要删除该分组用户吗？",
        success: function () {
            $.ajax({
                url: '/CompetitionAdmin/Match/DeleteGroupUser',
                type: "POST",
                data: {
                    matchId: matchId,
                    groupId: groupId
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        dialogHelper.Success({
                            content: '删除分组成功！',
                            success: function () {
                                location.href = location.href;
                            }
                        });
                    }
                    else {
                        dialogHelper.Error({
                            content: "删除失败，请联系系统管理员！"
                        });
                    }
                }
            })
        }
    })
}


//4.批量上传
function btnUpLoadClick() {
    if ($.trim($('#textfield').val()) == '') {
        dialogHelper.Error({ content: "请选择要上传的文件！" });
        return;
    }

    //验证上传文件类型
    var filePath = $("#fileField").val();
    if (filePath.length > 0) {
        var fileType = filePath.substring(filePath.lastIndexOf("."))
        if (fileType == ".xls" || fileType == ".xlsx") {

        } else {
            dialogHelper.Error({ content: "只能选择xls | xlsx文件！" });
            return;
        }
    }

    $('#hideFilePath').val('');          // 保存文件路径
    $(".background,.progressBar").show();
    $.ajaxFileUpload({
        url: "/CompetitionAdmin/Match/ImportUser",
        secureuri: false,
        fileElementId: 'fileField',
        dataType: 'json',
        success: function (data, status) {
            // 清空文件路径
            $('#textfield').val('');
            if (data.result) {
                var patch = data.ErrorCode.replace(/%5C/g, "\\");           // 文件路径反编译，保存的文件路径
                $('#hideFilePath').val(patch);          // 文件路径
                pageHelper.Init({
                    url: "/CompetitionAdmin/Match/ImportPaged",
                    type: "POST",
                    pageDiv: "#pages2",
                    ContentType: "text/html; charset=utf-8",
                    data: {
                        //userName: $("#searchName").val()
                        filePath: patch
                    },
                    bind: function (data2) {
                        var html = "";
                        $("#ImportUser").html(html);
                        $(data2.Data).each(function (index, dom) {
                            //每行html
                            var trHtml = "";
                            trHtml += "<tr>";
                            trHtml += "<td>{0}</td>";
                            trHtml += "<td>{1}</td>";
                            trHtml += "<td><div class=\"ellipsis userNames\">{2}</div></td>";
                            trHtml += "<td><div title='" + dom.CollegeName + "' class=\"ellipsis\">{3}</div></td>";
                            trHtml += "</tr>";
                            //拼接tbody
                            html += StringHelper.FormatStr(trHtml,
                                ((data2.PageIndex - 1) * data2.PageSize + index + 1),     //0 序号
                                  dom.TeamNumber,                                       //1 组队人数
                                dom.UserNames,                                       //2 姓名
                                dom.CollegeName.ToLeft(16)                                        // 3 学院
                            );
                        });
                        $("#ImportUser").html(html);

                        $("#ImportUser .userNames").each(function (index, item) {
                            var arrName = $(this).html().split(' | ');
                            var str = '';
                            for (var i = 0; i < arrName.length; i++) {
                                str += '<a title="' + arrName[i] + '">' + arrName[i].toString().ToLeft(8) + '</a>';
                            }
                            $(this).html(str);
                        })
                    }
                });
                return;
            }
        },
        error: function (data, status, e)//服务器响应失败处理函数
        {
            $(".background,.progressBar").hide();
            dialogHelper.Error({ content: msgList["21003"] });//"21003": "网站出现异常，请联系管理员!",
        },
    });
}

// 批量导入分组用户-上传按钮
function AddUpLoadUser() {
    var filePath = $('#hideFilePath').val();          // 文件路径
    if ($.trim(filePath) == '') {
        dialogHelper.Error({
            content: '请先上传Excel模板文件！'
        });
        return;
    }

    // 判断文件是否有数据
    if ($('#ImportUser tr').length == 0) {
        dialogHelper.Error({
            content: '该Excel文件没有数据！'
        });
        return;
    }

    var matchId = $('#hideMatchId').val();             // 竞赛ID

    dialogHelper.Confirm({
        content: '确定批量上传分组吗？',
        success: function () {
            $.ajax({
                url: "/CompetitionAdmin/Match/AddImportUser",
                type: "POST",
                async: false,
                dataType: "json",
                data: {
                    filePath: filePath,
                    matchId: matchId
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        dialogHelper.Success({
                            content: "批量上传成功！",
                            success: function () {
                                window.location = '/CompetitionAdmin/Match/UserSet/' + matchId;
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



// 人员设置页面-加载报名审核用户列表
function GetAuditList() {
    var matchId = $('#hideMatchId').val();             // 竞赛ID

    // 判断是否隐藏操作按钮
    var state = $('#selGroupAudit').val();
    if (state == 0) {
        $('#divAudit').show();
    } else {
        $('#divAudit').hide();
    }

    pageHelper.Init({
        url: "/CompetitionAdmin/Match/UserSetList",
        type: "POST",
        pageDiv: "#pages2",
        data: {
            id: matchId,
            name: $("#searchAuditName").val(),
            souce: 3,              // 分组来源，只查询官网报名的
            isAudit: $('#selGroupAudit').val()            // 审核状态
            //pageIndex: 1,
            //pageSize: 10
        },
        bind: function (data) {
            var html = "";
            $(data.Data).each(function (index, dom) {
                //每行html
                var trHtml = "";
                trHtml += "<tr >";
                trHtml += "<td><input type='checkbox'  rel='{3}'/></td>";
                trHtml += "<td>{0}</td>";
                trHtml += "<td><div class=\"ellipsis userNames\" userIds=\"{6}\">{1}</div></td>";
                trHtml += "<td><div class=\"ellipsis\">{2}</div></td>";
                trHtml += "<td>{5}</td>";
                if (state == 0) {
                    trHtml += "<td><a onclick='Audit({4},{3},1)'>通过</a>";
                    trHtml += "<a onclick='Audit({4},{3},2)'>不通过</a></td>";
                }
                else {
                    trHtml += "<td>-</td>";
                }
                trHtml += "</tr>";
                //拼接tbody
                html += StringHelper.FormatStr(trHtml,
                    ((data.PageIndex - 1) * data.PageSize + index + 1),     //0 序号
                    dom.UserNames,                                           //1 姓名
                    dom.CollegeName,      //2 学校
                    dom.GroupId,                                       //3 分组Id
                    matchId,             // 4 竞赛ID
                    dom.IsAudit == 0 ? "待审核" : dom.IsAudit == 1 ? "已审核" : "已拒绝",      // 5 审核状态
                    dom.UserIds                 // 6 用户Ids

                );
            });
            $("#auditList").html(html);

            $("#auditList .userNames").each(function (index, item) {
                var arrName = $(this).html().split(' | ');
                var arrIds = $(this).attr('userIds').split(',');
                var str = '';
                for (var i = 0; i < arrName.length; i++) {
                    str += '<a onclick="OpenInfo(' + arrIds[i] + ')" title="' + arrName[i] + '">' + arrName[i].ToLeft(6) + '</a>';
                }
                $(this).html(str);
            })
        }
    });
}



// 参赛用户组审核
function Audit(matchId, groupId, isAudit) {
    dialogHelper.Confirm({
        content: "确认要提交审核吗？",
        success: function () {
            $.ajax({
                url: "/CompetitionAdmin/Match/UserAudit",
                type: "POST",
                dataType: "json",
                data: {
                    matchId: matchId,
                    groupId: groupId,
                    isAudit: isAudit
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        dialogHelper.Success({
                            content: '审核成功！',
                            success: function () {
                                // 审核列表刷新
                                GetAuditList();
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
    })
}


// 批量审核
function BatchAudit(isAudit) {
    // 判断是否选择了用户
    var listGroupId = new Array();
    var index = 0;

    $("#auditList input[type='checkbox']").each(function () {
        if ($(this).attr("checked")) {
            listGroupId[index] = $(this).attr("rel");
            index++;
        }
    });

    if (listGroupId.length <= 0) {
        dialogHelper.Error({
            content: "未选中任何用户！",
        });
        return;
    }

    var matchId = $('#hideMatchId').val();
    dialogHelper.Confirm({
        content: "确认要提交审核吗？",
        success: function () {
            $.ajax({
                url: "/CompetitionAdmin/Match/BatchUserAudit",
                type: "POST",
                dataType: "json",
                data: {
                    matchId: matchId,
                    groupId: listGroupId,
                    isAudit: isAudit
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        dialogHelper.Success({
                            content: '审核成功！',
                            success: function () {
                                // 审核列表刷新
                                GetAuditList();
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
    })
}


// 批量上传取消，跳转到列表页面
function BatchCacelClick() {
    dialogHelper.Confirm({
        content: "确认要取消导入吗？",
        success: function () {
            window.location = '/CompetitionAdmin/Match/UserSet/' + $('#hideMatchId').val();
        }
    })
}