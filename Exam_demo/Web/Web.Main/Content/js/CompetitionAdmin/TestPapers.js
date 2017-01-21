var CompetitionHelper = new arrayHelper("CompetitionId");//竞赛
var ClassHelper = new arrayHelper("ClassId");//班级
var TestPapersHelper = new arrayHelper("CharpterID");//试题来源
var QuestionsHelper = new arrayHelper("CharpterID");//题型
var WaitSelQuestionsHelper = new arrayHelper("QuesionId");//待添加试题
var AlreadyQuestionsHelper = new arrayHelper("QuesionId");//已添加试题
var flag = false;//当前页面是否为新增
$(function () {

    //当查看状态时控件屏蔽
    var Type = $.getUrlParam("Status");
    if (Type != "Add") {
        $("#labtypeName").text("编辑试卷");
        $("#FormType").attr("disabled", true);
        var PapersId = $.getUrlParam("PapersId");
        $("#TestPapersId").val(PapersId);
        LodingTestPapers(PapersId)
        if (TestPapersHelper.GetList().length <= 0) {
            $("#ScoreList").hide();
            //$("#Oper").hide();
        }
        //当组卷方式为手动时隐藏题型管理
        if ($("#FormType").val() == "自动组卷") {
            $("#TiXingGuanLi").show();
        } else {
            $("#TiXingGuanLi").hide();
        }
        $("#btnSaveTestPapers").remove();

    } else {
        //隐藏列表和提交区域
        $("#ScoreList").hide();
        $("#ExamPaperName").html("添加比赛内容");
    }

    //返回
    $("#btnReturn").unbind("click").click(function () {
        var MatchId = $.getUrlParam("MatchId");
        var MatchType = $.getUrlParam("MatchType");
        location.href = "/CompetitionAdmin/Paper/MatchContent?MatchId=" + MatchId + "&MatchType=" + MatchType;
    });
    //编辑试题
    $("#btnEditQuestions").unbind("click").click(function () {
        EditQuestionsLoading();
    });
    //保存
    $("#btnSaveQuestions").unbind("click").click(function () {
        var PaperID = $("#TestPapersId").val() * 1;
        if (PaperID == 0) {
            dialogHelper.Error({
                content: "请先保存试卷基本信息！",
                success: function () { }
            });
            return;
        }
        //检测勾选行输入内容校验
        if (!VerificationHelper.checkFrom("QuestionsList"))
            return;
        var ScoreHelper = new arrayHelper("Id");//题型
        //获取待保存对象 
        $("#QuestionsList tr").each(function (i, n) {
            var Score = $(n).find("td:eq(2) :input").val();
            var Id = $(n).find("td:eq(2) :input").attr("flag");
            var CharpterID = $(n).find("td:eq(2) :input").attr("flag1");
            var Count = $(n).find("td:eq(1)").text();
            var obj = new Object();
            obj.Id = Id;
            obj.Score = Score;
            obj.PaperID = PaperID;
            obj.CharpterID = CharpterID;
            obj.Count = Count;
            ScoreHelper.Add(obj);
        });
        if (ScoreHelper.GetList().length <= 0) {
            dialogHelper.Error({
                content: "请添加考试题型！",
                success: function () { }
            });
            return;
        }

        var TotalScore = $("#TotalScore1").text();
        $.ajax({
            url: "/CompetitionAdmin/Paper/SaveScore",
            type: "POST",
            data: {
                List: ScoreHelper.GetList(),
                TotalScore: TotalScore
            },
            success: function (data) {
                dialogHelper.Success({
                    content: "保存成功！",
                    success: function () {
                        //$("#Oper").show();
                        //publishPaper();
                    }
                });
            }
        });
    });
    //题量验证
    var validata = function TestPaperScoreCheck() {
        $("#popTestSet #TestPapersList tr").each(function (i, n) {
            if ($(n).find("td:eq(0) input").attr("checked") == "checked") {
                var value = $(n).find("td:eq(3) input").val() * 1;
                var max = $(n).find("td:eq(3) input").attr("maxnumber") * 1;
                var tixing = $(n).find("td:eq(1) div").text();
                if (tixing == "单选题" || tixing == "多选题" || tixing == "判断题") {
                    if (value > 100) {
                        showValidateMsg($(n).find("td:eq(3) input").attr("id"), "该题型题量不能大于100题！");
                    }
                }
                if (value > max) {
                    showValidateMsg($(n).find("td:eq(3) input").attr("id"), "题量不能大于总题量！");
                }
            }
        });
    }
    //保存题型
    $("#AddQuestionsSel").unbind("click").click(function () {
        //检测勾选行输入内容校验
        if (!VerificationHelper.checkFrom("TestPapersList", validata))
            return;
        dialogHelper.Close('popTestSet');
        //将勾选的放入QuestionsHelper
        QuestionsHelper.RemoveAll();
        $("#TestPapersList tr").each(function (i, n) {
            if ($(n).find("td:eq(0) input:eq(0)").attr("checked") == "checked") {
                var obj = new Object();
                obj.Id = 0;
                obj.PaperID = 0;
                obj.CharpterID = $(n).find("td:eq(0) input:eq(1)").val();
                obj.Count = $(n).find("td:eq(3) input").val();
                obj.Score = $(n).find("td:eq(4) input").val();
                QuestionsHelper.Add(obj);
            }
        });
    });

    //保存试卷
    $("#btnSaveTestPapers").unbind("click").click(function () {
        //页面字段检测
        if (!VerificationHelper.checkFrom("ContextDev")) {
            return;
        }
        if (TestPapersHelper.GetList().length <= 0) {
            dialogHelper.Error({
                content: "请选择试题来源！",
                success: function () { }
            });
            return;
        }
        var FormType = $("#FormType").val();
        if (FormType == "自动组卷" && QuestionsHelper.GetList().length <= 0) {
            dialogHelper.Error({
                content: "请选择题型！",
                success: function () { }
            });
            return;
        }
        var Paper = new Object();
        Paper.Id = $("#TestPapersId").val();
        //Paper.ExamPaperName = $("#ExamPaperName").val();
        //Paper.StartDate = $("#StartDate").val();
        //Paper.EndDate = $("#EndDate").val();
        Paper.TotalScore = $("#TotalScore").text();
        var FormTypeName = $("#FormType").val();
        var FormType = 1;
        if (FormTypeName == "手动组卷")
            FormType = 2;
        Paper.FormType = FormType;
        //Paper.ClassList = ClassHelper.GetList();
        Paper.CharpterList = TestPapersHelper.GetList();
        Paper.ScoreInfo = QuestionsHelper.GetList();
        var competitionObj = new Object();//实训考核/销售机会发布班级
        competitionObj["CompetitionId"] = $.getUrlParam("MatchId");//竞赛ID        
        CompetitionHelper.Add(competitionObj);
        Paper.CompetitionList = CompetitionHelper.GetList();
        Paper.CompetitionId = $.getUrlParam("MatchId");
        //保存对象
        $.ajax({
            url: "/CompetitionAdmin/Paper/SaveTestPapers",
            async: false,
            type: "POST",
            data: {
                Paper: Paper
            },
            success: function (data) {
                if (data != null) {
                    $("#TestPapersId").val(data.Id);
                    dialogHelper.Success({
                        content: "保存成功！",
                        success: function () {
                            flag = true;
                            $("#FormType").attr("disabled", true);
                            $("#btnSaveTestPapers").hide();
                            Confirm();
                            $("#ScoreList").show();

                            //试题库
                            if (data.Details != null && data.Details.length > 0) {
                                $.each(data.Details, function (i, n) {
                                    AlreadyQuestionsHelper.Add(n);
                                });
                            }
                        }
                    });
                }
            }
        });

    });


    //$("#KeyWord").unbind("focus").focus(function () {
    //    $("#KeyWord").val("").css("color", "black");
    //});

    //$("#KeyWord1").unbind("focus").focus(function () {

    //    $("#KeyWord1").val("").css("color", "black");
    //});
    //全选
    $("#CheckboxSelAll").unbind().click(function () {
        if ($("#CheckboxSelAll").attr("checked") != "checked") {
            $("#TestPapersList :checkbox").attr("checked", false);//全不选  
            //输入框移除验证
            $("#TestPapersList :text").removeClass("IsRequired");
            //总分置为0
            $("#TotalScore").text(0);
        } else {
            $("#TestPapersList :checkbox").attr("checked", true);//全选  
            //输入框添加验证
            $("#TestPapersList :text").addClass("IsRequired");
            var Total = 0;
            $("#TestPapersList tr").each(function (i, n) {
                Total += $(n).find("td:eq(5)").text() * 1;
            });
            $("#TotalScore").text(Total);
        }
    });
    //待选题库全选
    $("#QuestionBankSelAll").unbind().click(function () {
        if ($("#QuestionBankSelAll").attr("checked") != "checked") {
            //对已添加的考题不去除全选 
            $("#QuestionList :checkbox").each(function (index, dom) {
                if ($(dom).attr("disabled") != "disabled") {
                    $(dom).attr("checked", false);//不选 
                }
            });
            WaitSelQuestionsHelper.RemoveAll();
        } else {
            var PapersId = $("#TestPapersId").val();
            $("#QuestionList :checkbox").attr("checked", true);//全选  
            $("#QuestionList tr").each(function (i, n) {
                if ($(n).find("td:eq(0) :input").attr("checked") == "checked" && $(n).find("td:eq(0) :input").attr("disabled") != "disabled")//$(n).find("td:eq(0) :input").attr("disabled") != "disabled" &&
                {
                    var obj = new Object();
                    obj.QuesionId = $(n).find("td:eq(0) :input").attr("flag");
                    obj.ExamPaperId = PapersId;
                    WaitSelQuestionsHelper.Add(obj);
                }
            });
        }
    });

    //输入框绑定改变事件
    $("#TestPapersList :text").live("blur", function () {
        var Score = 1;
        $(this).parents("tr").find("input[type='text']").each(function (i, n) {
            if ($(n).val() * 1 > 0) {

                Score = Multiplication(Score, $(n).val());
                //Score *= $(n).val() * 1;
            } else {
                Score = 0;
            }
        });
        $(this).parents("tr").find("td:eq(5)").text(Score);
        ReCalculation();
    });
    //输入框绑定改变事件
    $("#QuestionsList :text").live("blur", function () {
        var Score = 0;
        var Num = $(this).parents("tr").find("td:eq(1)").text() * 1;
        var value = $(this).val() * 1;
        if (value > 0) {
            Score = Multiplication(value, Num);
            //Score = value * Num;
        }
        $(this).parents("tr").find("td:eq(3)").text(Score);
        var Total = 0;
        //计算总分
        $("#QuestionsList tr").each(function (i, n) {
            Total += $(n).find("td:eq(3)").text() * 1;
        });
        $("#TotalScore1").text(Total);
    });
    //待选确定
    $("#WaitSelQuestion").unbind().click(function () {
        var PapersId = $("#TestPapersId").val() * 1;
        if (PapersId == 0) {
            dialogHelper.Error({
                content: "请先保存基本信息！",
                success: function () { }
            });
            return;
        }
        //获取所有勾选题保存
        $("#QuestionsList tr").each(function (index, dom) {
            var td = $(dom).find("td:eq(0) :input");
            if (td.attr("checked") == "checked" && td.attr("disabled") != "disabled") {
                var obj = new Object();
                obj.QuesionId = td.attr("flag");
                obj.ExamPaperId = PapersId;
                WaitSelQuestionsHelper.Add(obj);
            }
        });
        //判断是否有选中对象
        if (WaitSelQuestionsHelper.GetList().length <= 0) {
            dialogHelper.Error({
                content: "请选择要添加试题！",
                success: function () { }
            });
            return;
        }
        $(WaitSelQuestionsHelper.GetList()).each(function (i, n) {
            AlreadyQuestionsHelper.Add(n);
        });

        //将已添加的加入到待添加列表中 
        $.ajax({
            url: "/CompetitionAdmin/Paper/SaveWaitSelQuestions",
            async: false,
            type: "POST",
            data: {
                list: AlreadyQuestionsHelper.GetList(),
                list1: WaitSelQuestionsHelper.GetList()
            },
            success: function (data) {
                if (data != null) {
                    //试题库
                    if (data.Details != null && data.Details.length > 0) {
                        WaitSelQuestionsHelper.RemoveAll();
                        //关闭对话框 
                        dialogHelper.Close("popQuestionBankSelect");
                        //加载已选题库列表
                        ShowQuestionsList("", "");
                    }
                }
            }
        });
    });
})
//发布
function publishPaper() {
    //判断是否存在
    var PaperID = $("#TestPapersId").val() * 1;
    if (PaperID == 0) {
        dialogHelper.Error({
            content: "请先保存试卷基本信息！",
            success: function () { }
        });
        return;
    }
    //检测勾选行输入内容校验
    if (!VerificationHelper.checkFrom("QuestionsList"))
        return;
    dialogHelper.Confirm({
        content: "确认是否发布？",
        afterSuccess: function () {
            //检测paper.ScoreInfo表中对应题库总数量是否与paper.Details表的数量是否一致,如不一致则更新paper.ScoreInfo对应题型的数量
            var result = 1;
            $.ajax({
                url: "/CompetitionAdmin/Paper/CheckNum",
                type: "POST",
                async: false,
                data: {
                    PaperID: PaperID
                },
                success: function (data) {
                    result = data;
                }
            });
            if (result == 1) {//考题数量相同
                Release(PaperID, false)
            } else if (result == 2) {//考题数量不相同 
                dialogHelper.Confirm({
                    content: "系统检测到当前考题数量与设置的题量不一致！是否继续发布？",
                    success: function () {
                        Release(PaperID, true);
                    }
                });

            } else {
                dialogHelper.Error({
                    content: "发布失败！系统未找到该试卷，请尝试刷新。",
                    success: function () {
                    }
                });
                return;
            }
        }
    });
}
// 返回按钮
function ComeBackb() {
    var MatchId = $.getUrlParam("MatchId");
    var MatchType = $.getUrlParam("MatchType");
    location.href = "/CompetitionAdmin/Paper/MatchContent?MatchId=" + MatchId + "&MatchType=" + MatchType;
}
//IsUpdate为True时需要重新更新考题数量
function Release(PaperID, IsUpdate) {
    $.ajax({
        url: "/CompetitionAdmin/Paper/ReleasePaper",
        type: "POST",
        data: {
            PaperID: PaperID,
            IsUpdate: IsUpdate
        },
        success: function (data) {
            dialogHelper.Success({
                content: "发布成功！",
                success: function () {
                    location.href = "/CompetitionAdmin/Paper/Papers";
                }
            });
        }
    });
}

