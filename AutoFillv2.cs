using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace Upholstery_Builder
{
    class AutoFillv2
    {
        public static void BuildWorkSheet(string chosenPath, string initials, string styleId,
            List<string> procedureText, string[] upholProcedures, List<string> photoList, List<string> photoPathsList,
            string revInitials, string date,
            List<List<string>> excelData = null)
        {
            //clarify the app, should we ever need to reference another app (i.e. aCad)
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                Console.WriteLine("EXCEL could not be started. Check that your office installation and project references are correct.");
                return;
            }
            xlApp.Visible = true;

            //update to open file as read only
            //move file location from hard code to registry
            Workbook wb = xlApp.Workbooks.Open(@"DRIVE LOCATION:\Product Development\Forms\Auto Upholstery Spec.xls", false, true);
            Worksheet ws = (Worksheet)wb.Worksheets[1];

            if (ws == null)
            { Console.WriteLine("Worksheet could not be created. Check that your office installation and project references are correct.", null, true); }

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

            //pulling dimensions from Excel file
            //need to pull dimensions from SQL server when it is created
            #region Write dimensions
            //Insert text into the dimension fields
            try
            {
                System.Array dimensions = getDimensions(styleId, xlApp);
                //Overall Width
                ws.Cells[51, 2] = dimensions.GetValue(1, 2);
                //Overall Depth
                ws.Cells[51, 3] = dimensions.GetValue(1, 3);
                //Overall Height
                ws.Cells[51, 4] = dimensions.GetValue(1, 4);
                //Height to Frame
                ws.Cells[51, 5] = dimensions.GetValue(1, 5);
                //Arm Height
                ws.Cells[51, 6] = dimensions.GetValue(1, 6);
                //Seat Height to seam
                ws.Cells[51, 7] = dimensions.GetValue(1, 7);
                //Seat Height to crown
                ws.Cells[51, 8] = dimensions.GetValue(1, 8);
                //Seat Width
                ws.Cells[51, 9] = dimensions.GetValue(1, 9);
                //Seat Depth
                ws.Cells[51, 10] = dimensions.GetValue(1, 10);
                //Arm Width
                ws.Cells[51, 11] = dimensions.GetValue(1, 11);
                //Diagonal ** Not on form anymore
                //Back Height
                ws.Cells[51, 12] = dimensions.GetValue(1, 13);
            }
            catch (Exception)
            { }

            #endregion

            //keep most recent revisions
            //might be able to replace this data from coming from previous excel to using SQL server for change log
            #region write revisions
            //unload the list for revision data into revisions blocks
            //if there are already 6 revisions, ignore the first one
            //after revisions are loaded insert the latest revision to it
            if (excelData != null)
            {
                int revRow = 54;
                foreach (List<string> listName in excelData)
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
            #endregion

            //old code inserted to absolut locations.
            //now inserts to the relevant cell
            #region insert pics to cells
            foreach (string pathName in photoPathsList)
            {
                foreach (string view in photoList)
                {
                    if (pathName.Contains(view.ToUpper()))
                    {
                        object missing = System.Reflection.Missing.Value;
                        Pictures p = ws.Pictures(missing) as Pictures;
                        Picture pic = null;
                        Range picPostions = null;
                        switch (view.ToUpper())
                        {
                            case "TOP":
                                //picPostions = ws.Range[ws.Cells[5, 7], ws.Cells[17, 12]];                                
                                picPostions = ws.Cells[5,7];
                                break;
                            case "FRONT":
                                //picPostions = ws.Range[ws.Cells[20, 7], ws.Cells[33, 12]];
                                picPostions = ws.Cells[20, 7];
                                break;
                            case "SIDE":
                                //picPostions = ws.Range[ws.Cells[35, 7], ws.Cells[48, 12]];
                                picPostions = ws.Cells[35, 7];
                                break;
                            default:
                                picPostions = ws.Range[ws.Cells[35, 13], ws.Cells[48, 25]];
                                break;
                        }                        
                        pic = p.Insert(pathName, missing);
                        //offset insert point to account for thicker borders
                        pic.Left = Convert.ToDouble(picPostions.Left)+1;
                        pic.Top = Convert.ToDouble(picPostions.Top) +1;
                        pic.Height = 175;
                        pic.Width = 270;
                    }
                }
            }
            #endregion


            //insert upholstery instructions
            #region Write upholstery procedures
            //post title of procedure
            //apply formatting to procedure cells
            //post procedure info, account for neccissary lines, merge cells together and apply text wrap

            int rowCount = 4;
            //place upholstery procedures onto form scaling as parts are listed
            for (int i = 0; i < upholProcedures.Count(); i++)
            {
                if (procedureText[i] != null && procedureText[i] != "")
                {
                    //write procedure Title Blocks
                    //format the title, border, row height
                    Range title = ws.Range[ws.Cells[rowCount, 2], ws.Cells[rowCount, 6]];
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

                    //move row counter down
                    rowCount++;

                    //find lines used for mereged cell considering carriage returns and text wrapped returns
                    double procLines = mergedRows(rowCount,procedureText[i],ws);
                    procLines = Math.Round(procLines);

                    //insert procedure text into a range
                    Range procedure = ws.Range[ws.Cells[rowCount, 2], ws.Cells[rowCount + procLines, 6]];
                    procedure.Merge();
                    procedure.Font.Size = 8;
                    procedure.HorizontalAlignment = XlHAlign.xlHAlignLeft;
                    procedure.VerticalAlignment = XlVAlign.xlVAlignTop;
                    procedure.WrapText = true;
                    procedure.Value = procedureText[i];

                    //change row counter
                    rowCount = rowCount + (int)procLines + 1;                   
                }   

           } 
            #endregion


            //save file in the uphol folder
            //if save location can be used *bc file already exists, need to make sure it doesnt try to save over the orginal template
            try
            { wb.SaveAs(chosenPath + "\\Upholstery\\" + styleId + " Upholstery Spec.xls"); }
            catch (Exception)
            { }

        }

        static System.Array getDimensions(string styleId, Application xlApp)
        {
            //***Need to change file location to registry
            Workbook wb = xlApp.Workbooks.Open(@"DRIVE LOCATION:\Customers\Style Info\Styles and Dimensions.XLS", null, true);
            Worksheet ws = (Worksheet)wb.Worksheets[1];

            Range all = ws.UsedRange;
            Range target = all.Find(styleId);

            if (target == null)
            { wb.Close(); }

            target = target.EntireRow;
            //target = ws.get_Range(ws.Cells[target.Row,1], ws.Cells[target.Row,13]);
            System.Array myvalues = (Array)target.Cells.Value;

            wb.Close();


            return myvalues;
        }

        private void ScalePicture(Picture pic, double width, double height)
        {
            double fX = width / pic.Width;
            double fY = height / pic.Height;
            double oldH = pic.Height;
            if (fX < fY)
            {
                pic.Width *= fX;
                if (pic.Height == oldH) // no change if aspect ratio is locked
                    pic.Height *= fX;
                pic.Top += (height - pic.Height) / 2;
            }
            else
            {
                pic.Width *= fY;
                if (pic.Height == oldH) // no change if aspect ratio is locked
                    pic.Height *= fY;
                pic.Left += (width - pic.Width) / 2;
            }
        }

        static private double mergedRows(int rowCount, string wrpText, Worksheet ws)
        {
            //hold values to compare newlines for cell
            double h1, h2;
            //holder for the text
            //Range mergeCell = ws.Range[ws.Cells[rowCount, 2], ws.Cells[rowCount, 6]];
            Range mergeCell = ws.Cells[62, 15];
            mergeCell.Font.Size = 8;
            mergeCell.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            mergeCell.VerticalAlignment = XlVAlign.xlVAlignTop;
            mergeCell.Value = wrpText;
            //mergeCell.Columns.AutoFit();
            mergeCell.Rows.AutoFit();
            mergeCell.ColumnWidth = 40;

            mergeCell.WrapText = false;
            h1 = mergeCell.Height;
            mergeCell.WrapText = true;
            h2 = mergeCell.Height;

            mergeCell.Value = null;
            return h2 / h1;
        }
    }
}
