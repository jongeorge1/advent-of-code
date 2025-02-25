﻿namespace AdventOfCode.Year2020.Day15
{
    using System;
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            var numbers = input[0]
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            // Store last index of each occurrence in an array
            int[] lastOccurrences = new int[2020];
            ////Array.Fill(lastOccurrences, -1);

            for (int index = 0; index < numbers.Count - 1; index++)
            {
                lastOccurrences[numbers[index]] = index;
            }

            int lastNumber = numbers[^1];

            // Offset -1 to account for 0-based indexing
            for (int lastIndex = numbers.Count - 1; lastIndex < (2020 - 1); lastIndex++)
            {
                int newNumber = 0;

                int previousOccurrenceindex = lastOccurrences[lastNumber];

                if (previousOccurrenceindex != 0 || lastNumber == numbers[0])
                {
                    newNumber = lastIndex - previousOccurrenceindex;
                }

                lastOccurrences[lastNumber] = lastIndex;
                lastNumber = newNumber;
            }

            return lastNumber.ToString();
        }
    }
}
