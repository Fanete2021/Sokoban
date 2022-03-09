using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game;

namespace Entities
{
    class PlaceBox: Entity
    {
        public PlaceBox(char name, Position position, int index) : base(name, position, index)
        {

        }

        public override void Action(Sokoban sokoban, Entity entity)
        {
            if (entity is Box)
                sokoban.CountInPlace++;
        }
    }
}
