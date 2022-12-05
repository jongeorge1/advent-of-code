namespace AdventOfCode.Year2018.Day08
{
    using System;

    public class Node
    {
        public int[] Metadata { get; set; } = Array.Empty<int>();

        public Node[] Children { get; set; } = Array.Empty<Node>();
    }
}
