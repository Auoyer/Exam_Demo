
var QuestionOption = new arrayHelper("Sort");//选项
var QuestionAnswer = new arrayHelper("Sort");//答案
var QuestionAttachments = new arrayHelper("Name");//附件
var CurQuestionCount = 0;
$(function () {

    //文件选择
    $("#FileChoose").click(function () {
        $("#HFilePath").click();
    });

    //将file的值赋值给input
    $("#HFilePath").live("change", function () {
        var filepath = $(this).val();
        $("#filePath").val(filepath);
    });

    $("#btnAdd").attr('disabled', false);
    //上传附件按钮
    $("#btnAdd").click(function () {
        dialogHelper.Show("addpop", 756);
        $("#txtResourceName").val("");
        $("#filePath").val("");

    });


    //附件确定按钮
    $("#FileUpload").click(function () {
        FileUpload();
    });
    //附件上传取消按钮
    $("#addclose").click(function () {
        dialogHelper.Close("addpop");

        $("#filePath").val("");
        $("#HFilePath").val("");
        var CurQuestionCounts = $("#CurQuestionCount").html();
        QuestionAttachments.Remove(CurQuestionCounts);
    });

    //保存按钮
    $("#preserve").click(function () {
        //章节Id
        var TheoryChapterId = $.getUrlParam("TheoryChapterId");
        var typeid = $.getUrlParam("typeid");
        var PaperId = $("#TestPapersId").val();

        AddTitle(TheoryChapterId, typeid, PaperId);
    });



});

//加载试题
function LoadTitle(QuestionId) {
    $.ajax({
        url: "/CompetitionAdmin/Resource/GetQuestionObj",
        async: false,
        type: "POST",
        data: {
            QuestionId: QuestionId
        },
        success: function (data) {
            QuestionOption.RemoveAll();//选项
            QuestionAnswer.RemoveAll(); //答案
            QuestionAttachments.RemoveAll(); //附件
            var mode = data.TE;
            if (mode.Context != "") {
                $("#Context").val(mode.Context);
                $("#Analysis").val(mode.Analysis);

                $("#option").find("li").remove();
                $("#QuestionIdAdd1").val(mode.Id);

            }
            //选项
            if (mode.OptionList != null) {
                var html = "";
                $.each(mode.OptionList, function (i, n) {
                    QuestionOption.Add(n);

                });
            }
            //附件
            if (mode.AttachmentList != null) {
                $.each(mode.AttachmentList, function (i1, n1) {
                    QuestionAttachments.Add(n1);
                });
            }

            createOptions();
            var typeName = $("#TypeName").find("option:selected").text();
            if (typeName == "单选题" || typeName == "多选题" || typeName == "判断题") {
                CreateSelect();
            }
            if (typeName != "单选题" && typeName != "多选题" && typeName != "判断题") {

                $("#xuanxaing1").hide();
                $("#jiexi").hide();
            } else {


                $("#xuanxaing1").show();
                $("#jiexi").show();
            }
            //答案
            if (mode.AnswerList != null) {
                $.each(mode.AnswerList, function (i2, n2) {
                    QuestionAnswer.Add(n2);
                    switch (n2.Answer) {
                        case 0:
                            $("#A").attr("checked", true);
                            break;
                        case 1:
                            $("#B").attr("checked", true);
                            break;
                        case 2:
                            $("#C").attr("checked", true);
                            break;
                        case 3:
                            $("#D").attr("checked", true);
                            break;
                        default:
                            $("#option").find("li:eq(4)").find("input").attr("checked", true);
                            break;
                    }
                });
            }
            dialogHelper.Reset('popQuestionBankAdd');
        }
    });
}

var type = "";
//查询对应题型
function SelectTitle(strList) {
    //题型下拉列表   
    selectHelper.GetSelect({
        url: "/CompetitionAdmin/Paper/TiXingSelList",
        Id: "#TypeName",
        value: "全部",
        data: { strList: strList }
    });
}

