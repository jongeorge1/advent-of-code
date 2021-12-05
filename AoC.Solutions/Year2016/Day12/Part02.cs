namespace AoC.Solutions.Year2016.Day12
{
    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            var computer = new Computer(input);
            computer.Registers["c"] = 1;
            computer.Execute();

            return computer.Registers["a"].ToString();
        }
    }
}