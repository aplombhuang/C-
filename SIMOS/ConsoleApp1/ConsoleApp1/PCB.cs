using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class PCB
    {
        private int status = -1; //ready = 0, running = 1, wait = 2, terminated = 3; note that process has new state, pcb does not

        private bool flag = false; //true if there is i/o needed for the process

        private int prior = 8; // integer for priority, 0 is top, 7 is the least

        private int que = 17; // number of spot on the que, no more than 16

        private int totalBT = 0; // total burst time from process requirement

        private int runTime = 0; // log total run time

        private int waitTime = 0; // log total wait time

        private int burstTime = 0; // log total burst time used

        private int size = 0; // this is the memory size for this part of the program

        private int processID = -1; // this is the id # in order to match the process

        private int Code = 0; // this is where line of code stored

        public PCB() { }// blank constructor

        public int PCBstatus { set { status = value; } get { return status; } } // getter and setter method for process status

        public bool hasIO { set { flag = value; } get { return flag; } } // getter and setter method for process type

        public int PCBprior { set { prior = value; } get { return prior; } } // getter and setter method for process priority

        public int PCBtotalBT { set { totalBT = value; } get { return totalBT; } } // getter and setter method for process total burst time

        public int PCBmemory { set { size = value; } get { return size; } } // getter and setter method for process memory size

        public int PCBpID { set { processID = value; } get { return processID; } } // getter and setter method for process ID

        public int PCBcode { set { Code = value; } get { return Code; } } // getter and setter method for process number of code



    }
}
