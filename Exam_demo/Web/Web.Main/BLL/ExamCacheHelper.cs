using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using VM;
using Utils;
using Server.Factory;

namespace Web.BLL
{
    /// <summary>
    /// 考试Cache扩展帮助类
    /// </summary>
    public class ExamCacheHelper
    {
        /// <summary>
        /// 公共试卷缓存
        /// </summary>
        private PaperVM cache = null;
        /// <summary>
        /// 个人使用试卷缓存
        /// </summary>
        private PaperVM cache2 = null;
        public ExamCacheHelper(PaperVM cache, PaperVM cache2)
        {
            this.cache = cache;
            this.cache2 = cache2;
        }

        /// <summary>
        /// 获取第一个问题类型问题ID
        /// </summary>
        /// <returns></returns>
        public KeyValue GetFirstQuestionType()
        {
            PaperDetailVM pd = GetPaperDetail().FirstOrDefault();
            KeyValue model = new KeyValue();
            model.Key = pd.StructType;
            model.Value = pd.QuesionId;
            return model;

            //var allQue = GetPaperQuestions();
            //KeyValue model = new KeyValue();
            //List<PaperDetailVM> list = cache.Details.ToList();
            //list.ForEach(x =>
            //{
            //    var qq = allQue.FirstOrDefault(y => y.Id == x.QuesionId);
            //    //var qq = ExamCaches.GetQuestionCache(x.QuesionId);
            //    if (qq != null)
            //    {
            //        x.StructType = qq.StructType;
            //        x.StructTypeId = qq.CharpterID;
            //    }
            //});

            //var scorlist = GetTheoryQuestionType();

            //List<PaperDetailVM> list2 = new List<PaperDetailVM>();
            //list2 = GetPaperDetail();

            

            //string[] strAn = scorlist[0].strIdList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            //for (int an = 0; an < strAn.Length; an++)
            //{
            //    list2.AddRange(list.Where(a => a.StructTypeId == Convert.ToInt32(strAn[an])).OrderBy(x => x.QuesionId).ToList());
            //}
            //list2 = list2.OrderBy(x => x.QuesionId).ToList();

            //int StructType = 0;
            //int questionId = 0;
            //if (list2.Count > 0)
            //{
            //    StructType = list2[0].StructType;
            //    questionId = list2[0].QuesionId;
            //}
            //model.Key = StructType;
            //model.Value = questionId;

            //return model;
        }

        /// <summary>
        /// 获取当前问题在试卷中排第几个
        /// </summary>
        /// <param name="questionType">问题类型</param>
        /// <param name="questionId">问题ID</param>
        /// <returns></returns>
        public int GetCurrentIndex(int questionType, int questionId)
        {
            int index = 0;

            var model = ExamCaches.GetQuestionCache(questionId);

            //获取本题在list中的位置
            index = cache.Details.Where(x => x.QuesionId == questionId).Select(x => x.QuesionId).ToList().IndexOf(questionId);

            return index;
        }

        /// <summary>
        /// 返回排序后的题型最小值（及就是排在第一个单选题）
        /// </summary>
        /// <param name="questionType">问题类型</param>
        /// <param name="questionId">问题ID</param>
        /// <returns></returns>
        public KeyValue GetCharpterIdIndex()
        {
            var paix = GetTheoryQuestionType();

            KeyValue model = new KeyValue();
            model.Key = paix[0].Id;
            model.Value = paix[0].strIdList;
            return model;
        }


        /// <summary>
        /// 获取题型列表（题型下章节细分最小值，题型下章节细分字符串，题型名称，题型ID as Sort）
        /// </summary>
        /// <param name="questionType">问题类型</param>
        /// <param name="questionId">问题ID</param>
        /// <returns></returns>
        public List<TheoryQuestionTypeVM> GetTheoryQuestionType()
        {
            // 如果缓存存在，直接取缓存
            var theoryQuestionType = cache.PaperQuestionType;
            if (theoryQuestionType != null && theoryQuestionType.Count > 0)
            {
                return theoryQuestionType;
            }

            //准备存放容器
            List<TheoryQuestionTypeVM> TheoryQuestionType = new List<TheoryQuestionTypeVM>();

            var scorlist = cache.ScoreInfo.ToList();
            for (int f = 0; f < scorlist.Count; f++)
            {
                // 拆分题型
                List<int> QuestionType = scorlist[f].CharpterID.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList().ConvertAll(x => Convert.ToInt32(x)).ToList();
                string key = scorlist[f].CharpterID; // 题型下章节细分字符串
                int value = QuestionType.Min(); // 题型下章节细分最小值

                TheoryQuestionTypeVM type = new TheoryQuestionTypeVM();
                type.Id = value;
                type.strIdList = key;
                var tqt = TrainingCaches.GetQueTypeCache(value);
                if (tqt != null)
                {
                    type.TypeName = tqt.TypeName;
                }

                //题型Id
                //var ty = TrainingCaches.GetQueTypeCache(value);
                if (type.TypeName.Contains("单选题"))
                {
                    type.Sort = 1;
                }
                else if (type.TypeName.Contains("多选题"))
                {
                    type.Sort = 2;
                }
                else {
                    type.Sort = 3;
                }

                TheoryQuestionType.Add(type);
            }

            TheoryQuestionType = TheoryQuestionType.OrderBy(x => x.Sort).ToList();

            // 更新缓存
            cache.PaperQuestionType = TheoryQuestionType;
            ExamCaches.SetPaperCache(cache.Id, cache);
            return TheoryQuestionType;
        }

