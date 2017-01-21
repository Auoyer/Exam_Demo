//*******************************
//考生端-理论成绩
//*******************************

var answer_cache = new Array();
var time = 0;


$(function () {

    //UserTimeHelper.Init();
    $("#fujian2").show();
    //获取列表
    var Id = $.getUrlParam("Id");
    //是否为认证考试
    var LibraryId = $.getUrlParam("LibraryId");
    //已评分查看
    var Type = $.getUrlParam("type");
    if (Type == 1) {
        LookOver(Id);
        //readonly = "readonly"
        $("#TextContent2").attr('readonly', true); //TextContent2
        $("#zaida").html("当前");

        $("#wrong-answer").html("错误");
        $("#right-answer").html("正确");
    } else {
        GetPaperDetailList(Id, LibraryId)
        GetQuestion(Id, LibraryId);
        $("#tijiao").show();
        $("#tiankongAnswer2").hide();
        $("#tab_1").hide();
        $("#tab_2").hide();
    }

    var charpterID = $.getUrlParam("charpterID");

    //上一题
    $(".as2").unbind("click").click(function () {
        var prevType = parseInt($("#hdPrevType").val());
        var prevId = parseInt($("#hdPrevId").val());
        if (prevId == 0) {
            var question = $("#question_list_content .curr-answer").attr("tag");
            var type = $("#question_list_content .curr-answer").attr("ty");
            if (Type != 1) {
                UpdateExamAnswer(Id, question, type);
            }

            $(".as2").attr('disabled', true);
            $("#btnPrev").attr('disabled', true);
            $("#btnPrev").addClass('btndisable');//00a2e2
            return;
        } else {
            $(".as2").attr('disabled', false);
            $("#btnPrev").attr('disabled', false); //TextContent2
            $("#btnPrev").removeClass('btndisable');//00a2e2

            $(".as3").attr('disabled', false);
            $("#btnNext").attr('disabled', false);
            $("#btnNext").removeClass('btndisable');//00a2e2

            var question = $("#question_list_content .curr-answer").attr("tag");// $("#hdQuestionId").val();
            var type = $("#question_list_content .curr-answer").attr("ty");
            if (Type != 1) {
                UpdateExamAnswer(Id, question, type);
            }
            GetQuestion(Id, LibraryId, prevType, prevId);
        }

    });
    //下一题
    $(".as3").unbind("click").click(function () {

        var nextType = parseInt($("#hdNextType").val());
        var nextId = parseInt($("#hdNextId").val());
        if (nextId == 0) {
            var question = $("#question_list_content .curr-answer").attr("tag");
            var type = $("#question_list_content .curr-answer").attr("ty");
            if (Type != 1) {
                UpdateExamAnswer(Id, question, type);
            }

            $(".as3").attr('disabled', true);
            $("#btnNext").attr('disabled', true);
            $("#btnNext").addClass('btndisable');//00a2e2           
            return;
        } else {
            $(".as3").attr('disabled', false);
            $("#btnNext").attr('disabled', false);
            $("#btnNext").removeClass('btndisable');//00a2e2

            $(".as2").attr('disabled', false);
            $("#btnPrev").attr('disabled', false);
            $("#btnPrev").removeClass('btndisable');//00a2e2

            var question = $("#question_list_content .curr-answer").attr("tag");
            var type = $("#question_list_content .curr-answer").attr("ty");
            if (Type != 1) {
                UpdateExamAnswer(Id, question, type);
            }
            GetQuestion(Id, LibraryId, nextType, nextId);

        }

    });

    //首题
    $(".as1").unbind("click").click(function () {
        var question = $("#question_list_content .curr-answer").attr("tag");// $("#hdQuestionId").val();
        var type = $("#question_list_content .curr-answer").attr("ty");
        if (Type != 1) {
            UpdateExamAnswer(Id, question, type);
        }
        var question2 = $("#question_list_content a:first").attr("tag");
        GetQuestion(Id, LibraryId, type, question2);

    });
    //尾题
    $(".as4").unbind("click").click(function () {
        var question = $("#question_list_content .curr-answer").attr("tag");
        var type = $("#question_list_content .curr-answer").attr("ty");
        if (Type != 1) {
            UpdateExamAnswer(Id, question, type);
        }
        var question2 = $("#question_list_content a:last").attr("tag");
        GetQuestion(Id, LibraryId, type, question2);

    });

    if (Type == 1) {
    } else {
        //倒计时
        daojishi(Id);
    }


    //提交
    $("#tijiao").unbind("click").bind("click", function () {
        window.history.back(-1);
    })


});
//加载左侧列表
function GetPaperDetailList(Id, LibraryId, questionType, pageIndex, questionTypeId, optAsync) {


    //点击题型按钮时保存题目
    var question = $("#question_list_content .curr-answer").attr("tag");
    var type = $("#question_list_content .curr-answer").attr("tyid");
    var type3 = $("#question_list_content .curr-answer").attr("ty");
    var type2 = $("#hdQuestionType").val();
    if (type2 == type) {
        var Type = $.getUrlParam("type");
        if (Type != 1) {
            UpdateExamAnswer(Id, question, type3);
        }
    }


    var async = true;
    //题型选中样式
    if (questionType != undefined) {
        $(".caption-switch-con > a").removeClass("on");

        $(".caption-switch-con > a[name='type_" + questionTypeId + "']").addClass("on");

    }
    if (optAsync != undefined) {
        async = optAsync;
    }

    $.ajax({
        url: "/CompetitionUser/EheoryExamine/GetPaperDetailList",
        async: false,
        type: "POST",
        data: {
            questionType: questionType,
            pageIndex: pageIndex,
            questionTypeId: questionTypeId,
            Id: Id,
            LibraryId: LibraryId
        },
        success: function (data) {
            //if (!$.checkSignIn(data)) return;
            //是否为认证考试
            var LibraryId = $.getUrlParam("LibraryId");
            var charpterID = $.getUrlParam("charpterID");

            var Number = 0;
            if (data != null && data != "") {
                //姓名、科目、时间等信息
                $(".user-name").html(data.UserName);

                $("#li_name").html(data.UserName);
                $("#Sexs").val(data.Sex);
                $("#StudentNo").val(data.StudentNo);

                $("#li_subject").html(data.PaperName);
                $("#li_subject").attr("title", data.PaperName);
                $(".exam-name").html(data.PaperName);
                $("#li_examtime").html(data.strTime);

                $("#question_list_content").html("");
                var QID = 0;
                var type = $.getUrlParam("type");
                //加载题目
                $(data.Question).each(function (index, dom) {
                    if (index == 0) {
                        $("#FirstQuestionId").val(dom.Id);
                    }
                    var html = "";
                    if (dom.StructType == 4) {
                        //html += "<li id=\"li_question_" + dom.Id + "\" tyid=" + dom.strIdList + " ty=" + dom.StructType + " tag=" + dom.Id + " class=\"";
                    } else {
                        html += "<a id=\"li_question_" + dom.Id + "\" tyid=" + dom.strIdList + " ty=" + dom.StructType + " tag=" + dom.Id + " class=\"";
                    }
                    if (type != 1) {
                        //是否已完成
                        if (dom.Result != null && dom.IsDaTi == true) {
                            html += " has-answer ";
                        } else if (dom.Result == null && dom.IsDaTi == true) {
                            html += " has-answer ";
                        }
                    }
                    else {
                        //是否正确
                        if (dom.IsDaTi != true) {
                            html += " no-answer ";
                        } else if (dom.Result > 0) {
                            html += " right-answer ";
                        } else {
                            html += " wrong-answer ";
                        }
                    }
                    //是否当前题目
                    if (String(dom.strIdList) == $("#hdQuestionType").val() || index == 0) {
                        //在查看的时候不需要在答的底色
                        //var type = $.getUrlParam("type");
                        if (type != 1) {
                            if (dom.StructType == 4) {
                                if (String(dom.Id) == $("#hdQuestionId").val()) {
                                    html += " curr-answer ";
                                }
                            } else {
                                if (String(dom.Id) == $("#hdQuestionId").val() || index == 0) {
                                    html += " curr-answer ";
                                }
                            }
                        }
                        else {
                            if (String(dom.Id) == $("#hdQuestionId").val() || index == 0) {
                                html += " curr-answer ";
                            }
                        }
                    }

                    html += "\" onclick=\"javascript:UpdateExamAnswer(" + Id + "," + dom.Id + "," + dom.StructType + ");GetQuestion(" + Id + "," + LibraryId + "," + dom.StructType + "," + dom.Id + ");\" >";

                    html += ((data.PageIndex - 1) * data.PageSize + index + 1);
                    html += "</a>";
                    $("#question_list_content").append(html);
                });


                //加载题目类型
                if (data.QuestionTypes != null && data.QuestionTypes.length > 0) {
                    var htmls = "";

                    $(".caption-switch-con").html("");
                    $(data.QuestionTypes).each(function (index, dom) {

                        htmls += "<a data-value=\"" + dom.strIdList + "\" name=\"type_" + dom.strIdList + "\"" + (index == 0 ? "class=\"on\"" : "") + " onclick=\"javascript:GetPaperDetailList(" + Id + "," + LibraryId + "," + GetTypeId(dom.TypeName) + ",1,'" + dom.strIdList + "',true);GetQuestion(" + Id + "," + LibraryId + "," + GetTypeId(dom.TypeName) + ",-1)\" >" + dom.TypeName + "</a>";
                    });
                    $(".caption-switch-con").html(htmls);
                }
                //该题型下有多少题
                $("#hdQuestionCount").val(data.Count);
                //该题型当前页数
                $("#hdQuestionPageIndex").val(data.PageIndex);
                //该题型总页数
                $("#hdQuestionPageCount").val(data.PageCount);
                //具体的题目列表
                //$("#question_list_content").html("");                             
            }

        }

    });
}

