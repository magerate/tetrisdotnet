using System;
using System.Windows.Threading;

using TetrisGame.Core;

namespace WpfTetris
{
    public class AutoDropTetris : TetrisDecorator
    {
        private DispatcherTimer timer;

        public AutoDropTetris(ITetris tetris)
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

        public override void End()
        {
            timer.Stop();
            base.End();
        }

    }
}
