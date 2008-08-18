using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace TetrisGame.Drawing
{
    public class TetrisRendererState
    {
        protected BitmapTetris bitmapTetris;
        protected ColorMatrix colorMatrix;

        public TetrisRendererState(BitmapTetris bitmapTetris, ColorMatrix colorMatrix)
        {
            this.bitmapTetris = bitmapTetris;
            this.colorMatrix = colorMatrix;
        }

        public virtual void Enter(Control control, Point offset)
        {
            EnterAnimate(control, offset);
        }

        public virtual void Leave(Control control, Point offset)
        {
            LeaveAnimate(control, offset);
        }

        public void Render(Graphics g, Rectangle rect, Point offset)
        {
            if (g != null)
            {
                PerformRender(g, rect, offset);
            }
        }

        protected virtual void PerformRender(Graphics g, Rectangle rect, Point offset)
        {
            using (ImageAttributes ia = new ImageAttributes())
            {
                Image image = bitmapTetris.Bitmap;
                ia.SetColorMatrix(colorMatrix);
                Rectangle destRect = new Rectangle(rect.X + offset.X, rect.Y + offset.Y, rect.Width, rect.Height);
                g.DrawImage(image, destRect, rect.X, rect.Y, rect.Width, rect.Height, GraphicsUnit.Pixel, ia);
            }
        }

        protected void ResetMatrix()
        {
            colorMatrix[0, 0] = 1.0f;
            colorMatrix[0, 1] = 0.0f;
            colorMatrix[0, 2] = 0.0f;

            colorMatrix[1, 0] = 0.0f;
            colorMatrix[1, 1] = 1.0f;
            colorMatrix[1, 2] = 0.0f;

            colorMatrix[2, 0] = 0.0f;
            colorMatrix[2, 1] = 0.0f;
            colorMatrix[2, 2] = 1.0f;

            colorMatrix[3, 3] = 1.0f;

            colorMatrix[4, 0] = 0.0f;
            colorMatrix[4, 1] = 0.0f;
            colorMatrix[4, 2] = 0.0f;
        }

        protected virtual void EnterAnimate(Control control, Point offset) { }
        protected virtual void LeaveAnimate(Control control, Point offset) { }
    }
}
