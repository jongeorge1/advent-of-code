namespace AdventOfCode.Year2016.Day22
{
    using System;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            // Note: the quickest way to solve this is to print the grid and then count it by hand.
            Node[] nodes = input.Skip(2).Select(x => new Node(x)).ToArray();

            int maxX = nodes.Max(x => x.X);
            int maxY = nodes.Max(x => x.Y);

            int minSize = nodes.Min(x => x.Size);

            for (int y = 0; y <= maxY; y++)
            {
                for (int x = 0; x <= maxX; x++)
                {
                    Node node = nodes.First(node => node.X == x && node.Y == y);

                    if (x == maxX && y == 0)
                    {
                        Console.Write(" G ");
                    }
                    else if (x == 0 && y == 0)
                    {
                        Console.Write("(.)");
                    }
                    else if (node.Used == 0)
                    {
                        Console.Write(" _ ");
                    }
                    else if (node.Used > minSize)
                    {
                        Console.Write(" # ");
                    }
                    else
                    {
                        Console.Write(" . ");
                    }
                }

                Console.WriteLine();
            }

            return string.Empty;
        }
    }
}