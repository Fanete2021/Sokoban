using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game;

namespace Entities
{
    class Player: Entity
    {
        public Directions DirectionMove = Directions.Nothing;
        public Player(char name, Position position, int index) : base(name, position, index)
        {

        }

        public void MovePlayer(Sokoban sokoban, Position offsetPosition)
        {
            var indexSelectedEntity = sokoban.FindEntity(Position + offsetPosition);
            EntityEdtior.ChangeEntities(sokoban, this, sokoban.ListEntities[indexSelectedEntity]);
        }
    }
}
