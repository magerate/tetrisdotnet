using System;

namespace TetrisGame.Core
{
    public interface ITetris
    {
        Tetris TetrisModel { get; }

        void Start();
        void End();

        void MoveLeft();
        void MoveRight();
        void Drop();
        void DropToBottom();
        void Rotate();
    }

    public abstract class TetrisDecorator:ITetris
    {
        protected ITetris tetris;

        protected TetrisDecorator(ITetris tetris)
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

        public virtual void End()
        {
            tetris.End();
        }

        public virtual void MoveLeft()
        {
            tetris.MoveLeft();
        }

        public virtual void MoveRight()
        {
            tetris.MoveRight();
        }

        public virtual void Drop()
        {
            tetris.Drop();
        }

        public virtual void DropToBottom()
        {
            tetris.DropToBottom();
        }

        public virtual void Rotate()
        {
            tetris.Rotate();
        }
    }
}
