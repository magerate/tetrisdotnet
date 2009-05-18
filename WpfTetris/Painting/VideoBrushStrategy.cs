using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using TetrisGame.Core;

namespace WpfTetris.Painting
{
    
    public class VideoBrushStrategy : IBrushStrategy
    {
        private Brush brush;
        private TileMode tileMode;
        private TetrisGrid grid;

        public VideoBrushStrategy(TetrisGrid grid)
        {
            this.grid=grid;
            brush = null;
            tileMode = TileMode.None;
        }

        public Brush CreateBrush(int index,
                               double cellWidth,
                               double cellHeight,
                               double offsetX,
                               double offsetY)
        {
            if (brush == null)
            {
                brush = DoCreateBrush(index, cellWidth, cellHeight, offsetX, offsetY);
            }
            return brush;
        }

        private Brush DoCreateBrush(int index,
                               double cellWidth,
                               double cellHeight,
                               double offsetX,
                               double offsetY)
        {
            Uri uri = new Uri(@"C:\Users\tomato\Desktop\zhongaiqingyou.swf", UriKind.Absolute);
            MediaTimeline timeline = new MediaTimeline(uri);
            timeline.RepeatBehavior = RepeatBehavior.Forever;
            MediaClock clock = timeline.CreateClock();
            MediaPlayer player = new MediaPlayer();
            player.Clock = clock;


            VideoDrawing videoDrawing = new VideoDrawing();
            videoDrawing.Player = player;

            if (TileMode.Tile == tileMode)
            {
                videoDrawing.Rect = new Rect(0.0, 0.0, cellWidth, cellHeight);
            }
            else
            {
                videoDrawing.Rect = new Rect(0.0, 0.0, cellWidth * grid.Width, cellHeight * grid.Height);
            }

            DrawingBrush drawingBrush = new DrawingBrush(videoDrawing);
            if (TileMode.Tile == tileMode)
            {
                TetrisBrushHelper.SetTileBrush(drawingBrush, cellWidth, cellHeight, offsetX, offsetY);
            }
            else
            {
                drawingBrush.TileMode = TileMode.None;
                drawingBrush.Viewport = new Rect(0.0, 0.0, cellWidth * grid.Width, cellHeight * grid.Height);
                drawingBrush.ViewportUnits = BrushMappingMode.Absolute;
            }
            return drawingBrush;
        }
    }
}
