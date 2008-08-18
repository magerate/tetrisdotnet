using System;
using System.ComponentModel;
using System.Windows.Forms;
using TetrisGame.Core;

namespace TetrisGame
{
    public class AutoDropTetris : ITetris, INotifyPropertyChanged,IDisposable
    {
        public const int InitialLevel = 1;
        const int LevelUpScore = 10000;
        private Timer timer;
        private ITetris tetris;
        private int level;

        public event EventHandler LevelUp;
        public event PropertyChangedEventHandler PropertyChanged;

        public int Level
        {
            get { return level; }
            set
            {
                if (value >= 1 && value < 10)
                {
                    level = value;
                    SwitchLevel();
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Level"));
                    }
                }
            }
        }

        public Tetris TetrisModel
        {
            get { return tetris.TetrisModel; }
        }

        public int Interval
        {
            get { return timer.Interval; }
            set { timer.Interval = value; }
        }

        public AutoDropTetris(ITetris tetris)
        {
            this.level = InitialLevel;
            this.tetris = tetris;
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += delegate { tetris.TetrisModel.Drop(); };

            tetris.TetrisModel.Changed += TetrisChanged;

            tetris.TetrisModel.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Score")
                {
                    if (tetris.TetrisModel.Score >= level * LevelUpScore)
                    {
                        this.Level++;
                        SwitchLevel();
                        if (LevelUp != null)
                        {
                            LevelUp(this, EventArgs.Empty);
                        }
                    }
                }
            };
        }

        private void TetrisChanged(object sender, TetrisChangedEventArgs e)
        {
            switch (e.Action)
            {
                case TetrisAction.Start:
                    timer.Start();
                    break;
                case TetrisAction.End:
                    timer.Stop();
                    break;
                case TetrisAction.Pause:
                    timer.Stop();
                    break;
                case TetrisAction.Resume:
                    timer.Start();
                    break;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing && timer!=null)
            {
                timer.Dispose();
            }
        }

        private void SwitchLevel()
        {
            timer.Interval = (11 - level) * 100;
        }
    }
}
