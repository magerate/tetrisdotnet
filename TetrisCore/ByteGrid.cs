using System;
using System.Collections;
using System.Collections.Generic;

namespace TetrisGame.Core
{
    public class ByteGrid:TetrisGrid
    {
        private byte[] gridBytes;

        public ByteGrid()
            : base()
        {
            gridBytes = new byte[Width * Height];
        }

        protected override bool GetCell(int column, int row)
        {
            return gridBytes[row * Width + column] != 0;
        }

        protected override void SetCell(int column, int row, TetrisShape shape)
        {
            SetCell(column, row, (byte)shape.Index);
        }

        private void SetCell(int column, int row, byte value)
        {
            gridBytes[row * Width + column] = value;
        }

        protected override void OnClear(EventArgs e)
        {
            Buffer.SetByte(gridBytes, 0, 0);
            base.OnClear(e);
        }
     
        protected override void ChangeSize()
        {
            gridBytes = new byte[Width * Height];
        }

        protected override void OnClearRows(int row, int span)
        {
            Array.Clear(gridBytes, row * Width, span * Width);
            base.OnClearRows(row, span);
        }

        protected override void OnCreateRows(int count)
        {
            for (int j = Height - count; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    SetCell(i, j, (byte)TetrisRandom.Next(2));
                }
            }
            base.OnCreateRows(count);
        }

        protected override void OnOffsetRows(int row, int span, int offset)
        {
            Buffer.BlockCopy(gridBytes, row * Width, gridBytes, (row + offset) * Width, span * Width);
            base.OnOffsetRows(row, span, offset);
        }

        public IEnumerable<TetrisPoint> GetCells(int value)
        {
            for (int i = 0; i < gridBytes.Length; i++)
            {
                if (gridBytes[i] == value)
                {
                    int x = i % Width;
                    int y = i / Width;
                    yield return new TetrisPoint(x, y);
                }
            }
        }
    }
}
