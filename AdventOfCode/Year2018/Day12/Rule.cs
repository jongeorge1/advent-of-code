namespace AdventOfCode.Year2018.Day12
{
    using System.Linq;

    public class Rule
    {
        public Rule(bool[] pattern, bool result)
        {
            this.Pattern = pattern;
            this.Result = result;
        }

        public bool[] Pattern { get; set; }

        public bool Result { get; set; }

        public static Rule Parse(string input)
        {
            bool[] pattern = input.Substring(0, 5).Select(x => x == '#').ToArray();
            bool result = input.Last() == '#';

            return new Rule(pattern, result);
        }
    }
}
