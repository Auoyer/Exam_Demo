using Server.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VM;
using Utils;

namespace Web
{
    public class ExamBLL
    {
        /// <summary>
        /// 理论考核自动组卷
        /// </summary>
        /// <param name="paper"></param>
        public void TheoAutoFormPaper(PaperVM paper)
        {  
            paper.Details = new List<PaperDetailVM>();
            var scoreInfo = paper.ScoreInfo;

            QuestionSearch ts = new QuestionSearch()
            {
                Status = 1,
                CollegeId = MvcHelper.User.CollegeId,
            };

            var allQue = SvrFactory.Instance.ExamSvr.GetAllQuestions(ts);

            foreach (var item in scoreInfo)
            {
                var charpterList = item.CharpterID.Split(',').Select(l => int.Parse(l)).ToList();
                var questions = (from x in allQue
                                 where charpterList.Contains(x.CharpterID)
                                 orderby Guid.NewGuid()
                                 select x.Id).Take(item.Count).ToList();
                paper.Details.AddRange(questions.Select(l => new PaperDetailVM { QuesionId = l }));
            }
        }
    }
}