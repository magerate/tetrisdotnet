using System;
using System.Collections.Generic;

namespace TetrisGame.Core
{
    public class RowsOffsetedEventArgs : RowsClearedEventArgs
    {
        private int offset;

        public RowsOffsetedEventArgs(int row, int span, int offset)
            : base(row, span)
        {
            this.offset = offset;
        }

        public int Offset
        {
            get { return offset; }
        }

    }

    //public class RowsKilledEventArgs : EventArgs
    //{
    //    private int killed;
    //    public RowsKilledEventArgs(int killed)
    //    {
    //        this.killed = killed;
    //    }
    //    public int Killed
    //    {
    //        get { return killed; }
    //    }
    //}

    public class RowsCreatedEventArgs : EventArgs
    {
        private int count;

        public int Count
        {
            get { return count; }
        }

        public RowsCreatedEventArgs(int count)
        {
            this.count = count;
        }

    }

    public class RowsClearedEventArgs : EventArgs
    {
        private int row, span;

        public int Span
        {
            get { return span; }
        }

        public int Row
        {
            get { return row; }
        }

        public RowsClearedEventArgs(int row, int span)
        {
            this.row = row;
            this.span = span;
        }
    }

    public class RowsKillEventArgs : EventArgs
    {
        private KilledRows killedRows;

        public KilledRows KilledRows
        {
            get { return killedRows; }
        }

        public RowsKillEventArgs(KilledRows rows)
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

    //public class StatusChangedEventArgs : EventArgs
    //{
    //    private TetrisStatus previousStatus;

    //    public TetrisStatus PreviousStatus
    //    {
    //        get { return previousStatus; }
    //    }

    //    public StatusChangedEventArgs(TetrisStatus previousStatus)
    //    {
    //        this.previousStatus = previousStatus;
    //    }
    //}

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

    public class TransformEventArgs : EventArgs
    {
        private TetrisAction action;

        public TetrisAction Action
        {
            get { return action; }
        }

        public TransformEventArgs(TetrisAction action)
        {
            this.action = action;
        }
    }
}
