namespace AdventOfCode.Year2022.Day15
{
    using System.Collections.Generic;
    using System;
    using AdventOfCode;
    using System.Threading.Tasks;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            int maxCoordinate = 4000000;

            if (MemoryExtensions.Equals(input[0..4], "TEST"))
            {
                maxCoordinate = 20;
                input = input[4..];
            }

            List<Sensor> sensors = Sensor.BuildSensors(input);

            long result = 0;

            Parallel.For(0, maxCoordinate, (y, state) =>
            {
                var coveredRanges = sensors.GetCoveredRangesForTargetRow(y, 0, maxCoordinate);

                // Do the covered ranges leave space for a beacon?
                if (coveredRanges.Count == 2)
                {
                    // Yes.
                    result = ((coveredRanges[0].XMax + 1) * 4000000L) + y;
                    state.Stop();
                }
            });

            return result.ToString();
        }
    }
}
