using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Xml;
using System.Reflection;

namespace Utils
{
    public static class CommValue
    {
        private static NetTcpBinding _tcpBinding;

        /// <summary>
        /// 默认TcpBinding
        /// </summary>
        public static NetTcpBinding DefaultBinding
        {
            get
            {
                if (_tcpBinding == null)
                {
                    try
                    {
                        _tcpBinding = new NetTcpBinding("defaultBinding");
                        return _tcpBinding;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Log.WriteError("未找到默认配置节点", ex);
                    }

                    _tcpBinding = new NetTcpBinding(SecurityMode.None)
                    {
                        Name = "_defaultBinding",
                        CloseTimeout = new TimeSpan(0, 1, 0),
                        OpenTimeout = new TimeSpan(0, 1, 0),
                        ReceiveTimeout = new TimeSpan(0, 30, 0),
                        SendTimeout = new TimeSpan(0, 10, 0),
                        TransactionFlow = false,
                        TransferMode = TransferMode.Streamed,
                        TransactionProtocol = TransactionProtocol.OleTransactions,
                        HostNameComparisonMode = HostNameComparisonMode.StrongWildcard,
                        ListenBacklog = 10,
                        MaxBufferPoolSize = int.MaxValue,
                        MaxBufferSize = int.MaxValue,
                        MaxConnections = 2000,
                        MaxReceivedMessageSize = long.MaxValue,
                    };
                    _tcpBinding.ReaderQuotas = new XmlDictionaryReaderQuotas
                    {
                        MaxArrayLength = 16384,
                        MaxDepth = 32,
                        MaxStringContentLength = 8192,
                        MaxBytesPerRead = 4096,
                        MaxNameTableCharCount = 16384,
                    };
                    _tcpBinding.ReliableSession.Ordered = true;
                    _tcpBinding.ReliableSession.InactivityTimeout = new TimeSpan(0, 10, 0);
                    _tcpBinding.ReliableSession.Enabled = false;
                }
                return _tcpBinding;
            }
        }

        /// <summary>
        /// 服务通道检查间隔 10秒
        /// </summary>
        public const double chkInterval = 10 * 1000;

        /// <summary>
        /// 根据IP和端口生成对应的服务调用地址[0]和元数据添加地址[1]
        /// </summary>
        /// <param name="serverType"></param>
        /// <param name="serverIP"></param>
        /// <param name="serverPort"></param>
        /// <returns></returns>
        public static Uri[] GetSvrUri(Type serverType, string serverIP, int serverPort)
        {
            Uri[] rtnValue = new Uri[2];
            string serverName = serverType.Name;
            rtnValue[0] = new Uri(string.Format("net.tcp://{0}:{1}/{2}", serverIP, serverPort, serverName));
            rtnValue[1] = new Uri(string.Format("net.tcp://{0}:{1}/{2}/Mex", serverIP, serverPort + 1, serverName));
            return rtnValue;
        }

        public static bool GetIPAndPort(Type serverType, out string ip, out int port)
        {
            ip = string.Empty;
            port = 0;
            try
            {
                string key = string.Format("{0}.WCF", serverType.Assembly.GetName().Name);
                var configVal = AppSettingsHelper.GetStringByKey(key, "");
                if (string.IsNullOrWhiteSpace(configVal))
                {
                    LogHelper.Log.WriteError("无可用服务地址配置");
                    return false;
                }
                var tar = configVal.Split(':');
                if (tar.Length < 1)
                {
                    LogHelper.Log.WriteError("IP端口格式非法");
                    return false;
                }
                ip = tar[0];
                if (!int.TryParse(tar[1], out port))
                {
                    LogHelper.Log.WriteError("IP端口格式非法");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("", ex);
                return false;
            }
        }

        /// <summary>
        /// Linq分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageIdx"></param>
        /// <param name="pageLength"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static List<T> Page<T>(this IEnumerable<T> source, int pageIdx, int pageLength, out int total)
        {
            total = 0;
            if (source == null || source.Count() <= 0 || pageIdx <= 0 || pageLength <= 0)
            {
                return new List<T>();
            }
            total = source.Count();
            return source.Skip(pageLength * (pageIdx - 1)).Take(pageLength).ToList();
        }

        /// <summary>
        /// 克隆对象
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static object CloneObject(object o)
        {
            Type t = o.GetType();
            PropertyInfo[] properties = t.GetProperties();
            Object p = t.InvokeMember("", System.Reflection.BindingFlags.CreateInstance, null, o, null);
            foreach (PropertyInfo pi in properties)
            {
                if (pi.CanWrite)
                {
                    object value = pi.GetValue(o, null);
                    pi.SetValue(p, value, null);
                }
            }
            return p;
        }

        /// <summary>
        /// 随机排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputList"></param>
        /// <returns></returns>
        public static List<T> GetRandomList<T>(this List<T> inputList)
        {
            //Copy to a array
            T[] copyArray = new T[inputList.Count];
            inputList.CopyTo(copyArray);

            //Add range
            List<T> copyList = new List<T>();
            copyList.AddRange(copyArray);

            //Set outputList and random
            List<T> outputList = new List<T>();
            Random rd = new Random((int)DateTime.Now.Ticks);

            while (copyList.Count > 0)
            {
                //Select an index and item
                int rdIndex = rd.Next(0, copyList.Count - 1);
                T remove = copyList[rdIndex];

                //remove it from copyList and add it to output
                copyList.Remove(remove);
                outputList.Add(remove);
            }
            return outputList;
        }
    }
}