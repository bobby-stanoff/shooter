using System;
using System.Collections.Generic;
using System.IO;
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
        public int Health { get => health; set => health = value; }
        public int Score { get => score; set => score = value; }

        private List<Bullet> bullets;
        private string facing;
        private bool canShoot = true;
        private int health;
        private int score;
        private System.Windows.Forms.Timer shootTimer;
        private Panel gamePanel;
       
        private int clientHeight;
        private int clientWidth;
        

        public Player(PictureBox pictureBox, int speed, Panel gamePanel)
        {
            PictureBox = pictureBox;
            Speed = speed;
            
            bullets = new List<Bullet>();
            Facing = "up";
            Health = 3;
            Score = 0;
            shootTimer = new System.Windows.Forms.Timer();
            shootTimer.Interval = 100;
            shootTimer.Tick += ShootTimer_Tick;
            clientHeight = gamePanel.Height;
            clientWidth = gamePanel.Width;
            this.gamePanel = gamePanel;
            
        }

        public void MoveUp()
        {
            if (PictureBox.Top >= 50)
            {
                PictureBox.Top -= Speed;
                Facing = "up";
            }
        }

        public void MoveDown()
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

        public void MoveRight()
        {
            if (PictureBox.Right + Speed <= clientWidth)
            {
                PictureBox.Left += Speed;
                Facing = "right";
            }
        }
        public void MoveUpRight()
        {
            if (PictureBox.Top - Speed >= 0 && PictureBox.Right + Speed <= clientWidth)
            {
                PictureBox.Top -= Speed/10;
                PictureBox.Left += Speed/10;
                Facing = "upright";
            }
        }

        public void MoveDownRight()
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

        public void MoveDownLeft()
        {
            if (PictureBox.Bottom + Speed <= clientHeight && PictureBox.Left - Speed >= 0)
            {
                PictureBox.Top += Speed / 10;
                PictureBox.Left -= Speed / 10;
                Facing = "downleft";
            }
        }
        public void BasicShoot()
        {
            Shoot(facing);
        }
        public void Shoot(string direction)
        {
            
            if (canShoot)
            {
                int directionX = 0;
                int directionY = 0;

                switch (direction)
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




                int bulletLeft = PictureBox.Left + (PictureBox.Width / 2);
                int bulletTop = PictureBox.Top + (PictureBox.Width / 2);
                Bullet shootBullet = new Bullet(gamePanel,direction,bulletLeft,bulletTop);
                
                //shootBullet.MakeBullet();
                shootBullet.Render(directionX, directionY);
                // Add the projectile to the form's Controls collection


                canShoot = false;
                shootTimer.Start();
            }
            
        }

        public void TwoShoot()
        {
            

            switch (facing)
            {
                case "left":
                    
                    Shoot("upleft");
                    canShoot = true;
                    Shoot("downleft");
                    break;
                case "right":
                    
                    Shoot("upright");
                    canShoot = true;
                    Shoot("downright");
                    break;
                case "up":
                    
                    Shoot("upleft");
                    canShoot = true;
                    Shoot("upright");
                    break;
                case "down":
                    
                    Shoot("downright");
                    canShoot = true;
                    Shoot("downleft");
                    break;
                default:
                    Shoot(facing);
                    break;

            }
        }
        
        private void ShootTimer_Tick(object sender, EventArgs e)
        {
            canShoot = true;
            shootTimer.Stop();
        }
        public void UpdatePosition()
        {
            clientWidth = gamePanel.Width;
            clientHeight = gamePanel.Height;
        }
    }
}
