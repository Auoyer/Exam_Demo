var fileUploadAddress;
$(function () {
    fileUploadAddress = $("#hdFileUploadAddress").val();
    SaveFile1();
    GetHomePageModel();
    extensionIE();
});

//IE浏览器下，JS没有indexOf方法，补上
function extensionIE()
{
    if (!Array.prototype.indexOf) {
        Array.prototype.indexOf = function (elt /*, from*/) {
            var len = this.length >>> 0;
            var from = Number(arguments[1]) || 0;
            from = (from < 0)
                 ? Math.ceil(from)
                 : Math.floor(from);
            if (from < 0)
                from += len;
            for (; from < len; from++) {
                if (from in this &&
                    this[from] === elt)
                    return from;
            }
            return -1;
        };
    }
}

//1、获取首页数据
function GetHomePageModel() {
    $.ajax({
        url: "/CompetitionAdmin/OfficialWebsite/GetHomePageModel",
        type: "POST",
        async: false,
        success: function (data) {
            if (data.Data != null) {
                $("#hidHomePageId").val(data.Data.Id);
                $("#tComIntroduction").val(data.Data.CompetitionIntroduction != null ? data.Data.CompetitionIntroduction : "").attr("disabled", "disabled");
                $("#txtTile1").val(data.Data.Title1).attr("disabled", "disabled");
                $("#txtTile2").val(data.Data.Title2).attr("disabled", "disabled");
                $("#txtTile3").val(data.Data.Title3).attr("disabled", "disabled");

                ///加载活动列表
                var data2 = data.Data.ActivityImageList;
                if (data2 != null && data2.length > 0) {
                    $("#tbActiveIamge").find("tr:eq(0)").remove();
                    $(data2).each(function (index, dom) {
                        LoadActiveImageLabelRow(dom);
                    })
                }
                else {
                    //AddActiveImageRow();
                    //$("#tbActiveIamge").find("tr").find("td:eq(1)").find("input").attr("disabled", "disabled");
                    //$("#tbActiveIamge").find("tr").find("td:eq(3)").find("input:eq(0)").attr("disabled", "disabled");
                    //$("#tbActiveIamge").find("tr").find("td:eq(3)").find("input:eq(3)").attr("disabled", "disabled");
                    //$("#tbActiveIamge").find("tr").find("td:eq(3)").find("input:eq(4)").attr("style", "display:none");
                }
                $("#txtQRCode").val(data.Data.QRCodeIntroduction != null ? data.Data.QRCodeIntroduction : "").attr("disabled", "disabled");
                $("#QRCodefilePath").val(data.Data.QRCodeImgPath != null ? data.Data.QRCodeImgPath : "");
                //$("#QRCodetextfield").attr("disabled", "disabled");
                $("#QRCodehideFilePath").val(data.Data.QRCodeImgPath);
                $("#dtianjia2").attr("style", "display:none");

                $("#txtComTelConsultation").val(data.Data.ComTelConsultation != null ? data.Data.ComTelConsultation : "");//大赛咨询电话
                $("#txtComPhone").val(data.Data.ComPhone != null ? data.Data.ComPhone : "");//系统问题反馈电话
                $("#txtComQQ").val(data.Data.ComQQ != null ? data.Data.ComQQ : "");//大赛QQ群

                $("#tStep1").val(data.Data.Step1Description != null ? data.Data.Step1Description : "");//STEP1报名
                $("#tStep2").val(data.Data.Step2Description != null ? data.Data.Step2Description : "");//STEP2比赛中
                $("#tStep3").val(data.Data.Step3Description != null ? data.Data.Step3Description : "");//STEP3评审
                $("#tStep4").val(data.Data.Step4Description != null ? data.Data.Step4Description : "");//STEP4颁奖

                //加载友情链接
                var data3 = data.Data.FriendLinkList;
                if (data3 != null && data3.length > 0) {
                    $(data3).each(function (index, dom) {
                        LoadFriendLinkRow(dom);
                    })
                }

                //加载专家风采
                var data4 = data.Data.ExpertsList;
                if (data4 != null && data4.length > 0) {
                    $(data4).each(function (index, dom) {
                        LoadExpertsRow(dom);
                    })
                }

                //加载荣誉榜
                var data5 = data.Data.HonorRollList;
                if (data5 != null && data5.length > 0) {
                    $(data5).each(function (index, dom) {
                        LoadHonorRollRow(dom);
                    })
                }
            }
        }
    });
}

//2、保存大赛介绍
function SaveFile1() {
    $("#btnSave1").click(function () {
        $.ajax({
            url: "/CompetitionAdmin/OfficialWebsite/SaveFile1",
            type: "POST",
            async: false,
            data: {
                CompetitionIntroduction: $.trim($("#tComIntroduction").val()),
                Id: $("#hidHomePageId").val()
            },
            success: function (data) {
                if (data.IsSuccess) {
                    dialogHelper.Success({
                        content: msgList["20010"],
                        success: function () {
                            $("#tComIntroduction").attr("disabled","disabled");
                        }
                    });
                    //dialogHelper.Success({
                    //    content: msgList["20010"]
                    //    //success: function () {
                    //    //    location.href = location.href;
                    //    //}
                    //});
                    //location.href = "/CompetitionAdmin/OfficialWebsite/Index";
                }
                else {
                    dialogHelper.Error({
                        content: data.ErrorCode
                    });
                }
            }
        });

    })
}

