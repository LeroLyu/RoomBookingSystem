using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form04 : Form
    {
        public string rpwd;
        public string rrwd;
        int xWidth = SystemInformation.PrimaryMonitorSize.Width;
        int yHeight = SystemInformation.PrimaryMonitorSize.Height;
        public Form04()
        {
            InitializeComponent();
            // this.skinEngine1.SkinFile = "Skins\\MacOS.ssk";
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.Location = new Point(xWidth / 2 - this.Width / 2, yHeight / 2 - this.Height / 2);
            textBox1.PasswordChar = '*';
            textBox2.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form01 f1 = (Form01)this.Owner;
            rpwd = textBox1.Text.Trim();
            rrwd = textBox2.Text.Trim();
            if (!rpwd.Equals(rrwd))
            {
                MessageBox.Show("两次输入的密码不一致！");
            }
            else
            {
                if (rpwd.Length < 6)
                {
                    MessageBox.Show("密码位数不能小于6");
                }
                else
                {
                    string sql = "update Student set password = '" + rpwd + "' " +
                                 "where studentID = '" + f1.Uid + "';";
                    int status = Form03.ExecuteSql(sql);
                    if (status >= 0)
                    {
                        this.Hide();
                        this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("设置失败！");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Dispose();
        }
    }
}
