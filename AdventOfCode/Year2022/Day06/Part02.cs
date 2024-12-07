namespace AdventOfCode.Year2022.Day06
{
    using System.Linq;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            for (int i = 14; i < input[0].Length; i++)
            {
                if (input[0][(i - 14)..i].Distinct().Count() == 14)
                {
                    return i.ToString();
                }
            }

            return "Not found";
        }
    }
}
