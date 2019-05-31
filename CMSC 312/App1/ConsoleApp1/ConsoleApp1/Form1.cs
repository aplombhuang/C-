using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ConsoleApp1
{
    public partial class Form1 : Form
    {
        public int n = -1;
        Timer timer = new Timer();

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;

            button2.Enabled = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            n = int.Parse(textBox1.Text);

            MessageBox.Show("You decided to create {0} processes randomly.", textBox1.Text);

            
            timer.Tick += new EventHandler(on_timer_event);
            timer.Interval = 100;
            timer.Enabled = true;


        }
           

        private void on_timer_event(object sender, EventArgs e)
        {
            textBox2.Text += "Program is " + Environment.NewLine;
            textBox2.Text += DateTime.Now.ToString("dd/MM/yy H:mm:ss ttt") + Environment.NewLine;
            textBox2.SelectionStart = textBox1.TextLength;
            textBox2.ScrollToCaret();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer.Stop();

            textBox2.Text += "Program is Terminated manually.";


        }
    }
    
}
