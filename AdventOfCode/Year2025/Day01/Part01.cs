namespace AdventOfCode.Year2025.Day01;

using System;
using AdventOfCode;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        const int max = 100;
        int current = 50;
        int password = 0;

        foreach (string line in input)
        {
            int number = int.Parse(line[1..]);

            if (line[0] == 'L')
            {
                current -= number;
            }
            else if (line[0] == 'R')
            {
                current += number;
            }

            if (current % max == 0)
            {
                password++;
            }
        }

        return password.ToString();
    }
}
