namespace AdventOfCode.Year2022.Day07
{
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            var filesystem = new FileSystem(input);

            return filesystem.AllDirectories.Where(x => x.GetSize() <= 100000).Sum(x => x.GetSize()).ToString();
        }
    }
}
