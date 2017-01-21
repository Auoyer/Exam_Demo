// 加载已发布大赛列表
function GetStartList() {
    var isRelease = $('#hideRelease').val();             // 发布状态，0=未发布，1=已发布，2=已结束

    pageHelper.Init({
        url: "/CompetitionAdmin/StatAnalysis/MatchList2",
        type: "POST",
        pageDiv: "#pages",
        data: {
            name: $("#searchName").val(),
            //isRelease: isRelease,
            random: Math.random()
        },
        bind: function (data) {
            var html = "";
            $(data.Data).each(function (index, dom) {
                //每行html
                var trHtml = "";
                trHtml += "<tr>";
                trHtml += "<td>{0}</td>";
                trHtml += "<td><div title=\"{1}\" class=\"ellipsis\">{1}</div></td>";
                trHtml += "<td><div title=\"{2}\" class=\"ellipsis\">{2}</div></td>";
                trHtml += "<td>{3}</td>";
                trHtml += "<td>{4}</td>";
                trHtml += "<td class=\"operate\">";
                if (dom.IsRelease == 2) {
                    trHtml += "<a class=\"d_chakan\" href=\"/CompetitionAdmin/StatAnalysis/Detail/{5}\">查看</a>";
                }
                else {
                    trHtml += "<a class=\"d_chakan\" href=\"javascript:void(0)\">--</a>";
                }


                trHtml += "</td>";
                trHtml += "</tr>";

                // 比赛时间段
                var timeSpan = '';
                if (dom.Type == 1 || dom.Type == 2)
                    timeSpan = dom._PreliminaryStartTime + " - " + dom._PreliminaryEndTime;
                else if (dom.Type == 3)
                    timeSpan = dom._PreliminaryStartTime + " - " + dom._RematchEndTime;

                //拼接tbody
                html += StringHelper.FormatStr(trHtml,
                    ((data.PageIndex - 1) * data.PageSize + index + 1),     //0 序号
                    dom.Name,                                       //1 竞赛名称
                    dom.AddUserName,                                           //2 创建人
                    timeSpan,
                    GetMatchStateName(dom.Type, dom.State),          // 4 比赛状态
                    dom.Id                                        //5 Id
                );
            });
            $("#studentList").html(html);
        }
    });
}


// 比赛状态
// type：比赛类型
//State：进行状态
function GetMatchStateName(type, state) {
    if (type == 1) {
        // 单项理论赛
        if (state == 100)
            return '未开始';
        else if (state == 101)
            return '报名中';
        else if (state == 102)
            return '待比赛';
        else if (state == 103)
            return '比赛中';
        else if (state == 104)
            return '成绩待发布';
        else if (state == 105)
            return '比赛结束';
    }
    else if (type == 2) {
        // 单项实训赛
        if (state == 200)
            return '未开始';
        else if (state == 201)
            return '报名中';
        else if (state == 202)
            return '待比赛';
        else if (state == 203)
            return '比赛中';
        else if (state == 204)
            return '待评分';
        else if (state == 205)
            return '评分中';
        else if (state == 206)
            return '成绩待发布';
        else if (state == 207)
            return '比赛结束';
    }
    else if (type == 3) {
        // 复合赛
        if (state == 300)
            return '未开始';
        else if (state == 301)
            return '报名中';
        else if (state == 302)
            return '待初赛';
        else if (state == 303)
            return '初赛中';
        else if (state == 304)
            return '成绩待发布（初赛）';
        else if (state == 305)
            return '待复赛';
        else if (state == 306)
            return '复赛中';
        else if (state == 307)
            return '待评分';
        else if (state == 308)
            return '评分中';
        else if (state == 309)
            return '成绩待发布';
        else if (state == 310)
            return '比赛结束';
    }
}

