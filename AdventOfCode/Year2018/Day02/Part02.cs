namespace AdventOfCode.Year2018.Day02
{
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            foreach (string outer in input)
            {
                foreach (string inner in input)
                {
                    if (outer == inner)
                    {
                        continue;
                    }

                    var differences = Enumerable.Range(0, outer.Length).Where(x => outer[x] != inner[x]).ToList();

                    if (differences.Count == 1)
                    {
                        return outer.Substring(0, differences[0]) + outer.Substring(differences[0] + 1);
                    }
                }
            }

            return "Failed to find a solution";
        }
    }
}
