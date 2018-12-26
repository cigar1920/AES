using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AES
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s="";

            if (comboBox1.SelectedIndex == 0)
                s=textBox1.Text+"加密后"+Encryption.Encrypt(textBox1.Text.Trim().ToString(), textBox2.Text.Trim().ToString(),"1");
            if (comboBox1.SelectedIndex == 1)
            {
                try
                {
                    s = textBox1.Text+"解密后"+Encryption.Decrypt(textBox1.Text.Trim().ToString(), textBox2.Text.Trim().ToString(), "1");
                }
                catch (Exception ce)
                {
                    MessageBox.Show("密码错误");
                }
            }
                    textBox3.Text +="\n"+ s+"\n";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("麻麻没有告诉你不要瞎碰嘛！");
        }
    }
}
