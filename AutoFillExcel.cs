using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace Upholstery_Builder
{
    class AutoFillExcel
    {
        public static void BuildWorkSheet(string chosenPath,string initials,string styleId,
            List<string> procedureText, string[] upholProcedures, List<string> photoList, List<string> photoPathsList,
            string revInitials, string date,
            List<List<string>> excelData = null)
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                Console.WriteLine("EXCEL could not be started. Check that your office installation and project references are correct.");
                return;
            }
            xlApp.Visible = true;

            //Workbook wb = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Workbook wb = xlApp.Workbooks.Open(@"Y:\Product Development\Forms\Auto Upholstery Spec.xls");
            Worksheet ws = (Worksheet)wb.Worksheets[1];

            if (ws == null)
            { Console.WriteLine("Worksheet could not be created. Check that your office installation and project references are correct.",null,true); }

            //try to get the name for the styleID
            string idName = "";
            try
            { idName = ID_Reader.interpreter(styleId); }
            catch (System.Exception e)
            { idName = "Unknown Style"; }

            //Input Text for the titleblocks
            if (date == null)
            { ws.Cells[1, 12] = DateTime.Now.ToShortDateString(); }
            else
            { ws.Cells[1, 12] = date; }
                ws.Cells[2, 3] = styleId;
                ws.Cells[2, 8] = idName;
                ws.Cells[3, 11] = initials;
                

            //Insert text into the dimension fields
            try
            {
                System.Array dimensions = getDimensions(styleId, xlApp);
                //Overall Width
                ws.Cells[52, 2] = dimensions.GetValue(1, 2);
                //Overall Depth
                ws.Cells[52, 3] = dimensions.GetValue(1, 3);
                //Overall Height
                ws.Cells[52, 4] = dimensions.GetValue(1, 4);
                //Height to Frame
                ws.Cells[52, 5] = dimensions.GetValue(1, 5);
                //Arm Height
                ws.Cells[52, 6] = dimensions.GetValue(1, 6);
                //Seat Height to seam
                ws.Cells[52, 7] = dimensions.GetValue(1, 7);
                //Seat Height to crown
                ws.Cells[52, 8] = dimensions.GetValue(1, 8);
                //Seat Width
                ws.Cells[52, 9] = dimensions.GetValue(1, 9);
                //Seat Depth
                ws.Cells[52, 10] = dimensions.GetValue(1, 10);
                //Arm Width
                ws.Cells[52, 11] = dimensions.GetValue(1, 11);
                //Diagonal ** Not on form anymore
                //Back Height
                ws.Cells[52, 12] = dimensions.GetValue(1, 13);
            }
            catch (Exception)
            { }

            int rowCount = 4;
            //place upholstery procedures onto form scaling as parts are listed
            for (int i = 0; i < upholProcedures.Count();i++ )
            {
                if(procedureText[i] != null && procedureText[i] != "")
                {
                    //format the title, border, row height
                    Range title = ws.Range[ws.Cells[rowCount, 2], ws.Cells[rowCount,6]];
                    title.Merge();
                    title.RowHeight = 12.75;
                    title.Value = upholProcedures[i];
                    title.Font.Size = 10;
                    title.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    Borders brdr = title.Borders;
                    brdr[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
                    brdr[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
                    brdr.Weight = XlBorderWeight.xlMedium;
                    brdr.ColorIndex = 15;

                    //iterate the row counter below the title
                    rowCount++;
                    //count the lines in the text
                    string[] procedurelines = procedureText[i].Split('\n');
                    int linesNeeded = procedurelines.Length;
                    for(int next = 0; next < linesNeeded; next++, rowCount++)
                    {
                        Range procedure = ws.Range[ws.Cells[rowCount, 2], ws.Cells[rowCount, 6]];
                        procedure.Value = procedurelines[next];
                        procedure.RowHeight = 12.75;
                        procedure.Font.Size = 8;
                        procedure.HorizontalAlignment = XlHAlign.xlHAlignLeft;
                    }
                    //move row counter to bottom and one more
                    //leave a margin along bottom of text
                    Range margin = ws.Range[ws.Cells[rowCount, 2], ws.Cells[rowCount, 6]];
                    margin.Merge();
                    margin.RowHeight = 6.75;
                }
            }

            //unload the list for revision data into revisions blocks
            //if there are already 6 revisions, ignore the first one
            //after revisions are loaded insert the latest revision to it
            if(excelData != null)
            {
                int revRow = 55;
                foreach(List<string> listName in excelData)
                {
                    if (listName.Count > 0)
                    {
                        //if the list is a revision it will be named as such
                        if (listName[0].Contains("Revision"))
                        {
                            //as long as there arent too many revisions
                            int revisions = listName.Count;
                            int x;
                            if (revisions < 6)
                            { x = 0; }
                            else
                            { x = 1; }

                            while (x < revisions)
                            {
                                //split the list up, ignore "revision" , diliminated by |
                                string[] revisionArray = listName[x].Split('|');
                                ws.Cells[revRow, 2] = revisionArray[1];
                                ws.Cells[revRow, 3] = revisionArray[2];
                                ws.Cells[revRow, 4] = revisionArray[3];
                                x++;
                                revRow++;
                            }
                        }
                    }
                }
                //then add one more note for new revision
                ws.Cells[revRow, 2] = DateTime.Now.ToShortDateString();
                ws.Cells[revRow, 3] = revInitials;
            }


            //insert pics to upholspec
            foreach(string pathName in photoPathsList)
            {
                foreach(string view in photoList)
                {
                    if(pathName.Contains(view.ToUpper()))
                    {
                        switch (view.ToUpper())
                        {
                            case "TOP":
                                ws.Shapes.AddPicture(pathName,
                                    Microsoft.Office.Core.MsoTriState.msoFalse,
                                    Microsoft.Office.Core.MsoTriState.msoTrue,
                                    260, 65, 270, 175);
                                break;
                            case "FRONT":
                                ws.Shapes.AddPicture(pathName,
                                    Microsoft.Office.Core.MsoTriState.msoFalse,
                                    Microsoft.Office.Core.MsoTriState.msoTrue,
                                    260, 259, 270, 175);
                                break;                            
                            case "SIDE":
                                ws.Shapes.AddPicture(pathName,
                                    Microsoft.Office.Core.MsoTriState.msoFalse,
                                    Microsoft.Office.Core.MsoTriState.msoTrue,
                                    260, 455, 270, 175);
                                break;
                            default:
                                ws.Shapes.AddPicture(pathName,
                                    Microsoft.Office.Core.MsoTriState.msoFalse,
                                    Microsoft.Office.Core.MsoTriState.msoTrue,
                                    630, 65, 270, 175);
                                break;
                        }
                    }
                }
            }


            //save file in the uphol folder
            try
            { wb.SaveAs(chosenPath + "\\Upholstery\\" + styleId + " Upholstery Spec.xls"); }
            catch (Exception)
            { }
        }

        static System.Array getDimensions(string styleId, Application xlApp)
        {
            //***Need change file location from Code to registry
            Workbook wb = xlApp.Workbooks.Open(@"DRIVELOCATION:\Customers\Style Info\Styles and Dimensions.XLS", null, true);
            Worksheet ws = (Worksheet)wb.Worksheets[1];

            Range all = ws.UsedRange;
            Range target = all.Find(styleId);

            if(target == null)
            { wb.Close(); }

            target = target.EntireRow;
            //target = ws.get_Range(ws.Cells[target.Row,1], ws.Cells[target.Row,13]);
            System.Array myvalues = (Array)target.Cells.Value;

            wb.Close();


            return myvalues;
        }
    }
}
