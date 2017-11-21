using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serpent
{
    class Serpent
    {
        private int _x;
        private int _y;
        private int _xspeed;
        private int _yspeed;

        public Serpent()
        {
            this._x = 0;
            this._y = 0;
            this._xspeed = 1;
            this._yspeed = 0;
        }

        public Serpent(int x, int y, int xspeed, int yspeed)
        {
            this._x = x;
            this._y = y;
            this._xspeed = xspeed;
            this._yspeed = yspeed;
        }

        public void update()
        {
            this._x = this._x + this._xspeed * SCALE;
            this._y = this._y + this._yspeed * SCALE;

            /* TODO: boundries */

        }
    }
}