//题目上下翻页
function GoPage(type) {
    var pageIndex = parseInt($("#hdQuestionPageIndex").val());
    var pageCount = parseInt($("#hdQuestionPageCount").val());
    var questionType = $(".caption-switch-con > a[class='on']").attr("data-value");
    var Id = $.getUrlParam("Id");
    if (type == 1) {
        //上翻页
        if (pageIndex - 1 < 1) {
            return;
        } else {
            GetPaperDetailList(Id, questionType, (pageIndex - 1), true);
        }
    } else {
        //下翻页
        if (pageIndex + 1 > pageCount) {
            return;
        } else {
            GetPaperDetailList(Id, questionType, (pageIndex + 1), true);
        }
    }
}

//获取题目
function GetQuestion(PaperId, LibraryId, questionType, questionId) {
    if (questionId != undefined && questionId == -1) {
        questionId = $("#FirstQuestionId").val();
    }
    $("#fujian2").hide();
    //清空标记框
    //$("#isMark").removeAttr("checked");

    $.ajax({
        url: "/CompetitionUser/EheoryExamine/GetQuestion",
        type: "POST",
        async: false,
        dataType: "json",
        crossDomain: false,
        data:
        {
            questionType: questionType,
            questionId: questionId,
            PaperId: PaperId,
            LibraryId: LibraryId
        },
        success: function (data) {

            if (data != null && data != "") {
                var Type = $.getUrlParam("type");
                if (data.Flag) {
                    //显示试题
                    var question = data.Data;
                    $("#questionContext").html(question.Context);
                    $(".item-info ul").html("");
                    if (data.StructType == 1) {
                        $("#selectAnswer").show();
                        //单选
                        var html1 = "";
                        $.each(question.OptionList, function (i, item) {
                            html1 += " <li class='clear'><label><input value=" + (i) + " type='radio' name='answer' >" + GetAnswerStr(i) + "</label><textarea type='text' disabled='disabled' class='inputtext seleOp' style='width:85%;height:25px;float:right;margin-bottom:10px;'>" + item.OptionName + "</textarea></li>";
                        });
                        $(".item-info ul").html(html1);
                    } else if (data.StructType == 2) {
                        $("#selectAnswer").show();
                        //多选
                        var html2 = "";
                        $.each(question.OptionList, function (i, item) {
                            html2 += " <li class='clear'><label><input value=" + (i) + " type='checkbox' name='answer'>" + GetAnswerStr(i) + "</label><textarea type='text' disabled='disabled' class='inputtext seleOp' style='width:85%;height:25px;float:right;margin-bottom:10px;'>" + item.OptionName + "</textarea></li>";
                        });
                        $(".item-info ul").html(html2);
                    } else if (data.StructType == 3) {
                        $("#selectAnswer").show();
                        //判断
                        var html3 = "";
                        $.each(question.OptionList, function (i, item) {
                            html3 += " <li class='clear'><label><input value=" + (i) + " type='radio' name='answer'>" + GetAnswerStr(i) + "</label><textarea type='text' disabled='disabled' class='inputtext seleOp' style='width:85%;height:25px;float:right;margin-bottom:10px;'>" + item.OptionName + "</textarea></li>";
                        });
                        $(".item-info ul").html(html3);
                        $("#selectAnswer").show();
                    }
                    $(".seleOp").each(function (index, dom) {
                        this.style.height = (this.scrollHeight - 10) + 'px';
                    });
                    var LibraryId = $.getUrlParam("LibraryId");
                    var ty = "";
                    if (LibraryId == 1) {

                        if (question.AttachmentList != null) {
                            if (question.AttachmentList.length > 0) {
                                //附件
                                $("#fujian2").show();
                                var html = "";
                                $.each(question.AttachmentList, function (i, item) {
                                    if (i == 0) {
                                        ty = "一";
                                    } else if (i == 1) {
                                        ty = "二";
                                    } else if (i == 2) {
                                        ty = "三";
                                    }
                                    var lastIndex = item.Name.lastIndexOf("\\");
                                    var fullName = item.Name.substring(lastIndex + 1);
                                    var _index = fullName.lastIndexOf(".");
                                    var name = fullName.substring(0, _index);
                                    html += " <a href=\"javascript:DownLoad('" + item.FileUrl + "','" + name + "','" + item.Id + "');\"><img src=\"/Content/images/text-icon.png\"><span>附件" + ty + "</span></a>";

                                });
                                $(".accessory").html(html);
                            }
                        }
                    } else {
                        $("#fujian2").hide();
                    }


                    //当前题目类型和ID
                    $("#hdQuestionType").val(data.Current.Key);
                    $("#hdQuestionId").val(data.Current.Value);
                    var Id = $.getUrlParam("Id");
                    //是否为认证考试
                    var LibraryId = $.getUrlParam("LibraryId");
                    //题型跟左侧是否一致，不一致则切换加载,一致则看是否在当前页
                    var type = $(".caption-switch-con > a[class='on']").attr("data-value");
                    if (type != String(data.Current.Key)) {
                        GetPaperDetailList(Id, LibraryId, data.StructType, 1, data.Current.Key, false);
                    } else {
                        var pageIndex = parseInt($("#hdQuestionPageIndex").val());
                        //找不到当前题目时
                        if ($("#li_question_" + data.Current.Value).length < 1) {
                            //上一题存在,则说明本题在下一页
                            if ($("#li_question_" + data.Prev.Value).length > 0) {
                                GetPaperDetailList(Id, LibraryId, data.StructType, (pageIndex + 1), data.Current.Key, false);
                            } else {
                                //下一题存在，则说明本题在上一页
                                if ($("#li_question_" + data.Next.Value).length > 0) {
                                    GetPaperDetailList(Id, LibraryId, data.StructType, (pageIndex - 1), data.Current.Key, false);
                                    return;
                                }
                            }
                        }
                    }

                    var type = $.getUrlParam("type");
                    if (type != 1) {
                        //当前题目颜色
                        $("#question_list_content > a").removeClass("curr-answer");
                        $("#li_question_" + data.Current.Value).addClass("curr-answer");
                    }
                    else {
                        //当前题目颜色
                        $("#question_list_content > a").removeClass("curr-answer");
                        $("#li_question_" + data.Current.Value).addClass("curr-answer");
                    }

                    var checked = "";
                    $("#daan1").show();
                    $("#daan2").show();
                    $("#daan3").show();
                    $("#daan4").show();
                    //答案
                    answer_cache = new Array();
                    if (data.StructType == 1 || data.StructType == 2 || data.StructType == 3) {
                        if (data.Answers != null && data.Answers.length > 0) {
                            $(data.Answers).each(function (index, dom) {
                                $("input[name='answer'][value='" + dom + "']").attr("checked", true);
                                answer_cache.push(dom);
                                checked += GetAnswerStr(dom);
                            });
                        }
                    }
                    $("#span_3").val(checked);//您的答案

                    //上一题，下一题
                    $("#hdPrevType").val(data.Prev.Key)
                    $("#hdPrevId").val(data.Prev.Value);
                    $("#hdNextType").val(data.Next.Key);
                    $("#hdNextId").val(data.Next.Value);


                    $("#btnPrev,#btnNext").removeClass("btnNo");
                    if (data.Prev.Value == 0) {
                        $("#btnPrev").addClass("btnNo");
                    }
                    if (data.Next.Value == 0) {
                        $("#btnNext").addClass("btnNo");
                    }

                    //头部信息
                    $(".crumb").html(data.Info);
                    $(".d_shi_right_2").html(data.Info);

                    //主题干
                    if (data.Topic == null || data.Topic == "") {
                        $("#itm").hide();
                    } else {
                        $("#itm").show();
                        $("#itm").html(data.Topic);

                    }
                } else {
                    if (data.ErrorCode != undefined) {
                        if (data.ErrorCode == 404 || data.ErrorCode == "404") {
                            dialogHelper.Error({
                                content: data.ErrorMsg,
                                success: function () {
                                    location.href = "/CompetitionUser/Index";
                                },
                                cancle: function () {
                                    location.href = "/CompetitionUser/Index";
                                }
                            });
                        } else {
                            dialogHelper.Error({ content: data.ErrorMsg });
                        }
                    } else {
                        dialogHelper.Error({ content: data.ErrorMsg });
                    }
                }
                var type = $.getUrlParam("type");
                if (type != 1) {
                    $(".li_question_" + questionId).addClass("curr-answer");
                }
                else {
                    $(".li_question_" + questionId).addClass("curr-answer");

                }

            }

            if (Type == 1) {

                //是否为认证考试
                var LibraryId = $.getUrlParam("LibraryId");
                //if (LibraryId == 1) {
                if (data.analyse != null && data.analyse != "") {
                    $("#tiankongAnswer2").show();
                    $("#TextContent2").val(data.analyse);

                    var type = $("#question_list_content .curr-answer").attr("ty");
                    if (type == 1 || type == 2 || type == 3) {
                        $("#pingxi").html("解析：");
                    } else {
                        $("#pingxi").html("评析：");
                    }
                } else {
                    $("#tiankongAnswer2").hide();
                }

                $("#span_1").html(data.StandardScore);//标准分
                $("#span_2").html(data.RightScore);//得分
                if (data.StructType == 1 || data.StructType == 2 || data.StructType == 3) {

                    $("#daan1").show();
                    $("#daan2").show();
                    $("#daan3").show();
                    $("#daan4").show();
                    var check = "";
                    $.each(data.answer, function (i, n) {
                        check += GetAnswerStr(n);
                    });
                    $("#span_4").val(check);//标准答案


                } else {
                    $("#daan3").hide();
                    $("#daan4").hide();
                }

                $(".item-info ul").find("input").attr("disabled", "disabled");
                $("#time").hide();
                $("#tijiao").html("返回");
            }

            var prevType = parseInt($("#hdPrevType").val());
            var prevId = parseInt($("#hdPrevId").val());
            var prev = $("#hdPrevId").val();
            var nextType = parseInt($("#hdNextType").val());
            var nextId = parseInt($("#hdNextId").val());
            var next = $("#hdNextId").val();

            if (prevId == 0 || prev == "") {
                $(".as2").attr('disabled', true);
                $("#btnPrev").attr('disabled', true);
                $("#btnPrev").addClass('btndisable');//00a2e2
            } else {
                $(".as2").attr('disabled', false);
                $("#btnPrev").attr('disabled', false);
                $("#btnPrev").removeClass('btndisable');//00a2e2
            }

            if (nextId == 0 || next == "") {
                $(".as3").attr('disabled', true);
                $("#btnNext").attr('disabled', true);
                $("#btnNext").addClass('btndisable');//00a2e2
            } else {
                $(".as3").attr('disabled', false);
                $("#btnNext").attr('disabled', false);
                $("#btnNext").removeClass('btndisable');//00a2e2
            }
        }
    });
}

