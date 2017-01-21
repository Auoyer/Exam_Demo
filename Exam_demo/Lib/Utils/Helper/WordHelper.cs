using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using Microsoft.Office.Interop.Word;
using Aspose.Words;
using VM;

namespace Utils
{
    /// <summary>
    /// Excel操作类
    /// </summary>
    public class WordHelper
    {
        public static string CreateWordFile(CaseVM model,string path)
        {
            string message = "";
            try
            {
                Object Nothing = System.Reflection.Missing.Value;
                //Directory.CreateDirectory("C:/CNSI");  //创建文件所在目录
                //string name = "CNSI_" + DateTime.Now.ToLongDateString() + ".doc";
                object filename = path;  //文件保存路径
                //创建Word文档
                Microsoft.Office.Interop.Word.Application WordApp = new Microsoft.Office.Interop.Word.ApplicationClass();
                Microsoft.Office.Interop.Word.Document WordDoc = WordApp.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);
                
                //添加页眉
                WordApp.ActiveWindow.View.Type = WdViewType.wdOutlineView;
                WordApp.ActiveWindow.View.SeekView = WdSeekView.wdSeekPrimaryHeader;
                WordApp.ActiveWindow.ActivePane.Selection.InsertAfter("[国泰安理财规划大赛案例]");
                WordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;//设置右对齐
                WordApp.ActiveWindow.View.SeekView = WdSeekView.wdSeekMainDocument;//跳出页眉设置

                WordApp.Selection.ParagraphFormat.LineSpacing = 15f;//设置文档的行间距

                //移动焦点并换行
                object count = 14;
                object WdLine = Microsoft.Office.Interop.Word.WdUnits.wdLine;//换一行;
                WordApp.Selection.MoveDown(ref WdLine, ref count, ref Nothing);//移动焦点

                // 插入段落
                Microsoft.Office.Interop.Word.Paragraph para;
                para = WordDoc.Content.Paragraphs.Add(ref Nothing);


                para.Range.Text = "案例信息";
                //para.Range.Font.Size = 20;
                para.Range.Font.Bold = 2;  
                para.Range.Font.Color = WdColor.wdColorRed;  
                para.Range.Font.Italic = 2;  
                para.Range.InsertParagraphAfter();
                //WordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;//水平居中

                para.Range.Text = "客户姓名：" + model.CustomerName;
                //para.Range.Font.Size = 12;
                para.Range.Font.Bold = 1;
                para.Range.Font.Color = WdColor.wdColorBlack;
                para.Range.Font.Italic = 0;  
                para.Range.InsertParagraphAfter();

                para.Range.Text = "身份证号：" + model.IDNum;
                para.Range.InsertParagraphAfter();

                para.Range.Text = "理财类型：" + model.strFinancialType;
                para.Range.InsertParagraphAfter();

                para.Range.Text = "客户背景：" + model.CustomerStory;
                para.Range.InsertParagraphAfter();

                WordApp.Selection.MoveDown(ref WdLine, ref count, ref Nothing);//移动焦点
                WordApp.Selection.MoveDown(ref WdLine, ref count, ref Nothing);//移动焦点

                para.Range.Text = "考核点";
                para.Range.Font.Bold = 2;
                para.Range.Font.Color = WdColor.wdColorRed;
                para.Range.Font.Italic = 2;
                para.Range.InsertParagraphAfter();
                WordApp.Selection.MoveDown(ref WdLine, ref count, ref Nothing);//移动焦点

                para.Range.Font.Bold = 1;
                para.Range.Font.Color = WdColor.wdColorBlack;
                para.Range.Font.Italic = 0;  
                //文档中创建表格
                Microsoft.Office.Interop.Word.Table newTable = WordDoc.Tables.Add(WordApp.Selection.Range, 200, 4, ref Nothing, ref Nothing);
                //设置表格样式
                newTable.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleThickThinLargeGap;
                newTable.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                newTable.Columns[1].Width = 50f;
                newTable.Columns[2].Width = 50f;
                newTable.Columns[3].Width = 160f;
                newTable.Columns[4].Width = 160f;

                //填充表格内容
                newTable.Cell(1, 1).Range.Text = "序号";
                newTable.Cell(1, 1).Range.Bold = 2;//设置单元格中字体为粗体
                //合并单元格
                //newTable.Cell(1, 1).Merge(newTable.Cell(1, 3));
                WordApp.Selection.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;//垂直居中
                WordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;//水平居中
                newTable.Cell(1, 2).Range.Text = "类型";
                newTable.Cell(1, 2).Range.Bold = 2;//设置单元格中字体为粗体
                //合并单元格
                //newTable.Cell(1, 1).Merge(newTable.Cell(1, 3));
                WordApp.Selection.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;//垂直居中
                WordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;//水平居中
                newTable.Cell(1, 3).Range.Text = "考点";
                newTable.Cell(1, 3).Range.Bold = 2;//设置单元格中字体为粗体
                //合并单元格
                //newTable.Cell(1, 1).Merge(newTable.Cell(1, 3));
                WordApp.Selection.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;//垂直居中
                WordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;//水平居中
                newTable.Cell(1, 4).Range.Text = "答案";
                newTable.Cell(1, 4).Range.Bold = 2;//设置单元格中字体为粗体
                //合并单元格
                //newTable.Cell(1, 1).Merge(newTable.Cell(1, 3));
                WordApp.Selection.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;//垂直居中
                WordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;//水平居中

                int i = 1;
                foreach (var item in model.ExamPointAnswer)
                {
                    i++;
                    //填充表格内容
                    newTable.Cell(i, 1).Range.Text =(i-1).ToString();
                    WordApp.Selection.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    newTable.Cell(i, 2).Range.Text = item.strExamType;
                    WordApp.Selection.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    newTable.Cell(i, 3).Range.Text = item.strExamPoint;
                    WordApp.Selection.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    newTable.Cell(i, 4).Range.Text = item.Answer;
                    WordApp.Selection.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                }





                ////填充表格内容
                //newTable.Cell(2, 1).Range.Text = "产品基本信息";
                //newTable.Cell(2, 1).Range.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorDarkBlue;//设置单元格内字体颜色
                ////合并单元格
                //newTable.Cell(2, 1).Merge(newTable.Cell(2, 3));
                //WordApp.Selection.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                ////填充表格内容
                //newTable.Cell(3, 1).Range.Text = "品牌名称：";
                //newTable.Cell(3, 2).Range.Text = "BrandName";
                ////纵向合并单元格
                //newTable.Cell(3, 3).Select();//选中一行
                //object moveUnit = Microsoft.Office.Interop.Word.WdUnits.wdLine;
                //object moveCount = 5;
                //object moveExtend = Microsoft.Office.Interop.Word.WdMovementType.wdExtend;
                //WordApp.Selection.MoveDown(ref moveUnit, ref moveCount, ref moveExtend);
                //WordApp.Selection.Cells.Merge();
                ////////插入图片
                //////string FileName = @"c:\picture.jpg";//图片所在路径
                //////object LinkToFile = false;
                //////object SaveWithDocument = true;
                //////object Anchor = WordDoc.Application.Selection.Range;
                //////WordDoc.Application.ActiveDocument.InlineShapes.AddPicture(FileName, ref LinkToFile, ref SaveWithDocument, ref Anchor);
                //////WordDoc.Application.ActiveDocument.InlineShapes[1].Width = 100f;//图片宽度
                //////WordDoc.Application.ActiveDocument.InlineShapes[1].Height = 100f;//图片高度
                ////////将图片设置为四周环绕型
                //////Microsoft.Office.Interop.Word.Shape s = WordDoc.Application.ActiveDocument.InlineShapes[1].ConvertToShape();
                //////s.WrapFormat.Type = Microsoft.Office.Interop.Word.WdWrapType.wdWrapSquare;

                //newTable.Cell(12, 1).Range.Text = "产品特殊属性";
                //newTable.Cell(12, 1).Merge(newTable.Cell(12, 3));
                ////在表格中增加行
                //WordDoc.Content.Tables[1].Rows.Add(ref Nothing);

                //WordDoc.Paragraphs.Last.Range.Text = "文档创建时间：" + DateTime.Now.ToString();//“落款”
                //WordDoc.Paragraphs.Last.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;

                //文件保存
                WordDoc.SaveAs(ref filename, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);
                WordDoc.Close(ref Nothing, ref Nothing, ref Nothing);
                WordApp.Quit(ref Nothing, ref Nothing, ref Nothing);
                //message = name + "文档生成成功，以保存到C:CNSI下";
            }
            catch
            {
                message = "文件导出异常！";
            }
            return message;
        }

