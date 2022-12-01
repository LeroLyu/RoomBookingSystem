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
    public partial class Form15 : Form
    {
        string StudentId;
        public Form15(string studentId)
        {
            InitializeComponent();
            this.skinEngine1.SkinFile = "Skins\\MacOS.ssk";
            StudentId = studentId;
            string sql = "select studentID,studentName,studentSex,studentMajor,studentGrade from Student where studentID = '" + StudentId + "';";
            DataSet ds = Form11.Query(sql);
            DataTable dt = ds.Tables[0];
            textBox1.Text = dt.Rows[0][1].ToString();
            label6.Text = dt.Rows[0][0].ToString();
            comboBox1.Text = dt.Rows[0][2].ToString();
            comboBox2.Text = dt.Rows[0][3].ToString();
            comboBox3.Text = dt.Rows[0][4].ToString();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string studentId = "";
            string name = "";
            string sex = "";
            string major = "";
            string grade = "";
            studentId = label6.Text.Trim();
            name = textBox1.Text.Trim();
            sex = comboBox1.Text.Trim();
            major = comboBox2.Text.Trim();
            grade = comboBox3.Text.Trim();
            string sql = "update  Student set studentName = '" + name + "' ,studentSex = '" + sex + "',studentMajor = '" + major + "',studentGrade = '" + grade + "' where studentID = '" + studentId + "' ";
            if (name == "" || sex == ""|| major == ""|| grade == "")
            {
                MessageBox.Show("学生信息中不能包含空值！");
            }
            else
            {
                DialogResult dr = MessageBox.Show("修改后不可撤销操作，确定修改？", "提示", MessageBoxButtons.OKCancel);
                if (Form11.ExecuteSql(sql) > 0)
                {
                    MessageBox.Show("修改学生信息成功");
                }
            }
        }

        private void Form15_Load(object sender, EventArgs e)
        {
            int xWidth = SystemInformation.PrimaryMonitorSize.Width;
            int yHeight = SystemInformation.PrimaryMonitorSize.Height;
            this.Location = new Point(xWidth / 2 - this.Width / 2, yHeight / 2 - this.Height / 2);
        }
    }
}
