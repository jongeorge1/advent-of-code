namespace AoC.Solutions.Year2021.Day10
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AoC.Solutions;

    public class Part02 : ISolution
    {
        private static readonly char[] Openers = new char[] { '(', '[', '{', '<' };

        private static readonly Dictionary<char, char> Closers = new ()
        {
            { '(', ')' },
            { '[', ']' },
            { '{', '}' },
            { '<', '>' },
        };

        private static readonly Dictionary<char, int> CompletionScores = new ()
        {
            { '(', 1 },
            { '[', 2 },
            { '{', 3 },
            { '<', 4 },
        };

        public string Solve(string input)
        {
            string[] rows = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToArray();

            var completionScores = new List<long>();

            foreach (string row in rows)
            {
                var validationStack = new Stack<char>();
                bool isValid = true;

                foreach (char c in row)
                {
                    if (Openers.Contains(c))
                    {
                        validationStack.Push(c);
                    }
                    else
                    {
                        char lastOpener = validationStack.Pop();

                        char expectedCloser = Closers[lastOpener];

                        if (expectedCloser != c)
                        {
                            // This row is invalid, so abandon it and move on.
                            isValid = false;
                            break;
                        }
                    }
                }

                if (isValid)
                {
                    // Is there anything left on the stack? If so, the row is incomplete.
                    long completionScore = 0;
                    while (validationStack.Count > 0)
                    {
                        char current = validationStack.Pop();
                        completionScore *= 5;
                        completionScore += CompletionScores[current];
                    }

                    if (completionScore > 0)
                    {
                        completionScores.Add(completionScore);
                    }
                }
            }

            completionScores.Sort();

            return completionScores[completionScores.Count / 2].ToString();
        }
    }
}
