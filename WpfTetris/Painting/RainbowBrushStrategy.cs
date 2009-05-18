using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace WpfTetris.Painting
{
    public class RainbowBrushStrategy : IBrushStrategy
    {
        public Brush CreateBrush(int index,
                               double cellWidth,
                               double cellHeight,
                               double offsetX,
                               double offsetY)
        {
            Color color = TetrisBrushHelper.GetColorByShapeIndex(index);
            Rect rect = new Rect(0.0, 0.0, cellWidth, cellHeight);
            RectangleGeometry rectGeometry = new RectangleGeometry(rect);
            Brush brush = new SolidColorBrush(color);
            GeometryDrawing rectDrawing = new GeometryDrawing(brush,null,rectGeometry);
            DrawingGroup drawingGroup = new DrawingGroup();
            drawingGroup.Children.Add(rectDrawing);
            drawingGroup.BitmapEffect = new BevelBitmapEffect();
            DrawingBrush drawingBrush = new DrawingBrush(drawingGroup);
            TetrisBrushHelper.SetTileBrush(drawingBrush, 
                cellWidth, cellHeight,offsetX, offsetY);
            drawingBrush.Freeze();

            return drawingBrush;
        }
    }
}
