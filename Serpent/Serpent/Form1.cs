using System;
using System.Drawing;
using System.Windows.Forms;

namespace Serpent
{
    public partial class Form1 : Form {
		private int maxWidth;
		private int maxHeight;
		private Serpent firstPlayer;
		private Serpent secondPlayer;
		private Serpent[] players;
		// private Pomme pomme;
		private TouchedTiles touchedTiles;
		private String winner;

		public int MaxWidth { get => maxWidth; set => maxWidth = value; }
		public int MaxHeight { get => maxHeight; set => maxHeight = value; }
		internal Serpent FirstPlayer { get => firstPlayer; set => firstPlayer = value; }
		internal Serpent SecondPlayer { get => secondPlayer; set => secondPlayer = value; }
		internal Serpent[] Players { get => players; set => players = value; }
		// internal Pomme Pomme { get => pomme; set => pomme = value; }
		internal TouchedTiles TouchedTiles { get => touchedTiles; set => touchedTiles = value; }

		public Form1() {
			InitializeComponent();
			
			new Parametres();
			
			timer.Interval = 1000 / Parametres.Speed;
			timer.Tick += Update;
			timer.Start();

			MaxWidth = pictureBox.Size.Width / Parametres.Width;
			MaxHeight = pictureBox.Size.Height / Parametres.Height;

			StartGame();
		}

		private void StartGame() {
			label1.Visible = false;
			
			new Parametres();

			FirstPlayer = new Serpent(this, Brushes.Green, new Position(40, 40));
			FirstPlayer.Index = 0;
			FirstPlayer.Direction = Direction.Up;
			SecondPlayer = new Serpent(this, Brushes.Blue, new Position(10, 10));
			SecondPlayer.Index = 1;

			Players = new Serpent[2];
			Players[0] = FirstPlayer;
			Players[1] = SecondPlayer;

			TouchedTiles = new TouchedTiles();

			winner = "";

			// GeneratePomme();
		}

		/*
		public void GeneratePomme() {
			Random random = new Random();
			int randomX = random.Next(0, MaxWidth);
			int randomY = random.Next(0, MaxHeight);
			// Pomme = new Pomme(randomX, randomY);
		}
		*/


		private void Update(object sender, EventArgs e) {
			if (!Parametres.GameStarted || Parametres.GameOver) {
				if (Input.KeyPressed(Keys.Enter)) {
					StartGame();
					Parametres.GameStarted = true;
				}
			} else {
				// Update first player
				Direction firstPlayerDirection = FirstPlayer.Direction;
				if (Input.KeyPressed(Keys.Right) && firstPlayerDirection != Direction.Left) {
					firstPlayerDirection = Direction.Right;
				} else if (Input.KeyPressed(Keys.Left) && firstPlayerDirection != Direction.Right) {
					firstPlayerDirection = Direction.Left;
				} else if (Input.KeyPressed(Keys.Up) && firstPlayerDirection != Direction.Down) {
					firstPlayerDirection = Direction.Up;
				} else if (Input.KeyPressed(Keys.Down) && firstPlayerDirection != Direction.Up) {
					firstPlayerDirection = Direction.Down;
				}
				FirstPlayer.Move(firstPlayerDirection);

				// Update second player
				Direction secondPlayerDirection = SecondPlayer.Direction;
				if (Input.KeyPressed(Keys.D) && secondPlayerDirection != Direction.Left) {
					secondPlayerDirection = Direction.Right;
				} else if (Input.KeyPressed(Keys.A) && secondPlayerDirection != Direction.Right) {
					secondPlayerDirection = Direction.Left;
				} else if (Input.KeyPressed(Keys.W) && secondPlayerDirection != Direction.Down) {
					secondPlayerDirection = Direction.Up;
				} else if (Input.KeyPressed(Keys.S) && secondPlayerDirection != Direction.Up) {
					secondPlayerDirection = Direction.Down;
				}
				SecondPlayer.Move(secondPlayerDirection);

				// Update touched tiles
				TouchedTiles.Add(FirstPlayer.Body[0], FirstPlayer.Color);
				TouchedTiles.Add(SecondPlayer.Body[0], SecondPlayer.Color);
			}

			pictureBox.Invalidate();
		}

		private void Draw(object sender, PaintEventArgs e) {
			Graphics canvas = e.Graphics;

			if (Parametres.GameStarted && !Parametres.GameOver) {
				// Draw players
				for (int i = 0; i < Players.Length; ++i) {
					Players[i].Draw(canvas, Parametres.Width, Parametres.Height);
				}

				// Draw pomme
				// pomme.Draw(canvas, Parametres.Width, Parametres.Height);

				// Draw touched tiles
				TouchedTiles.Draw(canvas, Parametres.Width, Parametres.Height);

				// Draw grid
				int numOfCells = (pictureBox.Width * pictureBox.Height) / (Parametres.Width * Parametres.Height);
				int cellSize = Parametres.Width;
				Pen pen = new Pen(Color.Black);
				for (int i = 0; i < numOfCells; ++i) {
					canvas.DrawLine(pen, 0, i * cellSize, numOfCells * cellSize, i * cellSize);
					canvas.DrawLine(pen, i * cellSize, 0, i * cellSize, numOfCells * cellSize);
				}
			} else if (!Parametres.GameStarted && !Parametres.GameOver) {
				String endText = "Press Enter to play.\nWASD for Blue Player\nArrow keys for Green Player";
				label1.Text = endText;
				label1.Visible = true;
			} else if (!Parametres.GameStarted && Parametres.GameOver) {
				String endText = winner + " player wins.\nPress Enter to play again.";
				label1.Text = endText;
				label1.Visible = true;
			}
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e) {
			Input.ChangeState(e.KeyCode, true);
		}

		private void Form1_KeyUp(object sender, KeyEventArgs e) {
			Input.ChangeState(e.KeyCode, false);
		}

		public void Die() {
			if (Players[0].Alive && !Players[1].Alive) {
				winner = "Green";
			} else if (!Players[0].Alive && Players[1].Alive) {
				winner = "Blue";
			}
			Parametres.GameStarted = false;
			Parametres.GameOver = true;
		}
	}
}