//根据竞赛id获取竞赛信息
function GetModel() {
    // 加载编辑页面
    $.ajax({
        url: '/CompetitionAdmin/StatAnalysis/GetModel',
        data: {
            id: $('#hideMatchId').val()
        },
        success: function (data) {
            if (data.IsSuccess) {
                $("#spComName").html(data.Data.Name);//大赛名称
                $("#tdAddUser").html(data.Data.AddUserName);//大赛管理员
                //$("#tdFengzhi").html(data.Data.Name);//登录峰值

                var bmsj = data.Data._RegistrationStartTime + "-" + data.Data._RegistrationEndTime;
                $("#tdbmsj").html(bmsj);//报名时间

                $("#tdbmrs").html(data.Data.RegistrationPersons);//报名人数

                var timeSpan = '';
                if (data.Data.Type == 1 || data.Data.Type == 2)
                    timeSpan = data.Data._PreliminaryStartTime + " - " + data.Data._PreliminaryEndTime;
                else if (data.Data.Type == 3)
                    timeSpan = data.Data._PreliminaryStartTime + " - " + data.Data._RematchEndTime;
                $("#tdbssj").html(timeSpan);//比赛时间

                $("#tdrs").html(data.Data.InMatchPersons);//参赛人数
            }
            else {
                dialogHelper.Error({
                    content: data.ErrorCode,
                    success: function () {
                        history.go(-1);
                    }
                });
            }
        }
    })
}

//加载参赛者信息和成绩信息
function GetDetail() {
    pageHelper.Init({
        url: '/CompetitionAdmin/StatAnalysis/GetDetail',
        type: "POST",
        pageDiv: "#UserScorePages",
        data: {
            competitionId: $('#hideMatchId').val(),
            SearchContent: $.trim($("#txtSearchContent").val()),
            IsPageRegistration: $("#cmbIsPageRegistration option:selected").val()
        },
        bind: function (data) {
            var html = "";
            $(data.Data).each(function (index, dom) {
                //每行html
                var trHtml = "";
                trHtml += "<tr>";
                trHtml += "<td>{0}</td>";
                if (dom.IsPageRegistration == 1) {
                    trHtml += "<td title=\"{6}\">{1}<font class=\"d_zhu\">注</font></td>";
                }
                else {
                    trHtml += "<td title=\"{6}\">{1}</td>";
                }
                trHtml += "<td>{2}</td>";
                trHtml += "<td>{3}</td>";
                trHtml += "<td>{4}</td>";
                trHtml += "<td>{5}</td>";
                trHtml += "</tr>";
                var pc = getCodeName(dom.ProvinceCode, dom.CityCode);
                var p, c;
                if (dom.ProvinceCode == "" || dom.ProvinceCode == 0) {
                    p = "";
                }
                else {
                    p = pc;
                }
                if (dom.CityCode == "" || dom.CityCode == 0) {
                    c = "";
                }
                else {
                    c = pc;
                }

                if (dom.ProvinceCode != "" && dom.ProvinceCode != 0 && dom.CityCode != "" && dom.CityCode != "") {
                    p = $.trim(pc.substring(0, pc.indexOf("-")));//取省
                    c = $.trim(pc.substring(pc.indexOf("-") + 1));//取市
                }
                //拼接tbody
                html += StringHelper.FormatStr(trHtml,
                    dom.GroupId,
                    dom.UserName != null ? dom.UserName.length > 6 ? dom.UserName.substring(0, 6) + "..." : dom.UserName : "",
                    dom.IDCard,
                    p,
                    c,
                    dom.CollegeName,
                    dom.UserName
                );

            });
            $("#tbUserScoreList").html(html);
        }
    });
}

