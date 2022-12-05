namespace AdventOfCode.Year2015.Day02
{
    using System;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            return input
                .Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split('x', StringSplitOptions.RemoveEmptyEntries))
                .Select(x => (Length: int.Parse(x[0]), Width: int.Parse(x[1]), Height: int.Parse(x[2])))
                .Sum(x => CalculateRequiredPaper(x.Length, x.Width, x.Height))
                .ToString();
        }

        private static int CalculateRequiredPaper(int length, int width, int height)
        {
            int side1 = length * width;
            int side2 = width * height;
            int side3 = height * length;

            return (2 * side1) + (2 * side2) + (2 * side3) + Math.Min(side1, Math.Min(side2, side3));
        }
    }
}
