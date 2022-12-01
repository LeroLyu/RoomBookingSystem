using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace database
{

    public partial class Form11 : Form
    {
        public static string connectionString = "server = sh-cynosdbmysql-grp-09q11w48.sql.tencentcdb.com; " +
                             "port = 26948; user = root ; " +
                             "password = @slh2000; database = SeatTest; charset = utf8";
        public Form11()
        {
            InitializeComponent();
            this.skinEngine1.SkinFile = "Skins\\MacOS.ssk";
        }
        public static DataSet Query(String sql)
        {
            MySqlConnection con = new MySqlConnection(connectionString); // 创建数据库连接
            MySqlDataAdapter sda = new MySqlDataAdapter(sql, con); // 创建桥接器，用于对数据库操作
            DataSet ds = new DataSet(); // 创建一个数据集
            try
            {
                con.Open(); // 打开连接
                sda.Fill(ds, "student"); // 往窗体student表中填充数据集ds
                return ds;
            }
            catch (MySqlException e)
            {
                throw new Exception(e.Message); // 抛出异常
            }
            finally
            {
                sda.Dispose(); // 对sda进行处理回收
                con.Close(); // 连接关闭
            }
        }
        //添加、删除和修改数据
        public static int ExecuteSql(string sql)
        {
            MySqlConnection con = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand(sql, con);
            try
            {
                con.Open();//打开连接
                int rows = cmd.ExecuteNonQuery(); //接受sql执行后返回的行数
                return rows;
            }
            catch (MySqlException e)
            {
                throw new Exception(e.Message);//抛出异常
            }
            finally
            {
                cmd.Dispose();//对cmd进行处理回收
                con.Close();//连接关闭
            }
        }
        public static void delete_admin(string adminId)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            int xWidth = SystemInformation.PrimaryMonitorSize.Width;
            int yHeight = SystemInformation.PrimaryMonitorSize.Height;
            this.Location = new Point(xWidth / 2 - this.Width / 2, yHeight / 2 - this.Height / 2);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form13 childform = new Form13();
            childform.Owner = this;
            childform.FormClosed += button1_Click;
            childform.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string faculty = comboBox1.Text;
            string grade = comboBox2.Text;
            string sex = comboBox3.Text;
            string sql = "select  studentID as 学号,studentName as 姓名, studentSex as 性别, studentMajor as 专业, studentGrade as 年级  from Student where studentName like '%" + name + "%' and studentSex like '%" + sex + "%' and studentMajor like '%" + faculty + "%' and studentGrade like '%" + grade + "%';";
            //string sql = "select * from Student where studentName like  '%" + name + "%' and studentSex like '%" + sex + "%' and studentMajor like '%" + faculty + "%' and studentGrade like '%" + grade + "%';";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = Query(sql).Tables[0];
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string name = textBox2.Text;
            string sex = comboBox4.Text;
            string sql = "select adminID as 工号, adminName as 姓名, adminSex as 性别  from Admin where adminName like '%" + name + "%' and adminSex like '%" + sex + "%';";
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.DataSource = Query(sql).Tables[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show("请选中要修改的学生记录！");
                return;
            }
            DialogResult dr = MessageBox.Show("删除后不可撤销操作，确定删除？", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                int a = dataGridView1.CurrentRow.Index;//获取当前选中行
                string id = dataGridView1.Rows[a].Cells[0].Value.ToString().Trim();//获取该行第0列的数据
                string sql = "delete from Student where studentId = '" + id + "'";
                if (ExecuteSql(sql) > 0)
                {
                    MessageBox.Show("删除学生信息成功");
                    dataGridView1.Rows.RemoveAt(a);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count != 1)
            {
                MessageBox.Show("请选中要修改的管理员记录！");
                return;
            }
            DialogResult dr = MessageBox.Show("删除后不可撤销操作，确定删除？", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                int a = dataGridView2.CurrentRow.Index;//获取当前选中行
                string id = dataGridView2.Rows[a].Cells[0].Value.ToString().Trim();//获取该行第0列的数据
                string sql = "delete from Admin  where adminId = '" + id + "'";
                if (ExecuteSql(sql) > 0)
                {
                    MessageBox.Show("删除管理员信息成功");
                    dataGridView2.Rows.RemoveAt(a);
                }


            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form12 childform = new Form12();
            childform.Owner = this;
            childform.FormClosed += button4_Click;
            childform.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show("请选中要修改的学生记录！");
                return;
            }
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                string studentId = dataGridView1.SelectedRows[i].Cells[0].Value.ToString();
                Form15 childform = new Form15(studentId);
                childform.Owner = this;
                childform.FormClosed += button1_Click;
                childform.Show();
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count != 1)
            {
                MessageBox.Show("请选中要修改的管理员记录！");
                return;
            }
            for (int i = 0; i < dataGridView2.SelectedRows.Count; i++)
            {
                string adminId = dataGridView2.SelectedRows[i].Cells[0].Value.ToString();
                Form14 childform = new Form14(adminId);
                childform.Owner = this;
                childform.FormClosed += button4_Click;
                childform.Show();
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form16 childform = new Form16();
            childform.Owner = this;
            childform.FormClosed += button1_Click;
            childform.Show();
        }
    }
}
