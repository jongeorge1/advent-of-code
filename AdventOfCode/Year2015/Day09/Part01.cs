namespace AdventOfCode.Year2015.Day09
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            var distances = new Dictionary<(string, string), int>();
            var locationsSet = new HashSet<string>();

            foreach (string entry in input)
            {
                string[] components = entry.Split(' ');
                int distance = int.Parse(components[4]);
                locationsSet.Add(components[0]);
                locationsSet.Add(components[2]);
                distances.Add((components[0], components[2]), distance);
                distances.Add((components[2], components[0]), distance);
            }

            // Now we need all the permutations of the hash set.
            var locations = locationsSet.ToList();
            int shortestRouteDistance = int.MaxValue;

            foreach (IList<string> current in Permutate(locations, locations.Count))
            {
                int distance = 0;
                for (int i = 1; i < current.Count; i++)
                {
                    distance += distances[(current[i - 1], current[i])];
                }

                if (distance < shortestRouteDistance)
                {
                    shortestRouteDistance = distance;
                }
            }

            return shortestRouteDistance.ToString();
        }

        private static IEnumerable<IList<T>> Permutate<T>(IList<T> sequence, int count)
        {
            if (count == 1)
            {
                yield return sequence;
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    foreach (IList<T> perm in Permutate(sequence, count - 1))
                    {
                        yield return perm;
                    }

                    RotateRight(sequence, count);
                }
            }
        }

        private static void RotateRight<T>(IList<T> sequence, int count)
        {
            T tmp = sequence[count - 1];
            sequence.RemoveAt(count - 1);
            sequence.Insert(0, tmp);
        }
    }
}
