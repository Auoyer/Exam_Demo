using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Helper
{
    public class StringHelper
    {

        /// <summary>
        /// 返回指定数量的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string FilterStr(string str, int num, string temp = "")
        {
            string resStr = string.Empty;
            if (str != null)
            {
                if (str.Length > num)
                {
                    resStr = str.Substring(0, num);
                    if (string.IsNullOrEmpty(temp))
                        resStr += "...";
                    else
                        resStr += temp;
                }
                else
                {
                    resStr = str;
                }
            }
            return resStr;
        }


    }
}