        public static string CreateWordFile2(CaseVM model, string path)
        {
            string message = "";
            try
            {
                Aspose.Words.Document doc = new Aspose.Words.Document();
                DocumentBuilder builder = new DocumentBuilder(doc);

                builder.PageSetup.DifferentFirstPageHeaderFooter = true;
                builder.PageSetup.OddAndEvenPagesHeaderFooter = true;
                builder.MoveToHeaderFooter(HeaderFooterType.HeaderFirst);
                builder.Font.Color = Color.Blue;
                builder.Font.Size = 12;
                builder.Write("[国泰安理财规划大赛案例]");
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;

                builder.MoveToDocumentStart();
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
                builder.Font.Color = Color.Red;
                builder.Font.Bold = true;
                builder.Font.Italic = true;
                builder.Writeln("案例信息");
                builder.InsertBreak(BreakType.LineBreak);

                builder.Font.Color = Color.Black;
                builder.Font.Bold = false;
                builder.Font.Italic = false;
                builder.Writeln("客户姓名：" + model.CustomerName);
                builder.Writeln("身份证号：" + model.IDNum);
                builder.Writeln("理财类型：" + model.strFinancialType);
                builder.Writeln("客户背景：" + model.CustomerStory);
                builder.InsertBreak(BreakType.LineBreak);

                builder.Font.Color = Color.Red;
                builder.Font.Bold = true;
                builder.Font.Italic = true;
                builder.Writeln("考核点");
                builder.InsertBreak(BreakType.LineBreak);
                builder.Font.Color = Color.Black;
                builder.Font.Bold = false;
                builder.Font.Italic = false;

                
                builder.CellFormat.Borders.LineStyle = LineStyle.Single;
                builder.CellFormat.Borders.Color = Color.Black;

                builder.InsertCell();
                builder.Write("序号");
                builder.InsertCell();
                builder.Write("类型");
                builder.InsertCell();
                builder.Write("考点");
                builder.InsertCell();
                builder.Write("答案");
                builder.EndRow();

                int i = 1;
                foreach (var item in model.ExamPointAnswer)
                {
                    i++;
                    builder.InsertCell();
                    builder.Write((i - 1).ToString());
                    builder.InsertCell();
                    builder.Write(item.strExamType == null ? "" : item.strExamType);
                    builder.InsertCell();
                    builder.Write(item.strExamPoint == null ? "" : item.strExamPoint);
                    builder.InsertCell();
                    builder.Write(item.Answer == null ? "" : item.Answer);
                    builder.EndRow();
                }
                builder.EndTable();
                doc.Save(path);

            }
            catch
            {
                message = "文件导出异常！";
            }
            return message;
        }
    }
}
