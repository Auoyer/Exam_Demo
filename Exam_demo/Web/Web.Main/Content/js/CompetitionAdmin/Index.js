//跳转至个人信息编辑页面
function EditPage() {
    dialogHelper.Close('popPersonalCenter');

    $("#AccountNo").val($.trim($("#hdAccount").val())).attr('disabled', 'disabled');
    $("#UserName").val($("#hdUserName").val());
    $("#cmbSex").val($("#hdSexName").val())

    addressInit($("#hdProvince").val(), $("#hdCity").val(), 0);
    $("#CollegeName").val($("#hdCollegeName").val());
    $("#DepartmentName").val($("#hdDepartmentName").val());
    $("#Phone").val($("#hdPhone").val());
    $("#Email").val($("#hdEmail").val());

    dialogHelper.Show('popIndexUserInfo', 400);
}
function EditPage2() {
    dialogHelper.Close('popPersonalCenter');

    $("#AccountNo").val($.trim($("#hdAccount").val())).attr('disabled', 'disabled');
    $("#UserName").val($("#hdUserName").val());
    $("#cmbSex").val($("#hdSexName").val())

    addressInit2($("#hdProvince").val(), $("#hdCity").val(), 0);
    $("#CollegeName").val($("#hdCollegeName").val());
    $("#DepartmentName").val($("#hdDepartmentName").val());
    $("#Phone").val($("#hdPhone").val());
    $("#Email").val($("#hdEmail").val());

    dialogHelper.Show('popIndexUserInfo', 400);
}


//超管端-个人信息查看
function PersonCenter() {
    _ajax_backup({
        url: "/CompetitionAdmin/Home/GetModel",
        type: "POST",
        async: false,
        dataType: "json",
        data: {
            Id: $("#hdUserId").val()
        },
        success: function (data) {
            if (data.IsSuccess) {
                var sex = data.Data.Sex;
                $("#txtAccount").html(data.Data.AccountNo);
                $("#txtUserName").html(data.Data.UserName);
                $("#txtArea").html(getCodeName(data.Data.ProvinceCode, data.Data.CityCode));

                $("#txtCollegeName").html(data.Data.CollegeName);
                $("#txtDepartmentName").html(data.Data.DepartmentName);
                $("#txtPhone").html(data.Data.Phone);
                $("#txtEmail").html(data.Data.Email);
                var sexName = "男";
                if (sex == 2) {
                    sexName = "女";
                }
                //var role = $.trim($("#hdRoleId").val());
                //if (role == 3) {
                //    $("#labnum").text("工号：");
                //}
                sexName != null ? $("#txtsex").html(sexName) : $("#txtsex").text("");
                dialogHelper.Show("popPersonalCenter");
            }
            else {
                dialogHelper.Error({
                    content: data.ErrorCode
                });
            }
        }
    });
}

