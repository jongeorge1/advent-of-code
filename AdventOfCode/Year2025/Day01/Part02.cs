namespace AdventOfCode.Year2025.Day01;

using System;
using System.Diagnostics;
using AdventOfCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;

public class Part02 : ISolution
{
    public string Solve(string[] input)
    {
        const int max = 100;
        int current = 50;
        int password = 0;

        foreach (string line in input)
        {
            int number = int.Parse(line[1..]);

            int completeRevolutions = number / max;
            int distanceInCurrentRevolution = number % max;
            bool startedOnZero = current == 0;

            if (line[0] == 'L')
            {
                current -= distanceInCurrentRevolution;
            }
            else if (line[0] == 'R')
            {
                current += distanceInCurrentRevolution;
            }

            // Now work out the necessary increments to the password. Firstly, we'll have passed 0 for every complete revolution.
            password += completeRevolutions;

            // If current is now > max, or < 0, we have crossed 0 during the incomplete revolution - unless we started on 0.
            if (!startedOnZero && (current > max || current < 0))
            {
                password++;
            }

            // Now normalize the current position to see where we've landed.
            current = (current + max) % max;

            // Finally, if we've landed on 0 exactly, we need to account for that too. The only exception is if we started on 0 and moved an exact number of full revolutions.
            if (current == 0 && !(startedOnZero && distanceInCurrentRevolution == 0))
            {
                password++;
            }

            ////Console.WriteLine($"{line}: {current} ({password})");

            ////Debug.Assert(current >= 0, "Current position should never be negative");
        }

        return password.ToString();
    }
}
