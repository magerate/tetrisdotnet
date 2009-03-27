using System;
using System.Windows.Threading;

using TetrisGame.Core;

namespace WpfTetris
{
    public class AutoDropTetris : TetrisDecorator
    {
        private DispatcherTimer timer;

        public AutoDropTetris(ITetrisDecorator tetris)
            : base(tetris)
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += delegate { tetris.TetrisModel.Drop(); };
        }

        public override void Start()
        {
            timer.Start();
            base.Start();
        }

    }
}
