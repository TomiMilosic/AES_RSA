using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tripledesanddes
{
    public partial class Form1 : Form
    {

        public string ImeDatoteke { get; set; }
        public Form1()
        {
            InitializeComponent();

            keysitetxb.Text = "128";//192, 256 
            keysizersatbx.Text = "1024";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "All Files|*";
            openFile.FileName = "";
            if (openFile.ShowDialog()== DialogResult.OK)
            {
                textBox1.Text = openFile.FileName;
                ImeDatoteke = Path.GetFileName(openFile.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                RSA rsa = new RSA(Convert.ToInt32(keysizersatbx.Text));
                rsa.EncryptFile(textBox1.Text, Convert.ToInt32(keysizersatbx.Text));
                GC.Collect();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                RSA rsa = new RSA(Convert.ToInt32(keysizersatbx.Text));
                rsa.DecyrptFile(textBox1.Text, Convert.ToInt32(keysizersatbx.Text));
                GC.Collect();
            }
            catch (Exception ex)
            {

                if (ex.Message == "Bad Data.\r\n")
                {
                    MessageBox.Show("Napačen ključ");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }






       




        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "All Files|*";
            openFile.FileName = "";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = openFile.FileName;
                ImeDatoteke = Path.GetFileName(openFile.FileName);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                AES des = new AES(Convert.ToInt32(keysitetxb.Text));
                des.EncryptFile(textBox3.Text);
                GC.Collect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                AES des = new AES(Convert.ToInt32(keysitetxb.Text));
                des.DecryptFile(textBox3.Text);
                GC.Collect();
            }
            catch (Exception ex)
            {

                if (ex.Message == "Bad Data.\r\n")
                {
                    MessageBox.Show("Napačen ključ");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        

       
    }
}
