namespace AdventOfCode.Year2022.Day15;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AdventOfCode.Helpers;

public class Sensor
{
    public Sensor(ReadOnlySpan<char> input)
    {
        // An input row looks like this:
        // 0         1         2         3         4         5         6         7
        // 01234567890123456789012345678901234567890123456789012345678901234567890123456789
        // Sensor at x=2662540, y=1992627: closest beacon is at x=1562171, y=2000000
        input = input[12..];

        int end = input.IndexOf(',');
        int sensorX = int.Parse(input[0..end]);
        input = input[(end + 1)..];

        int start = input.IndexOf('=') + 1;
        end = input.IndexOf(':');
        int sensorY = int.Parse(input[start..end]);
        input = input[(end + 1)..];

        start = input.IndexOf('=') + 1;
        end = input.IndexOf(',');
        int beaconX = int.Parse(input[start..end]);
        input = input[(end + 1)..];

        start = input.IndexOf('=') + 1;
        int beaconY = int.Parse(input[start..]);

        this.SensorLocation = (sensorX, sensorY);
        this.ClosestBeaconLocation = (beaconX, beaconY);

        this.ClosestBeaconDistance = Distance.Manhattan(this.SensorLocation, this.ClosestBeaconLocation);

        this.RangeBoundingBox = (this.SensorLocation.X - this.ClosestBeaconDistance,
            this.SensorLocation.Y - this.ClosestBeaconDistance,
            this.SensorLocation.X + this.ClosestBeaconDistance,
            this.SensorLocation.Y + this.ClosestBeaconDistance);
    }

    public (int X, int Y) SensorLocation { get; }

    public (int X, int Y) ClosestBeaconLocation { get; }

    public int ClosestBeaconDistance { get; }

    public (int XMin, int YMin, int XMax, int YMax) RangeBoundingBox { get; }

    public static List<Sensor> BuildSensors(string[] input)
    {
        List<Sensor> result = [];
        foreach (string line in input)
        {
            result.Add(new Sensor(line));
        }

        return result;
    }

    public bool TryGetCoveredXRangeInTargetRow(int row, [NotNullWhen(true)] out (int XMin, int XMax)? coveredRange)
    {
        int xRangeAtTargetRow = this.ClosestBeaconDistance - Math.Abs(this.SensorLocation.Y - row);

        if (xRangeAtTargetRow < 0)
        {
            coveredRange = null;
            return false;
        }

        coveredRange = (this.SensorLocation.X - xRangeAtTargetRow, this.SensorLocation.X + xRangeAtTargetRow);
        return true;
    }
}
