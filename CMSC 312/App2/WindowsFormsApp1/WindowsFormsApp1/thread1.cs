using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class thread1
    {
        public void thread_1()
        {
            MessageBox.Show("this is tread 1 running");
        }

        public void thread1_toProcess(String str, int mem)
        {
            double ramP = (mem / 8196.0) * 100;
            String str1 = "ProcessID " + str + "is now terminated, updated available Ram is " + ramP +"%." ;

            MessageBox.Show(str1);
        }
    }
}
