namespace AdventOfCode.Year2016.Day01
{
    using System.Collections.Generic;
    using AdventOfCode.Helpers;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            (int X, int Y)[] movementMatrices =
            [
                (0, 1),
                (1, 0),
                (0, -1),
                (-1, 0),
            ];

            string[] directions = input[0].Split(", ");
            (int X, int Y) location = (0, 0);
            int currentDirection = 0;

            var visitedLocations = new List<(int, int)>() { location };

            foreach (string direction in directions)
            {
                string turn = direction.Substring(0, 1);
                int distance = int.Parse(direction.Substring(1));

                // Change direction
                currentDirection = ((turn == "L" ? currentDirection - 1 : currentDirection + 1) + 4) % 4;

                for (int i = 0; i < distance; i++)
                {
                    location.X += movementMatrices[currentDirection].X;
                    location.Y += movementMatrices[currentDirection].Y;

                    if (visitedLocations.Contains(location))
                    {
                        return Distance.Manhattan(location.X, location.Y).ToString();
                    }

                    visitedLocations.Add(location);
                }
            }

            return string.Empty;
        }
    }
}
