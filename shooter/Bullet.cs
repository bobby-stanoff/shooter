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


        

        public void MakeBullet()
        {

            bullet.BackColor = Color.Red;
            bullet.Size = new Size(5, 5);
            bullet.Tag = "bullet";
            bullet.Left = bulletLeft;
            bullet.Top = bulletTop;
            bullet.BringToFront();

            Form1.ActiveForm.Controls.Add(bullet);


            bulletTimer.Interval = speed;
            bulletTimer.Tick += new EventHandler(BulletTimerEvent);
            bulletTimer.Start();

        }
        public void MakeBullet(int directionX, int directionY)
        {
            bullet.BackColor = Color.Red;
            bullet.Size = new Size(5, 5);
            bullet.Tag = "bullet";
            bullet.Left = bulletLeft;
            bullet.Top = bulletTop;
            bullet.BringToFront();

            Form1.ActiveForm.Controls.Add(bullet);
            Form1.Bullets.Add(bullet);

            bulletTimer.Interval = speed;
            //bulletTimer.Tick += BulletTimerEvent;
            bulletTimer.Tick += new EventHandler(BulletTimerEvent);
            bulletTimer.Start();

            deltaX = speed * directionX;
            deltaY = speed * directionY;

            void BulletTimerEvent(object sender, EventArgs e)
            {
                bullet.Left += deltaX;
                bullet.Top += deltaY;

                if (bullet.Left < 10 || bullet.Left > 860 || bullet.Top < 10 || bullet.Top > 600)
                {
                    bulletTimer.Stop();
                    bulletTimer.Dispose();
                    bullet.Dispose();
                    bulletTimer = null;
                    bullet = null;
                }
            }
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


            if (bullet.Left < 10 || bullet.Left > 860 || bullet.Top < 10 || bullet.Top > 600)
            {
                bulletTimer.Stop();
                bulletTimer.Dispose();
                bullet.Dispose();
                bulletTimer = null;
                bullet = null;
            }



        }



    }
}