
namespace TetrisGame.Core
{
    public struct TetrisPoint
    {
        private int x, y;

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public TetrisPoint(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