//3、动态添加活动图片上传文本框
function AddActiveImageRow() {
    var targetTable = $("#tbActiveIamge");
    var index = targetTable.find("tr").length;
    if (index > 4) {
        dialogHelper.Error({ content: "活动图片超过上限！" });
        return;
    }

    var trHtml = "";

    trHtml += "<tr data-rowId=" + (parseInt(index) + 1) + ">";
    trHtml += "<td>图片说明：</td>";
    trHtml += "<td><input onkeypress=\"specialTextValidate()\" maxlength=\"25\" type='text' name=\"txtfield" + (parseInt(index) + 1) + "\" id=\"txtfield" + (parseInt(index) + 1) + "\"></td>";
    trHtml += "<td>图片：</td>";
   
    //trHtml += "<td><input type='file' onchange=\"$('#textfield" + (parseInt(index) + 1) + "').val($(this).val()) \" style=\"display:none;\" name=\"fileField" + (parseInt(index) + 1) + "\" id=\"fileField" + (parseInt(index) + 1) + "\" > ";
    //trHtml += " <input class=\"disabled\"  type=\"text\" id=\"textfield" + (parseInt(index) + 1) + "\" readonly=\"readonly\" onclick=\"$('#fileField" + (parseInt(index) + 1) + "').trigger('click')\">";
    //trHtml += " <input type=\"image\" id=\"liulan\" onclick=\"$('#fileField" + (parseInt(index) + 1) + "').trigger('click')\" src=\"/Content/images/d_xin/liulan.png\">";
    //trHtml += " <input type=\"image\" src=\"/Content/images/d_xin/shangchuang.png\" onclick=\"Upload(this)\" id=\"btnUpLoad" + (parseInt(index) + 1) + "\">";
    //trHtml += " <input name=\"input\" type=\"image\" onclick=\"delRow(this)\" src=\"/Content/images/d_xin/d_shanchu.png\">";

    trHtml += "<td>";

    trHtml += "<div id=\"fileDiv\" class=\"upload-field fr\">";
    trHtml += " <input class=\"file-field IEUploadButton\"  style=\"width:60px;\" type=\"file\" id=\"fileField" + (parseInt(index) + 1) + "\" name=\"fileField\" onchange=\"$(this).parent().parent().find('input:eq(4)').val($(this).val())\" >";
    trHtml += " <input type=\"image\"  style=\"margin-left:5px;\" id=\"liulan\" src=\"/Content/images/d_xin/liulan.png\"> ";
    trHtml += " <input type=\"image\" src=\"/Content/images/d_xin/shangchuang.png\" onclick=\"Upload(this)\" id=\"btnUpLoad" + (parseInt(index) + 1) + "\">";
    trHtml += " <input name=\"input\"   type=\"image\" onclick=\"delRow(this)\" src=\"/Content/images/d_xin/d_shanchu.png\">";
    trHtml += "</div>";

    trHtml += " <input style=\"margin-top:3px;\"  disabled type=\"text\" id=\"filePath\" msgname=\"文件路径\"> ";

    trHtml += " <input type=\"hidden\" id=\"hideFilePath" + (parseInt(index) + 1) + "\" /></td></tr>";

    //newRow.innerHTML = trHtml;
    $("#tbActiveIamge").append(trHtml);
}

//4、动态添加活动图片上传标签框
function LoadActiveImageLabelRow(dom) {
    var targetTable = $("#tbActiveIamge");
    var index = targetTable.find("tr").length;
    var trHtml = "";

    trHtml += "<tr data-rowId=" + (parseInt(index) + 1) + ">";
    trHtml += "<td>图片说明：</td>";
    trHtml += "<td><input maxlength=\"25\" disabled=\"disabled\"  type=\"text\" value=" + dom.ImageDescription + " name=\"txtfield" + (parseInt(index) + 1) + "\" id=\"txtfield" + (parseInt(index) + 1) + "\"></td>";
    trHtml += "<td>图片：</td>";
    trHtml += "<td>";

    trHtml += "<div id=\"fileDiv" + (parseInt(index) + 1) + "\" class=\"upload-field fr\">";
    trHtml += " <input class=\"file-field IEUploadButton\" disabled=\"disabled\" style=\"width:60px;\" type=\"file\" id=\"fileField" + (parseInt(index) + 1) + "\" name=\"fileField\" onchange=\"$('#filePath" + (parseInt(index) + 1) + "').val($(this).val())\" >";
    trHtml += " <input type=\"image\" disabled=\"disabled\" style=\"margin-left:5px;\" id=\"liulan\" src=\"/Content/images/d_xin/liulan.png\"> ";
    trHtml += " <input type=\"image\" disabled=\"disabled\" src=\"/Content/images/d_xin/shangchuang.png\" onclick=\"Upload(this)\" id=\"btnUpLoad" + (parseInt(index) + 1) + "\">";
    trHtml += " <input name=\"input\" style=\"display:none\"  type=\"image\" onclick=\"delRow(this)\" src=\"/Content/images/d_xin/d_shanchu.png\">";
    trHtml += "</div>";

    trHtml += " <input style=\"margin-top:3px;\" value=" + dom.ActivityImagePath + " disabled type=\"text\" id=\"filePath" + (parseInt(index) + 1) + "\" msgname=\"文件路径\"> ";
  
    //trHtml += "<input type=\"file\"  onchange=\"$('#textfield" + (parseInt(index) + 1) + "').val($(this).val()) \" style=\"display:none;\" name=\"fileField" + (parseInt(index) + 1) + "\" id=\"fileField" + (parseInt(index) + 1) + "\" >";
    //trHtml += "<input class=\"disabled\" title=" + dom.ActivityImagePath + "  value=" + dom.ActivityImagePath + "  type=\"text\" id=\"textfield" + (parseInt(index) + 1) + "\" readonly=\"readonly\" onclick=\" $('#fileField" + (parseInt(index) + 1) + "')\">";
    //trHtml += " <input type=\"image\" id=\"liulan\" onclick=\"$('#fileField" + (parseInt(index) + 1) + "')\" src=\"/Content/images/d_xin/liulan.png\">";
   
   


    trHtml += " <input type=\"hidden\" id=\"hideFilePath" + (parseInt(index) + 1) + "\" value=" + dom.ActivityImagePath + ">";
    trHtml += " <input type=\"hidden\" id=\"hideId" + (parseInt(index) + 1) + "\" value=" + dom.ActivityImageId + ">";
    trHtml += "</td>";
    trHtml += "</tr>";

    $("#tbActiveIamge").append(trHtml);
}

//5、删除行
function delRow(obj) {
    var $tr = $(obj);
    var id = $tr.parent().parent().find("input:eq(6)").val();
    if (!id) {
        var trLen = $("#tbActiveIamge").find("tr").length;
        $("#tbActiveIamge").find("tr:eq(" + (trLen - 1) + ")").remove();
    }
    else {
        dialogHelper.Confirm({
            content: "您确定要删除该条记录吗？",
            success: function () {
                $tr.parent().parent().parent().remove();
                Del(id);
            }
        })
    }
}

