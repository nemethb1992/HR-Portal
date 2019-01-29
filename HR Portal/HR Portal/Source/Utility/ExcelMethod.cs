using HR_Portal.Source.Model.Other;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace HR_Portal.Source.Utility
{
    class ExcelMethod
    {
        private Workbook wb;
        private Worksheet ws;
        private Application excel;
        

        public void Stat1(int weeks)
        {
            excel = new Application();
            wb = (excel.Workbooks.Add());
            ws = (Worksheet)wb.ActiveSheet;
            string savePath = "";


            List<ModelStat> stat = GetStatList(DateTime.Now, weeks);
            ws.Cells[1, 1].Value = "Időszak";
            ws.Cells[1, 2].Value = "Szerelő";
            ws.Cells[1, 3].Value = "QS";
            ws.Cells[1, 4].Value = "Raktár";
            ws.Cells[1, 5].Value = "Szellemi";
            for (int i = 0; i < stat.Count; i++)
            {
                ws.Cells[i+2, 1] = stat[i].date;
                ws.Cells[i+2, 2] = stat[i].szerelo;
                ws.Cells[i+2, 3] = stat[i].qs;
                ws.Cells[i+2, 4] = stat[i].raktar;
                ws.Cells[i+2, 5] = stat[i].szellemi;
            }
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.InitialDirectory = "c:\\";
                saveFileDialog1.Filter = "Excel Files(.xls)|*.xls| Excel Files(.xlsx)| *.xlsx ";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    savePath = saveFileDialog1.FileName;
                    ws.SaveAs(savePath);
                }
            }
            catch (Exception)
            {
                throw;
            }

            wb.Close();
            excel.Quit();
            Process.Start(savePath);
        }

        private List<ModelStat> GetStatList(DateTime date, int weekNo)
        {
            List<ModelStat> list = new List<ModelStat>();
            for (int i = 0; i < weekNo; i++)
            {
                list.Add(ModelStat.GetModelStat1(date.AddDays(-7).ToString("yyyy.MM.dd"), date.ToString("yyyy.MM.dd")));
                date = date.AddDays(-7);
            }
            return list;
        }
    }
}
