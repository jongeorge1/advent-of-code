namespace AdventOfCode.Year2022.Day15;

using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        int targetRow = 2000000;

        if (MemoryExtensions.Equals(input[0][0..4], "TEST"))
        {
            targetRow = 10;
            input = input[4..];
        }

        List<Sensor> sensors = Sensor.BuildSensors(input);

        // We also need to know how many beacons are already in the target row.
        int beaconsInTargetRow = sensors.Where(x => x.ClosestBeaconLocation.Y == targetRow).Select(x => x.ClosestBeaconLocation).Distinct().Count();

        int coveredRangesTotalSize = sensors.GetCoveredSpaceCountForTargetRow(targetRow);

        return (coveredRangesTotalSize - beaconsInTargetRow).ToString();
    }
}
