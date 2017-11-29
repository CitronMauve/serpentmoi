using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serpent
{
    class Serpent
    {
        // Position
        private Position position;

        // Vitesse
        private Position vitesse;

        // Taille
        private int total;

        private Position[] tail;

        public Serpent(Position position, Position vitesse)
        {
            this.position = position;
            this.vitesse = vitesse;
            this.tail = new Position[0];
        }

        public bool Eat(Position position)
        {
            double distance = this.position.Distance(position);

            if (distance < 1)
            {
                ++this.total;
                return true;
            } else
            {
                return false;
            }
        }

        public void Dir(Position vitesse)
        {
            this.vitesse = vitesse;
        }

        public void Death()
        {
            for (int i = 0; i < this.tail.Length; ++i)
            {
                Position position = this.tail[i];
                double distance = this.position.Distance(position);

                if (distance < 1)
                {
                    this.total = 0;
                    this.tail = new Position[0];
                }
            }
        }

        public void Update()
        {
            for (int i = 0; i < this.tail.Length - 1; ++i)
            {
                this.tail[i] = this.tail.[i + 1];
            }

            if (this.total >= 1)
            {
                this.tail[this.total - 1] = new Position(this.position);
            }

            this.position.X += (this.vitesse.X * SCALE);
            this.position.Y += (this.vitesse.Y * SCALE);
        }

        public void Show()
        {
            for (int i = 0; i < this.tail.Length; ++i)
            {

            }
        }

    }
}