//更新答案
function UpdateExamAnswer(Id, question, nextType, bo) {
    var Type = $.getUrlParam("type");
    if (Type != 1) {

        if (!VerificationHelper.checkFrom("tiankongAnswer")) {
            return false;
        }

        //是否为认证考试
        var LibraryId = $.getUrlParam("LibraryId");

        var check_answer_flag = true;
        //var check_ismark_flag = true;
        //拼接
        var strAnswer = "";
        $("input[name='answer']").each(function (index, dom) {
            if ($(dom).attr("checked")) {
                strAnswer += $(dom).val();
                strAnswer += ",";
            }
        });
        //if ($("#TextContent").val() != "") {
        //    strAnswer = $("#TextContent").val();
        //    strAnswer = htmlEncode(strAnswer);
        //    //strAnswer = strAnswer.replace(/[<>&"]/g, function (c) { return { '<': '&lt;', '>': '&gt;', '&': '&amp;', '"': '&quot;', '"': '&quot;', "'": '&prime;', "'": '&prime;' }[c]; });
        //}  
        //答案是否更改
        var arr = [];
        if (nextType == 1 || nextType == 2 || nextType == 3) {
            if (strAnswer.lastIndexOf(",") > -1) {
                strAnswer = strAnswer.substring(0, strAnswer.lastIndexOf(","));
            }

            if (strAnswer.length > 0) {
                arr = strAnswer.split(',');
            }
        } else {
            if (strAnswer.length > 0) {
                arr = strAnswer;
            }
        }

        if (answer_cache.length == arr.length) {
            $(arr).each(function (index, dom) {
                if (answer_cache[index] != undefined) {
                    if (answer_cache[index] != parseInt(dom)) {
                        check_answer_flag = false;
                    }
                }
            });
        } else {
            check_answer_flag = false;
        }
        //标记是否被更改
        //var isMark = $("#isMark").attr("checked") == "checked";
        //if (ismark_cache != isMark) {
        //    check_ismark_flag = false;
        //}
        if (check_answer_flag) {
            return;
        }
        question = $("#question_list_content .curr-answer").attr("tag");
        //更新答案
        $.ajax({
            url: "/CompetitionUser/EheoryExamine/UpdateExamAnswer",
            type: "POST",
            async: false,
            dataType: "json",
            crossDomain: false,
            data:
            {
                strAnswer: strAnswer,
                //isMark: isMark,
                Id: Id,
                QuestionId: question,
                Type: nextType,
                LibraryId: LibraryId
            },
            success: function (data) {
                //if (!$.checkSignIn(data)) return;
                var type = $.getUrlParam("type");
                if (type != 1) {
                    //填写答案则标识为已完成
                    if (strAnswer.length > 0) {
                        $("#li_question_" + $("#hdQuestionId").val()).addClass("has-answer");
                    } else {
                        $("#li_question_" + $("#hdQuestionId").val()).removeClass("has-answer");
                    }
                }
                if (bo != true) {
                    //$("#TextContent").val("");
                }

                if (!data.Flag) {
                    dialogHelper.Error({ content: data.ErrorMsg });
                }
            }
        });

        // $("#TextContent").val("");
    }
}

