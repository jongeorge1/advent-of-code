namespace AdventOfCode.Year2015.Day01
{
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            return (input[0].Count(x => x == '(') - input[0].Count(x => x == ')')).ToString();
        }
    }
}
