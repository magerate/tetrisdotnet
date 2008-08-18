using System;
using System.Collections.Generic;

namespace TetrisGame.Core
{
    public abstract class TetrisGrid
    {
        public const int DefaultWidth = 10;
        public const int DefaultHeight = 20;

        private int width, height;

        protected TetrisGrid()
        {
            width = DefaultWidth;
            height = DefaultHeight;
        }

        public int Height
        {
            get { return height; }
            set
            {
                height = value;
                ChangeSize();
            }
        }

        public int Width
        {
            get { return width; }
            set
            {
                width = value;
                ChangeSize();
            }
        }


        protected abstract bool this[int column, int rows] { get; set; }
        protected abstract void ChangeSize();

        public abstract void CreateRandomRows(int count);
        public abstract void Reset();
        public abstract void Fill(TetrisShape shape);
        public abstract void OffsetRows(int row, int height, int offset);
        public abstract void ClearRows(int row, int height);

        public bool IsBlock(IEnumerable<TetrisPoint> shape, int x, int y)
        {
            foreach (TetrisPoint point in shape)
            {
                if (point.Y + y > 0 && this[point.X + x, point.Y + y])
                {
                    return true;
                }
            }
            return false;
        }


        public IEnumerable<TetrisPoint> GetFilledCells(int randomRowHeight)
        {
            for (int j = this.height - randomRowHeight; j < this.height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    if (this[i, j])
                    {
                        yield return new TetrisPoint(i, j);
                    }
                }
            }
        }

        public bool IsFull(int row)
        {
            for (int column = 0; column < width; column++)
            {
                if (!this[column, row])
                    return false;
            }
            return true;
        }
    }
}
