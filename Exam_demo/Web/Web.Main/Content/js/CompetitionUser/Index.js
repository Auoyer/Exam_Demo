
//获取历史大赛数量
function GetHistoryCompetitionNum() {
    var int = parseInt($('#hideLSDS').val());
    var s = JSON.stringify(int);
    var a = FormatNum(int, 6);
    for (var i = 0; i < a.length; i++) {
        var cur = a[i];
        $('.d_wrap span').eq(i).animate({ top: -24 * cur }, 1000);
    }
    //$.ajax({
    //    url: "/CompetitionUser/Home/GetHistoryCompetitionNum",
    //    type: "POST",
    //    async: false,
    //    success: function (data) {
    //        var s = JSON.stringify(data.Data);
    //        var a = FormatNum(s, 6);
    //        for (var i = 0; i < a.length; i++) {
    //            var cur = a[i];
    //            $('.d_wrap span').eq(i).animate({ top: -24 * cur }, 1000);
    //        }
    //    }
    //});
}

//获取总人数
function GetTotalUserNum() {
    var int = parseInt($('#hideZRS').val());
    var s = JSON.stringify(int);
    var a = FormatNum(s, 6);
    for (var i = 0; i < a.length; i++) {
        var cur = a[i];
        $('.d_wrap1 span').eq(i).animate({ top: -24 * cur }, 1000);
    }
    //$.ajax({
    //    url: "/CompetitionUser/Home/GetTotalUserNum",
    //    type: "POST",
    //    async: false,
    //    success: function (data) {
    //        var s = JSON.stringify(data.Data);
    //        var a = FormatNum(s, 6);
    //        for (var i = 0; i < a.length; i++) {
    //            var cur = a[i];
    //            $('.d_wrap1 span').eq(i).animate({ top: -24 * cur }, 1000);
    //        }
    //    }
    //});
}

//待参加大赛数量
function GetRegisterNotAduitNum() {
    $.ajax({
        url: "/CompetitionUser/Home/GetWaitJoinNum",
        type: "POST",
        success: function (data) {
            $('.d_home_2_1 span').html(data.Data);
        }
    });
}

//已参加大赛数量
function GetSiginupNotAduitNum() {
    $.ajax({
        url: "/CompetitionUser/Home/GetHasJoinNum",
        type: "POST",
        success: function (data) {
            $('.d_home_2_1s span').html(data.Data);
        }
    });
}

//获取大赛列表
function GetIndexMatchList() {
    pageHelper.Init({
        url: "/CompetitionUser/Home/GetLatestCompetitionList",
        type: "POST",
        pageDiv: "#pages",
        //data: {
        //    userName: $("#searchName").val(),
        //    isPage: 0
        //},
        bind: function (data) {
            var html = "";
            $(data).each(function (index, dom) {
                //每行html
                var trHtml = "";
                trHtml += "<tr>";
                trHtml += "<td>{0}</td>";
                trHtml += "<td>{1}</td>";
                trHtml += "<td>{2}</td>";
                trHtml += "<td>{3}</td>";
                trHtml += "<td>{4}</td>";
                trHtml += "<td>";
                trHtml += "<a href=\"/CompetitionUser/Match/Detail/{5}\">进入大赛</a>";
                trHtml += "</td>";
                trHtml += "</tr>";

                if (dom.Type != 3) {

                    //拼接tbody
                    html += StringHelper.FormatStr(trHtml,
                        index + 1,                                        //0 序号
                        dom.Name,                                       //1 大赛名称
                        dom.AddUserName,                                //2 创建人
                        dom._PreliminaryStartTime,                       //3 开始时间
                        dom._PreliminaryEndTime,                         //4 结束时间
                        dom.Id                                          //5 Id
                    );
                }
                else {
                    //拼接tbody
                    html += StringHelper.FormatStr(trHtml,
                        index + 1,                                        //0 序号
                        dom.Name,                                       //1 大赛名称
                        dom.AddUserName,                                //2 创建人
                        dom._PreliminaryStartTime,                       //3 开始时间
                        dom._RematchEndTime,                         //4 结束时间
                        dom.Id                                          //5 Id
                    );
                }
            });
            $("#CompetitionList").html(html);
        }
    });
}

