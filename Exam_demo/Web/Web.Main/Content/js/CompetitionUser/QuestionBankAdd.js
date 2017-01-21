
var QuestionOption = new arrayHelper("Sort");//选项
var QuestionAnswer = new arrayHelper("QuestionId");//答案
var QuestionAttachments = new arrayHelper("Name");//附件
var CurQuestionCount = 0;
$(function () {
    var selOpE = $(".selOpE");
    if (selOpE != null){
        DeleteSelect(selOpE);
    }
    // 章节Id
    var TheoryChapterId = $.getUrlParam("TheoryChapterId"); // 章节Id
    var typeid = $.getUrlParam("typeid");                   // 章节Id（细分）
    var ChapterName = unescape($.getUrlParam("ChapterName"));       
    // 查询章节对应题型
    SelectTitle(TheoryChapterId, ChapterName, typeid);

    // 获得题号
    var count = GetTitleCount(typeid);
    CurQuestionCount = count;

    // 判断是编辑模式还是添加模式
    var edit = $.getUrlParam("edit");
    if (edit==1) {
        $("#CurQuestionCount").html(CurQuestionCount);
    } else {
        $("#CurQuestionCount").html(CurQuestionCount + 1);
    }

    // 获取题干
    var QuestionId = Number($.getUrlParam("QuestionId"));
    if (QuestionId != null && QuestionId!=0) {
        LoadTitle(QuestionId);
    }
    // 文件选择
    $("#FileChoose").click(function () {
        $("#HFilePath").click();
    });

    // 将file的值赋值给input
    $("#HFilePath").live("change", function () {
        var filepath = $(this).val();
        $("#filePath").val(filepath);
    });

    // 上传附件按钮
    $("#btnAdd").click(function () {
        dialogHelper.Show("addpop", 756);            
        $("#txtResourceName").val("");
        $("#filePath").val("");
    });

    // 附件确定按钮
    $("#FileUpload").click(function () {
        FileUpload(); 
    });

    // 附件上传取消按钮
    $("#addclose").click(function () {
        dialogHelper.Close("addpop");
        $("#txtResourceName").val("");
        $("#filePath").val("");
        $("#HFilePath").val("");
        var CurQuestionCounts = $("#CurQuestionCount").html();
        QuestionAttachments.Remove(CurQuestionCounts);
    });

    // 保存按钮
    $("#preserve").click(function () {
        // 章节Id
        var TheoryChapterId = $.getUrlParam("TheoryChapterId");
        var typeid = $.getUrlParam("typeid");
        AddTitle(TheoryChapterId, typeid);
    });

    // 屏蔽按钮
    $("#AddTitle").attr('disabled',true); 
    $("#AddTitle").addClass('btndisable');

    $("#upward").attr('disabled', true);
    $("#upward").addClass('btndisable');

    $("#down").attr('disabled', true);
    $("#down").addClass('btndisable');       
});

// 加载试题
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
            QuestionAnswer.RemoveAll();//答案
            QuestionAttachments.RemoveAll();//附件

            var mode = data.TE;
            if (mode.Context != "") {
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
            $("#CurQuestionCount").html($.getUrlParam("count"));
        }
    });
}

var type = "";
// 查询章节对应题型
function SelectTitle(TheoryChapterId, ChapterName, typeid) {
    var a = decodeURI(ChapterName);
    $("#charpterName").html(a);

    $.ajax({
        url: "/CompetitionAdmin/Resource/GetQuestionTypeList",
        async: false,
        type: "POST",
        data: {
            charpterId: TheoryChapterId
        },
        success: function (data) {
            var html = "";               
            var type2=0;
            $.each(data, function (e, f) {
                if (typeid == f.Id) {
                    type = f.TypeName
                    type2 = f.Id 
                }
                html += "<option tag='" + f.Id + "' text='" + f.TypeName + "'>" + f.TypeName + "</option>";
            });
            $("#TypeName").html(html);
            $("#TypeName option[text='" + type + "']").attr("selected", true);
        }
    });
}

