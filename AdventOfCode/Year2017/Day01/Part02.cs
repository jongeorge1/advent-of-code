namespace AdventOfCode.Year2017.Day01
{
    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            int length = input.Length;
            int offset = length / 2;
            int total = 0;

            for (int i = 0; i < input.Length; ++i)
            {
                int target = (i + offset) % length;
                if (input[i] == input[target])
                {
                    total += input[i] - '0';
                }
            }

            return total.ToString();
        }
    }
}
