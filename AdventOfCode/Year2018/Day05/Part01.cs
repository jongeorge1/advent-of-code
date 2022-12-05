namespace AdventOfCode.Year2018.Day05
{
    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            string result = PolymerReactor.React(input);
            return result.Length.ToString();
        }
    }
}
