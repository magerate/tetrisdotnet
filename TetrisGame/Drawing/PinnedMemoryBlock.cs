using System;
using System.Runtime.InteropServices;

namespace TetrisGame.Drawing
{
    internal class PinnedMemoryBlock : IDisposable
    {
        //default length 1m
        const int DefaultLength = 1048576;

        private byte[] block;
        private GCHandle handle;

        public PinnedMemoryBlock(int length)
        {
            block = new byte[length];
            handle = GCHandle.Alloc(block, GCHandleType.Pinned);
        }

        public PinnedMemoryBlock() : this(DefaultLength) { }

        public IntPtr IntPtr
        {
            get { return Marshal.UnsafeAddrOfPinnedArrayElement(block, 0); }
        }

        public void Clear(int index, int length)
        {
            IntPtr p = Marshal.UnsafeAddrOfPinnedArrayElement(block, index);
            SafeNativeMethods.memset(p, 0, length);
        }

        public void Offset(int index, int length, int offset)
        {
            IntPtr src = Marshal.UnsafeAddrOfPinnedArrayElement(block, index);
            IntPtr dst = Marshal.UnsafeAddrOfPinnedArrayElement(block, index + offset);
            SafeNativeMethods.memcpy(dst, src, length);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~PinnedMemoryBlock()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (handle.IsAllocated)
            {
                handle.Free();
            }
        }
    }
}
