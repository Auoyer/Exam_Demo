/**
 * @name 表单验证JS帮助类
 * @remark IsRequired 必输验证
 * @remark IsMaxLength 验证最大长度
 * @remark IsMinLength 验证最小长度
 * @remark IsFloat 验证浮点数
 * @remark IsFloat1 保留一位小数
 * @remark IsMaxFloat 验证最大浮点数
 * @remark IsMinFloat 验证最小浮点数
 * @remark IsNumber 验证整数
 * @remark IsNumberby100 验证100的整数倍
 * @remark IsReg 验证正则表达式
 * @remark IsEmail 验证邮箱格式
 * @remark IsMobile 验证手机格式
 * @remark IsPhone 验证电话号码格式
 * @remark IsIDCard 验证身份证格式
 * @remark IsCode 验证登录账号
 * @remark IsName 验证姓名和学校名称，只能输入汉字、字母和数字
 * @remark IsRemote 验证数据唯一性(RemoteUrl 数据验证地址,RemoteKey1,RemoteKey2,RemoteKey3... 数据验证传输字段名，RemoteValue1,RemoteValue2,RemoteValue3... 数据验证传输值的页面元素的Id)
 * @remark 
 * @remark 
 */
; var VerificationHelper = (function ($, window, document, undefined) {
    /**
	 * 返回content中输入框验证结果
	 * @param content div或from的ID
	 * @return 验证结果：true，验证通过；false，验证不通过
	 */
    var checkFrom = function (content, myFun) {
        content = content || "body";
        content = content == "body" ? content : "#" + content;

        //去除所有错误提示
        $(".warn-box", content).remove();
        //验证
        $("input[type='text']", content).each(function (index, dom) {
            //去前后空格
            $(this).val($.trim($(this).val()));
            checkInput($(this));
        });
        $("input[type='password']", content).each(function (index, dom) {
            //去前后空格
            $(this).val($.trim($(this).val()));
            checkInput($(this));
        });
        $("select", content).each(function (index, dom) {
            checkSelect($(this));
        });
        $("textarea", content).each(function (index, dom) {
            //去前后空格
            $(this).val($.trim($(this).val()));
            checkTextarea($(this));
        });
        //自定义验证方法
        if (typeof (myFun) == "function") {
            myFun();
        }
        //若控件值发生变动，则去除错误提示
        $("input[type='text'],input[type='password'],textarea", content).unbind("change").change(function () {
            $(this).next(".warn-box").remove();
        });
        $("select", content).change(function () {
            $(this).next(".warn-box").remove();
        });


        //检测页面是否有错误提示，有则返回false
        if ($(".warn-box", content).size() > 0) {
            return false;
        } else {
            return true;
        }
    };
    /**
	 * 检测input输入
	 * @param textBox jquery对象
	 */
    var checkInput = function (textBox) {
        if (textBox.attr("disabled") == "disabled")
            return;
        //验证必填项
        if (textBox.hasClass("IsRequired") && ($.trim(textBox.val()) == "" || textBox.val() == undefined)) {
            showValidateMsg(textBox.attr("id"), "请输入" + textBox.attr("MsgName"));
            return;
        }
        //验证最大长度
        if (textBox.hasClass("IsMaxLength") && $.trim(textBox.val()).length > 0 && $.trim(textBox.val()).length > parseInt(textBox.attr("MaxLength"))) {
            showValidateMsg(textBox.attr("id"), textBox.attr("MsgName") + "的长度不能大于" + textBox.attr("MaxLength"));
            return;
        }
        //验证最小长度
        if (textBox.hasClass("IsMinLength") && $.trim(textBox.val()).length > 0 && $.trim(textBox.val()).length < parseInt(textBox.attr("MinLength"))) {
            showValidateMsg(textBox.attr("id"), textBox.attr("MsgName") + "的长度不能小于" + textBox.attr("MinLength"));
            return;
        }

        //验证整数
        if (textBox.hasClass("IsNumber") && $.trim(textBox.val()).length > 0 && !/^[-]?\d+$/.test($.trim(textBox.val()))) {
            showValidateMsg(textBox.attr("id"), textBox.attr("MsgName") + "必须为整数");
            return;
        }


        //验证最大数
        if (textBox.hasClass("IsMaxNumber") && parseInt($.trim(textBox.val()), 10) > parseInt(textBox.attr("MaxNumber"), 10)) {
            showValidateMsg(textBox.attr("id"), textBox.attr("MsgName") + "不能大于" + textBox.attr("MaxNumber"));
            return;
        }
        //验证最大数(含最大值)
        if (textBox.hasClass("IsMaxNumber1") && parseInt($.trim(textBox.val()), 10) >= parseInt(textBox.attr("MaxNumber"), 10)) {
            showValidateMsg(textBox.attr("id"), textBox.attr("MsgName") + "不能大于" + textBox.attr("MaxNumber"));
            return;
        }
        //验证最小数
        if (textBox.hasClass("IsMinNumber") && parseInt($.trim(textBox.val()), 10) < parseInt(textBox.attr("MinNumber"), 10)) {
            showValidateMsg(textBox.attr("id"), textBox.attr("MsgName") + "不能小于" + textBox.attr("MinNumber"));
            return;
        }
        //验证最小数（含最小值）
        if (textBox.hasClass("IsMinNumber1") && parseInt($.trim(textBox.val()), 10) <= parseInt(textBox.attr("MinNumber"), 10)) {
            showValidateMsg(textBox.attr("id"), textBox.attr("MsgName") + "不能小于" + textBox.attr("MinNumber"));
            return;
        }

        //验证浮点数
        if (textBox.hasClass("IsFloat") && $.trim(textBox.val()).length > 0 && !/^[-]?\d+(\.\d+)?$/.test($.trim(textBox.val()))) {
            showValidateMsg(textBox.attr("id"), textBox.attr("MsgName") + "必须为数字");
            return;
        }

        //验证正则表达式
        if (textBox.hasClass("IsReg") && $.trim(textBox.val()).length > 0 && !new RegExp("^" + textBox.attr("Reg") + "$").test($.trim(textBox.val()))) {
            showValidateMsg(textBox.attr("id"), textBox.attr("MsgReg"));
            return;
        }



        //验证浮点数(含负数验证)
        if (textBox.hasClass("IsFloat1") && $.trim(textBox.val()).length > 0 && !/^[-]?\d+(\.\d+)?$/.test($.trim(textBox.val()))) {

            showValidateMsg(textBox.attr("id"), textBox.attr("MsgName") + "必须为数字");
            return;
        }

        //保留一位小数
        if (textBox.hasClass("IsFloat3") && $.trim(textBox.val()).length > 0 && !/^\d+(\.\d{0,1})?$/.test($.trim(textBox.val()))) {
            showValidateMsg(textBox.attr("id"), textBox.attr("MsgName") + "最多为1位小数");
            return;
        }

        //验证2位小数
        if (textBox.hasClass("IsFloat2") && $.trim(textBox.val()).length > 0 && !/^[-]?\d+(\.\d{1,2})?$/.test($.trim(textBox.val()))) {
            showValidateMsg(textBox.attr("id"), textBox.attr("MsgName") + "最多为2位小数");
            return;
        }

        //验证4位小数
        if (textBox.hasClass("IsFloat4") && $.trim(textBox.val()).length > 0 && !/^[-]?\d+(\.\d{1,4})?$/.test($.trim(textBox.val()))) {
            showValidateMsg(textBox.attr("id"), textBox.attr("MsgName") + "最多为4位小数");
            return;
        }

        //验证最大浮点数
        if (textBox.hasClass("IsMaxFloat") && parseFloat($.trim(textBox.val())) > parseFloat(textBox.attr("MaxFloat"))) {
            showValidateMsg(textBox.attr("id"), textBox.attr("MsgName") + "不能大于" + textBox.attr("MaxFloat"));
            return;
        }
        //验证最小浮点数
        if (textBox.hasClass("IsMinFloat") && parseFloat($.trim(textBox.val())) < parseFloat(textBox.attr("MinFloat"))) {
            showValidateMsg(textBox.attr("id"), textBox.attr("MsgName") + "不能小于" + textBox.attr("MinFloat"));
            return;
        }





        //验证100的整数倍
        if (textBox.hasClass("IsNumberby100") && $.trim(textBox.val()).length > 0 && ($.trim(textBox.val()) % 100 != 0)) {
            showValidateMsg(textBox.attr("id"), textBox.attr("MsgName") + "必须为100的整数倍");
            return;
        }

        //验证10的整数倍
        if (textBox.hasClass("IsNumberby10") && $.trim(textBox.val()).length > 0 && ($.trim(textBox.val()) % 10 != 0)) {
            showValidateMsg(textBox.attr("id"), textBox.attr("MsgName") + "必须为10的整数倍");
            return;
        }

        //验证0.5的整数倍
        if (textBox.hasClass("IsNumberby5") && $.trim(textBox.val()).length > 0 && ($.trim(textBox.val()) % 0.5 != 0)) {
            showValidateMsg(textBox.attr("id"), textBox.attr("MsgName") + "必须为0.5的整数倍");
            return;
        }

        ////验证邮箱
        //if (textBox.hasClass("IsMail") && $.trim(textBox.val()).length > 0 && $.trim(textBox.val()) != "" && !/^\w+@\w+(\.\w+)+$/.test($.trim(textBox.val()))) {
        //    showValidateMsg(textBox.attr("id"), "邮箱格式不正确");
        //    return;
        //}

        //验证邮箱
        if (textBox.hasClass("IsEmail") && $.trim(textBox.val()).length > 0 && $.trim(textBox.val()) != "" && !/^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test($.trim(textBox.val()))) {
            showValidateMsg(textBox.attr("id"), "邮箱格式不正确");
            return;
        }

        //验证手机号码
        if (textBox.hasClass("IsMobile") && $.trim(textBox.val()).length > 0 && $.trim(textBox.val()) != "" && !/^1[3|4|5|7|8]\d{9}$/.test($.trim(textBox.val()))) {
            showValidateMsg(textBox.attr("id"), textBox.attr("MsgName") + "格式错误");
            return;
        }


        //验证手机号码
        if (textBox.hasClass("IsPhone") && $.trim(textBox.val()).length > 0 && $.trim(textBox.val()) != "" && !/^((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{1,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{1,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)$/.test($.trim(textBox.val()))) {
            showValidateMsg(textBox.attr("id"), textBox.attr("MsgName") + "格式错误");
            return;
        }

        //验证身份证号码
        if (textBox.hasClass("IsIDCard") && $.trim(textBox.val()).length > 0 && $.trim(textBox.val()) != "" && !/^\d{18}$|^\d{17}(\d|X|x)/.test($.trim(textBox.val()))) {
            showValidateMsg(textBox.attr("id"), "身份证号码格式不正确");
            return;
        }


        //验证登录账号，数字字母和_
        if (textBox.hasClass("IsCode") && $.trim(textBox.val()).length > 0 && $.trim(textBox.val()) != "" && !/^([a-zA-Z0-9]|[_]){2,20}$/.test($.trim(textBox.val()))) {
            showValidateMsg(textBox.attr("id"), "请输入数字、字母和下划线");
            return;
        }


        //验证姓名，汉字、数字和字母
        if (textBox.hasClass("IsName") && $.trim(textBox.val()).length > 0 && $.trim(textBox.val()) != "" && !/^[a-zA-Z0-9\u4e00-\u9fa5]*$/.test($.trim(textBox.val()))) {
            showValidateMsg(textBox.attr("id"), "请输入汉字、数字和字母");
            return;
        }


        //验证位数
        if (textBox.hasClass("IsDigit")) {
            var Digit = textBox.attr("Digit") ? textBox.attr("Digit") : "";
            if (Digit != "") {
                var a = Digit.split(",");
                var i = a[0]//整数部分
                var j = a[1]//小数部分
                if (!new RegExp("^\\d{0," + i + "}\\.*\\d{0," + j + "}$").test($.trim(textBox.val()))) {
                    showValidateMsg(textBox.attr("id"), DigitMessage(i, j));
                    return;
                }
            }
        }


        //验证数据唯一性
        if (textBox.hasClass("IsRemote")) {
            var romoteUrl = textBox.attr("RemoteUrl") ? textBox.attr("RemoteUrl") : "";
            //var romoteId = textBox.attr("RemoteId") ? $("#" + textBox.attr("RemoteId")).val() : "0";
            //var romoteKey = textBox.attr("RemoteKey") ? textBox.attr("RemoteKey") : "";

            var data = {};
            //data["Id"] = romoteId;

            var size = textBox.get(0).attributes.length;
            var attrs = textBox.get(0).attributes;
            for (var i = 0; i < size; i++) {
                if (attrs[i].name.indexOf("remotekey") > -1) {
                    var index = attrs[i].name.replace("remotekey", "");

                    data[attrs[i].value] = $("#" + textBox.attr("RemoteValue" + index)).val();
                }
            }

            //data[romoteKey] = $.trim(textBox.val());
            data["rid"] = Math.random();
            var validationResult = false;
            if (romoteUrl != "" && $.trim(textBox.val()).length > 0) {
                $.ajax({
                    url: romoteUrl,
                    dataType: "json",
                    async: false, //验证设为同步检测
                    data: data,
                    type: "POST",
                    success: function (data) {
                        if (data) {
                            validationResult = true;
                            showValidateMsg(textBox.attr("id"), "该" + textBox.attr("MsgName") + "已存在");
                        }
                    }
                });
            }
            if (validationResult)
                return;
        }
        //非法字符验证
        if ($.trim(textBox.val()).length > 0 && /((<[^>]#+>)|(<[^>]+)|([^>]+>))/g.test(textBox.val())) {
            showValidateMsg(textBox.attr("id"), "请不要输入非法字符！");
            return;
        }
    };
    /**
     * 检测select输入
     * @param select jquery对象
     */
    var checkSelect = function (select) {
        //验证必填项
        if (select.hasClass("IsRequired") && (select.val() == "" || select.val() == undefined || select.val() == "0" || select.val().indexOf("请选择") > 0)) {
            showValidateMsg(select.attr("id"), "请选择" + select.attr("MsgName"));
            return;
        }
    };
    /**
     * 检测textarea输入
     * @param textarea jquery对象
     */
    var checkTextarea = function (textarea) {
        //验证必填项
        if (textarea.hasClass("IsRequired") && ($.trim(textarea.val()) == "" || textarea.val() == undefined)) {
            showValidateMsg2(textarea.attr("id"), "请输入" + textarea.attr("MsgName"));
            return;
        }
        //验证最大长度
        if (textarea.hasClass("IsMaxLength") && $.trim(textarea.val()).length > 0 && $.trim(textarea.val()).length > parseInt(textarea.attr("MaxLength"))) {
            showValidateMsg2(textarea.attr("id"), textarea.attr("MsgName") + "的长度不能大于" + textarea.attr("MaxLength") + "字");
            return;
        }
        //验证最小长度
        if (textarea.hasClass("IsMinLength") && $.trim(textarea.val()).length > 0 && $.trim(textarea.val()).length < parseInt(textarea.attr("MinLength"))) {
            showValidateMsg2(textarea.attr("id"), textarea.attr("MsgName") + "的长度不能小于" + textarea.attr("MinLength") + "字");
            return;
        }
        //非法字符验证
        if ($.trim(textarea.val()).length > 0 && /<\/?[^>]*>/g.test(textarea.val())) {
            showValidateMsg2(textarea.attr("id"), "请不要输入非法字符！");
            return;
        }
    };

    /**
     * 设置表单内容是否只读
     * @param content div或from的ID
     * @param flag 设置表单只读：true，只读；false，可输入
     */
    var ChangeStatus = function (content, flag) {
        content = content || "body";
        content = content == "body" ? content : "#" + content;
        if (flag) {
            //设为只读
            $("input[type='text'],input[type='checkbox'],select,textarea", content).each(function (index, dom) {
                $(this).attr({ "disabled": "disabled" });
            });
        } else {
            //取消只读
            $("input[type='text'],input[type='checkbox'],select,textarea", content).each(function (index, dom) {
                $(this).removeAttr("disabled");
            });
        }
    };

    //   /**
    //* 错误提示1(input,select使用)
    //* @param ctrlName 需要提示的dom元素ID
    //* @param msg 提示信息
    //*/
    //   var showValidateMsg = function (ctrlName, msg) {
    //       $("#" + ctrlName).addClass("warn");
    //       var validMsg = "<div class=\"warn-box\" id=\"warn-" + ctrlName + "\" style=\"display: inline-block;\"><b style=\"display: none;\"></b><p style=\"display: none;\">" + msg + "</p></div>";
    //       $(validMsg).insertAfter($("#" + ctrlName));
    //       $("#warn-" + ctrlName).hover(function () {
    //           $("#warn-" + ctrlName + " > b").toggle();
    //           $("#warn-" + ctrlName + " > p").toggle();
    //       });
    //       //alert(msg);
    //   };
    //   /**
    //    * 错误提示2(textarea使用)
    //    * @param ctrlName 需要提示的dom元素ID
    //    * @param msg 提示信息
    //    */
    //   var showValidateMsg2 = function (ctrlName, msg) {
    //       $("#" + ctrlName).addClass("warn");
    //       var outerHeight = $("#" + ctrlName).outerHeight();
    //       var validMsg = "<div class=\"warn-box\" id=\"warn-" + ctrlName + "\" style=\"display: inline-block;z-index: 12;margin-top:-" + outerHeight + "px;\"><b style=\"display: none;\"></b><p style=\"display: none;\">" + msg + "</p></div>";
    //       $(validMsg).insertAfter($("#" + ctrlName));
    //       $("#warn-" + ctrlName).hover(function () {
    //           $("#warn-" + ctrlName + " > b").toggle();
    //           $("#warn-" + ctrlName + " > p").toggle();
    //       });
    //       //alert(msg);
    //   };

    return {
        checkFrom: checkFrom,
        ChangeStatus: ChangeStatus
    };
})(jQuery, window, document);

/**
 * 错误提示1(input,select使用)
 * @param ctrlName 需要提示的dom元素ID
 * @param msg 提示信息
 */
function showValidateMsg(ctrlName, msg) {
    if ($("#warn-" + ctrlName).size()) {
        return;
    }
    $("#" + ctrlName).addClass("warn");
    var validMsg = "<div class=\"warn-box\" id=\"warn-" + ctrlName + "\" style=\"display: inline-block;\"><b style=\"display: none;\"></b><p style=\"display: none;\">" + msg + "</p></div>";
    $(validMsg).insertAfter($("#" + ctrlName));
    $("#warn-" + ctrlName).hover(function () {
        $("#warn-" + ctrlName + " > b").toggle();
        $("#warn-" + ctrlName + " > p").toggle();
    });
    //alert(msg);
};
/**
 * 错误提示2(textarea使用)
 * @param ctrlName 需要提示的dom元素ID
 * @param msg 提示信息
 */
function showValidateMsg2(ctrlName, msg) {
    if ($("#warn-" + ctrlName).size()) {
        return;
    }
    $("#" + ctrlName).addClass("warn");
    var outerHeight = $("#" + ctrlName).outerHeight();
    var validMsg = "<div class=\"warn-box\" id=\"warn-" + ctrlName + "\" style=\"display: inline-block;z-index: 12;margin-top:-" + outerHeight + "px;\"><b style=\"display: none;\"></b><p style=\"display: none;\">" + msg + "</p></div>";
    $(validMsg).insertAfter($("#" + ctrlName));
    $("#warn-" + ctrlName).hover(function () {
        $("#warn-" + ctrlName + " > b").toggle();
        $("#warn-" + ctrlName + " > p").toggle();
    });
    //alert(msg);
};


function DigitMessage(i, j) {
    var str = "";
    var str1 = "0.";
    var str2 = "";
    for (i1 = 0; i1 < i; i1++) {
        str2 = str2 + "9";
    }
    for (j1 = 0; j1 < j; j1++) {
        str1 = str1 + "0";
        if (j1 == 0) { str2 = str2 + ".9"; }
        else { str2 = str2 + "9"; }
    }


    str = "请输入" + str1 + "到" + str2 + "之间的数字";


    return str;
}