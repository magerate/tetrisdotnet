using System;

namespace TetrisGame.Core
{
    public interface ITetrisDecorator
    {
        Tetris TetrisModel { get; }

        void Start();
    }

    public abstract class TetrisDecorator:ITetrisDecorator
    {
        protected ITetrisDecorator tetris;

        protected TetrisDecorator(ITetrisDecorator tetris)
        {
            this.tetris = tetris;
        }

        public Tetris TetrisModel
        {
            get { return tetris.TetrisModel; }
        }

        public virtual void Start()
        {
            tetris.Start();
        }
    }
}
