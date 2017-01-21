using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.Factory;
using VM;

namespace Web
{
    public class CaseBLL
    {
        /// <summary>
        /// 获取考核案例
        /// </summary>
        /// <param name="ProposalId">建议书Id</param>
        /// <returns></returns>
        public static ExamCaseVM GetExamCase(int TrainExamId)
        {
            var model = TrainingCaches.CurExamCaseCache().Where(x => x.TrainExamId == TrainExamId).FirstOrDefault();
            if (model == null)
            {
                //缓存没有时，从数据库加载
                //1.考核案例
                model = SvrFactory.Instance.TrainingSvr.GetExamCaseByTrainExamId(TrainExamId);
                if (model != null)
                {
                    TrainingCaches.SetExamCaseCache(model.Id, model);
                }
            }

            return model;
        }

        /// <summary>
        /// 获取考核案例
        /// </summary>
        /// <param name="TrainExamId">案例Id</param>
        /// <returns></returns>
        public static CaseVM GetCase(int TrainExamId)
        {
            CaseVM model = new CaseVM();
            var teCase = TrainingCaches.GetCaseCache(TrainExamId);
            if (teCase != null && teCase.Case != null)
            {
                model = teCase.Case;
            }
            return model;
        }

    }
}