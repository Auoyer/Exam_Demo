var ExamCase = new arrayHelper("IDNum");                        //案例
var ClassHelper = new arrayHelper("ClassId");                   //班级
var answerHelper = new arrayHelper("ExamPointId");              //答案
var TrainExamDetailHelper = new arrayHelper("ExamPointId");     //考核点详细信息
var tempDetail = new arrayHelper("ExamPointId");                //临时存放考点信息

$(function () {

    ExamCase.RemoveAll();
    //销售机会时隐藏考核名称
    var ExamTypeId = $.getUrlParam("ExamTypeId");
    var TrainExamId = $.getUrlParam("TrainExamId");
    if (TrainExamId == null) {
        TrainExamId = 0;
    }

    $("#TrainExamName").unbind("blur").blur(function () {
        var leng = $("#TrainExamName").val();
        if ($("#TrainExamName").val() != "" && (leng.length >= 2 && leng.length <= 30)) {
            $("#T_Name").addClass("finish");
        } else {
            $("#T_Name").removeClass("finish");
        }
    });

    //总表的全选按钮
    $("#selectAll_").click(function () {
        if ($("#selectAll_").attr("checked") != "checked") {
            $("#playList :checkbox[disabled!=disabled]").attr("checked", false);//全不选  
        } else {
            $("#playList :checkbox[disabled!=disabled]").attr("checked", true);//全选  
        }

        //选中就是添加
        var DetailList = $("#ExamContentList tr");

        $.each(DetailList, function (i, n) {
            if ($(n).find("td").find("input:eq(0)").attr("checked") == "checked") {
                if ($(n).find("td").find("input").attr("tag") == "chk_list") {
                    //循环添加详细信息

                    var scor = $(n).find("td:eq(3)").find("input").val();
                    var obj2 = new Object();//实训考核/销售机会详细信息
                    obj2["TrainExamId"] = $(n).find("td").find("span").attr("tag");//实训考核/销售机会Id
                    obj2["ExamPointId"] = $(n).find("td").find("span").attr("value");//考核点Id
                    obj2["Score"] = scor;//分数
                    obj2["ModularId"] = $("#selectExamContent").val();//模块Id
                    obj2["ExamPointType"] = $(n).find("td").find("span").attr("pointType");//主观题客观题

                    //TrainExamDetailHelper.Add(obj2);
                    tempDetail.Add(obj2);

                }

            } else {
                if ($(n).find("td").find("input").attr("tag") == "chk_list") {
                    var pointid = $(n).find("td").find("span").attr("value");
                    var scor = $(n).find("td:eq(3)").find("input").val();

                    //TrainExamDetailHelper.Remove(pointid);
                    tempDetail.Remove(pointid);
                }

            }
        });

    });

    //去除前端原有绑定事件
    $("#detailButton").unbind("click");
    //案例Id
    $("#txtcaseId").val($.getUrlParam("CaseId"));
})

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
            var ExamTypeId = $.getUrlParam("ExamTypeId")

            if (data.TE != null) {

                //案例
                var Case = data.TE.Case;
                ExamCase.Add(Case);
                //考核点
                var TrainExamDetail = data.TE.TrainExamDetail;
                $.each(TrainExamDetail, function (c, d) {
                    TrainExamDetailHelper.Add(d);
                    tempDetail.Add(d);
                });
                //隐藏域
                $("#Id").val(data.TE.Id);
                $("#txtcaseId").val(data.TE.CaseId);
                
            }

        }
    });
}

//单击案例按钮事件
function clickCase() {
    // 清除记录
    ExamCase.RemoveAll();
    tempDetail.RemoveAll();
    TrainExamDetailHelper.RemoveAll();
    answerHelper.RemoveAll();

    $("#txtcaseId").val("0");
    //加载案例 
    GetCaseList();
    dialogHelper.Show('popSelectCase', 850);
}

