namespace AoC.Solutions.Year2021.Day20
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AoC.Solutions;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            string[] components = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            bool[] enhancement = components[0].ToCharArray().Select(x => x == '#').ToArray();

            // If both the first and last elements of the enhancement are '#' then the first iteration will
            // turn all "out of range" pixels to lit and they will stay that way. So that must be considered
            // as bad input.
            if (enhancement[0] && enhancement[^1])
            {
                throw new Exception("Bad input");
            }

            var image = components[1..].SelectMany((row, y) => row.ToCharArray().Select((col, x) => ((x, y), col == '#'))).ToDictionary(x => x.Item1, x => x.Item2);

            for (int i = 0; i < 2; ++i)
            {
                int minX = image.Min(x => x.Key.x) - 2;
                int minY = image.Min(x => x.Key.y) - 2;
                int maxX = image.Max(x => x.Key.x) + 2;
                int maxY = image.Max(x => x.Key.y) + 2;

                // There's a gotcha here: if the first character in the enhancement string is '#' and the last is '.',
                // which it's pretty safe to assume will be the case for all non-test inputs, that means that everything
                // "out of range" of the pixels we care about will flip on for every even numbered iteration and off for
                // every odd one. That means that for pixels we're not actively tracking, we have to assume that they
                // are lit on odd iterations and unlit on even ones.
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

            return image.Count(x => x.Value == true).ToString();
        }
    }
}
