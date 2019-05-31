using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        static int amount = 0; static int temp = 0;

        int time = 0; int a, b, c, d, z = 0;

        static Class3[] que1 = new Class3[3]; //io schedule;

        static Class3[] que2 = new Class3[6]; //shortest time first schedule;

        static Class3[] que3 = new Class3[12]; // round robin;

        static Class3[] que4 = new Class3[6]; //FIFO schedule for background;

        static Class1 Paging = new Class1();

        static int freeRam = 0;

        static thread1 t1 = new thread1();

        static thread2 t2 = new thread2();

        Timer timer2 = new Timer();

        Timer timer = new Timer();

        public Form1()
        {
            InitializeComponent();

            this.Load += Form1_Load;

            this.Load += Form1_Load2;

            //this.Load += Form1_Load3;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string fileIn = "";

            var path = Path.Combine(Directory.GetCurrentDirectory(), "\\DataSample.txt");

            System.IO.StreamReader file = new System.IO.StreamReader(path);

            while ((fileIn = file.ReadLine()) != null) { temp++; }

            file.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button3.Enabled = true;

            amount = int.Parse(textBox1.Text);

        }

        static Class2[] templet;

        private void Form1_Load2(object sender, EventArgs e)
        {
            templet = new Class2[temp];

            string fileIn = ""; int count = 0;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "\\DataSample.txt");

            System.IO.StreamReader file = new System.IO.StreamReader(path);

            while ((fileIn = file.ReadLine()) != null)
            {
                Class2 p = new Class2();

                string type = fileIn.Substring(0, fileIn.IndexOf(","));

                p.processType = type;

                string[] numbers = Regex.Split(fileIn, @"\D+");

                p.burstTime = int.Parse(numbers[1]);

                p.priority = int.Parse(numbers[2]);

                p.Memory = int.Parse(numbers[3]);

                p.processID = count;

                templet[count] = p;

                count++;
            }
            file.Close();

        }

        private void Form1_Load3()
        {
            a = Convert.ToInt32(0.05 * amount); // amount of i/o

            b = Convert.ToInt32(0.65 * amount); // amount of calculation

            c = Convert.ToInt32(0.35 * amount); // amount to browser

            d = Convert.ToInt32(0.05 * amount); // amount of data transfer

            b = b + (amount - (a + b + c + d));

            Form1_Load4();
        }

        static Class2[] que0;

        private void Form1_Load4()
        {
            que0 = new Class2[amount]; // waiting que or process in "New State"

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

                        int burst = rnd.Next(5, n + 6); // creates a random cpu burst time 

                        p.burstTime = burst;

                        n = templet[0].priority;

                        int prio = rnd.Next(n + 3);   // creates a random priority 

                        p.priority = prio;

                        n = templet[0].Memory;

                        int mem = rnd.Next(1, n + 1); // creates a random memory requirement

                        p.Memory = mem;

                        p.processID = pick;

                        que0[pick] = p;

                        a--;

                    }
                    else if (b > 0)
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

                    }
                    else if (c > 0)
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

                    }
                    else if (d > 0)
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
                int num = i + 1;

                que0[i].processID = num;

                que0[i].Status = 0;

            } //rename all process id, set all status to 0

        }

        private void textBox2_TextChanged(object sender, EventArgs e){ }
        
        private void label1_Click(object sender, EventArgs e) {}

        Thread my_t1 = new Thread(new ThreadStart(t1.thread_1));

        Thread my_t2 = new Thread(new ThreadStart(t2.thread_2));

        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            
            Form1_Load5();

            my_t1.Start();

            my_t2.Start();


            timer2.Tick += new EventHandler(on_timer_event2);
            timer2.Interval = 100;
            timer2.Enabled = true;
        }

        private void on_timer_event1(object sender, EventArgs e)
        {
            time++;
            if (z < amount) { que0[z].processID = z + 1; textBox2.Text += "Process generated: " + que0[z] + "." + Environment.NewLine; z++; }
            textBox2.SelectionStart = textBox2.Text.Length;
            textBox2.ScrollToCaret();

            if (z == amount) { timer.Stop(); time = 0; }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;

            Form1_Load3();

            MessageBox.Show("You decided to create " + amount + " processes randomly.");

            textBox2.Text += "Program has started: " + Environment.NewLine;

            timer.Tick += new EventHandler(on_timer_event1);
            timer.Interval = 1000/amount;
            timer.Enabled = true;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer2.Stop();

            textBox2.Text += "Program is Terminated manually.";

            time = 0;

        }

        private void on_timer_event2(object sender, EventArgs e)
        {
             time++;
            textBox2.Text += "CPU clock passed : " + time + "ms" + Environment.NewLine;
            
            textBox2.SelectionStart = textBox2.Text.Length;
            textBox2.ScrollToCaret();

            Form1_Load6();

        }

        private void Form1_Load5()//object sender, EventArgs e)
        { 
             int i = 0;
            bool q1 = false;

            bool q2 = false;

            bool q3 = false;

            bool q4 = false;

            freeRam = 8196;
            
            while (freeRam > 0)
            {
                string process = "" + que0[i].processID;
                Paging.fillMem(process, que0[i].Memory);
                i++; if (i == amount) { break; }
                freeRam = Paging.checkMem();
                double ramP = (8196.0 - freeRam)/8196;

                textBox2.Text += "Ram Usage percent: " + ramP*100 +"%" + Environment.NewLine; 
            }

            i = 0;

            do
            {

                freeRam = Paging.checkMem();

                 q1 = Array.Exists(que1, element => element == null);

                 q2 = Array.Exists(que2, element => element == null);

                 q3 = Array.Exists(que3, element => element == null);

                 q4 = Array.Exists(que4, element => element == null);

                while (i < amount) { if (que0[i].Status != 0) { i++; } else { break; } }
                // scan from the list of process, skipping the ones that is already in ready/run que or teminated

                if (i == amount) { break; }

                if (que0[i].processType.Equals("i/o") && q1 && que0[i].Status == 0)
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

                    textBox2.Text += str + Environment.NewLine;

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
                            int j = m + 1;

                            while (j < 6) { if (que2[j] == null) { break; } }

                            while (j > m) { que2[j] = que2[j - 1]; j--; }

                            break;

                        }
                        else m++;
                    }

                    pcb.quePos = m;

                    que2[m] = pcb;

                    que0[i].Status = 1;

                    String tempt = "" + que0[i].processID;

                    String pageTable = Paging.fillMem(tempt, que0[i].Memory);

                    Console.WriteLine(pageTable);

                    string str = "The process " + que0[i].processType + " process code: " + que2[m].processID + " is now in shortest burst time que. status is " + que0[i].Status;

                    textBox2.Text += str + Environment.NewLine;
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

                    textBox2.Text += str + Environment.NewLine;
                }

                else if (q4 && que0[i].Status == 0)
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

                    textBox2.Text += str + Environment.NewLine;
                }
             } while ((q1 || q2 || q3 || q4) && freeRam > 0);
            
        }
        static int k = 0;
        private void Form1_Load6()
        {
            bool finished = false;
            

            bool q1 = false;

            bool q2 = false;

            bool q3 = false;

            bool q4 = false;

           
                 if (k < amount) {
                String str = "" + que0[k].processID; int m = Paging.checkMem();

                t1.thread1_toProcess(str, m);

                Paging.clearMem(str, que0[k].Memory);

                k++;

                str = "" + que0[k].processID; m = Paging.checkMem();

                t2.thread2_toProcess(str, m);

                Paging.clearMem(str, que0[k].Memory);

                k++;
                    }
            

            while (!finished) { 

            if (q1)
            {
                for (k = 0; k < que1.Length; k++)
                {
                    if (que1[k] != null)
                    {
                        string str = "i/o " + " process code: " + que1[k].processID + " is now running. status is 2";

                            textBox2.Text += str + Environment.NewLine;

                            str = "i/o " + " process code: " + que1[k].processID + " finished running and terminated. status is 4";

                            textBox2.Text += str + Environment.NewLine;

                            int id = que1[k].processID; que0[id].Status = 4;

                            str = "" + id;

                            que0[id].Status = 4; Paging.clearMem(str, que0[id].Memory);

                            que1[k] = null;

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

                    textBox2.Text += str + Environment.NewLine;

                    for (int j = 1; j <= que2[k].totalBT; j++) { Console.WriteLine("Burst time: {0}", j); }

                str = "Process ID" + id + " finished running and terminated. status is 4"; str = "" + id;

                    textBox2.Text += str + Environment.NewLine;

                    que0[id].Status = 4; Paging.clearMem(str, que0[id].Memory);

                    que2[k] = null;

            }

            k = 0;

            if (q3)
            {
                Console.WriteLine("Processing round robin.");

                while (k < que3.Length)
                {
                    if (que3[k] != null)
                    {
                        int id = que3[k].processID;

                        string str = "Process ID " + id + " is now running. burst time +5, status is 2";

                            textBox2.Text += str + Environment.NewLine;

                            que3[k].burstTime = que3[k].burstTime + 5;

                        if (que3[k].totalBT - que3[k].burstTime < 10)
                        {
                            str = "Process ID" + id + " now promoted to shortest burst time first que. status is 0";

                                textBox2.Text += str + Environment.NewLine;

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

                        textBox2.Text += str + Environment.NewLine;

                        que4[k].burstTime = que4[k].burstTime + 10;

                    if (que4[k].totalBT - que4[k].burstTime < 60)
                    {
                        str = "Process ID" + id + " now promoted to round robin. status is 0";

                            textBox2.Text += str + Environment.NewLine;

                            que0[id].burstTime = que0[id].burstTime - que4[k].burstTime;

                        que0[id].Status = 0; que4[k] = null;
                    }
                }
            }

                q1 = Array.Exists(que1, element => element == null);

                q2 = Array.Exists(que2, element => element == null);

                q3 = Array.Exists(que3, element => element == null);

                q4 = Array.Exists(que4, element => element == null);

                break;

            finished = Array.TrueForAll(que0, element => element.Status == 4);

        }

            my_t1.Abort();
            my_t2.Abort();
            timer2.Stop();
            Application.Exit();
    }
    }

}
