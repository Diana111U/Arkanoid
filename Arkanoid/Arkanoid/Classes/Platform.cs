using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkanoid.Classes
{
    internal class Platform
    {
        public int Height { get; set; }

        public int Width { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        /// <summary>
        /// Метод инициализации платформы
        /// </summary>
        public Platform(int height, int width, int x, int y) 
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}
