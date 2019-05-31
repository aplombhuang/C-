using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class thread2
    {
        public void thread_2()
        {
            MessageBox.Show("this is tread 2 running");
        }

        public void thread2_toProcess(String str, int mem)
        {
            double ramP = (mem / 8196.0) * 100;
            String str1 = "ProcessID " + str + "is now terminated, updated available Ram is " + ramP + "%.";

            MessageBox.Show(str1);
        }
    }
}
