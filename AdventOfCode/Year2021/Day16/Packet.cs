namespace AdventOfCode.Year2021.Day16
{
    using System;
    using System.Text;

    public abstract class Packet
    {
        public Packet(ref ReadOnlySpan<char> data)
        {
            this.Version = Convert.ToInt32(data[0..3].ToString(), 2);
        }

        public int Version { get; }

        public static Packet ReadPacketFromHex(string input)
        {
            StringBuilder binaryInput = new(input.Length * 4);

            foreach (char current in input)
            {
                binaryInput.Append(current switch
                {
                    '0' => "0000",
                    '1' => "0001",
                    '2' => "0010",
                    '3' => "0011",
                    '4' => "0100",
                    '5' => "0101",
                    '6' => "0110",
                    '7' => "0111",
                    '8' => "1000",
                    '9' => "1001",
                    'A' => "1010",
                    'B' => "1011",
                    'C' => "1100",
                    'D' => "1101",
                    'E' => "1110",
                    'F' => "1111",
                    _ => throw new Exception("What?"),
                });
            }

            ReadOnlySpan<char> data = binaryInput.ToString().AsSpan();
            return ReadPacketFromBinary(ref data);
        }

        public static Packet ReadPacketFromBinary(ref ReadOnlySpan<char> data)
        {
            if (data[3..6].Equals(PacketType.Literal, StringComparison.Ordinal))
            {
                return new LiteralPacket(ref data);
            }

            return new OperatorPacket(ref data);
        }

        public virtual int SumVersionsRecursive() => this.Version;

        public abstract long Evaluate();
    }
}
