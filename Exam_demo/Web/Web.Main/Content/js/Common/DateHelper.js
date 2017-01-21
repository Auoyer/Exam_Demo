/**
 * @name 日期常用JS帮助类
 * @remark 2015-03-11 改为模块化写法
 */
var dateHelper = (function(){
	/**
	 * 判断是否闰年
	 * @param strDate，年份
	 * @return 是否闰年
	 */
    var checkLeapYear = function (strDate) {
        var year = parseInt(strDate);
        if(!isNaN(year) && ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0)){
            return true;
        }
        return false;
    };
    
	/**
	 * 返回两个日期间的差(sDate1-sDate2)
	 * @param sDate1 日期1
     * @param sDate2 日期2
	 * @return (sDate1 - sDate2)
	 */
    var dateDiff = function (sDate1, sDate2) {
        if (sDate1.length > 10) {
            sDate1 = sDate1.substring(0, 10);
        }
        if (sDate2.length > 10) {
            sDate2 = sDate2.substring(0, 10);
        }
        var aDate, oDate1, oDate2, iDays;
        aDate = sDate1.split("/");
        oDate1 = new Date(aDate[0], aDate[1] - 1, aDate[2]); //调用Date的构造函数 
        aDate = sDate2.split("/");
        oDate2 = new Date(aDate[0], aDate[1] - 1, aDate[2]);
        iDays = parseInt((oDate1 - oDate2) / 1000 / 60 / 60 / 24); //把相差的毫秒数转换为天数
        return iDays;
    };
    
	/**
	 * 返回两个日期间的天数间隔(sDate1-sDate2)绝对值
	 * @param sDate1 日期1
     * @param sDate2 日期2
	 * @return (sDate1 - sDate2)绝对值
	 */
    var dateDiffAbs = function (sDate1, sDate2) {
        if (sDate1.length > 10) {
            sDate1 = sDate1.substring(0, 10);
        }
        if (sDate2.length > 10) {
            sDate2 = sDate2.substring(0, 10);
        }
        var aDate, oDate1, oDate2, iDays;
        aDate = sDate1.split("-");
        oDate1 = new Date(aDate[0], aDate[1] - 1, aDate[2]); //调用Date的构造函数 
        aDate = sDate2.split("-");
        oDate2 = new Date(aDate[0], aDate[1] - 1, aDate[2]);
        iDays = parseInt(Math.abs(oDate1 - oDate2) / 1000 / 60 / 60 / 24); //把相差的毫秒数转换为天数
        return iDays;
    };
    
    return {
        checkLeapYear : checkLeapYear,
        dateDiff : dateDiff,
        dateDiffAbs : dateDiffAbs
    }
})();

