using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Person
    {
        private int x;

        private double y;

        private string str;

        private double result;

        public Person() { Console.WriteLine("First Object"); }

        //public void setNum(int n) { x = n; }

        // public void setDoub(double m) { y = m; }

        //public void setStr(string str) { this.str = str; }

        // public int getNum() { return x; }

        // public double getDoub() { return y; }

        //public string getStr() { return str; }

        public void updater()
        {
            x++; y--; result = y / x; str += "\n";

            str = "result is " + result + str;
        }

        public string getResult
        { 
            get { return str; }
        }

        public string Name
        {
            set { str = value; }
            get { return str; }
        }

        public int age
        {
            set { x = value; }
            get { return x; }
        }

        public double height
        {
            set { y = value; }
            get { return y; }
        }

        public string Adds { get; set; } // auto-implement
    }
}
