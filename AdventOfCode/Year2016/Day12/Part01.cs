namespace AdventOfCode.Year2016.Day12
{
    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            var computer = new Computer(input);
            computer.Execute();

            return computer.Registers["a"].ToString();
        }
    }
}