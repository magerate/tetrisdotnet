using System;
using System.Collections.Generic;

namespace TetrisGame.Core
{
    public class Tetris : ITetrisDecorator
    {
        private RotateDirection rotateDirection;

        private TetrisShape shape;
        private TetrisGrid grid;
        private IShapeStrategy shapeStrategy;

        #region "Properties

        public TetrisShape CurrentShape
        {
            get { return shape; }
        }

        public TetrisGrid Grid
        {
            get { return grid; }
        }

        public RotateDirection RotateDirection
        {
            get { return rotateDirection; }
            set { rotateDirection = value; }
        }

        public IShapeStrategy ShapeStrategy
        {
            get { return shapeStrategy; }
            set { shapeStrategy = value; }
        }

        public Tetris TetrisModel
        {
            get { return this; }
        }

        #endregion

        #region "Events"

        public event EventHandler<EventArgs> ShapeCreated;
        public event EventHandler Resize;
        public event EventHandler Bumped;

        private void OnResize(EventArgs e)
        {
            if (Resize != null)
            {
                Resize(this, e);
            }
        }

        private void OnShapeCreated(EventArgs e)
        {
            if (ShapeCreated != null)
            {
                ShapeCreated(this, e);
            }
        }

        private void OnBumped(EventArgs e)
        {
            if (Bumped != null)
            {
                Bumped(this, e);
            }
        }

        #endregion

        public Tetris(IShapeStrategy shapeStrategy)
            : this(shapeStrategy, new BitGrid())
        { }

        public Tetris(IShapeStrategy shapeStrategy, TetrisGrid grid)
        {
            this.shapeStrategy = shapeStrategy;
            this.grid = grid;
            rotateDirection = RotateDirection.Clockwise;
        }

        public Tetris()
            : this(new ShapeStrategy())
        { }

        public void Start()
        {
            grid.Reset();
            CreateNextShape();
        }

        public void DropToBottom()
        {
            int offset = 0;
            while (shape.Bottom + offset < grid.Height && !IsBlock(shape, 0, offset + 1))
            {
                ++offset;
            }
            if (offset > 0)
            {
                shape.Offset(0, offset, TetrisAction.DropToBottom);
            }
            Bump();
        }

        public void MoveLeft()
        {
            if (shape.Left > 0 && !IsBlock(shape, -1, 0))
            {
                shape.Offset(-1, 0, TetrisAction.MoveLeft);
            }
        }

        public void MoveRight()
        {
            if (shape.Right < grid.Width && !IsBlock(shape, 1, 0))
            {
                shape.Offset(1, 0, TetrisAction.MoveRight);
            }
        }

        public void Drop()
        {
            if (shape.Bottom < grid.Height && !IsBlock(shape, 0, 1))
            {
                shape.Offset(0, 1, TetrisAction.Drop);
            }
            else
            {
                Bump();
            }
        }

        public void Rotate()
        {
            if (shape.Left + shape.Height <= grid.Width && 
                shape.Top + shape.Width <= grid.Height &&
                !IsBlock(shape.GetRotatedCells(rotateDirection), 0, 0))
            {
                shape.Rotate(rotateDirection);
            }
        }

        public void Add(int count)
        {
            int overflow = grid.Add(count);
            AdjustShape();
            if (overflow > 0)
            {
                End();
            }
        }

        private void AdjustShape()
        {
            int offset = 0;
            while (IsBlock(shape, 0, offset))
            {
                offset--;
            }

            if (offset < 0)
            {
                shape.Offset(0, offset, TetrisAction.Drop);
            }

            if (shape.Top < 0)
            {
                End();
            }
        }

        private void End()
        {
        }

        private void CreateNextShape()
        {
            shape = shapeStrategy.Create();
            shape.Move((grid.Width - shape.Width) / 2, 0);
            OnShapeCreated(EventArgs.Empty);
            if (IsBlock(shape, 0, 0))
            {
                End();
            }
        }

        private void Bump()
        {
            OnBumped(EventArgs.Empty);
            grid.Fill(shape);
            CreateNextShape();
        }

        private bool IsBlock(IEnumerable<TetrisPoint> shape, int x, int y)
        {
            foreach (TetrisPoint point in shape)
            {
                if (point.Y + y > 0 && grid[point.X + x, point.Y + y])
                {
                    return true;
                }
            }
            return false;
        }
    }
}
