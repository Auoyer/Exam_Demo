using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class EncryptHelper
    {
        /// <summary>
        /// MD5 hash加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5(string str)
        {
            var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            var result = BitConverter.ToString(md5.ComputeHash(UnicodeEncoding.UTF8.GetBytes(str.Trim())));
            return result;
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="str">待加密的明文</param>
        /// <returns></returns>
        public static string EncodeBase64(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            string result = Convert.ToBase64String(bytes);
            return result;
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="str">待解密的密文</param>
        /// <returns></returns>
        public static string DecodeBase64(string str)
        {
            byte[] bytes = Convert.FromBase64String(str);
            string result = Encoding.UTF8.GetString(bytes);
            return result;
        }


    }
}
