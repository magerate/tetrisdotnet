using System.Drawing;
using System.Drawing.Imaging;
using TetrisGame.Core;

namespace TetrisGame.Drawing
{
    public class PinnedMemoryBitmapTetris : BitmapTetris
    {
        private PinnedMemoryBlock buffer;
        public PinnedMemoryBitmapTetris(Tetris tetris)
            : base(tetris)
        {
            buffer = new PinnedMemoryBlock();
        }
        protected override Bitmap CreateBitmap()
        {
            return new Bitmap(Width, Height, Stride, PixelFormat.Format32bppArgb, buffer.IntPtr);
        }

        protected override void EraseRect(Rectangle rect)
        {
            int index = rect.Y * Stride + rect.X * BytesPerPixel;
            int length = rect.Width * BytesPerPixel;
            for (int i = rect.Y; i < rect.Bottom; i++, index += Stride)
            {
                buffer.Clear(index, length);
            }
        }

        protected override void DoEraseRows(int row, int height)
        {
            int stride = CellHeight * Stride;
            buffer.Clear(stride * row, stride * height);
        }

        protected override void DoOffsetRows(int row, int height, int offset)
        {
            int stride = CellHeight * Stride;
            buffer.Offset(row * stride, height * stride, offset * stride);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && buffer != null)
            {
                buffer.Dispose();
            }
            base.Dispose(disposing);
        }

        private int Stride { get { return Width * BytesPerPixel; } }
    }
}
