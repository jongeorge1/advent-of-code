namespace AdventOfCode.Year2022.Day13
{
    using System;

    public ref struct PacketTokenEnumerator
    {
        private ReadOnlySpan<char> packet;

        public PacketTokenEnumerator(ReadOnlySpan<char> packet)
        {
            this.packet = packet;
            this.Current = default;
        }

        public PacketToken Current { get; private set; }

        public PacketTokenEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            if (this.packet.Length == 0)
            {
                // We're at the end.
                return false;
            }

            if (this.packet[0] == '[' || this.packet[0] == ']')
            {
                this.Current = new PacketToken(this.packet[0..1]);

                // A ']' might be followed by a ',', in which case we need to slice the packet at a different point
                if (this.packet.Length > 1 && this.packet[1] == ',')
                {
                    this.packet = this.packet[2..];
                }
                else
                {
                    this.packet = this.packet[1..];
                }
            }
            else
            {
                // This *should* mean it's a number. Since this is AoC, we're going to take things as they should
                // be, so no big load of error handling... the next token will either be another number, in which case
                // we need to look for a comma, or an end-of-list, in which case we look for a closing bracket.
                int index = 1;
                while (this.packet[index] != ',' && this.packet[index] != ']')
                {
                    index++;
                }

                this.Current = new PacketToken(this.packet[0..index]);

                if (this.packet[index] == ',')
                {
                    this.packet = this.packet[(index + 1)..];
                }
                else
                {
                    this.packet = this.packet[index..];
                }
            }

            return true;
        }
    }
}