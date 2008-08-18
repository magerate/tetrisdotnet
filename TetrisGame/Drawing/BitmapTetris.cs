using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using TetrisGame.Core;

namespace TetrisGame.Drawing
{
    /// <summary>
    /// Draw tetris to a bitmap.
    /// </summary>
    public class BitmapTetris : IDisposable
    {
        public const int BytesPerPixel = 4;
        public const int DefaultCellWidth = 32;
        public const int DefaultCellHeight = 32;
        public const int MinCellSize = 16;
        public const int MaxCellSize = 48;

        public event EventHandler CellSizeChanged;

        private int cellWidth, cellHeight;
        private Bitmap bitmap;
        private Tetris tetris;

        #region "Properties"

        public int Width
        {
            get { return cellWidth * tetris.Width; }
        }

        public int Height
        {
            get { return cellHeight * tetris.Height; }
        }

        public Size Size
        {
            get { return new Size(Width, Height); }
        }

        public Rectangle Bound
        {
            get { return new Rectangle(0, 0, Width, Height); }
        }

        public int CellHeight
        {
            get { return cellHeight; }
            set { this.CellSize = new Size(cellWidth, value); }
        }

        public int CellWidth
        {
            get { return cellWidth; }
            set { this.CellSize = new Size(value, cellHeight); }
        }

        public Size CellSize
        {
            get { return new Size(cellWidth, cellHeight); }
            set
            {
                if (CellSize != value &&
                    value.Width >= MinCellSize && value.Height <= MaxCellSize &&
                    value.Height >= MinCellSize && value.Height <= MaxCellSize)
                {
                    cellWidth = value.Width;
                    cellHeight = value.Height;
                    Dispose(true);
                    OnSizeChanged();
                }
            }
        }

        public Bitmap Bitmap
        {
            get
            {
                if (bitmap == null)
                {
                    bitmap = CreateBitmap();
                }
                return bitmap;
            }
        }

        #endregion


        public BitmapTetris(Tetris tetris)
        {
            cellWidth = DefaultCellWidth;
            cellHeight = DefaultCellHeight;

            this.tetris = tetris;

            tetris.Resize += delegate { Dispose(true); };
        }

        public void Clear()
        {
            if (bitmap != null)
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.Clear(Color.Transparent);
                }
            }
        }

        public void DrawShape(Brush brush, IEnumerable<TetrisPoint> shape)
        {
            DoDrawShape(brush, shape);
        }

        public void EraseShape(IEnumerable<TetrisPoint> shape)
        {
            DoEraseShape(shape);
        }

        public void OffsetRows(int row, int height, int offset)
        {
            DoOffsetRows(row, height, offset);
        }

        public void EraseRows(int row, int height)
        {
            DoEraseRows(row, height);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && bitmap != null)
            {
                bitmap.Dispose();
                bitmap = null;
            }
        }

        protected virtual Bitmap CreateBitmap()
        {
            return new Bitmap(cellWidth * tetris.Width, cellHeight * tetris.Height);
        }

        protected virtual void DoDrawShape(Brush brush, IEnumerable<TetrisPoint> shape)
        {
            using (Graphics g = Graphics.FromImage(Bitmap))
            {
                TetrisPaint.DrawShape(g, brush, shape, cellWidth, cellHeight);
            }
        }

        protected virtual void DoEraseShape(IEnumerable<TetrisPoint> shape)
        {
            IEnumerable<Rectangle> rectangles = TetrisPaint.GetShapeRects(shape, cellWidth, cellHeight);
            foreach (Rectangle rect in rectangles)
            {
                EraseRect(rect);
            }
        }

        protected virtual void DoEraseRows(int row, int height)
        {
            Rectangle rect = GetRowsBound(row, height);
            EraseRect(rect);
        }

        protected virtual void DoOffsetRows(int row, int height, int offset)
        {
            Rectangle rect = GetRowsBound(row, height);
            BitmapData bitmapData = Bitmap.LockBits(rect, ImageLockMode.ReadWrite, Bitmap.PixelFormat);
            unsafe
            {
                uint length = (uint)(bitmapData.Stride * bitmapData.Height);
                void* src = (void*)bitmapData.Scan0;
                void* dst = (void*)((byte*)src + offset * cellHeight * bitmapData.Stride);
                SaveNativeMethods.memcpy(dst, src, length);
            }
            Bitmap.UnlockBits(bitmapData);
        }

        protected virtual void EraseRect(Rectangle rect)
        {
            if (rect.Y >= 0)
            {
                BitmapData bitmapData = Bitmap.LockBits(rect, ImageLockMode.ReadWrite, Bitmap.PixelFormat);
                unsafe
                {
                    byte* p = (byte*)bitmapData.Scan0;
                    for (int j = 0; j < rect.Height; j++)
                    {
                        for (int i = 0; i < rect.Width; i++)
                        {
                            *(int*)(p + i * 4 + j * bitmapData.Stride) = 0;
                        }
                    }
                }
                Bitmap.UnlockBits(bitmapData);
            }
        }

        private void OnSizeChanged()
        {
            if (CellSizeChanged != null)
            {
                CellSizeChanged(this, EventArgs.Empty);
            }
        }

        public Rectangle GetRowsBound(int row, int height)
        {
            return new Rectangle(0, row * cellHeight, tetris.Width * cellWidth, height * cellHeight);
        }

    }
}