//加载试卷
function LodingTestPapers(PapersId) {
    $.ajax({
        url: "/CompetitionAdmin/Paper/GetTestPapersInfo",
        async: false,
        type: "POST",
        data: { PapersId: PapersId },
        success: function (data) {
            if (data != null) {
                $("#ExamPaperName").html(data.ExamPaperName);
                //$("#StartDate").val(data.strStartDate);
                $//("#EndDate").val(data.strEndDate);
                var FormType = "自动组卷";
                if (data.FormType == 2) {
                    FormType = "手动组卷";
                }
                $("#FormType").val(FormType);
                //分值
                if (data.ScoreInfo != null && data.ScoreInfo.length > 0) {
                    $.each(data.ScoreInfo, function (i, n) {
                        QuestionsHelper.Add(n);
                    });
                    GenerationHtml(data.ScoreInfo);
                }
                ////班级
                //if (data.ClassList != null && data.ClassList.length > 0) {
                //    $.each(data.ClassList, function (i, n) {
                //        ClassHelper.Add(n);
                //    });
                //    //显示选择的班级
                //    ShowContext("SelShowClass", ClassHelper.GetList(), "ClassName");
                //}
                //章节
                if (data.CharpterList != null && data.CharpterList.length > 0) {
                    $.each(data.CharpterList, function (i, n) {
                        var obj = new Object();
                        obj["CharpterID"] = data.CharpterList[i].CharpterID;
                        obj["Name"] = data.CharpterList[i].Name;
                        TestPapersHelper.Add(obj);
                    });
                    //显示选择的班级
                    ShowContext("SelShowTestPaper", TestPapersHelper.GetList(), "Name");
                }
                //试题库
                if (data.Details != null && data.Details.length > 0) {
                    $.each(data.Details, function (i, n) {
                        AlreadyQuestionsHelper.Add(n);
                    });
                }
            }
        }
    });
}
//生成列表
function GenerationHtml(data) {
    var html = "";
    var Total = NumScore = 0;
    $.each(data, function (i, n) {
        NumScore = n.Count * n.Score;
        Total += NumScore;
        html += "<tr> <td>" + n.CharpterName + "</td>";
        html += " <td>" + n.Count + "</td>";
        html += " <td> <input id=\"Score" + i + "\" flag=\"" + n.Id + "\" flag1=\"" + n.CharpterID + "\" style=\"width:140px;height:80%;\" msgreg=\"请输入分值\" msgname=\"分值\" class=\"ipt-text grid-12 IsRequired IsNumber IsMaxNumber IsMinNumber\" maxnumber=\"50\" minnumber=\"1\" value=\"" + n.Score + "\" type=\"text\" ></td> <td>" + NumScore + "</td> </tr> ";
    });
    $("#QuestionsList").html("").append(html);
    $("#TotalScore1").text(Total);
}