//获取答案序号
function GetAnswerStr(index) {
    var result = "";
    switch (index) {
        case 0:
            result = "A";
            break;
        case 1:
            result = "B";
            break;
        case 2:
            result = "C";
            break;
        case 3:
            result = "D";
            break;
        case 4:
            result = "E";
            break;
        case 5:
            result = "F";
            break;
        case 6:
            result = "G";
            break;
        case 7:
            result = "H";
            break;
        case 8:
            result = "I";
            break;
        case 9:
            result = "J";
            break;
    }
    return result;
}


//检测试卷完成情况
function CheckExam(LibraryId) {
    var Id = $.getUrlParam("Id");

    //更新当前页答案
    var question = $("#question_list_content .curr-answer").attr("tag");
    var type = $("#question_list_content .curr-answer").attr("ty");
    UpdateExamAnswer(Id, question, type, true);

    //检测是否有未完成题目
    $.ajax({
        url: "/CompetitionUser/EheoryExamine/CheckExamQuestion",
        type: "POST",
        async: false,
        dataType: "json",
        crossDomain: false,
        data:
        {
            ExamPaperId: Id,
            LibraryId: LibraryId
        },
        success: function (data) {
            var error = "";
            if (data.radioNum > 0) {
                error += "您还有" + data.radioNum + "道题未完成，";
                error += "确认提交试卷？<br>";

                dialogHelper.Confirm({
                    content: error,
                    success: function () {
                        FinishExam(Id);

                    }
                });

                return;
            }

            if (data.Flag) {
                dialogHelper.Confirm({
                    content: "考试尚未结束，确认提交试卷？",
                    success: function () {
                        FinishExam(Id);
                    }
                });
            } else {
                if (data.ErrorCode != undefined) {
                    if (data.ErrorCode == 404 || data.ErrorCode == "404") {
                        dialogHelper.Error({
                            content: data.ErrorMsg,
                            success: function () {
                                location.href = "/CompetitionUser/MatchList/NotStart";
                            },
                            cancle: function () {
                                location.href = "/CompetitionUser/MatchList/NotStart";
                            }
                        });
                    } else {
                        dialogHelper.Error({ content: data.ErrorMsg });
                    }
                } else {

                }
            }
        }
    });
}

