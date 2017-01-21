// 评委端-实训考核评分用

//投资规划--序号常量
var IndexFlag = 0;
//显示计数器
var NumCalc = 0;
//财务分配--序号常量
var DistributionOfProperty_index = 0;
//销售机会Id
var TrainExamIdid;
//建议书Id
var ProposalIdid;
var AssessmentResultsIdid;
$(function () {
    var ax = 1.889;
    ax = ax.toFixed(2);
    //销售机会Id
    var TrainExamId = $.getUrlParam("TrainExamId");
    //建议书Id
    var ProposalId = $.getUrlParam("ProposalId");
    //加载考核点得分
    var AssessmentResultsId = $.getUrlParam("AssessmentResultsId");

    TrainExamIdid = TrainExamId;
    ProposalIdid = ProposalId;
    AssessmentResultsIdid = AssessmentResultsId
    //加载考核点标准评分
    LoadStandardScore(TrainExamId);
    //加载考核点信息
    LoadExamInfo(TrainExamId, ProposalId);


    LoadExamResultInfo(AssessmentResultsId);
    //绑定单个考核得分
    var IdName = "#RSFinancialRatios,#RSCashPlan,#RSLifeEducationPlan,#RSLifeEducationPlan1,#RSConsumptionPlan,#RSConsumptionPlan1,";
    IdName += "#RSStartAnUndertakingPlan,#RSStartAnUndertakingPlan1,#RSRetirementPlan,#RSRetirementPlan1,#RSInsurancePlan,#RSInvestmentPlan,";
    IdName += "#RSInvestmentPlan1,#RSTaxPlan,#RSDistributionOfPropertyScore,#RSDistributionOfPropertyScore1,#RSHeritage,#RSHeritage1";
    $(IdName).unbind("blur").blur(function () {
        //获取ID名
        var IdName = $(this).attr("id");
        IdName = IdName.substr(2, IdName.length);
        //$("#" + IdName + "Total").text(0);//默认先赋值为0
        var Max = $(this).attr("maxfloat") * 1;
        $("#warn-" + $(this).attr("id")).remove();

        var Values = $(this).val();
        var pattern6 = /^\d+(\.\d{0,1})?$/;//只能输入一位小数
        if (!pattern6.test(Values)) {
            Values = 0;
            var Msg = "请输入0到" + Max + "的分值(最多1位小数)！";
            showValidateMsg(this.id, Msg);
            return false;
        }
        Values = Values * 1;

        if (0 > Values || Values > Max) {
            var Msg = "请输入0到" + Max + "的分值(最多1位小数)！";
            showValidateMsg(this.id, Msg);
            return false;
        }
        //重新计算总分
        var ObjectiveScore = 0;
        var IsExists = $(this).attr("id").lastIndexOf('1');
        var PartSubjectiveScore = 0;
        var Id = 0;
        if (IsExists > 0) {
            ObjectiveScore = ($("#RO" + IdName.substr(0, IdName.length - 1)).text()) * 1;//客观分
            //获取隐藏ID
            Id = $("#hd" + IdName.substr(0, IdName.length - 1) + "Id1").val();//明细表Id
            if ($("#RS" + IdName.substr(0, IdName.length - 1)) != undefined) {
                PartSubjectiveScore = ($("#RS" + IdName.substr(0, IdName.length - 1)).val()) * 1;
            }
            $("#" + IdName.substr(0, IdName.length - 1) + "Total").text(Values * 1 + PartSubjectiveScore * 1 + ObjectiveScore * 1);
        }
        else {
            ObjectiveScore = ($("#RO" + IdName).text()) * 1;//客观分
            //获取隐藏ID
            Id = $("#hd" + IdName + "Id").val();//明细表Id
            if ($("#RS" + IdName + "1").val() != undefined) {
                PartSubjectiveScore = ($("#RS" + IdName + "1").val()) * 1;
            }
            $("#" + IdName + "Total").text(Values * 1 + PartSubjectiveScore * 1 + ObjectiveScore * 1);
        }

        var ModularId = $(this).attr("modularid"); //考核点ID 
        var ExamPointType = EnumList.ExamPointType.Subjective;//主观题 
        var Status = EnumList.IsCorrect.Correct;//正确
        if (Values == 0) {
            Status = EnumList.IsCorrect.Error;
        }
        var AssessmentPoint = $(this).attr("exampointid");//考核点ID
        //保存 
        _ajaxhepler({
            url: "/CompetitionJudges/Assessment/SaveScore",
            type: "POST",
            async: false,
            dataType: "json",
            data: {
                Id: Id,
                AssessmentResultsId: AssessmentResultsId,
                ExamPointType: ExamPointType,
                ModularId: ModularId,
                AssessmentPoint: AssessmentPoint,
                Status: Status,
                Score: Values,
                TrainExamId: TrainExamId
            }, success: function (data) {
                if (data != null) {
                    //存在Id名称带1的ID
                    if (IsExists > 0) {
                        $("#hd" + IdName.substr(0, IdName.length - 1) + "Id1").val(data.Id);
                    } else {
                        $("#hd" + IdName + "Id").val(data.Id);
                    }
                }
            }
        });
    });

    //绑定保存事件
    $("#btnSave").unbind().click(function () {
        var flag = $("#btnSave").attr("flag");
        if (!ValidateDiv()) {
            dialogHelper.Error({
                content: "有主观题评分存在错误！请返回修改", success: function () {
                }
            });
        } else {
            SubmitExam(ProposalId, TrainExamId, AssessmentResultsId, flag);
        }
    });
    //绑定取消事件
    $("#btnCancel").unbind().click(function () {
        location.href = "/CompetitionJudges/MatchList/NotStart";
    });
});

function SubmitExam(ProposalId, TrainExamId, AssessmentResultsId, flag) {
    _ajaxhepler({
        url: "/CompetitionJudges/Assessment/SaveExamStatus",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: ProposalId,
            TrainExamId: TrainExamId,
            AssessmentResultsId: AssessmentResultsId,
            rId: Math.random(),
        },
        success: function (data) {
            if (data == true) {
                //弹出成功提示 
                dialogHelper.Success({
                    content: "提交成功！",
                    success: function () {
                        var CustomerName = $("#ProposalName").text();
                        location.href = "/CompetitionJudges/MatchList/Start";
                    }
                });
            } else {
                dialogHelper.Error({
                    content: data.ErrorCode
                });
            }
        }
    });
}

//查询还有多少主观题没有评完分
function LookForType(TrainExamId, AssessmentResultsId) {
    var count = 0;
    _ajaxhepler({
        url: "/CompetitionJudges/Assessment/LookForType",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            TrainExamId: TrainExamId,
            AssessmentResultsId: AssessmentResultsId
        },
        success: function (data) {
            if (data != null) {
                if (data > 0) {
                    count = data;
                }
            }
        }
    });
    return count;
}


