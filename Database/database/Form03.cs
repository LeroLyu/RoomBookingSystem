using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using database;
using MySql.Data.MySqlClient;

namespace WinFormsApp1
{
    public partial class Form03 : Form
    {
        int xWidth = SystemInformation.PrimaryMonitorSize.Width;
        int yHeight = SystemInformation.PrimaryMonitorSize.Height;
        public static string connectionString = "server = sh-cynosdbmysql-grp-09q11w48.sql.tencentcdb.com; " +
                             "port = 26948; user = root ; " +
                             "password = @slh2000; database = SeatTest ;" +
                             "charSet = utf8;" +
                             "Allow User Variables = True;";
        public static DataSet Query(String sql)
        {
            MySqlConnection con = new MySqlConnection(connectionString); // 创建数据库连接
            MySqlDataAdapter sda = new MySqlDataAdapter(sql, con); // 创建桥接器，用于对数据库操作
            DataSet ds = new DataSet(); // 创建一个数据集
            try
            {
                con.Open(); // 打开连接
                sda.Fill(ds); // 数据集ds
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

        public Form03()
        {
            InitializeComponent();
            // this.skinEngine1.SkinFile = "Skins\\MacOS.ssk";
        }


        private void Form3_Load(object sender, EventArgs e)
        {
            this.Location = new Point(xWidth / 2 - this.Width / 2, yHeight / 2 - this.Height / 2);
            textBox2.PasswordChar = '*';
            button1.Text = "登录";
        }

        private bool CheckID(string uid, string pwd, string sql)
        {
            DataSet ds = Query(sql);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("此用户不存在！");
                return false;
            }
            else
            {
                if (pwd != dt.Rows[0][0].ToString())
                {
                    MessageBox.Show("密码错误！");
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string uid = textBox1.Text.Trim();
            string pwd = textBox2.Text.Trim();
            int len = uid.Length;
            if (uid.Equals("000"))
            {
                string sql = "select password from Admin where adminID = 100;";
                if (CheckID(uid, pwd, sql))
                {
                    this.Hide();
                    Form11 f11 = new Form11();
                    f11.ShowDialog();
                    this.Close();
                }
            }
            if (len == 3)
            {
                string sql = "select password from Admin where adminID = '" + uid + "';";
                if (CheckID(uid, pwd, sql))
                {
                    this.Hide();
                    Form31 f31 = new Form31(uid);
                    f31.ShowDialog();
                    this.Close();
                }
            }
            else if (len == 4)
            {
                string sql = "select password from Student where studentID = '" + uid + "';";
                if(CheckID(uid, pwd, sql))
                {
                    this.Hide();
                    Form01 f1 = new Form01(uid);
                    f1.ShowDialog();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("账号输入错误！");
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
