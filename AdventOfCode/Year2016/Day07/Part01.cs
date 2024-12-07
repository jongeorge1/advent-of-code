namespace AdventOfCode.Year2016.Day07
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class Part01 : ISolution
    {
        private static readonly Regex NotRegex = new Regex(@"\[(\w+)\]", RegexOptions.Compiled);

        public string Solve(string[] input)
        {
            int supportsTlsCount = 0;

            for (int i = 0; i < input.Length; i++)
            {
                // Extract the sections in square brackets
                string row = input[i];
                var notSections = new List<string>();

                MatchCollection matches = NotRegex.Matches(row);

                notSections.AddRange(matches.Select(x => x.Value));

                string[] sections = NotRegex.Replace(row, "~").Split("~");

                bool sectionsContainsAbba = sections.Select(this.ContainsAbba).Aggregate(false, (curr, agg) => curr || agg);
                bool notSectionsContainsAbba = notSections.Select(this.ContainsAbba).Aggregate(false, (curr, agg) => curr || agg);

                bool supportsTls = sectionsContainsAbba && !notSectionsContainsAbba;

                if (supportsTls)
                {
                    supportsTlsCount++;
                }
            }

            return supportsTlsCount.ToString();
        }

        public bool ContainsAbba(string input)
        {
            // console.log(input);
            for (int i = 0; i < input.Length - 3; i++)
            {
                string source = input[i..(i + 2)];
                if (source[0] != source[1])
                {
                    string target = input[(i + 2)..(i + 4)];
                    string search = new string(source.Reverse().ToArray());

                    if (target == search)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
