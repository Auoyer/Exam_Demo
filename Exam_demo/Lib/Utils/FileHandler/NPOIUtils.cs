using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Collections;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace Utils
{
    public class NPOIUtils
    {
        /// <summary>
        /// 导出Excel（客户端导出）
        /// </summary>
        /// <param name="dt">数据表</param>
        public static void ExportExcelFile(DataTable dt)
        {
            HSSFWorkbook book = new HSSFWorkbook();
            ISheet sheet = book.CreateSheet("Sheet1");

            // 第一行（标题）
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
                row.CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);

            // 第二行、...（数据）
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row2 = sheet.CreateRow(i + 1);

                for (int j = 0; j < dt.Columns.Count; j++)
                    row2.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
            }

            // 写入到客户端  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff")));
            System.Web.HttpContext.Current.Response.BinaryWrite(ms.ToArray());

            book = null;
            ms.Close();
            ms.Dispose();
        }

        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static DataTable ImportExcelFile(string filePath)
        {
            HSSFWorkbook hssfworkbook;
  
            #region 初始化信息
            try
            {
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    hssfworkbook = new HSSFWorkbook(file);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            #endregion

            ISheet sheet = hssfworkbook.GetSheetAt(0);
            IEnumerator rows = sheet.GetRowEnumerator();
            DataTable dt = new DataTable();

            if (sheet.GetRow(0) == null)
            {
                return dt;//如果一行都没有，返回空
            }
            int tarColCount = sheet.GetRow(0).LastCellNum;
            // DataTable列标题取Excel的A、B、C、...
            for (int j = 0; j < tarColCount; j++)
            {
                dt.Columns.Add(Convert.ToChar(((int)'A') + j).ToString());
            }

            // 第一行一般是用于显示的标题，跳过
            rows.MoveNext();

            // 数据从第二行开始读取
            while (rows.MoveNext())
            {
                HSSFRow row = (HSSFRow)rows.Current;
                DataRow dr = dt.NewRow();

                for (int i = 0; i < tarColCount; i++)
                {
                    ICell cell = row.GetCell(i);
                    if (cell == null)
                    {
                        dr[i] = null;
                    }
                    else
                    {
                        dr[i] = cell.ToString();
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}