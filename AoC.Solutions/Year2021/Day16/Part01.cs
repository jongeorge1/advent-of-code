namespace AoC.Solutions.Year2021.Day16
{
    using AoC.Solutions;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            var packet = Packet.ReadPacketFromHex(input);

            return packet.SumVersionsRecursive().ToString();
        }
    }
}
