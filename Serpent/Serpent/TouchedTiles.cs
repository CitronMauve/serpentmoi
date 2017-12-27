using System.Collections.Generic;
using System.Drawing;

namespace Serpent {
	class TouchedTiles {
		private Dictionary<Position, Brush> tiles;
		internal Dictionary<Position, Brush> Tiles { get => tiles; set => tiles = value; }

		public TouchedTiles() {
			Tiles = new Dictionary<Position, Brush>();
		}

		public void Draw(Graphics canvas, int scaleWidth, int scaleHeight) {
			foreach (KeyValuePair<Position, Brush> brush in Tiles) {
				canvas.FillRectangle(brush.Value, 
					new Rectangle(brush.Key.X * scaleWidth, brush.Key.Y * scaleHeight, scaleWidth, scaleHeight));
			}
		}

		public void Add(Position position, Brush color) {
			if (Tiles.ContainsKey(position)) {
			} else {
				Position newPosition = new Position(position.X, position.Y);
				Tiles.Add(newPosition, color);
			}
		}
	}
}
