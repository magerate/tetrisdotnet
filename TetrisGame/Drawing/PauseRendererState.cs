using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Threading;

namespace TetrisGame.Drawing
{
    public class PauseRendererState : TetrisRendererState
    {
        public PauseRendererState(BitmapTetris bitmapTetris, ColorMatrix colorMatrix)
            : base(bitmapTetris, colorMatrix)
        {}

        public override void Enter(Control control, Point offset)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    colorMatrix[i, j] = 0.3333333f;
                }
            }
            EnterAnimate(control, offset);
        }

        public override void Leave(Control control, Point offset)
        {
            ResetMatrix();
            TetrisPaint.InvalidateControl(control, bitmapTetris.Bound, offset);
        }

        protected override void EnterAnimate(Control control, Point offset)
        {
            const float step = 0.005f;
            for (int i = 1; i < 11; i++)
            {
                colorMatrix[4, 0] -= i * step;
                colorMatrix[4, 1] -= i * step;
                colorMatrix[4, 2] -= i * step;
                TetrisPaint.InvalidateControl(control, bitmapTetris.Bound, offset);
                control.Update();
                Thread.Sleep(20);
            }
        }
    }
}
