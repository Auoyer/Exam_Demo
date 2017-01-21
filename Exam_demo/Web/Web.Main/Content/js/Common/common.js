$(document).ready(function () {
    //登录-输入框 ==========================
    $(".login-from .input input").focus(function () {
        $(this).parent().addClass("hover");
    });
    $(".login-from .input input").blur(function () {
        $(this).parent().removeClass("hover");
    });
    //表单验证信息的消失，下拉框的消失语句加在select.js里了 ==========================
    $(".tips-text").each(function (i) {
        $(this).css("z-index", 50 - i);
    });
    $(".tips-text, .pst-rela input, .pst-rela textarea").live("click", function () {
        $(this).parents(".pst-rela").find(".tips-text").hide();
    });
    //顶部帐户信息 ==========================
    $(".d_top").mouseenter(function () {
        $(".personal").show();
    });
    $(".d_top").mouseleave(function () {
        $(".personal").hide();
    });
    //表格 table ==========================
    $(".tableline tr").each(function () {
        $(this).find("th:first").css("border-left", "0");
        $(this).find("td:first").css("border-left", "0");
        $(this).find("th:last").css("border-right", "0");
        $(this).find("td:last").css("border-right", "0");
    });
    //理财实训-潜在客户-新增客户 ==========================
    $(".sib-list").each(function () {
        $(".add-sib").each(function () {
            $(this).click(function () {
                $(this).prev(".sib-item").clone(true).insertAfter($(this).prev());
            });
        });
        $(".close").click(function () {
            $(this).parent(".sib-item").remove();
        });
    });
    $(".js_addItem").each(function () {
        $(this).click(function () {
            $(this).prev(".js_itembox").clone(true).insertAfter($(this).prev());
        });
    });
    $(".js_remove").each(function () {
        $(this).click(function () {
            $(this).parents(".js_itembox").remove();
        })
    });
    //表单
    $(".item .fif-form").each(function () {
        $(this).find(".fif-box.w100:last").css("margin-bottom", "0");
    });
    $(".selected span i").each(function () {
        $(this).click(function () {
            $(this).parent("span").remove();
        });
    });
    //子系统 
    $(".subsystem .item .fif-form").css({ "margin-left": $(".subsystem .item .item-left").width(), "float": "none" });
    //题型切换
    $(".question-types span").click(function () {
        $(this).addClass("active").siblings().removeClass("active");
        var indext = $(".question-types span").index(this);
        $(".selected-topic").eq(indext).show().siblings().hide();
    });

    //左右点击按钮滚动
    $(".caption-list-title").each(function () {
        console.log(1);
        var Wid = 51,  //单个的宽度
			n = 1,     //一次翻动的个数 
			$Ul = $(this).children(".caption-switch").children(".caption-switch-con"),
			$Pre = $(this).find(".prev"),
			$Nex = $(this).find(".next"),
			Len = $Ul.children("span").length,
			Left = parseInt($Ul.css("left"));
        $Pre.click(function () {
            Left = parseInt(Left) + n * Wid;
            if (Left > 0) {
                Left = 0;
            }
            $Ul.stop().animate({ left: Left });
        });
        $Nex.click(function () {
            Left -= n * Wid;//n为一次滚动的个数
            if (Left < -Wid * (Len - 4)) {//4为显示的个数
                Left = -Wid * (Len - 4);
            };
            $Ul.stop().animate({ left: Left });
        });
        $(".caption-switch-con span").click(function () {
            $(this).addClass("on").siblings().removeClass("on");
        });
    });
    //左右点击按钮滚动-题型
    $(".question-types").each(function () {
        console.log(1);
        var Wid = 73,  //单个的宽度
			n = 1,     //一次翻动的个数 
			$Ul = $(this).children(".caption-switch").children(".question-types-box"),
			$Pre = $(this).find(".prev"),
			$Nex = $(this).find(".next"),
			Len = $Ul.children("span").length,
			Left = parseInt($Ul.css("left"));
        $Pre.click(function () {
            Left = parseInt(Left) + n * Wid;
            if (Left > 0) {
                Left = 0;
            }
            $Ul.stop().animate({ left: Left });
        });
        $Nex.click(function () {
            Left -= n * Wid;//n为一次滚动的个数
            if (Left < -Wid * (Len - 10)) {//4为显示的个数
                Left = -Wid * (Len - 10);
            };
            $Ul.stop().animate({ left: Left });
        });
        $(".question-types-box span").click(function () {
            //tab切换
            var idx = $(".question-types-box span").index(this);
            console.log(idx);
            $(this).addClass("active").siblings().removeClass("active");
            $(".serial-number .number").hide().eq(idx).show();

        });
    });

    //弹窗 ==============================
    $(".popup").each(function () {
        var height = $(this).height();
        $(this).css("margin-top", -height / 2);
    });
    $(".JS-addInfo").click(function () {
        $("#popAddInfo").show();
    });
    $(".JS-registrations").click(function () {
        $("#popRegistrations").show();
    });
    $(".JS-fundType").click(function () {
        $("#popFundType").show();
    });
    $(".JS-selectCase").click(function () {
        $("#popSelectCase").show();
    });
    $(".JS-assessmentSettings").click(function () {
        $("#popAssessmentSettings").show();
    });
    $(".JS-trainingClass").click(function () {
        $("#popTrainingClass").show();
    });
    $(".JS-chapterManage").click(function () {
        $("#popChapterManage").show();
    });
    $(".JS-viewMark").click(function () {
        $("#popViewMark").show();
    });
    $(".JS-viewChart").click(function () {
        $("#popViewChart").show();
    });
    $(".JS-viewBer").click(function () {
        $("#popViewBer").show();
    });
    $(".JS-scoreReport").click(function () {
        $("#popScoreReport").show();
    });
    $(".JS-testSet").click(function () {
        $("#popTestSet").show();
    });
    $(".JS-personalCenter").click(function () {
        $("#popPersonalCenter").show();
    });
    $(".JS-changePassword").click(function () {
        $("#popChangePassword").show();
    });
    $(".JS-questionBankList").click(function () {
        $("#popQuestionBankList").show();
    });
    $(".JS-questionBankSelect").click(function () {
        $("#popQuestionBankSelect").show();
    });
    $(".JS-questionBankAdd").click(function () {
        $("#popQuestionBankAdd").show();
    });

    $(".JS-questionBankAdd3").click(function () {
        $("#popQuestionBankAdd3").show();
    });
    $(".JS-delete").click(function () {
        $("#popDelete").show();
    });
    $(".JS-confirm").click(function () {
        $("#popConfirm").show();
    });
    $(".JS-enquire").click(function () {
        $("#popEnquire").show();
    });
    $(".popup .close,.pop-button .btn-close").click(function () {
        $(this).parents(".popup").hide();
        var len = $(".windowBg:visible").length;
        $(".windowBg").eq(len - 1).remove();
    });
    $(".JS-popBtn").click(function () {
        $("body").append('<div class="windowBg"></div>');
        for (var i = 0; i < $(".popup:visible").length; i++) {
            $(".popup:visible")[i].style.zIndex = 222 + i * 10;
            $(".windowBg")[i].style.zIndex = 221 + i * 9;
        };
    });
});

