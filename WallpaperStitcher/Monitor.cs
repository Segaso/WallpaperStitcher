namespace WallpaperSticher {
    using System;
    using System.Drawing;
    
    public class Monitor {
        public Monitor(int Width, int Height) {
            this.Height = Height;
            this.Width = Width;
        }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Resolution { get { return Width.ToString() + "x" + Height.ToString(); } }
        public Bitmap Image { get; set; }

        public void ScaleImage() {
            var ratioX = (double)this.Height / Image.Height;
            var ratioY = (double)this.Height / Image.Width;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(Image.Width * ratio);
            var newHeight = (int)(Image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(this.Image, 0, 0, newWidth, newHeight);
            Image = newImage;
        }
    }
}