//获取全部要被选择的案例
function GetCaseList(TypeId, KeyWord) {
    var Type = $.getUrlParam("Type");
    var MatchId = $.getUrlParam("MatchId");
    pageHelper.Init({
        url: "/CompetitionAdmin/Resource/GetCaseList2",
        type: "POST",
        pageDiv: "#pages",
        async: false,
        data:
        {
            MatchId: MatchId,
        },
        bind: function (data) {
            var html = "";
            $(data.Data).each(function (index, dom) {
                //每行html
                var trHtml = "";
                trHtml += "<tr>";
                trHtml += "<td><input name=\"cz\" type=\"radio\" id='anli_" + dom.Id + "' UserId=\"{11}\" date=\"{5}\" userName=\"{4}\"  title=\"{1}\" names=\"{1}\" values=\"{6}\" tag=\"{2}\" text=\"{8}\" typeId=\"{9}\" types=\"{3}\" idytpe=\"{10}\" onclick='ClickRadio(this)' ></td>";
                trHtml += "<td name=\"dataNo\"><spam >{0}</span></td>";
                trHtml += "<td><div class=\"ellipsis\"  title=\"{1}\" names=\"{1}\" value=\"{6}\" tag=\"{2}\" text=\"{8}\" typeId=\"{9}\" type=\"{3}\" idytpe=\"{10}\">{1}</div></td>";
                trHtml += "<td><div class=\"ellipsis\" title=\"{2}\" names=\"{1}\" value=\"{6}\" tag=\"{2}\" text=\"{8}\" typeId=\"{9}\" type=\"{3}\">{2}</div></td>";
                trHtml += "<td><div class=\"ellipsis\" title=\"{3}\" names=\"{1}\" value=\"{6}\" tag=\"{2}\" text=\"{8}\" typeId=\"{9}\" type=\"{3}\">{3}</div></td>";
                trHtml += "<td><div class=\"ellipsis\" title=\"{4}\" names=\"{1}\" value=\"{6}\" tag=\"{2}\" text=\"{8}\" typeId=\"{9}\" type=\"{3}\">{4}</div></td>";
                trHtml += "<td class=\"time\"><div class=\"ellipsis\" title=\"{5}\" names=\"{1}\" value=\"{6}\" tag=\"{2}\" text=\"{8}\" typeId=\"{9}\" type=\"{3}\">{5}</div></td>";
                trHtml += "</tr>";
                //拼接tbody
                html += StringHelper.FormatStr(trHtml,
                    ((data.PageIndex - 1) * data.PageSize + index + 1),   //0 序号
                    dom.CustomerName,                               //1 客户姓名
                    dom.IDNum,                                      //2 身份证号
                    dom.strFinancialType,                           //3 理财类型
                    dom.strCollege,                                   //4 创建人
                    dom.strCreateDate,                              //5 创建日期
                    dom.Id,                                        //6 Id
                    dom.ExamCaseId,                                       //7 ExamCaseId
                    dom.CustomerStory,                                       //8 CustomerStory背景
                    dom.FinancialTypeId,                                      //9
                    dom.IDType,                                       //10
                    dom.UserId                                         //11
                    );
            });
            $("#caseList2").html(html);


            var caseId = $("#txtcaseId").val();
            $("input[name='cz']").removeAttr("checked")
            if (caseId != 0) {
                $("#anli_" + caseId).attr("checked", true);
            }
            //屏蔽控件
            var Type = $.getUrlParam("Type");
            ShieldCase(Type);
        }
    });
}

