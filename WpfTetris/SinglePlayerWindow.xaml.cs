using System.Windows;
using System.Windows.Input;

using WpfTetris.Painting;
namespace WpfTetris
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SinglePlayerWindow : Window
    {
        private IBrushStrategy brushStrategy;

        public SinglePlayerWindow()
        {
            InitializeComponent();
            brushStrategy = new RainbowBrushStrategy();

            tetrisBox.BrushStrategy = brushStrategy;
            shapeBox.BrushStrategy = brushStrategy;

            tetrisBox.Tetris.TetrisModel.ShapeCreated += delegate 
            {
                shapeBox.Shape = tetrisBox.Tetris.TetrisModel.ShapeStrategy.Peek();
            };
        }

        private void ExecuteMoveLeft(object sender, ExecutedRoutedEventArgs e)
        {
            tetrisBox.Tetris.MoveLeft();
        }

        private void ExecuteMoveRight(object sender, ExecutedRoutedEventArgs e)
        {
            tetrisBox.Tetris.MoveRight();
        }

        private void ExecuteDrop(object sender, ExecutedRoutedEventArgs e)
        {
            tetrisBox.Tetris.Drop();
        }

        private void ExecuteDropToBottom(object sender, ExecutedRoutedEventArgs e)
        {
            tetrisBox.Tetris.DropToBottom();
        }

        private void ExecuteRotate(object sender, ExecutedRoutedEventArgs e)
        {
            tetrisBox.Tetris.Rotate();
        }

        private void ExecutePlay(object sender, ExecutedRoutedEventArgs e)
        {
            tetrisBox.Tetris.Start();
        }
    }
}