        /// <summary>
        /// 获取题型列表下所有题目（按题型 题号排序）
        /// </summary>
        /// <param name="questionType">问题类型</param>
        /// <param name="questionId">问题ID</param>
        /// <returns></returns>
        public List<PaperDetailVM> GetPaperDetail()
        {
            // 如果缓存存在，直接取缓存
            var paperDetailSort = cache.DetailsSort;
            if (paperDetailSort != null && paperDetailSort.Count > 0)
            {
                return paperDetailSort;
            }

            //准备存放容器
            var allQue = GetPaperQuestions();
            List<PaperDetailVM> list = cache.Details.ToList(); // 获取试卷所有题目
            //list.ForEach(x =>
            //{
            //    var qq = allQue.FirstOrDefault(y => y.Id == x.QuesionId);
            //    if (qq != null)
            //    {
            //        x.StructTypeId = qq.CharpterID;
            //        x.StructType = qq.StructType;
            //    }
            //});

            List<PaperDetailVM> list2 = new List<PaperDetailVM>();
            var questionType = GetTheoryQuestionType();
            // 按题型 题号排序重组
            foreach (var item in questionType)
            {
                string[] strAn = item.strIdList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                List<PaperDetailVM> l = new List<PaperDetailVM>();
                for (int an = 0; an < strAn.Length; an++)
                {
                    l.AddRange(list.Where(a => a.StructTypeId == Convert.ToInt32(strAn[an])).OrderBy(x => x.QuesionId).ToList());
                }
                l = l.OrderBy(x => x.QuesionId).ToList();
                // 按题型 题号排序重组
                list2.AddRange(l);
            }

            cache.DetailsSort = list2;
            ExamCaches.SetPaperCache(cache.Id, cache);

            return list2;
        }

        /// <summary>
        /// 获取题型列表下所有题目（随机）
        /// </summary>
        /// <param name="questionType">问题类型</param>
        /// <param name="questionId">问题ID</param>
        /// <returns></returns>
        public List<PaperDetailVM> GetPaperDetail2()
        {
            var allQue = GetPaperQuestions();
            //准备存放容器
            List<PaperDetailVM> list = cache.Details.ToList();
            //list.ForEach(x =>
            //{
                
            //    var qq = allQue.FirstOrDefault(y => y.Id == x.QuesionId);
            //    if (qq != null)
            //    {
            //        x.StructTypeId = qq.CharpterID;
            //        x.StructType = qq.StructType;
            //    }
            //});

            List<PaperDetailVM> list2 = new List<PaperDetailVM>();
            var questionType = GetTheoryQuestionType();

            foreach (var item in questionType)
            {
                string[] strAn = item.strIdList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                List<PaperDetailVM> l = new List<PaperDetailVM>();
                for (int an = 0; an < strAn.Length; an++)
                {
                    l.AddRange(list.Where(a => a.StructTypeId == Convert.ToInt32(strAn[an])).OrderBy(x => x.QuesionId).ToList());
                }
                l = l.GetRandomList();
                list2.AddRange(l);
            }

            return list2;
        }

        /// <summary>
        /// 根据当前问题位置，获取问题(问题类型，问题ID)
        /// </summary>
        /// <param name="index">位置</param>
        /// <returns>问题(问题类型，问题ID)</returns>
        public KeyValue GetKeyValueByIndex(int index, int PaperId, int? type, int LibraryId)
        {
            KeyValue model = new KeyValue();
            var allQue = ExamCaches.CurQuestionCache();
            PaperVM papercont = null;
            //理论考试
            papercont = ExamCaches.GetPaperCache(PaperId);

            //准备数据
            List<PaperDetailVM> list = papercont.Details.ToList();
            list.ForEach(x =>
            {
                var qq = allQue.FirstOrDefault(y => y.Id == x.QuesionId);
                if (qq != null)
                {
                    x.StructType = qq.StructType;
                    x.StructTypeId = qq.CharpterID;
                }
            });

            List<PaperDetailVM> list2 = new List<PaperDetailVM>();

            var charpter2 = GetTheoryQuestionType();
            string strcharpter2 = null;
            for (int a = 0; a < charpter2.Count; a++)
            {
                string[] An = charpter2[a].strIdList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                for (int b = 0; b < An.Length; b++)
                {
                    if (An[b] == type.ToString())
                    {
                        strcharpter2 = charpter2[a].strIdList;  // 获取当前题型的章节细分字符串
                        break;
                    }
                }
            }

            string[] strAn = strcharpter2.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for (int an = 0; an < strAn.Length; an++)
            {
                list2.AddRange(list.Where(a => a.StructTypeId == Convert.ToInt32(strAn[an])).OrderBy(x => x.QuesionId).ToList());
            }
            list2 = list2.OrderBy(x => x.QuesionId).ToList(); // 获取当前题型下的所有题目

            model.Key = list2[index].StructType;
            model.Value = list2[index].QuesionId;

            return model;
        }

