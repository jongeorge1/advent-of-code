namespace AdventOfCode.Year2021.Day20
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            string[] components = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            bool[] enhancement = components[0].ToCharArray().Select(x => x == '#').ToArray();

            var image = components[1..].SelectMany((row, y) => row.ToCharArray().Select((col, x) => ((x, y), col == '#'))).ToDictionary(x => x.Item1, x => x.Item2);

            for (int i = 0; i < 50; ++i)
            {
                int minX = image.Min(x => x.Key.x) - 2;
                int minY = image.Min(x => x.Key.y) - 2;
                int maxX = image.Max(x => x.Key.x) + 2;
                int maxY = image.Max(x => x.Key.y) + 2;

                bool defaultForOutOfRangePixels = false;

                if (enhancement[0] && i % 2 == 1)
                {
                    defaultForOutOfRangePixels = true;
                }

                var newImage = new Dictionary<(int, int), bool>();

                for (int y = minY; y <= maxY; ++y)
                {
                    for (int x = minX; x <= maxX; ++x)
                    {
                        string binary = string.Concat(
                            image.GetValueOrDefault((x - 1, y - 1), defaultForOutOfRangePixels) ? '1' : '0',
                            image.GetValueOrDefault((x, y - 1), defaultForOutOfRangePixels) ? '1' : '0',
                            image.GetValueOrDefault((x + 1, y - 1), defaultForOutOfRangePixels) ? '1' : '0',
                            image.GetValueOrDefault((x - 1, y), defaultForOutOfRangePixels) ? '1' : '0',
                            image.GetValueOrDefault((x, y), defaultForOutOfRangePixels) ? '1' : '0',
                            image.GetValueOrDefault((x + 1, y), defaultForOutOfRangePixels) ? '1' : '0',
                            image.GetValueOrDefault((x - 1, y + 1), defaultForOutOfRangePixels) ? '1' : '0',
                            image.GetValueOrDefault((x, y + 1), defaultForOutOfRangePixels) ? '1' : '0',
                            image.GetValueOrDefault((x + 1, y + 1), defaultForOutOfRangePixels) ? '1' : '0');

                        int index = Convert.ToInt32(binary, 2);

                        newImage[(x, y)] = enhancement[index];
                    }
                }

                image = newImage;
            }

            return image.Count(x => x.Value).ToString();
        }
    }
}