//个人信息编辑保存
function TopEdit() {
    if (!VerificationHelper.checkFrom("popIndexUserInfo")) {
        return;
    }
    // 判断是否选择了省市区
    var ProvinceCode = $(".province").find("option:selected").attr("postcode");
    var CityCode = $(".city").find("option:selected").attr("postcode");

    //if ($(".province").val() == '--请选择省--' || $.trim($(".city").val()) == '') {
    //    dialogHelper.Error({
    //        content: msgList["20055"]
    //    });
    //    return;
    //}

    _ajax_backup({
        url: "/CompetitionAdmin/Home/Update",
        type: "POST",
        async: false,
        dataType: "json",
        data: {
            Id: $("#hdUserId").val(),
            AccountNo: $('#hdAccount').val(),
            IDCard: $('#hdIDCard').val(),
            Email: $.trim($("#Email").val()),
            UserName: $.trim($("#UserName").val()),
            Sex: $("#cmbSex").val(),
            CollegeName: $.trim($("#CollegeName").val()),
            ProvinceCode: $(".province").find("option:selected").attr("postcode"),
            CityCode: $(".city").find("option:selected").attr("postcode"),
            DepartmentName: $.trim($("#DepartmentName").val()),
            Phone: $.trim($("#Phone").val())
        },
        success: function (data) {
            if (data.IsSuccess) {
                dialogHelper.Success({
                    content: "个人信息修改成功！",
                    success: function () {
                        location.href = location.href;
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

//关闭个人信息编辑页面弹窗
function TopCancel() {
    dialogHelper.Close("popIndexUserInfo");
}

//获取历史大赛数量
function GetHistoryCompetitionNum() {
    $.ajax({
        url: "/CompetitionAdmin/Home/GetHistoryCompetitionNum",
        type: "POST",
        async: false,
        success: function (data) {
            var s = JSON.stringify(data);
            var a = FormatNum(data.Data, 6);
            for (var i = 0; i < a.length; i++) {
                var cur = a[i];
                $('.d_wrap span').eq(i).animate({ top: -24 * cur }, 1000);
            }
        }
    });
}

//获取总人数
function GetTotalUserNum() {
    $.ajax({
        url: "/CompetitionAdmin/Home/GetTotalUserNum",
        type: "POST",
        async: false,
        success: function (data) {
            var s = JSON.stringify(data.Data);
            var a = FormatNum(s, 6);
            for (var i = 0; i < a.length; i++) {
                var cur = a[i];
                $('.d_wrap1 span').eq(i).animate({ top: -24 * cur }, 1000);
            }
        }
    });
}

//获取注册待审核用户数量
function GetRegisterNotAduitNum() {
    $.ajax({
        url: "/CompetitionAdmin/Home/GetRegisterNotAduitNum",
        type: "POST",
        success: function (data) {
            $('.d_home_2_1 span').html(data.Data);
            $('.d_home_2_1').attr('href', '/CompetitionAdmin/User/Index#index');
        }
    });
    // 判断是否有创建权限
    //var isCreate = parseInt($('#hdCreate').val());
    //if (isCreate == 1) {
    //    $.ajax({
    //        url: "/CompetitionAdmin/Home/GetRegisterNotAduitNum",
    //        type: "POST",
    //        success: function (data) {
    //            $('.d_home_2_1 span').html(data.Data);
    //            $('.d_home_2_1').attr('href', '/CompetitionAdmin/User/Index#index');

    //        }
    //    });
    //} else {
    //    dialogHelper.Error({
    //        content: '对不起，您没有权限操作！'
    //    })
    //}
}

// 点击跳转
function GetRegisterNotAduitNum2() {
    // 判断是否有创建权限
    //var isCreate = parseInt($('#hdCreate').val());
    //if (isCreate == 1) {
    //    //if ($('.d_home_2_1 span').html() == 0) {
    //    //    $('.d_home_2_1').removeAttr('href');
    //    //    dialogHelper.Error({
    //    //        content: '没有注册待审核的用户！'
    //    //    })
    //    //    return false;
    //    //}
    //} else {
    //    dialogHelper.Error({
    //        content: '对不起，您没有权限操作！',
    //        success: function () {
    //            return false;
    //        }
    //    })
    //}
}

//获取报名待审核用户数量
function GetSiginupNotAduitNum() {
    $.ajax({
        url: "/CompetitionAdmin/Home/GetSiginupNotAduitNum",
        type: "POST",
        async: false,
        success: function (data) {
            $('.d_home_2_1s span').html(data.Data);
            // 查询要跳转的竞赛ID
            if (data.Data != 0) {
                $.ajax({
                    url: "/CompetitionAdmin/Home/GetSiginupNotAduitMatchId",
                    type: "POST",
                    success: function (data) {
                        $('.d_home_2_1s').attr('href', '/CompetitionAdmin/Match/UserSet/' + data.Data + '#index');
                        // 查询第一个需要
                    }
                });
            }
        }
    });
}

//获取报名待审核用户数量
function GetSiginupNotAduitNum2() {
    // 判断是否有创建权限
    //var isCreate = parseInt($('#hdCreate').val());
    //if (isCreate == 1) {
    //    // 判断是否有用户
    //    if ($('.d_home_2_1s span').html() == 0) {
    //        dialogHelper.Error({
    //            content: '没有报名待审核的用户！'
    //        })
    //    }

    //    return true;
    //} else {
    //    dialogHelper.Error({
    //        content: '对不起，您没有权限操作！',
    //        success: function () {
    //            return false;
    //        }
    //    })
    //}
}

//获取大赛列表
function GetIndexMatchList() {
    pageHelper.Init({
        url: "/CompetitionAdmin/Home/GetLatestCompetitionList",
        type: "POST",
        pageDiv: "#pages",
        async: false,
        //data: {
        //    userName: $("#searchName").val(),
        //    isPage: 0
        //},
        bind: function (data) {
            var html = "";
            $(data).each(function (index, dom) {
                //每行html
                var trHtml = "";
                trHtml += "<tr>";
                trHtml += "<td>{0}</td>";
                trHtml += "<td>{1}</td>";
                trHtml += "<td>{2}</td>";
                trHtml += "<td>{3}</td>";
                trHtml += "<td>{4}</td>";
                trHtml += "<td>";
                trHtml += "<a href=\"/CompetitionAdmin/Match/Detail/{5}\">进入大赛</a>";
                trHtml += "</td>";
                trHtml += "</tr>";

                if (dom.Type != 3) {

                    //拼接tbody
                    html += StringHelper.FormatStr(trHtml,
                        index + 1,                                        //0 序号
                        dom.Name,                                       //1 大赛名称
                        dom.AddUserName,                                //2 创建人
                        dom._PreliminaryStartTime,                       //3 开始时间
                        dom._PreliminaryEndTime,                         //4 结束时间
                        dom.Id                                          //5 Id
                    );
                }
                else {
                    //拼接tbody
                    html += StringHelper.FormatStr(trHtml,
                        index + 1,                                        //0 序号
                        dom.Name,                                       //1 大赛名称
                        dom.AddUserName,                                //2 创建人
                        dom._PreliminaryStartTime,                       //3 开始时间
                        dom._RematchEndTime,                         //4 结束时间
                        dom.Id                                          //5 Id
                    );
                }
            });
            $("#CompetitionList").html(html);
        }
    });
}

//公告弹窗
function NoticePage() {
    $("#addNotice h3").text("发布公告");
    $("#cmbNotice").attr("disabled", false);
    $("#addNotice .pop-button").show();
    $("#txtNoticeContext").val("");
    $("#txtNoticeContext").attr("readonly", false)
    $('#cmbNotice').removeAttr('disabled');

    dialogHelper.Show("addNotice", 500);
}

//获取公告信息
function GetNotice() {
    pageHelper.Init2({
        url: "/CompetitionAdmin/Home/GetNoticeList",
        type: "POST",
        pageDiv: "#NoticePages",

        data:
        {
            PageSize: 8
        },
        bind: function (data) {
            var html = "";

            if (data.Data.length > 0) {
                $(data.Data).each(function (index, dom) {
                    //每行html
                    var trHtml = "";
                    trHtml += "<tr>";
                    trHtml += "<td name=\"dataNo\">{0}</td>";
                    trHtml += "<td class='left'><div  title=\"{1}\">【{3}】{2}</div></td>";
                    trHtml += "<td><a  class=\"chakan\" onclick=\"DetailsNotice('" + dom.Id + "')\">查看</a>&nbsp; ";
                    if ($('#hdCreate').val() == 1) {
                        trHtml += "<a class=\"shanchu\" onclick=\"DeleteNotice('" + dom.Id + "')\">删除</a>";
                    }
                    trHtml += "</td></tr>";
                    html += StringHelper.FormatStr(trHtml, (
                        (data.PageIndex - 1) * data.PageSize + index + 1),
                        dom.Content,
                        dom.Content.toString().ToLeft(9),
                        GetNoticeTypeName(dom.NoticeType)
                   )
                });
                $("#trNotice").html(html);
                $("#NoticePages").show();
            }
            else {
                $("#trNotice").html("<td colspan='3'><h3 style='text-align: center;padding:30px'>暂无相关信息！</h3></td>");
                $("#NoticePages").hide();
                //  $("#trNotice").html("<h5>暂无相关信息！</h5>");
            }
        }
    });
}


// 返回公告类型名称
function GetNoticeTypeName(type) {
    if (type == 1)
        return "系统公告";
    else if (type == 2)
        return "大赛公告";
    else if (type == 3)
        return "温馨提示";
    else if (type == 4)
        return "资讯快报";
}

//保存发布公告
function SaveNotice() {
    if (!VerificationHelper.checkFrom("addNotice"))
        return;

    $.ajax({
        url: "/CompetitionAdmin/Home/AddNotice",
        type: "POST",
        data: {
            NoticeType: $("#cmbNotice option:selected").val(),
            Content: $.trim($("#txtNoticeContext").val())
        },
        success: function (data) {
            dialogHelper.Success({
                content: "发布公告成功！"
            });

            dialogHelper.Close("addNotice");
            GetNotice();
        }
    })
}

//公告详情
function DetailsNotice(id) {
    $("#addNotice .pop-button").hide();
    $("#addNotice h3").text("查看公告");
    $.ajax({
        url: "/CompetitionAdmin/Home/GetNoticeModel",
        type: "POST",
        async: false,
        data: {
            id: id
        },
        success: function (data) {
            $("#cmbNotice").val(data.Data.NoticeType).attr("disabled", true);
            $("#txtNoticeContext").val(data.Data.Content).attr("readonly", true);
            dialogHelper.Show("addNotice", 500);


        }
    });
}

//删除公告
function DeleteNotice(id) {
    dialogHelper.Confirm({
        content: "确定删除该公告？",
        success: function () {
            $.ajax({
                url: "/CompetitionAdmin/Home/DeleteNotice",
                type: "POST",
                async: true,
                data: {
                    id: id
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        dialogHelper.Success({
                            content: "删除成功！"

                        });
                        //弹出成功提示
                        //code ...

                        //刷新当前页
                        GetNotice();
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