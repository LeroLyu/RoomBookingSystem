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

namespace WinFormsApp1
{
    public partial class Form01 : Form
    {
        public string Uid;
        private string Sstate;
        private string Scredit;
        public int isSeated = 0;
        public int isConfirm = 0;
        public string seatID;
        public string roomID;

        public Form01(string uid)
        {
            Uid = uid;
            InitializeComponent();
            this.skinEngine1.SkinFile = "Skins\\mp10";
            comboBox1.Items.Add("海韵七 124");
            comboBox1.Items.Add("公寓自习室101");
            comboBox1.Items.Add("海韵14");
            comboBox1.Items.Add("图书馆");
            string sql = "select studentID,studentName,studentMajor,studentGrade, studentState, studentCredit " +
                         "from Student where studentID = '" +
                         uid + "';";
            DataSet ds = Form03.Query(sql);
            DataTable dt = ds.Tables[0];
            label2.Text = dt.Rows[0][0].ToString();
            label4.Text = dt.Rows[0][1].ToString();
            label6.Text = dt.Rows[0][2].ToString();
            label8.Text = dt.Rows[0][3].ToString();
            Sstate = dt.Rows[0][4].ToString();
            Scredit = dt.Rows[0][5].ToString();
            if (int.Parse(Sstate) == 1)
            {
                isSeated = 1;
                sql = "select seatID from InSeat where studentID = '" + uid + "';";
                ds = Form03.Query(sql);
                dt = ds.Tables[0];
                seatID = dt.Rows[0][0].ToString();
                sql = "select roomID from Seat where seatID = '" + seatID + "';";
                ds = Form03.Query(sql);
                dt = ds.Tables[0];
                roomID = dt.Rows[0][0].ToString();
                label11.Text = comboBox1.Items[int.Parse(roomID) - 1] + " 座位" + seatID;               
            }
        }

