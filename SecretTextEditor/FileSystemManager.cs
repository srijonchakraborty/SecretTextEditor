using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaFileFormatTry
{
    public class FileSystemManager
    {
        public static FileSystemManager FSM = new FileSystemManager();

        //dycription for simple *.txt file
        public String SimpleTextFileReader(String FilePath)
        {
            return File.ReadAllText(@"" + FilePath);
        }
        public void SimpleTextFileSaving(String Content, String FilePath)
        {
            //file Saving
            if (FilePath != "")
            {
                string res = FilePath;
                res = res.Substring(res.Length - 4, 4);
                if (res == ".txt" || res == ".TXT")
                {
                    var myFile = File.Create(@"" + FilePath);
                    myFile.Close();
                    File.WriteAllText(@"" + FilePath, Content);
                    MessageBox.Show("File Saved.");
                }
                else
                {
                    res = FilePath;
                    var myFile = File.Create(@"" + res);
                    myFile.Close();
                    File.WriteAllText(@"" + res, Content);
                    MessageBox.Show("File Saved.");
                }
            }
        }
        public String Dycription(String FilePath, String SEC)
        {
            //Decrypting          
           
            string res = FilePath;

            res = res.Substring(res.Length - 5, 5);
            if (res == ".stet" || res == ".STET")
            {
                string contents = File.ReadAllText(@"" + FilePath);
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

                    return Encoding.UTF8.GetString(encrypted_bytes);
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
            return "";
        }
        
        public void Encription(String Content, String SEC, String FilePath)
        {
            //Encrypting 
            byte[] initial_text_bytes = Encoding.UTF8.GetBytes(Content);
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

            //file Saving
            if (FilePath != "")
            {
                string res = FilePath;
                res = res.Substring(res.Length - 5, 5);
                if (res == ".stet" || res == ".STET")
                {
                    var myFile = File.Create(@"" + FilePath);
                    myFile.Close();
                    File.WriteAllText(@"" + FilePath, Convert.ToBase64String(encrypted_bytes));
                    MessageBox.Show("File Saved.");
                }
                else
                {
                    res = FilePath;
                    var myFile = File.Create(@"" + res);
                    myFile.Close();
                    File.WriteAllText(@"" + res, Convert.ToBase64String(encrypted_bytes));
                    MessageBox.Show("File Saved.");
                }
            }
        }
    }
}
