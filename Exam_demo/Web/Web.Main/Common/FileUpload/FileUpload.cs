using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace Web
{
    public class FileUpload
    {
        private HttpContextBase context = null;

        public string Type { get; set; }
        public string UploadPath { get; set; }
        public int MaxLength { get; set; }


        public string PostName { get; private set; }
        public string SaveName { get; private set; }
        public int FileLength { get; private set; }
        public string ExtName { get; private set; }
        public string SavePath { get; private set; }

        //private static readonly string _uploadPath = "~/upload/";

        //public static string UploadPath { get { return _uploadPath; } }

        public FileUpload(HttpContextBase httpContext)
            : this()
        {
            context = httpContext;
        }

        public FileUpload()
        {
            Type = "txt|rar|zip|jpg|jpeg|png|gif|bmp";
            PostName = string.Empty;
            SaveName = string.Empty;
            FileLength = 0;
            UploadPath = string.Empty;
            MaxLength = 200;
            ExtName = string.Empty;
            if (context == null)
                context = new HttpContextWrapper(HttpContext.Current);
        }

        public FileUpload(string type)
        {
            Type = type;
            PostName = string.Empty;
            SaveName = string.Empty;
            FileLength = 0;
            UploadPath = string.Empty;
            MaxLength = 200;
            ExtName = string.Empty;
            if (context == null)
                context = new HttpContextWrapper(HttpContext.Current);
        }

        private string CreateForderName()
        {
            return DateTime.Now.ToString("yyyyMM");
        }

        private string CreateFileName()
        {
            //return DateTime.Now.ToString("yyyyMMssHHmmss");
            return String.Format("{0:yyyyMMssHHmmss}.{1}", DateTime.Now, ExtName);
        }

        /// <summary>
        /// 保存上传文件-本地
        /// </summary>
        public void Save()
        {
            HttpPostedFileBase upfile = context.Request.Files[0];
            PostFile file = new PostFile(upfile);
            Save(file);
        }

        /// <summary>
        /// 保存上传文件-本地及网络文件夹
        /// （操作指南用）
        /// </summary>
        public void Save2()
        {
            HttpPostedFileBase upfile = context.Request.Files[0];
            PostFile file = new PostFile(upfile);
            Save2(file);
        }

        /// <summary>
        /// 保存上传文件-网络文件夹
        /// （操作指南用）
        /// </summary>
        public void Save3()
        {
            HttpPostedFileBase upfile = context.Request.Files[0];
            PostFile file = new PostFile(upfile);
            Save3(file);
        }

        public void Save(PostFile postFile)
        {
            if (string.IsNullOrEmpty(Type))
                throw new ArgumentNullException("Type");
            if (string.IsNullOrEmpty(UploadPath))
                throw new ArgumentNullException("UploadPath");
            if (MaxLength < 0)
                throw new ArgumentOutOfRangeException("ManLength");
            PostName = postFile.Name.Substring(postFile.Name.LastIndexOf('\\') + 1);
            string extName = PostName.Substring(PostName.LastIndexOf('.') + 1);
            if (string.IsNullOrEmpty(extName))
                throw new Exception("无扩展名信息,该文件不被支持.");
            else
            {
                extName = extName.ToLower();
                string[] fs = Type.Split('|');
                if (!fs.Any(f => f == extName))
                    throw new Exception(string.Format("扩展名{0}不被系统支持,请重新上传.", extName));
                ExtName = extName;
            }
            FileLength = postFile.Buffer.Length;
            if (FileLength > MaxLength * 1024)
                throw new Exception(string.Format("上传文件过大,系统限制为{0}KB,请重写上传.", MaxLength));
            string rootPath = context.Server.MapPath(UploadPath);
            if (!Directory.Exists(rootPath))
                throw new Exception("系统错误:不存在该上传路径:" + UploadPath);
            SavePath = CreateForderName();
            //string savePath = rootPath + '\\' + SavePath;
            string savePath = Path.Combine(rootPath, SavePath);
            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);
            SaveName = CreateFileName();
            //postFile.SaveAs(savePath + "\\" + SaveName);
            postFile.SaveAs(Path.Combine(savePath, SaveName));
        }

        public void Save2(PostFile postFile)
        {
            if (string.IsNullOrEmpty(Type))
                throw new ArgumentNullException("Type");
            if (string.IsNullOrEmpty(UploadPath))
                throw new ArgumentNullException("UploadPath");
            if (MaxLength < 0)
                throw new ArgumentOutOfRangeException("ManLength");
            PostName = postFile.Name.Substring(postFile.Name.LastIndexOf('\\') + 1);
            string extName = PostName.Substring(PostName.LastIndexOf('.') + 1);
            if (string.IsNullOrEmpty(extName))
                throw new Exception("无扩展名信息,该文件不被支持.");
            else
            {
                extName = extName.ToLower();
                string[] fs = Type.Split('|');
                if (!fs.Any(f => f == extName))
                    throw new Exception(string.Format("扩展名{0}不被系统支持,请重新上传.", extName));
                ExtName = extName;
            }
            FileLength = postFile.Buffer.Length;
            if (FileLength > MaxLength * 1024)
                throw new Exception(string.Format("上传文件过大,系统限制为{0}KB,请重写上传.", MaxLength));

            string rootPath = context.Server.MapPath(UploadPath);
            string netFilePath = ConfigurationManager.AppSettings["FileUploadAddress"]; // "//10.10.21.74/upload/"

            if (!Directory.Exists(netFilePath) || !Directory.Exists(rootPath))
                throw new Exception("系统错误:不存在该上传路径:" + UploadPath + "或" + rootPath);

            string savePath2 = Path.Combine(rootPath);
            string savePath = Path.Combine(netFilePath);

            SaveName = CreateFileName();
            postFile.SaveAs(Path.Combine(savePath2, SaveName));
            postFile.SaveAs(Path.Combine(savePath, SaveName));
        }


        public void Save3(PostFile postFile)
        {
            if (string.IsNullOrEmpty(Type))
                throw new ArgumentNullException("Type");
            if (string.IsNullOrEmpty(UploadPath))
                throw new ArgumentNullException("UploadPath");
            if (MaxLength < 0)
                throw new ArgumentOutOfRangeException("ManLength");
            PostName = postFile.Name.Substring(postFile.Name.LastIndexOf('\\') + 1);
            string extName = PostName.Substring(PostName.LastIndexOf('.') + 1);
            if (string.IsNullOrEmpty(extName))
                throw new Exception("无扩展名信息,该文件不被支持.");
            else
            {
                extName = extName.ToLower();
                string[] fs = Type.Split('|');
                if (!fs.Any(f => f == extName))
                    throw new Exception(string.Format("扩展名{0}不被系统支持,请重新上传.", extName));
                ExtName = extName;
            }
            FileLength = postFile.Buffer.Length;
            if (FileLength > MaxLength * 1024)
                throw new Exception(string.Format("上传文件过大,系统限制为{0}KB,请重写上传.", MaxLength));

            string netFilePath = ConfigurationManager.AppSettings["FileUploadAddress"]; // "//10.10.21.74/upload/"

            if (!Directory.Exists(netFilePath))
                throw new Exception("系统错误:不存在该上传路径:" + netFilePath);

            string savePath = Path.Combine(netFilePath);
            SaveName = CreateFileName();
            postFile.SaveAs(Path.Combine(savePath, SaveName));
        }
    }
}