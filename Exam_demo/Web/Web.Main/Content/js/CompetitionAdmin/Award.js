$(function () {
    GetAwardList();
    Save();
})

//获取奖项设置列表
function GetAwardList() {
    $.ajax({
        url: "/CompetitionAdmin/Award/GetAwardList",
        type: "POST",
        async: false,
        success: function (data) {
            if (data.Data != null && data.Data.length > 0) {
                $(data.Data).each(function (index, dom) {
                    if (dom.AwardType == 1)//特等
                    {
                        if (dom.IsVisible)
                            $("#chb_td").attr("checked", "checked");
                        else
                            $("#chb_td").removeAttr("checked");
                        $(".tedeng").val(dom.AwardTypeComment);
                        $("#ttedeng").val(dom.AwardDescription);
                        $("#hdtd").val(dom.Id);
                    }
                    else if (dom.AwardType == 2)//一等奖
                    {
                        if (dom.IsVisible)
                            $("#chb_yd").attr("checked", "checked");
                        else
                            $("#chb_yd").removeAttr("checked");
                        $(".yideng").val(dom.AwardTypeComment);
                        $("#tyideng").val(dom.AwardDescription != null ? dom.AwardDescription : "");
                        $("#hdyd").val(dom.Id);
                    }
                    else if (dom.AwardType == 3)//二等奖
                    {
                        if (dom.IsVisible)
                            $("#chb_ed").attr("checked", "checked");
                        else
                            $("#chb_ed").removeAttr("checked");
                        $(".erdeng").val(dom.AwardTypeComment);
                        $("#terdeng").val(dom.AwardDescription);
                        $("#hded").val(dom.Id);
                    }
                    else if (dom.AwardType == 4)//三等奖
                    {
                        if (dom.IsVisible)
                            $("#chb_sd").attr("checked", "checked");
                        else
                            $("#chb_sd").removeAttr("checked");
                        $(".sandeng").val(dom.AwardTypeComment);
                        $("#tsandeng").val(dom.AwardDescription);
                        $("#hdsd").val(dom.Id);
                    }
                    else if (dom.AwardType == 5)//优秀奖
                    {
                        if (dom.IsVisible)
                            $("#chb_yx").attr("checked", "checked");
                        else
                            $("#chb_yx").removeAttr("checked");
                        $(".youxiu").val(dom.AwardTypeComment);
                        $("#tyouxiu").val(dom.AwardDescription);
                        $("#hdyx").val(dom.Id);
                    }
                    else if (dom.AwardType == 6)//成功参赛等奖
                    {
                        if (dom.IsVisible)
                            $("#chb_cs").attr("checked", "checked");
                        else
                            $("#chb_cs").removeAttr("checked");
                        $(".cansai").val(dom.AwardTypeComment);
                        $("#tcansai").val(dom.AwardDescription);
                        $("#hdcs").val(dom.Id);
                    }
                })
            }
        }
    });
}

//保存奖项设置
function Save() {
    $("#btnSaveAward").click(function () {
        $.ajax({
            url: "/CompetitionAdmin/Award/DelAndAdd",
            type: "POST",
            async: false,
            dataType: "json",
            data: {
                chbtd: $("#chb_td").is(':checked') == true ? 1 : 0,
                hdtd: $("#hdtd").val(),
                tdAwardTypeComment: $(".tedeng").val(),
                tdAwardDescription: $("#ttedeng").val(),


                chbyd: $("#chb_yd").is(':checked') == true ? 1 : 0,
                hdyd: $("#hdyd").val(),
                ydAwardTypeComment: $(".yideng").val(),
                ydAwardDescription: $("#tyideng").val(),

                chbed: $("#chb_ed").is(':checked') == true ? 1 : 0,
                hded: $("#hded").val(),
                edAwardTypeComment: $(".erdeng").val(),
                edAwardDescription: $("#terdeng").val(),

                chbsd: $("#chb_sd").is(':checked') == true ? 1 : 0,
                hdsd: $("#hdsd").val(),
                sdAwardTypeComment: $(".sandeng").val(),
                sdAwardDescription: $("#tsandeng").val(),

                chbyx: $("#chb_yx").is(':checked') == true ? 1 : 0,
                hdyx: $("#hdyx").val(),
                yxAwardTypeComment: $(".youxiu").val(),
                yxAwardDescription: $("#tyouxiu").val(),

                chbcs: $("#chb_cs").is(':checked') == true ? 1 : 0,
                hdcs: $("#hdcs").val(),
                csAwardTypeComment: $(".cansai").val(),
                csAwardDescription: $("#tcansai").val()
            },
            success: function (data) {
                if (data.IsSuccess) {
                    GetAwardList();
                    dialogHelper.Success({
                        content: msgList["20010"],
                        success: function () {
                            location.href = location.href;
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
    })
}