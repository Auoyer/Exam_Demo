$(function () {
    GetNewsList();
    //Cancel();
    //EditNews();
})

//1、获取新闻列表
function GetNewsList() {
    //获取新闻列表  
    pageHelper.Init({
        url: "/CompetitionAdmin/News/GetNewsList",
        type: "POST",
        bind: function (data) {
            var html = "";
            var hideText = "隐藏";
            $(data).each(function (index, dom) {
                //每行html
                var trHtml = "";
                if (dom.IsHidden == false) {
                    hideText = "隐藏";
                }
                else if (dom.IsHidden == true) {
                    hideText = "发布";
                }
                trHtml += "<tr>";
                trHtml += "<td title=\"{0}\" class=\"ellipsis\">{0}</td>";
                trHtml += "<td class=\"ellipsis\">{1}</td>";
                trHtml += "<td class=\"ellipsis\">{2}</td>";
                trHtml += "<td >{3}</td>";
                trHtml += "<td>";
                trHtml += "<a href=\"javascript:View({4});\">查看</a> <a href=\"javascript:Edit({4});\">编辑</a> <a href=\"javascript:Del({4});\">删除</a> <a href=\"javascript:PlaceTop({4});\">置顶</a><a href=\"javascript:void(0);\" onclick=\"HideNews({4},this);\">" + hideText + "</a>";
                trHtml += "</td>";
                trHtml += "</tr>";

                //拼接tbody
                html += StringHelper.FormatStr(trHtml,
                    //index + 1,                                        //0 序号
                    dom.Title,                                       //1 标题
                    dom.UserName,                                //2 发布人
                    dom.ReleaseTime,                       //3 发布时间    
                    dom.IsHidden == false ? "否" : "是",
                    dom.Id                                          //4 Id
                );

            });
            $("#NewsList").html(html);
        }
    });
}

