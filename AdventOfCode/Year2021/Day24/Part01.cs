namespace AdventOfCode.Year2021.Day24
{
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            var computer = new Computer(input, "58426589716442", true);
            computer.Execute();
            return string.Empty;
        }
    }
}
