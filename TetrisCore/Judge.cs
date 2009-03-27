using System;

namespace TetrisGame.Core
{
    public class Judge
    {
        private Tetris leftTetris, rightTetris;

        private RivalShapeStrategy LeftShapeStrategy
        {
            get 
            { 
                return (RivalShapeStrategy)leftTetris.ShapeStrategy; 
            }
        }

        private RivalShapeStrategy RightShapeStrategy
        {
            get { return (RivalShapeStrategy)rightTetris.ShapeStrategy; }
        }

        public Judge(Tetris leftTetris, Tetris rightTetris)
        {
            this.leftTetris = leftTetris;
            this.rightTetris = rightTetris;

            LeftShapeStrategy.RandomShapeCreated += RandomShapeCreated;
            RightShapeStrategy.RandomShapeCreated += RandomShapeCreated;

            //leftTetris.RowsKilled += RowsKilled;
            //rightTetris.RowsKilled += RowsKilled;

            //leftTetris.Changed += TetrisChanged;
            //rightTetris.Changed += TetrisChanged;

        }

        public void Start()
        {
            int index, state;
            for (int i = 0; i < RivalShapeStrategy.DefaultDepth; i++)
            {
                ShapeHelper.CreateRandomIndexAndState(out index, out state);
                LeftShapeStrategy.Enqueue(index, state);
                RightShapeStrategy.Enqueue(index, state);
            }
            leftTetris.Start();
            rightTetris.Start();
        }

        public void Pause()
        {
            //leftTetris.PauseOrResume();
            //rightTetris.PauseOrResume();
        }

        private void RandomShapeCreated(object sender, RandomShapeCreatedEventArgs e)
        {
            if (object.ReferenceEquals(sender, leftTetris.ShapeStrategy))
            {
                RightShapeStrategy.Enqueue(e.Index, e.State);
            }
            else
            {
                LeftShapeStrategy.Enqueue(e.Index, e.State);
            }
        }

        private void RowsKilled(object sender, RowsKillEventArgs e)
        {
            if (object.ReferenceEquals(sender, leftTetris))
            {
                rightTetris.Add(e.KilledRows.Count);
            }
            else
            {
                leftTetris.Add(e.KilledRows.Count);
            }
        }

        private void TetrisChanged(object sender, TetrisChangedEventArgs e)
        {
            if (e.Action==TetrisAction.End)
            {
                if (object.ReferenceEquals(sender, leftTetris))
                {
                    //rightTetris.End(false);
                }
                else
                {
                    //leftTetris.End(false);
                }
            }
        }
    }
}
