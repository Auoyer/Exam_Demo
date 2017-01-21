var arrayHelper = function (key) {
    var list = new Array();
    //用于区分list的主键名称
    this.key = key;
    //根据复杂类型ID查找
    this.Find = function (id) {
        var index = -1;
        for (var i = 0; i < list.length; i++) {
            if (list[i][this.key] == id) {
                index = i;
            }
        }
        return index;
    };
    //根据ID查找
    this.FindInt = function (id) {
        var index = -1;
        for (var i = 0; i < list.length; i++) {
            var a = list[i];
            if (list[i] == Number(id)) {
                
                index = i;
            }
        }
        return index;
    };
    //根据ID查找文本内容
    this.FindRecord = function (id) {
        var result = null;
        for (var i = 0; i < list.length; i++) {
            if (list[i][this.key] == id) {
                result = list[i];
                break;
            }
        }
        return result;
    };
    //添加
    this.Add = function (obj) {
        var index = this.Find(obj[this.key]);
        //如果ID已存在，则覆盖
        if (index > -1) {
            list[index] = obj;
            
        } else {
            list.push(obj);
        }
        return index;
    };
    //添加
    this.AddInt = function (obj) {
        var index = this.FindInt(obj);
        //如果ID已存在，则覆盖
        if (index > -1) {
            this.Remove2(obj);
            //list[index] = obj;
        } else {
            list.push(obj);
        }
    };
    //移除
    this.Remove = function (id) {
        var index = this.Find(id);
        if (index > -1) {
            list.splice(index, 1);
        }
    };
    //移除
    this.Remove2 = function (id) {
        var index = this.FindInt(id);
        if (index > -1) {
            list.splice(index, 1);
        }
    };
    //移除所有
    this.RemoveAll = function () {
        list.splice(0, list.length);
    }
    //获取列表
    this.GetList = function () {
        return list;
    };
}

