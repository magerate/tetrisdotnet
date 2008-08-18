using System;
using System.Collections;
using System.Collections.Generic;

namespace TetrisGame.Core
{
    //public struct OffsetRows
    //{
    //    private int topRow, height;

    //    public int Height
    //    {
    //        get { return height; }
    //        set { height = value; }
    //    }

    //    public int TopRow
    //    {
    //        get { return topRow; }
    //        set { topRow = value; }
    //    }
    //}

    public class RowsKilled : IEnumerable<int>
    {
        private List<int> killedRows;

        internal RowsKilled()
        {
            killedRows = new List<int>();
        }

        public int BottomRow
        {
            get { return killedRows[0]; }
        }

        public int TopRow
        {
            get { return killedRows[killedRows.Count - 1]; }
        }

        public int Count
        {
            get { return killedRows.Count; }
        }

        public void Add(int row)
        {
            killedRows.Add(row);
        }

        internal void GetOffsetRows(out int topRow, out int height, out int offset)
        {
            height = 0;
            topRow = -1;
            offset = 0;
            for (int i = 0; i < killedRows.Count - 1; i++)
            {
                ++offset;
                if (killedRows[i] != killedRows[i + 1] + 1)
                {
                    topRow = killedRows[i + 1] + 1;
                    height = killedRows[i] - killedRows[i + 1] - 1;
                    return;
                }
            }
        }

        public IEnumerator<int> GetEnumerator()
        {
            return killedRows.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

    }
}
