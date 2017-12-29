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
		public static bool GameStarted { get; set; }
		public static bool GamePaused { get; set; }
		public static bool GameOver { get; set; }

		public Parametres() {
			Width = 10;
			Height = 10;
			Speed = 18;
			GameStarted = false;
			GamePaused = false;
			GameOver = false;
		}
	}
}
