namespace AoC.Solutions.Year2016.Day22
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
        }

        public string Filesystem { get; }

        public int Size { get; }

        public int Used { get; }

        public int Avail { get; }

        public int UsePc { get; }
    }
}
