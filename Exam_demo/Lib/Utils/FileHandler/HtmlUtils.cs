using System;
using System.IO;
using System.Net;
using System.Text;

namespace Utils
{
    /// <summary>
    /// 描述：操作HTML的工具类
    /// 作者：李冬利
    /// 时间：2013-08-14
    /// </summary>
    public class HtmlUtils
    {
        /// <summary>
        /// 打开网页。传入有效的URL，返回HTML代码。
        /// </summary>
        /// <param name="url">有效的URL地址</param>
        /// <returns>返回HTML代码。如果发生错误，则返回错误消息。</returns>
        public static string OpenUrl(string url)
        {
            return OpenUrl(url, Encoding.Default);
        }

        /// <summary>
        /// 打开网页。传入有效的URL，返回HTML代码。
        /// </summary>
        /// <param name="url">有效的URL地址</param>
        /// <param name="encoding">网页编码格式</param>
        /// <returns>返回HTML代码。如果发生错误，则返回错误消息。</returns>
        public static string OpenUrl(string url, Encoding encoding)
        {
            return OpenUrl(url, encoding, new CookieContainer());
        }

        /// <summary>
        /// 打开网页。传入有效的URL，返回HTML代码。
        /// </summary>
        /// <param name="url">有效的URL地址</param>
        /// <param name="encoding">网页编码格式</param>
        /// <param name="container">存放Cookie集合。相同的CookieContainer实例，可以保持Session。</param>
        /// <returns>返回HTML代码。如果发生错误，则返回错误消息。</returns>
        public static string OpenUrl(string url, Encoding encoding, CookieContainer container)
        {
            if (encoding == null)
                encoding = Encoding.Default;

            if (container == null)
                container = new CookieContainer();

            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.CookieContainer = container;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                response.Cookies = container.GetCookies(request.RequestUri);

                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream, encoding))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
