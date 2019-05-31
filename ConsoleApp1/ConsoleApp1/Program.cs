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
            // int x = 10; double y = 20.0;

            // Console.WriteLine("x = {0}, y = {1}", x, y);

            //  string name = "";

            //name = Console.ReadLine();

            //  Console.WriteLine(name);

            //string number = "";

            // number = Console.ReadLine();

            // int numb = Convert.ToInt32(number);

            // Console.WriteLine("square would be {0}", numb * numb);

            //double num1 = 15;

            //var num2 = 15;

            //Console.WriteLine( "{0}, {1}",num1/2, num2/2);

            //const double PI = 3.14; // constant

            //  for (int i = 0; i < 10; i++)
            //  {
            //      if (i == 5) { continue; }
            //      Console.WriteLine(i);
            //  }


            //string str = Console.ReadLine(); string msg = "";

            // int age = Convert.ToInt16(str);
            // msg = (age >= 18) ? "Welcome" : "Sorry";
            // Console.WriteLine(msg);

            // sayHi();

            //Console.WriteLine(calc(5));

            // Console.WriteLine(calc(5, 5));

            //Person p1 = new Person();

            // p.Name = "ATH";

            //  p.age = 28;

            // p.height = 178.9;

            //Console.WriteLine(p.getResult);

            // p.Adds = "Short Pump";

            //Console.WriteLine(p.Adds);

            //p.setDoub(2);

            //p.setNum(1);

            //p.setStr(".");

            //p.updater();

            //p.printResult();

            //Person[] ObjArr = new Person[5];

            //ObjArr[0] = new Person();

            //ObjArr[0].age = 2;

            //ObjArr[0].Name = "baby";

            //Console.WriteLine("{0} is {1} years old", ObjArr[0].Name, ObjArr[0].age);

            Thread th = Thread.CurrentThread;// they are all child thread
        
            th.Name = "MainThread";

            Console.WriteLine("This is {0}", th.Name);
            Console.ReadKey();

        }

        static void sayHi()
        { Console.WriteLine("method 1"); }

        static int calc(int x, int y = 2)
        {
            int result = x + y;

            return result;
        }
    }
}
