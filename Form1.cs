using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Upholstery_Builder
{
    public partial class Form1 : Form
    {
        //currently using a global variable. not thrilled about it.
        string chosenPath = null;

        public Form1()
        { InitializeComponent(); }

        [STAThreadAttribute]
        private void createButton_Click(object sender, EventArgs e)
        {
            buildExcel();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            //use chosenPath to open a wsfor the Excel file
            //then try to find the data related to each procedure title
            //put said data into a dataset and pass it to the buildExcel() method
            string specName = chosenPath + "\\Upholstery\\" + styleIDBox.Text + " Upholstery Spec.xls";
            List<List<string>> excelData = null;
            try
            { excelData = OldSpecReader.grabProcedures(specName); }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }

            buildExcel(excelData);
        }

        //method adds to a list any view that was checked
        private List<string> getViews(List<string> photoList)
        {
            if (topViewBox.Checked == true)
            { photoList.Add("Top"); }
            if (frontViewBox.Checked == true)
            { photoList.Add("Front"); }
            if (sideViewBox.Checked == true)
            { photoList.Add("Side"); }
            if (otherViewBox.Checked == true)
            { photoList.Add("Other"); }

            return  photoList;
        }

        //validates if the photos exist or not, adds them to list
        private List<string> findPics(List<string> photoPathsList, List<string> photoList, string upholsterFolder)
        {
            //confirm if the photos requested exist
            //-Create a list of .tiff files in folder                
            DirectoryInfo dir = new DirectoryInfo(upholsterFolder);
            IEnumerable<FileInfo> fileList = dir.GetFiles(
                "*.*",
                SearchOption.AllDirectories);

            //create the query for .tiff files
            IEnumerable<FileInfo> fileQuery =
                from file in fileList
                where file.Extension == ".tif"
                orderby file.DirectoryName
                select file;

            //**IF MORE THAN ONE FILE HAVE THE SAME VIEW TAG, THEN THE LIST
            //WILL GROW TO MORE THAN WE WANTED OF THE VIEWS
            //-search each one for the appropriate name top, front...
            foreach (FileInfo fi in fileQuery)
            {
                string fileName = fi.Name.ToUpper();
                //Console.WriteLine(fileName);
                foreach (string view in photoList)
                {
                    if (fileName.Contains(view.ToUpper()))
                    { photoPathsList.Add(fi.FullName); }
                }
            }
            return photoPathsList;
        }

        //open a form for each of the uphol procedures previously named
        //if spec already exists, fill in the form with the data
        //*this way the program can override the old spec without losing data but still scale
        //each time it runs
        private List<string> getProcedure(string[] upholProcedures, List<List<string>> excelData = null)
        {
            //use a list to hold data if it isnt blank for future use
            List<string> procedureText = new List<string>();

            //for each Upholstery Description prompt user to input procedure
            foreach (string procedure in upholProcedures)
            {
                //bring the form up for each procedure, have them insert data if viable
                Procedure_Description l = new Procedure_Description();
                l.Text = procedure;
                if (excelData != null)
                {
                    string pText = "";
                    foreach (List<string> column in excelData)
                    {
                        if (column.Count > 0)
                        {
                            if (column[0].ToString() == procedure)
                            {
                                //string builder piles up all of the text
                                //account for merged cells!!! might already                            
                                for (int line = 1; line < column.Count; line++)
                                { pText = pText + column[line] + '\n'; }
                            }
                        }
                    }
                    l.richTextBox1.Text = pText;
                }

                //if the length of the strings are too long, need to add a return and make them fit the
                //excel cell
                string procText = l.TheValue;
                if (l.ShowDialog(this) == DialogResult.OK)
                {
                    procText = l.TheValue;
                    //if (procText != "" && procText != null)
                   // { procText = fitString(procText); }

                    //add either result of the if statment
                    //in the next class it decides to use the data or not based on there being text
                    procedureText.Add(procText);
                }
            }
            return procedureText;
        }

        //method acquires data from the forms and potential previous uphol spec
        //passes data to the excel making class
        private void buildExcel(List<List<string>> excelData = null)
        {
            //create list of photos to be imported
            List<string> photoList = new List<string>();
            List<string> photoPathsList = new List<string>();
            
            //get list of views desired
            photoList = getViews(photoList);

            //if the global variable wasn't set automatically then we'll need to figure it out
            //based on the the data written to the textbox
            //**Currently made the textbox read Only so that will force a folder dialogue
            if (chosenPath != null)
            {
                string upholsterFolder = chosenPath + "\\Upholstery";

                //confirm if the Upholstery folder exists
                //if not then create one
                if (!Directory.Exists(upholsterFolder))
                {   Directory.CreateDirectory(upholsterFolder); }

                //find the associated photos in upholspec
                photoPathsList = findPics(photoPathsList, photoList, upholsterFolder);
            }

            //array holds each procedure subject on the uphol spec sheet
            string[] upholProcedures = new string[]{
                "Seat Roll & Deck",
                "Inside Arm",
                "Inside Back",
                "Inside Arm Border",
                "Inside Back Border",
                "Outside Specs",
                "Front Border",
                "Other - Border, Wings, Etc…"};

            List<string> procedureText = getProcedure(upholProcedures, excelData);

            //need to pass the data to the worksheet builder and have it format it as needed
            //chosen path, user Initials, chosen pics, picture paths, procedure descriptions
            //CreateExcelWorksheet.BuildWorkSheet();//old sheet that tried to create everything, new one opens template
            string initials = null;
            string revInitials = null ;
            string date = null;

            string styleId = styleIDBox.Text;

            if(excelData == null)
            { initials = initialsBox.Text; }
            else
            {
                initials = excelData[0][0];
                revInitials = initialsBox.Text;
                date = excelData[0][1];
            }

            //AutoFillExcel.BuildWorkSheet(chosenPath, initials, styleId, procedureText, upholProcedures, photoList, photoPathsList, revInitials,date,excelData);
            AutoFillv2.BuildWorkSheet(chosenPath, initials, styleId, procedureText, upholProcedures, photoList, photoPathsList, revInitials, date, excelData);
        }

        //checks each line inside of a string to find if they will fit the cell width
        private string fitString(string procText)
        {
            string[] lines = procText.Split('\n');
            string constructMe = "";
            foreach(string l in lines)
            {
                //if the line is too big for our cell then we'll need to split it up
                if (l.Length > 60)
                {
                    char[] longline = l.ToCharArray();
                    int place = 60;
                    bool blank = false;
                    //we'll need to check if the 39th char is whitespace or not
                    //if not we need to back up in the line till it is
                    //then we need to replace that with a return (\n)
                    //then we'll have to repackage this into a string and return it
                    while (blank == false)
                    {
                        if (longline[place] == ' ')
                        {
                            longline[place] = '\n';
                            blank = true;
                        }
                        else
                        { place--; }
                    }
                    constructMe = constructMe + new string(longline) +'\n';
                }
                else
                { constructMe = constructMe + l +'\n'; }
            }

            return constructMe;
        }

        //prompts the folder dialogue to pick an appropriate folder
        private void lookUpButton_Click(object sender, EventArgs e)
        {
            string searchResult;
            DialogResult choice = folderBrowserDialog1.ShowDialog();
            searchResult = folderBrowserDialog1.SelectedPath;

            //load the value to a gloabal var for easy referance
            chosenPath = searchResult;

            //break down the file path to just the style ID
            searchResult = Path.GetFileName(searchResult);
            styleIDBox.Text = searchResult;

            string specName = chosenPath + "\\Upholstery\\" + searchResult + " Upholstery Spec.xls";

            //clear up if the file exists already so the user can update instead of save over
            if(File.Exists(specName))
            {
                checkBox1.CheckState = CheckState.Checked;
                createButton.Enabled = false;
                createButton.Visible = false;
                updateButton.Enabled = true;
                updateButton.Visible = true;
            }
            else
            {
                checkBox1.CheckState = CheckState.Unchecked;
                createButton.Enabled = true;
                createButton.Visible = true;
                updateButton.Enabled = false;
                updateButton.Visible = false;
            }

            
        }
    }
}
