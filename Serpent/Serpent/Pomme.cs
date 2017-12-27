using System.Drawing;

namespace Serpent {
	class Pomme {
		private Brush color = Brushes.Red;
		private int x;
		private int y;

		public Brush Color { get => color; set => color = value; }
		public int X { get => x; set => x = value; }
		public int Y { get => y; set => y = value; }

		public Pomme() {
			X = 0;
			Y = 0;
		}

		public Pomme(int X, int Y) {
			this.X = X;
			this.Y = Y;
		}

		public void Draw(Graphics canvas, int scaleWidth, int scaleHeight) {
			canvas.FillEllipse(Color, new Rectangle(X * Parametres.Width, Y * Parametres.Height, Parametres.Width, Parametres.Height));
		}
	}
}
