using System;

namespace TetrisGame.Core
{
    internal static class ShapeBuffer
    {
        private static byte[] buffer = new byte[16];

        internal static int InitializeCells(int offset, int index)
        {
            unsafe
            {
                fixed (byte* p = &buffer[offset])
                {
                    switch (index)
                    {
                        case 0:
                            *((int*)p) = 0x00000100;
                            *((int*)p + 4) = 0x01010001;
                            return 4;
                        case 1:
                            return 4;
                        case 2:
                            return 4;
                        case 3:
                            return 4;
                        case 4:
                            return 4;
                        case 5:
                            return 4;
                        case 6:
                            return 4;
                        default:
                            return 4;
                    }
                }


            }
        }
    }
}
