using System;
using WMPLib;
using TagLib;
using System.Drawing;
using System.Windows.Forms;

namespace A_Player
{
    public partial class Form1 : Form
    {
        WindowsMediaPlayer wmp;

        bool pausePlay;
        bool mousePressed = false;
        Point mouseDownPos;
        string fileName;
        int trackInfo = 0;
        bool left = true;

        public Form1()
        {
            InitializeComponent();

            wmp = new WindowsMediaPlayer();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if ((ofd.ShowDialog() == DialogResult.OK) && (ofd.FileName != string.Empty))
            {
                wmp.URL = ofd.FileName;
                wmp.controls.play();
                trackBar1.Enabled = true;
                trackBar2.Enabled = true;
                timer1.Enabled = true;
                timer1.Interval = 1000;
                label1.Text = "Сейчас играет:";
                button1.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = true;
                ShowInTaskbar = false;
                TopMost = true;

                var file = File.Create(ofd.FileName);
                fileName = file.Tag.FirstPerformer + " - " + file.Tag.Title;
                label11.Text = fileName;

                FormBorderStyle = FormBorderStyle.None;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            pausePlay = !pausePlay;
            if (pausePlay)
            {
                button2.Image = Image.FromFile("N:\\Универ\\Групповая динамика\\A_Player\\A_Player\\pause.bmp");
                wmp.controls.pause();
                label1.Text = "Пауза:";
                
            }
            if (!pausePlay)
            {
                button2.Image = Image.FromFile("N:\\Универ\\Групповая динамика\\A_Player\\A_Player\\play.bmp");
                wmp.controls.play();
                label1.Text = "Сейчас играет:";  
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            wmp.controls.stop();
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
            trackBar1.Value = 0;
            trackBar2.Value = 10;
            trackBar1.Enabled = false;
            trackBar2.Enabled = false;
            timer1.Enabled = false;
            label1.Text = "";
            label11.Text = "";
            label3.Text = "0:00:00";
            label2.Text = "0:00:00";
            ShowInTaskbar = true;
            TopMost = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            wmp.controls.currentPosition = trackBar1.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (trackBar2.Value == 10)
                wmp.settings.volume = 100;
            if (trackBar2.Value == 9)
                wmp.settings.volume = 90;
            if (trackBar2.Value == 8)
                wmp.settings.volume = 80;
            if (trackBar2.Value == 7)
                wmp.settings.volume = 70;
            if (trackBar2.Value == 6)
                wmp.settings.volume = 60;
            if (trackBar2.Value == 5)
                wmp.settings.volume = 50;
            if (trackBar2.Value == 4)
                wmp.settings.volume = 40;
            if (trackBar2.Value == 3)
                wmp.settings.volume = 30;
            if (trackBar2.Value == 2)
                wmp.settings.volume = 20;
            if (trackBar2.Value == 1)
                wmp.settings.volume = 10;
            if (trackBar2.Value == 0)
                wmp.settings.volume = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            trackBar1.Maximum = Convert.ToInt32(wmp.currentMedia.duration);
            trackBar1.Value = Convert.ToInt32(wmp.controls.currentPosition);

            if (wmp != null)
            {
                int s = (int)wmp.currentMedia.duration;
                int h = s / 3600;
                int m = (s - (h * 3600)) / 60;
                s = s - (h * 3600 + m * 60);
                label3.Text = String.Format("{0:D}:{1:D2}:{2:D2}", h, m, s);

                s = (int)wmp.controls.currentPosition;
                h = s / 3600;
                m = (s - (h * 3600)) / 60;
                s = s - (h * 3600 + m * 60);
                label2.Text = String.Format("{0:D}:{1:D2}:{2:D2}", h, m, s);
            }
            else
            {
                label3.Text = "0:00:00";
                label2.Text = "0:00:00";
            }

            if (trackInfo <= fileName.Length)
            {
                timer1.Interval = 250;
                if (left)
                    label11.Text = fileName.Substring(trackInfo, fileName.Length - trackInfo);
                else
                    label11.Text =  fileName.Substring(fileName.Length - trackInfo, trackInfo);
                trackInfo++;
            }
            else
            {
                trackInfo = 0;
                if (left) left = false;
                else left = true;
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Focus();
            mousePressed = true;
            mouseDownPos = new Point(f1.Location.X + e.Location.X, f1.Location.Y + e.Location.Y + SystemInformation.CaptionHeight);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousePressed)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (FormBorderStyle == FormBorderStyle.None)
                    {
                        Left = Cursor.Position.X - mouseDownPos.X;
                        Top = Cursor.Position.Y - mouseDownPos.Y + 22;
                    }
                    else
                    {
                        Left = Cursor.Position.X - mouseDownPos.X - 3;
                        Top = Cursor.Position.Y - mouseDownPos.Y - 3;
                    }
                }
            }
           
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mousePressed = false;
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
