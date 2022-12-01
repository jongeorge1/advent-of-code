namespace AoC.Solutions.Year2021.Day22
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AoC.Solutions;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            string[] rows = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var activeCubes = new HashSet<(int X, int Y, int Z)>();

            foreach (string current in rows)
            {
                string[] instruction = current.Split(new char[] { ' ', '=', '.', ',' }, StringSplitOptions.RemoveEmptyEntries);

                bool on = instruction[0] == "on";
                int minX = int.Parse(instruction[2]);
                int maxX = int.Parse(instruction[3]);
                int minY = int.Parse(instruction[5]);
                int maxY = int.Parse(instruction[6]);
                int minZ = int.Parse(instruction[8]);
                int maxZ = int.Parse(instruction[9]);

                if ((maxX < -50 || minX > 50) || (maxY < -50 || minY > 50) || (maxZ < -50 || minZ > 50))
                {
                    continue;
                }

                for (int x = minX; x <= maxX; ++x)
                {
                    for (int y = minY; y <= maxY; ++y)
                    {
                        for (int z = minZ; z <= maxZ; ++z)
                        {
                            if (on)
                            {
                                activeCubes.Add((x, y, z));
                            }
                            else
                            {
                                activeCubes.Remove((x, y, z));
                            }
                        }
                    }
                }
            }

            return activeCubes.Count.ToString();
        }
    }
}
