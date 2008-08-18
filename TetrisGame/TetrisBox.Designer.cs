namespace TetrisGame
{
    partial class TetrisBox
    {
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (renderer != null)
                {
                    renderer.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}
