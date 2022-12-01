using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataBase3;

namespace WinFormsApp1
{
    public partial class Form33 : Form
    {
        public string Uid;
        public Form33(string adminid)
        {
            InitializeComponent();
            this.skinEngine1.SkinFile = "Skins\\MacOS.ssk";
            Uid = adminid;
            string sql = "select adminID,adminName,adminSex " +
             "from Admin where adminID = '" +
             Uid + "';";
            DataSet ds = Form35.Query(sql);
            DataTable dt = ds.Tables[0];
            label1.Text = dt.Rows[0][0].ToString();
            label2.Text = dt.Rows[0][1].ToString();
            label3.Text = dt.Rows[0][2].ToString();

        }

        private void Form6_Load(object sender, EventArgs e)
        {
            int xWidth = SystemInformation.PrimaryMonitorSize.Width;
            int yHeight = SystemInformation.PrimaryMonitorSize.Height;
            this.Location = new Point(xWidth / 2 - this.Width / 2, yHeight / 2 - this.Height / 2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string stuid = textBox2.Text;
            string stuname = textBox1.Text;
            string sql = "select studentID as '学号', studentName as '姓名', studentGrade as '年级', studentMajor '专业' from V1 where studentID like '%" 
                + stuid 
                + "%' and studentName like '%"
                + stuname
                + "%';";
            DataSet ds = Form35.Query(sql);
            DataTable dt = ds.Tables[0];
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                string studentID = dataGridView1.SelectedRows[i].Cells[0].Value.ToString();
                string sql = "update Student set studentCredit = 100 where studentID = '" + studentID + "';";
                int status = Form35.ExecuteSql(sql);

                if (status >= 0) MessageBox.Show("移出黑名单成功！");
                else MessageBox.Show("失败！");

                button1_Click(button1,e);
            }
        }
    }
}
