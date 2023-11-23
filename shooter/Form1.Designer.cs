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
            ((System.ComponentModel.ISupportInitialize)character).BeginInit();
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
            character.Location = new Point(102, 118);
            character.Name = "character";
            character.Size = new Size(51, 53);
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(877, 591);
            Controls.Add(character);
            Name = "Form1";
            Text = "Form1";
            KeyDown += KeyIsDown;
            KeyPress += KeyIsPress;
            KeyUp += KeyIsUp;
            ((System.ComponentModel.ISupportInitialize)character).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer MainTimer;
        private PictureBox character;
        private System.Windows.Forms.Timer SpawnTimer;
    }
}