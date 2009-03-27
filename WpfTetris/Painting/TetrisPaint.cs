using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Diagnostics;
using TetrisGame.Core;


namespace WpfTetris.Painting
{
    public static class TetrisPaint
    {
        public const double DefaultWidth = 32.0;
        public const double DefaultHeight = 32.0;

        private static Geometry GetShapeGeometry(IEnumerable<TetrisPoint> shape,
                                                double cellWidth,
                                                double cellHeight,
                                                double offsetX,
                                                double offsetY)
        {
            GeometryGroup gg = new GeometryGroup();
            Rect rect = new Rect();
            foreach (TetrisPoint point in shape)
            {
                rect.X = point.X * cellWidth + offsetX;
                rect.Y = point.Y * cellHeight + offsetY;
                rect.Width = cellWidth;
                rect.Height = cellHeight;

                gg.Children.Add(new RectangleGeometry(rect));
            }
            return gg;
        }


        public static void DrawShape(DrawingContext drawingContext,
                                    Brush brush,
                                    IEnumerable<TetrisPoint> shape,
                                    double cellWidth,
                                    double cellHeight,
                                    double offsetX,
                                    double offsetY)
        {
            Debug.Assert(drawingContext != null);
            Debug.Assert(brush != null);
            Debug.Assert(shape != null);
            Debug.Assert(cellWidth > 0.0);
            Debug.Assert(cellHeight > 0.0);

            Geometry g = GetShapeGeometry(shape, cellWidth, cellHeight, offsetX, offsetY);
            drawingContext.DrawGeometry(brush, null, g);
        }

        public static void DrawShape(DrawingContext drawingContext,
                                    Brush brush,
                                    IEnumerable<TetrisPoint> shape,
                                    double cellWidth,
                                    double cellHeight)
        {
            DrawShape(drawingContext, brush, shape, cellWidth, cellHeight, .0, .0);
        }

        public static void DrawGrid(DrawingContext drawingContext,
                                    Pen pen,
                                    Size cellSize,
                                    Size logicalSize,
                                    Point offset)
        {
            Debug.Assert(drawingContext != null);
            Debug.Assert(pen != null);
            Point start = new Point();
            Point end = new Point();
            for (int i = 0; i <= logicalSize.Width; i++)
            {
                start.X = offset.X + i * cellSize.Width;
                start.Y = offset.Y;
                end.X = start.X;
                end.Y = offset.Y + cellSize.Height * logicalSize.Height;
                drawingContext.DrawLine(pen, start, end);
            }

            for (int i = 0; i <= logicalSize.Height; i++)
            {
                start.X = offset.X;
                start.Y = offset.Y + i * cellSize.Height;
                end.X = offset.X + logicalSize.Width * cellSize.Width;
                end.Y = start.Y;
                drawingContext.DrawLine(pen, start, end);
            }
        }
    }
}