//加载考核结果
function LoadExamResultInfo(AssessmentResultsId) {
    _ajaxhepler({
        url: "/CompetitionJudges/Assessment/GetExamResultScore",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            AssessmentResultsId: AssessmentResultsId,
            rId: Math.random(),
        },
        success: function (data) {
            if (data != null) {
                SetExamResultInfo(data);//编辑建议书客户信息的设置
            }
        }
    });
}
//计算分值
function CalculationScore(data) {
    //分值
    var Customerinfo = 0, RiskIndex = 0, Liability = 0, IncomeAndExpenses = 0, CashFlow = 0, FinancialRatios = 0, OCashPlan = 0, SCashPlan = 0;
    var OLifeEducationPlan = 0, SLifeEducationPlan = 0, SLifeEducationPlan1 = 0, OConsumptionPlan = 0, SConsumptionPlan = 0, SConsumptionPlan1 = 0, OStartAnUndertakingPlan = 0;
    var SStartAnUndertakingPlan = 0, SStartAnUndertakingPlan1 = 0, ORetirementPlan = 0, SRetirementPlan1 = 0, SRetirementPlan = 0, OInsurancePlan = 0, SInsurancePlan = 0;
    var OInvestmentPlan = 0, SInvestmentPlan = 0, SInvestmentPlan1 = 0, OTaxPlan = 0, STaxPlan = 0, DistributionOfProperty = 0, DistributionOfProperty1 = 0, OHeritage = 0, SHeritage = 0, SHeritage1 = 0;
    //主观题ID
    var FinancialRatiosId = 0, CashPlanId = 0, LifeEducationPlanId = 0, LifeEducationPlanId1 = 0, ConsumptionPlanId = 0, ConsumptionPlanId1 = 0, StartAnUndertakingPlanId = 0;
    var StartAnUndertakingPlanId1 = 0, RetirementPlanId = 0, RetirementPlanId1 = 0, InsurancePlanId = 0, InvestmentPlanId = 0, InvestmentPlanId1 = 0, TaxPlanId = 0, DistributionOfPropertyScoreId = 0;
    var DistributionOfPropertyScoreId1 = 0, HeritageId = 0, HeritageId1 = 0;
    //答案
    var FinancialRatiosAnswer = "", CashPlanAnswer = "", LifeEducationPlanAnswer = "", LifeEducationPlanAnswer1 = "", ConsumptionPlanAnswer = "", ConsumptionPlanAnswer1 = "";
    var StartAnUndertakingPlanAnswer = "", StartAnUndertakingPlanAnswer1 = "", RetirementPlanAnswer = "", RetirementPlanAnswer1 = "", InsurancePlanAnswer = "";
    var InvestmentPlanAnswer = "", InvestmentPlanAnswer1 = "", TaxPlanAnswer = "", DistributionOfPropertyAnswer = "", DistributionOfPropertyAnswer1 = "", HeritageAnswer = "", HeritageAnswer1 = "";
    FinancialRatios = "";
    //考核点Id
    var ExamPointId = "";


    SCashPlan = "";
    SLifeEducationPlan = "";
    SLifeEducationPlan1 = "";
    SConsumptionPlan = "";
    SConsumptionPlan1 = "";
    SStartAnUndertakingPlan = "";
    SStartAnUndertakingPlan1 = "";
    SRetirementPlan = "";
    SRetirementPlan1 = "";
    SInsurancePlan = "";
    SInvestmentPlan = "";
    SInvestmentPlan1 = "";
    STaxPlan = "";
    DistributionOfProperty = "";
    DistributionOfProperty1 = "";
    SHeritage = "";
    SHeritage1 = "";

    var Count = data.length;
    for (var i = 0; i < Count; i++) {
        switch (data[i].ModularId * 1) {
            case EnumList.ExamContent.Customerinfo://客户信息
                Customerinfo += data[i].Score;
                break;
            case EnumList.ExamContent.RiskIndex://风险测评-风险指标
                RiskIndex += data[i].Score;
                break;
            case EnumList.ExamContent.Liability://财务分析-资产负债表
                Liability += data[i].Score;
                break;
            case EnumList.ExamContent.IncomeAndExpenses://财务分析-收支储蓄表
                IncomeAndExpenses += data[i].Score;
                break;
            case EnumList.ExamContent.CashFlow://财务分析-现金流量表
                CashFlow += data[i].Score;
                break;
            case EnumList.ExamContent.FinancialRatios://财务分析-财务比率分析
                FinancialRatios == "" ? 0 : FinancialRatios;
                FinancialRatios = data[i].Score;
                FinancialRatiosId = data[i].Id;


                // FinancialRatiosAnswer = data[i].Answer;

                if (data[i].Answer == null) {
                    FinancialRatiosAnswer = "";
                } else {
                    FinancialRatiosAnswer = data[i].Answer;
                }
                break;
            case EnumList.ExamContent.CashPlan://现金规划
                //  CashPlanId = data[i].Id;
                if (data[i].ExamPointType == EnumList.ExamPointType.Objective)//客观题
                {
                    OCashPlan += data[i].Score;
                    //SCashPlan = ""; //主观题为空
                } else {
                    SCashPlan == "" ? 0 : SCashPlan;
                    CashPlanId = data[i].Id;
                    SCashPlan = data[i].Score;
                    if (data[i].Answer == null) {
                        CashPlanAnswer = "";
                    } else {
                        CashPlanAnswer = data[i].Answer;
                    }

                }
                break;
            case EnumList.ExamContent.LifeEducationPlan://生涯规划-教育规划
                if (data[i].ExamPointType == EnumList.ExamPointType.Objective)//客观题
                {
                    OLifeEducationPlan += data[i].Score;
                    //SLifeEducationPlan = "";
                    //SLifeEducationPlan1 = "";
                } else {
                    if (data[i].ExamPointId == 86) {
                        SLifeEducationPlan == "" ? 0 : SLifeEducationPlan;
                        SLifeEducationPlan = data[i].Score;//理财方案
                        LifeEducationPlanId = data[i].Id;


                        if (data[i].Answer == null) {
                            LifeEducationPlanAnswer = "";
                        } else {
                            LifeEducationPlanAnswer = data[i].Answer;
                        }
                    }
                    if (data[i].ExamPointId == 87) {
                        SLifeEducationPlan1 == "" ? 0 : SLifeEducationPlan1;
                        SLifeEducationPlan1 = data[i].Score; //教育规划分析
                        LifeEducationPlanId1 = data[i].Id;
                        // LifeEducationPlanAnswer1 = data[i].Answer;

                        if (data[i].Answer == null) {
                            LifeEducationPlanAnswer1 = "";
                        } else {
                            LifeEducationPlanAnswer1 = data[i].Answer;
                        }
                    }
                }
                break;
            case EnumList.ExamContent.ConsumptionPlan://生涯规划-消费规划
                if (data[i].ExamPointType == EnumList.ExamPointType.Objective)//客观题
                {
                    OConsumptionPlan += data[i].Score;

                } else {
                    if (data[i].ExamPointId == 101) {
                        SConsumptionPlan == "" ? 0 : SConsumptionPlan;
                        SConsumptionPlan = data[i].Score;//理财方案
                        ConsumptionPlanId = data[i].Id;
                        // ConsumptionPlanAnswer = data[i].Answer;

                        if (data[i].Answer == null) {
                            ConsumptionPlanAnswer = "";
                        } else {
                            ConsumptionPlanAnswer = data[i].Answer;
                        }

                    }
                    if (data[i].ExamPointId == 102) {
                        SConsumptionPlan1 == "" ? 0 : SConsumptionPlan1;
                        SConsumptionPlan1 = data[i].Score;//消费规划分析
                        ConsumptionPlanId1 = data[i].Id;
                        // ConsumptionPlanAnswer1 = data[i].Answer;

                        if (data[i].Answer == null) {
                            ConsumptionPlanAnswer1 = "";
                        } else {
                            ConsumptionPlanAnswer1 = data[i].Answer;
                        }
                    }
                }

                if (Count - 1 != i) {
                    ExamPointId = ExamPointId + data[i].ExamPointId + ",";
                } else {
                    ExamPointId = ExamPointId + data[i].ExamPointId;
                }

                break;
            case EnumList.ExamContent.StartAnUndertakingPlan://生涯规划-创业规划
                if (data[i].ExamPointType == EnumList.ExamPointType.Objective)//客观题
                {
                    OStartAnUndertakingPlan += data[i].Score;

                } else {
                    if (data[i].ExamPointId == 105) {

                        SStartAnUndertakingPlan == "" ? 0 : SStartAnUndertakingPlan;
                        SStartAnUndertakingPlan = data[i].Score;//理财方案
                        StartAnUndertakingPlanId = data[i].Id;
                        //StartAnUndertakingPlanAnswer = data[i].Answer;

                        if (data[i].Answer == null) {
                            StartAnUndertakingPlanAnswer = "";
                        } else {
                            StartAnUndertakingPlanAnswer = data[i].Answer;
                        }
                    }
                    if (data[i].ExamPointId == 106) {

                        SStartAnUndertakingPlan1 == "" ? 0 : SStartAnUndertakingPlan1;
                        SStartAnUndertakingPlan1 = data[i].Score;//创业规划分析
                        StartAnUndertakingPlanId1 = data[i].Id;
                        //StartAnUndertakingPlanAnswer1 = data[i].Answer;

                        if (data[i].Answer == null) {
                            StartAnUndertakingPlanAnswer1 = "";
                        } else {
                            StartAnUndertakingPlanAnswer1 = data[i].Answer;
                        }
                    }
                }
                break;
            case EnumList.ExamContent.RetirementPlan://生涯规划-退休规划
                if (data[i].ExamPointType == EnumList.ExamPointType.Objective)//客观题
                {
                    ORetirementPlan += data[i].Score;

                } else {
                    if (data[i].ExamPointId == 120) {

                        SRetirementPlan == "" ? 0 : SRetirementPlan;
                        SRetirementPlan = data[i].Score; //理财方案
                        RetirementPlanId = data[i].Id;
                        //RetirementPlanAnswer = data[i].Answer;


                        if (data[i].Answer == null) {
                            RetirementPlanAnswer = "";
                        } else {
                            RetirementPlanAnswer = data[i].Answer;
                        }
                    }
                    if (data[i].ExamPointId == 121) {

                        SRetirementPlan1 == "" ? 0 : SRetirementPlan1;
                        SRetirementPlan1 = data[i].Score;//退休规划分析
                        RetirementPlanId1 = data[i].Id;
                        // RetirementPlanAnswer1 = data[i].Answer;

                        if (data[i].Answer == null) {
                            RetirementPlanAnswer1 = "";
                        } else {
                            RetirementPlanAnswer1 = data[i].Answer;
                        }
                    }
                }
                break;
            case EnumList.ExamContent.InsurancePlan://生涯规划-保险规划
                if (data[i].ExamPointType == EnumList.ExamPointType.Objective)//客观题
                {
                    OInsurancePlan += data[i].Score;

                } else {
                    SInsurancePlan == "" ? 0 : SInsurancePlan;
                    SInsurancePlan = data[i].Score;
                    InsurancePlanId = data[i].Id;
                    $("#RSInsurancePlan").attr("exampointid", data[i].ExamPointId);
                    // InsurancePlanAnswer = data[i].Answer;

                    if (data[i].Answer == null) {
                        InsurancePlanAnswer = "";
                    } else {
                        InsurancePlanAnswer = data[i].Answer;
                    }
                }
                break;
            case EnumList.ExamContent.InvestmentPlan://投资规划
                if (data[i].ExamPointType == EnumList.ExamPointType.Objective)//客观题
                {
                    OInvestmentPlan += data[i].Score;

                } else {
                    if (data[i].ExamPointId == 162) {
                        SInvestmentPlan == "" ? 0 : SInvestmentPlan;
                        SInvestmentPlan = data[i].Score;//产品选择
                        InvestmentPlanId = data[i].Id;
                        // InvestmentPlanAnswer = data[i].Answer;

                        if (data[i].Answer == null) {
                            InvestmentPlanAnswer = "";
                        } else {
                            InvestmentPlanAnswer = data[i].Answer;
                        }
                    }
                    if (data[i].ExamPointId == 163) {
                        SInvestmentPlan1 == "" ? 0 : SInvestmentPlan1;
                        SInvestmentPlan1 = data[i].Score;//投资规划分析
                        InvestmentPlanId1 = data[i].Id;
                        // InvestmentPlanAnswer1 = data[i].Answer;

                        if (data[i].Answer == null) {
                            InvestmentPlanAnswer1 = "";
                        } else {
                            InvestmentPlanAnswer1 = data[i].Answer;
                        }
                    }
                }
                break;
            case EnumList.ExamContent.TaxPlan://税收筹划
                if (data[i].ExamPointType == EnumList.ExamPointType.Objective)//客观题
                {
                    OTaxPlan += data[i].Score;

                } else {
                    STaxPlan == "" ? 0 : STaxPlan;
                    STaxPlan = data[i].Score;
                    TaxPlanId = data[i].Id;
                    // TaxPlanAnswer = data[i].Answer;

                    if (data[i].Answer == null) {
                        TaxPlanAnswer = "";
                    } else {
                        TaxPlanAnswer = data[i].Answer;
                    }
                }
                break;
            case EnumList.ExamContent.DistributionOfProperty://财产分配 

                if (data[i].ExamPointId == 174) {
                    DistributionOfProperty == "" ? 0 : DistributionOfProperty;
                    DistributionOfProperty = data[i].Score; //婚姻、财产状况分析
                    DistributionOfPropertyScoreId = data[i].Id;
                    // DistributionOfPropertyAnswer = data[i].Answer;

                    if (data[i].Answer == null) {
                        DistributionOfPropertyAnswer = "";
                    } else {
                        DistributionOfPropertyAnswer = data[i].Answer;
                    }
                }
                if (data[i].ExamPointId == 175) {

                    DistributionOfProperty1 == "" ? 0 : DistributionOfProperty1;
                    DistributionOfProperty1 = data[i].Score;//财产分配规划分析
                    DistributionOfPropertyScoreId1 = data[i].Id;
                    //DistributionOfPropertyAnswer1 = data[i].Answer;

                    if (data[i].Answer == null) {
                        DistributionOfPropertyAnswer1 = "";
                    } else {
                        DistributionOfPropertyAnswer1 = data[i].Answer;
                    }
                }
                break;
            case EnumList.ExamContent.Heritage://财产传承
                if (data[i].ExamPointType == EnumList.ExamPointType.Objective)//客观题
                {
                    OHeritage += data[i].Score;

                } else {
                    if (data[i].ExamPointId == 201) {

                        SHeritage == "" ? 0 : SHeritage;
                        SHeritage = data[i].Score;//财务分析
                        HeritageId = data[i].Id;
                        //HeritageAnswer = data[i].Answer;

                        if (data[i].Answer == null) {
                            HeritageAnswer = "";
                        } else {
                            HeritageAnswer = data[i].Answer;
                        }
                    }
                    if (data[i].ExamPointId == 202) {
                        SHeritage1 == "" ? 0 : SHeritage1;
                        SHeritage1 = data[i].Score;//财产传承规划分析
                        HeritageId1 = data[i].Id;
                        //HeritageAnswer1 = data[i].Answer;

                        if (data[i].Answer == null) {
                            HeritageAnswer1 = "";
                        } else {
                            HeritageAnswer1 = data[i].Answer;
                        }
                    }
                }
                break;
        }
    }
    var obj = new Object();
    obj.Customerinfo = Customerinfo;
    obj.RiskIndex = RiskIndex;
    obj.Liability = Liability;
    obj.IncomeAndExpenses = IncomeAndExpenses;
    obj.CashFlow = CashFlow;
    obj.FinancialRatios = FinancialRatios;
    obj.OCashPlan = OCashPlan;
    obj.SCashPlan = SCashPlan;
    obj.OLifeEducationPlan = OLifeEducationPlan;
    obj.SLifeEducationPlan = SLifeEducationPlan;
    obj.SLifeEducationPlan1 = SLifeEducationPlan1;
    obj.OConsumptionPlan = OConsumptionPlan;
    obj.SConsumptionPlan = SConsumptionPlan;
    obj.SConsumptionPlan1 = SConsumptionPlan1;
    obj.OStartAnUndertakingPlan = OStartAnUndertakingPlan;
    obj.SStartAnUndertakingPlan = SStartAnUndertakingPlan;
    obj.SStartAnUndertakingPlan1 = SStartAnUndertakingPlan1;
    obj.ORetirementPlan = ORetirementPlan;
    obj.SRetirementPlan = SRetirementPlan;
    obj.SRetirementPlan1 = SRetirementPlan1;
    obj.OInsurancePlan = OInsurancePlan;
    obj.SInsurancePlan = SInsurancePlan;
    obj.OInvestmentPlan = OInvestmentPlan;
    obj.SInvestmentPlan = SInvestmentPlan;
    obj.SInvestmentPlan1 = SInvestmentPlan1;
    obj.OTaxPlan = OTaxPlan;
    obj.STaxPlan = STaxPlan;
    obj.DistributionOfProperty = DistributionOfProperty;
    obj.DistributionOfProperty1 = DistributionOfProperty1;
    obj.OHeritage = OHeritage;
    obj.SHeritage = SHeritage;
    obj.SHeritage1 = SHeritage1;
    obj.RetirementPlanId = RetirementPlanId;
    obj.RetirementPlanId1 = RetirementPlanId1;
    obj.InsurancePlanId = InsurancePlanId;
    obj.InvestmentPlanId = InvestmentPlanId;
    obj.InvestmentPlanId1 = InvestmentPlanId1;
    obj.TaxPlanId = TaxPlanId;
    obj.DistributionOfPropertyScoreId = DistributionOfPropertyScoreId;
    obj.DistributionOfPropertyScoreId1 = DistributionOfPropertyScoreId1;
    obj.HeritageId = HeritageId;
    obj.HeritageId1 = HeritageId1;
    obj.FinancialRatiosId = FinancialRatiosId;
    obj.CashPlanId = CashPlanId;
    obj.LifeEducationPlanId = LifeEducationPlanId;
    obj.LifeEducationPlanId1 = LifeEducationPlanId1;
    obj.ConsumptionPlanId = ConsumptionPlanId;
    obj.ConsumptionPlanId1 = ConsumptionPlanId1;
    obj.StartAnUndertakingPlanId = StartAnUndertakingPlanId;
    obj.StartAnUndertakingPlanId1 = StartAnUndertakingPlanId1;
    //参考答案
    obj.FinancialRatiosAnswer = FinancialRatiosAnswer;
    obj.CashPlanAnswer = CashPlanAnswer;
    obj.LifeEducationPlanAnswer = LifeEducationPlanAnswer;
    obj.LifeEducationPlanAnswer1 = LifeEducationPlanAnswer1;
    obj.ConsumptionPlanAnswer = ConsumptionPlanAnswer;
    obj.ConsumptionPlanAnswer1 = ConsumptionPlanAnswer1;
    obj.StartAnUndertakingPlanAnswer = StartAnUndertakingPlanAnswer;
    obj.StartAnUndertakingPlanAnswer1 = StartAnUndertakingPlanAnswer1;
    obj.RetirementPlanAnswer = RetirementPlanAnswer;
    obj.RetirementPlanAnswer1 = RetirementPlanAnswer1;
    obj.InsurancePlanAnswer = InsurancePlanAnswer;
    obj.InvestmentPlanAnswer = InvestmentPlanAnswer;
    obj.InvestmentPlanAnswer1 = InvestmentPlanAnswer1;
    obj.TaxPlanAnswer = TaxPlanAnswer;
    obj.DistributionOfPropertyAnswer = DistributionOfPropertyAnswer;
    obj.DistributionOfPropertyAnswer1 = DistributionOfPropertyAnswer1;
    obj.HeritageAnswer = HeritageAnswer;
    obj.HeritageAnswer1 = HeritageAnswer1;

    obj.ExamPointId = ExamPointId;
    return obj;
}
//设置考核结果信息
function SetExamResultInfo(data) {
    var obj = CalculationScore(data);
    var MaxValue = 0;
    $("#RCustomerinfo,#CustomerinfoTotal").text(obj.Customerinfo);//客户基本信息   
    $("#RRiskIndex,#RiskIndexTotal").text(obj.RiskIndex);//风险测评  
    $("#RLiability,#LiabilityTotal").text(obj.Liability);//资产负债表  
    $("#RIncomeAndExpenses,#IncomeAndExpensesTotal").text(obj.IncomeAndExpenses);//收支储蓄表  
    $("#RCashFlow,#CashFlowTotal").text(obj.CashFlow);//现金流量表  

    if ($("#FinancialRatios").text() * 1 != 0) {
        $("#hdFinancialRatiosId").val(obj.FinancialRatiosId);
        $("#RSFinancialRatios").val(obj.FinancialRatios);
        $("#FinancialRatiosTotal").text(obj.FinancialRatios == "" ? 0 : obj.FinancialRatios);
        MaxValue = $("#FinancialRatios").text();
        $("#RSFinancialRatios").attr("maxfloat", MaxValue);//设置最大值   
    } else {
        $("#FinancialRatiosDiv").hide();//隐藏财务比率分析模块
    }

    $("#ROCashPlan").text(obj.OCashPlan);//现金规划--客观题标准分   
    $("#RSCashPlan").val(obj.SCashPlan);//现金规划--主观题标准分 
    $("#hdCashPlanId").val(obj.CashPlanId);
    $("#CashPlanTotal").text(obj.SCashPlan + obj.OCashPlan);//现金规划--总分
    MaxValue = $("#SCashPlan").text();
    $("#RSCashPlan").attr("maxfloat", MaxValue);//设置最大值 
    if (MaxValue * 1 == 0) {
        //  $("#RSCashPlan").addClass("b-gray").attr("readonly", "readonly");//禁用
        $("#RSCashPlan").addClass("b-gray").attr("disabled", true);//禁用
    }

    $("#ROLifeEducationPlan").text(obj.OLifeEducationPlan);
    $("#RSLifeEducationPlan").val(obj.SLifeEducationPlan);
    $("#hdLifeEducationPlanId").val(obj.LifeEducationPlanId);
    $("#RSLifeEducationPlan1").val(obj.SLifeEducationPlan1);
    $("#hdLifeEducationPlanId1").val(obj.LifeEducationPlanId1);
    $("#LifeEducationPlanTotal").text(obj.SLifeEducationPlan * 1 + obj.SLifeEducationPlan1 * 1 + obj.OLifeEducationPlan * 1);//教育规划 
    MaxValue = $("#SLifeEducationPlan").text();
    $("#RSLifeEducationPlan").attr("maxfloat", MaxValue);//设置最大值 
    if (MaxValue * 1 == 0) {
        //  $("#RSLifeEducationPlan").addClass("b-gray").attr("readonly", "readonly");//禁用
        $("#RSLifeEducationPlan").addClass("b-gray").attr("disabled", true);//禁用
    }
    MaxValue = $("#SLifeEducationPlan1").text();
    $("#RSLifeEducationPlan1").attr("maxfloat", MaxValue);//设置最大值 
    if (MaxValue * 1 == 0) {
        //  $("#RSLifeEducationPlan1").addClass("b-gray").attr("readonly", "readonly");//禁用
        $("#RSLifeEducationPlan1").addClass("b-gray").attr("disabled", true);
    }



    $("#ROConsumptionPlan").text(obj.OConsumptionPlan);
    $("#RSConsumptionPlan").val(obj.SConsumptionPlan);
    $("#hdConsumptionPlanId").val(obj.ConsumptionPlanId);
    $("#RSConsumptionPlan1").val(obj.SConsumptionPlan1);
    $("#hdConsumptionPlanId1").val(obj.ConsumptionPlanId1);
    $("#ConsumptionPlanTotal").text(obj.SConsumptionPlan * 1 + obj.SConsumptionPlan1 * 1 + obj.OConsumptionPlan * 1);//消费规划
    MaxValue = $("#SConsumptionPlan").text();
    $("#RSConsumptionPlan").attr("maxfloat", MaxValue);//设置最大值 
    if (MaxValue * 1 == 0) {
        //  $("#RSConsumptionPlan").addClass("b-gray").attr("readonly", "readonly");//禁用
        $("#RSConsumptionPlan").addClass("b-gray").attr("disabled", true);//禁用
    }
    MaxValue = $("#SConsumptionPlan1").text();
    $("#RSConsumptionPlan1").attr("maxfloat", MaxValue);//设置最大值 
    if (MaxValue * 1 == 0) {
        //  $("#RSConsumptionPlan1").addClass("b-gray").attr("readonly", "readonly");//禁用
        $("#RSConsumptionPlan1").addClass("b-gray").attr("disabled", true);//禁用
    }

    $("#ROStartAnUndertakingPlan").text(obj.OStartAnUndertakingPlan);
    $("#RSStartAnUndertakingPlan").val(obj.SStartAnUndertakingPlan);
    $("#hdStartAnUndertakingPlanId").val(obj.StartAnUndertakingPlanId);
    $("#RSStartAnUndertakingPlan1").val(obj.SStartAnUndertakingPlan1);
    $("#hdStartAnUndertakingPlanId1").val(obj.StartAnUndertakingPlanId1);
    $("#StartAnUndertakingPlanTotal").text(obj.SStartAnUndertakingPlan * 1 + obj.SStartAnUndertakingPlan1 * 1 + obj.OStartAnUndertakingPlan * 1);//创业规划
    MaxValue = $("#SStartAnUndertakingPlan").text();
    $("#RSStartAnUndertakingPlan").attr("maxfloat", MaxValue);//设置最大值  
    if (MaxValue * 1 == 0) {
        // $("#RSStartAnUndertakingPlan").addClass("b-gray").attr("readonly", "readonly");//禁用
        $("#RSStartAnUndertakingPlan").addClass("b-gray").attr("disabled", true);//禁用
    }
    MaxValue = $("#SStartAnUndertakingPlan1").text();
    $("#RSStartAnUndertakingPlan1").attr("maxfloat", MaxValue);//设置最大值  
    if (MaxValue * 1 == 0) {
        //   $("#RSStartAnUndertakingPlan1").addClass("b-gray").attr("readonly", "readonly");//禁用
        $("#RSStartAnUndertakingPlan1").addClass("b-gray").attr("disabled", true);//禁用
    }


    $("#RORetirementPlan").text(obj.ORetirementPlan);
    $("#RSRetirementPlan").val(obj.SRetirementPlan);
    $("#hdRetirementPlanId").val(obj.RetirementPlanId);
    $("#RSRetirementPlan1").val(obj.SRetirementPlan1);
    $("#hdRetirementPlanId1").val(obj.RetirementPlanId1);
    $("#RetirementPlanTotal").text(obj.SRetirementPlan * 1 + obj.SRetirementPlan1 * 1 + obj.ORetirementPlan * 1);//退休规划
    MaxValue = $("#SRetirementPlan").text();
    $("#RSRetirementPlan").attr("maxfloat", $("#SRetirementPlan").text());//设置最大值  
    if (MaxValue * 1 == 0) {
        //  $("#RSRetirementPlan").addClass("b-gray").attr("readonly", "readonly");//禁用
        $("#RSRetirementPlan").addClass("b-gray").attr("disabled", true);//禁用
    }
    MaxValue = $("#SRetirementPlan1").text();
    $("#RSRetirementPlan1").attr("maxfloat", $("#SRetirementPlan1").text());//设置最大值  
    if (MaxValue * 1 == 0) {
        // $("#RSRetirementPlan1").addClass("b-gray").attr("readonly", "readonly");//禁用
        $("#RSRetirementPlan1").addClass("b-gray").attr("disabled", true);//禁用
    }

    //获取寿险需求测算方法
    var MethodTypeId = $("#FinanceInsurancePlanDiv #MethodTypeId").val() * 1;
    //MethodTypeId=1时为遗属需求法，否则生命价值法
    if (MethodTypeId == 1) {
        $("#ROInsurancePlan").attr("exampointid", 150);
    } else {
        $("#ROInsurancePlan").attr("exampointid", 160);
    }
    $("#ROInsurancePlan").text(obj.OInsurancePlan);
    $("#RSInsurancePlan").val(obj.SInsurancePlan);
    $("#hdInsurancePlanId").val(obj.InsurancePlanId);
    $("#InsurancePlanTotal").text(obj.SInsurancePlan * 1 + obj.OInsurancePlan * 1);//保险规划
    MaxValue = $("#SInsurancePlan").text();
    $("#RSInsurancePlan").attr("maxfloat", MaxValue);//设置最大值   
    if (MaxValue * 1 == 0) {

        // $("#RSInsurancePlan").addClass("b-gray").attr("readonly", "readonly");//禁用
        $("#RSInsurancePlan").addClass("b-gray").attr("disabled", true);//禁用
    }

    $("#ROInvestmentPlan").text(obj.OInvestmentPlan);
    $("#RSInvestmentPlan").val(obj.SInvestmentPlan);
    $("#hdInvestmentPlanId").val(obj.InvestmentPlanId);
    $("#RSInvestmentPlan1").val(obj.SInvestmentPlan1);
    $("#hdInvestmentPlanId1").val(obj.InvestmentPlanId1);
    $("#InvestmentPlanTotal").text(obj.SInvestmentPlan * 1 + obj.SInvestmentPlan1 * 1 + obj.OInvestmentPlan * 1);//投资规划
    MaxValue = $("#SInvestmentPlan").text();
    $("#RSInvestmentPlan").attr("maxfloat", MaxValue);//设置最大值   
    if (MaxValue * 1 == 0) {

        //   $("#RSInvestmentPlan").addClass("b-gray").attr("readonly", "readonly");//禁用
        $("#RSInvestmentPlan").addClass("b-gray").attr("disabled", true);//禁用
    }
    MaxValue = $("#SInvestmentPlan1").text();
    $("#RSInvestmentPlan1").attr("maxfloat", MaxValue);//设置最大值   
    if (MaxValue * 1 == 0) {
        //  $("#RSInvestmentPlan1").addClass("b-gray").attr("readonly", "readonly");//禁用
        $("#RSInvestmentPlan1").addClass("b-gray").attr("disabled", true);//禁用
    }


    $("#ROTaxPlan").text(obj.OTaxPlan);
    $("#RSTaxPlan").val(obj.STaxPlan);
    $("#hdTaxPlanId").val(obj.TaxPlanId);
    $("#TaxPlanTotal").text(obj.STaxPlan + obj.OTaxPlan);//税收规划
    MaxValue = $("#STaxPlan").text();
    $("#RSTaxPlan").attr("maxfloat", MaxValue);//设置最大值  
    if (MaxValue * 1 == 0) {
        // $("#RSTaxPlan").addClass("b-gray").attr("readonly", "readonly");//禁用
        $("#RSTaxPlan").addClass("b-gray").attr("disabled", true);//禁用
    }


    $("#RSDistributionOfPropertyScore").val(obj.DistributionOfProperty);
    $("#hdDistributionOfPropertyScoreId").val(obj.DistributionOfPropertyScoreId);
    $("#RSDistributionOfPropertyScore1").val(obj.DistributionOfProperty1);
    $("#hdDistributionOfPropertyScoreId1").val(obj.DistributionOfPropertyScoreId1);
    $("#DistributionOfPropertyScoreTotal").text(obj.DistributionOfProperty * 1 + obj.DistributionOfProperty1 * 1);//财产分配表
    MaxValue = $("#DistributionOfProperty").text();
    $("#RSDistributionOfPropertyScore").attr("maxfloat", MaxValue);//设置最大值 
    if (MaxValue * 1 == 0) {
        //  $("#RSDistributionOfPropertyScore").addClass("b-gray").attr("readonly", "readonly");//禁用
        $("#RSDistributionOfPropertyScore").addClass("b-gray").attr("disabled", true);//禁用
    }

    MaxValue = $("#DistributionOfProperty1").text();
    $("#RSDistributionOfPropertyScore1").attr("maxfloat", MaxValue);//设置最大值    
    if (MaxValue * 1 == 0) {
        //  $("#RSDistributionOfPropertyScore1").addClass("b-gray").attr("readonly", "readonly");//禁用
        $("#RSDistributionOfPropertyScore1").addClass("b-gray").attr("disabled", true);//禁用
    }


    $("#ROHeritage").text(obj.OHeritage);
    $("#hdHeritageId").val(obj.HeritageId);
    $("#RSHeritage").val(obj.SHeritage);
    $("#hdHeritageId1").val(obj.HeritageId1);
    $("#RSHeritage1").val(obj.SHeritage1);
    $("#HeritageTotal").text(obj.SHeritage * 1 + obj.SHeritage1 * 1 + obj.OHeritage * 1);//财产传承
    MaxValue = $("#SHeritage").text();
    $("#RSHeritage").attr("maxfloat", $("#SHeritage").text());//设置最大值   
    if (MaxValue * 1 == 0) {
        //  $("#RSHeritage").addClass("b-gray").attr("readonly", "readonly");//禁用
        $("#RSHeritage").addClass("b-gray").attr("disabled", true);//禁用
    }
    MaxValue = $("#SHeritage1").text();
    $("#RSHeritage1").attr("maxfloat", $("#SHeritage1").text());//设置最大值   
    if (MaxValue * 1 == 0) {
        //   $("#RSHeritage1").addClass("b-gray").attr("readonly", "readonly");//禁用
        $("#RSHeritage1").addClass("b-gray").attr("disabled", true);//禁用
    }
}
//加载标准评分
function LoadStandardScore(TrainExamId) {
    _ajaxhepler({
        url: "/CompetitionJudges/Assessment/GetStandardScore",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            TrainExamId: TrainExamId,
            rId: Math.random(),
        },
        success: function (data) {
            if (data != null) {
                SetStandardScoreInfo(data);//编辑建议书客户信息的设置
            }
        }
    });
}
//设置标准分
function SetStandardScoreInfo(data) {
    var obj = CalculationScore(data);
    if (obj.Customerinfo == 0) {
        $("#ProposalCustomer").hide();
    } else {
        $("#Customerinfo").text(obj.Customerinfo);
    }
    if (obj.RiskIndex == 0) {
        $("#RiskIndexDiv").hide();
    } else {
        $("#RiskIndex").text(obj.RiskIndex);
    }
    if (obj.Liability == 0) {
        $("#FinanceLiabilityDiv").hide();
    } else {
        $("#Liability").text(obj.Liability);
    }
    if (obj.IncomeAndExpenses == 0) {
        $("#FinanceIncomeAndExpensesDiv").hide();
    } else {
        $("#IncomeAndExpenses").text(obj.IncomeAndExpenses);
    }
    if (obj.CashFlow == 0) {
        $("#CashFlow").hide();
    } else {
        $("#OCashFlow").text(obj.CashFlow);
    }
    if (obj.FinancialRatios == 0) {
        $("#FinancialRatiosDiv").hide();//隐藏财务比率分析模块
    } else {
        $("#FinancialRatios").text(obj.FinancialRatios);
        $("#FinancialRatiosAnswer").text(obj.FinancialRatiosAnswer);
    }
    if (obj.OCashPlan == 0 && obj.SCashPlan == 0) {
        $("#FinanceCashPlanDiv").hide();
    } else {
        $("#OCashPlan").text(obj.OCashPlan);//现金规划--客观题标准分
        $("#SCashPlan").text(obj.SCashPlan);//现金规划--主观题标准分 
        $("#CashPlanAnswer").text(obj.CashPlanAnswer);
    }
    if (obj.OLifeEducationPlan == 0 && obj.SLifeEducationPlan == 0 && obj.SLifeEducationPlan1 == 0) {
        $("#LifeEducationPlanDiv").hide();
    } else {
        $("#OLifeEducationPlan").text(obj.OLifeEducationPlan);
        $("#SLifeEducationPlan").text(obj.SLifeEducationPlan);
        $("#LifeEducationPlanAnswer").text(obj.LifeEducationPlanAnswer);
        $("#SLifeEducationPlan1").text(obj.SLifeEducationPlan1);
        $("#LifeEducationPlanAnswer1").text(obj.LifeEducationPlanAnswer1);
    }

    //消费规划
    if (obj.OConsumptionPlan == 0 && obj.SConsumptionPlan == 0 && obj.SConsumptionPlan1 == 0) {
        $("#ConsumptionPlanDiv").hide();
    } else {

        var num = 0;
        var num2 = 0;
        var listId = obj.ExamPointId.split(",");
        for (var i = 0; i < listId.length; i++) {
            //88-92考点属于购房规划
            if (listId[i] == "88" || listId[i] == "89" || listId[i] == "90" || listId[i] == "91" || listId[i] == "92") {
                num = num + 1;
            }
            //93-100考点属于购车规划
            if (listId[i] == "93" || listId[i] == "94" || listId[i] == "95" || listId[i] == "96" || listId[i] == "97" || listId[i] == "98" || listId[i] == "99" || listId[i] == "100") {
                num2 = num2 + 1;
            }
        }

        if (num > 0) {
            $("#ShopHouseDiv").show();
        } else {
            $("#ShopHouseDiv").hide();
        }
        if (num2 > 0) {
            $("#ShopCarDiv").show();
        } else {
            $("#ShopCarDiv").hide();
        }

        $("#OConsumptionPlan").text(obj.OConsumptionPlan);
        $("#SConsumptionPlan").text(obj.SConsumptionPlan);
        $("#ConsumptionPlanAnswer").text(obj.ConsumptionPlanAnswer);
        $("#SConsumptionPlan1").text(obj.SConsumptionPlan1);
        $("#ConsumptionPlanAnswer1").text(obj.ConsumptionPlanAnswer1);
    }
    if (obj.OStartAnUndertakingPlan == 0 && obj.SStartAnUndertakingPlan == 0 && obj.SStartAnUndertakingPlan1 == 0) {
        $("#StartAnUndertakingPlanDiv").hide();
    } else {
        $("#OStartAnUndertakingPlan").text(obj.OStartAnUndertakingPlan);
        $("#SStartAnUndertakingPlan").text(obj.SStartAnUndertakingPlan);
        $("#StartAnUndertakingPlanAnswer").text(obj.StartAnUndertakingPlanAnswer);
        $("#SStartAnUndertakingPlan1").text(obj.SStartAnUndertakingPlan1);
        $("#StartAnUndertakingPlanAnswer1").text(obj.StartAnUndertakingPlanAnswer1);
    }
    if (obj.ORetirementPlan == 0 && obj.SRetirementPlan == 0 && obj.SRetirementPlan1 == 0) {
        $("#LiveRetirementPlanDiv").hide();
    } else {
        $("#ORetirementPlan").text(obj.ORetirementPlan);
        $("#SRetirementPlan").text(obj.SRetirementPlan);
        $("#RetirementPlanAnswer").text(obj.RetirementPlanAnswer);
        $("#SRetirementPlan1").text(obj.SRetirementPlan1);
        $("#RetirementPlanAnswer1").text(obj.RetirementPlanAnswer1);
    }

    if (obj.OInsurancePlan == 0 && obj.SInsurancePlan == "") {
        $("#FinanceInsurancePlanDiv").hide();
    } else {
        $("#OInsurancePlan").text(obj.OInsurancePlan);
        $("#SInsurancePlan").text(obj.SInsurancePlan);
        $("#InsurancePlanAnswer").text(obj.InsurancePlanAnswer);
    }

    if (obj.OInvestmentPlan == 0 && obj.SInvestmentPlan == 0 && obj.SInvestmentPlan1 == 0) {
        $("#InvestmentPlanDiv").hide();
    } else {
        $("#OInvestmentPlan").text(obj.OInvestmentPlan);
        $("#SInvestmentPlan").text(obj.SInvestmentPlan);
        $("#InvestmentPlanAnswer").text(obj.InvestmentPlanAnswer);
        $("#SInvestmentPlan1").text(obj.SInvestmentPlan1);
        $("#InvestmentPlanAnswer1").text(obj.InvestmentPlanAnswer1);
    }

    if (obj.OTaxPlan == 0 && obj.STaxPlan == 0) {
        $("#TaxPlanDiv").hide();
    } else {
        $("#OTaxPlan").text(obj.OTaxPlan);
        $("#STaxPlan").text(obj.STaxPlan);
        $("#TaxPlanAnswer").text(obj.TaxPlanAnswer);
    }

    if (obj.DistributionOfProperty == 0 && obj.DistributionOfProperty1 == 0) {
        $("#DistributionOfPropertyDiv").hide();
    } else {
        $("#DistributionOfProperty").text(obj.DistributionOfProperty);
        $("#DistributionOfPropertyAnswer").text(obj.DistributionOfPropertyAnswer);
        $("#DistributionOfProperty1").text(obj.DistributionOfProperty1);
        $("#DistributionOfPropertyAnswer1").text(obj.DistributionOfPropertyAnswer1);
        //   $("#DistributionOfPropertyScoreTotal").text(obj.DistributionOfProperty + obj.DistributionOfProperty1);
    }

    if (obj.OHeritage == 0 && obj.SHeritage == 0 && obj.SHeritage1 == 0) {
        $("#HeritageDiv").hide();
    } else {
        $("#OHeritage").text(obj.OHeritage);
        $("#SHeritage").text(obj.SHeritage);
        $("#HeritageAnswer").text(obj.HeritageAnswer);
        $("#SHeritage1").text(obj.SHeritage1);
        $("#HeritageAnswer1").text(obj.HeritageAnswer1);
    }
}

