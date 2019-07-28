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
    
    public partial class Form3 : Form
    {
        Form F;
        String Font="", Size="";
        public Form3(Form _F)
        {
            InitializeComponent();
            
            foreach (FontFamily font in System.Drawing.FontFamily.Families)
            {
                listBox2.Items.Add(font.Name);
            }
            F = new Form1(null);
            F = _F;
            
            F = (Form1)this.Owner;
            listBox1.SelectedIndex = 0;
            listBox2.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Font = listBox2.SelectedItem.ToString();
            Size = listBox1.SelectedItem.ToString();

            Control[] c1 = Owner.Controls.Find("label1", true);
            Control[] c2 = Owner.Controls.Find("label2", true);
            Label l1 = (Label)c1[0];
            Label l2 = (Label)c2[0];
            l1.Text = Font;
            l2.Text = Size;
            this.Close();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
