namespace AoC.Solutions.Year2016.Day23
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