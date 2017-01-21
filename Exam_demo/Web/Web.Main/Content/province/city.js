function getXmlDoc() {
    var xmldoc;
    try {
        //IE浏览器
        xmlDoc = new ActiveXObject("microsoft.XMLDOM");
    } catch (e) {
        try {
            //firefox 其他浏览器
            xmlDoc = document.implementation.createDocument("", "", null);
        } catch (er) {
            messageBox("您的浏览器版本太低了", "jg")
        }
    }
    //关闭异步加载,确保在文档完全加载之前解析器不会继续脚本的执行
    xmlDoc.async = false;
    //转载xml文件
    xmlDoc.load("/Content/province/city.xml");
    return xmlDoc;
}

var addressInit = function (prov, city, area) {
    _ajax_backup({
        url: '/Content/province/city.xml',
        async: false,
        data: { random: Math.random() },
        success: function (data) {
            $('#province option').remove();
            $('#province').change(function () { provinceChaneg(data) });
            $('#city').append("<option value='--请选择--'>--请选择--</option>");
            $(data).find("province").each(function (i) {
                var pName = $(this).attr("name");               // 省名称
                var pPostcode = $(this).attr("postcode");               // 市名称
                if (prov == pPostcode) {
                    $('#province').append("<option value='" + pName + "' postcode='" + pPostcode + "' selected='selected'>" + pName + "</option>");
                    $('#province').change();
                }
                else
                    $('#province').append("<option value='" + pName + "' postcode='" + pPostcode + "'>" + pName + "</option>");
            });

        }
    })

    // 省联动
    function provinceChaneg(data) {
        $('#city option').remove();
        $('#city').append("<option value='--请选择--'>--请选择--</option>");
        $(data).find("province").each(function (i) {
            var pName = $(this).attr("name");               // 省名称
            var pPostcode = $(this).attr("postcode");               // 市编号

            if ($('#province').val() == pName) {
                $(this).find('city').each(function () {
                    var cName = $(this).attr("name");               // 市名称
                    var cPostcode = $(this).attr("postcode");               // 市编号
                    if (city == cPostcode) {
                        $('#city').append("<option value='" + cName + "' postcode='" + cPostcode + "' selected='selected'>" + cName + "</option>");
                    }
                    else
                        $('#city').append("<option value='" + cName + "' postcode='" + cPostcode + "'>" + cName + "</option>");
                })
            }

        });


    }

}

var addressInit2 = function (prov, city, area) {
    _ajax_backup({
        url: '/Content/province/city.xml',
        async: false,
        data: { random: Math.random() },
        success: function (data) {
            $('.province').change(function () { provinceChaneg2(data) });
            $('.city').append("<option value='--请选择--'>--请选择--</option>");
            $(data).find("province").each(function (i) {
                var pName = $(this).attr("name");               // 省名称
                var pPostcode = $(this).attr("postcode");               // 市名称
                if (prov == pPostcode) {
                    $('.province').append("<option value='" + pName + "' postcode='" + pPostcode + "' selected='selected'>" + pName + "</option>");
                    $('.province').change();
                }
                else
                    $('.province').append("<option value='" + pName + "' postcode='" + pPostcode + "'>" + pName + "</option>");
            });

        }
    })

    // 省联动
    function provinceChaneg2(data) {
        $('.city option').remove();
        $('.city').append("<option value='--请选择--'>--请选择--</option>");
        $(data).find("province").each(function (i) {
            var pName = $(this).attr("name");               // 省名称
            var pPostcode = $(this).attr("postcode");               // 市编号

            if ($('.province').val() == pName) {
                $(this).find('city').each(function () {
                    var cName = $(this).attr("name");               // 市名称
                    var cPostcode = $(this).attr("postcode");               // 市编号
                    if (city == cPostcode) {
                        $('.city').append("<option value='" + cName + "' postcode='" + cPostcode + "' selected='selected'>" + cName + "</option>");
                    }
                    else
                        $('.city').append("<option value='" + cName + "' postcode='" + cPostcode + "'>" + cName + "</option>");
                })
            }

        });


    }

}


// 根据编号获取地区名称，优化
var getCodeName = function (provCode, cityCode) {
    if (provCode == 0)
        return "";
    var setName = "";
    _ajax_backup({
        url: '/Content/province/city.xml',
        async: false,
        dataType: 'xml',
        data: { random: Math.random() },
        success: function (data) {
            $(data).find("province").each(function (i) {
                var _prov = $(this).attr("postcode");         // 获取省编号
                if (_prov == provCode) {
                    setName = $(this).attr('name');
                    // 遍历下一级
                    $(this).find("city").each(function (item1) {
                        var _city = $(this).attr("postcode");         // 获取省编号
                        if (_city == cityCode) {
                            return setName += ' - ' + $(this).attr('name');
                        }
                    });
                }

            });
        }
    });
    return setName;
}



