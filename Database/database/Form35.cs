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


namespace DataBase3
{
    public partial class Form35 : Form
    {
        public static string connectionString = "server = sh-cynosdbmysql-grp-09q11w48.sql.tencentcdb.com; " +
                     "port = 26948; user = root ; " +
                     "password = @slh2000; database = SeatTest ;" +
                     "charSet = utf8;";
        public Form35()
        {
            InitializeComponent();

        }
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
        private void Form35_Load(object sender, EventArgs e)
        {

        }
    }
}
