using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.IO;

using System.Diagnostics;

namespace TetrisGame.Drawing
{
    public class ImageBrushStrategy : IBrushStrategy, IDisposable
    {
        private List<Image> tileImages = new List<Image>();

        public ImageBrushStrategy(CellTheme theme)
        {
            Load(theme);
        }

        public Brush CreateBrush(int index, int width, int height, int offsetX, int offsetY)
        {            
            Image image = tileImages[index % tileImages.Count];
            TextureBrush brush = new TextureBrush(image, WrapMode.Tile);
            if (offsetX != 0 || offsetY != 0)
            {
                brush.TranslateTransform((float)offsetX, (float)offsetY);
            }
            if (width != image.Width || height != image.Height)
            {
                brush.ScaleTransform((float)width / image.Width, (float)height / image.Height);
            }
            return brush;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void Load(CellTheme theme)
        {
            Clear();
            foreach (Stream stream in theme)
            {
                tileImages.Add(new Bitmap(stream));
            }
        }

        private void Clear()
        {
            if (tileImages.Count > 0)
            {
                Dispose(true);
                tileImages.Clear();
            }
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (Image image in tileImages)
                {
                    image.Dispose();
                }
            }
        }
    }
}