//设置单个验证信息
function ValidateDiv() {
    var flag = true;
    if ($("#ProposalCustomer").css("display") != "none") {
        if (!VerificationHelper.checkFrom("ProposalCustomer")) {
            //dialogHelper.Error({
            //    content: "保存失败！有主观题评分存在错误！", success: function () {
            //    }
            //});
            flag = false;
            //   return flag;
        }
    }
    if ($("#RiskIndexDiv").css("display") != "none") {
        if (!VerificationHelper.checkFrom("RiskIndexDiv")) {
            //dialogHelper.Error({
            //    content: "保存失败！有主观题评分存在错误！", success: function () {
            //    }
            //});
            flag = false;
            //   return flag;
        }
    }
    if ($("#FinanceLiabilityDiv").css("display") != "none") {
        if (!VerificationHelper.checkFrom("FinanceLiabilityDiv")) {
            //dialogHelper.Error({
            //    content: "保存失败！有主观题评分存在错误！", success: function () {
            //    }
            //});
            flag = false;
            //return flag;
        }
    }
    if ($("#FinanceIncomeAndExpensesDiv").css("display") != "none") {
        if (!VerificationHelper.checkFrom("FinanceIncomeAndExpensesDiv")) {
            //dialogHelper.Error({
            //    content: "保存失败！有主观题评分存在错误！", success: function () {
            //    }
            //});
            flag = false;
            // return flag;
        }

    }
    if ($("#CashFlow").css("display") != "none") {
        if (!VerificationHelper.checkFrom("CashFlow")) {
            //dialogHelper.Error({
            //    content: "保存失败！有主观题评分存在错误！", success: function () {
            //    }
            //});
            flag = false;
            //   return flag;
        }
    }
    if ($("#FinancialRatiosDiv").css("display") != "none") {
        if (!VerificationHelper.checkFrom("FinancialRatiosDiv")) {
            //dialogHelper.Error({
            //    content: "保存失败！有主观题评分存在错误！", success: function () {
            //    }
            //});
            flag = false;
            // return flag;
        }
    }
    if ($("#FinanceCashPlanDiv").css("display") != "none") {
        if (!VerificationHelper.checkFrom("FinanceCashPlanDiv")) {
            //dialogHelper.Error({
            //    content: "保存失败！有主观题评分存在错误！", success: function () {
            //    }
            //});
            flag = false;
            //  return flag;
        }
    }
    if ($("#LifeEducationPlanDiv").css("display") != "none") {
        if (!VerificationHelper.checkFrom("LifeEducationPlanDiv")) {
            flag = false;
        }
    }
    if ($("#ConsumptionPlanDiv").css("display") != "none") {
        if (!VerificationHelper.checkFrom("ConsumptionPlanDiv")) {
            //dialogHelper.Error({
            //    content: "保存失败！有主观题评分存在错误！", success: function () {
            //    }
            //});
            flag = false;
            //return flag;
        }
    }
    if ($("#StartAnUndertakingPlanDiv").css("display") != "none") {
        if (!VerificationHelper.checkFrom("StartAnUndertakingPlanDiv")) {
            //dialogHelper.Error({
            //    content: "保存失败！有主观题评分存在错误！", success: function () {
            //    }
            //});
            flag = false;
            //  return flag;
        }
    }
    if ($("#LiveRetirementPlanDiv").css("display") != "none") {
        if (!VerificationHelper.checkFrom("LiveRetirementPlanDiv")) {
            //dialogHelper.Error({
            //    content: "保存失败！有主观题评分存在错误！", success: function () {
            //    }
            //});
            flag = false;
            // return flag;
        }
    }
    if ($("#LifeEducationPlanDiv").css("display") != "none") {
        if (!VerificationHelper.checkFrom("LifeEducationPlanDiv")) {
            flag = false;
        }
    }
    if ($("#ConsumptionPlanDiv").css("display") != "none") { //消费规划特殊处理


        if ($("#ShopHouseDiv").css("display") != "none") {
            if (!VerificationHelper.checkFrom("ShopHouseDiv")) {
                flag = false;
                return flag;
            }
        };
        if ($("#ShopCarDiv").css("display") != "none") {
            if (!VerificationHelper.checkFrom("ShopCarDiv")) {
                flag = false;
                return flag;
            }
        };
    }
    if ($("#StartAnUndertakingPlanDiv").css("display") != "none") {
        if (!VerificationHelper.checkFrom("StartAnUndertakingPlanDiv")) {
            //dialogHelper.Error({
            //    content: "保存失败！有主观题评分存在错误！", success: function () {
            //    }
            //});
            flag = false;
            // return flag;
        }
    }
    if ($("#LiveRetirementPlanDiv").css("display") != "none") {
        if (!VerificationHelper.checkFrom("LiveRetirementPlanDiv")) {
            //dialogHelper.Error({ ///////////////////////////////////
            //    content: "保存失败！有主观题评分存在错误！", success: function () {                             
            //    }
            //});
            flag = false;
            //  return flag;
        }
    }
    if ($("#FinanceInsurancePlanDiv").css("display") != "none") {
        if (!VerificationHelper.checkFrom("FinanceInsurancePlanDiv")) {
            //dialogHelper.Error({
            //    content: "保存失败！有主观题评分存在错误！", success: function () {
            //    }
            //});
            flag = false;
            // return flag;
        }
    }
    if ($("#LiveRetirementPlanDiv").css("display") != "none") {
        if (!VerificationHelper.checkFrom("LiveRetirementPlanDiv")) {
            //dialogHelper.Error({
            //    content: "保存失败！有主观题评分存在错误！", success: function () {
            //    }
            //});
            flag = false;
            //  return flag;
        }
    }
    if ($("#FinanceInsurancePlanDiv").css("display") != "none") {
        if (!VerificationHelper.checkFrom("FinanceInsurancePlanDiv")) {
            flag = false;
            //   return flag;
        }
    }

    if ($("#InvestmentPlanDiv").css("display") != "none") {
        if (!VerificationHelper.checkFrom("InvestmentPlanDiv")) {
            //dialogHelper.Error({
            //    content: "保存失败！有主观题评分存在错误！", success: function () {
            //    }
            //});
            flag = false;
            //  return flag;
        }
    }

    if ($("#TaxPlanDiv").css("display") != "none") {
        if (!VerificationHelper.checkFrom("TaxPlanDiv")) {
            //dialogHelper.Error({
            //    content: "保存失败！有主观题评分存在错误！", success: function () {
            //    }
            //});
            flag = false;
            //  return flag;
        }
    }

    if ($("#DistributionOfPropertyDiv").css("display") != "none") {
        if (!VerificationHelper.checkFrom("DistributionOfPropertyDiv")) {
            //dialogHelper.Error({
            //    content: "保存失败！有主观题评分存在错误！", success: function () {
            //    }
            //});
            flag = false;
            //  return flag;
        }
    }

    if ($("#HeritageDiv").css("display") != "none") {
        if (!VerificationHelper.checkFrom("HeritageDiv")) {
            //dialogHelper.Error({
            //    content: "保存失败！有主观题评分存在错误！", success: function () {
            //    }
            //});
            flag = false;
            //  return flag;
        }
    }
    return flag;
}


//加载考核点信息
function LoadExamInfo(TrainExamId, ProposalId) {
    CaseInfo(TrainExamId);
    //获取建议书客户信息及家属信息列表
    if (ProposalId != null && ProposalId != "" && ProposalId != undefined) {
        //客户基本信息
        CustomerInfo(ProposalId);
        //风险测评
        GetRiskEvaluationInfo(ProposalId);
        //资产负债表
        LoadLiabilityByProposalId(ProposalId);
        //收支储蓄表
        GetIncomeAndExpenses(ProposalId);
        //现金流量
        GetCashFlowList(ProposalId)
        //财务比率分析
        GetFinancialRatiosList(ProposalId);
        //现金规划
        GetCashPlanByProposalId(ProposalId);
        //教育规划
        GetLifeEducationPlan(ProposalId);
        JudgesViewEveryMonthMoney("Assessment/GetmoneyList", ProposalId, "LifeEducationPlanDiv");
        //消费规划
        GetConsumptionPlan(ProposalId);
        JudgesViewEveryMonthMoney("Assessment/GetmoneyList", ProposalId, "ConsumptionPlanDiv");
        //创业规划
        GetStartAnUndertakingPlanList(ProposalId);
        JudgesViewEveryMonthMoney("Assessment/GetmoneyList", ProposalId, "StartAnUndertakingPlanDiv");
        //退休规划
        LoadRetirementPlan(ProposalId);
        //保险规划
        LoadInsurancePlan(ProposalId);
        //投资规划
        LoadInvestmentPlan(ProposalId);
        //税收规划
        GetTaxPlan(ProposalId);
        //财务分配
        LoadDistributionOfPropertyInfo(ProposalId);
        //财产传承
        GetHeritage(ProposalId);
    }
}
function CaseInfo(TrainExamId) {
    _ajaxhepler({
        url: "/CompetitionJudges/Assessment/GetExamCase",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            TrainExamId: TrainExamId,
            rId: Math.random(),
        },
        success: function (data) {
            if (data != null) {
                SetCaseInfo(data);//编辑建议书客户信息的设置
            }
        }
    });
}
//设置案例信息
function SetCaseInfo(data) {
    $("#CustomerName").text(data.CustomerName).attr("title", data.CustomerName);
    $("#IDNum").text(data.IDNum);
    $("#FinancialTypeName").text(data.strFinancialType);
    $("#CustomerStory").val(data.CustomerStory);
}

//客户信息显示
function CustomerInfo(ProposalId) {
    _ajaxhepler({
        url: "/CompetitionJudges/Assessment/GetProposalCustomer",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: ProposalId,
            rId: Math.random(),
        },
        success: function (data) {
            if (data != null) {
                SetProposalCustomerInfo(data);//编辑建议书客户信息的设置
            }
        }
    });
}

//设置建议书客户信息
function SetProposalCustomerInfo(data) {
    if (data.ProposalNum != null && data.ProposalNum != "") {
        //建议书
        $("#ProposalNum").text("建议书编号:" + data.ProposalNum);
    }
    $("#ProposalName").text(data.ProposalName);
    if (data.ProposalCustomerVM != null) {
        //客户信息
        $("#CustomerName1").text(data.ProposalCustomerVM.CustomerName);
        if (data.ProposalCustomerVM.PinYin != null && data.ProposalCustomerVM.PinYin != "") {
            $("#PinYin").text(data.ProposalCustomerVM.PinYin);
        }
        $("#Age").text(data.ProposalCustomerVM.Age);
        $("#IDType").text(data.ProposalCustomerVM.IDType);
        $("#IDNum1").text(data.ProposalCustomerVM.IDNum);
        if (data.ProposalCustomerVM.Phone != null && data.ProposalCustomerVM.Phone != "") {
            $("#Phone").text(data.ProposalCustomerVM.Phone);
        }
        if (data.ProposalCustomerVM.Tel != null && data.ProposalCustomerVM.Tel != "") {
            $("#Tel").text(data.ProposalCustomerVM.Tel);
        }
        if (data.ProposalCustomerVM.Email != null && data.ProposalCustomerVM.Email != "") {
            $("#Email").text(data.ProposalCustomerVM.Email);
        }
        if (data.ProposalCustomerVM.Position != null && data.ProposalCustomerVM.Position != "") {
            $("#Position").text(data.ProposalCustomerVM.Position);
        }
        if (data.ProposalCustomerVM.Company != null && data.ProposalCustomerVM.Company != "") {
            $("#Company").text(data.ProposalCustomerVM.Company);
        }
        if (data.ProposalCustomerVM.Address != null && data.ProposalCustomerVM.Address != "") {
            $("#Address").text(data.ProposalCustomerVM.Address);
        }
        //客户亲属列表
        $("#siblist").html("");
        $(data.ProposalCustomerVM.ProposalCustomerDetailList).each(function (index, dom) {
            EditList(dom.DependentName, dom.Age, dom.Relation, dom.InCome);
        });
    }
}
//客户家庭信息
function EditList(DependentName, Age, Relation, InCome) {

    var trHtml = "";
    trHtml += "<div class=\"sib-item\">";
    trHtml += "<div class=\"fif-form\">";
    trHtml += "<div class=\"fif-box\">";
    trHtml += "<label class=\"fif-text\">姓 名</label>";
    trHtml += "<div class=\"input\">{0}</div>";
    trHtml += "</div>";
    trHtml += "<div class=\"fif-box\">";
    trHtml += "<label class=\"fif-text\">年 龄</label>";
    trHtml += "<div class=\"input\">{1}<span class=\"ml10\">岁</span></div>";
    trHtml += "</div>";
    trHtml += "<div class=\"fif-box\">";
    trHtml += "<label class=\"fif-text\">与客户关系</label>";
    trHtml += "<div class=\"input\">{2}</div>";
    trHtml += "</div>";
    trHtml += "<div class=\"fif-box\">";
    trHtml += "<label class=\"fif-text\">年收入</label>";
    trHtml += "<div class=\"input\">{3}</div>";
    trHtml += "</div></div></div>";

    var html = StringHelper.FormatStr(trHtml,
        DependentName,      //0 姓名
        Age,                //1 年龄
        Relation,           //2 与客户关系
        InCome            //3 年收入

        );

    $("#siblist").append(html);
}

//风险测评
function GetRiskEvaluationInfo(ProposalId) {

    _ajaxhepler({
        url: "/CompetitionJudges/Assessment/GetRiskEvaluationInfo",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: ProposalId
        },
        success: function (data) {
            if (data != null) {
                $("#RiskIndexId").text(data.Id);
                $("#ProposalId").val(ProposalId);
                $("#AgeScore").text(data.AgeScore);
                $("#JobScore").text(data.JobScore);
                $("#FamilyScore").text(data.FamilyScore);
                $("#HouseScore").text(data.HouseScore);
                $("#EXPScore").text(data.EXPScore);
                $("#KnowledgeScore").text(data.KnowledgeScore);
                $("#RCIScore").text(data.RCIScore);
                $("#TolerateScore").text(data.TolerateScore);
                $("#ConsiderationScore").text(data.ConsiderationScore);
                $("#LossScore").text(data.LossScore);
                $("#MentalityScore").text(data.MentalityScore);
                $("#CharacterScore").text(data.CharacterScore);
                $("#AvoidScore").text(data.AvoidScore);
                $("#UpdateDate").text(data.UpdateDate);
                $("#RAIScore").text(data.RAIScore);
                ShowInfo(data);
            }
        }
    });
}

//显示评测结果
function ShowInfo(data) {
    $("#EvaluationDate").text(data.UpdateDateStr);
    var RCIScore = data.RCIScore;//风险承受能力 
    var RAIScore = data.RAIScore;//风险容忍态度 
    $("#RiskBearingCapacity").text(RCIScore);
    $("#RiskToleranceAttitude").text(RAIScore);
    var length = ControlTable.length;
    var AbilityMin = 0, AbilityMax = 0, AttitudeMin = 0;
    for (var i = 0; i < length; i++) {
        AbilityMin = ControlTable[i].AbilityMin;
        AbilityMax = ControlTable[i].AbilityMax;
        AttitudeMin = ControlTable[i].AttitudeMin;
        AttitudeMax = ControlTable[i].AttitudeMax;
        if (AbilityMin <= RCIScore && RCIScore <= AbilityMax && AttitudeMin <= RAIScore && RAIScore <= AttitudeMax) {
            $("#DistributionRatio tr:eq(1) td:eq(0)").text(ControlTable[i].Currency);
            $("#DistributionRatio tr:eq(1) td:eq(1)").text(ControlTable[i].Bond);
            $("#DistributionRatio tr:eq(1) td:eq(2)").text(ControlTable[i].Stock);
            $("#RiskBearingCapacity").text(ControlTable[i].Ability);//风险承受能力
            $("#RiskToleranceAttitude").text(ControlTable[i].Attitude);//风险容忍态度 
            ShowPieInfo(ControlTable[i].Currency, ControlTable[i].Bond, ControlTable[i].Stock);
            return true;
        }
    }
}
//测评饼
function ShowPieInfo(Currency, Bond, Stock) {
    var chart;
    $('.distribute').highcharts({
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false
        },
        title: {
            text: '投资分配比例',
            align: 'left'
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        colors: ['#63b2f4', '#2a91e6', '#086cc1'],
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: false
                },
                showInLegend: true
            }
        },
        series: [{
            type: 'pie',
            name: '投资分配比例',
            data: [
                ['货币', Currency],
                {
                    name: '债券',
                    y: Bond,
                    sliced: true,
                    selected: true
                },
                ['股票', Stock],
            ]
        }]
    });
}

//资产负债表------------------------预览加载
function LoadLiabilityByProposalId(ProposalId) {
    _ajaxhepler({
        url: "/CompetitionJudges/Assessment/LoadLiabilityByProposalId",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: ProposalId

        },
        success: function (data) {
            if (data != null) {

                $("#Cash").text(data.Cash);
                $("#RMBDeposit").text(data.RMBDeposit);
                $("#OtherAsset").text(data.OtherAsset);
                $("#RMBFixedDeposit").text(data.RMBFixedDeposit);//----人民币固定存款
                $("#ForeignCurrencyFixedDeposit").text(data.ForeignCurrencyFixedDeposit);
                $("#StockInvestment").text(data.StockInvestment);
                $("#BondInvestment").text(data.BondInvestment);
                $("#FundInvestment").text(data.FundInvestment);
                $("#IndustryInvestment").text(data.IndustryInvestment);
                $("#EstateInvestment").text(data.EstateInvestment);
                $("#PolicyInvestment").text(data.PolicyInvestment);
                $("#OtherInvestment").text(data.OtherInvestment);
                $("#Estate").text(data.Estate);//---------房产
                $("#Car").text(data.Car);
                $("#Others").text(data.Others);
                $("#TotalAssets").text(data.TotalAssets);
                $("#CreditCard").text(data.CreditCard);//-------信用卡借款
                $("#Microfinance").text(data.Microfinance);
                $("#OtherLoan").text(data.OtherLoan);
                $("#FinancialLoan").text(data.FinancialLoan);//-----金融实用借款
                $("#IndustryInvestmentLoan").text(data.IndustryInvestmentLoan);
                $("#EstateInvestmentLoan").text(data.EstateInvestmentLoan);
                $("#OtherInvestmentLoan").text(data.OtherInvestmentLoan);
                $("#EstateLoan").text(data.EstateLoan);//------自用房地产
                $("#CarLoan").text(data.CarLoan);
                $("#OthersLoan").text(data.OthersLoan);
                $("#TotalLoan").text(data.TotalLoan);
                //然后给所有的小计赋值
                var flowAssets = calcFlowAssets(data.Cash, data.RMBDeposit, data.OtherAsset);//流动资产小计
                $("#assetSum01").text(flowAssets.toFixed(2));//流动资产小计
                var invesymentAsset = calcInvestmentAssets(data.RMBFixedDeposit, data.ForeignCurrencyFixedDeposit, data.StockInvestment, data.BondInvestment, data.FundInvestment, data.IndustryInvestment, data.EstateInvestment, data.PolicyInvestment, data.OtherInvestment);//投资资产小计
                $("#assetSum02").text(invesymentAsset.toFixed(2));//投资资产小计
                var selfAsset = calcSelfAsset(data.Estate, data.Car, data.Others);
                $("#assetSum03").text(selfAsset.toFixed(2));//自用资产小计
                var consumeLiability = calcConsumeAssets(data.CreditCard, data.Microfinance, data.OtherLoan);//消费负债
                $("#loanSum01").text(consumeLiability.toFixed(2));//消费负债
                var inverstmentLiability = calcInvestmentLiability(data.FinancialLoan, data.IndustryInvestmentLoan, data.EstateInvestmentLoan, data.OtherInvestmentLoan);
                $("#loanSum02").text(inverstmentLiability.toFixed(2));//投资负债
                var selfLiability = calcSelfLiability(data.EstateLoan, data.CarLoan, data.OthersLoan);
                $("#loanSum03").text(selfLiability.toFixed(2));//自用负债
                //消费净资产
                var consumeVal = flowAssets * 1 - consumeLiability * 1;
                $("#consumeVal").text(consumeVal.toFixed(2));
                //投资净自残
                var investmentVal = invesymentAsset * 1 - inverstmentLiability * 1;
                $("#investmentVal").text(investmentVal.toFixed(2));
                //自用净值
                var selfVal = selfAsset * 1 - selfLiability * 1;
                $("#selfVal").text(selfVal.toFixed(2));
                //净值合计
                var TotalVal = consumeVal * 1 + investmentVal * 1 + selfVal * 1;
                $("#TotalVal").text(TotalVal.toFixed(2))
                //资产合计
                SaveDefaultValueCommon("FinanceLiabilityDiv");
            }
        }
    });
};