//选中案例单击事件
function ClickRadio(valu) {

    ExamCase.RemoveAll();
    if ($(valu).attr("checked") == "checked") {
        var obj = new Object();//实训考核/销售机会发布班级
        obj["CustomerName"] = $(valu).attr("title");//姓名
        obj["IDType"] = $(valu).attr("idytpe");//证件类型
        obj["IDNum"] = $(valu).attr("tag");//证件号码
        obj["FinancialTypeId"] = $(valu).attr("typeId");//理财类型
        obj["CustomerStory"] = $(valu).attr("text");//背景
        obj["FinancialTypeName"] = $(valu).attr("types");//理财类型
        //obj["strUserName"] = $(valu).attr("userName");//创建人
        obj["strCreateTime"] = $(valu).attr("date");//创建时间
        obj["CreateTime"] = $(valu).attr("date");//创建时间
        obj["CaseId"] = $(valu).attr("values");//案例Id
        obj["UserId"] = $(valu).attr("UserId");//用户Id

        ExamCase.Add(obj);
    } else {
        ExamCase.Remove($(valu).attr("value"));
    }
    TrainExamDetailHelper.RemoveAll();
    $("#S_ExamPointId").find("p").remove();
    $("#tubiao3").removeClass("finish");
}

//单击案例确定 hzq
function AddCase() {
    //考核点下拉框加载（新增模式）
    selectHelper.GetSelect({
        Id: "#selectExamContent",
        url: "/CompetitionAdmin/Resource/GetExamContentList",
        flag: false,
        changeFun: function (value) {
            GetExamPointList(value, 0, 0);
        }
    });
    ClickTrainExamDetail(0);
}

//删除案例
function DelteCase(valu) {

    $(valu).parent().remove();

    //移除集合对应Id的班级
    ExamCase.RemoveAll();
    var caseId = $("#txtcaseId").val();
    $("#anli_" + caseId).removeAttr("checked");
    $("#txtcaseId").val(0);
    

    var ExamCaseList = ExamCase.GetList();
    if (ExamCaseList.length == 0) {
        $("#Add_Case").removeClass("finish");

        TrainExamDetailHelper.RemoveAll();
        $("#tubiao3").removeClass("finish");
        $("#S_ExamPointId").find("p").remove();
        // $("#tubiao3").removeClass("finish");
    }

}



//单击班级按钮
function ClickClass() {

    //加载老师对应的所以班级
    GetClassList();

    dialogHelper.Show('popTrainingClass', 350);

    var TrainExamId = $.getUrlParam("TrainExamId");

    //选中集合中被添加的班级
    var trList = ClassHelper.GetList(); //获取已选中的班级
    var arrClass = new Array();
    var index = 0;
    var flag = false;
    $("#S_Class span").each(function () {
        var classId = $(this).find("i").attr("tag");
        arrClass.push(classId);
        flag = true;
    });

    //有选中值的时候才让他被选中
    if (flag) {
        $.each(arrClass, function (i, n) {
            $("#checkbox_" + n).attr("checked", true);
        });
    }
    //控件屏蔽
    var Type = $.getUrlParam("Type");
    ShieldClass(Type);
}

//获取对应用户的所以班级
function GetClassList() {
    $.ajax({
        url: "/CompetitionAdmin/Paper/SelectClass",
        async: false,
        type: "POST",
        success: function (data) {
            var html = "";
            $.each(data, function (i, n) {
                html += '<li><span class="icheckbox labellipsis1" value="' + n.Id + '"><input class="icheck" type="checkbox" field="Class" title1="' + n.Id + '" id="checkbox_' + n.Id + '" value="' + n.ClassName + '"><span title="' + n.ClassName + '">' + n.ClassName + '</span></span></li>';
            });
            $("#A_ul").html(html);
        }
    });
}

//单击班级确定按钮
function AddClass() {
    //获取选中班级
    ClassHelper.RemoveAll();
    $("#A_ul input:checked").each(function (index, dom) {
        var obj = new Object();
        obj["ClassId"] = $(dom).attr("title1");//班级ID
        obj["ClassName"] = $(dom).attr("value");//班级名称 
        ClassHelper.Add(obj);
    });
    showClass();
};

