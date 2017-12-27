using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Serpent
{
    public partial class Form1 : Form {
		private List<Circle> Snake = new List<Circle>();
		private Circle food = new Circle();

		public Form1() {
			InitializeComponent();
			
			new Parametres();
			
			timer.Interval = 1000 / Parametres.Speed;
			timer.Tick += UpdateScreen;
			timer.Start();
			
			StartGame();
		}

		private void StartGame() {
			// lblGameOver.Visible = false;

			//Set settings to default
			new Parametres();

			//Create new player object
			Snake.Clear();
			Circle head = new Circle { X = 10, Y = 5 };
			Snake.Add(head);

			
			GenerateFood();
		}

		//Place random food object
		private void GenerateFood() {
			int maxXPos = pictureBox.Size.Width / Parametres.Width;
			int maxYPos = pictureBox.Size.Height / Parametres.Height;

			Random random = new Random();
			food = new Circle { X = random.Next(0, maxXPos), Y = random.Next(0, maxYPos) };
		}


		private void UpdateScreen(object sender, EventArgs e) {
			if (Parametres.GameOver) {
				if (Input.KeyPressed(Keys.Enter)) {
					StartGame();
				}
			} else {
				if (Input.KeyPressed(Keys.Right) && Parametres.Direction != Direction.Left) {
					Parametres.Direction = Direction.Right;
				} else if (Input.KeyPressed(Keys.Left) && Parametres.Direction != Direction.Right) {
					Parametres.Direction = Direction.Left;
				} else if (Input.KeyPressed(Keys.Up) && Parametres.Direction != Direction.Down) {
					Parametres.Direction = Direction.Up;
				} else if (Input.KeyPressed(Keys.Down) && Parametres.Direction != Direction.Up) {
					Parametres.Direction = Direction.Down;
				}

				MovePlayer();
			}

			pictureBox.Invalidate();
		}

		private void pbCanvas_Paint(object sender, PaintEventArgs e) {
			Graphics canvas = e.Graphics;

			if (!Parametres.GameOver) {
				for (int i = 0; i < Snake.Count; i++) {
					Brush snakeColour;
					if (i == 0) {
						snakeColour = Brushes.Black;
					}
					else {
						snakeColour = Brushes.Green;
					}

					//Draw snake
					canvas.FillEllipse(snakeColour, new Rectangle(Snake[i].X * Parametres.Width, Snake[i].Y * Parametres.Height, Parametres.Width, Parametres.Height));

					//Draw Food
					canvas.FillEllipse(Brushes.Red, new Rectangle(food.X * Parametres.Width, food.Y * Parametres.Height, Parametres.Width, Parametres.Height));
				}
			} else {
				/*
				lblGameOver.Text = gameOver;
				lblGameOver.Visible = true;
				*/
			}
		}


		private void MovePlayer() {
			for (int i = Snake.Count - 1; i >= 0; i--) {
				//Move head
				if (i == 0) {
					switch (Parametres.Direction) {
						case Direction.Right:
							Snake[i].X++;
							break;
						case Direction.Left:
							Snake[i].X--;
							break;
						case Direction.Up:
							Snake[i].Y--;
							break;
						case Direction.Down:
							Snake[i].Y++;
							break;
					}

					//Get maximum X and Y Pos
					int maxXPos = pictureBox.Size.Width / Parametres.Width;
					int maxYPos = pictureBox.Size.Height / Parametres.Height;

					//Detect collission with game borders.
					if (Snake[i].X < 0 || Snake[i].Y < 0 || Snake[i].X >= maxXPos || Snake[i].Y >= maxYPos) {
						Die();
					}

					//Detect collission with body
					for (int j = 1; j < Snake.Count; j++) {
						if (Snake[i].X == Snake[j].X &&
						   Snake[i].Y == Snake[j].Y) {
							Die();
						}
					}

					//Detect collision with food piece
					if (Snake[0].X == food.X && Snake[0].Y == food.Y) {
						Eat();
					}

				} else {
					//Move body
					Snake[i].X = Snake[i - 1].X;
					Snake[i].Y = Snake[i - 1].Y;
				}
			}
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e) {
			Input.ChangeState(e.KeyCode, true);
		}

		private void Form1_KeyUp(object sender, KeyEventArgs e) {
			Input.ChangeState(e.KeyCode, false);
		}

		private void Eat() {
			//Add circle to body
			Circle circle = new Circle {
				X = Snake[Snake.Count - 1].X,
				Y = Snake[Snake.Count - 1].Y
			};
			Snake.Add(circle);

			//Update Score
			Parametres.Score++;

			GenerateFood();
		}

		private void Die() {
			Parametres.GameOver = true;
		}
	}
}
