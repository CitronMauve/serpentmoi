using System.Drawing;

namespace Serpent {
	abstract class Bonus {
		private Brush color;
		private int x;
		private int y;
		private int duration;
		bool forEnemy;

		public Brush Color { get => color; set => color = value; }
		public int X { get => x; set => x = value; }
		public int Y { get => y; set => y = value; }
		public int Duration { get => duration; set => duration = value; }
		public bool ForEnemy { get => forEnemy; set => forEnemy = value; }

		public virtual void Draw(Graphics canvas, int scaleWidth, int scaleHeight) {
			canvas.FillEllipse(Brushes.Black,
				new Rectangle(X * Parametres.Width, Y * Parametres.Height, Parametres.Width, Parametres.Height));
		}
	}
}
