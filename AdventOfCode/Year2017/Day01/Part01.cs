namespace AdventOfCode.Year2017.Day01
{
    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            int total = 0;

            for (int i = 0; i < input.Length - 1; ++i)
            {
                if (input[i] == input[i + 1])
                {
                    total += input[i] - '0';
                }
            }

            if (input[^1] == input[0])
            {
                total += input[^1] - '0';
            }

            return total.ToString();
        }
    }
}