//结束考试
function FinishExam(PaperId) {
    //是否为认证考试
    var LibraryId = $.getUrlParam("LibraryId");
    $.ajax({
        url: "/CompetitionUser/EheoryExamine/FinishExam",
        type: "POST",
        async: false,
        dataType: "json",
        crossDomain: false,
        data:
        {
            PaperId: PaperId,
            LibraryId: LibraryId
        },
        success: function (data) {
            if (data.Flag) {
                dialogHelper.Success({
                    content: "试卷提交成功！",
                    success: function () {
                        location.href = "/CompetitionUser/MatchList/Start";
                    },
                    cancle: function () {
                        location.href = "/CompetitionUser/MatchList/Start";
                    }
                })

            } else {
                dialogHelper.Error({ content: "系统出错，请联系管理员！" });
            }
        }
    });
}

//获取题目类型
function GetQuestionType(type) {
    var typename = ""
    if (String(type) == 1) {
        typename = "单选题";
    } else if (String(type) == 2) {
        typename = "多选题";
    } else if (String(type) == 3) {
        typename = "判断题";
    } else if (String(type) == 4) {
        typename = "综合题";
    }
    return typename;
}

//获取题目类型
function GetTypeId(type) {
    var typename = ""
    if (String(type) == "单选题") {
        typename = 1;
    } else if (String(type) == "多选题") {
        typename = 2;
    } else if (String(type) == "判断题") {
        typename = 3;
    } else if (String(type) == "填空题") {
        typename = 4;
    } else {
        typename = 5;
    }
    return typename;
}


