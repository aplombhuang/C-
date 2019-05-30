using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;// library in reference to office is needed to read from excel file;

namespace Library_App_1
{
    static class Program
    {
        public static string fileName, year, semester; // public because it passes the data between the UI window and this back end program

        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            System.Windows.Forms.Application.Run(new Form1());

            MessageBox.Show("Received file parth: " + fileName + "\nReceived year and semester entry: " + semester + year);

            List<string> course = new List<string>(); // contains department code and course number
            List<string> section = new List<string>(); // store section number if there's any
            List<string> author = new List<string>(); // store author info
            List<string> title = new List<string>();  // store title info
            List<string> edition = new List<string>(); // store edition info if there's any
            List<string> pbYear = new List<string>(); //  store publish year info if there's any
            List<string> publisher = new List<string>(); // store publisher info if there's any
            List<string> isbn = new List<string>(); // store ISBN info
            List<string> accFlag = new List<string>(); // flag for access code
            List<string> dskFlag = new List<string>(); // flag for media, such as dvd or vcd or cd
            List<string> ctmFlag = new List<string>(); // flag for customed books

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(fileName);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            
             int i = 1, j = 1 , k ; 
            
            int deptNum = 99, authNum = 99, titlNum = 99, edtnNum = 99, plbsNum = 99, isbnNum = 99, sectNum = 99, 
                    aflag = 0, mflag = 0, cflag = 0;
            
