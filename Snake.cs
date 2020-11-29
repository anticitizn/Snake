using System.Drawing;
using System.Collections.Generic;

namespace SnakeForms
{
    class Snake
    {
        public List<Piece> snakePieces = new List<Piece>();
        private Direction LastPieceDirection { get; set; }
        public Piece Head { get { return snakePieces[0]; } }
        private int StartX { get; set; }
        private int StartY { get; set; }
        protected Direction StartDirection { get; set; }
        private Color HeadColor { get; set; }
        private Color BodyColor { get; set; }
        public Snake(int x, int y, Direction direction, Color headColor, Color bodyColor)
        {
            StartX = x;
            StartY = y;
            HeadColor = headColor;
            BodyColor = bodyColor;
            StartDirection = direction;

            snakePieces.Add(new Piece(StartX, StartY, StartDirection, PieceType.HeadPiece, HeadColor));

            int x2 = StartX;
            int y2 = StartY;
            TranslateOnce(InverseDirection(StartDirection), ref x2, ref y2);
            snakePieces.Add(new Piece(x2, y2, StartDirection, PieceType.SnakePiece, BodyColor));
        }

        public Snake(Snake snake)
        {

        }

        public void Grow()
        {
            Piece lastP = snakePieces[snakePieces.Count - 1];
            int x = lastP.X;
            int y = lastP.Y;
            TranslateOnce(InverseDirection(LastPieceDirection), ref x, ref y);

            snakePieces.Add(new Piece(x, y, LastPieceDirection, PieceType.SnakePiece, snakePieces[1].Color));
        }

        public void ChangeDirection(Direction direction)
        {
            if (direction != InverseDirection(snakePieces[1].MoveDirection))
            {
                snakePieces[0].MoveDirection = direction;
            }
        }

        public void Move()
        {
            Direction previousPieceDirection = Direction.None;

            for (int i = 0; i < snakePieces.Count; i++)
            {
                int x2 = snakePieces[i].X;
                int y2 = snakePieces[i].Y;
                TranslateOnce(snakePieces[i].MoveDirection, ref x2, ref y2);

                snakePieces[i].X = x2;
                snakePieces[i].Y = y2;
                
                if (i == 0)
                {
                    previousPieceDirection = snakePieces[i].MoveDirection;
                }
                else
                {
                    Direction tempDirection = snakePieces[i].MoveDirection;
                    snakePieces[i].MoveDirection = previousPieceDirection;
                    previousPieceDirection = tempDirection;
                }
            }

            LastPieceDirection = previousPieceDirection;
        }

        public bool EatsItself()
        {
            for(int i = 1; i < snakePieces.Count; i++)
            {
                if (Head.X == snakePieces[i].X && Head.Y == snakePieces[i].Y)
                {
                    snakePieces[i].Color = Color.DarkRed;
                    snakePieces[1].Color = snakePieces[0].Color;
                    return true;
                }
            }

            return false;
        }

        public bool CanEat(int x, int y)
        {
            if (Head.X == x && Head.Y == y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool HitsWall(int x, int y)
        {
            if (Head.X == x && Head.Y == y)
            {
                snakePieces[1].Color = Color.DarkRed;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsAtPos(int x, int y)
        {
            foreach (Piece piece in snakePieces)
            {
                if (piece.X == x && piece.Y == y)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HitsBorder(int Width, int Height)
        {
            if (Head.X < 0 || Head.X >= Width|| Head.Y < 0 || Head.Y >= Height)
            {
                snakePieces[1].Color = Color.DarkRed;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Clear()
        {
            snakePieces.Clear();

            snakePieces.Add(new Piece(StartX, StartY, StartDirection, PieceType.HeadPiece, HeadColor));
            int x2 = StartX;
            int y2 = StartY;
            TranslateOnce(InverseDirection(StartDirection), ref x2, ref y2);
            snakePieces.Add(new Piece(x2, y2, StartDirection, PieceType.SnakePiece, BodyColor));
        }

        protected void TranslateOnce(Direction direction, ref int x, ref int y)
        {
            if (direction == Direction.Up)
            {
                y -= 1;
            }
            else if (direction == Direction.Right)
            {
                x += 1;
            }
            else if (direction == Direction.Down)
            {
                y += 1;
            }
            else if (direction == Direction.Left)
            {
                x -= 1;
            }
        }

        protected Direction InverseDirection(Direction direction)
        {
            if (direction == Direction.Up)
            {
                return Direction.Down;
            }
            else if (direction == Direction.Right)
            {
                return Direction.Left;
            }
            else if (direction == Direction.Down)
            {
                return Direction.Up;
            }
            else if (direction == Direction.Left)
            {
                return Direction.Right;
            }
            else
            {
                return Direction.None;
            }
        }

        protected Direction ClockwiseDirection(Direction direction)
        {
            if (direction == Direction.Up)
            {
                return Direction.Right;
            }
            else if (direction == Direction.Right)
            {
                return Direction.Down;
            }
            else if (direction == Direction.Down)
            {
                return Direction.Left;
            }
            else if (direction == Direction.Left)
            {
                return Direction.Up;
            }
            else
            {
                return Direction.None;
            }
        }

    }
}
