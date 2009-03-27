using System.Windows;
using System.Windows.Media;

using TetrisGame.Core;
using WpfTetris.Painting;
namespace WpfTetris
{
    
    public class TetrisBox : FrameworkElement
    {
        private Tetris tetris;
        private AutoDropTetris autoTetris;
        IBrushStrategy brushStrategy;


        public TetrisBox()
        {
            TetrisGrid grid=new ByteGrid();
            tetris = new Tetris(new ShapeStrategy(),grid );
            autoTetris = new AutoDropTetris(tetris);
            brushStrategy = new VideoBrushStrategy(grid);

            tetris.ShapeCreated += delegate 
            { 
                InvalidateVisual();
                tetris.CurrentShape.Transformed += delegate { InvalidateVisual(); };
            };
            tetris.Grid.RowsKilled += delegate { InvalidateVisual(); };
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            //base.OnRender(drawingContext);
            double cellWidth = (RenderSize.Width )/ tetris.Grid.Width;
            double cellHeight = (RenderSize.Height )/ tetris.Grid.Height;
            DrawGrid(drawingContext,tetris.Grid,cellWidth, cellHeight);
            if (tetris.CurrentShape != null)
            {
                DrawShape(drawingContext, tetris.CurrentShape, cellWidth, cellHeight);
            }
            Pen pen = new Pen(new SolidColorBrush(Colors.Black), 0.15);
            TetrisPaint.DrawGrid(drawingContext, pen, new Size(32.0, 32.0), new Size(10.0, 20.0), new Point());
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

       
        public void Start()
        {
            autoTetris.Start();
        }

        public void MoveLeft()
        {
            tetris.MoveLeft();
        }

        public void MoveRight()
        {
            tetris.MoveRight();
        }

        public void Drop()
        {
            tetris.DropToBottom();
        }

        public void Rotate()
        {
            tetris.Rotate();
        }
    }
}
