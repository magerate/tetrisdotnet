using System;
using System.Media;
using System.Reflection;
using System.IO;
using TetrisGame.Core;


namespace TetrisGame
{
    public class TetrisSpeaker
    {
        enum TetrisSound
        {
            Start, Bump, Kill, Win, Lost
        }

        const string SoundPath = "Sound";

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

            tetris.TetrisModel.Bumped += delegate { PlaySound(TetrisSound.Bump); };
            tetris.TetrisModel.RowsKilled += delegate { PlaySound(TetrisSound.Kill); };
        }

        private void TetrisChanged(object sender, TetrisChangedEventArgs e)
        {
            switch (e.Action)
            {
                case TetrisAction.Start:
                    PlaySound(TetrisSound.Start);
                    break;
                case TetrisAction.End:
                    switch (tetris.TetrisModel.Status)
                    {
                        case TetrisStatus.Won:
                            PlaySound(TetrisSound.Win);
                            break;
                        case TetrisStatus.Lost:
                            PlaySound(TetrisSound.Lost);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        private void PlaySound(TetrisSound sound)
        {
            if (SoundOn)
            {
                player.SoundLocation = GetSoundFullName(sound);
                player.Play();
            }
        }

        private string GetSoundFullName(TetrisSound sound)
        {
            string fileName = string.Empty;
            switch (sound)
            {
                case TetrisSound.Start:
                    fileName = "start.wav";
                    break;
                case TetrisSound.Bump:
                    fileName = "bump.wav";
                    break;
                case TetrisSound.Kill:
                    fileName = "kill.wav";
                    break;
                case TetrisSound.Win:
                    fileName = "win.wav";
                    break;
                case TetrisSound.Lost:
                    fileName = "lost.wav";
                    break;
                default:
                    break;
            }
            return GetSoundPath() + "\\" + fileName;
        }

        private string GetSoundPath()
        {
#if DEBUG
            return TetrisUtility.GetTetrisPath() + @"\..\..\" + SoundPath;
#else
           return TetrisUtility.GetTetrisPath() + "\\" + SoundPath;
#endif
        }

    }
}
