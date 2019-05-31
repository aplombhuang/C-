using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Class1
    {
        private String[,] mainMem = new String[8196, 3];
        // this 2 d array has 8196 rolls, and 1 collumns, first collum is the process, second is the valiating bits

        private int free = -1;

        public Class1() { for (int i = 0; i < 8196; i++) { mainMem[i, 0] = "free"; mainMem[i, 1] = "0"; mainMem[i, 2] = "0"; } }
        // constructor w/o parameter to initiate the Memory-page matrix, setting all memory to free, dirty bit to 0, and victim countdown to 0.

        public int checkMem()
        {
            free = 0;

            for (int i = 0; i < 8196; i++) { if (mainMem[i, 0] == "free") { free++; } }

            return free;
        }

        public String fillMem(String str, int n)
        {
            int m = n; String str1 = "";

            for (int i = 0; i < 8196; i++)
            {
                if (mainMem[i, 0] == "free" && m > 0)
                {
                    mainMem[i, 0] = str;

                    //Console.WriteLine("{0} has been added to ram", str);

                    mainMem[i, 2] = "2";

                    str1 = str1 + i + " ";

                    m--;
                }
                else { if (m == 0) { break; } }

            }
            return str1;
        }

        public void clearMem(String str, int n)
        {
            int m = n; String str1 = "";

            for (int i = 0; i < 8196; i++)
            {
                if (mainMem[i, 0] == str )
                {
                    mainMem[i, 0] = "free";
                    
                    mainMem[i, 2] = "0";
                    m--;
                }
                else { if (m == 0) { break; } }
            }
        }

        public void updatePage(String pages)
        {
            string[] numbers = Regex.Split(pages, @"\D+");

            int size = numbers.Length;

            for (int i = 0; i < size; i++)
            {
                int pageNm = int.Parse(numbers[i]);

                mainMem[pageNm, 2] = "2";
            }
        }


        public void pageAger()
        {
            for (int i = 0; i < 8196; i++)
            {
                String str = ""; int chances = 0;

                str = mainMem[i, 2];

                chances = int.Parse(str);

                if (chances > 0) { chances--; str = "" + chances; }

                mainMem[i, 2] = str;
            }
        }


    }
}
