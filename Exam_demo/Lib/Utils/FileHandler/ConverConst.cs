using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Utils
{
    public static class ConverConst
    {
        static ConverConst()
        {
            perpageFile = false;
            objectPathType = 1;
            toolpath = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["toolpath"];
            pdf2swfexe = toolpath + ConfigurationManager.AppSettings["pdf2swfexe"];
            swfrenderexe = toolpath + ConfigurationManager.AppSettings["swfrenderexe"];
            swfcombineexe = toolpath + ConfigurationManager.AppSettings["swfcombineexe"];
            swfview = toolpath + ConfigurationManager.AppSettings["swfview"];
            xpdfdir = toolpath + ConfigurationManager.AppSettings["xpdfdir"];
            supportFormat = ConfigurationManager.AppSettings["supportFormat"].Split(',').ToList();
            ThumbW = Convert.ToInt32(ConfigurationManager.AppSettings["ThumbW"]);
            ThumbH = Convert.ToInt32(ConfigurationManager.AppSettings["ThumbH"]);
        }

        /// <summary>
        /// 工具目录
        /// </summary>
        public static string toolpath;

        /// <summary>
        /// pdf转swf工具
        /// </summary>
        public static string pdf2swfexe;

        /// <summary>
        /// swf转缩略图工具
        /// </summary>
        public static string swfrenderexe;

        /// <summary>
        /// 合并swf工具
        /// </summary>
        public static string swfcombineexe;

        /// <summary>
        /// SWF翻页导航
        /// </summary>
        public static string swfview;

        /// <summary>
        /// pdf文件解析，可解析pdf的文本、图像等，可转换输出html，txt,及图像格式
        /// </summary>
        public static string xpdfdir;

        /// <summary>
        /// 输入类型，数据来源 0：表示来源文件夹，1表示来源Web Service
        /// </summary>
        public static int SourceType;

        /// <summary>
        /// 缩略图大小
        /// </summary>
        public static int ThumbH;
        public static int ThumbW;

        /// <summary>
        /// 目标路径类型 0表示指定方式，1表示默认原文件路径
        /// </summary>
        public static int objectPathType;

        /// <summary>
        /// 文档目标
        /// </summary>
        public static string documentDesPath;

        /// <summary>
        /// 图片目标
        /// </summary>
        public static string imageDesPath;

        /// <summary>
        /// 文档转换超时
        /// </summary>
        public static double documentConvertTimeout;

        /// <summary>
        /// 图片转换超时
        /// </summary>
        public static double imageConvertTimeout;

        /// <summary>
        /// 文档输出方式,每页输出为真，整个文档输出时为假
        /// </summary>
        public static bool perpageFile;

        /// <summary>
        /// 支持的格式
        /// </summary>
        public static List<string> supportFormat = new List<string>();
    }
}