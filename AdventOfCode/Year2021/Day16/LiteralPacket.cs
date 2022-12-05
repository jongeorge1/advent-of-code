namespace AdventOfCode.Year2021.Day16
{
    using System;
    using System.Text;

    public class LiteralPacket : Packet
    {
        public LiteralPacket(ref ReadOnlySpan<char> data)
            : base(ref data)
        {
            int pos = 6;
            bool last = false;

            StringBuilder value = new ();

            while (!last)
            {
                last = data[pos] == '0';
                value.Append(data[(pos + 1) .. (pos + 5)]);
                pos += 5;
            }

            this.Value = Convert.ToInt64(value.ToString(), 2);

            data = data[pos..];
        }

        public long Value { get; }

        public override long Evaluate() => this.Value;
    }
}
