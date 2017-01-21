//全局变量，备份原有的ajax方法
var _ajax_backup = null;
var _ajax_helper = null;
(function ($) {
    //备份jquery的ajax方法  
    var _ajax = $.ajax;
    _ajax_backup = $.ajax;

    _ajaxhepler = $.ajax;

    //重写jquery的ajax方法  
    $.ajax = function (options) {
        var defaults = {
            type: "GET",//请求方式
            url: "",
            data: {},
            datatype: "json", //返回数据类型,
            async: true, //异步
            error: function (XMLHttpRequest, textStatus, errorThrown) { },
            success: function (data, textStatus) { },
            complete: function (XMLHttpRequest, textStatus) { },
            myComplete: function (data, textStatus) { }
        };
        //将默认参数与自定义参数合并
        var opt = {};
        opt = $.extend({}, defaults, options || {});

        //扩展增强处理  
        var _opt = $.extend(true, {}, opt);
        _opt.success = function (data, textStatus) {

            if (data.IsSuccess) {
                //成功
                if (data.Url != null && data.Url != "") {
                    location.href = data.Url;
                    return;
                }
                for (var o in data.Data) {
                    if (typeof (data.Data[o]) == "string") {
                        data.Data[o] = htmlDecode(data.Data[o]);
                    }
                }

                opt.success(data.Data, textStatus);
            } else {
                //如果页面已经有了错误框，则不再弹出新的错误
                var flag = $("#popMsg").css("display") == "block";
                if (!flag) {
                    if (data.Url != null && data.Url != "") {
                        var msg = msgList[data.ErrorCode];
                        if (msg == null || msg == "" || msg == undefined) {
                            msg = data.ErrorCode;
                        }

                        dialogHelper.Error({
                            content: msg,
                            success: function () {
                                location.href = data.Url;
                            },
                            cancle: function () {
                                location.href = data.Url;
                            }
                        });
                        return;
                    } else {
                        var msg = msgList[data.ErrorCode];
                        if (msg == null || msg == "" || msg == undefined) {
                            msg = data.ErrorCode;
                        }
                        dialogHelper.Error({ content: msg });
                        return;
                    }
                }
            }
            if (opt.myComplete != undefined && typeof (opt.myComplete) == "function") {
                opt.myComplete();
            }
        };

        _opt.complete = function (XMLHttpRequest, textStatus) {
            if (typeof (opt.complete) == "function") {
                opt.complete();
            }
            $(".background,.progressBar").hide();
        };

        $(".background,.progressBar").show();
        _ajax(_opt);
    };

    //重写jquery的ajax方法  
    _ajaxhepler = function (options) {
        var defaults = {
            type: "GET",//请求方式
            url: "",
            data: {},
            datatype: "json", //返回数据类型,
            async: true, //异步
            error: function (XMLHttpRequest, textStatus, errorThrown) { },
            success: function (data, textStatus) { },
            complete: function (XMLHttpRequest, textStatus) { },
            myComplete: function (data, textStatus) { }
        };
        //将默认参数与自定义参数合并
        var opt = {};
        opt = $.extend({}, defaults, options || {});

        //扩展增强处理  
        var _opt = $.extend(true, {}, opt);
        _opt.success = function (data, textStatus) {

            if (data.IsSuccess) {
                //成功
                if (data.Url != null && data.Url != "") {
                    location.href = data.Url;
                    return;
                }
                for (var o in data.Data) {
                    if (typeof (data.Data[o]) == "string") {
                        data.Data[o] = htmlDecode(data.Data[o]);
                    }
                }

                opt.success(data.Data, textStatus);
            } else {
                //如果页面已经有了错误框，则不再弹出新的错误
                var flag = $("#popMsg").css("display") == "block";
                if (!flag) {
                    if (data.Url != null && data.Url != "") {
                        var msg = msgList[data.ErrorCode];
                        if (msg == null || msg == "" || msg == undefined) {
                            msg = data.ErrorCode;
                        }

                        dialogHelper.Error({
                            content: msg,
                            success: function () {
                                location.href = data.Url;
                            },
                            cancle: function () {
                                location.href = data.Url;
                            }
                        });
                        return;
                    } else {
                        var msg = msgList[data.ErrorCode];
                        if (msg == null || msg == "" || msg == undefined) {
                            msg = data.ErrorCode;
                        }
                        dialogHelper.Error({ content: msg });
                        return;
                    }
                }
            }
            if (opt.myComplete != undefined && typeof (opt.myComplete) == "function") {
                opt.myComplete();
            }
        };

        _opt.complete = function (XMLHttpRequest, textStatus) {
            if (typeof (opt.complete) == "function") {
                opt.complete();
            }
            $(".background,.progressBar").hide();
        };

        $(".background,.progressBar").show();
        _ajax(_opt);
    };
})(jQuery);