//公告弹窗
function NoticePage() {
    $("#addNotice h3").text("发布公告");
    $("#cmbNotice").attr("disabled", false);
    $("#addNotice .pop-button").show();
    $("#txtNoticeContext").val("");
    $("#txtNoticeContext").attr("readonly", false)
    $('#cmbNotice').removeAttr('disabled');

    dialogHelper.Show("addNotice", 500);
}

//获取公告信息
function GetNotice() {
    pageHelper.Init2({
        url: "/CompetitionUser/Home/GetNoticeList",
        type: "POST",
        pageDiv: "#NoticePages",

        //data:
        //{           
        //    PageSize: 3           
        //},
        bind: function (data) {
            var html = "";

            if (data.Data.length > 0) {
                $(data.Data).each(function (index, dom) {
                    //每行html
                    var trHtml = "";
                    trHtml += "<tr>";
                    trHtml += "<td name=\"dataNo\">{0}</td>";
                    trHtml += "<td class='left'><div  title=\"{1}\">【{3}】{2}</div></td>";
                    trHtml += "<td><a  class=\"chakan\" onclick=\"DetailsNotice('" + dom.Id + "')\">查看</a> ";
                    if ($('#hdCreate').val() == 1) {
                        trHtml += "<a class=\"shanchu\" onclick=\"DeleteNotice('" + dom.Id + "')\">删除</a>";
                    }
                    trHtml += "</td></tr>";
                    html += StringHelper.FormatStr(trHtml, (
                        (data.PageIndex - 1) * data.PageSize + index + 1),
                        dom.Content,
                        dom.Content.toString().ToLeft(10),
                        GetNoticeTypeName(dom.NoticeType)
                   )
                });
                $("#trNotice").html(html);
                $("#NoticePages").show();
            }
            else {
                $("#trNotice").html("<td colspan='3'><h3 style='text-align: center;padding:30px'>暂无相关信息！</h3></td>");
                $("#NoticePages").hide();
                //  $("#trNotice").html("<h5>暂无相关信息！</h5>");
            }
        }
    });
}


// 返回公告类型名称
function GetNoticeTypeName(type) {
    if (type == 1)
        return "系统公告";
    else if (type == 2)
        return "大赛公告";
    else if (type == 3)
        return "温馨提示";
    else if (type == 4)
        return "资讯快报";
}

//保存发布公告
function SaveNotice() {
    if (!VerificationHelper.checkFrom("addNotice"))
        return;

    $.ajax({
        url: "/CompetitionUser/Home/AddNotice",
        type: "POST",
        data: {
            NoticeType: $("#cmbNotice option:selected").val(),
            Content: $.trim($("#txtNoticeContext").val()),
        },
        success: function (data) {
            dialogHelper.Success({
                content: "发布公告成功！"
            });

            dialogHelper.Close("addNotice");
            GetNotice();
        }
    })
}

//公告详情
function DetailsNotice(id) {
    $("#addNotice .pop-button").hide();
    $("#addNotice h3").text("查看公告");
    $.ajax({
        url: "/CompetitionUser/Home/GetNoticeModel",
        type: "POST",
        async: false,
        data: {
            id: id
        },
        success: function (data) {
            $("#cmbNotice").val(data.Data.NoticeType).attr("disabled", true);
            $("#txtNoticeContext").val(data.Data.Content).attr("readonly", true);
            dialogHelper.Show("addNotice", 500);


        }
    });
}

//删除公告
function DeleteNotice(id) {
    dialogHelper.Confirm({
        content: "确定删除该公告？",
        success: function () {
            $.ajax({
                url: "/CompetitionUser/Home/DeleteNotice",
                type: "POST",
                async: true,
                data: {
                    id: id
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        dialogHelper.Success({
                            content: "删除成功！",

                        });
                        //弹出成功提示
                        //code ...

                        //刷新当前页
                        GetNotice();
                    }
                    else {
                        dialogHelper.Error({
                            content: data.ErrorCode
                        });
                    }
                }
            });

        }
    });

}