using System;
using System.Drawing;

namespace TetrisGame.Drawing
{
    public interface IBrushStrategy
    {
        Brush CreateBrush(int index, int width, int height,int offsetX, int offsetY);
    }

    public static class BrushHelper
    {
        public static Brush CreateBrush(this IBrushStrategy factory, int index, int width, int height)
        {
            return factory.CreateBrush(index,  width, height,0,0);
        }
    }

    public class DefaultBrushStrategy : IBrushStrategy
    {
        public static readonly DefaultBrushStrategy Instance = new DefaultBrushStrategy();
        private DefaultBrushStrategy() { }

        public Brush CreateBrush(int index,  int width, int height,int offsetX, int offsetY)
        {
            return new SolidBrush(Color.Aquamarine);
        }
    }
}
