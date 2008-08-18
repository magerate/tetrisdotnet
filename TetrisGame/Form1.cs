using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

using TetrisGame.Drawing;
namespace TetrisGame
{
    public partial class Form1 : Form
    {
        private AutoDropTetris autoDropTetris;
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            autoDropTetris = new AutoDropTetris(tetrisBox1.Tetris);
            labelHeight.DataBindings.Add("Text", tetrisBox1.Tetris, "CurrentHeight");
            labelKilled.DataBindings.Add("Text", tetrisBox1.Tetris, "KilledRows");
            labelScore.DataBindings.Add("Text", tetrisBox1.Tetris, "Score");
            labelShapeCount.DataBindings.Add("Text", tetrisBox1.Tetris, "ShapeCount");
            labelLevel.DataBindings.Add("Text", autoDropTetris, "Level");

            tetrisBox1.Tetris.ShapeCreated += delegate
            { shapeBox1.Shape = tetrisBox1.Tetris.ShapeStrategy.Peek(); };

            ImageBrushStrategy brushStrategy = new ImageBrushStrategy(@"C:\Users\tomato\Desktop\heart\heart.ibs");
            tetrisBox1.Renderer.BrushStrategy = brushStrategy;
            shapeBox1.BrushStrategy = brushStrategy;

        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);

            tetrisBox1.Tetris.InitialHeight = 5;

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                    tetrisBox1.Tetris.MoveLeft();
                    break;
                case Keys.Right:
                    tetrisBox1.Tetris.MoveRight();
                    break;
                case Keys.Down:
                    tetrisBox1.Tetris.DropToBottom();
                    break;
                case Keys.Up:
                    tetrisBox1.Tetris.Rotate();
                    break;
                case Keys.F2:
                    tetrisBox1.Tetris.Start();
                    break;
                case Keys.Space:
                    tetrisBox1.Tetris.PauseOrResume();
                    break;
                case Keys.Enter:
                    tetrisBox1.Tetris.Drop();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            //for (int i = 100; i >0; i-=5)
            //{
            //    this.Opacity = i / 100.0;
            //    Refresh();
            //    //Thread.Sleep(10);
            //}
            base.OnClosing(e);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }


    }
}
