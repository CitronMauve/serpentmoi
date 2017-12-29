using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Serpent {
	class Serpent {
		private Form1 form;
		private Position head;
		// private List<Position> body = new List<Position>();
		private Direction direction;
		private Brush color;
		private int index;
		private bool alive;
		// private HashSet<Bonus> activeBonus;
		private Dictionary<string, int> activeBonus;

		internal Position Head { get => head; set => head = value; }
		public Direction Direction { get => direction; set => direction = value; }
		// public List<Position> Body { get => body; set => body = value; }
		public Brush Color { get => color; set => color = value; }
		public int Index { get => index; set => index = value; }
		public bool Alive { get => alive; set => alive = value; }
		internal Dictionary<string, int> ActiveBonus { get => activeBonus; set => activeBonus = value; }

		// internal HashSet<Bonus> ActiveBonus { get => activeBonus; set => activeBonus = value; }

		public Serpent(Form1 form, Brush color, Position position) {
			this.form = form;
			Head = position;
			// Body.Add(head);
			Direction = Direction.Down;
			Color = color;
			Alive = true;
			// ActiveBonus = new HashSet<Bonus>();
			InitializeActiveBonus();
		}

		private void InitializeActiveBonus() {
			ActiveBonus = new Dictionary<string, int>();
			foreach (EnumBonus bonus in Enum.GetValues(typeof(EnumBonus))) {
				ActiveBonus.Add(bonus.ToString(), 0);
			}
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
			if (ActiveBonus["Inverse"] > 0) {
				InverseDirection(direction);
			} else {
				Direction = direction;
			}
			switch (Direction) {
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

			Collisions();
		}

		private void InverseDirection(Direction direction) {
			switch (direction) {
				case Direction.Right:
					Direction = Direction.Left;
					break;
				case Direction.Left:
					Direction = Direction.Right;
					break;
				case Direction.Up:
					Direction = Direction.Down;
					break;
				case Direction.Down:
					Direction = Direction.Up;
					break;
			}
		}

		private void Collisions() {
			if (ActiveBonus["Invincibility"] > 0) {
				BonusCollision();
			} else {
				BorderCollision();
				SerpentCollision();
				TileCollision();
				BonusCollision();
			}
		}
		
		private void BonusCollision() {
			if (form.Bonuss.Count != 0) {
				for (int i = 0; i < form.Bonuss.Count; ++i) {
					if (Head.X == form.Bonuss[i].X && Head.Y == form.Bonuss[i].Y) {
						if (form.Bonuss[i].ForEnemy) {
							if (Index == 0) {
								form.Players[1].AddBonus(form.Bonuss[i]);
							} else if (Index == 1) {
								form.Players[0].AddBonus(form.Bonuss[i]);
							}
						} else {
							AddBonus(form.Bonuss[i]);
						}
						form.Bonuss.RemoveAt(i);
						break;
					}
				}
			}
		}

		private void AddBonus(Bonus bonus) {
			ActiveBonus[bonus.Name] = bonus.MaxDuration;
		}

		public void UpdateActiveBonus() {
			foreach (string key in ActiveBonus.Keys.ToList()) {
				if (ActiveBonus[key] > 0) {
					ActiveBonus[key] = ActiveBonus[key] - 1;
					Console.WriteLine(Color + " bonus " + key + ": " + ActiveBonus[key]);
				}
			}
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
