using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data;
using System.Linq;
using Dapper;


namespace DataSync
{
    public class DBHelper
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        private static string dbProviderName = string.Empty;
        /// <summary>
        /// 数据库连接字符窜
        /// </summary>
        private static string dbConnectionString = string.Empty;
        /// <summary>
        /// 当前程序集名称
        /// </summary>
        private static string assemblyName = string.Empty;

        private DBHelper()
        {
        }

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static DBHelper()
        {
        }

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <returns></returns>
        public static DbConnection CreateConnection(string dbConnectionString)
        {
            //根据数据库名获取工厂
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            //创建连接
            DbConnection dbconn = dbfactory.CreateConnection();
            //设置连接字符窜
            dbconn.ConnectionString = dbConnectionString;
            //返回连接
            return dbconn;
        }

    }

}
