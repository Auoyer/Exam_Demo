using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web
{
    public class FileUpLoadKeys
    {
        private static readonly string _uploadPath = "/UploadFiles/";
        private static readonly string _imgUploadPath = "/ImgUploadFiles";

        public static string UploadPath { get { return _uploadPath; } }

        public static string ImgUploadPath { get { return _imgUploadPath; } }

        public const string TypeImg = "Img";

        public const string TypeFile = "File";
    }
}