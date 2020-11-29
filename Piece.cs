using System.Drawing;

namespace SnakeForms
{
    class Piece
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction MoveDirection { get; set; }
        public PieceType Type { get; set; }
        public Color Color { get; set; }

        public Piece(int x, int y, Direction moveDirection, PieceType pieceType, Color color)
        {
            X = x;
            Y = y;
            MoveDirection = moveDirection;
            Type = pieceType;
            Color = color;
        }

        public Piece(int x, int y, PieceType pieceType, Color color)
        {
            X = x;
            Y = y;
            MoveDirection = Direction.None;
            Type = pieceType;
            Color = color;
        }

        public Piece(int x, int y, PieceType pieceType)
        {
            X = x;
            Y = y;
            MoveDirection = Direction.None;
            Type = pieceType;
            Color = Color.White;
        }
    }
}
