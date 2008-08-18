using System;

namespace TetrisGame.Core
{
    public enum TetrisMessage
    {
        RowsOffseted, RowsCreated, RowsKilled,RowsKilling,RowsCleared, Resize, CurrentHeightChanged,KilledRowsChanged
    }

    public enum TetrisAction
    {
        MoveLeft, MoveRight, Drop, DropToBottom, Rotate,Start,Pause,Resume,End
    }

    public enum TetrisStatus
    {
        Waiting,Playing,Paused,Won,Lost
    }

    public delegate void RequestHandler(TetrisMessage message, EventArgs e);
}
