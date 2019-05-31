/*@Author: Aplomb TR huang
 *@Class: CMSC 312 2018 FALL
 *@OS simulation program 
 */ 

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter how many processes to create:");

            string fileIn = ""; int temp = 0; int count = 0; int k = 0;

            int amount = int.Parse(Console.ReadLine());

            Class2[] que0 = new Class2[amount]; // waiting que or process in "New State"

            var path = Path.Combine(Directory.GetCurrentDirectory(), "\\DataSample.txt");
            
            System.IO.StreamReader file =  new System.IO.StreamReader(path);

            while ((fileIn = file.ReadLine()) != null) { temp++; }    
            
            file.Close(); Class2[] templet = new Class2[temp];
            
            file = new System.IO.StreamReader(path); 

            while ((fileIn = file.ReadLine()) != null)
            {
                Class2 p = new Class2();

                string type = fileIn.Substring(0, fileIn.IndexOf(","));
                
                p.processType = type;
                
                string [] numbers = Regex.Split(fileIn, @"\D+");
    
                p.burstTime = int.Parse(numbers[1]);

                p.priority = int.Parse(numbers[2]);

                p.Memory = int.Parse(numbers[3]);
                
                p.processID = count;

                templet[count] = p;

                count++;
            }
            file.Close();

            int a, b, c, d ;

                a = Convert.ToInt32(0.05 * amount); // amount of i/o

                b = Convert.ToInt32(0.65 * amount); // amount of calculation

                c = Convert.ToInt32(0.35 * amount); // amount to browser

                d = Convert.ToInt32(0.05 * amount); // amount of data transfer

                b = b + (amount - (a + b + c + d));  Console.WriteLine(amount - amount - (a + b + c + d));

            bool finished = true; int record = -1; Random rnd = new Random();

            while (finished)
            {
                int pick = rnd.Next(amount); // choose a random location to store the process

                if (que0[pick] == null && pick != record)
                {
                    record = pick;

                    if (a > 0)
                    {
                        Class2 p = new Class2();

                        p = templet[0];

                        int n = templet[0].burstTime; 

                        int burst = rnd.Next(5, n+6); // creates a random cpu burst time 

                        p.burstTime = burst;

                        n = templet[0].priority;

                        int prio = rnd.Next(n+3);   // creates a random priority 

                        p.priority = prio;

                        n = templet[0].Memory;

                        int mem = rnd.Next(1, n+1); // creates a random memory requirement

                        p.Memory = mem;

                        p.processID = pick;

                        que0[pick] = p;
                        
                        a--;

                    }else if (b > 0)
                    {
                        Class2 p = new Class2();

                        p = templet[1];

                        int n = templet[1].burstTime;

                        int burst = rnd.Next(5, n + 6); // creates a random cpu burst time 

                        p.burstTime = burst;

                        n = templet[1].priority;

                        int prio = rnd.Next(n + 3);   // creates a random priority 

                        p.priority = prio;

                        n = templet[1].Memory;

                        int mem = rnd.Next(7, n + 2); // creates a random memory requirement

                        p.Memory = mem;

                        p.processID = pick;

                        que0[pick] = p;

                        b--;

                    }else if (c > 0)
                    {
                        Class2 p = new Class2();

                        p = templet[3];

                        int n = templet[3].burstTime;

                        int burst = rnd.Next(5, n + 6); // creates a random cpu burst time 

                        p.burstTime = burst;

                        n = templet[3].priority;

                        int prio = rnd.Next(n + 3);   // creates a random priority 

                        p.priority = prio;

                        n = templet[3].Memory;

                        int mem = rnd.Next(30, n + 2); // creates a random memory requirement

                        p.Memory = mem;

                        p.processID = pick;

                        que0[pick] = p;

                        c--;

                    }else if (d > 0)
                    {
                        Class2 p = new Class2();

                        p = templet[2];

                        int n = templet[2].burstTime;

                        int burst = rnd.Next(n - 5, n + 6); // creates a random cpu burst time 

                        p.burstTime = burst;

                        n = templet[2].priority;

                        int prio = rnd.Next(n + 3);   // creates a random priority 

                        p.priority = prio;

                        n = templet[2].Memory;

                        int mem = rnd.Next(20, n + 2); // creates a random memory requirement

                        p.Memory = mem;

                        p.processID = pick;

                        que0[pick] = p;

                        d--;
                    }
                    
                }

                finished = Array.Exists(que0, element => element == null);
            }

            for (int i = 0; i < amount; i++)
            {
                que0[i].processID = i+1;

                que0[i].Status = 0;

                Console.WriteLine(que0[i]);
            } //rename all process id, set all status to 0

            Class3[] que1 = new Class3[3]; //io schedule;
       
            Class3[] que2 = new Class3[6]; //shortest time first schedule;

            Class3[] que3 = new Class3[12]; // round robin;

            Class3[] que4 = new Class3[6]; //FIFO schedule for background;
            
            Class1 Paging = new Class1();

            int freeRam = 0; finished = false;

            while (!finished)
            {
                int i = 0; freeRam = Paging.checkMem();

                Console.WriteLine("\n Free page is: {0}. \n", freeRam);

                bool q1 = Array.Exists(que1, element => element == null);

                bool q2 = Array.Exists(que2, element => element == null);

                bool q3 = Array.Exists(que3, element => element == null);

                bool q4 = Array.Exists(que4, element => element == null);
                                
                while ((q1 || q2 || q3 || q4) && freeRam >0)
                {
                     
                    while ( i<amount) { if (que0[i].Status != 0) { i++; } else { break; } } 
                    // scan from the list of process, skipping the ones that is already in ready/run que or teminated

                    if (i == amount) { break; }

                    if (que0[i].processType.Equals("i/o") && q1 && que0[i].Status == 0 )
                    {
                        Class3 pcb = new Class3(); int m = 0;

                        pcb.checkIO = true;

                        pcb.processID = que0[i].processID;

                        pcb.totalBT = que0[i].burstTime;

                        while (m < 3) { if (que1[m] == null) { break; } else m++; }

                        pcb.quePos = m;

                        que1[m] = pcb;

                        que0[i].Status = 1;

                        String tempt = "" + que0[i].processID;

                        String pageTable = Paging.fillMem(tempt, que0[i].Memory);

                        Console.WriteLine("added page table: {0}", pageTable);

                        string str = "The process " + que0[i].processType + " process code: " + que1[m].processID + " is now in i/o que. status is " + que0[i].Status;

                        Console.WriteLine(str); 

                    }

                    else if (que0[i].burstTime < 10 && q2 && que0[i].Status == 0)
                    {
                        Class3 pcb = new Class3(); int m = 0;

                        pcb.processID = que0[i].processID;

                        pcb.totalBT = que0[i].burstTime;
                        
                        while (m < 6)
                        {
                            if (que2[m] == null) { break; }

                            if (pcb.totalBT <= que2[m].totalBT)
                            {
                                int j = m+1;
                                
                                while(j < 6) { if(que2[j] == null){ break; } }

                                while( j > m) { que2[j] = que2[j - 1]; j--; }

                                break;
                                                                                             
                            } else m++;
                        }

                        pcb.quePos = m;

                        que2[m] = pcb;

                        que0[i].Status = 1;

                        String tempt = "" + que0[i].processID;

                        String pageTable = Paging.fillMem(tempt, que0[i].Memory);

                        Console.WriteLine(pageTable);

                        string str = "The process " + que0[i].processType + " process code: " + que2[m].processID + " is now in shortest burst time que. status is " + que0[i].Status;

                        Console.WriteLine(str); 
                    }

                    else if (que0[i].burstTime < 60 && q3 && que0[i].Status == 0)
                    {
                        Class3 pcb = new Class3(); int m = 0;

                        pcb.processID = que0[i].processID;

                        pcb.totalBT = que0[i].burstTime;
                        
                        while (m < 12) { if (que3[m] == null) { break; } else m++; }

                        pcb.quePos = m;

                        que3[m] = pcb;
                                                
                        que0[i].Status = 1;

                        String tempt = "" + que0[i].processID;

                        String pageTable = Paging.fillMem(tempt, que0[i].Memory);

                        Console.WriteLine(pageTable);

                        string str = "The process " + que0[i].processType + " process code: " + que3[m].processID + " is now in RoundRobin. status is " + que0[i].Status;

                        Console.WriteLine(str); 
                    }

                    else if ( q4 && que0[i].Status == 0)
                    {
                        Class3 pcb = new Class3(); int m = 0;

                        pcb.processID = que0[i].processID;

                        pcb.totalBT = que0[i].burstTime;
                        
                        while (m < 6) { if (que4[m] == null) { break; } else m++; }

                        pcb.quePos = m;

                        que4[m] = pcb;

                        que0[i].Status = 1;

                        String tempt = "" + que0[i].processID;

                        String pageTable = Paging.fillMem(tempt, que0[i].Memory);

                        Console.WriteLine(pageTable);

                        string str = "The process " + que0[i].processType + " process code: " + que4[m].processID + " is now in background FIFO que. status is " + que0[i].Status;

                        Console.WriteLine(str); 
                    }

                    q1 = Array.Exists(que1, element => element == null);

                    q2 = Array.Exists(que2, element => element == null);

                    q3 = Array.Exists(que3, element => element == null);

                    q4 = Array.Exists(que4, element => element == null);

                }

                freeRam = Paging.checkMem();

                Console.WriteLine("\n Free page is: {0}. \n", freeRam);

                q1 = Array.Exists(que1, element => element != null);

                q2 = Array.Exists(que2, element => element != null);

                q3 = Array.Exists(que3, element => element != null);

                q4 = Array.Exists(que4, element => element != null);
                
                if (q1)
                {
                    for (k = 0; k < que1.Length; k++)
                    {
                        if(que1[k] != null)
                        {
                            string str = "i/o " + " process code: " + que1[k].processID + " is now running. status is 2";

                            Console.WriteLine(str);

                            str = "i/o " + " process code: " + que1[k].processID + " finished running and terminated. status is 3";

                            int id = que1[k].processID; que0[id].Status = 3; que1[k] = null;
                            
                        }
                    }

                }

                k = 0;

                if (q2)
                {
                    Console.WriteLine("Processing shortest burst time que.");

                    while (k < que2.Length && que2[k] == null) { k++; }

                    int id = que2[k].processID;

                    //if (que0[id].processType.Equals("yield")) { Console.WriteLine("Yield encountered."); Environment.Exit(0); }
                                            
                            string str = "Process ID " + id + " is now running. status is 2";

                            Console.WriteLine(str);

                        for (int j = 1; j <= que2[k].totalBT; j++) { Console.WriteLine("Burst time: {0}", j); }

                            str = "Process ID" + id + " finished running and terminated. status is 3";

                             que0[id].Status = 3; que2[k] = null;
                        
                }

                k = 0;

                if (q3)
                {
                    Console.WriteLine("Processing round robin.");
                    
                    while (k < que3.Length )
                    {
                        if (que3[k] != null)
                        {
                            int id = que3[k].processID;

                            string str = "Process ID " + id + " is now running. burst time +5, status is 2";

                            Console.WriteLine(str);

                            que3[k].burstTime = que3[k].burstTime + 5;

                            if (que3[k].totalBT - que3[k].burstTime < 10)
                            {
                                str = "Process ID" + id + " now promoted to shortest burst time first que. status is 0";

                                que0[id].burstTime = que0[id].burstTime - que3[k].burstTime;

                                que0[id].Status = 0; que3[k] = null;
                            }
                            
                        }

                        k++;

                        if (k == que3.Length) { break; }
                    }

                }

                k = 0;

                if (q4)
                {
                    Console.WriteLine("Processing backround FIFO.");

                    while (k < que4.Length && que4[k] == null) { k++; }

                    if (k < que4.Length)
                    {

                        int id = que4[k].processID;

                        string str = "Process ID " + id + " is now running. burst time +10, status is 2";

                        Console.WriteLine(str);

                        que4[k].burstTime = que4[k].burstTime + 10;

                        if (que4[k].totalBT - que4[k].burstTime < 60)
                        {
                            str = "Process ID" + id + " now promoted to round robin. status is 0";

                            que0[id].burstTime = que0[id].burstTime - que4[k].burstTime;
                            
                            que0[id].Status = 0; que4[k] = null;
                        }
                    }
                 }
                break;
                finished = Array.TrueForAll(que0, element => element.Status ==4);
             
            }
              
            Thread th = Thread.CurrentThread;

            th.Name = "MainThread";

            Console.WriteLine(" ");

            Console.WriteLine("This is {0} \n", th.Name);                     

            ThreadStart childref = new ThreadStart(CallToChildThread);

            Console.WriteLine("In Main: Creating the Child thread");

            Thread childThread = new Thread(childref);

            childThread.Start(); 

            Console.WriteLine("The main thread id  {0} is in state : {1}", th.ManagedThreadId, th.ThreadState);

            Console.WriteLine("The child thread id  {0} is in state : {1}", childThread.ManagedThreadId, childThread.ThreadState);

            Console.ReadKey();
        }

        public static void CallToChildThread()
        {
            Console.WriteLine("Child thread starts");
        }
    }
}
    

