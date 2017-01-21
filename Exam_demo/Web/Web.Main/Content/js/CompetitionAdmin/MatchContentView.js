var TestPapersHelper = new arrayHelper("CharpterID");//试题来源
var AlreadyQuestionsHelper = new arrayHelper("QuesionId");//已添加试题

var ExamCase = new arrayHelper("IDNum");                        //案例
var ClassHelper = new arrayHelper("ClassId");                   //班级
var answerHelper = new arrayHelper("ExamPointId");              //答案
var TrainExamDetailHelper = new arrayHelper("ExamPointId");     //考核点详细信息
var tempDetail = new arrayHelper("ExamPointId");                //临时存放考点信息

// 根据竞赛Id获取理论试卷Id
function GetPaperId(MatchId) {
    var paperId = 0;
    $.ajax({
        url: "/CompetitionAdmin/Paper/GetPaperId",
        type: "POST",
        async: false,
        dataType: "json",
        data: { MatchId: MatchId },
        success: function (data) {
            paperId = data.Data.PaperId;
        }
    });
    return paperId;
}

function LodingTestPapers(PapersId) {
    var MatchId = $("#hideMatchId").val();
    var MatchType = $("#hideType").val();
    $.ajax({
        url: "/CompetitionAdmin/Paper/GetTestPapersInfo",
        async: false,
        type: "POST",
        data: { PapersId: PapersId },
        success: function (data) {
            if (data != null && data.Data != null) {
                var curData = data.Data;
                //分值
                if (curData.ScoreInfo != null && curData.ScoreInfo.length > 0) {
                    GenerationHtml(curData.ScoreInfo);

                    //章节
                    if (curData.CharpterList != null && curData.CharpterList.length > 0) {
                        $.each(curData.CharpterList, function (i, n) {
                            var obj = new Object();
                            obj["CharpterID"] = curData.CharpterList[i].CharpterID;
                            obj["Name"] = curData.CharpterList[i].Name;
                            TestPapersHelper.Add(obj);
                        });
                    }
                    //试题库
                    if (curData.Details != null && curData.Details.length > 0) {
                        $.each(curData.Details, function (i, n) {
                            AlreadyQuestionsHelper.Add(n);
                        });
                    }

                    $("#addMatchPaper").attr("href", "javascript:void(0)");
                    $("#addMatchPaper").addClass("btn-unuse");

                    $("#btnEditQuestions").attr("href", "/CompetitionAdmin/Paper/TheoryPaperAdd?Status=Edit&PapersId=" + PapersId + "&MatchId=" + MatchId + "&MatchType=" + MatchType);
                    $("#btnEditQuestions").removeClass("btn-unuse");
                }
            }
            else {
                var html = "";
                html += "<tr><td>单选题</td><td>0</td><td>0</td><td>0</td></tr>";
                html += "<tr><td>多选题</td><td>0</td><td>0</td><td>0</td></tr>";
                html += "<tr><td>判断题</td><td>0</td><td>0</td><td>0</td></tr>";
                $("#QuestionsList").html("").append(html);
                $("#TotalScore1").text(0);

                $("#addMatchPaper").attr("href", "/CompetitionAdmin/Paper/TheoryPaperAdd?Status=Add&MatchId=" + MatchId + "&MatchType=" + MatchType);
                $("#addMatchPaper").removeClass("btn-unuse");

                $("#btnEditQuestions").attr("href", "javascript:void(0)");
                $("#btnEditQuestions").addClass("btn-unuse");
            }
        }
    });
}

// 生成列表
function GenerationHtml(data) {
    var html = "";
    var Total = NumScore = 0;
    $.each(data, function (i, n) {
        NumScore = n.Count * n.Score;
        Total += NumScore;
        html += "<tr> <td>" + n.CharpterName + "</td>";
        html += " <td>" + n.Count + "</td>";
        html += " <td>" + n.Score + "</td> <td>" + NumScore + "</td> </tr> ";
    });
    $("#QuestionsList").html("").append(html);
    $("#TotalScore1").text(Total);
}

