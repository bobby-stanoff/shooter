using shooter.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shooter
{
    internal class Item
    {
        public PictureBox PictureBox { get; private set; }
        public int Value { get; private set; }
        public string Type { get; set; }
        public int Lifetime { get; private set; }
        private System.Windows.Forms.Timer lifetimeTimer;



        public Item(Panel form, string type)
        {
            PictureBox = new PictureBox();
            PictureBox.BackColor = Color.Blue;
            //get image base on type
            string resourceName = type;
            Bitmap bmp = (Bitmap)Properties.Resources.ResourceManager.GetObject(resourceName);
            PictureBox.Image = bmp;
            PictureBox.Size = new Size(20, 20);
            PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox.Tag = "item";

            PictureBox.Left = GetRandomSpawnX(form);
            PictureBox.Top = GetRandomSpawnY(form);
            PictureBox.BringToFront();

            form.Controls.Add(PictureBox);

            Value = 10;
            Type = type;

            Value = 10;
            Lifetime = 15000; // 15 seconds

            lifetimeTimer = new System.Windows.Forms.Timer();
            lifetimeTimer.Interval = Lifetime;
            lifetimeTimer.Tick += LifetimeTimer_Tick;
            lifetimeTimer.Start();
        }

        private int GetRandomSpawnX(Panel form)
        {
            Random rand = new Random();
            return rand.Next(0, form.ClientSize.Width - PictureBox.Width);
        }

        private int GetRandomSpawnY(Panel form)
        {
            Random rand = new Random();
            return rand.Next(0, form.ClientSize.Height - PictureBox.Height);
        }
        private void LifetimeTimer_Tick(object sender, EventArgs e)
        {
            // Time's up, remove the item
            if (!this.PictureBox.Visible)
            {
                return;
            }
            PictureBox.Parent.Controls.Remove(PictureBox);
            PictureBox.Dispose();
            lifetimeTimer.Stop();
        }

    }
}
