namespace AdventOfCode.Year2017.Day03
{
    using AdventOfCode.Helpers;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            (int X, int Y) position = Spiral.GetGridPosition(int.Parse(input[0]));
            return Distance.Manhattan(position).ToString();
        }
    }
}
