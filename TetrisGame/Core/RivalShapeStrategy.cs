using System;
using System.Collections.Generic;

namespace TetrisGame.Core
{
    public class RivalShapeStrategy : ShapeStrategy
    {
        public const int DefaultDepth = 16;
        private Queue<int> shapeQueue = new Queue<int>(2 * DefaultDepth);
        public event EventHandler<RandomShapeCreatedEventArgs> RandomShapeCreated;

        public void Enqueue(int index, int state)
        {
            shapeQueue.Enqueue(index);
            shapeQueue.Enqueue(state);
        }

        protected override TetrisShape CreateShape()
        {
            int index, state;
            TetrisShape shape;
            if (shapeQueue.Count > 0)
            {
                index = shapeQueue.Dequeue();
                state = shapeQueue.Dequeue();
                shape = ShapeHelper.CreateShape(index, state);
            }
            else
            {
                shape = ShapeHelper.CreateRandomShape(out index, out state);
                if (RandomShapeCreated != null)
                {
                    RandomShapeCreated(this, new RandomShapeCreatedEventArgs(index, state));
                }
            }
            return shape;
        }

        public override void Reset()
        {
            base.Reset();
            shapeQueue.Clear();
        }

    }
}
