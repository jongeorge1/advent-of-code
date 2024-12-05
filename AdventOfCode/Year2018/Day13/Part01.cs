namespace AdventOfCode.Year2018.Day13
{
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            int yOffset = input[0].Length;
            string map = string.Concat(input);

            // Find the minecarts
            List<Minecart> minecarts = Minecart.FindInMap(ref map, yOffset);

            while (true)
            {
                // First, sort the minecarts by their position...
                minecarts = minecarts.OrderBy(x => x.Position).ToList();

                // Now move them all...
                foreach (Minecart current in minecarts)
                {
                    current.Position += current.NextMove;
                    current.SetNextMove(yOffset, map[current.Position]);

                    // Has it crashed?
                    if (current.GetCrashedIntoMinecart(minecarts) != null)
                    {
                        return $"{current.Position % yOffset},{current.Position / yOffset}";
                    }
                }
            }
        }
    }
}
