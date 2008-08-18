using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using TetrisGame.Core;
using TetrisGame.Drawing;

namespace TetrisGame
{
    public partial class TetrisBox : Control
    {
        private Tetris tetris;
        private TetrisRenderer renderer;


        public TetrisBox()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint, true);
            this.BackColor = Color.Transparent;

            this.tetris = new Tetris();
            BitmapTetris bitmapTetris = new PinnedMemoryBitmapTetris(tetris);
            this.renderer = new TetrisRenderer(this, tetris, bitmapTetris);
        }

        public bool ShowGrid
        {
            get { return renderer.ShowGrid; }
            set { renderer.ShowGrid = value; }
        }

        [Browsable(false)]
        public Tetris Tetris
        {
            get { return tetris; }
        }

        [Browsable(false)]
        public TetrisRenderer Renderer
        {
            get { return renderer; }
        }

        protected override Size DefaultSize
        {
            get
            {
                return new Size(TetrisGrid.DefaultWidth * BitmapTetris.DefaultCellWidth + 1,
                    TetrisGrid.DefaultHeight * BitmapTetris.DefaultCellHeight + 1);
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            renderer.Draw(pe.Graphics, pe.ClipRectangle);
        }

        //private void ChangeSize(object sender, EventArgs e)
        //{
        //    //this.Size = new Size(bitmapTetris.CellWidth * tetris.Width, bitmapTetris.CellHeight * tetris.Height);
        //}
    }
}
