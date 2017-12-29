using System.Drawing;

namespace Serpent {
	class Serpent {
		private Form1 form;
		private Position head;
		// private List<Position> body = new List<Position>();
		private Direction direction;
		private Brush color;
		private int index;
		private bool alive;

		internal Position Head { get => head; set => head = value; }
		public Direction Direction { get => direction; set => direction = value; }
		// public List<Position> Body { get => body; set => body = value; }
		public Brush Color { get => color; set => color = value; }
		public int Index { get => index; set => index = value; }
		public bool Alive { get => alive; set => alive = value; }

		public Serpent(Form1 form, Brush color, Position position) {
			this.form = form;
			Head = position;
			// Body.Add(head);
			Direction = Direction.Down;
			Color = color;
			Alive = true;
		}

		public void Draw(Graphics canvas, int scaleWidth, int scaleHeight) {
			canvas.FillEllipse(Color,
				new Rectangle(Head.X * scaleWidth, Head.Y * scaleHeight, scaleWidth, scaleHeight));
		}

		/*
		private void Eat() {
			Position bodyPart = new Position {
				X = Body[Body.Count - 1].X,
				Y = Body[Body.Count - 1].Y
			};
			Body.Add(bodyPart);
		}
		*/

		public void Move(Direction direction) {
			switch (direction) {
				case Direction.Right:
					Head.X++;
					break;
				case Direction.Left:
					Head.X--;
					break;
				case Direction.Up:
					Head.Y--;
					break;
				case Direction.Down:
					Head.Y++;
					break;
			}
			Direction = direction;

			Collisions();
		}

		private void Collisions() {
			BorderCollision();
			SerpentCollision();
			TileCollision();
			// Pomme collision
			/*
			if (Body[0].X == form.Pomme.X && Body[0].Y == form.Pomme.Y) {
				Eat();
				form.GeneratePomme();
			}
			*/
		}

		private void TileCollision() {
			Position position = new Position(Head.X, Head.Y);
			if (form.TouchedTiles.Tiles.ContainsKey(position)) {
				Alive = false;
				form.Die();
			}
		}

		private void SerpentCollision() {
			if (Index == 0) {
				Serpent secondPlayer = form.Players[1];
				if (Head.X == secondPlayer.Head.X &&
					Head.Y == secondPlayer.Head.Y) {
					Alive = false;
					form.Die();
				}
			}
			if (Index == 1) {
				Serpent firstPlayer = form.Players[0];
				if (Head.X == firstPlayer.Head.X &&
					Head.Y == firstPlayer.Head.Y) {
					Alive = false;
					form.Die();
				}
			}
		}

		private void BorderCollision() {
			if (Head.X < 0 || Head.Y < 0 ||
							Head.X >= form.MaxWidth || Head.Y >= form.MaxHeight) {
				Alive = false;
				form.Die();
			}
		}
	}
}
