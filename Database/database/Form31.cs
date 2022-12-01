using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using Ubiety.Dns.Core;
using System.Drawing.Drawing2D;
using DataBase3;

namespace WinFormsApp1
{
    public partial class Form31 : Form
    {
        public string Uid;
        public int seatSum;
        public int seatSumNow;
        public Form31(string adminid)
        {
            string uid = adminid;
            Uid = adminid;
            InitializeComponent();
            this.skinEngine1.SkinFile = "Skins\\MacOS.ssk";
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
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox1.Items.Add(dt.Rows[i][1].ToString());
            }
            sql = "select studentName, totalTime " +
             "from Student order by totalTime desc;";
            ds = Form35.Query(sql);
            dt = ds.Tables[0];
            label9.Text = dt.Rows[0][0].ToString();
            label10.Text = dt.Rows[1][0].ToString();
            label11.Text = dt.Rows[2][0].ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string roomName = comboBox1.Text;
            if (roomName == "") MessageBox.Show("请选择自习室");
            else
            {
                string sql = "select seatSum from TypeRoom t, Room r where r.roomName like '"
                + roomName + "' and r.roomType = t.roomType";
                DataSet ds = Form35.Query(sql);
                DataTable dt = ds.Tables[0];
                //找到对应房间类型的座位数
                seatSum = Convert.ToInt32(dt.Rows[0][0].ToString());
                sql = "select count(*) from InSeat, Seat, Room where roomName = '"
                + roomName + "' and Seat.roomID = Room.roomID and Seat.seatID = InSeat.seatID";
                ds = Form35.Query(sql);
                dt = ds.Tables[0];
                //当前已经占好的座位数
                seatSumNow = Convert.ToInt32(dt.Rows[0][0].ToString());
                CreateImage2();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //如果有空在这边写当前占座人数年级分布
        private void CreateImage1()
        {
            string roomName = comboBox1.Text;
            if (roomName == "") {
                MessageBox.Show("请选择自习室"); 
                return; 
            }
            string sql = "select count(*) from Student, InSeat,Seat, Room where Student.studentID = InSeat.studentID and studentGrade = '2017'"
                + " and Seat.seatID = InSeat.seatID and Room.roomID = Seat.roomID and Room.roomName = '" + roomName + "';";
            DataSet ds = Form35.Query(sql);
            DataTable dt = ds.Tables[0];
            int count17 = Convert.ToInt32(dt.Rows[0][0].ToString());

            sql = "select count(*) from Student, InSeat,Seat, Room where Student.studentID = InSeat.studentID and studentGrade = '2018'"
                + " and Seat.seatID = InSeat.seatID and Room.roomID = Seat.roomID and Room.roomName = '" + roomName + "';";
            ds = Form35.Query(sql);
            dt = ds.Tables[0];
            int count18 = Convert.ToInt32(dt.Rows[0][0].ToString());

            sql = "select count(*) from Student, InSeat,Seat, Room where Student.studentID = InSeat.studentID and studentGrade = '2019'"
                + " and Seat.seatID = InSeat.seatID and Room.roomID = Seat.roomID and Room.roomName = '" + roomName + "';";
            ds = Form35.Query(sql);
            dt = ds.Tables[0];
            int count19 = Convert.ToInt32(dt.Rows[0][0].ToString());


            sql = "select count(*) from Student, InSeat,Seat, Room where Student.studentID = InSeat.studentID and studentGrade = '2020'"
                + " and Seat.seatID = InSeat.seatID and Room.roomID = Seat.roomID and Room.roomName = '" + roomName + "';";
            ds = Form35.Query(sql);
            dt = ds.Tables[0];
            int count20 = Convert.ToInt32(dt.Rows[0][0].ToString());

            float Total;

            //转换成单精度。也可写成Convert.ToInt32
            Total = Convert.ToSingle(count17 + count18 + count19 + count20);

            // Total=Convert.ToSingle(ds.Tables[0].Rows[0][this.count[0]]);
            //设置字体，fonttitle为主标题的字体
            Font fontlegend = new Font("verdana", 9);
            Font fonttitle = new Font("verdana", 10, FontStyle.Bold);

            //背景宽度
            int width = pictureBox1.Width;
            int piewidth = pictureBox1.Width;
            int bufferspace = 15;
            int legendheight = fontlegend.Height * 10 + bufferspace; //高度
            int titleheight = fonttitle.Height + bufferspace;
            int height = width + legendheight + titleheight + bufferspace;//白色背景高
            int pieheight = piewidth;
            Rectangle pierect = new Rectangle(0, titleheight, piewidth, pieheight);

            //加上各种随机色
            ArrayList colors = new ArrayList();
            Random rnd = new Random();
            for (int i = 0; i < 4; i++)
                colors.Add(new SolidBrush(Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255))));

            //创建一个bitmap实例
            Bitmap objbitmap = new Bitmap(width, height);
            Graphics objgraphics = Graphics.FromImage(objbitmap);

