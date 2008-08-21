using System;
using System.Drawing;

namespace TetrisGame.Drawing
{
    public interface IBrushStrategy
    {
        Brush CreateBrush(int index, int width, int height, int offsetX, int offsetY);
    }

    public static class BrushHelper
    {
        public static Brush CreateBrush(this IBrushStrategy factory, int index, int width, int height)
        {
            return factory.CreateBrush(index, width, height, 0, 0);
        }
    }

    public class RainbowBrushStrategy : IBrushStrategy
    {
        public static readonly RainbowBrushStrategy Instance = new RainbowBrushStrategy();
        private RainbowBrushStrategy() { }

        public Brush CreateBrush(int index, int width, int height, int offsetX, int offsetY)
        {
            Color color;
            switch (index % 7 + 1)
            {
                case 2: color = Color.Orange; break;
                case 3: color = Color.Yellow; break;
                case 4: color = Color.Green; break;
                case 5: color = Color.Blue; break;
                case 6: color = Color.Pink; break;
                case 7: color = Color.Purple; break;
                default: color = Color.Red; break;
            }
            return new SolidBrush(color);
        }
    }
}
