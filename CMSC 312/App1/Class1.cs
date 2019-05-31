using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Class1
    { private int n, x, y;

        private string str, line, phs;

        public Class1()
        {
            Console.WriteLine("this is a first object");
        }

        public void setStr(String str)
        {
            this.str = str;
        }

        public void setNum(int n)
        {
            this.n = n;
        }

        public string getStr()
        {
            return str;
        }

        public int getNum()
        {
            return n;
        }

        public int priority { set{ n = value; }  get{ return n; } }

        // Class3 pcb1 = new Class3();

        //Class2 p1 = new Class2(); // putting process info to class

        // Class3 b1 = new Class3(); // pcb of p1

        //p1.processType = "i/o";

        //p1.priority = 0;

        // p1.burstTime = 10;

        //p1.waitTime = 0;

        // p1.CodeLine = 1024;

        //  if (p1.processType.Equals("i/o")) { b1.checkIO = true; que1[0] = b1; }

        // b1.priority = p1.priority;

        // b1.totalBT = p1.burstTime;

        // b1.totalLine = p1.CodeLine;

        //que1[1] = null;

        //que1[1] = b1;

        //que1[2] = b1;

        //que1[3] = b1;

        //que1[4] = b1;

        // Class2 p2 = new Class2();

        // p2.processType = "calculate";

        // p2.priority = 1;

        // p2.burstTime = 50;

        //  p2.CodeLine = 3096;

        // Class3 b2 = new Class3();

        //if (p2.processType.Equals("i/o")) { b2.checkIO = true; }

        // b2.priority = p2.priority;

        // b2.totalBT = p2.burstTime;

        // b2.totalLine = p2.CodeLine;

        // Class2 p3 = new Class2();

        // Class3 b3 = new Class3();

        // p3.processType = "File Copy";

        // p3.priority = 6;

        // p3.burstTime = 5000;

        // p3.CodeLine = 30960;

        // if (p3.processType.Equals("i/o")) { b3.checkIO = true; }

        // b3.priority = p3.priority;

        //b3.totalBT = p3.burstTime;

        //b3.totalLine = p3.CodeLine;

        //que1[0] = p1;

        // que1[0].waitTime = 15;

        //Console.WriteLine(p1.waitTime);

        // sortToQ(p1);

        //if (p1.processType.Equals("i/o")) { que1[0] = b1; }

        // que1[0].priority = 5;

        // Console.WriteLine(p1.priority);

        //  sortToQ(p1);

        // if (p1.burstTime <= 50) { que3[0] = b1; }

        // else if (p1.burstTime <= 150) { que2[0]= b1; }

        // if (p2.burstTime <= 50) { que3[0] = b2; }

        // else if (p2.burstTime <= 150) { que2[0] = b2; }

        // Console.WriteLine(que3[0].totalBT);

        // if (p3.burstTime <= 50) { que3[0] = b3; }

        //else if (p3.burstTime > 150) { que2[0] = b3; }

        // Console.WriteLine(que2[0].totalBT);

        //Console.WriteLine(Pow(6));

        //Console.WriteLine(Pow(3, 4));

        //int result = Area(w: 5, h: 4);

        //Console.WriteLine("Area of 5*4 is {0}", result);

        //string str1, str2; int x = 4;

        //Console.WriteLine(Pow(x));// default to pass by value

        //sq(ref x); // pass by reference

        //Console.WriteLine(x);

        //passVal(out str1, out str2); // pass by output

        //Console.WriteLine("{0} {1} !", str1, str2);

        //Class1 c1 = new Class1();

        //c1.setStr("APTX");

        //c1.setNum(4869);

        //Console.WriteLine("{0}{1}",c1.getStr(), c1.getNum());

        //c1.priority = 10;

        //Console.WriteLine(c1.priority);

        static int Pow(int x, int y = 2)
        {
            int result = 0;

            result = x + y;

            return result;
        }

        static int Area(int h, int w)
        {
            return h * w;
        }

        static void sq(ref int x)// the a reference 
        {
            x = x * x;
        }

        static void passVal(out string x, out string y) //pass by output
        {
            x = Console.ReadLine();

            y = Console.ReadLine();

        }



        static void sortToQ(object o)
        {
            string type = "";

            type = o.ToString();

            Console.WriteLine(type);

        }


    }
}
