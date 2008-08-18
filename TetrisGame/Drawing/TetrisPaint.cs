using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Windows.Forms;
using TetrisGame.Core;

namespace TetrisGame.Drawing
{
    public static class TetrisPaint
    {
        public static void DrawShape(Graphics g, Brush brush, IEnumerable<TetrisPoint> shape, int width, int height, int offsetX, int offsetY)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                foreach (Rectangle rect in GetShapeRects(shape, width, height, offsetX, offsetY))
                {
                    path.AddRectangle(rect);
                }
                g.FillPath(brush, path);
            }
        }

        public static void DrawShape(Graphics g, Brush brush, IEnumerable<TetrisPoint> shape, int width, int height)
        {
            DrawShape(g, brush, shape, width, height, 0, 0);
        }

        public static IEnumerable<Rectangle> GetShapeRects(IEnumerable<TetrisPoint> shape, int width, int height, int offsetX, int offsetY)
        {
            foreach (TetrisPoint point in shape)
            {
                yield return new Rectangle(point.X * width + offsetX, point.Y * height + offsetY, width, height);
            }
        }

        public static IEnumerable<Rectangle> GetShapeRects(IEnumerable<TetrisPoint> shape, int width, int height)
        {
            return GetShapeRects(shape, width, height, 0, 0);
        }

        public static Rectangle GetShapeBound(TetrisShape shape, int width, int height, int offsetX, int offsetY)
        {
            return new Rectangle(shape.Left * width + offsetX,
                shape.Top * height + offsetY,
                shape.Width * width,
                shape.Height * height);
        }

        public static Rectangle GetShapeBound(TetrisShape shape, int width, int height)
        {
            return GetShapeBound(shape, width, height, 0, 0);
        }

        public static void DrawGrid(Graphics g, Pen pen, Rectangle gridRect, Size cellSize, Point offset)
        {
            for (int i = offset.X + gridRect.Left; i <= offset.X + gridRect.Right; i += cellSize.Width)
            {
                g.DrawLine(pen, i, offset.Y + gridRect.Top, i, offset.Y + gridRect.Bottom);
            }
            for (int i = offset.Y + gridRect.Top; i <= offset.Y + gridRect.Bottom; i += cellSize.Height)
            {
                g.DrawLine(pen, offset.X + gridRect.Left, i, offset.X + gridRect.Right, i);
            }
        }

        public static void DrawGrid(Graphics g, Pen pen, Rectangle gridRect, Size cellSize)
        {
            DrawGrid(g, pen, gridRect, cellSize, Point.Empty);
        }

        public static void GetGridRect(ref Rectangle rect, Size cellSize, Point offset)
        {
            int x = (rect.X - offset.X) % cellSize.Width;
            int y = (rect.Y - offset.Y) % cellSize.Height;
            rect.X -= x;
            rect.Y -= y;
            rect.Width += x;
            rect.Height += y;

            x = (rect.Right - offset.X) % cellSize.Width;
            y = (rect.Bottom - offset.Y) % cellSize.Height;
            x = x == 0 ? 0 : cellSize.Width - x;
            y = y == 0 ? 0 : cellSize.Height - y;
            rect.Width += x;
            rect.Height += y;
        }

        public static void GetGridRect(ref Rectangle rect, Size cellSize)
        {
            GetGridRect(ref rect, cellSize, Point.Empty);
        }

        public static void InvalidateControl(Control control, Rectangle rect, Point offset)
        {
            rect.Offset(offset);
            control.Invalidate(rect);
        }
    }
}
