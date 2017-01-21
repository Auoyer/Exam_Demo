$(document).ready(function () {
    LoadUrl();


    // 2. 新增或修改保存
    $("#btnSave").click(function () {
        if (!VerificationHelper.checkFrom("popAddInfo")) {
            return;
        }

        // 判断是否选择了省市区
        var ProvinceCode = $("#province").find("option:selected").attr("postcode");
        var CityCode = $("#city").find("option:selected").attr("postcode");

        if ($("#province").val() == '--请选择省--' || $.trim($("#city").val()) == '') {
            dialogHelper.Error({
                content: msgList["20055"]
            });
            return;
        }

        $.ajax({
            url: "/CompetitionAdmin/User/AddOrUpdate",
            type: "POST",
            async: false,
            dataType: "json",
            data: {
                Id: $("#uid").val(),
                AccountNo: $.trim($("#uCode").val()),
                Email: $.trim($("#uEmail").val()),
                UserName: $.trim($("#uName").val()),
                Sex: $("#cmbSex").val(),
                CollegeName: $.trim($("#uCollege").val()),
                ProvinceCode: $("#province").find("option:selected").attr("postcode"),
                CityCode: $("#city").find("option:selected").attr("postcode"),
                DepartmentName: $.trim($("#uClass").val()),
                Phone: $.trim($("#uPhone").val()),
                IDCard: $.trim($("#uIDCard").val()),
            },
            success: function (data) {
                if (data.IsSuccess) {
                    dialogHelper.Success({
                        content: msgList["20010"],
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
    });
    // 密码重置
    $("#btnCancel").click(function () {
        dialogHelper.Confirm({
            content: "用户密码重置为“888888”？",
            success: function () {
                $.ajax({
                    url: "/CompetitionAdmin/Judge/PwdRest",
                    type: "POST",
                    async: false,
                    dataType: "json",
                    data: {
                        userId: $('#uid').val()
                    },
                    success: function (data) {
                        if (data.IsSuccess) {
                            dialogHelper.Success({
                                content: "密码重置成功！",
                                success: function () {
                                    dialogHelper.Close("popAddInfo");
                                    // location.href = location.href;
                                },
                            });
                        }
                        else {
                            dialogHelper.Error({
                                content: data.ErrorCode
                            });
                        }
                    }
                });
            },
        });
    });


    //4.批量上传
    $("#btnUpLoad").click(function () {

        if (!VerificationHelper.checkFrom("popRegistrations")) {
            return;
        }
        //验证上传文件类型
        var filePath = $("#fileField").val();
        if (filePath.length > 0) {
            var fileType = filePath.substring(filePath.lastIndexOf("."))
            if (fileType == ".xls" || fileType == ".xlsx") {

            } else {
                dialogHelper.Error({ content: "只能选择xls、xlsx文件!" });
                return;
            }
        }

        $('#hideFilePath').val('');          // 保存文件路径
        var classId = $("#cmbClass_BatchDialog").val();

        $(".background,.progressBar").show();
        $.ajaxFileUpload({
            url: "/CompetitionAdmin/User/Import",
            secureuri: false,
            fileElementId: 'fileField',
            dataType: 'json',
            success: function (data, status) {
                $('#hideFilePath').val(data.Data);          // 保存文件路径

                pageHelper.Init({
                    url: "/CompetitionAdmin/User/ImportPaged",
                    type: "POST",
                    pageDiv: "#pages2",
                    data: {
                        //userName: $("#searchName").val()
                        filePath: data.Data
                    },
                    bind: function (data2) {
                        var html = "";
                        $("#ImportUser").html(html);
                        $(data2.Data).each(function (index, dom) {
                            //每行html
                            var trHtml = "";
                            trHtml += "<tr>";
                            trHtml += "<td>{0}</td>";
                            trHtml += "<td><div title=\"{1}\" class=\"ellipsis\">{1}</div></td>";
                            trHtml += "<td><div title=\"{2}\" class=\"ellipsis\">{2}</div></td>";
                            trHtml += "<td>{3}</td>";
                            trHtml += "<td><div title=\"{4}\" class=\"ellipsis\">{4}</div></td>";
                            trHtml += "<td><div title=\"{5}\" class=\"ellipsis\">{5}</div></td>";
                            trHtml += "<td><div title=\"{6}\" class=\"ellipsis\">{6}</div></td>";
                            //trHtml += "<td class=\"operate\">";
                            //trHtml += "<a class=\"spr spr-edit\" title=\"编辑\" href=\"javascript:Edit({6});\"></a>";
                            //trHtml += "<a class=\"spr spr-del\" \" title=\"删除\" href=\"javascript:Del({6});\"></a>";
                            //trHtml += "</td>";
                            trHtml += "</tr>";
                            //拼接tbody
                            html += StringHelper.FormatStr(trHtml,
                                ((data2.PageIndex - 1) * data2.PageSize + index + 1),     //0 序号
                                dom.AccountNo,                                       //1 账号
                                dom.UserName,                                           //2 姓名
                                dom.IDCard,                                            //3 身份证号码
                                dom.Phone,                                            //4 联系方式
                                dom.CollegeName,                                        //5 学院
                                dom.Email,                                          //6 邮箱
                                dom.Id                                                  //7 Id
                            );
                        });
                        $("#ImportUser").html(html);
                    }
                });
                return;
            },
            error: function (data, status, e)//服务器响应失败处理函数
            {
                $(".background,.progressBar").hide();
                dialogHelper.Error({ content: msgList["21003"] });//"21003": "网站出现异常，请联系管理员!",
            },
        });
    });
    $("#btnBatchCancel").click(function () {
        dialogHelper.Confirm({
            content: "确认要取消上传的用户吗？",
            success: function () {
                $('#ImportUser').html('');
            }
        })
    });
});

// 判断是否从首页待审核用户点击进入
function LoadUrl() {
    var name, value;
    var str = location.href; //取得整个地址栏
    var num = str.indexOf("#")
    if (num != -1)
        $('.d_yonghu1 a').eq(1).click();
    else
        $('.d_yonghu1 a').eq(0).click();
}

// 用户列表
function GetList() {
    pageHelper.Init({
        url: "/CompetitionAdmin/User/GetUserList",
        type: "POST",
        pageDiv: "#pages",
        data: {
            userName: $("#searchName").val(),
            isPage: 0
        },
        bind: function (data) {
            var html = "";
            $(data.Data).each(function (index, dom) {
                //每行html
                var trHtml = "";
                trHtml += "<tr>";
                trHtml += "<td><input class=\"icheck\" type=\"checkbox\" id=\"chkUser_{6}\"></td>";
                trHtml += "<td>{0}</td>";
                trHtml += "<td><div title=\"{1}\" class=\"ellipsis\">{1}</div></td>";
                trHtml += "<td><div title=\"{2}\" class=\"ellipsis\">{2}</div></td>";
                trHtml += "<td>{3}</td>";
                trHtml += "<td><div title=\"{4}\" class=\"ellipsis\">{4}</div></td>";
                trHtml += "<td><div title=\"{5}\" class=\"ellipsis\">{5}</div></td>";
                trHtml += "<td class=\"operate\">";
                trHtml += "<a class=\"d_chakan\" href=\"javascript:Details({6},true);\">查看</a>";
                trHtml += "<a class=\"d_shanchu\" \" href=\"javascript:Del({6});\">删除</a>";
                trHtml += "</td>";
                trHtml += "</tr>";
                //拼接tbody
                html += StringHelper.FormatStr(trHtml,
                    ((data.PageIndex - 1) * data.PageSize + index + 1),     //0 序号
                    dom.AccountNo,                                       //1 账号
                    dom.UserName,                                           //2 姓名
                    dom.IDCard,                                          //3 身份证号码
                    dom.Phone,                                            //4 联系方式
                    dom.CollegeName,                                        //5 学院
                    dom.Id                                                  //6 Id
                );
            });
            $("#studentList").html(html);
        }
    });
}

//新增操作
function Add() {
    $("#dialogType").val(0);            // 操作类型，0=新增，1=修改

    $("#popAddInfo h3").html("新增竞赛用户");
    $("#uid").val(0);
    $("#uCode").val("").removeAttr('disabled', 'disabled');
    $("#uEmail").val("");
    $("#uName").val("").removeAttr('disabled', 'disabled');
    $("#uCollege").val("");
    $("#uClass").val("");
    $("#uPhone").val("");
    $("#uIDCard").val("").removeAttr('disabled', 'disabled');

    // $("#province").val("--请选择省--");
    // $("#city").val("--请选择--");
    addressInit(0, 0, 0);
    dialogHelper.Show("popAddInfo", 600);

}

//批量新增
function AddBatch() {
    $("#textfield").val("");
    $('#hideFilePath').val('');          // 保存文件路径
    $("#ImportUser").html('');
    $("#pages2").html('');
    //$("#fileDiv").html("");
    //$("#fileDiv").html("<input class=\"file-field\" type=\"file\" name=\"fileField\" id=\"fileField\" onchange=\"$('#textfield').val($(this).val())\"><input class=\"btn btn-small btn-blue\" type=\"button\" value=\"浏览\">");

    $("#cmbCollege_BatchDialog").val(0).trigger("change");
    $("#cmbClass_BatchDialog").val(0).trigger("change");

    dialogHelper.Show("popRegistrations", 756);
}

// 批量新增用户提交
function AddUpLoadUser() {
    if (!VerificationHelper.checkFrom("popRegistrations")) {
        return;
    }


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

    dialogHelper.Confirm({
        content: '确定批量上传用户吗？',
        success: function () {
            $.ajax({
                url: "/CompetitionAdmin/User/AddImportUser",
                type: "POST",
                async: false,
                dataType: "json",
                data: {
                    filePath: filePath
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        dialogHelper.Success({
                            content: msgList["20010"],
                            success: function () {
                                history.go(-1);
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

//修改操作
function Edit(id) {
    $.ajax({
        url: "/CompetitionAdmin/User/Details",
        type: "POST",
        async: false,
        data: { uid: id },
        success: function (data) {
            if (data.IsSuccess) {
                $("#dialogType").val(1);
                $("#popAddInfo h3").html("编辑用户信息");
                $("#uid").val(data.Data.Id);
                $("#uName").val(data.Data.UserName);
                $("#cmbSex").val(data.Data.Sex).trigger("change");
                $("#uCode").val(data.Data.AccountNo).attr('disabled', 'disabled');           // 禁止修改账号
                $("#uEmail").val(data.Data.Email);
                $("#uCollege").val(data.Data.CollegeName);
                $("#uClass").val(data.Data.DepartmentName);
                $("#uPhone").val(data.Data.Phone);
                $("#uIDCard").val(data.Data.IDCard).attr('disabled', 'disabled');

                // 省市加载
                addressInit(data.Data.ProvinceCode, data.Data.CityCode, 0);
                dialogHelper.Show("popAddInfo", 600);
            }
            else {
                dialogHelper.Error({
                    content: msgList["20007"]
                });
            }
        }
    });
}

// 删除
function Del(id) {
    var listId = new Array();
    listId[0] = id;

    dialogHelper.Confirm({
        content: "确认删除该用户吗？",
        success: function () {
            $.ajax({
                url: "/CompetitionAdmin/User/Delete",
                type: "POST",
                async: false,
                dataType: "json",
                data: {
                    listJudgeId: listId
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        dialogHelper.Success({
                            content: "删除成功！",
                            success: function () {
                                location.href = '/CompetitionAdmin/User/Index';
                            },
                        });
                    }
                    else {
                        dialogHelper.Error({
                            content: data.ErrorCode

                        });
                    }
                }
            });
        },
    });
}

// 批量删除
function DelBatch() {

    var listId = new Array();
    var index = 0;

    $("input[type='checkbox']").each(function () {
        if ($(this).attr("id") == "chkAll") {
            return;
        }

        if ($(this).attr("checked")) {
            listId[index] = $(this).attr("id").split("_")[1];
            index++;
        }
    });

    if (listId.length <= 0) {
        dialogHelper.Error({
            content: "未选中任何用户!",
        });
        return;
    }

    dialogHelper.Confirm({
        content: "确认删除选择的用户吗？",
        success: function () {
            $.ajax({
                url: "/CompetitionAdmin/User/Delete",
                type: "POST",
                async: false,
                dataType: "json",
                data: {
                    listJudgeId: listId
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        dialogHelper.Success({
                            content: "删除成功！",
                            success: function () {
                                location.href = '/CompetitionAdmin/User/Index';
                            },
                        });
                    }
                    else {
                        dialogHelper.Error({
                            content: data.ErrorCode
                        });
                    }
                }
            });
        },
    });
}


// 查看
function Details(id, isEdit) {
    $.ajax({
        url: "/CompetitionAdmin/User/Details",
        type: "POST",
        async: false,
        data: { uid: id },
        success: function (data) {
            if (data.IsSuccess) {
                $('#uid').val(id);              // 用户ID
                //   $('#dialogType').val();             // 用户类型
                $("#popInfo h3").html("查看用户信息");
                $("#lblid").html(data.Data.Id);
                $("#lblName").html(data.Data.UserName);
                $("#lblSex").html(data.Data.Sex == 1 ? "男" : "女");
                $("#lblCode").html(data.Data.AccountNo).attr('disabled', 'disabled');           // 禁止修改账号
                $("#lblEmail").html(data.Data.Email);
                $("#lblCollege").html(data.Data.CollegeName);
                $("#lblClass").html(data.Data.DepartmentName);
                $("#lblPhone").html(data.Data.Phone);
                $("#lblIDCard").html(data.Data.IDCard);
                $("#lblArea").html(getCodeName(data.Data.ProvinceCode, data.Data.CityCode));                 // 省市名称加载

                // 从用户列表进入查看页面，显示编辑按钮，从注册用户列表进入，不显示编辑按钮
                if (isEdit)
                    $('#btnEdit').show();
                else
                    $('#btnEdit').hide();

                dialogHelper.Show("popInfo", 600);
            }
            else {
                dialogHelper.Error({
                    content: msgList["20007"]
                });
            }
        }
    });
}


// 点击“编辑”，关闭“查看弹框”，显示“编辑弹框”
function btnEditClick() {
    dialogHelper.Close('popInfo');
    Edit($('#uid').val());
}


// 注册用户列表
function GetRegList() {
    pageHelper.Init({
        url: "/CompetitionAdmin/User/GetUserList",
        type: "POST",
        pageDiv: "#pagesReg",
        data: {
            userName: $("#searchRegName").val(),
            isAudit: $('#selAudit').val(),              // 查询用户的审核状态
            isPage: 1               // 标示为官网注册用户
        },
        bind: function (data) {
            var html = "";
            $("#userRegList").html('');
            $(data.Data).each(function (index, dom) {
                //每行html
                var trHtml = "";
                trHtml += "<tr>";
                trHtml += "<td><input class=\"icheck\" type=\"checkbox\" id=\"chkAuditUser_{6}\"></td>";
                trHtml += "<td>{0}</td>";
                trHtml += "<td><div title=\"{1}\" class=\"ellipsis\">{1}</div></td>";
                trHtml += "<td><div title=\"{2}\" class=\"ellipsis\">{2}</div></td>";
                trHtml += "<td>{3}</td>";
                trHtml += "<td><div title=\"{4}\" class=\"ellipsis\">{4}</div></td>";
                trHtml += "<td><div title=\"{5}\" class=\"ellipsis\">{5}</div></td>";
                trHtml += "<td class=\"operate\">";
                trHtml += "<a class=\"d_chakan\" href=\"javascript:Details({6},false);\">查看</a>";
                trHtml += "<a class=\"d_shanchu\" href=\"javascript:SetAudit([{6}],1);\">通过</a>";
                trHtml += "<a class=\"d_shanchu\" href=\"javascript:SetAudit([{6}],2);\">拒绝</a>";
                trHtml += "</td>";
                trHtml += "</tr>";
                //拼接tbody
                html += StringHelper.FormatStr(trHtml,
                    ((data.PageIndex - 1) * data.PageSize + index + 1),     //0 序号
                    dom.AccountNo,                                       //1 账号
                    dom.UserName,                                           //2 姓名
                    dom.IDCard,                                          //3 身份证号码
                    dom.Phone,                                            //4 联系方式
                    dom.CollegeName,                                        //5 学院
                    dom.Id                                                  //6 Id
                );
            });
            $("#userRegList").html(html);
        }
    });
}


// 修改注册用户的审核状态
// ids:要修改的用户ids数组
// audit:修改为的审核状态，1=通过，2=拒绝
function SetAudit(ids, audit) {
    // 判断是否传入了参数
    if (ids.length > 0) {
        dialogHelper.Confirm({
            content: "确认审核选择的用户吗？",
            success: function () {
                $.ajax({
                    url: '/CompetitionAdmin/User/SetAudit',
                    type: "POST",
                    async: false,
                    dataType: "json",
                    data: {
                        listUserId: ids,
                        audit: audit
                    },
                    success: function (data) {
                        dialogHelper.Success({
                            content: "审核成功！",
                            success: function () {
                                // 重新加载列表
                                GetRegList();
                                // location.href = location.href;
                            }
                        });
                    }
                });
            }
        });
    }
    else {
        dialogHelper.Error({
            content: "未选中任何用户！",
        });
    }
}


// 批量审核
function BatchSetAudit() {
    var listId = new Array();
    var index = 0;

    $("input[type='checkbox']").each(function () {
        if ($(this).attr("id") == "chkAll" || $(this).attr('id') == 'chkRegAll') {
            return;
        }

        if ($(this).attr("checked")) {
            listId[index] = $(this).attr("id").split("_")[1];
            index++;
        }
    });

    if (listId.length <= 0) {
        dialogHelper.Error({
            content: "未选中任何用户!",
        });
        return;
    }
    SetAudit(listId, 1);
}