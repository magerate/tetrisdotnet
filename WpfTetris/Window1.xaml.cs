using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfTetris
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            tetrisBox.Start();
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            switch (e.Key)
            {
                case Key.Left:
                    tetrisBox.MoveLeft();
                    break;
                case Key.Right:
                    tetrisBox.MoveRight();
                    break;
                case Key.Down:
                    tetrisBox.Drop();
                    break;
                case Key.Up:
                    tetrisBox.Rotate();
                    break;
                default:
                    break;
            }
        }
    }
}