//根据选中班级进行显示
function showClass() {
    var trList = ClassHelper.GetList();
    var html = "";
    $.each(trList, function (i, n) {
        var Name = n.ClassName;
        var Id = n.ClassId;
        html += "<span>" + Name + "<i id=\"D_Class_" + i + "\" onclick=\"DeleteClass(this)\" title='" + Name + "' tag='" + Id + "'></i></span>";

    });

    if (html != "") {
        $("#S_Class").html(html);
        $("#popTrainingClass").hide();
        $("#tubiao4").addClass("finish");
    } else {
        $("#S_Class").find("span").remove();
        $("#tubiao4").removeClass("finish");
    }
}

//删除班级
function DeleteClass(valu) {
    var Id = $(valu).attr("tag");
    //移除集合对应Id的班级
    ClassHelper.Remove(Id);

    $(valu).parent().remove();

    var classList = ClassHelper.GetList();
    if (classList.length == 0) {
        $("#tubiao4").removeClass("finish");
    }
}

function ShowTrainExamDetail(TrainExamId)
{
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

// 删除实训案例
function DeleteTrainExamCase(TrainExamId) {
    dialogHelper.Confirm({
        content: "确定删除该案例?", success: function () {
            $.ajax({
                url: "/CompetitionAdmin/Paper/DelTrainExam",
                type: "POST",
                async: false,
                dataType: "json",
                data:
                {
                    TrainExamId: TrainExamId,
                    rId: Math.random()
                },
                success: function (data) {
                    dialogHelper.Success({
                        content: "删除成功！", success: function () {
                            window.location.href = window.location.href;
                        }
                    });
                }
            });
        },
        cancle: function () {
        }
    });
}

//点击考核点按钮 hzq
function ClickTrainExamDetail(Type) {
    //var ContentId = 1;
    //var S_CaseHtml = $("#S_Case").html();
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
            $("#FinancialTypeName").val(n.FinancialTypeName);
            $("#CustomerStory").val(n.CustomerStory);

        });

        //var Type = $.getUrlParam("Type");
        //案例id
        var CaseId = trList[0].CaseId;
        if (Type == 1) {
            //查看时，从Detail中获取答案数据
            $(TrainExamDetailHelper.GetList()).each(function (index, dom) {
                var obj = new Object();
                obj["Id"] = 0;
                obj["CaseId"] = 0;
                obj["ExamPointId"] = dom.ExamPointId;
                obj["Answer"] = dom.Answer;
                answerHelper.Add(dom);
            });
        } else {
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
                    $(data.ExamPointAnswer).each(function (index, dom) {
                        answerHelper.Add(dom);
                    });
                }
            });
        }

        //单击考核点按钮是生成考核点
        var TrainExamId = $.getUrlParam("TrainExamId");
        GetExamPointList(1, TrainExamId, Type);

        //选中被保存了的考核点
        CheckCheckbox();

        //下拉框默认选中第一个
        $("#selectExamContent").val(1);
    }
}

