namespace AdventOfCode.Year2022.Day01
{
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            int currentMaxElfLoad = 0;
            int currentElfLoad = 0;

            foreach (string entry in input)
            {
                if (entry.Length == 0)
                {
                    if (currentElfLoad > currentMaxElfLoad)
                    {
                        currentMaxElfLoad = currentElfLoad;
                    }

                    currentElfLoad = 0;
                }
                else
                {
                    currentElfLoad += int.Parse(entry);
                }
            }

            if (currentElfLoad > currentMaxElfLoad)
            {
                currentMaxElfLoad = currentElfLoad;
            }

            return currentMaxElfLoad.ToString();
        }
    }
}
