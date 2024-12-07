namespace AdventOfCode.Year2022.Day15;

using System;
using System.Collections.Generic;

public static class SensorExtensions
{
    public static int GetCoveredSpaceCountForTargetRow(this List<Sensor> sensors, int targetRow)
    {
        List<(int XMin, int XMax)> coveredRangesInTargetRow = [];
        foreach (Sensor sensor in sensors)
        {
            if (sensor.TryGetCoveredXRangeInTargetRow(targetRow, out (int XMin, int XMax)? coveredRange))
            {
                coveredRangesInTargetRow.Add(coveredRange.Value);
            }
        }

        // Now we've got a bunch of (likely) overlapping ranges in the list. We need to merge them into one or more
        // contiguous ranges and count the number of spaces the ranges cover.
        int coveredRangesTotalSize = 0;

        // Sort the ranges into order of minx to make it easy to find the overlaps.
        coveredRangesInTargetRow.Sort((left, right) => left.XMin.CompareTo(right.XMin));

        int startRange = 0;
        while (startRange < coveredRangesInTargetRow.Count)
        {
            if (startRange == coveredRangesInTargetRow.Count - 1)
            {
                coveredRangesTotalSize += GetRangeSize(coveredRangesInTargetRow[startRange]);
            }

            int endRangeIndex = 1;
            int currentXMax = coveredRangesInTargetRow[startRange].XMax;
            while (endRangeIndex < coveredRangesInTargetRow.Count && currentXMax >= coveredRangesInTargetRow[endRangeIndex].XMin)
            {
                currentXMax = int.Max(currentXMax, coveredRangesInTargetRow[endRangeIndex].XMax);
                ++endRangeIndex;
            }

            coveredRangesTotalSize += GetRangeSize((coveredRangesInTargetRow[startRange].XMin, currentXMax));
            startRange = endRangeIndex;
        }

        return coveredRangesTotalSize;
    }

    public static List<(int XMin, int XMax)> GetCoveredRangesForTargetRow(this List<Sensor> sensors, int targetRow, int minimumStart = int.MinValue, int maximumEnd = int.MaxValue)
    {
        List<(int XMin, int XMax)> coveredRangesInTargetRow = [];
        foreach (Sensor sensor in sensors)
        {
            if (sensor.TryGetCoveredXRangeInTargetRow(targetRow, out (int XMin, int XMax)? coveredRange))
            {
                coveredRangesInTargetRow.Add(coveredRange.Value);
            }
        }

        // Now we've got a bunch of (likely) overlapping ranges in the list. We need to merge them into one or more
        // contiguous ranges and count the number of spaces the ranges cover.
        List<(int, int)> coveredRanges = [];

        // Sort the ranges into order of minx to make it easy to find the overlaps.
        coveredRangesInTargetRow.Sort((left, right) => left.XMin.CompareTo(right.XMin));

        int startRangeIndex = 0;
        while (startRangeIndex < coveredRangesInTargetRow.Count)
        {
            (int XMin, int XMax) startRange = coveredRangesInTargetRow[startRangeIndex];

            // If this range is entirely outside our target area, ignore it.
            // Note: after eyeballing the input, it's obvious this will not be needed.
            ////if (startRange.XMax < minimumStart || startRange.XMin > maximumEnd)
            ////{
            ////    Console.WriteLine("OUT OF AREA KLAXON");
            ////    continue;
            ////}

            if (startRangeIndex == coveredRangesInTargetRow.Count - 1)
            {
                coveredRanges.Add(TrimRange(startRange, minimumStart, maximumEnd));
            }

            int endRangeIndex = 1;
            int currentXMax = coveredRangesInTargetRow[startRangeIndex].XMax;
            while (endRangeIndex < coveredRangesInTargetRow.Count && currentXMax >= coveredRangesInTargetRow[endRangeIndex].XMin)
            {
                currentXMax = int.Max(currentXMax, coveredRangesInTargetRow[endRangeIndex].XMax);
                ++endRangeIndex;
            }

            coveredRanges.Add(TrimRange((coveredRangesInTargetRow[startRangeIndex].XMin, currentXMax), minimumStart, maximumEnd));
            startRangeIndex = endRangeIndex;
        }

        return coveredRanges;
    }

    private static int GetRangeSize((int Min, int Max) range)
    {
        return range.Max - range.Min + 1;
    }

    private static (int X, int Y) TrimRange((int Min, int Max) range, int minX, int maxX)
    {
        return (Math.Max(minX, range.Min), Math.Min(maxX, range.Max));
    }
}
