#nullable disable
namespace AoC.Solutions.Year2018.Day15
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class State
    {
        public State(MapSpace[] map, int yOffset, int maxY)
        {
            this.Map = map;
            this.YOffset = yOffset;
            this.MaxY = maxY;
        }

        public int YOffset { get; set; }

        public MapSpace[] Map { get; set; }

        public int MaxY { get; set; }

        public static State Parse(string input)
        {
            string[] inputRows = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            int yOffset = inputRows[0].Length;
            char[] mapChars = inputRows.SelectMany(x => x.ToCharArray()).ToArray();
            MapSpace[] map = Enumerable.Range(0, mapChars.Length).Select(x => MapSpace.Parse(mapChars[x], x)).ToArray();

            return new State(map, yOffset, inputRows.Length);
        }

        public override string ToString()
        {
            string mapString = string.Concat(this.Map.Select(x => x?.ToString() ?? "#"));
            IEnumerable<string> rows = Enumerable.Range(0, this.Map.Length / this.YOffset).Select(x => mapString.Substring(x * this.YOffset, this.YOffset));
            return string.Join(Environment.NewLine, rows);
        }
    }
}