// 实训案例列表
function LodingTrainCase(MatchId) {
    pageHelper.Init({
        url: "/CompetitionAdmin/Paper/GetTrainExamWithDetail",
        type: "POST",
        pageDiv: "#CasePage",
        data:
        {
            matchId: MatchId
        },
        bind: function (data) {

            var html = "";
            $(data.Data).each(function (index, dom) {
                //每行html
                var trHtml = "";
                trHtml += "<tr>";
                trHtml += "<td>{0}</td>";
                trHtml += "<td><div class=\"ellipsis\" title=\"{1}\">{1}</div></td>";
                trHtml += "<td><div class=\"ellipsis\" title=\"{2}\">{2}</div></td>";
                trHtml += "<td><div class=\"ellipsis\" title=\"{3}\">{3}</div></td>";
                trHtml += "<td><div class=\"ellipsis\" title=\"{4}\">{4}</div></td>";
                trHtml += "<td><div class=\"ellipsis\" title=\"{5}\">{5}</div></td>";
                if (index == 0) {
                    trHtml += "<td><div id='MatchCaseScore' class=\"ellipsis\" title=\"{6}\">{6}</div></td>";
                }
                else {
                    trHtml += "<td><div class=\"ellipsis\" title=\"{6}\">{6}</div></td>";
                }
                trHtml += "<td class=\"operate\">";
                trHtml += '<a href="javascript:void(0)"  onclick="ShowTrainExamDetail(' + dom.Id + ')" title="查看">查看详情</a>';
                trHtml += "</td>";
                trHtml += "</tr>";


                //拼接tbody
                html += StringHelper.FormatStr(trHtml,
                    ((data.PageIndex - 1) * data.PageSize + index + 1),       //0序号
                    dom.Case.CustomerName,                                    //1客户姓名
                    dom.Case.IDNum,                                           //2身份证
                    dom.Case.strFinancialType,                                //3理财类型
                    dom.Case.strCollege,                                      //4案例来源（学校）
                    dom.TrainExamDetail.length,                               //5考核点
                    dom.AllScore,                                             //6总分
                    dom.Id                                                    //7实训考核ID
                    );
            });
            if (data.Data == "" || data.Data == null) {
                html += "<tr><td  colspan='8'>未找到相关记录！</td>";
                $("#TrainCaseList").html(html);
            } else {
                $("#TrainCaseList").html(html);
            }

        }
    });
}

//题库列表弹出框内容加载
function EditQuestionsLoading() {
    //判断试题来源是否为空
    var trList = TestPapersHelper.GetList();
    var strList = "";
    $.each(trList, function (i, n) {
        strList += n.CharpterID + ",";
    });
    strList = strList.substr(0, strList.length - 1);

    var myFun = function () { Confirm(); }
    dialogHelper.Show('popQuestionBankList', 800, myFun);
    //题型下拉列表   
    selectHelper.GetSelect({
        url: "/CompetitionAdmin/Paper/TiXingSelList",
        Id: "#TiXingSel",
        value: "全部",
        data: { strList: strList }
    });
    var PaperId = $("#hidePaperId").val() * 1;
    if (PaperId != 0) {
        //获取题型下拉列表(编辑题目时)           
        SelectTitle(strList);

        //加载列表
        ShowQuestionsList("", "");
    }
}

//弹出题库列表显示内容
function ShowQuestionsList(Value, KeyWord) {
    var PapersId = $("#hidePaperId").val();
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
                trHtml += "<td><div title=\"{6}\" class=\"ellipsis\">{6}</div></td>";
                //trHtml += "<td class=\"operate\">";
                //trHtml += "<a class=\"edit\" title=\"编辑\" href=\"javascript:EditTopic({3},'{2}',{0});\">查看</a>";
                //trHtml += "</td>";
                trHtml += "</tr>";
                //拼接tbody
                html += StringHelper.FormatStr(trHtml,
                    ((data.PageIndex - 1) * data.PageSize + index + 1),   //0 序号
                    dom.Context,                                          //1 题干
                    dom.CharpterName,                                     //2 题型 
                    dom.Id,                                                //3 Id  
                    dom.CharpterID,                                         //4
                    dom.StructType,                                           //5
                    dom.strCollege                                           //6 来源
                    );
            });
            $("#QuestionBankList").html(html);
            dialogHelper.Reset("popQuestionBankList");
        }
    });
    //显示已选择内容及数量
    var PaperId = $("#hidePaperId").val();
    $.ajax({
        url: "/CompetitionAdmin/Paper/GetSelNum",
        async: false,
        type: "POST",
        data: {
            PaperId: PaperId,
        },
        success: function (data) {
            if (data != null && data.Data != null) {
                var strContext = "已选择";
                $.each(data.Data, function (i, d) {
                    strContext += d.CharpterName + d.Num + "题,";
                })
                strContext = strContext.substring(0, strContext.lastIndexOf(","));
                $("#ShowMsg").text(strContext);
            }
        }
    });
}

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

