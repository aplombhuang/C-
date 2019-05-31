using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class ThreadWithState
    {
        // State information used in the task.
        private Process [] arr ;
        private int value;
        
        private Process p1;

        // The constructor obtains the state information.
        public ThreadWithState(Process p1, int i)
        {
            this.p1 = p1;
            p1.Pstatus = 1;
            //arr[i] = p1;
            
        }

        // The thread procedure performs the task, such as formatting
        // and printing a document.
        public void ThreadProc()
        {
            //Console.WriteLine(p1.Pstatus);
        }
    }

    // Entry point for the example.
    //
}

