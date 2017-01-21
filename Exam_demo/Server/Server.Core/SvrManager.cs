using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Exam.API;
using Exam.Svr;
using Training.API;
using Training.Svr;
using Utils;

namespace Server.Core
{
    public static class SvrManager
    {
        public static bool StartAllService(out int ErrCode)
        {
            ErrCode = 10000;
            try
            {
                HostHelper<IExamManage, ExamManage> ExamSvr = new HostHelper<IExamManage, ExamManage>();
                ExamSvr.Initialize();

                HostHelper<ITrainingManage, TrainingManage> TrainingSvr = new HostHelper<ITrainingManage, TrainingManage>();
                TrainingSvr.Initialize();

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("后台服务开启失败", ex);
                return false;
            }
        }
    }
}
