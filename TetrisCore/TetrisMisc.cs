using System;

namespace TetrisGame.Core
{

    public enum TetrisAction
    {
        MoveLeft, 
        MoveRight, 
        Drop, 
        DropToBottom, 
        Rotate, 
        Start, 
        Pause, 
        Resume, 
        End
    }


    public enum RotateDirection
    {
        Clockwise,Anticlockwise
    }
}
