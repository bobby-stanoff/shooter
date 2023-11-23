using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shooter
{
    internal class Player
    {
        public PictureBox PictureBox { get; } // PictureBox control for the player
        public int Speed { get; set; }
        public string Facing { get => facing; set => facing = value; }

        private List<Bullet> bullets;
        private string facing;
        private bool canShoot = true;
        private System.Windows.Forms.Timer shootTimer;


        public Player(PictureBox pictureBox, int speed)
        {
            PictureBox = pictureBox;
            Speed = speed;
            bullets = new List<Bullet>();
            Facing = "up";
            shootTimer = new System.Windows.Forms.Timer();
            shootTimer.Interval = 100;
            shootTimer.Tick += ShootTimer_Tick;
        }

        public void MoveUp()
        {
            if (PictureBox.Top - Speed >= 0)
            {
                PictureBox.Top -= Speed;
                Facing = "up";
            }
        }

        public void MoveDown(int clientHeight)
        {
            if (PictureBox.Bottom + Speed <= clientHeight)
            {
                PictureBox.Top += Speed;
                Facing = "down";
            }
        }

        public void MoveLeft()
        {
            if (PictureBox.Left - Speed >= 0)
            {
                PictureBox.Left -= Speed;
                Facing = "left";
            }
        }

        public void MoveRight(int clientWidth)
        {
            if (PictureBox.Right + Speed <= clientWidth)
            {
                PictureBox.Left += Speed;
                Facing = "right";
            }
        }
        public void MoveUpRight(int clientWidth)
        {
            if (PictureBox.Top - Speed >= 0 && PictureBox.Right + Speed <= clientWidth)
            {
                PictureBox.Top -= Speed/10;
                PictureBox.Left += Speed/10;
                Facing = "upright";
            }
        }

        public void MoveDownRight(int clientWidth, int clientHeight)
        {
            if (PictureBox.Bottom + Speed <= clientHeight && PictureBox.Right + Speed <= clientWidth)
            {
                PictureBox.Top += Speed/10;
                PictureBox.Left += Speed/10;
                Facing = "downright";
            }
        }

        public void MoveUpLeft()
        {
            if (PictureBox.Top - Speed >= 0 && PictureBox.Left - Speed >= 0)
            {
                PictureBox.Top -= Speed / 10;
                PictureBox.Left -= Speed / 10;
                Facing = "upleft";
            }
        }

        public void MoveDownLeft(int clientHeight)
        {
            if (PictureBox.Bottom + Speed <= clientHeight && PictureBox.Left - Speed >= 0)
            {
                PictureBox.Top += Speed / 10;
                PictureBox.Left -= Speed / 10;
                Facing = "downleft";
            }
        }
        public void Shoot()
        {
            if (canShoot)
            {
                int directionX = 0;
                int directionY = 0;

                switch (Facing)
                {
                    case "left":
                        directionX = -1;
                        break;
                    case "right":
                        directionX = 1;
                        break;
                    case "up":
                        directionY = -1;
                        break;
                    case "down":
                        directionY = 1;
                        break;
                    case "upleft":
                        directionX = -1;
                        directionY = -1;
                        break;
                    case "upright":
                        directionX = 1;
                        directionY = -1;
                        break;
                    case "downleft":
                        directionX = -1;
                        directionY = 1;
                        break;
                    case "downright":
                        directionX = 1;
                        directionY = 1;
                        break;
                        
                    default:
                        
                        break;
                } 
                
              
                Point bulletLocation = new Point(PictureBox.Left + (PictureBox.Width / 2) , PictureBox.Top);


                Bullet shootBullet = new Bullet();
                shootBullet.direction = Facing;
                shootBullet.bulletLeft = PictureBox.Left + (PictureBox.Width / 2);
                shootBullet.bulletTop = PictureBox.Top;
                //shootBullet.MakeBullet();
                shootBullet.MakeBullet(directionX, directionY);
                // Add the projectile to the form's Controls collection


                canShoot = false;
                shootTimer.Start();
            }
            
        }
        
        private void ShootTimer_Tick(object sender, EventArgs e)
        {
            canShoot = true;
            shootTimer.Stop();
        }
    }
}