var Index = 0;
//保存题目
function AddTitle(TheoryChapterId, typeid, PaperId) {
    if ($("#preserve").val() == "编辑") {

        var bo = CheckSource($("#QuestionIdAdd1").val());
        if (bo) {
            dialogHelper.Error({ content: "内置题目不能进行编辑！", success: function () { } });
            return false;
        }

        $("#preserve").val("保存");
        //启用文本框
        $("#Context").attr('disabled', false);
        //选项
        var Fobj = $("#option").find("li");
        $.each(Fobj, function (i, n) {
            $(n).find("input:eq(0)").attr('disabled', false);
            $(n).find("input:eq(1)").attr('disabled', false);
            $(n).find("span").show();
        });

        $("#btnAdd").attr('disabled', false);
        //添加选项按
        var Fobj = $("#option").find("li");
        if (Fobj.length <= 4) {
            $("#Add1").show();
        } else {
            $("#Add1").hide();
        }
        //附件
        var Fobj = $("#Dappendix").find("a");
        $.each(Fobj, function (i, n) {
            $(n).find("b").show();
        });
        //解析
        $("#Analysis").attr('disabled', false);

        $("#upward").attr('disabled', true);
        $("#upward").addClass('btndisable');
        $("#down").attr('disabled', true);
        $("#down").addClass('btndisable');

    } else {
        if (!VerificationHelper.checkFrom("popQuestionBankAdd")) {
            return false;
        }
        QuestionOption.RemoveAll();//选项
        QuestionAnswer.RemoveAll();//答案
        //添加试题
        var obg = new Object();
        obg["Id"] = $("#QuestionIdAdd1").val();//Id
        obg["Context"] = $("#Context").val();//题干
        obg["LibraryID"] = 1;//所属题库(理论考核/认证考试)
        var typeName = $("#TypeName").find("option:selected").text();
        if (typeName == "单选题") {
            obg["StructType"] = 1;//题目类型（枚举）
        } else if (typeName == "多选题") {
            obg["StructType"] = 2;//题目类型（枚举）
        } else if (typeName == "判断题") {
            obg["StructType"] = 3;//题目类型（枚举）
        }

        obg["CharpterID"] = typeid;//认证类型Id/章节Id(对应下面的题型)      
        obg["Analysis"] = $("#Analysis").val();//解析
        obg["Status"] = 1;//状态 开启   
        obg["PaperId"] = PaperId;//状态 开启   

        //添加选项和答案
        var num = 0;
        var num2 = 0;
        var Fobj = $("#option").find("li");
        $.each(Fobj, function (i, n) {
            var obj = new Object();
            obj["OptionName"] = $(n).find("input:eq(1)").val();
            obj["Sort"] = i;
            QuestionOption.Add(obj);
            num2 = num2 + 1;
            //选中的为答案
            if ($(n).find("label").find("input").attr("checked") == "checked") {
                var objs = new Object();
                objs["Answer"] = i;
                objs["Sort"] = i;
                QuestionAnswer.Add(objs);
                num = num + 1;
            }
        });
        obg["OptionList"] = QuestionOption.GetList();//选项
        obg["AnswerList"] = QuestionAnswer.GetList();//答案
        obg["AttachmentList"] = QuestionAttachments.GetList();//附件

        var number = 0;
        var lis = QuestionOption.GetList();
        $.each(lis, function (i, n) {
            $.each(lis, function (a, b) {
                if (i != a) {
                    if (n.OptionName == b.OptionName) {
                        number = number + 1;
                    }
                }
            });
        });

        if (typeName == "单选题" || typeName == "多选题") {
            if (num2 < 3) {
                dialogHelper.Error({ content: "请至少输入3个选项！", success: function () { } });
                return false;
            }
            if (number > 0) {
                number = 0;
                dialogHelper.Error({ content: "不能添加相同的选项！", success: function () { } });
                return false;
            }
            if (num == 0) {
                dialogHelper.Error({ content: "请选择正确答案！", success: function () { } });
                return false;
            }
        }
        if (typeName == "多选题") {
            if (num < 2) {
                dialogHelper.Error({ content: "多选题请至少选择两个答案！", success: function () { } });
                return false;
            }
        }

        $.ajax({
            url: "/CompetitionAdmin/Resource/AddUpdateQuestion2",
            type: "POST",
            async: false,
            dataType: "json",
            data: JSON.stringify(obg),
            contentType: "application/json",
            success: function (data) {
                dialogHelper.Success({
                    content: "保存成功！", success: function () {

                        Index = data.index;
                        $("#QuestionIdAdd1").val(data.result);
                        $("#preserve").val("编辑");

                        //更新题目选择JS缓存
                        var old_question = AlreadyQuestionsHelper.FindRecord(obg["Id"]);
                        if (old_question != null && old_question != undefined) {
                            AlreadyQuestionsHelper.Remove(obg["Id"]);
                            old_question.QuesionId = data.result;
                            AlreadyQuestionsHelper.Add(old_question);
                        }

                        var PaperId = $("#TestPapersId").val();
                        var CurQuestionCount = GetTitleCount(PaperId);
                        $("#CurQuestionCount").html(Index + 1)
                        var count = $("#CurQuestionCount").html();
                        if (count == 1) {
                            $("#upward").attr('disabled', true);
                            $("#upward").addClass('btndisable');
                            $("#down").attr('disabled', false);
                            $("#down").removeClass('btndisable');

                        } else if (count == CurQuestionCount) {
                            $("#down").attr('disabled', true);
                            $("#down").addClass('btndisable');
                            $("#upward").attr('disabled', false);
                            $("#upward").removeClass('btndisable');
                        } else {
                            $("#upward").attr('disabled', false);
                            $("#upward").removeClass('btndisable');
                            $("#down").attr('disabled', false);
                            $("#down").removeClass('btndisable');
                        }

                        if (count == 1 && count == CurQuestionCount) {
                            $("#upward").attr('disabled', true);
                            $("#upward").addClass('btndisable');
                            $("#down").attr('disabled', true);
                            $("#down").addClass('btndisable');
                        }

                        $("#AddTitle").attr('disabled', false);
                        $("#AddTitle").removeClass('btndisable');
                        //激活按钮                      

                        //禁用文本框
                        $("#Context").attr('disabled', true);
                        //选项
                        var Fobj = $("#option").find("li");
                        $.each(Fobj, function (i, n) {
                            $(n).find("input:eq(0)").attr('disabled', true);
                            $(n).find("input:eq(1)").attr('disabled', true);
                            $(n).find("span").hide();
                        });

                        $("#btnAdd").attr('disabled', true);
                        //添加选项按
                        $("#Add1").hide();
                        //附件
                        var Fobj = $("#Dappendix").find("a");
                        $.each(Fobj, function (i, n) {
                            $(n).find("b").hide();
                        });
                        //解析
                        $("#Analysis").attr('disabled', true);

                        if (Index == 0) {
                            $("#upward").attr('disabled', true);
                            $("#upward").addClass('btndisable');
                        }

                        QuestionOption.RemoveAll();//选项
                        QuestionAnswer.RemoveAll(); //答案
                        //QuestionAttachments.RemoveAll(); //附件

                        ShowQuestionsList("", "");
                    }
                });
            }, error: function (msg) {
                $(".background,.progressBar").hide();
            }
        });
    }
}

