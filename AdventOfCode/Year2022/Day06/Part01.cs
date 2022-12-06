namespace AdventOfCode.Year2022.Day06
{
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            for (int i = 4; i < input.Length; i++)
            {
                if (input[(i - 4)..i].Distinct().Count() == 4)
                {
                    return i.ToString();
                }
            }

            return "Not found";
        }
    }
}
