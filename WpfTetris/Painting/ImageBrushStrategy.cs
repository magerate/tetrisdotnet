using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;

namespace WpfTetris.Painting
{
    public class ImageBrushStrategy : IBrushStrategy
    {
        private List<ImageSource> tileImages = new List<ImageSource>();

        public ImageBrushStrategy()
            : this(CellTheme.GetDefaultThemeFileName())
        { }

        public ImageBrushStrategy(string fileName)
            : this(new CellTheme(fileName))
        { }

        public ImageBrushStrategy(CellTheme theme)
        {
            Load(theme);
        }

        public Brush CreateBrush(int index,
                               double cellWidth,
                               double cellHeight,
                               double offsetX,
                               double offsetY)
        {
            ImageSource image = tileImages[index % tileImages.Count];
            ImageBrush brush = new ImageBrush(image);
            TetrisBrushHelper.SetTileBrush(brush, cellWidth, cellHeight, offsetX, offsetY);
            return brush;
        }

        public void Load(CellTheme theme)
        {
            Clear();
            foreach (Stream stream in theme)
            {
                tileImages.Add(BitmapFrame.Create(stream));
            }
        }

        private void Clear()
        {
            if (tileImages.Count > 0)
            {
                tileImages.Clear();
            }
        }
    }
}
