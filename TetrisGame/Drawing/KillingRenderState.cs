using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using TetrisGame.Core;

namespace TetrisGame.Drawing
{
    public class KillingRenderState : TetrisRendererState
    {
        private RowsKilled rowsKilled;

        public RowsKilled RowsKilled
        {
            get { return rowsKilled; }
            set { rowsKilled = value; }
        }

        public KillingRenderState(BitmapTetris bitmapTetris, ColorMatrix colorMatrix)
            : base(bitmapTetris, colorMatrix)
        { }

        protected override void EnterAnimate(Control control, Point offset)
        {
            const int horizontalStep = 8;
            const int verticalStep = 4;

            float opcacitySetp = colorMatrix[3, 3] * horizontalStep / bitmapTetris.Width;

            Rectangle leftRect = new Rectangle(0, 0, horizontalStep, verticalStep);
            Rectangle rightRect = leftRect;

            using (GraphicsPath path = new GraphicsPath())
            {
                Rectangle rect;
                foreach (int row in rowsKilled)
                {
                    rect = bitmapTetris.GetRowsBound(row, 1);
                    rect.Offset(offset);
                    path.AddRectangle(rect);
                }

                using (Region region = new Region(path))
                {
                    for (int x = 0; x < bitmapTetris.Width; x += horizontalStep)
                    {
                        foreach (int row in rowsKilled)
                        {

                            for (int y = 0; y < bitmapTetris.CellHeight; y += verticalStep * 2)
                            {
                                leftRect.X = x;
                                leftRect.Y = row * bitmapTetris.CellHeight + y;
                                ClearRect(leftRect);

                                rightRect.X = bitmapTetris.Width - x - horizontalStep;
                                rightRect.Y = row * bitmapTetris.CellHeight + y + verticalStep;
                                ClearRect(rightRect);


                            }
                        }
                        colorMatrix[3, 3] -= opcacitySetp;
                        control.Invalidate(region);
                        control.Update();
                    }
                }
            }
        }

        private void ClearRect(Rectangle rect)
        {
            Bitmap bitmap = bitmapTetris.Bitmap;
            //for (int x = rect.X; x < rect.Right; x++)
            //{
            //    for (int y = rect.Y; y < rect.Bottom; y++)
            //    {
            //        bitmap.SetPixel(x, y, Color.Transparent);
            //    }
            //}

            BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
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
            bitmap.UnlockBits(bitmapData);
        }

        public override void Leave(Control control, Point offset)
        {
            ResetMatrix();
        }
    }
}
