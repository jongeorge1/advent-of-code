namespace AdventOfCode.Year2021.Day18
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("{ToString()}")]
    public class SnailfishNumberLiteral : SnailfishNumber
    {
        public SnailfishNumberLiteral(int value, SnailfishNumberPair? parent = null)
            : base(parent)
        {
            this.Value = value;
        }

        public SnailfishNumberLiteral(ref ReadOnlySpan<char> input, SnailfishNumberPair? parent)
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
                new SnailfishNumberLiteral(leftValue),
                new SnailfishNumberLiteral(rightValue),
                this.Parent);

            SnailfishNumberPair? parent = this.Parent;

            if (parent!.Left == this)
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

        public override SnailfishNumberLiteral? FindFirstLiteralNumberToSplit()
        {
            return this.Value > 9 ? this : null;
        }

        public override int Magnitude()
        {
            return this.Value;
        }

        public override SnailfishNumber DeepCopy()
        {
            return new SnailfishNumberLiteral(this.Value);
        }
    }
}