//收支储蓄表------------------------预览加载
function GetIncomeAndExpenses(ProposalId) {
    _ajaxhepler({
        url: "/CompetitionJudges/Assessment/LoadIncomeAndExpensesByProposalId",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: ProposalId
        },
        success: function (data) {
            if (data != null) {


                var EndowmentInsurance = data.EndowmentInsurance;
                var HousingFund = data.HousingFund;
                $("#FinanceIncomeAndExpensesDiv #IncomeAndExpensesId").text(data.Id);;//---工作收入小计;
                $("#FinanceIncomeAndExpensesDiv #ProposalId").text(data.ProposalId);;//---工作收入小计;
                $("#FinanceIncomeAndExpensesDiv #JobIncome").text(data.JobIncome);
                $("#FinanceIncomeAndExpensesDiv #EndowmentInsurance").text(EndowmentInsurance);//养老
                $("#FinanceIncomeAndExpensesDiv #MedicalInsurance").text(data.MedicalInsurance);
                $("#FinanceIncomeAndExpensesDiv #HousingFund").text(HousingFund);//住房
                $("#FinanceIncomeAndExpensesDiv #OtherJobIncome").text(data.OtherJobIncome);
                $("#FinanceIncomeAndExpensesDiv #FamilyExpense").text(data.FamilyExpense);;//---2.	生活支出
                $("#FinanceIncomeAndExpensesDiv #ChildExpense").text(data.ChildExpense);
                $("#FinanceIncomeAndExpensesDiv #OtherExpense").text(data.OtherExpense);
                $("#FinanceIncomeAndExpensesDiv #Interest").text(data.Interest);//--3理财收入
                $("#FinanceIncomeAndExpensesDiv #CapitalGains").text(data.CapitalGains);
                $("#FinanceIncomeAndExpensesDiv #OtherIncome").text(data.OtherIncome);
                $("#FinanceIncomeAndExpensesDiv #InterestExpense").text(data.InterestExpense);//理财支出
                $("#FinanceIncomeAndExpensesDiv #InsuranceExpense").text(data.InsuranceExpense);
                $("#FinanceIncomeAndExpensesDiv #OtherFinanceExpense").text(data.OtherFinanceExpense);
                //减：养老保险储蓄 住房公积金储蓄
                $("#FinanceIncomeAndExpensesDiv #EndowmentInsuranceSub").text(data.EndowmentInsurance.toFixed(2));//养老
                $("#FinanceIncomeAndExpensesDiv #HousingFundSub").text(data.HousingFund.toFixed(2));//住房
                //然后给所有的小计赋值
                var WorkIncome = calcWorkIncome(data.JobIncome, data.EndowmentInsurance, data.MedicalInsurance, data.HousingFund, data.OtherJobIncome);//---工作收入小计;
                $("#FinanceIncomeAndExpensesDiv #workIncome01").text(WorkIncome.toFixed(2));//工作收入小计;
                var LiveExpense = calcLiveExpense(data.FamilyExpense, data.ChildExpense, data.OtherExpense);//-2.	生活支出
                $("#FinanceIncomeAndExpensesDiv #liveExpense01").text(LiveExpense.toFixed(2));//	生活支出
                var InvestmentIncome = calcInvestmentIncome(data.Interest, data.CapitalGains, data.OtherIncome);//--3理财收入
                $("#FinanceIncomeAndExpensesDiv #investmentIncome01").text(InvestmentIncome.toFixed(2));///--3理财收入
                var InvestmentExpense = calcInvestmentExpense(data.InterestExpense, data.InsuranceExpense, data.OtherFinanceExpense);//理财支出
                $("#FinanceIncomeAndExpensesDiv #investmentExpense01").text(InvestmentExpense.toFixed(2));//理财支出

                //3.	工作储蓄
                var WolkDeposit01 = WorkIncome * 1 - LiveExpense * 1;
                $("#FinanceIncomeAndExpensesDiv #wolkDeposit01").text(WolkDeposit01);
                //6.	理财储蓄
                var InvestmentDeposit01 = InvestmentIncome * 1 - InvestmentExpense * 1;
                $("#FinanceIncomeAndExpensesDiv #InvestmentDeposit01").text(InvestmentDeposit01);
                //7.	储蓄合计=工作储蓄+理财储蓄
                var TotalDeposit = WolkDeposit01 * 1 + InvestmentDeposit01 * 1;
                $("#FinanceIncomeAndExpensesDiv #TotalDeposit").text(TotalDeposit);
                //9.	自由储蓄=储蓄合计－∑（养老保险储蓄、住房公积金储蓄）
                var FreeMoney = TotalDeposit * 1 - (EndowmentInsurance * 1 + HousingFund * 1);
                $("#FinanceIncomeAndExpensesDiv #FreeMoney").text(FreeMoney)
                //获取原值
                SaveDefaultValueCommon("FinanceIncomeAndExpensesDiv");
            }
        }
    });
}

//获取得到现金流量数据--------------预览加载
function GetCashFlowList(ProposalId) {
    _ajaxhepler({
        url: "/CompetitionJudges/Assessment/GetCashFlowList",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: ProposalId
        },
        success: function (datas) {
            if (datas.list != null || datas.list2 != null) {
                var InsuranceExpense = 0;
                var investMoney = 0;
                var li = datas.list;
                if (li != null) {
                    data = li;
                    //工作收入（薪资收入,养老保险储蓄,医疗保险储蓄,住房公积金储蓄,其他工作收入）
                    var JobIncome = data.JobIncome;
                    var endowmentInsurance = data.EndowmentInsurance;
                    var MedicalInsurance = data.MedicalInsurance;
                    var HousingFund = data.HousingFund;
                    var OtherJobIncome = data.OtherJobIncome;

                    var revenue = JobIncome + endowmentInsurance + MedicalInsurance + HousingFund + OtherJobIncome;
                    //生活支出（家计支出,子女教育支出,其他支出）
                    var FamilyExpense = data.FamilyExpense;
                    var ChildExpense = data.ChildExpense;
                    var OtherExpense = data.OtherExpense;

                    var pay = FamilyExpense + ChildExpense + OtherExpense;
                    //投资收益（利息收入,资本利得,其他理财收入）
                    var Interest = data.Interest;
                    var CapitalGains = data.CapitalGains;
                    var OtherIncome = data.OtherIncome;

                    var investIncome = Interest + CapitalGains + OtherIncome;
                    //利息支出（利息支出）
                    var InterestExpense = data.InterestExpense;
                    //保费支出（保障型保费支出）//保障现金流量净额
                    InsuranceExpense = data.InsuranceExpense;

                    //生活现金流量净额：工作收入-生活支出
                    var lifeMoney = revenue - pay;

                    //投资现金流量净额：投资收益+投资赎回-新增投资
                    investMoney = investIncome + 12 + 12;

                    //借贷现金流量净额: 借入本金-利息支出-还款本金
                    var borrowMoney = 10 - InterestExpense - 10;

                    //本期现金及现金等价物净增加额: ∑（生活现金流量净额，投资现金流量净额，借贷现金流量净额，保障现金流量净额）
                    var Money = lifeMoney + investMoney + borrowMoney - InsuranceExpense;

                    $("#revenue").text(revenue.toFixed(2));
                    $("#pay").text(pay.toFixed(2));
                    $("#investIncome").text(investIncome.toFixed(2));
                    $("#InterestExpenseFlow").text(InterestExpense.toFixed(2));
                    $("#InsuranceExpenseFlow").text(InsuranceExpense.toFixed(2));
                    $("#lifeMoney").text(lifeMoney.toFixed(2));
                    $("#borrowMoney").text(borrowMoney.toFixed(2));
                    $("#Money").text(Money.toFixed(2));
                }

                var li2 = datas.list2;
                if (li2 != null) {
                    var list2 = li2;
                    $("#Redemption").text(list2.Redemption.toFixed(2));
                    $("#Investment").text(list2.Investment.toFixed(2));
                    $("#BorrowCapital").text(list2.BorrowCapital.toFixed(2));
                    $("#RepaymentCapital").text(list2.RepaymentCapital.toFixed(2));
                    $("#investMoney").text(investMoney.toFixed(2));
                    $("#InsuranceExpense2").text(InsuranceExpense.toFixed(2));
                    //隐藏域
                    $("#Id").text(list2.Id);

                    if (li2.JudgeVal == false) {
                        $("#revenue").text(li2.WorkIncome.toFixed(2));                        //工作收入
                        $("#pay").text(li2.LiveExpense.toFixed(2));                      //生活支出
                        $("#investIncome").text(li2.InvestIncome.toFixed(2));                    //投资收益
                        $("#InsuranceExpenseFlow").text(li2.InsuranceExpense.toFixed(2));        //保费支出
                        $("#InterestExpenseFlow").text(li2.InterestExpense.toFixed(2));          //利息支出
                    }


                    //生活现金流量
                    var revenue = $.trim($("#revenue").text()) * 1;
                    var pay = $.trim($("#pay").text()) * 1;
                    var Num = revenue - pay;
                    $("#lifeMoney").text((Num).toFixed(2));

                    //投资现金流量
                    var investIncome = $.trim($("#investIncome").text()) * 1;
                    var Redemption = $.trim($("#Redemption").text()) * 1;
                    var Investment = $.trim($("#Investment").text()) * 1;
                    var Num2 = investIncome + Redemption - Investment;
                    $("#investMoney").text((Num2).toFixed(2));

                    //借贷现金流量
                    var BorrowCapital = $.trim($("#BorrowCapital").text()) * 1;
                    var InterestExpense = $.trim($("#InterestExpenseFlow").text()) * 1;
                    var RepaymentCapital = $.trim($("#RepaymentCapital").text()) * 1;
                    var Num3 = BorrowCapital - InterestExpense - RepaymentCapital;
                    $("#borrowMoney").text((Num3).toFixed(2));

                    //保障现金流量失去焦点时计算               
                    var InsuranceExpense = $.trim($("#InsuranceExpenseFlow").text()) * 1;
                    $("#InsuranceExpense2").text(-InsuranceExpense)



                    //加载本期现金及现金等价物净增加额
                    loadings();
                }

            }
        }
    });
}

//获取得到现金比率分析---------------预览加载
function GetFinancialRatiosList(ProposalId) {

    _ajaxhepler({
        url: "/CompetitionJudges/Assessment/GetFRList",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: ProposalId
        },
        success: function (data) {
            if (data.list3 != null) {
                var li = data.list;
                if (li != null) {
                    NumCalc++;
                    var list = li;
                    //负债比率（负债合计/资产合计）           
                    var bearScale = list.TotalLoan / list.TotalAssets;
                    //融资比率（投资负债小计/投资资产小计）
                    var a = list.RMBFixedDeposit;
                    var b = list.ForeignCurrencyFixedDeposit;
                    var c = list.StockInvestment;
                    var d = list.BondInvestment;
                    var e = list.FundInvestment;
                    var f = list.IndustryInvestment;
                    var g = list.EstateInvestment;
                    var h = list.PolicyInvestment;
                    var i = list.OtherInvestment;
                    var jinancingScale = (list.FinancialLoan + list.IndustryInvestmentLoan + list.EstateInvestmentLoan + list.OtherInvestmentLoan) / (a + b + c + d + e + f + g + h + i)
                    //投资性资产权数（投资资产小计/资产合计）
                    var invest = (a + b + c + d + e + f + g + h + i) / list.TotalAssets;
                    //流动性资产权数（流动资产小计/资产合计）
                    //流动性资产权数（流动资产小计/资产合计）
                    var flowMoney = (list.Cash + list.RMBDeposit + list.OtherAsset) / list.TotalAssets;

                    if ((a + b + c + d + e + f + g + h + i) == 0) {
                        $("#jinancingScale").text("无法统计该指标");
                    } else {
                        $("#jinancingScale").text(jinancingScale.toFixed(2) * 1000 / 10);
                    }

                    if (list.TotalAssets == 0) {
                        $("#invest").text("无法统计该指标");
                        $("#flowMoney").text("无法统计该指标");
                        $("#bearScale").text("无法统计该指标");
                    } else {
                        $("#invest").text(invest.toFixed(2) * 1000 / 10);
                        $("#flowMoney").text(flowMoney.toFixed(2) * 1000 / 10);
                        $("#bearScale").text(bearScale.toFixed(2) * 1000 / 10);
                    }
                }
                var li2 = data.list2;
                if (li2 != null) {
                    var list2 = li2;

                    //支出比率（生活支出小计+理财支出小计）/（工作收入小计+理财收入小计）
                    var licai = list2.InterestExpense + list2.InsuranceExpense + list2.OtherFinanceExpense;
                    var liftpay = (list2.FamilyExpense + list2.ChildExpense + list2.OtherExpense + licai);
                    var work = (list2.JobIncome + list2.EndowmentInsurance + list2.MedicalInsurance + list2.HousingFund + list2.Interest + list2.CapitalGains + list2.OtherIncome);
                    // var payScale = liftpay / work;
                    var payScale = ((list2.LiveExpense01 + list2.InvestmentExpense01) / (list2.WorkIncome01 + list2.InvestmentIncome01)).toFixed(2);
                    //财务负担率：理财支出小计/（工作收入小计+理财收入小计）       
                    //  var finance = licai / work;
                    var finance = (list2.InvestmentExpense01 / (list2.WorkIncome01 + list2.InvestmentIncome01)).toFixed(2);
                    //自由储蓄率：自由储蓄/（工作收入小计+理财收入小计）
                    //	工作储蓄=工作收入－生活支出
                    var workExist = (list2.JobIncome + list2.EndowmentInsurance + list2.MedicalInsurance + list2.HousingFund + list2.Interest) - (list2.FamilyExpense + list2.ChildExpense + list2.OtherExpense)
                    //// 理财储蓄=理财收入－理财支出
                    var licaiExist = (list2.Interest + list2.CapitalGains + list2.OtherIncome) - (licai)
                    ////自由储蓄
                    var freedom = (workExist + licaiExist) - (list2.EndowmentInsurance + list2.HousingFund);
                    //自由储蓄率 ：自由储蓄/（工作收入小计+理财收入小计）
                    // var FreedomScale = freedom / (work);
                    var FreedomScale = (list2.FreeMoney / (list2.WorkIncome01 + list2.InvestmentIncome01)).toFixed(2);
                    // var FreedomScale = Division(list2.FreeMoney/)

                    if (work == 0) {
                        $("#payScale").text("无法统计该指标");
                        $("#finance").text("无法统计该指标");
                        $("#FreedomScale").text("无法统计该指标");
                    } else {
                        $("#payScale").text(payScale * 1000 / 10);

                        $("#finance").text((finance) * 1000 / 10);
                        $("#FreedomScale").text((FreedomScale) * 1000 / 10);
                    }
                }

                if (li != null && li2 != null) {
                    //净资产增长率（致富公式）:(工作储蓄+理财储蓄)/(资产合计-负债合计)
                    var a = (workExist + licaiExist);
                    var b = (list.TotalAssets - list.TotalLoan);
                    if (b == 0) {
                        $("#addScale").text(0);
                    } else {
                        var addScale = a / b;
                        $("#addScale").text(addScale.toFixed(2) * 1000 / 10);
                    }

                }

                var li3 = data.list3;
                if (li3 != null) {
                    var n = li3;
                    var LiabilityAnalysis = n.LiabilityAnalysis == null ? "" : n.LiabilityAnalysis
                    var IncomeAndExpensesAnalysis = n.IncomeAndExpensesAnalysis == null ? "" : n.IncomeAndExpensesAnalysis
                    //资产负债结构分析
                    $("#LiabilityAnalysis").text(LiabilityAnalysis);
                    //收支储蓄结构分析
                    $("#IncomeAndExpensesAnalysis").text(IncomeAndExpensesAnalysis);
                    //客户财务情况分析
                    $("#AnalysisRate").text(n.Analysis);
                }
            } else {

            }
        }
    });

}

//获取现金规划页面------------------预览加载
function GetCashPlanByProposalId(proposalId) {
    _ajaxhepler({
        url: "/CompetitionJudges/Assessment/GetCashPlanByProposalId",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            proposalId: proposalId
        },
        success: function (data) {
            if (data.Analysis != null) {
                var Id = data.Id;
                var FamilyMonthExpense = data.FamilyMonthExpense;
                var RetainCashType = data.RetainCashType;
                var Deposit = data.Deposit;
                var Fund = data.Fund;
                var CreditCard = data.CreditCard;
                var Analysis = data.Analysis;
                $("#CashPlanId").text(Id);
                $("#FamilyMonthExpense").attr("Family", FamilyMonthExpense).text(FamilyMonthExpense);
                //$("#RetainCashType").find("option[value='" + RetainCashType + "']").attr("selected", true);
                var CashRate = EnumConvert.CashConvert(RetainCashType);
                $("#RetainCashType").text(CashRate);
                var retainCashVal = clacCashPanVal.calcRetainCashType(RetainCashType);
                $("#RetainCashMultiple").text(retainCashVal);
                $("#Deposit").text(Deposit);
                $("#Fund").text(Fund);
                $("#CreditCardPlan").text(CreditCard);
                $("#Analysis").text(Analysis);
            }
        }
    });
}
//获取教育规划-----------------------预览加载
function GetLifeEducationPlan(proposalId) {
    _ajaxhepler({
        url: "/CompetitionJudges/Assessment/GetLifeEducationPlanList",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: proposalId
        },
        success: function (datas) {
            if (datas != null) {
                //教育规划信息
                //var data =new Object(datas)
                var li = datas.list;
                var LifeEducationPlanDetailList = null;
                if (li != null) {
                    LifeEducationPlanDetailList = li.LifeEducationPlanDetailList;
                    var n = li;
                    $("#ChildAge").text(n.ChildAge);//子女年龄
                    $("#InlandEduFee").text(n.InlandEduFee);//国内学费增长率
                    $("#ForeignEduFee").text(n.ForeignEduFee);//国外学费增长率
                    $("#Insurance").text(n.Insurance);//商业保险
                    $("#DepositEducation").text(n.Deposit.toFixed(2));//储蓄计划
                    $("#Other").text(n.Other.toFixed(2));//其他安排
                    $("#EduTotalAmount").text(n.EduTotalAmount.toFixed(2));//上学前需准备的现金总金额
                    $("#ReturnOnInvestment").text(n.ReturnOnInvestment.toFixed(2));//预计投资收益率
                    $("#DisposableInput").text(n.DisposableInput.toFixed(2));//一次性投入金额
                    $("#MonthlyInvestment").text(n.MonthlyInvestment.toFixed(2));//每月定期投资金额
                    $("#RegularYear").text(n.RegularYear);//定期定额投资年限
                    $("#TargetAmount").text(n.TargetAmount.toFixed(2));//此方案能实现的目标金额
                    $("#AnalysisLife").text(n.Analysis);//教育规划分析
                    //小计
                    var xiao = n.Insurance + n.Deposit + n.Other;
                    $("#xiaoji").text(xiao.toFixed(2));
                }

                //教育规划详细信息

                if (LifeEducationPlanDetailList != null) {
                    var num = 0;
                    var obj = new Object(LifeEducationPlanDetailList);
                    $.each(obj, function (i, n) {
                        //先生成后赋值
                        num = i + 1;
                        $("#EducationList").remove();
                        AddHTML(num)
                        var educationLife = EnumConvert.LifeConvet(n.EduStage);
                        $("#EducationList" + num).find("span[field='EduStage']").text(educationLife);//教育阶段
                        $("#EducationList" + num).find("span[field='EduAge']").text(n.EduAge);//求学年龄
                        $("#EducationList" + num).find("span[field='EduTime']").text(n.EduTime);//求学时间
                        $("#EducationList" + num).find("span[field='Tuition']").text(n.Tuition.toFixed(2));//目前学费
                        $("#EducationList" + num).find("span[field='EduTuition']").text(n.EduTuition.toFixed(2));//上学时学费
                        $("#EducationList" + num).find("span[field='TotalTuition']").text(n.TotalTuition.toFixed(2));//上学前需准备的总学费  
                        $("#EducationList" + num).find("input[field='ID']").text(n.Id);

                    });
                }
                //每月可支配资金
                //可用资产
            }
        }
    });
}

