using System.Drawing;

namespace Serpent {
	class Inverse : Bonus {
		public Inverse() {
			X = 0;
			Y = 0;
		}

		public Inverse(int X, int Y, bool ForEnemy) {
			Color = Brushes.DarkMagenta;
			this.X = X;
			this.Y = Y;
			this.ForEnemy = ForEnemy;
		}

		public override void Draw(Graphics canvas, int scaleWidth, int scaleHeight) {
			canvas.FillEllipse(Color,
				new Rectangle(X * Parametres.Width, Y * Parametres.Height, Parametres.Width, Parametres.Height));
		}
	}
}
