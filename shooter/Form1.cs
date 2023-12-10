using System;
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
        private static List<Item> items = new List<Item>();
        private static List<PictureBox> playerLife = new List<PictureBox>();

        private int collisionCooldown = 0;
        private const int maxCollisionCooldown = 80; // 180 frames = 3 seconds at 60 FPS
        private bool gamePaused = false;
        private int pauseTimer = 80;
        private System.Windows.Forms.Timer blinkTimer;
        private int BlinkelapsedDuration = 0;



        int speed = 10;

        internal static List<PictureBox> Bullets { get => bullets; set => bullets = value; }
        internal static List<PictureBox> PlayerLife { get => playerLife; set => playerLife = value; }


        public Form1()
        {
            InitializeComponent();


            player = new Player(character, speed, gamePanel);
            gamePanel.Controls.Add(player.PictureBox);
            Size = new Size(gamePanel.Size.Width+10,gamePanel.Size.Height+50);

            RenderStatus();




        }

        private void MainTimerEvent(object sender, EventArgs e)
        {

            player.UpdatePosition();
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

            foreach (Item item in items)
            {
                if (item.PictureBox.Bounds.IntersectsWith(player.PictureBox.Bounds))
                {
                    player.ConsumeItem(item.Type);

                    item.PictureBox.Dispose();
                    this.Controls.Remove(item.PictureBox);
                    items.Remove(item);
                    //MessageBox.Show(player.Consumed);

                    // Remove the enemy


                    break; // Exit the loop since the enemy has been removed
                }
            }
            if (player.Consumed != "")
            {
                if (player.Consumed == "galactic")
                {
                    shrinkTimer.Start();
                }
                else if (player.Consumed == "antigalactic")
                {
                    shrinkTimer.Stop();
                }
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
                        StartBlinking(player.PictureBox, 2000);
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
                        this.Controls.Remove(bullet);
                        Bullets.Remove(bullet);

                        // Remove the enemy
                        enemy.Heath = 0;
                        player.Score += 1;
                        ScoreLabel.Text = "· " + player.Score.ToString();

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
                if (player.Consumed == "sword")
                {
                    player.TwoShoot();
                }
                else player.BasicShoot();
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
        private void SpawnItem()
        {
            List<string> availableItems = new List<string> { "sword", "galactic", "antigalactic" };
            Random random = new Random();
            int index = random.Next(availableItems.Count);
            Item item = new Item(gamePanel, availableItems[index]);
            items.Add(item);
        }
        private void SpawnTimerEvent(object sender, EventArgs e)
        {

            SpawnEnemy();
            Random random = new Random();
            int itemchances = random.Next(0, 5);
            if (itemchances == 0)
            {
                SpawnItem();
            }
        }

        private void GameOver()
        {
            // MessageBox.Show("boo boo over");
            // Stop the game
            MainTimer.Stop();
            shrinkTimer.Stop();
            SpawnTimer.Stop();

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
            for (int i = 0; i < player.Health; i++)
            {
                var lifeimg = new PictureBox();
                lifeimg.Size = new Size(30, 30);
                lifeimg.BackColor = Color.Red;
                lifeimg.Tag = "kk";
                lifeimg.Left = 100 + 50 * i;
                lifeimg.Top = 10;
                lifeimg.BringToFront();
                gamePanel.Controls.Add(lifeimg);
                playerLife.Add(lifeimg);
            }
        }



        private void shrinkTimer_Tick(object sender, EventArgs e)
        {
            UpdateShrink();

        }
        private void UpdateShrink()
        {
            int MaxGamePanelWidth = 720;
            int MaxGamePanelHeight = 720;
            int MinGamePanelWidth = 400;
            int MinGamePanelHeight = 200;
            int gamePanelWidth = gamePanel.Width;
            int gamePanelHeight = gamePanel.Height;
            if (gamePanelWidth > MaxGamePanelWidth)
            {
                gamePanelWidth = MaxGamePanelWidth;
            }
            if (gamePanelHeight > MaxGamePanelHeight)
            {
                gamePanelHeight = MaxGamePanelHeight;
            }
            gamePanelWidth = gamePanelWidth > MinGamePanelWidth ? gamePanelWidth : MinGamePanelWidth;
            gamePanelHeight = gamePanelHeight > MinGamePanelHeight ? gamePanelHeight : MinGamePanelHeight;

            gamePanel.Size = new Size(gamePanelWidth-1, gamePanelHeight-1);
            this.Size = new Size(gamePanel.Size.Width+10,gamePanel.Size.Height+50);
        }

        public void StartBlinking(PictureBox pictureBox, int blinkDuration)
        {
            blinkTimer = new System.Windows.Forms.Timer();
            blinkTimer.Interval = 200;
            blinkTimer.Enabled = true;
            blinkTimer.Tick += (sender, e) => BlinkTimer_Tick(pictureBox, blinkDuration);

            pictureBox.Visible = true;

            // Reset the elapsed duration
            BlinkelapsedDuration = 0;

            // Start the Timer
            blinkTimer.Start();
        }



        private void BlinkTimer_Tick(PictureBox pictureBox, int blinkDuration)
        {
            // Toggle the visibility of the PictureBox
            pictureBox.Visible = !pictureBox.Visible;

            // Increment the elapsed duration
            BlinkelapsedDuration += 200;

            // Check if the elapsed duration reaches the blink duration
            if (BlinkelapsedDuration >= blinkDuration)
            {
                blinkTimer.Stop();
                blinkTimer.Enabled = false;
                // Ensure the PictureBox is visible
                pictureBox.Visible = true;
            }
        }

        private void btnplay_Click(object sender, EventArgs e)
        {
            MainTimer.Start();
            SpawnTimer.Start();


        }
    }
}