//2、保存新闻信息
function SaveNews() {
    var title = $.trim($("#txtTitle").val());
    if (title == "") {
        dialogHelper.Error({ content: "标题不能为空！" });
        return;
    }
    //if (title.length > 40) {
    //    dialogHelper.Error({ content: "标题长度超过限制！" });
    //    return;
    //}

    //var titleImagePath = $('#filePath').val();
    var titleImagePath = $('#hideFilePath').val();
    //if (titleImagePath == "")
    //{
    //    dialogHelper.Error({ content: "请上传图片！" });
    //    return;
    //}

    var content = CKEDITOR.instances.ttContent.getData();
    if (content == "") {
        dialogHelper.Error({ content: "新闻内容不能为空！" });
        return;
    }

    if (content.length > 2000) {
        dialogHelper.Error({ content: "新闻内容不能超过2000字！" });
        return;
    }
    $.ajax({
        url: "/CompetitionAdmin/News/Add",
        type: "POST",
        async: false,
        dataType: "json",
        data: {
            Title: title,
            Content: content,
            Image: titleImagePath
        },
        success: function (data) {
            if (data.IsSuccess) {
                dialogHelper.Success({
                    content: msgList["20010"],
                    success: function () {
                        GetNewsList();
                        $("#txtTitle").val("");
                        $("#filePath").val("");
                        $('#hideFilePath').val("");
                        CKEDITOR.instances.ttContent.setData("");
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

//3、删除新闻
function Del(id) {
    dialogHelper.Confirm({
        content: "确定删除该新闻？",
        success: function () {
            $.ajax({
                url: "/CompetitionAdmin/News/Delete",
                type: "POST",
                async: true,
                data: {
                    id: id
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        dialogHelper.Success({
                            content: "删除成功！",
                            success: function () {
                                GetNewsList();
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
    })
}

//4、根据id获取新闻实体对象
function Details(id) {
    $.ajax({
        url: "/CompetitionAdmin/News/Detail",
        type: "POST",
        async: false,
        dataType: "json",
        data: {
            id: id
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
}

//5、跳转至新闻查看页面弹出框
function View(id) {
    $.ajax({
        url: "/CompetitionAdmin/News/Detail",
        type: "POST",
        async: false,
        data: { id: id },
        success: function (data) {
            if (data.IsSuccess) {

                $("#lTitle").html(data.Data.Title);
                $("#lReleaseTime").html(data.Data.ReleaseTime);
                $("#lContent").val(data.Data.Content.replace(/<[^>]+>/g, ""));//去除html标签

                dialogHelper.Show("popNews", 600);
            }
            else {
                dialogHelper.Error({
                    content: msgList["20007"]
                });
            }
        }
    });
}

//6、置顶新闻信息
function PlaceTop(id) {
    $.ajax({
        url: "/CompetitionAdmin/News/PlaceTop",
        type: "POST",
        async: false,
        data: { id: id },
        success: function (data) {
            if (data.IsSuccess) {
                GetNewsList();
            }
            else {
                dialogHelper.Error({
                    content: msgList["20007"]
                });
            }
        }
    });
}

//7、跳转至新闻编辑页面
function Edit(id) {
    $.ajax({
        url: "/CompetitionAdmin/News/Detail",
        type: "POST",
        async: false,
        data: { id: id },
        success: function (data) {
            if (data.IsSuccess) {
                $("#uTitle").val(data.Data.Title);
                CKEDITOR.instances.uContent.setData(data.Data.Content);
                $("#newsId").val(id);
                $("#hdNum").val(data.Data.Num);
                $("#ufilePath").val(data.Data.Image);
                $("#uhideFilePath").val(data.Data.Image);
                dialogHelper.Show("popEditNews", 600);
            }
            else {
                dialogHelper.Error({
                    content: msgList["20007"]
                });
            }
        }
    });
}

//8、关闭新闻信息编辑页面弹窗
function Cancel() {
    //$("#btnCancel").click(function () {
    dialogHelper.Close("popEditNews");
    //});
}

//9、保存编辑的内容
function EditNews() {
    var title = $.trim($("#uTitle").val());
    if (title == "") {
        dialogHelper.Error({ content: "标题不能为空！" });
        return;
    }
    if (title.length > 40) {
        dialogHelper.Error({ content: "标题长度超过限制！" });
        return;
    }

    var titleImagePath = $('#ufilePath').val();
    //if (titleImagePath == "") {
    //    dialogHelper.Error({ content: "请上传图片！" });
    //    return;
    //}

    var content = CKEDITOR.instances.uContent.getData();
    if (content == "") {
        dialogHelper.Error({ content: "新闻内容不能为空！" });
        return;
    }
    if (content.length > 2000) {
        dialogHelper.Error({ content: "新闻内容不能超过2000字！" });
        return;
    }

    $.ajax({
        url: "/CompetitionAdmin/News/Update",
        type: "POST",
        data: {
            id: $("#newsId").val(),
            Title: $("#uTitle").val(),
            Num: $("#hdNum").val(),
            Image: $("#uhideFilePath").val(),
            Content: content
        },
        success: function (data) {
            if (data.IsSuccess) {
                dialogHelper.Success({
                    content: "修改成功！",
                    success: function () {
                        dialogHelper.Close("popEditNews");
                        GetNewsList();
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

//10、隐藏新闻
function HideNews(id, obj) {

    var hideText = $(obj).text();
    var IsHidden = false;
    if (hideText == "发布") {
        IsHidden = false;
    }
    else if (hideText == "隐藏") {
        IsHidden = true;
    }
    $.ajax({
        url: "/CompetitionAdmin/News/HideNews",
        type: "POST",
        data: {
            id: id,
            IsHidden: IsHidden
        },
        success: function (data) {
            if (data.IsSuccess) {
                dialogHelper.Success({
                    content: "设置成功！",
                    success: function () {
                        GetNewsList();
                        return false;
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

//11、新增时图片上传
function Upload() {
    //验证上传文件类型
    var filePath = $("#fileField").val();
    if (filePath == "") {
        dialogHelper.Error({ content: "图片路径不能为空！" });
        return false;
    }
    if (filePath.length > 0) {
        var fileType = filePath.substring(filePath.lastIndexOf(".")).toLowerCase();
        if (fileType == ".jpg" || fileType == ".gif" || fileType == ".png") {

        } else {
            dialogHelper.Error({ content: "只能选择jpg|gif|png文件!" });
            return false;
        }
    }

    $('#hideFilePath').val('');          // 保存文件路径       

    $(".background,.progressBar").show();
    $.ajaxFileUpload({
        url: "/CompetitionAdmin/News/Import",
        secureuri: false,
        fileElementId: 'fileField',
        dataType: 'json',
        success: function (data, status) {
            $('#hideFilePath').val(data.Data);          // 保存文件路径            
            dialogHelper.Success({ content: "图片上传成功！" });
            $(".background,.progressBar").hide();
        },
        error: function (data, status, e)//服务器响应失败处理函数
        {
            $(".background,.progressBar").hide();
            dialogHelper.Error({ content: msgList["21003"] });//"21003": "网站出现异常，请联系管理员!",
        },
    });
}

//12、编辑时图片上传
function uUpload() {
    //验证上传文件类型
    var filePath = $("#ufileField").val();
    if (filePath == "") {
        dialogHelper.Error({ content: "请选择新的图片上传！" });
        return false;
    }
    if (filePath.length > 0) {
        var fileType = filePath.substring(filePath.lastIndexOf(".")).toLowerCase();
        if (fileType == ".jpg" || fileType == ".gif" || fileType == ".png") {

        } else {
            dialogHelper.Error({ content: "只能选择jpg|gif|png文件!" });
            return false;
        }
    }

    $('#uhideFilePath').val('');          // 保存文件路径       

    $(".background,.progressBar").show();
    $.ajaxFileUpload({
        url: "/CompetitionAdmin/News/Import",
        secureuri: false,
        fileElementId: 'ufileField',
        dataType: 'json',
        success: function (data, status) {
            $('#uhideFilePath').val(data.Data);          // 保存文件路径            
            dialogHelper.Success({ content: "图片上传成功！" });
            $(".background,.progressBar").hide();
        },
        error: function (data, status, e)//服务器响应失败处理函数
        {
            $(".background,.progressBar").hide();
            dialogHelper.Error({ content: msgList["21003"] });//"21003": "网站出现异常，请联系管理员!",
        },
    });
}


