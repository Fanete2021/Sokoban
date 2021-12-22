using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    static class Entities
    {
        static public List<Entity> CreateListEntities(List<string> map)
        {
            var entity = new List<Entity>();
            var index = 0;

            for (var position1 = 0; position1 < map.Count; ++position1)
            {
                for (var position2 = 0; position2 < map[position1].Length; ++position2)
                {
                    entity.Add(CreateEntity(map[position1][position2], new Position(position1, position2), index));
                    index++;
                }
            }

            return entity;
        }

        static public Entity CreateEntity(char entity, Position position, int index)
        {
            switch (entity)
            {
                case '+':
                    return new PlaceBox(entity, position, index);
                case 'o':
                    return new Box(entity, position, index);
                case '?':
                    return new Pit(entity, position, index);
                case '#':
                    return new Wall(entity, position, index);
                case 'p':
                    return Sokoban.player = new Player(entity, position, index);
                default:
                    return new Void(entity, position, index);
            }
        }
    }

    abstract class Entity
    {
        public char name;
        public Position position;
        public int index;
        abstract public void Action(Entity entity);
    }

    class Wall: Entity
    {
        public Wall(char name, Position position, int index)
        {
            this.name = name;
            this.position = position;
            this.index = index;
        }

        public override void Action(Entity entity)
        {

        }
    }

    class Player: Entity
    {
        public Direction directionMove = Direction.Nothing;

        public Player(char name, Position position, int index)
        {
            this.name = name;
            this.position = position;
            this.index = index;
        }

        public override void Action(Entity entity)
        {
            
        }

        public void MovePlayer(Position offsetPorition)
        {
            Sokoban.ChangeEntity(this.index, Sokoban.FindEntity(position + offsetPorition));
        }
    }

    class PlaceBox: Entity
    {
        public PlaceBox(char name, Position position, int index)
        {
            this.name = name;
            this.position = position;
            this.index = index;
        }

        public override void Action(Entity entity)
        {
            if (entity is Box)
            {
                Sokoban.countInPlace++;
            }
        }
    }

    class Box : Entity
    {
        public Box(char name, Position position, int index)
        {
            this.name = name;
            this.position = position;
            this.index = index;
            if (Sokoban.countMoves == 0)
                Sokoban.allBox++;
        }

        public override void Action(Entity entity)
        {
            var offsetPosition = Sokoban.DeterminePosition(Sokoban.player.directionMove);
            var indexSelectedEntity = Sokoban.FindEntity(position + offsetPosition);
            if (!(Sokoban.entities[indexSelectedEntity] is Wall) && !(Sokoban.entities[indexSelectedEntity] is Box))
            {
                Sokoban.entities[indexSelectedEntity].Action(this);
                Sokoban.ChangeEntity(Sokoban.FindEntity(this.position), Sokoban.FindEntity(position + offsetPosition));
                Sokoban.player.MovePlayer(offsetPosition);
                position += offsetPosition;
            }
        }
    }

    class Pit : Entity
    {
        public Pit(char name, Position position, int index)
        {
            this.name = name;
            this.position = position;
            this.index = index;
        }

        public override void Action(Entity entity)
        {
            
        }
    }

    class Void : Entity
    {
        public Void(char name, Position position, int index)
        {
            this.name = name;
            this.position = position;
            this.index = index;
        }

        public override void Action(Entity entity)
        {

        }
    }
}
