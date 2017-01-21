using System.IO;

namespace Utils
{
    public class FileUtils
    {
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filePath"></param>
        public static void DownloadFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                FileInfo file = new FileInfo(filePath);

                // 写入到客户端  
                System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8"); //解决中文乱码
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" 
                    + System.Web.HttpContext.Current.Server.UrlEncode(file.Name)); //解决中文文件名乱码    
                System.Web.HttpContext.Current.Response.AddHeader("Content-length", file.Length.ToString());
                System.Web.HttpContext.Current.Response.ContentType = "appliction/octet-stream";
                System.Web.HttpContext.Current.Response.WriteFile(file.FullName);
                System.Web.HttpContext.Current.Response.End();
            }
        }

        /// <summary>
        /// 判断文件是否被使用
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsFileInUse(string fileName)
        {
            bool inUse = true;
            if (File.Exists(fileName))
            {
                FileStream fs = null;
                try
                {
                    fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                    inUse = false;
                }
                catch
                {
                    // exception....
                }
                finally
                {
                    if (fs != null)
                        fs.Close();
                }

                return inUse;//true表示正在使用,false没有使用
            }
            else
            {
                return false;//文件不存在,肯定没有被使用
            }
        }
    }
}
