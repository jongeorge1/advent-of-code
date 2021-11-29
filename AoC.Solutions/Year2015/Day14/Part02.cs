namespace AoC.Solutions.Year2015.Day14
{
    using System;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            Reindeer[] reindeer = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(x => new Reindeer(x)).ToArray();

            var scores = reindeer.ToDictionary(x => x, _ => 0);

            for (int i = 1; i <= 2503; i++)
            {
                scores[reindeer.OrderByDescending(x => x.GetDistanceAfterTime(i)).First()]++;
            }

            return scores.Values.Max().ToString();
        }
    }
}
