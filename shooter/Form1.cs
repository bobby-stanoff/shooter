using System.Diagnostics;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace shooter
{
    public partial class Form1 : Form
    {

        private Player player;
        bool moveRight, moveLeft, moveUp, moveDown;
        private List<Enemy> enemies = new List<Enemy>();
        private static List<PictureBox> bullets = new List<PictureBox>();
        private static List<PictureBox> playerLife = new List<PictureBox>();
        public static int NYA;
        private int collisionCooldown = 0;
        private const int maxCollisionCooldown = 80; // 180 frames = 3 seconds at 60 FPS
        private bool gamePaused = false;
        private int pauseTimer = 80;


        int speed = 10;

        internal static List<PictureBox> Bullets { get => bullets; set => bullets = value; }
        internal static List<PictureBox> PlayerLife { get => playerLife; set => playerLife = value; }


        public Form1()
        {
            InitializeComponent();


            player = new Player(character, speed, gamePanel);
            gamePanel.Controls.Add(player.PictureBox);
            RenderStatus();

        }

        private void MainTimerEvent(object sender, EventArgs e)
        {

            if (collisionCooldown > 0)
            {
                collisionCooldown--;
                return; // Pause the game for the collision cooldown duration
            }

            if (moveRight)
            {
                player.MoveRight();
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
                player.MoveDown();
            }
            if (moveUp && moveRight)
            {
                player.MoveUpRight();
            }
            else if (moveDown && moveRight)
            {
                player.MoveDownRight();
            }
            else if (moveUp && moveLeft)
            {
                player.MoveUpLeft();
            }
            else if (moveDown && moveLeft)
            {
                player.MoveDownLeft();
            }





            foreach (Enemy enemy in enemies)
            {

                // Calculate the direction towards the player
                enemy.Render(player);
                gamePanel.Controls.Add(enemy.PictureBox);

                // Check if the enemy has collided with the player
                if (enemy.PictureBox.Bounds.IntersectsWith(player.PictureBox.Bounds))
                {

                   
                    if (player.Health <= 0)
                    {
                        GameOver();
                    }
                    else
                    {
                        player.Health--;
                        //lifelabel.Text = "Health: " + player.Health.ToString();
                        
                        collisionCooldown = maxCollisionCooldown;
                        RemoveAllEnemies();
                        RenderStatus();

                        
                    }

                    break;
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
                player.BasicShoot();
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

        private void GameOver()
        {
            // MessageBox.Show("boo boo over");
            // Stop the game
            MainTimer.Stop();

            // Display game over message
            MessageBox.Show("Game Over!");
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void RemoveAllEnemies()
        {
            foreach (var enemy in enemies)
            {

                enemy.PictureBox.Dispose();
                this.Controls.Remove(enemy.PictureBox);
            }
            enemies.Clear();
        }
        private void RenderStatus()
        {
            foreach (var enemy in playerLife)
            {

                enemy.Dispose();
                this.Controls.Remove(enemy);
            }
            playerLife.Clear();
            for(int i = 0; i< player.Health; i++)
            {
                var lifeimg = new PictureBox();
                lifeimg.Size = new Size(30, 30);
                lifeimg.BackColor = Color.Black;
                lifeimg.Tag = "kk";
                lifeimg.Left = 830 + 50*i;
                lifeimg.Top = 18;
                lifeimg.BringToFront();
                Controls.Add(lifeimg);
                playerLife.Add(lifeimg);
            }
        }

    }
}