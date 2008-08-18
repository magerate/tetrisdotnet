using System;
using System.Runtime.InteropServices;
using System.Security;

namespace TetrisGame.Drawing
{
    [SuppressUnmanagedCodeSecurity]
    internal static class SaveNativeMethods
    {
        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        internal static extern unsafe void memcpy(void* dst, void* src, uint length);

        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        internal static extern unsafe void memset(void* dst, int c, uint length);


        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        internal static extern unsafe void memcpy(IntPtr dst, IntPtr src, int length);

        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        internal static extern unsafe void memset(IntPtr dst, int c, int length);

    }
}
