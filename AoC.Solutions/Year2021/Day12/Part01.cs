namespace AoC.Solutions.Year2021.Day12
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AoC.Solutions;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            Dictionary<string, List<string>> pathways = new ();

            // Build a map...
            foreach (string row in input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] caves = row.Split('-');

                if (!pathways.TryGetValue(caves[0], out List<string>? paths))
                {
                    paths = new List<string>();
                    pathways[caves[0]] = paths;
                }

                paths.Add(caves[1]);

                if (!pathways.TryGetValue(caves[1], out paths))
                {
                    paths = new List<string>();
                    pathways[caves[1]] = paths;
                }

                paths.Add(caves[0]);
            }

            // Now we can path find.
            // For each path, we need to retain a list of the visited small caves so we can ensure we don't visit them again.
            // We're finding all routes, not shortest, so we don't need the normal list of visited locations + steps. We just
            // need a count of times we've reached the end.
            // We could do this using breadth or depth first; it doesn't really matter.
            int completeRoutes = 0;
            var queue = new Queue<List<string>>();
            queue.Enqueue(new List<string> { "start" });

            while (queue.Count > 0)
            {
                List<string> current = queue.Dequeue();
                if (current[^1] == "end")
                {
                    ++completeRoutes;
                    continue;
                }

                foreach (string potentialDestination in pathways[current[^1]])
                {
                    if (potentialDestination == potentialDestination.ToLowerInvariant() && current.Contains(potentialDestination))
                    {
                        // It's a small cave and we've been there before.
                        continue;
                    }

                    var newPath = current.ToList();
                    newPath.Add(potentialDestination);
                    queue.Enqueue(newPath);
                }
            }

            return completeRoutes.ToString();
        }
    }
}