//点击考核点按钮 hzq(查看)
function ClickTrainExamDetail2(Type) {
    //var ContentId = 1;
    //var S_CaseHtml = $("#S_Case").html();
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
                    $(data.ExamPointAnswer).each(function (index, dom) {
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
            GenerationHtmlList(data, TrainExamId, ContentId);
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

//点击考核点确定按钮 hzq
function AddTrainExamDetail() {
    //验证考核点
    var TrainExamDetailList = tempDetail.GetList();
    var number = 0;          //考核点个数
    var score = 0;           //分数
    var flag = false;        //是否有空的分数
    $.each(TrainExamDetailList, function (i, n) {
        number = number + 1;
        score = score + Number(n.Score);
        if (n.Score == null || n.Score == "" || n.Score == undefined || isNaN(parseInt(n.Score)) || parseInt(n.Score) < 1 || parseInt(n.Score) > 50) {
            flag = true;
        }
    });
    if (flag) {
        dialogHelper.Error({ content: "已选择的考核点需设置1-50间的分值！" });
        return;
    }

    //将tempDetail数据复制到TrainExamDetailHelper
    TrainExamDetailHelper.RemoveAll();
    $(tempDetail.GetList()).each(function (index, dom) {
        TrainExamDetailHelper.Add(dom);
    });



    //if (number != 0 && score != 0) {
    //    var html = "<p class=\"bor pl10 pr10 mt10\">您共选择了<span class=\"c-red\">" + number + "</span>个考核点，总分<span class=\"c-red\">" + score + "</span>分！</p>";
    //    $("#S_ExamPointId").html(html);
    //    $("#tubiao3").addClass("finish");
    //} else {
    //    $("#S_ExamPointId").find("p").remove();
    //    $("#tubiao3").removeClass("finish");
    //}

    //dialogHelper.Close('popAssessmentSettings');
    AddTrainExams();
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

//点击考核点取消按钮
function ClickQuXiao() {
    //TrainExamDetailHelper.RemoveAll();
    tempDetail.RemoveAll();
}


//时间控件改变事件
function getTime(valu) {
    var text = $("#StartDate").val();
    if (text != "") {
        $("#tubiao5").addClass("finish");
    } else {
        $("#tubiao5").removeClass("finish");
    }
    $("#StartDate").blur();
}

//时间控件改变事件
function getTime2(valu) {
    var text = $("#EndDate").val();
    if (text != "") {
        $("#tubiao6").addClass("finish");
    } else {
        $("#tubiao6").removeClass("finish");
    }
    $("#EndDate").blur();
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


//屏蔽班级选择框
function ShieldClass(Type) {
    if (Type == 1) {
        //班级
        var classList = $("#popTrainingClass #A_ul").find("input");
        $.each(classList, function (i, c) {
            $(c).attr("disabled", "disabled");
        });
    }
}

//屏蔽案例单选按钮
function ShieldCase(Type) {
    if (Type == 1) {
        var classList = $("#caseList2").find("input");
        $.each(classList, function (i, c) {
            $(c).attr("disabled", "disabled");
        });
    }
}

//屏蔽班级显现
function ShieldClass2(Type) {

    if (Type == 1) {
        var trList = ClassHelper.GetList(); //获取已选中的班级
        $.each(trList, function (i, n) {
            document.getElementById("D_Class_" + i).onclick = null;
        });
    }
}

//反选
function notCheck(valu) {
    var totalLength = $("input[tag='chk_list']").length;
    var checkedLength = $("input:checked[tag='chk_list']").length;
    if (checkedLength > 0 && totalLength == checkedLength) {
        $("#selectAll_").attr("checked", true);
    }
    else if (checkedLength < totalLength) {
        $("#selectAll_").attr("checked", false);
    }

    var two = $(valu).attr("name");
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

    //选中就是添加
    var DetailList = $(valu).parent().parent();
    if ($(valu).attr("checked") == "checked") {

        $.each(DetailList, function (i, n) {
            var a = $(n).find("span").attr("tag");
            var aq = $(n).find("div").html();
            //循环添加详细信息
            var obj = new Object();//实训考核/销售机会详细信息
            obj["TrainExamId"] = $(n).find("span").attr("tag");//实训考核/销售机会Id
            obj["ExamPointId"] = $(n).find("span:eq(1)").attr("value");//考核点Id
            obj["Score"] = $(n).find("input:eq(1)").val();//考核点Id   
            obj["ModularId"] = $("#selectExamContent").val();//模块Id
            obj["ExamPointType"] = $(n).find("span:eq(1)").attr("pointType");//主观题客观题
            //TrainExamDetailHelper.Add(obj);
            tempDetail.Add(obj);

        });

    } else {
        //var id = $(valu).parent().parent().find("span").attr("tag");
        var pointid = $(valu).parent().parent().find("span").attr("value");
        //TrainExamDetailHelper.Remove(pointid);
        tempDetail.Remove(pointid);
    }

}

//修改分数
function UpdateScore(valu) {
    //首先移除该条记录
    var pointId = $(valu).parent().parent().find("span").attr("value");
    //已勾选考点存在时，才进行更新分数操作
    var index = tempDetail.Find(pointId);
    if (index > -1) {
        //TrainExamDetailHelper.Remove(pointId);
        tempDetail.Remove(pointId);

        var score = $.trim($(valu).parent().parent().find("input:eq(1)").val());
        if (isNaN(parseInt(score))) {

        } else {
            $(valu).val(parseInt(score));
            score = parseInt(score);
        }
        var ExamId = $(valu).parent().parent().find("span").attr("tag");
        var obj = new Object();
        obj["TrainExamId"] = ExamId;
        obj["ExamPointId"] = pointId;//考核名称
        obj["Score"] = score;//分数   
        obj["ModularId"] = $("#selectExamContent").val();//模块Id
        obj["ExamPointType"] = $(valu).parent().parent().find("span").attr("pointType");//主观题客观题
        //TrainExamDetailHelper.Add(obj);
        tempDetail.Add(obj);
    }
}

//取消
function cancel() {
    window.history.back(-1);
}


//保存
function AddTrainExams() {

    if (!VerificationHelper.checkFrom("AddTrainExamCase")) {
        return false;
    }

    //var ExamTypeId = $.getUrlParam("ExamTypeId");
    //var name = "";
    //if (ExamTypeId == 2) {
    //    name = $.trim($("#TrainExamName").val());
    //    if (name == "") {
    //        dialogHelper.Error({ content: "您还有设置未完成！", success: function () { } });
    //        return false;
    //    }

    //}
    ////判断是否添加案例
    //if ($("#AddTrainExamCase #S_Case").html() == "" || $("#AddTrainExamCase #S_Case").html() == null) {
    //    dialogHelper.Error({ content: "您还有设置未完成！", success: function () { } });
    //    return false;
    //}
    //判断是否添加考核点
    if (TrainExamDetailHelper.GetList().length == 0) {
        dialogHelper.Error({ content: "您尚未添加考核点！", success: function () { } });
        return false;
    }
    //判断是否总分不一致

    var TrainExamDetailList = TrainExamDetailHelper.GetList(); //TrainExamDetailHelper.GetList();

    var AllScore = 0;
    $.each(TrainExamDetailList, function (i, n) {
        AllScore = AllScore + (n.Score * 1);
    });

    if ($("#MatchCaseScore").html() != null && $("#MatchCaseScore").html() != AllScore) {
        dialogHelper.Error({ content: "当前选择的考核点总分必须与上一案例相同！", success: function () { } });
        return false;
    }
    //判断是否添加班级
    //if ($("#AddTrainExamCase #S_Class").html() == "" || $("#AddTrainExamCase #S_Class").html() == null) {
    //    dialogHelper.Error({ content: "您还有设置未完成！", success: function () { } });
    //    return false;
    //}
    //判断时间
    //var starttime = $("#StartDate").val();
    //var endtime = $("#EndDate").val();
    //if (starttime == "") {
    //    dialogHelper.Error({ content: "您还有设置未完成！", success: function () { } });
    //    return false;
    //}
    //if (endtime == "") {
    //    dialogHelper.Error({ content: "您还有设置未完成！", success: function () { } });
    //    return false;
    //}


    //var a = $("#txtcaseId").val();


    var obj = new Object();
    var selExamCase = ExamCase.GetList();//实训考核/销售机会
    //obj["Id"] = $("#Id").val();
    //obj["TrainExamName"] = $("#TrainExamName").val();       //考核名称
    obj["ExamCaseId"] = selExamCase[0].CaseId;                                  //案例Id
    obj["CompetitionId"] = $("#hdMatchId").val();
    obj["CaseId"] = selExamCase[0].CaseId;                                   //案例Id
    obj["UserId"] = $("#hdUserId").val();                   //发布用户Id
    var Type = $.getUrlParam("Type");
    if (Type == 1) {
        obj["Status"] = 1;                                  //状态 已发布
    } else {
        obj["Status"] = 0;                                  //状态 未发布
    }
    obj["TrainExamStatus"] = 1;                             //评分状态   
    obj["TrainExamDetail"] = null;                          //详细信息

    //添加案例
    obj["ExamCase"] = ExamCase.GetList();
    
    var IDNum = "";
    $.each(selExamCase, function (i, n) {
        IDNum = n.IDNum;
    });

    //添加详细信息
    obj["TrainExamDetail"] = TrainExamDetailHelper.GetList();//新增（添加时）   

    $.ajax({
        url: "/CompetitionAdmin/Paper/AddTrainExam2",
        type: "POST",
        async: false,
        dataType: "json",
        data: JSON.stringify(obj),
        contentType: "application/json",
        success: function (data) {
            $("#Id").val(data);
            num = 0;
            Score = 0;
            dialogHelper.Success({
                content: "保存成功！", success: function () {
                    //var TrainExamId = $.getUrlParam("TrainExamId");
                    //if (TrainExamId == null) {
                    //    TrainExamId = data;
                    //} else {
                    //    //SelectTrainExam(TrainExamId);
                    //}

                    window.location.href = window.location.href;
                }
            });
        }, error: function (msg) {
            $(".background,.progressBar").hide();
        }
    });
}



//发布销售机会或实训考核
function PublishPractise(Id, Status, INNum, ExamTypeId, strStartDate, TrainExamName) {
    $.ajax({
        url: "/CompetitionAdmin/Paper/publishTrainExam",
        data: { Id: Id, Status: Status, IDNum: INNum, ExamTypeId: ExamTypeId, strStartDate: strStartDate, TrainExamName: TrainExamName },
        async: false,
        type: "POST",
        success: function (data) {
            dialogHelper.Success({
                content: "发布成功！", success: function () {
                    if (ExamTypeId == 1) {
                        window.location.href = "/CompetitionAdmin/Paper/Published";
                    } else {
                        window.location.href = "/CompetitionAdmin/Paper/CheckPublished";
                    }
                }
            });
        },
        error: function (msg) {
            $(".background,.progressBar").hide();
        }
    });
}

//考核点二级全选事件
function CheckedCheckBox(valu) {
    var name = $(valu).attr("name");
    var Id = name + "b0";
    var list = $(valu).parent().parent().parent().find("tr");

    if ($(valu).attr("checked") == "checked") {
        $.each(list, function (i, n) {
            if ($(n).find("td:eq(0)").find("input").attr("name") == Id) {
                //并且选中框不能disabled
                if ($(n).find("td:eq(0)").find("input").attr("disabled") != undefined) {
                    return;
                }

                $(n).find("td:eq(0)").find("input").attr("checked", true);

                //循环添加详细信息
                var obj = new Object();//实训考核/销售机会详细信息
                obj["TrainExamId"] = $(n).find("td").find("span").attr("tag");//实训考核/销售机会Id
                obj["ExamPointId"] = $(n).find("td").find("span").attr("value");//考核点Id
                obj["Score"] = $(n).find("td:eq(3)").find("input").val();//考核点Id
                obj["ModularId"] = $("#selectExamContent").val();//模块Id
                obj["ExamPointType"] = $(n).find("td").find("span").attr("pointType");//主观题客观题
                //TrainExamDetailHelper.Add(obj);
                tempDetail.Add(obj);
            }

        });
    } else {
        $.each(list, function (i, n) {
            if ($(n).find("td:eq(0)").find("input").attr("name") == Id) {
                $(n).find("td:eq(0)").find("input").attr("checked", false);

                var pointid = $(n).find("td").find("span").attr("value");
                //TrainExamDetailHelper.Remove(pointid);
                tempDetail.Remove(pointid);
            }
        });
    }

    var totalLength = $("input[tag='chk_list']").length;
    var checkedLength = $("input:checked[tag='chk_list']").length;
    if (checkedLength > 0 && totalLength == checkedLength) {
        $("#selectAll_").attr("checked", true);
    }
    else if (checkedLength < totalLength) {
        $("#selectAll_").attr("checked", false);
    }
}