//加载参赛者信息和成绩信息
function GetScore() {
    pageHelper.Init({
        url: '/CompetitionAdmin/StatAnalysis/GetDetail',
        type: "POST",
        pageDiv: "#ScorePages",
        data: {
            competitionId: $('#hideMatchId').val(),
            SearchContent: $.trim($("#txtSearchContent2").val()),
            IsPageRegistration: $("#cmbIsPageRegistration2 option:selected").val()
        },
        bind: function (data) {
            if (data.Data == "" || data.Data == null) {
                var html = "";
                html = "<thead><tr><th width=\"10%\">序号</th><th \"20%\">姓名</th><th \"20%\">省份</th><th \"20%\">城市</th><th \"10%\">学校</th><th \"10%\">院部/系</th><th \"10%\">比赛成绩</th></tr></thead>";
                $("#tbScore").html(html);
            }
            else {
                if (data.Data[0].Type == 1) {
                    var html = "";
                    html = "<thead><tr><th width=\"10%\">序号</th><th \"20%\">姓名</th><th \"20%\">省份</th><th \"20%\">城市</th><th \"10%\">学校</th><th \"10%\">院部/系</th><th \"10%\">比赛成绩</th></tr></thead>";

                    $(data.Data).each(function (index, dom) {
                        //每行html
                        var trHtml = "";
                        trHtml += "<tbody>";
                        trHtml += "<tr>";
                        trHtml += "<td >{0}</td>";
                        if (dom.IsPageRegistration == 1) {
                            trHtml += "<td title=\"{7}\">{1}<font class=\"d_zhu\">注</font></td>";
                        }
                        else {
                            trHtml += "<td title=\"{7}\">{1}</td>";
                        }
                        trHtml += "<td>{2}</td>";
                        trHtml += "<td>{3}</td>";
                        trHtml += "<td>{4}</td>";
                        trHtml += "<td>{5}</td>";
                        trHtml += "<td>{6}</td>";
                        trHtml += "</tr>";
                        trHtml += "</tbody>";
                        var pc = getCodeName(dom.ProvinceCode, dom.CityCode);
                        var p, c;
                        if (dom.ProvinceCode == "" || dom.ProvinceCode == 0) {
                            p = "";
                        }
                        else {
                            p = pc;
                        }
                        if (dom.CityCode == "" || dom.CityCode == 0) {
                            c = "";
                        }
                        else {
                            c = pc;
                        }

                        if (dom.ProvinceCode != "" && dom.ProvinceCode != 0 && dom.CityCode != "" && dom.CityCode != "") {
                            p = $.trim(pc.substring(0, pc.indexOf("-")));//取省
                            c = $.trim(pc.substring(pc.indexOf("-") + 1));//取市
                        }

                        //拼接tbody
                        html += StringHelper.FormatStr(trHtml,
                            dom.GroupId,
                            dom.UserName != null ? dom.UserName.length > 6 ? dom.UserName.substring(0, 6) + "..." : dom.UserName : "",
                            p,
                            c,
                            dom.CollegeName,
                            dom.DepartmentName,
                            dom.Score != null ? dom.Score : "缺考",
                            dom.UserName
                        );

                    });
                    $("#tbScore").html(html);
                }
                else if (data.Data[0].Type == 2) {
                    var html = "";
                    html = "<thead><tr><th width=\"10%\">序号</th><th \"20%\">姓名</th><th \"20%\">省份</th><th \"20%\">城市</th><th \"10%\">学校</th><th \"10%\">院部/系</th><th \"10%\">比赛成绩</th></tr></thead>";

                    $(data.Data).each(function (index, dom) {
                        //每行html
                        var trHtml = "";
                        trHtml += "<tbody>";
                        trHtml += "<tr>";
                        trHtml += "<td>{0}</td>";
                        if (dom.IsPageRegistration == 1) {
                            trHtml += "<td title=\"{7}\">{1}<font class=\"d_zhu\">注</font></td>";
                        }
                        else {
                            trHtml += "<td title=\"{7}\">{1}</td>";
                        }
                        trHtml += "<td>{2}</td>";
                        trHtml += "<td>{3}</td>";
                        trHtml += "<td>{4}</td>";
                        trHtml += "<td>{5}</td>";
                        trHtml += "<td>{6}</td>";
                        trHtml += "</tr>";
                        trHtml += "</tbody>";
                        var pc = getCodeName(dom.ProvinceCode, dom.CityCode);
                        var p, c;
                        if (dom.ProvinceCode == "" || dom.ProvinceCode == 0) {
                            p = "";
                        }
                        else {
                            p = pc;
                        }
                        if (dom.CityCode == "" || dom.CityCode == 0) {
                            c = "";
                        }
                        else {
                            c = pc;
                        }

                        if (dom.ProvinceCode != "" && dom.ProvinceCode != 0 && dom.CityCode != "" && dom.CityCode != "") {
                            p = $.trim(pc.substring(0, pc.indexOf("-")));//取省
                            c = $.trim(pc.substring(pc.indexOf("-") + 1));//取市
                        }

                        //拼接tbody
                        html += StringHelper.FormatStr(trHtml,
                            dom.GroupId,
                            dom.UserName != null ? dom.UserName.length > 6 ? dom.UserName.substring(0, 6) + "..." : dom.UserName : "",
                            p,
                            c,
                            dom.CollegeName,
                            dom.DepartmentName,
                            dom.SubjectiveResults != null && dom.ObjectiveResults != null ? dom.SubjectiveResults + dom.ObjectiveResults : "缺考",
                            dom.UserName
                        );

                    });
                    $("#tbScore").html(html);
                }
                else {
                    var html = "";
                    html = "<thead><tr><th width=\"10%\">序号</th><th \"20%\">姓名</th><th \"20%\">省份</th><th \"20%\">城市</th><th \"10%\">学校</th><th \"10%\">院部/系</th><th \"10%\">初赛成绩</th><th \"10%\">复赛成绩</th></tr></thead>";
                    $(data.Data).each(function (index, dom) {
                        //每行html
                        var trHtml = "";
                        trHtml += "<tbody>";
                        trHtml += "<tr>";
                        trHtml += "<td>{0}</td>";
                        if (dom.IsPageRegistration == 1) {
                            trHtml += "<td title=\"{8}\">{1}<font class=\"d_zhu\">注</font></td>";
                        }
                        else {
                            trHtml += "<td title=\"{8}\">{1}</td>";
                        }
                        trHtml += "<td>{2}</td>";
                        trHtml += "<td>{3}</td>";
                        trHtml += "<td>{4}</td>";
                        trHtml += "<td>{5}</td>";
                        trHtml += "<td>{6}</td>";
                        trHtml += "<td>{7}</td>";
                        trHtml += "</tr>";
                        trHtml += "</tbody>";
                        var pc = getCodeName(dom.ProvinceCode, dom.CityCode);
                        var p, c;
                        if (dom.ProvinceCode == "" || dom.ProvinceCode == 0) {
                            p = "";
                        }
                        else {
                            p = pc;
                        }
                        if (dom.CityCode == "" || dom.CityCode == 0) {
                            c = "";
                        }
                        else {
                            c = pc;
                        }

                        if (dom.ProvinceCode != "" && dom.ProvinceCode != 0 && dom.CityCode != "" && dom.CityCode != "") {
                            p = $.trim(pc.substring(0, pc.indexOf("-")));//取省
                            c = $.trim(pc.substring(pc.indexOf("-") + 1));//取市
                        }

                        //拼接tbody
                        html += StringHelper.FormatStr(trHtml,
                            dom.GroupId,
                            dom.UserName != null ? dom.UserName.length > 6 ? dom.UserName.substring(0, 6) + "..." : dom.UserName : "",
                            p,
                            c,
                            dom.CollegeName,
                            dom.DepartmentName,
                            dom.Score != null ? dom.Score : "缺考",
                            dom.SubjectiveResults != null && dom.ObjectiveResults != null ? dom.SubjectiveResults + dom.ObjectiveResults : "―",
                            dom.UserName
                        );

                    });
                    $("#tbScore").html(html);
                }

            }
        }
    });
}

