namespace shooter
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            MainTimer = new System.Windows.Forms.Timer(components);
            character = new PictureBox();
            SpawnTimer = new System.Windows.Forms.Timer(components);
            gamePanel = new Panel();
            menu = new Panel();
            ScoreLabel = new Label();
            shrinkTimer = new System.Windows.Forms.Timer(components);
            btn_play = new Button();
            ((System.ComponentModel.ISupportInitialize)character).BeginInit();
            gamePanel.SuspendLayout();
            menu.SuspendLayout();
            SuspendLayout();
            // 
            // MainTimer
            // 
            MainTimer.Enabled = true;
            MainTimer.Interval = 20;
            MainTimer.Tick += MainTimerEvent;
            // 
            // character
            // 
            character.BackColor = Color.Transparent;
            character.Image = Properties.Resources.Baddy1;
            character.Location = new Point(112, 129);
            character.Name = "character";
            character.Size = new Size(36, 36);
            character.SizeMode = PictureBoxSizeMode.StretchImage;
            character.TabIndex = 0;
            character.TabStop = false;
            character.Click += character_Click;
            // 
            // SpawnTimer
            // 
            SpawnTimer.Enabled = true;
            SpawnTimer.Interval = 2000;
            SpawnTimer.Tick += SpawnTimerEvent;
            // 
            // gamePanel
            // 
            gamePanel.BackColor = Color.Black;
            gamePanel.BackgroundImageLayout = ImageLayout.Stretch;
            gamePanel.Controls.Add(menu);
            gamePanel.Controls.Add(ScoreLabel);
            gamePanel.Location = new Point(0, 0);
            gamePanel.Name = "gamePanel";
            gamePanel.Size = new Size(720, 720);
            gamePanel.TabIndex = 1;
            // 
            // menu
            // 
            menu.BackgroundImage = Properties.Resources._2020968_2001;
            menu.BackgroundImageLayout = ImageLayout.Stretch;
            menu.Controls.Add(btn_play);
            menu.Location = new Point(173, 89);
            menu.Name = "menu";
            menu.Size = new Size(308, 467);
            menu.TabIndex = 3;
            // 
            // ScoreLabel
            // 
            ScoreLabel.AutoSize = true;
            ScoreLabel.BackColor = Color.Transparent;
            ScoreLabel.Font = new Font("Calibri", 20F, FontStyle.Bold, GraphicsUnit.Point);
            ScoreLabel.ForeColor = Color.RebeccaPurple;
            ScoreLabel.Location = new Point(3, -1);
            ScoreLabel.Name = "ScoreLabel";
            ScoreLabel.Size = new Size(62, 49);
            ScoreLabel.TabIndex = 2;
            ScoreLabel.Text = "10";
            // 
            // shrinkTimer
            // 
            shrinkTimer.Tick += shrinkTimer_Tick;
            // 
            // btn_play
            // 
            btn_play.Location = new Point(97, 212);
            btn_play.Name = "btn_play";
            btn_play.Size = new Size(112, 34);
            btn_play.TabIndex = 0;
            btn_play.Text = "button1";
            btn_play.UseVisualStyleBackColor = true;
            btn_play.Click += btnplay_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(698, 664);
            Controls.Add(character);
            Controls.Add(gamePanel);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            KeyDown += KeyIsDown;
            KeyPress += KeyIsPress;
            KeyUp += KeyIsUp;
            ((System.ComponentModel.ISupportInitialize)character).EndInit();
            gamePanel.ResumeLayout(false);
            gamePanel.PerformLayout();
            menu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer MainTimer;
        private PictureBox character;
        private System.Windows.Forms.Timer SpawnTimer;
        private Panel gamePanel;
        private System.Windows.Forms.Timer shrinkTimer;
        private Label ScoreLabel;
        private Panel menu;
        private Button btn_play;
    }
}