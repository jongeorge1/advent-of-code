namespace AdventOfCode.Year2024.Day07;

using System;

public static class CalibrationEquationExtensions
{
    public static bool IsValidForPart1(this CalibrationEquation self)
    {
        for (int operatorCombination = 0; operatorCombination < Math.Pow(2, self.Numbers.Length); ++operatorCombination)
        {
            if (self.TestOperatorCombinationForPart1(operatorCombination))
            {
                return true;
            }
        }

        return false;
    }

    public static bool IsValidForPart2(this CalibrationEquation self)
    {
        for (int operatorCombination = 0; operatorCombination < Math.Pow(3, self.Numbers.Length); ++operatorCombination)
        {
            if (self.TestOperatorCombinationForPart2(operatorCombination))
            {
                return true;
            }
        }

        return false;
    }

    private static bool TestOperatorCombinationForPart1(this CalibrationEquation self, int operatorCombination)
    {
        long total = 0;
        for (int i = 0; i < self.Numbers.Length; ++i)
        {
            if ((operatorCombination & (1 << i)) == 0)
            {
                total += self.Numbers[i];
            }
            else
            {
                total *= self.Numbers[i];
            }
        }

        return total == self.TestValue;
    }

    private static bool TestOperatorCombinationForPart2(this CalibrationEquation self, int operatorCombination)
    {
        long total = 0;
        long[] numbersCopy = [.. self.Numbers];
        for (int i = 0; i < self.Numbers.Length; ++i)
        {
            int current = operatorCombination % 3;
            operatorCombination = operatorCombination / 3;

            switch (current)
            {
                case 0:
                    total += self.Numbers[i];
                    break;
                case 1:
                    total *= self.Numbers[i];
                    break;
                case 2:
                    total = long.Parse($"{total}{self.Numbers[i]}");
                    break;
            }
        }

        return total == self.TestValue;
    }
}
