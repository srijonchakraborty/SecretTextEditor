using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaFileFormatTry
{
    public partial class Form2 : Form
    {
        String uName = "srijonchak",pass="srijon1234";

        public Form2()
        {
            InitializeComponent();
            //Icon sizedIcon = new Icon("favicon.ico", new Size(128, 128));
            //this.Icon = sizedIcon;
        }
        private void submit()
        {
            if (uName == textBox1.Text && pass == textBox2.Text)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                this.Hide();
                Form1 f = new Form1(this);
                f.Show();
            }
            else
                MessageBox.Show("!!!Wrong Uname Or Pass!!!");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            submit();
        }

        private void Form2_Enter(object sender, EventArgs e)
        {
            submit();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