//获取消费规划相关信息--------------预览加载
function GetConsumptionPlan(proposalId) {

    _ajaxhepler({
        url: "/CompetitionJudges/Assessment/GetConsumptionPlanObj",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: proposalId
        },
        success: function (datas) {
            if (datas != null) {
                //教育规划信息
                var li = datas.list;

                var num = 0;
                var num2 = 0;
                if (li != null) {
                    var n = li;
                    //购房
                    if (n.ShopHouseYear != 0) {
                        $("#ShopHouseYear").text(n.ShopHouseYear);//年限
                        n.HouseArea != null ? $("#HouseArea").text(n.HouseArea) : $("#HouseArea").text("");//面积
                        n.HousePrice != null ? $("#HousePrice").text(n.HousePrice) : $("#HousePrice").text("");//单价
                        $("#HouseDownPaymentPercent").text(n.HouseDownPaymentPercent);//首付比例
                        $("#HouseLoanYear").text(n.HouseLoanYear);//贷款年限
                        $("#HouseLoanRate").text(n.HouseLoanRate);//贷款利率
                        $("#HouseDownPayment").text(n.HouseDownPayment);//首付款
                        $("#HouseTotalAmount").text(n.HouseTotalAmount);//购房总花费
                        $("#HouseMonthlyAmount").text(n.HouseMonthlyAmount);//购房月供

                        //var HouseArea = $.trim($("#HouseArea").text()) * 1;
                        //var HousePrice = $.trim($("#HousePrice").text()) * 1;
                        //var Num2 = HouseArea * HousePrice;
                        //总金额
                        $("#HouseAllMoney").text(n.HouseAllMoney);

                        num = num + n.HouseDownPayment;

                        var HouseArea = $.trim($("#HouseArea").html()) * 1;
                        var HousePrice = $.trim($("#HousePrice").html()) * 1;
                        var Num2 = (HouseArea * HousePrice).toFixed(2);
                        //总金额
                        $("#HouseAllMoney").text(n.HouseAllMoney == null ? (Num2) : n.HouseAllMoney);

                    } else {
                        //$("#TitleShopHouse").hide(0);
                        //$("#ShopHouseDiv").hide(0);
                        // $("#Add").show(0);
                    }

                    //购车
                    if (n.ShopCarYear != 0) {
                        $("#ShopCarYear").text(n.ShopCarYear);//年限
                        if (n.CarType != null) {
                            $("#CarType").text(n.CarType);//车款型号
                        } else {
                            $("#CarType").text("");//车款型号
                        }
                        $("#CarPrice").text(n.CarPrice);//裸车价格
                        $("#CarDownPaymentPercent").text(n.CarDownPaymentPercent);//首付比例

                        $("#CarLoanYear").text(n.CarLoanYear);//贷款期限

                        $("#CarDownPaymentPercent").attr("disabled", false);

                        $("#CarLoanRate").text(n.CarLoanRate);//贷款利率
                        $("#PurchaseTax").text(n.PurchaseTax);//购置税
                        $("#CarRegFee").text(n.CarRegFee);//上牌费用
                        // $("#Displacement").text(n.Displacement);//汽车排量
                        $("#VehicleAndVesselTax").text(n.VehicleAndVesselTax);//车船使用税
                        $("#Selcts").text(n.VehicleAndVesselTax);
                        $("#MotorVehicleCompulsory").text(n.MotorVehicleCompulsory);//交强险
                        $("#Selects2").text(n.MotorVehicleCompulsory);
                        $("#MotorVehicleCommercial").text(n.MotorVehicleCommercial);//商业保险
                        $("#CarDownPayment").text(n.CarDownPayment);//首付款
                        $("#CarTotalAmount").text(n.CarTotalAmount);//购车总花费
                        $("#CarMonthlyAmount").text(n.CarMonthlyAmount);//购车月供

                        num2 = num2 + n.CarDownPayment;
                    } else {
                        // $("#TitleShopCar").hide();
                        // $("#ShopCarDiv").hide();
                        // $("#Add").show();
                    }

                    $("#AnalysisConsumption").text(n.Analysis);//消费规划分析

                    var num3 = 0;
                    if (!$("#ShopCarDiv").is(":hidden")) {
                        num3 = num3 + num2;
                    }
                    if (!$("#ShopHouseDiv").is(":hidden")) {
                        num3 = num3 + num;
                    }

                    $("#FirstAmount").text(num3);//首付款准备的现金总金额 n.FirstAmount

                    $("#ReturnOnInvestmentConsumption").text(n.ReturnOnInvestment);//预计投资收益率
                    $("#DisposableInputConsumption").text(n.DisposableInput);//一次性投入金额
                    $("#MonthlyInvestmentConsumption").text(n.MonthlyInvestment);//每月定期投资金额
                    $("#RegularYearConsumption").text(n.RegularYear);//定期定额投资年限
                    $("#TargetAmountConsumption").text(n.TargetAmount);//此方案能实现的目标金额

                    //隐藏域Id
                    $("#Id").text(n.Id);

                }
            }
        }
    });
}

//获取创业规划相关信息---------------预览加载
function GetStartAnUndertakingPlanList(proposalId) {
    _ajaxhepler({
        url: "/CompetitionJudges/Assessment/GetSUPList",
        type: "POST",
        async: false,
        dataType: "json",
        data: {
            ProposalId: proposalId
        },
        success: function (data) {
            if (data != null) {
                //创业规划信息
                var li = data.list;
                if (li != null) {
                    $("#AgeUndertaking").text(li.Age);//当前年龄

                    $("#StartPlanAge").text(li.StartPlanAge);//计划创业年龄
                    $("#CostInput").text(li.CostInput.toFixed(2));//创业时一次性投入
                    $("#DistanceYear").text(li.DistanceYear);//离创业年限
                    $("#ReturnOnInvestmentRateUndertaking").text(li.ReturnOnInvestmentRate);//预计投资收益率
                    $("#DisposableInputUndertaking").text(li.DisposableInput.toFixed(2));//一次性投入金额
                    $("#MonthlyInvestmentUndertaking").text(li.MonthlyInvestment.toFixed(2));//每月定期投资金额
                    $("#RegularYearUndertaking").text(li.RegularYear);//定期定额投资年限
                    $("#TargetAmountUndertaking").text(li.TargetAmount.toFixed(2));//此方案能实现的目标金额
                    $("#AnalysisUndertaking").text(li.Analysis);//创业规划分析
                    $("#Id").text(li.Id)
                }
                //客户信息
                var li2 = data.list2;
                if (li2 != null) {
                    var n = li2;
                    $("#Age").text(n.Age);//当前年龄                
                }
            }
        }
    });
}

//获取退休规划---------------------预览加载
function LoadRetirementPlan(proposalId) {
    _ajaxhepler({
        url: "/CompetitionJudges/Assessment/GetRetirementPlanByProposalId",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: proposalId
        },
        success: function (data) {
            if (data != null) {
                if (data.Analysis != null) {
                    var Id = data.Id;
                    var Age = data.Age;
                    var BeforeInflationRate = data.BeforeInflationRate;
                    var AfterInflationRate = data.AfterInflationRate;
                    var RetirementRate = data.RetirementRate;
                    var SociaSecurityRate = data.SociaSecurityRate;
                    var RentRate = data.RentRate;
                    var OtherRate = data.OtherRate;

                    var SocialInsurance = data.SocialInsurance;
                    var RetirementAge = data.RetirementAge;
                    var RetirementYears = data.RetirementYears;
                    var LivingStandardNow = data.LivingStandardNow;
                    var Satisfaction = data.Satisfaction;
                    var SatisfactionLivingStandard = data.SatisfactionLivingStandard;
                    var ConvertProportion = data.ConvertProportion;
                    var lineageFee = data.lineageFee;
                    var CommercialInsurance = data.CommercialInsurance;
                    var RentIncome = data.RentIncome;
                    var RetirementLivingStandard = data.RetirementLivingStandard;
                    var AfterLivingStandard = data.AfterLivingStandard;
                    var OtherIncome = data.OtherIncome;
                    var TotalIncome = data.TotalIncome;
                    var TotalAmount = data.TotalAmount;
                    var ReturnOnInvestmentRate = data.ReturnOnInvestmentRate;
                    var MonthlyInvestment = data.MonthlyInvestment;
                    var DisposableInput = data.DisposableInput;
                    var RegularYear = data.RegularYear;
                    var TargetAmount = data.TargetAmount;
                    var Analysis = data.Analysis;
                    //每月可支配资金
                    var MonthMoney = data.MonthMoney;
                    //可用资产
                    var UserableAsset = data.UserableAsset;
                } else {
                    var Age = data.Age == 0 ? "" : data.Age;
                    var BeforeInflationRate = data.BeforeInflationRate == 0 ? "" : data.BeforeInflationRate;
                    var AfterInflationRate = data.AfterInflationRate == 0 ? "" : data.AfterInflationRate;
                    var RetirementRate = data.RetirementRate == 0 ? "" : data.RetirementRate;
                    var SociaSecurityRate = data.SociaSecurityRate == 0 ? "" : data.SociaSecurityRate;
                    var RentRate = data.RentRate == 0 ? "" : data.RentRate;
                    var OtherRate = data.OtherRate == 0 ? "" : data.OtherRate;
                    var SocialInsurance = data.SocialInsurance == 0 ? "" : data.SocialInsurance;
                    var RetirementAge = data.RetirementAge == 0 ? "" : data.RetirementAge;
                    var RetirementYears = data.RetirementYears == 0 ? "" : data.RetirementYears;
                    var LivingStandardNow = data.LivingStandardNow == 0 ? "" : data.LivingStandardNow;
                    var Satisfaction = data.Satisfaction == 0 ? "" : data.Satisfaction;  //*生活满意度
                    var SatisfactionLivingStandard = data.SatisfactionLivingStandard == 0 ? "" : data.AgeSatisfactionLivingStandard
                    var ConvertProportion = data.ConvertProportion == 0 ? "" : data.ConvertProportion;//退休后、退休前生活水平折算比例
                    var lineageFee = data.lineageFee == 0 ? "" : data.lineageFee;
                    var CommercialInsurance = data.CommercialInsurance == 0 ? "" : data.CommercialInsurance;
                    var RentIncome = data.RentIncome == 0 ? "" : data.RentIncome;
                    var RetirementLivingStandard = data.RetirementLivingStandard == 0 ? "" : data.RetirementLivingStandard;
                    var AfterLivingStandard = data.AfterLivingStandard == 0 ? "" : data.AfterLivingStandard;
                    var OtherIncome = data.OtherIncome == 0 ? "" : data.OtherIncome;
                    var TotalIncome = data.TotalIncome == 0 ? "" : data.TotalIncome;
                    var TotalAmount = data.TotalAmount == 0 ? "" : data.AgeTotalAmount
                    var ReturnOnInvestmentRate = data.ReturnOnInvestmentRate == 0 ? "" : data.ReturnOnInvestmentRate;
                    var MonthlyInvestment = data.MonthlyInvestment == 0 ? "" : data.MonthlyInvestment;
                    var DisposableInput = data.DisposableInput == 0 ? "" : data.DisposableInput;
                    var RegularYear = data.RegularYear == 0 ? "" : data.RegularYear;
                    var TargetAmount = data.TargetAmount == 0 ? "" : data.TargetAmount;
                    var Analysis = data.Analysis;
                    //每月可支配资金
                    var MonthMoney = data.MonthMoney == 0 ? "" : data.MonthMoney.toFixed(2);
                    //可用资产
                    var UserableAsset = data.UserableAsset == 0 ? "" : data.UserableAsset.toFixed(2);
                    //获取URL参数
                }
                $("#LiveRetirementPlanDiv #AgeRetirement").text(Age);
                $("#LiveRetirementPlanDiv #BeforeInflationRate").text(BeforeInflationRate);
                $("#LiveRetirementPlanDiv #AfterInflationRate").text(AfterInflationRate);
                $("#LiveRetirementPlanDiv #RetirementRate").text(RetirementRate);

                if (SociaSecurityRate == null) {
                    $("#LiveRetirementPlanDiv #SociaSecurityRate").text("");
                } else {
                    $("#LiveRetirementPlanDiv #SociaSecurityRate").text(SociaSecurityRate);
                }
                if (RentRate == null) {
                    $("#LiveRetirementPlanDiv #RentRate").text("");
                } else {
                    $("#LiveRetirementPlanDiv #RentRate").text(RentRate);
                }
                if (OtherRate == null) {
                    $("#LiveRetirementPlanDiv #OtherRate").text("");
                } else {
                    $("#LiveRetirementPlanDiv #OtherRate").text(OtherRate);
                }



                $("#LiveRetirementPlanDiv #RetirementAge").text(RetirementAge);
                $("#LiveRetirementPlanDiv #RetirementYears").text(RetirementYears);
                $("#LiveRetirementPlanDiv #LivingStandardNow").text(LivingStandardNow);
                $("#LiveRetirementPlanDiv #Satisfaction").text(Satisfaction);
                $("#LiveRetirementPlanDiv #SatisfactionLivingStandard").text(SatisfactionLivingStandard);
                $("#LiveRetirementPlanDiv #ConvertProportion").text(ConvertProportion);
                $("#LiveRetirementPlanDiv #lineageFee").text(lineageFee);
                $("#LiveRetirementPlanDiv #SocialInsurance").text(SocialInsurance);
                $("#LiveRetirementPlanDiv #CommercialInsurance").text(CommercialInsurance);
                $("#LiveRetirementPlanDiv #RentIncome").text(RentIncome);
                $("#LiveRetirementPlanDiv #RetirementLivingStandard").text(RetirementLivingStandard);
                $("#LiveRetirementPlanDiv #AfterLivingStandard").text(AfterLivingStandard);
                $("#LiveRetirementPlanDiv #OtherIncome").text(OtherIncome);
                $("#LiveRetirementPlanDiv #TotalIncome").text(TotalIncome);
                $("#LiveRetirementPlanDiv #TotalAmountRetirement").text(TotalAmount);
                $("#LiveRetirementPlanDiv #ReturnOnInvestmentRateRetirement").text(ReturnOnInvestmentRate);
                $("#LiveRetirementPlanDiv #DisposableInputRetirement").text(DisposableInput);
                $("#LiveRetirementPlanDiv #MonthlyInvestmentRetirement").text(MonthlyInvestment);
                $("#LiveRetirementPlanDiv #RegularYearRetirement").text(RegularYear);
                $("#LiveRetirementPlanDiv #TargetAmountRetirement").text(TargetAmount);
                if (Analysis != null) {
                    $("#LiveRetirementPlanDiv #AnalysisRetirement").text(Analysis);
                }
                $("#LiveRetirementPlanDiv #monthMoneyRetirement").text(MonthMoney);
                $("#LiveRetirementPlanDiv #UserableAssetRetirement").text(UserableAsset)

            }
        }
    });
};

