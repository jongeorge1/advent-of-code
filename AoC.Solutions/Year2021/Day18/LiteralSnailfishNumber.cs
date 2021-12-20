namespace AoC.Solutions.Year2021.Day18
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("{ToString()}")]
    public class LiteralSnailfishNumber : SnailfishNumber
    {
        public LiteralSnailfishNumber(int value, SnailfishNumberPair? parent = null)
            : base(parent)
        {
            this.Value = value;
        }

        public LiteralSnailfishNumber(ref ReadOnlySpan<char> input, SnailfishNumberPair? parent)
            : base(parent)
        {
            int nextComma = input.IndexOf(',');
            int nextClosingBracket = input.IndexOf(']');

            int endIndex = nextComma == -1 || nextClosingBracket == -1 ? Math.Max(nextComma, nextClosingBracket) : Math.Min(nextComma, nextClosingBracket);
            this.Value = int.Parse(input[0..endIndex]);
            input = input[(endIndex + 1) ..];
        }

        public int Value { get; set; }

        public override string ToString() => this.Value.ToString();

        public void ReplaceWithPair(int leftValue, int rightValue)
        {
            var pair = new SnailfishNumberPair(
                new LiteralSnailfishNumber(leftValue),
                new LiteralSnailfishNumber(rightValue),
                this.Parent);

            SnailfishNumberPair? parent = this.Parent;

            if (parent.Left == this)
            {
                parent.Left = pair;
            }
            else
            {
                parent.Right = pair;
            }
        }


        public override SnailfishNumberPair? FindFirstNumberPairToExplode()
        {
            return null;
        }

        public override LiteralSnailfishNumber? FindFirstLiteralNumberToSplit()
        {
            return this.Value > 9 ? this : null;
        }

        public override int Magnitude()
        {
            return this.Value;
        }

        public override SnailfishNumber DeepClone()
        {
            return new LiteralSnailfishNumber(this.Value);
        }
    }
}
