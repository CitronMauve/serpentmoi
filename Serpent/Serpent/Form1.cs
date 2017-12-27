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
		private Pomme pomme;

		public int MaxWidth { get => maxWidth; set => maxWidth = value; }
		public int MaxHeight { get => maxHeight; set => maxHeight = value; }
		internal Serpent FirstPlayer { get => firstPlayer; set => firstPlayer = value; }
		internal Serpent SecondPlayer { get => secondPlayer; set => secondPlayer = value; }
		internal Serpent[] Players { get => players; set => players = value; }
		internal Pomme Pomme { get => pomme; set => pomme = value; }

		public Form1() {
			InitializeComponent();
			
			new Parametres();
			
			timer.Interval = 1000 / Parametres.Speed;
			timer.Tick += UpdateScreen;
			timer.Start();

			MaxWidth = pictureBox.Size.Width / Parametres.Width;
			MaxHeight = pictureBox.Size.Height / Parametres.Height;

			StartGame();
		}

		private void StartGame() {
			// lblGameOver.Visible = false;
			
			new Parametres();

			FirstPlayer = new Serpent(this, Brushes.Green, new Position(10, 10));
			FirstPlayer.Index = 0;
			SecondPlayer = new Serpent(this, Brushes.Blue, new Position(40, 40));
			SecondPlayer.Index = 1;
			SecondPlayer.Direction = Direction.Up;

			Players = new Serpent[2];
			Players[0] = FirstPlayer;
			Players[1] = SecondPlayer;

			GeneratePomme();
		}

		public void GeneratePomme() {
			Random random = new Random();
			int randomX = random.Next(0, MaxWidth);
			int randomY = random.Next(0, MaxHeight);
			Pomme = new Pomme(randomX, randomY);
		}


		private void UpdateScreen(object sender, EventArgs e) {
			if (Parametres.GameOver) {
				if (Input.KeyPressed(Keys.Enter)) {
					StartGame();
				}
			} else {
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
			}

			pictureBox.Invalidate();
		}

		private void pbCanvas_Paint(object sender, PaintEventArgs e) {
			Graphics canvas = e.Graphics;

			if (!Parametres.GameOver) {
				// Draw players
				for (int i = 0; i < Players.Length; ++i) {
					Players[i].Draw(canvas, Parametres.Width, Parametres.Height);
				}

				// Draw Pomme
				pomme.Draw(canvas, Parametres.Width, Parametres.Height);
			} else {
				/*
				lblGameOver.Text = gameOver;
				lblGameOver.Visible = true;
				*/
			}
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e) {
			Input.ChangeState(e.KeyCode, true);
		}

		private void Form1_KeyUp(object sender, KeyEventArgs e) {
			Input.ChangeState(e.KeyCode, false);
		}

		public void Die() {
			Parametres.GameOver = true;
		}
	}
}
