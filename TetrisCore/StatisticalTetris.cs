using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TetrisGame.Core
{
    public class StatisticalTetris : TetrisDecorator, INotifyPropertyChanged
    {

        private int score;
        private int shapeCount;
        private int killedCount;

        public event PropertyChangedEventHandler PropertyChanged;

        public StatisticalTetris(ITetris tetris)
            : base(tetris)
        {
            tetris.TetrisModel.Bumped += delegate { ShapeCount++; };
            tetris.TetrisModel.Grid.RowsKilled += delegate(object sender, RowsKillEventArgs e)
            {
                KilledCount += e.KilledRows.Count;
                Score += e.KilledRows.Count * 100;
            };
        }

        private void Initialize()
        {
            score = 0;
            shapeCount = 0;
            killedCount = 0;
        }

        public override void Start()
        {
            Initialize();
            base.Start();
        }

        public int Score
        {
            get { return score; }
            private set
            {
                score = value;
                OnPropertyChanged("Score");
            }
        }

        public int KilledCount
        {
            get { return killedCount; }
            private set
            {
                killedCount = value;
                OnPropertyChanged("KilledCount");
            }
        }

        public int ShapeCount
        {
            get { return shapeCount; }
            private set
            {
                shapeCount = value;
                OnPropertyChanged("ShapeCount");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
