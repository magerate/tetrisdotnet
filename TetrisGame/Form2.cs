using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using TetrisGame.Core;
using TetrisGame.Drawing;
namespace TetrisGame
{
    public partial class Form2 : Form
    {
        private AutoDropTetris leftAutoTetris, rightAutoTetris;
        private Judge judge;
        public Form2()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            tetrisBox1.Tetris.ShapeStrategy = new RivalShapeStrategy();
            tetrisBox2.Tetris.ShapeStrategy = new RivalShapeStrategy();
            judge = new Judge(tetrisBox1.Tetris, tetrisBox2.Tetris);
            leftAutoTetris = new AutoDropTetris(tetrisBox1.Tetris);
            rightAutoTetris = new AutoDropTetris(tetrisBox2.Tetris);

            ImageBrushStrategy brushStrategy = new ImageBrushStrategy(@"C:\Users\tomato\Desktop\Buff Face1\Buff Face1.ibs");
            tetrisBox1.Renderer.BrushStrategy = brushStrategy;
            tetrisBox2.Renderer.BrushStrategy = brushStrategy;

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F2:
                    judge.Start();
                    break;

                case Keys.A:
                    tetrisBox1.Tetris.MoveLeft();
                    break;
                case Keys.D:
                    tetrisBox1.Tetris.MoveRight();
                    break;
                case Keys.S:
                    tetrisBox1.Tetris.DropToBottom();
                    break;
                case Keys.W:
                    tetrisBox1.Tetris.Rotate();
                    break;


                case Keys.Left:
                    tetrisBox2.Tetris.MoveLeft();
                    break;
                case Keys.Right:
                    tetrisBox2.Tetris.MoveRight();
                    break;
                case Keys.Down:
                    tetrisBox2.Tetris.DropToBottom();
                    break;
                case Keys.Up:
                    tetrisBox2.Tetris.Rotate();
                    break;
                default:
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    ImageBrushScheme ibs = new ImageBrushScheme();
        //    ibs.Convert(@"C:\Users\tomato\Desktop\Buff Face1", @"C:\Users\tomato\Desktop\Buff Face1\Buff Face1.ibs");
        //}
    }
}