        public void UnVisButton()
        {
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            button7.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
            button10.Visible = false;
            button11.Visible = false;
            button12.Visible = false;
            button13.Visible = false;
            button14.Visible = false;
            button15.Visible = false;
            button16.Visible = false;
        }
        public void EnableSelectButton()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
            button10.Enabled = false;
            button11.Enabled = false;
            button12.Enabled = false;
            button13.Enabled = false;
            button14.Enabled = false;
            button15.Enabled = false;
            button16.Enabled = false;
        }
        public void SetButtonTextTag1(int i)
        {
            if (i < 2)
            {
                button1.Text = (i * 5 + 1).ToString();
                button2.Text = (i * 5 + 2).ToString();
                button3.Text = (i * 5 + 3).ToString();
                button4.Text = (i * 5 + 4).ToString();
                button5.Text = (i * 5 + 5).ToString();
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                button7.Visible = false;
                button8.Visible = false;
                button9.Visible = false;
                button10.Visible = false;
                button11.Visible = false;
                button12.Visible = false;
                button13.Visible = false;
                button14.Visible = false;
                button15.Visible = false;
                button16.Visible = false;
            }
            else if(i == 2)
            {
                button1.Text = 11.ToString();
                button2.Text = 12.ToString();
                button3.Text = 13.ToString();
                button4.Text = 14.ToString();
                button5.Text = 15.ToString();
                button7.Text = 16.ToString();
                button8.Text = 17.ToString();
                button9.Text = 18.ToString();
                button10.Text = 19.ToString();
                button11.Text = 20.ToString();
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                button7.Visible = true;
                button8.Visible = true;
                button9.Visible = true;
                button10.Visible = true;
                button11.Visible = true;
                button12.Visible = false;
                button13.Visible = false;
                button14.Visible = false;
                button15.Visible = false;
                button16.Visible = false;
            }
            else if(i == 3)
            {
                button1.Text = 21.ToString();
                button2.Text = 22.ToString();
                button3.Text = 23.ToString();
                button4.Text = 24.ToString();
                button5.Text = 25.ToString();
                button7.Text = 26.ToString();
                button8.Text = 27.ToString();
                button9.Text = 28.ToString();
                button10.Text = 29.ToString();
                button11.Text = 30.ToString();
                button12.Text = 31.ToString();
                button13.Text = 32.ToString();
                button14.Text = 33.ToString();
                button15.Text = 34.ToString();
                button16.Text = 35.ToString();
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                button7.Visible = true;
                button8.Visible = true;
                button9.Visible = true;
                button10.Visible = true;
                button11.Visible = true;
                button12.Visible = true;
                button13.Visible = true;
                button14.Visible = true;
                button15.Visible = true;
                button16.Visible = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int xWidth = SystemInformation.PrimaryMonitorSize.Width;
            int yHeight = SystemInformation.PrimaryMonitorSize.Height;
            this.Location = new Point(xWidth / 2 - this.Width / 2, yHeight / 2 - this.Height / 2);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = Form03.Query("set @num = 0;select @num := @num + 1 as '序号', beginTime as '开始时间', endTime as '结束时间', Room.roomName as '自习室',Record.seatID as '座位号' " +
                                           "from Record, Room, Seat where Record.seatID = Seat.seatID and " +
                                           "Seat.roomID = Room.roomID and studentID = '" +
                                           Uid + "';").Tables[0];
            UnVisButton();
            EnableSelectButton();

            label13.Text = Scredit;
            if (int.Parse(Scredit) < 80)
            {
                label13.Text = Scredit + "，信誉积分不足!";
                comboBox1.Enabled = false;
            }
        }

        public void updateSeatStatus()
        {
            int ind = comboBox1.SelectedIndex + 1;
            string sql = "select seatID, seatState from Seat " +
                         "where roomID = '" + ind.ToString() + "';";
            DataSet ds = Form03.Query(sql);
            DataTable dt = ds.Tables[0];
            int rowsCount = int.Parse(Form03.Query("select count(*) from Seat where roomID = '" + ind.ToString() + "' group by roomID").Tables[0].Rows[0][0].ToString());
            for (int i = 0; i < rowsCount; i++)
            {
                string seatID = dt.Rows[i][0].ToString();
                string seatState = dt.Rows[i][1].ToString();
                foreach (Control c in tabControl1.TabPages)
                {
                    if (c is TabPage)
                    {
                        foreach (Control b in c.Controls)
                        {
                            if (b.Text.Equals(seatID) && seatState.Equals("1"))
                            {
                                b.Enabled = false;
                                b.BackColor = Color.Transparent;
                            }
                            else if (b.Text.Equals(seatID) && seatState.Equals("0"))
                            {
                                b.Enabled = true;
                                b.BackColor = Color.Blue;
                            }
                        }
                    }
                }
            }
            if (isSeated == 1)
            {
                EnableSelectButton();
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetButtonTextTag1(comboBox1.SelectedIndex);
            updateSeatStatus(); 
        }
        private void SelectSeat(Button b1)
        {
            seatID = b1.Text;
            if (isSeated == 0)
            {
                Form02 f2 = new Form02();
                f2.Owner = this;
                f2.Show();
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        // button1-5 选座
        private void button1_Click(object sender, EventArgs e)
        {
            SelectSeat(button1);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SelectSeat(button2);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            SelectSeat(button3);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            SelectSeat(button4);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            SelectSeat(button5);
        }

        
        // button6 释放座位
        private void button6_Click(object sender, EventArgs e)
        {
            if(isSeated == 1)
            {
                string sql = "delete from InSeat where seatID = '" + seatID + "';";
                int status = Form03.ExecuteSql(sql);
                if (status >= 0)
                {
                    isSeated = 0;
                    updateSeatStatus();
                    dataGridView1.DataSource = Form03.Query("set @num = 0;select @num := @num + 1 as '序号', beginTime as '开始时间', endTime as '结束时间', Room.roomName as '自习室',Record.seatID as '座位号' " +
                                                   "from Record, Room, Seat where Record.seatID = Seat.seatID and " +
                                                   "Seat.roomID = Room.roomID and studentID = '" +
                                                   Uid + "';").Tables[0];
                }
                label11.Text = "无";
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            
        }

        // button7-16 选座
        private void button7_Click(object sender, EventArgs e)
        {
            SelectSeat(button7);
        }
        private void button8_Click(object sender, EventArgs e)
        {
            SelectSeat(button8);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SelectSeat(button9);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            SelectSeat(button10);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            SelectSeat(button11);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            SelectSeat(button12);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            SelectSeat(button13);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            SelectSeat(button14);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            SelectSeat(button15);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            SelectSeat(button16);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Form04 f4 = new Form04();
            f4.Owner = this;
            f4.Show();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
