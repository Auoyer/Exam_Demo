using Utils;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Utils
{
    public class ConvertFile : IDisposable
    {
        #region 私有字段

        /// <summary>
        /// 文件格式
        /// </summary>
        private string fileExt;

        /// <summary>
        /// 目标文件路径
        /// </summary>
        private string objectPath;

        /// <summary>
        /// 转换文件路径
        /// </summary>
        private string convertFilePath;

        /// <summary>
        /// 缩略图文件路径
        /// </summary>
        private string thumbnailFilePath;

        /// <summary>
        /// pdf文件路径
        /// </summary>
        private string pdfFilePath;

        /// <summary>
        /// 原文件路径
        /// </summary>
        private string sourceFilePath;

        /// <summary>
        /// 文件名不含扩展
        /// </summary>
        private string fileNameWithoutExtension;

        /// <summary>
        /// 文件名（组成：文件格式_文件名）
        /// </summary>
        private string fileNameEx;

        /// <summary>
        /// 超时时间,单位：分钟
        /// </summary>
        private double convertTimeout;

        /// <summary>
        /// 活动时间
        /// </summary>
        private DateTime activeDateTime;

        #endregion

        #region 公有字段

        /// <summary>
        /// 文件大小
        /// </summary>
        public long fileSize;

        public delegate void ConvertLogHandler(string message);
        public event ConvertLogHandler ConvertLog;
        public delegate void ConvertCompletedHandler(string filename, Enumerate.ConvertStatus status);
        public event ConvertCompletedHandler ConvertCompleted;
        Thread convertThread;
        Process convertProcess;

        /// <summary>
        /// 转换开始时间
        /// </summary>
        public DateTime startTime;

        /// <summary>
        /// 转换结束时间
        /// </summary>
        public DateTime endTime;

        /// <summary>
        /// 转换结果信息
        /// </summary>
        public string convertResult;

        /// <summary>
        /// 转换结果
        /// </summary>
        public bool status;

        /// <summary>
        /// 是否完成
        /// </summary>
        public bool IsFinished = false;

        /// <summary>
        /// 当前状态
        /// </summary>
        public string CurrentStateMsg;

        /// <summary>
        /// 文件名称
        /// </summary>
        public string fileName;
        public string id;

        #endregion

        #region 公有方法

        /// <summary>
        /// 转换文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public void Convert(string filePath)
        {
            startTime = System.DateTime.Now;
            endTime = startTime;
            activeDateTime = startTime;
            sourceFilePath = filePath.ToLower();
            fileSize = 0;
            convertTimeout = 0;
            if (CheckFile(sourceFilePath))
            {
                fileSize = new FileInfo(sourceFilePath).Length;
                fileExt = System.IO.Path.GetExtension(sourceFilePath).ToLower();
                fileName = System.IO.Path.GetFileName(sourceFilePath);
                fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(sourceFilePath).Trim();
                fileNameEx = fileExt.Replace(".", "") + "_" + fileNameWithoutExtension;
                if (GenerateFilePath())
                {
                    DoConvert();
                }
            }
            convertResult = CurrentStateMsg;
        }

        /// <summary>
        /// pdf转swf
        /// </summary>
        /// <param name="exename">获取或设置要启动的应用程序或文档</param>
        /// <param name="batstr">获取或设置启动应用程序时要使用的一组命令行参数</param>
        /// <returns>bool</returns>
        public bool ExeBat(string exename, string command)
        {
            convertProcess = new System.Diagnostics.Process();
            convertProcess.StartInfo.FileName = exename;
            convertProcess.StartInfo.Arguments = command;
            // 禁用操作系统外壳程序    
            convertProcess.StartInfo.UseShellExecute = false;
            convertProcess.StartInfo.CreateNoWindow = true;
            convertProcess.StartInfo.RedirectStandardOutput = true;
            convertProcess.Start();
            OutPutMsg(convertProcess);
            convertProcess.WaitForExit();
            convertProcess.Close();
            return true;
        }

        /// <summary>
        /// 检查超时
        /// </summary>
        public void CheckTimeout()
        {
            if ((!IsFinished) && (convertTimeout > 0))
            {
                TimeSpan t = System.DateTime.Now - activeDateTime;
                if (t.TotalMinutes > convertTimeout)
                {
                    Stop();
                    Log("converstate:转换超时!" + sourceFilePath + "转换超时. ");
                }
            }
        }

        /// <summary>
        /// 停止转换
        /// </summary>
        public void Stop()
        {
            if (convertProcess != null)
            {
                try
                {
                    convertProcess.Kill();
                    convertProcess = null;
                }
                catch
                {
                }
            }
            Thread.Sleep(200);
            if (convertThread != null)
            {
                try
                {
                    convertThread.Abort();
                    convertThread = null;
                }
                catch
                {
                }
                IsFinished = true;
            }
            Thread.Sleep(200);
        }

        /// <summary>
        /// 资源释放
        /// </summary>
        public void Dispose()
        {
            Stop();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 生成文件路径
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool GenerateFilePath()
        {
            bool match = false;
            if (!ConverConst.supportFormat.Contains(fileExt))
            {
                IsFinished = true;
                Log("converstate:不支持的文件格式!" + sourceFilePath);
                return false;
            }
            if (ConverConst.objectPathType == 1)
            {
                objectPath = System.IO.Path.GetDirectoryName(sourceFilePath);
                if (!objectPath.EndsWith("\\"))
                {
                    objectPath = objectPath + "\\";
                }
                convertFilePath = objectPath;
                thumbnailFilePath = objectPath;
                pdfFilePath = objectPath;
                match = true;
                IsFinished = !match;
            }
            else
            {
                switch (fileExt)
                {
                    case ".txt":
                    case ".doc":
                    case ".docx":
                    case ".xls":
                    case ".xlsx":
                    case ".ppt":
                    case ".pptx":
                    case ".pdf":
                    case ".swf":
                        match = true;
                        convertTimeout = ConverConst.documentConvertTimeout;
                        objectPath = ConverConst.documentDesPath + DateTime.Now.ToString("yyyyMMdd");
                        break;
                    case ".bmp":
                    case ".gif":
                    case ".jpeg":
                    case ".jpg":
                    case ".png":
                        match = true;
                        convertTimeout = ConverConst.imageConvertTimeout;
                        objectPath = ConverConst.imageDesPath + DateTime.Now.ToString("yyyyMMdd");
                        break;
                    default:
                        objectPath = "";
                        break;
                }
                if (match)
                {
                    if (!Directory.Exists(objectPath))
                    {
                        Directory.CreateDirectory(objectPath);
                    }
                    objectPath = objectPath + "\\";
                    convertFilePath = objectPath + "ConvertFile\\" + fileNameEx;
                    thumbnailFilePath = objectPath + "Thumbnail\\" + fileNameEx;
                    pdfFilePath = objectPath + "Pdf\\" + fileNameEx;
                    if (!Directory.Exists(convertFilePath))
                    {
                        Directory.CreateDirectory(convertFilePath);
                    }
                    if (!Directory.Exists(thumbnailFilePath))
                    {
                        Directory.CreateDirectory(thumbnailFilePath);
                    }
                    if (!Directory.Exists(pdfFilePath))
                    {
                        Directory.CreateDirectory(pdfFilePath);
                    }
                    convertFilePath = convertFilePath + "\\";
                    thumbnailFilePath = thumbnailFilePath + "\\";
                    pdfFilePath = pdfFilePath + "\\";
                    if (ConverConst.SourceType == 0)
                    {
                        System.IO.File.Copy(sourceFilePath, convertFilePath + fileName, true);
                        System.IO.File.Delete(sourceFilePath);
                        sourceFilePath = convertFilePath + fileName;
                    }
                }
                IsFinished = !match;
            }
            return match;
        }

        /// <summary>
        /// 检查文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>是否有效</returns>
        private bool CheckFile(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                IsFinished = true;
                Log("converstate:未找到文件 " + filePath);
                return false;
            }
            else
            {
                //判断文件是否被使用
                if (FileUtils.IsFileInUse(filePath) == true)
                {
                    IsFinished = true;
                    Log("converstate:文件正在使用 " + filePath);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 执行转换
        /// </summary>
        private void DoConvert()
        {
            try
            {
                //默认转换状态
                status = false;
                Log("converstate:开始转换 " + sourceFilePath);
                string pdffilename = "";           //pdf文件名
                pdffilename = pdfFilePath + fileNameWithoutExtension + ".pdf";
                try
                {
                    switch (fileExt)
                    {
                        case ".txt":
                            //txt转pdf
                            status = txttopdf(sourceFilePath, pdffilename);
                            break;
                        case ".doc":
                            //doc转pdf
                            status = wordtopdf(sourceFilePath, pdffilename);
                            break;
                        case ".docx":
                            //docx转pdf
                            status = wordtopdf(sourceFilePath, pdffilename);
                            break;
                        case ".xls":
                            //xls转pdf
                            status = exceltopdf(sourceFilePath, pdffilename);
                            break;
                        case ".xlsx":
                            //xlsx转pdf
                            status = exceltopdf(sourceFilePath, pdffilename);
                            break;
                        case ".ppt":
                            //ppt转pdf
                            status = ppttopdf(sourceFilePath, pdffilename);
                            break;
                        case ".pptx":
                            //pptx转pptx
                            status = ppttopdf(sourceFilePath, pdffilename);
                            break;
                        case ".pdf":
                            if (!System.IO.File.Exists(pdffilename))
                            {
                                System.IO.File.Copy(sourceFilePath, pdffilename);
                            }
                            break;
                        case ".swf":
                            string swfFileName = fileNameWithoutExtension;
                            if (!System.IO.File.Exists(convertFilePath + swfFileName + ".swf"))
                            {
                                System.IO.File.Copy(sourceFilePath, convertFilePath + swfFileName + ".swf");
                            }
                            break;
                        default:
                            break;
                    }
                    string swffilename = convertFilePath + fileNameWithoutExtension + ".swf";
                    if (File.Exists(pdffilename))
                    {
                        status = pdftoswf(pdffilename, swffilename);
                    }

                    if (status)
                    {
                        ConvertImage();

                        // 合并转换后的swf文件和swf导航文件
                        //swftopager(swffilename);

                        Log("converstate:转换成功!" + sourceFilePath + "转换耗时：" + (System.DateTime.Now - startTime).TotalSeconds.ToString() + "秒。");

                        try
                        {
                            if (Path.GetExtension(sourceFilePath).ToLower() != ".pdf")
                            {
                                File.Delete(pdffilename);
                                Log("converstate:删除PDF文件成功!" + pdffilename);
                            }
                        }
                        catch (Exception e)
                        {
                            Log("converstate:删除PDF文件失败!" + pdffilename + e.Message);
                        }

                        if (ConvertCompleted != null)
                            ConvertCompleted(Path.GetFileNameWithoutExtension(sourceFilePath), Enumerate.ConvertStatus.Success);
                    }
                    else
                    {
                        Log("converstate:转换失败!" + sourceFilePath);

                        if (ConvertCompleted != null)
                            ConvertCompleted(Path.GetFileNameWithoutExtension(sourceFilePath), Enumerate.ConvertStatus.Failed);
                    }
                }
                catch (Exception e)
                {
                    Log("converstate:转换失败!" + sourceFilePath + e.Message);

                    if (ConvertCompleted != null)
                        ConvertCompleted(Path.GetFileNameWithoutExtension(sourceFilePath), Enumerate.ConvertStatus.Failed);
                }
                convertResult = CurrentStateMsg;
            }
            finally
            {
                IsFinished = true;
                endTime = DateTime.Now;
            }
        }

        private void ConvertImage()
        {
            switch (fileExt)
            {
                case ".txt":
                case ".doc":
                case ".docx":
                case ".xls":
                case ".xlsx":
                case ".ppt":
                case ".pptx":
                case ".pdf":
                case ".swf":
                    string swffilename = convertFilePath + fileNameWithoutExtension + ".swf";
                    if (File.Exists(swffilename))
                    {
                        status = swftojpg(swffilename, thumbnailFilePath + fileNameWithoutExtension + ".png");
                    }
                    break;
                case ".jpg":
                case ".jpeg":
                case ".png":
                case ".gif":
                case ".bmp":
                    //图片转jpg
                    if (File.Exists(sourceFilePath))
                    {
                        //生成缩略图
                        string newFile = Path.Combine(Path.GetDirectoryName(thumbnailFilePath), fileNameWithoutExtension + ".jpg");
                        status = CreateThumb(sourceFilePath, newFile, ConverConst.ThumbW, ConverConst.ThumbH);
                    }
                    break;
                default:
                    objectPath = "";
                    break;
            }
        }

        private void Log(string message)
        {
            activeDateTime = System.DateTime.Now;
            CurrentStateMsg = message;
            if (ConvertLog != null)
            {
                ConvertLog(message);
            }
        }

        #region wordtopdf
        /// <summary>
        /// word转pdf
        /// </summary>
        /// <param name="resourcefile">源文档路径</param>
        /// <param name="desfile">目标文档路径</param>
        private bool wordtopdf(string resourcefile, string desfile) //word转pdf
        {
            SaveToPdf t = new SaveToPdf();
            Log(string.Format("{0}：Converting    {1} To {2}", System.DateTime.Now.ToString(), Path.GetFileName(resourcefile), Path.GetFileName(desfile)));

            bool r = t.WordtoPdf(resourcefile, desfile);

            if (r)
                Log(System.DateTime.Now.ToString() + "：Success       " + desfile);
            else
                Log(System.DateTime.Now.ToString() + "：Fail          " + desfile);

            return r;
        }
        #endregion

        #region txttopdf
        /// <summary>
        /// word转pdf
        /// </summary>
        /// <param name="resourcefile">源文档路径</param>
        /// <param name="desfile">目标文档路径</param>
        private bool txttopdf(string resourcefile, string desfile) //word转pdf
        {
            SaveToPdf t = new SaveToPdf();
            //Log(string.Format("{0}：Converting    {1} To {2}", System.DateTime.Now.ToString(), Path.GetFileName(resourcefile), Path.GetFileName(desfile)));

            bool r = t.Txttopdf(resourcefile, desfile);

            if (r)
                Log(System.DateTime.Now.ToString() + "：Success       " + desfile);
            else
                Log(System.DateTime.Now.ToString() + "：Fail          " + desfile);

            return r;
        }
        #endregion

        #region exceltopdf
        /// <summary>
        /// excel转pdf
        /// </summary>
        /// <param name="resourcefile">源文档路径</param>
        /// <param name="desfile">目标文档路径</param>
        private bool exceltopdf(string resourcefile, string desfile) //excel转pdf
        {
            SaveToPdf t = new SaveToPdf();
            bool r = t.ExceltoPdf(resourcefile, desfile);

            Log(string.Format("{0}：Converting    {1} To {2}", System.DateTime.Now.ToString(), Path.GetFileName(resourcefile), Path.GetFileName(desfile)));

            if (r)
                Log(System.DateTime.Now.ToString() + "：Success       " + desfile);
            else
                Log(System.DateTime.Now.ToString() + "：Fail          " + desfile);

            return r;
        }
        #endregion

        #region ppttopdf
        /// <summary>
        /// ppt转pdf
        /// </summary>
        /// <param name="resourcefile">源文档路径</param>
        /// <param name="desfile">目标文档路径</param>
        private bool ppttopdf(string resourcefile, string desfile) //ppt转pdf
        {
            SaveToPdf t = new SaveToPdf();
            bool r = t.PowerpointtoPdf(resourcefile, desfile);

            if (r)
                Log(System.DateTime.Now.ToString() + "：成功转换" + desfile);
            else
                Log(System.DateTime.Now.ToString() + "：转换失败" + desfile);

            return r;
        }
        #endregion

        #region pdftoswf
        /// <summary>
        /// pdf转成swf
        /// </summary>
        /// <param name="resourcefile">源文档路径</param>
        /// <param name="desfile">目标文档路径</param>
        private bool pdftoswf(string resourcefile, string desfile)
        {
            bool r = false;

            FileInfo fileInfo = new FileInfo(resourcefile);
            if (fileInfo.Exists)
            {
                string strCommand = "";
                if (ConverConst.perpageFile)
                {
                    strCommand = strCommand + " -j 100";//Set quality of embedded jpeg pictures to quality. 0 is worst (small), 100 is best (big). (default:85)
                    strCommand = strCommand + " -t";//The resulting SWF file will not turn pages automatically.
                    strCommand = strCommand + " -s zoom=55";
                    strCommand = strCommand + " -s disablelinks ";
                    strCommand = strCommand + " -f "; //Store full fonts in SWF 
                    //strCommand = strCommand + string.Format(" -T {0}", ConverConst.flashVersion);//flash version
                    strCommand = strCommand + string.Format(" \"{0}\" ", resourcefile);
                    strCommand = strCommand + " -o \"" + convertFilePath + "%.swf\"";
                }
                else
                {
                    //strCommand = strCommand + string.Format(" -T {0}", ConverConst.flashVersion);//flash version
                    strCommand = strCommand + string.Format(" \"{0}\" ", resourcefile);
                    strCommand = strCommand + " -o \"" + desfile + "\" -t -T 9";
                }
                if (fileInfo.Length > 1024 * 1024)
                {
                    //由于文件大太或者文件图形过多而引起的异常
                    //pdf2swf 1.pdf -o 1.swf -f -T 9 -G -s poly2bitmap    只对文件中的图形转成点阵
                    // pdf2swf 1.pdf -o 1.swf -f -T 9 -G -s bitmap    对everything 转成点阵
                    //pdf2swf 1.pdf -o 1.swf -f -T 9 -G -s enablezlib  bitmap    对everything 转成点阵，并通过enablezlib压缩

                    //strCommand = strCommand + " -s enablezlib bitmap";
                    strCommand = strCommand + " -s poly2bitmap -S ";
                }
                strCommand = strCommand + string.Format(" -s languagedir=\"{0}\" ", ConverConst.xpdfdir);
                //string strCommand = string.Format("\"{0}\" -o \"{1}\" -s languagedir=\"{2}\" -T 9 -s poly2bitmap", resourcefile, desfile, ConverConst.xpdfdir);
                Log(string.Format("{0}：Converting    {1} To {2}", System.DateTime.Now.ToString(), Path.GetFileName(resourcefile), Path.GetFileName(desfile)));

                r = ExeBat(GetPath(ConverConst.pdf2swfexe), strCommand);
            }
            else
            {
                Log(System.DateTime.Now.ToString() + resourcefile + " 文件不存在！");
            }

            if (!File.Exists(desfile)) { r = false; }

            if (r)
                Log(System.DateTime.Now.ToString() + "：pdf转swf完成!");
            else
                Log(System.DateTime.Now.ToString() + "：pdf转swf完成!");

            return r;
        }
        #endregion

        #region swf生成缩略图
        /// <summary>
        /// swf生成缩略图
        /// </summary>
        /// <param name="resourcefile">源文档路径</param>
        /// <param name="desfile">目标文档路径</param>
        private bool swftojpg(string resourcefile, string desfile)
        {
            string strCommand = string.Format("\"{0}\" -o \"{1}\"", resourcefile, desfile);

            Log(string.Format("{0}：Converting    {1} To {2}", System.DateTime.Now.ToString(), Path.GetFileName(resourcefile), Path.GetFileName(desfile)));
            bool r = ExeBat(GetPath(ConverConst.swfrenderexe), strCommand);

            if (r)
            {
                Log(System.DateTime.Now.ToString() + "：Success       " + desfile);
                CreateThumb(desfile);
            }
            else
                Log(System.DateTime.Now.ToString() + "：Fail          " + desfile);

            return r;
        }
        #endregion

        #region swf添加翻页导航
        /// <summary>
        /// swf添加翻页导航
        /// </summary>
        /// <param name="resourcefile">文档目标路径跟远路径一致</param>
        /// <returns></returns>
        private bool swftopager(string resourcefile)
        {
            string strCommand = string.Format("\"{0}\" viewport=\"{1}\" -o \"{2}\"", ConverConst.swfview, resourcefile, resourcefile);

            Log(string.Format("{0}：添加翻页导航Converting    {1} To {2}", System.DateTime.Now.ToString(), Path.GetFileName(resourcefile), Path.GetFileName(resourcefile)));
            bool r = ExeBat(GetPath(ConverConst.swfcombineexe), strCommand);

            if (r)
            {
                Log(System.DateTime.Now.ToString() + "：添加翻页导航成功       " + resourcefile);
            }
            else
                Log(System.DateTime.Now.ToString() + "：添加翻页导航失败          " + resourcefile);

            return r;
        }
        #endregion

        #region 根据源图生成图片
        /// <summary>
        /// 创建缩略图
        /// </summary>
        /// <param name="desFile">缩略图路径</param>
        private bool CreateThumb(string desFile)
        {
            string newFile = Path.Combine(Path.GetDirectoryName(desFile), fileNameWithoutExtension + ".jpg");
            return CreateImage(desFile, newFile, ConverConst.ThumbW, ConverConst.ThumbH);
        }

        /// <summary>
        /// 创建指定大小的图片
        /// </summary>
        /// <param name="desFile">缩略图路径</param>
        /// <param name="desFile">新文件名称【包括路径】</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        private bool CreateThumb(string desFile, string newFile, int width, int height)
        {
            return CreateImage(desFile, newFile, width, height);
        }

        /// <summary>
        /// 创建指定大小的预览图
        /// </summary>
        /// <param name="desFile">源图路径</param>
        /// <param name="desFile">新文件名称【包括路径】</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        private bool CreatePreview(string desFile, string newFile, int width, int height)
        {
            return CreateImage(desFile, newFile, width, height);
        }

        /// <summary>
        /// 创建指定大小的图片
        /// </summary>
        /// <param name="desFile">缩略图路径</param>
        /// <param name="desFile">新文件名称【包括路径】</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        private bool CreateImage(string desFile, string newFile, int width, int height)
        {
            bool result = false;
            if (File.Exists(desFile))
            {
                Log(string.Format("{0}：Converting    {1} To {2}", System.DateTime.Now.ToString(), Path.GetFileName(desFile), fileNameWithoutExtension + ".jpg"));
                ThumbnailHelper imgHelper = new ThumbnailHelper();
                imgHelper.SourceImagePath = desFile;
                imgHelper.ThumbnailImageHeight = height;
                imgHelper.ThumbnailImageWidth = width;
                imgHelper.ThumbnailImagePath = newFile;
                result = imgHelper.ToThumbnailImage();
                try
                {
                    File.Delete(desFile);
                    Log("converstate:删除预览图" + desFile);
                }
                catch (Exception e)
                {
                    Log("converstate:删除预览图失败" + desFile + e.Message);

                }
            }
            return result;
        }
        #endregion

        #region 获取文件全路径
        ///<summary>
        /// 获取文件全路径
        ///</summary>
        ///<param name="path">文件路径</param>
        ///<returns>文件详细路径</returns>
        static string GetPath(string path)
        {
            return string.Format("\"{0}\"", path);
        }
        #endregion

        void OutPutMsg(System.Diagnostics.Process pro)
        {
            //异步获取命令行内容 
            pro.BeginOutputReadLine();
            // 为异步获取订阅事件    
            pro.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(pro_OutputDataReceived);

        }

        void pro_OutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            if (String.IsNullOrEmpty(e.Data) == false)
                Log(e.Data);
        }

        #endregion
    }
}