        /// <summary>
        /// 获取用户填写的答案
        /// </summary>
        /// <param name="questionType">问题类型</param>
        /// <param name="questionId">问题ID</param>
        /// <returns>答案</returns>
        public List<int> GetUserAnswers(int questionType, int questionId)
        {
            int userId = MvcHelper.User.Id;
            List<int> answer = null;
            if (cache2.UserAnswer != null && cache2.UserAnswer.Count > 0)
            {
                if (questionType == (int)StructType.SelectRadio || questionType == (int)StructType.SelectCheckBox || questionType == (int)StructType.Determine)
                {
                    var myAnswers = cache2.UserAnswer.Where(x => x.QuesionId == questionId && x.UserId == userId).Select(x => x.Answer).ToList();
                    if (myAnswers != null && myAnswers.Count > 0)
                    {
                        answer = new List<int>();
                        foreach (var item in myAnswers)
                        {
                            if (item.HasValue)
                            {
                                answer.Add(item.Value);
                            }
                        }
                    }
                }
            }

            return answer;
        }

        /// <summary>
        /// 获取用户填写的答案
        /// </summary>
        /// <param name="questionType">问题类型</param>
        /// <param name="questionId">问题ID</param>
        /// <returns>答案</returns>
        public List<int> GetUserAnswers(int questionType, int questionId, int uId)
        {
            int userId = uId;
            List<int> answer = null;
            if (cache2.UserAnswer != null && cache2.UserAnswer.Count > 0)
            {
                if (questionType == (int)StructType.SelectRadio || questionType == (int)StructType.SelectCheckBox || questionType == (int)StructType.Determine)
                {
                    var myAnswers = cache2.UserAnswer.Where(x => x.QuesionId == questionId && x.UserId == userId).Select(x => x.Answer).ToList();
                    if (myAnswers != null && myAnswers.Count > 0)
                    {
                        answer = new List<int>();
                        foreach (var item in myAnswers)
                        {
                            if (item.HasValue)
                            {
                                answer.Add(item.Value);
                            }
                        }
                    }
                }
            }

            return answer;
        }

        /// <summary>
        /// 获取展示信息(题量分数)
        /// </summary>
        /// <param name="questionType">问题类型</param>
        /// <param name="questionId">问题ID</param>
        /// <returns>展示信息</returns>
        public string GetInfoShow(int questionType, int questionId, int PaperId)
        {
            var typeName = TrainingCaches.GetQueTypeCache(questionType).TypeName;
            int curType = (typeName == "单选题") ? 1 : (typeName == "多选题") ? 2 : 3;
            List<PaperDetailVM> list2 = GetPaperDetail().FindAll(x => x.StructType == curType);

            StringBuilder sb = new StringBuilder();
            //var allQue = ExamCaches.CurQuestionCache();
            ////准备存放容器
            //List<PaperDetailVM> list = cache.Details.ToList();
            //list.ForEach(x =>
            //{
            //    var qq = allQue.FirstOrDefault(y => y.Id == x.QuesionId);
            //    if (qq != null)
            //    {
            //        x.StructTypeId = qq.CharpterID;
            //        x.StructType = qq.StructType;
            //    }
            //});

            //List<PaperDetailVM> list2 = new List<PaperDetailVM>();

            //var qType = GetTheoryQuestionType();

            //string[] qTypes = null;

            //for (int ag = 0; ag < qType.Count; ag++)
            //{
            //    string[] strAn2 = qType[ag].strIdList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            //    for (int g = 0; g < strAn2.Length; g++)
            //    {
            //        if (strAn2[g] == questionType.ToString())
            //        {
            //            qTypes = qType[ag].strIdList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            //        }
            //    }
            //}

            //foreach (var item in qTypes)
            //{
            //    string[] strAn = item.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            //    List<PaperDetailVM> l = new List<PaperDetailVM>();
            //    for (int an = 0; an < strAn.Length; an++)
            //    {
            //        l.AddRange(list.Where(a => a.StructTypeId == Convert.ToInt32(strAn[an])).OrderBy(x => x.QuesionId).ToList());
            //    }

            //    list2.AddRange(l);
            //}
            //list2 = list2.OrderBy(x => x.QuesionId).ToList();
            //var qId = cache.Details.First(x => x.QuesionId == questionId);

            //var typeName = TrainingCaches.GetQueTypeCache(questionType).TypeName;
            var Score = cache.ScoreInfo.Where(x => x.CharpterID.Contains(questionType.ToString())).FirstOrDefault();
            decimal RadioScore = Score.Score;
            sb.Append(typeName);
            sb.Append("（共").Append(list2.Count).Append("题，");
            sb.Append("每题").Append(RadioScore.ToString("0.0")).Append("分，");
            sb.Append("共").Append((list2.Count * RadioScore).ToString("0.0")).Append("分）");

            return sb.ToString();
        }

