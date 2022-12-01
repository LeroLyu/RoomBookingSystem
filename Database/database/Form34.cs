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
    public partial class Form34 : Form
    {
        public string Uid;
        public Form34(string admin)
        {
            InitializeComponent();
            this.skinEngine1.SkinFile = "Skins\\MacOS.ssk";
            Uid = admin;
            string sql = "select adminID,adminName,adminSex " +
             "from Admin where adminID = '" +
             Uid + "';";
            DataSet ds = Form35.Query(sql);
            DataTable dt = ds.Tables[0];
            label1.Text = dt.Rows[0][0].ToString();
            label2.Text = dt.Rows[0][1].ToString();
            label3.Text = dt.Rows[0][2].ToString();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            int xWidth = SystemInformation.PrimaryMonitorSize.Width;
            int yHeight = SystemInformation.PrimaryMonitorSize.Height;
            this.Location = new Point(xWidth / 2 - this.Width / 2, yHeight / 2 - this.Height / 2);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string stuid = textBox2.Text;
            string seatid = textBox3.Text;
            //MessageBox.Show(Convert.ToString(dateTimePicker1.Value));
            string sql = "select Student.studentID as '学号', studentName as '姓名', beginTime as '开始时间', endTime as '结束时间', seatID as '座位号' from Student, Record where Student.studentID like '%"
                + stuid
                + "%' and Student.studentID = Record.studentID and seatID like '%"
                + seatid
                + "%' and beginTime >= '" 
                + Convert.ToString(dateTimePicker1.Value)
                + "' and endTime <='" 
                + Convert.ToString(dateTimePicker2.Value)
                + "';";
            DataSet ds = Form35.Query(sql);
            DataTable dt = ds.Tables[0];
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = dt;
        }
    }
}
