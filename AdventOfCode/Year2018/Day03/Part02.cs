namespace AdventOfCode.Year2018.Day03
{
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            var claims = input.Select(Claim.FromString).ToList();
            var overlaps = claims.ToDictionary(x => x, _ => new List<Claim>());

            foreach (Claim outer in claims)
            {
                foreach (Claim inner in claims)
                {
                    if (outer == inner || overlaps[outer].Contains(inner))
                    {
                        continue;
                    }

                    if (outer.OverlapsWith(inner))
                    {
                        overlaps[outer].Add(inner);
                        overlaps[inner].Add(outer);
                    }
                }
            }

            return overlaps.Keys.Single(x => overlaps[x].Count == 0).Number.ToString();
        }
    }
}
