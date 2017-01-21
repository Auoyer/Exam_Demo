
using DotNet.Utilities;
using Utils;
using HtmlAgilityPack;
using Server.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using VM;

namespace Web
{
    public class P2PHelper
    {


        private HttpHelper CurHelper = new HttpHelper();
        /// <summary>
        /// 爬取数据
        /// </summary>
        /// <returns></returns>
        private List<P2PProductVM> DoGet()
        {
            List<P2PProductVM> tmplist = new List<P2PProductVM>();
            HtmlNodeCollection AccountToken = null;
            int j = 1;
            do
            {
                //
                HttpItem item = new HttpItem
                {
                    //j==1为第一页的数据，后面的数据需要加上pageNum参数
                    URL = j == 1 ? string.Format("http://caifu.baidu.com/wealth?fr=ps&zt=ps&tn=baiduhome_pg&qid=16050522501477954528&iframe=0&iswise=0&category=3102")
                    : string.Format("http://caifu.baidu.com/wealth?fr=ps&zt=ps&tn=baiduhome_pg&qid=16050522501477954528&iframe=0&iswise=0&category=3102&pageNum={0}", j),//URL     必需项    
                    ResultCookieType = ResultCookieType.CookieCollection,
                    //  Postdata = string.Format("pageSize={0}&pageNum={1}", 10, j),
                    Method = "GET",
                    ContentType = "application/json;charset=utf-8",
                };
                HttpResult result = CurHelper.GetHtml(item);
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(result.Html);
                if (j > 1)
                {
                    AccountToken.Clear();//先清空
                }
                AccountToken = doc.DocumentNode.SelectNodes("//ul[@id='result-list']/li[@class='list-item ']");
                if (AccountToken != null && AccountToken.Count > 1)
                {
                    for (int i = 0; i < AccountToken.Count; i++)
                    {
                        P2PProductVM tmpmodel = new P2PProductVM();
                        tmpmodel.P2PName = GetString(AccountToken[i].SelectNodes("//div[@class='item-title']")[i].InnerText).Trim();
                        tmpmodel.InvestmentField = GetString(AccountToken[i].SelectNodes("//span[@class='field-col field-col2']")[i].InnerText).Trim();
                        tmpmodel.StartAmount = GetStartAmount(GetString(AccountToken[i].SelectNodes("//span[@class='field-col field-col3']")[i].InnerText));
                        tmpmodel.InvestmentCycle = GetInvestmentCycle(GetString(AccountToken[i].SelectNodes("//span[@class='field-col field-col1 field-col-extend']")[i].InnerText));
                        tmpmodel.EarningsRate = AccountToken[i].SelectNodes("//div[@class='money ']")[i].InnerText;
                        tmpmodel.SourceId = GetP2PId(AccountToken[0].SelectNodes("//a[@class='view-detail ']")[i].OuterHtml).Trim();
                        //并且SourceId不重复
                        if (tmplist.FirstOrDefault(x => x.SourceId.Equals(tmpmodel.SourceId)) == null)
                        {
                            tmplist.Add(tmpmodel);
                        }
                    }
                    j++;

                }
            } while (AccountToken != null);//直到获取的AccountToken为空，表示后面没有数据了，停止

            //var query = (from tm in tmplist
            //             select tm.SourceId).Distinct().ToList();

            return tmplist;
        }

        /// <summary>
        /// 截取字符,去掉：之前的字符
        /// </summary>
        /// <returns></returns>
        private string GetString(string s)
        {
            string str = s.Substring(s.IndexOf("：") + 1);
            str = str.Replace("&nbsp;", "").Trim(new char[] { '/', 't', 'n' });
            return str;
        }

        /// <summary>
        /// 真是蛋疼，只要到元，不要后面的“起投”
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string GetStartAmount(string s)
        {

            string str = s.Substring(0, s.IndexOf("起"));
            return str;
        }

        /// <summary>
        /// 一样的蛋疼，不要后面（）里面的
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string GetInvestmentCycle(string s)
        {
            if (s.IndexOf('(') > -1)
            {
                s = s.Substring(0, s.IndexOf("("));
            }

            return s;
        }

        /// <summary>
        /// 获取SourceId的值，这里得出的数据太长，不能和上面一样处理
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string GetP2PId(string s)
        {
            int i = s.IndexOf("item: ") + 5;//去掉item：
            int j = s.IndexOf(", act:");
            s = s.Substring(i, j - i);
            return s;
        }

        /// <summary>
        /// 将爬取的数据入库，和数据库原来数据通过SourceId比较，
        /// </summary>
        /// <returns></returns>
        public bool AddBlukP2PProduce()
        {
            int result = 0; bool result1 = false; bool result2 = false;
            var model = DoGet().Distinct();
            List<string> newmodeId = model.Select(x => x.SourceId).ToList();//新获取的
            if (newmodeId != null && newmodeId.Count > 0)
            {
                string newList = string.Join(",", newmodeId);
                LogHelper.Log.WriteInfo("获取到的P2P产品Id：" + newList);
            }
            List<string> oldmodelId = SvrFactory.Instance.TrainingSvr.GetOldP2PProductList().Select(x => x.SourceId).ToList();//数据库原来的
            if (oldmodelId != null && oldmodelId.Count > 0)
            {
                string oldList = string.Join(",", oldmodelId);
                LogHelper.Log.WriteInfo("原有的P2P产品Id：" + oldList);
            }
            List<string> updatemodelId = oldmodelId.Intersect(newmodeId).ToList();//需要更新的
            List<string> addmodelId = newmodeId.Except(updatemodelId).ToList();//需要新增的

            List<string> deletemodels = oldmodelId.Except(updatemodelId).ToList();//需要删除的

            //批量删除
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    

                    result2 = SvrFactory.Instance.TrainingSvr.DeleteP2PProductBluk(deletemodels);
                    //批量更新
                    var updatemodel = model.Where(x => updatemodelId.Contains(x.SourceId)).ToList();
                    result1 = SvrFactory.Instance.TrainingSvr.UpdateP2PProduct(updatemodel);
                    //批量新增
                    var addmodel = model.Where(x => addmodelId.Contains(x.SourceId)).ToList();
                    result = SvrFactory.Instance.TrainingSvr.AddBlukP2PProduce(addmodel);
                    //删除重复信息
                    SvrFactory.Instance.TrainingSvr.DeleteRepeatP2P();

                    TrainingCaches.ClearP2PCache();
                    TrainingCaches.LoadP2PCache();

                    scope.Complete();//出错会自动回滚

                }

                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("AddBlukP2PProduce", ex);
                }
                finally
                {
                    scope.Dispose();
                }
            }

            return result > 0 && result1 && result2 ? true : false;
        }

    }
}