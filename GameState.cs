using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SnakeForms
{
    class GameState
    {
        static private readonly Random rng = new Random();
        private bool hasPlayer = false;
        public bool Running { get; private set; }
        public int Score { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        private Snake PlayerSnake { get; set; }
        private List<Piece> FoodPieces { get; set; }
        private List<Piece> Walls { get; set; }
        private List<Snake> Snakes { get; set; }
        public List<List<Piece>> Map { get; private set; }
        public GameState(int width, int height)
        {
            Running = true;
            Width = width;
            Height = height;
            Snakes = new List<Snake>();
            FoodPieces = new List<Piece>();

            Restart();
        }

        private void GenerateFoodPiece()
        {
            while (FoodPieces.Count <= Snakes.Count)
            {
                int x = rng.Next((Width - 1));
                int y = rng.Next((Height - 1));

                foreach (Snake snake in Snakes)
                {
                    if (snake.IsAtPos(x, y))
                    {
                        GenerateFoodPiece();
                        return;
                    }
                }

                for (int i = 0; i < Walls.Count - 1; i++)
                {
                    if (Walls[i].X == x && Walls[i].Y == y)
                    {
                        GenerateFoodPiece();
                        return;
                    }
                }

                FoodPieces.Add(new Piece(x, y, PieceType.Food, Color.Red));
            }
        }

        private void GenerateWalls()
        {
            Walls = new List<Piece>();
            for (int i = 0; i < rng.Next(3, Width); i++)
            {
                int wallLength = rng.Next(0, Width / 2);

                // 1 - vertical, 2 - horizontal
                int axis = rng.Next(2);
                
                if (axis == 1)
                {
                    int wallX = rng.Next(Width);
                    int offset = rng.Next(0, Width);
                    for (int j = 0; j < wallLength; j++)
                    {
                        Walls.Add(new Piece(wallX, j + offset, PieceType.Wall, Color.Blue));
                    }
                }
                else
                {
                    int wallY = rng.Next(Height);
                    int offset = rng.Next(0, Width);
                    for (int j = 0; j < wallLength; j++)
                    {
                        Walls.Add(new Piece(j, wallY + offset, PieceType.Wall, Color.Blue));
                    }
                }
            }
        }

        private void AssembleMap()
        {
            Map = new List<List<Piece>>();
            for (int y = 0; y < Height; y++)
            {
                Map.Add(new List<Piece>());
                for (int x = 0; x < Width; x++)
                {
                    if (Walls.Any(p => p.X == x && p.Y == y))
                    {
                        Map[y].Add(new Piece(x, y, PieceType.Wall, Color.Blue));
                    }
                    else if (Snakes.Any(s => s.snakePieces.Any(p => p.X == x && p.Y == y && p.Type == PieceType.SnakePiece)))
                    {
                        Map[y].Add(new Piece(x, y, PieceType.SnakePiece, Snakes.Where(s => s.IsAtPos(x, y)).FirstOrDefault().snakePieces.Where(p => p.X == x && p.Y == y).FirstOrDefault().Color));
                    }
                    else if (Snakes.Any(s => s.snakePieces.Any(p => p.X == x && p.Y == y && p.Type == PieceType.HeadPiece)))
                    {
                        Map[y].Add(new Piece(x, y, PieceType.HeadPiece, Snakes.Where(s => s.IsAtPos(x, y)).FirstOrDefault().snakePieces.Where(p => p.X == x && p.Y == y).FirstOrDefault().Color));
                    }
                    else if (FoodPieces.Any(p => p.X == x && p.Y == y))
                    {
                        Piece piece = FoodPieces.Where(p => p.X == x && p.Y == y).FirstOrDefault();
                        Map[y].Add(piece);
                    }
                    else
                    {
                        Map[y].Add(new Piece(x, y, PieceType.Empty));
                    }
                }
            }
        }

        public void CheckCollisions()
        {
            foreach (Snake snake in Snakes)
            {
                
                foreach(Piece food in FoodPieces.ToArray())
                {
                    if (snake.CanEat(food.X, food.Y))
                    {
                        FoodPieces.Remove(food);
                        GenerateFoodPiece();
                        snake.Grow();
                        Score += 1;
                    }
                }

                if (snake.HitsBorder(Width, Height))
                {
                    Running = false;
                }

                if (snake.EatsItself())
                {
                    Running = false;
                }

                for (int i = 0; i < Walls.Count - 1; i++)
                {
                    if (snake.HitsWall(Walls[i].X, Walls[i].Y))
                    {
                        Running = false;
                    }
                }
            }
        }

        public void OnTick()
        {
            AssembleMap();

            if(Running)
            {
                foreach (ComputerSnake snake in Snakes.Where(s => s.GetType() == typeof(ComputerSnake)))
                {
                    snake.Map = Map;
                    snake.Move();
                }

                // HACK
                foreach (Snake snake in Snakes.Where(s => s.GetType() != typeof(ComputerSnake)))
                {
                    snake.Move();
                }
            }

            CheckCollisions();
        }

        public void Restart()
        {
            GenerateWalls();
            Snakes.Clear();
            AddAISnake();
            if (hasPlayer == true)
            {
                AddPlayerSnake();
            }

            GenerateFoodPiece();
            AssembleMap();
            Running = true;
            Score = 0;
        }

        public void SwitchPlayer()
        {
            if (hasPlayer == false)
            {
                AddPlayerSnake();
                hasPlayer = true;
            }
            else
            {
                Snakes.Remove(PlayerSnake);
                hasPlayer = false;
            }
        }

        public void HandleInput(KeyEventArgs e)
        {
            if (PlayerSnake == null)
            {
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.W:
                    PlayerSnake.ChangeDirection(Direction.Up);
                    break;

                case Keys.D:
                    PlayerSnake.ChangeDirection(Direction.Right);
                    break;

                case Keys.S:
                    PlayerSnake.ChangeDirection(Direction.Down);
                    break;

                case Keys.A:
                    PlayerSnake.ChangeDirection(Direction.Left);
                    break;
            }
        }

        private Snake GetRandomSnake()
        {
            Color headColor = Color.FromArgb(rng.Next(256), rng.Next(256), rng.Next(256));
            Color bodyColor = Color.FromArgb(rng.Next(256), rng.Next(256), rng.Next(256));

            Array directionValues = Enum.GetValues(typeof(Direction));
            Direction startDirection = (Direction)directionValues.GetValue(rng.Next(directionValues.Length));
            while (startDirection == Direction.None)
            {
                startDirection = (Direction)directionValues.GetValue(rng.Next(directionValues.Length));
            }

            int x = rng.Next(2, Width - 1);
            int y = rng.Next(2, Height - 1);

            while (Walls.Any(w => w.X == x && w.Y == y))
            {
                x = rng.Next(2, Width - 1);
                y = rng.Next(2, Height - 1);
            }

            Snake randomSnake = new Snake(x, y, startDirection, headColor, bodyColor);
            return randomSnake;
        }

        private ComputerSnake GetRandomComputerSnake()
        {
            Color headColor = Color.FromArgb(rng.Next(256), rng.Next(256), rng.Next(256));
            Color bodyColor = Color.FromArgb(rng.Next(256), rng.Next(256), rng.Next(256));

            Array directionValues = Enum.GetValues(typeof(Direction));
            Direction startDirection = (Direction)directionValues.GetValue(rng.Next(directionValues.Length));
            while (startDirection == Direction.None)
            {
                startDirection = (Direction)directionValues.GetValue(rng.Next(directionValues.Length));
            }

            int x = rng.Next(2, Width - 1);
            int y = rng.Next(2, Height - 1);

            while (Walls.Any(w => w.X == x && w.Y == y))
            {
                x = rng.Next(2, Width - 1);
                y = rng.Next(2, Height - 1);
            }

            ComputerSnake randomComputerSnake = new ComputerSnake(x, y, startDirection, headColor, bodyColor);
            return randomComputerSnake;
        }

        public void AddAISnake()
        {
            Snakes.Add(GetRandomComputerSnake());
        }

        public void AddAISnake(int x, int y, Direction direction, Color headColor, Color bodyColor)
        {
            Snakes.Add(new ComputerSnake(x, y, direction, headColor, bodyColor));
        }

        public void AddPlayerSnake(int x, int y, Direction direction, Color headColor, Color bodyColor)
        {
            Snake newPlayerSnake = new Snake(x, y, direction, headColor, bodyColor);
            Snakes.Add(newPlayerSnake);
            PlayerSnake = newPlayerSnake;
        }

        public void AddPlayerSnake()
        {
            Snake newPlayerSnake = GetRandomSnake();
            Snakes.Add(newPlayerSnake);
            PlayerSnake = newPlayerSnake;
        }
    }
}
