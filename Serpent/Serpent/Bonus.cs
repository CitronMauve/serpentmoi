using System.Drawing;

namespace Serpent {
	abstract class Bonus {
		private Brush color;
		private int x;
		private int y;

		public Brush Color { get => color; set => color = value; }
		public int X { get => x; set => x = value; }
		public int Y { get => y; set => y = value; }

		public abstract void Draw(Graphics canvas, int scaleWidth, int scaleHeight);
	}
}
