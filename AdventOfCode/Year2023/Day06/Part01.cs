namespace AdventOfCode.Year2023.Day06
{
    using System;
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            int[] times = input[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1..].Select(int.Parse).ToArray();
            int[] bestDistances = input[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1..].Select(int.Parse).ToArray();

            int currentScore = 1;

            for (int i = 0; i < times.Length; ++i)
            {
                currentScore *= this.GetVictoryCount(times[i], bestDistances[i]);
            }

            return currentScore.ToString();
        }

        private int GetVictoryCount(int time, int bestDistance)
        {
            int victoryCount = 0;

            for (int holdTime = 1; holdTime <= time; ++holdTime)
            {
                int travelTime = time - holdTime;
                if (holdTime * travelTime > bestDistance)
                {
                    ++victoryCount;
                }
            }

            return victoryCount;
        }
    }
}
