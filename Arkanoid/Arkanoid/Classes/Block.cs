using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkanoid.Classes
{
    internal class Block
    {
        public int Height {  get; set; }
        public int Width { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Health { get; set; }

        /// <summary>
        /// Метод инициализации блока
        /// </summary>
        public Block(int height, int width,int x, int y) 
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Health = 1; 
        }

        /// <summary>
        /// Метод уменьшения жизни блока
        /// </summary>
        public void Punch()
        {
            Health -= 1;
        }

    }
}
