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
        private PictureBox bullet = new PictureBox();
        private System.Windows.Forms.Timer bulletTimer = new System.Windows.Forms.Timer();
        private Panel gamepanel;

        public Bullet(Panel gamepanel,string direction, int left, int top) 
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
            gamepanel.Controls.Add(bullet);
            Form1.Bullets.Add(bullet);

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
                //if (bullet.Left > gamepanel.Width - 5)
                //{
                //    gamepanel.Width += 20;
                //}
                //if (bullet.Top > gamepanel.Height - 5)
                //{
                //    gamepanel.Height += 20;
                //}
                bulletTimer.Stop();
                bulletTimer.Dispose();
                bullet.Dispose();
                bulletTimer = null;
                bullet = null;
                
            }



        }



    }
}