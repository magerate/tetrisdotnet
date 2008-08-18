using System;
using System.Drawing;
using System.Windows.Forms;

using TetrisGame.Core;
using TetrisGame.Drawing;

namespace TetrisGame
{
    public partial class ShapeBox : Control
    {
        private Size cellSize = new Size(24, 24);
        private TetrisShape shape;
        private IBrushStrategy brushStrategy = DefaultBrushStrategy.Instance;

        public IBrushStrategy BrushStrategy
        {
            get { return brushStrategy; }
            set { brushStrategy = value; }
        }

        public TetrisShape Shape
        {
            get { return shape; }
            set
            {
                shape = value;
                Invalidate();
            }
        }
        public ShapeBox()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint, true);
            this.BackColor = Color.Transparent;
            this.DoubleBuffered = true;
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (shape != null)
            {
                int offsetX = (cellSize.Width * 4 - shape.Width * cellSize.Width) / 2;
                int offsetY = (cellSize.Height * 4 - shape.Height * cellSize.Height) / 2;
                using (Brush brush = brushStrategy.CreateBrush(shape.Index, cellSize.Width, cellSize.Height, offsetX, offsetY))
                {
                    TetrisPaint.DrawShape(pe.Graphics, brush, shape, cellSize.Width, cellSize.Height, offsetX, offsetY);
                }
            }
            ControlPaint.DrawBorder(pe.Graphics, ClientRectangle, Color.Gray, ButtonBorderStyle.Solid);
        }

        protected override Size DefaultSize
        {
            get { return new Size(24 * 4, 24 * 4); }
        }
    }
}
