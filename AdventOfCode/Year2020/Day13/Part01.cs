namespace AdventOfCode.Year2020.Day13
{
    using System;
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            int earliestDepartureTime = int.Parse(input[0]);
            int[] departureIntervals = input[1].Split(',').Where(x => x != "x").Select(int.Parse).ToArray();

            (int ServiceNumber, int MinutesToWait) earliestSuitableDeparture = (0, int.MaxValue);

            foreach (int current in departureIntervals)
            {
                // What is the lowest multiple of the departure interval that's larger than
                // the departure time?
                // Given the answer requires multiplication of the wait time, we can assume
                // that none of the services will depart exactly on our arrival time.
                int departureTime = (1 + (earliestDepartureTime / current)) * current;
                int minutesToWait = departureTime - earliestDepartureTime;
                if (minutesToWait < earliestSuitableDeparture.MinutesToWait)
                {
                    earliestSuitableDeparture = (current, minutesToWait);
                }
            }

            return (earliestSuitableDeparture.ServiceNumber * earliestSuitableDeparture.MinutesToWait).ToString();
        }
    }
}
