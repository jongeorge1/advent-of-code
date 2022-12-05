namespace AdventOfCode.Year2015.Day08
{
    using System;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            string[] entries = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            int totalCodeLength = 0;
            int totalStringLength = 0;

            foreach (string entry in entries)
            {
                totalCodeLength += entry.Length;

                for (int i = 1; i < entry.Length - 1; ++i)
                {
                    if (entry[i] == '\\')
                    {
                        // This is one of the two possible escape sequences
                        if (entry[i + 1] == 'x')
                        {
                            // This is an escaped hex string. Probably.
                            ++totalStringLength;
                            i += 3;
                            continue;
                        }
                        else if (entry[i + 1] == '"' || entry[i + 1] == '\\')
                        {
                            ++totalStringLength;
                            ++i;
                            continue;
                        }
                    }

                    totalStringLength++;
                }
            }

            return (totalCodeLength - totalStringLength).ToString();
        }
    }
}
