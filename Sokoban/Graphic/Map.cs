using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Game;

namespace Graphic
{
    class Map : Transformable, Drawable
    {
        private readonly List<Entity> ListEntities;
        public Map(Sokoban sokoban)
        {
            ListEntities = sokoban.ListEntities;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;

            foreach(var entity in ListEntities)
                target.Draw(entity, states);
        }
    }
}
