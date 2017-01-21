; var dialogHelper = (function ($, window, document, undefined) {
    var defaults = {
        title: "",                      //弹出框标题
        content: "",                    //提示内容
        success: function () { },        //点击确认时事件
        cancle: function () { },        //点击取消时事件
        afterSuccess: function () { }   //点击确认，窗体关闭后事件
    };

    /**
	 * 成功提示框
	 */
    var Success = function (options) {
        var opt = $.extend({}, defaults, options || {});

        var size = $(".windowBg").size();
        var index = 260 + (size * 10);

        //弹窗位置居中
        $("#popMsg").each(function () {
            var width = $(this).width() / 2;
            var height = $(this).height() / 2;
            $(this).css("margin-left", -width);
            $(this).css("margin-top", -height);
            $(this).css("z-index", index + 1);
        });
        //标题、内容、图片
        if (opt.title != null && opt.title != "" && opt.title != undefined) {
            $("#popMsg h3").html(opt.title);
        }
        $("#popMsg p span").html(opt.content);
        $("#popMsg img").attr("src", "/Content/images/ico1.png");
        //绑定确定按钮事件
        $("#popMsg input[name='btnSuccess']").unbind("click").click(function () {
            //用户自定义方法
            if (typeof (opt.success) == "function") {
                opt.success();
            }
            //关闭窗口
            $("#popMsg").hide();
            $("#bgpopMsg").remove();
        });
        //关闭窗口
        $("#popMsg .close,#popMsg .btn-close").unbind("click").click(function () {
            //用户自定义方法
            if (typeof (opt.success) == "function") {
                opt.success();
            }
            $("#popMsg").hide();
            $("#bgpopMsg").remove();
        });
        if ($("#bgpopMsg").size() < 1) {
            $("body").append('<div id="bgpopMsg" class="windowBg" style="z-index:' + index + ';" ></div>');
        }
        $("#popMsg").show();
    }

    /**
	 * 错误提示框
	 */
    var Error = function (options) {
        var opt = $.extend({}, defaults, options || {});

        var size = $(".windowBg").size();
        var index = 260 + (size * 10);

        //弹窗位置居中
        $("#popMsg").each(function () {
            var width = $(this).width() / 2;
            var height = $(this).height() / 2;
            $(this).css("margin-left", -width);
            $(this).css("margin-top", -height);
            $(this).css("z-index", index + 1);
        });
        //标题、内容、图片
        if (opt.title != null && opt.title != "" && opt.title != undefined) {
            $("#popMsg h3").html(opt.title);
        }
        //$("#popMsg p span").html(opt.content);
        // 判断错误信息是否已2开头
        if (opt.content.indexOf('2') == 0)
            $("#popMsg p span").html(msgList[opt.content]);
        else
            $("#popMsg p span").html(opt.content);
        $("#popMsg img").attr("src", "/Content/images/ico3.png");
        //绑定确定按钮事件
        $("#popMsg input[name='btnSuccess']").unbind("click").click(function () {
            //用户自定义方法
            if (typeof (opt.success) == "function") {
                opt.success();
            }
            //关闭窗口
            $("#popMsg").hide();
            $("#bgpopMsg").remove();
        });
        //关闭窗口
        $("#popMsg .close,#popMsg .btn-close").unbind("click").click(function () {
            //用户自定义方法
            if (typeof (opt.cancle) == "function") {
                opt.success();
            }
            $("#popMsg").hide();
            $("#bgpopMsg").remove();
        });
        if ($("#bgpopMsg").size() < 1) {
            $("body").append('<div id="bgpopMsg" class="windowBg" style="z-index:' + index + ';" ></div>');
        }
        $("#popMsg").show();
    };

    /**
	 * 确认提示框
	 */
    var Confirm = function (options) {
        var opt = $.extend({}, defaults, options || {});

        var size = $(".windowBg").size();
        var index = 260 + (size * 10);

        //弹窗位置居中
        $("#popConfirm").each(function () {
            var width = $(this).width() / 2;
            var height = $(this).height() / 2;
            $(this).css("margin-left", -width);
            $(this).css("margin-top", -height);
            $(this).css("z-index", index + 1);
        });
        //标题、内容、图片
        if (opt.title != null && opt.title != "" && opt.title != undefined) {
            $("#popMsg h3").html(opt.title);
        }
        $("#popConfirm p span").html(opt.content);
        $("#popConfirm img").attr("src", "/Content/images/ico2.png");
        //绑定确定按钮事件
        $("#popConfirm input[name='btnSuccess']").unbind("click").click(function () {
            //用户自定义方法
            if (typeof (opt.success) == "function") {
                opt.success();
            }
            //关闭窗口
            $("#popConfirm").hide();
            $("#bgpopConfirm").remove();
            if (typeof (opt.afterSuccess) == "function") {
                opt.afterSuccess();
            }
        });
        //关闭窗口
        $("#popConfirm .close,#popConfirm .btn-close").unbind("click").click(function () {
            //用户自定义方法
            if (typeof (opt.cancle) == "function") {
                opt.cancle();
            }
            $("#popConfirm").hide();
            $("#bgpopConfirm").remove();
        });
        if ($("#bgpopConfirm").size() < 1) {
            $("body").append('<div id="bgpopConfirm" class="windowBg" style="z-index:' + index + ';" ></div>');
        }
        $("#popConfirm").show();
    };

    /**
	 * 显示弹出框
	 * @param id 弹出框Id
     * @param width 弹出框宽度(默认420px)
	 */
    var Show = function (id, width, myCloseFun) {
        var d_width = 420;
        if (width != undefined && typeof (width) == "number") {
            d_width = width
        }

        var size = $(".windowBg").size();
        var index = 260 + (size * 10);

        $("#" + id).css({ "width": d_width + "px", "margin-left": "-" + (d_width / 2) + "px", "z-index": (index + 1) });
        $("#" + id + " .close").unbind("click").click(function () {
            if (typeof (myCloseFun) == "function") {
                myCloseFun();
            }
            Close(id);
        });

        //居中显示
        $("#" + id).each(function () {
            var width = $(this).width() / 2;
            var height = $(this).height() / 2;
            $(this).css("margin-left", -width);
            $(this).css("margin-top", -height);
        });

        //清空错误信息
        $("#" + id + " .warn-box").remove();

        //遮罩层
        if ($("#bg" + id).size() < 1) {
            $("body").append('<div id="bg' + id + '" class="windowBg" style="z-index:' + index + ';" ></div>');
        }

        $("#" + id).show();
    };

    /**
	 * 隐藏弹出框
	 * @param id 弹出框Id
	 */
    var Close = function (id) {
        $("#" + id).hide();
        var size = $("#bg" + id).size();
        for (var i = 0 ; i < size; i++) {
            $("#bg" + id).remove();
        }
    };

    /**
	 * 弹出框复位居中
	 * @param id 弹出框Id
	 */
    var Reset = function (id) {
        //居中显示
        $("#" + id).each(function () {
            var width = $(this).width() / 2;
            var height = $(this).height() / 2;
            $(this).css("margin-left", -width);
            $(this).css("margin-top", -height);
        });
    };

    return {
        Success: Success,
        Error: Error,
        Confirm: Confirm,
        Show: Show,
        Close: Close,
        Reset: Reset
    };
})(jQuery, window, document);