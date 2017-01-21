using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Factory
{
    /// <summary>
    /// 服务工厂
    /// </summary>
    public class SvrFactory
    {
        #region 单例

        private static object locker = new object();
        private static SvrFactory _instance;
        public static SvrFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new SvrFactory();
                        }
                    }
                }
                return _instance;
            }

        }

        #endregion

        public bool Initialize()
        {
            bool res = true;
            res &= _examSvr.Initialize();
            res &= _trainingSvr.Initialize();
            return res;
        }

        private ExamService _examSvr = new ExamService();
        public ExamService ExamSvr
        {
            get
            {
                return _examSvr;
            }
        }

        private TrainingService _trainingSvr = new TrainingService();
        public TrainingService TrainingSvr
        {
            get
            {
                return _trainingSvr;
            }
        }

    }
}
