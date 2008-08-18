using System;
using System.Collections;
using System.Collections.Generic;

namespace TetrisGame.Core
{
    public class BitGrid : TetrisGrid
    {
        private BitArray bitArray;

        public BitGrid():base()
        {
            bitArray = new BitArray(Width * Height);
        }

        protected override bool this[int column, int row]
        {
            get { return bitArray[row * Width + column]; }
            set { bitArray[row * Width + column] = value; }
        }

        public override void Fill(TetrisShape shape)
        {
            foreach (TetrisPoint point in shape)
            {
                if (point.Y > 0)
                {
                    this[point.X, point.Y] = true;
                }
            }
        }

        public override void Reset()
        {
            bitArray.SetAll(false);
        }

        protected override void ChangeSize()
        {
            bitArray = new BitArray(Width * Height);
        }

        public override void ClearRows(int row, int height)
        {
            for (int j = row; j < row + height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    this[i, j] = false;
                }
            }
        }


        public override void CreateRandomRows(int count)
        {
            for (int j = Height - count; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    this[i, j] = Convert.ToBoolean(TetrisRandom.Next(2));
                }
            }
        }

        public override void OffsetRows(int row, int height, int offset)
        {
            if (offset > 0)
            {
                for (int j = row + height - 1; j >= row; j--)
                {
                    for (int i = 0; i < Width; i++)
                    {
                        this[i, j + offset] = this[i, j];
                    }
                }
            }

            if (offset < 0)
            {
                for (int j = row; j < row + height; j++)
                {
                    for (int i = 0; i < Width; i++)
                    {
                        this[i, j + offset] = this[i, j];
                    }
                }
            }
        }
    }
}
