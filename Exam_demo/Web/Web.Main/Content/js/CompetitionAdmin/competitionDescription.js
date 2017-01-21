$(function () {
    SaveFile();
    GetCompetitionDes();
});



//保存文件
function SaveFile() {
    $("#btnSaveFile").click(function () {;
        $.ajax({
            url: "/CompetitionAdmin/CompetitionDes/SaveFile",
            type: "POST",
            async: false,            
            data: {
                ComDesSettings:$.trim($("#tComDesSettings").val()),
                EventSchedule:$.trim($("#tEventSchedule").val()),
                TroubleShooting:$.trim($("#tTroubleShooting").val()),           
                Id: $("#hidCompetitionDescriptionId").val()
            },
            success: function (data) {
                if (data.IsSuccess) {
                    dialogHelper.Success({
                        content: msgList["20010"],
                        success: function () {
                            GetCompetitionDes();
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

function GetCompetitionDes() {
    $.ajax({
        url: "/CompetitionAdmin/CompetitionDes/GetCompetitionDescriptionModel",
        type: "POST",
        async: false,
        success: function (data) {
            if (data.Data != null) {
                $("#tComDesSettings").val(data.Data.ComDesSettings != null ? data.Data.ComDesSettings : "");
                $("#tEventSchedule").text(data.Data.EventSchedule != null ? data.Data.EventSchedule : "");
                $("#tTroubleShooting").text(data.Data.TroubleShooting != null ? data.Data.TroubleShooting : "");

                $("#hidCompetitionDescriptionId").val(data.Data.Id);
            }          
        }
    });
}

