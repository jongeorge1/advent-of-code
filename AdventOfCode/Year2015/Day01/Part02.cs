namespace AdventOfCode.Year2015.Day01
{
    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            int floor = 0;
            for (int i = 0; i < input[0].Length; i++)
            {
                floor += input[0][i] == '(' ? 1 : -1;
                if (floor == -1)
                {
                    return (i + 1).ToString();
                }
            }

            return string.Empty;
        }
    }
}
