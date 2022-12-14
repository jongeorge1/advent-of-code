namespace AdventOfCode.Year2022.Day13
{
    using System;

    public readonly ref struct PacketToken
    {
        public PacketToken(ReadOnlySpan<char> token)
        {
            this.Token = token;
        }

        public ReadOnlySpan<char> Token { get; }

        public PacketTokenType TokenType
        {
            get
            {
                return this.Token[0] switch
                {
                    '[' => PacketTokenType.ListStart,
                    ']' => PacketTokenType.ListEnd,
                    _ => PacketTokenType.Number,
                };
            }
        }

        public int AsNumber()
        {
            if (this.TokenType != PacketTokenType.Number)
            {
                throw new InvalidOperationException();
            }

            return int.Parse(this.Token);
        }
    }
}
