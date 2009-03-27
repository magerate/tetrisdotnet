using System;
using System.Windows.Media;

namespace WpfTetris.Painting
{
    public class SimpleRainbowBrushStrategy:IBrushStrategy
    {
        public Brush CreateBrush(int index,
                                double cellWidth,
                                double cellHeight,
                                double offsetX,
                                double offsetY)
        {
            Color color = TetrisBrushHelper.GetColorByShapeIndex(index);
            return new SolidColorBrush(color);
        }
    }
}
