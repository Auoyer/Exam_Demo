//子系统中选择项的所有枚举
var EnumConvert = (function () {
    //现金规划
    var CashConvert = function (LifeNum) {
        var result = "";
        if (LifeNum == 3) {
            result = "3倍";
        } else if (LifeNum == 4) {
            result = "4倍";
        } else if (LifeNum == 5) {
            result = "5倍";
        } else if (LifeNum == 6) {
            result = "6倍";
        }
        return result;
    }

    //教育规划
    var LifeConvet = function (LifeNum) {
        var result = "";
        switch (LifeNum) {
            case 1: return result = "幼儿园教育";
                break;
            case 2: return result = "小学教育";
                break;
            case 3: return result = "初中教育";
                break;
            case 4: return result = "高中教育";
                break;
            case 5: return result = "大学教育";
                break;
            case 6: return result = "留学教育";
                break;
        }
    }

    //保险规划
    var InsuraneConvet = function (LifeNum) {
        var result = "";
        switch (LifeNum) {
            case 1: return result = "遗属需求法";
                break;
            case 2: return result = "生命价值法";
                break;
        }
    }

    //投资规划
    //---当前客户所处家庭生命周期
    var InsertmentConvert = function (LifeNum) {
        var result = "";
        switch (LifeNum) {
            case 1: return result = "单身期";
                break;
            case 2: return result = "家庭形成期（筑巢期）";
                break;
            case 3: return result = "家庭成长期（满巢期）";
                break;
            case 4: return result = "家庭成熟期（离巢期）";
                break;
            case 5: return result = "家庭衰老期（空巢期）";
                break;
        }
    }

    //基础层的建议配置
    var InsertmentBaseConvet = function (LifeNum) {
        var result = "";
        switch (LifeNum) {
            case 1: return result = "意外险、重疾险";
                break;
            case 2: return result = "意外险、重疾险、寿险";
                break;
            case 3: return result = "意外险、重疾险、寿险、教育年金";
                break;
            case 4: return result = "意外险、重疾险、养老年金";
                break;
            case 5: return result = "意外险、看护险";
                break;
        }
    }


    //--已完成规划
    var CompleteplanConvet = function (LifeNum) {
        var result = "";
        switch (LifeNum) {

            case 0: return result = "";
                break;
            case 1: return result = "教育规划";
                break;
            case 2: return result = "消费规划";
                break;
            case 3: return result = "创业规划";
                break;
            case 4: return result = "退休规划";
                break;
        }
    }
    //---选择年份
    var YearSelectConvet = function (LifeNum) {
        var result = "";
        switch (LifeNum) {
            case 0: return result = "";
                break
            case 1: return result = "三个月";
                break;
            case 2: return result = "半年";
                break;
            case 3: return result = "一年";
                break;
            case 4: return result = "二年";
                break;
            case 5: return result = "三年";
                break;
            case 6: return result = "五年";
                break;
        }
    };

    //财产分配规划工具选择
    var DistributionConvert = function (LifeNum) {
        var result = "";
        switch (LifeNum) {
            case 0: return result = "";
                break;
            case 1: return result = "公证";
                break;
            case 2: return result = "信托";
                break;
        }
    }


    //财产传承
    var HeritageConvert = function (LifeNum) {
        var result = "";
        switch (LifeNum) {
            case 0: return result = "";
                break
            case 1: return result = "遗嘱";
                break;
            case 2: return result = "遗嘱信托";
                break;
        }
    };
    //性别
    var SexConvert = function (LifeNum) {
        var result = "";
        switch (LifeNum) {
            case 1: return result = "男";
                break;
            case 2: return result = "女";
                break;
        }
    };

    return {
        CashConvert: CashConvert,
        LifeConvet: LifeConvet,
        InsuraneConvet: InsuraneConvet,
        CompleteplanConvet: CompleteplanConvet,
        YearSelectConvet: YearSelectConvet,
        InsertmentConvert: InsertmentConvert,
        InsertmentBaseConvet:InsertmentBaseConvet,
        DistributionConvert:DistributionConvert,
        HeritageConvert: HeritageConvert,
        SexConvert: SexConvert
    };
})()