//获取保险规划--------------------预览加载
function LoadInsurancePlan(proposalId) {
    _ajaxhepler({
        url: "/CompetitionJudges/Assessment/LoadInsurancePlanByProposalId",
        type: "POST",
        async: false,
        dataType: "json",
        data: {
            ProposalId: proposalId,
            rId: Math.random
        },
        success: function (data) {
            if (data != null) {
                var Id = data.Id;
                var ProposalId = data.ProposalId
                var MethodTypeId = data.MethodTypeId;
                $("#FinanceInsurancePlanDiv #MethodTypeId").val(MethodTypeId);
                if (MethodTypeId == 1) {
                    $("#FinanceInsurancePlanDiv #InsuranceOne").css("display", "block");
                    $("#FinanceInsurancePlanDiv #InsuranceTwo").css("display", "none");
                } else {
                    $("#FinanceInsurancePlanDiv #InsuranceOne").hide()//.css("display","none");
                    $("#FinanceInsurancePlanDiv #InsuranceTwo").show()//.css("display","block");
                }
                if (data.Analysis != null) {
                    var InsureName = data.InsureName == null ? "" : data.InsureName;//被保险人名
                    var SpouseName = data.SpouseName == null ? "" : data.SpouseName;//客户名
                    var Age = data.Age;
                    var MonthMoney = data.MonthMoney;//每月可支配金额
                    var UserableAsset = data.UserableAsset//可用资产
                    var Age1 = data.Age1;
                    var Age2 = data.Age2;
                    var RetirementAge1 = data.RetirementAge1
                    var RetirementAge2 = data.RetirementAge2
                    var ReturnOnInvestment1 = data.ReturnOnInvestment1
                    var ReturnOnInvestment2 = data.ReturnOnInvestment2
                    var InflationRate1 = data.InflationRate1
                    var InflationRate2 = data.InflationRate2
                    var RevenueGrowth1 = data.RevenueGrowth1
                    var RevenueGrowth2 = data.RevenueGrowth2
                    var FamilyExpensesPay1 = data.FamilyExpensesPay1
                    var FamilyExpensesPay2 = data.FamilyExpensesPay2
                    var FamilyIncomePay1 = data.FamilyIncomePay1
                    var FamilyIncomePay2 = data.FamilyIncomePay2
                    var SpouseAge1 = data.SpouseAge1
                    var SpouseAge2 = data.SpouseAge2
                    var FamilyFutureSaving1 = data.FamilyFutureSaving1
                    var FamilyFutureSaving2 = data.FamilyFutureSaving2
                    var MatrimonialFee1 = data.MatrimonialFee1
                    var MatrimonialFee2 = data.MatrimonialFee2
                    var AfterAccidentRate1 = data.AfterAccidentRate1
                    var AfterAccidentRate2 = data.AfterAccidentRate2
                    var AdjustMatrimonialFee1 = data.AdjustMatrimonialFee1
                    var AdjustMatrimonialFee2 = data.AdjustMatrimonialFee2
                    var MatrimonialFeeNow1 = data.MatrimonialFeeNow1
                    var MatrimonialFeeNow2 = data.MatrimonialFeeNow2
                    var Income1 = data.Income1
                    var Income2 = data.Income2
                    var SpouseMonthIncome1 = data.SpouseMonthIncome1
                    var SpouseMonthIncome2 = data.SpouseMonthIncome2
                    var FamilyLiveOverdraft1 = data.FamilyLiveOverdraft1
                    var FamilyLiveOverdraft2 = data.FamilyLiveOverdraft2
                    var ReserveFund1 = data.ReserveFund1;
                    var ReserveFund2 = data.ReserveFund2;
                    var EduAmount1 = data.EduAmount1;
                    var EduAmount2 = data.EduAmount2;
                    var PensionFunds1 = data.PensionFunds1;
                    var PensionFunds2 = data.PensionFunds2;
                    var DeathExpense1 = data.DeathExpense1
                    var DeathExpense2 = data.DeathExpense2
                    var LoanBalance1 = data.LoanBalance1
                    var LoanBalance2 = data.LoanBalance2
                    var EarningAssets1 = data.EarningAssets1
                    var EarningAssets2 = data.EarningAssets2
                    var RelativeFinancial1 = data.RelativeFinancial1
                    var RelativeFinancial2 = data.RelativeFinancial2
                    var InsureName1 = data.InsureName1
                    var InsureName2 = data.InsureName2
                    var InsureNeedCash1 = data.InsureNeedCash1
                    var InsureNeedCash2 = data.InsureNeedCash2
                    var InsuranceAmount1 = data.InsuranceAmount1
                    var InsuranceAmount2 = data.InsuranceAmount2
                    var GapCash1 = data.GapCash1
                    var GapCash2 = data.GapCash2
                    var BudgetAmount1 = data.BudgetAmount1
                    var BudgetAmount2 = data.BudgetAmount2
                    var SupplementaryQuota1 = data.SupplementaryQuota1
                    var SupplementaryQuota2 = data.SupplementaryQuota2
                    var BalanceCash1 = data.BalanceCash1
                    var BalanceCash2 = data.BalanceCash2
                    var Analysis = data.Analysis;

                    var Expenditure = data.Expenditure;
                    var FutureExpend = data.FutureExpend;//未来给人支出
                    var PredictRetirementAgeLIfe = data.PredictRetirementAgeLIfe;
                    var FutureIncomeLife = data.FutureIncomeLife;//未来个人收入
                    var FutureAnnuityIncome = data.FutureAnnuityIncome;//个人未来净收入的年金现值/元
                } else {
                    var InsureName = data.InsureName == null ? "" : data.InsureName;//被保险人名
                    var SpouseName = data.SpouseName == null ? "" : data.InsureName;//客户名
                    var Age = data.Age==0?"":data.Age;
                    var MonthMoney = data.MonthMoney == 0 ? "" : data.MonthMoney.toFixed(2);//每月可支配金额
                    var UserableAsset = data.UserableAsset == 0 ? "" : data.UserableAsset.toFixed(2); //可用资产
                    var Age1 = data.Age1 == 0 ? "" : data.Age1;
                    var Age2 = data.Age2 == 0 ? "" : data.Age2
                    var RetirementAge1 = data.RetirementAge1 == 0 ? "" : data.RetirementAge1
                    var RetirementAge2 = data.RetirementAge2 == 0 ? "" : data.RetirementAge2
                    var ReturnOnInvestment1 = data.ReturnOnInvestment1 == 0 ? "" : data.ReturnOnInvestment1
                    var ReturnOnInvestment2 = data.ReturnOnInvestment2 == 0 ? "" : data.ReturnOnInvestment2
                    var InflationRate1 = data.InflationRate1 == 0 ? "" : data.InflationRate1
                    var InflationRate2 = data.InflationRate2 == 0 ? "" : data.InflationRate2
                    var RevenueGrowth1 = data.RevenueGrowth1 == 0 ? "" : data.RevenueGrowth1
                    var RevenueGrowth2 = data.RevenueGrowth2 == 0 ? "" : data.RevenueGrowth2
                    var FamilyExpensesPay1 = data.FamilyExpensesPay1 == 0 ? "" : (data.FamilyExpensesPay1*1).toFixed(2)
                    var FamilyExpensesPay2 = data.FamilyExpensesPay2 == 0 ? "" : (data.FamilyExpensesPay2*1).toFixed(2)
                    var FamilyIncomePay1 = data.FamilyIncomePay1 == 0 ? "" : (data.FamilyIncomePay1*1).toFixed(2)
                    var FamilyIncomePay2 = data.FamilyIncomePay2 == 0 ? "" : (data.FamilyIncomePay2 * 1).toFixed(2)
                    var SpouseAge1 = data.SpouseAge1 == 0 ? "" : data.SpouseAge1
                    var SpouseAge2 = data.SpouseAge2 == 0 ? "" : data.SpouseAge2
                    var FamilyFutureSaving1 = data.FamilyFutureSaving1 == 0 ? "" : data.FamilyFutureSaving1
                    var FamilyFutureSaving2 = data.FamilyFutureSaving2 == 0 ? "" : data.FamilyFutureSaving2
                    var MatrimonialFee1 = data.MatrimonialFee1 == 0 ? "" : (data.MatrimonialFee1).toFixed(2)
                    var MatrimonialFee2 = data.MatrimonialFee2 == 0 ? "" : data.MatrimonialFee2.toFixed(2)
                    var AfterAccidentRate1 = data.AfterAccidentRate1 == 0 ? "" : data.AfterAccidentRate1
                    var AfterAccidentRate2 = data.AfterAccidentRate2 == 0 ? "" : data.AfterAccidentRate2
                    var AdjustMatrimonialFee1 = data.AdjustMatrimonialFee1 == 0 ? "" : data.AdjustMatrimonialFee1.toFixed(2);
                    var AdjustMatrimonialFee2 = data.AdjustMatrimonialFee2 == 0 ? "" : data.AdjustMatrimonialFee2.toFixed(2);
                    var MatrimonialFeeNow1 = data.MatrimonialFeeNow1 == 0 ? "" : data.MatrimonialFeeNow1.toFixed(2);
                    var MatrimonialFeeNow2 = data.MatrimonialFeeNow2 == 0 ? "" : data.MatrimonialFeeNow2.toFixed(2);
                    var Income1 = data.Income1 == 0 ? "" : data.Income1.toFixed(2);
                    var Income2 = data.Income2 == 0 ? "" : data.Income2.toFixed(2);
                    var SpouseMonthIncome1 = data.SpouseMonthIncome1 == 0 ? "" : data.SpouseMonthIncome1.toFixed(2);
                    var SpouseMonthIncome2 = data.SpouseMonthIncome2 == 0 ? "" : data.SpouseMonthIncome2.toFixed(2);
                    var FamilyLiveOverdraft1 = data.FamilyLiveOverdraft1 == 0 ? "" : data.FamilyLiveOverdraft1.toFixed(2);
                    var FamilyLiveOverdraft2 = data.FamilyLiveOverdraft2 == 0 ? "" : data.FamilyLiveOverdraft2.toFixed(2);
                    var ReserveFund1 = data.ReserveFund1 == 0 ? "" : data.ReserveFund1.toFixed(2); //紧急备用金现值---数据来源现金规划保留规模
                    //if (data.ReserveFund1 != 0) {
                    //    ReserveFund1 = data.ReserveFund1.toFixed(2);
                    //    $("#ReserveFund1").attr("readonly", "readonly").css("disabled");
                    //}
                    var ReserveFund2 = data.ReserveFund2 == 0 ? "" : data.ReserveFund2.toFixed(2);
                    //if (data.ReserveFund2 != 0) {
                    //    ReserveFund2 = data.ReserveFund2.toFixed(2);
                    //    $("#ReserveFund2").attr("readonly", "readonly").css("disabled");
                    //}
                    var EduAmount1 = data.EduAmount1 == 0 ? "" : data.EduAmount1.toFixed(2); //教育金现值--数据来源教育规划或输
                    //if (data.EduAmount1 != 0) {
                    //    EduAmount1 = data.EduAmount1.toFixed(2);
                    //    $("#EduAmount1").attr("readonly", "readonly").css("disabled");
                    //}
                    var EduAmount2 = data.EduAmount2 == 0 ? "" : data.EduAmount2.toFixed(2);
                    //if (data.EduAmount2 != 0) {
                    //    EduAmount2 = data.EduAmount2.toFixed(2);
                    //    $("#EduAmount2").attr("readonly", "readonly").css("disabled");
                    //}
                    var PensionFunds1 = data.PensionFunds1 == 0 ? "" : data.PensionFunds1.toFixed(2); //养老基金现值/元--数据来源退休规划或输入
                    //if (data.PensionFunds1 != 0) {
                    //    PensionFunds1 = data.PensionFunds1.toFixed(2);
                    //    $("#PensionFunds1").attr("readonly", "readonly").css("disabled");
                    //}
                    var PensionFunds2 = data.PensionFunds2 == 0 ? "" : data.PensionFunds2.toFixed(2);
                    //if (data.PensionFunds2 != 0) {
                    //    PensionFunds2 = data.PensionFunds2.toFixed(2);
                    //    $("#PensionFunds2").attr("readonly", "readonly").css("disabled");
                    //}
                    var DeathExpense1 = data.DeathExpense1 == 0 ? "" : data.DeathExpense1.toFixed(2);
                    var DeathExpense2 = data.DeathExpense2 == 0 ? "" : data.DeathExpense2.toFixed(2);
                    var LoanBalance1 = data.LoanBalance1 == 0 ? "" : data.LoanBalance1.toFixed(2);
                    var LoanBalance2 = data.LoanBalance2 == 0 ? "" : data.LoanBalance2.toFixed(2);
                    var EarningAssets1 = data.EarningAssets1 == 0 ? "" : data.EarningAssets1.toFixed(2);
                    var EarningAssets2 = data.EarningAssets2 == 0 ? "" : data.EarningAssets2.toFixed(2);
                    var RelativeFinancial1 = data.RelativeFinancial1 == 0 ? "" : data.RelativeFinancial1;
                    var RelativeFinancial2 = data.RelativeFinancial2 == 0 ? "" : data.RelativeFinancial2;
                    var InsureName1 = data.InsureName1;
                    var InsureName2 = data.InsureName2;
                    var InsureNeedCash1 = data.InsureNeedCash1 == 0 ? "" : data.InsureNeedCash1;
                    var InsureNeedCash2 = data.InsureNeedCash2 == 0 ? "" : data.InsureNeedCash2;
                    var InsuranceAmount1 = data.InsuranceAmount1 == 0 ? "" : data.InsuranceAmount1;
                    var InsuranceAmount2 = data.InsuranceAmount2 == 0 ? "" : data.InsuranceAmount2;
                    var GapCash1 = data.GapCash1 == 0 ? "" : data.GapCash1;
                    var GapCash2 = data.GapCash2 == 0 ? "" : data.GapCash2;
                    var BudgetAmount1 = data.BudgetAmount1 == 0 ? "" : data.BudgetAmount1;
                    var BudgetAmount2 = data.BudgetAmount2 == 0 ? "" : data.BudgetAmount2;
                    var SupplementaryQuota1 = data.SupplementaryQuota1 == 0 ? "" : data.SupplementaryQuota1;
                    var SupplementaryQuota2 = data.SupplementaryQuota2 == 0 ? "" : data.SupplementaryQuota2;
                    var BalanceCash1 = data.BalanceCash1 == 0 ? "" : data.BalanceCash1;
                    var BalanceCash2 = data.BalanceCash2 == 0 ? "" : data.BalanceCash2;
                    var Analysis = data.Analysis == null ? "" : data.Analysis;
                    var Expenditure = data.Expenditure == 0 ? "" : data.Expenditure;
                    var FutureExpend = data.FutureExpend == 0 ? "" : data.FutureExpend;//未来给人支出
                    var PredictRetirementAgeLIfe = data.PredictRetirementAgeLIfe == 0 ? "" : data.PredictRetirementAgeLIfe;
                    var FutureIncomeLife = data.FutureIncomeLife == 0 ? "" : data.FutureIncomeLife;//未来个人收入
                    var FutureAnnuityIncome = data.FutureAnnuityIncome == 0 ? "" : data.FutureAnnuityIncome;//个人未来净收入的年金现值/元
                }


                //加入枚举
                var methodVal = EnumConvert.InsuraneConvet(MethodTypeId)
                $("#FinanceInsurancePlanDiv #MethodTypeId").text(methodVal);
                $("#FinanceInsurancePlanDiv #TabZH").text(MethodTypeId)
                //判断保险规划的需求算法1-遗属需求法 ，2-生命需求法
                $("#FinanceInsurancePlanDiv #monthMoneyInsurance").text(MonthMoney);//每月可用资金
                $("#FinanceInsurancePlanDiv #UserableAssetInsurance").text(UserableAsset);//可用资产
                $("#FinanceInsurancePlanDiv #AnalysisInsuranceTwo").text(Analysis);//--客户财务情况分析
                if (MethodTypeId == 1) {

                    $("#FinanceInsurancePlanDiv #InsureName").text(InsureName);
                    $("#FinanceInsurancePlanDiv #InsureName1Life").text(InsureName);
                    $("#FinanceInsurancePlanDiv #SpouseName").text(SpouseName);
                    //这地方特殊两边通用
                    $("#FinanceInsurancePlanDiv #Age1").text(Age);
                    $("#FinanceInsurancePlanDiv #SpouseAge2").text(Age);  //---------配偶当前年龄-男的写女的
                    $("#FinanceInsurancePlanDiv #SpouseAge1").text(Age2);
                    $("#FinanceInsurancePlanDiv #Age1Life").text(Age);//被保险人年龄/岁


                    $("#FinanceInsurancePlanDiv #Age2").text(Age2)
                    $("#FinanceInsurancePlanDiv #RetirementAge1").text(RetirementAge1);
                    $("#FinanceInsurancePlanDiv #RetirementAge2").text(RetirementAge2);
                    $("#FinanceInsurancePlanDiv #ReturnOnInvestment1").text(ReturnOnInvestment1);
                    $("#FinanceInsurancePlanDiv #ReturnOnInvestment2").text(ReturnOnInvestment2);//----人民币固定存款
                    $("#FinanceInsurancePlanDiv #InflationRate1").text(InflationRate1);
                    $("#FinanceInsurancePlanDiv #InflationRate2").text(InflationRate2);
                    $("#FinanceInsurancePlanDiv #RevenueGrowth1").text(RevenueGrowth1);
                    $("#FinanceInsurancePlanDiv #RevenueGrowth2").text(RevenueGrowth2);
                    $("#FinanceInsurancePlanDiv #FamilyExpensesPay1").text(FamilyExpensesPay1);//家庭生活费用实质报酬率
                    $("#FinanceInsurancePlanDiv #FamilyExpensesPay2").text(FamilyExpensesPay2);
                    $("#FinanceInsurancePlanDiv #FamilyIncomePay1").text(FamilyIncomePay1);
                    $("#FinanceInsurancePlanDiv #FamilyIncomePay2").text(FamilyIncomePay2);

                    //   $("#FinanceInsurancePlanDiv #SpouseAge2").text(Age2);//女的写男的
                    $("#FinanceInsurancePlanDiv #FamilyFutureSaving1").text(FamilyFutureSaving1);//家庭未来生活费准备年数/年
                    $("#FinanceInsurancePlanDiv #FamilyFutureSaving2").text(FamilyFutureSaving2);
                    $("#FinanceInsurancePlanDiv #MatrimonialFee1").text(MatrimonialFee1);//-------当前的家庭生活费用/元
                    $("#FinanceInsurancePlanDiv #MatrimonialFee2").text(MatrimonialFee2);
                    $("#FinanceInsurancePlanDiv #AfterAccidentRate1").text(AfterAccidentRate1);//-----保险事故发生后支出调整率
                    $("#FinanceInsurancePlanDiv #AfterAccidentRate2").text(AfterAccidentRate2);
                    $("#FinanceInsurancePlanDiv #AdjustMatrimonialFee1").text(AdjustMatrimonialFee1);//调整后家庭年生活费用/元
                    $("#FinanceInsurancePlanDiv #AdjustMatrimonialFee2").text(AdjustMatrimonialFee2);
                    $("#FinanceInsurancePlanDiv #MatrimonialFeeNow1").text(MatrimonialFeeNow1);//------家庭生活费用现值/元
                    $("#FinanceInsurancePlanDiv #MatrimonialFeeNow2").text(MatrimonialFeeNow2);
                    $("#FinanceInsurancePlanDiv #Income1").text(Income1);//配偶的个人年收入/元
                    $("#FinanceInsurancePlanDiv #Income2").text(Income2);
                    $("#FinanceInsurancePlanDiv #SpouseMonthIncome1").text(SpouseMonthIncome1);//配偶的个人收入现值/元
                    $("#FinanceInsurancePlanDiv #SpouseMonthIncome2").text(SpouseMonthIncome2);
                    $("#FinanceInsurancePlanDiv #FamilyLiveOverdraft1").text(FamilyLiveOverdraft1)//家庭未来生活费用缺口现值/元
                    $("#FinanceInsurancePlanDiv #FamilyLiveOverdraft2").text(FamilyLiveOverdraft2);
                    $("#FinanceInsurancePlanDiv #ReserveFund1").text(ReserveFund1);//紧急备用金现值/元
                    $("#FinanceInsurancePlanDiv #ReserveFund2").text(ReserveFund2);
                    $("#FinanceInsurancePlanDiv #EduAmount1").text(EduAmount1);//--教育金现值/元
                    $("#FinanceInsurancePlanDiv #EduAmount2").text(EduAmount2);
                    $("#FinanceInsurancePlanDiv #PensionFunds1").text(PensionFunds1);//养老基金现值/元
                    $("#FinanceInsurancePlanDiv #PensionFunds2").text(PensionFunds2);
                    $("#FinanceInsurancePlanDiv #DeathExpense1").text(DeathExpense1);//临终及丧葬支出现值/元
                    $("#FinanceInsurancePlanDiv #DeathExpense2").text(DeathExpense2);
                    $("#FinanceInsurancePlanDiv #LoanBalance1").text(LoanBalance1);//目前贷款余额/元
                    $("#FinanceInsurancePlanDiv #LoanBalance2").text(LoanBalance2);
                    $("#FinanceInsurancePlanDiv #EarningAssets1").text(EarningAssets1);//家庭生息资产/元
                    $("#FinanceInsurancePlanDiv #EarningAssets2").text(EarningAssets2);
                    $("#FinanceInsurancePlanDiv #RelativeFinancial1").text(RelativeFinancial1);//遗属需求法应有的寿险保额/元
                    $("#FinanceInsurancePlanDiv #RelativeFinancial2").text(RelativeFinancial2);
                    $("#FinanceInsurancePlanDiv #InsureName1").text(InsureName);//-------被保险人
                    $("#FinanceInsurancePlanDiv #InsureName2").text(SpouseName);
                    $("#FinanceInsurancePlanDiv #InsureNeedCash1").text(InsureNeedCash1);//保险需求额度/元	
                    $("#FinanceInsurancePlanDiv #InsureNeedCash2").text(InsureNeedCash2);
                    $("#FinanceInsurancePlanDiv #InsuranceAmount1").text(InsuranceAmount1);//----*已有额度/元
                    $("#FinanceInsurancePlanDiv #InsuranceAmount2").text(InsuranceAmount2);
                    $("#FinanceInsurancePlanDiv #GapCash1").text(GapCash1);//缺口额度/元
                    $("#FinanceInsurancePlanDiv #GapCash2").text(GapCash2);
                    $("#FinanceInsurancePlanDiv #BudgetAmount1").text(BudgetAmount1);//----*预算金额/元
                    $("#FinanceInsurancePlanDiv #BudgetAmount2").text(BudgetAmount2);
                    $("#FinanceInsurancePlanDiv #SupplementaryQuota1").text(SupplementaryQuota1);//*补充额度/元
                    $("#FinanceInsurancePlanDiv #SupplementaryQuota2").text(SupplementaryQuota2);
                    $("#FinanceInsurancePlanDiv #BalanceCash1").text(BalanceCash1);//欠缺额度/元
                    $("#FinanceInsurancePlanDiv #BalanceCash2").text(BalanceCash2);

                    SaveDefaultValueCommon("InsuranceOne");//保存原值。和新值要做一个对比的
                } else {


                    //年龄共用
                    $("#FinanceInsurancePlanDiv #Age1").text(Age);
                    $("#FinanceInsurancePlanDiv #Age1Life").text(Age);//被保险人年龄/岁
                    $("#FinanceInsurancePlanDiv #SpouseAge2").text(Age);  //---------配偶当前年龄-男的写女的

                    $("#FinanceInsurancePlanDiv #RetirementAge1Life").text(RetirementAge1);//*预计退休年龄/岁
                    $("#FinanceInsurancePlanDiv #PredictRetirementAgeLIfe").text(PredictRetirementAgeLIfe);//离退休年数/年
                    $("#FinanceInsurancePlanDiv #ReturnOnInvestment1Life").text(ReturnOnInvestment1);//*投资报酬率
                    $("#FinanceInsurancePlanDiv #Income1Life").text(Income1);//当前个人年收入/元
                    $("#FinanceInsurancePlanDiv #RevenueGrowth1Life").text(RevenueGrowth1);//收入增长率
                    $("#FinanceInsurancePlanDiv #FutureIncomeLife").text(FutureIncomeLife);//未来工作期间收入现值/元
                    $("#FinanceInsurancePlanDiv #Expenditure").text(Expenditure);//-个人年收入支出
                    $("#FinanceInsurancePlanDiv #InflationRate1Life").text(InflationRate1);//年通货膨胀率
                    $("#FinanceInsurancePlanDiv #FutureExpend").text(FutureExpend);//未来工作期间支出现值/元
                    $("#FinanceInsurancePlanDiv #FutureAnnuityIncome").text(FutureAnnuityIncome);//个人未来净收入的年金现值/元
                    $("#FinanceInsurancePlanDiv #FutureAnnuityIncomeSub").text(FutureAnnuityIncome);//弥补收入应有的寿险保额/元
                    //---------------------被保险人

                    $("#FinanceInsurancePlanDiv #InsureNeedCash1Life").text(FutureAnnuityIncome);//保险需求额度/元
                    $("#FinanceInsurancePlanDiv #InsuranceAmount1Life").text(InsuranceAmount1);//已有额度/元
                    $("#FinanceInsurancePlanDiv #GapCash1Life").text(GapCash1);//缺口额度/元
                    $("#FinanceInsurancePlanDiv #BudgetAmount1Life").text(BudgetAmount1);//预算金额/元

                    $("#FinanceInsurancePlanDiv #SupplementaryQuota1Life").text(SupplementaryQuota1);//-补充额度/元
                    $("#FinanceInsurancePlanDiv #BalanceCash1Life").text(BalanceCash1);//欠缺额度/元

                    $("#FinanceInsurancePlanDiv #InsureName").text(InsureName);//被保险人姓名
                    $("#FinanceInsurancePlanDiv #InsureName1Life").text(InsureName);//被保险人姓名
                    $("#FinanceInsurancePlanDiv #InsureName1").text(InsureName);//-------被保险人

                    SaveDefaultValueCommon("InsuranceTwo");//保存原值。和新值要做一个对比的
                }
            }
        }
    });

}

//*********************************************************
//获取投资规划--------------------预览加载
function LoadInvestmentPlan(proposalId) {

    _ajaxhepler({
        url: "/CompetitionJudges/Assessment/LoadInvestmentPlan",
        type: "POST",
        async: false,
        dataType: "json",
        data: {
            ProposalId: proposalId
        },
        success: function (data) {
            if (data != null) {

                analysisData(data);
            } else {

            }
        }

    });
};
//分解data数据
//分解data数据
function analysisData(data) {
    NumCalc++;
    var InvestmentPlanList = data.InvestmentPlanProductList;
    var LifeCycleId = data.LifeCycleId;
    var HoldRate = data.HoldRate * 1;
    var IncreaseRate = data.IncreaseRate * 1;
    var SpeculationRate = data.SpeculationRate * 1;
    var Analysis = data.Analysis;
    var Id = data.Id;
    //  $("#InvestmentPlanId").text(Id);
    var LifeCycleId1 = EnumConvert.InsertmentConvert(LifeCycleId);
    $("#LifeCycleId").text(LifeCycleId1);
    $("#HoldRate").text(HoldRate);
    $("#IncreaseRate").text(IncreaseRate);
    $("#SpeculationRate").text(SpeculationRate);
    $("#AnalysisInsertment").text(Analysis);
    var InsureShow = EnumConvert.InsertmentBaseConvet(LifeCycleId);
    $("#InsureShow").text(InsureShow);

    //赋值饼状图
    ShowInsertmentInfo(HoldRate, IncreaseRate, SpeculationRate)

    if (InvestmentPlanList != null) {
        // $("#Add").prev().remove();
        $("#ProductSelect").remove();// 这样也可以
        $(InvestmentPlanList).each(function (index, dom) {
            var fundobj = new Object(dom);
            index = index + 1;
            //应该先删除第一行然后添加
            //添加
            if (index != InvestmentPlanList.length) {
                AddList(index, fundobj);
            } else {
                AddListLast(index, fundobj);
            }
            //f赋值
            eacTransVal(index, fundobj);
            //给常量赋值
            IndexFlag = index;
        });
    }
}

