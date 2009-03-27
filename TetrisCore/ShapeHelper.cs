using System;
using System.Diagnostics;

namespace TetrisGame.Core
{
    public static class ShapeHelper
    {
        public static TetrisPoint[] CreateCells(int index)
        {
            switch (index)
            {
                case 1:
                    return new TetrisPoint[4]{new TetrisPoint(0,0),new TetrisPoint(1,0),
                        new TetrisPoint(1,1),new TetrisPoint(0,1)};
                case 2:
                    return new TetrisPoint[4]{new TetrisPoint(0,0),new TetrisPoint(1,0),
                        new TetrisPoint(2,0),new TetrisPoint(3,0)};
                case 3:
                     return new TetrisPoint[4]{new TetrisPoint(0,0),new TetrisPoint(1,0),
                        new TetrisPoint(1,1),new TetrisPoint(2,1)};
                case 4:
                    return new TetrisPoint[4]{new TetrisPoint(1,0),new TetrisPoint(2,0),
                        new TetrisPoint(0,1),new TetrisPoint(1,1)};
                case 5:
                    return new TetrisPoint[4]{new TetrisPoint(0,0),new TetrisPoint(0,1),
                        new TetrisPoint(1,1),new TetrisPoint(2,1)};
                case 6:
                    return new TetrisPoint[4]{new TetrisPoint(2,0),new TetrisPoint(0,1),
                        new TetrisPoint(1,1),new TetrisPoint(2,1)};
                case 7:
                    return new TetrisPoint[4]{new TetrisPoint(1,0),new TetrisPoint(0,1),
                        new TetrisPoint(1,1),new TetrisPoint(2,1)};

                default:
                    return null;
            }
        }

        public static TetrisShape CreateRandomShape(int range, out int index, out int state)
        {
            CreateRandomIndexAndState(range,out index, out state);
            return CreateShape(index, state);
        }

        public static TetrisShape CreateRandomShape(out int index, out int state)
        {
            return CreateRandomShape(ShapeStrategy.ClassicShapeCount, out index, out state);
        }

        public static TetrisShape CreateShape(int index, int state)
        {
            TetrisPoint[] cells = CreateCells(index);
            return new TetrisShape(index, state, cells);
        }

        public static void CreateRandomIndexAndState(int range,out int index, out int state)
        {
            index = TetrisRandom.Next(range) + 1;
            state = TetrisRandom.Next(4);
        }

        public static void CreateRandomIndexAndState(out int index, out int state)
        {
            CreateRandomIndexAndState(ShapeStrategy.ClassicShapeCount, out index, out state);
        }
    }
}