//屏蔽班级选择框
function ShieldPop(Type, PopId) {
    if (Type == "Views") {
        if (PopId == "popTrainingClass") {
            //班级
            var classList = $("#popTrainingClass #A_ul").find("input");
            $.each(classList, function (i, c) {
                $(c).attr("disabled", "disabled");
            });
        } else if (PopId == "popQuestions") {
            //试题库
            var classList = $("#popQuestions #A_ul1").find("input");
            $.each(classList, function (i, c) {
                $(c).attr("disabled", "disabled");
            });
        } else {
            //题型 
            $("#TestPapersList").find("input[type='checkbox'],input[type='text']").attr("disabled", true);
            $("#AddQuestionsSel").hide();
        }
    }
}

//题库选择
function QuestionsSelect() {
    //当查看状态时控件屏蔽
    var Type = $.getUrlParam("Status");
    if (Type == "Add" && !flag) {
        var QuestionNum = 0, SelectQuestionNum = 0;
        // 生成章节列表Select
        QuestionNum = GetQuestionsList();
        //2、显示设置
        dialogHelper.Show('popQuestions', 350);
        //3、加载当前设置该保存项
        var PaperId = $("#TestPapersId").val();
        if (PaperId != "0") {
            $.ajax({
                url: "/CompetitionAdmin/Paper/GetQuestionsList",
                async: false,
                type: "POST",
                data: {
                    PaperId: PaperId,
                },
                success: function (data) {
                    if (data.Data.CharpterList != "" || data.Data.CharpterList != null) {
                        $.each(data.CharpterList, function (i, n) {
                            TestPapersHelper.Add(n);
                        });
                    }
                }
            });
        }
        //选中集合中被添加的试题库
        var trList = TestPapersHelper.GetList();
        $.each(trList, function (i, n) {
            $("#checkbox1_" + n.CharpterID).attr("checked", true);
        });
        SelectQuestionNum = trList.length;
        $("#popQuestions #TiKuSelectAll").attr("checked", false);
        if (QuestionNum > 0 && SelectQuestionNum > 0 && QuestionNum == SelectQuestionNum) {
            $("#popQuestions #TiKuSelectAll").attr("checked", true);
        }
        //当查看状态时控件屏蔽
        var Type = $.getUrlParam("Status");
        ShieldPop(Type, "popTestSet");

    }
}