//导出参赛者信息
function ExportInMatchUsers() {
    $tr = $("#tbUserScoreList").find("tr");
    var trLen = $tr.length;
    if (trLen <= 0) {
        dialogHelper.Error({
            content: "没有可导出的内容！"
        });
        return;
    }
    else {
        location.href = "/CompetitionAdmin/StatAnalysis/DownLoadFileExcel?competitionId=" + $('#hideMatchId').val() + "&&SearchContent=" + $.trim($("#txtSearchContent").val()) + "&&IsPageRegistration=" + $("#cmbIsPageRegistration option:selected").val();

    }
}

//导出成绩信息
function ExportInMatchUserScore() {
    $tr = $("#tbScore").find("tr");
    var trLen = $tr.length;
    if (trLen <= 0) {
        dialogHelper.Error({
            content: "没有可导出的内容！"
        });
        return;
    }
    else {
        location.href = "/CompetitionAdmin/StatAnalysis/DownLoadUserScoreFileExcel?competitionId=" + $('#hideMatchId').val() + "&&SearchContent=" + $.trim($("#txtSearchContent").val()) + "&&IsPageRegistration=" + $("#cmbIsPageRegistration option:selected").val();

    }
}

//获取大赛成绩
function GetStatistic() {
    var xData = [];
    var series = [];
    $.ajax({
        url: "/CompetitionAdmin/StatAnalysis/GetUserMatchScoreList",
        data: { competitionId: $('#hideMatchId').val() },
        async: false,
        type: "POST",
        success: function (data) {
            if (data != null) {
                if (data.Data.UserMatchScoreList != "") {
                    ShowChart(data.Data.UserMatchScoreList);
                }
                else {
                    ShowChart3();
                }
                ShowChart2(data.Data);
                var ddata = data.Data.PersonScoreList;
                for (var key in ddata) {
                    xData.push(ddata[key].ScoreSegment);
                    series.push(ddata[key].Persons);
                }
                if (xData != "" && series != "") {
                    Inithighcharts(xData, series);
                }
                else {
                    Inithighcharts2(xData, series);
                }
            }
        },
        error: function (msg) {
            $(".background,.progressBar").hide();
        }
    });


}