//添加产品选择
function AddList(index, dom) {
    var trHtml = "";
    if (typeof index == "undefined") {
        IndexFlag = IndexFlag + 1;
        index = IndexFlag;
    }

    trHtml += "<div class=\"item b-grayish Tageach\"  id=\"ProductSelect" + index + "\" eacflag=\"ProductSelect\"><div class=\"fif-con  mb10\">";
    trHtml += "<div class=\"fif-box grid-4\"><label class=\"fif-text\">已完成规划：</label> <div class=\"input\">";
    trHtml += "<span id=\"PlanId" + index + " \" class=\"\" eacflag=\"PlanId\"></span> </div></div>";
    trHtml += "<div class=\"fif-box grid-6\"><label class=\"fif-text\"><i class=\"c-red\">*</i>方案所需投资收益率：</label>";
    trHtml += " <div class=\"input\"> <span id=\"PlanRate" + index + "\" class=\"grid-4\" eacflag=\"PlanRate\"></span></div> </div>";
    trHtml += "<a ></a></div>";
    trHtml += "<div class=\"table\"> <table class=\"mb10\" id=\"ProductSelect" + index + "TabCashFund\" eacflag=\"ProductSelectTabCashFund\" width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
    trHtml += " <tbody> <tr> <td width=\"83\" rowspan=\"2\" align=\"center\"  class=\"c-white\" style=\"background-color: #63b2f4\">保值层</td>";
    trHtml += "<td><span class=\"fl\">银行储蓄选择</span><div class=\"fif-con fr grid-10\"> <div class=\"fif-box grid-12\" style=\"margin-bottom:10px;\">";
    trHtml += " <label class=\"fif-text\">活期：</label>   <div class=\"input grid-11\">";

    //银行选择
    trHtml += "<span id=\"DemandDepositsBank" + index + "\" class=\"\" eacflag=\"DemandDepositsBank\"></span>"

    trHtml += " <span id=\"DemandDepositsBankRate" + index + "\" class=\"\" eacflag=\"DemandDepositsBankRate\"></span>  </div></div>";
    trHtml += "<div class=\"fif-box grid-12\"> <label class=\"fif-text\">定期：</label><div class=\"input grid-11\">";
    //银行选择定期
    trHtml += "<span id=\"TimeDepositBank" + index + "\" class=\"\" eacflag=\"TimeDepositBank\"></span>";
    //银行期限
    trHtml += "<span id=\"TimeDepositBankTime" + index + "\" class=\"\" eacflag=\"TimeDepositBankTime\"></span>";

    trHtml += "<span id=\"TimeDepositBankRate" + index + "\" class=\"\" eacflag=\"TimeDepositBankRate\"></span> </div> </div></div></td> </tr>";
    trHtml += "<tr> <td> <span>基金产品选择</span>  <span class=\"grid-4\">货币市场基金</span> ";
    //货币基金
    if (dom.Fund1 != 0) {
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund1\"> </span> <div class=\"fif-form b-grayish\">  <span class=\"grid-3\" eacflag=\"CashCode\">小龙</span>     <span class=\"grid-4\" eacflag=\"CashFund\">汇添富基金-汇添富社会责任股票</span>   <span class=\"grid-2\" eacflag=\"CashMarket\">货币市场</span>   <span class=\"grid-3\"  eacflag=\"YearlyEarningsRate1\">151.15%</span>  </div>  </div>    </div>";
    } else {
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund1\"></div></div>"
    }
    trHtml += "<input type=\"hidden\" id=\"Fund1" + index + "\" name=\"Fund1" + index + "\" value=\"\" eacflag=\"Fund1\" />  <input type=\"hidden\" eacflag=\"CashCode\" value=\" \" /><input type=\"hidden\" eacflag=\"CashFund\" value=\" \" /> <input type=\"hidden\" eacflag=\"CashMarket\" value=\" \" /><input type=\"hidden\" eacflag=\"YearlyEarningsRate1\" value=\" \" /> </td> </tr> <tr class=\"end\"></tr> </tbody> </table>";
    trHtml += "  <table class=\"mb10\" id=\"ProductSelect" + index + "TabBondFund\" eacflag=\"ProductSelectTabBondFund\" width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">  <tbody> <tr> <td width=\"83\" align=\"center\" class=\"c-white\"  style=\"background-color: #2a91e6\">增值层</td>";
    trHtml += "<td>  <span><i class=\"c-red\">*</i>基金产品选择</span> <span class=\"grid-4\">债券型基金、混合型基金</span> ";
    //债券基金
    if (dom.Fund2 != 0) {
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund2\"><div class=\"fif-form b-grayish\">  <span class=\"grid-3\" eacflag=\"BondCode\">小龙</span>     <span class=\"grid-4\" eacflag=\"BondFund\">汇添富基金-汇添富社会责任股票</span>   <span class=\"grid-2\" eacflag=\"BondMarket\">货币市场</span>   <span class=\"grid-3\"  eacflag=\"YearlyEarningsRate2\">151.15%</span>  </div>  </div>    </div>";
    } else {
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund2\"></div></div>"
    }
    trHtml += "  <input type=\"hidden\" name=\"Fund2\" id=\"Fund2" + index + "\" value=\"\" eacflag=\"Fund2\" class=\"eac\" />  <input type=\"hidden\" eacflag=\"BondCode\" value=\" \" /><input type=\"hidden\" eacflag=\"BondFund\" value=\" \" /> <input type=\"hidden\" eacflag=\"BondMarket\" value=\" \" /><input type=\"hidden\" eacflag=\"YearlyEarningsRate2\" value=\" \" /> </td> </tr> <tr class=\"end\"></tr>  </tbody>  </table>";
    trHtml += "<table class=\"mb10\" id=\"ProductSelect" + index + "TabStockFund\" eacflag=\"ProductSelectTabStockFund\" width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">  <tbody> <tr> <td width=\"83\" rowspan=\"2\" align=\"center\" class=\"c-white\"  style=\"background-color: #086cc1\">投机层</td>";
    //p2p产品
    trHtml += "   <td> <span>P2P产品选择</span> <span class=\"grid-4\">P2P产品</span>  ";
    if (dom.P2PProduct != 0) {
        trHtml += "<div class=\"selected-list sib-list mt10\"><div class=\"sib-item\" field=\"P2PProduct\"><span title=\"\"></span> <div class=\"fif-form b-grayish\"> <span class=\"\" eacflag=\"P2PName\" title=></span> <span class=\"grid-2\" eacflag=\"InvestmentField\" title=></span> <span class=\"grid-2\" eacflag=\"InvestmentCycle\" title=\"\"></span> <span class=\"grid-2\" eacflag=\"StartAmount\" title=></span>  <span class=\"grid-2\" eacflag=\"EarningsRate\" title=\"\"></span>  </div> </div></div>"
    } else {
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\"  field=\"P2PProduct\"></div></div>"
    }
    trHtml += "<input type=\"hidden\" class=\"eac\" id=\"P2PProduct" + index + "\" value=\"0\" eacflag=\"P2PProduct\"><input type=\"hidden\" class=\"eac\" id=\"P2PProductRate" + index + "\" value=\"0\" eacflag=\"P2PProductRate\"></td></tr>";


    trHtml += "<tr> <td>  <span>基金产品选择</span>  <span class=\"grid-4\">股票型基金</span> "
    //股票基金
    if (dom.Fund3 != 0) {
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund3\"> <div class=\"fif-form b-grayish\">  <span class=\"grid-3\" eacflag=\"StockCode\">小龙</span>     <span class=\"grid-4\" eacflag=\"StockFund\">汇添富基金-汇添富社会责任股票</span>   <span class=\"grid-2\" eacflag=\"StockMarket\">货币市场</span>   <span class=\"grid-3\"  eacflag=\"YearlyEarningsRate3\">151.15%</span>  </div>  </div>    </div>";
    } else {
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund3\"></div></div>"
    }
    trHtml += " <input type=\"hidden\" id=\"Fund3" + index + "\" name=\"Fund3'\" value=\"\" eacflag=\"Fund3\" />  <input type=\"hidden\" eacflag=\"StockCode\" value=\" \" /><input type=\"hidden\" eacflag=\"StockFund\" value=\" \" /> <input type=\"hidden\" eacflag=\"StockMarket\" value=\" \" /><input type=\"hidden\" eacflag=\"YearlyEarningsRate3\" value=\" \" /> </td> </tr>    <tr class=\"end\"></tr> </tbody>  </table>   </div>";
    trHtml += "  <div class=\"fif-con \"> <div class=\"fif-box grid-5\"> <label class=\"fif-text\">产品组合预期收益率：</label>";
    trHtml += " <div class=\"input\"><span id=\"TotalRate" + index + "\" class=\"grid-4\" eacflag=\"TotalRate\"></span> <span class=\"ml10\">%</span> </div></div></div></div>"

    if (index == 1) {
        $("#InvestmentPlanParkchild101").append(trHtml);
    }
    if (index == 2) {
        $("#InvestmentPlanParkchild102").append(trHtml);
    }
    if (index == 3) {
        $("#InvestmentPlanParkchild201").append(trHtml);
    }
    if (index == 4) {
        $("#InvestmentPlanParkchild202").append(trHtml);
    }
    if (index == 5) {
        $("#InvestmentPlanParkchild301").append(trHtml);
    }
    if (index == 6) {
        $("#InvestmentPlanParkchild302").append(trHtml);
    }
    if (index == 7) {
        $("#InvestmentPlanParkchild401").append(trHtml);
    }
    if (index == 8) {
        $("#InvestmentPlanParkchild402").append(trHtml);
    }

};

function AddListLast(index, dom) {
    var trHtml = "";
    if (typeof index == "undefined") {
        IndexFlag = IndexFlag + 1;
        index = IndexFlag;
    }

    trHtml += "<div class=\"item b-grayish Tageach\"  id=\"ProductSelect" + index + "\" eacflag=\"ProductSelect\"><div class=\"fif-con  mb10\">";
    trHtml += "<div class=\"fif-box grid-4\"><label class=\"fif-text\">已完成规划：</label> <div class=\"input\">";
    trHtml += "<span id=\"PlanId" + index + " \" class=\"\" eacflag=\"PlanId\"></span> </div></div>";
    trHtml += "<div class=\"fif-box grid-6\"><label class=\"fif-text\"><i class=\"c-red\">*</i>方案所需投资收益率：</label>";
    trHtml += " <div class=\"input\"> <span id=\"PlanRate" + index + "\" class=\"grid-4\" eacflag=\"PlanRate\"></span></div> </div>";
    trHtml += "<a ></a></div>";
    trHtml += "<div class=\"table\"> <table class=\"mb10\" id=\"ProductSelect" + index + "TabCashFund\" eacflag=\"ProductSelectTabCashFund\" width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
    trHtml += " <tbody> <tr> <td width=\"83\" rowspan=\"2\" align=\"center\"  class=\"c-white\" style=\"background-color: #63b2f4\">保值层</td>";
    trHtml += "<td><span class=\"fl\">银行储蓄选择</span><div class=\"fif-con  fr grid-10\"> <div class=\"fif-box grid-12\" style=\"margin-bottom:10px;\">";
    trHtml += " <label class=\"fif-text\">活期：</label>   <div class=\"input grid-11\">";

    //银行选择
    trHtml += "<span id=\"DemandDepositsBank" + index + "\" class=\"\" eacflag=\"DemandDepositsBank\"></span>"

    trHtml += " <span id=\"DemandDepositsBankRate" + index + "\" class=\"\" eacflag=\"DemandDepositsBankRate\"></span>  </div></div>";
    trHtml += "<div class=\"fif-box grid-12\"> <label class=\"fif-text\">定期：</label><div class=\"input grid-11\">";
    //银行选择定期
    trHtml += "<span id=\"TimeDepositBank" + index + "\" class=\"\" eacflag=\"TimeDepositBank\"></span>";
    //银行期限
    trHtml += "<span id=\"TimeDepositBankTime" + index + "\" class=\"\" eacflag=\"TimeDepositBankTime\"></span>";

    trHtml += "<span id=\"TimeDepositBankRate" + index + "\" class=\"\" eacflag=\"TimeDepositBankRate\"></span> </div> </div></div></td> </tr>";
    trHtml += "<tr> <td> <span>基金产品选择</span>  <span class=\"grid-4\">货币市场基金</span> ";
    //货币基金
    if (dom.Fund1 != 0) {
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund1\"> </span> <div class=\"fif-form b-grayish\">  <span class=\"grid-3\" eacflag=\"CashCode\">小龙</span>     <span class=\"grid-4\" eacflag=\"CashFund\">汇添富基金-汇添富社会责任股票</span>   <span class=\"grid-2\" eacflag=\"CashMarket\">货币市场</span>   <span class=\"grid-3\"  eacflag=\"YearlyEarningsRate1\">151.15%</span>  </div>  </div>    </div>";
    } else {
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund1\"></div></div>"
    }
    trHtml += "<input type=\"hidden\" id=\"Fund1" + index + "\" name=\"Fund1" + index + "\" value=\"\" eacflag=\"Fund1\" />  <input type=\"hidden\" eacflag=\"CashCode\" value=\" \" /><input type=\"hidden\" eacflag=\"CashFund\" value=\" \" /> <input type=\"hidden\" eacflag=\"CashMarket\" value=\" \" /><input type=\"hidden\" eacflag=\"YearlyEarningsRate1\" value=\" \" /> </td> </tr> <tr class=\"end\"></tr> </tbody> </table>";
    trHtml += "  <table class=\"mb10\" id=\"ProductSelect" + index + "TabBondFund\" eacflag=\"ProductSelectTabBondFund\" width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">  <tbody> <tr> <td width=\"83\" align=\"center\" class=\"c-white\"  style=\"background-color: #2a91e6\">增值层</td>";
    trHtml += "<td>  <span>基金产品选择</span> <span class=\"grid-4\">债券型基金、混合型基金</span> ";
    //债券基金
    if (dom.Fund2 != 0) {
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund2\"><div class=\"fif-form b-grayish\">  <span class=\"grid-3\" eacflag=\"BondCode\">小龙</span>     <span class=\"grid-4\" eacflag=\"BondFund\">汇添富基金-汇添富社会责任股票</span>   <span class=\"grid-2\" eacflag=\"BondMarket\">货币市场</span>   <span class=\"grid-3\"  eacflag=\"YearlyEarningsRate2\">151.15%</span>  </div>  </div>    </div>";
    } else {
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund2\"></div></div>"
    }
    trHtml += "  <input type=\"hidden\" name=\"Fund2\" id=\"Fund2" + index + "\" value=\"\" eacflag=\"Fund2\" class=\"eac\" />  <input type=\"hidden\" eacflag=\"BondCode\" value=\" \" /><input type=\"hidden\" eacflag=\"BondFund\" value=\" \" /> <input type=\"hidden\" eacflag=\"BondMarket\" value=\" \" /><input type=\"hidden\" eacflag=\"YearlyEarningsRate2\" value=\" \" /> </td> </tr> <tr class=\"end\"></tr>  </tbody>  </table>";
    trHtml += "<table class=\"mb10\" id=\"ProductSelect" + index + "TabStockFund\" eacflag=\"ProductSelectTabStockFund\" width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">  <tbody> <tr> <td width=\"83\" rowspan=\"2\" align=\"center\" class=\"c-white\"  style=\"background-color: #086cc1\">投机层</td>";
    //p2p产品
    trHtml += "   <td> <span>P2P产品选择</span> <span class=\"grid-4\">P2P产品</span>  ";
    if (dom.P2PProduct != 0) {
        trHtml += "<div class=\"selected-list sib-list mt10\"><div class=\"sib-item\" field=\"P2PProduct\"><span title=\"\"></span> <div class=\"fif-form b-grayish\"> <span class=\"\" eacflag=\"P2PName\" title=></span> <span class=\"grid-2\" eacflag=\"InvestmentField\" title=></span> <span class=\"grid-2\" eacflag=\"InvestmentCycle\" title=\"\"></span> <span class=\"grid-2\" eacflag=\"StartAmount\" title=></span>  <span class=\"grid-2\" eacflag=\"EarningsRate\" title=\"\"></span>  </div> </div></div>"
    } else {
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\"  field=\"P2PProduct\"></div></div>"
    }
    trHtml += "<input type=\"hidden\" class=\"eac\" id=\"P2PProduct" + index + "\" value=\"0\" eacflag=\"P2PProduct\"><input type=\"hidden\" class=\"eac\" id=\"P2PProductRate" + index + "\" value=\"0\" eacflag=\"P2PProductRate\"></td></tr>";


    trHtml += "<tr> <td>  <span>基金产品选择</span>  <span class=\"grid-4\">股票型基金</span> "
    //股票基金
    if (dom.Fund3 != 0) {
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund3\"> <div class=\"fif-form b-grayish\">  <span class=\"grid-3\" eacflag=\"StockCode\">小龙</span>     <span class=\"grid-4\" eacflag=\"StockFund\">汇添富基金-汇添富社会责任股票</span>   <span class=\"grid-2\" eacflag=\"StockMarket\">货币市场</span>   <span class=\"grid-3\"  eacflag=\"YearlyEarningsRate3\">151.15%</span>  </div>  </div>    </div>";
    } else {
        trHtml += "<div class=\"selected-list sib-list mt10\"> <div class=\"sib-item\" field=\"Fund3\"></div></div>"
    }
    trHtml += " <input type=\"hidden\" id=\"Fund3" + index + "\" name=\"Fund3'\" value=\"\" eacflag=\"Fund3\" />  <input type=\"hidden\" eacflag=\"StockCode\" value=\" \" /><input type=\"hidden\" eacflag=\"StockFund\" value=\" \" /> <input type=\"hidden\" eacflag=\"StockMarket\" value=\" \" /><input type=\"hidden\" eacflag=\"YearlyEarningsRate3\" value=\" \" /> </td> </tr>    <tr class=\"end\"></tr> </tbody>  </table>   </div>";
    trHtml += "  <div class=\"fif-con \"> <div class=\"fif-box grid-5\"> <label class=\"fif-text\">产品组合预期收益率：</label>";
    trHtml += " <div class=\"input\"><span id=\"TotalRate" + index + "\" class=\"grid-4\" eacflag=\"TotalRate\"></span> </div></div></div></div>"


    $("#InvestmentPlanParkchild081").append(trHtml);

}

//循环赋值
function eacTransVal(index, dom) {
    var panid = EnumConvert.CompleteplanConvet(dom.PlanId)
    $("#ProductSelect" + index).find("span[eacflag='PlanId']").text(panid);
    $("#ProductSelect" + index).find("span[eacflag='PlanRate']").text(dom.PlanRate + " %");
    $("#ProductSelect" + index).find("span[eacflag='DemandDepositsBank']").text(dom.BankView);
    $("#ProductSelect" + index).find("span[eacflag='DemandDepositsBankRate']").text(dom.DemandDepositsBankRate + " %");

    //$("#ProductSelect" + index).find("span[eacflag='TimeDepositBank']").text(dom.BankTimeView);
    //var TimeDepositBankTime = EnumConvert.YearSelectConvet(dom.TimeDepositBankTime);
    //$("#ProductSelect" + index).find("span[eacflag='TimeDepositBankTime']").text(TimeDepositBankTime);
    //$("#ProductSelect" + index).find("span[eacflag='TimeDepositBankRate']").text(dom.TimeDepositBankRate + " %");

    if (dom.BankTimeView == null) {
        $("#ProductSelect" + index).find("span[eacflag='TimeDepositBank']").text("");
    } else {
        $("#ProductSelect" + index).find("span[eacflag='TimeDepositBank']").text(dom.BankTimeView);
    }
    var TimeDepositBankTime = EnumConvert.YearSelectConvet(dom.TimeDepositBankTime);
    if (TimeDepositBankTime == "" || TimeDepositBankTime == null) {
        $("#ProductSelect" + index).find("span[eacflag='TimeDepositBankTime']").text("");
    } else {
        $("#ProductSelect" + index).find("span[eacflag='TimeDepositBankTime']").text(TimeDepositBankTime);
    }
    if (dom.TimeDepositBankRate == null) {
        $("#ProductSelect" + index).find("span[eacflag='TimeDepositBankRate']").text(" %");
    } else {
        $("#ProductSelect" + index).find("span[eacflag='TimeDepositBankRate']").text(dom.TimeDepositBankRate + " %");
    }

    $("#ProductSelect" + index).find("span[eacflag='Fund1']").text(dom.Fund1);
    $("#ProductSelect" + index).find("span[eacflag='Fund2']").text(dom.Fund2);
    $("#ProductSelect" + index).find("span[eacflag='P2PProduct']").text(dom.P2PProduct);
    $("#ProductSelect" + index).find("span[eacflag='P2PProductRate']").text(dom.P2PProductRate);
    $("#ProductSelect" + index).find("span[eacflag='Fund3']").text(dom.Fund3);
    $("#ProductSelect" + index).find("span[eacflag='TotalRate']").text(dom.TotalRate + " %");
    //将其赋值到隐藏域里面
    ////货币基金
    //$("#ProductSelect" + index).find("input[eacflag='CashCode']").val(dom.CashCode);
    //$("#ProductSelect" + index).find("input[eacflag='CashFund']").val(dom.CashFund);
    //$("#ProductSelect" + index).find("input[eacflag='CashMarket']").val(dom.CashMarket);
    //$("#ProductSelect" + index).find("input[eacflag='YearlyEarningsRate1']").val(dom.YearlyEarningsRate1);
    //// 债券型基金
    //$("#ProductSelect" + index).find("input[eacflag='BondCode']").val(dom.BondCode);
    //$("#ProductSelect" + index).find("input[eacflag='BondFund']").val(dom.BondFund);
    //$("#ProductSelect" + index).find("input[eacflag='BondMarket']").val(dom.BondMarket);
    //$("#ProductSelect" + index).find("input[eacflag='YearlyEarningsRate2']").val(dom.YearlyEarningsRate2);
    ////股票型基金
    //$("#ProductSelect" + index).find("input[eacflag='StockCode']").val(dom.StockCode);
    //$("#ProductSelect" + index).find("input[eacflag='StockFund']").val(dom.StockFund);
    //$("#ProductSelect" + index).find("input[eacflag='StockMarket']").val(dom.StockMarket);
    //$("#ProductSelect" + index).find("input[eacflag='YearlyEarningsRate3']").val(dom.YearlyEarningsRate3);

    //货币基金
    $("#ProductSelect" + index).find("span[eacflag='CashCode']").attr("title", dom.CashCode).text(dom.CashCode);
    $("#ProductSelect" + index).find("span[eacflag='CashFund']").attr("title", dom.CashFund).text(dom.CashFund);
    $("#ProductSelect" + index).find("span[eacflag='CashMarket']").attr("title", dom.CashMarket).text(dom.CashMarket);
    $("#ProductSelect" + index).find("span[eacflag='YearlyEarningsRate1']").attr("title", dom.YearlyEarningsRate1).text(dom.YearlyEarningsRate1 + "%");
    // 债券型基金
    $("#ProductSelect" + index).find("span[eacflag='BondCode']").attr("title", dom.BondCode).text(dom.BondCode);
    $("#ProductSelect" + index).find("span[eacflag='BondFund']").attr("title", dom.BondFund).text(dom.BondFund);
    $("#ProductSelect" + index).find("span[eacflag='BondMarket']").attr("title", dom.BondMarket).text(dom.BondMarket);
    $("#ProductSelect" + index).find("span[eacflag='YearlyEarningsRate2']").attr("title", dom.YearlyEarningsRate2).text(dom.YearlyEarningsRate2 + "%");
    //股票型基金
    $("#ProductSelect" + index).find("span[eacflag='StockCode']").attr("title", dom.StockCode).text(dom.StockCode);
    $("#ProductSelect" + index).find("span[eacflag='StockFund']").attr("title", dom.StockFund).text(dom.StockFund);
    $("#ProductSelect" + index).find("span[eacflag='StockMarket']").attr("title", dom.StockMarket).text(dom.StockMarket);
    $("#ProductSelect" + index).find("span[eacflag='YearlyEarningsRate3']").attr("title", dom.YearlyEarningsRate3).text(dom.YearlyEarningsRate3 + "%");
    //p2p产品赋值
    $("#ProductSelect" + index).find("span[eacflag='P2PName']").attr("title", dom.P2PName).text(dom.P2PName);
    $("#ProductSelect" + index).find("span[eacflag='InvestmentField']").attr("title", dom.InvestmentField).text(dom.InvestmentField);
    $("#ProductSelect" + index).find("span[eacflag='InvestmentCycle']").attr("title", dom.InvestmentCycle).text(dom.InvestmentCycle);
    $("#ProductSelect" + index).find("span[eacflag='StartAmount']").attr("title", dom.StartAmount).text(dom.StartAmount);
    $("#ProductSelect" + index).find("span[eacflag='EarningsRate']").attr("title", dom.EarningsRate).text(dom.EarningsRate);
}

//投资规划饼
function ShowInsertmentInfo(Currency, Bond, Stock) {
    var chart;
    $('.showPie').highcharts({
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false
        },
        title: {
            text: '投资分配比例',
            align: 'left'
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        colors: ['#63b2f4', '#2a91e6', '#086cc1'], //'#46adb7', '#f2a83e', '#e16556'
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: false
                },
                showInLegend: true
            }
        },
        series: [{
            type: 'pie',
            name: '投资分配比例',
            data: [
                ['货币', Currency],
                {
                    name: '证券',
                    y: Bond,
                    sliced: false,
                    selected: false
                },
                ['股票', Stock],
            ]
        }]
    });
}

//投资规划结束--------------------预览加载投资规划结束
//************************************************************

//获取税收筹划相关信息--------------预览加载
function GetTaxPlan(proposalId) {
    _ajaxhepler({
        url: "/CompetitionJudges/Assessment/GetTaxPlanObj",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: proposalId
        },
        success: function (datas) {
            if (datas != null) {

                //教育规划信息
                var li = datas.list;
                if (li != null) {
                    var n = li;
                    $("#Salary").text(n.Salary.toFixed(2));//工资、薪金所得
                    $("#SalaryTax").text(n.SalaryTax.toFixed(2));//工资、薪金所得税
                    $("#OperatingRevenue").text(n.OperatingRevenue.toFixed(2));//个体工商户的生产、经营所得
                    $("#OperatingRevenueTax").text(n.OperatingRevenueTax.toFixed(2));//个体工商户的生产、经营所得税
                    $("#EnterprisesRevenue").text(n.EnterprisesRevenue.toFixed(2));// 对企事业单位承包、承租经营所得
                    $("#EnterprisesRevenueTax").text(n.EnterprisesRevenueTax.toFixed(2));//对企事业单位承包、承租经营所得税
                    $("#ServiceIncome").text(n.ServiceIncome.toFixed(2));//劳务报酬所得
                    $("#ServiceIncomeTax").text(n.ServiceIncomeTax.toFixed(2));//劳务报酬所得税
                    $("#Remuneration").text(n.Remuneration.toFixed(2));//稿酬所得
                    $("#RemunerationTax").text(n.RemunerationTax.toFixed(2));//稿酬所得税
                    $("#Loyalities").text(n.Loyalities.toFixed(2));//特许权使用费所得
                    $("#LoyalitiesTax").text(n.LoyalitiesTax.toFixed(2));//特许权使用费所得税
                    $("#Demise").text(n.Demise.toFixed(2));// 财产转让所得
                    $("#DemiseTax").text(n.DemiseTax.toFixed(2));//财产转让所得税
                    $("#IncidentalIncome").text(n.IncidentalIncome.toFixed(2));//偶然所得
                    $("#IncidentalIncomeTax").text(n.IncidentalIncomeTax.toFixed(2));//偶然所得税
                    $("#InterestTaxMain").text(n.Interest.toFixed(2));//利息、红利、股利所得
                    $("#InterestTax").text(n.InterestTax.toFixed(2));//利息、红利、股利所得税
                    $("#TotalAmount").text(n.TotalAmount.toFixed(2));//合计
                    $("#TotalTax").text(n.TotalTax.toFixed(2));//合计税
                    $("#AnalysisTax").text(n.Analysis);//税收筹划分析
                    //隐藏域
                    $("#Id").text(n.Id);
                }
            }
        }
    });
}

