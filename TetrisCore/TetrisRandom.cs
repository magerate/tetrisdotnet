using System;

namespace TetrisGame.Core
{
    public static class TetrisRandom
    {
        private static Random random = new Random();

        public static int Next()
        {
            return random.Next();
        }

        public static int Next(int maxValue)
        {
            return random.Next(maxValue);
        }
    }
}
