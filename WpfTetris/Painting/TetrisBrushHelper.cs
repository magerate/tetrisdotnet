using System;
using System.Windows;
using System.Windows.Media;
using System.Diagnostics;

namespace WpfTetris.Painting
{
    public static class TetrisBrushHelper
    {
        public static Color GetColorByShapeIndex(int index)
        {
            switch (index % 7)
            {
                case 1: return Colors.Orange;
                case 2: return Colors.Yellow;
                case 3: return Colors.Green;
                case 4: return Colors.Blue;
                case 5: return Colors.Gray;
                case 6: return Colors.Purple;
                default: return Colors.Red;
            }
        }

        public static void SetTileBrush(TileBrush brush,
                                        double cellWidth, 
                                        double cellHeight, 
                                        double offsetX, 
                                        double offsetY)
        {
            Debug.Assert(brush != null);
            brush.TileMode = TileMode.Tile;
            brush.Viewport = new Rect(0.0, 0.0, cellWidth, cellHeight);
            brush.ViewportUnits = BrushMappingMode.Absolute;

            if (offsetX != 0.0 || offsetY != 0.0)
            {
                brush.Transform = new TranslateTransform(offsetX, offsetY);                
            }
        }
    }
}
