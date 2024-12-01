namespace AdventOfCode.Year2023.Day06
{
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            long time = long.Parse(input[0][9..].Replace(" ", string.Empty));
            long bestDistance = long.Parse(input[1][9..].Replace(" ", string.Empty));

            long victoryCount = 0;

            // There's no point starting our count at 0. There's a minimum speed we'd have to travel to make the
            // distance if we set off at 0, so we can discount anything below that.
            long minimumSpeed = bestDistance / time;

            for (long holdTime = minimumSpeed; holdTime < time; ++holdTime)
            {
                long travelTime = time - holdTime;
                if (holdTime * travelTime > bestDistance)
                {
                    ++victoryCount;
                }
            }

            return victoryCount.ToString();
        }
    }
}