//单击班级确定按钮
function AddClass() {
    $("#popTrainingClass li span input[type='checkbox']").each(function (i, n) {
        if ($(n).attr("checked") == "checked") {
            var obj = new Object();//实训考核/销售机会发布班级
            obj["ClassId"] = $(n).attr("title");//班级ID        
            obj["ClassName"] = $(n).attr("value");//班级名称 
            ClassHelper.Add(obj);
        } else {
            ClassHelper.Remove($(n).attr("title"));
        }
    });
    $("#popTrainingClass").hide();
    //显示选择的班级
    ShowContext("SelShowClass", ClassHelper.GetList(), "ClassName");
}
//Id:要显示的Id元素
//显示选择内容
function ShowContext(Id, Value, text) {
    var ShowContext = "";
    if (Value.length > 0) {
        if (Value[0][text] == undefined) {
            dialogHelper.Error({
                content: "显示字段错误,未找到" + text + "属性内容",
                success: function () { }
            });
            return;
        }
        $.each(Value, function (i, n) {
            ShowContext += Value[i][text] + ",";
        });
        ShowContext = ShowContext.substr(0, ShowContext.length - 1);
    }
    $("#" + Id).text(ShowContext);
    $("#" + Id).attr("title", ShowContext);
}
//题库列表
function GetQuestionsList() {
    var QuestionNum = 0;
    $.ajax({
        url: "/CompetitionAdmin/Paper/GetSelectQuestions",
        async: false,
        type: "POST",
        success: function (data) {
            var html = "";
            QuestionNum = data.length;
            $.each(data, function (i, n) {
                html += '<li><span class="icheckbox" value="' + n.Id + '"><input class="icheck" type="checkbox" field="Class" title="' + n.Id + '" id="checkbox1_' + n.Id + '" value="' + n.ChapterName + '">' + n.ChapterName + '</span></li>';
            });
            $("#A_ul1").html(html);
        }
    });
    return QuestionNum;
}

