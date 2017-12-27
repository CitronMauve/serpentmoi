using System;
namespace Serpent {
	class Position {
		public int X { get; set; }
		public int Y { get; set; }

		public Position() {
			X = 0;
			Y = 0;
		}

		public Position(int X, int Y) {
			this.X = X;
			this.Y = Y;
		}
	}
}
