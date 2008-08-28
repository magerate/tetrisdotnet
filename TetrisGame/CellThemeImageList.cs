using System;
using System.Windows.Forms;
using TetrisGame.Drawing;
using System.IO;
using System.Drawing;

namespace TetrisGame
{
    public class CellThemeImageList : ListView
    {
        private CellTheme theme;
        private ImageList imageList = new ImageList();

        public CellThemeImageList()
        {
            imageList.ImageSize = new Size(64, 64);
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            LargeImageList = imageList;
        }

        public CellTheme CellTheme
        {
            get { return theme; }
            set
            {
                theme = value;
                RefreshTheme();
            }
        }

        public void RefreshTheme()
        {
            imageList.Images.Clear();
            Items.Clear();

            if (theme!=null)
            {
                LoadTheme();
            }
        }

        private void LoadTheme()
        {
            foreach (Stream stream in theme)
            {
                try
                {
                    imageList.Images.Add(new Bitmap(stream));
                }
                catch (ArgumentException)
                {
                    //theme.Remove(stream);
                }
            }

            for (int i = 0; i < imageList.Images.Count; i++)
            {
                ListViewItem lvi = new ListViewItem(string.Empty, i);
                lvi.Tag = theme[i];
                Items.Add(lvi);
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                imageList.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
