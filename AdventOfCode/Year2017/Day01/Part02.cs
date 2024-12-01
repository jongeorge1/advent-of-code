namespace AdventOfCode.Year2017.Day01
{
    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            string data = input[0];
            int length = data.Length;
            int offset = length / 2;
            int total = 0;

            for (int i = 0; i < data.Length; ++i)
            {
                int target = (i + offset) % length;
                if (data[i] == data[target])
                {
                    total += data[i] - '0';
                }
            }

            return total.ToString();
        }
    }
}
