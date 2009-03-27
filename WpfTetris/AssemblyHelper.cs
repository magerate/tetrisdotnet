using System;
using System.Reflection;
using System.IO;

namespace WpfTetris
{
    public static class AssemblyHelper
    {
        public static string GetCurrentAssemblyPath()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            return Path.GetDirectoryName(asm.Location);
        }
    }
}
