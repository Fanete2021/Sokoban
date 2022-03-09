using Game;
using Game.Graphic;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    static class EntityEdtior
    {
        public static List<Entity> CreateListEntities(List<string> map)
        {
            var entity = new List<Entity>();
            var index = 0;

            for (var y = 0; y < map.Count; ++y)
                for (var x = 0; x < map[y].Length; ++x)
                {
                    entity.Add(CreateEntity(map[y][x], new Position(y, x), index));
                    index++;
                }

            return entity;
        }

        public static Entity CreateEntity(char entity, Position position, int index)
        {
            return char.ToLower(entity) switch
            {
                '+' => new PlaceBox(entity, position, index),
                'o' => new Box(entity, position, index),
                '?' => new Pit(entity, position, index),
                '#' => new Wall(entity, position, index),
                'p' => new Player(entity, position, index),
                _ => new Void(entity, position, index),
            };
        }
        public static void ChangeEntities(Sokoban sokoban, Entity movingEntity, Entity interactingEntity)
        {
            if (sokoban.ListEntities[interactingEntity.Index] is PlaceBox)
                sokoban.ListEntities[interactingEntity.Index] = EntityEdtior.CreateEntity(char.ToUpper(movingEntity.Name), interactingEntity.Position, interactingEntity.Index);
            else if (!(interactingEntity is Pit))
                sokoban.ListEntities[interactingEntity.Index] = EntityEdtior.CreateEntity(char.ToLower(movingEntity.Name), interactingEntity.Position, interactingEntity.Index);

            if (movingEntity is Player)
                sokoban.SokobanPlayer = (Player)sokoban.ListEntities[interactingEntity.Index];

            if (char.IsUpper(movingEntity.Name))
            {
                if (movingEntity is Box)
                    sokoban.CountInPlace--;
                sokoban.ListEntities[movingEntity.Index] = EntityEdtior.CreateEntity('+', movingEntity.Position, movingEntity.Index);
            }
            else
                sokoban.ListEntities[movingEntity.Index] = EntityEdtior.CreateEntity(' ', movingEntity.Position, movingEntity.Index);

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
    abstract class Entity: Transformable, Drawable
    {
        public char Name;
        public Position Position { get; }
        public int Index { get; }
        public Texture Texture;

        private RectangleShape rectShape;
        private int rectShapeSize = 100;

        public Entity(char name, Position position, int index)
        {
            Name = name;
            Position = position;
            Index = index;

            var typeEntity = this.GetType().ToString().Replace("Entities.", "");
            Texture = Textures.GetTexture(typeEntity);

            rectShape = new RectangleShape(new Vector2f(rectShapeSize, rectShapeSize));
            rectShape.Position = new Vector2f(this.Position.X * 100, this.Position.Y * 100);
            rectShape.Texture = Texture;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            target.Draw(rectShape, states);
        }

        public virtual void Action(Sokoban sokoban, Entity interactingEntity)
        {

        }
    }
}
