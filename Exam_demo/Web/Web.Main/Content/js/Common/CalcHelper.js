/**
 * @name 计算常用JS帮助类
 * @remark 2015-03-11 改为模块化写法
 */
var calcHelper = (function(){
    var _fixNum = 2;
    
	/**
	 * 设置精度
	 * @param num 保留小数位
	 * @remark 因JS自身问题，返回的数字跟保留的小数位不符，如：1.00，实际返回1
	 */
    var SetFixed = function (num){
        if(isNaN(parseInt(num))){
            _fixNum = 2;
        } else {
            _fixNum = parseInt(num);
        }
    };
    
	/**
	 * 加法运算
	 * @param num1 数字1
     * @param num2 数字2
	 * @return (num1 + num2)
	 */
    var Addition = function (num1, num2) {
        var n1, n2;
        try {
            n1 = num1.toString().split(".")[1].length //获取数1小数部位的长度
        } catch (e) { n1 = 0; }
        try {
            n2 = num2.toString().split(".")[1].length //获取数2小数部位的长度
        } catch (e) { n2 = 0; }
        //计算最大长度，得到10的N次方
        var pow = Math.pow(10, Math.max(n1, n2));
        //将两个因数升级为10的N次方形式进行相加，最后再还原为10的倒N次方，得出的就是无损数据
        var result = (Multiplication(num1, pow) + Multiplication(num2, pow)) / pow;
        return parseFloat(result.toFixed(_fixNum));
    };
    
	/**
	 * 减法运算
	 * @param num1 数字1
     * @param num2 数字2
	 * @return (num1 - num2)
	 */
    var Subtraction = function (num1, num2) {
        return this.Addition(num1, -num2);
    };
    
	/**
	 * 乘法运算
	 * @param num1 数字1
     * @param num2 数字2
	 * @return (num1 * num2)
	 */
    var Multiplication = function (num1, num2) {
        var square = 0; //次方数
        var n1 = num1.toString();
        var n2 = num2.toString();
        try {
            square += n1.split(".")[1].length;
        } catch (ex) { }
        try {
            square += n2.split(".")[1].length;
        } catch (ex) { }
        var result = Number(n1.replace(".", "")) * Number(n2.replace(".", "")) / Math.pow(10, square);
        return parseFloat(result.toFixed(_fixNum));
    };
    
	/**
	 * 除法运算
	 * @param num1 数字1
     * @param num2 数字2
	 * @return (num1 / num2)
	 */
     var Division = function (num1, num2) {
         return this.Multiplication(num1, 1 / num2);
     };
    
    return {
        SetFixed : SetFixed,
        Addition : Addition,
        Subtraction : Subtraction,
        Multiplication : Multiplication,
        Division : Division
    };
})();

