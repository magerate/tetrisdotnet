using System;
using System.Windows;
using System.Windows.Media;

namespace WpfTetris.Painting
{
    public interface IBrushStrategy
    {
        Brush CreateBrush(int index, double width, double height, double offsetX, double offsetY);
    }

    public static class BrushExtension
    {
        public static Brush CreateBrush(this IBrushStrategy brushStrategy, int index, double width, double height)
        {
            return brushStrategy.CreateBrush(index, width, height, 0.0, 0.0);
        }

        public static Brush CreateBrush(this IBrushStrategy brushStrategy, int index)
        {
            return brushStrategy.CreateBrush(index, 32.0, 32.0, 0.0, 0.0);
        }

        public static Brush CreateBrush(this IBrushStrategy brushStrategy,int index,Size cellSize,Point offset)
        {
            return brushStrategy.CreateBrush(index, cellSize.Width, cellSize.Height, offset.X, offset.Y);
        }
    }
}
