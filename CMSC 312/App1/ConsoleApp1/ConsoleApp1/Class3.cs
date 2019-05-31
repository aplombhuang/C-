using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Class3
    {
        private int stC = 3; // this is statu code, 0 is ready, 1 is running, 2 is waiting, 3 is terminate

        private int cd = 0; // total line of code from process

        private int pc = 0; //program counting line of code

        private int pr = 11; // priority code, ranking from 0 is top to 7 is the least

        private int que = 17; // que location is where it stands in the array, no more than 16

        private bool ioFlag = false; // whether or not there's a i/o which determine if it is on wait que or ready que.

        private int rt = 0; // log the total run time

        private int wt = 0; // log the total wait time

        private int bt = 0; // total burst time used

        private int tb = 0; // total burst time require

        private int psID = 0; // process ID

        public Class3() { Console.WriteLine(); }

        public int processID { set { psID = value; } get { return psID; } }

        public int totalLine { set { cd = value; } get{ return cd; } }

        public int totalBT { set { tb = value; } get { return tb; } }

        public int priority { set { pr = value; } get { return pr; } }

        public int waitTime { set { wt = value; } get { return wt; } }

        public int burstTime { set { bt = value; } get { return bt; } }

        public int status { set { stC = value; } get { return stC; } }

        public int codePt { set { pc = value; } get { return pc; } }

        public int quePos { set { que = value; } get { return que; } }

        public bool checkIO { set { ioFlag = value; } get { return ioFlag; } }

        public int runTime { set { rt = value; } get { return rt; } }

        public override string ToString()
        {
            string str = "";

            //str = pType + " " + p + " " + wt + " " + bt + " " + numL;

            return str;
        }
    }
}
