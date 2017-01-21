using Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Server.Core
{
    /// <summary>
    /// Title：WCF服务托管类
    /// Create：彭浩
    /// CreateDate：2014年4月16日
    /// </summary>
    class HostHelper<T, K> where K : class,T, new()
    {

        private ServiceHost _service;
        private Uri urlService = null;
        private Uri urlMex = null;

        public void Initialize()
        {
            string ip;
            int port;
            if (CommValue.GetIPAndPort(typeof(T), out ip, out port))
            {
                var url = CommValue.GetSvrUri(typeof(T), ip, port);
                urlService = url[0];
                urlMex = url[1];
                OpenService();
            }
        }

        private void OpenService()
        {
            _service = new ServiceHost(new K(), urlService);
            _service.AddServiceEndpoint(typeof(T), CommValue.DefaultBinding, urlService);

            _service.Description.Behaviors.Add(new ServiceMetadataBehavior());
            _service.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexTcpBinding(), urlMex);

            _service.Open();

            string serviceInfo = string.Format("Service[{0}] Start Success!{2}ServiceURL:[{1}],urlMex:[{3}]", typeof(T).Name, urlService.ToString(), Environment.NewLine, urlMex);
            LogHelper.Log.WriteInfo(serviceInfo);
        }

    }
}
