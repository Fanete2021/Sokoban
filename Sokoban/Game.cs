using Graphic;
using System;
using System.Collections.Generic;
using Entities;
using SFML.Window;
using static SFML.Window.Keyboard;

namespace Game
{
    public enum ActResult
    {
        Win,
        Defeat,
        Nothing
    }

    class Sokoban
    {
        public Player SokobanPlayer { get; set; }
        public List<Entity> ListEntities { get; set; }
        public int CountMoves = 0, CountInPlace;
        public readonly int CountBox;

        public Sokoban(List<string> map)
        {
            ListEntities = EntityEdtior.CreateListEntities(map);
            CountBox = CalculateBox();
            SokobanPlayer = FindPlayer();
        }

        private int CalculateBox()
        {
            var countBox = 0;

            foreach (var entity in ListEntities)
                if (entity is Box)
                    countBox++;

            return countBox;
        }

        public ActResult Act()
        {
            SokobanPlayer.DirectionMove = DetermineDirection();
            var offsetCoordinatesPlayer = Direction.DeterminePosition(SokobanPlayer.DirectionMove);
            var selectedEntity = ListEntities[FindEntity(SokobanPlayer.Position + offsetCoordinatesPlayer)];

            if (SokobanPlayer.DirectionMove == Directions.Nothing || selectedEntity is Wall)
                return ActResult.Nothing;
            else
            {
                CountMoves++;
                selectedEntity.Action(this, SokobanPlayer);
            }

            if (!(selectedEntity is Box))
                SokobanPlayer.MovePlayer(this, offsetCoordinatesPlayer);

            return DetermineActResult(selectedEntity);
        }

        private bool IsWin()
        {
            return CountBox == CountInPlace;
        }

        private bool IsDefeat(Entity selectedEntity)
        {
            return selectedEntity is Pit ||
                   (selectedEntity is Box && (ListEntities[FindEntity(selectedEntity.Position)] is Pit || IsBoxStuck(selectedEntity)));
        }

        public int FindEntity(Position position)
        {
            for(var index = 0; index < ListEntities.Count; ++index)
                if (Position.CompareTo(position, ListEntities[index].Position))
                    return index;

            throw new ArgumentNullException("missing element");
        }

        public Player FindPlayer()
        {
            for (var index = 0; index < ListEntities.Count; ++index)
                if (ListEntities[index] is Player)
                    return (Player)ListEntities[index];

            return null;
        }

        private Directions DetermineDirection()
        {
            if (Keyboard.IsKeyPressed(Key.Up))
                return Directions.Up;
            if (Keyboard.IsKeyPressed(Key.Down))
                return Directions.Down;
            if (Keyboard.IsKeyPressed(Key.Right))
                return Directions.Right;
            if (Keyboard.IsKeyPressed(Key.Left))
                return Directions.Left;
            return Directions.Nothing;
        }

        private bool IsBoxStuck(Entity box)
        {
            var entityLeft = FindEntity(new Position(box.Position.Y, box.Position.X - 1));
            var entityRight = FindEntity(new Position(box.Position.Y, box.Position.X + 1));
            var entityAbove = FindEntity(new Position(box.Position.Y - 1, box.Position.X));
            var entityBelow = FindEntity(new Position(box.Position.Y + 1, box.Position.X));

            return char.IsLower(box.Name) &&
                ((ListEntities[entityAbove] is Wall && ListEntities[entityRight] is Wall) ||
                (ListEntities[entityAbove] is Wall && ListEntities[entityLeft] is Wall) ||
                (ListEntities[entityBelow] is Wall && ListEntities[entityRight] is Wall) ||
                (ListEntities[entityBelow] is Wall && ListEntities[entityLeft] is Wall));
        }

        private ActResult DetermineActResult(Entity selectedEntity)
        {
            if (IsWin())
                return ActResult.Win;
            if (IsDefeat(ListEntities[selectedEntity.Index]))
                return ActResult.Defeat;

            return ActResult.Nothing;
        }
    }
}