            while (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
            {
                    string colName = xlRange.Cells[i, j].Value2.ToString();
                    if (colName.Equals("Dept", StringComparison.OrdinalIgnoreCase)) { deptNum = j;  }
                    else if (colName.Equals("Author",StringComparison.OrdinalIgnoreCase)) { authNum = j; }
                    else if (colName.Equals("Title", StringComparison.OrdinalIgnoreCase)) { titlNum = j; }
                    else if (colName.Equals("Edition", StringComparison.OrdinalIgnoreCase) || colName.Equals("Ed", StringComparison.OrdinalIgnoreCase)) { edtnNum = j; }
                    else if (colName.Equals("Publisher", StringComparison.OrdinalIgnoreCase) || colName.Equals("Pub", StringComparison.OrdinalIgnoreCase)) { plbsNum = j; }
                    else if (colName.Equals("ISBN", StringComparison.OrdinalIgnoreCase) ) { isbnNum = j;  }
                    else if (colName.Equals("Section", StringComparison.OrdinalIgnoreCase) || colName.Equals("Sec", StringComparison.OrdinalIgnoreCase)) { sectNum = j; }
                 
                j++;
            }                
            

            if (deptNum == 99 ||authNum == 99 || titlNum == 99 || edtnNum == 99 || plbsNum == 99 || isbnNum == 99)
            { MessageBox.Show("There's error, program failed to read column names correctly. \n Application will close now."); Application.Exit(); }
            

            string temp; i = 2; j = 1;

            while (xlRange.Cells[i, 1] != null && xlRange.Cells[i, 1].Value2 != null)
            {
                j = deptNum; // find the column number for department and concast department char to course level numb and add to course array 

                if (xlRange.Cells[i, j].Value2 != null)
                {
                    temp = "" + xlRange.Cells[i, j].Value2.ToString() + xlRange.Cells[i, j + 1].Value2.ToString(); // concast if not null

                    course.Add(temp);
                    
                }
                else { course.Add("NULL"); } // store null

                j = authNum; // find the column number for author and add author info to the array 

                if (xlRange.Cells[i, j].Value2 != null)
                {
                    temp = "" + xlRange.Cells[i, j].Value2.ToString();

                    author.Add(temp);

                }
                else { author.Add("NULL"); }

                j = titlNum; // find the column number for title, store in array, then process the flags 

                if (xlRange.Cells[i, j].Value2 != null)
                {
                    temp = "" + xlRange.Cells[i, j].Value2.ToString(); title.Add(temp); // adding the whole title string to array

                    if (temp.Contains("<CUSTOM>") || temp.Contains(">CUSTOM<")) { cflag = 1; } // check for custom and flag it

                    string[] separator = { "W/" };

                    var flags = temp.Split(separator, StringSplitOptions.None); // try to split the string in to 2 part by using "W/" as marker

                    if (flags.Length > 1) // if there are 2 strings after the split
                    {
                        string flag = flags[1]; // the flags are in the second part of the splitted string

                        if (flag.Length > 3)
                        {
                            aflag = 1; // since the flag string is longer than 3, which rules out the string "dvd" or "cd", then access code must be there

                            if (flag.Contains("DVD") || flag.Contains("CD")) { mflag = 1; } // find out if there's cd or dvd
                            else { mflag = 0; } // if not then disk is NO

                        }
                        else { aflag = 0; mflag = 1; } // since string lenght is smaller than 4 there must be dvd or cd so access flag is NOT, cd or dvd is yes

                    }
                    else
                    {
                        string flag = flags[0]; // the flags are in the first part of the splitted string

                        mflag = 0; // it automatically rules out any media flag

                        if (flag.Contains("ACCESS")) { aflag = 1; } // if title contain the keyword access, flag access flag
                        else { aflag = 0; }

                    } // there the input string cannot be splitted, there's no flag at all
                    
                    accFlag.Add(""+aflag);// adding all the flags to their coresponding array
                    dskFlag.Add(""+mflag);
                    
                }
                else { title.Add("NULL"); }

                j = edtnNum; // find the column number for edition, separate edition and publish year info and store in array

                if (xlRange.Cells[i, j].Value2 != null)
                {
                    temp = "" + xlRange.Cells[i, j].Value2.ToString();

                    int l = temp.Length; int n;

                    string ed = "";  // this is a string that stores edition information

                    string yr = ""; // this is a string that stores the publishing year

                    if (l == 1)
                    {
                        if (Int32.TryParse(temp, out n)) { yr = "200" + temp; ed = "NULL"; } // if the string is an integer, meaning its in single digit year, so we add it up to 2000s
                        else { ed = "NULL"; yr = "NULL"; } // if string is not an integer, we consider both info are null
                    }
                    else if (l == 2)
                    {
                        if (Int32.TryParse(temp, out n)) // if the string is an integer, meaning its in double digits year,
                        {
                            int m = Int32.Parse(temp);

                            if (m < 20) { yr = "20" + temp; } // since year is double digit, we add up to 2000s if the last digit is smaller than 20 (between 2000 and 2019)

                            else { yr = "19" + temp; } // since year is bigger than 20, then we add up to 1900s (between 1920 and 1999)

                            ed = "NULL";
                        }
                        else { ed = temp; yr = "NULL"; } // if string is not an integer, we store it as edition information
                    }
                    else if (l == 3)
                    {
                        if (Int32.TryParse((temp.Substring(2)), out n)) // if the last character of the string is an integer, meaning its a single digit year,
                        {
                            yr = "200" + temp.Substring(2); // since the year is single digit, we add up to 2000s

                            ed = "NULL";
                        }
                        else { ed = temp; yr = "NULL"; } // if string is not an integer, we store it as edition information
                    }
                    else if (l == 4)
                    {
                        if (Int32.TryParse(temp, out n)) // if the last character of the string is an integer, meaning its full 4 digit year,
                        {
                            yr = temp; // since the year is single digit, we add up to 2000s

                            ed = "NULL";
                        }
                        else { ed = temp; yr = "NULL"; } // if string is not an integer, we store it as edition information
                    }
                    else if (l == 5)
                    {
                        if (Int32.TryParse(temp.Substring(1), out n)) { yr = temp.Substring(1); ed = "NULL"; } // if the last 4 digit is year, store year and the rest as edition

                        else if (Int32.TryParse(temp.Substring(3), out n)) // if the last character of the string is an integer, meaning its a double digit year,
                        {
                            int m = Int32.Parse(temp.Substring(3));

                            if (m < 20) { yr = "20" + temp.Substring(3); } // since year is double digit, we add up to 2000s if the last digit is smaller than 20 (between 2000 and 2019)

                            else { yr = "19" + temp.Substring(3); } // since year is bigger than 20, then we add up to 1900s (between 1920 and 1999)

                            ed = temp.Substring(0, 2); // store the first character of the string as edition since the middle character is an empty space " " .
                        }
                        else { ed = temp; yr = "NULL"; } // if string is not an integer, we store it as edition information = "NULL";
                    }
                    else if (l == 6)
                    {
                        if (Int32.TryParse(temp.Substring(2), out n)) { yr = temp.Substring(2); ed = temp.Substring(0, 1); } // if the last 4 digit is year, store year and the rest as edition

                        else if (Int32.TryParse(temp.Substring(4), out n)) // if the last character of the string is an integer, meaning its a double digit year,
                        {
                            int m = Int32.Parse(temp.Substring(4));

                            if (m < 20) { yr = "20" + temp.Substring(4); } // since year is double digit, we add up to 2000s if the last digit is smaller than 20 (between 2000 and 2019)

                            else { yr = "19" + temp.Substring(4); } // since year is bigger than 20, then we add up to 1900s (between 1920 and 1999)

                            ed = temp.Substring(0, 3); // store the first character of the string as edition since the middle character is an empty space " " .
                        }
                        else { ed = temp;  yr = "NULL"; } // if string is not an integer, we store it as edition information
                    }
                    else if (l == 7)
                    {
                        if (Int32.TryParse(temp.Substring(3), out n)) { yr = temp.Substring(3); ed = temp.Substring(0, 2); } // if the last 4 digit is year, store year and the rest as edition

                        else if (Int32.TryParse(temp.Substring(5), out n)) // if the last character of the string is an integer, meaning its a double digit year,
                        {
                            int m = Int32.Parse(temp.Substring(5));

                            if (m < 20) { yr = "20" + temp.Substring(5); } // since year is double digit, we add up to 2000s if the last digit is smaller than 20 (between 2000 and 2019)

                            else { yr = "19" + temp.Substring(5); } // since year is bigger than 20, then we add up to 1900s (between 1920 and 1999)

                            ed = temp.Substring(0, 4); // store the first character of the string as edition since the middle character is an empty space " " .
                        }
                        else { ed = temp; yr = "NULL"; } // if string is not an integer, we store it as edition information

                    }
                    else if (l == 8)
                    {
                        MessageBox.Show("there's a length of 8 at row " + (i-1) + "\n Content: " + temp); // since I never found a string of 8 in year, and edition, so  this becomes a notice
                    }

                    edition.Add(ed);

                    pbYear.Add(yr);

                }
                else { edition.Add("NULL"); pbYear.Add("NULL"); }

                j = plbsNum; // find the column number for publisher, store in array, then process the custom flags 

                if (xlRange.Cells[i, j].Value2 != null)
                {
                    temp = "" + xlRange.Cells[i, j].Value2.ToString();

                    if (temp.Contains("CUSTOM")) { cflag = 1; }

                    publisher.Add(temp);
                }
                else { publisher.Add("NULL"); }

                ctmFlag.Add(""+cflag); // now since we checked both title and publisher for custom flags, it is ready to add to array

                j = isbnNum; // find the column number for ISNB, remove "-" and store in array

                if (xlRange.Cells[i, j].Value2 != null)
                {
                    temp = "" + xlRange.Cells[i, j].Value2.ToString();

                    if (temp.Length == 17) // i am doing this because there exists a "NONE" as isbn
                    {

                        var deIS = temp.Split('-');

                        temp = "" + deIS[0] + deIS[1] + deIS[2] + deIS[3] + deIS[4];

                    }

                    isbn.Add(temp);

                }
                else { isbn.Add("NULL"); }
                
                if (sectNum == -1 || sectNum == 99) { section.Add("NULL"); }
                else
                {
                    j = sectNum;

                    temp = "" + xlRange.Cells[i, j].Value2.ToString();

                    section.Add(temp);
                }

                k = i - 2;

                Console.WriteLine(i + " " + course[k] +
                                  ", " + section[k] + ", " + isbn[k] + ", " + title[k] +
                                  ", " + author[k] + ", " + publisher[k] + ", " + edition[k] +
                                  ", " + pbYear[k] + ", " + accFlag[k] + ", " + dskFlag[k] +
                                  ", " + ctmFlag[k] );

                i++; 

                aflag = 0; mflag = 0; cflag = 0;// reset all flags  
            }


            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

            var outFileStr = fileName.Split('.'); // split the input file path in half

            string outFile = outFileStr[0] + "_OUT.csv"; //attach the same input file name but attach new file type extention to it

            using (System.IO.StreamWriter file =
           new System.IO.StreamWriter(outFile)) // just wrting info to output file.
            {
                for (k = 0; k < isbn.Count; k++)
                {
                    string line = "" + year + ", " + semester + ", " + course[k] +
                               ", " + section[k] + ", " + isbn[k] + ", " + title[k] +
                               ", " + author[k] + ", " + publisher[k] + ", " + edition[k] +
                               ", " + pbYear[k] + ", " + accFlag[k] + ", " + dskFlag[k] +
                               ", " + ctmFlag[k];
                   
                    file.WriteLine(line);
                }   
            }
            MessageBox.Show("The output file will be ready in the same directory of Excel File."); // end of program message 
        }
    }
}
