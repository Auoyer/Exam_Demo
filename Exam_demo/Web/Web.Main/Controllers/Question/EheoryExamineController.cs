using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Server.Factory;
using Utils;
using VM;
using Web.BLL;
using VM.Custom;
using System.IO;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace Web.Areas.CompetitionUser.Controllers
{
    public class EheoryExamineController : Controller
    {
        private Dictionary<string, byte[]> downDic = new Dictionary<string, byte[]>();

        /// <summary>
        /// 试卷页
        /// </summary>
        /// <returns></returns>
        [OutputCache(VaryByParam = "Id;UserId", Duration = 1800)]
        public ActionResult DoExam(int? Id, int? UserId)
        {
            //var user = MvcHelper.User;
            PaperVM userPaper = ExamCaches.GetUserPaperCache(Id.Value, UserId.Value);
            if (userPaper != null)
            {
                bool bo = (userPaper != null && userPaper.UserSummary != null && userPaper.UserSummary.Count != 0);
                if (bo == false)
                {
                    //最后判断数据库中是否存在该试卷
                    if (userPaper.UserSummary.Count == 0)
                    {
                        #region  //领取试卷 添加数据和缓存
                        //进入考试添加用户试卷得分情况表
                        PaperUserSummaryVM PUserSummary = new PaperUserSummaryVM();
                        PUserSummary.UserId = UserId.Value;
                        PUserSummary.ExamPaperId = userPaper.Id;
                        PUserSummary.TotalScore = userPaper.TotalScore;
                        PUserSummary.Score = 0;
                        PUserSummary.CompetitionId = userPaper.CompetitionId;
                        PUserSummary.Status = (int)PaperUserSummaryStatus.Init;
                        var boo = SvrFactory.Instance.ExamSvr.TakeUpPaper(PUserSummary);
                        if (boo > 0)
                        {
                            //添加到缓存                         
                            List<PaperUserSummaryVM> su = new List<PaperUserSummaryVM>();
                            su.Add(PUserSummary);
                            userPaper.UserSummary = su;
                            PUserSummary.Id = boo;

                            ExamCaches.SetUserPaperCache(userPaper.Id, UserId.Value, userPaper);
                        }
                        #endregion
                    }
                }
            }
            return View();
        }

        /// <summary>
        /// 获取对应题型下试题列表，试卷下方用
        /// </summary>
        /// <param name="questionType">试题类型</param>
        /// <param name="pageIndex">页数</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetPaperDetailList(int? questionType, int? pageIndex, string questionTypeId, int Id, int LibraryId)
        {
            var user = MvcHelper.User;
            PaperLeftVM model = new PaperLeftVM();

            // 获取用户试卷
            PaperVM userPaper = ExamCaches.GetUserPaperCache(Id, user.Id);
            //var userCache = StructureCaches.GetUserCache(user.Id);
            //if (userCache.ExamInfo != null && userCache.ExamInfo.ContainsKey(Id))
            //{
            //    userPaper = userCache.ExamInfo[Id];
            //}
            //else
            //{
            //    userPaper = SvrFactory.Instance.ExamSvr.GetUserPaperProc(Id, user.Id);
            //    userCache.ExamInfo.Add(Id, userPaper);
            //    StructureCaches.SetCache(userCache.Id, userCache);
            //}
            // 获取公共试卷缓存
            PaperVM cache = ExamCaches.GetPaperCache(Id);


            //帮助类
            ExamCacheHelper ex = new ExamCacheHelper(cache, userPaper);

            // 获取所有习题
            var allQue = ex.GetPaperQuestions();

            #region // 获取题型列表和页数
            model.PageSize = 100;
            if (pageIndex.HasValue)
            {
                //若页数为空，则取第一页
                model.PageIndex = pageIndex.Value;
            }
            if (!questionType.HasValue)
            {
                //获取题型列表
                List<TheoryQuestionTypeVM> QuestionType = ex.GetTheoryQuestionType();
                model.QuestionTypes = QuestionType;
                questionType = QuestionType[0].Sort;
            }
            #endregion

            #region //获取试卷姓名等相关信息
            // 姓名、考试名称、时间等信息
            model.UserName = user.UserName;
            model.Sex = user.Sex;
            model.PaperName = userPaper.ExamPaperName;
            model.strTime = string.Format("{0}--{1}", userPaper.StartDate.ToString("HH:mm:ss"), userPaper.EndDate.ToString("HH:mm:ss"));

            // 题量总数
            model.Count = userPaper.Details.Count();
            // 总页数
            model.PageCount = model.Count / model.PageSize;
            if (model.Count % model.PageSize > 0)
            {
                model.PageCount = model.PageCount + 1;
            }
            #endregion

            #region //获取对应题型下的题目和是否标记和作答
            //var ran = SessionHelper.GetSession("DetailsRandom" + Id + "_" + user.Id) as List<PaperDetailVM>;
            var ran = userPaper.DetailsRandom;
            // 如果用户答题结果为空（题目随机排序）
            if (userPaper.UserAnswer.Where(x => x.ExamPaperId == Id && x.UserId == user.Id).FirstOrDefault() == null)
            {

                if (ran != null && ran.Count > 0)
                {
                    model.Details = ran;
                }
                else
                {
                    model.Details = ex.GetPaperDetail2();
                    userPaper.DetailsRandom = model.Details;
                    ExamCaches.SetUserPaperCache(userPaper.Id, user.Id, userPaper);
                    //SessionHelper.SetSession("DetailsRandom" + Id + "_" + user.Id, model.Details);
                }
            }
            else
            {
                if (ran != null && ran.Count > 0)
                {
                    model.Details = ran;
                }
                else
                {
                    //获取全部题型下的所有题目
                    model.Details = ex.GetPaperDetail();
                }
            }

            // 如果没限制当前题型，加载第一个题型
            if (string.IsNullOrEmpty(questionTypeId))
            {
                //获取最开始加载时的第一个题型                           
                var CharpterIdIndex = ex.GetCharpterIdIndex();
                questionTypeId = CharpterIdIndex.Value.ToString(); // 题型章节细分字符串
            }

            var curTypeDetails = model.Details.FindAll(x => x.StructType == questionType.Value);

            // 生成每个习题答题状态
            for (int j = 0; j < curTypeDetails.Count; j++)
            {
                // 获取当前试题
                int QId = curTypeDetails[j].QuesionId;
                var Question = allQue.FirstOrDefault(y => y.Id == QId);
                //var Question = ExamCaches.GetQuestionCache(QId);



                if (userPaper != null)
                {
                    //用户答题结果分析
                    if (userPaper.UserAnswerResult != null)
                    {
                        // 获取当前习题的考生答案结果
                        var paper = userPaper.UserAnswerResult.Where(x => x.QuesionId == QId && x.UserId == user.Id).FirstOrDefault();
                        if (paper == null)
                        {
                            // 当前习题结果为空
                            Question.Result = null;
                            Question.IsDaTi = false;
                        }
                        else
                        {
                            if (paper.UserScore != null)
                            {
                                Question.Result = Convert.ToInt32(paper.UserScore);
                            }
                            else
                            {
                                //判断标记了但未作答时前端颜色问题
                                Question.Result = null;
                            }

                            var pa = userPaper.UserAnswer.Where(x => x.QuesionId == QId && x.UserId == user.Id).FirstOrDefault();
                            if (pa != null)
                            {
                                Question.IsDaTi = true;
                            }
                            else
                            {
                                Question.IsDaTi = false;
                            }
                        }
                    }
                    else
                    {
                        //用户尚未答题
                        Question.Result = null;
                        Question.IsDaTi = false;
                    }
                }
                else
                {
                    // 尚无考试信息
                    Question.Result = null;
                    Question.IsDaTi = false;
                }

                //获取相应的题目

                if (questionTypeId != null)
                {

                    Question.strIdList = questionTypeId;
                    model.Question.Add(Question);
                }
            }
            #endregion

            return Json(new JsonModel(true, "", model));
        }

        /// <summary>
        /// 进入考试
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddPaperUserSummary(int ExamPaperId, int LibraryId)
        {
            var user = MvcHelper.User;
            //var userCache = StructureCaches.GetUserCache(user.Id);
            PaperVM TotalScore = null;
            //理论考试
            TotalScore = ExamCaches.GetPaperCache(ExamPaperId);

            //判断缓存中是否有相应的试卷
            if (TotalScore != null)
            {
                //从数据库取试卷答题信息（从缓存有获取失败的隐患，会导致重大bug）
                PaperVM userPaper = ExamCaches.GetUserPaperCache(ExamPaperId, user.Id);
                bool bo = (userPaper != null && userPaper.UserSummary != null && userPaper.UserSummary.Count != 0);
                if (bo == false)
                {
                    //最后判断数据库中是否存在该试卷
                    if (userPaper.UserSummary.Count == 0)
                    {
                        #region  //领取试卷 添加数据和缓存
                        //userPaper = TotalScore;
                        //进入考试添加用户试卷得分情况表
                        PaperUserSummaryVM PUserSummary = new PaperUserSummaryVM();
                        PUserSummary.UserId = user.Id;
                        PUserSummary.ExamPaperId = ExamPaperId;
                        PUserSummary.TotalScore = TotalScore.TotalScore;
                        PUserSummary.Score = 0;
                        PUserSummary.CompetitionId = TotalScore.CompetitionId;
                        PUserSummary.Status = (int)PaperUserSummaryStatus.Init;
                        var boo = SvrFactory.Instance.ExamSvr.TakeUpPaper(PUserSummary);
                        if (boo > 0)
                        {
                            //添加到缓存                         
                            List<PaperUserSummaryVM> su = new List<PaperUserSummaryVM>();
                            su.Add(PUserSummary);
                            userPaper.UserSummary = su;
                            PUserSummary.Id = boo;

                            ExamCaches.SetUserPaperCache(userPaper.Id, user.Id, userPaper);
                            //if (userCache.ExamInfo != null && userCache.ExamInfo.ContainsKey(ExamPaperId))
                            //{
                            //    userCache.ExamInfo.Remove(model.Id);
                            //    userCache.ExamInfo.Add(model.Id, model);
                            //    StructureCaches.SetCache(userCache.Id, userCache);
                            //}
                            //else
                            //{
                            //    userCache.ExamInfo.Add(model.Id, model);
                            //    StructureCaches.SetCache(userCache.Id, userCache);
                            //}

                            return Json(new JsonModel(true, "", boo));
                        }
                        #endregion
                    }
                    else
                    {
                        //if (userCache.ExamInfo != null && userCache.ExamInfo.ContainsKey(ExamPaperId))
                        //{
                        //    userCache.ExamInfo.Remove(model.Id);
                        //    userCache.ExamInfo.Add(model.Id, model);
                        //    StructureCaches.SetCache(userCache.Id, userCache);
                        //}
                        //else
                        //{
                        //    userCache.ExamInfo.Add(model.Id, model);
                        //    StructureCaches.SetCache(userCache.Id, userCache);
                        //}
                        return Json(new JsonModel(true, "", true));
                    }
                }
                else
                {
                    //if (userCache.ExamInfo != null && userCache.ExamInfo.ContainsKey(ExamPaperId))
                    //{
                    //    userCache.ExamInfo.Remove(model.Id);
                    //    userCache.ExamInfo.Add(model.Id, model);
                    //    StructureCaches.SetCache(userCache.Id, userCache);
                    //}
                    //else
                    //{
                    //    userCache.ExamInfo.Add(model.Id, model);
                    //    StructureCaches.SetCache(userCache.Id, userCache);
                    //}
                    return Json(new JsonModel(true, "", true));
                }
            }

            return Json(new JsonModel(false, "21029", null));// "21029": "对不起！领取试卷失败",
        }

        /// <summary>
        /// 检测试题完成情况
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CheckExamQuestion(int ExamPaperId, int LibraryId)
        {
            bool result = true;
            var user = MvcHelper.User;
            int RadioNum = 0;
            // 若类型为空，则查询考试有什么类型，按单选>多选>判断取          
            //PaperVM papercont = ExamCaches.GetPaperCache(ExamPaperId);

            // 获取用户试卷
            PaperVM userPaper = ExamCaches.GetUserPaperCache(ExamPaperId, user.Id);
            //var userCache = StructureCaches.GetUserCache(user.Id);
            //if (userCache.ExamInfo != null && userCache.ExamInfo.ContainsKey(ExamPaperId))
            //{
            //    userPaper = userCache.ExamInfo[ExamPaperId];
            //}
            //else
            //{
            //    userPaper = SvrFactory.Instance.ExamSvr.GetUserPaperProc(ExamPaperId, user.Id);
            //    userCache.ExamInfo.Add(ExamPaperId, userPaper);
            //    StructureCaches.SetCache(userCache.Id, userCache);
            //}

            // 获取答题数量
            var resultcount = (from a in userPaper.UserAnswer
                               group a by a.QuesionId into b
                               select b).Count();

            if (userPaper != null)
            {
                //准备数据
                List<PaperDetailVM> list = userPaper.Details.ToList();
                RadioNum = list.Count - resultcount;
            }
            if (RadioNum > 0)
            {
                return Json(new JsonModel(true, "", new { Flag = result, radioNum = RadioNum }));
            }

            return Json(new JsonModel(true, "", new { Flag = result }));
        }

        /// <summary>
        /// 获取题目
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetQuestion(int? questionType, int? questionId, int PaperId, int LibraryId)
        {
            var user = MvcHelper.User;


            // 获取公共试卷缓存
            PaperVM cache = ExamCaches.GetPaperCache(PaperId);
            // 获取用户试卷
            PaperVM userPaper = ExamCaches.GetUserPaperCache(PaperId, user.Id);
            //var userCache = StructureCaches.GetUserCache(user.Id);
            //if (userCache.ExamInfo != null && userCache.ExamInfo.ContainsKey(PaperId))
            //{
            //    userPaper = userCache.ExamInfo[PaperId];
            //}
            //else
            //{
            //    userPaper = SvrFactory.Instance.ExamSvr.GetUserPaperProc(PaperId, user.Id);
            //    userCache.ExamInfo.Add(PaperId, userPaper);
            //    StructureCaches.SetCache(userCache.Id, userCache);
            //}
            //var ran = SessionHelper.GetSession("DetailsRandom" + PaperId + "_" + user.Id) as List<PaperDetailVM>;
            var ran = userPaper.DetailsRandom;
            ExamCacheHelper helper = new ExamCacheHelper(cache, userPaper);

            ExamShowVM model = new ExamShowVM();
            //var allQue = helper.GetPaperQuestions();

            //初次加载进来时获取对应的题型和题目
            if (!questionType.HasValue)
            {
                if (ran != null && ran.Count > 0)
                {
                    var typeIdAndQuestionId = helper.GetFirstQuestionType();
                    questionType = Convert.ToInt32(typeIdAndQuestionId.Key);
                    questionId = ran[0].QuesionId;
                }
                else
                {
                    var typeIdAndQuestionId = helper.GetFirstQuestionType();
                    questionType = Convert.ToInt32(typeIdAndQuestionId.Key);
                    questionId = Convert.ToInt32(typeIdAndQuestionId.Value);
                }
            }
            var ques = ExamCaches.GetQuestionCache(questionId.Value);  // 当前题目

            QuestionVM tempEntity = new QuestionVM();
            //清空答案
            tempEntity.AnswerList = null;
            model.Data = tempEntity;

            // 取得当前习题所在的题型章节细分字符串
            if (model.Data != null)
            {
                var charpter2 = helper.GetTheoryQuestionType(); // 全章节
                string strcharpter2 = null; // 当前习题所在的题型章节细分字符串
                strcharpter2 = charpter2.FirstOrDefault(x => x.Sort == ques.StructType).strIdList;

                model.StructType = questionType.Value;
                model.Current.Key = strcharpter2;
                model.Current.Value = questionId.Value;

                #region 获取上下题目ID

                int curIndex = 1;
                //PaperVM papercont = null;

                ////理论考试
                //papercont = ExamCaches.GetPaperCache(PaperId);
                List<PaperDetailVM> list2 = new List<PaperDetailVM>();
                if (ran != null && ran.Count > 0)
                {

                    //准备数据
                    List<PaperDetailVM> list = ran.ToList(); // 获取当前试卷所有题目，并获取类型ID

                    //对应章节下的全部题目（排序）

                    List<int> strAn = strcharpter2.Split(',').ToList().ConvertAll<int>(y => int.Parse(y));

                    list2 = list.Where(x => strAn.Contains(x.StructTypeId)).ToList(); // 获取当前题型下的所有题目
                    var mo = list2.Where(x => x.QuesionId == questionId.Value).FirstOrDefault(); // 获取当前题目
                    int index = list2.IndexOf(mo); // 获取当前题目在当前题型下的index
                    curIndex = index + 1;
                    //首先是题型排序 
                    List<PaperDetailVM> l2 = new List<PaperDetailVM>(); // 试卷所有题目
                    //var st = helper.GetTheoryQuestionType();
                    l2 = ran;

                    int index2 = l2.IndexOf(mo); // 取得当前题目在试卷所有题目中的位置

                    #region 随机列表获取
                    if (l2.Count == 1)
                    {
                        model.Prev = new KeyValue() { Key = 0, Value = 0 };
                        model.Next = new KeyValue() { Key = 0, Value = 0 };
                    }
                    else if (index2 == 0)
                    {
                        model.Prev = new KeyValue() { Key = 0, Value = 0 };
                        model.Next = new KeyValue() { Key = l2[index2 + 1].StructType, Value = l2[index2 + 1].QuesionId };
                    }
                    else if (index2 == l2.Count - 1)
                    {
                        model.Prev = new KeyValue() { Key = l2[index2 - 1].StructType, Value = l2[index2 - 1].QuesionId };
                        model.Next = new KeyValue() { Key = 0, Value = 0 };
                    }
                    else
                    {
                        model.Prev = new KeyValue() { Key = l2[index2 - 1].StructType, Value = l2[index2 - 1].QuesionId };
                        model.Next = new KeyValue() { Key = l2[index2 + 1].StructType, Value = l2[index2 + 1].QuesionId };
                    }
                    #endregion

                }
                else if (cache != null)
                {
                    //准备数据
                    List<PaperDetailVM> list = helper.GetPaperDetail(); // 获取当前试卷所有题目，并获取类型ID

                    //对应章节下的全部题目（排序）
                    List<int> strAn = strcharpter2.Split(',').ToList().ConvertAll<int>(y => int.Parse(y));
                    list2 = list.Where(x => strAn.Contains(x.StructTypeId)).OrderBy(x => x.QuesionId).ToList(); // 获取当前题型下的所有题目

                    var mo = list2.Where(x => x.QuesionId == questionId.Value).FirstOrDefault(); // 获取当前题目
                    int index = list2.IndexOf(mo); // 获取当前题目在当前题型下的index
                    curIndex = index + 1;

                    int index2 = list.IndexOf(mo); // 取得当前题目在试卷所有题目中的位置

                    #region 顺序列表获取
                    if (list.Count == 1)
                    {
                        model.Prev = new KeyValue() { Key = 0, Value = 0 };
                        model.Next = new KeyValue() { Key = 0, Value = 0 };
                    }
                    else if (index2 == 0)
                    {
                        model.Prev = new KeyValue() { Key = 0, Value = 0 };
                        model.Next = new KeyValue() { Key = list[index2 + 1].StructType, Value = list[index2 + 1].QuesionId };
                    }
                    else if (index2 == list.Count - 1)
                    {
                        model.Prev = new KeyValue() { Key = list[index2 - 1].StructType, Value = list[index2 - 1].QuesionId };
                        model.Next = new KeyValue() { Key = 0, Value = 0 };
                    }
                    else
                    {
                        model.Prev = new KeyValue() { Key = list[index2 - 1].StructType, Value = list[index2 - 1].QuesionId };
                        model.Next = new KeyValue() { Key = list[index2 + 1].StructType, Value = list[index2 + 1].QuesionId };
                    }
                    #endregion
                }
                #endregion

                #region 获取用户答案
                model.Answers = helper.GetUserAnswers(questionType.Value, questionId.Value);
                #endregion

                #region 主题干（第x题：....）
                //model.Topic = helper.GetTopic(ques.CharpterID, questionId.Value, PaperId);
                model.Topic = " <font>第" + curIndex + "题</font><span>" + ques.Context + "</span>";
                #endregion

                #region 选项
                //tempEntity.OptionList = helper.OptionList(questionId.Value);
                tempEntity.OptionList = ques.OptionList;
                #endregion

                #region 附件
                //tempEntity.AttachmentList = helper.Attachments(questionId.Value);
                tempEntity.AttachmentList = ques.AttachmentList;
                #endregion

                #region 标准分
                //string str = "";
                //var typelist = helper.GetTheoryQuestionType();
                //for (int ag = 0; ag < typelist.Count; ag++)
                //{
                //    string[] strAn2 = typelist[ag].strIdList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                //    for (int g = 0; g < strAn2.Length; g++)
                //    {
                //        if (strAn2[g] == ques.CharpterID.ToString())
                //        {
                //            str = typelist[ag].strIdList;
                //        }
                //    }
                //}
                var sco2 = userPaper.ScoreInfo.Where(x => x.CharpterID == strcharpter2).FirstOrDefault();
                if (sco2 != null)
                {
                    model.StandardScore = sco2.Score;
                }

                #endregion

                #region 标题栏(XX题（共X题，每题X分，共X分。）)
                //model.Info = helper.GetInfoShow(ques.CharpterID, questionId.Value, PaperId);
                StringBuilder sb = new StringBuilder();
                string typeName = ques.StructType == 1 ? "单选题" : (ques.StructType == 2 ? "多选题" : "判断题");
                sb.Append(typeName);
                sb.Append("（共").Append(list2.Count).Append("题，");
                sb.Append("每题").Append(sco2.Score.ToString("0.0")).Append("分，");
                sb.Append("共").Append((list2.Count * sco2.Score).ToString("0.0")).Append("分）");
                model.Info = sb.ToString();
                #endregion

                #region 得分
                var sco = userPaper.UserAnswerResult.Where(x => x.QuesionId == questionId && x.UserId == user.Id).FirstOrDefault();
                if (sco != null)
                {
                    if (sco.UserScore != null)
                    {
                        model.RightScore = sco.UserScore.Value;
                    }
                    else
                    {
                        model.RightScore = 0;
                    }
                }
                else
                {
                    model.RightScore = 0;
                }
                #endregion

                #region 标准答案
                model.answer = ques.AnswerList.Select(x => x.Answer).ToList();
                #endregion

                #region 评析
                if (ques != null)
                {
                    model.analyse = ques.Analysis;
                }
                #endregion

                return Json(new JsonModel(true, "", model));
            }

            return Json(new JsonModel(false, "无法找到试卷", null));
        }

        /// <summary>
        /// 更新用户答案 
        /// </summary>
        /// <param name="strAnswer"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateExamAnswer(string strAnswer, int Id, int QuestionId, int Type, int LibraryId)
        {
            var user = MvcHelper.User;

            Task.Run(() =>
            {
                string[] strAnswers = strAnswer.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);


                // 获取用户试卷
                PaperVM userPaper = ExamCaches.GetUserPaperCache(Id, user.Id);
                //var userCache = StructureCaches.GetUserCache(user.Id);
                //if (userCache.ExamInfo != null && userCache.ExamInfo.ContainsKey(Id))
                //{
                //    userPaper = userCache.ExamInfo[Id];
                //}
                //else
                //{
                //    userPaper = SvrFactory.Instance.ExamSvr.GetUserPaperProc(Id, user.Id);
                //    userCache.ExamInfo.Add(Id, userPaper);
                //    StructureCaches.SetCache(userCache.Id, userCache);
                //}


                var question = ExamCaches.GetQuestionCache(QuestionId);

                List<PaperUserAnswerVM> examAnswers = new List<PaperUserAnswerVM>();

                List<PaperUserAnswerResultVM> examResult = new List<PaperUserAnswerResultVM>();
                //用户答题结果分析
                var modelScore = userPaper.ScoreInfo.Where(x => x.CharpterID.Contains(question.CharpterID.ToString())).FirstOrDefault();
                PaperUserAnswerResultVM PUAResult = new PaperUserAnswerResultVM();
                PUAResult.ExamPaperId = Id;
                PUAResult.UserId = user.Id;
                PUAResult.QuesionId = QuestionId;
                PUAResult.QuestionTypeId = question.CharpterID;
                PUAResult.StructType = question.StructType;
                PUAResult.QuestionScore = modelScore.Score;
                PUAResult.Result = (int)PaperUserAnswerStatus.Right;
                PUAResult.UserScore = null;

                //通过查询是否在缓存中存在
                var res = userPaper.UserAnswerResult;
                if (res != null)
                {
                    var ress = userPaper.UserAnswerResult.Where(x => x.QuesionId == QuestionId && x.UserId == user.Id).FirstOrDefault();
                    if (ress != null)
                    {
                        PUAResult.Id = ress.Id;
                    }
                }

                if (strAnswers != null && strAnswers.Length > 0)
                {
                    #region 答案不为空时


                    #region 普通题目

                    List<int> Answers = new List<int>();
                    foreach (string item in strAnswers)
                    {
                        int result = 0;
                        Int32.TryParse(item, out result);
                        Answers.Add(result);

                    }

                    //插入数据库                   
                    foreach (int answer in Answers)
                    {
                        //用户试卷答案
                        PaperUserAnswerVM PUAnswer = new PaperUserAnswerVM();
                        PUAnswer.ExamPaperId = userPaper.Id;
                        PUAnswer.UserId = user.Id;
                        PUAnswer.QuesionId = QuestionId;
                        PUAnswer.QuesionTypeId = Type;
                        PUAnswer.Answer = answer;
                        examAnswers.Add(PUAnswer);
                    }

                    //算分           
                    List<int> standardAnswers = question.AnswerList.Select(x => x.Answer).ToList();
                    if (standardAnswers.SequenceEqual(Answers))
                    {
                        #region 得分

                        //得分                                                                 
                        PUAResult.Result = (int)PaperUserAnswerStatus.Right;
                        PUAResult.UserScore = modelScore.Score;

                        examResult.Add(PUAResult);

                        #endregion
                    }
                    else
                    {
                        #region 失分

                        //失分                        
                        PUAResult.Result = (int)PaperUserAnswerStatus.Wrong;
                        PUAResult.UserScore = 0;
                        examResult.Add(PUAResult);

                        #endregion
                    }

                    #endregion


                    //用户答题结果分析
                    //更新数据库
                    var beat = SvrFactory.Instance.ExamSvr.AnswerQuetion(examAnswers, PUAResult);
                    if (beat > 0)
                    {
                        //////////////更新缓存/////////////////
                        //////////////更新缓存////////////////
                        //添加用户答题结果分析到缓存
                        userPaper.UserAnswerResult.RemoveAll(x => x.QuesionId == QuestionId && x.UserId == user.Id);
                        userPaper.UserAnswer.RemoveAll(x => x.QuesionId == QuestionId && x.UserId == user.Id);
                        PUAResult.Id = beat;
                        userPaper.UserAnswerResult.Add(PUAResult);
                        userPaper.UserAnswer.AddRange(examAnswers);
                    }

                    #endregion
                }
                else
                {
                    #region 答案为空时

                    //没答案无论如何都要清空答案表
                    var resu = userPaper.UserAnswerResult;
                    var beat = 0;
                    if (resu != null)
                    {
                        //传的答案为空没有标记（判断结果表是否存在数据）
                        var AnswerResult = userPaper.UserAnswerResult.Where(x => x.QuesionId == QuestionId && x.UserId == user.Id).FirstOrDefault();
                        //判断存在——修改（判断状态是否为未答，是否标记）

                        if (AnswerResult != null)
                        {
                            if (AnswerResult.Result != (int)PaperUserAnswerStatus.Init)
                            {
                                //已答（清空答案表。修改结果表为未答） //已标记（修改成未标记）  
                                PUAResult.Result = (int)PaperUserAnswerStatus.Init;
                                beat = SvrFactory.Instance.ExamSvr.AnswerQuetion(null, PUAResult);

                                ///////////////////////////////         
                                ////////////更新缓存///////////
                                //清空对应答案缓存
                                userPaper.UserAnswer.RemoveAll(x => x.QuesionId == QuestionId && x.UserId == user.Id);
                                userPaper.UserAnswerResult.RemoveAll(x => x.QuesionId == QuestionId && x.UserId == user.Id);
                                userPaper.UserAnswerResult.Add(PUAResult);
                            }
                        }
                    }

                    if (beat > 0)
                    {
                        ///////////////////////////////         
                        ////////////更新缓存///////////
                        //清空对应答案缓存
                        userPaper.UserAnswer.RemoveAll(x => x.QuesionId == QuestionId && x.UserId == user.Id);
                        //修改对应结果缓存
                        userPaper.UserAnswerResult.RemoveAll(x => x.QuesionId == QuestionId && x.UserId == user.Id);
                        userPaper.UserAnswerResult.Add(PUAResult);
                    }

                    #endregion
                }
                //StructureCaches.SetCache(userCache.Id, userCache);
                ExamCaches.SetUserPaperCache(userPaper.Id, user.Id, userPaper);

            });

            return Json(new JsonModel(true, "", new { Flag = true }));
        }

        /// <summary>
        /// 获取考试剩余时间
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetExamTimes(int Id, int LibraryId)
        {
            double times = 0;
            PaperVM paper = ExamCaches.GetPaperCache(Id);

            if (paper != null)
            {
                times = (paper.EndDate - DateTime.Now).TotalSeconds;
            }

            return Json(new JsonModel(true, "", times));
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="path">文件所在路径</param>
        /// <param name="name">文件名称(下载时显示的名称)</param>
        /// <returns></returns>
        public ActionResult DownloadFile(string path, string name)
        {
            string file = Path.GetFileName(path);
            string extension = Path.GetExtension(path).ToLower();

            //文件路径
            //string filePath = Server.MapPath(string.Format("~/UploadFiles/{0}/{1}", file.Substring(0, 6), file));
            string fileUploadAddress = ConfigurationManager.AppSettings["FileUploadAddress"];
            string filePath = Path.Combine(fileUploadAddress, file);

            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");

            switch (extension)
            {
                case ".jpg":
                    Response.ContentType = "application/x-jpg";
                    break;
                case ".bmp":
                    Response.ContentType = "application/x-bmp";
                    break;
                case ".gif":
                    Response.ContentType = "image/gif";
                    break;
                case ".png":
                    Response.ContentType = "application/x-png";
                    break;
                case ".pdf":
                    Response.ContentType = "application/pdf";
                    break;
                case ".ppt":
                    Response.ContentType = "application/vnd.ms-powerpoint";
                    break;
                case ".swf":
                    Response.ContentType = "application/x-shockwave-flash";
                    break;
                case ".txt":
                    Response.ContentType = "text/plain";
                    break;
                default:
                    Response.ContentType = "application/octet-stream";
                    break;
            }
            //解决文件名乱码问题  
            string filename = string.Format("{0}{1}", name, extension);
            Response.AddHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode(filename));

            #region 缓存

            FileStream fs = new FileStream(filePath, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();

            downDic.Add(file, bytes);
            Response.BinaryWrite(downDic[file]);
            #endregion

            Response.Flush();
            Response.End();
            return new EmptyResult();
        }

        /// <summary>
        /// 完成试卷
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FinishExam(int PaperId, int LibraryId)
        {
            var user = MvcHelper.User;
            Task.Run(() =>
            {
                // 获取用户试卷
                PaperVM userPaper = ExamCaches.GetUserPaperCache(PaperId, user.Id);
                //var userCache = StructureCaches.GetUserCache(user.Id);
                //if (userCache.ExamInfo != null && userCache.ExamInfo.ContainsKey(PaperId))
                //{
                //    userPaper = userCache.ExamInfo[PaperId];
                //}
                //else
                //{
                //    userPaper = SvrFactory.Instance.ExamSvr.GetUserPaperProc(PaperId, user.Id);
                //    userCache.ExamInfo.Add(PaperId, userPaper);
                //    StructureCaches.SetCache(userCache.Id, userCache);
                //}

                decimal? Score = (decimal)0;
                if (userPaper.UserAnswerResult != null && userPaper.UserAnswerResult.Count > 0)
                {
                    Score = userPaper.UserAnswerResult.Sum(x => x.UserScore);
                }
                //算分
                //var Score = (from x in userPaper.UserAnswerResult
                //             where x.UserId == user.Id
                //             select x.UserScore).Sum();
                //无主观题(算分，修改状态为已评分，完成时间)
                //更新数据库
                var getSummary = userPaper.UserSummary.FirstOrDefault();
                getSummary.FinishDate = DateTime.Now;
                getSummary.Status = (int)PaperUserSummaryStatus.Marked;
                getSummary.Score = Score.Value;
                var beat = SvrFactory.Instance.ExamSvr.UpDatePaperSummary(getSummary);
                //SessionHelper.RemoveSession("DetailsRandom" + PaperId + "_" + user.Id);
                //StructureCaches.SetCache(userCache.Id, userCache);
                if (beat)
                {
                    userPaper.DetailsRandom = null;
                    ExamCaches.SetUserPaperCache(userPaper.Id, user.Id, userPaper);
                }
            });
            return Json(new JsonModel(true, "", new { Flag = true }));
        }

        /// <summary>
        /// 评分查看
        /// </summary>
        /// <param name="questionType">试题类型</param>
        /// <param name="pageIndex">页数</param>
        /// <returns></returns>
        [HttpPost]
        [OutputCache(VaryByParam = "Id;userId", Duration = 1800)]
        public ActionResult GetPaperDetail(int Id, int LibraryId, int userId)
        {
            var user = MvcHelper.User;
            PaperLeftVM model = new PaperLeftVM();
            // 获取公共试卷缓存
            PaperVM cache = ExamCaches.GetPaperCache(Id);
            // 获取用户试卷
            PaperVM userPaper = ExamCaches.GetUserPaperCache(Id, user.Id);

            ExamCacheHelper helper = new ExamCacheHelper(cache, userPaper);

            #region 底部信息

            // 获取所有习题
            var allQue = helper.GetPaperQuestions();

            #region // 获取题型列表和页数
            model.PageSize = 100;

            //若页数为空，则取第一页
            model.PageIndex = 1;


            //获取题型列表
            List<TheoryQuestionTypeVM> QuestionType = helper.GetTheoryQuestionType();
            model.QuestionTypes = QuestionType;
            int questionType = QuestionType[0].Sort;

            #endregion

            #region //获取试卷姓名等相关信息
            // 姓名、考试名称、时间等信息
            model.UserName = user.UserName;
            model.Sex = user.Sex;
            model.PaperName = userPaper.ExamPaperName;
            model.strTime = string.Format("{0}--{1}", userPaper.StartDate.ToString("HH:mm:ss"), userPaper.EndDate.ToString("HH:mm:ss"));

            // 题量总数
            model.Count = userPaper.Details.Count();
            // 总页数
            model.PageCount = model.Count / model.PageSize;
            if (model.Count % model.PageSize > 0)
            {
                model.PageCount = model.PageCount + 1;
            }
            #endregion

            //获取全部题型下的所有题目
            model.Details = helper.GetPaperDetail();


            //获取最开始加载时的第一个题型                           
            var CharpterIdIndex = helper.GetCharpterIdIndex();
            string questionTypeId = CharpterIdIndex.Value.ToString(); // 题型章节细分字符串

            var curTypeDetails = model.Details.FindAll(x => x.StructType == questionType);

            // 生成每个习题答题状态
            for (int j = 0; j < curTypeDetails.Count; j++)
            {
                // 获取当前试题
                int QId = curTypeDetails[j].QuesionId;
                var Question = allQue.FirstOrDefault(y => y.Id == QId);
                if (userPaper != null)
                {
                    //用户答题结果分析
                    if (userPaper.UserAnswerResult != null)
                    {
                        // 获取当前习题的考生答案结果
                        var paper = userPaper.UserAnswerResult.Where(x => x.QuesionId == QId).FirstOrDefault();
                        if (paper == null)
                        {
                            // 当前习题结果为空
                            Question.Result = null;
                            Question.IsDaTi = false;
                        }
                        else
                        {
                            if (paper.UserScore != null)
                            {
                                Question.Result = Convert.ToInt32(paper.UserScore);
                            }
                            else
                            {
                                //判断标记了但未作答时前端颜色问题
                                Question.Result = null;
                            }

                            var pa = userPaper.UserAnswer.Where(x => x.QuesionId == QId).FirstOrDefault();
                            if (pa != null)
                            {
                                Question.IsDaTi = true;
                            }
                            else
                            {
                                Question.IsDaTi = false;
                            }
                        }
                    }
                    else
                    {
                        //用户尚未答题
                        Question.Result = null;
                        Question.IsDaTi = false;
                    }
                }
                else
                {
                    // 尚无考试信息
                    Question.Result = null;
                    Question.IsDaTi = false;
                }

                //获取相应的题目

                if (questionTypeId != null)
                {

                    Question.strIdList = questionTypeId;
                    model.Question.Add(Question);
                }
            }
            #endregion

            #region 首题信息
            int questionId = model.Details[0].QuesionId;
            var ques = ExamCaches.GetQuestionCache(questionId);  // 当前题目
            if (ques != null)
            {

                QuestionVM tempEntity = new QuestionVM();
                //清空答案
                tempEntity.AnswerList = null;
                model.ExamFirstQuestion.Data = tempEntity;

                //var charpter2 = QuestionType; // 全章节
                //string strcharpter2 = null; // 当前习题所在的题型章节细分字符串
                //strcharpter2 = QuestionType[0].strIdList;

                model.ExamFirstQuestion.StructType = questionType;
                model.ExamFirstQuestion.Current.Key = questionTypeId;
                model.ExamFirstQuestion.Current.Value = questionId;

                //int curIndex = 1;
                //List<PaperDetailVM> list2 = curTypeDetails;
                //准备数据
                List<PaperDetailVM> list = model.Details; // 获取当前试卷所有题目，并获取类型ID

                //对应章节下的全部题目（排序）
                //List<int> strAn = questionTypeId.Split(',').ToList().ConvertAll<int>(y => int.Parse(y));
                //list2 = list.Where(x => strAn.Contains(x.StructTypeId)).OrderBy(x => x.QuesionId).ToList(); // 获取当前题型下的所有题目

                //var mo = list2.Where(x => x.QuesionId == questionId).FirstOrDefault(); // 获取当前题目
                //int index = list2.IndexOf(mo); // 获取当前题目在当前题型下的index
                //curIndex = index + 1;

                //int index2 = list.IndexOf(mo); // 取得当前题目在试卷所有题目中的位置

                #region 顺序列表获取
                if (list.Count == 1)
                {
                    model.ExamFirstQuestion.Prev = new KeyValue() { Key = 0, Value = 0 };
                    model.ExamFirstQuestion.Next = new KeyValue() { Key = 0, Value = 0 };
                }
                else
                {
                    model.ExamFirstQuestion.Prev = new KeyValue() { Key = 0, Value = 0 };
                    model.ExamFirstQuestion.Next = new KeyValue() { Key = list[1].StructType, Value = list[1].QuesionId };
                }
                #endregion

                model.ExamFirstQuestion.Answers = helper.GetUserAnswers(questionType, questionId);

                model.ExamFirstQuestion.Topic = " <font>第1题</font><span>" + ques.Context + "</span>";

                tempEntity.OptionList = ques.OptionList;

                tempEntity.AttachmentList = ques.AttachmentList;

                var sco2 = userPaper.ScoreInfo.Where(x => x.CharpterID == questionTypeId).FirstOrDefault();
                if (sco2 != null)
                {
                    model.ExamFirstQuestion.StandardScore = sco2.Score;
                }

                #region 标题栏(XX题（共X题，每题X分，共X分。）)
                StringBuilder sb = new StringBuilder();
                string typeName = ques.StructType == 1 ? "单选题" : (ques.StructType == 2 ? "多选题" : "判断题");
                sb.Append(typeName);
                sb.Append("（共").Append(curTypeDetails.Count).Append("题，");
                sb.Append("每题").Append(sco2.Score.ToString("0.0")).Append("分，");
                sb.Append("共").Append((curTypeDetails.Count * sco2.Score).ToString("0.0")).Append("分）");
                model.ExamFirstQuestion.Info = sb.ToString();
                #endregion

                var sco = userPaper.UserAnswerResult.Where(x => x.QuesionId == questionId).FirstOrDefault();
                if (sco != null)
                {
                    if (sco.UserScore != null)
                    {
                        model.ExamFirstQuestion.RightScore = sco.UserScore.Value;
                    }
                    else
                    {
                        model.ExamFirstQuestion.RightScore = 0;
                    }
                }
                else
                {
                    model.ExamFirstQuestion.RightScore = 0;
                }

                model.ExamFirstQuestion.answer = ques.AnswerList.Select(x => x.Answer).ToList();
                model.ExamFirstQuestion.analyse = ques.Analysis;

            }
            #endregion

            // 得分表格
            if (userPaper.UserSummary.Count > 0)
            {
                model.PUserAnswerResult = userPaper.UserAnswerResult;
                model.ExamPagerScore = userPaper.TotalScore;
            }

            return Json(new JsonModel(true, "", model));
        }

        /// <summary>
        /// 进入考试
        /// </summary>
        /// <param name="questionType">试题类型</param>
        /// <param name="pageIndex">页数</param>
        /// <returns></returns>
        [HttpPost]
        [OutputCache(VaryByParam = "Id;UserId", Duration = 30)]
        public ActionResult GetPaperEnter(int Id, int LibraryId, int? UserId)
        {
            var user = MvcHelper.User;
            PaperLeftVM model = new PaperLeftVM();
            // 获取公共试卷缓存
            PaperVM cache = ExamCaches.GetPaperCache(Id);
            // 获取用户试卷
            PaperVM userPaper = ExamCaches.GetUserPaperCache(Id, user.Id);
            //var userCache = StructureCaches.GetUserCache(user.Id);
            //if (userCache.ExamInfo != null && userCache.ExamInfo.ContainsKey(Id))
            //{
            //    userPaper = userCache.ExamInfo[Id];
            //}
            //else
            //{
            //    userPaper = SvrFactory.Instance.ExamSvr.GetUserPaperProc(Id, user.Id);
            //    userCache.ExamInfo.Add(Id, userPaper);
            //    StructureCaches.SetCache(userCache.Id, userCache);
            //}

            ExamCacheHelper helper = new ExamCacheHelper(cache, userPaper);

            #region 底部信息

            // 获取所有习题
            var allQue = helper.GetPaperQuestions();

            #region // 获取题型列表和页数
            model.PageSize = 100;

            //若页数为空，则取第一页
            model.PageIndex = 1;


            //获取题型列表
            List<TheoryQuestionTypeVM> QuestionType = helper.GetTheoryQuestionType();
            model.QuestionTypes = QuestionType;
            int questionType = QuestionType[0].Sort;

            #endregion

            #region //获取试卷姓名等相关信息
            // 姓名、考试名称、时间等信息
            model.UserName = user.UserName;
            model.Sex = user.Sex;
            model.PaperName = userPaper.ExamPaperName;
            model.strTime = string.Format("{0}--{1}", userPaper.StartDate.ToString("HH:mm:ss"), userPaper.EndDate.ToString("HH:mm:ss"));
            model.RestTime = (userPaper.EndDate - DateTime.Now).TotalSeconds;
            // 题量总数
            model.Count = userPaper.Details.Count();
            // 总页数
            model.PageCount = model.Count / model.PageSize;
            if (model.Count % model.PageSize > 0)
            {
                model.PageCount = model.PageCount + 1;
            }
            #endregion

            #region 判断习题顺序
            //var ran = SessionHelper.GetSession("DetailsRandom" + Id + "_" + user.Id) as List<PaperDetailVM>;
            var ran = userPaper.DetailsRandom;
            // 如果用户答题结果为空（题目随机排序）
            if (userPaper.UserAnswer == null || userPaper.UserAnswer.Count == 0)
            {

                if (ran != null && ran.Count > 0)
                {
                    model.Details = ran;
                }
                else
                {
                    model.Details = helper.GetPaperDetail2();
                    userPaper.DetailsRandom = model.Details;
                    ExamCaches.SetUserPaperCache(userPaper.Id, user.Id, userPaper);
                    //SessionHelper.SetSession("DetailsRandom" + Id + "_" + user.Id, model.Details);
                }
            }
            else
            {
                if (ran != null && ran.Count > 0)
                {
                    model.Details = ran;
                }
                else
                {
                    //获取全部题型下的所有题目
                    model.Details = helper.GetPaperDetail();
                }
            }
            #endregion

            //获取最开始加载时的第一个题型                           
            var CharpterIdIndex = helper.GetCharpterIdIndex();
            string questionTypeId = CharpterIdIndex.Value.ToString(); // 题型章节细分字符串

            var curTypeDetails = model.Details.FindAll(x => x.StructType == questionType);

            // 生成每个习题答题状态
            for (int j = 0; j < curTypeDetails.Count; j++)
            {
                // 获取当前试题
                int QId = curTypeDetails[j].QuesionId;
                var Question = allQue.FirstOrDefault(y => y.Id == QId);
                if (userPaper != null)
                {
                    //用户答题结果分析
                    if (userPaper.UserAnswerResult != null)
                    {
                        // 获取当前习题的考生答案结果
                        var paper = userPaper.UserAnswerResult.Where(x => x.QuesionId == QId).FirstOrDefault();
                        if (paper == null)
                        {
                            // 当前习题结果为空
                            Question.Result = null;
                            Question.IsDaTi = false;
                        }
                        else
                        {
                            if (paper.UserScore != null)
                            {
                                Question.Result = Convert.ToInt32(paper.UserScore);
                            }
                            else
                            {
                                //判断标记了但未作答时前端颜色问题
                                Question.Result = null;
                            }

                            var pa = userPaper.UserAnswer.Where(x => x.QuesionId == QId).FirstOrDefault();
                            if (pa != null)
                            {
                                Question.IsDaTi = true;
                            }
                            else
                            {
                                Question.IsDaTi = false;
                            }
                        }
                    }
                    else
                    {
                        //用户尚未答题
                        Question.Result = null;
                        Question.IsDaTi = false;
                    }
                }
                else
                {
                    // 尚无考试信息
                    Question.Result = null;
                    Question.IsDaTi = false;
                }

                //获取相应的题目
                if (questionTypeId != null)
                {
                    Question.strIdList = questionTypeId;
                    model.Question.Add(Question);
                }
            }
            #endregion

            #region 首题信息
            int questionId = model.Details[0].QuesionId;
            var ques = ExamCaches.GetQuestionCache(questionId);  // 当前题目
            if (ques != null)
            {

                QuestionVM tempEntity = new QuestionVM();
                //清空答案
                tempEntity.AnswerList = null;
                model.ExamFirstQuestion.Data = tempEntity;

                var charpter2 = helper.GetTheoryQuestionType(); // 全章节
                string strcharpter2 = null; // 当前习题所在的题型章节细分字符串
                strcharpter2 = charpter2.FirstOrDefault(x => x.Sort == ques.StructType).strIdList;

                model.ExamFirstQuestion.StructType = questionType;
                model.ExamFirstQuestion.Current.Key = strcharpter2;
                model.ExamFirstQuestion.Current.Value = questionId;

                int curIndex = 1;
                List<PaperDetailVM> list2 = new List<PaperDetailVM>();
                //准备数据
                List<PaperDetailVM> list = model.Details; // 获取当前试卷所有题目，并获取类型ID

                //对应章节下的全部题目（排序）
                List<int> strAn = strcharpter2.Split(',').ToList().ConvertAll<int>(y => int.Parse(y));

                list2 = list.Where(x => strAn.Contains(x.StructTypeId)).ToList(); // 获取当前题型下的所有题目
                var mo = list2.Where(x => x.QuesionId == questionId).FirstOrDefault(); // 获取当前题目
                int index = list2.IndexOf(mo); // 获取当前题目在当前题型下的index
                curIndex = index + 1;

                int index2 = list.IndexOf(mo); // 取得当前题目在试卷所有题目中的位置

                #region 顺序列表获取
                if (list.Count == 1)
                {
                    model.ExamFirstQuestion.Prev = new KeyValue() { Key = 0, Value = 0 };
                    model.ExamFirstQuestion.Next = new KeyValue() { Key = 0, Value = 0 };
                }
                else
                {
                    model.ExamFirstQuestion.Prev = new KeyValue() { Key = 0, Value = 0 };
                    model.ExamFirstQuestion.Next = new KeyValue() { Key = list[1].StructType, Value = list[1].QuesionId };
                }
                #endregion

                model.ExamFirstQuestion.Answers = helper.GetUserAnswers(questionType, questionId);

                model.ExamFirstQuestion.Topic = " <font>第" + curIndex + "题</font><span>" + ques.Context + "</span>";

                tempEntity.OptionList = ques.OptionList;

                tempEntity.AttachmentList = ques.AttachmentList;

                var sco2 = userPaper.ScoreInfo.Where(x => x.CharpterID == strcharpter2).FirstOrDefault();
                if (sco2 != null)
                {
                    model.ExamFirstQuestion.StandardScore = sco2.Score;
                }

                #region 标题栏(XX题（共X题，每题X分，共X分。）)
                StringBuilder sb = new StringBuilder();
                string typeName = ques.StructType == 1 ? "单选题" : (ques.StructType == 2 ? "多选题" : "判断题");
                sb.Append(typeName);
                sb.Append("（共").Append(list2.Count).Append("题，");
                sb.Append("每题").Append(sco2.Score.ToString("0.0")).Append("分，");
                sb.Append("共").Append((list2.Count * sco2.Score).ToString("0.0")).Append("分）");
                model.ExamFirstQuestion.Info = sb.ToString();
                #endregion

                var sco = userPaper.UserAnswerResult.Where(x => x.QuesionId == questionId).FirstOrDefault();
                if (sco != null)
                {
                    if (sco.UserScore != null)
                    {
                        model.ExamFirstQuestion.RightScore = sco.UserScore.Value;
                    }
                    else
                    {
                        model.ExamFirstQuestion.RightScore = 0;
                    }
                }
                else
                {
                    model.ExamFirstQuestion.RightScore = 0;
                }

                model.ExamFirstQuestion.answer = ques.AnswerList.Select(x => x.Answer).ToList();
                model.ExamFirstQuestion.analyse = ques.Analysis;

            }
            #endregion

            return Json(new JsonModel(true, "", model));
        }

        /// <summary>
        /// 判断考试是否结束
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ExamEnd(int PaperId)
        {
            var Flag = false;
            var paper = ExamCaches.GetPaperCache(PaperId);
            if (paper != null)
            {
                if (paper.EndDate < DateTime.Now)
                {
                    Flag = true;
                }
            }
            else
            {
                Flag = true;
            }
            return Json(new JsonModel(true, "", new { Flag = Flag }));
        }



    }
}