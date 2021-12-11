namespace AoC.Solutions.Year2021.Day10
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AoC.Solutions;

    public class Part01 : ISolution
    {
        private static readonly char[] Openers = new char[] { '(', '[', '{', '<' };
        private static readonly Dictionary<char, char> Closers = new ()
        {
            { '(', ')' },
            { '[', ']' },
            { '{', '}' },
            { '<', '>' },
        };

        private static readonly Dictionary<char, int> SyntaxErrorScores = new ()
        {
            { ')', 3 },
            { ']', 57 },
            { '}', 1197 },
            { '>', 25137 },
        };

        public string Solve(string input)
        {
            string[] rows = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToArray();

            int syntaxErrorScore = 0;

            foreach (string row in rows)
            {
                var validationStack = new Stack<char>();

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
                            syntaxErrorScore += SyntaxErrorScores[c];
                            break;
                        }
                    }
                }
            }

            return syntaxErrorScore.ToString();
        }
    }
}
