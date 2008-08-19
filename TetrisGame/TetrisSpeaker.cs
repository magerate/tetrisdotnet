using System;
using System.Media;
using System.Reflection;
using System.IO;
using TetrisGame.Core;


namespace TetrisGame
{
    public class TetrisSpeaker
    {
        private SoundPlayer player;

        private ITetris tetris;
        public Tetris TetrisModel
        {
            get { return tetris.TetrisModel; }
        }

        public bool SoundOn { get; set; }
        public TetrisSpeaker(ITetris tetris)
        {
            SoundOn = true;
            player = new SoundPlayer();
            this.tetris = tetris;

            tetris.TetrisModel.Changed += TetrisChanged;

            tetris.TetrisModel.Bumped += delegate { PlaySound("bump.wav"); };
            tetris.TetrisModel.RowsKilled += delegate { PlaySound("kill.wav"); };
        }

        private void TetrisChanged(object sender, TetrisChangedEventArgs e)
        {
            switch (e.Action)
            {
                case TetrisAction.Start:
                    PlaySound("start.wav");
                    break;
                //case TetrisAction.Rotate:
                //    PlaySound("transform.wav");
                //    break;
                case TetrisAction.End:
                    switch (tetris.TetrisModel.Status)
                    {
                        case TetrisStatus.Won:
                            PlaySound("win.wav");
                            break;
                        case TetrisStatus.Lost:
                            PlaySound("lost.wav");
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }


        private void PlaySound(string fileName)
        {
            if (SoundOn)
            {
                player.SoundLocation = GetSoundFullPath(fileName);
                player.Play(); 
            }
        }

        private string GetSoundFullPath(string fileName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
#if DEBUG
            return Path.GetDirectoryName(asm.Location) + @"\..\..\Sound\" + fileName;
#else
           return Path.GetDirectoryName(asm.Location) + "\\" + fileName;
#endif

        }

    }
}
