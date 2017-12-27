using System.Collections.Generic;
using System.Drawing;

namespace Serpent {
	class Serpent {
		private Form1 form;
		private List<Position> body = new List<Position>();
		private Direction direction;
		private Brush color;
		private int index;

		public Direction Direction { get => direction; set => direction = value; }
		public List<Position> Body { get => body; set => body = value; }
		public Brush Color { get => color; set => color = value; }
		public int Index { get => index; set => index = value; }

		public Serpent(Form1 form, Brush color) {
			this.form = form;
			Position head = new Position(10, 5);
			Body.Add(head);
			Direction = Direction.Down;
			Color = color;
		}

		public Serpent(Form1 form, Brush color, Position position) {
			this.form = form;
			Position head = position;
			Body.Add(head);
			Direction = Direction.Down;
			Color = color;
		}

		public void Draw(Graphics canvas, int scaleWidth, int scaleHeight) {
			for (int i = 0; i < Body.Count; i++) {
				canvas.FillEllipse(Color,
					new Rectangle(Body[i].X * scaleWidth,
									Body[i].Y * scaleHeight,
									scaleWidth, scaleHeight));
			}
		}

		private void Eat() {
			Position bodyPart = new Position {
				X = Body[Body.Count - 1].X,
				Y = Body[Body.Count - 1].Y
			};
			Body.Add(bodyPart);
		}

		public void Move(Direction direction) {
			for (int i = Body.Count - 1; i >= 0; i--) {
				if (i == 0) {
					switch (direction) {
						case Direction.Right:
							Body[i].X++;
							break;
						case Direction.Left:
							Body[i].X--;
							break;
						case Direction.Up:
							Body[i].Y--;
							break;
						case Direction.Down:
							Body[i].Y++;
							break;
					}
					Direction = direction;

					// Border collision
					if (Body[i].X < 0 || Body[i].Y < 0 || 
						Body[i].X >= form.MaxWidth || Body[i].Y >= form.MaxHeight) {
						form.Die();
					}
					// Own body collision
					for (int j = 1; j < Body.Count; ++j) {
						if (Body[i].X == Body[j].X &&
						   Body[i].Y == Body[j].Y) {
							form.Die();
						}
					}
					// Serpents collision
					/*
					foreach (Serpent serpent in form.Players) {
						for (int j = 0; j < serpent.Body.Count; ++j) {
							if (Body[i].X == serpent.Body[j].X &&
							   Body[i].Y == serpent.Body[j].Y) {
								form.Die();
							}
						}
					}
					*/
					if (Index == 0) {
						Serpent secondPlayer = form.Players[1];
						for (int j = 0; j < secondPlayer.Body.Count; ++j) {
							if (Body[i].X == secondPlayer.Body[j].X &&
							   Body[i].Y == secondPlayer.Body[j].Y) {
								form.Die();
							}
						}
					}

					if (Index == 1) {
						Serpent firstPlayer = form.Players[0];
						for (int j = 0; j < firstPlayer.Body.Count; ++j) {
							if (Body[i].X == firstPlayer.Body[j].X &&
							   Body[i].Y == firstPlayer.Body[j].Y) {
								form.Die();
							}
						}
					}
					/*
					for (int j = 0; j < form.Players.Length; ++j) {
						if (Index != j) {
							for (int k = 0; j < form.Players[j].Body.Count; ++k) {
								if (Body[i].X == form.Players[j].Body[k].X &&
								   Body[i].Y == form.Players[j].Body[k].Y) {
									form.Die();
								}
							}
						}
					}
					*/

					// Pomme collision
					if (Body[0].X == form.Pomme.X && Body[0].Y == form.Pomme.Y) {
						Eat();
						form.GeneratePomme();
					}
				} else {
					Body[i].X = Body[i - 1].X;
					Body[i].Y = Body[i - 1].Y;
				}
			}
		}
	}
}
