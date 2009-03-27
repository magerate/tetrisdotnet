using System;

namespace TetrisGame.Core
{
    public interface IShapeStrategy
    {
        TetrisShape Create();
        TetrisShape Peek();
        void Reset();
    }

    public class ShapeStrategy : IShapeStrategy
    {
        public const int ClassicShapeCount = 7;

        private TetrisShape shape;

        public TetrisShape Create()
        {
            if (shape != null)
            {
                TetrisShape current = shape;
                shape = CreateShape();
                return current;
            }
            else
            {
                return CreateShape();
            }
        }

        public TetrisShape Peek()
        {
            if (shape == null)
            {
                shape = CreateShape();
            }
            return shape;
        }

        protected virtual TetrisShape CreateShape()
        {
            int index, state;
            return ShapeHelper.CreateRandomShape(out index,out state);
        }

        public virtual void Reset()
        {
            shape = null;
        }
    }
}