//获取是否参赛
function GetInMatch() {
    $.ajax({
        url: "/CompetitionAdmin/StatAnalysis/GetModel",
        data: { competitionId: $('#hideMatchId').val() },
        async: false,
        type: "POST",
        success: function (data) {
            if (data != null) {
                ShowChart2(data.Data);
            }
        },
        error: function (msg) {
            $(".background,.progressBar").hide();
        }
    });
}


//饼图查看（成绩及格率统计）
function ShowChart(data) {
    var count = data.length;
    var MinScore = 0, MaxScore = 0, AverageScore = 0, TotalScore = 0, YouXiu = 0, LiangHao = 0, YiBan = 0, BuJiGe = 0, ExamScore = 0;
    if (count > 0) {
        MinScore = data[0].ObjectiveResults * 1 + data[0].SubjectiveResults * 1
    }
    for (var i = 0; i < count; i++) {
        var Score = data[i].ObjectiveResults * 1 + data[i].SubjectiveResults * 1;
        ExamScore = data[i].AllScore * 1;
        if (Score < MinScore) {
            MinScore = Score;
        }

        if (MaxScore < Score) {
            MaxScore = Score;
        }
        TotalScore += Score;
        var Grade = 0;//评分等级---优、良、及格、不及格
        Grade = (Score / ExamScore) * 10;
        if (Grade >= 9) {
            YouXiu++;
        } else if (Grade >= 8) {
            LiangHao++;
        } else if (Grade >= 6) {
            YiBan++;
        } else {
            BuJiGe++;
        }
    }
    //$("#MaxScore").text(MaxScore);//最高分
    //$("#MinScore").text(MinScore);//最低分
    //AverageScore = TotalScore / count;
    //if (isNaN(AverageScore)) {
    //    AverageScore = 0;
    //}
    //$("#AverageScore").text(AverageScore.toFixed(2));//平均分
    var YouXiuRate = (YouXiu / count * 100).toFixed(2);
    if (YouXiuRate == "NaN") {
        YouXiuRate = 0;
    }
    var LiangHaoRate = (LiangHao / count * 100).toFixed(2);
    if (LiangHaoRate == "NaN") {
        LiangHaoRate = 0;
    }
    var YiBanRate = (YiBan / count * 100).toFixed(2);
    if (YiBanRate == "NaN") {
        YiBanRate = 0;
    }
    var BuJiGeRate = (BuJiGe / count * 100).toFixed(2);
    if (BuJiGeRate == "NaN") {
        BuJiGeRate = 0;
    }
    $('#container').highcharts({
        chart: {
            type: 'pie',
           
            options3d: {
                enabled: true,
                alpha: 45
            }
        },
        title: {
            text: '成绩及格率统计'
        },
        colors: ['#46adb7', '#f2a83e', '#e16556', '#00a2e2'],
        plotOptions: {
            pie: {
                size: 200,
                innerSize: 100,
                depth: 45
            }
        },
        series: [{
            name: '队数',
            data: [
                ['优秀' + YouXiuRate + '%', YouXiu],
                ['良好' + LiangHaoRate + '%', LiangHao],
                ['一般' + YiBanRate + '%', YiBan],
                ['不及格' + BuJiGeRate + '%', BuJiGe],
            ]
        }]
    });
}

