using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace SnakeForms
{
    class ComputerSnake : Snake
    {
        public List<List<Piece>> Map { get; set; }
        private List<Piece> CurrentPath {
            get;
            set; 
        }
        public Point CurrentTarget { get; set; }
        public ComputerSnake(int x, int y, Direction direction, Color headColor, Color snakeColor) : base(x, y, direction, headColor, snakeColor)
        {
            CurrentPath = new List<Piece>();
        }

        public ComputerSnake(Snake snake) : base(snake) { }

        public new void Move()
        {

            CalculatePath();

            if (CurrentPath.Any() && CurrentPath[0] != null)
            {
                MoveToNeighborPos(CurrentPath[0].X, CurrentPath[0].Y);
            }
            else
            {
                SimplePath();
            }
            base.Move();
        }

        public void SimplePath()
        {
            // A simple heuristic that is engaged when there is no evident direct way to the target
            // If it detects an obstacle directly ahead, it tries to turn left or right
            //Console.WriteLine("SIMPLE PATH MODE ENGAGED");
            CurrentTarget = null;
            CurrentPath.Clear();
            int x = Head.X;
            int y = Head.Y;

            TranslateOnce(Head.MoveDirection, ref x, ref y);

            Direction headRight = ClockwiseDirection(Head.MoveDirection);
            Direction headLeft = InverseDirection(headRight);
            

            if (x < 0 || y < 0 || y >= Map.Count || x >= Map[0].Count || Map[y].Any(p => p.X == x && (p.Type == PieceType.Wall || p.Type == PieceType.SnakePiece)))
            {
                x = Head.X;
                y = Head.Y;

                TranslateOnce(ClockwiseDirection(Head.MoveDirection), ref x, ref y);

                if ((x < 0 || y < 0 || y >= Map.Count || x >= Map[0].Count) || Map[y].Any(p => p.X == x && (p.Type == PieceType.Wall || p.Type == PieceType.SnakePiece)))
                {
                    ChangeDirection(InverseDirection(ClockwiseDirection(Head.MoveDirection)));
                    //Console.WriteLine("TURNING LEFT");
                }
                else
                {
                    ChangeDirection(ClockwiseDirection(Head.MoveDirection));
                    //Console.WriteLine("TURNING RIGHT");
                }
            }
        }
        
        public void CalculatePath()
        {
            List<Piece> tilesPath = new List<Piece>();
            Dictionary<Piece, Piece> cameFrom = BreadthFirstSearch();

            if (CurrentTarget == null)
            {
                return;
            }

            Piece current = Map[CurrentTarget.Y].Where(p => p.X == CurrentTarget.X).FirstOrDefault();
            Piece start = Map[Head.Y].Where(p => p.X == Head.X).FirstOrDefault();
            while (current != start)
            {
                tilesPath.Add(current);
                if (current != null)
                {
                    cameFrom.TryGetValue(current, out current);
                }
                else
                {
                    //Console.WriteLine("AI Snake can't find a way");
                    CurrentTarget = null;
                    CurrentPath.Clear();
                    break;
                }
            }

            tilesPath.Append(start);
            tilesPath.Reverse();

            CurrentPath = tilesPath;
        }

        private Dictionary<Piece, Piece> BreadthFirstSearch()
        {
            Queue<Piece> queue = new Queue<Piece>();
            Dictionary<Piece, List<Piece>> neighbors = new Dictionary<Piece, List<Piece>>();
            Dictionary<Piece, Piece> cameFrom = new Dictionary<Piece, Piece>();

            // finds all neighbors of every tile
            // gott rette mich

            for (int y = 0; y < Map.Count; y++)
            {
                for (int x = 0; x < Map[y].Count; x++)
                {
                    if (Map[y].Any(p => p.X == x + 1 && p.Y == y))
                    {
                        if (neighbors.ContainsKey(Map[y][x]))
                        {
                            neighbors[Map[y][x]].Add(Map[y].Where(p => p.X == x + 1).FirstOrDefault());
                        }
                        else
                        {
                            neighbors[Map[y][x]] = Map[y].Where(p => p.X == x + 1).ToList();
                        }
                    }

                    if (Map[y].Any(p => p.X == x -1 && p.Y == y))
                    {
                        if (neighbors.ContainsKey(Map[y][x]))
                        {
                            neighbors[Map[y][x]].Add(Map[y].Where(p => p.X == x - 1).FirstOrDefault());
                        }
                        else
                        {
                            neighbors[Map[y][x]] = Map[y].Where(p => p.X == x - 1).ToList();
                        }
                    }

                    if (y < Map.Count - 1 && Map[y + 1].Any(p => p.X == x))
                    {
                        if (neighbors.ContainsKey(Map[y][x]))
                        {
                            neighbors[Map[y][x]].Add(Map[y + 1].Where(p => p.X == x).FirstOrDefault());
                        }
                        else
                        {
                            neighbors[Map[y][x]] = Map[y + 1].Where(p => p.X == x).ToList();
                        }
                    }

                    if (y > 0 && Map[y - 1].Any(p => p.X == x))
                    {
                        if (neighbors.ContainsKey(Map[y][x]))
                        {
                            neighbors[Map[y][x]].Add(Map[y - 1].Where(p => p.X == x).FirstOrDefault());
                        }
                        else
                        {
                            neighbors[Map[y][x]] = Map[y - 1].Where(p => p.X == x).ToList();
                        }
                    }
                }
            }

            List<Piece> visited = new List<Piece>();
            queue.Enqueue(Map[Head.Y].Where(p => p.X == Head.X).FirstOrDefault());

            cameFrom[Map[Head.Y][Head.X]] = Map[Head.Y][Head.X];
            while (queue.Count > 0)
            {
                Piece current = queue.Dequeue();

                if (current.Type != PieceType.Wall && current.Type != PieceType.SnakePiece)
                {

                    if (current.Type == PieceType.Food)
                    {
                        CurrentTarget = new Point(current.X, current.Y);
                        queue.Clear();
                        break;
                    }

                    //Console.WriteLine("current: " + current.X + " " + current.Y);
                    foreach (Piece neighbor in neighbors[current])
                    {
                        if (!cameFrom.ContainsKey(neighbor))
                        {
                            //Console.WriteLine(neighbor.X + " " + neighbor.Y);
                            queue.Enqueue(neighbor);
                            cameFrom.Add(neighbor, current);
                        }
                    }
                }
            }
            //Console.WriteLine("BFS DONE");
            return cameFrom;
        }

        private void MoveToNeighborPos(int x, int y)
        {
            //Console.WriteLine("MOVE TO: " + x + " " + y + " | " + "HEAD: " + Head.X + " " + Head.Y + " | " + "TARGET: " + CurrentTarget.X + " " + CurrentTarget.Y);
            if (Head.X - 1 == x && Head.Y == y)
            {
                ChangeDirection(Direction.Left);
            }
            else if (Head.X + 1 == x && Head.Y == y)
            {
                ChangeDirection(Direction.Right);
            }
            else if (Head.X == x && Head.Y - 1 == y)
            {
                ChangeDirection(Direction.Up);
            }
            else if (Head.X == x && Head.Y + 1 == y)
            {
                ChangeDirection(Direction.Down);
            }
        }
    }
}
