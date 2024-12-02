namespace AdventOfCode.Year2016.Day25
{
    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            var computer = new Computer(input, false);

            int startValue = 1;

            do
            {
                computer.Reset();

                int stableCount = 0;
                int? expectedNextValue = null;

                computer.Output = (nextValue) =>
                {
                    if (!expectedNextValue.HasValue && (nextValue == 0 || nextValue == 1))
                    {
                        ++stableCount;
                        expectedNextValue = nextValue == 0 ? 1 : 0;
                    }
                    else if (expectedNextValue.HasValue && nextValue == expectedNextValue)
                    {
                        ++stableCount;
                        expectedNextValue = expectedNextValue == 0 ? 1 : 0;
                    }
                    else
                    {
                        return false;
                    }

                    return stableCount < 10;
                };

                computer.Registers["a"] = startValue;
                computer.Execute();

                if (stableCount == 10)
                {
                    return startValue.ToString();
                }

                ++startValue;
            }
            while (true);
        }
    }
}