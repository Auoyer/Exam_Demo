using Server.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utils;
using VM;

namespace Web
{
    public class AutoCommit
    {
        public static AutoCommit Instance = new AutoCommit();

        private TimerEvent autoCommitEvent;
        private AutoCommit()
        {
            autoCommitEvent = new TimerEvent("未完成试卷自动提交", new TimeSpan(0, 1, 0), Commmit, null);
        }

        public void Init()
        {
            autoCommitEvent.Start();
        }

        private void Commmit()
        {
            var now = DateTime.Now;
            var tarPapers = (from x in ExamCaches.CurPaperCache()
                             where now >= x.EndDate.AddMinutes(1) && x.Status == (int)ExamPaperStatus.Publish
                             select x).ToList();
            if (tarPapers != null && tarPapers.Count > 0)
            {
                for (int i = 0; i < tarPapers.Count; i++)
                {
                    var paperInfo = tarPapers[i];
                    var answerInfo = SvrFactory.Instance.ExamSvr.GetPaperForScore(tarPapers[i].Id);

                    //只处理未自己交卷的部分
                    var summaryInfo = answerInfo.UserSummary.Where(l => l.Status != (int)PaperUserSummaryStatus.Marked);
                    foreach (var tarSummary in summaryInfo)
                    {

                        tarSummary.UnScoredCount = 0;
                        tarSummary.Score = (from x in answerInfo.UserAnswerResult
                                            where x.UserId == tarSummary.UserId && x.UserScore.HasValue
                                            select x.UserScore.Value).Sum();
                        tarSummary.Status = (int)PaperUserSummaryStatus.Marked;

                        if (!tarSummary.FinishDate.HasValue)
                        {
                            tarSummary.FinishDate = paperInfo.EndDate;
                        }

                        var res = SvrFactory.Instance.ExamSvr.UpdatePaperUserSummary(tarSummary);
                        //SessionHelper.RemoveSession("DetailsRandom" + tarSummary.ExamPaperId + "_" + tarSummary.UserId);
                        if (!res)
                        {
                            throw new Exception("自动交卷算分更新用户得分失败！1分钟后重试……");
                        }
                        else
                        {
                            PaperVM userPaper = ExamCaches.GetUserPaperCache(tarSummary.ExamPaperId, tarSummary.UserId);
                            userPaper.DetailsRandom = null;
                            ExamCaches.SetUserPaperCache(userPaper.Id, tarSummary.UserId, userPaper);
                        }
                    }
                    if (answerInfo.UserSummary.Count(l => l.Status == (int)PaperUserSummaryStatus.Marked) == answerInfo.UserSummary.Count)
                    {
                        paperInfo.Status = (int)ExamPaperStatus.End;
                        if (SvrFactory.Instance.ExamSvr.UpdatePaper(paperInfo))
                        {
                            var cachePaperModel = ExamCaches.GetPaperCache(paperInfo.Id);
                            cachePaperModel.Status = (int)ExamPaperStatus.End;
                            ExamCaches.SetPaperCache(cachePaperModel.Id, cachePaperModel);
                        }
                    }
                }
            }
        }
    }
}