//饼图查看（成绩及格率统计）
function ShowChart3() {

    $('#container').highcharts({
        chart: {
            type: 'pie',
            options3d: {
                enabled: true,
                alpha: 45
            },


        },
        title: {
            text: '成绩及格率统计'
        },
        colors: ['#46adb7', '#f2a83e', '#e16556', '#00a2e2'],
        plotOptions: {
            pie: {
                size: 180,
                innerSize: 100,
                depth: 45
            }
        },
        series: [{
            name: '队数',
            data: [
                ['优秀' + 0 + '%', 0],
                ['良好' + 0 + '%', 0],
                ['一般' + 0 + '%', 0],
                ['不及格' + 100 + '%', 1],
            ]
        }],

    });

}

//饼图查看（是否参赛）
function ShowChart2(data) {
    var bmrs = 0;
    var csrs = 0;
    var wcsrs = 0;
    csrs = data.InMatchPersons;
    bmrs = data.RegistrationPersons;
    wcsrs = bmrs - csrs;
    csrsbl = (csrs / bmrs * 100).toFixed(2);
    wcsrsbl = (wcsrs / bmrs * 100).toFixed(2);
    $('#container2').highcharts({
        chart: {
            type: 'pie',
            options3d: {
                enabled: true,
                alpha: 45
            },
           
        },
        title: {
            text: '报名成功，是否参赛人员比例'
        },
        colors: ['#46adb7', '#f2a83e', '#e16556', '#00a2e2'],
        plotOptions: {
            pie: {
                size: 200,
                innerSize: 100,
                depth: 45
            }
        },
        series: [{
            name: '人数',
            data: [
                ['参赛' + csrsbl + '%', csrs],
                ['未参赛' + wcsrsbl + '%', wcsrs],

            ]
        }]
    });
}

//初始化报表插件
function Inithighcharts(xData, series) {
    $("#divcharts").highcharts({
        chart: {
            
            type: 'column'
        },
        title: {
            text: '比赛各分数段人员数统计',
            align: 'center',

        },
        credits: {
            enabled: false
        },
        xAxis: {
            categories: xData,
            title: {
                text: '分数'
            }
        },
        yAxis: {
            title: {
                text: '人数',

            },
            allowDecimals: false
        },
        tooltip: {
            // shared: true,
            headerFormat: '<b>{point.x}</b><br />',
            pointFormat: '{series.name}：{point.y}<br/>'
        },

        plotOptions: {
            series: {
                allowPointSelect: false,
                showInLegend: false
            }
        },

        series: [{
            name: "人数",
            data: series
        }]
    });
}

//初始化报表插件（没有数据的时候）
function Inithighcharts2(xData, series) {
    $("#divcharts").highcharts({
        chart: {

            type: 'column'
        },
        title: {
            text: '比赛各分数段人员数统计',
            align: 'center',

        },
        credits: {
            enabled: false
        },
        xAxis: {
            categories: xData,
            title: {
                text: '分数'
            }
        },
        yAxis: {
            title: {
                text: '人数',

            },
            allowDecimals: false,
            tickPositions: [0, 2, 4, 6, 8, 10]
        },
        tooltip: {
            // shared: true,
            headerFormat: '<b>{point.x}</b><br />',
            pointFormat: '{series.name}：{point.y}<br/>'
        },

        plotOptions: {
            series: {
                allowPointSelect: false,
                showInLegend: false
            }
        },

        series: [{
            name: "人数",
            data: series
        }]
    });
}