//删除选项
function DeleteSelect(obj) {
    $(obj).parent().remove();
    var optionName = $(obj).parent().find("input:eq(1)").val();

    QuestionOption.Remove(optionName);

    //$(obj).parent().remove();

    var Fobj = $("#option").find("li");
    var num = 0;
    $.each(Fobj, function (i, n) {
        var valu = Option(i);
        var typeName = $("#TypeName").find("option:selected").text();
        if (typeName == "单选题") {
            var li = $(n).find("label").html('<label><input name="dm" type="radio">' + valu);
        } else if (typeName == "多选题") {
            var li = $(n).find("label").html('<label><input name="dm" type="checkbox">' + valu);
        }

        num = num + 1;
    });
    if (num < 5) {
        $("#Add1").show();
    }
}

//添加选项
function AddOption() {
    var typeName = $("#TypeName").find("option:selected").text();
    if (typeName == "单选题") {
        var html = '<li><label><input name="dm" type="radio">D</label><input type="text" id="D" class="inputtext IsRequired IsMaxLength IsMinLength" msgname="选项" style="width: 80%;" maxlength="80" minlength="1" /><span class="close" onclick="DeleteSelect(this)"></span></li>';

    } else if (typeName == "多选题") {
        var html = '<li><label><input name="dm" type="checkbox">D</label><input type="text" id="D" class="inputtext IsRequired IsMaxLength IsMinLength" msgname="选项" style="width: 80%;" maxlength="80" minlength="1" /><span class="close" onclick="DeleteSelect(this)"></span></li>';
    }


    var Fobj = $("#option").find("li");
    if (Fobj.length == 0) {
        $("#Add1").before(html);
    } else {
        $("#Add1").prevAll("li:eq(0)").after(html);
    }

    var Fobj = $("#option").find("li");
    var num = 0;
    $.each(Fobj, function (i, n) {
        var valu = Option(i);
        if (typeName == "单选题") {
            var li = $(n).find("label").html('<label><input name="dm" type="radio">' + valu);
        } else if (typeName == "多选题") {
            var li = $(n).find("label").html('<label><input name="dm" type="checkbox">' + valu);
        }

        num = num + 1;
    });
    if (num >= 5) {
        $("#Add1").hide();
    }
}

