using System.Drawing;

namespace Serpent {
	class SpeedUp : Bonus {
		public SpeedUp() {
			X = 0;
			Y = 0;
		}

		public SpeedUp(int X, int Y, bool ForEnemy) {
			Color = Brushes.DeepPink;
			Name = "Speed Up";
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