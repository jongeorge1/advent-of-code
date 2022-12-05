namespace AdventOfCode.Year2016.Day22
{
    using System;

    public class Node
    {
        public Node(string input)
        {
            string[] components = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            this.Filesystem = components[0];
            this.Size = int.Parse(components[1][0..^1]);
            this.Used = int.Parse(components[2][0..^1]);
            this.Avail = int.Parse(components[3][0..^1]);
            this.UsePc = int.Parse(components[4][0..^1]);

            string[] fileSystemComponents = components[0].Split('-');
            this.X = int.Parse(fileSystemComponents[1].Substring(1));
            this.Y = int.Parse(fileSystemComponents[2].Substring(1));
        }

        public string Filesystem { get; }

        public int X { get; }

        public int Y { get; }

        public int Size { get; }

        public int Used { get; }

        public int Avail { get; }

        public int UsePc { get; }
    }
}
