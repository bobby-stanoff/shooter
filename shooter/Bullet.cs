using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

namespace shooter
{
    class Bullet
    {
        public string direction;
        public int bulletLeft;
        public int bulletTop;

        int deltaX = 0;
        int deltaY = 0;
        private int speed = 20;
        private int type = 0 ;
        private PictureBox bullet = new PictureBox();
        private System.Windows.Forms.Timer bulletTimer = new System.Windows.Forms.Timer();
        private Panel gamepanel;
        private int initalPanelWidth;
        private int initalPanelHeight;
        public int InititalPanelWidth { get => initalPanelWidth; set => initalPanelWidth = value; }
        public int InitialPanelHeight { get => initalPanelHeight; set => initalPanelHeight = value; }
        public Bullet(Panel gamepanel, string direction, int left, int top,int type)
        {
            this.direction = direction;
            this.bulletLeft = left;
            this.bulletTop = top;
            bullet.BackColor = Color.Red;
            bullet.Size = new Size(5, 5);
            bullet.Tag = "bullet";
            bullet.Left = bulletLeft;
            bullet.Top = bulletTop;
            bullet.BringToFront();
            this.gamepanel = gamepanel;
            InititalPanelWidth = gamepanel.Width;
            InitialPanelHeight = gamepanel.Height;
            gamepanel.Controls.Add(bullet);
            Form1.Bullets.Add(bullet);
            this.type = type;

        }

        public void Render(int directionX, int directionY)
        {



            bulletTimer.Interval = speed;
            bulletTimer.Tick += BulletTimerEvent;

            bulletTimer.Start();

            deltaX = speed * directionX;
            deltaY = speed * directionY;


        }

        private void BulletTimerEvent(object sender, EventArgs e)
        {
            bullet.Left += deltaX;
            bullet.Top += deltaY;


            if (direction == "left")
            {
                bullet.Left -= speed;
            }

            if (direction == "right")
            {
                bullet.Left += speed;
            }

            if (direction == "up")
            {
                bullet.Top -= speed;
            }

            if (direction == "down")
            {
                bullet.Top += speed;
            }


            if (bullet.Left < 10 || bullet.Left > gamepanel.Width || bullet.Top < 10 || bullet.Top > gamepanel.Height)
            {
                if(type == 1)
                {
                   ResizeBullet();
                }
                bulletTimer.Stop();
                bulletTimer.Dispose();
                bullet.Dispose();
                bulletTimer = null;
                bullet = null;

            }

        }
        private void ResizeBullet()
        {
            if (bullet.Left > gamepanel.Width - 5)
            {
                gamepanel.Width += 20;
            }
            if (bullet.Top > gamepanel.Height - 5 )
            {
                gamepanel.Height += 20;
            }

        }
    }
}