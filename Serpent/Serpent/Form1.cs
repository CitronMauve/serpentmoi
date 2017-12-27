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
		private Circle food = new Circle();

		public int MaxWidth { get => maxWidth; set => maxWidth = value; }
		public int MaxHeight { get => maxHeight; set => maxHeight = value; }
		public Circle Food { get => food; set => food = value; }

		public Form1() {
			InitializeComponent();
			
			new Parametres();
			
			timer.Interval = 1000 / Parametres.Speed;
			timer.Tick += UpdateScreen;
			timer.Start();
			
			StartGame();

			MaxWidth = pictureBox.Size.Width / Parametres.Width;
			MaxHeight = pictureBox.Size.Height / Parametres.Height;
		}

		private void StartGame() {
			// lblGameOver.Visible = false;
			
			new Parametres();

			firstPlayer = new Serpent(this, Brushes.Green);
			secondPlayer = new Serpent(this, Brushes.Blue);
			
			GenerateFood();
		}
		
		public void GenerateFood() {
			Random random = new Random();
			Food = new Circle { X = random.Next(0, MaxWidth), Y = random.Next(0, MaxHeight) };
		}


		private void UpdateScreen(object sender, EventArgs e) {
			if (Parametres.GameOver) {
				if (Input.KeyPressed(Keys.Enter)) {
					StartGame();
				}
			} else {
				Direction firstPlayerDirection = firstPlayer.Direction;
				if (Input.KeyPressed(Keys.Right) && firstPlayerDirection != Direction.Left) {
					firstPlayerDirection = Direction.Right;
				} else if (Input.KeyPressed(Keys.Left) && firstPlayerDirection != Direction.Right) {
					firstPlayerDirection = Direction.Left;
				} else if (Input.KeyPressed(Keys.Up) && firstPlayerDirection != Direction.Down) {
					firstPlayerDirection = Direction.Up;
				} else if (Input.KeyPressed(Keys.Down) && firstPlayerDirection != Direction.Up) {
					firstPlayerDirection = Direction.Down;
				}

				firstPlayer.Move(firstPlayerDirection);
			}

			pictureBox.Invalidate();
		}

		private void pbCanvas_Paint(object sender, PaintEventArgs e) {
			Graphics canvas = e.Graphics;

			if (!Parametres.GameOver) {
				firstPlayer.Draw(canvas, Parametres.Width, Parametres.Height);

				//Draw Food
				canvas.FillEllipse(Brushes.Red, new Rectangle(Food.X * Parametres.Width, Food.Y * Parametres.Height, Parametres.Width, Parametres.Height));
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
