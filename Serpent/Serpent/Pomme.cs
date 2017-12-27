using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serpent {
	class Pomme {
		public int X { get; set; }
		public int Y { get; set; }

		public Pomme() {
			X = 0;
			Y = 0;
		}

		public Pomme(int X, int Y) {
			this.X = X;
			this.Y = Y;
		}
	}
}
