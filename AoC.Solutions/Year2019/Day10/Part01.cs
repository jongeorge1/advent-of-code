namespace AoC.Solutions.Year2019.Day10
{
    using System;
    using System.Drawing;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string data)
        {
            Point[] points = data
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .SelectMany((row, rowNum) => row.ToCharArray().Select((col, colNum) => (new Point(rowNum, colNum), col)))
                .Where(x => x.col == '#')
                .Select(x => x.Item1)
                .ToArray();

            int result = points.Max(
                source => points.Where(t => t != source)
                    .Select(target => Math.Atan2(target.X - source.X, target.Y - source.Y))
                    .Distinct()
                    .Count());

            return result.ToString();
        }
    }
}
