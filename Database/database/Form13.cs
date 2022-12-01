using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace database
{
    public partial class Form13 : Form
    {
        public Form13()
        {
            InitializeComponent();
            this.skinEngine1.SkinFile = "Skins\\MacOS.ssk";
        }
        public DataSet load_excel()

        {

            //打开文件

            OpenFileDialog file = new OpenFileDialog();

            //file.Filter = "Excel(*.xlsx)|*.xlsx|Excel(*.xls)|*.xls";
            file.Filter = "Excel文件 |*.xlsx;*.xls";
            file.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            file.Multiselect = false;

            if (file.ShowDialog() == DialogResult.Cancel)

                return null;

            //判断文件后缀

            var path = file.FileName;

            string fileSuffix = System.IO.Path.GetExtension(path);

            if (string.IsNullOrEmpty(fileSuffix))

                return null;

            using (DataSet ds = new DataSet())

            {


                string connString = "";

                if (fileSuffix == ".xls")
                {
                    connString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + path + ";" + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
                }
                else
                {
                    connString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + path + ";" + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"";
                }
                //读取文件

                string sql_select = " SELECT * FROM [Sheet1$]";

                using (OleDbConnection conn = new OleDbConnection(connString))

                using (OleDbDataAdapter cmd = new OleDbDataAdapter(sql_select, conn))
                {
                    conn.Open();
                    cmd.Fill(ds);
                }

                if (ds == null || ds.Tables.Count <= 0) return null;

                return ds;

            }

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            int xWidth = SystemInformation.PrimaryMonitorSize.Width;
            int yHeight = SystemInformation.PrimaryMonitorSize.Height;
            this.Location = new Point(xWidth / 2 - this.Width / 2, yHeight / 2 - this.Height / 2);
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string studentID = "";
            string name = "";
            string sex = "";
            string major = "";
            string grade = "";
            name = textBox1.Text.Trim();
            studentID = textBox2.Text.Trim();
            sex = comboBox1.Text.Trim();
            major = comboBox2.Text.Trim();
            grade = comboBox3.Text.Trim();
            if (name == "" || studentID == "" || sex == "" || major == "" || grade == " ")
            {
                MessageBox.Show("新增学生信息中不能包含空值！");
            }
            else
            {
                string sql = "insert into Student(studentID,studentName,studentSex,studentMajor,studentGrade) values('" + studentID + "','" + name + "','" + sex + "','" + major + "','" + grade + "')";
                if (Form11.ExecuteSql(sql) > 0)
                {
                    MessageBox.Show("添加学生信息成功");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("添加学生信息失败");
                    this.Close();
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataSet dataSet = load_excel();
            if (dataSet != null)
            {
                DataTable dt = dataSet.Tables[0];
                dataGridView1.DataSource = dt;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource != null)
            {

                for (var i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string studentID = "";
                    string name = "";
                    string grade = "";
                    string sex = "";
                    string major = "";
                    dataGridView1.CurrentCell = dataGridView1[0, i];
                    if (dataGridView1.CurrentCell.FormattedValue.ToString() == "")
                    {
                        continue;
                    }

                    name = dataGridView1[0, i].FormattedValue.ToString();
                    major = dataGridView1[3, i].FormattedValue.ToString();

                    grade = dataGridView1[4, i].FormattedValue.ToString();
                    sex = dataGridView1[2, i].FormattedValue.ToString();

                    studentID = dataGridView1[1, i].FormattedValue.ToString();

                    string sql = "insert into Student(studentID,studentName,studentSex,studentMajor,studentGrade) values('" + studentID + "','" + name + "','" + sex + "','" + major + "','" + grade + "')";
                    Form11.ExecuteSql(sql);
                    
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
