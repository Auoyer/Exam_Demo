using Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Server.Factory
{
    public class ClientHelper<T> where T : class
    {
        private static object locker = new object();
        private ChannelFactory<T> factory;
        private T CurChannel;
        private MethodInfo testAction;
        System.Timers.Timer t;
        public T GetService()
        {
            return CurChannel;
        }

        private void DoTest()
        {
            testAction.Invoke(CurChannel, null);
        }

        public bool Initialize(string testActionFromT)
        {
            try
            {
                if (factory == null)
                {
                    string ip;
                    int port;
                    if (!CommValue.GetIPAndPort(typeof(T), out ip, out port))
                    {
                        return false;
                    }
                    testAction = typeof(T).GetMethod(testActionFromT);
                    if (testAction == null)
                    {
                        return false;
                    }
                    var url = CommValue.GetSvrUri(typeof(T), ip, port);
                    EndpointAddress ServerAddress = new EndpointAddress(url[0]);
                    factory = new ChannelFactory<T>(CommValue.DefaultBinding, ServerAddress);
                }
                lock (locker)
                {
                    CurChannel = factory.CreateChannel();
                    DoTest();
                }
                if (t == null)
                {
                    t = new System.Timers.Timer(CommValue.chkInterval);
                    t.Elapsed += new ElapsedEventHandler((obj, e) =>
                    {
                        try
                        {
                            DoTest();
                        }
                        catch
                        {
                            try
                            {
                                lock (locker)
                                {
                                    CurChannel = factory.CreateChannel();
                                    DoTest();
                                }
                            }
                            catch
                            {
                                //do nothing,wait next check
                            }
                        }
                    });
                    t.Start();
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("WCF服务初始化异常", ex);
                return false;
            }
        }
    }
}
