using System.Collections.Generic;
using System.Drawing;

namespace Serpent {
	class Serpent {
		private Form1 form;
		private List<Circle> Body = new List<Circle>();
		private Direction direction;
		private Brush color;

		public Direction Direction { get => direction; set => direction = value; }
		public List<Circle> Body1 { get => Body; set => Body = value; }


		public Serpent(Form1 form, Brush color) {
			this.form = form;
			Circle head = new Circle { X = 10, Y = 5 };
			Body1.Add(head);
			Direction = Direction.Down;
			this.color = color;
		}

		public void Draw(Graphics canvas, int scaleWidth, int scaleHeight) {
			for (int i = 0; i < Body1.Count; i++) {
				canvas.FillEllipse(color,
					new Rectangle(Body1[i].X * scaleWidth,
									Body1[i].Y * scaleHeight,
									scaleWidth, scaleHeight));
			}
		}

		private void Eat() {
			Circle circle = new Circle {
				X = Body1[Body1.Count - 1].X,
				Y = Body1[Body1.Count - 1].Y
			};
			Body1.Add(circle);
		}

		public void Move(Direction direction) {
			for (int i = Body1.Count - 1; i >= 0; i--) {
				if (i == 0) {
					switch (direction) {
						case Direction.Right:
							Body1[i].X++;
							break;
						case Direction.Left:
							Body1[i].X--;
							break;
						case Direction.Up:
							Body1[i].Y--;
							break;
						case Direction.Down:
							Body1[i].Y++;
							break;
					}
					Direction = direction;

					// Border collision
					if (Body1[i].X < 0 || Body1[i].Y < 0 || 
						Body1[i].X >= form.MaxWidth || Body1[i].Y >= form.MaxHeight) {
						form.Die();
					}
					// Own body collision
					for (int j = 1; j < Body1.Count; j++) {
						if (Body1[i].X == Body1[j].X &&
						   Body1[i].Y == Body1[j].Y) {
							form.Die();
						}
					}
					// Food collision
					if (Body1[0].X == form.Food.X && Body1[0].Y == form.Food.Y) {
						Eat();
						form.GenerateFood();
					}
				} else {
					Body1[i].X = Body1[i - 1].X;
					Body1[i].Y = Body1[i - 1].Y;
				}
			}
		}
	}
}
