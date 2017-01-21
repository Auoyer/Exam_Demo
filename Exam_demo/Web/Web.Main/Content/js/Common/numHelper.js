//将数字字符串转为数组
function FormatNum(data, l) {
    var s = pad(data, l);
    var re = new RegExp(".{1}", "g")
    var a = []
    while ((n = re.exec(s)) != null) {
        a[a.length] = n[0];
    }
    return a;
}

//给数字字符串左侧补0
function pad(num, n) {
    var len = num.toString().length;
    while (len < n) {
        num = "0" + num;
        len++;
    }
    return num;
}