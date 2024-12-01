namespace AdventOfCode.Year2017.Day01
{
    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            int total = 0;
            string data = input[0];

            for (int i = 0; i < data.Length - 1; ++i)
            {
                if (data[i] == data[i + 1])
                {
                    total += data[i] - '0';
                }
            }

            if (data[^1] == data[0])
            {
                total += data[^1] - '0';
            }

            return total.ToString();
        }
    }
}
