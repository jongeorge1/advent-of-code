namespace AdventOfCode.Year2018.Day18
{
    using System.Linq;

    public static class MapParser
    {
        public static (char[] Map, int yOffset) Parse(string[] rows)
        {
            int yOffset = rows[0].Length;
            char[] map = rows.SelectMany(x => x.ToCharArray()).ToArray();

            return (map, yOffset);
        }
    }
}
