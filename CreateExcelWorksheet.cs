using System;
using Microsoft.Office.Interop.Excel;

namespace Upholstery_Builder
{
    class CreateExcelWorksheet
    {
        public static void BuildWorkSheet()
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                Console.WriteLine("EXCEL could not be started. Check that your office installation and project references are correct.");
                return;
            }
            xlApp.Visible = true;

            Workbook wb = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = (Worksheet)wb.Worksheets[1];

            if (ws == null)
            { Console.WriteLine("Worksheet could not be created. Check that your office installation and project references are correct."); }

            //format the column widths
            Range columnA = ws.get_Range("A:A");
            columnA.EntireColumn.ColumnWidth = 5;
            Range columnBL = ws.get_Range("B:B", "L:L");
            columnBL.EntireColumn.ColumnWidth = 8;

            //format row heights (Constants i.e. title block and bottom block)
            Range row1 = ws.get_Range("1:1");
            row1.EntireRow.RowHeight = 25.5;
            Range row2_4 = ws.get_Range("2:2", "4:4");
            row2_4.EntireRow.RowHeight = 12.75;
            Range row54 = ws.get_Range("54:54");
            row54.EntireRow.RowHeight = 23.25;
            Range row55 = ws.get_Range("55:55");
            row55.EntireRow.RowHeight = 13.5;
            Range row56 = ws.get_Range("56:56");
            row56.EntireRow.RowHeight = 6.75;
            Range row57_64 = ws.get_Range("57:57", "64:64");
            row57_64.EntireRow.RowHeight = 13.5;
            

            //merge title blocks
            Range cellLogo = ws.get_Range("A1", "C1");
            cellLogo.Merge();
            Range cellLabel = ws.get_Range("D1", "J1");
            cellLabel.Merge();
            Range cellStyle = ws.get_Range("B2", "B3");
            cellStyle.Merge();
            Range cellStyleId = ws.get_Range("C2", "E3");
            cellStyleId.Merge();
            Range cellDesc = ws.get_Range("F2", "G3");
            cellDesc.Merge();
            Range cellDescription = ws.get_Range("H2", "J3");
            cellDescription.Merge();
            Range written = ws.get_Range("K2", "L2");
            written.Merge();
            Range writtenBy = ws.get_Range("K3", "L3");
            writtenBy.Merge();
            

            //Input Text for the titleblocks
            cellLabel.Value = "UPHOLSTERY SPECIFICATION";
            cellLabel.Font.Size = 14;
            written.Value = "Written By:";
            written.Font.Size = 10;
            cellDesc.Value = "Description:";
            cellDesc.Font.Size = 14;
            cellStyle.Value = "Style No.";
            cellStyle.Font.Size = 12;
            //cellStyleId.Value = 
            cellStyleId.Font.Size = 14;
            ws.Cells[1, 11] = "DATE:";
            ws.Cells[1, 11].Font.Size = 10;
            ws.Cells[1, 12] = DateTime.Now.ToShortDateString();
            ws.Cells[1, 12].Font.Size = 10;

            Range header = ws.get_Range("A1", "L4");
            header.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            //Text for the dimensions section
            Range dimensions = ws.get_Range("B54", "L55");
            dimensions.Font.Size = 8;
            dimensions.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ws.Cells[54, 2] = "Overall\nWidth";
            ws.Cells[54, 3] = "Overall\nDepth";
            ws.Cells[54, 4] = "Overall\nHeight";
            ws.Cells[54, 5] = "Height to\nFrame";
            ws.Cells[54, 6] = "Arm Height";
            ws.Cells[54, 7] = "Seat Height";
            ws.Cells[54, 8] = "Seat Width";
            ws.Cells[54, 9] = "Seat Depth";
            ws.Cells[54, 10] = "Arm Width";
            ws.Cells[54, 11] = "Diagonal";
            ws.Cells[54, 12] = "Back Height";
            //int dimCounter;
            //foreach(string styleDim in listORarray)
            //{
            //ws.Cells[55, dimcounter +2] = styleDim;
            //dimCounter++;
            //}
            
            //add logo to the first Cell

            //set borders for the Cells
            //border around everything
            Range all = ws.get_Range("A1", "L64");
            all.BorderAround(XlLineStyle.xlContinuous,
                XlBorderWeight.xlThin,
                XlColorIndex.xlColorIndexAutomatic,
                XlColorIndex.xlColorIndexAutomatic);
            cellLogo.BorderAround(XlLineStyle.xlContinuous,
                XlBorderWeight.xlThin,
                XlColorIndex.xlColorIndexAutomatic,
                XlColorIndex.xlColorIndexAutomatic);
            cellLabel.BorderAround(XlLineStyle.xlContinuous,
                XlBorderWeight.xlThin,
                XlColorIndex.xlColorIndexAutomatic,
                XlColorIndex.xlColorIndexAutomatic);
            ws.Cells[1, 11].BorderAround(XlLineStyle.xlContinuous,
                XlBorderWeight.xlThin,
                XlColorIndex.xlColorIndexAutomatic,
                XlColorIndex.xlColorIndexAutomatic);
            ws.Cells[1, 12].BorderAround(XlLineStyle.xlContinuous,
                XlBorderWeight.xlThin,
                XlColorIndex.xlColorIndexAutomatic,
                XlColorIndex.xlColorIndexAutomatic);

            //save file in the uphol folder
            //wb.SaveAs(filelocation +"\uhpolstery\" + styleID " Upholstery Spec.xls", Excel.X1FileFormat.wbNormal);
        }
    }
}
