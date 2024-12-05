namespace AdventOfCode.Year2021.Day08
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            (string[] Inputs, string[] Outputs)[] digits = input.Select(x => x.Split('|'))
                .Select(x => (x[0].Split(' ', StringSplitOptions.RemoveEmptyEntries), x[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)))
                .ToArray();

            // Now we need to narrow down the possibilities for each segment
            IEnumerable<int> outputs = digits.Select(x => Decode(x));

            return outputs.Sum().ToString();
        }

        private static int Decode((string[] Inputs, string[] Outputs) display)
        {
            // The inputs contain an example of every digit. We use this to work out what's what:
            // - The one with 2 characters is the 1
            // - The one with 3 characters is the 7
            // - The one with 4 characters is the 4
            // - The one with 7 characters is the 8
            // - If we then look at the ones with 5 characters (2, 3, 5) and find the one that contains the 2 signals for the number 1, that will be the three.
            // - Recap: we now know 1, 3, 4, 7 and 8.
            // - If we then look at the ones with 6 (0, 6, 9) characters and find the one that doesn't contain the two signals for the number 1, that will be the 6.
            // - Recap: we now know 1, 3, 4, 6, 7 and 8.
            // - We can look at the difference between 6 and 8 to tell us what is the signal for position c.
            // - Since we know c, we can look at the code for 1 to find out f.
            // - We now look at the two remaining digits which both have 5 characters, which are 2 and 5. The one that contains the signal for c, but not f is the two, the other is the 5.
            // - Recap: we now know 1, 2, 3, 4, 5, 6, 7 and 8.
            // - Finally we look at the two remaining with 6 digits, 0 and 9. The one that contains all the signals of 3 is 9. The other is 0.
            // We can then match the outputs against the inputs to determine the code. There's not guarantee that the order of signals in the outputs
            // will match the orders in the inputs, so we sort them before getting started.
            Dictionary<int, string> digits = [];
            Dictionary<char, char> signals = [];

            string[] inputs = display.Inputs.Select(x => string.Concat(x.OrderBy(y => y))).ToArray();
            string[] outputs = display.Outputs.Select(x => string.Concat(x.OrderBy(y => y))).ToArray();

            digits[1] = inputs.First(x => x.Length == 2);
            digits[7] = inputs.First(x => x.Length == 3);
            digits[4] = inputs.First(x => x.Length == 4);
            digits[8] = inputs.First(x => x.Length == 7);
            digits[3] = inputs.First(x => x.Length == 5 && x.Contains(digits[1][0]) && x.Contains(digits[1][1]));
            digits[6] = inputs.First(x => x.Length == 6 && (x.Contains(digits[1][0]) ^ x.Contains(digits[1][1])));

            char signalC = digits[8].First(x => !digits[6].Contains(x));

            digits[2] = inputs.First(x => x.Length == 5 && x != digits[3] && x.Contains(signalC));
            digits[5] = inputs.First(x => x.Length == 5 && x != digits[3] && x != digits[2]);

            digits[9] = inputs.First(x => x.Length == 6 && x != digits[6] && digits[3].All(d => x.Contains(d)));
            digits[0] = inputs.First(x => x.Length == 6 && x != digits[6] && x != digits[9]);

            var digitCodes = digits.ToDictionary(x => string.Concat(x.Value.OrderBy(x => x)), x => x.Key);

            return (digitCodes[string.Concat(outputs[0].OrderBy(x => x))] * 1000) +
                (digitCodes[string.Concat(outputs[1].OrderBy(x => x))] * 100) +
                (digitCodes[string.Concat(outputs[2].OrderBy(x => x))] * 10) +
                digitCodes[string.Concat(outputs[3].OrderBy(x => x))];
        }
    }
}
