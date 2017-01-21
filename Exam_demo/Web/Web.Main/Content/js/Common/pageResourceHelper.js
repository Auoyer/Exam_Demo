//1.  用自调用匿名函数包裹代码,可以避免跟其他人的代码变量冲突
//2.  将系统变量以变量形式传递到插件内部,window等系统变量在插件内部就有了一个局部的引用，可以提高访问速度，会有些许性能的提升
//2.1 为了得到没有被修改的undefined，我们并没有传递这个参数，但却在接收时接收了它，因为实际并没有传，所以‘undefined’那个位置接收到的就是真实的'undefined'
//3.  在代码开头加一个分号，这在任何时候都是一个好的习惯
; var pageResourceHelper = (function ($, window, document, undefined) {
    //默认参数
    var defaults = {
        /*****   查询用参数   *****/
        pageIndex: 1,       //第几页
        pageSize: 10,       //每页显示数量
        type: "GET",        //请求方式
        pageDiv: "",        //显示分页控件的容器ID
        /*****   ajax参数   *****/
        url: "",
        data: {},
        async: true,
        dataType: "json",
        bind: function (data) { },
        completed: function () { }
    };
    //其他全局参数
    var pageCount = 1;
    var opt = new Object();

    //var setOptions = function (data) {
    //    opt = $.extend(opt, data || {});
    //};

    var Init = function (options) {
        //将默认参数与自定义参数合并
        opt[options.pageDiv] = {};
        opt[options.pageDiv] = $.extend({}, defaults, options || {});
        //调用goPage方法
        this.goPage(options.pageDiv, options.data.PageIndex);
    };

    var goPage = function (pageDiv, pageIndex) {
        //不存在时，设为1
        if (!pageIndex)
            pageIndex = 1;

        //设置参数
        opt[pageDiv].pageIndex = pageIndex;
        opt[pageDiv].data.PageIndex = pageIndex;
        //opt[pageDiv].pageCount = pageIndex;
        //opt.skip = (opt.pageIndex - 1) * opt.pageSize;
        //opt.top = opt.pageSize;
        var dataObj = {};
        dataObj["PageIndex"] = opt[pageDiv].pageIndex;
        dataObj["PageSize"] = opt[pageDiv].pageSize;
        dataObj["rId"] = Math.random();
        var ajaxData = $.extend({}, dataObj, opt[pageDiv].data || {});
        //ajax访问后台
        $.ajax({
            url: opt[pageDiv].url,
            type: opt[pageDiv].type,
            async: opt[pageDiv].async,
            dataType: opt[pageDiv].dataType,
            data: ajaxData,
            //成功，绑定数据
            success: function (data) {
                if (data != null && data != "") {
                    if (typeof (opt[pageDiv].bind) == "function") {
                        opt[pageDiv].bind(data);
                    }

                    //分页拼接
                    var page = $(opt[pageDiv].pageDiv);
                    page.html("");
                    var count = 0;              //统计用
                    var show_page_num = 5;      //中间显示5格页码
                    pageCount = data.TotalPages;
                    if (pageCount == 1)
                    { $('<a href="javascript:void(0);" class="cur">1</a>').appendTo(page); }
                    else {
                        for (var i = 0; i < 5; i++) {
                            count++;
                            if (count <= pageCount) {
                                if (data.PageIndex == count) {
                                    $('<a href="javascript:void(0);" class="cur">' + count + '</a>').appendTo(page);
                                } else {
                                    $('<a href="javascript:void(0);">' + count + '</a>').bind('click', { pageIndex: count }, function (event) {
                                        pageResourceHelper.goPage(pageDiv, event.data.pageIndex);
                                        return;
                                    }).appendTo(page);
                                }
                            }
                        }
                    }

                        
                    
                   
                
                   
                }
            },
            //失败，提示
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("textStatus:" + textStatus + ",errorThrown:" + errorThrown);
            },
            //完成，调用自定义方法
            complete: function () {
                opt[pageDiv].completed()
           
            }
        });
    };

    return {
     //   setOptions: setOptions,
        Init: Init,
        goPage: goPage
    };
})(jQuery, window, document);