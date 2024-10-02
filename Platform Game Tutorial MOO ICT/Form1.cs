using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platform_Game_Tutorial_MOO_ICT
{
    public partial class Form1 : Form
    {
        public static string firstName = "";
        public static string lastName = "";
        public static string email = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            firstName = textBox1.Text;
            lastName = textBox2.Text;
            email = textBox3.Text;

            Form2 frm2 = new Form2();
            frm2.Show();
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
