namespace AoC.Solutions.Year2020.Day01
{
    using System;
    using System.Collections.Generic;
    using AoC.Solutions;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            ReadOnlySpan<char> newLine = Environment.NewLine.AsSpan();
            var numbers = new List<int>(1000);
            ReadOnlySpan<char> inputSpan = input.AsSpan();

            while (inputSpan.Length > 0)
            {
                int end = inputSpan.IndexOf(newLine, StringComparison.InvariantCultureIgnoreCase);

                ReadOnlySpan<char> current = end == -1 ? inputSpan : inputSpan[0..end];
                numbers.Add(int.Parse(current));

                inputSpan = inputSpan[(end + newLine.Length) ..];
            }

            foreach (int number in numbers)
            {
                int target = 2020 - number;
                if (numbers.Contains(target))
                {
                    return (number * target).ToString();
                }
            }

            return string.Empty;
        }
    }
}
