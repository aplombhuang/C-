using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    class Process
    {
        private int status = 99; //new = 0, ready = 1, running = 2, wait = 3, terminated = 4;

        private String type = ""; //there is i/o or other type

        private int prior = 8; // integer for priority, 0 is top, 7 is the least

        //private int que = 17; // number of spot on the que, no more than 16

        private int totalBT = 0; // total burst time from process requirement

        //private int runTime = 0; // log total run time

        //private int waitTime = 0; // log total wait time

        //private int burstTime = 0; // log total burst time used

        private int size = 0; // this is the memory size for this part of the program

        private int processID = -1; // this is the id # 

        private int Code = 0; // this is the number of lines of codes 

        public Process() { } // blank constructor

        public int Pstatus { set{ status = value; } get{ return status; } } // getter and setter method for process status

        public String Ptype { set { type = value; } get { return type; } } // getter and setter method for process type

        public int Pprior { set { prior = value; } get { return prior; } } // getter and setter method for process priority

        public int PtotalBT { set { totalBT = value; } get { return totalBT; } } // getter and setter method for process total burst time

        public int Pmemory { set { size = value; } get { return size; } } // getter and setter method for process memory size

        public int PpID { set { processID = value; } get { return processID; } } // getter and setter method for process ID

        public int Pcode { set { Code = value; } get { return Code; } } // getter and setter method for process number of code
    }

}
