using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serpent
{
    class Position
    {
        private double _x;
        private double _y;

        public Position(int x = 0, int y = 0)
        {
            this.X = x;
            this.Y = y;
        }

        public Position(Position position)
        {
            this.X = position.X;
            this.Y = position.Y;
        }

        public double Distance(Position position)
        {
            return Math.Sqrt(Math.Pow(position.X - this.X, 2) + Math.Pow(position.Y - this.Y, 2));
        }

        public int X
        {
            get
            {
                return _x;
            }

            set
            {
                _x = value;
            }
        }

        public int Y
        {
            get
            {
                return _y;
            }

            set
            {
                _y = value;
            }
        }
    }
}
