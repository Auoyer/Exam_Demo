//1.  用自调用匿名函数包裹代码,可以避免跟其他人的代码变量冲突
//2.  将系统变量以变量形式传递到插件内部,window等系统变量在插件内部就有了一个局部的引用，可以提高访问速度，会有些许性能的提升
//2.1 为了得到没有被修改的undefined，我们并没有传递这个参数，但却在接收时接收了它，因为实际并没有传，所以‘undefined’那个位置接收到的就是真实的'undefined'
//3.  在代码开头加一个分号，这在任何时候都是一个好的习惯
; var pageHelper = (function ($, window, document, undefined) {
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
        bind: function(data){ },
        completed: function () { }
    };
    //其他全局参数
    var pageCount = 1;
    var opt = new Object();

    //var setOptions = function (data) {
    //    opt = $.extend(opt, data || {});
    //};

    var Init2 = function (options) {
        //将默认参数与自定义参数合并
        opt[options.pageDiv] = {};
        opt[options.pageDiv] = $.extend({}, defaults, options || {});
        //调用goPage方法
        goPage2(options.pageDiv, options.pageIndex);
    };

    var goPage2 = function (pageDiv, pageIndex) {
        //不存在时，设为1
        if (!pageIndex)
            pageIndex = 1;

        //设置参数
        opt[pageDiv].pageIndex = pageIndex;
        var dataObj = {};
        dataObj["PageIndex"] = opt[pageDiv].pageIndex;
        dataObj["PageSize"] = opt[pageDiv].pageSize;
        dataObj["rId"] = Math.random();
        var ajaxData = $.extend({}, dataObj, opt[pageDiv].data || {});
        //循环参数，并把html标签转义
        for (var o in ajaxData) {
            if (typeof (ajaxData[o]) == "string") {
                if (/((<[^>]+>)|(<[^>]+)|([^>]+>))/g.test(ajaxData[o])) {
                    ajaxData[o] = htmlEncode(ajaxData[o]);
                }
            }
        }

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
                    pageCount = data.TotalPages;
                    page.html("");
                    var count = 1;              //统计用
                    var show_page_num = 2;      //中间显示5格页码
                    if (data.PageIndex == 1) {
                        $('<a href="javascript:void(0);"><上一页</a>').appendTo(page);
                        $('<a href="javascript:void(0);" class="cur">1</a>').appendTo(page);
                    } else {
                        //上一页
                        $('<a href="javascript:void(0);"><上一页</a>').unbind("click").bind("click",
                        function () {
                            if (data.HasPreviousPage) {
                                goPage2(pageDiv, data.PageIndex - 1);
                            }
                            return;
                        }).appendTo(page);
                        //第一页
                        $('<a href="javascript:void(0);">1</a>').unbind("click").bind("click",
                        function () {
                            goPage2(pageDiv, 1);
                            return;
                        }).appendTo(page);
                        //$(":checkbox").attr("checked", false);
                    }
                    //省略号后，过2个页数框
                    if (count < data.PageIndex - 2 && pageCount > (show_page_num + 2)) {
                        $(page).append('...');
                        count = data.PageIndex - 2;
                    }
                    if (pageCount > (show_page_num + 1) && count > (pageCount - show_page_num - 1)) {
                        count = pageCount - show_page_num - 1;
                    }
                    for (var i = 0; i < 2; i++) {
                        count++;
                        if (count < pageCount) {
                            if (data.PageIndex == count) {
                                $('<a href="javascript:void(0);" class="cur">' + count + '</a>').appendTo(page);
                            } else {
                                $('<a href="javascript:void(0);">' + count + '</a>').bind('click', { pageIndex: count }, function (event) {
                                    goPage2(pageDiv, event.data.pageIndex);
                                    return;
                                }).appendTo(page);
                            }
                        }
                    }
                    if (count + 1 < pageCount) {
                        $(page).append('...');
                    }
                    if (pageCount > 1) {
                        if (data.PageIndex == pageCount && pageCount != 1) {
                            //最后一页
                            $('<a href="javascript:void(0);" class="cur">' + pageCount + '</a>').appendTo(page);
                        } else {
                            //最后一页
                            $('<a href="javascript:void(0);">' + pageCount + '</a>').bind('click', function () {
                                goPage2(pageDiv, data.TotalPages);
                                return;
                            }).appendTo(page);
                        }
                    }
                    if (data.PageIndex == pageCount) {
                        //下一页
                        $('<a href="javascript:void(0);">下一页></a>').appendTo(page);
                    } else {
                        //下一页
                        $('<a href="javascript:void(0);">下一页></a>').bind('click', function () {
                            if (data.HasNextPage) {
                                goPage2(pageDiv, data.PageIndex + 1);
                            }
                            return;
                        }).appendTo(page);
                        //$(":checkbox").attr("checked", false);
                    }
                    $('<input type="hidden" id="pageTotalCount" value="' + data.TotalCount + '" />').appendTo(page);
                    ////跳转
                    //page.append("到");
                    //$('<input type="text" name="txt_goPageIndex" id="txt_goPageIndex" class="input" value="' + data.PageIndex + '">').appendTo(page);
                    //page.append("页");
                    //$('<input type="button" class="GoBtn" value="GO">').bind('click', function (event) {
                    //    var pi = parseInt($("#txt_goPageIndex").val());
                    //    if (!pi || pi <= 0 || !/^\d+$/.test(pi)) {
                    //        goPage(data.PageIndex);
                    //        //$("#txt_goPageIndex").val(data.PageIndex);
                    //        return false;
                    //    }
                    //    if (pi < 1 || pi > pageCount) {
                    //        //alert('页码在' + (s + 1) + '到' + (e + 1) + '之间');
                    //        goPage(data.PageIndex);
                    //        return false;
                    //    }
                    //    goPage(pi);
                    //}).appendTo(page);
                    //$(":checkbox").attr("checked", false);
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

    var Init = function (options) {
        //将默认参数与自定义参数合并
        opt[options.pageDiv] = {};
        opt[options.pageDiv] = $.extend({}, defaults, options || {});
        //调用goPage方法
        goPage(options.pageDiv, options.pageIndex);
    };

    var goPage = function (pageDiv, pageIndex) {
        //不存在时，设为1
        if (!pageIndex)
            pageIndex = 1;

        //设置参数
        opt[pageDiv].pageIndex = pageIndex;
        var dataObj = {};
        dataObj["PageIndex"] = opt[pageDiv].pageIndex;
        dataObj["PageSize"] = opt[pageDiv].pageSize;
        dataObj["rId"] = Math.random();
        var ajaxData = $.extend({}, dataObj, opt[pageDiv].data || {});
        //循环参数，并把html标签转义
        for (var o in ajaxData) {
            if (typeof (ajaxData[o]) == "string") {
                if (/((<[^>]+>)|(<[^>]+)|([^>]+>))/g.test(ajaxData[o])) {
                    ajaxData[o] = htmlEncode(ajaxData[o]);
                }
            }
        }

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
                    pageCount = data.TotalPages;
                    page.html("");
                    var count = 1;              //统计用
                    var show_page_num = 5;      //中间显示5格页码
                    if (data.PageIndex == 1) {
                        $('<a href="javascript:void(0);"><上一页</a>').appendTo(page);
                        $('<a href="javascript:void(0);" class="cur">1</a>').appendTo(page);
                    } else {
                        //上一页
                        $('<a href="javascript:void(0);"><上一页</a>').unbind("click").bind("click",
                        function () {
                            if (data.HasPreviousPage) {
                                goPage(pageDiv, data.PageIndex - 1);
                            }
                            return;
                        }).appendTo(page);
                        //第一页
                        $('<a href="javascript:void(0);">1</a>').unbind("click").bind("click",
                        function () {
                            goPage(pageDiv, 1);
                            return;
                        }).appendTo(page);
                        //$(":checkbox").attr("checked", false);
                    }
                    //省略号后，过2个页数框
                    if (count < data.PageIndex - 3 && pageCount > (show_page_num + 2)) {
                        $(page).append('...');
                        count = data.PageIndex - 3;
                    }
                    if (pageCount > (show_page_num + 1) && count > (pageCount - show_page_num - 1)) {
                        count = pageCount - show_page_num - 1;
                    }
                    for (var i = 0; i < 5; i++) {
                        count++;
                        if (count < pageCount) {
                            if (data.PageIndex == count) {
                                $('<a href="javascript:void(0);" class="cur">' + count + '</a>').appendTo(page);
                            } else {
                                $('<a href="javascript:void(0);">' + count + '</a>').bind('click', { pageIndex: count }, function (event) {
                                    goPage(pageDiv, event.data.pageIndex);
                                    return;
                                }).appendTo(page);
                            }
                        }
                    }
                    if (count + 1 < pageCount) {
                        $(page).append('...');
                    }
                    if (pageCount > 1) {
                        if (data.PageIndex == pageCount && pageCount != 1) {
                            //最后一页
                            $('<a href="javascript:void(0);" class="cur">' + pageCount + '</a>').appendTo(page);
                        } else {
                            //最后一页
                            $('<a href="javascript:void(0);">' + pageCount + '</a>').bind('click', function () {
                                goPage(pageDiv, data.TotalPages);
                                return;
                            }).appendTo(page);
                        }
                    }
                    if (data.PageIndex == pageCount) {
                        //下一页
                        $('<a href="javascript:void(0);">下一页></a>').appendTo(page);
                    } else {
                        //下一页
                        $('<a href="javascript:void(0);">下一页></a>').bind('click', function () {
                            if (data.HasNextPage) {
                                goPage(pageDiv, data.PageIndex + 1);
                            }
                            return;
                        }).appendTo(page);
                        //$(":checkbox").attr("checked", false);
                    }
                    $('<input type="hidden" id="pageTotalCount" value="' + data.TotalCount + '" />').appendTo(page);
                    ////跳转
                    //page.append("到");
                    //$('<input type="text" name="txt_goPageIndex" id="txt_goPageIndex" class="input" value="' + data.PageIndex + '">').appendTo(page);
                    //page.append("页");
                    //$('<input type="button" class="GoBtn" value="GO">').bind('click', function (event) {
                    //    var pi = parseInt($("#txt_goPageIndex").val());
                    //    if (!pi || pi <= 0 || !/^\d+$/.test(pi)) {
                    //        goPage(data.PageIndex);
                    //        //$("#txt_goPageIndex").val(data.PageIndex);
                    //        return false;
                    //    }
                    //    if (pi < 1 || pi > pageCount) {
                    //        //alert('页码在' + (s + 1) + '到' + (e + 1) + '之间');
                    //        goPage(data.PageIndex);
                    //        return false;
                    //    }
                    //    goPage(pi);
                    //}).appendTo(page);
                    //$(":checkbox").attr("checked", false);
                }
            },
            //失败，提示
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("textStatus:" + textStatus + ",errorThrown:" + errorThrown);
            },
            //完成，调用自定义方法
            complete: function(){
                opt[pageDiv].completed()
            }
        });
    };

    return {
        //setOptions: setOptions,
        Init: Init,
        Init2: Init2,
        goPage2: goPage2,
        goPage: goPage
    };
})(jQuery, window, document);