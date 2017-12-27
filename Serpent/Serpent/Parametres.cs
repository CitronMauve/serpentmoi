using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serpent {
		public enum Direction {
			Up,
			Down,
			Left,
			Right
		};

		public class Parametres {
			public static int Width { get; set; }
			public static int Height { get; set; }
			public static int Speed { get; set; }
			public static int Score { get; set; }
			public static bool GameOver { get; set; }
			public static Direction Direction { get; set; }

			public Parametres() {
				Width = 16;
				Height = 16;
				Speed = 16;
				Score = 0;
				GameOver = false;
				Direction = Direction.Down;
			}
		}
}
