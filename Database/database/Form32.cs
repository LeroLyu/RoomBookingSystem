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
    public partial class Form32 : Form
    {
        public string Uid;
        public Form32(string uid)
        {
            InitializeComponent();
            this.skinEngine1.SkinFile = "Skins\\MacOS.ssk";
            Uid = uid;
            string sql = "select adminID,adminName,adminSex " +
             "from Admin where adminID = '" +
             uid + "';";
            DataSet ds = Form35.Query(sql);
            DataTable dt = ds.Tables[0];
            label1.Text = dt.Rows[0][0].ToString();
            label2.Text = dt.Rows[0][1].ToString();
            label3.Text = dt.Rows[0][2].ToString();
            sql = "select roomID, roomName from Room where adminID = '" + uid + "';";
            ds = Form35.Query(sql);
            dt = ds.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox1.Items.Add(dt.Rows[i][1].ToString());
            }
        }



        private void Form5_Load(object sender, EventArgs e)
        {
            int xWidth = SystemInformation.PrimaryMonitorSize.Width;
            int yHeight = SystemInformation.PrimaryMonitorSize.Height;
            this.Location = new Point(xWidth / 2 - this.Width / 2, yHeight / 2 - this.Height / 2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = Form35.Query("select Student.studentID as '学号', studentName as '姓名', studentGrade as '年级', studentMajor '专业', Seat.seatID '座位号' from Room, Seat, InSeat, Student where Room.roomID = Seat.roomID " +
                                                   "and InSeat.SeatID = Seat.seatID and Student.studentId = InSeat.studentID and roomName = '" + 
                                                   Convert.ToString(comboBox1.SelectedItem) + "';").Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                string studentID = dataGridView1.SelectedRows[i].Cells[0].Value.ToString();
                string sql = "delete from InSeat where studentID = '" + studentID + "';" + 
                    "update Student set studentCredit = studentCredit - 20 where studentID = '"  + studentID + "';";
                int status = Form35.ExecuteSql(sql);

                if (status >= 0) MessageBox.Show("释放成功！");
                else MessageBox.Show("释放失败！");

                button1_Click(button1, e);
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
