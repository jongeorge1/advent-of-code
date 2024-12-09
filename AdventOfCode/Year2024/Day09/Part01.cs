namespace AdventOfCode.Year2024.Day09;

using System;
using System.Linq;
using AdventOfCode;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        // Expand input out into an array representing the file system blocks, one entry per block.
        int[] fileSystem = input[0].Chunk(2).SelectMany((input, index) =>
        {
            return (int[])[
                ..Enumerable.Range(0, input[0] - '0').Select(_ => index),
                .. input.Length == 2 ? Enumerable.Range(0, input[1] - '0').Select(_ => -1) : [],
            ];
        }).ToArray();

        int firstEmptySpace = Array.IndexOf(fileSystem, -1);
        int lastFileBlock = Array.FindLastIndex(fileSystem, el => el != -1);
        fileSystem = fileSystem[..(lastFileBlock + 1)];

        while (firstEmptySpace < lastFileBlock)
        {
            fileSystem[firstEmptySpace] = fileSystem[lastFileBlock];
            fileSystem[lastFileBlock] = -1;
            firstEmptySpace = Array.IndexOf(fileSystem, -1, firstEmptySpace + 1);
            lastFileBlock = Array.FindLastIndex(fileSystem, el => el != -1);
            fileSystem = fileSystem[..(lastFileBlock + 1)];
        }

        long checksum = 0;
        for (int i = 0; i < fileSystem.Length; ++i)
        {
            checksum += fileSystem[i] * i;
        }

        return checksum.ToString();
    }
}
