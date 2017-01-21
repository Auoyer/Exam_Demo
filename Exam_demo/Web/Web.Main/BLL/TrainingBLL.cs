using Server.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VM;

namespace Web
{
    public class TrainingBLL
    {
        public static TrainExamVM GetTrainExam(int TrainExamId)
        {
            return TrainingCaches.GetTrainExamCache(TrainExamId);
        }
    }
}