//文件上传
function FileUpload() {
    var chapterId = $.getUrlParam("TheoryChapterId");

    var resourceName = "";
    var i = $("#Dappendix").find("a").length;
    resourceName = "附件" + Num((i + 1));



    var num = UploaDappendix(resourceName);
    if (num == 3) {
        dialogHelper.Error({
            content: "本试题资源数量超过最大限制3个！"
        })
        return;
    }
    if (num == 5) {
        dialogHelper.Error({
            content: "已存在该资源名称，请重新命名"
        })
        return;
    }
    if (chapterId != "" && chapterId != 0) {
        var filePath = $("#filePath").val();
        if (filePath == "") {
            dialogHelper.Error({ content: "请选择要上传的文件！" });
            return false;
        }

        //文件上传
        $.ajaxFileUpload({
            url: "/CompetitionAdmin/Resource/UploadFile",
            secureuri: false,
            fileElementId: 'HFilePath',
            dataType: 'json',
            data: {
                resourceName: resourceName
            },
            success: function (data, status) {
                $(".window-mask,.progressBar").hide();
                if (data.result) {

                    var obj = new Object();
                    obj["Name"] = $("#filePath").val();
                    obj["Url"] = $("#filePath").val();
                    obj["FileUrl"] = data.error;
                    var index = QuestionAttachments.Add(obj);
                    if (index >= 0) {
                        dialogHelper.Error({
                            content: "不能添加相同的附件！"
                        })
                        $("#filePath").val("");
                        $("#HFilePath").val("");
                        return;
                    }

                    dialogHelper.Success({
                        content: "资源上传成功",
                        success: function () {
                            //生成附件
                            createOptions();

                            $("#HFilePath").val("");
                            $("#txtResourceName").val("");
                            $("#filePath").val("");
                            $("#txtResourceName").val("");
                            $("#filePath").val("");

                            ShowQuestionsList("", "");
                            dialogHelper.Close("addpop");
                        }
                    });
                }
                else {
                    if (data.error != "" && data.error != undefined) {
                        dialogHelper.Error({
                            content: data.error,
                            success: function () {
                                $("#filePath").val("");
                                $("#HFilePath").val("");
                            }
                        });
                    }
                    else {
                        dialogHelper.Error({
                            content: "您上传的资源超过500KB，请重新上传！",
                            success: function () {
                                $("#filePath").val("");
                                $("#HFilePath").val("");
                            }
                        });
                    }
                }
            },
            error: function (data, status, e) {
                dialogHelper.Error({ context: "上传异常" });
            }
        });

    }
    else { dialogHelper.Error({ context: "系统出错，没有选中章节" }); }

}