//***********************************************************
//获取财产分配表显示-----------------预览加载
function LoadDistributionOfPropertyInfo(proposalId) {
    _ajaxhepler({
        url: "/CompetitionJudges/Assessment/GetDistributionOfPropertyByProposalId",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: proposalId,
            rId: Math.random(),
        },
        success: function (data) {
            if (data != null) {
                SetDistributionOfPropertyInfo(data);//编辑建议书财产分配表设置
            }
        }
    });
}

//设置财产分配表显示
function SetDistributionOfPropertyInfo(data) {

    //客户性别
    var CustomerName = data.CustomerName == null ? "" : data.CustomerName;
    $("#CustomerNameDistribution").text(CustomerName);
    var CustomerAge = data.CustomerAge == 0 ? "" : data.CustomerAge;
    $("#CustomerAge").text(CustomerAge);
    var CustomerSex = EnumConvert.SexConvert(data.CustomerSex);
    $("#CustomerSex").text(CustomerSex);

    if (data.Address == null) {
        $("#AddressDistribution").text("");
    } else {
        $("#AddressDistribution").text(data.Address);
    }

    if (data.Address == null) {
        $("#PositionDistribution").text("");
    } else {
        $("#PositionDistribution").text(data.Position);
    }


    $("#FamilyNum").text(data.FamilyNum);
    if (data.SituationAnalysis==null) {
        $("#SituationAnalysis").text("");
    } else {
        $("#SituationAnalysis").text(data.SituationAnalysis);
    }
    

    var PlanTool = "公正";
    if (data.PlanTool == 2) {
        PlanTool = "信托";
    }
    $("#PlanTool").text(PlanTool);

    if (data.SituationAnalysis == null) {
        $("#PlanAnalysis").text("");
    } else {
        $("#PlanAnalysis").text(data.PlanAnalysis);
    }
   

    //客户亲属列表
    $("#siblistDistribution").html("");
    $(data.ProposalCustomerDetailList).each(function (index, dom) {
        SetDistributionList(dom.DependentName, dom.Age, dom.Relation, dom.InCome);
    });
}
//增加建议书客户家属列表  
function SetDistributionList(DependentName, Age, Relation, InCome) {
    DistributionOfProperty_index += 1;
    var trHtml = "";
    trHtml += "<div class=\"sib-item\">";
    trHtml += "<span id=\"closeId\"></span>";
    trHtml += "<div class=\"fif-form\">";
    trHtml += "<div class=\"fif-box\">";
    trHtml += "<label class=\"fif-text\">姓 名</label>";
    trHtml += "<div class=\"input\"><span id=\"Distribution_name_{4}\" name=\"Distribution_detail_name\" class=\"grid-4\" type=\"text\" value='{0}' >{0}</span></div>";
    trHtml += "</div>";
    trHtml += "<div class=\"fif-box\">";
    trHtml += "<label class=\"fif-text\">年 龄</label>";
    trHtml += "<div class=\"input\"><span id=\"Distribution_age_{4}\" name=\"Distribution_detail_age\" class=\"grid-4\" type=\"text\" value='{1}' >{1}</span><span class=\"ml10\">岁</span></div>";
    trHtml += "</div>";
    trHtml += "<div class=\"fif-box\">";
    trHtml += "<label class=\"fif-text\">与客户关系</label>";
    trHtml += "<div class=\"input\"><span id=\"Distribution_relation_{4}\" name=\"Distribution_detail_relation\" class=\"grid-4\" type=\"text\" value='{2}'>{2}</span></div>";
    trHtml += "</div>";
    trHtml += "<div class=\"fif-box\">";
    trHtml += "<label class=\"fif-text\">年收入</label>";
    trHtml += "<div class=\"input\"><span id=\"Distribution_income_{4}\" name=\"Distribution_detail_income\" class=\"grid-4\" type=\"text\" value='{3}'>{3}</span><span class=\"ml10\">元</span></div>";
    trHtml += "</div></div></div>";

    var html = StringHelper.FormatStr(trHtml,
        DependentName,      //0 姓名
        Age,                //1 年龄
        Relation,           //2 与客户关系
        InCome,             //3 年收入
        DistributionOfProperty_index      //4 随机Id
        );

    $("#siblistDistribution").append(html);
}

//*********************************************************
/**
 * @name 获取财产传承--------------预览加载
 */
function GetHeritage(proposalId) {
    _ajaxhepler({
        url: "/CompetitionJudges/Assessment/GetHeritage",
        type: "POST",
        async: false,
        dataType: "json",
        data:
        {
            ProposalId: proposalId,
            rId: Math.random()
        },
        success: function (data) {
            if (data != null && data != "") {
                $("#CashHeritage").text(data.Cash);//现金
                $("#DepositHeritage").text(data.Deposit);//银行存款
                $("#LifeInsurance").text(data.LifeInsurance);//人寿保单赔偿金额
                $("#OtherCashAccount").text(data.OtherCashAccount);//其他现金账户
                $("#CashSubtotal").text((data.Cash + data.Deposit + data.LifeInsurance + data.OtherCashAccount).toFixed(2));

                $("#StockHeritage").text(data.Stock);//股票
                $("#BondHeritage").text(data.Bond);//债券
                $("#FundHeritage").text(data.Fund);//基金
                $("#OtherInvestmentHeritage").text(data.OtherInvestment);//其他投资收益
                $("#InvestmentSubtotal").text((data.Stock + data.Bond + data.Fund + data.OtherInvestment).toFixed(2));

                $("#Pension").text(data.Pension);//养老金（一次性收入现值）
                $("#AnnuityRevenue").text(data.AnnuityRevenue);//配偶/遗孤年金收益（现值）
                $("#OtherPension").text(data.OtherPension);//其他退休基金
                var subtotal1 = (data.Pension + data.AnnuityRevenue + data.OtherPension).toFixed(2);
                $("#PensionSubtotal").text(subtotal1);

                $("#House").text(data.House);//房产
                $("#CarHeritage").text(data.Car);//汽车
                $("#OtherHeritage").text(data.Other);//其他个人资产
                $("#OtherProperty").text(data.OtherProperty);//其他资产
                var subtotal2 = (data.House + data.Car + data.Other + data.OtherProperty).toFixed(2);
                $("#LoanSubtotalHeritage").text(subtotal2);

                $("#PersonSubtotal").text((data.House + data.Car + data.Other).toFixed(2));

                $("#TotalProperty").text(data.TotalProperty);//资产总计
                $("#TotalProperty2").text(data.TotalProperty);//资产总计

                $("#ShortTermLoan").text(data.ShortTermLoan);//短期贷款
                $("#MediumTermLoans").text(data.MediumTermLoans);//中期贷款
                $("#LongTermLoan").text(data.LongTermLoan);//长期贷款
                $("#OtherLoanHeritage").text(data.OtherLoan);//其他贷款
                $("#LoanSubtotal").text((data.ShortTermLoan + data.MediumTermLoans + data.LongTermLoan + data.OtherLoan).toFixed(2));

                $("#MedicalCosts").text(data.MedicalCosts);//临终医疗费用
                $("#TaxCosts").text(data.TaxCosts);//预期收入纳税额支出
                $("#FuneralExpenses").text(data.FuneralExpenses);//丧葬费用
                $("#HeritageCosts").text(data.HeritageCosts);//遗产处置费用
                $("#OtherCosts").text(data.OtherCosts);//其他费用
                $("#CostSubtotal").text((data.MedicalCosts + data.TaxCosts + data.FuneralExpenses + data.HeritageCosts + data.OtherCosts).toFixed(2));

                $("#OtherLiabilities").text(data.OtherLiabilities);//其他负债
                $("#TotalLiabilities").text(data.TotalLiabilities);//负债总计
                $("#TotalLiabilities2").text(data.TotalLiabilities);

                $("#FinanceAnalysis").text(data.FinanceAnalysis);//财务分析
                var plantool = EnumConvert.HeritageConvert(data.PlanTool)
                $("#PlanToolHeritage").text(plantool);//财产传承规划工具
                $("#PlanAnalysisHeritage").text(data.PlanAnalysis);//财产传承规划分析

                $("#TotalHeritage").text((data.TotalProperty - data.TotalLiabilities).toFixed(2));//净遗产总计
                //隐藏域
                $("#Id").text(data.Id);
            }
        }
    });
}

//判断是否非法字符
function CheckNum(num) {
    var pattern6 = /^\d+(\.\d{1,6})?$/;//只能输入两位小数
    if (!pattern6.test(num)) {
        num = 0;
        return num;
    }
    return num;
}

//=========================计算
//流动资产计算
function calcFlowAssets(Cash, RMBDeposit, OtherAsset) {
    var Cash1 = CheckNum(Cash);
    var RMBDeposit1 = CheckNum(RMBDeposit);
    var OtherAsset1 = CheckNum(OtherAsset);
    var sum = 0;
    if (Cash == Cash1 && RMBDeposit == RMBDeposit1 && OtherAsset == OtherAsset1) {
        sum = Cash * 1 + RMBDeposit * 1 + OtherAsset * 1;
    } else {
        sum = 0;
    }
    return sum;
}
//消费资产计算
function calcConsumeAssets(CreditCard, Microfinance, OtherLoan) {
    var CreditCard1 = CheckNum(CreditCard);
    var Microfinance1 = CheckNum(Microfinance);
    var OtherLoan1 = CheckNum(OtherLoan);
    var sum = 0;
    if (CreditCard1 == CreditCard && Microfinance1 == Microfinance && OtherLoan1 == OtherLoan) {
        sum = CreditCard * 1 + Microfinance * 1 + OtherLoan * 1
    } else {
        sum = 0;
    }
    return sum;
}
//投资资产计算
function calcInvestmentAssets(RMBFixedDeposit, ForeignCurrencyFixedDeposit, StockInvestment, BondInvestment, FundInvestment, IndustryInvestment,
   PolicyInvestment, EstateInvestment, OtherInvestment) {
    var RMBFixedDeposit1 = CheckNum(RMBFixedDeposit);
    var ForeignCurrencyFixedDeposit1 = CheckNum(ForeignCurrencyFixedDeposit);
    var StockInvestment1 = CheckNum(StockInvestment);
    var BondInvestment1 = CheckNum(BondInvestment);
    var FundInvestment1 = CheckNum(FundInvestment);
    var IndustryInvestment1 = CheckNum(IndustryInvestment);
    var PolicyInvestment1 = CheckNum(PolicyInvestment);
    var EstateInvestment1 = CheckNum(EstateInvestment);
    var OtherInvestment1 = CheckNum(OtherInvestment);
    var sum = 0;
    if (RMBFixedDeposit1 == RMBFixedDeposit && ForeignCurrencyFixedDeposit1 == ForeignCurrencyFixedDeposit && StockInvestment1 == StockInvestment && BondInvestment1 == BondInvestment && FundInvestment1 == FundInvestment && IndustryInvestment1 == IndustryInvestment && PolicyInvestment1 == PolicyInvestment && EstateInvestment1 == EstateInvestment && OtherInvestment1 == OtherInvestment) {
        sum = RMBFixedDeposit * 1 + ForeignCurrencyFixedDeposit * 1 + StockInvestment * 1 + BondInvestment * 1 + FundInvestment * 1 + IndustryInvestment * 1 + PolicyInvestment * 1 + EstateInvestment * 1 + OtherInvestment * 1;
    } else {
        sum = 0;
    }
    return sum;
}
//投资负债计算

function calcInvestmentLiability(FinancialLoan, IndustryInvestmentLoan, EstateInvestmentLoan, OtherInvestmentLoan) {
    var FinancialLoan1 = CheckNum(FinancialLoan);
    var IndustryInvestmentLoan1 = CheckNum(IndustryInvestmentLoan);
    var EstateInvestmentLoan1 = CheckNum(EstateInvestmentLoan);
    var OtherInvestmentLoan1 = CheckNum(OtherInvestmentLoan);
    var sum = 0;
    if (FinancialLoan1 == FinancialLoan && IndustryInvestmentLoan1 == IndustryInvestmentLoan && EstateInvestmentLoan1 == EstateInvestmentLoan && OtherInvestmentLoan1 == OtherInvestmentLoan) {
        sum = FinancialLoan * 1 + IndustryInvestmentLoan * 1 + EstateInvestmentLoan * 1 + OtherInvestmentLoan * 1;
    } else {
        sum = 0;
    }
    return sum;
}
//自用负债
function calcSelfLiability(EstateLoan, CarLoan, OthersLoan) {
    var EstateLoan1 = CheckNum(EstateLoan);
    var CarLoan1 = CheckNum(CarLoan);
    var OthersLoan1 = CheckNum(OthersLoan);
    var sum = 0;
    if (EstateLoan1 == EstateLoan && CarLoan1 == CarLoan && OthersLoan1 == OthersLoan) {
        sum = EstateLoan * 1 + CarLoan * 1 + OthersLoan * 1;
    } else {
        sum = 0;
    }
    return sum;
}
//自用资产计算
function calcSelfAsset(Estate, Car, Others) {
    var Others1 = CheckNum(Others);
    var Car1 = CheckNum(Car);
    var Estate1 = CheckNum(Estate);
    //TotalAssets
    var sum = 0;
    if (Others1 == Others && Car1 == Car && Estate1 == Estate) {
        sum = Estate * 1 + Car * 1 + Others * 1;
    } else {
        sum = 0;
    }
    return sum;
}
//资产合计
function calcTotalAssets(FlowAssets, InvestmentAssets, SelfAsset) {
    FlowAssets = CheckNum(FlowAssets);
    InvestmentAssets = CheckNum(InvestmentAssets);
    SelfAsset = CheckNum(SelfAsset);
    var sum = FlowAssets * 1 + InvestmentAssets * 1 + SelfAsset * 1;
    return sum;
}
//负债合计
function calcTotalLoan(ConsumeAssets, InvestmentLiability, SelfLiability) {
    ConsumeAssets = CheckNum(ConsumeAssets);
    InvestmentLiability = CheckNum(InvestmentLiability);
    SelfLiability = CheckNum(SelfLiability);
    var sum = ConsumeAssets * 1 + InvestmentLiability * 1 + SelfLiability * 1;
    return sum;
}

//==========================净值计算==================
//消费净值计算 1）	消费净值=流动资产小计-流动负债小计
function calcConsumeVal(flowAssets, ConsumeAssets) {
    flowAssets = CheckNum(flowAssets);
    ConsumeAssets = CheckNum(ConsumeAssets);

    var sum = flowAssets * 1 - ConsumeAssets * 1;
    return sum;
}
//2）	投资净值=投资资产小计-投资负债小计
function calcInvestmentVal(InvestmentAssets, InvestmentLiability) {
    InvestmentAssets = CheckNum(InvestmentAssets);
    InvestmentLiability = CheckNum(InvestmentLiability);

    var sum = InvestmentAssets * 1 - InvestmentLiability * 1;
    return sum;
}
//3）	自用净值=自用资产-自用负债
function clacSelfVal(SelfAsset, SelfLiability) {
    SelfAsset = CheckNum(SelfAsset);
    SelfLiability = CheckNum(SelfLiability);

    var sum = SelfAsset * 1 - SelfLiability * 1;
    return sum;
}
//4）	净值合计=∑（消费净值、投资净值、自用净值）
function calcSumVal(ConsumeVal, InvestmentVal, SelfVal) {
    ConsumeVal = CheckNum(ConsumeVal);
    InvestmentVal = CheckNum(InvestmentVal);
    SelfVal = CheckNum(SelfVal);

    var sum = ConsumeVal * 1 + InvestmentVal * 1 + SelfVal * 1;
    return sum;
}

//=========================计算---------------------------收支储蓄中的计算
//1.	工作收入计算
function calcWorkIncome(JobIncome, EndowmentInsurance, MedicalInsurance, HousingFund, OtherJobIncome) {
    var JobIncome1 = CheckNum(JobIncome);
    var EndowmentInsurance1 = CheckNum(EndowmentInsurance);
    var MedicalInsurance1 = CheckNum(MedicalInsurance);
    var HousingFund1 = CheckNum(HousingFund);
    var OtherJobIncome1 = CheckNum(OtherJobIncome);
    var sum = 0;
    if (JobIncome1 == JobIncome && EndowmentInsurance1 == EndowmentInsurance && MedicalInsurance1 == MedicalInsurance && HousingFund1 == HousingFund && OtherJobIncome1 == OtherJobIncome) {
        sum = JobIncome * 1 + EndowmentInsurance * 1 + MedicalInsurance * 1 + HousingFund * 1 + OtherJobIncome * 1
    } else {
        sum = 0;
    }
    return sum;
}

//1.	生活支出
function calcLiveExpense(FamilyExpense, ChildExpense, OtherExpense) {
    var FamilyExpense1 = CheckNum(FamilyExpense);
    var ChildExpense1 = CheckNum(ChildExpense);
    var OtherExpense1 = CheckNum(OtherExpense);
    var sum = 0;
    if (FamilyExpense1 == FamilyExpense && ChildExpense1 == ChildExpense && OtherExpense1 == OtherExpense) {
        sum = FamilyExpense * 1 + ChildExpense * 1 + OtherExpense * 1;
    } else {
        sum = 0;
    }
    return sum;
}
//1.	理财收入
function calcInvestmentIncome(Interest, CapitalGains, OtherIncome) {
    var Interest1 = CheckNum(Interest);
    var CapitalGains1 = CheckNum(CapitalGains);
    var OtherIncome1 = CheckNum(OtherIncome);
    var sum = 0;
    if (Interest1 == Interest && CapitalGains1 == CapitalGains && OtherIncome1 == OtherIncome) {
        sum = Interest * 1 + CapitalGains * 1 + OtherIncome * 1;
    } else {
        sum = 0;
    }
    return sum;
}
//1.	理财支出
function calcInvestmentExpense(InterestExpense, InsuranceExpense, OtherFinanceExpense) {
    var InterestExpense1 = CheckNum(InterestExpense);
    var InsuranceExpense1 = CheckNum(InsuranceExpense);
    var OtherFinanceExpense1 = CheckNum(OtherFinanceExpense);
    var sum = 0;
    if (InterestExpense1 == InterestExpense && InsuranceExpense1 == InsuranceExpense && OtherFinanceExpense1 == OtherFinanceExpense) {
        sum = InterestExpense * 1 + InsuranceExpense * 1 + OtherFinanceExpense * 1;
    } else {
        sum = 0;
    }
    return sum;
}

//--------------------------------------现金流量的方法
//加载本期现金及现金等价物净增加额
function loadings() {
    var Money1 = $.trim($("#lifeMoney").text()) * 1;
    var Money2 = $.trim($("#investMoney").text()) * 1;
    var Money3 = $.trim($("#borrowMoney").text()) * 1;
    var Money4 = $.trim($("#InsuranceExpense2").text()) * 1;
    var All = Money1 + Money2 + Money3 + Money4;
    $("#Money").text(All.toFixed(2));
}

//计算现金规划计算页面
var clacCashPanVal = {
    calcRetainCashType: function (multiple) {
        //var multiple = $("RetainCashMultiple").text();
        if (multiple == "0") {
            $("#RetainCashMultiple").text(0);
        } else {
            var familyMonthExpense = $("#FamilyMonthExpense").attr("Family") * 1;
            var result = (multiple * familyMonthExpense).toFixed(2);
            return result;
        }
    }
}
//-----------------------------------教育规划添加HTML方法
function AddHTML(valu) {

    var Nums = valu;

    var html = '';
    html += '<div id="EducationList' + Nums + '" field="EducationList" class="itemBox js_itemboxs" style="border-bottom:1px solid #d7d7d7;">';
    html += '<div class="item-title b-gray">';
    html += '<strong><i class="c-red">*</i>教育阶段选择&nbsp;&nbsp;</strong>';
    html += '<span id="EduStage' + Nums + '" class="" style="width:110px;" field="EduStage"></span>'
    html += '</div>';
    html += ' <div class="fif-form fif-form3 grid-7">';
    html += '<div class="fif-box w100">';
    html += '<label class="fif-text"><i class="c-red">*</i>求学年龄：</label>';
    html += '<div class="input"><span id="EduAge' + Nums + '" field="EduAge" style="" class=""></span><span class="ml10">岁</span></div>';
    html += '</div>';
    html += '<div class="fif-box w100">';
    html += '<label class="fif-text"><i class="c-red">*</i>求学时间：</label>';
    html += '<div class="input"><span id="EduTime' + Nums + '" class="" style="" field="EduTime"></span><span class="ml10">年</span></div>';
    html += '</div>';
    html += '<div class="fif-box w100">';
    html += '<label class="fif-text"><i class="c-red">*</i>目前学费：</label>';
    html += '<div class="input"><span id="Tuition' + Nums + '" class="" style="" field="Tuition"></span><span class="ml10">元/年</span></div>';
    html += '</div>';
    html += '<div class="fif-box w100">';
    html += '<label class="fif-text">上学时学费：</label>';
    html += '<div class="input"><span id="EduTuition' + Nums + '" class="" style="" field="EduTuition"></span><span class="ml10">元/年</span></div></div>';

    html += '<div class="fif-box w100" style="width:110%;">';
    html += '<label class="fif-text"  style="font-size:16px;font-weight:600;height:50px;">上学前需准备的总学费：</label>';
    html += '<div class="input"><span style="font-size:20px;font-weight:600;height:50px;color:#f87608;" id="TotalTuition' + Nums + '" class="" field="TotalTuition"></span><span class="ml10">元</span></div></div>';


    html += '</div>';
    html += '<p class="clear"></p>';
    html += '<input type="hidden" value="' + Nums + '" id="NUM' + Nums + '"/>';
    html += '<input type="hidden" value="0" id="IDN' + Nums + '" field="IDN" /></div>';

    if (Nums < 4) {
        $("#LifeEducationPlanPart1").append(html);
    } else {
        $("#LifeEducationPlanPartSub01").append(html);
    }
    //$("#TagSpan").prev().after(html);
}
