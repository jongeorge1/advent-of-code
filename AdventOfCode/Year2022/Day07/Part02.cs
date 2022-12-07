namespace AdventOfCode.Year2022.Day07
{
    using System.Linq;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            var filesystem = new FileSystem(input);

            var requiredSize = 30000000 - filesystem.GetFreeSpace();

            return filesystem.AllDirectories.Where(x => x.GetSize() >= requiredSize).Min(x => x.GetSize()).ToString();
        }
    }
}