            //画一个白色背景
            objgraphics.FillRectangle(new SolidBrush(Color.White), 0, 0, width, height);

            //画一个亮黄色背景 
            //objgraphics.FillRectangle(new SolidBrush(Color.Beige), pierect);

            //以下为画饼图(有几行row画几个)
            float currentdegree = 0.0f;

            //2017
            objgraphics.FillPie((SolidBrush)colors[0], pierect, currentdegree, Convert.ToSingle(count17) / Total * 360);
            currentdegree += Convert.ToSingle(count17) / Total * 360;

            //MessageBox.Show(Convert.ToString(currentdegree));
            //2018
            objgraphics.FillPie((SolidBrush)colors[1], pierect, currentdegree,
           (Convert.ToSingle(count18)) / Total * 360);
            currentdegree += ((Convert.ToSingle(count18))) / Total * 360;
            //MessageBox.Show(Convert.ToString(currentdegree));

            //2019
            objgraphics.FillPie((SolidBrush)colors[2], pierect, currentdegree,
           (Convert.ToSingle(count19)) / Total * 360);
            currentdegree += ((Convert.ToSingle(count19))) / Total * 360;
            //MessageBox.Show(Convert.ToString(currentdegree));

            //2020
            objgraphics.FillPie((SolidBrush)colors[3], pierect, currentdegree,
           (Convert.ToSingle(count20)) / Total * 360);
            currentdegree += ((Convert.ToSingle(count20))) / Total * 360;
            //MessageBox.Show(Convert.ToString(currentdegree));



            SolidBrush blackbrush = new SolidBrush(Color.Black);
            SolidBrush bluebrush = new SolidBrush(Color.Blue);
            string title = " 年级分布情况" + "\n \n\n";
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            objgraphics.DrawString(title, fonttitle, blackbrush,
            new Rectangle(0, 0, piewidth, titleheight + 10), stringFormat);

            //列出各字段与得数目
            // objgraphics.DrawRectangle(new Pen(Color.Red, 2), 0, height + 10 - legendheight, piewidth, legendheight + 50);

            //objgraphics.DrawString("----------------统计信息------------------",
            //fontlegend, bluebrush, 20, height - legendheight + fontlegend.Height * 1 - pieheight + 1);
            objgraphics.DrawString("自习室名称: " + Convert.ToString(comboBox1.SelectedItem),
            fontlegend, blackbrush, 20, height - legendheight + fontlegend.Height * 1 - pieheight + 1);

            objgraphics.FillRectangle((SolidBrush)colors[0], 5, height - legendheight + fontlegend.Height * 5 - pieheight + 1, 10, 10);
            objgraphics.DrawString("2017: " + Convert.ToString(count17),
            fontlegend, blackbrush, 20, height - legendheight + fontlegend.Height * 5 - pieheight + 1);
            objgraphics.FillRectangle((SolidBrush)colors[1], 5, height - legendheight + fontlegend.Height * 6 - pieheight + 1, 10, 10);
            objgraphics.DrawString("2018: " + Convert.ToString(count18),
            fontlegend, blackbrush, 20, height - legendheight + fontlegend.Height * 6 - pieheight + 1);
            objgraphics.FillRectangle((SolidBrush)colors[2], 5, height - legendheight + fontlegend.Height * 7 - pieheight + 1, 10, 10);
            objgraphics.DrawString("2019: " + Convert.ToString(count19), fontlegend, blackbrush, 20, height - pieheight - legendheight + fontlegend.Height * 7 + 1);
            objgraphics.FillRectangle((SolidBrush)colors[3], 5, height - legendheight + fontlegend.Height * 8 - pieheight + 1, 10, 10);
            objgraphics.DrawString("2020: " + Convert.ToString(count20) , fontlegend,
            blackbrush, 20, height - legendheight + fontlegend.Height * 8 - pieheight + 1);
            //Response.ContentType = "image/Jpeg";
            //objbitmap.Save(System.IO.Stream.Null, System.Drawing.Imaging.ImageFormat.Jpeg);
            Image img = Image.FromHbitmap(objbitmap.GetHbitmap());
            pictureBox1.Image = img;
            img.Save("1.jpg");
            objgraphics.Dispose();
            objbitmap.Dispose();

        }

