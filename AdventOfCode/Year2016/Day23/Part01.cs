namespace AdventOfCode.Year2016.Day23
{
    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            bool testMode = input[0].StartsWith("TEST");

            var computer = new Computer(testMode ? input[1..] : input, true);

            if (!testMode)
            {
                computer.Registers["a"] = 7;
            }

            computer.Execute();

            return computer.Registers["a"].ToString();
        }
    }
}