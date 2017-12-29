using System.Drawing;

namespace Serpent {
	abstract class Bonus {
		private Brush color;
		private int x;
		private int y;
		private int remainingDuration = 1000;
		private int maxDuration = 1000;
		bool forEnemy;

		public Brush Color { get => color; set => color = value; }
		public int X { get => x; set => x = value; }
		public int Y { get => y; set => y = value; }
		public int RemainingDuration { get => remainingDuration; set => remainingDuration = value; }
		public int MaxDuration { get => maxDuration; set => maxDuration = value; }
		public bool ForEnemy { get => forEnemy; set => forEnemy = value; }

		public virtual void Draw(Graphics canvas, int scaleWidth, int scaleHeight) {
			canvas.FillEllipse(Brushes.Black,
				new Rectangle(X * Parametres.Width, Y * Parametres.Height, Parametres.Width, Parametres.Height));
		}
	}
}
