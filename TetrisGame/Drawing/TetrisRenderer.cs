using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Collections.Generic;
using TetrisGame.Core;
using TetrisGame.Drawing;

namespace TetrisGame.Drawing
{
    public class TetrisRenderer : IDisposable
    {
        private Control control;
        private TetrisRendererState rendererState;
        private Tetris tetris;
        private BitmapTetris bitmapTetris;
        private ColorMatrix colorMatrix;
        private IBrushStrategy brushStrategy;

        private Point offset;
        private bool showGrid;

        public bool ShowGrid
        {
            get { return showGrid; }
            set
            {
                if (showGrid != value)
                {
                    showGrid = value;
                    Rectangle rect = bitmapTetris.Bound;
                    rect.Offset(offset);
                    rect.Width++;
                    rect.Height++;
                    control.Invalidate(rect);
                }
            }
        }


        private Dictionary<TetrisStatus, TetrisRendererState> stateTable;

        public TetrisRenderer(Control control, Tetris tetris, BitmapTetris bitmapTetris)
        {
            this.showGrid = true;
            this.offset = Point.Empty;
            this.control = control;
            this.tetris = tetris;
            this.bitmapTetris = bitmapTetris;
            this.colorMatrix = new ColorMatrix();
            this.rendererState = new TetrisRendererState(bitmapTetris, colorMatrix);
            this.brushStrategy = RainbowBrushStrategy.Instance;

            stateTable = new Dictionary<TetrisStatus, TetrisRendererState>();
            stateTable.Add(TetrisStatus.Playing, rendererState);
            stateTable.Add(TetrisStatus.Paused, new PauseRendererState(bitmapTetris, colorMatrix));
            stateTable.Add(TetrisStatus.Lost, new EndRenderState(bitmapTetris, colorMatrix));

            Initialize();
        }

        public float Opacity
        {
            get { return colorMatrix.Matrix33; }
            set
            {
                if (value >= .0f && value <= 1.0f)
                {
                    colorMatrix.Matrix33 = value;
                }
            }
        }

        public IBrushStrategy BrushStrategy
        {
            get { return brushStrategy; }
            set
            {
                IDisposable d = brushStrategy as IDisposable;
                if (d != null)
                {
                    d.Dispose();
                }
                brushStrategy = value;
            }
        }

        public void Dispose()
        {
            bitmapTetris.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Draw(Graphics g)
        {
            Draw(g, bitmapTetris.Bound);
        }

        public void Draw(Graphics g, Rectangle rect)
        {
            rendererState.Render(g, rect, offset);
            if (showGrid)
            {
                TetrisPaint.GetGridRect(ref rect, bitmapTetris.CellSize);
                //using (Pen pen = new Pen(Color.FromArgb(64, 128, 128, 128)))
                //{
                //    TetrisPaint.DrawGrid(g, pen, rect, bitmapTetris.CellSize);
                //}
                TetrisPaint.DrawGrid(g, Pens.Gray, rect, bitmapTetris.CellSize);
            }
        }

        private void SwitchState(TetrisRendererState oldState, TetrisRendererState newState)
        {
            if (!object.ReferenceEquals(oldState, newState))
            {
                rendererState.Leave(control, offset);
                rendererState = newState;
                rendererState.Enter(control, offset);
            }
        }

        private void Initialize()
        {
            tetris.Changed += TetrisChanged;
            tetris.ShapeCreated += ShapeCreated;
            tetris.ShapeChanging += ShapeChanging;
            tetris.ShapeChanged += ShapeChanged;
            tetris.RowsOffseted += RowsOffseted;
            tetris.RowsKilled += RowsKilled;
            tetris.RowsCreated += RowsCreated;
            tetris.RowsCleared += RowsCleared;
            //tetris.Resize += ChangeSize;

            tetris.RowsKilling += delegate(object sender, RowsKillEventArgs e)
            {
                KillingRenderState state = new KillingRenderState(bitmapTetris, colorMatrix);
                state.RowsKilled = e.KilledRows;
                state.Enter(control, offset);
                state.Leave(control, offset);
            };
        }

        private void TetrisChanged(object sender, TetrisChangedEventArgs e)
        {
            TetrisRendererState newState;
            if (stateTable.ContainsKey(tetris.Status))
            {
                newState = stateTable[tetris.Status];
            }
            else
            {
                newState = stateTable[TetrisStatus.Playing];
            }
            SwitchState(rendererState, newState);

            if (e.Action == TetrisAction.Start)
            {
                if (!tetris.Initialized)
                {
                    bitmapTetris.Clear();
                    control.Invalidate();
                }
            }
        }

        private void ShapeCreated(object sender, ShapeEventArgs e)
        {
            using (Brush brush = brushStrategy.CreateBrush(e.Shape.Index, bitmapTetris.CellWidth, bitmapTetris.CellHeight))
            {
                bitmapTetris.DrawShape(brush, e.Shape);
                InvalidateControl(e.Shape);
            }
        }

        private void ShapeChanging(object sender, ShapeEventArgs e)
        {
            bitmapTetris.EraseShape(e.Shape);
            InvalidateControl(e.Shape);
        }

        private void ShapeChanged(object sender, ShapeEventArgs e)
        {
            using (Brush brush = brushStrategy.CreateBrush(e.Shape.Index, bitmapTetris.CellWidth, bitmapTetris.CellHeight))
            {
                bitmapTetris.DrawShape(brush, e.Shape);
                InvalidateControl(e.Shape);
            }
        }

        private void RowsOffseted(object sender, RowsOffsetedEventArgs e)
        {
            bitmapTetris.OffsetRows(e.Row, e.Height, e.Offset);
            InvalidateControl(Math.Min(e.Row, e.Row + e.Offset), e.Height + Math.Abs(e.Offset));
        }

        private void RowsKilled(object sender, RowsKillEventArgs e)
        {
            //int topRow = tetris.Top - e.KilledRows.Count;
            //int height = e.KilledRows.BottomRow - topRow + 1;
            //InvalidateControl(topRow, height);
        }

        private void RowsCreated(object sender, RowsCreatedEventArgs e)
        {
            int index = TetrisRandom.Next();
            using (Brush brush = brushStrategy.CreateBrush(index, bitmapTetris.CellWidth, bitmapTetris.CellHeight))
            {
                bitmapTetris.DrawShape(brush, tetris.GetFilledCells(e.Height));
                InvalidateControl(tetris.Height - e.Height, e.Height);
            }
        }

        private void RowsCleared(object sender, RowsClearedEventArgs e)
        {
            bitmapTetris.EraseRows(e.Row, e.Height);
            InvalidateControl(e.Row, e.Height);
        }

        private void InvalidateControl(TetrisShape shape)
        {
            Rectangle rect = TetrisPaint.GetShapeBound(shape, bitmapTetris.CellWidth, bitmapTetris.CellHeight);
            TetrisPaint.InvalidateControl(control, rect, offset);
        }

        private void InvalidateControl(int topRow, int height)
        {
            Rectangle rect = bitmapTetris.GetRowsBound(topRow, height);
            TetrisPaint.InvalidateControl(control, rect, offset);
        }

    }
}
