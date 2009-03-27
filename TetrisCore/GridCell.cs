using System;

namespace TetrisGame.Core
{
    public struct GridCell
    {
        private int value, column, row;
        public int Row
        {
            get { return row; }
            set { row = value; }
        }
        public int Column
        {
            get { return column; }
            set { column = value; }
        }
        public int Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public GridCell(int column,int row,int value)
        {
            this.column = column;
            this.row = row;
            this.value=value;
        }
    }
}
