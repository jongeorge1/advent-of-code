namespace AoC.Solutions.Year2016.Day23
{
    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            bool testMode = input.StartsWith("TEST");

            var computer = new Computer(testMode ? input[4..] : input);

            if (!testMode)
            {
                computer.Registers["a"] = 7;
            }

            computer.Execute();

            return computer.Registers["a"].ToString();
        }
    }
}