var Index = 0;
// 编辑/保存题目
function AddTitle(TheoryChapterId, typeid) {
    if ($("#preserve").val() == "编辑") {
        var bo = CheckSource($("#Id").val());
        if (bo) {
            dialogHelper.Error({ content: "内置题目不能进行编辑！", success: function () { } });
            return false;
        }

        $("#preserve").val("保存");
        // 启用文本框
        $("#Context").attr('disabled', false);
        // 选项
        var Fobj = $("#option").find("li");
        $.each(Fobj, function (i, n) {
            $(n).find("input:eq(0)").attr('disabled', false);
            $(n).find("input:eq(1)").attr('disabled', false);
            $(n).find("span").show();
        });

        $("#btnAdd").attr('disabled', false);
        // 添加选项按钮
        if (Fobj.length <= 4) {
            $("#Add").show();
        } else {
            $("#Add").hide();
        }

        // 附件
        var Fobj = $("#Dappendix").find("a");
        $.each(Fobj, function (i, n) {
            $(n).find("b").show();
        });

        // 解析
        $("#Analysis").attr('disabled', false);

        $("#upward").attr('disabled', true);
        $("#upward").addClass('btndisable');

        $("#down").attr('disabled', true);
        $("#down").addClass('btndisable');

        $("#AddTitle").attr('disabled', true);
        $("#AddTitle").addClass('btndisable');
    }
    else {
        if (!VerificationHelper.checkFrom("Questions")) {
            return false;
        }
        QuestionOption.RemoveAll(); // 选项
        QuestionAnswer.RemoveAll(); // 答案
        //添加试题
        var obg = new Object();
        obg["Id"] = $("#Id").val(); // Id
        obg["Context"] = $("#Context").val(); // 题干              
        obg["StructType"] = 1; // 题目类型（枚举）
        obg["CharpterID"] = typeid; // 章节Id(细分)      
        obg["Analysis"] = $("#Analysis").val(); // 解析
        obg["Status"] = 1; // 状态 开启

        // 添加选项和答案
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
            if ($(n).find("input:eq(0)").attr("checked") == "checked") {
                var objs = new Object();
                objs["Answer"] = i;
                objs["Sort"] = i;
                QuestionAnswer.Add(objs);
                num = num + 1;
            }
        });

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

        if (num2 < 3) {
            num2 = 0;
            dialogHelper.Error({ content: "请至少输入3个选项！", success: function () { } });
            return false;
        }
        if (num == 0) {
            dialogHelper.Error({ content: "请选择正确答案！", success: function () { } });
            return false;
        }
        if (number > 0) {
            number = 0;
            dialogHelper.Error({ content: "不能添加相同的选项！", success: function () { } });
            return false;
        }

        obg["OptionList"] = QuestionOption.GetList(); // 选项
        obg["AnswerList"] = QuestionAnswer.GetList(); // 答案
        obg["AttachmentList"] = QuestionAttachments.GetList(); // 附件

        $.ajax({
            url: "/CompetitionAdmin/Resource/AddUpdateQuestion",
            type: "POST",
            async: false,
            dataType: "json",
            data: JSON.stringify(obg),
            contentType: "application/json",
            success: function (data) {
                dialogHelper.Success({
                    content: "保存成功！", success: function () {
                        Index = data.index;
                        $("#Id").val(data.result);
                        $("#preserve").val("编辑");
                        //激活按钮
                        var count = $("#CurQuestionCount").html();
                        var StructTypeName = $("#TypeName").find("option:selected").text();
                        var typeid = $.getUrlParam("typeid");
                        var CurQuestionCount = GetTitleCount(typeid);
                        $("#CurQuestionCount").html(CurQuestionCount)
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
                        }
                        else {
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

                        QuestionOption.RemoveAll();//选项
                        QuestionAnswer.RemoveAll();//答案
                        //QuestionAttachments.RemoveAll();//附件
                    }
                });
            }, error: function (msg) {

                $(".background,.progressBar").hide();

            }
        });
    }
}

