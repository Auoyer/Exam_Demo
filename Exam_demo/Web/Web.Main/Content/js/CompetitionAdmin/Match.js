
// 加载未发布比赛列表
function GetList() {
    var isCreate = $('#hdCreate').val();
    var isRelease = $('#hideRelease').val();             // 发布状态，0=未发布，1=已发布，2=已结束

    pageHelper.Init({
        url: "/CompetitionAdmin/Match/MatchList",
        type: "GET",
        cache: false,
        pageDiv: "#pages",
        data: {
            name: $("#searchName").val(),
            isRelease: isRelease
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
                trHtml += "<td><a href='/CompetitionAdmin/Match/UserSet/{4}'>人员设置</a>";
                if (isCreate == 1)
                    trHtml += "<a href='javascript:SetPaper({4},{5})'>赛制设置</a>";
                trHtml += "</td>";
                trHtml += "<td class=\"operate\">";
                trHtml += "<a class=\"d_chakan\" href=\"/CompetitionAdmin/Match/Detail/{4}\">查看</a>";
                if (isCreate == 1) {
                    trHtml += "<a class=\"d_shanchu\" href='javascript:Delete({4})'>删除</a>";
                    trHtml += "<a class=\"d_shanchu\" href=\"javascript:Release({4},{5});\">发布</a>";
                }
                trHtml += "</td>";
                trHtml += "</tr>";
                //拼接tbody
                html += StringHelper.FormatStr(trHtml,
                    ((data.PageIndex - 1) * data.PageSize + index + 1),     //0 序号
                    dom.Name,                                       //1 竞赛名称
                    dom.AddUserName,                                           //2 创建人
                    dom.Type == 1 ? "单项理论赛" : dom.Type == 2 ? "单项实训赛" : "复合赛",      //3 比赛类型
                    dom.Id,                                        //4 Id
                    dom.Type                        // 5 竞赛类型
                );
            });
            $("#studentList").html(html);
        }
    });
}


// 加载已发布大赛列表
function GetStartList() {
    var isCreate = $('#hdCreate').val();
    var isRelease = $('#hideRelease').val();             // 发布状态，0=未发布，1=已发布，2=已结束

    pageHelper.Init({
        url: "/CompetitionAdmin/Match/MatchList",
        type: "GET",
        pageDiv: "#pages",
        cache: false,
        data: {
            name: $("#searchName").val(),
            isRelease: isRelease,
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
                // 单项实训赛：状态为：比赛中及之后，人员设置按钮消失
                if ((dom.Type == 1 && dom.State < 103) || (dom.Type == 2 && dom.State < 203) || (dom.Type == 3 && dom.State < 303))
                    trHtml += "<a href='/CompetitionAdmin/Match/UserSet/{5}'>人员设置</a>";
                if (isCreate == 1) {
                    if (dom.Type == 1 && dom.PreliminaryResultType == 1 && dom.State == 104) {
                        // 单项理论，设置是否有成绩发布按钮
                        trHtml += "<a onclick='Result({5})'>成绩发布</a>";
                    }
                    else if (dom.Type == 2 && dom.PreliminaryResultType == 1 && dom.State == 206) {
                        // 单项实训，设置是否有成绩发布按钮
                        trHtml += "<a onclick='Result({5})'>成绩发布</a>";
                    }
                    else if (dom.Type == 3 && dom.PreliminaryResultType == 1 && dom.State == 304) {
                        // 综合赛，初赛为手动发布，且状态在初赛成绩待发布
                        trHtml += "<a onclick='Result({5})'>成绩发布</a>";
                    }
                    else if (dom.Type == 3 && dom.RematchResultType == 1 && dom.State == 309) {
                        // 综合赛，复赛赛为手动发布，且状态在复赛成绩待发布
                        trHtml += "<a onclick='Result({5})'>成绩发布</a>";
                    }
                }
                trHtml += "<a class=\"d_chakan\" href=\"/CompetitionAdmin/Match/Detail/{5}\">查看</a>";
                if (isCreate == 1) {
                    trHtml += "<a class=\"d_shanchu\" href=\"javascript:Delete({5})\">删除</a>";
                    if (dom.State == 100 || dom.State == 200 || dom.State == 300) {
                        // 报名开始前有撤销按钮
                        trHtml += "<a class=\"d_shanchu\" href=\"javascript:MatchCancel({5});\">撤销</a>";
                    }
                    trHtml += "<a class=\"d_shanchu\" href=\"javascript:Ovre({5});\">结束</a>";
                }
                trHtml += "</td>";
                trHtml += "</tr>";
                //拼接tbody
                html += StringHelper.FormatStr(trHtml,
                    ((data.PageIndex - 1) * data.PageSize + index + 1),     //0 序号
                    dom.Name,                                       //1 竞赛名称
                    dom.AddUserName,                                           //2 创建人
                    dom.Type == 1 ? "单项理论赛" : dom.Type == 2 ? "单项实训赛" : "复合赛",      //3 比赛类型
                    GetMatchStateName(dom.Type, dom.State),          // 4 比赛状态
                    dom.Id                                        //5 Id
                );
            });
            $("#studentList").html(html);
            //document.getElementById('studentList').innerHTML = html
        }
    });
}