//var addressInit = function (prov, city, area) {
//    _ajax_backup({
//        url: '/Content/province/city.xml',
//        async: false,
//        success: function (data) {
//            //通过方法获取对象
//            var xmlDoc = data;
//            //获取xml文件的根节点
//            var root = xmlDoc.documentElement;
//            //获得所有的省节点
//            var provinces = root.childNodes;
//            //获取页面中要显示的省的控件dom对象
//            var sheng = document.getElementById("province");
//            var shi = document.getElementById("city");
//            //var xian = document.getElementById("county");

//            // 清空节点
//            $(sheng).empty();
//            $(shi).empty();

//            var shioptt = document.createElement("option");
//            shioptt.appendChild(document.createTextNode("--请选择--"));
//            shioptt.setAttribute("value", "");
//            shi.appendChild(shioptt);
//            var xianoptt = document.createElement("option");
//            xianoptt.appendChild(document.createTextNode("--请选择--"));
//            xianoptt.setAttribute("value", "");
//            //xian.appendChild(xianoptt);

//            $('#province').change(shengChange);

//            //遍历所有的省
//            for (var i = 0; i < provinces.length; i++) {
//                //查看该节点是否是元素节点 也是为了实现不同浏览器之间的兼容性问题
//                if (provinces[i].nodeType == 1) {
//                    //创建option节点对象
//                    var shengopt = document.createElement("option");
//                    //为省节点添加文本节点
//                    var _prov = document.createTextNode(provinces[i].getAttribute("name"));
//                    shengopt.appendChild(_prov);
//                    //为省节点添加属性
//                    shengopt.setAttribute("value", provinces[i].getAttribute("name"));
//                    shengopt.setAttribute("postcode", provinces[i].getAttribute("postcode"));
//                    //添加省道页面dom对象中
//                    sheng.appendChild(shengopt);
//                    if (provinces[i].getAttribute("postcode") == prov) {
//                        shengopt.setAttribute("selected", "selected");
//                        $('#province').change();
//                    }
//                }
//            }



//            //当省节点发生改变时 触发事件
//            //    sheng.onchange = function () {
//            function shengChange() {
//                //获取省节点所有的option对象的集合
//                var shengs = sheng.options;
//                //获取选中option对象的selectedIndex(下标值)
//                var num = shengs.selectedIndex;
//                //清空市 区    
//                shi.length = 1;
//                //  xian.length = 1;
//                var ppostocode = shengs[num].getAttribute("postcode");
//                //遍历所有的省
//                for (var i = 0; i < provinces.length; i++) {
//                    //查看该节点是否是元素节点 也是为了实现不同浏览器之间的兼容性问题
//                    if (provinces[i].nodeType == 1) {
//                        var postcode = provinces[i].getAttribute("postcode");
//                        if (postcode == ppostocode) {
//                            var cities = provinces[i].childNodes;
//                            shi.length = 1;
//                            for (var i = 0; i < cities.length; i++) {
//                                if (cities[i].nodeType == 1) {
//                                    var shiopt = document.createElement("option");
//                                    var _city = document.createTextNode(cities[i].getAttribute("name"));
//                                    shiopt.appendChild(_city);
//                                    shiopt.setAttribute("value", cities[i].getAttribute("name"));
//                                    shiopt.setAttribute("postcode", cities[i].getAttribute("postcode"));
//                                    shi.appendChild(shiopt);
//                                    if (cities[i].getAttribute("postcode") == city) {
//                                        shiopt.setAttribute("selected", "selected");
//                                        //$('#city').change();
//                                    }

//                                }
//                            }
//                            break;
//                        }
//                    }
//                }
//            };
//            function shiChange() {
//                var shis = shi.options;
//                var num = shis.selectedIndex;
//                var spostcode = shis[num].getAttribute("postcode");
//                for (var i = 0; i < provinces.length; i++) {
//                    if (provinces[i].nodeType == 1) {
//                        var cities = provinces[i].childNodes;
//                        for (var j = 0; j < cities.length; j++) {
//                            if (cities[j].nodeType == 1) {
//                                var postcode = cities[j].getAttribute("postcode");
//                                if (postcode == spostcode) {
//                                    xian.length = 1;
//                                    var areas = cities[j].childNodes;
//                                    for (var k = 0; k < areas.length; k++) {
//                                        if (areas[k].nodeType == 1) {
//                                            var xianopt = document.createElement("option");
//                                            var _area = document.createTextNode(areas[k].getAttribute("name"));
//                                            xianopt.appendChild(_area);
//                                            xianopt.setAttribute("value", areas[k].getAttribute("name"));
//                                            xianopt.setAttribute("postcode", areas[k].getAttribute("postcode"));
//                                            xian.appendChild(xianopt);

//                                            if (_area.nodeValue == area) {
//                                                xianopt.setAttribute("selected", "selected");
//                                            }
//                                        }
//                                    }
//                                    break;
//                                }
//                            }
//                        }
//                    }
//                }
//            };
//        }
//    })




//}



