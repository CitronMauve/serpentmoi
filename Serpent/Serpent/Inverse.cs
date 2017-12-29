using System;
using System.Drawing;

namespace Serpent {
	class Inverse : Bonus {
		public Inverse() {
			X = 0;
			Y = 0;
		}

		public Inverse(int X, int Y) {
			this.X = X;
			this.Y = Y;
		}

		public override void Draw(Graphics canvas, int scaleWidth, int scaleHeight) {
			throw new NotImplementedException();
		}
	}
}
