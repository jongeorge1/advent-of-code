namespace AdventOfCode.Year2022.Day14
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            Dictionary<(int X, int Y), CaveContent> map = new();

            foreach (string line in input)
            {
                string[] segments = line.Split(new[] { ',', ' ', '-', '>' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < segments.Length - 2; i += 2)
                {
                    (int X, int Y) from = (int.Parse(segments[i]), int.Parse(segments[i + 1]));
                    (int X, int Y) to = (int.Parse(segments[i + 2]), int.Parse(segments[i + 3]));

                    if (from == to)
                    {
                        throw new Exception();
                    }

                    (int X, int Y) direction = from.X == to.X
                        ? (0, from.Y < to.Y ? 1 : -1)
                        : (from.X < to.X ? 1 : -1, 0);

                    (int X, int Y) current = from;
                    while (current != to)
                    {
                        map[current] = CaveContent.Rock;
                        current = (current.X + direction.X, current.Y + direction.Y);
                    }

                    map[current] = CaveContent.Rock;
                }
            }

            int floorLevel = map.Keys.Max(x => x.Y) + 2;
            int sandDropped = 0;

            // Drop sand indefinitely. We'll break out of the loop as soon as we're done.
            while (true)
            {
                ++sandDropped;

                (int X, int Y) currentSandPosition = (500, 0);

                bool sandStopped = false;
                while (!sandStopped)
                {
                    if (currentSandPosition.Y == (floorLevel - 1))
                    {
                        // We're in the space above the floor.
                        map[currentSandPosition] = CaveContent.Sand;
                        sandStopped = true;
                    }
                    else if (!map.ContainsKey((currentSandPosition.X, currentSandPosition.Y + 1)))
                    {
                        // Drop down
                        currentSandPosition = (currentSandPosition.X, currentSandPosition.Y + 1);
                    }
                    else if (!map.ContainsKey((currentSandPosition.X - 1, currentSandPosition.Y + 1)))
                    {
                        // Drop down-left
                        currentSandPosition = (currentSandPosition.X - 1, currentSandPosition.Y + 1);
                    }
                    else if (!map.ContainsKey((currentSandPosition.X + 1, currentSandPosition.Y + 1)))
                    {
                        // Drop down-right
                        currentSandPosition = (currentSandPosition.X + 1, currentSandPosition.Y + 1);
                    }
                    else
                    {
                        // The sand can't move. If we're still at source, we've finished.
                        if (currentSandPosition == (500, 0))
                        {
                            return sandDropped.ToString();
                        }

                        map[currentSandPosition] = CaveContent.Sand;
                        sandStopped = true;
                    }
                }
            }
        }
    }
}
