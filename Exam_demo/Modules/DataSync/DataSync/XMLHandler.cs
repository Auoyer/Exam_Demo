using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;
using Utils;

namespace DataSync
{
    public class XMLHandler
    {
        #region 单例
        private static object objLock = new object();
        private static XMLHandler _intance;
        private XMLHandler() { }

        /// <summary>
        /// XML文件访问类单例访问入口
        /// </summary>
        public static XMLHandler Instance
        {
            get
            {
                lock (objLock)
                {
                    if (_intance == null)
                    {
                        _intance = new XMLHandler();
                    }
                }
                return _intance;
            }
        }
        #endregion

        /// <summary>
        /// 文本对象
        /// </summary>
        XmlDocument dom;
        /// <summary>
        /// 记录当前打开的XML数据文件
        /// </summary>
        string currentFile;

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            currentFile = Application.StartupPath + @"\DataStruct.xml";
            ReLoadFile();
        }

        /// <summary>
        /// 重新加载XML文件
        /// </summary>
        public void ReLoadFile()
        {
            dom = new XmlDocument();
            dom.Load(currentFile);
        }

        /// <summary>
        /// 将对应的列名加入Dic
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="basicNodeName">节点名</param>
        /// <param name="dic">要插入的Dic</param>
        /// <param name="t">列名枚举</param>
        private void AddToDic<T>(string basicNodeName, IDictionary<T, string> dic, T t) where T : new()
        {
            try
            {
                string value = string.Empty;
                string xPath = string.Format("//root/{0}/Col[@ColName='{1}']", basicNodeName, t.ToString());
                XmlNodeList targetNotes = dom.SelectNodes(xPath);
                if (targetNotes.Count > 0)
                {
                    XmlNodeList valueNode = targetNotes[0].ChildNodes;
                    if (valueNode.Count > 0)
                    {
                        value = valueNode[0].Value;
                    }
                }
                if (!string.IsNullOrEmpty(value))
                {
                    dic.Add(t, value);
                }
            }
            catch (Exception ex)
            {
                string errMsg = string.Format("根据Xpath【//root/{0}/Col[@ColName='{1}']】读取XML异常",basicNodeName, t.ToString());
                LogHelper.Log.WriteError(errMsg, ex);
            }
        }

        /// <summary>
        /// 获取表名
        /// </summary>
        /// <param name="basicNodeName">节点名称</param>
        /// <returns></returns>
        private string GetTableName(string basicNodeName)
        {
            string value = string.Empty;
            try
            {
                string xPath = string.Format("//root/{0}", basicNodeName);
                XmlNodeList targetNotes = dom.SelectNodes(xPath);
                if (targetNotes.Count > 0)
                {
                    value = targetNotes[0].Attributes["TableName"].Value;
                }
            }
            catch (Exception ex)
            {
                string errMsg = string.Format("根据Xpath【//root/{0}】读取XML异常",basicNodeName);
                LogHelper.Log.WriteError(errMsg, ex);
            }
            return value;
        }

        /// <summary>
        /// 获取基金主体信息表列名
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public Dictionary<DataEnums.MainInfo, string> GetMainInfoNames(out string tableName)
        {
            var rtnValue = new Dictionary<DataEnums.MainInfo, string>();
            string basicNodeName = "MainInfo";
            tableName = GetTableName(basicNodeName);
            AddToDic(basicNodeName, rtnValue, DataEnums.MainInfo.FUNDID);       //FundId
            AddToDic(basicNodeName, rtnValue, DataEnums.MainInfo.FundType);     //基金类型
            AddToDic(basicNodeName, rtnValue, DataEnums.MainInfo.FundCompany);  //基金公司

            return rtnValue;
        }

        /// <summary>
        /// 获取基金份额类别信息表列名
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public Dictionary<DataEnums.UnitClassInfo, string> GetUnitClassInfoNames(out string tableName)
        {
            var rtnValue = new Dictionary<DataEnums.UnitClassInfo, string>();
            string basicNodeName = "UnitClassInfo";
            tableName = GetTableName(basicNodeName);
            AddToDic(basicNodeName, rtnValue, DataEnums.UnitClassInfo.FUNDID);          //FundId
            AddToDic(basicNodeName, rtnValue, DataEnums.UnitClassInfo.FundCode);        //基金代码
            AddToDic(basicNodeName, rtnValue, DataEnums.UnitClassInfo.FundName);        //基金名称

            return rtnValue;
        }

        /// <summary>
        /// 获取基金日净值文件列名
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public Dictionary<DataEnums.NAV, string> GetNAVNames(out string tableName)
        {
            var rtnValue = new Dictionary<DataEnums.NAV, string>();
            string basicNodeName = "NAV";
            tableName = GetTableName(basicNodeName);
            AddToDic(basicNodeName, rtnValue, DataEnums.NAV.NewNetValue);               //最新净值
            AddToDic(basicNodeName, rtnValue, DataEnums.NAV.TotalNewValue);             //累计净值
            AddToDic(basicNodeName, rtnValue, DataEnums.NAV.AnnualizedYield);           //7日年化收益率
            AddToDic(basicNodeName, rtnValue, DataEnums.NAV.UpdateDate);                //交易日期

            return rtnValue;
        }

    }
}
