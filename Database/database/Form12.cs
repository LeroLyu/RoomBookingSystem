using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace database
{
    public partial class Form12 : Form
    {
        public Form12()
        {
            InitializeComponent();
            this.skinEngine1.SkinFile = "Skins\\MacOS.ssk";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            int xWidth = SystemInformation.PrimaryMonitorSize.Width;
            int yHeight = SystemInformation.PrimaryMonitorSize.Height;
            this.Location = new Point(xWidth / 2 - this.Width / 2, yHeight / 2 - this.Height / 2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string adminId = "";
            string name = "";
            string sex = "";
            adminId = textBox1.Text.Trim();
            name = textBox2.Text.Trim();
            sex = comboBox1.Text.Trim();
            if(name == "" || adminId == "" || sex == "")
            {
                MessageBox.Show("新增管理员信息中不能包含空值！");
            }
            else
            {
                string sql = "insert into Admin(adminID,adminName,adminSex) values('"+ adminId + "','" + name + "','" + sex + "')";
                if (Form11.ExecuteSql(sql) > 0)
                {
                    MessageBox.Show("添加管理员信息成功");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("添加管理员信息失败");
                    this.Close();
                }
            }
        }
    }
}