function UploaDappendix(valu) {
    var list = $("#Dappendix").find("a");
    var num = 0;
    $.each(list, function (i, n) {
        num = num + 1;
        var a = $(n).find("span").attr("tag");
        if (a == valu) {
            num = 5;
        }
    });

    return num;
}

//生成附件
function createOptions() {
    var list = QuestionAttachments.GetList();
    var html = "";
    $.each(list, function (i, n) {

        html += '<a href="#">';
        html += '<img src="/Content/images/text-icon.png">';
        html += '<span tag="' + n.Name + '" title="' + n.Name + '">附件' + Num((i + 1)) + '</span>';
        html += '<b class="close" tag="' + n.Name + '" onclick="DeleteAttachments(this)"></b>';
        html += '</a>';
    });
    $("#Dappendix").html(html);
}

function Num(valu) {
    switch (valu) {
        case 1:
            return "一";
            break;
        case 2:
            return "二";
            break;
        case 3:
            return "三";
            break;
    };
}

//生成选项
function CreateSelect() {
    var list = QuestionOption.GetList();
    var html = "";
    var ID = "";
    var num = 0;
    var typeName = $("#TypeName").find("option:selected").text();
    $.each(list, function (i, n) {
        switch (i) {
            case 0:
                ID = "A";
                break;
            case 1:
                ID = "B";
                break;
            case 2:
                ID = "C";
                break;
            case 3:
                ID = "D";
                break;
            default:
                ID = "E";
                break;
        }
        var OptionName = n.OptionName;
        OptionName = OptionName.replace(/[<>&"]/g, function (c) { return { '<': '&lt;', '>': '&gt;', '&': '&amp;', '"': '&quot;', '"': '&quot;', "'": '&prime;', "'": '&prime;' }[c]; });

        if (typeName == "单选题") {
            html += '<li><label><input name="dm" type="radio" id="' + ID + '">' + ID + '</label><input type="text" id="text' + ID + '" value="' + OptionName + '" class="inputtext IsRequired IsMaxLength IsMinLength" msgname="选项" style="width: 80%;" maxlength="80" minlength="1" /><span class="close" onclick="DeleteSelect(this)"></span></li>';
            num = num + 1;
        } else if (typeName == "多选题") {
            html += '<li><label><input name="dm" type="checkbox" id="' + ID + '">' + ID + '</label><input type="text" id="text' + ID + '" value="' + OptionName + '" class="inputtext IsRequired IsMaxLength IsMinLength" msgname="选项" style="width: 80%;" maxlength="80" minlength="1" /><span class="close" onclick="DeleteSelect(this)"></span></li>';
            num = num + 1;
        }
    });
    if (typeName == "判断题") {
        html += '<li><label class="mr20"><input name="dm" type="radio" id="A" value="正确" >正确</label><label><input name="dm" id="B" value="错误" type="radio">错误</label></li>';
    }
    if (typeName == "单选题" || typeName == "多选题") {
        //if (num <= 4) {
        html += '<span class="add-sib" id="Add1" onclick="AddOption()"></span>';
        //}
    }
    $("#option").html(html);


    if (num > 4) {
        $("#Add1").hide();
    }

    if ($("#Context").attr("disabled") == "disabled") {
        $("#Add1").hide();
    }
}

//删除上传文件
function DeleteAttachments(valu) {
    var url = $(valu).attr("tag");
    QuestionAttachments.Remove(url);
    $(valu).parent().remove();

    createOptions();
}

//返回按钮
function ComeBack() {
    if ($("#Context").attr("disabled") != "disabled") {
        dialogHelper.Confirm({
            content: "当前试题未保存，是否继续退出？", success: function () {
                QuestionAttachments.RemoveAll(); //附件
                dialogHelper.Close("popQuestionBankAdd");
                ShowQuestionsList("", "");
            },
            cancle: function () {

            }
        });
    } else {
        QuestionAttachments.RemoveAll(); //附件
        dialogHelper.Close("popQuestionBankAdd");
        ShowQuestionsList("", "");
    }

}

//选项
function Option(valu) {
    var option = "";
    switch (valu) {
        case 0:
            option = "A";
            break;
        case 1:
            option = "B";
            break;
        case 2:
            option = "C";
            break;
        case 3:
            option = "D";
            break;
        case 4:
            option = "E";
            break;
    }
    return option;
}

//下拉框的改变事件
function SelectChange(valu) {

    //类型Id
    var typeid = 0;
    //获得界面题型名称
    var typeName = $(valu).find("option:selected").text();
    //章节Id
    var TheoryChapterId = $.getUrlParam("TheoryChapterId");
    //章节名称
    var ChapterName = unescape($.getUrlParam("ChapterName"));

    //var CertificationId = $.getUrlParam("CertificationId");
    //题目数量
    var CurQuestionCount = 0;
    $.ajax({
        url: "/CompetitionAdmin/Resource/GetQuestionTypeList",
        type: "POST",
        async: false,
        data: { charpterId: TheoryChapterId },
        success: function (data) {
            var html = "";
            $(data).each(function (index, dom) {
                if (typeName == dom.TypeName) {
                    typeid = dom.Id;
                    CurQuestionCount = dom.CurQuestionCount;
                }
            });

        }
    });

    switch (typeName) {
        case "单选题":
            window.location.href = "/CompetitionAdmin/Resource/QuestionDetail1?TheoryChapterId=" + TheoryChapterId + "&typeid=" + typeid + "&ChapterName=" + escape(ChapterName) + "&CurQuestionCount=" + CurQuestionCount + "";//  

            break;
        case "多选题":
            window.location.href = "/CompetitionAdmin/Resource/QuestionDetail2?TheoryChapterId=" + TheoryChapterId + "&typeid=" + typeid + "&ChapterName=" + escape(ChapterName) + "&CurQuestionCount=" + CurQuestionCount + "";//  
            break;
        case "判断题":
            window.location.href = "/CompetitionAdmin/Resource/QuestionDetail3?TheoryChapterId=" + TheoryChapterId + "&typeid=" + typeid + "&ChapterName=" + escape(ChapterName) + "&CurQuestionCount=" + CurQuestionCount + "";//  
            break;
        default:
            break;
    }
}

// 获取题号
function GetTitleCount(PaperId) {
    var count = 0;
    $.ajax({
        url: "/CompetitionAdmin/Resource/GetTitleCount2",
        type: "POST",
        async: false,
        dataType: "json",
        data: { PaperId: PaperId },
        success: function (data) {
            count = data.Count;
        }
    });
    return count;
}

//新增按钮
function AddTitleClick() {
    //清除集合
    QuestionOption.RemoveAll();
    QuestionAnswer.RemoveAll();
    QuestionAttachments.RemoveAll();

    $("#Id").val("0");
    //题号加1       
    var StructTypeName = $("#TypeName").find("option:selected").text();
    var typeid = $.getUrlParam("typeid");
    var count = GetTitleCount(StructTypeName, typeid);
    $("#CurQuestionCount").html(count + 1);

    //清空文本
    $("#Context").val("");
    $("#Analysis").val("");

    $("#Context").attr('disabled', false);
    $("#Analysis").attr('disabled', false);

    $("#preserve").val("保存");
    //选项
    var Fobj = $("#option").find("li");
    $.each(Fobj, function (i, n) {

        $(n).find("input:eq(0)").attr('disabled', false);
        $(n).find("input:eq(1)").attr('disabled', false);
        $(n).find("span").show();

        $(n).find("input:eq(0)").attr("checked", false);
        $(n).find("input:eq(1)").val("");
    });

    //附件
    var Fobj = $("#Dappendix").find("a");
    $.each(Fobj, function (i, n) {
        $(n).remove();
    });

    $("#btnAdd").attr('disabled', false);
    $("#Add").show();

    //屏蔽按钮
    $("#AddTitle").attr('disabled', true);
    $("#AddTitle").addClass('btndisable');

    $("#upward").attr('disabled', true);
    $("#upward").addClass('btndisable');

    $("#down").attr('disabled', true);
    $("#down").addClass('btndisable');
}

//上一题
function Thelast() {
    //章节Id
    var typeid = $.getUrlParam("typeid");
    if (typeid == null) {
        typeid = 0;
    }
    var PaperId = $("#TestPapersId").val();// $.getUrlParam("PapersId");

    Index = Index - 1;
    if (Index < 0) {
        Index = 0;
    }
    //清除集合
    QuestionOption.RemoveAll();
    QuestionAnswer.RemoveAll();
    QuestionAttachments.RemoveAll();
    $.ajax({
        url: "/CompetitionAdmin/Resource/GetQuestionId",
        async: false,
        type: "POST",
        data: {
            Index: Index,
            CharpterID: typeid,
            PaperId: PaperId
        },
        success: function (data) {
            var mode = data.model;

            $("#QuestionIdAdd1").val(mode.Id);
            var count = Number($("#CurQuestionCount").html());
            if (count - 1 == 1) {
                Index = 0;
                $("#upward").attr("disabled", true);
                $("#upward").addClass('btndisable');
            } else {
                $("#upward").attr("disabled", false);
                $("#upward").removeClass('btndisable');
            }
            if ($("#down").attr("disabled") == "disabled") {
                $("#down").attr("disabled", false);
                $("#down").removeClass('btndisable');
            }

            if (mode != undefined) {
                var html = "<option  text='" + mode.StrStructType + "'>" + mode.StrStructType + "</option>";
                $("#TypeName").html(html);
                if (mode.StrStructType != "单选题" && mode.StrStructType != "多选题" && mode.StrStructType != "判断题") {
                    $("#xuanxaing1").hide();
                } else {
                    $("#xuanxaing1").show();
                }
                if (mode.Context != "") {
                    var count = Number($("#CurQuestionCount").html());
                    if (count > 1) {
                        $("#CurQuestionCount").html(count - 1);
                    }


                    $("#Context").val(mode.Context);
                    $("#Analysis").val(mode.Analysis);

                    $("#option").find("li").remove();
                    $("#Id").val(mode.Id);
                }
                //选项
                if (mode.OptionList != null) {
                    var html = "";
                    $.each(mode.OptionList, function (i, n) {
                        QuestionOption.Add(n);

                    });
                }
                //附件
                if (mode.AttachmentList != null) {
                    $.each(mode.AttachmentList, function (i1, n1) {
                        QuestionAttachments.Add(n1);
                    });
                }

                createOptions();
                CreateSelect();
                //答案
                if (mode.AnswerList != null) {
                    $.each(mode.AnswerList, function (i2, n2) {
                        QuestionAnswer.Add(n2);
                        switch (n2.Answer) {
                            case 0:
                                $("#A").attr("checked", true);
                                break;
                            case 1:
                                $("#B").attr("checked", true);
                                break;
                            case 2:
                                $("#C").attr("checked", true);
                                break;
                            case 3:
                                $("#D").attr("checked", true);
                                break;
                            default:
                                $("#option").find("li:eq(4)").find("input").attr("checked", true);
                                break;
                        }
                    });
                }
            }
        }
    });

    jinyong()
}

var styles = 0;
//下一题
function Thedown() {
    //章节Id
    var typeid = $.getUrlParam("typeid");
    if (typeid == null) {
        typeid = 0;
    }
    var PaperId = $("#TestPapersId").val();
    //清除集合
    QuestionOption.RemoveAll();
    QuestionAnswer.RemoveAll();
    QuestionAttachments.RemoveAll();
    var sty = Index;
    if (Index < 0) {
        Index = 0;
    }
    Index = Index + 1;
    if (Index > 0) {
        $.ajax({
            url: "/CompetitionAdmin/Resource/GetQuestionId",
            async: false,
            type: "POST",
            data: {
                Index: Index,
                CharpterID: typeid,
                PaperId: PaperId
            },
            success: function (data) {
                var mode = data.model;
                $("#QuestionIdAdd1").val(mode.Id);
                // styles = data.style;

                var PaperId = $("#TestPapersId").val();
                var CurQuestionCount = GetTitleCount(PaperId);
                var count = Number($("#CurQuestionCount").html());
                if (CurQuestionCount == count + 1) {
                    $("#down").attr("disabled", true);
                    $("#down").addClass('btndisable');

                    Index = CurQuestionCount - 1;
                } else {
                    $("#down").attr("disabled", false);
                    $("#down").removeClass('btndisable');
                }
                if (mode != undefined) {
                    var html = "<option  text='" + mode.StrStructType + "'>" + mode.StrStructType + "</option>";
                    $("#TypeName").html(html);
                    if (mode.StrStructType != "单选题" && mode.StrStructType != "多选题" && mode.StrStructType != "判断题") {

                        $("#xuanxaing1").hide();
                    } else {

                        $("#xuanxaing1").show();
                    }
                    if (mode.Context != "") {
                        var count = Number($("#CurQuestionCount").html());
                        $("#CurQuestionCount").html(count + 1);

                        $("#Context").val(mode.Context);
                        $("#Analysis").val(mode.Analysis);

                        $("#option").find("li").remove();
                        $("#Id").val(mode.Id);
                    }
                    //选项
                    if (mode.OptionList != null) {
                        var html = "";
                        $.each(mode.OptionList, function (i, n) {
                            QuestionOption.Add(n);

                        });
                    }
                    //附件
                    if (mode.AttachmentList != null) {
                        $.each(mode.AttachmentList, function (i1, n1) {
                            QuestionAttachments.Add(n1);
                        });
                    }

                    createOptions();
                    CreateSelect();
                    //答案
                    if (mode.AnswerList != null) {
                        $.each(mode.AnswerList, function (i2, n2) {
                            QuestionAnswer.Add(n2);
                            switch (n2.Answer) {
                                case 0:
                                    $("#A").attr("checked", true);
                                    break;
                                case 1:
                                    $("#B").attr("checked", true);
                                    break;
                                case 2:
                                    $("#C").attr("checked", true);
                                    break;
                                case 3:
                                    $("#D").attr("checked", true);
                                    break;
                                default:
                                    $("#option").find("li:eq(4)").find("input").attr("checked", true);
                                    break;
                            }
                        });
                    }
                    if ($("#upward").attr("disabled") == "disabled") {
                        $("#upward").attr("disabled", false);
                        $("#upward").removeClass('btndisable');
                    }
                }
            }
        });
        jinyong();

    }
}

//禁用文本
function jinyong() {
    //禁用文本框
    $("#Context").attr('disabled', true);
    //选项
    var Fobj = $("#option").find("li");
    $.each(Fobj, function (i, n) {
        $(n).find("input:eq(0)").attr('disabled', true);
        $(n).find("input:eq(1)").attr('disabled', true);
        $(n).find("span").hide();
    });

    $("#btnAdd").attr('disabled', true);
    //添加选项按
    $("#Add").hide();
    //附件
    var Fobj = $("#Dappendix").find("a");
    $.each(Fobj, function (i, n) {
        $(n).find("b").hide();
    });
    //解析
    $("#Analysis").attr('disabled', true);
}

// 查看是否是内置题
function CheckSource(Id) {
    var bo = false;
    $.ajax({
        url: "/CompetitionAdmin/Resource/CheckQuestionSource",
        async: false,
        type: "POST",
        data: {
            Id: Id
        },
        success: function (data) {
            if (data) {
                bo = true;
            }
        }
    });
    return bo;
}