//单击题库选择
function AddQuestions() {
    // 判断是否选择了题库
    var selCount = 0;
    $("#popQuestions li span input[type='checkbox']").each(function (i, n) {
        if ($(n).attr("checked") == "checked") {
            selCount++;
        }
    });

    if (selCount == 0) {
        dialogHelper.Error({
            content: '请选择试题！'
        })
    }
    else {
        $("#popQuestions li span input[type='checkbox']").each(function (i, n) {
            if ($(n).attr("checked") == "checked") {
                var obj = new Object();
                obj["CharpterID"] = $(n).attr("title");//试题库ID        
                obj["Name"] = $(n).attr("value");//试题库名称 
                TestPapersHelper.Add(obj);
            } else {
                TestPapersHelper.Remove($(n).attr("title"));
            }
        });
        dialogHelper.Close("popQuestions");
        //显示选择的班级
        ShowContext("SelShowTestPaper", TestPapersHelper.GetList(), "Name");
    }
}

//试题列表全选
function SelectAll(val) {
    if ($(val).attr("checked") != "checked") {
        $("#A_ul1 :checkbox").attr("checked", false);//全不选    
    } else {
        $("#A_ul1 :checkbox").attr("checked", true);//全选      
    }
}

function TestSet() {
    //当查看状态时控件屏蔽
    var Type = $.getUrlParam("Status");
    if (Type == "Add" && !flag) {
        var trList = TestPapersHelper.GetList();
        if (trList.length <= 0) {
            dialogHelper.Error({
                content: "请先选择试题来源！",
                success: function () { }
            });
            return;
        }
        var strList = "";
        $.each(trList, function (i, n) {
            strList += n.CharpterID + ",";
        });
        strList = strList.substr(0, strList.length - 1);
        //题型加载
        GetOriginalTiXingList(strList)
        dialogHelper.Show('popTestSet', 800);
        //加载原保存值
        GetTiXingList();
        //选中集合数据初始化至题型列表
        var trList = QuestionsHelper.GetList();

        $.each(trList, function (i, n) {
            $("#TestPapersList tr").find("td:eq(0) input:eq(1)").each(function (y, z) {
                if ($(z).val() == n.CharpterID) {
                    $(z).parents("tr").find("td:eq(0) input:eq(0)").attr("checked", true);//选中  
                    $(z).parents("tr").find("td:eq(3) :input").val(n.Count);//题量
                    $(z).parents("tr").find("td:eq(4) :input").val(n.Score);//分值 
                    $(z).parents("tr").find("td:eq(5)").text(n.Score * n.Count);//分值 
                }
            });
        });
        //当查看状态时控件屏蔽
        var Type = $.getUrlParam("Status");
        ShieldPop(Type, "popTestSet");
        ReCalculation();
    }
}

