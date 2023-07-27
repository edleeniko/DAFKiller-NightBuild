using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAFKiller.Cryptography;

namespace DevTools
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
            
        }

       

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text != string.Empty)
            textBox2.Text = TripleDESEncryptDecrypt.Encrypt(textBox1.Text, "dafkiller");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if(textBox2.Text != string.Empty)
            textBox3.Text = TripleDESEncryptDecrypt.Decrypt(textBox2.Text, "dafkiller");
        }
    }
}
