using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Utils;

namespace DataSync
{
    public class FundCodeDAL
    {
        public string trainDBConnString = string.Empty;
        public FundCodeDAL()
        {
            trainDBConnString = AppSettingsHelper.GetStringByKey("Training.Svr.SQL", "");
        }


        public List<string> GetFundCodeList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from FundCode ");

            List<string> result = new List<string>();
            using (var conn = DBHelper.CreateConnection(trainDBConnString))
            {
                conn.Open();
                result = conn.Query<string>(strSql.ToString()).ToList();
            }

            return result;
        }


    }
}
