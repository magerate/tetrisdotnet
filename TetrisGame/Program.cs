using System;
using System.Windows.Forms;
//using System.Reflection;

namespace TetrisGame
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Assembly asm = Assembly.GetExecutingAssembly();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CellThemeEditorForm());
        }
    }
}
