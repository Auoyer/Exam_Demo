/**
 * @name 下拉菜单帮助类
 */
; var selectHelper = (function ($, window, document, undefined) {
    var defaults = {
        url: "",                        //获取菜单的地址
        data: { rId: Math.random() },   //需要的参数
        Id: "",                         //页面绑定元素的Id
        flag: true,                     //是否显示默认值
        key: 0,                         //默认值的Key值
        value: "",                      //默认值的Value值
        changeFun: function (value) { } //菜单切换时的事件
    }

    /**
	 * (私有方法)下拉菜单绑定
	 */
    var bind = function (opt, data) {
        $(opt.Id).html("");
        if (opt.flag) {
            $(opt.Id).append($("<option>").val(opt.key).text(opt.value));
        }
        $(data).each(function (index, dom) {
            $(opt.Id).append($("<option>").val(dom.Key).text(dom.Value));
        });
        if (typeof (opt.changeFun) == "function") {
            $(opt.Id).unbind("change").change(function () {
                var value = $(opt.Id).val();
                opt.changeFun(value);
            });
        }
    };

    /**
	 * 获取下拉菜单
	 */
    var GetSelect = function (options) {
        var opt = {};
        opt = $.extend({}, defaults, options || {});

        $.ajax({
            url: opt.url,
            type: "POST",
            async: false,
            dataType: "json",
            data: opt.data,
            success: function (data) {
                bind(opt, data);
            }
        });
    }


    return {
        GetSelect: GetSelect
    };
})(jQuery, window, document);