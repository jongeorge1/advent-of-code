namespace AdventOfCode.Year2018.Day08
{
    using System.Linq;

    public static class NodeExtensions
    {
        public static int MetadataChecksum(this Node[] nodes)
        {
            return nodes.Sum(x => x.MetadataChecksum());
        }

        public static int MetadataChecksum(this Node node)
        {
            return node.Metadata.Sum() + node.Children.MetadataChecksum();
        }

        public static int Value(this Node node)
        {
            if (node.Children.Length == 0)
            {
                return node.Metadata.Sum();
            }

            return node.Metadata.Sum(x => (x > 0 && x <= node.Children.Length) ? node.Children[x - 1].Value() : 0);
        }
    }
}
