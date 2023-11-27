using shooter.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace shooter
{
    class Enemy
    {
      
        public PictureBox PictureBox { get; private set; }
        public int Speed { get; private set; }
        public int Heath { get => heath; set => heath = value; }

        private int heath;

        public Enemy(Form form, PictureBox player)
        {
            PictureBox = new PictureBox();
            PictureBox.BackColor = Color.Transparent;
            PictureBox.Image = Resources.Baddy5;
            PictureBox.Size = new Size(30, 30);
            PictureBox.SizeMode  = PictureBoxSizeMode.StretchImage;
            PictureBox.Tag = "enemy";
            PictureBox.Left = GetRandomSpawnX(form);
            PictureBox.Top = GetRandomSpawnY(form);
            PictureBox.BringToFront();

            form.Controls.Add(PictureBox);

            Speed = 3;
            Heath = 1;
        }
        private int GetRandomSpawnX(Form form)
        {
            Random rand = new Random();
            int side = rand.Next(0, 4); // Determine which side of the window to spawn from

            switch (side)
            {
                case 0: // Top
                    return rand.Next(0, form.ClientSize.Width - PictureBox.Width);
                case 1: // Right
                    return form.ClientSize.Width;
                case 2: // Bottom
                    return rand.Next(0, form.ClientSize.Width- PictureBox.Width);
                case 3: // Left
                    return -PictureBox.Width;
                default:
                    return 0;
            }
        }

        private int GetRandomSpawnY(Form form)
        {
            Random rand = new Random();
            int side = rand.Next(0, 4); // Determine which side of the window to spawn from

            switch (side)
            {
                case 0: // Top
                    return -PictureBox.Height;
                case 1: // Right
                    return rand.Next(0, form.ClientSize.Height - PictureBox.Height);
                case 2: // Bottom
                    return form.ClientSize.Height;
                case 3: // Left
                    return rand.Next(0, form.ClientSize.Height - PictureBox.Height);
                default:
                    return 0;
            }
        }
        public void Render(Player player)
        {
            int directionX = player.PictureBox.Left - this.PictureBox.Left;
            int directionY = player.PictureBox.Top - this.PictureBox.Top;

            // Normalize the direction
            double length = Math.Sqrt(directionX * directionX + directionY * directionY);

            directionX = (int)(this.Speed * directionX / length);
            directionY = (int)(this.Speed * directionY / length);

            // Move the this towards the player
            this.PictureBox.Left += directionX;
            this.PictureBox.Top += directionY;
        }
    }
}