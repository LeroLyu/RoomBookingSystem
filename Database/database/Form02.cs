using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form02 : Form
    {
        int xWidth = SystemInformation.PrimaryMonitorSize.Width;
        int yHeight = SystemInformation.PrimaryMonitorSize.Height;
        public Form02()
        {
            InitializeComponent();
            // this.skinEngine1.SkinFile = "Skins\\MacOS.ssk";
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            this.Location = new Point(xWidth / 2 - this.Width / 2, yHeight / 2 - this.Height / 2);
            Form01 f1 = (Form01)this.Owner;
            label2.Text = "座位" + f1.seatID;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form01 f1 = (Form01)this.Owner;
            f1.isConfirm = 1;
            f1.isSeated = 1;
            string sql = "insert into InSeat(seatID,studentID) " +
                         "values('" + f1.seatID + "','" + f1.Uid + "');";
            f1.isConfirm = 0;
            int status = Form03.ExecuteSql(sql);
            if (status >= 0)
            {
                sql = "select roomID from Seat where seatID = '" + f1.seatID + "';";
                DataSet ds = Form03.Query(sql);
                DataTable dt = ds.Tables[0];
                f1.roomID = dt.Rows[0][0].ToString();
                f1.updateSeatStatus();
                f1.label11.Text = f1.comboBox1.Items[int.Parse(f1.roomID) - 1] + " 座位" + f1.seatID;
            }
            this.Hide();
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Dispose();
        }
    }
}
