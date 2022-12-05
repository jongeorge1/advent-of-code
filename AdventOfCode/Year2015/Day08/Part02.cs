namespace AdventOfCode.Year2015.Day08
{
    using System;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            string[] entries = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            int totalCodeLength = 0;
            int totalStringLength = 0;

            foreach (string entry in entries)
            {
                totalStringLength += entry.Length;
                totalCodeLength += 2; // For the quotes.

                foreach (char current in entry)
                {
                    if (current == '"' || current == '\\')
                    {
                        totalCodeLength += 2;
                    }
                    else
                    {
                        ++totalCodeLength;
                    }
                }
            }

            return (totalCodeLength - totalStringLength).ToString();
        }
    }
}
