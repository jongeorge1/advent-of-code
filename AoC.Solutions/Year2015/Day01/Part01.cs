namespace AoC.Solutions.Year2015.Day01
{
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            return (input.Count(x => x == '(') - input.Count(x => x == ')')).ToString();
        }
    }
}
