using System;

namespace TetrisGame.Core
{
    public class ScoreTetris:ITetris
    {
        private ITetris tetris;
        public ScoreTetris(ITetris tetris)
        {
            this.tetris = tetris;
        }
    }
}