        private void CreateImage2()
        {


            float Total;

            //转换成单精度。也可写成Convert.ToInt32
            Total = Convert.ToSingle(seatSum);

            // Total=Convert.ToSingle(ds.Tables[0].Rows[0][this.count[0]]);
            //设置字体，fonttitle为主标题的字体
            Font fontlegend = new Font("verdana", 9);
            Font fonttitle = new Font("verdana", 10, FontStyle.Bold);

            //背景宽度
            int width = pictureBox1.Width;
            int piewidth = pictureBox1.Width;
            int bufferspace = 15;
            int legendheight = fontlegend.Height * 10 + bufferspace; //高度
            int titleheight = fonttitle.Height + bufferspace;
            int height = width + legendheight + titleheight + bufferspace;//白色背景高
            int pieheight = piewidth;
            Rectangle pierect = new Rectangle(0, titleheight, piewidth, pieheight);

            //加上各种随机色
            ArrayList colors = new ArrayList();
            Random rnd = new Random();
            for (int i = 0; i < 2; i++)
                colors.Add(new SolidBrush(Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255))));

            //创建一个bitmap实例
            Bitmap objbitmap = new Bitmap(width, height);
            Graphics objgraphics = Graphics.FromImage(objbitmap);

            //画一个白色背景
            objgraphics.FillRectangle(new SolidBrush(Color.White), 0, 0, width, height);

            //以下为画饼图(有几行row画几个)
            float currentdegree = 0.0f;

            //画通过人数
            objgraphics.FillPie((SolidBrush)colors[1], pierect, currentdegree,Convert.ToSingle(seatSumNow) / Total * 360);
            currentdegree += Convert.ToSingle(seatSumNow) / Total * 360;

            //MessageBox.Show(Convert.ToString(currentdegree));
            //未通过人数饼状图
            objgraphics.FillPie((SolidBrush)colors[0], pierect, currentdegree,
           (Convert.ToSingle(seatSum - seatSumNow)) / Total * 360);
            currentdegree += ((Convert.ToSingle(1.0 * seatSum - seatSumNow))) / Total * 360;
            //MessageBox.Show(Convert.ToString(currentdegree));

            //以下为生成主标题
            SolidBrush blackbrush = new SolidBrush(Color.Black);
            SolidBrush bluebrush = new SolidBrush(Color.Blue);
            string title = " 上座率统计: " + "\n \n\n";
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            objgraphics.DrawString(title, fonttitle, blackbrush,
            new Rectangle(0, 0, piewidth, titleheight - 20), stringFormat);

            //列出各字段与得数目
            // objgraphics.DrawRectangle(new Pen(Color.Red, 2), 0, height + 10 - legendheight, piewidth, legendheight + 50);

            //objgraphics.DrawString("----------------统计信息------------------",
            //fontlegend, bluebrush, 20, height - legendheight + fontlegend.Height * 1 - pieheight + 1);
            objgraphics.DrawString("自习室名称: " + Convert.ToString(comboBox1.SelectedItem),
            fontlegend, blackbrush, 20, height - legendheight + fontlegend.Height * 3 - pieheight + 1);

            objgraphics.FillRectangle((SolidBrush)colors[1], 5, height - legendheight + fontlegend.Height * 6 - pieheight + 1, 10, 10);
            objgraphics.DrawString("座位总数: " + Convert.ToString(seatSum),
            fontlegend, blackbrush, 20, height - legendheight + fontlegend.Height * 5 - pieheight + 1);
            objgraphics.FillRectangle((SolidBrush)colors[0], 5, height - legendheight + fontlegend.Height * 7 - pieheight + 1, 10, 10);
            objgraphics.DrawString("自习人数: " + Convert.ToString(seatSumNow),
            fontlegend, blackbrush, 20, height - legendheight + fontlegend.Height * 6 - pieheight + 1);
            objgraphics.DrawString("空闲座位: " + ((Convert.ToSingle(seatSum - seatSumNow))), fontlegend, blackbrush, 20, height - pieheight - legendheight + fontlegend.Height * 7 + 1);

            objgraphics.DrawString("上座率: " +  Convert.ToString(seatSumNow * 1.0 / seatSum * 100) + " %", fontlegend,
            blackbrush, 20, height - legendheight + fontlegend.Height * 9 - pieheight + 1);
            //Response.ContentType = "image/Jpeg";
            //objbitmap.Save(System.IO.Stream.Null, System.Drawing.Imaging.ImageFormat.Jpeg);
            Image img = Image.FromHbitmap(objbitmap.GetHbitmap());
 
            pictureBox1.Image = img;
            img.Save("2.jpg");
            objgraphics.Dispose();
            objbitmap.Dispose();

        }


        private void button2_Click(object sender, EventArgs e)
        {
            CreateImage1();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // this.Hide();
            Form32 f5 = new Form32(Uid);
            f5.Owner = this;
            f5.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form33 f6 = new Form33(Uid);
            f6.Owner = this;
            f6.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form34 f7 = new Form34(Uid);
            f7.Owner = this;
            f7.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string sql = "select studentName, totalTime " +
                         "from Student order by totalTime desc;";
            DataSet ds = Form35.Query(sql);
            DataTable dt = ds.Tables[0];
            label9.Text = dt.Rows[0][0].ToString();
            label10.Text = dt.Rows[1][0].ToString();
            label11.Text = dt.Rows[2][0].ToString();
        }

        private void Form31_Load(object sender, EventArgs e)
        {
            int xWidth = SystemInformation.PrimaryMonitorSize.Width;
            int yHeight = SystemInformation.PrimaryMonitorSize.Height;
            this.Location = new Point(xWidth / 2 - this.Width / 2, yHeight / 2 - this.Height / 2);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }


}