        /// <summary>
        /// 获取主题干
        /// </summary>
        /// <param name="questionType">问题类型</param>
        /// <param name="questionId">问题ID</param>
        /// <returns>主题干</returns>
        public string GetTopic(int questionType, int questionId, int PaperId)
        {
            var typeName = TrainingCaches.GetQueTypeCache(questionType).TypeName;
            int curType = (typeName == "单选题") ? 1 : (typeName == "多选题") ? 2 : 3;
            List<PaperDetailVM> list = GetPaperDetail().FindAll(x => x.StructType == curType);
            StringBuilder sb = new StringBuilder();

            //var allQue = ExamCaches.CurQuestionCache();
            ////准备数据
            //List<PaperDetailVM> list = cache.Details.Where(x => x.ExamPaperId == PaperId).Distinct().ToList();
            //list.ForEach(x =>
            //{
            //    var qq = allQue.FirstOrDefault(y => y.Id == x.QuesionId);
            //    if (qq != null)
            //    {
            //        x.StructTypeId = qq.CharpterID;
            //        x.StructType = qq.StructType;
            //    }
            //});

            //StringBuilder sb = new StringBuilder();
            //var charpter = GetTheoryQuestionType();
            //List<int> strcharpter = null;
            //for (int a = 0; a < charpter.Count; a++)
            //{
            //    string[] An = charpter[a].strIdList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            //    for (int b = 0; b < An.Length; b++)
            //    {
            //        if (An[b] == questionType.ToString())
            //        {
            //            strcharpter = charpter[a].strIdList.Split(',').ToList().ConvertAll<int>(y => int.Parse(y));
            //        }
            //    }
            //}

            ////准备数据
            //list = list.Where(a => strcharpter.Contains(a.StructTypeId)).ToList();
            var qId = list.First(x => x.QuesionId == questionId);
            var mo1 = ExamCaches.GetQuestionCache(questionId);

            //var typeName = TrainingCaches.GetQueTypeCache(questionType).TypeName;
            //拼接
            sb.Append(" <font>第" + (list.IndexOf(qId) + 1) + "题</font><span>" + mo1.Context + "</span>");
            return sb.ToString();
        }

        /// <summary>
        /// 获取选项
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public List<QuestionOptionVM> OptionList(int questionId)
        {
            List<QuestionOptionVM> option = new List<QuestionOptionVM>();

            var detail = ExamCaches.GetQuestionCache(questionId);
            option = detail.OptionList;
            return option;
        }

        /// <summary>
        /// 获取附件
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public List<QuestionAttachmentsVM> Attachments(int questionId)
        {
            List<QuestionAttachmentsVM> option = new List<QuestionAttachmentsVM>();

            var detail = ExamCaches.GetQuestionCache(questionId);


            if (detail.AttachmentList != null)
            {
                option = detail.AttachmentList;
            }

            return option;
        }

        /// <summary>
        /// 获取试卷习题
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public List<QuestionVM> GetPaperQuestions()
        {
            // 如果缓存存在，直接取缓存
            var paperQuestions = cache.PaperQuestions;
            if (paperQuestions != null && paperQuestions.Count > 0)
            {
                return paperQuestions;
            }

            //准备存放容器
            var allQue = ExamCaches.CurQuestionCache();
            List<PaperDetailVM> list = cache.Details.ToList(); // 获取试卷所有题目
            list.ForEach(x =>
            {
                var qq = allQue.FirstOrDefault(y => y.Id == x.QuesionId);
                x.StructTypeId = qq.CharpterID;
                x.StructType = qq.StructType;
                paperQuestions.Add(qq);
            });

            // 更新缓存
            cache.PaperQuestions = paperQuestions;
            ExamCaches.SetPaperCache(cache.Id, cache);

            return paperQuestions;
        }
    }
}