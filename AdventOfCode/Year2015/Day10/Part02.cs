﻿namespace AdventOfCode.Year2015.Day10
{
    using System.Text;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            string data = input[0].Trim();

            for (int i = 0; i < 50; i++)
            {
                data = LookAndSay(data);
            }

            return data.Length.ToString();
        }

        private static string LookAndSay(string input)
        {
            char current = input[0];
            int count = 1;
            var result = new StringBuilder(500000);

            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] == current)
                {
                    count++;
                }
                else
                {
                    result.Append(count);
                    result.Append(current);

                    current = input[i];
                    count = 1;
                }
            }

            result.Append(count);
            result.Append(current);

            return result.ToString();
        }
    }
}