// 删除选项
function DeleteSelect(obj) {
   
    var optionName = $(obj).parent().find("input:eq(1)").val();
    QuestionOption.Remove(optionName);

   $(obj).parent().remove();

    var Fobj = $("#option").find("li");
    var num = 0;
    $.each(Fobj, function (i, n) {
        var valu = Option(i);
        var li = $(n).find("label").html('<input name="dm" id="' + valu + '" type="radio">' + valu);
        var iuput = $(n).find("input:eq(1)").attr("id", "text" + valu);
        num = num + 1;
    });
    if (num < 5) {
        $("#Add").show();
    }
}

// 添加选项
function AddOption() {       
    var Fobj = $("#option").find("li");
   
    var html = '';
    if (Fobj.length == 0) {
        html = '<li><label><input name="dm" id="A" type="radio">D</label><input type="text" style="overflow:hidden" id="textA" class="inputtext IsRequired IsMaxLength IsMinLength" msgname="选项" maxlength="80" minlength="1" /><span class="close" onclick="DeleteSelect(this)"></span></li>';
    } else if (Fobj.length == 1) {
        html = '<li><label><input name="dm" id="B" type="radio">D</label><input type="text" style="overflow:hidden" id="textB" class="inputtext IsRequired IsMaxLength IsMinLength" msgname="选项" maxlength="80" minlength="1" /><span class="close" onclick="DeleteSelect(this)"></span></li>';
    } else if (Fobj.length == 2) {
        html = '<li><label><input name="dm" id="C" type="radio">D</label><input type="text" style="overflow:hidden" id="textC" class="inputtext IsRequired IsMaxLength IsMinLength" msgname="选项" maxlength="80" minlength="1" /><span class="close" onclick="DeleteSelect(this)"></span></li>';
    } else if (Fobj.length == 3) {
        html = '<li><label><input name="dm" id="D" type="radio">D</label><input type="text" style="overflow:hidden" id="textD" class="inputtext IsRequired IsMaxLength IsMinLength" msgname="选项" maxlength="80" minlength="1" /><span class="close" onclick="DeleteSelect(this)"></span></li>';
    } else if (Fobj.length == 4) {
        html = '<li><label><input name="dm" id="E" type="radio">D</label><input type="text" style="overflow:hidden" id="textE" class="inputtext IsRequired IsMaxLength IsMinLength" msgname="选项" maxlength="80" minlength="1" /><span class="close" onclick="DeleteSelect(this)"></span></li>';
    }
   
    if (Fobj.length==0) {
        $("#Add").before(html);
    } else {
         $("#Add").prevAll("li:eq(0)").after(html);  
    }

    var Fobj = $("#option").find("li");
    var num = 0;
    $.each(Fobj, function (i, n) {
        var valu = Option(i);
        var li = $(n).find("label").html('<input name="dm" id="' + valu + '" type="radio">' + valu);
        var iuput = $(n).find("input:eq(1)").attr("id", "text" + valu);
        num = num + 1;
    });
    if(num>=5){
        $("#Add").hide();

    }
}

// 附件上传
function FileUpload() {
    var chapterId = $.getUrlParam("TheoryChapterId");
   
    var resourceName = "";
    var i = $("#Dappendix").find("a").length;
    resourceName = "附件" + Num((i + 1));

    var num= UploaDappendix(resourceName);
    if (num==3) {
        dialogHelper.Error({
            content: "本试题资源数量超过最大限制3个！"
        })
        $("#filePath").val("");
        $("#HFilePath").val("");
        return;
    }
   
    if (chapterId != "" && chapterId != 0) {
        var filePath = $("#filePath").val();
        if (filePath == "") {
            dialogHelper.Error({ content: "请选择要上传的文件！" });
            $("#filePath").val("");
            $("#HFilePath").val("");
            return false;
        }                   

        // 文件上传
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
                            var a = QuestionAttachments.GetList();

                            $("#fujian").find("a").remove();
                            //生成附件
                            createOptions();
                          
                            $("#txtResourceName").val("");
                            $("#filePath").val("");
                          
                            $("#HFilePath").val("");
                            $("#txtResourceName").val("");
                            $("#filePath").val("");
                            dialogHelper.Close("addpop");
                            $("#resourceList").empty();
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
                $("#filePath").val("");
                $("#HFilePath").val("");
            }
        });

    }
    else { dialogHelper.Error({ context: "系统出错，没有选中章节" }); }

}