//题型加载
function GetOriginalTiXingList(strList) {
    $.ajax({
        url: "/CompetitionAdmin/Paper/GetOriginalTiXingList",
        async: false,
        type: "POST",
        data: { PaperCharpterId: strList },
        success: function (data) {
            var html = "";
            $.each(data, function (i, n) {
                html += " <tr> <td><input type=\"checkbox\" onclick=\"checkboxSel(this)\"> <input type=\"hidden\" id=\"CharpterID" + i + "\" value=\"" + n.IdList + "\" /> </td> ";
                html += " <td><div class=\"ellipsis\" title='" + n.TypeName + "'>" + n.TypeName + "</div></td> <td>" + n.TotalNum + "</td> ";
                html += " <td><input id=\"Num" + i + "\" style=\"width:140px;height:80%;\" msgreg=\"请输入题量\" msgname=\"题量\" class=\"ipt-text grid-12 IsNumber IsMinNumber \" maxnumber=\"" + QuestionsNum(n.TotalNum, n.TypeName) + "\" minnumber=\"1\"  type=\"text\" ></td>";
                html += " <td> <input id=\"Score" + i + "\" style=\"width:140px;height:80%;\" msgreg=\"请输入分值\" msgname=\"分值\" class=\"ipt-text grid-12 IsNumber IsMaxNumber IsMinNumber\" maxnumber=\"50\" minnumber=\"1\" type=\"text\" ></td> <td>0</td> </tr> ";
            });
            $("#TestPapersList").html("").append(html);
        }
    });
}

function GetTiXingList() {
    var PaperId = $("#TestPapersId").val();
    if (PaperId != "0") {
        $.ajax({
            url: "/CompetitionAdmin/Paper/GetTiXingList",
            async: false,
            type: "POST",
            data: {
                PaperId: PaperId,
            },
            success: function (data) {
                if (data.Data != "" || data.Data != null) {
                    $.each(data, function (i, n) {
                        QuestionsHelper.Add(n);
                    });
                }
            }
        });
    }
}

function checkboxSel(val) {
    if ($(val).attr("checked") != "checked") {
        //输入框移除验证
        $(val).parents("tr").find("input[type='text']").removeClass("IsRequired");
    } else {
        //输入框添加验证
        $(val).parents("tr").find("input[type='text']").addClass("IsRequired");
    }
    //合计
    ReCalculation();
}
//重新计算
function ReCalculation() {
    var Total = 0;
    $("#TestPapersList tr").each(function (i, n) {
        if ($(n).find("td:eq(0) input[type=checkbox]").attr("checked") == "checked") {
            Total += $(n).find("td:eq(5)").text() * 1;
        }
    });
    $("#TotalScore").text(Total);
}

//题量输入限制最大值
//Num:当前章节题库总数量
//Name：章节名称
function QuestionsNum(Num, Name) {
    if (Name == "单选题" || Name == "多选题" || Name == "判断题") {
        if (Num > 100)
            return 100;
        else
            return Num;
    } else {
        if (Num > 20)
            return 20
        else
            return Num;
    }
}

//组卷方式
function PaperMethod(Id) {
    if ($(Id).val() == "自动组卷") {
        $("#TiXingGuanLi").show();
    } else {
        $("#TiXingGuanLi").hide();
        QuestionsHelper.RemoveAll();
    }
}

