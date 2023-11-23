using System.Drawing.Imaging;

namespace shooter
{
    public partial class Form1 : Form
    {

        private Player player;
        bool moveRight, moveLeft, moveUp, moveDown;
        private List<Enemy> enemies = new List<Enemy>();
        private static List<PictureBox> bullets = new List<PictureBox>();
        public static int NYA;

        int speed = 10;

        internal static List<PictureBox> Bullets { get => bullets; set => bullets = value; }

        public Form1()
        {
            InitializeComponent();
            player = new Player(character, speed);

        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            if (moveRight)
            {
                player.MoveRight(ClientSize.Width);
            }
            if (moveLeft)
            {
                player.MoveLeft();
            }
            if (moveUp)
            {
                player.MoveUp();
            }
            if (moveDown)
            {
                player.MoveDown(ClientSize.Height);
            }
            if (moveUp && moveRight)
            {
                player.MoveUpRight(ClientSize.Width);
            }
            else if (moveDown && moveRight)
            {
                player.MoveDownRight(ClientSize.Width, ClientSize.Height);
            }
            else if (moveUp && moveLeft)
            {
                player.MoveUpLeft();
            }
            else if (moveDown && moveLeft)
            {
                player.MoveDownLeft(ClientSize.Height);
            }





            foreach (Enemy enemy in enemies)
            {

                // Calculate the direction towards the player
                int directionX = player.PictureBox.Left - enemy.PictureBox.Left;
                int directionY = player.PictureBox.Top - enemy.PictureBox.Top;

                // Normalize the direction
                double length = Math.Sqrt(directionX * directionX + directionY * directionY);

                directionX = (int)(enemy.Speed * directionX / length);
                directionY = (int)(enemy.Speed * directionY / length);

                // Move the enemy towards the player
                enemy.PictureBox.Left += directionX;
                enemy.PictureBox.Top += directionY;

                // Check if the enemy has collided with the player
                if (enemy.PictureBox.Bounds.IntersectsWith(player.PictureBox.Bounds))
                {
                    // Handle player-enemy collision
                    // ...
                }


                foreach (PictureBox bullet in Bullets)
                {
                    if (bullet.Bounds.IntersectsWith(enemy.PictureBox.Bounds))
                    {
                        // Handle bullet-enemy collision
                        // Remove the bullet
                        bullet.Dispose();
                        Bullets.Remove(bullet);

                        // Remove the enemy
                        enemy.Heath = 0;

                        break; // Exit the loop since the enemy has been removed
                    }
                }
                if (enemy.Heath == 0)
                {
                    enemy.PictureBox.Dispose();
                    enemies.Remove(enemy);
                    break;
                }
            }

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                moveLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                moveRight = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                moveUp = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                moveDown = true;
            }
            if (e.KeyCode == Keys.Space)
            {
                player.Shoot();
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                moveLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                moveRight = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                moveUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                moveDown = false;
            }
        }

        private void KeyIsPress(object sender, KeyPressEventArgs e)
        {

        }

        private void character_Click(object sender, EventArgs e)
        {

        }
        private void EnemyTimerEvent(object sender, EventArgs e)
        {


        }

        private void SpawnEnemy()
        {
            Enemy enemy = new Enemy(this, player.PictureBox);
            enemies.Add(enemy);
        }

        private void SpawnTimerEvent(object sender, EventArgs e)
        {
            SpawnEnemy();
        }
    }
}