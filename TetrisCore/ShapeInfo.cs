using System;

namespace TetrisGame.Core
{
    public struct ShapeInfo
    {
        private int index, state;

        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        public int State
        {
            get { return state; }
            set { state = value; }
        }

        public ShapeInfo(int index, int state)
        {
            this.index = index;
            this.state = state;
        }
    }
}
