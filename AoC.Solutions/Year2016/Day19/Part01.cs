namespace AoC.Solutions.Year2016.Day19
{
    using AoC.Solutions.Helpers;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            uint presentCount = uint.Parse(input);

            // This is a case of something called the Josephus problem -
            // https://en.wikipedia.org/wiki/Josephus_problem:
            // The problem — given the number of people, starting point,
            // direction, and number to be skipped — is to choose the position
            // in the initial circle to avoid execution.
            // There is an explicit solution for our case (shown on the Wikipedia
            // page as k=2.
            uint answer = ~Numeric.HighestOneBit(presentCount * 2) & ((presentCount << 1) | 1);
            return answer.ToString();
        }
    }
}