// 返回附件数目
function UploaDappendix(valu) {
    var list = $("#Dappendix").find("a");
    var num = 0;
    $.each(list, function (i,n) {
        num = num + 1;             
    });
    return num;
}

// 生成附件元素
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

// 生成附件数目文字
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

// 生成选项
function CreateSelect() {
    var list = QuestionOption.GetList();
    var html = "";
    var ID = "";
    var nums = 0;
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
        OptionName=OptionName.replace(/[<>&"]/g, function (c) { return { '<': '&lt;', '>': '&gt;', '&': '&amp;', '"': '&quot;', '"': '&quot;', "'": '&prime;', "'": '&prime;' }[c]; });
        html += '<li><label><input name="dm" type="radio" id="'+ID+'">'+ID+'</label><input type="text" value="'+OptionName+'" class="inputtext IsRequired IsMaxLength IsMinLength" msgname="选项" maxlength="80" minlength="1" /><span class="close" onclick="DeleteSelect(this)"></span></li>';
        nums = nums + 1;
    });         
    html += '<span class="add-sib" id="Add" onclick="AddOption()"></span>';

    $("#option").html(html);
    if (nums>=5) {
        $("#Add").hide();
    } else {
        $("#Add").show();
    }
    
}

// 删除上传文件
function DeleteAttachments(valu) {
    var url = $(valu).attr("tag");   
    QuestionAttachments.Remove(url);
  
    $(valu).parent().remove();

    createOptions();
}

// 返回按钮
function ComeBack() {
    if ($("#Context").attr("disabled") != "disabled") {
        dialogHelper.Confirm({
            content: "当前试题未保存，是否继续退出？", success: function () {
                QuestionAttachments.RemoveAll();//附件
                var TheoryChapterId = $.getUrlParam("TheoryChapterId");
                var typeid = $.getUrlParam("typeid");
                var ChapterName = unescape($.getUrlParam("ChapterName"));
                //var CurQuestionCount = $.getUrlParam("CurQuestionCount");
                window.location.href = "/CompetitionAdmin/Resource/QuestionList?TheoryChapterId=" + TheoryChapterId + "&typeid=" + typeid + "&ChapterName=" + escape(ChapterName) + "";
            },
            cancle: function () {
            }
        });
    } else {
        QuestionAttachments.RemoveAll();//附件
        var TheoryChapterId = $.getUrlParam("TheoryChapterId");
        var typeid = $.getUrlParam("typeid");
        var ChapterName = unescape($.getUrlParam("ChapterName"));
        //var CurQuestionCount = $.getUrlParam("CurQuestionCount");
        window.location.href = "/CompetitionAdmin/Resource/QuestionList?TheoryChapterId=" + TheoryChapterId + "&typeid=" + typeid + "&ChapterName=" + escape(ChapterName) + "";
    }
}

// 选项
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

// 下拉框的改变事件
function SelectChange(valu) {
    // 类型Id
    var typeid = 0;
    // 获得界面题型名称
    var typeName = $(valu).find("option:selected").text();
    // 章节Id
    var TheoryChapterId = $.getUrlParam("TheoryChapterId");       
    // 章节名称
    var ChapterName = unescape($.getUrlParam("ChapterName"));
    // 题目数量
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
                    CurQuestionCount=dom.CurQuestionCount;
                }                                                            
            });
               
        }
    });                             

    switch (typeName) {
        case "单选题":
            window.location.href = "/CompetitionAdmin/Resource/QuestionDetail1?TheoryChapterId=" + TheoryChapterId + "&typeid=" + typeid + "&ChapterName=" + escape(ChapterName);
            break;
        case "多选题":
            window.location.href = "/CompetitionAdmin/Resource/QuestionDetail2?TheoryChapterId=" + TheoryChapterId + "&typeid=" + typeid + "&ChapterName=" + escape(ChapterName);
            break;
        case "判断题":
            window.location.href = "/CompetitionAdmin/Resource/QuestionDetail3?TheoryChapterId=" + TheoryChapterId + "&typeid=" + typeid + "&ChapterName=" + escape(ChapterName);
            break;
        default:
            break;
    }
}

