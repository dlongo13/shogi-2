using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Shogi.Pieces
{
    public abstract class Piece
    {
        public Player OwnerPlayer { get; }
        public string Position { get; private set; }
        public char ShortName { get; protected set; }

        public bool CanMoveBack { get; protected set; }
        public bool CanMoveForwards { get; protected set; }
        public bool CanMoveForwardInRange { get; protected set; }
        public bool CanMoveDiagonallyInRange { get; protected set; }
        public bool CanMoveForwardsDiagonally { get; protected set; }
        public bool CanMoveBackwardsDiagonally { get; protected set; }
        public bool CanMoveHorizontallyAndVerticallyInRange { get; protected set; }
        public bool CanMoveSideways { get; protected set; }

        protected Piece(Player ownerPlayer, string position)
        {
            OwnerPlayer = ownerPlayer;
            Position = position;
        }

        public virtual bool IsMoveLegal(string toPosition)
        {
            return GetPossibleMovements().Contains(string.Concat(Position, toPosition));
        }

        public virtual string Move(string toPosition)
        {
            var movementDescription = $"{ShortName}{Position}-{toPosition}";
            Position = toPosition;

            return movementDescription;
        }

        public virtual IEnumerable<string> GetPossibleMovements()
        {
            var possibleMovements = new List<string>();

            AddForwardMovements(possibleMovements);
            AddBackwardMovements(possibleMovements);
            AddSidewayMovements(possibleMovements);
            AddForwardDiagonalMovements(possibleMovements);
            AddBackwardDiagonalMovements(possibleMovements);
            AddRangeForwardMovements(possibleMovements);
            AddRangeDiagonalMovements(possibleMovements);
            AddRangeHorizontallyAndVerticallyMovements(possibleMovements);

            return possibleMovements;
        }

        private void AddBackwardDiagonalMovements(ICollection<string> possibleMovements)
        {
            if (CanMoveBackwardsDiagonally)
                if (OwnerPlayer == Player.White)
                {
                    possibleMovements.Add(string.Concat(Position, Convert.ToChar(Position[0] + 1),
                        Convert.ToChar(Position[1] - 1)));
                    possibleMovements.Add(string.Concat(Position, Convert.ToChar(Position[0] - 1),
                        Convert.ToChar(Position[1] - 1)));
                }
                else
                {
                    possibleMovements.Add(string.Concat(Position, Convert.ToChar(Position[0] + 1),
                        Convert.ToChar(Position[1] + 1)));
                    possibleMovements.Add(string.Concat(Position, Convert.ToChar(Position[0] - 1),
                        Convert.ToChar(Position[1] + 1)));
                }
        }

        private void AddForwardDiagonalMovements(ICollection<string> possibleMovements)
        {
            if (CanMoveForwardsDiagonally)
                if (OwnerPlayer == Player.Black)
                {
                    possibleMovements.Add(string.Concat(Position, Convert.ToChar(Position[0] + 1),
                        Convert.ToChar(Position[1] - 1)));
                    possibleMovements.Add(string.Concat(Position, Convert.ToChar(Position[0] - 1),
                        Convert.ToChar(Position[1] - 1)));
                }
                else
                {
                    possibleMovements.Add(string.Concat(Position, Convert.ToChar(Position[0] + 1),
                        Convert.ToChar(Position[1] + 1)));
                    possibleMovements.Add(string.Concat(Position, Convert.ToChar(Position[0] - 1),
                        Convert.ToChar(Position[1] + 1)));
                }
        }

        private void AddRangeHorizontallyAndVerticallyMovements(ICollection<string> possibleMovements)
        {
            if (CanMoveHorizontallyAndVerticallyInRange)
            {
                for (int i = Position[1] - 1; i >= 'a'; i--)
                    possibleMovements.Add(string.Concat(Position, Position[0], Convert.ToChar(i)));
                for (int i = Position[1] + 1; i <= 'i'; i++)
                    possibleMovements.Add(string.Concat(Position, Position[0], Convert.ToChar(i)));
                for (int i = Position[0] - 1; i >= '1'; i--)
                    possibleMovements.Add(string.Concat(Position, Convert.ToChar(i), Position[1]));
                for (int i = Position[0] + 1; i <= '9'; i++)
                    possibleMovements.Add(string.Concat(Position, Convert.ToChar(i), Position[1]));
            }
        }

        private void AddRangeDiagonalMovements(ICollection<string> possibleMovements)
        {
            if (CanMoveDiagonallyInRange)
            {
                for (int i = Position[1]; i > 'a'; i--)
                {
                    var diff = (Position[1] - i) + 1;
                    possibleMovements.Add(string.Concat(Position, Convert.ToChar(Position[0] - diff),
                        Convert.ToChar(Position[1] - diff)));
                    possibleMovements.Add(string.Concat(Position, Convert.ToChar(Position[0] - diff),
                        Convert.ToChar(Position[1] + diff)));
                }

                for (int i = Position[1]; i < 'i'; i++)
                {
                    var diff = (i - Position[1]) + 1;
                    possibleMovements.Add(string.Concat(Position, Convert.ToChar(Position[0] + diff),
                        Convert.ToChar(Position[1] + diff)));
                    possibleMovements.Add(string.Concat(Position, Convert.ToChar(Position[0] + diff),
                        Convert.ToChar(Position[1] - diff)));
                }
            }
        }

        private void AddRangeForwardMovements(ICollection<string> possibleMovements)
        {
            if (CanMoveForwardInRange)
            {
                if (OwnerPlayer == Player.Black)
                    for (int i = Position[1] - 1; i >= 'a'; i--)
                        possibleMovements.Add(string.Concat(Position, Position[0], Convert.ToChar(i)));
                else
                    for (int i = Position[1] + 1; i <= 'i'; i++)
                        possibleMovements.Add(string.Concat(Position, Position[0], Convert.ToChar(i)));
            }
        }

        private void AddForwardMovements(ICollection<string> possibleMovements)
        {
            if (CanMoveForwards)
            {
                if (OwnerPlayer == Player.Black)
                    possibleMovements.Add(string.Concat(Position, Position[0], Convert.ToChar(Position[1] - 1)));
                else
                    possibleMovements.Add(string.Concat(Position, Position[0], Convert.ToChar(Position[1] + 1)));
            }
        }

        private void AddSidewayMovements(List<string> possibleMovements)
        {
            if (CanMoveSideways)
            {
                possibleMovements.Add(string.Concat(Position, Convert.ToChar(Position[0] - 1), Position[1]));
                possibleMovements.Add(string.Concat(Position, Convert.ToChar(Position[0] + 1), Position[1]));
            }
        }

        private void AddBackwardMovements(ICollection<string> possibleMovements)
        {
            if (CanMoveBack)
            {
                if (OwnerPlayer == Player.White)
                    possibleMovements.Add(string.Concat(Position, Position[0], Convert.ToChar(Position[1] - 1)));
                else
                    possibleMovements.Add(string.Concat(Position, Position[0], Convert.ToChar(Position[1] + 1)));
            }
        }
    }
}