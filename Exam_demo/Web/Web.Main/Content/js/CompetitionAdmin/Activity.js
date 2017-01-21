$(function () {
    //Upload();
    SaveFile();
    GetActivities();
});

//图片上传
function Upload() {
    $("#btnUpLoad").click(function () {
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
            url: "/CompetitionAdmin/Activity/Import",
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
    });
}

//保存文件
function SaveFile() {
    $("#btnSaveFile").click(function () {;
        $.ajax({
            url: "/CompetitionAdmin/Activity/SaveFile",
            type: "POST",          
            dataType: "json",
            contentType: "application/x-www-form-urlencoded",
            data: {
                //BackImagePath: $('#hideFilePath').val(),
                CompetitionPurpose: $.trim($("#tComPurpose").val()),
                CompetitionTime:  $.trim($("#tComTime").val()),
                Organization: $.trim($("#tOrganization").val()),
                Content: $.trim($("#tContent").val()),
                Id: $("#hidActivityId").val()
            },
            success: function (data) {
                if (data.IsSuccess) {
                    dialogHelper.Success({
                        content: msgList["20010"],
                        success: function () {
                            location.href = location.href;
                            GetActivities();
                        }
                    });
                }
                else {
                    dialogHelper.Error({
                        content: data.ErrorCode.indexOf('2') != -1 ? msgList[data.ErrorCode] : data.ErrorCode
                        //content: data.ErrorCode
                    });
                }
            }
        });

    })
}

function GetActivities() {
    $.ajax({
        url: "/CompetitionAdmin/Activity/GetActivitiesModel",
        type: "POST",
        async: false,
        success: function (data) {
            if (data.Data != null) {
                //$("#textfield").val(data.Data.BackImagePath != null ? data.Data.BackImagePath : "");
                $("#tComPurpose").html(data.Data.CompetitionPurpose != null ? data.Data.CompetitionPurpose : "");
                $("#tComTime").text(data.Data.CompetitionTime != null ? data.Data.CompetitionTime : "");
                $("#tOrganization").text(data.Data.Organization != null ? data.Data.Organization : "");
                $("#tContent").text(data.Data.Content != null ? data.Data.Content : "");
                $("#hidActivityId").val(data.Data.Id);
                //$("#hideFilePath").val(data.Data.BackImagePath);
            }
        }
    });
}