$(document).ready(function () {
    //浮层
    $(".JS-fixCase").click(function () {
        $(".fix-case").toggle();
        $(".fix-lore").hide();
        $(".fix-small").css({ "margin-top": "0", "margin-left": "-505px" });
    });
    $(".JS-fixLore").click(function () {
        $(".fix-lore").toggle();
        $(".fix-case").hide();
        $(".fix-small").css({ "margin-top": "0", "margin-left": "-505px" });
    });
    $(".fix-item-box .close").click(function () {
        $(this).parent().hide();
        $(".windowBg").remove();
    });
    var left, top, $this;
    $(document).delegate('.fix-small .fix-item-box ul', 'mousedown', function (e) {
        left = e.clientX, top = e.clientY, $this = $(this).css('cursor', 'move');
        this.setCapture ? (
        this.setCapture(),
        this.onmousemove = function (ev) { mouseMove(ev || event); },
        this.onmouseup = mouseUp
        ) : $(document).bind("mousemove", mouseMove).bind("mouseup", mouseUp);
    });
    function mouseMove(e) {
        var target = $this.parents('.fix-small');
        var l = Math.max((e.clientX - left + Number(target.css('margin-left').replace(/px$/, '')) || 0), -target.position().left);
        var t = Math.max((e.clientY - top + Number(target.css('margin-top').replace(/px$/, '')) || 0), -target.position().top);
        l = Math.min(l, $(window).width() - target.width() - target.position().left);
        t = Math.min(t, $(window).height() - target.height() - target.position().top);
        left = e.clientX;
        top = e.clientY;
        target.css({ 'margin-left': l, 'margin-top': t });
    }
    function mouseUp(e) {
        var el = $this.css('cursor', 'default').get(0);
        el.releaseCapture ?
        (
                el.releaseCapture(),
                el.onmousemove = el.onmouseup = null
        ) : $(document).unbind("mousemove", mouseMove).unbind("mouseup", mouseUp);
    }

    //董巍新增 ==============================
    //tab切换
    //$(".d_yonghu2 .d_yonghu3").hide();
    //$(".d_yonghu2").find(".d_yonghu3").eq(0).show();
    //$(".d_yonghu1 a").click(function () {
    //    $(".d_yonghu1 a").removeClass("hover");
    //    $(this).addClass("hover");
    //    var n = $(this).index();
    //    $(".d_yonghu2 .d_yonghu3").hide().eq(n).show();
    //})

    //  $(".d_yonghu2").find(".d_yonghu3").eq(0).show();
    $(".d_yonghu1 a").click(function () {
        $(".d_yonghu1 a").removeClass("hover");
        $(this).addClass("hover");
        var n = $(this).index();
        $(".d_yonghu2").find(".d_yonghu3").hide();
        $(".d_yonghu2").find(".d_yonghu3").eq(n).show();
    })
    // $(".d_yonghu1 a:first").addClass("hover");

    //tab切换
    $(".d_yi_1 a").click(function () {
        $(".d_yi_1 a").removeClass("hover");
        $(this).addClass("hover");
        var n = $(this).index();
        $(".d_yi_2").find(".d_yi_3").eq(n).show().siblings(".d_yi_3").hide();
    })
    $(".d_yi_1 a:first").addClass("hover");
    $(".d_yi_2").find(".d_yi_3").eq(0).show();
    //tab切换
    $(".d_fei7 a").click(function () {
        $(".d_fei7 a").removeClass("hover");
        $(this).addClass("hover");
        var n = $(this).index();
        $(".d_fei8").find(".d_fei8_1").eq(n).show().siblings(".d_fei8_1").hide();
    })
    $(".d_fei7 a:first").addClass("hover");
    $(".d_fei8").find(".d_fei8_1").eq(0).show();

    //tab切换
    $(".d_fei7s a").click(function () {
        $(".d_fei7s a").removeClass("hover");
        $(this).addClass("hover");
        var n = $(this).index();
        $(".d_fei8s").find(".d_fei8_1s").eq(n).show().siblings(".d_fei8_1s").hide();
    })
    $(".d_fei7s a:first").addClass("hover");
    $(".d_fei8s").find(".d_fei8_1s").eq(0).show();

    //tab切换
    //$(".i_rontyus1 a").click(function () {
    //    $(".i_rontyus1 a").removeClass("hover");
    //    $(this).addClass("hover");
    //    var n = $(this).index();
    //    $(".i_rontyus2").find(".i_rontyus3").eq(n).show().siblings(".i_rontyus3").hide();
    //})
    //$(".i_rontyus1 a:first").addClass("hover");
    //$(".i_rontyus2").find(".i_rontyus3").eq(0).show();
    //tab切换
    $(".d_ren3_1 dl dt").click(function () {
        $(".d_ren3_1 dl dd").hide();
        $(this).parent("dl").children("dd").show();
    })
    $(".d_ren3_1 dl:first dd").show();

    //tab切换
    //tab切换
    $(".masp area").click(function () {
        var n = $(this).index();
        $(".mas").find("div").eq(n).show().siblings("div").hide();
    })
    $(".mas").find("div").eq(0).show();

    //弹框
    $(".t_tankuang").each(function () {
        var height = $(this).height();
        $(this).css("margin-top", -height / 2);
    });
    $(".d_chakan").click(function () {
        $(".dd_chakans,.k_tanmu").show();
    });
    $(".fabu").click(function () {
        $(".dd_fabu,.k_tanmu").show();
    });
    $(".d_xinzeng").click(function () {
        $(".dd_xinzeng,.k_tanmu").show();
    });
    $(".d_shanchu").click(function () {
        $(".dd_shanchu,.k_tanmu").show();
    });
    $(".d_chuantu").click(function () {
        $(".dd_tianjiatu,.k_tanmu").show();
    });
    $(".d_gaimi").click(function () {
        $(".dd_gaimi,.k_tanmu").show();
    });
    $(".d_geren").click(function () {
        $(".dd_geren,.k_tanmu").show();
    });
    $(".d_queren").click(function () {
        $(".dd_shanchus").show();
        $(".dd_shanchu").hide();
    });
    $(".d_bianjis").click(function () {
        $(".dd_geren_bianji").show();
        $(".dd_geren").hide();
    });
    $(".d_xiugai").click(function () {
        $(".dd_xinzeng").show();
        $(".dd_chakans").hide();
    });
    $(".d_shiti").click(function () {
        $(".dd_tiku,.k_tanmu").show();
    });
    $(".d_close,.d_quxiao").click(function () {
        $(this).parents(".t_tankuang").hide();
        $(".k_tanmu").hide();
    });

});


