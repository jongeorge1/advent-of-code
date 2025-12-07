namespace AdventOfCode.Year2025.Day06;

using System.Collections.Generic;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;

public class Part02 : ISolution
{
    public string Solve(string[] input)
    {
        // This time we're going to keep track of each question as we go. We'll still process the operands independently.
        string[] questionRows = input[0..^1];
        string operandRow = input[^1];
        int columns = questionRows[0].Length;

        List<long> numbersForCurrentQuestion = [];
        char currentOperand = operandRow[0];

        long runningTotal = 0;

        for (int col = 0; col < columns; ++col)
        {
            string column = string.Concat(questionRows.Select(row => row[col])).Trim();

            if (column.Length == 0)
            {
                // We've reached the end of the current question, so process it.
                runningTotal += currentOperand == '+' ? numbersForCurrentQuestion.Sum() : numbersForCurrentQuestion.Product();
                numbersForCurrentQuestion.Clear();
                currentOperand = operandRow[col + 1];
            }
            else
            {
                numbersForCurrentQuestion.Add(long.Parse(column));
            }
        }

        // There'll be one set of numbers we haven't added to the total because there's no trailing space.
        runningTotal += currentOperand == '+' ? numbersForCurrentQuestion.Sum() : numbersForCurrentQuestion.Product();

        return runningTotal.ToString();
    }
}
