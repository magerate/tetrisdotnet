using System;
using System.Collections;
using System.Collections.Generic;

namespace TetrisGame.Core
{
    public class TetrisShape : IEnumerable<TetrisPoint>
    {
        private int index;
        private TetrisPoint[] cells;
        private int x, y;
        private int width, height;

        public int Index
        {
            get { return index; }
        }

        public int Height
        {
            get { return height; }
        }

        public int Width
        {
            get { return width; }
        }

        public int Left
        {
            get { return x; }
        }

        public int Top
        {
            get { return y; }
        }

        public int Right
        {
            get { return x + width; }
        }

        public int Bottom
        {
            get { return y + height; }
        }

        public TetrisShape(int index, int state, TetrisPoint[] cells)
        {
            this.index = index;
            this.cells = cells;
            x = y = 0;
            width = cells[0].X;
            height = cells[0].Y;
            for (int i = 1; i < cells.Length; i++)
            {
                width = Math.Max(width, cells[i].X);
                height = Math.Max(height, cells[i].Y);
            }
            ++width;
            ++height;

            Switch(state);
        }

        public void Rotate(bool clockwise)
        {
            if (clockwise)
            {
                for (int i = 0; i < cells.Length; i++)
                {
                    cells[i] = new TetrisPoint(-cells[i].Y + height - 1, cells[i].X);
                }

            }
            else
            {
                for (int i = 0; i < cells.Length; i++)
                {
                    cells[i] = new TetrisPoint(cells[i].Y, -cells[i].X + width - 1);
                }
            }

            int temp = width;
            width = height;
            height = temp;
        }

        public void Offset(int x, int y)
        {
            this.x += x;
            this.y += y;
        }

        public void Move(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public IEnumerable<TetrisPoint> GetRotatedCells(bool clockwise)
        {
            if (clockwise)
            {
                for (int i = 0; i < cells.Length; i++)
                {
                    yield return new TetrisPoint(x + -cells[i].Y + height - 1, y + cells[i].X);
                }

            }
            else
            {
                for (int i = 0; i < cells.Length; i++)
                {
                    yield return new TetrisPoint(x + cells[i].Y, y + -cells[i].X + width - 1);
                }
            }

        }

        private void Rotate180()
        {
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i] = new TetrisPoint(-cells[i].X + width - 1, -cells[i].Y + height - 1);
            }
        }

        private void Switch(int state)
        {
            switch (state % 4)
            {
                case 1:
                    Rotate(true);
                    break;
                case 2:
                    Rotate180();
                    break;
                case 3:
                    Rotate(false);
                    break;
                default:
                    break;
            }
        }

        public IEnumerator<TetrisPoint> GetEnumerator()
        {
            for (int i = 0; i < cells.Length; i++)
            {
                yield return new TetrisPoint(cells[i].X + x, cells[i].Y + y);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