//$(document).ready(function (e) {
//    var a = ['1', '5', '6', '2', '5', '8'];    
//    for (var i = 0; i < a.length; i++) {
//        var cur = a[i];
//        $('.d_wrap span').eq(i).animate({ top: -24 * cur }, 1000);
//    }
//});
//$(document).ready(function (e) {
//    var a = ['2', '2', '2', '2', '2', '2'];
//    for (var i = 0; i < a.length; i++) {
//        var cur = a[i];
//        $('.d_wrap1 span').eq(i).animate({ top: -24 * cur }, 1000);
//    }
//});

// 复选框全选  
function GetAllCheckBox(t) {
    var test = $('tbody').find('input:checkbox');
    for (var i = 0; i < test.length; i += 1) {
        test[i].checked = t.checked;
    }
}


// 过滤非法字符
$(function () {
    //$('input:text').blur(function () {
    //    if ($(this).hasClass('Wdate') || $(this).hasClass('IsPhone'))
    //    { } else {
    //        var pattern = new RegExp("[-`~!#$%^&*()=+|{}';',\\[\\]<>? ~！￥……&*（）;—|{}【】‘；：\"”“'。，、？]")
    //        var rs = "";
    //        var s = $(this).val();
    //        for (var i = 0; i < s.length; i++) {
    //            rs = rs + s.substr(i, 1).replace(pattern, '');
    //        }
    //        $(this).val(rs);
    //    }
    //})
    $('input:password').keyup(function () {
        var pattern = new RegExp("[-`~!#$%^&*()/=+|{}':;.',\\[\\]<>/?~！￥……&*（）;—|{}【】‘；：\"”“'。，、？]")
        var rs = "";
        var s = $(this).val();
        for (var i = 0; i < s.length; i++) {
            rs = rs + s.substr(i, 1).replace(pattern, '');
        }
        $(this).val(rs);
    })
})