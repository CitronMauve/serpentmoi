using System.Drawing;

namespace Serpent {
	class Invincibility : Bonus {
		public Invincibility() {
			X = 0;
			Y = 0;
		}

		public Invincibility(int X, int Y, bool ForEnemy) {
			Color = Brushes.DarkOrange;
			Name = "Invincibility";
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
