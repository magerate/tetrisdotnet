using System.Windows;
using System.Windows.Media;

using TetrisGame.Core;
using WpfTetris.Painting;
namespace WpfTetris
{

    public class ShapeBox : FrameworkElement
    {
        private TetrisShape shape;
        private IBrushStrategy brushStrategy;

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
                InvalidateVisual();
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            Pen pen = new Pen(Brushes.Black,0.15);
            Rect rect = new Rect(new Point(),RenderSize);
            drawingContext.DrawRectangle(null, pen, rect);
            if (shape != null && brushStrategy != null)
            {
                double cellWidth = RenderSize.Width / 5.0;
                double cellHeight = RenderSize.Height / 5.0;

                double offsetX = (RenderSize.Width - cellWidth * shape.Width) / 2.0;
                double offsetY = (RenderSize.Height - cellHeight * shape.Height) / 2.0;

                Brush brush = brushStrategy.CreateBrush(shape.Index, 
                    cellWidth, cellWidth, offsetX, offsetY);

                TetrisPaint.DrawShape(drawingContext, brush, 
                    shape, cellWidth, cellWidth,offsetX,offsetY);

            }
        }

    }
}
