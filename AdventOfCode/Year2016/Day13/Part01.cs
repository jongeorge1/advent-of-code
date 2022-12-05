namespace AdventOfCode.Year2016.Day13
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            (int x, int y) destination = (31, 39);

            if (input.StartsWith("TEST"))
            {
                input = input[4..];
                destination = (7, 4);
            }

            int offset = int.Parse(input);

            // This line writes out the map, if you want to see it.
            ////Console.WriteLine(BuildMap(destination, offset));

            // Breadth first search FTW
            Dictionary<(int, int), int> visitedLocations = new ();
            PriorityQueue<((int x, int y) position, int steps), int> processingQueue = new ();

            processingQueue.Enqueue(((1, 1), 0), 0);

            while (processingQueue.Count > 0)
            {
                ((int x, int y) location, int steps) = processingQueue.Dequeue();

                // Are we where we want to be?
                if (location == destination)
                {
                    return steps.ToString();
                }

                // Have we been here before?
                if (visitedLocations.TryGetValue(location, out int previousStepsToThisLocation))
                {
                    if (previousStepsToThisLocation <= steps)
                    {
                        // We've found a way here that's longer
                        continue;
                    }
                }

                visitedLocations[location] = steps;

                // Where to next?
                foreach ((int x, int y) current in GetPotentialNextLocations(location, offset))
                {
                    processingQueue.Enqueue((current, steps + 1), steps + 1);
                }
            }

            return string.Empty;
        }

        private static bool IsOpenSpace((int x, int y) location, int offset)
        {
            if (location.x < 0 || location.y < 0)
            {
                return false;
            }

            int step1 = (location.x * location.x) + (3 * location.x) + (2 * location.x * location.y) + location.y + (location.y * location.y);
            int step2 = step1 + offset;
            string step3 = Convert.ToString(step2, 2);
            int step4 = step3.Count(x => x == '1');

            return step4 % 2 == 0;
        }

        private static IEnumerable<(int x, int y)> GetPotentialNextLocations((int x, int y) location, int offset)
        {
            (int, int)[] potentialLocations = new[]
            {
                (location.x + 1, location.y),
                (location.x - 1, location.y),
                (location.x, location.y + 1),
                (location.x, location.y - 1),
            };

            return potentialLocations.Where(x => IsOpenSpace(x, offset));
        }

        private static string BuildMap((int x, int y) destination, int offset)
        {
            return string.Join(Environment.NewLine, Enumerable.Range(0, destination.y + 10).Select(x => new string(Enumerable.Range(0, destination.x + 10).Select(y => IsOpenSpace((x, y), offset) ? '.' : '#').ToArray())));
        }
    }
}