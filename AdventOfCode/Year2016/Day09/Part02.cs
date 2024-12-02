namespace AdventOfCode.Year2016.Day09
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            return this.GetDecompressedLength(input[0]).ToString();
        }

        private long GetDecompressedLength(string input)
        {
            int start = 0;
            int pos = input.IndexOf('(');
            long output = 0;

            while (pos != -1 && pos < input.Length)
            {
                output += pos - start;

                // Find the end index, pull out the numbers
                int end = input.IndexOf(')', pos);
                string commandStr = input[(pos + 1) ..end];
                string[] commandSections = commandStr.Split('x');
                int repeats = int.Parse(commandSections[1]);
                int len = int.Parse(commandSections[0]);

                string target = input.Substring(end + 1, len);

                long decompressedTargetLength = this.GetDecompressedLength(target);

                output += decompressedTargetLength * repeats;

                start = end + len + 1;
                pos = input.IndexOf('(', start);
            }

            if (start < input.Length)
            {
                output += input.Length - start;
            }

            return output;
        }
    }
}
