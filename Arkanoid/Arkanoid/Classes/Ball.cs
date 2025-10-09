using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkanoid.Classes
{
    internal class Ball
    {
        public int Height { get; set; }

        public int Width { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int BallSpeedX { get; set; }

        public int BallSpeedY { get; set; }

        /// <summary>
        /// Метод инициализации шарика
        /// </summary>
        public Ball(int x, int y, int width, int height, int ballSpeedX = 3, int ballSpeedY = -3)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            BallSpeedX = ballSpeedX;
            BallSpeedY = ballSpeedY;
        }
    }
}

