using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Microsoft.Live;
namespace MediaFileFormatTry
{
    public partial class Form1 : Form
    {
        string s,SEC="SRIJON";
        public static String Font = "Times New Roman", Size="12";
        Form x;
        Form3 f3 ;
       // public static Form1 FRM1=new Form1(null);
         SaveFileDialog saveFileDialog1 = new SaveFileDialog();
           
        public Form1(Form f)
        {
            InitializeComponent();
            x = f;
            saveFileDialog1.Filter = "*.stet|*.stet";
            openFileDialog1.Filter = "*.stet|*.stet";
            saveFileDialog1.Title = "Save a File";
            richTextBox1.Font = new Font("Times New Roman", 12);
            AllowDragAndDrop();
          
 
            //ContextMenu cm = new ContextMenu();
            //cm.MenuItems.Add("Item 1", new EventHandler(Removepicture_Click));
            //cm.MenuItems.Add("Item 2", new EventHandler(Addpicture_Click));
            //richTextBox1.ContextMenu = cm; 
        }

        private void AllowDragAndDrop()
        {
            //drag and drop
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);
            this.richTextBox1.AllowDrop = true;
            this.richTextBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.richTextBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            
        }
        void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) 
                e.Effect = DragDropEffects.Copy;
        }

        void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files) //Console.WriteLine(file);
            {
                richTextBox1.Text= FileSystemManager.FSM.Dycription(file, SEC);
            }
        }
        //public static void BoldSelectedText(RichTextBox control)
        //{
        //    control.SelectionFont = new Font(control.Font.FontFamily, control.Font.Size, FontStyle.Bold);
        //    //control.SelectionFont = new Font(control.Font, FontStyle.Bold);
        //    //control.SelectionStart = control.SelectionStart + control.SelectionLength;
        //    //control.SelectionLength = 0;
        //    //control.SelectionFont = control.Font;
        //}
        private void button1_Click(object sender, EventArgs e)
        {
           // openFileDialog1.ShowDialog();
            //Encription();
           
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Dycription();
        }

        private void Dycription()
        {
            //Decrypting          
            openFileDialog1.ShowDialog();
            string res = openFileDialog1.FileName;

            res = res.Substring(res.Length - 4, 4);
            if (res == ".ste" || res == ".STE")
            {
                string contents = File.ReadAllText(@"" + openFileDialog1.FileName);
                try
                {
                    byte[] initial_text_bytes = Convert.FromBase64String(contents);
                    byte[] secret_word_bytes = Encoding.UTF8.GetBytes(SEC);
                    byte[] encrypted_bytes = new byte[initial_text_bytes.Length];

                    int secret_word_index = 0;
                    for (int i = 0; i < initial_text_bytes.Length; i++)
                    {
                        if (secret_word_index == secret_word_bytes.Length)
                        {
                            secret_word_index = 0;
                        }                        
                        encrypted_bytes[i] = (byte)(initial_text_bytes[i] - secret_word_bytes[secret_word_index]);
                        secret_word_index++;
                    }
                  
                    richTextBox1.Text = Encoding.UTF8.GetString(encrypted_bytes);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("File May be Corrupted.");
                }
            }
            else
            {
                MessageBox.Show("Incorrect File Type.");
            }
        }

        private void Encription()
        {
            //Encrypting 
            byte[] initial_text_bytes = Encoding.UTF8.GetBytes(richTextBox1.Text);
            byte[] secret_word_bytes = Encoding.UTF8.GetBytes(SEC);
            byte[] encrypted_bytes = new byte[initial_text_bytes.Length];

            int secret_word_index = 0;
            for (int i = 0; i < initial_text_bytes.Length; i++)
            {
                if (secret_word_index == secret_word_bytes.Length)
                {
                    secret_word_index = 0;
                }
             
                encrypted_bytes[i] = (byte)(initial_text_bytes[i] + secret_word_bytes[secret_word_index]);
                secret_word_index++;
            }
            //File.CreateText();
           
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                //string path = openFileDialog1.FileName;
                string res = saveFileDialog1.FileName;

                res = res.Substring(res.Length - 4, 4);
                if (res == ".ste" || res == ".STE")
                {
                    var myFile = File.Create(@"" + saveFileDialog1.FileName);
                    myFile.Close();
                    File.WriteAllText(@"" + saveFileDialog1.FileName, Convert.ToBase64String(encrypted_bytes));
                    MessageBox.Show("File Saved.");
                }
                else 
                {
                    res = saveFileDialog1.FileName;
                    //res += ".ste";
                    var myFile = File.Create(@"" + res);
                    myFile.Close();
                    File.WriteAllText(@"" + res, Convert.ToBase64String(encrypted_bytes));
                    MessageBox.Show("File Saved.");
                }
            }
          
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                x.Show();
                x.WindowState = FormWindowState.Normal;
            }
            catch (Exception ex) { }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f3 = new Form3(this);
            f3.Owner = this;
            f3.Show();
        }

        private void label1_TextChanged(object sender, EventArgs e)
        {
            Font = label1.Text;
            richTextBox1.Font = new Font(Font, 8);
        }

        private void label2_TextChanged(object sender, EventArgs e)
        {
            Size = label2.Text;
            richTextBox1.Font = new Font(Font, Int32.Parse(Size));
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            x.Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Dycription();
            openFileDialog1.ShowDialog();
            richTextBox1.Text = FileSystemManager.FSM.Dycription(openFileDialog1.FileName,SEC);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Encription();
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != string.Empty || saveFileDialog1.FileName != "")
                FileSystemManager.FSM.Encription(richTextBox1.Text, SEC, saveFileDialog1.FileName);
            else
            { }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //BoldSelectedText(this.richTextBox1);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void richTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {   //click event
                //MessageBox.Show("you got it!");
                ContextMenu contextMenu = new System.Windows.Forms.ContextMenu();
                MenuItem menuItem = new MenuItem("Cut");
                menuItem.Click += new EventHandler(CutAction);
                contextMenu.MenuItems.Add(menuItem);
                menuItem = new MenuItem("Copy");
                menuItem.Click += new EventHandler(CopyAction);
                contextMenu.MenuItems.Add(menuItem);
                menuItem = new MenuItem("Paste");
                menuItem.Click += new EventHandler(PasteAction);
                contextMenu.MenuItems.Add(menuItem);

                richTextBox1.ContextMenu = contextMenu;
            }
        }
        private void CutAction(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void CopyAction(object sender, EventArgs e)
        {
            Graphics objGraphics;
            Clipboard.SetData(DataFormats.Rtf, richTextBox1.SelectedRtf);
            Clipboard.Clear();
        }

        private void PasteAction(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText(TextDataFormat.Rtf))
            {
                richTextBox1.SelectedRtf
                    = Clipboard.GetData(DataFormats.Rtf).ToString();
            }
        }

        private void richTextBox1_MouseUp(object sender, EventArgs e)
        {

        }

        private void richTextBox1_MouseEnter(object sender, EventArgs e)
        {
            //richTextBox1_MouseUp(sender, e);
        } 
    }
}
