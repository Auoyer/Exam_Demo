using Aspose.Words.Saving;
using Aspose.Words;
using System.IO;
using System.Text;
using Microsoft.Office.Core;
using Microsoft.Office.Interop;
using Aspose.Cells;
using Aspose.Slides;

namespace Utils
{
    public class SaveToPdf
    {
        /// <summary>
        /// Publisher导出类型声明
        /// </summary>
        #region Word转换pdf
        /// <summary>
        /// Word转换pdf
        /// </summary>
        /// <param name="sourcePah">源路径</param>
        /// <param name="targetPath">目标路径</param>
        /// <returns>bool</returns>
        public bool WordtoPdf(string sourcePah, string targetPath)
        {
            return this.ConvertWordToPdf(sourcePah, targetPath);
        }
        #endregion

        #region txt转换pdf
        /// <summary>
        /// Word转换pdf
        /// </summary>
        /// <param name="sourcePah">源路径</param>
        /// <param name="targetPath">目标路径</param>
        /// <returns>bool</returns>
        public bool Txttopdf(string sourcePah, string targetPath)
        {
            return this.ConvertTxtToPdf(sourcePah, targetPath);
        }
        #endregion

        #region Excel转换pdf
        /// <summary>
        /// Excel转换pdf
        /// </summary>
        /// <param name="sourcePath">源路径</param>
        /// <param name="targerPath">目标路径</param>
        /// <returns>bool</returns>
        public bool ExceltoPdf(string sourcePath, string targerPath)
        {
            return this.ConvertExcelToPdf(sourcePath, targerPath);
        }
        #endregion

        #region PowerPoint转换pdf
        /// <summary>
        /// PowerPoint转换pdf
        /// </summary>
        /// <param name="sourcePath">源路径</param>
        /// <param name="targerPath">目标路径</param>
        /// <returns>bool</returns>
        public bool PowerpointtoPdf(string sourcePath, string targerPath)
        {
            return this.ConvertPptToPdf(sourcePath, targerPath);
        }
        #endregion

        #region Word转换

        /// <summary>
        /// 使用aspose转换
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="targetPath"></param>
        /// <returns></returns>
        private bool ConvertWordToPdf(string sourcePath, string targetPath)
        {
            try
            {
                Aspose.Words.Document doc = new Aspose.Words.Document(sourcePath);
                doc.Save(targetPath, Aspose.Words.SaveFormat.Pdf);
            }
            finally
            {
            }
            return true;
        }
        #endregion

        #region Excel转换
        /// <summary>
        /// 使用aspose转换
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="targetPath"></param>
        /// <returns></returns>
        private bool ConvertExcelToPdf(string sourcePath, string targetPath)
        {
            try
            {
                Aspose.Cells.Workbook cel = new Aspose.Cells.Workbook(sourcePath);
                cel.Save(targetPath, Aspose.Cells.SaveFormat.Pdf);
            }
            finally
            {

            }
            return true;
        }
        #endregion

        #region PowerPoint转换
        public bool ConvertPptToPdf(string sourcePath, string targetPath)
        {
            try
            {
                if (true)
                {
                    Aspose.Slides.Presentation pres = new Aspose.Slides.Presentation(sourcePath);
                    pres.Save(targetPath, Aspose.Slides.Export.SaveFormat.Pdf);
                }
                else
                {
                    //Aspose.Slides.Pptx.PresentationEx pptx = new Aspose.Slides.Pptx.PresentationEx(sourcePath);
                    //pptx.Save(targetPath, Aspose.Slides.Export.SaveFormat.Pdf);
                }

            }
            finally
            {
            }
            return true;
        }
        #endregion

        #region txt转换
        public bool ConvertTxtToPdf(string sourcePath, string targetPath)
        {
            try
            {
                if (sourcePath.EndsWith("txt"))
                {
                    //System.IO.TextReader tr = new System.IO.StreamReader(sourcePath);

                    //Aspose.Pdf.Pdf pdf = new Aspose.Pdf.Pdf();
                    //Aspose.Pdf.Section sec = pdf.Sections.Add();
                    //Aspose.Pdf.Text txt = new Aspose.Pdf.Text(tr.ReadToEnd());
                    //sec.AddParagraph(txt);
                    //pdf.Save(targetPath);

                    Aspose.Words.LoadOptions optioins = new Aspose.Words.LoadOptions();
                    optioins.Encoding = Encoding.GetEncoding("gb2312");//编码格式  不指定的话 会出现乱码
                    Aspose.Words.Document oDoc = new Document(sourcePath, optioins);
                    oDoc.RemoveAllChildren();//清除掉  直接生成有些无法换行
                    DocumentBuilder oWordApplic = new DocumentBuilder(oDoc);
                    StreamReader sr = new StreamReader(sourcePath, Encoding.GetEncoding("gb2312"));//重新读取数据

                    string text = sr.ReadToEnd();
                    sr.Close();

                    sr.Dispose();
                    oWordApplic.Write(text);
                    oDoc.Save(targetPath, Aspose.Words.SaveFormat.Pdf);
                }
            }
            finally
            {
            }
            return true;
        }
        #endregion
    }
}