function daojishi(Id) {
    //倒计时
    time = 2 * 60 * 60;
    var flag = true;

    time = GetTimes(false, Id);
    //倒计时，每秒刷新一次
    window.setInterval(function () {
        if (time >= 0) {
            var hh = parseInt(time / 60 / 60 % 24, 10); //计算剩余的小时数  
            var mm = parseInt(time / 60 % 60, 10);      //计算剩余的分钟数 
            var ss = parseInt(time % 60, 10);           //计算剩余的秒数  
            $("#strTimes").html(hh + ":" + checkTime(mm) + ":" + checkTime(ss));
            time = time - 1;
        } else {
            //提交试卷
            if (flag) {
                flag = false;
                // UpdateExamAnswer();
                var Id = $.getUrlParam("Id");
                FinishExam(Id);
            }
        }
    }, 1000);
    //每五分钟访问一次后台，校准时间
    window.setInterval("GetTimes(true," + Id + ")", 1 * 60 * 1000);
}
//倒计时选相关方法
function GetTimes(async, Id) {
    //是否为认证考试
    var LibraryId = $.getUrlParam("LibraryId");
    $.ajax({
        url: "/CompetitionUser/EheoryExamine/GetExamTimes",
        type: "POST",
        async: async,
        dataType: "json",
        crossDomain: false,
        data:
        {
            Id: Id,
            LibraryId: LibraryId
        },
        success: function (data) {
            //if (!$.checkSignIn(data)) return;

            if (data != null && data != "") {
                time = data;

            }
        }
    });
    return time;
}
function checkTime(i) {
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}

//文件下载
function DownLoad(path, name, Id) {

    location.href = "/CompetitionUser/EheoryExamine/DownloadFile?path=" + encodeURIComponent(path) + "&name=" + encodeURIComponent(name) + "&randomId=" + Math.random();

}

