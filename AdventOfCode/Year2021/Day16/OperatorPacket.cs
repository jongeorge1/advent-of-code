namespace AdventOfCode.Year2021.Day16
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;

    public class OperatorPacket : Packet
    {
        private readonly Func<long> evaluator;

        public OperatorPacket(ref ReadOnlySpan<char> data)
            : base(ref data)
        {
            var subPackets = new List<Packet>();

            // What type of operator?
            if (data[3..6].Equals(PacketType.Minimum, StringComparison.Ordinal))
            {
                this.evaluator = () => this.SubPackets!.Min(x => x.Evaluate());
            }
            else if (data[3..6].Equals(PacketType.Maximum, StringComparison.Ordinal))
            {
                this.evaluator = () => this.SubPackets!.Max(x => x.Evaluate());
            }
            else if (data[3..6].Equals(PacketType.GreaterThan, StringComparison.Ordinal))
            {
                this.evaluator = () => this.SubPackets![0].Evaluate() > this.SubPackets![1].Evaluate() ? 1 : 0;
            }
            else if (data[3..6].Equals(PacketType.LessThan, StringComparison.Ordinal))
            {
                this.evaluator = () => this.SubPackets![0].Evaluate() < this.SubPackets![1].Evaluate() ? 1 : 0;
            }
            else if (data[3..6].Equals(PacketType.Sum, StringComparison.Ordinal))
            {
                this.evaluator = () => this.SubPackets!.Sum(x => x.Evaluate());
            }
            else if (data[3..6].Equals(PacketType.Product, StringComparison.Ordinal))
            {
                this.evaluator = () => this.SubPackets!.Aggregate(1L, (long product, Packet next) => product * next.Evaluate());
            }
            else if (data[3..6].Equals(PacketType.Equal, StringComparison.Ordinal))
            {
                this.evaluator = () => this.SubPackets![0].Evaluate() == this.SubPackets![1].Evaluate() ? 1 : 0;
            }
            else
            {
                throw new Exception("What?");
            }

            // Length type?
            if (data[6] == '0')
            {
                // Next 15 bits represent the number of bits in the subpackets
                int bitsInSubPackets = Convert.ToInt32(data[7..22].ToString(), 2);
                ReadOnlySpan<char> subPacketData = data[22.. (22 + bitsInSubPackets)];

                while (subPacketData.Length > 0)
                {
                    subPackets.Add(ReadPacketFromBinary(ref subPacketData));
                }

                data = data[(22 + bitsInSubPackets) ..];
            }
            else
            {
                int numberOfSubPackets = Convert.ToInt32(data[7..18].ToString(), 2);
                ReadOnlySpan<char> subPacketData = data[18..];
                for (int i = 0; i < numberOfSubPackets; ++i)
                {
                    subPackets.Add(ReadPacketFromBinary(ref subPacketData));
                }

                data = subPacketData;
            }

            this.SubPackets = subPackets.ToImmutableList();
        }

        public ImmutableList<Packet> SubPackets { get; }

        public override int SumVersionsRecursive()
        {
            return this.Version + this.SubPackets.Sum(x => x.SumVersionsRecursive());
        }

        public override long Evaluate() => this.evaluator();
    }
}
