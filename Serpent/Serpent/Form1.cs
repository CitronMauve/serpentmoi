using System;
using System.Collections.Generic;
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
		private List<Bonus> bonuss;
		private String winner;

		public int MaxWidth { get => maxWidth; set => maxWidth = value; }
		public int MaxHeight { get => maxHeight; set => maxHeight = value; }
		internal Serpent FirstPlayer { get => firstPlayer; set => firstPlayer = value; }
		internal Serpent SecondPlayer { get => secondPlayer; set => secondPlayer = value; }
		internal Serpent[] Players { get => players; set => players = value; }
		// internal Pomme Pomme { get => pomme; set => pomme = value; }
		internal TouchedTiles TouchedTiles { get => touchedTiles; set => touchedTiles = value; }
		internal List<Bonus> Bonuss { get => bonuss; set => bonuss = value; }

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

			FirstPlayer = new Serpent(this, Brushes.Green, new Position(maxWidth - (2 * Parametres.Width), maxHeight - (2 * Parametres.Height)));
			FirstPlayer.Index = 0;
			FirstPlayer.Direction = Direction.Up;
			SecondPlayer = new Serpent(this, Brushes.Blue, new Position(2 * Parametres.Width, 2 * Parametres.Height));
			SecondPlayer.Index = 1;

			Players = new Serpent[2];
			Players[0] = FirstPlayer;
			Players[1] = SecondPlayer;

			Bonuss = new List<Bonus>();

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

		private void GenerateBonus() {
			Random random = new Random();
			bool generate = random.Next(1, 101) >= 99;

			if (generate) {
				Array listBonus = Enum.GetValues(typeof(EnumBonus));
				EnumBonus enumBonus = (EnumBonus) listBonus.GetValue(random.Next(listBonus.Length));

				Bonus bonus;
				int randomX = random.Next(0, MaxWidth);
				int randomY = random.Next(0, MaxHeight);
				bool forEnemy = random.Next(0, 2) == 1;
				switch (enumBonus) {
					case EnumBonus.Inverse:
						bonus = new Inverse(randomX, randomY, forEnemy); 
						break;
					case EnumBonus.Invincibility:
						bonus = new Invincibility(randomX, randomY, forEnemy);
						break;
					case EnumBonus.SpeedUp:
						bonus = new SpeedUp(randomX, randomY, forEnemy);
						break;
					case EnumBonus.SpeedDown:
						bonus = new SpeedDown(randomX, randomY, forEnemy);
						break;
					case EnumBonus.Pomme:
						bonus = new Pomme(randomX, randomY, forEnemy);
						break;
					default:
						bonus = new Inverse(randomX, randomY, forEnemy);
						break;
				}

				Bonuss.Add(bonus);
			}
		}

		private void Update(object sender, EventArgs e) {
			if (!Parametres.GameStarted || Parametres.GamePaused || Parametres.GameOver) {
				if (Input.KeyPressed(Keys.Enter)) {
					StartGame();
					Parametres.GameStarted = true;
				}
				if (Input.KeyPressed(Keys.Space)) {
					Parametres.GamePaused = false;
				}
			} else if (Parametres.GameStarted && !Parametres.GamePaused && !Parametres.GameOver) {
				if (Input.KeyPressed(Keys.Space)) {
					Parametres.GamePaused = true;
				}

				GenerateBonus();


				// Update first player
				FirstPlayer.UpdateActiveBonus();
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
				SecondPlayer.UpdateActiveBonus();
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
				TouchedTiles.Add(FirstPlayer.Head, FirstPlayer.Color);
				TouchedTiles.Add(SecondPlayer.Head, SecondPlayer.Color);
			}

			pictureBox.Invalidate();
		}

		private void Draw(object sender, PaintEventArgs e) {
			Graphics canvas = e.Graphics;

			if (Parametres.GameStarted && !Parametres.GamePaused && !Parametres.GameOver) {
				label1.Visible = false;

				// Draw players
				for (int i = 0; i < Players.Length; ++i) {
					Players[i].Draw(canvas, Parametres.Width, Parametres.Height);
				}

				// Draw pomme
				// pomme.Draw(canvas, Parametres.Width, Parametres.Height);

				// Draw touched tiles
				TouchedTiles.Draw(canvas, Parametres.Width, Parametres.Height);

				// Draw bonuss
				foreach (Bonus bonus in Bonuss) {
					bonus.Draw(canvas, Parametres.Width, Parametres.Height);
				}

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
			} else if (Parametres.GameStarted && Parametres.GamePaused && !Parametres.GameOver) {
				String endText = "Press the Spacebar key to unpause the game.";
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
