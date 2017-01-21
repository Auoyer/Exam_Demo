var UserTimeHelper = (function ($, window, document, undefined) {
    //controller，注意全部小写
    //子系统
    var allowSystem = [
        "proposalcustomer",
        "riskevaluation",
        "liability",
        "incomeandexpenses",
        "cashflow",
        "financialratios",
        "cashplan",
        "lifeeducationplan",
        "consumptionplan",
        "startanundertakingplan",
        "retirementplan",
        "insuranceplan",
        "investmentplan",
        "taxplan",
        "distributionofproperty",
        "heritage"       
    ];
    //资源
    var allowResource = [
        "sturesource"
    ];
    //认证考试
    var allowExam = [
        "eheoryexamine"
    ];

    var timespan = 5 * 60 * 1000 - 5000; //4分钟55秒
    var time = timespan;
    var flag = true;
    var place = { x: 0, y: 0 };
    var time_Id_1 = "";
    var time_Id_2 = "";

	
	var Init = function(){
		//鼠标移动时，不进行倒计时
	    $(document).mousemove(function (e) {
	        //没有成功/错误/确认弹出框，才重新计时
	        var size1 = $("#popConfirm").size();
	        var size2 = $("#popMsg").size();
	        if (size1 < 1 && size2 < 1) {
	            if (place.x != e.pageX || place.y != e.pageY) {
	                place.x = e.pageX;
	                place.y = e.pageY;
	                time = timespan;
                    flag = true;
	            }
	        }
	    });
        //键盘输入时，不进行倒计时
	    $(window).keydown(function (event) {
	        time = timespan;
	        flag = true;
	    });
		//倒计时
		time_Id_1 = window.setInterval(countdown,1000);
		function countdown() {
		    var controller = $("#hdController").val();
		    var SummaryType = 0;
		    var index = -1;
		    //子系统
		    index = $.inArray(controller, allowSystem);
		    if (index > -1) {
		        SummaryType = 1;
		    }
		    //资源
		    index = $.inArray(controller, allowResource);
		    if (index > -1) {
		        SummaryType = 2;
		    }
		    //认证考试
		    index = $.inArray(controller, allowExam);
		    if (index > -1) {
		        SummaryType = 3;
		    }               
		    if (SummaryType != 0) {

		        if (time >= 0) {
		            time = time - 1000;
		            if (!flag) {
		                flag = true;
		            }
		        } else {
		            flag = false;
		            clearInterval(time_Id_1);
		            clearInterval(time_Id_2);
		            //弹出窗口，并停止统计时间
		            dialogHelper.Confirm({
		                content: "您已超时，是否继续操作？",
		                success: function () {
		                    flag = true;
		                    time = timespan;
		                    //重启计时
		                    time_Id_1 = window.setInterval(countdown, 1000);
		                    time_Id_2 = window.setInterval(counttime, (1 * 60 * 1000));
		                },
		                cancle: function () {
		                    location.href = "/SignIn/SignOut";
		                }
		            });

		        }

		    }
        }
		//每1分钟统计一次
		time_Id_2 = window.setInterval(counttime,(1 * 60 * 1000));
        function counttime(){
            if(flag){
                //判断是否需要统计的controller
                var controller = $("#hdController").val();
                var SummaryType = 0;
                var index = -1;
                //子系统
                index = $.inArray(controller, allowSystem);
                if (index > -1) {
                    SummaryType = 1;
                }
                //资源
                index = $.inArray(controller, allowResource);
                if (index > -1) {
                    SummaryType = 2;
                }
                //认证考试
                index = $.inArray(controller, allowExam);
                if (index > -1) {
                    SummaryType = 3;
                }               
                if (SummaryType != 0) {
                    if (_ajax_backup != null && _ajax_backup != undefined) {
                        _ajax_backup({
                            url: "/CompetitionUser/Common/AddUserTimeSummary",
                            type: "POST",
                            async: true,
                            dataType: "json",
                            data:
                            {
                                SummaryType: SummaryType,
                                usedTime: 1,
                                rId: Math.random()
                            },
                            success: function (data) {
                                //成功无需执行任何操作
                            }
                        });
                    }
                }
            }
        }
	};

	return {
	    Init: Init,
	};
})(jQuery, window, document);