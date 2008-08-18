using System;
using System.Collections.Generic;

namespace TetrisGame.Core
{
    public class RowsOffsetedEventArgs : RowsClearedEventArgs
    {
        private int offset;

        public RowsOffsetedEventArgs(int row, int height, int offset)
            : base(row, height)
        {
            this.offset = offset;
        }

        public int Offset
        {
            get { return offset; }
        }

    }

    public class RowsKilledEventArgs : EventArgs
    {
        private int killed;
        public RowsKilledEventArgs(int killed)
        {
            this.killed = killed;
        }
        public int Killed
        {
            get { return killed; }
        }
    }

    public class RowsCreatedEventArgs : EventArgs
    {
        private int height;

        public int Height
        {
            get { return height; }
        }

        public RowsCreatedEventArgs(int height)
        {
            this.height = height;
        }

    }

    public class RowsClearedEventArgs : EventArgs
    {
        private int row, height;

        public int Height
        {
            get { return height; }
        }

        public int Row
        {
            get { return row; }
        }

        public RowsClearedEventArgs(int row, int height)
        {
            this.row = row;
            this.height = height;
        }
    }

    public class RowsKillEventArgs : EventArgs
    {
        private RowsKilled killedRows;

        public RowsKilled KilledRows
        {
            get { return killedRows; }
        }

        public RowsKillEventArgs(RowsKilled rows)
        {
            this.killedRows = rows;
        }
    }

    public class ShapeEventArgs : EventArgs
    {
        private TetrisShape shape;

        public TetrisShape Shape
        {
            get { return shape; }
        }

        public ShapeEventArgs(TetrisShape shape)
        {
            this.shape = shape;
        }

    }

    public class RandomShapeCreatedEventArgs : EventArgs
    {
        private int index, state;

        public int State
        {
            get { return state; }
        }

        public int Index
        {
            get { return index; }
        }

        public RandomShapeCreatedEventArgs(int index, int state)
        {
            this.index = index;
            this.state = state;
        }
    }

    public class StatusChangedEventArgs : EventArgs
    {
        private TetrisStatus previousStatus;

        public TetrisStatus PreviousStatus
        {
            get { return previousStatus; }
        }

        public StatusChangedEventArgs(TetrisStatus previousStatus)
        {
            this.previousStatus = previousStatus;
        }
    }

    public class TetrisChangedEventArgs : EventArgs
    {
        private TetrisAction action;

        public TetrisAction Action
        {
            get { return action; }
        }

        public TetrisChangedEventArgs(TetrisAction action)
        {
            this.action = action;
        }
    }


}
