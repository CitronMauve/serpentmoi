﻿using System.Drawing;

namespace Serpent {
	class Pomme : Bonus {
		public Pomme() {
			Color = Brushes.Red;
			X = 0;
			Y = 0;
		}

		public Pomme(int X, int Y, bool ForEnemy) {
			Color = Brushes.Red;
			Name = "Pomme";
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
