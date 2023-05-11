using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;

namespace Upholstery_Builder
{
    class OldSpecReader
    {
        public static List<List<string>> grabProcedures(string filePath)
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            Workbook wb = xlApp.Workbooks.Open(filePath, null, true);
            Worksheet ws = (Worksheet)wb.Worksheets[1];
            xlApp.Visible = true;

            //System.Data.DataTable excelData = new System.Data.DataTable("Procedure Data");
            //DataColumn column;
            //DataRow row;
            //List<string> rowNames = null;

            string[] upholProcedures = new string[]{
                "Seat Roll & Deck",
                "Inside Arm",
                "Inside Back",
                "Inside Arm Border",
                "Inside Back Border",
                "Outside Trimming",
                "Outside Specs",
                "Front Border",
                "Other:Wings, Posts, Etc..",
                "Other - Border, Wings, Etc…"};

            Range rng = ws.UsedRange;
            List<List<string>> excelData = new List<List<string>>();

            //load the first data & initials into a list so they can get saved
            //*********Make sure to save initials and date ONLY if value != null
            List<string> basic = new List<string>();
            string initials = getCellValue(3,11,rng);
            string date = getCellValue(1,12,rng);
            basic.Add(initials);
            basic.Add(date);

            //store the date and orignal creator so that we don't change that
            excelData.Add(basic);

            //Upholstery procedure
            for (int i = 4; i < 54;i++)
            {
                foreach(string procName in upholProcedures)
                {
                    string cellValue = getCellValue(i, 2, rng);
                    if(cellValue == procName || cellValue == procName + ":")
                    {
                        List<string> column = new List<string>();
                        column.Add(procName);
                        i++;
                        cellValue = getCellValue(i, 2, rng);

                        while(cellValue != null && cellValue != "")
                        {
                            column.Add(cellValue);
                            i++;
                            cellValue = getCellValue(i, 2, rng);
                        }
                        excelData.Add(column);
                    }
                }
            }

            List<string> revisions = new List<string>();
            //store revision data
            for (int i = 59; i < 65;i++)
            {
                string cellValue = getCellValue(i, 2, rng);
                if(cellValue != "" && cellValue != null)
                {
                    string revString = "Revision|";
                    for(int k = 2; k < 5; k++)
                    {
                        revString = revString + getCellValue(i, k, rng) + "|";
                    }
                    revisions.Add(revString);
                }
            }
            excelData.Add(revisions);

            wb.Close();
            xlApp.Visible = false;
            return excelData;
        }

        private static string getCellValue(int row, int column, Range rng)
        {
            var cellValue = "";
            try
            {  cellValue = (string)(rng.Cells[row, column] as Range).Value; }
            catch (NullReferenceException)
            {  cellValue = null;}
            //catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
            //{ 
                //DateTime dt = (DateTime)(rng.Cells[row, column] as Range).Value;
              //  cellValue = dt.ToString();
            //}
            catch(Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
            {
                var dt = (rng.Cells[row, column] as Range).Value;
                cellValue = dt.ToString();
            }
                
            return cellValue;
        }
    }
}