//题库列表弹出框内容加载
function EditQuestionsLoading() {
    //判断试题来源是否为空
    var trList = TestPapersHelper.GetList();
    var FormType = $("#FormType").val();
    if (FormType == "自动组卷" && trList.length <= 0) {
        dialogHelper.Error({
            content: "请先选择试题来源！",
            success: function () { }
        });
        return;
    }
    var strList = "";
    $.each(trList, function (i, n) {
        strList += n.CharpterID + ",";
    });
    strList = strList.substr(0, strList.length - 1);
    //考试名称
    $("#TestPaperName").html($("#ExamPaperName").html());
    var myFun = function () { Confirm(); }
    dialogHelper.Show('popQuestionBankList', 800, myFun);
    //题型下拉列表   
    selectHelper.GetSelect({
        url: "/CompetitionAdmin/Paper/TiXingSelList",
        Id: "#TiXingSel",
        value: "全部",
        data: { strList: strList }
    });
    var PaperId = $("#TestPapersId").val() * 1;
    if (PaperId != 0) {
        //获取题型下拉列表(编辑题目时)           
        SelectTitle(strList);

        //加载列表
        ShowQuestionsList("", "");
    }

}
//搜索
function Search() {
    var TiXing = $("#TiXingSel").val();
    if (TiXing == "0") {
        TiXing = "";
    }
    var KeyWord = $("#KeyWord").val().replace(/[<>&"]/g, function (c) { return { '<': '&lt;', '>': '&gt;', '&': '&amp;', '"': '&quot;', '"': '&quot;', "'": '&prime;', "'": '&prime;' }[c]; });
    if (KeyWord == "题干") { KeyWord = ""; }
    ShowQuestionsList(TiXing, KeyWord);
}
//弹出题库列表显示内容
function ShowQuestionsList(Value, KeyWord) {
    var PapersId = $("#TestPapersId").val();
    //列表显示
    pageHelper.Init({
        url: "/CompetitionAdmin/Paper/QuestionsList",
        type: "POST",
        pageDiv: "#QuestionBankPage",
        data:
        {
            PapersId: PapersId,
            TiXingId: Value,
            KeyWords: KeyWord,
            rId: Math.random()
        },
        bind: function (data) {
            var html = "";
            $(data.Data).each(function (index, dom) {
                //每行html
                var trHtml = "";
                trHtml += "<tr>";
                trHtml += "<td name=\"dataNo\">{0}</td>";
                trHtml += "<td align=\"left\"><div title=\"{1}\" class=\"ellipsis\">{1}</div></td>";
                trHtml += "<td><div title=\"{2}\" class=\"ellipsis\">{2}</div></td>";
                trHtml += "<td class=\"operate\">";
                trHtml += "<a class=\"edit\" title=\"编辑\" href=\"javascript:EditTopic({3},'{2}',{0});\">编辑</a>";
                trHtml += "<a class=\"del\" title=\"删除\" href=\"javascript:DelTopic({3},{4});\">删除</a>";
                trHtml += "</td>";
                trHtml += "</tr>";
                //拼接tbody
                html += StringHelper.FormatStr(trHtml,
                    ((data.PageIndex - 1) * data.PageSize + index + 1),   //0 序号
                    dom.Context,                                          //1 题干
                    dom.CharpterName,                                     //2 题型 
                    dom.Id,                                                //3 Id  
                    dom.CharpterID,                                         //4
                    dom.StructType                                           //5

                    );
            });
            $("#QuestionBankList").html(html);
            dialogHelper.Reset("popQuestionBankList");
        }
    });
    //显示已选择内容及数量
    var PaperId = $("#TestPapersId").val();
    $.ajax({
        url: "/CompetitionAdmin/Paper/GetSelNum",
        async: false,
        type: "POST",
        data: {
            PaperId: PaperId,
        },
        success: function (data) {
            if (data != null) {
                var strContext = "已选择";
                $.each(data, function (i, d) {
                    strContext += d.CharpterName + d.Num + "题,";
                })
                strContext = strContext.substring(0, strContext.lastIndexOf(","));
                $("#ShowMsg").text(strContext);
            }
        }
    });
}

//编辑题目
function EditTopic(Value, CharpterName, CurQuestionCount) {

    //查看是否是内置题  
    var bo = false;
    $.ajax({
        url: "/CompetitionAdmin/Paper/CheckQuestionSource",
        async: false,
        type: "POST",
        data: {
            Id: Value,
        },
        success: function (data) {
            if (data) {
                bo = true;
            }
        }
    });

    if (!bo) {
        //弹出框
        dialogHelper.Show('popQuestionBankAdd', 800);

        //关闭按钮
        $("#close1").unbind("click").click(function () {
            ComeBack();
        });

        $("#Context").attr('disabled', false);
        $("#Analysis").attr('disabled', false);
        $("#btnAdd").attr('disabled', false);
        $("#preserve").val("保存");

        $("#QuestionIdAdd1").val(Value);
        //试卷名称
        $("#charpterName").html($("#ExamPaperName").html());
        //获取题号
        $("#CurQuestionCount").html(CurQuestionCount);

        $("#PaperId").val(Value);

        var html = "<option>" + CharpterName + "</option>";
        $("#TypeName").html(html);

        //屏蔽按钮
        $("#upward").attr('disabled', true);
        $("#upward").addClass('btndisable');

        $("#down").attr('disabled', true);
        $("#down").addClass('btndisable');
        //加载试题
        LoadTitle(Value);


    } else {
        dialogHelper.Error({ content: "内置题目不能进行编辑！", success: function () { } });
        return;
    }

}



//删除题目
function DelTopic(Value, CharpterID) {
    dialogHelper.Confirm({
        content: "确认是否删除？",
        success: function () {
            $.ajax({
                url: "/CompetitionAdmin/Paper/DelTopic",
                type: "POST",
                data: {
                    PaperId: $("#TestPapersId").val(),
                    Id: Value,
                    CharpterID: CharpterID
                },
                success: function (data) {
                    Search();
                    AlreadyQuestionsHelper.Remove(Value);
                }
            });
        }
    });
}

//题库列表确认
function Confirm() {
    //重新加载列表
    $.ajax({
        url: "/CompetitionAdmin/Paper/GetTestPapersInfo",
        async: false,
        type: "POST",
        data: { PapersId: $("#TestPapersId").val() },
        success: function (data) {
            if (data != null) {
                GenerationHtml(data.ScoreInfo);
            }
        }
    });
    //关闭窗口
    dialogHelper.Close("popAddTestPaper");
    dialogHelper.Close("popQuestionBankList");
    //$("#popAddTestPaper").hide();
    //$("#bgpopQuestionBankList").remove();
}
//题库选择
function TiKuSel() {
    //清空待添加题库列表
    WaitSelQuestionsHelper.RemoveAll();
    //判断试题来源是否为空
    var trList = TestPapersHelper.GetList();
    var strList = "";
    $.each(trList, function (i, n) {
        strList += n.CharpterID + ",";
    });
    strList = strList.substr(0, strList.length - 1);
    //题型下拉列表   
    selectHelper.GetSelect({
        url: "/CompetitionAdmin/Paper/TiXingSelList",
        Id: "#TiXingSel1",
        value: "全部",
        data: { strList: strList }
    });
    dialogHelper.Show('popQuestionBankSelect', 800);
    var zhangJieId = "";
    $("#TiXingSel1 option:not(:first)").each(function (i, n) {
        zhangJieId += $(n).val() + ",";
    });
    if (zhangJieId.length > 0) {
        zhangJieId = zhangJieId.substr(0, zhangJieId.length - 1);
    }
    //默认加载列表并且为已添加的试题打上勾
    QuestionsWaiting(zhangJieId, "");
    //显示当前用户所有可选章节
    GetChapterName();

}
//获取所有章节名称
function GetChapterName() {
    var PaperId = $("#TestPapersId").val();
    $.ajax({
        url: "/CompetitionAdmin/Paper/GetChapterName",
        async: false,
        type: "POST",
        data: { PaperId: PaperId },
        success: function (data) {
            if (data != null) {
                $("#ChapterName").text(data);
                $("#ChapterName").attr("title", data);
            }
        }
    });
}
//待选择题库
function QuestionsWaiting(TiXingId, Keyword) {
    //列表显示
    pageHelper.Init({
        url: "/CompetitionAdmin/Paper/GetQuestionsLibraryList",
        type: "POST",
        pageDiv: "#QuestionPage",
        data:
        {
            TiXingId: TiXingId,
            KeyWords: Keyword,
            rId: Math.random()
        },
        bind: function (data) {
            var html = "";
            $(data.Data).each(function (index, dom) {
                ////每行html
                var trHtml = "";
                trHtml += "<tr>";
                var AlreadyQuestions = AlreadyQuestionsHelper.Find(dom.Id);//已添加的题库 
                if (AlreadyQuestions >= 0) {
                    trHtml += "<td><input id=\"chk_{3}\" type=\"checkbox\" onclick=\"QuestionsSel(this)\" checked=\"checked\" disabled=\"disabled\" flag=\"{3}\"></td>";
                } else {
                    var WaitSelQuestions = WaitSelQuestionsHelper.Find(dom.Id);//待添加的题库
                    if (WaitSelQuestions >= 0) {
                        trHtml += "<td><input type=\"checkbox\" onclick=\"QuestionsSel(this)\" checked=\"checked\" flag=\"{3}\"></td>";
                    } else {
                        trHtml += "<td><input type=\"checkbox\" onclick=\"QuestionsSel(this)\" flag=\"{3}\"></td>";
                    }
                }
                trHtml += "<td name=\"dataNo\">{0}</td>";
                trHtml += "<td align=\"left\"><div title=\"{1}\" class=\"ellipsis\">{1}</div></td>";
                trHtml += "<td><div title=\"{2}\" class=\"ellipsis\">{2}</div></td>";
                trHtml += "</tr>";
                //拼接tbody
                html += StringHelper.FormatStr(trHtml,
                    ((data.PageIndex - 1) * data.PageSize + index + 1),   //0 序号
                    dom.Context,                                          //1 题干
                    dom.CharpterName,                                     //2 题型 
                    dom.Id                                                //3 Id   
                    );
            });
            $("#QuestionList").html(html);
            if ($("#QuestionList :checkbox[checked='checked']").length == $("#QuestionList :checkbox").length) {
                $("#QuestionBankSelAll").attr("checked", true);

            }
            else { $("#QuestionBankSelAll").attr("checked", false); }
            dialogHelper.Reset("popQuestionBankSelect");
        }
    });

}
//查询
function ChapterSearch() {
    var TiXingSel = $("#TiXingSel1").val();
    if (TiXingSel == "0") {
        $("#TiXingSel1 option:not(:first)").each(function (i, n) {
            TiXingSel += $(n).val() + ",";
        });
        TiXingSel = TiXingSel.substr(0, TiXingSel.length - 1);

    }
    var KeyWord = $("#KeyWord1").val().replace(/[<>&"]/g, function (c) { return { '<': '&lt;', '>': '&gt;', '&': '&amp;', '"': '&quot;', '"': '&quot;', "'": '&prime;', "'": '&prime;' }[c]; });
    if (KeyWord == "题干") { KeyWord = ""; }
    QuestionsWaiting(TiXingSel, KeyWord);
}

function QuestionsSel(val) {
    if ($("#QuestionList :checkbox[checked='checked']").length == $("#QuestionList :checkbox").length) {
        $("#QuestionBankSelAll").attr("checked", true);
    } else {
        $("#QuestionBankSelAll").attr("checked", false);
    }

    if ($(val).attr("checked") != "checked") {

        $(val).attr("checked", false);//全不选 
        WaitSelQuestionsHelper.Remove($(val).attr("flag"));
    } else {
        $(val).attr("checked", true);//全不选 
        var PapersId = $("#TestPapersId").val();
        var obj = new Object();
        obj.QuesionId = $(val).attr("flag");
        obj.ExamPaperId = PapersId;
        WaitSelQuestionsHelper.Add(obj);
    }
}

