using System;
using System.Collections.Generic;

namespace TetrisGame.Core
{
    public abstract class TetrisGrid
    {
        public const int DefaultWidth = 10;
        public const int DefaultHeight = 20;
        public const int MaxWidth = 15;
        public const int MinWidth = 5;
        public const int MaxHeight = 25;
        public const int MinHeight = 15;
        public const int MaxInitialHeight = 12;

        private int currentHeight;
        private int initialHeight;
        private int width, height;
        private bool initialized;

        public event EventHandler<RowsCreatedEventArgs> RowsCreated;
        public event EventHandler<RowsOffsetedEventArgs> RowsOffseted;
        public event EventHandler<RowsClearedEventArgs> RowsCleared;
        public event EventHandler<RowsKillEventArgs> RowsKilling;
        public event EventHandler<RowsKillEventArgs> RowsKilled;

        public event EventHandler<EventArgs> Cleared;


        protected TetrisGrid()
        {
            width = DefaultWidth;
            height = DefaultHeight;
            initialHeight = 0;
            currentHeight = 0;
            initialized = true;
        }

        public int CurrentHeight
        {
            get { return currentHeight; }
            private set
            {
                if (currentHeight != value)
                {
                    currentHeight = value;
                    //raise the event
                }
            }
        }

        public int Top
        {
            get { return Height - CurrentHeight; }
        }

        public int InitialHeight
        {
            get { return initialHeight; }
            set
            {
                if (initialHeight != value && value >= 0 && value < MaxInitialHeight)
                {
                    if (currentHeight > 0)
                    {
                        OnClearRows(Top, currentHeight);
                    }
                    initialHeight = value;
                    OnCreateRows(initialHeight);
                    CurrentHeight = initialHeight;
                    initialized = true;
                }
            }
        }

        public int Height
        {
            get { return height; }
            set
            {
                if (value != height && value >= MinHeight && value <= MaxHeight)
                {
                    height = value;
                    ChangeSize();
                }
            }
        }

        public int Width
        {
            get { return width; }
            set
            {
                if (value != width && value >= MinWidth && value <= MaxWidth)
                {
                    width = value;
                    ChangeSize();
                }
            }
        }


        public bool this[int column, int row]
        {
            get { return GetCell(column, row); }
        }

        protected virtual void OnCreateRows(int count)
        {
            if (RowsCreated != null)
            {
                RowsCreated(this, new RowsCreatedEventArgs(count));
            }
        }

        protected virtual void OnOffsetRows(int row, int span, int offset)
        {
            if (RowsOffseted != null)
            {
                RowsOffsetedEventArgs e = new RowsOffsetedEventArgs(row, span, offset);
                RowsOffseted(this, e);
            }
        }

        protected virtual void OnClearRows(int row, int span)
        {
            if (RowsCleared != null)
            {
                RowsClearedEventArgs e = new RowsClearedEventArgs(row, span);
                RowsCleared(this, e);
            }
        }

        protected virtual void OnRowsKilled(RowsKillEventArgs e)
        {
            if (RowsKilled != null)
            {
                RowsKilled(this, e);
            }
        }

        protected virtual void OnRowsKilling(RowsKillEventArgs e)
        {
            if (RowsKilling != null)
            {
                RowsKilling(this, e);
            }
        }

        protected virtual void OnClear(EventArgs e)
        {
            if (Cleared != null)
            {
                Cleared(this, e);
            }
        }

        protected abstract bool GetCell(int column, int row);
        protected abstract void SetCell(int column, int row, TetrisShape shape);
        protected abstract void ChangeSize();

        public void Reset()
        {
            if (!initialized)
            {
                OnClear(EventArgs.Empty);
                CurrentHeight = InitialHeight;
                if (CurrentHeight > 0)
                {
                    OnCreateRows(CurrentHeight);
                }
            }
            initialized = false;
        }

        public void Fill(TetrisShape shape)
        {
            foreach (TetrisPoint point in shape)
            {
                if (point.Y > 0)
                {
                    SetCell(point.X, point.Y, shape);
                }
            }
            CurrentHeight = Math.Max(CurrentHeight, Height - shape.Top);
            Kill(shape);
        }

        public int Add(int count)
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
                    OnOffsetRows(Top + overflow, currentHeight - overflow, -count);
                }
                OnCreateRows(count);

                if (currentHeight > 0)
                {
                    OnOffsetRows(Top + overflow, currentHeight - overflow, -count);
                }
                OnClearRows(Height - count, count);
                OnCreateRows(count);

                CurrentHeight = Math.Min(Height, CurrentHeight + count);
            }
            return overflow;
        }

        private bool IsFull(int row)
        {
            for (int column = 0; column < width; column++)
            {
                if (!this[column, row])
                    return false;
            }
            return true;
        }

        private void Kill(TetrisShape shape)
        {
            KilledRows rows = new KilledRows();
            for (int row = shape.Bottom - 1; row >= shape.Top; row--)
            {
                if (IsFull(row))
                {
                    rows.Add(row);
                }
            }

            if (rows.Count > 0)
            {
                RowsKillEventArgs e = new RowsKillEventArgs(rows);
                OnRowsKilling(e);

                int topRow, span, offset;
                rows.GetOffsetRows(out topRow, out span, out offset);
                if (span > 0)
                {
                    OnOffsetRows(topRow, span, offset);
                }
                OnOffsetRows(Top, rows.TopRow - Top, rows.Count);
                OnClearRows(Top, rows.Count);
                CurrentHeight -= rows.Count;
                OnRowsKilled(e);
            }
        }
    }
}