//已评分查看正确率
function LookOver(Id) {
    var userId = $("#hdUserId").val();
    //是否为认证考试
    var LibraryId = $.getUrlParam("LibraryId");
    $.ajax({
        url: "/CompetitionUser/EheoryExamine/GetPaperDetail",
        async: false,
        type: "POST",
        data: {
            Id: Id,
            userId: userId,
            LibraryId: LibraryId,
            random: Math.random()
        },
        success: function (data) {

            if (data != null && data != "") {
                var Id = $.getUrlParam("Id");
                //姓名、科目、时间等信息
                $(".user-name").html(data.UserName);

                $("#li_name").html(data.UserName);
                $("#Sexs").val(data.Sex);
                $("#StudentNo").val(data.StudentNo);

                $("#li_subject").html(data.PaperName);
                $("#li_subject").attr("title", data.PaperName);
                $(".exam-name").html(data.PaperName);
                $("#li_examtime").html(data.strTime);

                $("#question_list_content").html("");
                var QID = 0;
                var type = $.getUrlParam("type");
                //加载题目
                $(data.Question).each(function (index, dom) {
                    if (index == 0) {
                        $("#FirstQuestionId").val(dom.Id);
                    }
                    var html = "";

                    html += "<a id=\"li_question_" + dom.Id + "\" tyid=" + dom.strIdList + " ty=" + dom.StructType + " tag=" + dom.Id + " class=\"";


                    //是否正确
                    if (dom.IsDaTi != true) {
                        html += " no-answer ";
                    } else if (dom.Result > 0) {
                        html += " right-answer ";
                    } else {
                        html += " wrong-answer ";
                    }

                    //是否当前题目
                    if (String(dom.strIdList) == $("#hdQuestionType").val() || index == 0) {
                        if (String(dom.Id) == $("#hdQuestionId").val() || index == 0) {
                            html += " curr-answer ";
                        }
                    }

                    html += "\" onclick=\"javascript:UpdateExamAnswer(" + Id + "," + dom.Id + "," + dom.StructType + ");GetQuestion(" + Id + "," + LibraryId + "," + dom.StructType + "," + dom.Id + ");\" >";

                    html += ((data.PageIndex - 1) * data.PageSize + index + 1);
                    html += "</a>";
                    $("#question_list_content").append(html);
                });


                //加载题目类型
                if (data.QuestionTypes != null && data.QuestionTypes.length > 0) {
                    var htmls = "";

                    $(".caption-switch-con").html("");
                    $(data.QuestionTypes).each(function (index, dom) {

                        htmls += "<a data-value=\"" + dom.strIdList + "\" name=\"type_" + dom.strIdList + "\"" + (index == 0 ? "class=\"on\"" : "") + " onclick=\"javascript:GetPaperDetailList(" + Id + "," + LibraryId + "," + GetTypeId(dom.TypeName) + ",1,'" + dom.strIdList + "',true);GetQuestion(" + Id + "," + LibraryId + "," + GetTypeId(dom.TypeName) + ",-1)\" >" + dom.TypeName + "</a>";
                    });
                    $(".caption-switch-con").html(htmls);
                }
                //该题型下有多少题
                $("#hdQuestionCount").val(data.Count);
                //该题型当前页数
                $("#hdQuestionPageIndex").val(data.PageIndex);
                //该题型总页数
                $("#hdQuestionPageCount").val(data.PageCount);


                //加载题目类型
                if (data.QuestionTypes != null && data.QuestionTypes.length > 0) {
                    var htmll = "";
                    var AllNumber = 0;
                    var YesNumber = 0;
                    var fengshu = 0;
                    var AllFenShu = 0;
                    $(data.QuestionTypes).each(function (index, dom) {
                        var str = dom.strIdList.split(',');
                        for (var s = 0; s < str.length; s++) {
                            $(data.Details).each(function (i, n) {
                                if (n.StructTypeId == str[s]) {
                                    AllNumber = Number(AllNumber) + 1;
                                }
                            });

                            $(data.PUserAnswerResult).each(function (a, b) {
                                if (b.QuestionTypeId == str[s]) {
                                    if (b.Result == 2) {
                                        YesNumber = Number(YesNumber) + 1;
                                        fengshu = Number(fengshu) + Number(b.UserScore);
                                    }
                                }
                            });
                        }
                        htmll += "<tr >";
                        htmll += "<td style='border:1px solid gray'>" + dom.TypeName + "</td>";
                        if (dom.TypeName == "单选题" || dom.TypeName == "多选题" || dom.TypeName == "判断题") {
                            htmll += "<td style='border:1px solid gray'><span style='Color:red'>" + YesNumber + "</span>/" + AllNumber + "</td>";
                        } else {
                            htmll += "<td style='border:1px solid gray'><span style='Color:red'>——</td>";
                        }


                        htmll += "<td style='border:1px solid gray'>" + fengshu + "</td>";
                        if (index == 0) {
                            htmll += "<td style='border:1px solid gray' rowspan='" + data.QuestionTypes.length + "' ><span style='Color:red' class='AllFenShu'></span></td>";
                        }
                        htmll += "</tr>";

                        AllFenShu = AllFenShu + fengshu;
                        AllNumber = 0;
                        YesNumber = 0;
                        fengshu = 0;
                    });
                    htmll += "<tr><td style='border:1px solid gray' colspan='3' >试卷满分</td><td style='border:1px solid gray' >" + data.ExamPagerScore + "</td></tr>";
                    $("#tab").html(htmll);
                    $(".AllFenShu").html(AllFenShu);
                }

                // 加载首题
                var question = data.ExamFirstQuestion.Data;
                var firstShow = data.ExamFirstQuestion;
                $("#questionContext").html(question.Context);
                $(".item-info ul").html("");
                if (firstShow.StructType == 1) {
                    $("#selectAnswer").show();
                    //单选
                    var html1 = "";
                    $.each(question.OptionList, function (i, item) {
                        html1 += " <li class='clear'><label><input value=" + (i) + " type='radio' name='answer' >" + GetAnswerStr(i) + "</label><textarea type='text' disabled='disabled' class='inputtext seleOp' style='width:85%;height:25px;float:right;margin-bottom:10px;'>" + item.OptionName + "</textarea></li>";
                    });
                    $(".item-info ul").html(html1);
                } else if (firstShow.StructType == 2) {
                    $("#selectAnswer").show();
                    //多选
                    var html2 = "";
                    $.each(question.OptionList, function (i, item) {
                        html2 += " <li class='clear'><label><input value=" + (i) + " type='checkbox' name='answer'>" + GetAnswerStr(i) + "</label><textarea type='text' disabled='disabled' class='inputtext seleOp' style='width:85%;height:25px;float:right;margin-bottom:10px;'>" + item.OptionName + "</textarea></li>";
                    });
                    $(".item-info ul").html(html2);
                } else if (firstShow.StructType == 3) {
                    $("#selectAnswer").show();
                    //判断
                    var html3 = "";
                    $.each(question.OptionList, function (i, item) {
                        html3 += " <li class='clear'><label><input value=" + (i) + " type='radio' name='answer'>" + GetAnswerStr(i) + "</label><textarea type='text' disabled='disabled' class='inputtext seleOp' style='width:85%;height:25px;float:right;margin-bottom:10px;'>" + item.OptionName + "</textarea></li>";
                    });
                    $(".item-info ul").html(html3);
                    $("#selectAnswer").show();
                }
                $(".seleOp").each(function (index, dom) {
                    this.style.height = (this.scrollHeight - 10) + 'px';
                });
                var ty = "";
                $("#fujian2").hide();
                if (question.AttachmentList != null) {
                    if (question.AttachmentList.length > 0) {
                        //附件
                        $("#fujian2").show();
                        var html = "";
                        $.each(question.AttachmentList, function (i, item) {
                            if (i == 0) {
                                ty = "一";
                            } else if (i == 1) {
                                ty = "二";
                            } else if (i == 2) {
                                ty = "三";
                            }
                            var lastIndex = item.Name.lastIndexOf("\\");
                            var fullName = item.Name.substring(lastIndex + 1);
                            var _index = fullName.lastIndexOf(".");
                            var name = fullName.substring(0, _index);
                            html += " <a href=\"javascript:DownLoad('" + item.FileUrl + "','" + name + "','" + item.Id + "');\"><img src=\"/Content/images/text-icon.png\"><span>附件" + ty + "</span></a>";

                        });
                        $(".accessory").html(html);
                    }
                }



                //当前题目类型和ID
                $("#hdQuestionType").val(firstShow.Current.Key);
                $("#hdQuestionId").val(firstShow.Current.Value);
                var Id = $.getUrlParam("Id");

                //当前题目颜色
                $("#question_list_content > a").removeClass("curr-answer");
                $("#li_question_" + firstShow.Current.Value).addClass("curr-answer");

                var checked = "";
                //答案
                answer_cache = new Array();
                if (firstShow.Answers != null && firstShow.Answers.length > 0) {
                    $(firstShow.Answers).each(function (index, dom) {
                        $("input[name='answer'][value='" + dom + "']").attr("checked", true);
                        answer_cache.push(dom);
                        checked += GetAnswerStr(dom);
                    });
                }
                $("#span_3").val(checked);//您的答案

                //上一题，下一题
                $("#hdPrevType").val(firstShow.Prev.Key)
                $("#hdPrevId").val(firstShow.Prev.Value);
                $("#hdNextType").val(firstShow.Next.Key);
                $("#hdNextId").val(firstShow.Next.Value);


                $("#btnPrev,#btnNext").removeClass("btnNo");
                if (firstShow.Prev.Value == 0) {
                    $("#btnPrev").addClass("btnNo");
                }
                if (firstShow.Next.Value == 0) {
                    $("#btnNext").addClass("btnNo");
                }

                //头部信息
                $(".crumb").html(firstShow.Info);
                $(".d_shi_right_2").html(firstShow.Info);

                //主题干
                if (firstShow.Topic == null || firstShow.Topic == "") {
                    $("#itm").hide();
                } else {
                    $("#itm").show();
                    $("#itm").html(firstShow.Topic);
                }

                if (firstShow.analyse != null && firstShow.analyse != "") {
                    $("#tiankongAnswer2").show();
                    $("#TextContent2").val(firstShow.analyse);

                    var type = $("#question_list_content .curr-answer").attr("ty");
                    if (type == 1 || type == 2 || type == 3) {
                        $("#pingxi").html("解析：");
                    } else {
                        $("#pingxi").html("评析：");
                    }
                } else {
                    $("#tiankongAnswer2").hide();
                }

                $("#span_1").html(firstShow.StandardScore);//标准分
                $("#span_2").html(firstShow.RightScore);//得分

                $("#daan1").show();
                $("#daan2").show();
                $("#daan3").show();
                $("#daan4").show();
                var check = "";
                $.each(firstShow.answer, function (i, n) {
                    check += GetAnswerStr(n);
                });
                $("#span_4").val(check);//标准答案

                $(".item-info ul").find("input").attr("disabled", "disabled");
                $("#time").hide();
                $("#tijiao").html("返回");

                var prevType = parseInt($("#hdPrevType").val());
                var prevId = parseInt($("#hdPrevId").val());
                var prev = $("#hdPrevId").val();
                var nextType = parseInt($("#hdNextType").val());
                var nextId = parseInt($("#hdNextId").val());
                var next = $("#hdNextId").val();

                if (prevId == 0 || prev == "") {
                    $(".as2").attr('disabled', true);
                    $("#btnPrev").attr('disabled', true);
                    $("#btnPrev").addClass('btndisable');//00a2e2
                } else {
                    $(".as2").attr('disabled', false);
                    $("#btnPrev").attr('disabled', false);
                    $("#btnPrev").removeClass('btndisable');//00a2e2
                }

                if (nextId == 0 || next == "") {
                    $(".as3").attr('disabled', true);
                    $("#btnNext").attr('disabled', true);
                    $("#btnNext").addClass('btndisable');//00a2e2
                } else {
                    $(".as3").attr('disabled', false);
                    $("#btnNext").attr('disabled', false);
                    $("#btnNext").removeClass('btndisable');//00a2e2
                }


            } else {
                dialogHelper.Error({
                    content: "没有找到该试卷！", success: function () {
                        location.href = "/CompetitionUser/EheoryExamine/AlreadyEheoryExamine";
                    }
                });
            }

        }

    });

}

// 检查答案
function ChkResult() {
    var rightAnswer = $("#span_4").val();
    dialogHelper.Success({
        content: "正确答案是：" + rightAnswer
    })
}