// 加载已结束大赛列表
function GetEndList() {
    var isCreate = $('#hdCreate').val();
    var isRelease = $('#hideRelease').val();             // 发布状态，0=未发布，1=已发布，2=已结束

    pageHelper.Init({
        url: "/CompetitionAdmin/Match/MatchList",
        type: "GET",
        cache: false,
        pageDiv: "#pages",
        data: {
            name: $("#searchName").val(),
            isRelease: isRelease,
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
                trHtml += "<a class=\"d_chakan\" href=\"/CompetitionAdmin/Match/Detail/{5}\">查看</a>";
                if (isCreate == 1) {
                    trHtml += "<a class=\"d_shanchu\" href=\"javascript:Delete({5})\">删除</a>";
                }
                trHtml += "</td>";
                trHtml += "</tr>";
                //拼接tbody
                html += StringHelper.FormatStr(trHtml,
                    ((data.PageIndex - 1) * data.PageSize + index + 1),     //0 序号
                    dom.Name,                                       //1 竞赛名称
                    dom.AddUserName,                                           //2 创建人
                    dom.Type == 1 ? "单项理论赛" : dom.Type == 2 ? "单项实训赛" : "复合赛",      //3 比赛类型
                    GetMatchStateName(dom.Type, dom.State),          // 4 比赛状态
                    dom.Id                                        //5 Id
                );
            });
            $("#studentList").html(html);
        }
    });
}

// 删除竞赛
function Delete(matchId) {
    dialogHelper.Confirm({
        content: "是否确认删除竞赛及竞赛相关信息吗？",
        success: function () {
            $.ajax({
                url: "/CompetitionAdmin/Match/Delete",
                type: "POST",
                dataType: "json",
                data: {
                    id: matchId
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        dialogHelper.Success({
                            content: '竞赛删除成功！',
                            success: function () {
                                window.location = window.location;
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


// 发布竞赛
function Release(matchId, type) {
    var str = '确认要发布该竞赛吗？';
    if (type == 1 || type == 2)
        str = "单项赛发布后，竞赛信息不可修改，确认要发布该竞赛吗？";

    dialogHelper.Confirm({
        content: str,
        success: function () {
            $.ajax({
                url: "/CompetitionAdmin/Match/Release",
                type: "POST",
                dataType: "json",
                data: {
                    id: matchId
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        dialogHelper.Success({
                            content: '竞赛发布成功！',
                            success: function () {
                                // 跳转到已发布大赛
                                window.location = '/CompetitionAdmin/Match/Start';
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


// 比赛成绩发布
function Result(matchId) {
    var str = '确认要发布该竞赛成绩吗？';

    dialogHelper.Confirm({
        content: str,
        success: function () {
            $.ajax({
                url: "/CompetitionAdmin/Match/Result",
                type: "POST",
                dataType: "json",
                data: {
                    id: matchId
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        dialogHelper.Success({
                            content: '成绩发布成功！',
                            success: function () {
                                // 跳转到已发布大赛
                                window.location = '/CompetitionAdmin/Match/Start';
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


// 撤销竞赛
function MatchCancel(matchId) {
    dialogHelper.Confirm({
        content: "确认要撤销该竞赛吗？",
        success: function () {
            $.ajax({
                url: "/CompetitionAdmin/Match/Cancel",
                type: "POST",
                dataType: "json",
                data: {
                    matchId: matchId
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        dialogHelper.Success({
                            content: '竞赛撤销成功！',
                            success: function () {
                                window.location = window.location;
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


// 结束竞赛
function Ovre(matchId) {
    dialogHelper.Confirm({
        content: "竞赛进行中，确认要结束该竞赛吗？",
        success: function () {
            $.ajax({
                url: "/CompetitionAdmin/Match/MatchOver",
                type: "POST",
                dataType: "json",
                data: {
                    matchId: matchId
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        dialogHelper.Success({
                            content: '竞赛结束成功！',
                            success: function () {
                                window.location = window.location;
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

function SetPaper(MatchId, MatchType) {
    window.location.href = "/CompetitionAdmin/Paper/MatchContent?MatchId=" + MatchId + "&MatchType=" + MatchType;
}