//搜索
function Search() {
    var TiXing = $("#TiXingSel").val();
    if (TiXing == "0") {
        TiXing = "";
    }
    var KeyWord = $("#KeyWord").val().replace(/[<>&"]/g, function (c) { return { '<': '&lt;', '>': '&gt;', '&': '&amp;', '"': '&quot;', '"': '&quot;', "'": '&prime;', "'": '&prime;' }[c]; });
    //if (KeyWord == "题干") { KeyWord = ""; }
    ShowQuestionsList(TiXing, KeyWord);
}

function ShowTrainExamDetail(TrainExamId) {
    //考核点下拉框加载（查看模式）
    selectHelper.GetSelect({
        Id: "#selectExamContent",
        url: "/CompetitionAdmin/Resource/GetExamContentList",
        flag: false,
        changeFun: function (value) {
            GetExamPointList(value, TrainExamId, 1);
        }
    });

    // 清除记录
    ExamCase.RemoveAll();
    tempDetail.RemoveAll();
    TrainExamDetailHelper.RemoveAll();
    answerHelper.RemoveAll();

    SelectTrainExam(TrainExamId);
    ClickTrainExamDetail2(1);
}

//从数据库读取保存的信息
function SelectTrainExam(TrainExamId) {
    $.ajax({
        url: "/CompetitionAdmin/Paper/LoadingTrainExam",
        async: false,
        type: "POST",
        data: {
            Id: TrainExamId,
        },
        success: function (data) {
            if (data.Data.TE != null) {

                //案例
                var Case = data.Data.TE.Case;
                ExamCase.Add(Case);

                //考核点
                var TrainExamDetail = data.Data.TE.TrainExamDetail;
                $.each(TrainExamDetail, function (c, d) {
                    TrainExamDetailHelper.Add(d);
                    tempDetail.Add(d);
                });

                //隐藏域
                $("#Id").val(data.Data.TE.Id);
                $("#txtcaseId").val(data.Data.TE.CaseId);

            }

        }
    });
}

//点击考核点按钮 hzq(查看)
function ClickTrainExamDetail2(Type) {

    var trList = ExamCase.GetList();
    if (trList.length == 0) {
        dialogHelper.Error({ content: "请先添加案例信息！", success: function () { } });
        return false;
    } else {
        //将TrainExamDetailHelper数据复制到tempDetail
        tempDetail.RemoveAll();
        $(TrainExamDetailHelper.GetList()).each(function (index, dom) {
            tempDetail.Add(dom);
        });

        //弹出层
        dialogHelper.Show('popAssessmentSettings', 850);

        //获取被选中的案例    
        trList = ExamCase.GetList();
        $.each(trList, function (i, n) {
            $("#CustomerName").val(n.CustomerName);
            $("#IDNum").val(n.IDNum);
            $("#FinancialTypeName").val(n.strFinancialType);
            $("#CustomerStory").val(n.CustomerStory);

        });

        //var Type = $.getUrlParam("Type");
        //案例id
        var CaseId = $("#txtcaseId").val();

        //是否考虑要不要把获取答案单独封装
        //获取考核点答案
        $.ajax({
            url: "/CompetitionAdmin/Resource/GetCase",
            type: "POST",
            async: false,
            dataType: "json",
            data:
            {
                Id: CaseId,
                rId: Math.random()
            },
            success: function (data) {
                //答案
                $(data.Data.ExamPointAnswer).each(function (index, dom) {
                    answerHelper.Add(dom);
                });
            }
        });

        //单击考核点按钮是生成考核点
        var TrainExamId = $.getUrlParam("TrainExamId");
        GetExamPointList(1, TrainExamId, Type);

        //选中被保存了的考核点
        CheckCheckbox();

        //下拉框默认选中第一个
        $("#selectExamContent").val(1);
    }
}

// 根据考核内容Id，获取考核点
function GetExamPointList(ContentId, TrainExamId, Type) {
    $("#selectAll_").attr("checked", false);
    $.ajax({
        url: "/CompetitionAdmin/Resource/GetExamPointList",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ContentId: ContentId,
            rId: Math.random()
        },
        success: function (data) {
            //生成考核点列表
            GenerationHtmlList(data.Data, TrainExamId, ContentId);
            //选中被保存了的考核点
            CheckCheckbox();
        }

    });
    //在发布后查看多有的文本框禁用
    TextDisplay(Type);
    //查看时，非勾选考核点不显示
    if (Type == 1) {
        $("#ExamContentList input:checkbox").each(function (index, dom) {
            if (!$(dom).is(":checked")) {
                //移除tr
                $(dom).parent().parent().remove();
            }
        });
    }
    //特殊处理，保险规划禁用全选按钮
    if (ContentId == EnumList.ExamContent.InsurancePlan) {
        $("#selectAll_").attr("disabled", true);
    } else {
        $("#selectAll_").attr("disabled", false);
    }
}

//生成考核点列表
function GenerationHtmlList(data, TrainExamId, ContentId) {
    var StrHtml = "";
    //先绑模块及考核点
    var ModuleLength = data.ExamModuleList.length;
    var PointLength = data.ExamPointList.length;
    for (var i = 0; i < ModuleLength; i++) {
        var ExamModuleName = data.ExamModuleList[i].ExamModuleName;
        var Id = data.ExamModuleList[i].Id;
        if (ExamModuleName != "" && ExamModuleName != null) {
            StrHtml += " <tr><td><input filed=\"AllChecked\" type=\"checkbox\" name=\"selectAll_chiled0" + i + "_\" onclick=\"CheckedCheckBox(this)\" id=\"selectAll_chiled0" + i + "_\" {0} ></td> <td colspan=\"2\" align=\"left\"> " + ExamModuleName + " </td> </tr>";
        }
        if (ExamModuleName == "") {
            StrHtml += " <tr> <td colspan=\"5\" align=\"left\" style='height:20px'> </td> </tr>";
        }

        var disabled_point_num = 1;
        var point_num = 1;
        for (var j = 0; j < PointLength; j++) {
            var ExamModuleId = data.ExamPointList[j].ExamModuleId;
            var ExamPointId = data.ExamPointList[j].Id
            if (ExamModuleId == Id) {
                //模块下考点数量计算
                point_num++;

                var ExamPointType = data.ExamPointList[j].ExamPointType;
                var TypeName = "客观题";
                if (ExamPointType == 2) {
                    TypeName = "主观题";
                }
                //查找当前考核点答案
                var Record = answerHelper.FindRecord(ExamPointId);
                var Answer = "";
                if (Record != null && Record.Answer != null) {
                    Answer = Record.Answer;
                }
                //是否需要禁用选择框
                var disabled_flag = false;
                if (ExamPointType == 1 && Answer == "") {
                    disabled_flag = true;
                    disabled_point_num++;
                }

                StrHtml += "<tr>";
                StrHtml += "<td><input type=\"checkbox\" id='b" + data.ExamPointList[j].Id + "' name='selectAll_chiled0" + i + "_b0' tag='chk_list' onclick=\"notCheck(this)\" " + (disabled_flag ? "disabled=\"disabled\"" : "") + "></td>";//j
                StrHtml += "<td><span tag='" + TrainExamId + "' pointType='" + ExamPointType + "' value='" + data.ExamPointList[j].Id + "'>" + TypeName + "</span></td>";
                StrHtml += "<td><span tag='" + TrainExamId + "' pointType='" + ExamPointType + "' value='" + data.ExamPointList[j].Id + "'>" + data.ExamPointList[j].ExamPointName + "</span></td>";


                //拼接分数tempDetail
                //var detail = TrainExamDetailHelper.FindRecord(ExamPointId);
                var detail = tempDetail.FindRecord(ExamPointId);
                if (detail == null) {
                    StrHtml += "<td><input class=\"ipt-text grid-12\" type=\"text\" onblur=\"UpdateScore(this)\" value=\"2\"></td>";
                } else {
                    StrHtml += "<td><input class=\"ipt-text grid-12\" type=\"text\" onblur=\"UpdateScore(this)\" value='" + detail.Score + "'></td>";
                }


                StrHtml += "<td title='" + Answer + "'><div class=\"ellipsis\" title='" + Answer + "'>" + Answer + "</div></td>";
            }
        }
        //若禁用数量与考核点数量一致，则禁用对应模块
        if (disabled_point_num == point_num) {
            StrHtml = StringHelper.FormatStr(StrHtml, "disabled=disabled");
        } else {
            StrHtml = StringHelper.FormatStr(StrHtml, "");
        }


    }
    $("#ExamContentList").html(StrHtml);
}

//选中被保存了的考核点
function CheckCheckbox() {
    var TrainExamDetailList = tempDetail.GetList(); //TrainExamDetailHelper.GetList();

    $.each(TrainExamDetailList, function (i, n) {
        $("#b" + n.ExamPointId).attr("checked", true);
    });

    //如果子复选框全选中这选框要选中
    var totalLength = $("input[tag='chk_list']").length;
    var checkedLength = $("input:checked[tag='chk_list']").length;
    if (checkedLength > 0 && totalLength == checkedLength) {
        $("#selectAll_").attr("checked", true);
    }
    else if (checkedLength < totalLength) {
        $("#selectAll_").attr("checked", false);
    }

    //三级子复选框全部选中时二级复选框选中
    var list = $("#ExamContentList tr").find("td").find("[filed='AllChecked']");
    var two = null;
    $.each(list, function (i, n) {
        two = $(n).attr("name");

        var str = two.split('_');
        var TWO = str[0] + "_" + str[1] + "_b0";
        var TWO2 = str[0] + "_" + str[1] + "_";

        var length1 = $("input[name='" + TWO + "']").length;
        var length2 = $("input:checked[name='" + TWO + "']").length;

        if (length2 > 0 && length1 == length2) {
            $("#" + TWO2).attr("checked", true);
        }
        else if (length2 < length1) {
            $("#" + TWO2).attr("checked", false);
        }
    });

}

//在已发布查看时考核点文本框禁用
function TextDisplay(Type) {
    if (Type == 1) {
        var list = $("#ExamContentList").find("input");
        $.each(list, function (i, n) {
            $(n).attr("disabled", "disabled");
        });
        var classList2 = $("#S_Class span");
        $.each(classList2, function (i, nn) {
            $(nn).find("i").attr("disabled", "disabled");
        });

        $("#TrainExamName").attr("disabled", "disabled");
        $("#StartDate").attr("disabled", "disabled");
        $("#EndDate").attr("disabled", "disabled");

        $("#EndDate").attr("disabled", "disabled");

        //各种确定按钮
        //$("#anliButton").attr("disabled", "disabled");
        $("#detailButton").attr("disabled", "disabled");
        $("#classButton").attr("disabled", "disabled");

        $("#selectAll_").attr("disabled", "disabled");
        $("#buttons").hide();
        $("#deleteCase").hide();
        //$("#anliButton").hide();
        $("#detailButton").hide();
        $("#classButton").hide();

        //各种取消按钮变返回按钮
        $("#btnCancle").val("返回");
        //$("#btnCaseCancle").val("返回");
        $("#btnDetailCancle").val("返回");
        $("#btnClassCancle").val("返回");
    }
    else {
        $("#detailButton").attr("disabled", false);
        $("#detailButton").show();
        $("#btnDetailCancle").val("取消");
    }
}