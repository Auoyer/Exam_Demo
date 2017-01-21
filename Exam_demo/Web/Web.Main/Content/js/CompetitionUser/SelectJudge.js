$(function () {


    // 添加评委，打开评委选择弹框页面
    $('#btnSelectJudge').click(function () {

        $.ajax({
            url: '/CompetitionAdmin/Judge/SelectJudge',
            type: "GET",
            async: false,
            success: function (data) {
                // 调用弹框页面加载列表数据方法
                GetSelectList();

                $('#popDisplayJudeg').html(data);

                dialogHelper.Show("popDisplayJudeg", 750);
            }
        });
    });
});

// 获取评委信息
function GetSelectList() {
    pageHelper.Init({
        url: "/CompetitionAdmin/Judge/GetJudgeList",
        type: "POST",
        pageDiv: "#pages_selectJudge",
        //async: false,
        data: {
            userName: $("#seletJudgeSearchName").val()
        },
        bind: function (data) {
            var html = "";
            $(data.Data).each(function (index, dom) {
                //每行html
                var trHtml = "";
                trHtml += "<tr>";
                trHtml += "<td><input class=\"icheck\" type=\"checkbox\" id=\"chkUser_{5}\" val=\"{5}\" name=\"chbSelectJudge\"></td>";
                trHtml += "<td><div title=\"{0}\" class=\"ellipsis\">{0}</div></td>";
                trHtml += "<td><div title=\"{1}\" class=\"ellipsis\">{1}</div></td>";
                trHtml += "<td>{2}</td>";
                trHtml += "<td><div title=\"{3}\" class=\"ellipsis\">{3}</div></td>";
                trHtml += "<td><div title=\"{4}\" class=\"ellipsis\">{4}</div></td>";
                trHtml += "</tr>";
                //拼接tbody
                html += StringHelper.FormatStr(trHtml,
                    dom.AccountNo,                                       //0 账号
                    dom.UserName,                                           //1 姓名
                    dom.Phone,                                            //2 联系方式
                    dom.CollegeName,                                        //3 学院
                    dom.Email,                                          //4 邮箱
                    dom.Id                                                  //5 Id
                );
            });
            $("#selectJudgeList").html(html);

            dialogHelper.Close("popDisplayJudeg");
            dialogHelper.Show("popDisplayJudeg", 600);
        }
    });
}

// 保存，回调
function SelectJudgeCallBack() {
    var checked = '';

    var model = new Array();

    var index = 0;
    $('#selectJudgeList input[name="chbSelectJudge"]').each(function () {
        if ($(this).is(":checked")) {
            var temp = new Object();
            temp.Id = $(this).attr('val');          // 评委ID
            temp.AccountNo = $(this).parent().parent().find('td').eq(1).find('div').html();              // 账号
            temp.Name = $(this).parent().parent().find('td').eq(2).find('div').html();              // 姓名
            temp.Phone = $(this).parent().parent().find('td').eq(3).html();              // 联系方式
            temp.CollegeName = $(this).parent().parent().find('td').eq(4).find('div').html();              // 学校    
            temp.Email = $(this).parent().parent().find('td').eq(5).find('div').html();              // 学校    
            model[index] = temp;
            index++;
        }
    });

    // 判断是否有选择
    if (index != 0) {
        // 判断选择的评委和已选择的评委是否有重复
        var isReport = false;
        $('#createMatchAddJudge tr').each(function () {
            for (var i = 0; i < model.length; i++) {
                if ($(this).attr('rel') == model[i].Id) {
                    dialogHelper.Error({
                        content: "您勾选的第" + (i + 1) + "个评委已添加，不能重复选择！"
                    });
                    isReport = true;
                    return;
                }
            }
        });

        if (isReport)
            return;

        // 没有重复的了， 写入到页面
        var html = "";
        for (var i = 0; i < model.length; i++) {
            html += "<tr rel='" + model[i].Id + "'>";
            html += "<td>" + ($('#createMatchAddJudge tr').length + i + 1) + "</td>";
            html += "<td>" + model[i].AccountNo + "</td>";
            html += "<td>" + model[i].Name + "</td>";
            html += "<td>" + model[i].Email + "</td>";
            html += "<td>" + model[i].CollegeName + "</td>";
            html += "<td>" + model[i].Phone + "</td>";
            html += "<td onclick='DeleteSelectJudeg(" + model[i].Id + ")' style='cursor: pointer;'>删除</td>";
            html += "</tr>";
        }
        $('#createMatchAddJudge').append(html);

        dialogHelper.Close("popDisplayJudeg");
    }
    else {
        dialogHelper.Error({
            content: "请选择要添加的评委！"
        });
        return;
    }



    return;
}

// 取消
function SelectJudgeClose() {
    dialogHelper.Close("popDisplayJudeg");
}


// 删除当前行
function DeleteSelectJudeg(judgeId) {
    // 删除当前行
    $('#createMatchAddJudge tr').each(function () {
        if ($(this).attr('rel') == judgeId) {
            $(this).remove();
        }
    });

    // 序号重新开始排序
    $('#createMatchAddJudge tr').each(function (index) {
        $(this).find('td').eq(0).html(index + 1);
    });
}