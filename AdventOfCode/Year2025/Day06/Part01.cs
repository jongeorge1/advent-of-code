namespace AdventOfCode.Year2025.Day06;

using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        int actualNumbersPerQuestion = input.Length - 1;
        List<List<long>> allQuestions = [];

        for (int row = 0; row < actualNumbersPerQuestion; ++row)
        {
            long[] numbers = InputHelpers.ParseLongArray(input[row], ' ', StringSplitOptions.RemoveEmptyEntries);
            for (int question = 0; question < numbers.Length; ++question)
            {
                if (allQuestions.Count <= question)
                {
                    allQuestions.Add(new List<long>());
                }

                allQuestions[question].Add(numbers[question]);
            }
        }

        string[] operands = input[^1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

        long total = 0;

        for (int question = 0; question < operands.Length; ++question)
        {
            total += operands[question] == "+" ? allQuestions[question].Sum() : allQuestions[question].Product();
        }

        return total.ToString();
    }
}
