using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_App_1
{
    public partial class Form1 : Form
    {
        //string year, semester, fileName; 

        public Form1()
        {
            InitializeComponent();

            string[] years = { "2011", "2012", "2013", "2014", "2015", "2016", "2017", "2018", "2019", "2020",
                               "2021", "2022", "2023", "2024", "2025", "2026", "2027", "2028", "2029", "2030",
                               "2031", "2032", "2033", "2034", "2035", "2036", "2037", "2038", "2039", "2040",
            };

            comboBox1.Items.AddRange(years);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
                  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.year = comboBox1.SelectedItem.ToString();

            Program.semester = comboBox2.SelectedItem.ToString();

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Program.fileName = openFileDialog1.FileName;
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
