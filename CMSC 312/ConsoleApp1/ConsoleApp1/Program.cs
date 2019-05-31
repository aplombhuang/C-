using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // first test, pass array

            Process[] que0 = new Process[3];

            writeArray(que0, 1);

            //Console.WriteLine(que0[1].Pstatus);

            // Supply the state information required by the task.
        ThreadWithState tws = new ThreadWithState(que0[1], 1);

            // Create a thread to execute the task, and then
            // start the thread.
            Thread t = new Thread(new ThreadStart(tws.ThreadProc));
            t.Start();
            //Console.WriteLine("Main thread does some work, then waits.");
            t.Join();
           // Console.WriteLine("Independent task has completed; main thread ends.");

        }

        public static void writeArray(Process[] arr, int i)
        {
            Process p1 = new Process();
            p1.Pstatus = 4;
            arr[1] = p1;

        }
    }
}
