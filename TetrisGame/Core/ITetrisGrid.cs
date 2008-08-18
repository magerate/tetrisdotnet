using System;
using System.Collections.Generic;

namespace TetrisGame.Core
{
    public interface ITetrisGrid
    {
        int Width { get; set; }
        int Height { get; set; }

        void Fill(TetrisShape shape);
        void OffsetRows(int row, int height, int offset);
        void ClearRows(int row, int height);
        void CreateRandomRows(int count);
        void Reset();
        bool IsBlock(IEnumerable<TetrisPoint> shape, int x, int y);
        bool IsFull(
    }
}
