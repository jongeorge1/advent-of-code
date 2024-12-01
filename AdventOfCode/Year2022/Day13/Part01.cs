namespace AdventOfCode.Year2022.Day13
{
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            int index = 1;
            int sumOfCorrectlyOrderedPacketIndices = 0;

            foreach (string[] pair in input.Chunk(2))
            {
                if (PacketComparer.ComparePackets(pair[0], pair[1]) == PacketTokenComparisonResult.Correct)
                {
                    sumOfCorrectlyOrderedPacketIndices += index;
                }

                ++index;
            }

            return sumOfCorrectlyOrderedPacketIndices.ToString();
        }
    }
}
