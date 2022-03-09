using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game;

namespace Entities
{
    class Box: Entity
    {
        public Box(char name, Position position, int index) : base(name, position, index)
        {

        }

        public override void Action(Sokoban sokoban, Entity entity)
        {
            var offsetPosition = Direction.DeterminePosition(sokoban.SokobanPlayer.DirectionMove);
            var indexSelectedEntity = sokoban.FindEntity(Position + offsetPosition);

            if (!(sokoban.ListEntities[indexSelectedEntity] is Wall) && !(sokoban.ListEntities[indexSelectedEntity] is Box))
            {
                sokoban.ListEntities[indexSelectedEntity].Action(sokoban, this);
                EntityEdtior.ChangeEntities(sokoban, this, sokoban.ListEntities[indexSelectedEntity]);
                sokoban.SokobanPlayer.MovePlayer(sokoban, offsetPosition);
            }
        }
    }
}
