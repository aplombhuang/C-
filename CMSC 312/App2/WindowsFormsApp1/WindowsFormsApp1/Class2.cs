using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Class2
    {
        private int p = 1000; // priority
        private int RamNeed = 0; // memory requirement
        private int bt = 0; // cpu burst time
        private int numL = 0; //line number for code
        private int stCode = 0; // status code, 0 is waiting, 1 is ready/ running, 2 is terminated
        private string pType = ""; //process type
        private int psID = 0;// process code
                             //private object o;

        public Class2()
        {
            Console.WriteLine();
        }

        public int priority { set { p = value; } get { return p; } }

        //public int ramAddr { set { ramAd = value; } get { return ramAd; } }

        public int burstTime { set { bt = value; } get { return bt; } }

        public string processType { set { pType = value; } get { return pType; } }

        //public object objInfo { set { o = value; } get{ return o; } }

        public int CodeLine { set { numL = value; } get { return numL; } }

        public int Status { set { stCode = value; } get { return stCode; } }

        public int processID { set { psID = value; } get { return psID; } }

        public int Memory { set { RamNeed = value; } get { return RamNeed; } }

        public override string ToString()
        {
            string str = "";

            str = pType + " " + p + " " + " " + bt + " " + RamNeed + " " + psID;

            return str;
        }

    }
}
