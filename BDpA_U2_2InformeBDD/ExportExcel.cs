using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Office.Interop;
using System.Diagnostics;

namespace BDpA_U2_2InformeBDD
{
    class ExportExcel
    {
        public void Export_Ctr_Excel(DataTable tablelist, string excelFilename)
        {
            // Here is main process
            Microsoft.Office.Interop.Excel.Application objexcelapp = new Microsoft.Office.Interop.Excel.Application();
            objexcelapp.Application.Workbooks.Add(Type.Missing);
            objexcelapp.Columns.AutoFit();
            for (int i = 1; i < tablelist.Columns.Count + 1; i++)
            {
                Microsoft.Office.Interop.Excel.Range xlRange = (Microsoft.Office.Interop.Excel.Range)objexcelapp.Cells[1, i];
                xlRange.Font.Bold = -1;
                xlRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                xlRange.Borders.Weight = 1d;
                xlRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                objexcelapp.Cells[1, i] = tablelist.Columns[i - 1].ColumnName;
            }
            /*For storing Each row and column value to excel sheet*/
            for (int i = 0; i < tablelist.Rows.Count; i++)
            {
                for (int j = 0; j < tablelist.Columns.Count; j++)
                {
                    if (tablelist.Rows[i][j] != null)
                    {
                        Microsoft.Office.Interop.Excel.Range xlRange = (Microsoft.Office.Interop.Excel.Range)objexcelapp.Cells[i + 2, j + 1];
                        xlRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                        xlRange.Borders.Weight = 1d;
                        objexcelapp.Cells[i + 2, j + 1] = tablelist.Rows[i][j].ToString();
                    }
                }
            }
            objexcelapp.Columns.AutoFit(); // Auto fix the columns size
            System.Windows.Forms.Application.DoEvents();
            if (Directory.Exists(@"C:\Users\ibrah\Documents\Materias\Octavo Cuatrimestre\Base de Datos\")) // Folder dic
            {
                objexcelapp.ActiveWorkbook.SaveCopyAs(@"C:\Users\ibrah\Documents\Materias\Octavo Cuatrimestre\Base de Datos\" + excelFilename + ".xlsx");              
            }
            else
            {
                Directory.CreateDirectory(@"C:\Users\ibrah\Documents\Materias\Octavo Cuatrimestre\Base de Datos\");
                objexcelapp.ActiveWorkbook.SaveCopyAs(@"C:\Users\ibrah\Documents\Materias\Octavo Cuatrimestre\Base de Datos\" + excelFilename + ".xlsx");             
            }
            objexcelapp.ActiveWorkbook.Saved = true;
            System.Windows.Forms.Application.DoEvents();
            foreach (Process proc in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
            {
                proc.Kill();
            }
            
        }
    }
}