//6、使图片标题之标题文字，活动图片管理，二维码图片可编辑
function Edit2() {
    $("#txtTile1").removeAttr("disabled");
    $("#txtTile2").removeAttr("disabled");
    $("#txtTile3").removeAttr("disabled");
    $("#txtQRCode").removeAttr("disabled");
    $("#QRCodetextfield").removeAttr("disabled");
    $("#dtianjia2").attr("style", "display:inline");
    $("#QRCodetextfield,#QRCodefileField,#QRCodebtnUpLoad").removeAttr("disabled");

    var targetTable = $("#tbActiveIamge");
    var $tr = targetTable.find("tr");
    //$($tr[i]).find("td:eq(3)").find("input:eq(4)").removeAttr("style")
    $tr.each(function (index, dom) {
        $(dom).find("td:eq(1)").find("input").removeAttr("disabled");//使图片说明可编辑
   
        $(dom).find("td:eq(3)").find("input:eq(0)").removeAttr("disabled");//启用上传按钮
        $(dom).find("td:eq(3)").find("input:eq(3)").removeAttr("style");//把删除按钮显示出来
        $(dom).find("td:eq(3)").find("input:eq(1)").removeAttr("disabled");//启用上传按钮
        $(dom).find("td:eq(3)").find("input:eq(2)").removeAttr("disabled");//启用上传按钮
    })
    $("#dtianjia2").removeAttr("disabled");
}

