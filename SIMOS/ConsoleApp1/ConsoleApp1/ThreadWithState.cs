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
        private string boilerplate;
        private int value;

        // The constructor obtains the state information.
        public ThreadWithState(string text, int number)
        {
            boilerplate = text;
            value = number;
        }

        // The thread procedure performs the task, such as formatting
        // and printing a document.
        public void ThreadProc()
        {
            Console.WriteLine(boilerplate, value);
        }
    }

    // Entry point for the example.
    //
}

