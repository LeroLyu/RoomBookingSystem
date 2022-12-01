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
    public partial class Form16 : Form
    {
        public Form16()
        {
            InitializeComponent();
            this.skinEngine1.SkinFile = "Skins\\MacOS.ssk";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string major = "";
            string grade = "";
            major = comboBox1.Text.Trim();
            grade = comboBox2.Text.Trim();
            if(major == "" && grade =="")
            {
                MessageBox.Show("不允许全部为空！");
            }
            else
            {
                string sql = "delete from Student where studentMajor like '%" + major + "%' and studentGrade like '%" + grade + "%' ";
                DialogResult dr = MessageBox.Show("删除后不可撤销操作，确定删除？", "提示", MessageBoxButtons.OKCancel);
                if (Form11.ExecuteSql(sql) > 0)
                {
                    MessageBox.Show("删除学生信息成功");
                }
            }
        }

        private void Form16_Load(object sender, EventArgs e)
        {
            int xWidth = SystemInformation.PrimaryMonitorSize.Width;
            int yHeight = SystemInformation.PrimaryMonitorSize.Height;
            this.Location = new Point(xWidth / 2 - this.Width / 2, yHeight / 2 - this.Height / 2);
        }
    }
}
