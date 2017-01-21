using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using Aspose.Cells;
using NPOI.SS.UserModel;
using System.Data.OleDb;

namespace Utils
{
    /// <summary>
    /// Excel操作类
    /// </summary>
    public class ExcelHelper
    {

        /// <summary>
        /// 由DataTable导出Excel
        /// </summary>
        /// <param name="dt">需要导出的DataTable</param>
        /// <param name="fileName">导出文件名称</param>
        /// <param name="Title">表头</param>
        public static void ExportToExcel(DataTable dt, string fileName)
        {
            HttpResponse resp = System.Web.HttpContext.Current.Response; ;
            resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            resp.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8) + ".xls");
            string colHeaders = "", ls_item = "";

            ////定义表对象与行对象，同时用DataSet对其值进行初始化
            //DataTable dt = ds.Tables[0];
            DataRow[] myRow = dt.Select();//可以类似dt.Select("id>10")之形式达到数据筛选目的
            int i = 0;
            int cl = dt.Columns.Count;

            //取得数据表各列标题，各标题之间以t分割，最后一个列标题后加回车符
            //for (i = 0; i < cl; i++)
            //{
            //    if (i == (cl - 1))//最后一列，加n
            //    {
            //        colHeaders += Title[i].ToString() + "\n"; //dt.Columns[i].Caption.ToString()
            //    }
            //    else
            //    {
            //        colHeaders += Title[i].ToString() + "\t"; //dt.Columns[i].Caption.ToString()
            //    }

            //}
            //resp.Write(colHeaders);
            //向HTTP输出流中写入取得的数据信息

