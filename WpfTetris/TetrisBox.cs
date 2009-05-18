using System.Windows;
using System.Windows.Media;

using TetrisGame.Core;
using WpfTetris.Painting;
namespace WpfTetris
{

    public class TetrisBox : FrameworkElement
    {
        private ITetris tetris;

        IBrushStrategy brushStrategy;

        public IBrushStrategy BrushStrategy
        {
            get { return brushStrategy; }
            set { brushStrategy = value; }
        }

        public ITetris Tetris
        {
            get { return tetris; }
            set
            {
                tetris = value;
                HookTetris(tetris.TetrisModel);
            }
        }

        public TetrisBox()
        {
            var grid = new ByteGrid();
            var shapeStrategy = new ShapeStrategy();
            var baseTetris = new Tetris(shapeStrategy, grid);
            tetris = new AutoDropTetris(baseTetris);

            HookTetris(baseTetris);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            Pen pen = new Pen(Brushes.Black, 0.15);
            TetrisPaint.DrawGrid(drawingContext, pen,
                new Size(32.0, 32.0), new Size(10.0, 20.0), new Point());

            //base.OnRender(drawingContext);
            if (tetris != null && brushStrategy != null)
            {
                Tetris baseTetris = tetris.TetrisModel;
                double cellWidth = (RenderSize.Width) / baseTetris.Grid.Width;
                double cellHeight = (RenderSize.Height) / baseTetris.Grid.Height;
                DrawGrid(drawingContext, baseTetris.Grid, cellWidth, cellHeight);
                if (baseTetris.CurrentShape != null)
                {
                    DrawShape(drawingContext, baseTetris.CurrentShape, cellWidth, cellHeight);
                }
            }
        }


        private void DrawShape(DrawingContext dc, TetrisShape shape, double width, double height)
        {
            Brush brush = brushStrategy.CreateBrush(shape.Index, width, height);
            TetrisPaint.DrawShape(dc, brush, shape, width, height);
        }

        private void DrawGrid(DrawingContext dc, TetrisGrid grid, double width, double height)
        {
            ByteGrid byteGrid = grid as ByteGrid;
            if (byteGrid != null)
            {
                for (int i = 1; i <= 7; i++)
                {
                    Brush brush = brushStrategy.CreateBrush(i, width, height);
                    TetrisPaint.DrawShape(dc, brush, byteGrid.GetCells(i), width, height);
                }
            }
        }

        private void HookTetris(Tetris tetris)
        {
            tetris.ShapeCreated += delegate
            {
                InvalidateVisual();
                tetris.CurrentShape.Transformed += delegate { InvalidateVisual(); };
            };
            tetris.Grid.RowsKilled += delegate { InvalidateVisual(); };
        }
    }
}
