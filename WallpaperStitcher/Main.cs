namespace WallpaperSticher {
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    public partial class Main : Form {
        private readonly List<Monitor> Monitors = new List<Monitor>();
        public Main() {
            InitializeComponent();

            foreach (var Monitor in Screen.AllScreens) {
                Monitors.Add(new Monitor(Monitor.Bounds.Width,Monitor.Bounds.Height));
            }

            gbMonitor1.Text += " " + Monitors[0].Resolution;
            gbMonitor2.Text += " " + Monitors[1].Resolution;
        }

        private void btnBrowseMonitor1_Click(object sender, EventArgs e) {
            var Result = OpenFileDialog.ShowDialog();
            if (Result == DialogResult.OK) {
                txtWallpaperPath1.Text = OpenFileDialog.FileName;
                pbMonitor1.Image = Image.FromFile(OpenFileDialog.FileName);
                Monitors[0].Image = new Bitmap(OpenFileDialog.FileName);
                //Monitors[0].ScaleImage(); Not sure if I have to do this
            }
        }

        private void btnBrowseMonitor2_Click(object sender, EventArgs e) {
            var Result = OpenFileDialog.ShowDialog();
            if (Result == DialogResult.OK) {
                txtWallpaperPath2.Text = OpenFileDialog.FileName;
                pbMonitor2.Image = Image.FromFile(OpenFileDialog.FileName);

                Monitors[1].Image = new Bitmap(OpenFileDialog.FileName);
                //Monitors[1].ScaleImage(); Not sure if I have to do this
            }
        }

        private void btnSaveWallpaper_Click(object sender, EventArgs e) {
            var Result = SaveFileDialog.ShowDialog();
            if (Result == DialogResult.OK) {
                int Width, Height;

                Width = Monitors.Select(M => M.Width).Sum();
                Height = Monitors.Select(M => M.Height).Max();

                GenerateWallpaper(Width, Height);
            }
        }

        private void GenerateWallpaper(int Width, int Height) {
            using (var FinalImage = new Bitmap(Width, Height)) {
                using (var Wallpaper = Graphics.FromImage(FinalImage)) {
                    Wallpaper.Clear(Color.Black);

                    int Offset = 0;
                    foreach (var Monitor in Monitors) {
                        Wallpaper.DrawImage(Monitor.Image, new Rectangle(Offset, 0, Monitor.Width, Monitor.Height));
                        Offset += Monitor.Width;
                    }

                    FinalImage.Save(SaveFileDialog.FileName);

                }
            }
        }
    }
}
