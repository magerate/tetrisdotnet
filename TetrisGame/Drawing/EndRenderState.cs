using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace TetrisGame.Drawing
{
    public class EndRenderState : PauseRendererState
    {
        public EndRenderState(BitmapTetris bitmapTetris, ColorMatrix colorMatrix)
            : base(bitmapTetris, colorMatrix)
        {}

        protected override void PerformRender(Graphics g, Rectangle rect, Point offset)
        {
            base.PerformRender(g, rect, offset);
            string gameover = "Game Over";
            Font font = new Font("Arial Black", 20.0F, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            SizeF size = g.MeasureString(gameover, font);
            PointF location = new PointF(offset.X + (bitmapTetris.Width - size.Width) / 2, offset.Y + (bitmapTetris.Height - size.Height) / 2);
            g.DrawString(gameover, font, Brushes.BurlyWood, location);
        }

    }
}
