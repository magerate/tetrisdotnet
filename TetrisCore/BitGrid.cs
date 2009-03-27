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

        protected override bool GetCell(int column, int row)
        {
            return bitArray[row * Width + column];
        }

        protected override void SetCell(int column, int row, TetrisShape shape)
        {
            SetCell(column, row, true);
        }

        private void SetCell(int column, int row, bool value)
        {
            bitArray[row * Width + column] = value;
        }

        protected override void OnClear(EventArgs e)
        {
            bitArray.SetAll(false);
            base.OnClear(e);
        }

        protected override void ChangeSize()
        {
            bitArray = new BitArray(Width * Height);
        }

        protected override void OnClearRows(int row, int span)
        {
            for (int j = row; j < row + span; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    SetCell(i, j,false);
                }
            }
            base.OnClearRows(row, span);
        }


        protected override void OnCreateRows(int count)
        {
            for (int j = Height - count; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    SetCell(i, j,Convert.ToBoolean(TetrisRandom.Next(2)));
                }
            }
            base.OnCreateRows(count);
        }

        protected override void OnOffsetRows(int row, int span, int offset)
        {
            if (offset > 0)
            {
                for (int j = row + span - 1; j >= row; j--)
                {
                    for (int i = 0; i < Width; i++)
                    {
                        SetCell(i, j + offset,GetCell(i, j));
                    }
                }
            }

            if (offset < 0)
            {
                for (int j = row; j < row + span; j++)
                {
                    for (int i = 0; i < Width; i++)
                    {
                        SetCell(i, j + offset,GetCell(i, j));
                    }
                }
            }

            base.OnOffsetRows(row, span, offset);
        }
    }
}
