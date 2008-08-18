using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TetrisGame.Core
{
    public class Tetris : INotifyPropertyChanged, ITetris
    {
        private bool initialized;
        private bool clockwise;
        private int score;
        private int shapeCount;
        private int initialHeight;
        private int currentHeight;
        private int killedRows;

        private TetrisStatus status;

        private TetrisShape shape;
        private TetrisGrid grid;
        private IShapeStrategy shapeStrategy;

        #region "Properties

        public TetrisStatus Status
        {
            get { return status; }
            private set
            {
                if (value != status)
                {
                    status = value;
                    OnPropertyChanged("Status");
                }
            }
        }
        public bool Clockwise
        {
            get { return clockwise; }
            set { clockwise = value; }
        }

        public int Score
        {
            get { return score; }
            private set
            {
                score = value;
                OnPropertyChanged("Score");
            }
        }

        public int KilledRows
        {
            get { return killedRows; }
            private set
            {
                killedRows = value;
                OnPropertyChanged("KilledRows");
            }
        }

        public int ShapeCount
        {
            get { return shapeCount; }
            private set
            {
                shapeCount = value;
                OnPropertyChanged("ShapeCount");
            }
        }

        public int Top
        {
            get { return Height - currentHeight; }
        }

        public int CurrentHeight
        {
            get { return currentHeight; }
            private set
            {
                currentHeight = value;
                OnPropertyChanged("CurrentHeight");
            }
        }

        public int Width
        {
            get { return grid.Width; }
            set
            {
                if (Width != value && value >= 10 && value <= 15)
                {
                    grid.Width = value;
                }
            }
        }

        public int Height
        {
            get { return grid.Height; }
            set
            {
                if (Height != value && value >= 15 && value <= 25)
                {
                    grid.Height = value;
                }
            }
        }

        public int InitialHeight
        {
            get { return initialHeight; }
            set
            {
                if (initialHeight != value && value >= 0 && value < 12)
                {
                    if (currentHeight > 0)
                    {
                        OnRowsCleared(Top, currentHeight);
                    }
                    initialHeight = value;
                    CreateRowsAndNotify(initialHeight);
                }
            }
        }

        public bool Initialized
        {
            get { return initialized; }
        }

        public IShapeStrategy ShapeStrategy
        {
            get { return shapeStrategy; }
            set { shapeStrategy = value; }
        }
        #endregion

        #region "Events"

        public event EventHandler<ShapeEventArgs> ShapeCreated;
        public event EventHandler<ShapeEventArgs> ShapeChanging;
        public event EventHandler<ShapeEventArgs> ShapeChanged;

        public event EventHandler<RowsCreatedEventArgs> RowsCreated;
        public event EventHandler<RowsOffsetedEventArgs> RowsOffseted;
        public event EventHandler<RowsKillEventArgs> RowsKilling;
        public event EventHandler<RowsKillEventArgs> RowsKilled;
        public event EventHandler<RowsClearedEventArgs> RowsCleared;

        public event EventHandler<TetrisChangedEventArgs> Changed;
        public event EventHandler Resize;
        public event EventHandler Bumped;
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnChanged(TetrisAction action)
        {
            if (Changed != null)
            {
                Changed(this, new TetrisChangedEventArgs(action));
            }
        }

        private void OnResize(EventArgs e)
        {
            if (Resize != null)
            {
                Resize(this, e);
            }
        }

        private void OnShapeCreated(ShapeEventArgs e)
        {
            if (ShapeCreated != null)
            {
                ShapeCreated(this, e);
            }

        }

        private void OnShapeChanging(ShapeEventArgs e)
        {
            if (ShapeChanging != null)
            {
                ShapeChanging(this, e);
            }
        }

        private void OnShapeChanged(ShapeEventArgs e)
        {
            if (ShapeChanged != null)
            {
                ShapeChanged(this, e);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void OnRowsCreated(int height)
        {
            if (RowsCreated != null)
            {
                RowsCreated(this, new RowsCreatedEventArgs(height));
            }
        }

        private void OnRowsOffseted(int row, int height, int offset)
        {
            if (RowsOffseted != null)
            {
                RowsOffsetedEventArgs e = new RowsOffsetedEventArgs(row, height, offset);
                RowsOffseted(this, e);
            }
        }

        private void OnRowsKilled(RowsKillEventArgs e)
        {
            if (RowsKilled != null)
            {
                RowsKilled(this, e);
            }
        }

        private void OnRowsKilling(RowsKillEventArgs e)
        {
            if (RowsKilling != null)
            {
                RowsKilling(this, e);
            }
        }

        private void OnRowsCleared(int row, int height)
        {
            if (RowsCleared != null)
            {
                RowsClearedEventArgs e = new RowsClearedEventArgs(row, height);
                RowsCleared(this, e);
            }
        }

        private void OnBumped(EventArgs e)
        {
            if (Bumped != null)
            {
                Bumped(this, e);
            }
        }

        public Tetris TetrisModel { get { return this; } }

        #endregion

        public Tetris(IShapeStrategy shapeStrategy)
        {
            this.shapeStrategy = shapeStrategy;
            grid = new BitGrid();
            initialized = true;
            clockwise = true;
            status = TetrisStatus.Waiting;
        }

        public Tetris() : this(new ShapeStrategy()) { }

        public IEnumerable<TetrisPoint> GetFilledCells(int height)
        {
            return grid.GetFilledCells(height);
        }

        public void Start()
        {
            this.Status = TetrisStatus.Playing;
            OnChanged(TetrisAction.Start);
            Initialize();
            initialized = false;
            CreateShape();
        }

        public void DropToBottom()
        {
            if (status == TetrisStatus.Playing)
            {
                int offset = 0;
                while (shape.Bottom + offset < grid.Height && !grid.IsBlock(shape, 0, offset + 1))
                {
                    ++offset;
                }
                if (offset > 0)
                {
                    OffsetShape(0, offset, TetrisAction.DropToBottom);
                }
                Bump();
            }
        }

        public void MoveLeft()
        {
            if (status == TetrisStatus.Playing)
            {
                if (shape.Left > 0 && !grid.IsBlock(shape, -1, 0))
                {
                    OffsetShape(-1, 0, TetrisAction.MoveLeft);
                }

            }
        }

        public void MoveRight()
        {
            if (status == TetrisStatus.Playing)
            {
                if (shape.Right < grid.Width && !grid.IsBlock(shape, 1, 0))
                {
                    OffsetShape(1, 0, TetrisAction.MoveRight);
                }
            }
        }

        public void Drop()
        {
            if (TetrisStatus.Playing == status)
            {
                if (shape.Bottom < grid.Height && !grid.IsBlock(shape, 0, 1))
                {
                    OffsetShape(0, 1, TetrisAction.Drop);
                }
                else
                {
                    Bump();
                }
            }
        }

        public void Rotate()
        {
            if (TetrisStatus.Playing == status)
            {
                if (shape.Left + shape.Height <= grid.Width && shape.Top + shape.Width <= grid.Height &&
                    !grid.IsBlock(shape.GetRotatedCells(clockwise), 0, 0))
                {
                    OnShapeChanging(new ShapeEventArgs(shape));
                    shape.Rotate(clockwise);
                    OnShapeChanged(new ShapeEventArgs(shape));
                    OnChanged(TetrisAction.Rotate);
                }
            }
        }

        public void Add(int count)
        {
            int overflow = 0;

            if (count > 0 && count < 5)
            {
                if (currentHeight > 0)
                {
                    if (currentHeight + count > Height)
                    {
                        overflow = currentHeight + count - Height;
                    }
                    grid.OffsetRows(Top + overflow, currentHeight - overflow, -count);
                }
                grid.CreateRandomRows(count);
                AdjustShape();

                if (currentHeight > 0)
                {
                    OnRowsOffseted(Top + overflow, currentHeight - overflow, -count);
                }
                OnRowsCleared(Height - count, count);
                OnRowsCreated(count);

                this.CurrentHeight = Math.Min(Height, currentHeight + count);
            }
            if (overflow > 0)
            {
                End();
            }
        }

        private void AdjustShape()
        {
            int offset = 0;
            while (grid.IsBlock(shape, 0, offset))
            {
                offset--;
            }

            if (offset < 0)
            {
                OffsetShape(0, offset, TetrisAction.Drop);
            }

            if (shape.Top < 0)
            {
                End();
            }
        }


        public void End(bool lost)
        {
            if (status == TetrisStatus.Playing || status == TetrisStatus.Paused)
            {
                if (lost)
                {
                    this.Status = TetrisStatus.Lost;
                }
                else
                {
                    this.Status = TetrisStatus.Won;
                }
                shapeStrategy.Reset();
                OnChanged(TetrisAction.End);
            }
        }

        public void PauseOrResume()
        {
            if (status == TetrisStatus.Playing)
            {
                this.Status = TetrisStatus.Paused;
                OnChanged(TetrisAction.Pause);
            }
            else if (status == TetrisStatus.Paused)
            {
                this.Status = TetrisStatus.Playing;
                OnChanged(TetrisAction.Resume);
            }
        }

        private void End()
        {
            End(true);
        }

        private void CreateShape()
        {
            shape = shapeStrategy.Create();
            shape.Move((grid.Width - shape.Width) / 2, 0);
            this.ShapeCount++;
            OnShapeCreated(new ShapeEventArgs(shape));
            if (grid.IsBlock(shape, 0, 0))
            {
                End();
            }
        }

        private void Bump()
        {
            OnBumped(EventArgs.Empty);
            grid.Fill(shape);
            this.CurrentHeight = Math.Max(currentHeight, Height - shape.Top);
            Kill();
            CreateShape();
        }

        private void Initialize()
        {
            this.ShapeCount = 0;
            this.Score = 0;
            this.KilledRows = 0;

            this.CurrentHeight = initialHeight;

            if (!initialized)
            {
                grid.Reset();
                CreateRowsAndNotify(initialHeight);
            }
        }

        private void OffsetShape(int x, int y, TetrisAction action)
        {
            OnShapeChanging(new ShapeEventArgs(shape));
            shape.Offset(x, y);
            OnShapeChanged(new ShapeEventArgs(shape));
            OnChanged(action);
        }

        private void Kill()
        {
            RowsKilled rows = new RowsKilled();
            for (int row = shape.Bottom - 1; row >= shape.Top; row--)
            {
                if (grid.IsFull(row))
                {
                    rows.Add(row);
                }
            }

            if (rows.Count > 0)
            {
                RowsKillEventArgs e = new RowsKillEventArgs(rows);
                OnRowsKilling(e);

                int topRow, height, offset;
                rows.GetOffsetRows(out topRow, out height, out offset);
                if (height > 0)
                {
                    OffsetRowsAndNotify(topRow, height, offset);
                }
                OffsetRowsAndNotify(Top, rows.TopRow - Top, rows.Count);

                ClearRowsAndNotify(Top, rows.Count);

                this.CurrentHeight -= rows.Count;
                this.KilledRows += rows.Count;
                this.Score += rows.Count * rows.Count * 100;

                OnRowsKilled(e);
            }
        }

        private void CreateRowsAndNotify(int height)
        {
            if (height > 0)
            {
                grid.CreateRandomRows(height);
                OnRowsCreated(height);
            }
        }

        private void ClearRowsAndNotify(int row, int height)
        {
            grid.ClearRows(row, height);
            OnRowsCleared(row, height);
        }

        private void OffsetRowsAndNotify(int row, int height, int offset)
        {
            grid.OffsetRows(row, height, offset);
            OnRowsOffseted(row, height, offset);
        }
    }
}