// 获取题号
function GetTitleCount(CharpterID) {
    var count = 0;
    $.ajax({
        url: "/CompetitionAdmin/Resource/GetTitleCount",
        type: "POST",
        async: false,
        dataType: "json",
        data: { CharpterID: CharpterID },
        success: function (data) {
            count = data.Count;
        }
    });
    return count;
}

// 新增按钮
function AddTitleClick() {
    // 清除集合
    QuestionOption.RemoveAll();
    QuestionAnswer.RemoveAll();
    QuestionAttachments.RemoveAll();

    $("#Id").val("0");
    // 题号加1       
    var StructTypeName = $("#TypeName").find("option:selected").text();
    var typeid = $.getUrlParam("typeid");
    var count = GetTitleCount(typeid);
    $("#CurQuestionCount").html(count + 1);

    // 清空文本
    $("#Context").val("");
    $("#Analysis").val("");

    $("#Context").attr('disabled', false);
    $("#Analysis").attr('disabled', false);

    $("#preserve").val("保存");
    // 选项
    var Fobj = $("#option").find("li");
    $.each(Fobj, function (i, n) {
        if (i<=3) {
            $(n).find("input:eq(0)").attr('disabled', false);
            $(n).find("input:eq(1)").attr('disabled', false);
            $(n).find("span").show();

            $(n).find("input:eq(0)").attr("checked", false);
            $(n).find("input:eq(1)").val("");
        } else if(i==4) {
            $(n).remove();
        } else {
            $(n).hide();
        }
    });

    // 附件
    var Fobj = $("#Dappendix").find("a");
    $.each(Fobj, function (i, n) {
        $(n).remove();
    });

    $("#btnAdd").attr('disabled', false);
    $("#Add").show();

    // 屏蔽按钮
    $("#AddTitle").attr('disabled', true);
    $("#AddTitle").addClass('btndisable');

    $("#upward").attr('disabled', true);
    $("#upward").addClass('btndisable');

    $("#down").attr('disabled', true);
    $("#down").addClass('btndisable');
}

// 上一题
function Thelast() {
    //章节Id
    var typeid = $.getUrlParam("typeid");
    var PaperId = $.getUrlParam("PaperId");
    if (PaperId==null) {
        PaperId = "0";
    }
    Index = Index-1;
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
                         
            if (mode != undefined) {
                if (mode.Context != "") {
                    var count = Number($("#CurQuestionCount").html());
                    if (count>1) {
                        $("#CurQuestionCount").html(count - 1);
                    }
                        
                    var count = Number($("#CurQuestionCount").html());
                    if (count == 1) {
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
// 下一题
function Thedown() {
    //章节Id
    var typeid = $.getUrlParam("typeid");
    var PaperId = $.getUrlParam("PaperId");
    if (PaperId == null) {
        PaperId = "0";
    }
    //清除集合
    QuestionOption.RemoveAll();
    QuestionAnswer.RemoveAll();
    QuestionAttachments.RemoveAll();
    var sty = Index;
    if (Index<0) {
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
                styles = data.style;
               
                if (mode != undefined) {
                    if (mode.Context != "") {
                        var count = Number($("#CurQuestionCount").html());
                        $("#CurQuestionCount").html(count + 1);

                        var StructTypeName = $("#TypeName").find("option:selected").text();
                        var typeid = $.getUrlParam("typeid");
                        var CurQuestionCount = GetTitleCount(typeid);
                        if (CurQuestionCount == count+1) {
                            $("#down").attr("disabled", true);
                            $("#down").addClass('btndisable');
                            Index = CurQuestionCount-1;
                        } else {
                            $("#down").attr("disabled", false);
                            $("#down").removeClass('btndisable');
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

// 禁用文本
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
