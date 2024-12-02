namespace AdventOfCode.Year2016.Day17
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public class Part02 : ISolution
    {
        private readonly MD5 hasher = MD5.Create();

        public string Solve(string[] input)
        {
            // Once more with the breadth-first search
            var start = new State { X = 0, Y = 0, Steps = 0, Path = string.Empty, AvailableDirections = this.GetAvailableDirections(input[0], string.Empty, 0, 0) };

            var queue = new PriorityQueue<State, int>();
            queue.Enqueue(start, start.Steps);

            var previousStates = new Dictionary<State, int>();
            var solutions = new List<string>();

            while (queue.Count > 0)
            {
                State current = queue.Dequeue();

                if (current.X == 3 && current.Y == 3)
                {
                    solutions.Add(current.Path!);
                    continue;
                }

                // Have we been in the same location with the same available options in fewer steps?
                if (previousStates.TryGetValue(current, out int steps) && steps < current.Steps)
                {
                    continue;
                }

                previousStates.Add(current, current.Steps);

                foreach (AvailableDirections direction in Enum.GetValues<AvailableDirections>().Where(v => current.CanMove(v)))
                {
                    string newPath = current.Path + direction;
                    (int x, int y) = GetNewLocation(direction, current.X, current.Y);
                    queue.Enqueue(new State { X = x, Y = y, Steps = newPath.Length, Path = newPath, AvailableDirections = this.GetAvailableDirections(input[0], newPath, x, y) }, newPath.Length);
                }
            }

            return solutions.Max(x => x.Length).ToString();
        }

        private static bool RepresentsOpenDoor(char character)
        {
            return character > 'a' && character < 'g';
        }

        private static (int X, int Y) GetNewLocation(AvailableDirections direction, int x, int y)
        {
            return direction switch
            {
                AvailableDirections.U => (x, y - 1),
                AvailableDirections.D => (x, y + 1),
                AvailableDirections.L => (x - 1, y),
                AvailableDirections.R => (x + 1, y),
                _ => throw new ArgumentException("Unsupported direction"),
            };
        }

        private AvailableDirections GetAvailableDirections(string passcode, string path, int x, int y)
        {
            byte[] currentBytes = Encoding.UTF8.GetBytes(passcode + path);
            byte[] hash = this.hasher.ComputeHash(currentBytes);
            string hashedValue = BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
            AvailableDirections result = 0;

            if (y > 0 && RepresentsOpenDoor(hashedValue[0]))
            {
                result |= AvailableDirections.U;
            }

            if (y < 3 && RepresentsOpenDoor(hashedValue[1]))
            {
                result |= AvailableDirections.D;
            }

            if (x > 0 && RepresentsOpenDoor(hashedValue[2]))
            {
                result |= AvailableDirections.L;
            }

            if (x < 3 && RepresentsOpenDoor(hashedValue[3]))
            {
                result |= AvailableDirections.R;
            }

            return result;
        }
    }
}