namespace AdventOfCode.Year2021.Day16
{
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            var packet = Packet.ReadPacketFromHex(input);

            return packet.SumVersionsRecursive().ToString();
        }
    }
}
