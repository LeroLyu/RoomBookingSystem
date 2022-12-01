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
    public partial class Form14 : Form
    {
        string AdminId;
        public Form14(string adminId)
        {
            InitializeComponent();
            this.skinEngine1.SkinFile = "Skins\\MacOS.ssk";
            AdminId = adminId;

            string sql = "select adminID,adminName,adminSex from Admin where adminID = '" + AdminId + "';";
            DataSet ds = Form11.Query(sql);
            DataTable dt = ds.Tables[0];
            textBox1.Text= dt.Rows[0][1].ToString();
            label4.Text = dt.Rows[0][0].ToString();
            comboBox1.Text = dt.Rows[0][2].ToString();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string adminId = "";
            string name = "";
            string sex = "";
            adminId = label4.Text.Trim();
            name = textBox1.Text.Trim();
            sex = comboBox1.Text.Trim();
            string sql = "update  Admin set adminName = '" + name + "' ,adminSex = '" + sex + "' where adminID = '" + adminId + "' ";
            if (adminId == "" || name == "" || sex == "")
            {
                MessageBox.Show("管理员信息中不能包含空值！");
            }
            else
            {
                DialogResult dr = MessageBox.Show("修改后不可撤销操作，确定修改？", "提示", MessageBoxButtons.OKCancel);
                if (Form11.ExecuteSql(sql) > 0)
                {
                    MessageBox.Show("修改管理员信息成功");
                }
            }
        }

        private void Form14_Load(object sender, EventArgs e)
        {
            int xWidth = SystemInformation.PrimaryMonitorSize.Width;
            int yHeight = SystemInformation.PrimaryMonitorSize.Height;
            this.Location = new Point(xWidth / 2 - this.Width / 2, yHeight / 2 - this.Height / 2);
        }
    }
}