//var addressInit2 = function (prov, city, area) {
//    _ajax_backup({
//        url: '/Content/province/city.xml',
//        async: false,
//        success: function (data) {
//            //通过方法获取对象
//            var xmlDoc = data;
//            //获取xml文件的根节点
//            var root = xmlDoc.documentElement;
//            //获得所有的省节点
//            var provinces = root.childNodes;
//            //获取页面中要显示的省的控件dom对象
//            var sheng = $(".province");
//            var shi = $(".city");
//            //var xian = document.getElementById("county");getElementsByClassName

//            // 清空节点
//            $(sheng).empty();
//            $(shi).empty();

//            var shioptt = document.createElement("option");
//            shioptt.appendChild(document.createTextNode("--请选择--"));
//            shioptt.setAttribute("value", "");
//            shi.append(shioptt);
//            var xianoptt = document.createElement("option");
//            xianoptt.appendChild(document.createTextNode("--请选择--"));
//            xianoptt.setAttribute("value", "");
//            //xian.appendChild(xianoptt);

//            $('.province').change(shengChange2);

//            //遍历所有的省
//            for (var i = 0; i < provinces.length; i++) {
//                //查看该节点是否是元素节点 也是为了实现不同浏览器之间的兼容性问题
//                if (provinces[i].nodeType == 1) {
//                    //创建option节点对象
//                    var shengopt = document.createElement("option");
//                    //为省节点添加文本节点
//                    var _prov = document.createTextNode(provinces[i].getAttribute("name"));
//                    shengopt.appendChild(_prov);
//                    //为省节点添加属性
//                    shengopt.setAttribute("value", provinces[i].getAttribute("name"));
//                    shengopt.setAttribute("postcode", provinces[i].getAttribute("postcode"));
//                    //添加省道页面dom对象中
//                    sheng.append(shengopt);
//                    if (provinces[i].getAttribute("postcode") == prov) {
//                        shengopt.setAttribute("selected", "selected");
//                        $('.province').change();
//                    }
//                }
//            }

//            //当省节点发生改变时 触发事件
//            //    sheng.onchange = function () {
//            function shengChange2() {
//                $(shi).empty();

//                var shioptt = document.createElement("option");
//                shioptt.appendChild(document.createTextNode("--请选择--"));
//                shioptt.setAttribute("value", "");
//                shi.append(shioptt);

//                //获取省节点所有的option对象的集合
//                var shengs = sheng.find('option');
//                //debugger;
//                //获取选中option对象的selectedIndex(下标值)
//                var num = $(sheng).prop('selectedIndex');
//                //清空市 区    
//                //shi.length = 1;
//                //  xian.length = 1;
//                var ppostocode = shengs[num].getAttribute("postcode");
//                //遍历所有的省
//                for (var i = 0; i < provinces.length; i++) {
//                    //查看该节点是否是元素节点 也是为了实现不同浏览器之间的兼容性问题
//                    if (provinces[i].nodeType == 1) {
//                        var postcode = provinces[i].getAttribute("postcode");
//                        if (postcode == ppostocode) {
//                            var cities = provinces[i].childNodes;
//                            //    shi.length = 1;
//                            //debugger;
//                            for (var i = 0; i < cities.length; i++) {
//                                if (cities[i].nodeType == 1) {
//                                    var shiopt = document.createElement("option");
//                                    var _city = document.createTextNode(cities[i].getAttribute("name"));
//                                    shiopt.appendChild(_city);
//                                    shiopt.setAttribute("value", cities[i].getAttribute("name"));
//                                    shiopt.setAttribute("postcode", cities[i].getAttribute("postcode"));
//                                    $(shi).append(shiopt);
//                                    if (cities[i].getAttribute("postcode") == city) {
//                                        shiopt.setAttribute("selected", "selected");
//                                    }

//                                }
//                            }
//                            break;
//                        }
//                    }
//                }
//            };
//            function shiChange2() {
//                var shis = shi.options;
//                var num = shis.selectedIndex;
//                var spostcode = shis[num].getAttribute("postcode");
//                for (var i = 0; i < provinces.length; i++) {
//                    if (provinces[i].nodeType == 1) {
//                        var cities = provinces[i].childNodes;
//                        for (var j = 0; j < cities.length; j++) {
//                            if (cities[j].nodeType == 1) {
//                                var postcode = cities[j].getAttribute("postcode");
//                                if (postcode == spostcode) {
//                                    xian.length = 1;
//                                    var areas = cities[j].childNodes;
//                                    for (var k = 0; k < areas.length; k++) {
//                                        if (areas[k].nodeType == 1) {
//                                            var xianopt = document.createElement("option");
//                                            var _area = document.createTextNode(areas[k].getAttribute("name"));
//                                            xianopt.appendChild(_area);
//                                            xianopt.setAttribute("value", areas[k].getAttribute("name"));
//                                            xianopt.setAttribute("postcode", areas[k].getAttribute("postcode"));
//                                            xian.appendChild(xianopt);

//                                            if (_area.nodeValue == area) {
//                                                xianopt.setAttribute("selected", "selected");
//                                            }
//                                        }
//                                    }
//                                    break;
//                                }
//                            }
//                        }
//                    }
//                }
//            };
//        }
//    })




//}