            //逐行处理数据 
            foreach (DataRow row in myRow)
            {
                //当前行数据写入HTTP输出流，并且置空ls_item以便下行数据   
                for (i = 0; i < cl; i++)
                {
                    if (i == (cl - 1))//最后一列，加n
                    {
                        ls_item += "" + row[i].ToString() + "\n";
                    }
                    else
                    {
                        ls_item += "" + row[i].ToString() + "\t";
                    }

                }
                resp.Write(ls_item);
                ls_item = "";

            }
            resp.End();
        }


        public static void Export(DataTable dt, string fileName, List<string> Title)
        {
            HttpResponse response = System.Web.HttpContext.Current.Response; ;
            response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8) + ".xls");

            Workbook workbook = new Workbook();
            Worksheet sheet = (Worksheet)workbook.Worksheets[0];
            int i = 0;
            foreach (var item in Title)
            {
                Style s = new Style();
                s.Font.IsBold = true;
                sheet.Cells[0, i].PutValue(item);
                sheet.Cells[0, i].SetStyle(s);
                i++;
            }
            int RCount = dt.Rows.Count;
            int CCount = dt.Columns.Count;
            for (int x = 0; x < RCount; x++)
            {
                for (int y = 0; y < CCount; y++)
                {
                    sheet.Cells[x + 1, y].PutValue(dt.Rows[x][y] + "");
                }
            }
            response.Clear();
            response.Buffer = true;
            response.ContentEncoding = System.Text.Encoding.UTF8;
            response.ContentType = "application/ms-excel";
            response.BinaryWrite(workbook.SaveToStream().ToArray());
            response.End();
        }


        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static DataTable ImportExcelFile(string filepath)
        {
            NPOI.HSSF.UserModel.HSSFWorkbook hssworkbook;

            using (FileStream file = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                hssworkbook = new NPOI.HSSF.UserModel.HSSFWorkbook(file);
            }
            ISheet sheet = hssworkbook.GetSheetAt(0);
            //IEnumerator rows = sheet.GetRowEnumerator();

            IRow headerRow = sheet.GetRow(0);
            int rowCount = sheet.LastRowNum;
            int cellCount = headerRow.LastCellNum;
            DataTable dt = new DataTable();
            for (int j = 0; j < cellCount; j++)
            {
                dt.Columns.Add(Convert.ToChar(((int)('A')) + j).ToString());
            }
            //rows.MoveNext();
            for (int r = (sheet.FirstRowNum + 1); r <= rowCount; r++)
            {
                IRow row = sheet.GetRow(r);  //读取当前行数据
                if (row != null)
                {
                    DataRow dr = dt.NewRow();
                    cellCount = row.LastCellNum;
                    bool isCellNull = true;
                    for (int i = 0; i < cellCount; i++)
                    {
                        ICell cell = row.GetCell(i);
                        if (cell == null)
                        {
                            dr[i] = "";
                        }
                        else
                        {
                            cell.SetCellType(NPOI.SS.UserModel.CellType.STRING);
                            dr[i] = cell.StringCellValue;
                            if (isCellNull)
                            {
                                if (!string.IsNullOrWhiteSpace(cell.StringCellValue))
                                {
                                    isCellNull = false;
                                }
                            }
                        }
                    }
                    if (!isCellNull)
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }


        /// <summary>
        /// 导入，不使用HSSFWorkbook，以兼容2003和2007
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <returns></returns>
        public static DataTable ImportExcelFileCompatible(string filepath)
        {
            string strCon = "";
            if (filepath.IndexOf(".xlsx") != -1)
                strCon = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + filepath + ";" + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"";
            else if (filepath.IndexOf(".xls") != -1)
                strCon = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + filepath + ";" + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";

            string strCom = " SELECT * FROM [Sheet1$]";

            DataTable dt_temp = new DataTable();
            using (OleDbConnection myConn = new OleDbConnection(strCon))
            using (OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, myConn))
            {
                myConn.Open();
                myCommand.Fill(dt_temp);
            }

            return dt_temp;
        }

        /// <summary> 
        /// 导出数据到本地 
        /// </summary> 
        /// <param name="dt">要导出的数据</param> 
        /// <param name="tableName">表格标题</param> 
        /// <param name="path">保存路径</param> 
        public static void OutFileToDisk(DataTable dt, string tableName, string path)
        {
            Workbook workbook = new Workbook(); //工作簿 
            Worksheet sheet = workbook.Worksheets[0]; //工作表 
            Cells cells = sheet.Cells;//单元格 

            ////为标题设置样式     
            //Style styleTitle = workbook.Styles[workbook.Styles.Add()];//新增样式 
            //styleTitle.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            //styleTitle.Font.Name = "宋体";//文字字体 
            //styleTitle.Font.Size = 18;//文字大小 
            //styleTitle.Font.IsBold = true;//粗体 

            ////样式2 
            //Style style2 = workbook.Styles[workbook.Styles.Add()];//新增样式 
            //style2.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            //style2.Font.Name = "宋体";//文字字体 
            //style2.Font.Size = 14;//文字大小 
            //style2.Font.IsBold = true;//粗体 
            //style2.IsTextWrapped = true;//单元格内容自动换行 
            //style2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            //style2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            //style2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            //style2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            ////样式3 
            //Style style3 = workbook.Styles[workbook.Styles.Add()];//新增样式 
            //style3.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            //style3.Font.Name = "宋体";//文字字体 
            //style3.Font.Size = 12;//文字大小 
            //style3.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            //style3.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            //style3.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            //style3.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            int Colnum = dt.Columns.Count;//表格列数 
            int Rownum = dt.Rows.Count;//表格行数 

            //生成行1 标题行    
            //cells.Merge(0, 0, 1, Colnum);//合并单元格 
            //cells[0, 0].PutValue(tableName);//填写内容 
            //cells[0, 0].SetStyle(styleTitle);
            //cells.SetRowHeight(0, 38);

            //生成行2 列名行 
            for (int i = 0; i < Colnum; i++)
            {
                cells[0, i].PutValue(dt.Columns[i].ColumnName);
                //cells[1, i].SetStyle(style2);
                cells.SetRowHeight(0, 25);
            }

            //生成数据行 
            for (int i = 0; i < Rownum; i++)
            {
                for (int k = 0; k < Colnum; k++)
                {
                    cells[1 + i, k].PutValue(dt.Rows[i][k].ToString());
                    //cells[2 + i, k].SetStyle(style3);
                }
                cells.SetRowHeight(1 + i, 24);
            }

            workbook.Save(path);
        }


        public MemoryStream OutFileToStream(DataTable dt, string tableName)
        {
            Workbook workbook = new Workbook(); //工作簿 
            Worksheet sheet = workbook.Worksheets[0]; //工作表 
            Cells cells = sheet.Cells;//单元格 

            //为标题设置样式     
            Style styleTitle = workbook.Styles[workbook.Styles.Add()];//新增样式 
            styleTitle.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            styleTitle.Font.Name = "宋体";//文字字体 
            styleTitle.Font.Size = 18;//文字大小 
            styleTitle.Font.IsBold = true;//粗体 

            //样式2 
            Style style2 = workbook.Styles[workbook.Styles.Add()];//新增样式 
            style2.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            style2.Font.Name = "宋体";//文字字体 
            style2.Font.Size = 14;//文字大小 
            style2.Font.IsBold = true;//粗体 
            style2.IsTextWrapped = true;//单元格内容自动换行 
            style2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            //样式3 
            Style style3 = workbook.Styles[workbook.Styles.Add()];//新增样式 
            style3.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            style3.Font.Name = "宋体";//文字字体 
            style3.Font.Size = 12;//文字大小 
            style3.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style3.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            style3.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style3.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            int Colnum = dt.Columns.Count;//表格列数 
            int Rownum = dt.Rows.Count;//表格行数 

            //生成行1 标题行    
            cells.Merge(0, 0, 1, Colnum);//合并单元格 
            cells[0, 0].PutValue(tableName);//填写内容 
            cells[0, 0].SetStyle(styleTitle);
            cells.SetRowHeight(0, 38);

            //生成行2 列名行 
            for (int i = 0; i < Colnum; i++)
            {
                cells[1, i].PutValue(dt.Columns[i].ColumnName);
                cells[1, i].SetStyle(style2);
                cells.SetRowHeight(1, 25);
            }

            //生成数据行 
            for (int i = 0; i < Rownum; i++)
            {
                for (int k = 0; k < Colnum; k++)
                {
                    cells[2 + i, k].PutValue(dt.Rows[i][k].ToString());
                    cells[2 + i, k].SetStyle(style3);
                }
                cells.SetRowHeight(2 + i, 24);
            }

            MemoryStream ms = workbook.SaveToStream();
            return ms;
        } 

    }
}
