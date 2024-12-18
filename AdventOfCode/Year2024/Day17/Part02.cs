namespace AdventOfCode.Year2024.Day17;

using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode;

public class Part02 : ISolution
{
    public string Solve(string[] input)
    {
        ThreeBitComputer computer = new(input);

        // The program loops, outputting a digit for each loop. In each loop, the resulting digit is primarily based on the lowest
        // three bits of the input (i.e. what goes in register A). There are some XORs that affect the output, one of them with a value
        // that is also based on the input.
        // At the end of each loop, the value in A is shifted right by 3 bits.
        // This means that we're looking for a number which has (3 * number of outputs) bits.
        // It also means that the highest bits of the input give us the last digits of the output.
        //
        // Knowing this, we can search for possible inputs, three bits at a time. On the first iteration, we look for three-bit numbers
        // that would give us the final digit of the output. For each of those candidates, we then shift them left by three bits and look
        // for candidates between that number and that number + 7 that would give us the final two digits of the output. Then we rinse
        // and repeat until we have candidates that give us the full expected output.
        //
        // The smallest of these is the answer.

        // Since we're starting with the last three bits, our starting candidate is 0 (as this will still be 0 when shifted left by 3).
        long[] candidateSolutions = [0];

        // Note that digit 0 is the final digit of the output.
        for (int digit = 0; digit < computer.Program.Length; digit++)
        {
            candidateSolutions = candidateSolutions.SelectMany(candidate => FindCandidatesNextForDigit(computer, digit, candidate)).ToArray();
        }

        return candidateSolutions.Min().ToString();
    }

    private static IEnumerable<long> FindCandidatesNextForDigit(ThreeBitComputer computer, int digit, long currentCandidate)
    {
        // The candidate we're supplied with is a number that gives us the last (digit - 1) digits of the output. To start with,
        // we shift this left so we can look for the new values.
        long newCandidate = currentCandidate << 3;

        // Prep the expected output. Since the highest bits of the input result in the lowest bits of the answer, we need
        // to check the digits we have against the end of the program.
        int[] expectedOutput = digit == computer.Program.Length - 1 ? computer.Program : computer.Program[(^(digit + 1))..];

        // Now go through the possible values for the next three bits.
        for (long potentialAddition = newCandidate; potentialAddition < newCandidate + 8; ++potentialAddition)
        {
            computer.Reset();
            computer.SetRegister('A', potentialAddition);
            int[] result;

            try
            {
                result = computer.Execute();
            }
            catch (DivideByZeroException)
            {
                // Some inputs result in a divide by zero. We can ignore them.
                continue;
            }

            // Now see if this candidate gave the expected output.
            if (Enumerable.SequenceEqual(result, expectedOutput))
            {
                yield return potentialAddition;
            }
        }
    }
}