//7、保存图片标题之标题文字，活动图片管理，二维码图片
function SaveFile2() {
    var title1 = $.trim($("#txtTile1").val());
    var title2 = $.trim($("#txtTile2").val());
    var title3 = $.trim($("#txtTile3").val());
    if (title1 == "" || title2 == "" || title3 == "")
    {
        dialogHelper.Error({ content: "标题文字不能为空！" });
        return;
    }

    var targetTable = $("#tbActiveIamge");
    var $tr = targetTable.find("tr");
    var obj = null;
    var arr = new Array();
    $tr.each(function (index, dom) {
        obj = new Object();
        obj.ImageDescription = $(dom).find("td:eq(1)").find("input").val();//取图片说明的值     
        obj.ActivityImagePath = $(dom).find("td:eq(3)").find("input:eq(5)").val();
        obj.ActivityImageId = $(dom).find("td:eq(3)").find("input:eq(6)").val();
        arr.push(obj);
    })

    for (i = 0; i < arr.length; i++)
    {
        if (arr[i].ImageDescription == "null" || arr[i].ImageDescription == "") {
            dialogHelper.Error({ content: "图片说明不能为空！" });
            return;
        }
        if (arr[i].ActivityImagePath == "null" || arr[i].ActivityImagePath == "")
        {
            dialogHelper.Error({ content: "请选择图片上传！" });
            return;
        }
    }

    $.ajax({
        url: "/CompetitionAdmin/OfficialWebsite/SaveFile2",
        type: "POST",
        async: false,
        data: {
            Title1: $.trim($("#txtTile1").val()),
            Title2: $.trim($("#txtTile2").val()),
            Title3: $.trim($("#txtTile3").val()),
            activityImageList: arr,
            //QRCodeIntroduction: $.trim($("#txtQRCode").val()),
            QRCodeImgPath: $("#QRCodehideFilePath").val(),
            Id: $("#hidHomePageId").val()
        },
        success: function (data) {
            if (data.IsSuccess) {
                dialogHelper.Success({
                    content: msgList["20010"],
                    success: function () {
                        //$("#divtupian").find("input").attr("disabled", "disabled");
                        location.href = "/CompetitionAdmin/OfficialWebsite/Index";
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

//8、活动图片上传
function Upload(obj) {
    var $tr = $(obj);
    //验证上传文件类型
    var filePath = $tr.parent().find("input:eq(0)").val();
    var fileFiled = $tr.parent().find("input:eq(0)").attr("id");
    if (filePath == "") {
        dialogHelper.Error({ content: "请选择新的图片！" });
        return;
    }
    if (filePath.length > 0) {
        var fileType = filePath.substring(filePath.lastIndexOf(".")).toLowerCase();
        if (fileType == ".jpg" || fileType == ".gif" || fileType == ".png") {

        } else {
            dialogHelper.Error({ content: "只能选择jpg|gif|png文件!" });
            return;
        }
    }

    $tr.parent().parent().find("input:eq(4)").val('');          // 保存文件路径       
    $tr.parent().parent().find("input:eq(5)").val('');
    $(".background,.progressBar").show();
    $.ajaxFileUpload({
        url: "/CompetitionAdmin/OfficialWebsite/Import",
       
        fileElementId: fileFiled,
        async: false,
        dataType: 'json',
        success: function (data, status) {
            //data = JSON.parse(data);
            $tr.parent().parent().find("input:eq(4)").val(data.Data);          // 保存文件路径     
            $tr.parent().parent().find("input:eq(5)").val(data.Data);
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

//9、判断是否为null
function IsStrEmpty(str) {
    if (str == isNaN || str == "null") {
        return "";
    }
    else {

        return str;
    }
}

//10、删除行（物理删除）
function Del(id) {
    $.ajax({
        url: "/CompetitionAdmin/OfficialWebsite/DeleteActivityImage",
        type: "POST",
        async: false,
        data: {
            Id: id
        },
        success: function (data) {
            if (data.IsSuccess) {
                //dialogHelper.Success({
                //    content: msgList["20010"],
                //    success: function () {

                //    }
                //});
            }
            else {
                dialogHelper.Error({
                    content: data.ErrorCode
                });
            }
        }
    });
}

//11、联系我们可编辑
function Edit4() {
    $("#txtComTelConsultation").removeAttr("disabled");//大赛咨询电话
    $("#txtComPhone").removeAttr("disabled");//系统问题反馈电话
    $("#txtComQQ").removeAttr("disabled");//大赛QQ群
}

//12、联系我们保存
function SaveFile4() {
    if (!VerificationHelper.checkFrom("lianxime")) {
        return;
    }
    $.ajax({
        url: "/CompetitionAdmin/OfficialWebsite/SaveFile4",
        type: "POST",
        async: false,
        data: {
            ComTelConsultation: $.trim($("#txtComTelConsultation").val()),
            ComPhone: $.trim($("#txtComPhone").val()),
            ComQQ: $.trim($("#txtComQQ").val()),
            Id: $("#hidHomePageId").val()
        },
        success: function (data) {
            if (data.IsSuccess) {
                dialogHelper.Success({
                    content: msgList["20010"],
                    success: function () {
                        $("#lianxime").find("input").attr("disabled", "disabled");
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

//13、活动日程可编辑
function Edit5() {
    $("#tStep1").removeAttr("disabled");//STEP1报名
    $("#tStep2").removeAttr("disabled");//STEP2比赛中
    $("#tStep3").removeAttr("disabled");//STEP3评审
    $("#tStep4").removeAttr("disabled");//STEP4颁奖
}

//14、活动日程保存
function SaveFile5() {
    $.ajax({
        url: "/CompetitionAdmin/OfficialWebsite/SaveFile5",
        type: "POST",
        async: false,
        data: {
            Step1Description: $.trim($("#tStep1").val()),
            Step2Description: $.trim($("#tStep2").val()),
            Step3Description: $.trim($("#tStep3").val()),
            Step4Description: $.trim($("#tStep4").val()),
            Id: $("#hidHomePageId").val()
        },
        success: function (data) {
            if (data.IsSuccess) {
                dialogHelper.Success({
                    content: msgList["20010"],
                    success: function () {
                        $("#huodongricheng").find("textarea").attr("disabled", "disabled");
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

//友情链接图片上传
function Upload2(obj) {
    var $tr = $(obj);
    //验证上传文件类型
    var filePath = $tr.parent().find("input:eq(0)").val();
    var fileFiled = $tr.parent().find("input:eq(0)").attr("id");
    if (filePath == "") {
        dialogHelper.Error({ content: "请选择新的图片！" });
        return;
    }
    if (filePath.length > 0) {
        var fileType = filePath.substring(filePath.lastIndexOf(".")).toLowerCase();
        if (fileType == ".jpg" || fileType == ".gif" || fileType == ".png") {

        } else {
            dialogHelper.Error({ content: "只能选择jpg|gif|png文件!" });
            return;
        }
    }

    $tr.parent().parent().find("input:eq(4)").val('');          // 保存文件路径       

    $(".background,.progressBar").show();
    $.ajaxFileUpload({
        url: "/CompetitionAdmin/OfficialWebsite/Import",
        secureuri: false,
        fileElementId: fileFiled,
        dataType: 'json',
        success: function (data, status) {
            $("#logohideFilePath").val(data.Data);          // 保存文件路径            
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

//15、友情链接添加弹出框
function PopAddFriendLink() {
    var targetTable = $("#tbLogo");
    var index = targetTable.find("tr").length - 1;
    if (index > 4) {
        dialogHelper.Error({
            content: "最多只能添加5条记录！"
        });
        return;
    }
    $("#popAddFriendLink").find("table input").val("");//添加时先清空文本框及隐藏框的内容
    $("#logohideFilePath").val("");
    $("#popAddFriendLink h3").html("添加友情链接");
    $('#txtLinkAddress').val('http://');
    dialogHelper.Show("popAddFriendLink");
}

//16、提交友情链接，将提交的内容显示在友情链接列表下，不保存数据库
function CommitFriendLink() {

    //先判断该条记录是新添加还是待修改的
    var hideLinkID = $("#hideLinkID").val();
    //if (hideLinkID != "") {
    //    $("#tbLogo input[type='checkbox']:checked").parent().parent().remove();
    //}
    $("#tbLogo input[type='checkbox']:checked").parent().parent().remove();
    var txtLinkAddress = $.trim($("#txtLinkAddress").val());
    var txtLinkName = $.trim($("#txtLinkName").val());
   
    if (txtLinkAddress == "")
    {
        dialogHelper.Error({ content: "链接地址不能为空！" });
        return;
    }
    if (txtLinkName == "") {
        dialogHelper.Error({ content: "链接名称不能为空！" });
        return;
    }
    var filePath = $("#logohideFilePath").val();
    if (filePath == "") {
        dialogHelper.Error({
            content: "请先上传logo！"
        });
        return;
    }
    var trHtml = "";
    trHtml +="<tr>";        
    trHtml +="<td><input name=\"\"  type=\"checkbox\" value=\"\"></td>";
    trHtml += "<td><img src=" + fileUploadAddress + filePath + " width=\"79\" height=\"50\" class=\"d_imgs\"></td>";
    trHtml += "<td>" + txtLinkAddress + "</td>";
    trHtml += "<td><a href=\"javascript:delLinkRow(this)\">删除</a></td>";
    trHtml += " <input type=\"hidden\" id=\"logohideFilePath\" value=" + filePath + ">";
    trHtml += " <input type=\"hidden\" id=\"logohideId\" value=" + hideLinkID + ">";
    trHtml += " <input type=\"hidden\" id=\"hidLinkName\" value=" + txtLinkName + ">";
    trHtml += "</tr>";
    $("#tbLogo").append(trHtml);
    dialogHelper.Close("popAddFriendLink");
}

//17、加载友情链接行
function LoadFriendLinkRow(dom) {
    var targetTable = $("#tbLogo");
    var index = targetTable.find("tr").length - 1;
    var trHtml = "";
    trHtml += "<tr>";
    trHtml += "<td><input name=\"\" type=\"checkbox\" value="+dom.Id+"></td>";
    trHtml += "<td><img src=" + fileUploadAddress + dom.LinkImagePath + " width=\"79\" height=\"50\" class=\"d_imgs\"></td>";
    trHtml += "<td>" + dom.LinkAddress + "</td>";
    trHtml += "<td><a href=\"javascript:void(0)\" onclick=\"delLinkRow(this)\">删除</a></td>";
    trHtml += " <input type=\"hidden\" id=\"logohideFilePath" + (parseInt(index) + 1) + "\" value=" + dom.LinkImagePath + ">";
    trHtml += " <input type=\"hidden\" id=\"logohideId" + (parseInt(index) + 1) + "\" value=" + dom.Id + ">";
    trHtml += " <input type=\"hidden\" id=\"hidLinkName" + (parseInt(index) + 1) + "\" value=" + dom.LinkName + ">";
    trHtml += "</tr>";
    $("#tbLogo").append(trHtml);
}

//18、保存友情链接
function SaveFile3() {
    var targetTable = $("#tbLogo");
    var $tr = targetTable.find("tr").not(':eq(0)');//获取除去第一行的所有行
    var obj = null;
    var arr = new Array();
    $tr.each(function (index, dom) {
        obj = new Object();
        obj.LinkImagePath = $(dom).find("img").attr("src").substring($(dom).find("img").attr("src").lastIndexOf("\/") + 1);//取logo图片的路径     
        obj.LinkName = $(dom).find("input:eq(3)").val();//链接名称
        obj.LinkAddress = $(dom).find("td:eq(2)").html();//链接地址
        obj.Id = $(dom).find("input:eq(2)").val();
        arr.push(obj);
    })

    $.ajax({
        url: "/CompetitionAdmin/OfficialWebsite/SaveFile3",
        type: "POST",
        async: false,
        data: {
            FriendLinkList: arr,
            Id: $("#hidHomePageId").val()
        },
        success: function (data) {
            if (data.IsSuccess) {
                dialogHelper.Success({
                    content: msgList["20010"],
                    success: function () {
                        //$("#youqinglianjie").find("input").attr("disabled", "disabled");
                        location.href = "/CompetitionAdmin/OfficialWebsite/Index";
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

//19、友情链接编辑弹出框
function Edit3() {
    $("#popAddFriendLink h3").html("编辑友情链接");
    var trLen = $("#tbLogo input[type='checkbox']:checked").length;
    
    if (trLen <= 0) {
        dialogHelper.Error({
            content: "请选择一条友情链接！"
        });
        return;
    }
    if (trLen > 1) {
        dialogHelper.Error({
            content: "不能选择多条友情链接！"
        });
        return;
    }
    var linkId = $("#tbLogo input[type='checkbox']:checked").val();
    if (linkId != "") {
        GetFriendLinkModel(linkId);
    }
    else {
        var $selectRow = $("#tbLogo input[type='checkbox']:checked").parent().parent();

        $("#txtLinkAddress").val($selectRow.find("td").eq(2).text());
        $("#txtLinkName").val($selectRow.find("input").eq(3).val());
        $("#logotextfield").val($selectRow.find("input").eq(1).val());
        $("#logofilePath").val($selectRow.find("input").eq(1).val());
        $("#logohideFilePath").val($selectRow.find("input").eq(1).val());
        //$("#hideHomePageID").val(data.Data.HomePageId);
        //$("#hideCollegeID").val(data.Data.CollegeId);
        //$("#hideLinkID").val(data.Data.Id);
    }
   
    dialogHelper.Show("popAddFriendLink");
}

//20、获取友情链接对象
function GetFriendLinkModel(linkId) {
    $.ajax({
        url: "/CompetitionAdmin/OfficialWebsite/GetFriendLinkModel",
        type: "POST",
        async: false,
        data: {
            Id: linkId
        },
        success: function (data) {
            if (data.IsSuccess) {
                $("#txtLinkAddress").val(data.Data.LinkAddress);
                $("#txtLinkName").val(data.Data.LinkName);
                $("#logotextfield").val(data.Data.LinkImagePath);
                $("#logofilePath").val(data.Data.LinkImagePath);
                $("#logohideFilePath").val(data.Data.LinkImagePath);
                $("#hideHomePageID").val(data.Data.HomePageId);
                $("#hideCollegeID").val(data.Data.CollegeId);
                $("#hideLinkID").val(data.Data.Id);
            }
            else {
                dialogHelper.Error({
                    content: data.ErrorCode
                });
            }
        }
    });
}

//21、删除友情链接行
function delLinkRow(obj) {
    var $a = $(obj);
    var id = $a.parent().parent().find("input:eq(2)").val()
    if (!id) {
        var trLen = $("#tbLogo").find("tr").not(':eq(0)').length;
        $("#tbLogo").find("tr:eq(" + (trLen) + ")").remove();
    }
    else {
        dialogHelper.Confirm({
            content: "您确定要删除该条记录吗？",
            success: function () {
                DelLink(obj, id);
            }
        })

    }
}

//22、删除行（物理删除）
function DelLink(o, id) {

    $.ajax({
        url: "/CompetitionAdmin/OfficialWebsite/DeleteFriendLink",
        type: "POST",
        async: false,
        data: {
            Id: id
        },
        success: function (data) {
            if (data.IsSuccess) {
                dialogHelper.Success({
                    content: "删除成功！"
                });
                var $a = $(o);
                $a.parent().parent().remove();
            }
            else {
                dialogHelper.Error({
                    content: data.ErrorCode
                });
            }
        }
    });
}

//23、加载专家列表
function LoadExpertsRow(dom) {
    var targetTable = $("#tbExperts");
    var index = targetTable.find("tr").length - 1;
    var trHtml = "";
    trHtml += "<tr rowId=" + (parseInt(index) + 1) + ">";
    trHtml += "<td><input name=\"\" type=\"checkbox\" value=" + dom.Id + "></td>";
    trHtml += "<td>" + dom.ExpertsName + "</td>";
    trHtml += "<td><img data-imagepath=" + dom.ExpertsPicPath + " src=" + fileUploadAddress + dom.ExpertsPicPath + " width=\"79\" height=\"50\" class=\"d_imgs\"></td>";
    trHtml += "<td><textarea disabled=\"disabled\">" + dom.ExpertsIntroduction + "</textarea></td>";
    trHtml += "<td><a href=\"javascript:void(0)\" onclick=\"delExpertsRow(this)\">删除</a></td>";
    trHtml += " <input type=\"hidden\" id=\"expertshideFilePath" + (parseInt(index) + 1) + "\" value=" + dom.ExpertsPicPath + ">";
    trHtml += " <input type=\"hidden\" id=\"expertshideId" + (parseInt(index) + 1) + "\" value=" + dom.Id + ">";
    trHtml += "</tr>";
    $("#tbExperts").append(trHtml);
}

//专家风采图片上传
function Upload3(obj) {
    var $tr = $(obj);
    //验证上传文件类型
    var filePath = $tr.parent().find("input:eq(0)").val();
    var fileFiled = $tr.parent().find("input:eq(0)").attr("id");
    if (filePath == "") {
        dialogHelper.Error({ content: "请选择新的图片！" });
        return;
    }
    if (filePath.length > 0) {
        var fileType = filePath.substring(filePath.lastIndexOf(".")).toLowerCase();
        if (fileType == ".jpg" || fileType == ".gif" || fileType == ".png") {

        } else {
            dialogHelper.Error({ content: "只能选择jpg|gif|png文件!" });
            return;
        }
    }

    $tr.parent().find("input:eq(5)").val('');          // 保存文件路径       

    $(".background,.progressBar").show();
    $.ajaxFileUpload({
        url: "/CompetitionAdmin/OfficialWebsite/Import",
        secureuri: false,
        fileElementId: fileFiled,
        dataType: 'json',
        success: function (data, status) {
            $("#expertshideFilePath").val(data.Data);          // 保存文件路径            
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

//24、专家添加弹出框
function PopAddExperts() {
    var targetTable = $("#tbExperts");
    var index = targetTable.find("tr").length - 1;
    if (index > 9) {
        dialogHelper.Error({
            content: "最多只能添加10条记录！"
        });
        return;
    }
    $("#popAddExperts").find("table input,textarea").val("");//添加时先清空文本框及隐藏框的内容
    $("#expertshideFilePath").val("");
    $("#popAddExperts h3").html("添加专家");
    dialogHelper.Show("popAddExperts");
}

//25、提交专家，将专家放于专家风采列表下面，不保存数据库
function CommitExperts() {
    //先判断该条记录是新添加还是待修改的
    var hideexpertsID = $("#hideexpertsID").val();
    //if (hideexpertsID != "") {
    //    $("#tbExperts input[type='checkbox']:checked").parent().parent().remove();
    //}
    $("#tbExperts input[type='checkbox']:checked").parent().parent().remove();

    var txtExpertsName = $.trim($("#txtExpertsName").val());
    if (txtExpertsName == "")
    {
        dialogHelper.Error({ content: "专家姓名不能为空！" });
        return;
    }
    if (txtExpertsName.length > 15)
    {
        dialogHelper.Error({ content: "专家姓名字数受限制！" });
        return;
    }


    var txtExpertsIntroduction = $.trim($("#txtExpertsIntroduction").val());
    if (txtExpertsIntroduction == "")
    {
        dialogHelper.Error({ content: "专家介绍不能为空！" });
        return;
    }

    if (txtExpertsIntroduction.length > 400)
    {
        dialogHelper.Error({ content: "专家介绍字数受限制！" });
        return;
    }
    var filePath = $("#expertshideFilePath").val();

    if (filePath == "") {
        dialogHelper.Error({
            content: "请先上传照片！"
        });
        return;
    }
    var trHtml = "";
    var targetTable = $("#tbExperts");
    var index = targetTable.find("tr").length - 1;
    trHtml += "<tr rowId=" + (parseInt(index) + 1) + ">";
    trHtml += "<td><input name=\"\"  type=\"checkbox\" value=\"\"></td>";
    trHtml += "<td>" + txtExpertsName + "</td>";
    trHtml += "<td><img data-imagepath=" + filePath + " src=" + fileUploadAddress + filePath + " width=\"79\" height=\"50\" class=\"d_imgs\"></td>";
    trHtml += "<td><textarea disabled=\"disabled\">" + txtExpertsIntroduction + "</textarea></td>";
    trHtml += "<td><a href=\"javascript:void(0)\" onclick=\"delExpertsRow(this)\">删除</a></td>";
    trHtml += " <input type=\"hidden\" id=\"expertshideFilePath\" value=" + filePath + ">";
    trHtml += " <input type=\"hidden\" id=\"expertshideId\" value=" + hideexpertsID + ">";
    trHtml += "</tr>";

    $("#tbExperts").append(trHtml);
    dialogHelper.Close("popAddExperts");
}

//26、保存专家
function SaveFile6() {
    var targetTable = $("#tbExperts");
    var $tr = targetTable.find("tr").not(':eq(0)');//获取除去第一行的所有行
    var obj = null;
    var arr = new Array();
    $tr.each(function (index, dom) {
        obj = new Object();
        obj.ExpertsName = $(dom).find("td:eq(1)").html();//专家名称  
        obj.ExpertsIntroduction = $(dom).find("td textarea").val();//专家介绍      
        obj.ExpertsPicPath = $(dom).find("td:eq(2)").find("img").attr("data-imagepath");//取专家照片的路径     
        obj.Id = $(dom).find("input:eq(2)").val();
        arr.push(obj);
    })

    $.ajax({
        url: "/CompetitionAdmin/OfficialWebsite/SaveFile6",
        type: "POST",
        async: false,
        data: {
            ExpertsList: arr,
            Id: $("#hidHomePageId").val()
        },
        success: function (data) {
            if (data.IsSuccess) {
                dialogHelper.Success({
                    content: msgList["20010"],
                    success: function () {
                        //$("#tbExperts").find("textarea").attr("disabled", "disabled");
                        location.href = "/CompetitionAdmin/OfficialWebsite/Index";
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

//27、专家编辑弹出框
function Edit6() {
    $("#popAddExperts h3").html("编辑专家");
    var trLen = $("#tbExperts input[type='checkbox']:checked").length;   

    if (trLen <= 0) {
        dialogHelper.Error({
            content: "请选择一条记录！"
        });
        return;
    }
    if (trLen > 1) {
        dialogHelper.Error({
            content: "不能选择多条记录！"
        });
        return;
    }

    var expertsId = $("#tbExperts input[type='checkbox']:checked").val();
    if (expertsId != "") {
        GetExpertsModel(expertsId);
    }
    else {
        var $selectRow = $("#tbExperts input[type='checkbox']:checked").parent().parent();
        
        $("#txtExpertsName").val($selectRow.find("td").eq(1).text());
        $("#txtExpertsIntroduction").val($selectRow.find("td").eq(3).text());
        $("#expertstextfield").val($selectRow.find("td").eq(2).find("img").attr("data-imagepath"));
        $("#expertshideFilePath").val($selectRow.find("td").eq(2).find("img").attr("data-imagepath"));
        $("#expertsPath").val($selectRow.find("td").eq(2).find("img").attr("data-imagepath"));
        //$("#hideHomePageID").val(data.Data.HomePageId);
        //$("#hideCollegeID").val(data.Data.CollegeId);
        //$("#hideexpertsID").val(data.Data.Id);
    }  
   
    dialogHelper.Show("popAddExperts");
}

//28、获取专家对象
function GetExpertsModel(expertsId) {
    $.ajax({
        url: "/CompetitionAdmin/OfficialWebsite/GetExpertsModel",
        type: "POST",
        async: false,
        data: {
            Id: expertsId
        },
        success: function (data) {
            if (data.IsSuccess) {
                $("#txtExpertsName").val(data.Data.ExpertsName);
                $("#txtExpertsIntroduction").val(data.Data.ExpertsIntroduction);
                $("#expertstextfield").val(data.Data.ExpertsPicPath);
                $("#expertshideFilePath").val(data.Data.ExpertsPicPath);
                $("#expertsPath").val(data.Data.ExpertsPicPath);
                $("#hideHomePageID").val(data.Data.HomePageId);
                $("#hideCollegeID").val(data.Data.CollegeId);
                $("#hideexpertsID").val(data.Data.Id);
            }
            else {
                dialogHelper.Error({
                    content: data.ErrorCode
                });
            }
        }
    });
}

//29、删除专家行
function delExpertsRow(obj) {
    var $a = $(obj);
    var id = $a.parent().parent().find("input:eq(2)").val()
    if (!id) {
        var trLen = $("#tbExperts").find("tr").not(':eq(0)').length;
        $("#tbExperts").find("tr:eq(" + (trLen) + ")").remove();
    }
    else {
        dialogHelper.Confirm({
            content: "您确定要删除该条记录吗？",
            success: function () {
                DelExperts(obj, id);
            }
        })

    }
}

//30、删除行（物理删除）
function DelExperts(obj, id) {
    $.ajax({
        url: "/CompetitionAdmin/OfficialWebsite/DeleteExperts",
        type: "POST",
        async: false,
        data: {
            Id: id
        },
        success: function (data) {
            if (data.IsSuccess) {
                dialogHelper.Success({
                    content: "删除成功！"
                });
                var $a = $(obj);
                $a.parent().parent().remove();
            }
            else {
                dialogHelper.Error({
                    content: data.ErrorCode
                });
            }
        }
    });
}

//31、加载荣誉榜列表
function LoadHonorRollRow(dom) {
    var targetTable = $("#tbHonorRoll");
    var index = targetTable.find("tr").length;
    var trHtml = "";
    trHtml += "<tr>";
    //trHtml += "<td><input name=\"\"  type=\"checkbox\" value=" + dom.Id + "></td>";
    trHtml += "<td>名称：</td>";
    trHtml += "<td><input maxlength=\"10\" type=\"text\" disabled=\"disabled\"   name=\"txtComName\" id=\"txtComName\" value=" + dom.CompetitionName + "></td>";
    //trHtml += "<td>成绩信息：</td>";
    //trHtml += "<td><input type=\"text\"   value=\"添加成功\"></td>";
    //trHtml += "<td><input name=\"input\"   onclick=\"popCompetition()\" type=\"image\" src=\"/Content/images/d_xin/xuanze .png\"></td>";
    trHtml += "<td><input name=\"input\"   type=\"image\" onclick=\"delHonorRow(this)\" src=\"/Content/images/d_xin/d_shanchu.png\"></td>";
    trHtml += " <input type=\"hidden\" id=\"honorhideId" + (parseInt(index) + 1) + "\" value=" + dom.Id + ">";
    trHtml += " <input type=\"hidden\" id=\"hidComId" + (parseInt(index) + 1) + "\" value=" + dom.CompetitionId + ">";
    trHtml += "</tr>";
    $("#tbHonorRoll").append(trHtml);
}

//32、动态添加荣誉榜行
function AddHonorRow() {
    var targetTable = $("#tbHonorRoll");
    var index = targetTable.find("tr").length;
    if (index > 2) {
        dialogHelper.Error({ content: "荣誉榜超过上限！" });
        return;
    }

    var trHtml = "";

    trHtml += "<tr>";
    //trHtml += "<td><input name=\"\" type=\"checkbox\" value=" + index + "></td>";
    trHtml += "<td>名称：</td>";
    trHtml += "<td><input maxlength=\"10\" onkeypress=\"specialTextValidate()\" type=\"text\" name=\"txtComName" + (parseInt(index) + 1) + "\" id=\"txtComName" + (parseInt(index) + 1) + "\"></td>";
    //trHtml += "<td>成绩信息：</td>";
    //trHtml += "<td><input type=\"text\" value=\"\"></td>";
    //trHtml += "<td><input name=\"input\" onclick=\"popCompetition()\" type=\"image\" src=\"/Content/images/d_xin/xuanze .png\"></td>";
    trHtml += "</tr>";
    $("#tbHonorRoll").append(trHtml);
}

//33、弹出大赛列表
function popCompetition() {
    var targetTable = $("#tbHonorRoll");
    var trLen = targetTable.find("tr").length;
    if (trLen >= 3) {
        dialogHelper.Error({ content: "荣誉榜超过上限！" });
        return;
    }
    var comIdArr = new Array();
    $('#tbHonorRoll tr').each(function (index) {
        comIdArr[index] = $(this).find("input[type=hidden]:eq(1)").val();
    });

    pageHelper.Init({
        url: "/CompetitionAdmin/OfficialWebsite/GetCompetitionList",
        type: "POST",
        pageDiv: "#pages_selectCompetition",

        bind: function (data) {
            var html = "";
            $(data.Data).each(function (index, dom) {
                if (comIdArr.length > 0) {
                    //每行html
                    var trHtml = "";
                    trHtml += "<tr>";
                    if (jQuery.inArray(dom.Id.toString(),comIdArr) > -1) {
                        trHtml += "<td  style='text-align:center;'><input class=\"icheck\" checked=\"checked\" disabled=\"disabled\" type=\"checkbox\" id=\"chkUser_{1}\" val=\"{1}\"></td>";
                    }
                    else {
                        trHtml += "<td  style='text-align:center;'><input class=\"icheck\"  type=\"checkbox\" id=\"chkUser_{1}\" val=\"{1}\"></td>";
                    }
                    //if (comIdArr.indexOf(dom.Id.toString()) > -1) {
                    //    trHtml += "<td  style='text-align:center;'><input class=\"icheck\" checked=\"checked\" disabled=\"disabled\" type=\"checkbox\" id=\"chkUser_{1}\" val=\"{1}\"></td>";
                    //}
                    //else {
                    //    trHtml += "<td  style='text-align:center;'><input class=\"icheck\"  type=\"checkbox\" id=\"chkUser_{1}\" val=\"{1}\"></td>";
                    //}
                    trHtml += "<td id=\"txtCompetitionName_{1}\">{0}</td>";
                    trHtml += "<td><input type=\"hidden\" value=\"{1}\"></td>";
                    trHtml += "</tr>";
                    //拼接tbody
                    html += StringHelper.FormatStr(trHtml,
                        dom.Name,
                        dom.Id
                    );
                }
                else {
                    //每行html
                    var trHtml = "";
                    trHtml += "<tr>";
                    trHtml += "<td style='text-align:center;'><input class=\"icheck\"  type=\"checkbox\" id=\"chkUser_{1}\" val=\"{1}\"></td>";
                    trHtml += "<td id=\"txtCompetitionName_{1}\">{0}</td>";
                    trHtml += "<td><input type=\"hidden\" value=\"{1}\"></td>";
                    trHtml += "</tr>";
                    //拼接tbody
                    html += StringHelper.FormatStr(trHtml,
                        dom.Name,
                        dom.Id
                    );
                }

            });
            $("#tbdCompetition").html(html);
            dialogHelper.Show("popSelectCompetition");
        }
    });
}

//34、选中大赛，并置于荣誉榜下面
function SelectCompetition() {
    var trLength = $("#tbHonorRoll").find("tr").length;
    $trs = $("#tbdCompetition input[type='checkbox']:checked").not(":disabled")//获取属性不为disabled的行元素
    var trLen = $trs.length;
    if ((parseInt(trLength) + parseInt(trLen)) > 3) {
        dialogHelper.Error({
            content: "荣誉榜超过上限！"
        });
        return;
    }
    $trs.each(function (index, dom) {
        dialogHelper.Close("popSelectCompetition");
        LoadHonorRollRow2(dom);
    })
}

//35、加载荣誉榜列表
function LoadHonorRollRow2(dom) {
    var targetTable = $("#tbHonorRoll");
    var index = targetTable.find("tr").length;
    var comName = $(dom).parent().parent().find("td:eq(1)").html();
    var comId = $(dom).parent().parent().find("td:eq(2)").find("input").val();
    var trHtml = "";
    trHtml += "<tr>";
    //trHtml += "<td><input name=\"\" type=\"checkbox\" value=\"\"></td>";
    trHtml += "<td>名称：</td>";
    trHtml += "<td><input type=\"text\" name=\"txtComName\" id=\"txtComName\" value=" + comName + "></td>";
    //trHtml += "<td>成绩信息：</td>";
    //trHtml += "<td><input type=\"text\" value=\"添加成功\"></td>";
    //trHtml += "<td><input name=\"input\" onclick=\"popCompetition()\" type=\"image\" src=\"/Content/images/d_xin/xuanze .png\"></td>";
    trHtml += "<td> <input name=\"input\" type=\"image\" onclick=\"delHonorRow(this)\" src=\"/Content/images/d_xin/d_shanchu.png\"></td>";
    trHtml += "<input type=\"hidden\" id=\"honorhideId" + (parseInt(index) + 1) + "\" value=\"\">";
    trHtml += "<input type=\"hidden\" id=\"hidComId" + (parseInt(index) + 1) + "\" value=" + comId + ">";
    trHtml += "</tr>";
    $("#tbHonorRoll").append(trHtml);
}

//36、保存荣誉榜
function SaveFile7() {
    var targetTable = $("#tbHonorRoll");
    var $tr = targetTable.find("tr");
    var obj = null;
    var arr = new Array();
    $tr.each(function (index, dom) {
        obj = new Object();
        obj.CompetitionName = $(dom).find("td:eq(1)").find("input").val();
        obj.Id = $(dom).find("input[type='hidden']:eq(0)").val();
        obj.CompetitionId = $(dom).find("input[type='hidden']:eq(1)").val();
        arr.push(obj);
    })

    for (i = 0; i < arr.length; i++)
    {
        if (arr[i].CompetitionName == "" || arr[i].CompetitionName=="null")
        {
            dialogHelper.Error({ content: "名称不能为空！" });
            return;
        }
        if (arr[i].CompetitionName.length > 10)
        {
            dialogHelper.Error({ content: "名称不能超过10个字符！" });
            return;
        }
    }

    $.ajax({
        url: "/CompetitionAdmin/OfficialWebsite/SaveFile7",
        type: "POST",
        async: false,
        data: {
            HonorRollList: arr,
            Id: $("#hidHomePageId").val()
        },
        success: function (data) {
            if (data.IsSuccess) {
                dialogHelper.Success({
                    content: msgList["20010"],
                    success: function () {
                        //$("#tbHonorRoll input").attr("disabled", "disabled");
                        location.href = "/CompetitionAdmin/OfficialWebsite/Index";
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

//37、荣誉榜编辑弹出框
//function Edit7() {
//    $("#tbHonorRoll input").removeAttr("disabled");
//    var trLen = $("#tbHonorRoll input[type='checkbox']:checked").length;

//    if (trLen <= 0) {
//        dialogHelper.Error({
//            content: "请选择一条记录！"
//        });
//        return;
//    }
//    if (trLen > 1) {
//        dialogHelper.Error({
//            content: "不能选择多条记录！"
//        });
//        return;
//    }

//}



//38、删除行
function delHonorRow(obj) {
    var $tr = $(obj);
    var id = $tr.parent().parent().find("input[type='hidden']:eq(0)").val();
    if (!id) {
        var trLen = $("#tbHonorRoll").find("tr").length;
        $("#tbHonorRoll").find("tr:eq(" + (trLen - 1) + ")").remove();
    }
    else {
        dialogHelper.Confirm({
            content: "您确定要删除该条记录吗？",
            success: function () {
                $tr.parent().parent().remove();
                DelHonor(id);
            }
        })
    }
}

//39、删除行（物理删除）
function DelHonor(id) {
    $.ajax({
        url: "/CompetitionAdmin/OfficialWebsite/DeleteHonor",
        type: "POST",
        async: false,
        data: {
            Id: id
        },
        success: function (data) {
            if (data.IsSuccess) {

            }
            else {
                dialogHelper.Error({
                    content: data.ErrorCode
                });
            }
        }
    });
}

//40、荣誉榜编辑
function Edit7()
{
    $("#tbHonorRoll input").removeAttr("disabled");
}

//41、限制特殊字符
function specialTextValidate() {
    var code;
    var character;
    //var err_msg = "该输入域不能包含下列字符之一:\n \\ / : * ? \" < > | & , ";
    if (document.all) {
        code = window.event.keyCode;
    } else {
        code = arguments.callee.caller.arguments[0].which;
    }
    var character = String.fromCharCode(code);
    var txt = new RegExp("[\\*,\\&,\\\\,\\/,\\?,\\|,\\:,\\<,\\>,\"]");
    if (txt.test(character)) {
        //alert(err_msg);
        if (document.all) {
            window.event.returnValue = false;
        } else {
            arguments.callee.caller.arguments[0].preventDefault();
        }
    }
}

