using System;
using System.Reflection;
using System.IO;

namespace TetrisGame
{
    public static class TetrisUtility
    {
        public static string GetTetrisPath()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            return Path.GetDirectoryName(asm.Location);
        }
    }
}
