using Microsoft.AspNetCore.Mvc;

namespace projekat_API.Controllers
{
    [Route("api/maze")]
    [ApiController]
    public class MazeController : ControllerBase
    {
        [HttpPost]
        public IActionResult SolveMaze([FromBody] MazeRequest request)
        {
            var result = FindShortestPath(
                request.Maze,
                request.StartX,
                request.StartY,
                request.EndX,
                request.EndY
            );
            return Ok(result);
        }

        private PathResult FindShortestPath(int[][] maze, int startX, int startY, int endX, int endY)
        {
            int rows = maze.Length;
            int cols = maze[0].Length;

            if (startX < 0 || startX >= rows || startY < 0 || startY >= cols ||
               endX < 0 || endX >= rows || endY < 0 || endY >= cols ||
               maze[startX][startY] == 1 || maze[endX][endY] == 1)
            {
                return new PathResult(false, new List<Coordinate>(), 0);
            }



            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            Queue<(int x, int y, List<Coordinate> path)> queue = new Queue<(int, int, List<Coordinate>)>();
            HashSet<(int, int)> visited = new HashSet<(int, int)>();

            queue.Enqueue((startX, startY, new List<Coordinate> { new Coordinate(startX, startY) }));
            visited.Add((startX, startY));

            while (queue.Count > 0)
            {
                var (currentX, currentY, path) = queue.Dequeue();

                if (currentX == endX && currentY == endY)
                {
                    return new PathResult(true, path, path.Count);
                }

                for (int i = 0; i < 4; i++)
                {
                    int newX = currentX + dx[i];
                    int newY = currentY + dy[i];

                    if (newX >= 0 && newX < rows &&
                        newY >= 0 && newY < cols &&
                        maze[newX][newY] == 0 &&
                        !visited.Contains((newX, newY)))
                    {
                        var newPath = new List<Coordinate>(path) { new Coordinate(newX, newY) };
                        queue.Enqueue((newX, newY, newPath));
                        visited.Add((newX, newY));
                    }
                }
            }

            return new PathResult(false, new List<Coordinate>(), 0);
        }
    }

    public class MazeRequest
    {
        public int[][] Maze { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }
    }

    public class PathResult
    {
        public bool IsPossible { get; }
        public List<Coordinate> Path { get; }
        public int StepCount { get; }

        public PathResult(bool isPossible, List<Coordinate> path, int stepCount)
        {
            IsPossible = isPossible;
            Path = path;
            StepCount = stepCount;
        }
    }

    public class Coordinate
    {
        public int X { get; }
        public int Y { get; }
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}