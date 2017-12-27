using System.Collections.Generic;

namespace Serpent {
	class Position {
		public int X { get; set; }
		public int Y { get; set; }

		public Position() {
			X = 0;
			Y = 0;
		}

		public Position(int X, int Y) {
			this.X = X;
			this.Y = Y;
		}

		public override bool Equals(object obj) {
			var position = obj as Position;
			return position != null &&
				   X == position.X &&
				   Y == position.Y;
		}

		public override int GetHashCode() {
			var hashCode = 1861411795;
			hashCode = hashCode * -1521134295 + X.GetHashCode();
			hashCode = hashCode * -1521134295 + Y.GetHashCode();
			return hashCode;
		}

		public static bool operator ==(Position position1, Position position2) {
			return EqualityComparer<Position>.Default.Equals(position1, position2);
		}

		public static bool operator !=(Position position1, Position position2) {
			return !(position1 == position2);
		}
	}
}
